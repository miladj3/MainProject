using System.Data.Entity.ModelConfiguration;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
    public class SiteOptionConfig: EntityTypeConfiguration<SiteOption>
    {
        public SiteOptionConfig()
        {
            this.Property(x => x.Name).HasMaxLength(50);
            this.Property(x => x.Value).IsMaxLength();
        }
    }
}
