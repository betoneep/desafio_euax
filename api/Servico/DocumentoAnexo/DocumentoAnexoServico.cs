using Infra.Constante;
using Infra.Exception;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using Servico.DTO;
using Servico.DTO.DocumentoAnexo;
using Servico.DTO.Usuario;
using System;
using System.Linq;

namespace Servico.DocumentoAnexo
{
    public class DocumentoAnexoServico
    {
        private readonly Contexto _contexto;

        public DocumentoAnexoServico(Contexto contexto)
        {
            _contexto = contexto;
        }

        public void Executar(Guid id)
        {
            throw new NotImplementedException();
        }

        public object Obter(Guid id)
        {
            var model = _contexto.DocumentoAnexo
                .Include(x => x.Documento)
                .Where(x => !x.Excluido)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id)
                ?? throw new SistemaException("Documento Anexo não encontrado");

            return MontarDocumentoAnexo(model);
        }

        private DocumentoAnexoDTO MontarDocumentoAnexo(Dominio.Entidade.DocumentoAnexo model)
        {
            return new DocumentoAnexoDTO
            {
                Id = model.Id,
                DocumentoId = model.DocumentoId,
                DataCadastro = model.DataCadastro,
                UsuarioCadastro = model.UsuarioCadastro,
                DataAlteracao = model.DataAlteracao,
                UsuarioAlteracao = model.UsuarioAlteracao,
                UsuarioExclusao = model.UsuarioExclusao,
                Excluido = model.Excluido,
                DataExclusao = model.DataExclusao
            };
        }

        public GridDTO<DocumentoAnexoGridDTO> ObterGrid(Guid referenciaId, int pagina, int itensPorPagina)
        {
            var grid = new GridDTO<DocumentoAnexoGridDTO>
            {
                Pagina = pagina,
                ItensPorPagina = itensPorPagina <= 0 ? Paginacao.ITENS_POR_PAGINA : itensPorPagina
            };

            var consulta = _contexto.DocumentoAnexo
                .AsNoTracking()
                .Include(x => x.Documento)
                .Where(x => !x.Excluido)
                .Where(x => x.Documento.ReferenciaId == referenciaId)
                .Where(x => !x.Documento.Excluido)
                .AsQueryable();

            grid.Total = consulta.Count();
            grid.Itens = consulta
                .OrderBy(o => o.DataCadastro)
                .Skip((grid.Pagina - 1) * grid.ItensPorPagina)
                .Take(grid.ItensPorPagina)
                .Select(x => new DocumentoAnexoGridDTO
                {
                    ArquivoId = x.ArquivoId,
                    Nome = x.Documento.Numero
                })
                .ToList();

            return grid;
        }
    }
}