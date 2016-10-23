using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ViewModel.ViewModel.Admin.User
{
    public class AddressUserViewModel
    {
        [DataType(DataType.Text)]
        [DisplayName("نام شرکت")]
        [MaxLength(50, ErrorMessage ="نام شرکت بیشتر از حد مجاز میباشد. حد مجاز 50 کاراکتر میباشد")]
        public String CompanyName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("استان")]
        [MaxLength(50, ErrorMessage = " حد مجاز 50 کاراکتر میباشد")]
        [Required(AllowEmptyStrings = false, ErrorMessage ="لطفا نام استان محل زندگی خود را وارد نمایید")]
        public String State { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("شهر")]
        [MaxLength(50, ErrorMessage = " حد مجاز 50 کاراکتر میباشد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا نام شهر محل زندگی خود را وارد نمایید")]
        public String City { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("آدرس اول")]
        [MaxLength(150, ErrorMessage = " حد مجاز 150 کاراکتر میباشد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا آدرس  دقیق محل زندگی خود را وارد نمایید")]
        public String AddressLine1 { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("آدرس دوم")]
        [MaxLength(150, ErrorMessage = " حد مجاز 150 کاراکتر میباشد")]
        public String AddressLine2 { get; set; }
    }
}
