using Infra.Constante;
using Infra.Exception;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using Servico.DTO;
using Servico.DTO.Projeto;
using Servico.DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servico.Projeto
{
    public class ProjetoServico
    {
        private readonly Contexto _contexto;

        public ProjetoServico(Contexto contexto)
        {
            _contexto = contexto;
        }

        public void Executar(Guid id)
        {
            throw new NotImplementedException();
        }

        public object Obter(Guid id)
        {
            var model = _contexto.Projeto
                .AsNoTracking()
                .Where(x => !x.Excluido)
                .FirstOrDefault(x => x.Id == id)
                ?? throw new SistemaException("Projeto não encontrado");

            return MontarProjetoDTO(model);
        }

        private ProjetoDTO MontarProjetoDTO(Dominio.Entidade.Projeto model)
        {
            return new ProjetoDTO
            {
                Id = model.Id,
                Nome = model.Nome,
                DataFim = model.DataFim,
                DataInicio = model.DataInicio,
                DataCadastro = model.DataCadastro,
                UsuarioCadastro = model.UsuarioCadastro,
                DataAlteracao = model.DataAlteracao,
                UsuarioAlteracao = model.UsuarioAlteracao,
                UsuarioExclusao = model.UsuarioExclusao,
                Excluido = model.Excluido,
                DataExclusao = model.DataExclusao
            };
        }

        public GridDTO<ProjetoGridDTO> ObterGrid(ProjetoFiltroDTO filtro)
        {
            var grid = new GridDTO<ProjetoGridDTO>
            {
                Pagina = filtro.Pagina,
                ItensPorPagina = filtro.ItensPorPagina <= 0 ? Paginacao.ITENS_POR_PAGINA : filtro.ItensPorPagina
            };

            var projetos = _contexto.Projeto
                .AsNoTracking()
                .Where(x => !x.Excluido)
                //FILTRO NOME
                .Where(x => string.IsNullOrWhiteSpace(filtro.Nome) || x.Nome.ToLower().Contains(filtro.Nome.ToLower()))
                //FILTRO DATA INICIAL
                .Where(x => filtro.DataInicio == null || x.DataInicio.Value.Date.Equals(filtro.DataInicio)).ToList();

            var projetosAndamento = FindPercentualProjetos(_contexto.Projeto.Where(x => !x.Excluido).ToList());
            var atividades = _contexto.Atividade.Where(x => !x.Excluido).ToList();

            if (filtro.Atrasado == true)
            {
                projetos = projetos.Where(model => model.DataFim < DateTime.Today || atividades.Any(atividade => atividade.ProjetoId.Equals(model.Id) && (atividade.DataFim > model.DataFim || atividade.DataFim < DateTime.Today))).ToList();
            }

            if (filtro.Finalizado == true)
            {
                projetos = projetos.Where(model => projetosAndamento[model.Id] == 100).ToList();
            }

            grid.Total = projetos.Count();
            grid.Itens = projetos
                .OrderBy(o => o.Nome)
                .Skip((grid.Pagina - 1) * grid.ItensPorPagina)
                .Take(grid.ItensPorPagina)
                .Select(model => new ProjetoGridDTO
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    DataInicio = model.DataInicio,
                    DataFim = model.DataFim,
                    Percentual = projetosAndamento[model.Id],
                    Atrasado = (model.DataFim < DateTime.Today) || (atividades.Any(atividade => atividade.ProjetoId.Equals(model.Id) && (atividade.DataFim > model.DataFim || atividade.DataFim < DateTime.Today))),
                    Finalizado = projetosAndamento[model.Id] == 100
                })
                .ToList();

            return grid;
        }

        private IDictionary<Guid, decimal> FindPercentualProjetos(List<Dominio.Entidade.Projeto> projetos)
        {
            IDictionary<Guid, decimal> map = new Dictionary<Guid, decimal>();
            foreach (var projeto in projetos)
            {
                map[projeto.Id] = CalculaPercentualProjeto(projeto.Id);
            }

            return map.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        private decimal CalculaPercentualProjeto(Guid projetoId)
        {
            var atividades = _contexto.Atividade.Where(x => !x.Excluido && x.ProjetoId.Equals(projetoId));
            decimal atividadeFinalizadas = atividades.Where(x => !x.Excluido && x.ProjetoId.Equals(projetoId) && x.Finalizada == true).Count();
            return atividadeFinalizadas == 0 ? 0 : atividades.Count() == 0 ? 0 : (Convert.ToDecimal(atividadeFinalizadas / Convert.ToDecimal(atividades.Count()))) * 100;
        }

        public List<SelectDTO> ObterSelect()
        {
            return _contexto.Projeto
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