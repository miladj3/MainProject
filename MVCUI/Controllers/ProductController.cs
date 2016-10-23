using DataLayer.Context;
using DomainClasses.Entities;
using MVCUI.Extentions;
using MVCUI.Filters;
using MVCUI.Searching;
using ServiceLayer.EFServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ViewModel.ViewModel.Admin.Product;

namespace MVCUI.Controllers
{
    [T4MVC]
    [RoutePrefix("Product")]
    [Route("{action}")]
    public partial class ProductController : Controller
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductService _product;
        private readonly UserService _user;
        private readonly ValueService _value;
        private readonly CategoryService _category;
        #endregion

        #region Constracture
        public ProductController(IUnitOfWork unitOfworks,
                                                UserService user,
                                                ValueService value,
                                                ProductService product,
                                                CategoryService category)
        {
            this._unitOfWork = unitOfworks;
            this._value = value;
            this._product = product;
            this._user = user;
            this._category = category;
        }
        #endregion

        #region Rating
        [HttpPost]
        [AjaxOnly]
        [SiteAuthorize]
        [OutputCache(CacheProfile = "nostore")]
        [Route("SaveRating")]
        public virtual async Task<JsonResult> SaveRating(Int64? productId, Double? value, String sectionType)
        {
            if (productId == null ||
                value == null ||
                String.IsNullOrWhiteSpace(sectionType))
                return Json(new { result = false, hasMessage = false });

            Product model = await _product.GetById(productId.Value);
            if (model == null)
                return Json(new { result = false, hasMessage = false });

            Boolean _canUserRate = await _product.CanUserRate(productId.Value, User.Identity.Name);
            if (!_canUserRate)
                return Json(new { result = false, hasMessage = true, message = "شما به قبلا به این کالا امتیاز داده اید" });

            switch (sectionType)
            {
                case "PRODUCT":
                    try
                    {
                        await _product.SaveRating(productId.Value, value.Value);
                        User currentUser = await _user.GetUserByUserName(User.Identity.Name);
                        await _product.AddUserToLikedUsers(productId.Value, currentUser);
                        await _unitOfWork.SaveAllChangesAsync(true);
                        return Json(new { result = true, hasMessage = true, message = "امتیاز شما ثبت گردید" });
                    }
                    catch (Exception)
                    {
                        return Json(new { result = false, hasMessage = true, message = "مشکل داخلی بوجود آمده است. لطفا بار دیگر امتحان نمایید" });
                    }
                default:
                    return Json(new { result = false, hasMessage = true, message = "مشکل داخلی بوجود آمده است. لطفا بار دیگر امتحان نمایید" });
            }
        }
        #endregion

        #region Populate
        [HttpPost]
        [SiteAuthorize]
        [Route("AddToPopulate")]
        [OutputCache(CacheProfile = "nostore")]
        [AjaxOnly]
        public virtual async Task<JsonResult> AddToPopulate(Int64? productId)
        {
            if (productId == null)
                return Json(new { result = false, hasMessage = false, message = "" });
            String userName = User.Identity.Name;
            Boolean _userLimited = await _user.LimitAddToWishList(userName);
            if (_userLimited)
                return Json(new { result = false, hasMessage = true, message = "شما قادر به انتخاب بیشتر از پنجاه عدد محصول نمیباشید" });

            Boolean _userCanAdd = await _product.CanUserAddToWishList(productId.Value, userName);
            if (!(_userCanAdd))
                return Json(new { result = false, hasMessage = true, message = "قبلا به لیست مورد علاقه شما افزوده شده است" });

            User user = await _user.GetUserByUserName(userName);
            await _product.AddUserToWishList(productId.Value, user);
            await _unitOfWork.SaveAllChangesAsync(false);
            return Json(new { result = true, hasMessage = true, message = "به محصولات مورد علاقه افزوده شد" });
        }
        #endregion

        #region Compare
        [HttpPost]
        [Route("AddToCompare")]
        [OutputCache(CacheProfile = "nostore")]
        [AjaxOnly]
        public virtual async Task<JsonResult> AddToCompare(Int64? productId)
        {
            if (productId == null)
                return Json(new { result = false });

            String _cookiename = Links.BundleExtension.CookiesName.CookieNameOfCompareProduct,
             _countCompareList = Links.BundleExtension.CookiesName.CountCompareList;

            IList<ProductCompareViewModel> _itemsInCookie = new List<ProductCompareViewModel>();
            String _cookieValue = HttpContext.GetCookieValue(_countCompareList);
            Int32 _count = String.IsNullOrEmpty(_cookieValue) ? 0 : Int32.Parse(_cookieValue);
            if (_count > 3)
                return Json(new { result = false, hasMessage = true, message = "بیشتر از حدمجاز. حدمجاز 4 عدد میباشد" });

            if (_count == 0)
                HttpContext.AddCookie(_countCompareList, (++_count).ToString(), DateTime.Now.AddDays(1));
            else
            {
                _itemsInCookie = new JavaScriptSerializer().Deserialize<IList<ProductCompareViewModel>>(HttpContext.GetCookieValue(_cookiename));

                if (_itemsInCookie.Any(x => x.ProductId == productId))
                    return Json(new { result = false, hasMessage = true, message = "قبلا به لیست مقایسه اضافه شده است" });

                if (_itemsInCookie.Count > 3)
                    return Json(new { result = false, hasMessage = true, message = "بیشتر از حدمجاز. حدمجاز 4 عدد میباشد" });

                HttpContext.UpdateCookie(_countCompareList, (++_count).ToString());
            }
            ProductCompareViewModel _model = await _product.GetForAddToCompare(productId.Value);
            _itemsInCookie.Add(_model);
            HttpContext.UpdateCookie(_cookiename, _itemsInCookie.ToJson());
            return Json(new
            {
                result = true,
                hasMessage = true,
                message = "به لیست مقایسه کالا افزوده گردید",
                el = new
                {
                    name = _model.ProductName,
                    pic = _model.ImagePath,
                    id = _model.ProductId
                }
            });
        }

        [ChildActionOnly]
        [OutputCache(Duration = 1, VaryByParam = "*")]
        [Route("checkExistCookieForCompareList")]
        public virtual ContentResult checkExistCookieForCompareList()
        {
            IList<ProductCompareViewModel> _model = new List<ProductCompareViewModel>();
            IList<ProductCompareInHeaderViewModel> _m = new List<ProductCompareInHeaderViewModel>();
            StringBuilder _content = new StringBuilder();
            _model = new JavaScriptSerializer().Deserialize<IList<ProductCompareViewModel>>(HttpContext.GetCookieValue(Links.BundleExtension.CookiesName.CookieNameOfCompareProduct));
            _m = _model.Select(x => new ProductCompareInHeaderViewModel
            {
                ImagePath = x.ImagePath,
                ProductId = x.ProductId,
                ProductName = x.ProductName
            }).ToList();

            foreach (ProductCompareInHeaderViewModel item in _m)
                _content.Append($@"<li id=""compare_el_{ item.ProductId }"">
                                <i class=""fa fa-times"" data-id=""{ item.ProductId }"" aria-hidden=""true""></i>
                                <strong>{ item.ProductName }</strong>
                                <img src = ""{ item.ImagePath }"" />
                        </li>");

            return Content(_content.ToString());
        }

        [HttpPost]
        [OutputCache(CacheProfile = "nostore")]
        [Route("removeProductCompareFromHeader")]
        [AjaxOnly]
        public virtual JsonResult removeProductCompareFromHeader(Int64 productId)
        {
            try
            {
                Int64 _count = Int64.Parse(HttpContext.GetCookieValue(Links.BundleExtension.CookiesName.CountCompareList));
                --_count;
                if (_count <= 0)
                {
                    HttpContext.RemoveCookie(Links.BundleExtension.CookiesName.CountCompareList);
                    HttpContext.RemoveCookie(Links.BundleExtension.CookiesName.CookieNameOfCompareProduct);
                    return Json(true);
                }
                IList<ProductCompareViewModel> _model = new List<ProductCompareViewModel>();
                _model = new JavaScriptSerializer().Deserialize<IList<ProductCompareViewModel>>(HttpContext.GetCookieValue(Links.BundleExtension.CookiesName.CookieNameOfCompareProduct));
                ProductCompareViewModel _item = _model.SingleOrDefault(x => x.ProductId == productId);
                _model.Remove(_item);
                HttpContext.UpdateCookie(Links.BundleExtension.CookiesName.CookieNameOfCompareProduct, _model.ToJson());
                HttpContext.UpdateCookie(Links.BundleExtension.CookiesName.CountCompareList, _count.ToString());
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpGet]
        [OutputCache(CacheProfile = "nostore")]
        [Route("Compare")]
        public virtual ActionResult Compare()
        {
            IList<ProductCompareViewModel> _allProduct = new List<ProductCompareViewModel>();
            _allProduct = new JavaScriptSerializer().Deserialize<IList<ProductCompareViewModel>>(HttpContext.GetCookieValue(Links.BundleExtension.CookiesName.CookieNameOfCompareProduct));

            return View(MVC.Product.Views.Compare, _allProduct);
        }
        #endregion

        #region View Product
        [HttpGet]
        [OutputCache(CacheProfile = "long")]
        [Route("{productId}/{name}")]
        public virtual async Task<ActionResult> Index(Int64? productId, String name)
        {
            if (productId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ProductDetailsViewModel model = await _product.GetForShowDetails(productId.Value);
            if (model == null)
                return HttpNotFound();

            model.CategoryName = await _category.GetCategoryName(model.CategoryId);
            await _unitOfWork.SaveAllChangesAsync(false);
            return View(MVC.Product.Views.Index, model);
        }

        [ChildActionOnly]
        [HttpGet]
        public virtual ActionResult _SelectedProducctOfGP(Int64? categoryId, Int32 take = 4)
        {
            if (categoryId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.productId = categoryId.Value;
            IEnumerable<ProductSectionViewModel> _category = _product.GetSelecionProductOfCategory(categoryId.Value, take);

            return PartialView(MVC.Product.Views._SelectedProductOfGP, _category);
        }

        [ChildActionOnly]
        [HttpGet]
        public virtual ActionResult _RelatedProduct(Int64? productId, Int32 take = 4)
        {
            if (productId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            IList<ProductSearchResultViewModel> model = LuceneProducts.ShowMoreLikeThisPostItems(productId.Value);

            if (model == null)
                model = new List<ProductSearchResultViewModel>
                {
                   new ProductSearchResultViewModel
                     {
                            Id = productId.Value,
                            ImagePath = "",
                            Name = ""
                    }
                };
            if (model != null)
                model = model.Where(x => x.Id != productId.Value).ToList();

            return PartialView(MVC.Product.Views._RelateProduct, model);
        }

        [HttpGet]
        [ChildActionOnly]
        public virtual ActionResult _GetProperties(Int64? productId)
        {
            if (productId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            IEnumerable<AttributeValueViewModel> _model = _value.GetProductProperties(productId.Value);
            return PartialView(MVC.Product.Views._PropertiesProduct, _model);
        }
        #endregion
    }
}