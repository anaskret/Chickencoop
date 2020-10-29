
<template>
  <v-app>
    <v-content>
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
    </v-content>
  </v-app>
</template>

<script>
  import Board from "../components/Board";

  export default {
    data() { 
        return {
      gameOver: false,
      gameOverText: '',
      turn: 'x',
      board: new Board()
    } },

    created(){
      this.$tictactoeHub.$on('opponent-turn-end', this.onTurnChange)
      this.$tictactoeHub.$on('who-won', this.onVictory)
    },

    methods: {
      performMove(x, y) {
        
        if (!this.board.doMove(x, y, 'x')) {
          return;
        }

        this.$tictactoeHub.turnChange(x, y, this.turn)
      
        this.$forceUpdate()

        if (this.board.isGameOver()) {
          this.$tictactoeHub.victory(this.turn)
          this.gameOver = true
          
          this.gameOverText = this.board.playerHas3InARow(this.turn) ? `Player ${this.turn} wins!` : `Draw`
          return;
        }
      },
      
      restart(){
          this.board.resetBoard();
          this.gameOver = false;
          this.gameOverText = '';
          this.turn = 'x';
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