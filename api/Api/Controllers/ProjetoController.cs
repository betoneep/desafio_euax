using System;
using System.Net;
using Infra.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servico.DTO.Projeto;
using Servico.Projeto;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize("Bearer")]
    [ApiController]
    public class ProjetoController : ControllerBase
    {
        public ProjetoController()
        {
        }

        [HttpPost("novo")]
        public IActionResult Novo([FromBody] ProjetoDTO dto, [FromServices] NovoProjetoServico servico)
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
        public IActionResult Editar([FromBody] ProjetoDTO dto, [FromServices] EditarProjetoServico servico)
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
        public IActionResult Obter(Guid id, [FromServices] ProjetoServico servico)
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

        [HttpGet("obter-grid")]
        public IActionResult ObterGrid([FromQuery] ProjetoFiltroDTO filtro, [FromServices] ProjetoServico servico)
        {
            try
            {
                var dto = servico.ObterGrid(filtro);

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
        public IActionResult Remover(Guid id, [FromServices] RemoverProjetoServico servico)
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

        [HttpGet("obter-select")]
        public IActionResult ObterSelect([FromServices] ProjetoServico servico)
        {
            try
            {
                var dto = servico.ObterSelect();

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
    }
}
