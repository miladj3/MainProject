using DataLayer.Context;
using DomainClasses.Entities;
using MVCUI.Extentions;
using MVCUI.Filters;
using MVCUI.Helpers;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.ViewModel.Admin.SlideShow;

namespace MVCUI.Areas.Admin.Controllers
{
    [T4MVC]
    [RouteArea("Admin")]
    [RoutePrefix("Slides")]
    [Route("{action}")]
    [SiteAuthorize(Roles = "admin")]
    public partial class SlideShowController : Controller
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISlideShowService _slider;
        #endregion

        #region Constracture
        public SlideShowController(IUnitOfWork unitOfWork, ISlideShowService slideShow)
        {
            this._unitOfWork = unitOfWork;
            this._slider = slideShow;
        }
        #endregion

        #region List
        [HttpGet]
        [OutputCache(CacheProfile = "nostore")]
        [Route("Index")]
        public virtual async Task<ActionResult> Index()
        {
            IEnumerable<SlideShow> model = await _slider.List();
            return View(MVC.Admin.SlideShow.Views.Index, model);
        }
        #endregion

        #region Edit
        [HttpGet]
        [Route("{slideId}/Edit-SlideShow")]
        [OutputCache(CacheProfile = "long")]
        public virtual async Task<ActionResult> EditSlide(Int64? slideId)
        {
            if (slideId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            EditSlideShowViewModel model = await _slider.GetByIdForEdit(slideId.Value);
            if (model == null)
                return HttpNotFound();

            PopulateCategoriesDropDownList(model.ShowTransition, model.HideTransition, model.Position);
            return View(MVC.Admin.SlideShow.Views.EditSlide, model);
        }

        [HttpPost]
        [Route("{slideId}/Edit-SlideShow")]
        [OutputCache(CacheProfile = "nostore")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> EditSlide(EditSlideShowViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "لطفا تمام خانه های مورد نیاز را پر نمایید");
                PopulateCategoriesDropDownList(model.ShowTransition, model.HideTransition, model.Position);
                return View(MVC.Admin.SlideShow.Views.EditSlide, model);
            }
            _slider.Update(model);
            await _unitOfWork.SaveChangesAsync();
            CacheManager.InvalidateChildActionsCache();
            return RedirectToAction(MVC.Admin.SlideShow.ActionNames.Index, MVC.Admin.SlideShow.Name);
        }
        #endregion

        #region Create
        [HttpGet]
        [Route("Add-SlideShow")]
        [OutputCache(CacheProfile = "long")]
        public virtual async Task<ActionResult> AddSlide()
        {
            Boolean allow = await _slider.AllowAdd();
            if (!allow)
            {
                ViewBag.AllowAddSlide = "NOT";
                return View(MVC.Admin.SlideShow.Views.AddSlide);
            }
            PopulateCategoriesDropDownList(null, null, null);
            return View(MVC.Admin.SlideShow.Views.AddSlide);
        }

        [HttpPost]
        [Route("Add-SlideShow")]
        [ValidateAntiForgeryToken]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<ActionResult> AddSlide(AddSlideShowViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "لطفا تمام خانه های مورد نیاز را پر نمایید");
                PopulateCategoriesDropDownList(model.ShowTransition, model.HideTransition, model.Position);
                return View(MVC.Admin.SlideShow.Views.AddSlide, model);
            }
            _slider.Add(model);
            await _unitOfWork.SaveChangesAsync();
            CacheManager.InvalidateChildActionsCache();
            return RedirectToAction(MVC.Admin.SlideShow.ActionNames.AddSlide, MVC.Admin.SlideShow.Name);
        }
        #endregion

        #region Delete
        [AjaxOnly]
        [Route("Delete-SlideShow")]
        [ValidateAntiForgeryToken]
        [OutputCache(CacheProfile = "nostore")]
        [HttpPost]
        public virtual async Task<JsonResult> DeleteSlideShow(Int64? id)
        {
            Boolean result = false;
            if (id == null)
                return Json(result);

            try
            {
                await _slider.Delete(id.Value);
                await _unitOfWork.SaveChangesAsync();
                result = true;
            }
            catch
            {
                result = false;
            }
            CacheManager.InvalidateChildActionsCache();
            return Json(result);
        }
        #endregion

        #region NonAction
        [NonAction]
        private void PopulateCategoriesDropDownList(String showTranstion, String hideTranstion, String position)
        {
            ViewBag.ShowTransitionList = DropDown.GetShowTranstionSlide(showTranstion);
            ViewBag.HideTransitionList = DropDown.GetHideTranstionSlide(hideTranstion);
            ViewBag.PositionList = DropDown.GetPositonSlide(position);
        }
        #endregion
    }
}