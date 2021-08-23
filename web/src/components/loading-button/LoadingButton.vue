<template>
  <!-- <b-button :disabled="loading" :variant="variant" class="btn-square" @click="click">
    <i class="fa fa-spinner fa-spin" v-show="loading"></i>
    {{text}}
  </b-button>
  -->
  <button :disabled="loading" class="btn">
    <i v-show="loading" class="fa fa-spinner fa-spin"></i>
    {{ text }}
  </button>
</template>

<script>
export default {
  name: "LoadingButton",
  props: {
    text: {
      type: String,
      default: "Executar"
    }
  },
  data() {
    return {
      loading: false
    };
  },
  methods: {
    click() {
      //this.$emit("click");
    },
    unloading() {
      this.loading = false;
    }
  }
};
</script>

<style></style>
