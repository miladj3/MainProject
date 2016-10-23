using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModel.ViewModel.Admin.Page
{
    public class PageViewModel
    {
        public Int64 Id { get; set; }

        [DisplayName("محتوای صفحه")]
        [AllowHtml]
        public String Content { get; set; }

        [DisplayName("مکان نمایش")]
        public String ShowPlace { get; set; }

        [DisplayName("عنوان صفحه")]
        public String Title { get; set; }

        [DisplayName("تصویر لینک")]
        public String LinkImage { get; set; }

        public String Description { get; set; }

        public String KeyWords { get; set; }

        [DisplayName("تاریخ ایجاد")]
        public DateTime DateCreated { get; set; }
    }
}
