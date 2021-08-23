using Microsoft.EntityFrameworkCore;
using Persistencia;
using Servico.DTO.Arquivo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Servico.Arquivo
{
    public class ArquivoServico
    {
        private readonly Contexto _contexto;

        public ArquivoServico(Contexto contexto)
        {
            _contexto = contexto;

        }
        public BaixarAquivoDTO Obter(Guid id)
        {
            var arquivo = _contexto.Arquivo
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id)
            ?? throw new Exception("Arquivo não encontrado");

            return new BaixarAquivoDTO { Binario = arquivo.Binario, MimeType = arquivo.MimeType };
        }
    }
}
