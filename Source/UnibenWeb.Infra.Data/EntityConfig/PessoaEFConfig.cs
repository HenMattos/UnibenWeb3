using UnibenWeb.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace UnibenWeb.Infra.Data.EntityConfig
{
    public class PessoaEFConfig : EntityTypeConfiguration<Pessoa>
    {
        public PessoaEFConfig()
        {
            ToTable("Pessoas");
            HasKey(p => p.PessoaId);
            Ignore(c => c.ResultadoValidacao);

            Property(p => p.Nome)
                .HasMaxLength(150)
                .HasColumnType("varchar")
                .IsRequired();

            Property(p => p.CPF_CNPJ)
                .HasColumnName("CPF_CNPJ")
                .IsRequired()
                .HasMaxLength(14);

            Property(p => p.NacionalidadeId).IsOptional();
            Property(p => p.NaturalidadeId).IsOptional();



            // Relacionamento (1) para (1) 
            // HasRequired(p => p.EstadoCivil)
            //    .WithRequiredDependent() // Pessoa(this)
            //    .Map(p => p.MapKey("EstadoCivilId"));

            // Relacionamento (1:0) para (1) 
            // HasOptional(p => p.Banco)
            //    .WithOptionalDependent() // Dependent = Pessoa(this)
            //    .Map(p => p.MapKey("BancoId"));

            // Relacionamento P (N) para EC (1) 
            //HasRequired(s => s.EstadoCivil).WithMany(s => s.Pessoas).HasForeignKey(s => s.EstadoCivilId);


            //http://stackoverflow.com/questions/18719890/one-to-zero-or-one-with-hasforeignkey

            // Exclusivos
            HasRequired(s => s.Banco).WithMany().HasForeignKey(s => s.BancoId);
            HasRequired(s => s.PessoaTipo).WithMany(s => s.Pessoas).HasForeignKey(s => s.PessoaTipoId);
            //HasRequired(p => p.Banco).WithOptional().Map(p => p.MapKey("BancoId"));
            //HasRequired(p => p.PessoaTipo).WithOptional().Map(p => p.MapKey("PessoaTipoId"));

            // Exclusivos para Pessoa Física
            //HasOptional(s => s.EstadoCivil).WithOptionalDependent().Map(p => p.MapKey("EstadoCivilId"));
            //HasOptional(p => p.Sexo).WithOptionalDependent().Map(p => p.MapKey("PessoaSexoId"));

            HasOptional(s => s.EstadoCivil).WithMany().HasForeignKey(s=>s.EstadoCivilId);
            HasOptional(s => s.Sexo).WithMany().HasForeignKey(s => s.SexoId);

            //Relacionamento Enderecos(N) para Pessoas(N)
            /*
            HasMany(e => e.Enderecos)
                .WithMany()
                .Map(me =>
                {
                    me.MapLeftKey("PessoaId");
                    me.MapRightKey("EnderecoId");
                    me.ToTable("PessoaEndereco");          // Cria tabela de suporte para relacionamento
                });
                */

            // Relacionamento (N) Enderecos (1) Pessoa 
            HasMany(e => e.Enderecos)
                .WithRequired(p => p.Pessoa)
                .HasForeignKey(p => p.PessoaId)
                .WillCascadeOnDelete(true);

        }
    }
}
