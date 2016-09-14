using UnibenWeb.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace UnibenWeb.Infra.Data.EntityConfig
{
    public class GrauParentescoEFConfig : EntityTypeConfiguration<GrauParentesco>
    {
        public GrauParentescoEFConfig()
        {
            ToTable("GrausParentesco");
            HasKey(p => p.GrauParentescoId);
        }
    }
}
