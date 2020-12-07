import Vue from "vue";
import App from "./App.vue";
import vuetify from "@/plugins/vuetify";
import router from "./router/index";
import store from "./store/index";

import Amplify, * as AmplifyModules from "aws-amplify";
import Storage from "aws-amplify";
import { AmplifyPlugin } from "aws-amplify-vue";
import awsconfig from "./aws-exports";
Amplify.configure(awsconfig);
Storage.configure({ level: "protected" });
Vue.use(AmplifyPlugin, AmplifyModules);
import "@aws-amplify/ui-vue";
import IdleVue from "idle-vue";

import tictactoehub from "./plugins/TicTacToeHub";
import lobbyhub from "./plugins/LobbyHub";

import Cell from "./components/Cell";
import TicTacToe from "./views/TicTacToe";
import Lobby from "./views/Lobbies";
import Profile from "./views/Profile";
import NewLobby from "./components/NewLobby";
import ModalIdle from "./components/ModalIdle";

Vue.component("tic-tac-toe", TicTacToe);
Vue.component("lobbies", Lobby);
Vue.component("profile", Profile);
Vue.component("new-lobby", NewLobby);
Vue.component("cell", Cell);
Vue.component("idle-user", ModalIdle);
Vue.use(tictactoehub);
Vue.use(lobbyhub);

const eventsHub = new Vue();

Vue.use(IdleVue, {
  eventEmitter: eventsHub,
  store,
  idleTime: 600000,
  startAtIdle: false
});

new Vue({
  vuetify,
  router,
  store,
  el: "#app",
  render: h => h(App)
});
