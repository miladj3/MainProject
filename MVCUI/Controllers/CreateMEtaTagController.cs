using MVCUI.Helpers;
using ServiceLayer.Interfaces;
using System;
using System.Text;
using System.Web.Mvc;
using ViewModel.ViewModel.Admin.Setting;

namespace MVCUI.Controllers
{
    public partial class CreateMetaTagController : Controller
    {
        #region Fields
        private readonly ISettingService _setting;
        #endregion

        #region Constracture
        public CreateMetaTagController(ISettingService _setting)
        {
            this._setting = _setting;
        }
        #endregion

        #region Controller Of Meta Tag Title & Description & KeyWords

        public virtual PartialViewResult MetaTag(String title, String description, String keyWords)
        {
            EditSettingViewModel set = _setting.GetOptionsForShowOnFooter();
            ViewBag.Title = !String.IsNullOrEmpty(title) ?
                SeoExtentions.GeneratePageTitle(set.StoreName, title) :
                SeoExtentions.GeneratePageTitle(set.StoreName);
            ViewBag.Description = !String.IsNullOrEmpty(description) ? description : set.StoreDescription;
            ViewBag.KeyWords = !String.IsNullOrEmpty(keyWords) ? keyWords : set.StoreKeyWords;
            return PartialView(MVC.Shared.Views.MetaTag);
        }
        #endregion
    }
}
