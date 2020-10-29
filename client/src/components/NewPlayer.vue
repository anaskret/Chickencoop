<template>
    <v-app>
            <form class="submit-form">
                <div>
                    <v-text-field
                    label="Nickname"
                    v-model="player.nickname"
                    hide-details="auto"
                    required
                    ></v-text-field>

                    <v-btn @click="savePlayer" class="btn-success">Submit</v-btn>
                </div>
            </form>
    </v-app>
</template>

<script>
import PlayerDataService from "../services/PlayerDataService";

export default {
    name:'new-player',
    data(){
        return {
            player: {
                nickname: ""
            },
        };
    },
    methods: {
        savePlayer() {
            var data = {
                Nickname: this.player.nickname
            };
            
            PlayerDataService.create(data)
                .catch(e => {
                    console.log(e);
                });
        },
    }
}
</script>

<style>
.submit-form{
    width: 300px;
}

.btn-success{
    top: 20px;
    left: 100px;
    margin: auto;
}
</style>