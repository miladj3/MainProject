using DataLayer.Context;
using DomainClasses.Entities;
using DomainClasses.Enums;
using MVCUI.Filters;
using MVCUI.Helpers;
using ServiceLayer.EFServices;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.ViewModel.Admin.Category;
using ViewModel.ViewModel.Admin.ContactUs;
using ViewModel.ViewModel.Admin.Setting;
using ViewModel.ViewModel.Admin.Subscribe;

namespace MVCUI.Controllers
{
    [T4MVC]
    public partial class _SharedItemController : Controller
    {

        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _product;
        private readonly ICategoryService _category;
        private readonly ISettingService _setting;
        private readonly IPageService _page;
        private readonly ISubscribeService _subscribe;
        private readonly IContactService _contact;
        private const int _1day = 86400;
        private const int _12hour = 43200;
        private const int _1hour = 3600;
        private const int _15min = 900;
        private const int _10min = 600;
        #endregion

        #region Constracture
        public _SharedItemController(IUnitOfWork unitOfWork,
                                                        ICategoryService category,
                                                        IProductService product,
                                                        ISettingService setting,
                                                        IPageService page,
                                                        ISubscribeService subscribe,
                                                        IContactService contact)
        {
            this._unitOfWork = unitOfWork;
            this._category = category;
            this._product = product;
            this._setting = setting;
            this._page = page;
            this._subscribe = subscribe;
            this._contact = contact;
        }
        #endregion

        #region Actions

        #region Navbar
        [ChildActionOnly]
        public virtual PartialViewResult NavBar() =>
            PartialView(MVC._SharedItem.Views._navbar);

        #endregion

        #region Menu
        [ChildActionOnly]
        public virtual async Task<PartialViewResult> Menu()
        {
            IEnumerable<Category> category = await _category.GetCategoriesForMenu();
            return PartialView(MVC._SharedItem.Views._menu, category);
        }
        #endregion

        #region Search
        [ChildActionOnly]
        public virtual PartialViewResult Search() =>
           PartialView(MVC._SharedItem.Views._search);
        #endregion

        #region Beloved Product

        [ChildActionOnly]
        public virtual PartialViewResult BelovedProducts() =>
            PartialView(MVC._SharedItem.Views._BelovedProducts, _product.GetBelovedProducts(8));
        #endregion

        #region Newest Product
        [ChildActionOnly]
        public virtual PartialViewResult NewestProduct() =>
           PartialView(MVC._SharedItem.Views._NewestProduct, _product.GetNewProducts(8));
        #endregion

        #region More Sell
        [ChildActionOnly]
        public virtual PartialViewResult MoreSellProduct() =>
            PartialView(MVC._SharedItem.Views._MoreSellProduct, _product.GetMoreSellProducts(8));
        #endregion

        #region More Visit Product
        [ChildActionOnly]
        public virtual PartialViewResult MoreVisitedProduct() =>
            PartialView(MVC._SharedItem.Views._MoreVisit, _product.GetMoreViewedProducts(12));
        #endregion

        #region Select Product 
        [ChildActionOnly]
        public virtual PartialViewResult ProductSelected() =>
       PartialView(MVC._SharedItem.Views.ProductSelected, _product.GetProductSelected(12));
        #endregion

        #endregion

        #region  In Footer

        #region About Us
        [ChildActionOnly]
        [OutputCache(Duration = _12hour)]
        public virtual PartialViewResult AboutUs() =>
            PartialView(MVC.Shared.Views._Footer_Partial, new FooterViewModel
            {
                EditSettingViewModel = _setting.GetOptionsForShowOnFooter(),
                Pages = _page.GetForShowFooter(2,DomainClasses.Enums.PageOrderBy.Date)
            });

        #endregion
        
        [OutputCache(CacheProfile ="long")]
        [HttpGet]
        [Route("~/{type}/Contact")]
        public virtual ActionResult ContactUs(ContactType type)
        {
            PopulateCategoriesDropDownList(type);
            return View(MVC._SharedItem.Views.ContactUs);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        [OutputCache(CacheProfile ="nostore")]
        [AjaxOnly]
        [Route("~/{type}/Contact")]
        public virtual async Task<ActionResult> ContactUs(ContactUsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "لطفا حانه های مورد نظر را با دقت پرنمایید");
                return View(MVC._SharedItem.Views.ContactUs, model);
            }
            _contact.Create(model);
            await _unitOfWork.SaveAllChangesAsync(false);
            return Content("پیام شما فرستاده شد.");
        }
        [NonAction]
        private void PopulateCategoriesDropDownList(ContactType type)=>
            ViewBag.type = DropDown.GetTypeContactUs(type);
        
        #endregion

        #region Meta Tags
        [ChildActionOnly]
        [OutputCache(Duration = _1hour)]
        public virtual ActionResult _MetaTags(String title, String keywords, String description)
        {
            EditSettingViewModel set = _setting.GetOptionsForShowOnFooter();
            ViewBag.Title = !String.IsNullOrEmpty(title) ?
                SeoExtentions.GeneratePageTitle(set.StoreName, title) :
                SeoExtentions.GeneratePageTitle(set.StoreName);
            ViewBag.Description = !String.IsNullOrEmpty(description) ? description : set.StoreDescription;
            ViewBag.KeyWords = !String.IsNullOrEmpty(keywords) ? keywords : set.StoreKeyWords;
            return PartialView(MVC.Shared.Views.MetaTag);
        }
        #endregion

        #region Subscribe
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(CacheProfile = "nostore")]
        [AjaxOnly]
        public virtual async Task<JsonResult> subscribe(SubscribeViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { result = false, hasMessage = false });

            Boolean result = await _subscribe.Add(model);

            if (!result)
                return Json(new { result = false, hasMessage = true, message = "این ایمیل قبلا ثبت شده است" });

            await _unitOfWork.SaveAllChangesAsync();
            return Json(new { result = true, hasMessage = true, message = "ایمیل شما ثبت شد.  جدیدترین محصولات به آگاهی شما می رسد " });
        }
        #endregion
    }
}