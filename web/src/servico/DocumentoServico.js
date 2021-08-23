import Vue from "vue";
class DocumentoServico {
  Obter(id) {
    return Vue.prototype.$http({
      url: "documento/obter/" + id,
      method: "GET"
    });
  }

  ObterGrid(pagina, itensPorPagina, registroId) {
    return Vue.prototype.$http({
      url:
        "documento/obter-grid/" +
        pagina +
        "/" +
        itensPorPagina +
        "/" +
        registroId,
      method: "GET"
    });
  }

  Remover(id) {
    return Vue.prototype.$http({
      url: "documento/remover/" + id,
      method: "DELETE"
    });
  }

  Novo(viewModel) {
    return Vue.prototype.$http({
      url: "documento/novo",
      data: viewModel,
      method: "POST"
    });
  }

  Editar(viewModel) {
    return Vue.prototype.$http({
      url: "documento/editar",
      data: viewModel,
      method: "PUT"
    });
  }
}

export default new DocumentoServico();
