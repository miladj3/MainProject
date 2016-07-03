using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.User
{
    public class UserViewModel
    {
        public Int64 Id { get; set; }

        public String UserName { get; set; }

        public String FullName { get; set; }

        public String PhoneNumber { get; set; }

        public Int32 CommentCount { get; set; }

        public Int32 OrderCount { get; set; }

        public String RoleDescritpion { get; set; }

        public Boolean Baned { get; set; }

        public String RegisterType { get; set; }
    }
}
