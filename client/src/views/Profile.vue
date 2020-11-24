<template>
    <v-card
    full-width
    class="mx-auto"
    >
        <v-img height="100%" src="https://cdn.vuetifyjs.com/images/cards/server-room.jpg"></v-img>
        <v-col>
            <v-avatar size="100" style="position:absolute; top: 130px">
                <v-img src="https://cdn.vuetifyjs.com/images/profiles/marcus.jpg"></v-img>
            </v-avatar>
            <v-row>
                <h1 class="nickname">{{this.nickname}}</h1>
                <v-btn
                class="edit-profile"
                elevation="2"
                outlined
                >
                    Edit Profile 
                    <v-icon>
                        mdi-pencil-outline
                    </v-icon>
                </v-btn>
            </v-row>
        </v-col>

        <v-data-table
        :headers="headers"
        :items="records"
        :items-per-page="10"
        class="elevation-1"
        >
            <template v-slot:[`item.actions`]="{ item }">
                <v-icon
                small
                class="mr-2"
                @click="goToProfile(item.opponentId)"
                >
                    mdi-account
                </v-icon>
            </template>
        </v-data-table>
    </v-card>
</template>

<script>
import PersonalLeaderboardDataService from '../services/PersonalLeaderboardDataService'
import PlayerDataService from '../services/PlayerDataService'

export default {
    data(){
        return{
            headers: [
                { text: 'Opponent', value: 'opponentNickname'},
                { text: 'Result', value: 'result'},
                { text: 'Game', value: 'gameName'},
                { text: 'Game Time', value: 'gameTime'},
                { text: 'Game Date', value: 'gameDate'},
                { text: "Actions", value: "actions", sortable: false, align:"center", width: "5%"}
            ],
            records:[
            ],
            nickname: 'nobody'
        }
    },
    created(){
        this.getAllByPlayerId()
        this.getPlayer()
    },
    updated(){
        this.getAllByPlayerId()
        this.getPlayer()
    },
    methods:{
        getAllByPlayerId(){
            PersonalLeaderboardDataService.getAllByPlayerId(this.$route.params.id).then(res => {
                this.records = res.data
            })
        },
        getPlayer(){
            PlayerDataService.getById(this.$route.params.id).then(res=>{
                this.nickname = res.data.nickname
            })
        },
        goToProfile(id){
            this.$router.push({name: 'Profile', params: {id:id}})
            window.location.reload()
        }
    }
}
</script>

<style>
.nickname{
    position: relative;
    left: 10px;
}
.edit-profile{
    position: absolute;
    right: 10px; 
    padding:5px
}
</style>