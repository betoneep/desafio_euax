import axios from "axios";
import TipoPerfilEnum from "../../../enums/TipoPerfilEnum";
var url = "https://localhost:5001/api/";
if (process.env.NODE_ENV === "development") {
  url = "https://localhost:5001/api/";
}

/* eslint-disable no-unused-vars */
export default {
  state: {
    status: "",
    emptyGuid: "00000000-0000-0000-0000-000000000000",
    baseURL: url,
    homologacao: url.includes("rc-api"),
    token: JSON.parse(localStorage.getItem("user-token")) || ""
  },
  mutations: {
    auth_request(state) {
      state.status = "loading";
    },
    auth_success(state, token) {
      state.status = "success";
      state.token = token;
    },
    logout(state) {
      state.status = "";
      state.token = "";
    }
  },
  actions: {
    login({ commit }, token) {
      return new Promise((resolve, reject) => {
        localStorage.setItem("user-token", JSON.stringify(token));
        commit("auth_request");
        commit("auth_success", token);
        resolve();
      });
    },
    logout({ commit }) {
      return new Promise((resolve, reject) => {
        commit("logout");
        localStorage.removeItem("user-token");
        delete axios.defaults.headers.common["Authorization"];
        resolve();
      });
    },
    setAutenticacao(auth) {
      localStorage.setItem("user-token", JSON.stringify(auth));
      //state.token = auth;
    }
  },
  getters: {
    isLoggedIn: (state) => {
      if (!state.token || !state.token.dataExpiracao) {
        return false;
      }
      var dataExpiracao = new Date(state.token.dataExpiracao);
      var dataAtual = new Date();

      if (dataExpiracao <= dataAtual) return false;

      return true;
    },
    authStatus: (state) => state.status,
    getAutenticacao: (state) => state.token,
    getContaId: (state) => (state.token ? state.token.contaSelecionadaId : ""),
    getUsuarioId: (state) => (state.token ? state.token.usuarioId : ""),
    getPerfil: (state) => (state.token ? state.token.perfil : ""),
    getMensagem: (state) => state.mensagem,
    emptyGuid: (state) => state.emptyGuid,
    baseURL: (state) => state.baseURL,
    homologacao: (state) => state.homologacao,
    isUserSistema: (state) => {
      if (state.token) {
        return state.token.perfil == TipoPerfilEnum.Sistema;
      }
      return false;
    }
  },
  modules: {
    pessoa: {
      state: {
        vm: {}
      },
      mutations: {
        set_pessoa(state, obj) {
          state.vm = obj.vm;
        }
      }
    }
  }
};
