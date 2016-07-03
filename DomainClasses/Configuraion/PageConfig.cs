
using DomainClasses.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DomainClasses.Configuraion
{
    public class PageConfig : EntityTypeConfiguration<Page>
    {
        public PageConfig()
        {
            this.Property(x => x.Content)
                .IsMaxLength();

            this.Property(x => x.Description)
                .HasMaxLength(400);

            this.Property(x => x.KeyWords)
                .HasMaxLength(100);

            this.Property(x => x.Title)
                .HasMaxLength(100);

            this.Property(x => x.ImagePath)
                .HasMaxLength(400);

            this.Property(x => x.RowVersion)
                .IsRowVersion();
        }
    }
}
