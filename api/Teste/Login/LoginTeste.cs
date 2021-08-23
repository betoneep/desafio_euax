using Microsoft.VisualStudio.TestTools.UnitTesting;
using Servico.DTO.Login;
using Servico.Login.Validacao;
using System.Linq;

namespace Teste
{
    [TestClass]
    public class LoginTeste
    {
        [TestMethod]
        public void EmailNulo()
        {
            var dto = new LoginDTO {Senha = "123" };
            var validador = new LoginValidacaoCampos(dto);
            Assert.IsNotNull(validador.Erros);
            Assert.IsTrue(validador.Erros.Any(a => a == "Informe o campo E-mail."));
        }

        [TestMethod]
        public void EmailVazio()
        {
            var dto = new LoginDTO { Senha = "123", Email = "" };
            var validador = new LoginValidacaoCampos(dto);
            
            Assert.IsNotNull(validador.Erros);
            Assert.IsTrue(validador.Erros.Any(a => a == "Informe o campo E-mail."));
        }

        [TestMethod]
        public void SenhaNulo()
        {
            var dto = new LoginDTO { Email = "teste@teste.com" };
            var validador = new LoginValidacaoCampos(dto);
            Assert.IsNotNull(validador.Erros);
            Assert.IsTrue(validador.Erros.Any(a => a == "Informe o campo senha."));
        }

        [TestMethod]
        public void SenhaVazio()
        {
            var dto = new LoginDTO { Email = "teste@teste.com", Senha = "" };
            var validador = new LoginValidacaoCampos(dto);
            Assert.IsNotNull(validador.Erros);
            Assert.IsTrue(validador.Erros.Any(a => a == "Informe o campo senha."));
        }

        [TestMethod]
        public void EmailSenhaPreenchido()
        {
            var dto = new LoginDTO { Email = "teste@teste.com", Senha = "123" };
            var validador = new LoginValidacaoCampos(dto);
            Assert.IsNotNull(validador.Erros);
            Assert.AreEqual(0, validador.Erros.Count());
        }
    }
}
