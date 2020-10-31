<template>


    <v-card
    full-width
    class="mx-auto"
  >
    <v-toolbar
      color="light-blue"
      dark
    >

      <v-toolbar-title>Lobbies</v-toolbar-title>

      <v-spacer></v-spacer>

    </v-toolbar>

    <v-list>
      <v-list-item
        v-for="lobby in lobbies"
        :key="lobby.id"
      >
        <v-list-item-content>
          <v-list-item-title v-text="lobby.title"></v-list-item-title>

          <v-list-item-subtitle v-text="lobby.player"></v-list-item-subtitle>
        </v-list-item-content>

        <v-list-item-action>
          <v-btn 
          color="success"
          success
          :disabled="lobby.isFull"
          @click="joinLobby(lobby.id)"
          >
            Join Lobby
          </v-btn>
           <v-btn 
          color="error"
          
          @click="deleteLobby(lobby.id)"
          >
            WYJEB
          </v-btn>
        </v-list-item-action>
      </v-list-item>


    </v-list>
  </v-card>
           

</template>

<script>
import LobbyDataService from "../services/LobbyDataService";

export default {
    data: () => ({
        onlyNotFull:false,
        showMenu: false,
        headers:[
            {text:'title',value:'title'},
            {text:'players',value:'player'},
            {text:'',value:''},
        ],
        lobbies:[
        ]
    }),
    created(){
        this.$lobbyHub.$on('lobby-change', this.getAll)
        this.getAll()
    },

    methods: {
        joinLobby(id){
          this.$router.push({name: 'TicTacToe', params: {id:id}})
        },
        deleteLobby(id){
          LobbyDataService.delete(id)
          this.$lobbyHub.lobbyChange()
          const newList =  this.lobbies.filter(lobby=>lobby.id !== id)
          this.lobbies = newList
        },
      getAll(){
          LobbyDataService.getAll().then((res)=>{
          console.log(res.data)
          this.lobbies = res.data
      })
    }
    }
}
</script>

