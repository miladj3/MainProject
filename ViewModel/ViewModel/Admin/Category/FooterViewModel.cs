using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.Page;
using ViewModel.ViewModel.Admin.Setting;

namespace ViewModel.ViewModel.Admin.Category
{
    public class FooterViewModel
    {
        public IEnumerable<PageViewModel> Pages { get; set; }
        public EditSettingViewModel EditSettingViewModel { get; set; }
    }
}
