using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.Attribute
{
    public class DeleteAttributeViewModel
    {
        public Int64 Id { get; set; }
        [DisplayName("نام خصوصیت")]
        public String Name { get; set; }

        public Int64 CategoryId { get; set; }

        [DisplayName("حذف ویژگی از گروه های فرزند")]
        public Boolean CascadeDeleteForChildrenCategory { get; set; }
    }
}
