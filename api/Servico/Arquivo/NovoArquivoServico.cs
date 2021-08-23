using Infra.Exception;
using Microsoft.AspNetCore.Http;
using Persistencia;
using Servico.Arquivo.Validacao;
using Servico.DTO.Usuario;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Servico.Arquivo
{
    public class NovoArquivoServico
    {
        private readonly Contexto _contexto;
        private readonly UsuarioCorrente _usuarioCorrente;
        public NovoArquivoServico(Contexto contexto, UsuarioCorrente usuarioCorrente)
        {
            _contexto = contexto;
            _usuarioCorrente = usuarioCorrente;
        }

        public Guid[] Executar(ICollection<IFormFile> arquivo)
        {
            var validacao = new NovoArquivoValidacao(arquivo);
            if (!validacao.IsValido)
                throw new SistemaException(validacao.Erros);

            var arquivos = arquivo.Select(s => new Dominio.Entidade.Arquivo
            {
                MimeType = s.ContentType,
                Nome = s.FileName,
                DataCadastro = DateTime.Now,
                UsuarioCadastro = _usuarioCorrente.Email,
                Binario = GerarBinario(s)
            }).ToList();

            GerarHash(arquivos);
            Salvar(arquivos);

            return arquivos.Select(s => s.Id).ToArray();
        }

        private void Salvar(List<Dominio.Entidade.Arquivo> arquivos)
        {

            using (var transacao = _contexto.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in arquivos)
                    {
                        if (item.Id == Guid.Empty)
                        {
                            _contexto.Arquivo.Add(item);
                            _contexto.SaveChanges();
                        }
                    }
                    transacao.Commit();
                }
                catch (Exception)
                {
                    transacao.Rollback();
                    throw new SistemaException("Falha ao salvar os arquivos.");
                }
            }

        }

        private byte[] GerarBinario(IFormFile arquivo)
        {
            byte[] binario;
            using (var memoryStream = new MemoryStream())
            {
                arquivo.CopyToAsync(memoryStream).Wait();
                binario = memoryStream.ToArray();
            }
            return binario;
        }

        private void GerarHash(List<Dominio.Entidade.Arquivo> arquivo)
        {
            foreach (var item in arquivo)
            {
                var md5 = MD5.Create();
                var hash = BytesToString(md5.ComputeHash(item.Binario));
                var arq = _contexto.Arquivo
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Hash == hash);

                if (arq == null)
                    item.Hash = hash;
                else
                    item.Id = arq.Id;
            }
        }

        private string BytesToString(byte[] bytes)
        {
            string result = string.Empty;
            foreach (var b in bytes)
                result += b.ToString("x2");
            return result;
        }
    }
}
