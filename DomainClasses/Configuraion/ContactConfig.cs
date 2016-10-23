using DomainClasses.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DomainClasses.Configuraion
{
    public class ContactConfig :EntityTypeConfiguration<Contact>
    {
        public ContactConfig()
        {
            this.Property(x => x.Content)
                .IsMaxLength()
                .IsRequired();

            this.Property(x => x.Title)
                .HasMaxLength(100)
                .IsRequired();
            
            this.Property(x => x.RowVersion)
                .IsRowVersion();
        }
    }
}
