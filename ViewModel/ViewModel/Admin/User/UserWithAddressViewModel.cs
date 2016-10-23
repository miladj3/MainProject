using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.User
{
    public class UserWithAddressViewModel
    {
        public virtual RegisterViewModel Register { get; set; }
        public virtual AddressUserViewModel Address { get; set; }
    }
}
