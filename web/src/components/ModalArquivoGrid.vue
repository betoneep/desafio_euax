<template>
  <b-button
    class="btn btn-secondary mr-2"
    @click="AbrirModal()"
    title="Visualizar arquivos"
  >
    <i class="fas fa-download"></i>
    <b-modal
      v-model="abrir"
      title="Arquivo(s)"
      size="md"
      scrollable
      hide-footer
    >
      <div v-if="loading" class="loading-container">
        <RotateSquare
          class="loading-position animated fadeIn"
          size="60px"
        ></RotateSquare>
      </div>
      <b-table
        v-else
        :hover="true"
        responsive
        :items="itens"
        :fields="fields"
        striped
        :per-page="itensPorPagina"
        show-empty
        empty-text="Nenhum arquivo encontrado."
      >
        <template v-slot:empty="scope">
          <h4>{{ scope.emptyText }}</h4>
        </template>
        <template v-slot:cell(acoes)="data">
          <div class="btn-group-sm">
            <a
              class="btn btn-secondary"
              :href="$store.getters.baseURL + 'arquivo/obter/' + data.item.arquivoId"
              target="_blank"
              title="Download arquivo"
              ><i class="fas fa-download"></i
            ></a>
          </div>
        </template>
      </b-table>
      <b-pagination
        v-model="pagina"
        :total-rows="total"
        :per-page="itensPorPagina"
        align="right"
        size="md"
        class="mt-2"
      ></b-pagination>
    </b-modal>
  </b-button>
</template>

<script>
import DocumentoAnexoService from "../servico/DocumentoAnexoService";
export default {
  components: {},
  props: {
    referenciaId: {
      type: String,
      default: ""
    }
  },
  data() {
    return {
      abrir: false,
      loading: false,
      itens: [],
      pagina: 1,
      total: 0,
      itensPorPagina: 20,
      fields: [
        { key: "nome", label: "Nome", sortable: true },
        {
          key: "acoes",
          label: "Ações",
          sortable: false,
          thClass: "center, wd-120-px"
        }
      ]
    };
  },
  watch: {
    pagina: function (val) {
      this.ObterGrid(val);
    }
  },
  methods: {
    AbrirModal() {
      this.abrir = true;
      this.pagina = 1;
      this.total = 0;
      this.itens = [];
      this.ObterGrid(this.pagina);
    },
    ObterGrid(pagina) {
      this.loading = false;
      DocumentoAnexoService.ObterGrid(
        pagina,
        this.itensPorPagina,
        this.referenciaId
      )
        .then((response) => {
          this.loading = false;
          this.itens = response.data.itens;
          this.total = response.data.total;
          this.pagina = response.data.pagina;
          this.itensPorPagina = response.data.itensPorPagina;
        })
        .catch((erro) => {
          this.loading = false;
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
