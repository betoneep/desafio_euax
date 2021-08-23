using System;
using System.Net;
using Infra.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servico.DTO.Usuario;
using Servico.Usuario;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize("Bearer")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public UsuarioController()
        {
        }

        [HttpPost("novo")]
        [AllowAnonymous]
        public IActionResult Novo([FromBody] NovoUsuarioDTO dto, [FromServices] NovoUsuarioServico servico)
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
        public IActionResult Editar([FromBody] EditarUsuarioDTO dto, [FromServices] EditarUsuarioServico servico)
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

        [HttpGet("obter/{usuarioId}")]
        public IActionResult Obter(Guid usuarioId, [FromServices] UsuarioServico servico)
        {
            try
            {
                var dto = servico.Obter(usuarioId);

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

        [HttpGet("obter-grid/{pagina}/{itensPorPagina}")]
        public IActionResult ObterGrid(int pagina, int itensPorPagina, [FromServices] UsuarioServico servico)
        {
            try
            {
                var dto = servico.ObterGrid(pagina, itensPorPagina);

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
        public IActionResult Remover(Guid id, [FromServices] RemoverUsuarioServico servico)
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