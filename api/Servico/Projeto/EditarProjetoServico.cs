using Infra.Exception;
using Persistencia;
using Servico.DTO.Projeto;
using Servico.DTO.Usuario;
using Servico.Projeto.Validacao;
using System;
using System.Linq;

namespace Servico.Projeto
{
    public class EditarProjetoServico
    {
        private readonly UsuarioCorrente _usuarioCorrente;
        private readonly Contexto _contexto;

        public EditarProjetoServico(UsuarioCorrente usuarioCorrente, Contexto contexto)
        {
            _usuarioCorrente = usuarioCorrente;
            _contexto = contexto;
        }

        public void Executar(ProjetoDTO dto)
        {
            var validacao = new EditarProjetoValidacaoCampos(dto);
            if (!validacao.IsValido)
                throw new SistemaException(validacao.Erros);

            var validacaoBanco = new EditarProjetoValidacaoBanco(_contexto, dto);
            if (!validacaoBanco.IsValido)
                throw new SistemaException(validacaoBanco.Erros);

            using (var transacao = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var projeto = _contexto.Projeto.FirstOrDefault(x => x.Id == dto.Id && !x.Excluido) ?? throw new SistemaException("Projeto não encontrado para editar.");
                    projeto.DataAlteracao = DateTime.Now;
                    projeto.UsuarioAlteracao = _usuarioCorrente.Nome;
                    projeto.Nome = dto.Nome;
                    projeto.DataInicio = dto.DataInicio;
                    projeto.DataFim = dto.DataFim;

                    _contexto.SaveChanges();

                    transacao.Commit();
                }
                catch (Exception ex)
                {
                    transacao.Rollback();
                    throw new SistemaException("Falha ao editar projeto.");
                }

            }
        }
    }
}