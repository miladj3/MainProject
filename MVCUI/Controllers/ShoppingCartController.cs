using DataLayer.Context;
using DomainClasses.Entities;
using DomainClasses.Enums;
using MVCUI.Extentions;
using MVCUI.Filters;
using ServiceLayer.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ViewModel.ViewModel.Admin.Order;
using ViewModel.ViewModel.Admin.Setting;
using ViewModel.ViewModel.Admin.ShoppingCard;
using ViewModel.ViewModel.Admin.User;

namespace MVCUI.Controllers
{
    [T4MVC]
    [RoutePrefix("Order")]
    [Route("{action}")]
    public partial class ShoppingCartController : Controller
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShoppingCartService _shoppingCart;
        private readonly IProductService _product;
        private readonly IUserService _user;
        private readonly ISettingService _setting;
        private readonly IOrderService _order;
        #endregion

        #region Constracture
        public ShoppingCartController(IUnitOfWork unitOWork,
                                                        IProductService productservice,
                                                        IShoppingCartService shoppingcart,
                                                        IUserService user,
                                                        ISettingService setting,
                                                        IOrderService order)
        {
            this._unitOfWork = unitOWork;
            this._shoppingCart = shoppingcart;
            this._product = productservice;
            this._user = user;
            this._setting = setting;
            this._order = order;
        }
        #endregion

        #region Add To Cart
        [HttpPost]
        [AjaxOnly]
        [OutputCache(CacheProfile = "nostore")]
        [Route("AddToCart")]
        [SiteAuthorize]
        public virtual async Task<JsonResult> AddToCart(Int64? productId, Decimal? value)
        {
            if (productId == null)
                return Json(new { result = false, hasmessage = false, mesaage = "" });

            Product products = await _product.GetById(productId.Value);
            if (products == null)
                return Json(new { result = false, hasmessage = false, mesaage = "" });

            Decimal count = value ?? products.Ratio;
            Decimal result = Decimal.Remainder(count, products.Ratio);
            if (result != Decimal.Zero)
                return Json(new { result = false, hasmessage = true, mesaage = "در حال حاضر قادر به خرید کالا نمیباشید" });

            Boolean _isCanAdd = await _product.CanAddToShoppingCart(productId.Value, count);

            if (!_isCanAdd)
                return Json(new { result = false, hasmessage = true, mesaage = "در حال حاضرکالا موجود نمیباشد" });

            ShoppingCart _shopCatItem = await _shoppingCart.GetCartItem(productId.Value, User.Identity.Name);

            products.Reserve += count;
            Boolean isModel;
            if (_shopCatItem == null)
            {
                _shopCatItem = new ShoppingCart
                {
                    CartNumber = User.Identity.Name,
                    CreateDate = DateTime.Now,
                    Product = products,
                    Quantity = count,
                    isComplete = false,
                    TotalQuantity=count
                };
                _shoppingCart.Add(_shopCatItem);
                isModel = true;
            }
            else
            {
                _shopCatItem.Quantity += count;
                _shopCatItem.TotalQuantity = count;
                isModel = false;
            }
            await _unitOfWork.SaveAllChangesAsync(false);
            Decimal _totalValueInCart = await _shoppingCart.TotalValueInCart(User.Identity.Name);
            return Json(new
            {
                result = true,
                hasmessage = true,
                message = "محصول مورد نظر به سبد خرید افزوده شد",
                hasModel = isModel,
                value = new
                {
                    id = _shopCatItem.Id,
                    name = products.Name,
                    image = products.PrincipleImagePath,
                    totalNumberer = _totalValueInCart,
                    QuanitityItem = _shopCatItem.Quantity
                }
            });
        }
        #endregion

        #region Remove From ShoppingCard

        [HttpPost]
        [Route("RemoveFromShoppingCard")]
        [OutputCache(CacheProfile = "nostore")]
        [SiteAuthorize]
        [AjaxOnly]
        public virtual async Task<JsonResult> RemoveFromCart(Int64? shoppingId)
        {
            if (shoppingId == null)
                return Json(new { result = false, hasmessage = false }, JsonRequestBehavior.AllowGet);

            String _cartName = User.Identity.Name;
            Tuple<Decimal, Int64> result = await _shoppingCart.DeleteItem(shoppingId.Value, _cartName);

            if (result.Item1 == Decimal.Zero)
                return Json(new { result = false, hasmessage = false }, JsonRequestBehavior.AllowGet);

            await _product.DecreaseReserve(result.Item2, result.Item1);
            await _unitOfWork.SaveAllChangesAsync(false);
            Decimal count = await _shoppingCart.TotalValueInCart(_cartName);
            return Json(new { result = true, hasmessage = true, message = "کالای مورد نظر شما از سبدخرید حذف شد", count = count }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Show In Shopping Card When Refresh Page
        [ChildActionOnly]
        public virtual ActionResult ShowShoppingCardWhenRefreshPage()
        {
            String _shoppingCartName = User.Identity.Name;
            IEnumerable<ShoppingCart> model = _shoppingCart.ListNoAsync(_shoppingCartName);
            Decimal? count = model.Sum(x => x.Quantity);
            IList<shoppingCartInHeaderViewModel> _m = model.Select(x => new shoppingCartInHeaderViewModel
            {
                Name = x.Product.Name,
                Id = x.Id,
                Image = x.Product.PrincipleImagePath,
                Count = (Int64)count.Value,
                CountItem = (Int64)x.Quantity
            }).ToList();

            return PartialView(MVC.ShoppingCart.Views._ShowShoppingCardWhenRefreshPage, _m);
        }

        #endregion

        #region Index
        [HttpGet]
        [SiteAuthorize]
        [Route("Checkout")]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<ActionResult> Index()
        {
            ViewBag.Title = "تکمیل سفارش";
            String cartName = User.Identity.Name;
            IEnumerable<ShoppingCart> model = await _shoppingCart.List(cartName);
            IEnumerable<ShoppingCardViewModel> _m = model.Select(x => new ShoppingCardViewModel
            {
                CreateDate = x.CreateDate,
                Id = x.Id,
                ImageProduct = x.Product.PrincipleImagePath,
                isComplete = x.isComplete,
                NameProduct = x.Product.Name,
                PriceProduct = x.Product.Price,
                PriceProductAfterDiscount = x.Product.PriceAfterDiscount,
                ProductId = x.ProductId,
                Quantity = (Int64)x.Quantity,
                IsFreeShipping = x.Product.IsFreeShipping,
                PriceTotalProduct = 0
            }).ToList();
            return View(MVC.ShoppingCart.Views.Index, _m);
        }
        #endregion

        #region Order
        [HttpGet]
        [Route("PersonDetail")]
        [SiteAuthorize]
        [OutputCache(CacheProfile = "long")]
        [AjaxOnly]
        public virtual async Task<PartialViewResult> PersonDetail()
        {
            String userName = User.Identity.Name;
            UserWithAddressViewModel _m = await _user.GetInfoUserForOrder(userName);
            return PartialView(MVC.ShoppingCart.Views.PersonDetail, _m);
        }
        #endregion

        #region Complete Order
        [HttpPost]
        [Route("CompleteOrder")]
        [OutputCache(CacheProfile = "nostore")]
        [SiteAuthorize]
        [AjaxOnly]
        public virtual async Task<PartialViewResult> CompleteOrder()
        {
            EditSettingViewModel model = _setting.GetOptionsForShowOnFooter();
            String nameUser = User.Identity.Name;
            UserWithAddressViewModel _m = await _user.GetInfoUserForOrder(nameUser);
            IEnumerable<ShoppingCart> shoppingCard = await _shoppingCart.List(nameUser, false);
            Int64 totalQ = 0;

            shoppingCard.ToList().ForEach(x => {
                x.isComplete = true;
                totalQ += x.Product.PriceAfterDiscount * ((Int64)x.Quantity == 0 ? 1 : (Int64)x.Quantity);
            });

            StringBuilder _transactionCode = new StringBuilder(capacity: 10);
            _transactionCode.Append(nameUser.ToCharArray(0, 3));
            Random _r = new Random();
            _transactionCode.Append(_r.Next());

            String _address = _m.Address.State + " " + _m.Address.City + " " + _m.Address.AddressLine1 + " / " + _m.Address.AddressLine2 + "   " + _m.Register.PhoneNumber;

            OrderShowViewModel order = new OrderShowViewModel
            {
                Address =_address,
                BuyDate = DateTime.Now,
                DiscountPrice = 0,
                PeymentType = PeymentType.AtHome,
                Status = OrderStatus.NoSeen,
                TransactionCode = _transactionCode.ToString(),
                TotalPrice = totalQ,
                UserName = nameUser
            };

            await _order.Add(order, shoppingCard);
            await _unitOfWork.SaveAllChangesAsync(false);
            String text = $@"خریدار محترم در حال حاظر قادر به ادامه عملیات نمیباشید.همکاران ما در اسرع وقت با شما تماس گرفته و سفارشات شما را به آدرس شما ارسال میکنند. لطفا در صورت هرگونه سوال با شماره های {model.PhoneNumber1} و {model.PhoneNumber2} و یا {model.Tel1} تماس حاصل فرمایید.
                کد پیگیری محصول {_transactionCode} میباشد.";
            return PartialView(MVC.ShoppingCart.Views.CompleteOrder, (object)text);
        }
        #endregion
    }
}