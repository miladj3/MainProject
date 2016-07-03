using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
    public class CategoryConfig:EntityTypeConfiguration<Category>
    {
        public CategoryConfig()
        {
            this.HasMany(x => x.Products)
                .WithRequired(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .WillCascadeOnDelete(false);

            this.HasMany(x => x.Children)
                .WithOptional(x => x.Parent)
                .HasForeignKey(x => x.ParentId);

            this.Property(x => x.Description)
                .HasMaxLength(400)
                .IsRequired();

            this.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(x => x.KeyWords)
                .HasMaxLength(100)
                .IsRequired();

            this.Property(x => x.RowVersion)
                .IsRowVersion();

        }
    }
}
