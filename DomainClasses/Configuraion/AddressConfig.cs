using DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Configuraion
{
    public class AddressConfig: EntityTypeConfiguration<Address>
    {
        public AddressConfig()
        {
            this.HasRequired(x => x.User)
                .WithOptional(x => x.Address)
                .WillCascadeOnDelete(true);

            this.Property(x => x.RowVersion)
                .IsRowVersion();

            this.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(x => x.AddressLine1)
                .IsRequired()
                .IsMaxLength();

            this.Property(x => x.AddressLine2)
                .IsOptional()
                .IsMaxLength();

            this.Property(x => x.State)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(x => x.CompanyName)
                .IsOptional()
                .HasMaxLength(50);

            this.HasKey(x => x.Id);
        }
    }
}
