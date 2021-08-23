using Infra.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servico.DocumentoAnexo;
using Servico.DTO.DocumentoAnexo;
using System;
using System.Net;

namespace Api.Controllers
{

    [Route("api/[controller]")]
    [Authorize("Bearer")]
    [ApiController]
    public class DocumentoAnexoController : ControllerBase
    {
        public DocumentoAnexoController()
        {
        }

        [HttpPost("novo")]
        public IActionResult Novo([FromBody] DocumentoAnexoDTO dto, [FromServices] NovoDocumentoAnexoServico servico)
        {
            try
            {
                servico.Executar(dto);
                return Ok();
            }
            catch (SistemaException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { e.Erros });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("editar")]
        public IActionResult Editar([FromBody] DocumentoAnexoDTO dto, [FromServices] EditarDocumentoAnexoServico servico)
        {
            try
            {
                servico.Executar(dto);
                return Ok();
            }
            catch (SistemaException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { e.Erros });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("obter/{id}")]
        public IActionResult Obter(Guid id, [FromServices] DocumentoAnexoServico servico)
        {
            try
            {
                var dto = servico.Obter(id);

                return Ok(dto);
            }
            catch (SistemaException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { e.Erros });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("obter-grid/{referenciaId}/{pagina}/{itensPorPagina}")]
        public IActionResult ObterGrid(Guid referenciaId, int pagina, int itensPorPagina, [FromServices] DocumentoAnexoServico servico)
        {
            try
            {
                var dto = servico.ObterGrid(referenciaId, pagina, itensPorPagina);

                return Ok(dto);
            }
            catch (SistemaException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { e.Erros });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("remover/{id}/{vinculoId}")]
        public IActionResult Remover(Guid id, Guid vinculoId, [FromServices] RemoverDocumentoAnexoServico servico)
        {
            try
            {
                servico.Executar(id, vinculoId);
                return Ok();
            }
            catch (SistemaException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { e.Erros });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
