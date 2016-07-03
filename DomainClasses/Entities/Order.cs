using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Entities
{
    public class Order
    {
        public virtual Int64 Id { get; set; }
        public virtual String Address { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual User Buyer { get; set; }
        public virtual DateTime BuyDate { get; set; }
        public virtual DateTime? PostDate { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual String TransactionCode { get; set; }
        public virtual PeymentType PeymentType { get; set; }
        public virtual Decimal? DiscountPrice { get; set; }
        public virtual Byte[] RowVersion { get; set; }
        public virtual Decimal TotalPrice { get; set; }
    }
}
