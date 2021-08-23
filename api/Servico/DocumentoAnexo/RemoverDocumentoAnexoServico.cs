using Infra.Exception;
using Persistencia;
using Servico.DTO.Usuario;
using System.Linq;
using System;
using Servico.DocumentoAnexo.Validacao;

namespace Servico.DocumentoAnexo
{
    public class RemoverDocumentoAnexoServico
    {
        private readonly UsuarioCorrente _usuarioCorrente;
        private readonly Contexto _contexto;

        public RemoverDocumentoAnexoServico(UsuarioCorrente usuarioCorrente, Contexto contexto)
        {
            _usuarioCorrente = usuarioCorrente;
            _contexto = contexto;
        }

        public void Executar(Guid arquivoId, Guid id)
        {
            var validacao = new RemoverDocumentoAnexoValidacaoBanco(_contexto, id);
            if (!validacao.IsValido)
                throw new SistemaException(validacao.Erros);


            var documentoAnexo = _contexto.DocumentoAnexo
                .Where(x => !x.Excluido)
                .Where(x => x.DocumentoId == id)
                .FirstOrDefault(x => x.ArquivoId == arquivoId)
                    ?? throw new SistemaException("Documento anexo não encontrado para remover.");

            documentoAnexo.Excluido = true;
            documentoAnexo.UsuarioExclusao = _usuarioCorrente.Nome;
            documentoAnexo.DataExclusao = DateTime.Now;

            _contexto.SaveChanges();


        }
    }
}