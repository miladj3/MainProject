using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.Product
{
    public class ProductSectionViewModel
    {
        public Int64 Id { get; set; }

        public Int64 categoryId { get; set; }

        public String Name { get; set; }

        public String PrincipleImagePath { get; set; }

        public Boolean IsFreeShipping { get; set; }

        public Boolean IsInStock { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#}", ApplyFormatInEditMode = true)]
        public Decimal SellCount { get; set; }

        public Int32 ViewCount { get; set; }

        [DisplayFormat(DataFormatString = "{0:###,###.####}", ApplyFormatInEditMode = true)]
        public Int64 Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:###,###.####}", ApplyFormatInEditMode = true)]
        public Int64 PriceAfterDiscount { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#}", ApplyFormatInEditMode = true)]
        public Decimal Ratio { get; set; }

        public Int32 TotalRaters { get; set; }

        public Double? AvrageRate { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#}", ApplyFormatInEditMode = true)]
        public Decimal TotalDiscount { get; set; }
        
        public Boolean ComingSoon { get; set; }
    }
}
