using Infra.Exception;
using Persistencia;
using Servico.DTO.Projeto;
using Servico.DTO.Usuario;
using Servico.Projeto.Validacao;
using System;

namespace Servico.Projeto
{
    public class NovoProjetoServico
    {
        private readonly UsuarioCorrente _usuarioCorrente;
        private readonly Contexto _contexto;

        public NovoProjetoServico(UsuarioCorrente usuarioCorrente, Contexto contexto)
        {
            _usuarioCorrente = usuarioCorrente;
            _contexto = contexto;
        }

        public void Executar(ProjetoDTO dto)
        {
            var validacao = new NovoProjetoValidacaoCampos(dto);
            if (!validacao.IsValido)
                throw new SistemaException(validacao.Erros);

            var validacaoBanco = new NovoProjetoValidacaoBanco(_contexto, dto);
            if (!validacaoBanco.IsValido)
                throw new SistemaException(validacaoBanco.Erros);

            using (var transacao = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var Projeto = new Dominio.Entidade.Projeto
                    {
                        Nome = dto.Nome,
                        DataInicio = dto.DataInicio,
                        DataFim = dto.DataFim,
                        DataCadastro = DateTime.Now,
                        Excluido = false,
                        UsuarioCadastro = _usuarioCorrente.Nome
                    };

                    _contexto.Projeto.Add(Projeto);
                    _contexto.SaveChanges();

                    transacao.Commit();
                }
                catch (Exception ex)
                {
                    transacao.Rollback();
                    throw new SistemaException("Falha ao criar um novo projeto.");
                }

            }
        }
    }
}