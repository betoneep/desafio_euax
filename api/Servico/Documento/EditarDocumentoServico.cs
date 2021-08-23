using Infra.Exception;
using Persistencia;
using Servico.DTO.Usuario;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Servico.DTO.Documento;
using Servico.Documento.Validacao;

namespace Servico.Documento
{
    public class EditarDocumentoServico
    {
        private readonly UsuarioCorrente _usuarioCorrente;
        private readonly Contexto _contexto;

        public EditarDocumentoServico(UsuarioCorrente usuarioCorrente, Contexto contexto)
        {
            _usuarioCorrente = usuarioCorrente;
            _contexto = contexto;
        }

        public void Executar(DocumentoDTO dto)
        {
            var validacao = new EditarDocumentoValidacaoCampos(dto);
            if (!validacao.IsValido)
                throw new SistemaException(validacao.Erros);

            using (var transacao = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var documento = _contexto.Documento
                        .Where(x => !x.Excluido)
                        .FirstOrDefault(x => x.Id == dto.Id)
                        ?? throw new SistemaException("Documento não encontrado para editar.");

                    documento.DataAlteracao = DateTime.Now;
                    documento.UsuarioAlteracao = _usuarioCorrente.Nome;
                    documento.TipoDocumentoId = dto.TipoDocumentoId;
                    documento.Validade = dto.Validade;
                    documento.Numero = dto.Numero;
                    documento.Observacao = dto.Observacao;
                    documento.ReferenciaId = dto.ReferenciaId;

                    _contexto.SaveChanges();

                    transacao.Commit();
                }
                catch (Exception ex)
                {
                    transacao.Rollback();
                    throw new SistemaException("Falha ao editar documento.");
                }

            }
        }
    }
}