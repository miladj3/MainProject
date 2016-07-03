using System;
using System.Data.Entity.ModelConfiguration;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
    public class FolderConfig:EntityTypeConfiguration<Folder>
    {
        public FolderConfig()
        {
            this.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            this.HasMany(x => x.Pictures)
                .WithRequired(x => x.Folder)
                .WillCascadeOnDelete(true);
        }
    }
}
