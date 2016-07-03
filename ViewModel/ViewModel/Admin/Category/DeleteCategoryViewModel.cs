using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.Category
{
    public class DeleteCategoryViewModel
    {
        public Int64 Id { get; set; }
        public String Name { get; set; }
        public Int64 ReplaceCategoryIdForProducts { get; set; }

    }
}
