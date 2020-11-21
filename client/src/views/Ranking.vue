<template>
    <v-card
    full-width
    class="mx-auto"
    >
        <v-data-table
        :headers="headers"
        :items="players"
        :items-per-page="10"
        class="elevation-1"
        >
            <template v-slot:[`item.online`]="{ item }">
                <v-icon
                small
                class="mr-2"
                color="green"
                v-if="item.isOnline"
                >
                    mdi-account
                </v-icon>
                <v-icon
                small
                class="mr-2"
                color="red"
                v-if="!item.isOnline"
                >
                    mdi-account
                </v-icon>
            </template>
            <template v-slot:[`item.actions`]="{ item }">
                <v-icon
                small
                class="mr-2"
                @click="goToProfile(item.id)"
                >
                    mdi-account
                </v-icon>
            </template>
        </v-data-table>
    </v-card>
</template>

<script>
import PersonalLeaderboardDataService from '../services/PersonalLeaderboardDataService';
//import { mdiAccessPoint, mdiAccessPointOff } from '@mdi/js';

export default {
    data(){
        return{
            headers: [
                { text: 'Player', value: 'nickname'},
                { text: 'Wins', value: 'wins'},
                { text: "", value: "actions", sortable: false, align:"center", width: "5%" },
                { text: '', value: "online", width: "1%"}
            ],
            players:[
            ],
        }
    },
    created(){
        this.getRanking()
    },
    updated(){
        this.getRanking()
    },
    methods:{
        getRanking(){
            PersonalLeaderboardDataService.getRanking().then(res=>{
                this.players = res.data
            })
        },
        goToProfile(id){
            this.$router.push({name: 'Profile', params: {id:id}})
        }
    }
}
</script>