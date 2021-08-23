import Vue from "vue";
import BootstrapVue from "bootstrap-vue";
import CoreuiVue from "@coreui/vue";
import { iconsSet as icons } from "./assets/icons/icons.js";
import Axios from "axios";
import Notifications from "vue-notification";
import VSelect from "vue-select";
import "./directives/outsideClickHandler";
import router from "./router";
import store from "./store/index";
import App from "./App.vue";
import HighchartsVue from "highcharts-vue";
import Highcharts from "highcharts";
import VueCurrencyInput from "vue-currency-input";
import VueNumeric from "vue-numeric";
import VueMask from "v-mask";

const pluginOptions = {
  globalOptions: { currency: "BRL" }
};
Vue.use(VueCurrencyInput, pluginOptions);
Vue.use(VueNumeric);
Vue.use(VueMask);

var axiosInstance = Axios.create({
  baseURL: store.getters.baseURL
});

// declare a response interceptor
axiosInstance.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    if (error && error.response && error.response.status === 401) {
      localStorage.removeItem("user-token");
      window.location.reload();
    }
    return Promise.reject(error);
  }
);

Vue.prototype.$http = axiosInstance;
const local = JSON.parse(localStorage.getItem("user-token"));

if (local) {
  Vue.prototype.$http.defaults.headers.common["Authorization"] =
    "bearer " + local.token;
}

Vue.use(CoreuiVue);
Vue.use(BootstrapVue);

Vue.use(Notifications);
Vue.component("v-select", VSelect);
Vue.use(HighchartsVue);
Vue.use(Highcharts);

Vue.config.productionTip = false;

router.beforeEach((to, from, next) => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (!store.getters.isLoggedIn) {
      next("/login");
      return;
    }
    if (store.getters.isLoggedIn) {
      next();
      return;
    }
  } else {
    next();
  }
});

new Vue({
  router,
  store,
  icons,
  render: (h) => h(App)
}).$mount("#app");
