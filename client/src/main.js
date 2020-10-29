
import Vue from 'vue';
import App from './App.vue';
import Cell from './components/Cell';
import TicTacToe from './views/TicTacToe';
import Lobby from './views/Lobbies';
import vuetify from '@/plugins/vuetify';
import tictactoehub from './plugins/TicTacToeHub';

Vue.component('tic-tac-toe', TicTacToe);
Vue.component('lobby', Lobby);
Vue.component('cell', Cell);
Vue.use(tictactoehub);

new Vue({
  vuetify,
  el: '#app',
  render: h => h(App)
});