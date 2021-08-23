using Persistencia;
using Servico.DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Infra.Exception;
using Servico.DTO;
using Infra.Constante;

namespace Servico.Usuario
{
    public class UsuarioServico
    {
        private readonly Contexto _contexto;

        public UsuarioServico(Contexto contexto)
        {
            _contexto = contexto;
        }
        public UsuarioDTO Obter(Guid usuarioId)
        {
            var model = _contexto.Usuario
                .AsNoTracking()
                .Where(x => !x.Excluido)
                .FirstOrDefault(x => x.Id == usuarioId)
                ?? throw new SistemaException("Usuário não encontrado");

            return MontarUsuarioDTO(model);
        }

        public GridDTO<UsuarioDTO> ObterGrid(int pagina, int itensPorPagina)
        {
            var grid = new GridDTO<UsuarioDTO>
            {
                Pagina = pagina,
                ItensPorPagina = itensPorPagina <= 0 ? Paginacao.ITENS_POR_PAGINA : itensPorPagina
            };

            var consulta = _contexto.Usuario
                .Where(x => !x.Excluido)
                .AsQueryable();

            grid.Total = consulta.Count();
            grid.Itens = consulta
                .OrderBy(o => o.Nome)
                .Skip((grid.Pagina - 1) * grid.ItensPorPagina)
                .Take(grid.ItensPorPagina)
                .Select(s => new UsuarioDTO
                {
                    Ativo = s.Ativo,
                    DataAlteracao = s.DataAlteracao,
                    DataCadastro = s.DataCadastro,
                    Email = s.Email,
                    Id = s.Id,
                    Nome = s.Nome,
                    UsuarioAlteracao = s.UsuarioAlteracao,
                    UsuarioCadastro = s.UsuarioCadastro,
                })
                .ToList();

            return grid;
        }

        private UsuarioDTO MontarUsuarioDTO(Dominio.Entidade.Usuario model)
        {
            return new UsuarioDTO
            {
                Ativo = model.Ativo,
                DataAlteracao = model.DataAlteracao,
                DataCadastro = model.DataCadastro,
                Email = model.Email,
                Id = model.Id,
                Nome = model.Nome,
                Senha = model.Senha,
                UsuarioAlteracao = model.UsuarioAlteracao,
                UsuarioCadastro = model.UsuarioCadastro
            };
        }
    }
}
