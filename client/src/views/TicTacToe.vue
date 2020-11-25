<template>
  <v-app>
    <v-row justify="center">
      <v-dialog v-model="dialog" persistent max-width="790">
        <v-card>
          <v-card-title class="headline">
            Wait for player
          </v-card-title>
          <v-card-text
            >The game will start when another player joins</v-card-text
          >
        </v-card>
      </v-dialog>
    </v-row>

    <NewGame ref="newGameComponent" />

    <v-main>
      <div class="tictactoe-board">
        <h2>You are: {{ this.yourMark }}</h2>
        <div v-for="(n, i) in 3" :key="i">
          <div v-for="(n, j) in 3" :key="j">
            <cell @click="performMove(j, i)" :value="board.cells[j][i]"></cell>
          </div>
        </div>
        <div class="player-turn" v-if="!gameOver">It is {{ turn }} turn</div>
        <div class="game-over-text" v-if="gameOver">
          {{ gameOverText }}
        </div>
      </div>
      <v-btn
        v-if="gameOver"
        class="reset-button"
        color="primary"
        elevation="2"
        @click="this.$refs.newGameComponent.newGame"
        >New Game</v-btn
      >
    </v-main>
  </v-app>
</template>

<script>
import Board from "../components/Board";
import NewGame from "../components/NewGame";
import LobbyDataService from "../services/LobbyDataService";
import PersonalLeaderboardDataService from "../services/PersonalLeaderboardDataService";

export default {
  components: { NewGame },
  data() {
    return {
      dialog: false,
      startNewGame: false,
      awaitAccepting: false,
      gameOver: false,
      gameOverText: "",
      yourMark: "x",
      turn: "x",
      lobbyId: this.$route.params.id,
      board: new Board(),
      gameStartTime: 0,
      gameEndTime: 0,
      result: "nothing",
      unwantedPlayer: false
    };
  },

  created() {
    console.log(this.lobbyId);
    LobbyDataService.get(this.$route.params.id).then(res => {
      if (res.data.isFull) {
        this.unwantedPlayer = true;
        this.$router.push("/lobbies"); // WYPIERDALAJ BO FULL
      }
      this.$lobbyHub.joinLobby(this.lobbyId);
      if (
        res.data.playerOneId !== this.$store.state.playerId &&
        res.data.playerTwoId == null
      ) {
        let data = {
          Title: res.data.title,
          GameName: res.data.gameName,
          PlayerOneId: res.data.playerOneId,
          PlayerTwoId: this.$store.state.playerId,
          IsFull: true
        };
        LobbyDataService.update(this.lobbyId, data);

        this.yourMark = "o";
        this.$lobbyHub.newPlayer(this.lobbyId, this.$store.state.playerId);
      }
    });
  },
  mounted() {
    this.dialog = true;
    this.$tictactoeHub.$on("opponent-turn-end", this.onTurnChange);
    this.$tictactoeHub.$on("who-won", this.onVictory);
    this.$lobbyHub.$on("new-player", this.onNewPlayer);
    this.$lobbyHub.$on("host-change", this.hostChange);
    this.$lobbyHub.$on("game-accepted", this.onGameAccepted);
    this.$lobbyHub.$on("opponent-left", this.onOpponentLeft);
    this.$lobbyHub.joinLobby(this.lobbyId);
    console.log(this.startNewGame);
  },
  methods: {
    performMove(x, y) {
      if (this.yourMark != this.turn) return;

      if (!this.board.doMove(x, y, this.turn)) {
        return;
      }

      this.$tictactoeHub.turnChange(x, y, this.turn, this.lobbyId);

      this.$forceUpdate();

      if (this.board.isGameOver()) {
        this.$tictactoeHub.victory(this.turn, this.lobbyId);
        this.gameOver = true;

        this.gameOverText = this.board.playerHas3InARow(this.turn)
          ? `Player ${this.turn} wins!`
          : `Draw`;
        return;
      }
    },
    restart() {
      this.board.resetBoard();
      this.gameOver = false;
      this.gameOverText = "";
      this.turn = "x";
    },
    onTurnChange({ x, y, player }) {
      if (player == "x") this.board.doMove(x, y, "o");
      else this.board.doMove(x, y, "x");

      this.$forceUpdate();
      this.turn = player;
    },
    onVictory({ player }) {
      this.gameOver = true;

      var today = new Date();
      let gameEndTime = today;
      let gameDate =
        today.getFullYear() +
        "-" +
        (today.getMonth() + 1) +
        "-" +
        today.getDate();

      if (player == this.yourMark && this.board.playerHas3InARow(player)) {
        this.gameOverText = "You won!";
        this.result = "win";
      } else if (!this.board.playerHas3InARow(player)) {
        this.gameOverText = "Draw";
        this.result = "draw";
      } else {
        this.gameOverText = "You lost!";
        this.result = "loss";
      }

      LobbyDataService.get(this.lobbyId).then(res => {
        let data = {
          gameTime:
            gameEndTime.getMinutes() -
            this.gameStartTime.getMinutes() +
            ":" +
            (gameEndTime.getSeconds() - this.gameStartTime.getSeconds()),
          gameDate: gameDate,
          playerId: "id",
          opponentId: "id",
          result: this.result,
          gameName: res.data.gameName
        };
        if (this.yourMark == "x") {
          data.playerId = res.data.playerOneId;
          data.opponentId = res.data.playerTwoId;
          console.log(data);
          PersonalLeaderboardDataService.create(data);
        } else {
          data.playerId = res.data.playerTwoId;
          data.opponentId = res.data.playerOneId;
          console.log(data);
          PersonalLeaderboardDataService.create(data);
        }
      });
    },
    onNewPlayer() {
      this.dialog = false;
      this.gameStartTime = new Date();
    },
    newGame() {
      this.$lobbyHub.newGame(this.lobbyId);
    },
    onNewGame() {
      this.startNewGame = true;
    },
    gameAccepted() {
      this.$lobbyHub.gameAccepted(this.lobbyId);
    },
    onGameAccepted() {
      this.restart();
      this.dialog = false;
      this.gameStartTime = new Date();
    },
    onOpponentLeft() {
      this.dialog = true;
      this.restart();
    },
    hostChange() {
      this.dialog = true;
      this.yourMark = "x";
      this.turn = "x";
      console.log("New host");
    }
  },
  beforeDestroy() {
    this.$tictactoeHub.$off("opponent-turn-end", this.onTurnChange);
    this.$tictactoeHub.$off("who-won", this.onVictory);
    this.$lobbyHub.$off("new-player", this.onNewPlayer);
    this.$lobbyHub.$off("host-change", this.onNewPlayer);

    if (this.unwantedPlayer) {
      this.$lobbyHub.leaveLobby(this.lobbyId);
      return;
    }

    LobbyDataService.get(this.lobbyId).then(res => {
      if (
        res.data.playerOneId == this.$store.state.playerId &&
        res.data.playerTwoId == null
      ) {
        LobbyDataService.delete(this.lobbyId);
      } else if (
        res.data.playerOneId == this.$store.state.playerId &&
        res.data.playerTwoId != null
      ) {
        let data = {
          Title: res.data.title,
          Game: res.data.game,
          PlayerOneId: res.data.playerTwoId,
          PlayerTwoId: null,
          IsFull: false
        };
        LobbyDataService.update(this.lobbyId, data);
        this.$lobbyHub.hostChange(this.lobbyId, data);
      } else {
        this.$lobbyHub.opponentLeft(this.lobbyId);
        let data = {
          Title: res.data.title,
          Game: res.data.game,
          PlayerOneId: res.data.playerOneId,
          PlayerTwoId: null,
          IsFull: false
        };
        LobbyDataService.update(this.lobbyId, data);
      }
    });

    this.$lobbyHub.leaveLobby(this.lobbyId);
  }
};
</script>

<style>
.tictactoe-board {
  display: flex;
  flex-wrap: wrap;
  width: 204px;
  height: 204px;
  position: fixed;
  top: 40%;
  left: 50%;
  margin-top: -50px;
  margin-left: -100px;
}
.game-over-text {
  margin-left: 60px;
}
h2 {
  margin-left: 45px;
}
.player-turn {
  margin-left: 60px;
}
.reset-button {
  position: fixed;
  top: 40%;
  left: 50%;
  margin-top: 170px;
  margin-left: -65px;
}
</style>
