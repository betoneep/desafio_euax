using Infra.Exception;
using Persistencia;
using Servico.DocumentoAnexo.Validacao;
using Servico.DTO.DocumentoAnexo;
using Servico.DTO.Usuario;
using System;

namespace Servico.DocumentoAnexo
{
    public class NovoDocumentoAnexoServico
    {
        private readonly UsuarioCorrente _usuarioCorrente;
        private readonly Contexto _contexto;

        public NovoDocumentoAnexoServico(UsuarioCorrente usuarioCorrente, Contexto contexto)
        {
            _usuarioCorrente = usuarioCorrente;
            _contexto = contexto;
        }

        public void Executar(DocumentoAnexoDTO dto)
        {
            var validacao = new NovoDocumentoAnexoValidacaoCampos(dto);
            if (!validacao.IsValido)
                throw new SistemaException(validacao.Erros);

            var validacaoBanco = new NovoDocumentoAnexoValidacaoBanco(_contexto, dto);
            if (!validacaoBanco.IsValido)
                throw new SistemaException(validacaoBanco.Erros);

            using (var transacao = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var documentoAnexo = new Dominio.Entidade.DocumentoAnexo
                    {
                        DataCadastro = DateTime.Now,
                        Excluido = false,
                        Id = Guid.NewGuid(),
                        UsuarioCadastro = _usuarioCorrente.Nome
                    };

                    _contexto.DocumentoAnexo.Add(documentoAnexo);
                    _contexto.SaveChanges();

                    transacao.Commit();
                }
                catch (Exception ex)
                {
                    transacao.Rollback();
                    throw new SistemaException("Falha ao criar um novo documento anexo.");
                }

            }
        }
    }
}