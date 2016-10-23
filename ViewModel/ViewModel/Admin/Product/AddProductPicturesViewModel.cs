using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.Product
{
    public class AddProductPicturesViewModel
    {
        public Int64 ProductId { get; set; }

        [Display(Name ="تصویر اول")]
        public String Image1 { get; set; }

        [Display(Name = "تصویر دوم")]
        public String Image2 { get; set; }

        [Display(Name = "تصویر سوم")]
        public String Image3 { get; set; }

        //[Display(Name = "تصویر چهارم")]
        //public String Image4 { get; set; }

        //[Display(Name = "تصویر پنجم")]
        //public String Image5 { get; set; }
    }
}
