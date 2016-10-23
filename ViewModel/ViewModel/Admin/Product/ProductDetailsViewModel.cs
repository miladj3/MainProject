using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.ViewModel.Admin.Product
{
    public class ProductDetailsViewModel
    {
        [DisplayName("کد محصول")]
        public Int64 Id { get; set; }

        public String PrincipleImage { get; set; }

        [DisplayName("تعداد نظرات")]
        public Int32 CommentsCount { get; set; }

        [DisplayName("نام محصول")]
        public String Name { get; set; }

        [DisplayName("توضیحات")]
        public String Description { get; set; }

        [DisplayName("تعداد امتیازات")]
        public Int32 TotalRaters { get; set; }

        public String[] Images { get; set; }

        [DisplayName("ارسال رایگان کالا")]
        public Boolean IsFreeShipping { get; set; }

        public Int64 CategoryId { get; set; }

        [DisplayName("گروه دسته بندی کالا")]
        public String CategoryName { get; set; }

        [DisplayName("وضعیت کالا در انبار")]
        public Boolean IsInStock { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#}", ApplyFormatInEditMode = true)]
        [DisplayName("تعداد فروخته شده")]
        public Decimal SellCount { get; set; }

        [DisplayName("تعداد بازدید کالا")]
        public Int32 ViewCount { get; set; }

        [DisplayFormat(DataFormatString = "{0:###,###.####}", ApplyFormatInEditMode = true)]
        [DisplayName("قیمت کالا")]
        public Int64 Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:###,###.####}", ApplyFormatInEditMode = true)]
        [DisplayName("قیمت با احتساب تخفیف")]
        public Int64 PriceAfterDiscount { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#}", ApplyFormatInEditMode = true)]
        [DisplayName("ضریب خرید کالا")]
        public Decimal Ratio { get; set; }

        public Double? AvrageRate { get; set; }

        [DisplayName("درصد تخفیف")]
        [DisplayFormat(DataFormatString = "{0:0.#}", ApplyFormatInEditMode = true)]
        public Decimal TotalDiscount { get; set; }

        [DisplayName("وضعیت کالا")]
        public Boolean ComingSoon { get; set; }

        public String MetaDescription { get; set; }
        public String MetaKeywords { get; set; }
    }
}
