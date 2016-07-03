using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.User
{
    public class LoginViewModel
    {
        [DisplayName("شماره همراه")]
        [Required(ErrorMessage = "لطفا  شماره همراه خود را وارد کنید")]
        [RegularExpression("09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}", ErrorMessage = "لطفا شماره همراه خود را به شکل صحیح وارد کنید")]
        public String PhoneNumber { get; set; }

        [DisplayName("کد ورود")]
        [Required(ErrorMessage = "لطفا کد ورود خود را وارد کنید")]
        public String Password { get; set; }

        [DisplayName("مرا به خاطر بسپار")]
        public Boolean RememberMe { get; set; }
    }
}
