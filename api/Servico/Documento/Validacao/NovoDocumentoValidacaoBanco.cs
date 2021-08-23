using Persistencia;
using Servico.DTO.Documento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servico.Documento.Validacao
{
    public class NovoDocumentoValidacaoBanco : BaseValidacao
    {
        public NovoDocumentoValidacaoBanco(Contexto contexto, DocumentoDTO dto)
        {
            if (contexto.Documento.Any(x => x.Numero == dto.Numero && !x.Excluido))
                Erros.Add($"Já existe um documento com mesmo número cadastrado.");
        }
    }
}
