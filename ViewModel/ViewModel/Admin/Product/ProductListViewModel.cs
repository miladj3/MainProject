using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.Product
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductViewModel> ProductList { get; set; }

        public Int32 PageCount { get; set; }

        public DomainClasses.Enums.Order Order { get; set; }

        public Boolean FreeSend { get; set; }

        public Boolean Deleted { get; set; }

        public String Term { get; set; }

        public ProductOrderBy ProductOrderBy { get; set; }

        public ProductType ProductType { get; set; }

        public Int64 CategoryId { get; set; }

        public Int32 PageNumber { get; set; }

        public Int32 TotalProducts { get; set; }
    }
}
