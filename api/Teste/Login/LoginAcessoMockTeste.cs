using Dominio.Entidade;
using Infra.Exception;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Persistencia;
using Servico.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teste.Login
{
    [TestClass]
    public class LoginAcessoMockTeste
    {
        [TestMethod]
        [ExpectedException(typeof(SistemaException), "Limite de acesso atingido na conta.")]
        public void AcessoInvalido()
        {
            var data = new List<Token>
            {
                new Token{Id = Guid.Parse("8f565930-1b72-4a09-b395-906521b30b1c"), DataExpiracao = DateTime.Now.AddDays(1), UsuarioId = Guid.Parse("689993d6-18cb-451d-99fe-37135cf9a992"), Valor = "Token" },
                new Token{Id = Guid.Parse("de7568a7-d22a-45ed-9e9c-3093ecf39846"), DataExpiracao = DateTime.Now.AddDays(1), UsuarioId = Guid.Parse("e874712c-e17b-4f91-9958-b42242cc99fa"), Valor = "Token1" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Token>>();
            mockSet.As<IQueryable<Token>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Token>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Token>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Token>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.Token).Returns(mockSet.Object);

            var servico = new LoginServico(mockContext.Object);

            var usuario = new Usuario { Ativo = true, Email = "teste@teste.com", Id = Guid.Parse("d8efe592-140c-44d4-893f-a0e0db648f8f"), Nome = "Nome", Excluido = false };

            servico.ValidarAcesso(usuario);
        }

        [TestMethod]
        public void AcessoValido()
        {
            var data = new List<Token>
            {
                new Token{Id = Guid.Parse("8f565930-1b72-4a09-b395-906521b30b1c"), DataExpiracao = DateTime.Now.AddDays(1), UsuarioId = Guid.Parse("689993d6-18cb-451d-99fe-37135cf9a992"), Valor = "Token" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Token>>();
            mockSet.As<IQueryable<Token>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Token>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Token>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Token>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.Token).Returns(mockSet.Object);

            var usuario = new Usuario { Ativo = true, Email = "teste@teste.com", Id = Guid.Parse("d8efe592-140c-44d4-893f-a0e0db648f8f"), Nome = "Nome", Excluido = false };

            var servico = new LoginServico(mockContext.Object);
            var resultado = servico.ValidarAcesso(usuario);
            Assert.IsNotNull(resultado);
            Assert.AreEqual(resultado.UsuarioId, usuario.Id);
            Assert.AreEqual(resultado.Email, usuario.Email);
            Assert.AreEqual(resultado.Nome, usuario.Nome);
        }
    }
}
