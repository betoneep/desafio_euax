import Vue from "vue";

class ProjetoServico {
  Obter(id) {
    return Vue.prototype.$http({
      url: "projeto/obter/" + id,
      method: "GET"
    });
  }

  ObterGrid(
    pagina,
    itensPorPagina,
    nome,
    dataInicial,
    dataFinal,
    projetoAtrasado,
    projetoFinalizado
  ) {
    return Vue.prototype.$http({
      url:
        "/projeto/obter-grid?pagina=" +
        pagina +
        "&ItensPorPagina=" +
        itensPorPagina +
        (nome ? "&Nome=" + nome : "") +
        (dataInicial ? "&DataInicio=" + dataInicial : "") +
        (dataFinal ? "&DataFim=" + dataFinal : "") +
        (projetoAtrasado ? "&Atrasado=" + projetoAtrasado : "") +
        (projetoFinalizado ? "&Finalizado=" + projetoFinalizado : ""),
      method: "GET"
    });
  }

  Remover(id) {
    return Vue.prototype.$http({
      url: "projeto/remover/" + id,
      method: "DELETE"
    });
  }

  Novo(viewModel) {
    return Vue.prototype.$http({
      url: "projeto/novo",
      data: viewModel,
      method: "POST"
    });
  }

  Editar(viewModel) {
    return Vue.prototype.$http({
      url: "projeto/editar",
      data: viewModel,
      method: "PUT"
    });
  }
}

export default new ProjetoServico();
