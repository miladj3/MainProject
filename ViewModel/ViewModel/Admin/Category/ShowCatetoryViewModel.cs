using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.Category
{
    public class ShowCatetoryViewModel
    {
        public virtual Int64 Id { get; set; }

        [DisplayName("نام گروه")]
        public virtual String Name { get; set; }

        [DisplayName("درصد تخفیف")]
        public virtual Int32 DiscountPercent { get; set; }
    }
}
