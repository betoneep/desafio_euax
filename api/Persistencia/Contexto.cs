using Dominio.Entidade;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Npgsql.NameTranslation;
using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Persistencia
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }
        public Contexto()
        {
        }

        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Token> Token { get; set; }
        public virtual DbSet<Documento> Documento { get; set; }
        public virtual DbSet<DocumentoAnexo> DocumentoAnexo { get; set; }
        public virtual DbSet<Arquivo> Arquivo { get; set; }
        public virtual DbSet<Contato> Contato { get; set; }
        public virtual DbSet<Atividade> Atividade { get; set; }
        public virtual DbSet<Projeto> Projeto { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            RegistrarIndices(modelBuilder);

            UseSnakeCaseNames(modelBuilder);
        }

        void RegistrarIndices(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Usuario>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder
                .Entity<Contato>()
                .HasIndex(c => c.ReferenciaId);
        }


        public readonly Regex _keysRegex = new Regex("^(PK|FK|IX)_", RegexOptions.Compiled);

        void UseSnakeCaseNames(ModelBuilder modelBuilder)
        {
            var mapper = new NpgsqlSnakeCaseNameTranslator();

            foreach (var table in modelBuilder.Model.GetEntityTypes())
            {

                ConvertToSnake(mapper, table);

                foreach (var property in table.GetProperties())
                {
                    ConvertToSnake(mapper, property);
                }

                foreach (var primaryKey in table.GetKeys())
                {
                    ConvertToSnake(mapper, primaryKey);
                }

                foreach (var foreignKey in table.GetForeignKeys())
                {
                    ConvertToSnake(mapper, foreignKey);
                }

                foreach (var indexKey in table.GetIndexes())
                {
                    ConvertToSnake(mapper, indexKey);
                }
            }
        }

        void ConvertToSnake(INpgsqlNameTranslator mapper, object entity)
        {
            switch (entity)
            {
                case IMutableEntityType table:
                    table.SetTableName(ConvertGeneralToSnake(mapper, table.GetTableName()));
                    break;
                case IMutableProperty property:
                    property.SetColumnName(ConvertGeneralToSnake(mapper, property.GetColumnName()));
                    break;
                case IMutableKey primaryKey:
                    primaryKey.SetName(ConvertKeyToSnake(mapper, primaryKey.GetName()));
                    break;
                case IMutableForeignKey foreignKey:
                    foreignKey.SetConstraintName(ConvertKeyToSnake(mapper, foreignKey.GetConstraintName()));
                    break;
                case IMutableIndex indexKey:
                    indexKey.SetName(ConvertKeyToSnake(mapper, indexKey.GetName()));
                    break;
                default:
                    throw new NotImplementedException("Unexpected type was provided to snake case converter");
            }
        }

        string ConvertKeyToSnake(INpgsqlNameTranslator mapper, string keyName) =>
           ConvertGeneralToSnake(mapper, _keysRegex.Replace(keyName, match => match.Value.ToLower()));

        string ConvertGeneralToSnake(INpgsqlNameTranslator mapper, string entityName) =>
           mapper.TranslateMemberName(entityName);



    }
}
