using System.Data.Entity.ModelConfiguration;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
    public class OrderConfig:EntityTypeConfiguration<Order>
    {
        public OrderConfig()
        {
            this.Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
