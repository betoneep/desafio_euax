using Infra.Constante;
using Infra.Exception;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using Servico.DTO;
using Servico.DTO.Atividade;
using Servico.DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servico.Atividade
{
    public class AtividadeServico
    {
        private readonly Contexto _contexto;

        public AtividadeServico(Contexto contexto)
        {
            _contexto = contexto;
        }

        public void Executar(Guid id)
        {
            throw new NotImplementedException();
        }

        public object Obter(Guid id)
        {
            var model = _contexto.Atividade
                .AsNoTracking()
                .Where(x => !x.Excluido)
                .FirstOrDefault(x => x.Id == id) ?? throw new SistemaException("Atividade não encontrada.");

            return MontarAtividadeDTO(model);
        }

        private AtividadeDTO MontarAtividadeDTO(Dominio.Entidade.Atividade model)
        {
            return new AtividadeDTO
            {
                Id = model.Id,
                Nome = model.Nome,
                DataInicio = model.DataInicio,
                DataFim = model.DataFim,
                Finalizada = model.Finalizada,
                ProjetoId = model.ProjetoId,
                DataCadastro = model.DataCadastro,
                UsuarioCadastro = model.UsuarioCadastro,
                DataAlteracao = model.DataAlteracao,
                UsuarioAlteracao = model.UsuarioAlteracao,
                UsuarioExclusao = model.UsuarioExclusao,
                Excluido = model.Excluido,
                DataExclusao = model.DataExclusao
            };
        }

        public GridDTO<AtividadeDTO> ObterGrid(AtividadeFiltroDTO filtro)
        {
            var grid = new GridDTO<AtividadeDTO>
            {
                Pagina = filtro.Pagina,
                ItensPorPagina = filtro.ItensPorPagina <= 0 ? Paginacao.ITENS_POR_PAGINA : filtro.ItensPorPagina
            };

            var atividades = _contexto.Atividade
                .AsNoTracking()
                .Where(x => !x.Excluido)
                //FILTRO PROJETO
                .Where(x => (filtro.ProjetoId.Equals(null) || filtro.ProjetoId.Equals(Guid.Empty)) || x.ProjetoId.Equals(filtro.ProjetoId))
                //FILTRO NOME
                .Where(x => string.IsNullOrWhiteSpace(filtro.Nome) || x.Nome.ToLower().Contains(filtro.Nome.ToLower()))
                //FILTRO DATA INICIAL
                .Where(x => filtro.DataInicio == null || x.DataInicio.Value.Date.Equals(filtro.DataInicio))
                //FILTRO ATRASADOS
                .Where(x => !(filtro.Atrasado == true) || x.DataFim.Value.Date < DateTime.Today)
                //FILTRO FINALIZADOS
                .Where(x => !(filtro.Finalizada == true) || x.Finalizada == true).ToList();

            grid.Total = atividades.Count();
            grid.Itens = atividades
                .OrderBy(o => o.Nome)
                .Skip((grid.Pagina - 1) * grid.ItensPorPagina)
                .Take(grid.ItensPorPagina)
                .Select(model => new AtividadeDTO
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    DataInicio = model.DataInicio,
                    DataFim = model.DataFim,
                    Finalizada = model.Finalizada,
                    ProjetoId = model.ProjetoId,
                })
                .ToList();
            return grid;
        }

        public List<SelectDTO> ObterSelect()
        {
            return _contexto.Atividade
                .AsNoTracking()
                .Where(x => !x.Excluido)
                .OrderBy(o => o.Nome)
                .Select(s => new SelectDTO
                {
                    Text = s.Nome,
                    Value = s.Id
                }).ToList();
        }
    }
}