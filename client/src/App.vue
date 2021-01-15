<template>
  <div>
    <amplify-authenticator v-if="!signedIn"> </amplify-authenticator>

    <v-app id="inspire">
      <ModalIdle v-if="!signedIn && $store.state.showAutoLogoutModal" />

      <v-app-bar v-if="signedIn" app color="white" flat>
        <v-btn
          @click="$router.push('/lobbies/newlobby')"
          elevation="2"
          color="warning"
          dark
          >Create new lobby
        </v-btn>

        <v-tabs centered class="ml-n9" color="grey darken-1">
          <v-tab v-for="link in links" :key="link" @click="changeTab(link)">
            {{ link }}
          </v-tab>
        </v-tabs>

        <v-btn @click="signOut()" elevation="2" color="red" dark
          >Sign Out
        </v-btn>

        <v-avatar
          :color="
            $vuetify.breakpoint.smAndDown ? 'grey darken-1' : 'transparent'
          "
          size="36"
        >
          <img :src="$store.state.avatar" alt="" />
        </v-avatar>
      </v-app-bar>

      <v-main v-if="signedIn" class="grey lighten-3">
        <v-container>
          <v-row justify="center">
            <v-col cols="12" sm="8">
              <v-sheet rounded="lg">
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
import { Auth } from "aws-amplify";
import { AmplifyEventBus } from "aws-amplify-vue";
import PlayerDataService from "./services/PlayerDataService";
import ModalIdle from "@/components/ModalIdle";

export default {
  components: {
    ModalIdle
  },
  name: "App",
  props: {
    msg: String
  },
  data() {
    return {
      //is user authorized
      isAuth: true,
      //tab elements
      links: ["Lobbies", "Profile", "Ranking"],
      /*formFields: [
        { type: "username" },
        { type: "password" },
        { type: "email" }
      ]*/
    };
  },
  created() {
    PlayerDataService.checkForUpdate(); //check for new players
    this.findUser();

    AmplifyEventBus.$on("authState", info => {
      if (info === "signedIn") {
        this.findUser();
      } else {
        this.$store.state.signedIn = false;
        this.$store.state.user = null;
        this.$store.state.playerId = null;
      }
    });
  },
  computed: {
    signedIn() {
      return this.$store.state.signedIn;
    },
    isAppIdle() {
      return this.$store.state.idleVue.isIdle;
    }
  },
  watch: {
    isAppIdle(newS, oldS) {//Check if the app is idle, if it is log out the user after given time
      if (this.signedIn) {
        if (newS !== oldS) {
          console.log("Signed out");
          this.$store.state.showAutoLogoutModal = true;
          this.signOut();
        }
      }
    }
  },
  methods: {
    //find cognito user and authenticate him
    async findUser() {
      try {
        const user = await Auth.currentAuthenticatedUser();
        this.$store.state.signedIn = true;
        this.$store.state.user = user;

        //save in the app atabase that the player is online
        await PlayerDataService.get(user.username).then(res => {
          this.$store.state.playerId = res.data.id;
          this.$store.state.avatar = res.data.avatarUrl;
          this.$store.state.background = res.data.backgroundUrl;
        });
        let data = {
          Nickname: user.username,
          avatarUrl: this.$store.state.avatarUrl,
          backgroundUrl: this.$store.state.backgroundUrl,
          IsOnline: true
        };
        console.log(data);
        console.log(this.$store.state.playerId);
        PlayerDataService.checkForUpdate();
        PlayerDataService.update(this.$store.state.playerId, data);
      } catch (err) {
        //if failed to sign in, make sure user is signed out
        this.$store.state.signedIn = false;
        this.$store.state.user = null;
      }
    },
    //sign out the user
    async signOut() {
      try {
        await Auth.signOut();

        //update the app database that the user is offline
        let data = {
          Nickname: this.$store.state.user.username,
          avatarUrl: this.$store.state.avatar,
          backgroundUrl: this.$store.state.background,
          IsOnline: false
        };
        PlayerDataService.update(this.$store.state.playerId, data);

        this.$store.state.signedIn = false;
        this.$store.state.user = null;
        this.$store.state.playerId = null;
      } catch (error) {
        console.log("error signing out: ", error);
      }
    },
    //change currently selected tab (lobbies, profile, ranking)
    changeTab(name) {
      if (name == "Profile") {
        let id = this.$store.state.playerId;
        this.$router.push({ name: name, params: { id: id } });
      } else this.$router.push({ name: name });
    }
  }
};
</script>

<style>
amplify-sign-out {
  border-radius: 25px;
}
</style>
