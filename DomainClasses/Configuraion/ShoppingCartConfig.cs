using System.Data.Entity.ModelConfiguration;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
    public class ShoppingCartConfig:EntityTypeConfiguration<ShoppingCart>
    {
        public ShoppingCartConfig()
        {
            this.Property(x => x.CartNumber).HasMaxLength(50);
        }
    }
}
