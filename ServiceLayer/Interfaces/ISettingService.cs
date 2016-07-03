using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModel.Admin.Setting;

namespace ServiceLayer.Interfaces
{
    public interface ISettingService
    {
        void Update(EditSettingViewModel viewModel);
        EditSettingViewModel GetOptionsForEdit();
        EditSettingViewModel GetOptionsForShowOnFooter();
        Boolean CommentMangement();
    }
}
