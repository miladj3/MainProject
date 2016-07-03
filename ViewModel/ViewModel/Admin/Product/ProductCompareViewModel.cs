using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.Product
{
    public class ProductCompareViewModel
    {
        public Int64 ProductId { get; set; }

        public String ProductName { get; set; }

        public Int32 TotalRaters { get; set; }

        public Double? AvrageRate { get; set; }

        public String Description { get; set; }

        public String[] Attributes { get; set; }

        public Int64 Price { get; set; }

        public Int32 Discount { get; set; }

        public String ImagePath { get; set; }

        public Boolean FreeSend { get; set; }
    }
}
