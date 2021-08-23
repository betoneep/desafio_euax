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

namespace Teste
{
    [TestClass]
    public class LoginMockTeste
    {

        [TestMethod]
        [ExpectedException(typeof(SistemaException), "E-mail/Senha inválido.")]
        public void EmailSenhaInvalido()
        {
            var data = new List<Usuario>
            {
                new Usuario { Ativo = true, Excluido = false, Email = "teste@teste.com", Id = Guid.NewGuid(), Nome = "Nome", Senha = "698DC19D489C4E4DB73E28A713EAB07B" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Usuario>>();
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.Usuario).Returns(mockSet.Object);

            var servico = new LoginServico(mockContext.Object);
            servico.ValidarUsuario(new Servico.DTO.Login.LoginDTO { Email = "email@email", Senha = "123" });
        }

        [TestMethod]
        [ExpectedException(typeof(SistemaException), "Usuário está inativo.")]
        public void UsuarioInativo()
        {
            var data = new List<Usuario>
            {
                new Usuario { Ativo = false, Excluido = false, Email = "teste@teste.com", Id = Guid.NewGuid(), Nome = "Nome", Senha = "698DC19D489C4E4DB73E28A713EAB07B" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Usuario>>();
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.Usuario).Returns(mockSet.Object);

            var servico = new LoginServico(mockContext.Object);
            servico.ValidarUsuario(new Servico.DTO.Login.LoginDTO { Email = "teste@teste.com", Senha = "123" });
        }

        [TestMethod]
        [ExpectedException(typeof(SistemaException), "Usuário está excluído.")]
        public void UsuarioExcluido()
        {
            var data = new List<Usuario>
            {
                new Usuario { Ativo = true, Excluido = true, Email = "teste@teste.com", Id = Guid.NewGuid(), Nome = "Nome", Senha = "698DC19D489C4E4DB73E28A713EAB07B" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Usuario>>();
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.Usuario).Returns(mockSet.Object);

            var servico = new LoginServico(mockContext.Object);
            servico.ValidarUsuario(new Servico.DTO.Login.LoginDTO { Email = "teste@teste.com", Senha = "123" });
        }

        [TestMethod]
        public void UsuarioSucesso()
        {

            var data = new List<Usuario>
            {
                new Usuario { Ativo = true, Excluido = false, Email = "teste@teste.com", Id = Guid.Parse("3b1ca01c-405b-40c3-b8d7-174e25d83d9d"),
                    Nome = "Nome", Senha = "698DC19D489C4E4DB73E28A713EAB07B"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Usuario>>();
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.Usuario).Returns(mockSet.Object);

            var dto = new Servico.DTO.Login.LoginDTO { Email = "teste@teste.com", Senha = "teste" };
            var servico = new LoginServico(mockContext.Object);
            var usuario = servico.ValidarUsuario(dto);
            Assert.IsNotNull(usuario);
            Assert.AreEqual(usuario.Email, dto.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(SistemaException), "Nenhuma conta ativa para o e-mail informado.")]
        public void UsuarioContaInativa()
        {
            var data = new List<Usuario>
            {
                new Usuario { Ativo = true, Excluido = false, Email = "teste@teste.com", Id = Guid.Parse("3b1ca01c-405b-40c3-b8d7-174e25d83d9d"),
                    Nome = "Nome", Senha = "698DC19D489C4E4DB73E28A713EAB07B"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Usuario>>();
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.Usuario).Returns(mockSet.Object);

            var servico = new LoginServico(mockContext.Object);
            servico.ValidarUsuario(new Servico.DTO.Login.LoginDTO { Email = "teste@teste.com", Senha = "teste" });
        }

        [TestMethod]
        [ExpectedException(typeof(SistemaException), "Nenhuma conta ativa para o e-mail informado.")]
        public void UsuarioContaExcluida()
        {
            var data = new List<Usuario>
            {
                new Usuario { Ativo = true, Excluido = false, Email = "teste@teste.com", Id = Guid.Parse("3b1ca01c-405b-40c3-b8d7-174e25d83d9d"),
                    Nome = "Nome", Senha = "698DC19D489C4E4DB73E28A713EAB07B"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Usuario>>();
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.Usuario).Returns(mockSet.Object);

            var servico = new LoginServico(mockContext.Object);
            servico.ValidarUsuario(new Servico.DTO.Login.LoginDTO { Email = "teste@teste.com", Senha = "teste" });
        }
    }
}
