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
            <header class="card-header">
              <div class="d-flex">
                <strong class="align-self-center"
                  >{{
                    viewModel.id == this.$store.getters.emptyGuid
                      ? "Novo Projeto"
                      : "Editar Projeto"
                  }}
                </strong>
                <a
                  @click="Limpar()"
                  class="ml-auto btn btn-primary"
                  href="/#/projeto/novo"
                  title="Adicionar novo projeto"
                >
                  Adicionar
                </a>
              </div>
            </header>
            <div class="card-body">
              <div class="row">
                <div class="col">
                  <div class="form-group">
                    <small>Campos com * são de preenchimento obrigatório</small>
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
                      placeholder="Digite a data de inicio"
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
              </div>
            </div>
            <div class="btn-toolbar mb-3 ml-3" role="toolbar">
              <div class="btn-group" role="group">
                <button class="btn btn-success mr-2" type="submit">
                  Salvar
                </button>
              </div>
              <div class="btn-group" role="group">
                <button
                  class="btn btn-secondary"
                  type="reset"
                  @click="$router.go(-1)"
                >
                  Voltar
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </form>
    <div v-if="IsEdicao()">
      <ProjetoAtividade :projetoId="viewModel.id"> </ProjetoAtividade>
      <NovoDocumento :referenciaId="viewModel.id"> </NovoDocumento>
    </div>
  </div>
</template>

<script>
import RotateSquare from "../../components/RotateSquare";
import DateTime from "../../util/DateTime";
import ProjetoServico from "../../servico/ProjetoServico";
import NovoDocumento from "../../components/NovoDocumento";
import ProjetoAtividade from "./ProjetoAtividade";

export default {
  name: "NovoProjeto",
  components: {
    RotateSquare,
    ProjetoServico,
    NovoDocumento,
    ProjetoAtividade
  },
  data() {
    return {
      loading: false,
      viewModel: {
        id: this.$store.getters.emptyGuid,
        nome: "",
        dataInicio: "",
        dataFim: ""
      }
    };
  },
  created() {
    let projetoId = this.$route.params.id;
    if (projetoId) this.Obter(projetoId);
  },
  methods: {
    ValidarForm(evt) {
      evt.preventDefault();
      if (this.viewModel.id !== this.$store.getters.emptyGuid) this.Editar();
      else this.Novo();
    },
    Obter(projetoId) {
      this.loading = false;
      ProjetoServico.Obter(projetoId)
        .then((resposta) => {
          resposta.data.dataInicio = DateTime.formatar(
            resposta.data.dataInicio
          );
          resposta.data.dataFim = DateTime.formatar(resposta.data.dataFim);

          this.loading = false;
          this.viewModel = resposta.data;
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
    Novo() {
      this.loading = false;
      ProjetoServico.Novo(this.viewModel)
        .then(() => {
          this.loading = false;
          this.$router.push("/projeto");
          this.$notify({
            data: ["Projeto cadastrado com sucesso."],
            type: "success",
            duration: 5000
          });
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
    Editar() {
      this.loading = false;

      ProjetoServico.Editar(this.viewModel)
        .then(() => {
          this.loading = false;
          this.$router.push("/projeto");
          this.$notify({
            data: ["Projeto editado com sucesso."],
            type: "success",
            duration: 5000
          });
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
    IsEdicao() {
      return this.viewModel.id !== this.$store.getters.emptyGuid;
    },
    Limpar() {
      this.viewModel.id = this.$store.getters.emptyGuid;
      this.viewModel.nome = "";
      this.viewModel.dataInicio = "";
      this.viewModel.dataFim = "";
    }
  }
};
</script>
