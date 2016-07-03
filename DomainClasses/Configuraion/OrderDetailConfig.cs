using System.Data.Entity.ModelConfiguration;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
    public class OrderDetailConfig:EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailConfig()
        {
            this.HasRequired(x => x.Order)
                    .WithMany(x => x.OrderDetails)
                    .HasForeignKey(x => x.OrderId)
                    .WillCascadeOnDelete(true);

            this.Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
