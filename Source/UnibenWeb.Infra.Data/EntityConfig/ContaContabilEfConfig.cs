using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnibenWeb.Domain.Entities;

namespace UnibenWeb.Infra.Data.EntityConfig
{
    class ContaContabilEfConfig : EntityTypeConfiguration<ContaContabil>
    {
        public ContaContabilEfConfig()
        {
            ToTable("ContaContabeis");
            HasKey(p => p.ContaContabilId);

            Ignore(c => c.ResultadoValidacao);

        }
    }
}
