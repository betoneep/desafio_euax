using Infra.Utils;
using Persistencia;
using Servico.DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servico.Usuario.Validacao
{
    public class NovoUsuarioValidacaoBanco : BaseValidacao
    {
        public NovoUsuarioValidacaoBanco(Contexto contexto, NovoUsuarioDTO dto)
        {
            if (contexto.Usuario.Any(x => x.Email.ToLower().Trim() == dto.Email.ToLower().Trim() && !x.Excluido))
                Erros.Add($"Já existe um usuário com mesmo E-mail. Informe outro E-mail ou utilize a edição de usuário.");
        }
    }
}
