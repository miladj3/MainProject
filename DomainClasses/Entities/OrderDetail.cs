using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Entities
{
    public class OrderDetail
    {
        public virtual Int64 Id { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
        public virtual Int64 OrderId { get; set; }
        public virtual Decimal Quantity { get; set; }
        public virtual Byte[] RowVersion { get; set; }
        public virtual Decimal UnitPrice { get; set; }
    }
}
