//#### IR NO ARQUIVO TheSiderbar.vue  E CONFIGURAR O MENU PARA CADA PERFIL ####

const Menu = {
  Principal: {
    _name: "CSidebarNavTitle",
    _children: ["Principal"]
  },
  Projeto: {
    _name: "CSidebarNavItem",
    name: "Projetos",
    to: "/projeto",
    icon: "cil-library"
  },
  Configuracao: {
    _name: "CSidebarNavTitle",
    _children: ["Configurações"]
  },
  Usuarios: {
    _name: "CSidebarNavItem",
    name: "Usuários",
    to: "/usuario",
    icon: "cil-face"
  }
};

export default Menu;
