using System;
using Microsoft.AspNetCore.Mvc;
using Infra.Exception;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Servico.Arquivo;
using Microsoft.AspNetCore.Http;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ArquivoController : ControllerBase
    {
        public ArquivoController()
        {
        }

        [Authorize("Bearer")]
        [HttpPost("novo")]
        [RequestSizeLimit(100_000_000)]
        public IActionResult Novo(IFormFile[] arquivo, [FromServices] NovoArquivoServico servico)
        {
            try
            {
                return Ok(servico.Executar(arquivo));
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

        //Action sem autenticação, para realizar download do arquivo
        [HttpGet("obter/{id}")]
        public IActionResult Obter(Guid id, [FromServices] ArquivoServico servico)
        {
            try
            {
                var dto = servico.Obter(id);
                return File(dto.Binario, dto.MimeType);
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
