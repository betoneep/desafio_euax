import Vue from "vue";

class AtividadeServico {
  Obter(id) {
    return Vue.prototype.$http({
      url: "atividade/obter/" + id,
      method: "GET"
    });
  }

  ObterGrid(pagina, itensPorPagina, projetoId) {
    return Vue.prototype.$http({
      url:
        "/atividade/obter-grid?pagina=" +
        pagina +
        "&ItensPorPagina=" +
        itensPorPagina +
        (projetoId ? "&ProjetoId=" + projetoId : ""),
      method: "GET"
    });
  }

  Remover(id) {
    return Vue.prototype.$http({
      url: "atividade/remover/" + id,
      method: "DELETE"
    });
  }

  Novo(viewModel) {
    return Vue.prototype.$http({
      url: "atividade/novo",
      data: viewModel,
      method: "POST"
    });
  }

  Editar(viewModel) {
    return Vue.prototype.$http({
      url: "atividade/editar",
      data: viewModel,
      method: "PUT"
    });
  }
}

export default new AtividadeServico();
