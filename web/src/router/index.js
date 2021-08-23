import Vue from "vue";
import Router from "vue-router";
import BasePages from "./base-pages";

//Pages
const TheContainer = () => import("@/components/containers/TheContainer");
const Usuario = () => import("@/views/Usuario/Usuario");
const NovoUsuario = () => import("@/views/Usuario/NovoUsuario");
const Projeto = () => import("@/views/Projeto/Projeto");
const NovoProjeto = () => import("@/views/Projeto/NovoProjeto");

Vue.use(Router);

export default new Router({
  mode: "hash",
  linkActiveClass: "open active",
  scrollBehavior: () => ({
    y: 0
  }),
  routes: [
    {
      path: "/",
      name: "Home",
      component: TheContainer,
      meta: {
        requiresAuth: true
      },
      children: [
        //Cadastro de usuário
        {
          path: "usuario",
          name: "Usuário",
          // component: Usuario,
          meta: {
            requiresAuth: true,
            permission: true
          },
          component: {
            render(c) {
              return c("router-view");
            }
          },
          children: [
            {
              path: "",
              component: Usuario,
              meta: {
                requiresAuth: true,
                permission: true
              }
            },
            {
              path: "editar/:id",
              name: "Editar",
              component: NovoUsuario,
              meta: {
                requiresAuth: true,
                permission: true
              }
            },
            {
              path: "novo",
              name: "Novo",
              component: NovoUsuario,
              meta: {
                requiresAuth: true,
                permission: true
              }
            }
          ]
        },

        //Cadastro de Projeto
        {
          path: "projeto",
          name: "Projeto",
          meta: {
            requiresAuth: true,
            permission: true
          },
          component: {
            render(c) {
              return c("router-view");
            }
          },
          children: [
            {
              path: "",
              component: Projeto,
              meta: {
                requiresAuth: true,
                permission: true
              }
            },
            {
              path: "editar/:id",
              name: "Editar",
              component: NovoProjeto,
              meta: {
                requiresAuth: true,
                permission: true
              }
            },
            {
              path: "novo",
              name: "Novo",
              component: NovoProjeto,
              meta: {
                requiresAuth: true,
                permission: true
              }
            }
          ]
        }
      ]
    },
    BasePages
  ]
});
