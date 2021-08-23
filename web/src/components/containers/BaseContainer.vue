<template>
  <div class="c-app">
    <div class="c-wrapper">
      <div class="c-body">
        <main class="c-main">
          <CContainer fluid>
            <div class="row">
              <div class="col-sm-12 col-md-4 col-lg-3 col-xl-2">
                <a href="https://www.euax.com.br" target="_blank">
                  <img
                    src="img/logo-preto.png"
                    height="60"
                    alt="euax Logo"
                  />
                </a>
              </div>
              <div class="col-sm-12 col-md-8 col-lg-9 col-xl-10">
                <h2>
                  Seja bem vindo! Preencha seus dados pessoais abaixo para dar
                  continuidade em seu orçamento.
                </h2>
              </div>
            </div>
            <transition name="fade">
              <router-view />
            </transition>
          </CContainer>
        </main>
      </div>
      <TheFooter />
    </div>
  </div>
</template>

<script>
import TheFooter from "./TheFooter";

export default {
  name: "TheContainer",
  components: {
    TheFooter
  }
};
</script>

<style>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s;
}
.fade-enter,
.fade-leave-to {
  opacity: 0;
}
</style>
