using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClasses.Entities
{
    public class ShoppingCart
    {
        [Key]
        public virtual Int64 Id { get; set; }
        public virtual Decimal Quantity { get; set; }
        public virtual Decimal TotalQuantity { get; set; }
        public virtual Product Product { get; set; }
        public virtual Boolean isComplete { get; set; }

        [Index("IX_Cart",IsUnique =true, Order =1)]
        public virtual Int64 ProductId { get; set; }

        [Index("IX_Cart", IsUnique =true, Order =2)]
        public virtual String CartNumber { get; set; }
        public virtual DateTime CreateDate { get; set; }

        
    }
}