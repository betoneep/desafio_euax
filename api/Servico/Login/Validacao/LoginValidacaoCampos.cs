using Servico.DTO.Login;

namespace Servico.Login.Validacao
{
    public class LoginValidacaoCampos : BaseValidacao
    {

        public LoginValidacaoCampos(LoginDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                Erros.Add("Informe o campo E-mail.");

            if (string.IsNullOrWhiteSpace(dto.Senha))
                Erros.Add("Informe o campo senha.");
        }

    }
}
