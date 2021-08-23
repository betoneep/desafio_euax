using Infra.Constante;
using Infra.Exception;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using Servico.DTO;
using Servico.DTO.Documento;
using Servico.DTO.Usuario;
using System;
using System.Linq;

namespace Servico.Documento
{
    public class DocumentoServico
    {

        private readonly Contexto _contexto;

        public DocumentoServico(Contexto contexto)
        {
            _contexto = contexto;
        }

        public NovoDocumentoDTO Obter(Guid id)
        {
            var model = _contexto.Documento
                .AsNoTracking()
                .Where(x => !x.Excluido)
                .FirstOrDefault(x => x.Id == id)
                ?? throw new SistemaException("Documento não encontrado.");

            return MontarDocumento(model);
        }

        private NovoDocumentoDTO MontarDocumento(Dominio.Entidade.Documento model)
        {
            return new NovoDocumentoDTO
            {
                Id = model.Id,
                TipoDocumentoId = model.TipoDocumentoId,
                Validade = model.Validade,
                Numero = model.Numero,
                Observacao = model.Observacao,
            };
        }

        public GridDTO<GridDocumentoDTO> ObterGrid(int pagina, int itensPorPagina, Guid referenciaId)
        {
            var grid = new GridDTO<GridDocumentoDTO>
            {
                Pagina = pagina,
                ItensPorPagina = itensPorPagina <= 0 ? Paginacao.ITENS_POR_PAGINA : itensPorPagina
            };


            var consulta = _contexto.Documento
                .AsNoTracking()
                .Include(i => i.Arquivos)
                .Where(x => x.ReferenciaId == referenciaId)
                .Where(x => !x.Excluido)
                .AsQueryable();

            grid.Total = consulta.Count();
            grid.Itens = consulta
                .OrderBy(o => o.Numero)
                .Skip((grid.Pagina - 1) * grid.ItensPorPagina)
                .Take(grid.ItensPorPagina)
                .Select(x => new GridDocumentoDTO
                {
                    Id = x.Id,
                    Validade = x.Validade,
                    DataCadastro = x.DataCadastro,
                    Numero = x.Numero,
                    Arquivos = x.Arquivos
                        .Where(w => !w.Excluido)
                        .Select(s => s.ArquivoId).ToList()
                })
                .ToList();

            return grid;
        }
    }
}