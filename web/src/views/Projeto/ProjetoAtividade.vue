<template>
  <div class="animated fadeIn">
    <div v-if="loading" class="loading-container">
      <RotateSquare
        class="loading-position animated fadeIn"
        size="60px"
      ></RotateSquare>
    </div>
    <form v-else @submit="ValidarForm">
      <div class="row">
        <div class="col-sm-12 col-md-12 col-lg-12 col-xl-12">
          <div class="card">
            <header class="card-header" @click="abrir = !abrir">
              <div class="d-flex">
                <strong class="align-self-center">Atividades</strong>
                <small class="ml-2 mt-1">Clique para abrir/esconder</small>

                <i
                  :class="
                    abrir
                      ? 'ml-auto mt-1 fas fa-chevron-up'
                      : 'ml-auto mt-1 fas fa-chevron-down'
                  "
                ></i>
              </div>
            </header>
            <div :class="abrir ? 'collapse-show' : 'collapse'">
              <div class="card-body">
                <div class="row">
                  <div class="col">
                    <div class="form-group">
                      <small
                        >Campos com * são de preenchimento obrigatório</small
                      >
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col-sm-12 col-md-4 col-lg-4 col-xl-4">
                    <div class="form-group">
                      <label for>* Nome</label>
                      <input
                        v-model="viewModel.nome"
                        class="form-control"
                        type="text"
                        placeholder="Digite um nome"
                        required
                      />
                    </div>
                  </div>
                  <div class="col-sm-12 col-md-3 col-lg-3 col-xl-2">
                    <div class="form-group">
                      <label for>* Data Inicial</label>
                      <input
                        v-model="viewModel.dataInicio"
                        class="form-control"
                        type="date"
                        placeholder="Digite a de inicio"
                        required
                      />
                    </div>
                  </div>
                  <div class="col-sm-12 col-md-3 col-lg-3 col-xl-2">
                    <div class="form-group">
                      <label for>* Data Final</label>
                      <input
                        v-model="viewModel.dataFim"
                        class="form-control"
                        type="date"
                        placeholder="Digite a data final"
                        required
                      />
                    </div>
                  </div>
                  <div
                    class="col-sm-6 col-md-2 col-lg-2 col-xl-2"
                    title="Atividade finalizada."
                  >
                    <label for>Finalizada</label>
                    <b-form-checkbox
                      v-model="viewModel.finalizada"
                      name="check-button"
                      switch
                    >
                    </b-form-checkbox>
                  </div>
                </div>
                <div class="btn-toolbar mb-3" role="toolbar">
                  <div class="btn-group" role="group">
                    <button class="btn btn-success mr-2" type="submit">
                      Salvar
                    </button>
                  </div>
                  <div class="btn-group" role="group">
                    <button
                      class="btn btn-secondary"
                      type="reset"
                      @click="Fechar()"
                    >
                      Voltar
                    </button>
                  </div>
                </div>
                <div class="row">
                  <div class="col-12">
                    <b-table
                      :hover="true"
                      responsive
                      :items="itens"
                      :fields="fields"
                      striped
                      :per-page="itensPorPagina"
                      show-empty
                      empty-text="Nenhuma atividade encontrada."
                    >
                      <template v-slot:empty="scope">
                        <h4>{{ scope.emptyText }}</h4>
                      </template>
                      <template v-slot:cell(acoes)="data">
                        <div class="btn-group-sm">
                          <b-button
                            variant="warning"
                            style="margin-right: 10px"
                            title="Editar"
                            @click="Obter(data.item.id)"
                          >
                            <i class="fa fa-edit"></i>
                          </b-button>
                          <b-button
                            variant="info"
                            style="margin-right: 10px"
                            title="Finalizar atividade"
                            @click="FinalizarAtividade(data.item)"
                          >
                            <i class="fas fa-thumbs-up"></i>
                          </b-button>
                          <b-button
                            variant="dark"
                            style="margin-right: 10px"
                            title="Reabrir atividade"
                            @click="ReabrirAtividade(data.item)"
                          >
                            <i class="fas fa-thumbs-down"></i>
                          </b-button>
                          <b-button
                            variant="danger"
                            style="margin-right: 10px"
                            title="Remover"
                            @click="Remover(data.item.id)"
                          >
                            <i class="fas fa-trash-alt"></i>
                          </b-button>
                        </div>
                      </template>
                      <template v-slot:cell(dataInicio)="data">
                        <div class="center">
                          <span>{{ FormatarData(data.item.dataInicio) }}</span>
                        </div>
                      </template>
                      <template v-slot:cell(dataFim)="data">
                        <div class="center">
                          <span>{{ FormatarData(data.item.dataFim) }}</span>
                        </div>
                      </template>
                      <template v-slot:cell(finalizada)="data">
                        <div class="center">
                          <span
                            v-if="data.item.finalizada"
                            class="badge badge-success"
                          >
                            Sim
                          </span>
                          <span v-else class="badge badge-danger">Não</span>
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
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </form>
    <b-modal
      v-model="modalRemover"
      title="Confirmar exclusão"
      class="modal-danger"
      ok-variant="danger"
      @ok="ModalOk"
      @hidden="ModalCancel"
    >
      Você confirma a exclusão da atividade?
    </b-modal>
  </div>
</template>

<script>
import RotateSquare from "../../components/RotateSquare";
import AtividadeServico from "../../servico/AtividadeServico";
import DateTime from "../../util/DateTime";

export default {
  name: "ProjetoAtividade",
  components: {
    RotateSquare,
    AtividadeServico
  },
  props: {
    projetoId: {
      type: String,
      default: ""
    }
  },
  data() {
    return {
      modalRemover: false,
      itemRemover: null,
      loading: false,
      pagina: 1,
      total: 0,
      itensPorPagina: 10,
      itens: [],
      abrir: true,
      fields: [
        { key: "nome", label: "Nome", sortable: true },
        { key: "dataInicio", label: "Data Inicial", sortable: true },
        { key: "dataFim", label: "Data Final", sortable: true },
        { key: "finalizada", label: "Finalizada", sortable: true },
        {
          key: "acoes",
          label: "Ações",
          sortable: false,
          thClass: "center, wd-120-px"
        }
      ],
      viewModel: {
        id: this.$store.getters.emptyGuid,
        projetoId: this.projetoId,
        nome: "",
        dataInicio: "",
        dataFim: "",
        finalizada: false
      }
    };
  },
  mounted() {
    this.ObterGrid(1);
  },
  watch: {
    pagina: function (val) {
      this.ObterGrid(val);
    }
  },
  created() {},
  methods: {
    IsNovo() {
      return this.projetoId === this.$store.getters.emptyGuid;
    },
    ValidarForm(evt) {
      evt.preventDefault();

      if (this.viewModel.id !== this.$store.getters.emptyGuid) this.Editar();
      else this.Novo();
    },
    Obter(id) {
      this.loading = false;
      AtividadeServico.Obter(id)
        .then((resposta) => {
          this.loading = false;
          resposta.data.dataInicio = DateTime.formatar(
            resposta.data.dataInicio
          );
          resposta.data.dataFim = DateTime.formatar(resposta.data.dataFim);

          this.viewModel = resposta.data;
        })
        .catch((erro) => {
          this.loading = false;
          this.$notify({
            data: erro.response.data.erros,
            type: "warn",
            duration: 2000
          });
        });
    },
    ObterGrid(pagina) {
      this.loading = false;
      AtividadeServico.ObterGrid(pagina, this.itensPorPagina, this.projetoId)
        .then((resposta) => {
          this.loading = false;
          this.itens = resposta.data.itens;
          this.total = resposta.data.total;
          this.itensPorPagina = resposta.data.itensPorPagina;
        })
        .catch((erro) => {
          this.loading = false;
          this.$notify({
            data: erro.response.data.erros,
            type: "warn",
            duration: 2000
          });
        });
    },
    ModalCancel(evento) {
      evento.preventDefault();
      this.itemRemover = null;
    },
    ModalOk(evento) {
      evento.preventDefault();
      this.modalRemover = false;
      if (!this.itemRemover) return;

      AtividadeServico.Remover(this.itemRemover)
        .then(() => {
          this.ObterGrid(1);
          this.$notify({
            data: ["Atividade removida com sucesso."],
            type: "success",
            duration: 2000
          });
        })
        .catch((erro) => {
          this.$notify({
            data: erro.response.data.erros,
            type: "warn",
            duration: 2000
          });
        });
    },
    Remover(id) {
      this.modalRemover = true;
      this.itemRemover = id;
    },
    Novo() {
      this.loading = false;
      this.viewModel.projetoId = this.projetoId;
      AtividadeServico.Novo(this.viewModel)
        .then((resposta) => {
          this.loading = false;
          this.Limpar();
          this.ObterGrid(1);
          this.$notify({
            data: ["Atividade cadastrada com sucesso."],
            type: "success",
            duration: 2000
          });
        })
        .catch((erro) => {
          this.loading = false;
          this.$notify({
            data: erro.response.data.erros,
            type: "warn",
            duration: 2000
          });
        });
    },
    Editar() {
      this.loading = false;
      this.viewModel.projetoId = this.projetoId;
      AtividadeServico.Editar(this.viewModel)
        .then(() => {
          this.loading = false;
          this.Limpar();
          this.ObterGrid(1);
          this.$notify({
            data: ["Atividade editada com sucesso."],
            type: "success",
            duration: 2000
          });
        })
        .catch((erro) => {
          this.loading = false;
          this.$notify({
            data: erro.response.data.erros,
            type: "warn",
            duration: 2000
          });
        });
    },
    Limpar() {
      this.viewModel.id = this.$store.getters.emptyGuid;
      this.viewModel.projetoId = this.projetoId;
      this.viewModel.nome = "";
      this.viewModel.dataInicio = "";
      this.viewModel.dataFim = "";
      this.viewModel.finalizada = false;
    },
    FormatarData(value) {
      return value ? new Date(value).toLocaleDateString() : "";
    },
    Fechar() {
      this.Limpar();
      this.abrir = false;
    },
    FinalizarAtividade(item) {
      item.finalizada = true;
      this.EditarRegistro(item, "Atividade finalizada com sucesso.");
    },
    ReabrirAtividade(item) {
      item.finalizada = false;
      this.EditarRegistro(item, "Atividade reaberta com sucesso.");
    },
    EditarRegistro(item, mensagem) {
      AtividadeServico.Editar(item)
        .then(() => {
          this.loading = false;
          this.Limpar();
          this.ObterGrid(this.pagina);
          this.$notify({
            data: [mensagem],
            type: "success",
            duration: 2000
          });
        })
        .catch((erro) => {
          this.loading = false;
          this.$notify({
            data: erro.response.data.erros,
            type: "warn",
            duration: 2000
          });
        });
    }
  }
};
</script>
