using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.User;

namespace ViewModel.ViewModel.Admin.User
{
    public class UsersListViewModel
    {
        public IEnumerable<AddUserViewModel> UsersList { get; set; }

        public Int32 PageCount { get; set; }

        public DomainClasses.Enums.Order Order { get; set; }

        public String Term { get; set; }

        public UserOrderBy UserOrderBy { get; set; }

        public Int32 PageNumber { get; set; }

        public Int32 TotalUsers { get; set; }
    }
}
