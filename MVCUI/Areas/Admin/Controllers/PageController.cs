using DataLayer.Context;
using MVCUI.Extentions;
using MVCUI.Filters;
using MVCUI.HtmlCleaner;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.ViewModel.Admin.Page;

namespace MVCUI.Areas.Admin.Controllers
{
    [T4MVC]
    [RouteArea("Admin")]
    [RoutePrefix("Blog")]
    [Route("{action}")]
    [SiteAuthorize(Roles = "admin")]
    public partial class PageController : Controller
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPageService _page;
        #endregion

        #region Constracture
        public PageController(IUnitOfWork unitOfWork, IPageService page)
        {
            this._unitOfWork = unitOfWork;
            this._page = page;
        }
        #endregion

        #region Index & List
        [HttpGet]
        [Route("List-Page")]
        [OutputCache(CacheProfile = "long")]
        public virtual ActionResult Index() => View();

        [HttpGet]
        [OutputCache(Duration = 1, VaryByParam = "*")]
        public virtual async Task<ActionResult> List(String term = "")
        {
            ViewBag.Term = term;
            IEnumerable<PageViewModel> model =await _page.GetDataTable(term);
            return PartialView(MVC.Admin.Page.Views._list, model);
        }
        #endregion

        #region create
        [HttpGet]
        [Route("Create-Page")]
        [OutputCache(CacheProfile = "long")]
        public virtual ActionResult AddPage() => View();

        [HttpPost]
        [OutputCache(CacheProfile = "nostore")]
        [ValidateAntiForgeryToken]
        [Route("Create-Page")]
        [ValidateInput(false)]
        public virtual async Task<ActionResult> AddPage(AddPageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "لطفا همه خانه های مورد نیاز را پر نمایید");
                return View(MVC.Admin.Page.Views.AddPage, model);
            }
            model.Content = model.Content.ToSafeHtml();
            Regex regex = new Regex(@"(<.+?)\s+style\s*=\s*([""']).*?\2(.*?>)");
            model.Content = regex.Replace(model.Content, String.Empty);
            Boolean result = await _page.Add(model);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
                CacheManager.InvalidateChildActionsCache();
                return RedirectToAction(MVC.Admin.Page.ActionNames.Index, MVC.Admin.Page.Name);
            }
            else
            {
                ModelState.AddModelError("Title", "چنین صفحه ای قبلا ثبت شده است");
                return View(MVC.Admin.Page.Views.AddPage, model);
            }
        }
        #endregion

        #region Edit
        [HttpGet]
        [Route("{pageId}/Edit-Page")]
        [OutputCache(CacheProfile = "long")]
        public virtual async Task<ActionResult> Edit(Int64? pageId)
        {
            if (pageId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            EditPageViewModel model = await _page.GetForEditById(pageId.Value);
            if (model == null)
                return HttpNotFound();

            return View(MVC.Admin.Page.Views.Edit, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Route("{pageId}/Edit-Page")]
        [OutputCache(CacheProfile ="nostore")]
        public virtual async Task<ActionResult> Edit(EditPageViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "لطفا تمام خانه های مورد نیاز را پر نمایید");
                return View(MVC.Admin.Page.Views.Edit, model);
            }
            model.Content = model.Content.ToSafeHtml();
            Regex regex = new Regex(@"(<.+?)\s+style\s*=\s*([""']).*?\2(.*?>)");
            model.Content = regex.Replace(model.Content, String.Empty);
            Boolean result = await _page.Edit(model);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
                CacheManager.InvalidateChildActionsCache();
                return RedirectToAction(MVC.Admin.Page.ActionNames.Index, MVC.Admin.Page.Name);
            }
            else
            {
                ModelState.AddModelError("Title", "چنین صفحه ای قبلا ثبت شده است");
                return View(MVC.Admin.Page.Views.Edit, model);
            }
        }
        #endregion

        #region Delete
        [HttpPost]
        [Route("Delete-Page")]
        [OutputCache(CacheProfile ="nostore")]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public virtual async Task<JsonResult> Delete(Int64? id)
        {
            if (id == null)
                return Json(false);
            try
            {
                await _page.Delete(id.Value);
                await _unitOfWork.SaveChangesAsync();
                CacheManager.InvalidateChildActionsCache();
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }
        #endregion
    }
}