<template>
  <v-app>
    <v-row justify="center">
      <v-dialog v-model="dialog" persistent max-width="790">
        <v-card>
          <v-card-title class="headline justify-center">
            Wait for an opponent
          </v-card-title>
          <v-card-text class="text-center">
            The game will start when another player joins
          </v-card-text>
          <v-btn
            class="game-against-ai"
            elevation="2"
            color="primary"
            @click="playAgainstTheAi"
          >
            Play against the AI
          </v-btn>
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
        @click="aiOpponent ? onGameAccepted() : this.$refs.newGameComponent.newGame"
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
import PlayerDataService from '../services/PlayerDataService';

export default {
  components: { NewGame },
  data() {
    return {
      dialog: false,
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
      unwantedPlayer: false,
      aiOpponent: false
    };
  },

  created() {
    console.log(this.lobbyId);
    LobbyDataService.get(this.$route.params.id).then(res => {
      if (res.data.isFull) {
        this.unwantedPlayer = true;
        this.$router.push("/lobbies"); // game is full, kick the joining player out
      }
      this.$lobbyHub.joinLobby(this.lobbyId);
      if (
        res.data.playerOneId !== this.$store.state.playerId &&
        res.data.playerTwoId == null
      ) {//if the game has empty slot, and the player is a new one, join the game, and update the lobby in the database
        let data = {
          Title: res.data.title,
          GameName: res.data.gameName,
          PlayerOneId: res.data.playerOneId,
          PlayerTwoId: this.$store.state.playerId,
          IsFull: true
        };
        LobbyDataService.update(this.lobbyId, data);

        this.yourMark = "o"; //set players mark as 'o', as this is playerTwo
        this.$lobbyHub.newPlayer(this.lobbyId, this.$store.state.playerId); //send a signal that a new player has joined the game
      }
    });
  },
  mounted() {
    this.dialog = true;
    this.$tictactoeHub.$on("opponent-turn-end", this.onTurnChange); //await opponents turn end and change it
    this.$tictactoeHub.$on("who-won", this.onVictory); //await signal that a player won the game
    this.$lobbyHub.$on("new-player", this.onNewPlayer); //await for signal that a new player joined the game and that it can start
    this.$lobbyHub.$on("host-change", this.hostChange); //host has left the game
    this.$lobbyHub.$on("game-accepted", this.onGameAccepted); //new game was accepted
    this.$lobbyHub.$on("opponent-left", this.onOpponentLeft); //opponent left the game
    this.$lobbyHub.joinLobby(this.lobbyId); //join signalR group
  },
  methods: {
    performMove(x, y) {
      console.log(this.turn);
      if (this.yourMark != this.turn) return;

      //if the selected move can be performed, perform it, otherwise return and try again
      if (!this.board.doMove(x, y, this.turn)) {
        return;
      }

      this.$forceUpdate(); //ipdate the board

      //check for a possible game over
      if (this.board.isGameOver()) {
        //if the game is over, send a signal and set the game as over
        this.$tictactoeHub.victory(this.turn, this.lobbyId);
        this.gameOver = true;

        this.gameOverText = this.board.playerHas3InARow(this.turn)
          ? `Player ${this.turn} wins!`
          : `Draw`;
        return;
      }

      //turn change
      //if playing against human opponent send turnChange signal
      //otherwise perform AiMove
      if(this.aiOpponent == false){
        this.$tictactoeHub.turnChange(x, y, this.turn, this.lobbyId);
      }else{
        this.turn = 'o';
        this.performAiMove();
      }

      //update the board
      this.$forceUpdate();
    },
    performAiMove() {
      let isMoveIncorrect = false;
      //select random numbers until the move is correct and can be performed
      do {
        let x = Math.floor(Math.random() * 3);
        let y = Math.floor(Math.random() * 3);
        isMoveIncorrect = !this.board.doMove(x, y, this.turn);
      } while(isMoveIncorrect)

      this.$forceUpdate();

      //as before, check for a game over
      if (this.board.isGameOver()) {
        this.$tictactoeHub.victory(this.turn, this.lobbyId);
        this.gameOver = true;

        this.gameOverText = this.board.playerHas3InARow(this.turn)
          ? `Player ${this.turn} wins!`
          : `Draw`;
        return;
      }

      //change turn to players turn
      this.turn = "x";
    },
    restart() { //restart the game
      this.board.resetBoard();
      this.gameOver = false;
      this.gameOverText = "";
      this.turn = "x";
    },
    //change turn and perform opponents move
    onTurnChange({ x, y, player }) {
      if (player == "x") this.board.doMove(x, y, "o");
      else this.board.doMove(x, y, "x");

      this.$forceUpdate();
      this.turn = player;
    },
    //setup the game against the ai
    playAgainstTheAi() {
      this.aiOpponent = true;
      //save opponent in the db as the ai
      LobbyDataService.get(this.$route.params.id).then(res => {
        PlayerDataService.get("AiPlayer").then(playerRes => {
          let data = {
            Title: res.data.title,
            GameName: res.data.gameName,
            PlayerOneId: res.data.playerOneId,
            PlayerTwoId: playerRes.data.id,
            IsFull: true
          };
          LobbyDataService.update(this.lobbyId, data);
        });
      });
      this.dialog = false;
      this.gameStartTime = new Date();
    },
    //victory/draw signal was received
    onVictory({ player }) {
      this.gameOver = true;

      var today = new Date();
      let gameEndTime = today;
      //gets the date on which the game was played
      let gameDate =
        today.getFullYear() +
        "-" +
        (today.getMonth() + 1) +
        "-" +
        today.getDate();

      //checks who won the game
      if (player == this.yourMark && this.board.playerHas3InARow(player)) {
        this.gameOverText = "You won!";
        this.result = "win";
      } else if (player != this.yourMark && this.board.playerHas3InARow(player)) {
        this.gameOverText = "You lost!";
        this.result = "loss";
      } else {
        this.gameOverText = "Draw";
        this.result = "draw";
      }

      //saves data to the database
      LobbyDataService.get(this.lobbyId).then(res => {
        let data = {
          //gets length(time) of the game
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
        //checks which player is who
        if (this.yourMark == "x") {
          data.playerId = res.data.playerOneId;
          data.opponentId = res.data.playerTwoId;
          PersonalLeaderboardDataService.create(data);
        } else {
          data.playerId = res.data.playerTwoId;
          data.opponentId = res.data.playerOneId;
          PersonalLeaderboardDataService.create(data);
        }
        //saves result for an ai, if the game was played against it
        if (this.aiOpponent == true) {
          data.playerId = res.data.playerTwoId;
          data.opponentId = res.data.playerOneId;
          if (this.result == 'win') {
            data.result = 'loss';
          } else if(this.result == 'loss') {
            data.result = 'win';
          }
          console.log(data);
          PersonalLeaderboardDataService.create(data);
        }
      });
    },
    //new player joined the game
    onNewPlayer() {
      this.dialog = false;
      this.gameStartTime = new Date();
    },
    //new game was accepted
    onGameAccepted() {
      this.restart();
      this.dialog = false;
      this.gameStartTime = new Date();
    },
    //opponent left the game
    onOpponentLeft() {
      this.dialog = true;
      this.restart();
    },
    //opponent left aand he was the host
    hostChange() {
      this.dialog = true;
      this.yourMark = "x";
      this.turn = "x";
      console.log("New host");
    }
  },
  //stop listening for game signals and leave the lobby
  beforeDestroy() {
    this.$tictactoeHub.$off("opponent-turn-end", this.onTurnChange);
    this.$tictactoeHub.$off("who-won", this.onVictory);
    this.$lobbyHub.$off("new-player", this.onNewPlayer);
    this.$lobbyHub.$off("host-change", this.onNewPlayer);

    if (this.unwantedPlayer) {
      this.$lobbyHub.leaveLobby(this.lobbyId);
      return;
    }
    //check if leaving player is the host or not
    LobbyDataService.get(this.lobbyId).then(res => {
      if(this.aiOpponent == true){
        LobbyDataService.delete(this.lobbyId);
      }
  
      if (
        (res.data.playerOneId == this.$store.state.playerId &&
        res.data.playerTwoId == null)
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
    //leave the lobby signalR group
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
  top: 45%;
  left: 50%;
  margin-top: 170px;
  margin-left: -65px;
}
.game-against-ai{
  display: block;
  margin-left: auto;
  margin-right: auto;
  bottom: 5px;
}
</style>
