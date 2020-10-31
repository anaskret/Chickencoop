
<template>
  <v-app>

<v-row justify="center">
    <v-dialog
      v-model="dialog"
      persistent
      max-width="790"
    >

      <v-card>
        <v-card-title class="headline">
         Wait for player
        </v-card-title>
        <v-card-text>Jak gracz dołaczy gra sie rozpocznie ok?</v-card-text>
      
      </v-card>
    </v-dialog>
  </v-row>

    <v-main>

       

      <div class="tictactoe-board">
        <div v-for="(n, i) in 3" :key="i">
          <div v-for="(n, j) in 3" :key="j">
            <cell @click="performMove(j, i)" :value="board.cells[j][i]"></cell>
          </div>
        </div>
        <div class="player-turn" v-if="!gameOver">
          It is {{ turn }} turn
        </div>
        <div class="game-over-text" v-if="gameOver">
            {{ gameOverText }}
        </div>
      </div>
      <v-btn class="reset-button" color="primary" elevation="6" @click="restart()">Reset</v-btn>
    </v-main>
  </v-app>
</template>

<script>
  import Board from "../components/Board";
import LobbyDataService from "../services/LobbyDataService";

  export default {
    data() { 
        return {
          starGame:false,
          dialog:false,
          gameOver: false,
          gameOverText: '',
          turn: 'x',
          lobbyId: this.$route.params.id,
          board: new Board()
    } },

    created(){
    LobbyDataService.get(this.$route.params.id).then(res=>{
      console.log(res.data.isFull)
      if(res.data.isFull){
        this.$router.push('/lobbies')  // WYPIERDALAJ BO FULL
      }
    })
    },
   mounted(){
 this.dialog = true;
      this.$tictactoeHub.$on('opponent-turn-end', this.onTurnChange)
      this.$tictactoeHub.$on('who-won', this.onVictory)
      this.$lobbyHub.joinLobby(this.lobbyId)
   },
    methods: {
      performMove(x, y) {
        if(!this.startGame) return;
        if (!this.board.doMove(x, y, 'x')) {
          return;
        }

        this.$tictactoeHub.turnChange(x, y, this.turn, this.lobbyId)
      
        this.$forceUpdate()

        if (this.board.isGameOver()) {
          this.$tictactoeHub.victory(this.turn, this.lobbyId)
          this.gameOver = true
          
          this.gameOverText = this.board.playerHas3InARow(this.turn) ? `Player ${this.turn} wins!` : `Draw`
          return;
        }
      },
      
      restart(){
          this.board.resetBoard()
          this.gameOver = false
          this.gameOverText = ''
          this.turn = 'x'
      },

      onTurnChange({x, y, player}){
        
        this.board.doMove(x, y, player)

        this.$forceUpdate();
        this.turn = player;
      },

      onVictory({player}){
        
        this.gameOver = true;

        if(player == this.turn/*change to: currentPlayer*/){
          this.gameOverText = this.board.playerHas3InARow(player) ? 'You won!' : 'Draw'
        }
        else{
          this.gameOverText = 'You lost'
        }
      },
      beforeDestroy (){
        this.$tictactoeHub.$off('opponent-turn-end', this.onTurnChange)
        this.lobbyHub.leaveLobby(this.lobbyId)
        //  TY TO HOST ?  
        
      }
    }
  }
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
  .game-over-text{
    margin-left: 45px;
  }
  .player-turn{
    margin-left: 60px;
  }
  .reset-button{
    position: fixed;
    top: 40%;
    left: 50%;
    margin-top: 170px;
    margin-left: -45px;
  }
</style>