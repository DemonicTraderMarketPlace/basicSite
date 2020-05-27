import Vue from 'vue';
import Vuex from 'vuex';
import bookModule from './book-module';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
  },
  actions: {

  },
  getters: {
    },
  modules: {
    books: bookModule,
  },
});
