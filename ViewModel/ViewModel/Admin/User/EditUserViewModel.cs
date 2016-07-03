using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModel.ViewModel.Admin.User
{
    public class EditUserViewModel
    {
        public Int64 Id { get; set; }

        [DisplayName("نام نمایشی")]
        [Required(ErrorMessage = "لطفا نام نمایشی  را وارد کنید")]
        [MinLength(5, ErrorMessage = "نام نمایشی  باید بیشتر از 5 حرف باشد")]
        [MaxLength(30, ErrorMessage = "نام نمایشی نمیتواند بیشتر از 30 کاراکتر باشد")]
        [Remote("EditCheckUserNameIsExist", "User", "Admin", ErrorMessage = "این نام کاربری قبلا توسط اعضا انتخاب شده است", HttpMethod = "POST", AdditionalFields = "Id")]
        public String UserName { get; set; }

        [DisplayName("شماره همراه")]
        [Required(ErrorMessage = "لطفا برای تکمیل ثبت نام شماره همراه  را وارد کنید")]
        [Remote("EditCheckPhoneNumberIsExist", "User", "Admin", ErrorMessage = "این شماره همراه قبلا ثبت شده است", HttpMethod = "POST", AdditionalFields = "Id")]
        [RegularExpression("09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}", ErrorMessage = "لطفا شماره همراه  را به شکل صحیح وارد کنید")]
        public String PhoneNumber { get; set; }

        [DisplayName("کلمه عبور")]
        [MaxLength(128, ErrorMessage = "کلمه عبور باید کمتر از 128 حرف باشد")]
        [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 6 حرف باشد")]
        public String Password { get; set; }

        [DisplayName("تکرار کلمه عبور")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "کلمه عبور و تکرارش باید یکسان باشند")]
        public String ConfirmPassword { get; set; }

        [DisplayName("نام")]
        [MaxLength(30, ErrorMessage = "تعداد حروف مشخصه نام غیر مجاز است")]
        public String FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        [MaxLength(40, ErrorMessage = "تعداد حروف مشخصه نام خانوادگی غیر مجاز است")]
        public String LastName { get; set; }

        [DisplayName("نقش")]
        public Int64 RoleId { get; set; }

        [DisplayName("غیر فعال کردن")]
        public Boolean IsBaned { get; set; }
    }
}
