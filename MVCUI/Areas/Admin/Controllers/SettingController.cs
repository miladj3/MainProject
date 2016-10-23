using DataLayer.Context;
using MVCUI.Filters;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.ViewModel.Admin.Setting;

namespace MVCUI.Areas.Admin.Controllers
{
    [T4MVC]
    [SiteAuthorize(Roles ="admin")]
    [RouteArea("Admin")]
    [RoutePrefix("Setting")]
    [Route("{action}")]
    public partial class SettingController : Controller
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISettingService _setting;
        #endregion

        #region Constracture
        public SettingController(IUnitOfWork unitOfWork, ISettingService setting)
        {
            this._unitOfWork = unitOfWork;
            this._setting = setting;
        }
        #endregion

        #region Action Edit
        [HttpGet]
        [Route("Edit-Setting")]
        [OutputCache(CacheProfile ="long")]
        public virtual async Task<ActionResult> Edit()
        {
            EditSettingViewModel model = await _setting.GetOptionsForEdit();
            return View(MVC.Admin.Setting.Views.Edit, model);
        }

        [HttpPost]
        [OutputCache(CacheProfile ="nostore")]
        [ValidateAntiForgeryToken]
        [Route("Edit-Setting")]
        public virtual async Task<ActionResult> Edit(EditSettingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "لطفا تمام خانه های مورد نیاز را با دقت پر نمایید");
                return View(MVC.Admin.Setting.Views.Edit, model);
            }
            await _setting.Update(model);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(MVC.Admin.Home.ActionNames.Index, MVC.Admin.Home.Name);
        }
        #endregion
    }
}