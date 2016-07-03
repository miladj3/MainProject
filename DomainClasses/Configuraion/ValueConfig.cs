

using DomainClasses.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DomainClasses.Configuraion
{
    public class ValueConfig : EntityTypeConfiguration<Value>
    {
        public ValueConfig()
        {
            this.Property(a => a.Content)
                .IsRequired()
                .HasMaxLength(400);
        }
    }
}
