<template>
  <CDropdown
    in-nav
    class="c-header-nav-items"
    placement="bottom-end"
    add-menu-classes="pt-0"
  >
    <template #toggler>
      <CHeaderNavLink>
        <div class="c-avatar">
          <i class="fas fa-user-cog"></i>
          <!-- <img src="img/avatars/6.png" class="c-avatar-img" title="Perfil" /> -->
        </div>
      </CHeaderNavLink>
    </template>
    <CDropdownHeader tag="div" class="text-center" color="light">
      <strong>{{ $store.getters.getAutenticacao.nome }}</strong>
    </CDropdownHeader>
    <CDropdownDivider />
    <CDropdownItem @click="Sair">
      <i class="fas fa-sign-out-alt mr-2"></i> Sair
    </CDropdownItem>
  </CDropdown>
</template>

<script>
export default {
  name: "PerfilDropdown",
  data() {
    return {};
  },
  methods: {
    Sair() {
      this.$http({
        url: "login/sair/" + this.$store.getters.getAutenticacao.usuarioId,
        method: "GET"
      })
        .then(() => {})
        .catch(() => {})
        .finally(() => {
          this.$store.dispatch("logout").then(() => {
            this.$router.push("/login");
          });
        });
    }
  }
};
</script>

<style scoped>
.c-icon {
  margin-right: 0.3rem;
}
</style>
