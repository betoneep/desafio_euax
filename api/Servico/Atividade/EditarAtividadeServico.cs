using Infra.Exception;
using Persistencia;
using Servico.Atividade.Validacao;
using Servico.DTO.Atividade;
using Servico.DTO.Usuario;
using System;
using System.Linq;

namespace Servico.Atividade
{
    public class EditarAtividadeServico
    {
        private readonly UsuarioCorrente _usuarioCorrente;
        private readonly Contexto _contexto;

        public EditarAtividadeServico(UsuarioCorrente usuarioCorrente, Contexto contexto)
        {
            _usuarioCorrente = usuarioCorrente;
            _contexto = contexto;
        }

        public void Executar(AtividadeDTO dto)
        {
            var validacao = new EditarAtividadeValidacaoCampos(dto);
            if (!validacao.IsValido)
                throw new SistemaException(validacao.Erros);

            var validacaoBanco = new EditarAtividadeValidacaoBanco(_contexto, dto);
            if (!validacaoBanco.IsValido)
                throw new SistemaException(validacaoBanco.Erros);


            using (var transacao = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var atividade = _contexto.Atividade.FirstOrDefault(x => x.Id == dto.Id && !x.Excluido) ?? throw new SistemaException("Atividade não encontrada para editar.");

                    atividade.Nome = dto.Nome;
                    atividade.DataInicio = dto.DataInicio;
                    atividade.DataFim = dto.DataFim;
                    atividade.Finalizada = dto.Finalizada;
                    atividade.ProjetoId = dto.ProjetoId;
                    atividade.UsuarioAlteracao = _usuarioCorrente.Nome;
                    atividade.DataAlteracao = DateTime.Now;

                    _contexto.SaveChanges();

                    transacao.Commit();
                }
                catch (Exception ex)
                {
                    transacao.Rollback();
                    throw new SistemaException("Falha ao editar atividade.");
                }

            }
        }
    }
}