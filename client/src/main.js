
import Vue from 'vue';
import App from './App.vue';
import vuetify from '@/plugins/vuetify';
import router from './router/index';

import Amplify, * as AmplifyModules from 'aws-amplify';
import {AmplifyPlugin} from 'aws-amplify-vue';
import aws_exports from './aws-exports';

import tictactoehub from './plugins/TicTacToeHub';
import lobbyhub from './plugins/LobbyHub';

import Cell from './components/Cell';
import TicTacToe from './views/TicTacToe';
import Lobby from './views/Lobbies';
import NewPlayer from './components/NewPlayer';
import NewLobby from './components/NewLobby';


Amplify.configure(aws_exports);
Vue.use(AmplifyPlugin, AmplifyModules);

Vue.component('tic-tac-toe', TicTacToe);
Vue.component('lobbies', Lobby);
Vue.component('new-player', NewPlayer);
Vue.component('new-lobby', NewLobby);
Vue.component('cell', Cell);
Vue.use(tictactoehub);
Vue.use(lobbyhub);

new Vue({
  vuetify,
  router,
  el: '#app',
  render: h => h(App),
});