<template>
  <b-button
    :disabled="TemArquivo() ? null : 'disabled'"
    class="btn btn-secondary mr-2"
    @click="AbrirModal()"
    :title="TemArquivo() ? 'Visualizar arquivos' : 'Nenhum arquivo'"
  >
    <i class="fas fa-download"></i>
    <b-modal
      v-model="abrir"
      title="Arquivo(s)"
      size="sm"
      scrollable
      hide-footer
    >
      <div class="row" v-for="(item, index) in arquivos" :key="index">
        <div class="col-5">
          <a
            class="btn btn-primary mb-1"
            :href="$store.getters.baseURL + urlDownload + item"
            target="_blank"
            >Arquivo {{ index + 1 }}</a
          >
        </div>
        <div class="col-3">
          <button class="btn btn-danger mb-1" @click="Remover(item)">
            Remover
          </button>
        </div>
      </div>
      <div class="row" v-if="!TemArquivo()">
        <div class="col-6">
          <span>Nenhum arquivo...</span>
        </div>
      </div>
    </b-modal>
  </b-button>
</template>

<script>
export default {
  components: {},
  props: {
    arquivos: {
      type: Array,
      default: []
    },
    urlDownload: {
      type: String,
      default: ""
    },
    urlRemover: {
      type: String,
      default: ""
    },
    vinculoId: {
      type: String,
      default: ""
    }
  },
  data() {
    return {
      abrir: false
    };
  },
  mounted() {},
  methods: {
    TemArquivo() {
      if (!this.arquivos || this.arquivos.length <= 0) return false;

      return true;
    },
    AbrirModal() {
      this.abrir = true;
    },
    Remover(id) {
      this.$http({
        url: this.urlRemover + id + "/" + this.vinculoId,
        method: "DELETE"
      })
        .then(() => {
          let indice = this.arquivos.indexOf(id);
          this.arquivos.splice(indice, 1);
          this.$notify({
            data: ["Removido com sucesso."],
            type: "success",
            duration: 5000
          });
        })
        .catch((erro) => {
          this.$notify({
            data: erro.response.data.erros,
            type: "warn",
            duration: 5000
          });
        });
    }
  }
};
</script>
