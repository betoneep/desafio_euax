import Vue from "vue";
class DocumentoAnexoServico {

    ObterGrid(pagina, itensPorPagina, referenciaId) {
        return Vue.prototype.$http({
            url:
                "documentoanexo/obter-grid/" +
                referenciaId +
                "/" +
                pagina +
                "/" +
                itensPorPagina,
            method: "GET"
        });
    }
}

export default new DocumentoAnexoServico();
