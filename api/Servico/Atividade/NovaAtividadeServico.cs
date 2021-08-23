using Infra.Exception;
using Persistencia;
using Servico.Atividade.Validacao;
using Servico.DTO.Atividade;
using Servico.DTO.Usuario;
using System;

namespace Servico.Atividade
{
    public class NovaAtividadeServico
    {
        private readonly UsuarioCorrente _usuarioCorrente;
        private readonly Contexto _contexto;

        public NovaAtividadeServico(UsuarioCorrente usuarioCorrente, Contexto contexto)
        {
            _usuarioCorrente = usuarioCorrente;
            _contexto = contexto;
        }

        public void Executar(AtividadeDTO dto)
        {
            var validacao = new NovaAtividadeValidacaoCampos(dto);
            if (!validacao.IsValido)
                throw new SistemaException(validacao.Erros);

            var validacaoBanco = new NovaAtividadeValidacaoBanco(_contexto, dto);
            if (!validacaoBanco.IsValido)
                throw new SistemaException(validacaoBanco.Erros);

            using (var transacao = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var Atividade = new Dominio.Entidade.Atividade
                    {
                        Nome = dto.Nome,
                        DataInicio = dto.DataInicio,
                        DataFim = dto.DataFim,
                        ProjetoId = dto.ProjetoId,                        
                        DataCadastro = DateTime.Now,
                        Excluido = false,
                        Id = Guid.NewGuid(),
                        UsuarioCadastro = _usuarioCorrente.Nome
                    };

                    _contexto.Atividade.Add(Atividade);
                    _contexto.SaveChanges();

                    transacao.Commit();
                }
                catch (Exception ex)
                {
                    transacao.Rollback();
                    throw new SistemaException("Falha ao criar nova atividade.");
                }

            }
        }
    }
}