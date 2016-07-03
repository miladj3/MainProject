using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.User
{
    public class DetailsUserViewModel
    {
        [DisplayName("نام")]
        public String FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        public String LastName { get; set; }

        [DisplayName("نقش کاربری")]
        public String RoleName { get; set; }

        [DisplayName("نام کاربری")]
        public String UserName { get; set; }

        [DisplayName("شماره تماس")]
        public String PhoneNumber { get; set; }

        [DisplayName("تعداد نظرات")]
        public Int32 CommentsCount { get; set; }

        [DisplayName("تعداد خرید ")]
        public Int32 OrdersCount { get; set; }

        [DisplayName("آدرس آی پی")]
        public String IP { get; set; }

        [DisplayName("وضعیت عضویت")]
        public String RegisterType { get; set; }
    }
}
