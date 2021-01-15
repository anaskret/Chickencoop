import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    user: null, //current user
    playerId: null, //currecnt users id
    signedIn: false,
    showAutoLogoutModal: false,
    avatar: null, //avatar photo url
    background: null //background photo url
  },
  mutations: {},
  actions: {},
  modules: {}
});
