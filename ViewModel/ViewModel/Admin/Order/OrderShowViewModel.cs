using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.ViewModel.Admin.Order
{
    public class OrderShowViewModel
    {
        public Int64 Id { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("آدرس گیرنده")]
        public String Address { get; set; }
        public String UserName { get; set; }
        public ICollection<OrderDetailShowViewModel> OrderDetail { get; set; }
        public DateTime BuyDate { get; set; }
        public DateTime? PostDate { get; set; }
        public OrderStatus Status { get; set; }
        public String TransactionCode { get; set; }
        public PeymentType PeymentType { get; set; }
        public Decimal? DiscountPrice { get; set; }
        public Decimal TotalPrice { get; set; }
    }
}
