using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.Product
{
    public class SearchViewModel
    {
        public IEnumerable<ProductSectionViewModel> Products { get; set; }

        public Boolean IsInStok { get; set; }

        public Boolean SearchInDescription { get; set; }

        public Int32 Page { get; set; }

        public Int32 Count { get; set; }

        public Int32 Total { get; set; }

        public String Term { get; set; }

        public Int64 CategoryId { get; set; }

        public PSFilter Filter { get; set; }

        public Int64 MinPrice { get; set; }

        public Int64 MaxPrice { get; set; }
    }
}
