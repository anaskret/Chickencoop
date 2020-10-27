
import Vue from 'vue';
import App from './App.vue';
import TicTacToe from './views/TicTacToe';
import Cell from './components/Cell';
import vuetify from '@/plugins/vuetify';
import tictactoehub from './plugins/TicTacToeHub';

Vue.component('tic-tac-toe', TicTacToe);
Vue.component('cell', Cell);
Vue.use(tictactoehub);

new Vue({
  vuetify,
  el: '#app',
  render: h => h(App)
});