using CaptchaMvc.HtmlHelpers;
using DataLayer.Context;
using DomainClasses.Entities;
using DomainClasses.Enums;
using MVCUI.Filters;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Utilities.Security;
using ViewModel.ViewModel.Admin.Product;
using ViewModel.ViewModel.Admin.User;

namespace MVCUI.Controllers
{
    [RoutePrefix("Customer")]
    [Route("{action}")]
    [T4MVC]
    public partial class UserController : Controller
    {

        #region Field
        private readonly IRoleService _role;
        private readonly IUserService _user;
        private readonly IProductService _product;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constracture
        public UserController(IUserService user,
                                            IRoleService role,
                                            IProductService produt,
                                            IUnitOfWork unitOfWork)
        {
            _role = role;
            _unitOfWork = unitOfWork;
            _user = user;
            _product = produt;
        }
        #endregion

        #region Login
        [OutputCache(CacheProfile = "long")]
        [HttpGet]
        [AllowAnonymous]
        [Route("Login")]
        public virtual ActionResult Login(String returnUrl)
        {
            ViewBag.retunUrl = returnUrl;
            //LoginViewModel model = new LoginViewModel
            //{
            //    Password = "12345abc",
            //    PhoneNumber = "09120001234",
            //    returnUrl = ""
            //};
            if (User.Identity.IsAuthenticated)
                return RedirectToAction(actionName: MVC.User.ActionNames.Profile, controllerName: MVC.User.Name);

            return View(MVC.User.Views.Login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(CacheProfile = "nostore")]
        [AllowAnonymous]
        [Route("Login")]
        public virtual async Task<ActionResult> Login([Bind(Include = "PhoneNumber, Password, returnUrl, RememberMe")] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "لطفا خانه های مورد نیاز را بادقت پرنمایید");
                ViewBag.retunUrl = model.returnUrl;
                return View(MVC.User.Views.Login, model);
            }

            String username = String.Empty;
            Int64 userId = 0;
            String ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            VerifyUserStatus verificationResult = _user.VerifyUserByPhoneNumber(model.PhoneNumber, model.Password, ref username, ref userId, ip);

            switch (verificationResult)
            {
                case VerifyUserStatus.VerifiedSuccessfully:
                    {
                        Role roleOfTheUserId = await _role.GetRoleByUserId(userId);
                        SetAuthCookie(username, roleOfTheUserId.Name, model.RememberMe);
                        await _unitOfWork.SaveAllChangesAsync(false);
                        if (isValidReturnUrl(model.returnUrl))
                            return Redirect(model.returnUrl);
                        return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
                    }
                case VerifyUserStatus.UserIsBaned:
                    {
                        ModelState.AddModelError("", "حساب کاربری شما مسدود است");
                        return View(MVC.User.Views.Login, model);
                    }
                case VerifyUserStatus.VerifiedFaild:
                default:
                    {
                        ModelState.AddModelError("", "اطلاعات وارد شده صحیح نمی باشند");
                        return View(MVC.User.Views.Login, model);
                    }
            }
        }

        #endregion

        #region register
        [HttpGet]
        [Route("Register")]
        [AllowAnonymous]
        [OutputCache(CacheProfile = "long")]
        public virtual ActionResult Register()
        {
            //UserWithAddressViewModel model = new UserWithAddressViewModel
            //{
            //    Register = new RegisterViewModel
            //    {
            //        FirstName = "میلاد",
            //        LastName = "جعفری",
            //        Password = "123456abc",
            //        PhoneNumber = "09120001234",
            //        UserName = "miladj3",
            //        VerifyPassword = "123456abc"
            //    },
            //    Address = new AddressUserViewModel
            //    {
            //        AddressLine1 = "آدرس شماره یک در محل",
            //        City = "تهران",
            //        State = "تهران",
            //        AddressLine2 = "آدرس شماره دو در محل",
            //        CompanyName = "نام شرکت"
            //    }
            //};
            if (User.Identity.IsAuthenticated)
                return RedirectToAction(actionName: MVC.User.ActionNames.Profile, controllerName: MVC.User.Name);

            return View(MVC.User.Views.Register);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(CacheProfile = "nostore")]
        [Route("Register")]
        [AllowAnonymous]
        public virtual async Task<ActionResult> Register([Bind(Include = "Register, Address")] UserWithAddressViewModel model)
        {
            //TODO: UnComment For Recaptcha in register User
            //if (!this.IsCaptchaValid(" تصویر صحیح نمیباشد"))
            //    return View(MVC.User.Views.Register, model);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "لطفا تمام خانه ها را با دقت پر نمایید.");
                return View(MVC.User.Views.Register, model);
            }

            if (!model.Register.Password.Equals(model.Register.VerifyPassword))
            {
                ModelState.AddModelError("Register.VerifyPassword", "تکرار رمز عبور وارد شده با رمز عبور تفاوت دارد");
                return View(MVC.User.Views.Register, model);
            }

            User newUser = new User
            {
                FirstName = model.Register.FirstName,
                LastName = model.Register.LastName,
                RegisterDate = DateTime.Now,
                IP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"],
                IsBaned = false,
                UserName = model.Register.UserName,
                PhoneNumber = model.Register.PhoneNumber,
                Password = Encryption.EncryptingPassword(model.Register.Password),
                Role = _role.GetRoleByName("user"),
                LastLoginDate = DateTime.Now,
                Address = new Address
                {
                    State = model.Address.State,
                    City = model.Address.City,
                    AddressLine1 = model.Address.AddressLine1,
                    AddressLine2 = model.Address.AddressLine2,
                    CompanyName = model.Address.CompanyName
                }
            };
            var Str = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            AddUserStatus resultAddUser = await _user.AddAsync(newUser);
            switch (resultAddUser)
            {
                case AddUserStatus.UserNameExist:
                    ModelState.AddModelError("Register.UserName", "نام کاربری وارد شده توسط کاربران قبلا رزرو شده است.");
                    ModelState.AddModelError("", "لطفا تمام خانه ها را با دقت پر نمایید.");
                    return View(MVC.User.Views.Register, model);

                case AddUserStatus.PhoneNumberExist:
                    ModelState.AddModelError("Register.PhoneNumber", "شماره همراه وارد شده قبلا درسیستم ثبت شده است.");
                    ModelState.AddModelError("", "لطفا تمام خانه ها را با دقت پر نمایید.");
                    return View(MVC.User.Views.Register, model);

                case AddUserStatus.AddingUserSuccessfully:
                    Role roleUser = newUser.Role;
                    SetAuthCookie(newUser.UserName, roleUser.Name, true);
                    await _unitOfWork.SaveAllChangesAsync(false);
                    return RedirectToAction(actionName: MVC.Home.ActionNames.Index, controllerName: MVC.Home.Name);

                default:
                    ModelState.AddModelError("", "مشکل داخلی . لطفا دوباره تلاش کنید.");
                    return View(MVC.User.Views.Register, model);
            }
        }
        #endregion

        #region Forget Password

        [HttpGet]
        [AllowAnonymous]
        [OutputCache(CacheProfile = "long")]
        [Route("ForgetPassword")]
        public virtual ActionResult ForgetPassword() =>
            View(MVC.User.Views.ForgetPassword);

        [HttpPost]
        [OutputCache(CacheProfile = "nostore")]
        [Route("ForgetPassword")]
        [AllowAnonymous]
        public virtual async Task<ActionResult> ForgetPassword([Bind(Include = "PhoneNumber")] ForgetPasswordViewModel model)
        {
            if (!this.IsCaptchaValid("جواب تصویر نادرست است"))
            {
                ModelState.AddModelError("", "لطفا تمام خانه ها را با دقت پر نمایید");
                return View(MVC.User.Views.ForgetPassword, MVC.User.Name, model);
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "لطفا تمام خانه ها را با دقت پر نمایید");
                return View(MVC.User.Views.ForgetPassword, MVC.User.Name, model);
            }

            Boolean result = await _user.ExistsByPhoneNumberAsync(model.PhoneNumber);
            TagBuilder _div = new TagBuilder("div");
            _div.MergeAttribute("id", "result00", true);
            TagBuilder _p = new TagBuilder("p");
            switch (result)
            {
                case true:
                    _p.SetInnerText("رمز عبور جدید برای شما ارسال شد");
                    _div.InnerHtml += _p.ToString();
                    ViewBag.result = _div;
                    return View(MVC.User.Views.ForgetPassword);
                case false:
                default:
                    _p.SetInnerText("شماره وارد شده وجود ندارد");
                    _div.InnerHtml += _p.ToString();
                    ViewBag.result = _div;
                    return View(MVC.User.Views.ForgetPassword);
            }
        }
        #endregion

        #region LogOut
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("LogOut")]
        [OutputCache(CacheProfile = "nostore")]
        [SiteAuthorize]
        public virtual ActionResult logOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
        }
        #endregion

        #region Populate List
        [SiteAuthorize]
        [HttpGet]
        [OutputCache(CacheProfile = "nostore")]
        [Route("~/Populate/{ page=1 }")]
        public virtual ActionResult WhishesList(Int32 page = 1)
        {
            Int32 total,
                    count = 10;
            IEnumerable<ProductSectionViewModel> products = _user.GetUserWishList(out total, page, count, User.Identity.Name);
            ViewBag.CountWhishesList = total;
            ViewBag.page = page;
            return View(MVC.User.Views.WhishesList, products);
        }

        [SiteAuthorize]
        [OutputCache(CacheProfile = "nostore")]
        [HttpPost]
        [Route("RemovePopulate")]
        public virtual async Task<JsonResult> RemovePopulate(Int64? productId)
        {
            if (productId == null)
                return Json(false);
            User user = await _user.GetUserByUserName(User.Identity.Name);
            try
            {
                await _product.RemoveFromWishList(productId.Value, user);
                await _unitOfWork.SaveAllChangesAsync();
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
                throw;
            }
        }
        #endregion

        #region Order
        //UNDONE: Order List In Section User
        public virtual ActionResult Order()
        {
            return View();
        }
        #endregion

        #region Comment
        //UNDONE:  Comment List In Section User
        public virtual ActionResult Comment()
        {
            return View();
        }
        #endregion

        #region Profile
        [SiteAuthorize]
        [Route("Profile")]
        [OutputCache(CacheProfile = "nostore")]
        [HttpGet]
        public virtual new ActionResult Profile()
        {
            return View();
        }

        #endregion

        #region Edit Adrress In Order
        [HttpGet]
        [Route("EditUserOrder")]
        [OutputCache(CacheProfile = "nostore")]
        [SiteAuthorize]
        [AjaxOnly]
        public virtual async Task<PartialViewResult> EditUserOrder()
        {
            String userName = User.Identity.Name;
            UserWithAddressViewModel model = await _user.GetInfoUserForOrder(userName);
            return PartialView(MVC.User.Views._EditUserOrder, model);
        }

        [HttpPost]
        [Route("EditUserOrder")]
        [OutputCache(CacheProfile = "nostore")]
        [SiteAuthorize]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public virtual async Task<PartialViewResult> EditUserOrder(UserWithAddressViewModel viewModel)
        {
            String userName = User.Identity.Name;
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("", "لطفا خانه های مورد نظر با دقت پرنمایید");
                return PartialView(MVC.User.Views._EditUserOrder, viewModel);
            }
            EditedUserStatus status = await _user.UpdateInfoOrder(viewModel, userName);

            switch (status)
            {
                case EditedUserStatus.UpdatingUserSuccessfully:
                    await _unitOfWork.SaveAllChangesAsync();
                    return PartialView(MVC.ShoppingCart.Views.PersonDetail, viewModel);

                case EditedUserStatus.UserNameExist:
                case EditedUserStatus.PhoneNumberExist:
                default:
                    ModelState.AddModelError("", "لطفا خانه های مورد نظر با دقت پرنمایید");
                    return PartialView(MVC.User.Views._EditUserOrder, viewModel);
            }
        }
        #endregion

        #region check for exist
        [HttpGet]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<JsonResult> CheckPhoneNumberIsExist([Bind(Prefix = "Register.PhoneNumber, Register.OldPhoneNumber")]String PhoneNumber, String OldPhoneNumber)
        {
            String phone = Request.QueryString["Register.PhoneNumber"];
            String oldphone = Request.QueryString["Register.OldPhoneNumber"];

            if (String.IsNullOrEmpty(oldphone))
                return await _user.ExistsByPhoneNumberAsync(phone) ?
             Json(false, JsonRequestBehavior.AllowGet) :
             Json(true, JsonRequestBehavior.AllowGet);

            else
            {
                if (phone.ToUpperInvariant() == oldphone.ToUpperInvariant())
                    return Json(true, JsonRequestBehavior.AllowGet);

                else
                    return await _user.ExistsByPhoneNumberAsync(phone) ?
                 Json(false, JsonRequestBehavior.AllowGet) :
                 Json(true, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<JsonResult> CheckUserNameIsExist([Bind(Prefix = "Register.UserName")]String UserName) =>
            await _user.ExistsByUserNameAsync(UserName) ?
            Json(false) :
            Json(true);


        #endregion

        #region Authentication
        [NonAction]
        private void SetAuthCookie(String username, String roleOfUser, Boolean rememberMe)
        {
            Double timeOut = rememberMe ? FormsAuthentication.Timeout.TotalMinutes : 30;
            DateTime now = DateTime.UtcNow.ToLocalTime();
            TimeSpan ExpiretionTimeOut = TimeSpan.FromMinutes(timeOut);
            FormsAuthenticationTicket authTicks = new FormsAuthenticationTicket(
                1,
                username,
                now,
                now.Add(ExpiretionTimeOut),
                rememberMe,
                roleOfUser,
                FormsAuthentication.FormsCookiePath);
            String enctype = FormsAuthentication.Encrypt(authTicks);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, enctype)
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Path = FormsAuthentication.FormsCookiePath
            };

            if (FormsAuthentication.CookieDomain != null)
                authCookie.Domain = FormsAuthentication.CookieDomain;

            if (rememberMe)
                authCookie.Expires = DateTime.Now.AddMinutes(timeOut);

            Response.Cookies.Add(authCookie);
        }
        #endregion

        #region Return Url Valid
        [NonAction]
        private Boolean isValidReturnUrl(String returnUrl) =>
            Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1;
        #endregion
    }
}