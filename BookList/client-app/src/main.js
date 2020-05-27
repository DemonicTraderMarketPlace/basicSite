import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store/index';
import VueRouter from "vue-router";
import Vuex from "vuex";

import helloWorld from './components/hello-world';


Vue.config.productionTip = false;
Vue.use(VueRouter);
Vue.use(Vuex);

Vue.component("hello-world", helloWorld);

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');
