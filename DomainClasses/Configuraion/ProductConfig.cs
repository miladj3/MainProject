using System.Data.Entity.ModelConfiguration;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
    public class ProductConfig:EntityTypeConfiguration<Product>
    {
        public ProductConfig()
        {
            this.HasMany(x => x.Values)
                .WithRequired(x => x.Product)
                .WillCascadeOnDelete(true);

            this.HasMany(x => x.Images)
                .WithRequired(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(true);

            this.HasMany(x=>x.ShoppingCarts)
                 .WithRequired(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(true);

            this.HasMany(x => x.OrderDetails)
                .WithRequired(x => x.Product)
                .WillCascadeOnDelete(true);

            this.HasMany(x => x.LikedUser)
                .WithMany(x => x.LikedProducts)
                .Map(x =>
            {
                x.ToTable("LikeUsersProducts");
                x.MapLeftKey("ProductId");
                x.MapRightKey("UserId");
            });

            this.HasMany(x => x.UsersFavorite)
                .WithMany(x => x.ProductsFavorite)
                .Map(x =>
                {
                    x.ToTable("Favorite");
                    x.MapLeftKey("ProductId");
                    x.MapRightKey("UserId");
                });

            this.Property(x => x.Description).IsMaxLength().IsRequired();
            this.Property(x => x.MetaDescription).HasMaxLength(400).IsRequired();
            this.Property(x => x.MetaKeywords).HasMaxLength(100).IsRequired();

            this.Property(x => x.Name).HasMaxLength(100).IsRequired();
            this.Property(x => x.Ratio).IsRequired();
            this.Property(x => x.Price).IsRequired();
            this.Property(x => x.NotificationStockMinimum).IsRequired();
            this.Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
