<template>
  <v-card
  full-width
  class="mx-auto"
  v-if="lobbies"
  >
    <v-toolbar
      color="light-blue"
      dark
    >

      <v-toolbar-title>Lobbies</v-toolbar-title>

      <v-spacer></v-spacer>

    </v-toolbar>

    <v-list>
      <LobbyListView
        v-for="lobby in lobbies"
        :key="lobby.id"
        :item="lobby"
        @onDelete="deleteLobby"
      />
    </v-list>
  </v-card>
</template>

<script>
import LobbyListView from '../components/LobbyListItem'
import LobbyDataService from "../services/LobbyDataService";
// import PlayerDataService from '../services/PlayerDataService';

export default {
  components:{LobbyListView},
    data: () => ({
        onlyNotFull:false,
        showMenu: false,
        headers:[
            {text:'title',value:'title'},
            {text:'players',value:'player'},
            {text:'',value:''},
        ],
        lobbies:[
        ],
    }),
    created(){
      this.getAll()
    },
    mounted(){
      this.$lobbyHub.$on('lobby-change', this.getAll)
    },
    methods: {
      getAll(){
          LobbyDataService.getAll().then((res)=>{
            this.lobbies = res.data
      })
      },
       deleteLobby(id){
        LobbyDataService.delete(id)
        this.$lobbyHub.lobbyChange()
        const newList =  this.lobbies.filter(lobby=>lobby.id !== id)
        this.lobbies = newList
      },

    }
}
</script>

