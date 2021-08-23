using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Servico.Arquivo.Validacao
{
    class NovoArquivoValidacao : BaseValidacao
    {

        public NovoArquivoValidacao(ICollection<IFormFile> arquivo)
        {
            if (arquivo == null || !arquivo.Any())
                Erros.Add("Informe ao menos um arquivo.");
            else if (arquivo.Any(x => x.Length > 1024 * 1024 * 20))
                Erros.Add("Arquivos devem ter no máximo 20mb de tamaho.");
        }
    }
}
