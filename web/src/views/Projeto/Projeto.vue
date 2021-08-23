<template>
  <div class="animated fadeIn">
    <div class="row">
      <div class="col-sm-12">
        <div class="card">
          <header class="card-header">
            <div class="d-flex">
              <strong class="align-self-center">Projetos</strong>
              <a
                class="ml-auto btn btn-primary"
                href="/#/projeto/novo"
                title="Adicionar novo projeto"
              >
                Novo Projeto
              </a>
            </div>
          </header>
          <div v-if="loading" class="loading-container">
            <RotateSquare
              class="loading-position animated fadeIn"
              size="60px"
            ></RotateSquare>
          </div>
          <div v-else class="card-body">
            <div class="row">
              <div class="col-lg-4 col-md-6 col-sm-12">
                <div class="form-group">
                  <label>Nome</label>
                  <input
                    type="text"
                    v-model="filtro.nome"
                    class="form-control"
                  />
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-sm-12 col-md-3 col-lg-3 col-xl-2">
                <div class="form-group">
                  <label for>Data Inicial</label>
                  <input
                    v-model="filtro.dataInicial"
                    class="form-control"
                    type="date"
                    placeholder="Digite a data inicial"
                  />
                </div>
              </div>
              <div class="col-sm-12 col-md-3 col-lg-3 col-xl-2">
                <div class="form-group">
                  <label for>Data Final</label>
                  <input
                    v-model="filtro.dataFinal"
                    class="form-control"
                    type="date"
                    placeholder="Digite a data final"
                  />
                </div>
              </div>
              <div
                class="col-sm-6 col-md-2 col-lg-2 col-xl-1"
                title="Apenas projetos atrasados."
              >
                <label for>Atrasados</label>
                <b-form-checkbox
                  v-model="filtro.projetoAtrasado"
                  name="check-button"
                  switch
                >
                </b-form-checkbox>
              </div>
              <div
                class="col-sm-6 col-md-2 col-lg-2 col-xl-1"
                title="Apenas projetos finalizados."
              >
                <label for>Finalizados</label>
                <b-form-checkbox
                  v-model="filtro.projetoFinalizado"
                  name="check-button"
                  switch
                >
                </b-form-checkbox>
              </div>
              <div class="col-lg-4 col-md-5 col-sm-12 mt-4">
                <button
                  class="btn btn-primary mr-2"
                  type="button"
                  @click="ObterGrid(1)"
                >
                  Filtrar
                </button>
                <button
                  class="btn btn-secondary"
                  type="button"
                  @click="Limpar()"
                >
                  Limpar
                </button>
              </div>
            </div>

            <b-table
              :hover="true"
              responsive
              :items="itens"
              :fields="fields"
              striped
              :per-page="itensPorPagina"
              show-empty
              empty-text="Nenhum projeto encontrado."
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
                    @click="Editar(data.item)"
                  >
                    <i class="fa fa-edit"></i>
                  </b-button>
                  <b-button
                    variant="danger"
                    style="margin-right: 10px"
                    title="Remover"
                    @click="Remover(data.item)"
                  >
                    <i class="fas fa-trash-alt"></i>
                  </b-button>
                  <ModalArquivoGrid :referenciaId="data.item.id" />
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
              <template v-slot:cell(finalizado)="data">
                <div class="center">
                  <span v-if="data.item.finalizado" class="badge badge-success">
                    Sim
                  </span>
                  <span v-else class="badge badge-danger">Não</span>
                </div>
              </template>
              <template v-slot:cell(atrasado)="data">
                <div class="center">
                  <span v-if="data.item.atrasado" class="badge badge-danger">
                    Sim
                  </span>
                  <span v-else class="badge badge-success">Não</span>
                </div>
              </template>
              <template v-slot:cell(percentual)="data">
                <div class="left">
                  <span>{{ FormataPercentual(data.item.percentual) }}</span>
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
            <b-modal
              v-model="modalRemover"
              title="Confirmar exclusão do projeto"
              class="modal-danger"
              ok-variant="danger"
              @ok="ModalOk"
              @hidden="ModalCancel"
            >
              Você confirma a exclusão desse projeto?
            </b-modal>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import RotateSquare from "../../components/RotateSquare";
import ModalArquivoGrid from "../../components/ModalArquivoGrid";
import ProjetoServico from "../../servico/ProjetoServico";

export default {
  name: "Projeto",
  components: {
    RotateSquare,
    ModalArquivoGrid,
    ProjetoServico
  },
  data() {
    return {
      modalRemover: false,
      itemRemover: null,
      loading: false,
      itens: [],
      pagina: 1,
      total: 0,
      itensPorPagina: 20,
      filtro: {
        nome: "",
        dataInicial: "",
        dataFinal: "",
        projetoAtrasado: false,
        projetoFinalizado: false
      },
      fields: [
        { key: "nome", label: "Nome", sortable: true },
        { key: "dataInicio", label: "Data Inicial", sortable: true },
        { key: "dataFim", label: "Data Final", sortable: true },
        { key: "percentual", label: "% Completo", sortable: true },
        { key: "atrasado", label: "Atrasado", sortable: true },
        { key: "finalizado", label: "Finalizado", sortable: true },
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
  mounted() {
    this.ObterGrid(1);
  },
  methods: {
    Limpar() {
      this.filtro.nome = "";
      this.filtro.dataInicial = "";
      this.filtro.dataFinal = "";
      this.filtro.projetoAtrasado = false;
      this.filtro.projetoFinalizado = false;
      this.ObterGrid(this.pagina);
    },
    Editar(projeto) {
      this.$router.push("/projeto/editar/" + projeto.id);
    },
    ModalCancel(evento) {
      evento.preventDefault();
      this.itemRemover = null;
    },
    ModalOk(evento) {
      evento.preventDefault();
      this.modalRemover = false;
      if (!this.itemRemover) return;

      ProjetoServico.Remover(this.itemRemover.id)
        .then(() => {
          this.ObterGrid(1);
          this.$notify({
            data: ["Projeto removido com sucesso."],
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
    },
    Remover(item) {
      this.modalRemover = true;
      this.itemRemover = item;
    },
    ObterGrid(pagina) {
      this.loading = false;

      ProjetoServico.ObterGrid(
        pagina,
        this.itensPorPagina,
        this.filtro.nome,
        this.filtro.dataInicial,
        this.filtro.dataFinal,
        this.filtro.projetoAtrasado,
        this.filtro.projetoFinalizado
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
    },
    FormatarData(value) {
      return value ? new Date(value).toLocaleDateString() : "";
    },
    FormataPercentual(valor) {
      return valor
        ? Math.round((valor + Number.EPSILON) * 100) / 100 + "%"
        : "-";
    }
  }
};
</script>
