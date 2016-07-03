using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.ViewModel.Admin.Product
{
    public class ProductDetailsViewModel
    {
        public Int64 Id { get; set; }

        public String PrincipleImage { get; set; }

        public Int32 CommentsCount { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public Int32 TotalRaters { get; set; }

        public String[] Images { get; set; }

        public Boolean IsFreeShipping { get; set; }

        public Int64 CategoryId { get; set; }

        public Boolean IsInStock { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#}", ApplyFormatInEditMode = true)]
        public Decimal SellCount { get; set; }

        public Int32 ViewCount { get; set; }

        [DisplayFormat(DataFormatString = "{0:###,###.####}", ApplyFormatInEditMode = true)]
        public Int64 Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#}", ApplyFormatInEditMode = true)]
        public Decimal Ratio { get; set; }

        public Double? AvrageRate { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#}", ApplyFormatInEditMode = true)]
        public Decimal TotalDiscount { get; set; }


    }
}
