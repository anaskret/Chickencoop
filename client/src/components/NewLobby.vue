<template>
         <form class="submit-form">
                <div>
                    <v-text-field
                    label="Title"
                    v-model="lobby.title"
                    hide-details="auto"
                    required
                    ></v-text-field>

                    <v-text-field
                    label="Game"
                    id="game"
                    v-model="lobby.game"
                    hide-details="auto"
                    ></v-text-field>

                    <v-btn @click.prevent="saveLobby" class="btn-success">Submit</v-btn>
                </div>
            </form>
</template>

<script>
import LobbyDataService from "../services/LobbyDataService";

export default {
    name:'new-lobby',
    data(){
        return {
            lobby: {
                title: "",
                game: 0
            },
            lobbyId: "",
        };
    },
    methods: {
        saveLobby() {
            var data = {
                Title: this.lobby.title,
                Game: this.lobby.game,
                PlayerOneId: this.$store.state.playerId
            };

            LobbyDataService.create(data)
                .then(response => {
                    this.lobbyId = response.data.id
                    this.$lobbyHub.lobbyChange()
                     this.$router.push({name: 'TicTacToe', params: {id: response.data.id}})
                    })
                .catch(e => {
                    console.log(e);
                })
            

            console.log(this.lobbyId)
        
        },
    }
}
</script>