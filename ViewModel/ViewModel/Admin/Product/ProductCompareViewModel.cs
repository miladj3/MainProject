using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.Product
{
    public class ProductCompareViewModel
    {
        public Int64 ProductId { get; set; }
        [DisplayName("نام کالا")]

        public String ProductName { get; set; }

        [DisplayName("تعداد امتیاز")]
        public Int32 TotalRaters { get; set; }

        [DisplayName("امتیاز")]

        public Double? AvrageRate { get; set; }

        [DisplayName("توضیحات")]
        public String Description { get; set; }

        public String[] Attributes { get; set; }

        [DisplayName("قیمت")]
        public Decimal Price { get; set; }

        [DisplayName("تخفیف")]
        public Decimal Discount { get; set; }

        [DisplayName("قیمت با تخفیف")]
        public Decimal PriceAfterDiscount { get; set; }

        public String ImagePath { get; set; }

        [DisplayName("ارسال رایگان")]
        public Boolean FreeSend { get; set; }
    }
}
