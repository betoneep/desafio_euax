using System;
using System.Net;
using Infra.Exception;
using Microsoft.AspNetCore.Mvc;
using Servico.DTO.Login;
using Servico.Login;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginServico _loginServico;
        public LoginController(LoginServico servico)
        {
            _loginServico = servico;
        }

        [HttpPost("autenticar")]
        public IActionResult Autenticar([FromBody] LoginDTO dto)
        {
            try
            {
                return Ok(_loginServico.Autenticar(dto));
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