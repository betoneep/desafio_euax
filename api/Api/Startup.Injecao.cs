using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistencia;
using Servico.Arquivo;
using Servico.Atividade;
using Servico.Documento;
using Servico.DocumentoAnexo;
using Servico.DTO.Usuario;
using Servico.Login;
using Servico.Projeto;
using Servico.Usuario;

namespace Api
{
    public partial class Startup
    {
        public void CarregarInjecao(IServiceCollection services)
        {
            services.AddDbContext<Contexto>(x =>
                x.UseNpgsql(Configuration.GetConnectionString("Defa‌​ultConnection")));

            //Login
            services.AddScoped<LoginServico>();

            //Usuario
            services.AddScoped<NovoUsuarioServico>();
            services.AddScoped<EditarUsuarioServico>();
            services.AddScoped<RemoverUsuarioServico>();
            services.AddScoped<UsuarioServico>();

            //Documento
            services.AddScoped<NovoDocumentoServico>();
            services.AddScoped<EditarDocumentoServico>();
            services.AddScoped<RemoverDocumentoServico>();
            services.AddScoped<DocumentoServico>();

            //Projeto
            services.AddScoped<NovoProjetoServico>();
            services.AddScoped<EditarProjetoServico>();
            services.AddScoped<RemoverProjetoServico>();
            services.AddScoped<ProjetoServico>();

            //Atividade
            services.AddScoped<NovaAtividadeServico>();
            services.AddScoped<EditarAtividadeServico>();
            services.AddScoped<RemoverAtividadeServico>();
            services.AddScoped<AtividadeServico>();

            //Documento Anexo
            services.AddScoped<NovoDocumentoAnexoServico>();
            services.AddScoped<EditarDocumentoAnexoServico>();
            services.AddScoped<RemoverDocumentoAnexoServico>();
            services.AddScoped<DocumentoAnexoServico>();

            //Anexo
            services.AddScoped<ArquivoServico>();
            services.AddScoped<NovoArquivoServico>();

            services.AddHttpContextAccessor();
            services.AddScoped<UsuarioCorrente>();
        }
    }
}
