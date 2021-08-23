<template>
  <div id="app">
    <router-view />
    <notifications
      :ignoreDuplicates="true"
      :width="ObterLargura()"
      position="center top"
    >
      <template slot="body" slot-scope="props">
        <div class="vue-notification-wrapper" @click="props.close">
          <div
            :class="
              'vue-notification-template vue-notification ' + props.item.type
            "
          >
            <div
              v-if="props.item.data && props.item.data.length > 0"
              class="notification-content"
            >
              <div
                v-for="item in props.item.data"
                :key="item.id"
                style="font-size: 22px;"
              >
                {{ item }}
              </div>
            </div>
            <div v-else class="notification-content">
              <div style="font-size: 22px;">Mensagem não disponível.</div>
            </div>
          </div>
        </div>
      </template>
    </notifications>
  </div>
</template>

<script>
export default {
  name: "App",
  components: {},
  methods: {
    ObterLargura() {
      let largura = window.innerWidth;
      if (largura < 576) return "100%";
      if (largura >= 576 && largura < 768) return "70%";
      if (largura >= 768 && largura < 992) return "60%";
      if (largura >= 992 && largura < 1200) return "60%";
      if (largura >= 1200) return "50%";
    }
  }
};
</script>

<style lang="scss">
// @import "/assets/scss/main.scss";
</style>
