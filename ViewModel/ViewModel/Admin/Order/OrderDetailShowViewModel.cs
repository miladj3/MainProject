using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.Order
{
    public class OrderDetailShowViewModel
    {
        public Int64 Id { get; set; }
        public Int64 ProductId { get; set; }
        public Int64 OrderId { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal UnitPrice { get; set; }
    }
}
