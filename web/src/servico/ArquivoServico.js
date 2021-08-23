import Vue from "vue";
class ArquivoServico {
    Obter(id) {
        return Vue.prototype.$http({
            url: "arquivo/obter/" + id,
            method: "GET"
        });
    }

    Novo(arquivos) {
        let formData = new FormData();
        for (let index = 0; index < arquivos.length; index++) {
            formData.append("arquivo", arquivos[index]);
        }
        return Vue.prototype.$http({
            url: "arquivo/novo",
            data: formData,
            method: "POST",
            headers: {
                "Content-Type": "multipart/form-data"
            }
        });
    }
};

export default new ArquivoServico();