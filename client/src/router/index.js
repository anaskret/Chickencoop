import Vue from "vue";
import VueRouter from "vue-router";
import Lobbies from "../views/Lobbies.vue";
import TicTacToe from "../views/TicTacToe.vue";
import NewLobby from "../components/NewLobby.vue";
import Login from "../views/Login.vue";
Vue.use(VueRouter);

const routes = [
  {
    path: "/lobbies",
    name: "Lobbies",
    component: Lobbies
  },
  {
    path: '/tictactoe/:id',
    name: 'TicTacToe',
    component: TicTacToe
  },
  {
    path: "/lobbies/newlobby",
    name: "NewLobby",
    component: NewLobby
  },
  {
    path: "/login",
    name: "Login",
    component: Login
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;
