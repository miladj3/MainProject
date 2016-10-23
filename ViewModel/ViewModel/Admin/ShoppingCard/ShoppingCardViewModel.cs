using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.ShoppingCard
{
    public class ShoppingCardViewModel
    {
        public Int64 Id { get; set; }

        [DisplayName("تعداد سفارش")]
        public Int64 Quantity { get; set; }

        public  Int64 ProductId { get; set; }

        [DisplayName("تاریخ سفارش")]
        public  DateTime CreateDate { get; set; }

        [DisplayName("نام کالا")]
        public String NameProduct { get; set; }

        public String ImageProduct { get; set; }

        [DisplayName("قیمت کالا")]
        public Int64 PriceProduct{ get; set; }

        [DisplayName("قیمت با تخفیف")]
        public Int64 PriceProductAfterDiscount { get; set; }

        private Int64 _PriceTotalProduct;
        [DisplayName("قیمت کل")]
        public Int64 PriceTotalProduct
        {
            get
            {
                return _PriceTotalProduct;
            }
            set
            {
                Int64 _v = this.PriceProductAfterDiscount == 0 ? this.PriceProduct : this.PriceProductAfterDiscount;
                _PriceTotalProduct = _v * ((Int64)Quantity == 0 ? 1 : (Int64)Quantity);
            }
        }

        public Boolean isComplete { get; set; }

        [DisplayName("ارسال رایگان")]
        public Boolean IsFreeShipping { get; set; }
    }
}
