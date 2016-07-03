using System.Data.Entity.ModelConfiguration;
using DomainClasses.Entities;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClasses.Configuraion
{
    public class UserConfig:EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            this.HasMany(x => x.Orders)
                .WithRequired(x => x.Buyer)
                .WillCascadeOnDelete(true);
            this.Property(x => x.UserName)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UserName") { IsUnique = true }));

            this.Property(x => x.Password)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(x => x.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_PhoneNumber") { IsUnique = true }));

            this.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsOptional();

            this.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsOptional();

            this.Property(x => x.AvatarPath)
                .HasMaxLength(200);

            this.Property(x => x.Email)
                .HasMaxLength(100)
                .IsOptional();

            this.Property(x => x.IP)
                .HasMaxLength(20)
                .IsOptional();

            this.Property(x => x.RowVersion)
                .IsRowVersion();
        }
    }
}
