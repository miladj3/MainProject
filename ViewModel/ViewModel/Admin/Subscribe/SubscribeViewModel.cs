using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.Subscribe
{
    public class SubscribeViewModel
    {
        public Int64 Id { get; set; }

        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا نام خود را وارد نمایید")]
        [MaxLength(50, ErrorMessage = "نام وارد شده بیشتر از حد مجاز میباشد. حد مجاز 50 کاراکتر میباشد")]
        public String NameSubscribe { get; set; }
        
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا ایمیل خود را وارد نمایید")]
        public String EmailSubscribe { get; set; }
    }
}
