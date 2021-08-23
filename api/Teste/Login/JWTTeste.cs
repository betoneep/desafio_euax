using Dominio.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Persistencia;
using Servico.DTO.Login;
using Servico.JWT;
using Servico.Login;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Teste.Login
{
    [TestClass]
    public class JWTTeste
    {
        [TestMethod]
        public void JWTValido()
        {
            var acesso = new LoginValidoDTO
            {
                Email = "teste@teste.com",
                Nome = "Nome",
                UsuarioId = Guid.Parse("abc13b26-9f27-43dd-9a74-83899640e4f2")
            };
            var resultado = new AutenticacaoFabrica(acesso).Constuir();
            Assert.IsNotNull(resultado);
            Assert.IsNotNull(resultado.Token);
            Assert.AreEqual(resultado.Email, acesso.Email);
            Assert.AreEqual(resultado.Nome, acesso.Nome);
            Assert.AreEqual(resultado.UsuarioId, acesso.UsuarioId);
            Assert.AreNotEqual(resultado.SessaoId, Guid.Empty);
            Assert.IsTrue(resultado.DataExpiracao > DateTime.Now);
        }

        [TestMethod]
        public void RemoverTokenUsuarioExistente()
        {
            var data = new List<Token>
            {
                new Token{Id = Guid.Parse("8f565930-1b72-4a09-b395-906521b30b1c"), Excluido = false, DataExclusao = null, UsuarioExclusao = null, UsuarioId = Guid.Parse("689993d6-18cb-451d-99fe-37135cf9a992"), Valor = "Token" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Token>>();
            mockSet.As<IQueryable<Token>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Token>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Token>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Token>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.Token).Returns(mockSet.Object);

            var servico = new LoginServico(mockContext.Object);

            var dto = new LoginValidoDTO { UsuarioId = Guid.Parse("689993d6-18cb-451d-99fe-37135cf9a992"), Email = "teste@teste.com" };
            servico.RemoverTokenUsuario(dto);
            Assert.IsTrue(data.Any(x => x.Excluido));
            Assert.IsNotNull(data.FirstOrDefault().UsuarioExclusao);
            Assert.IsNotNull(data.FirstOrDefault().DataExclusao);
            Assert.AreEqual(data.FirstOrDefault().UsuarioExclusao, dto.Email);
        }

        [TestMethod]
        public void RemoverTokenOutroUsuarioExistente()
        {
            var data = new List<Token>
            {
                new Token{Id = Guid.Parse("8f565930-1b72-4a09-b395-906521b30b1c"), Excluido = false, DataExclusao = null, UsuarioExclusao = null, UsuarioId = Guid.Parse("689993d6-18cb-451d-99fe-37135cf9a992"), Valor = "Token" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Token>>();
            mockSet.As<IQueryable<Token>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Token>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Token>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Token>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.Token).Returns(mockSet.Object);

            var servico = new LoginServico(mockContext.Object);

            var dto = new LoginValidoDTO { UsuarioId = Guid.Parse("58c7fb0e-1fa2-4479-ab96-10236954e06a"), Email = "teste@teste.com" };
            servico.RemoverTokenUsuario(dto);
            Assert.IsTrue(data.Any(x => !x.Excluido));
            Assert.IsNull(data.FirstOrDefault().UsuarioExclusao);
            Assert.IsNull(data.FirstOrDefault().DataExclusao);
        }

        [TestMethod]
        public void CriaToken()
        {
            var data = new List<Token>();
            var query = data.AsQueryable();

            var mockSet = new Mock<DbSet<Token>>();
            mockSet.As<IQueryable<Token>>().Setup(m => m.Provider).Returns(query.Provider);
            mockSet.As<IQueryable<Token>>().Setup(m => m.Expression).Returns(query.Expression);
            mockSet.As<IQueryable<Token>>().Setup(m => m.ElementType).Returns(query.ElementType);
            mockSet.As<IQueryable<Token>>().Setup(m => m.GetEnumerator()).Returns(query.GetEnumerator());
            mockSet.Setup(d => d.Add(It.IsAny<Token>())).Callback<Token>((s) => data.Add(s));


            var mockContext = new Mock<Contexto>();
            mockContext.Setup(c => c.Token).Returns(mockSet.Object);

            var servico = new LoginServico(mockContext.Object);

            var dto = new LoginValidoDTO
            {
                UsuarioId = Guid.Parse("58c7fb0e-1fa2-4479-ab96-10236954e06a"),
                Email = "teste@teste.com",
                DataExpiracao = DateTime.Now.AddMinutes(12),
                Nome = "Nome",
                SessaoId = Guid.NewGuid(),
                Token = "Token1"
            };
            servico.CriarTokenUsuario(dto);
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Any());
            Assert.AreEqual(data.FirstOrDefault().SessaoId, dto.SessaoId);
            Assert.AreEqual(data.FirstOrDefault().Valor, dto.Token);
            Assert.AreEqual(data.FirstOrDefault().UsuarioId, dto.UsuarioId);
            Assert.AreEqual(data.FirstOrDefault().UsuarioCadastro, dto.Email);
            Assert.AreEqual(data.FirstOrDefault().DataExpiracao, dto.DataExpiracao);
            Assert.IsNotNull(data.FirstOrDefault().DataCadastro);
        }
    }
}
