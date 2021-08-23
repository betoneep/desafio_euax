<template>
  <CSidebar fixed :minimize="minimize" colorScheme="dark" :show.sync="show">
    <b-link class="c-sidebar-brand d-md-down-none router-link-active" to="/">
      <h2>Desafio EUAX</h2>
    </b-link>
    <CRenderFunction flat :content-to-render="MontarMenu()" />
    <CSidebarMinimizer
      class="d-md-down-none"
      @click.native="minimize = !minimize"
    />
  </CSidebar>
</template>

<script>
import Nav from "./_nav";

export default {
  name: "TheSidebar",
  data() {
    return {
      minimize: false,
      show: "responsive"
    };
  },
  mounted() {
    this.$root.$on("toggle-sidebar", () => {
      const sidebarOpened = this.show === true || this.show === "responsive";
      this.show = sidebarOpened ? false : "responsive";
    });
    this.$root.$on("toggle-sidebar-mobile", () => {
      const sidebarClosed = this.show === "responsive" || this.show === false;
      this.show = sidebarClosed ? true : "responsive";
    });
    this.MontarMenu();
  },
  methods: {
    MontarMenu() {
      let menu = [];
      menu.push({
        _name: "CSidebarNav",
        _children: []
      });

      menu[0]._children.push(Nav.Principal);
      menu[0]._children.push(Nav.Projeto);
      menu[0]._children.push(Nav.Configuracao);
      menu[0]._children.push(Nav.Usuarios);

      return menu;
    }
  }
};
</script>

<style lang="scss">
.sidebar-image-logo {
  max-width: 85%;
  max-height: 85%;
  margin: 0.5rem 1rem 0.5rem 1rem;
}

.sidebar-image-small-logo {
  max-width: 85%;
  max-height: 85%;
  margin: 0.5rem 1rem 0.5rem 1rem;
}
</style>
