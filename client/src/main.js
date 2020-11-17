
import Vue from 'vue';
import App from './App.vue';
import vuetify from '@/plugins/vuetify';
import router from './router/index';
import store from './store/index';

import Amplify, * as AmplifyModules from 'aws-amplify';
import { AmplifyPlugin} from 'aws-amplify-vue';
import awsconfig from './aws-exports';
Amplify.configure(awsconfig);
Vue.use(AmplifyPlugin, AmplifyModules);
import '@aws-amplify/ui-vue';

import tictactoehub from './plugins/TicTacToeHub';
import lobbyhub from './plugins/LobbyHub';

import Cell from './components/Cell';
import TicTacToe from './views/TicTacToe';
import Lobby from './views/Lobbies';
import NewLobby from './components/NewLobby';

Vue.component('tic-tac-toe', TicTacToe);
Vue.component('lobbies', Lobby);
Vue.component('new-lobby', NewLobby);
Vue.component('cell', Cell);
Vue.use(tictactoehub);
Vue.use(lobbyhub);

new Vue({
  vuetify,
  router,
  store,
  el: '#app',
  render: h => h(App),
});