<template>
   <v-list-item v-if="item">
        <v-list-item-content>
          <v-list-item-title v-text="item.title"></v-list-item-title>

          <v-list-item-subtitle>Players:</v-list-item-subtitle>
          <v-list-item-subtitle v-text="player"></v-list-item-subtitle>
          <v-list-item-subtitle v-text="playerTwo"></v-list-item-subtitle>
        </v-list-item-content>

        <v-list-item-action>
          <v-btn 
          color="success"
          success
          :disabled="item.isFull"
          @click="joinLobby(item.id)"
          >
            Join Lobby
          </v-btn>
           <v-btn 
          color="error"
          
          @click="deleteLobby(item.id)"
          >
            Delete
          </v-btn>
        </v-list-item-action>
      </v-list-item>
</template>

<script>
import PlayerDataService from '../services/PlayerDataService';

export default {
  name:"LobbyListItem",
  props:{
      item:{
          type:Object,
          default:()=>({
              id:'id',
          })
      }
  },
  data(){
      return{
          player:'nobody',
          playerTwo: 'nobody'
      }
  },
  created(){
          this.getPlayer()
  },
  methods:{

    joinLobby(id){
        this.$router.push({name: 'TicTacToe', params: {id:id}})
      },
      deleteLobby(){
       this.$emit('onDelete',this.item.id)
      },

    async getPlayer(){
      try{
        const resPlayerOne =  await PlayerDataService.getById(this.item.playerOneId)
        const resPlayerTwo =  await PlayerDataService.getById(this.item.playerTwoId)
        this.player =  resPlayerOne.data.nickname
        this.playerTwo = resPlayerTwo.data.nickname
      }catch(e){
          console.error(e)
      } 
    }
}
}
</script>

<style>

</style>