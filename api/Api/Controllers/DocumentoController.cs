using Infra.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servico.Documento;
using Servico.DTO.Documento;
using System;
using System.Net;

namespace Api.Controllers
{

    [Route("api/[controller]")]
    [Authorize("Bearer")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        public DocumentoController()
        {
        }

        [HttpPost("novo")]
        public IActionResult Novo([FromBody] DocumentoDTO dto, [FromServices] NovoDocumentoServico servico)
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
        public IActionResult Editar([FromBody] DocumentoDTO dto, [FromServices] EditarDocumentoServico servico)
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
        public IActionResult Obter(Guid id, [FromServices] DocumentoServico servico)
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

        [HttpGet("obter-grid/{pagina}/{itensPorPagina}/{referenciaId}")]
        public IActionResult ObterGrid(int pagina, int itensPorPagina, Guid referenciaId, [FromServices] DocumentoServico servico)
        {
            try
            {
                var dto = servico.ObterGrid(pagina, itensPorPagina, referenciaId);

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


        [HttpDelete("remover/{id}")]
        public IActionResult Remover(Guid id, [FromServices] RemoverDocumentoServico servico)
        {
            try
            {
                servico.Executar(id);
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
