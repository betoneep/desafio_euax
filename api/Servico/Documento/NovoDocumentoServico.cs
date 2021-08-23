using Infra.Exception;
using Persistencia;
using Servico.Documento.Validacao;
using Servico.DTO.Documento;
using Servico.DTO.Usuario;
using System;
using System.Linq;

namespace Servico.Documento
{
    public class NovoDocumentoServico
    {
        private readonly UsuarioCorrente _usuarioCorrente;
        private readonly Contexto _contexto;

        public NovoDocumentoServico(UsuarioCorrente usuarioCorrente, Contexto contexto)
        {
            _usuarioCorrente = usuarioCorrente;
            _contexto = contexto;
        }

        public void Executar(DocumentoDTO dto)
        {
            var validacao = new NovoDocumentoValidacaoCampos(dto);
            if (!validacao.IsValido)
                throw new SistemaException(validacao.Erros);

            using (var transacao = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var documento = new Dominio.Entidade.Documento
                    {
                        TipoDocumentoId = dto.TipoDocumentoId,
                        Validade = dto.Validade,
                        Numero = dto.Numero,
                        Observacao = dto.Observacao,
                        DataCadastro = DateTime.Now,
                        Excluido = false,
                        Id = Guid.NewGuid(),
                        UsuarioCadastro = _usuarioCorrente.Nome,
                        ReferenciaId = dto.ReferenciaId
                    };

                    if (dto.Arquivos != null && dto.Arquivos.Any())
                    {
                        var arquivos = dto.Arquivos.Select(s => new Dominio.Entidade.DocumentoAnexo
                        {
                            ArquivoId = s,
                            DataCadastro = DateTime.Now,
                            Documento = documento,
                            UsuarioCadastro = _usuarioCorrente.Nome,
                        });
                        _contexto.DocumentoAnexo.AddRange(arquivos);
                    }

                    _contexto.Documento.Add(documento);
                    _contexto.SaveChanges();
                    transacao.Commit();
                }
                catch (Exception ex)
                {
                    transacao.Rollback();
                    throw new SistemaException("Falha ao criar um novo documento.");
                }

            }
        }
    }
}