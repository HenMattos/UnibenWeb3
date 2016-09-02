using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using UnibenWeb.Domain.Entities;
using UnibenWeb.Infra.Data.EntityConfig;

namespace UnibenWeb.Infra.Data.Context
{
    public class UnibenContext : DbContext
    {
        public UnibenContext()
            : base("UnibenConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<UnibenContext>());
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<PessoaTipo> PessoaTipos { get; set; }
        public DbSet<PessoaSexo> PessoaSexos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<EstadoCivil> EstadoCivis { get; set; }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<CheckListContrato> CheckListContratos { get; set; }
        public DbSet<SegmentoAssistencial> SegmentoAssistenciais { get; set; }
        public DbSet<TipoContratacaoProduto> TipoContratacaoProdutos { get; set; }
        public DbSet<AbrangenciaPlano> AbrangenciaPlanos { get; set; }
        public DbSet<FatorModerador> FatorModeradores { get; set; }
        public DbSet<AcomodacaoTipo> AcomodacaoTipos { get; set; }
        public DbSet<PagarConta> PagarContas { get; set; }
        public DbSet<PagarContaParcela> PagarContaParcelas { get; set; }
        public DbSet<CentroCusto> CentroCustos { get; set; }
        public DbSet<ContaContabil> ContaContabeis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            // ~~~ Precisa comentar o modelBuilder para gerar o scatfold, pois ele não entende.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new PessoaEFConfig());
            modelBuilder.Configurations.Add(new EnderecoEFConfig());
            modelBuilder.Configurations.Add(new EstadoCivilEFConfig());
            modelBuilder.Configurations.Add(new TelContatoEFConfig());
            modelBuilder.Configurations.Add(new SegmentoAssistencialEFConfig());
            modelBuilder.Configurations.Add(new ModoPagamentoEFConfig());
            modelBuilder.Configurations.Add(new GrauParentescoEFConfig());
            modelBuilder.Configurations.Add(new BancoEFConfig());
            modelBuilder.Configurations.Add(new AcomodacaoTipoEFConfig());
            modelBuilder.Configurations.Add(new AbrangenciaPlanoEFConfig());
            modelBuilder.Configurations.Add(new PessoaSexoEfConfig());
            modelBuilder.Configurations.Add(new PessoaTipoEfConfig());
            modelBuilder.Configurations.Add(new ProdutoEfConfig());
            modelBuilder.Configurations.Add(new PagarContaEfConfig());
            modelBuilder.Configurations.Add(new CentroCustoEfConfig());
            modelBuilder.Configurations.Add(new PagarContaParcelaEfConfig());
            modelBuilder.Configurations.Add(new ContaContabilEfConfig());
            // ~~~

            base.OnModelCreating(modelBuilder);
            
        }


        public override int SaveChanges()
        {
            try
            {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Data_inclusao") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("Data_inclusao").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("Data_inclusao").IsModified = false;
                }
            }
                return base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                        DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
                //Write to file
                System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);
                throw;

                // Showing it on screen
                throw new Exception(string.Join(",", outputLines.ToArray()));
            }
        }
    }
}