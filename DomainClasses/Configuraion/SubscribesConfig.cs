using DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Configuraion
{
    public class SubscribesConfig:EntityTypeConfiguration<Subscribes>
    {
        public SubscribesConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();
            this.Property(x => x.Email)
                .IsRequired();
        }
    }
}
