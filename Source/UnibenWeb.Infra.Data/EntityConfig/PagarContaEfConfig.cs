using System.Data.Entity.ModelConfiguration;
using UnibenWeb.Domain.Entities;

namespace UnibenWeb.Infra.Data.EntityConfig
{
    public class PagarContaEfConfig : EntityTypeConfiguration<PagarConta>
    {
        public PagarContaEfConfig()
        {
            ToTable("PagarContas");
            HasKey(p => p.PagarContaId);

            Ignore(c => c.ResultadoValidacao);

            // Relacionamento CentroCusto (1) para PagarConta (0..1) em CentroCustoId
            //HasRequired(s => s.CentroCusto).WithOptional().Map(x => x.MapKey("CentroCustoId"));
            //Relacionamento Enderecos(N) para Pessoas(N)
            
            HasMany(e => e.CentrosCusto)
                .WithMany()
                .Map(me =>
                {
                    me.MapLeftKey("ContaPagarId");
                    me.MapRightKey("CentroCustoId");
                    me.ToTable("ContaPagarCentroCusto");          // Cria tabela de suporte para relacionamento
                });
                

            // Relacionamento de Pessoa (1) para PagarConta (0..1) em FornecedorId
            //HasRequired(s => s.Fornecedor).WithOptional().Map(x => x.MapKey("FornecedorId"));
            HasOptional(s => s.Fornecedor).WithMany().HasForeignKey(x=>x.FornecedorId);


            // Relacionamento (N) Enderecos (1) Pessoa 
            HasMany(x => x.ContaParcelas)
                .WithRequired(p => p.ContaOrigem)
                .HasForeignKey(p => p.ContaOrigemId)
                .WillCascadeOnDelete(true);

        }
    }
}
