using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.ContactUs
{
    public class ContactUsViewModel
    {
        public Int64 Id { get; set; }

        [DisplayName("موضوع")]
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings =true, ErrorMessage ="لطفا موضوع مورد نظر خود را وارد نمایید")]
        public String Title { get; set; }

        [DisplayName("نام و نام خانوادگی")]
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = true, ErrorMessage = "لطفانام و نام خانوادگی خود را وارد نمایید")]
        public String Name { get; set; }

        [DisplayName("ایمیل یا شماره تماس")]
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = true, ErrorMessage = "لطفاایمیل یا شماره تماس خود را وارد نمایید")]
        public String EmailOrPhone { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("توضیحات")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="لطفا توضیحات خود را وارد نمایید")]
        public String Content { get; set; }
        
        [DataType(DataType.DateTime)]
        [DisplayName("زمان فرستادن پیفام")]
        public DateTime Date { get; set; }

        [DisplayName("وضعیت خواندن")]
        public Boolean IsSeen { get; set; }

        [DisplayName("موضوع مورد نظر")]
        [Required(ErrorMessage ="لطفا موضوع مورد نظر خودراانتخاب نمایید")]
        public ContactType Type { get; set; }
    }
}
