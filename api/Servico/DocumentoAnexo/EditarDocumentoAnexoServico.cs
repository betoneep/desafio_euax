using Infra.Exception;
using Persistencia;
using Servico.DocumentoAnexo.Validacao;
using Servico.DTO.DocumentoAnexo;
using Servico.DTO.Usuario;
using System;
using System.Linq;

namespace Servico.DocumentoAnexo
{
    public class EditarDocumentoAnexoServico
    {
        private readonly UsuarioCorrente _usuarioCorrente;
        private readonly Contexto _contexto;

        public EditarDocumentoAnexoServico(UsuarioCorrente usuarioCorrente, Contexto contexto)
        {
            _usuarioCorrente = usuarioCorrente;
            _contexto = contexto;
        }

        public void Executar(DocumentoAnexoDTO dto)
        {
            var validacao = new EditarDocumentoAnexoValidacaoCampos(dto);
            if (!validacao.IsValido)
                throw new SistemaException(validacao.Erros);

            var validacaoBanco = new EditarDocumentoAnexoValidacaoBanco(_contexto, dto);
            if (!validacaoBanco.IsValido)
                throw new SistemaException(validacaoBanco.Erros);


            using (var transacao = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var documentoAnexo = _contexto.DocumentoAnexo.FirstOrDefault(x => x.Id == dto.Id && !x.Excluido)
              ?? throw new SistemaException("Documento anexo não encontrado para editar.");

                    documentoAnexo.DataAlteracao = DateTime.Now;
                    documentoAnexo.UsuarioAlteracao = _usuarioCorrente.Nome;

                    _contexto.SaveChanges();

                    transacao.Commit();
                }
                catch (Exception ex)
                {
                    transacao.Rollback();
                    throw new SistemaException("Falha ao editar documento anexo.");
                }

            }
        }
    }
}