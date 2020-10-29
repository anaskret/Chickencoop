<template>
    <v-app>
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

                    <v-text-field
                    label="Player Id"
                    id="playerOneId"
                    v-model="lobby.playerOneId"
                    hide-details="auto"
                    ></v-text-field>

                    <v-btn @click.prevent="saveLobby()" class="btn-success">Submit</v-btn>
                </div>
            </form>
    </v-app>
</template>

<script>
import LobbyDataService from "../services/LobbyDataService";

export default {
    name:'new-lobby',
    data(){
        return {
            lobby: {
                title: "",
                game: 0,
                playerOneId: ""
            },
            lobbyId: "",
        };
    },
    methods: {
        saveLobby() {
            var data = {
                Title: this.lobby.title,
                Game: this.lobby.game,
                PlayerOneId: this.lobby.playerOneId
            };

            LobbyDataService.create(data)
                .then(response => {
                    this.lobbyId = response.data.id
                    console.log(this.lobbyId)
                    console.log(response.data.id)})
                .catch(e => {
                    console.log(e);
                })
            
            debugger

            console.log(this.lobbyId)

            this.$router.push({name: 'TicTacToe', params: {id: this.lobbyId}})
        },
    }
}
</script>