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
    public class RegisterViewModel
    {
        [DisplayName("نام ")]
        [MinLength(5, ErrorMessage = "نام  شما باید بیشتر از 5 حرف باشد")]
        [Required(ErrorMessage = "لطفا نام  خود را وارد کنید")]
        [MaxLength(30, ErrorMessage = "نام  نمیتواند بیشتر از 30 کاراکتر باشد")]
        public String FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        [MinLength(5, ErrorMessage = "نام خانوادگی شما باید بیشتر از 5 حرف باشد")]
        [Required(ErrorMessage = "لطفا نام خانوادگی خود را وارد کنید")]
        [MaxLength(50, ErrorMessage = "نام خانوادگی نمیتواند بیشتر از 50 کاراکتر باشد")]
        public String LastName { get; set; }

        [DisplayName("نام کاربری")]
        [MinLength(5, ErrorMessage = "نام نمایشی شما باید بیشتر از 5 حرف باشد")]
        [Required(ErrorMessage = "لطفا نام نمایشی خود را وارد کنید")]
        [MaxLength(30, ErrorMessage = "نام نمایشی نمیتواند بیشتر از 30 کاراکتر باشد")]
        [Remote("CheckUserNameIsExist", "customer", "", ErrorMessage = "این نام کاربری قبلا توسط اعضا انتخاب شده است", HttpMethod = "POST")]
        public String UserName { get; set; }

        [DisplayName("شماره همراه")]
        [Required(ErrorMessage = "لطفا برای تکمیل ثبت نام شماره همراه خود را وارد کنید")]
        [Remote("CheckPhoneNumberIsExist", "customer", "", AdditionalFields = "OldPhoneNumber", ErrorMessage = "این شماره همراه قبلا ثبت شده است", HttpMethod = "GET")]
        [RegularExpression("09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}", ErrorMessage = "لطفا شماره همراه خود را به شکل صحیح وارد کنید")]
        public String PhoneNumber { get; set; }

        [HiddenInput]
        public String OldPhoneNumber { get; set; }

        [DisplayName("رمز عبور")]
        [Required(ErrorMessage ="لطفا رمز عبور خود را وارد نمایید")]
        [MinLength(6, ErrorMessage ="رمز عبور شما باید بیشتر از 6 حرف باشد")]
        [MaxLength(20, ErrorMessage ="رمز عبور شما باید کمتر از 20 حرف باشد")]
        public String Password { get; set; }

        [Required(ErrorMessage ="لطفا رمز عبور خود را دوباره وارد نمایید")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage ="تکرار رمز عبور وارد شده با رمز عبور تفاوت دارد")]
        [DisplayName("تکرار رمز عبور")]
        public String VerifyPassword { get; set; }
    }
}
