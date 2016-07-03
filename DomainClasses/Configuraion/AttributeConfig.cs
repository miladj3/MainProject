using System;
using System.Data.Entity.ModelConfiguration;

namespace DomainClasses.Configuraion
{
    public class AttributeConfig: EntityTypeConfiguration<Entities.Attribute>
    {
        public AttributeConfig()
        {
            this.HasRequired(x => x.Category)
                .WithMany(x => x.Attributes)
                .HasForeignKey(x => x.CategoryId)
                .WillCascadeOnDelete(true);

            this.HasMany(x => x.Values)
                .WithRequired(x => x.Attribute)
                .WillCascadeOnDelete(true);

            this.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();
            this.Property(x => x.RowVersion)
                .IsRowVersion();
        }
    }
}
