using Microsoft.IdentityModel.Tokens;
using Servico.DTO.Login;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Infra.Constante;
using System.Security.Claims;

namespace Servico.JWT
{
    public class AutenticacaoFabrica
    {
        private readonly LoginValidoDTO _acesso;

        public AutenticacaoFabrica(LoginValidoDTO acesso)
        {
            _acesso = acesso;
        }

        public LoginValidoDTO Constuir()
        {
            _acesso.DataExpiracao = DateTime.Now.AddHours(12);
            _acesso.SessaoId = Guid.NewGuid();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JWTConstante.CHAVE_SECRETA);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(JWTConstante.CLAIM_ID, _acesso.UsuarioId.ToString()),
                    new Claim(JWTConstante.CLAIM_NOME, _acesso.Nome.ToString()),
                    new Claim(JWTConstante.CLAIM_EMAIL, _acesso.Email.ToString())
                }),
                Claims = MontarPermissoes(),
                Expires = _acesso.DataExpiracao,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenObj = tokenHandler.CreateToken(tokenDescriptor);
            _acesso.Token = tokenHandler.WriteToken(tokenObj);

            return _acesso;
        }

        public override bool Equals(object obj)
        {
            return obj is AutenticacaoFabrica fabrica &&
                   EqualityComparer<LoginValidoDTO>.Default.Equals(_acesso, fabrica._acesso);
        }

        private IDictionary<string, object> MontarPermissoes()
        {
            var dicionario = new Dictionary<string, object>();
            return dicionario;
        }
    }
}
