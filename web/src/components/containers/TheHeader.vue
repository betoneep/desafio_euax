<template>
  <CHeader class="no-print" fixed with-subheader light>
    <CToggler
      v-c-emit-root-event:toggle-sidebar-mobile
      in-header
      class="ml-3 d-lg-none"
    />
    <CToggler
      v-c-emit-root-event:toggle-sidebar
      in-header
      class="ml-3 d-md-down-none"
    />
    <CHeaderBrand
      class="mx-auto d-lg-none"
      src="img/coreui-vue-logo.svg"
      width="190"
      height="46"
      alt="CoreUI Logo"
    />
    <CHeaderNav class="d-md-down-none mr-auto"> </CHeaderNav>
    <CHeaderNav class="mr-4">
      <CHeaderNavItem>
        <label class="ml-2">{{ $store.getters.getAutenticacao.apelido }}</label>
      </CHeaderNavItem>
      <PerfilDropdown />
    </CHeaderNav>
  </CHeader>
</template>

<script>
import PerfilDropdown from "./PerfilDropdown";

export default {
  name: "TheHeader",
  components: {
    PerfilDropdown
  }
};
</script>
