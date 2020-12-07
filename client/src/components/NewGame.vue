<template>
  <div>
    <v-row justify="center">
      <v-dialog v-model="startNewGame" persistent max-width="790">
        <v-card>
          <v-card-title class="headline justify-center">
            Do you want to play a new game?
          </v-card-title>
          <v-card-actions>
            <v-btn elevation="2" color="red" dark @click="gameDeclined()"
              >Leave</v-btn
            >
            <v-btn elevation="2" color="primary" @click="gameAccepted()"
              >Play</v-btn
            >
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-row>
    <v-row justify="center">
      <v-dialog v-model="awaitAccepting" persistent max-width="790">
        <v-card>
          <v-card-title class="headline">
            Wait for the other player to accept
          </v-card-title>
        </v-card>
      </v-dialog>
    </v-row>
  </div>
</template>

<script>
export default {
  data() {
    return {
      startNewGame: false,
      awaitAccepting: false,
      lobbyId: "id"
    };
  },
  created() {
    this.lobbyId = this.$route.params.id;
    console.log(this.lobbyId);
  },
  mounted() {
    this.$lobbyHub.$on("new-game", this.onNewGame);
    this.$lobbyHub.$on("game-accepted", this.onGameAccepted);
    this.$lobbyHub.$on("opponent-left", this.onOpponentLeft);
  },
  methods: {
    newGame() {
      this.awaitAccepting = true;
      this.$lobbyHub.newGame(this.lobbyId);
    },
    onNewGame() {
      this.startNewGame = true;
    },
    gameAccepted() {
      this.$lobbyHub.gameAccepted(this.lobbyId);
    },
    onGameAccepted() {
      this.startNewGame = false;
      this.awaitAccepting = false;
    },
    gameDeclined() {
      this.$lobbyHub.opponentLeft(this.lobbyId);
      this.$router.push("/lobbies");
    },
    onOpponentLeft() {
      this.awaitAccepting = false;
    }
  }
};
</script>
