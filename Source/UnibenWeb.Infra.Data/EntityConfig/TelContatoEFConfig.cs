using System.Data.Entity.ModelConfiguration;
using UnibenWeb.Domain.Entities;

namespace UnibenWeb.Infra.Data.EntityConfig
{
    class TelContatoEfConfig : EntityTypeConfiguration<TelContato>
    {
        public TelContatoEfConfig()
        {
            ToTable("TelContatos");
            HasKey(p => p.TelContatoId);
        }
    }
}
