using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class start_data_base : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "arquivo",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_cadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    excluido = table.Column<bool>(nullable: false),
                    usuario_cadastro = table.Column<string>(maxLength: 120, nullable: true),
                    usuario_exclusao = table.Column<string>(maxLength: 120, nullable: true),
                    usuario_alteracao = table.Column<string>(maxLength: 120, nullable: true),
                    binario = table.Column<byte[]>(nullable: true),
                    mime_type = table.Column<string>(maxLength: 50, nullable: true),
                    nome = table.Column<string>(maxLength: 150, nullable: true),
                    caminho = table.Column<string>(maxLength: 250, nullable: true),
                    hash = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_arquivo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contato",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_cadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    excluido = table.Column<bool>(nullable: false),
                    usuario_cadastro = table.Column<string>(maxLength: 120, nullable: true),
                    usuario_exclusao = table.Column<string>(maxLength: 120, nullable: true),
                    usuario_alteracao = table.Column<string>(maxLength: 120, nullable: true),
                    telefone = table.Column<string>(maxLength: 20, nullable: true),
                    email = table.Column<string>(maxLength: 100, nullable: true),
                    nome = table.Column<string>(maxLength: 100, nullable: true),
                    observacao = table.Column<string>(maxLength: 250, nullable: true),
                    referencia_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contato", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "documento",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_cadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    excluido = table.Column<bool>(nullable: false),
                    usuario_cadastro = table.Column<string>(maxLength: 120, nullable: true),
                    usuario_exclusao = table.Column<string>(maxLength: 120, nullable: true),
                    usuario_alteracao = table.Column<string>(maxLength: 120, nullable: true),
                    validade = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    tipo_documento_id = table.Column<Guid>(nullable: false),
                    numero = table.Column<string>(maxLength: 100, nullable: true),
                    observacao = table.Column<string>(maxLength: 100, nullable: true),
                    referencia_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_documento", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "projeto",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_cadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    excluido = table.Column<bool>(nullable: false),
                    usuario_cadastro = table.Column<string>(maxLength: 120, nullable: true),
                    usuario_exclusao = table.Column<string>(maxLength: 120, nullable: true),
                    usuario_alteracao = table.Column<string>(maxLength: 120, nullable: true),
                    nome = table.Column<string>(nullable: true),
                    data_inicio = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_fim = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projeto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_cadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    excluido = table.Column<bool>(nullable: false),
                    usuario_cadastro = table.Column<string>(maxLength: 120, nullable: true),
                    usuario_exclusao = table.Column<string>(maxLength: 120, nullable: true),
                    usuario_alteracao = table.Column<string>(maxLength: 120, nullable: true),
                    nome = table.Column<string>(maxLength: 80, nullable: true),
                    ativo = table.Column<bool>(nullable: false),
                    email = table.Column<string>(maxLength: 120, nullable: true),
                    senha = table.Column<string>(maxLength: 35, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "documento_anexo",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_cadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    excluido = table.Column<bool>(nullable: false),
                    usuario_cadastro = table.Column<string>(maxLength: 120, nullable: true),
                    usuario_exclusao = table.Column<string>(maxLength: 120, nullable: true),
                    usuario_alteracao = table.Column<string>(maxLength: 120, nullable: true),
                    arquivo_id = table.Column<Guid>(nullable: false),
                    documento_id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_documento_anexo", x => x.id);
                    table.ForeignKey(
                        name: "fk_documento_anexo_documento_documento_id",
                        column: x => x.documento_id,
                        principalTable: "documento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "atividade",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_cadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    excluido = table.Column<bool>(nullable: false),
                    usuario_cadastro = table.Column<string>(maxLength: 120, nullable: true),
                    usuario_exclusao = table.Column<string>(maxLength: 120, nullable: true),
                    usuario_alteracao = table.Column<string>(maxLength: 120, nullable: true),
                    projeto_id = table.Column<Guid>(nullable: false),
                    nome = table.Column<string>(nullable: true),
                    data_inicio = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_fim = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    finalizada = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_atividade", x => x.id);
                    table.ForeignKey(
                        name: "fk_atividade_projeto_projeto_id",
                        column: x => x.projeto_id,
                        principalTable: "projeto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "token",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_cadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    excluido = table.Column<bool>(nullable: false),
                    usuario_cadastro = table.Column<string>(maxLength: 120, nullable: true),
                    usuario_exclusao = table.Column<string>(maxLength: 120, nullable: true),
                    usuario_alteracao = table.Column<string>(maxLength: 120, nullable: true),
                    valor = table.Column<string>(nullable: true),
                    usuario_id = table.Column<Guid>(nullable: false),
                    data_expiracao = table.Column<DateTime>(nullable: false),
                    sessao_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_token", x => x.id);
                    table.ForeignKey(
                        name: "fk_token_usuario_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_atividade_projeto_id",
                table: "atividade",
                column: "projeto_id");

            migrationBuilder.CreateIndex(
                name: "ix_contato_referencia_id",
                table: "contato",
                column: "referencia_id");

            migrationBuilder.CreateIndex(
                name: "ix_documento_anexo_documento_id",
                table: "documento_anexo",
                column: "documento_id");

            migrationBuilder.CreateIndex(
                name: "ix_token_usuario_id",
                table: "token",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuario_email",
                table: "usuario",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "arquivo");

            migrationBuilder.DropTable(
                name: "atividade");

            migrationBuilder.DropTable(
                name: "contato");

            migrationBuilder.DropTable(
                name: "documento_anexo");

            migrationBuilder.DropTable(
                name: "token");

            migrationBuilder.DropTable(
                name: "projeto");

            migrationBuilder.DropTable(
                name: "documento");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
