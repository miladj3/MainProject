using System.Data.Entity.ModelConfiguration;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
    public class RoleConfig:EntityTypeConfiguration<Role>
    {
        public RoleConfig()
        {
            this.Property(x => x.Name)
                .HasMaxLength(20);

            this.Property(x => x.Description)
                .HasMaxLength(400);
        }
    }
}
