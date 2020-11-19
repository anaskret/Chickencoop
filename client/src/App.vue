<template>
<div>
  <amplify-authenticator v-if="!signedIn">
  </amplify-authenticator>

  <v-app v-if="signedIn" id="inspire">
    <v-app-bar
    v-if="isAuth"
      app
      color="white"
      flat
    >
      <v-btn
      @click="$router.push('/lobbies/newlobby')"
      elevation="2"
        color="warning"
        dark
      >Create new lobby
      </v-btn>

      <v-tabs
        centered
        class="ml-n9"
        color="grey darken-1"
      >
        <v-tab
          v-for="link in links"
          :key="link"
        >
          {{ link }}
        </v-tab>
      </v-tabs>

      <amplify-sign-out/>
      
      <v-avatar
      :color="$vuetify.breakpoint.smAndDown ? 'grey darken-1' : 'transparent'"
      size="32"
      >
      <img src="https://scontent.frix7-1.fna.fbcdn.net/v/t1.0-9/52528710_2089406407822992_8364014239874023424_n.jpg?_nc_cat=111&ccb=2&_nc_sid=174925&_nc_ohc=zMExb5ZSWOgAX_Ujk3L&_nc_ht=scontent.frix7-1.fna&oh=910cb44addc2740b2cbd5395530b7d17&oe=5FC1E3AA" alt="">
      </v-avatar>
    </v-app-bar>

    <v-main class="grey lighten-3">
      <v-container>
        <v-row
          justify="center"
        >
          <v-col
            cols="12"
            sm="8"
          >
            <v-sheet

              rounded="lg"
            >
            <router-view></router-view>
              <!--CENTER-->
            </v-sheet>
          </v-col>

        
        </v-row>
      </v-container>
    </v-main>
  </v-app>
</div>
</template>
 
<script>
import {Auth} from 'aws-amplify'
import {AmplifyEventBus} from 'aws-amplify-vue'
import PlayerDataService from "./services/PlayerDataService"

export default {
  name: 'App',
  props:{
    msg: String,
  },
  data(){
    return{
      //signedIn: false,
      isAuth:true,
        links: [
        'Lobbies',
        'Profile', 
      ],
      formFields: [
        {type: 'username'},
        {type: 'password'},
        {type: 'email'}
      ]
    }
  },
  created(){
    this.findUser();

    AmplifyEventBus.$on('authState', info => {
      if(info === 'signedIn'){
        this.findUser();
      }
      else{
        this.$store.state.signedIn = false;
        this.$store.state.user = null;
      }
    })
  },
  computed:{
    signedIn(){
      return this.$store.state.signedIn;
    }
  },
  methods:{
    async findUser(){
      try{
        const user = await Auth.currentAuthenticatedUser();
        //this.$router.push("lobbies")
        this.$store.state.signedIn = true;
        this.$store.state.user = user;

        PlayerDataService.get(user.username).then(res => {
          this.$store.state.playerId = res.data.id;
        });
      }
      catch(err){
        this.$store.state.signedIn = false;
        this.$store.state.user = null;
      }
    }
  }
}
</script>