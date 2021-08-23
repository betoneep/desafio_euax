using Persistencia;
using Servico.DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Infra.Utils;

namespace Servico.Usuario.Validacao
{
    public class EditarUsuarioValidacaoBanco : BaseValidacao
    {
        public EditarUsuarioValidacaoBanco(Contexto contexto, EditarUsuarioDTO dto)
        {

            if (contexto.Usuario.Any(x => x.Email.ToLower().Trim() == dto.Email.ToLower().Trim()
            && !x.Excluido
            && x.Id != dto.Id))
                Erros.Add($"Já existe um usuário com mesmo E-mail. Informe outro E-mail ou utilize a edição de usuário.");
        }
    }
}
