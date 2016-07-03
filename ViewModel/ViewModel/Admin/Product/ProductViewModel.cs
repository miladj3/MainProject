using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.ViewModel.Admin.Product
{
    public class ProductViewModel
    {
        public Int64 Id { get; set; }

        public Boolean CompletedAttributes { get; set; }

        public Boolean AddedImages { get; set; }

        public String Name { get; set; }

        public String PrincipleImagePath { get; set; }

        public Boolean IsFreeShipping { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#}", ApplyFormatInEditMode = true)]
        public Decimal Stock { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#}", ApplyFormatInEditMode = true)]
        public Decimal ReserveCount { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#}", ApplyFormatInEditMode = true)]
        public Decimal SellCount { get; set; }

        public Int32 ViewCount { get; set; }

        public Boolean Deleted { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#}", ApplyFormatInEditMode = true)]
        public Decimal NotificationStockMinimum { get; set; }

        [DisplayFormat(DataFormatString = "{0:###,###.####}", ApplyFormatInEditMode = true)]
        public Int64 Price { get; set; }

        public Boolean Notification { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#}", ApplyFormatInEditMode = true)]
        public Decimal Ratio { get; set; }

        public Double? Rate { get; set; }

        public Boolean ApplyCategoryDiscount { get; set; }

        public String CategoryName { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#}", ApplyFormatInEditMode = true)]
        public Decimal DiscountPercent { get; set; }
    }
}
