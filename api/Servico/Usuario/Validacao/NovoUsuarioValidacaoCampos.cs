using Servico.DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servico.Usuario.Validacao
{
    public class NovoUsuarioValidacaoCampos : BaseValidacao
    {

        public NovoUsuarioValidacaoCampos(NovoUsuarioDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                Erros.Add("Informe o campo E-mail.");
            else if (dto.Email.Length > 120)
                Erros.Add("O campo E-mail não pode possuir mais de 120 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.Nome))
                Erros.Add("Informe o campo Nome.");
            else if (dto.Nome.Length > 80)
                Erros.Add("O campo Nome não pode possuir mais de 80 caracteres.");

            if (string.IsNullOrWhiteSpace(dto.Senha))
                Erros.Add("Informe o campo Senha.");
            else if (dto.Senha.Length > 35)
                Erros.Add("O campo Senha não pode possuir mais de 35 caracteres.");
        }
    }
}
