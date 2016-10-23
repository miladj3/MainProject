using DataLayer.Context;
using DomainClasses.Entities;
using DomainClasses.Enums;
using MVCUI.Filters;
using MVCUI.Helpers;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Utilities.Security;
using ViewModel.ViewModel.Admin.User;

namespace MVCUI.Areas.Admin.Controllers
{
    [T4MVC]
    [RouteArea("Admin")]
    [RoutePrefix("Customer")]
    [Route("{action}")]
    [SiteAuthorize(Roles = "admin")]
    public partial class UserController : Controller
    {

        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _user;
        private readonly ICommentService _comment;
        private readonly IRoleService _role;
        #endregion

        #region Constracture
        public UserController(IUnitOfWork unitOfWork, IUserService user, ICommentService comment, IRoleService role)
        {
            this._unitOfWork = unitOfWork;
            this._user = user;
            this._comment = comment;
            this._role = role;
        }
        #endregion

        #region Index
        [HttpGet]
        [Route("Index")]
        [OutputCache(CacheProfile = "nostore")]
        public virtual ActionResult Index()
        {
            ViewBag.UserSearchByList = DropDown.GetUserSearchByList(DomainClasses.Enums.UserSearchBy.PhoneNumber);
            return View();
        }

        [Route("List")]
        [HttpGet]
        [OutputCache(NoStore = false, Duration = 0, Location = System.Web.UI.OutputCacheLocation.None, VaryByParam = "*")]
        public virtual PartialViewResult List(String term = "",
                                                                Int32 pageNumber = 1,
                                                                Int32 pageCount = 10,
                                                                DomainClasses.Enums.Order order = DomainClasses.Enums.Order.Descending,
                                                                UserOrderBy userOrderBy = UserOrderBy.RegisterDate,
                                                                UserSearchBy userSearchBy = UserSearchBy.PhoneNumber)
        {
            Int32 totalUser;
            IList<UserViewModel> user = _user.GetDataTable(out totalUser, term, pageNumber, pageCount, order, userOrderBy, userSearchBy);
            UsersListViewModel model = new UsersListViewModel
            {
                UserOrderBy = userOrderBy,
                Order = order,
                PageCount = pageCount,
                PageNumber = pageNumber,
                Term = term,
                TotalUsers = totalUser,
                UsersList = user
            };

            ViewBag.UserSearchByList = DropDown.GetUserSearchByList(userSearchBy);
            ViewBag.UserOrderByList = DropDown.GetUserOrderByList(userOrderBy);
            ViewBag.CountList = DropDown.GetCountList(pageCount);
            ViewBag.OrderList = DropDown.GetOrderList(order);
            ViewBag.UserSearchBy = userSearchBy;
            
            return PartialView(MVC.Admin.User.Views.List, model);
        }
        #endregion

        #region Add User
        [HttpGet]
        [OutputCache(CacheProfile = "long")]
        [Route("Create")]
        public virtual async Task<ActionResult> Add()
        {
            IList<Role> role = await _role.GetAllRolesAsync();
            ViewBag.role = role.Select(x => new SelectListItem { Text = x.Description, Value = x.Id.ToString(CultureInfo.InvariantCulture) });
            return View(MVC.Admin.User.Views.Add);
        }

        [HttpPost]
        [OutputCache(CacheProfile ="nostore")]
        [Route("Create")]
        public virtual async Task<ActionResult> Add([Bind(Include = "UserName, PhoneNumber, Password, ConfirmPassword, FirstName, LastName, RoleId")] AddUserViewModel model)
        {
            IList<Role> role = await _role.GetAllRolesAsync();
            ViewBag.role = role.Select(x => new SelectListItem { Text = x.Description, Value = x.Id.ToString(CultureInfo.InvariantCulture) });

            if (!ModelState.IsValid)
                return View(MVC.Admin.User.Views.Add, model);

            if (!model.Password.Equals(model.ConfirmPassword))
            {
                ModelState.AddModelError("Password", "رمز عبور وارد شده با هم برابر نیست");
                ModelState.AddModelError("ConfirmPassword", "رمز عبور وارد شده با هم برابر نیست");
                return View(MVC.Admin.User.Views.Add, model);
            }
            
            User user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Password = Encryption.EncryptingPassword(model.Password),
                PhoneNumber = model.PhoneNumber,
                LastLoginDate = DateTime.Now,
                RegisterDate = DateTime.Now,
                Role = await _role.GetRoleByRoleIdAsync(model.RoleId)
            };
           AddUserStatus status=  await _user.AddAsync(user);
            switch (status)
            {
                case AddUserStatus.UserNameExist:
                    ModelState.AddModelError("UserName", "نام کاربری مورد نظر قبلا ثبت شده است");
                    return View(MVC.Admin.User.Views.Add, model); 

                case AddUserStatus.PhoneNumberExist:
                    ModelState.AddModelError("PhoeNumber", "شماره همراه مورد نظر قبلا ثبت شده است");
                    return View(MVC.Admin.User.Views.Add, model);

                case AddUserStatus.AddingUserSuccessfully:
                    await _unitOfWork.SaveAllChangesAsync(false);
                    return RedirectToAction(MVC.Admin.User.ActionNames.Add, MVC.Admin.User.Name);

                default:
                    ModelState.AddModelError("", "لطفا تمام خانه ها را با دقت پر نمایید");
                    return View(MVC.Admin.User.Views.Add, model);
            }
        }
        #endregion

        #region Details
        [Route("Details/{id:long}")]
        [HttpGet]
        [OutputCache(CacheProfile ="nostore")]
        public virtual async Task<ActionResult> Details(Int64? id)
        {
            if (id.Equals(null))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            DetailsUserViewModel model =await _user.GetUserDetailAsync(id.Value);
            if (model.Equals(null))
                return HttpNotFound();
            return View(MVC.Admin.User.Views.Details, model);
        }
        #endregion

        #region Edit
        [Route("Edit/{id:long}")]
        [HttpGet]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<ActionResult> Edit(Int64? id)
        {
            if (id.Equals(null))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            EditUserViewModel model = await _user.GetUserDataForEditAsync(id.Value);
            if (model.Equals(null))
                return HttpNotFound();
            ViewBag.Roles = new SelectList(await _role.GetAllRolesAsync(), "Id", "Description", model.RoleId);
            return View(MVC.Admin.User.Views.Edit, model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Edit/{id}")]
        public virtual async Task<ActionResult> Edit(EditUserViewModel model)
        {
            ViewBag.Roles = new SelectList(await _role.GetAllRolesAsync(), "Id", "Description", model.RoleId);

            if (!ModelState.IsValid)
                return View(MVC.Admin.User.Views.Edit, model);

            if (model.Password != null)
                if (model.Password.Equals(model.ConfirmPassword))
                {
                    ModelState.AddModelError("Password", "رمز عبور وارد شده با هم برابر نیست");
                    ModelState.AddModelError("ConfirmPassword", "رمز عبور وارد شده با هم برابر نیست");
                    return View(MVC.Admin.User.Views.Edit, model);
                }


            User user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                id = model.Id,
                Password = (model.Password !=null) ? Encryption.EncryptingPassword(model.Password) : null,
                PhoneNumber = model.PhoneNumber,
                Role = await _role.GetRoleByRoleIdAsync(model.RoleId),
                IsBaned = model.IsBaned,
                UserName = model.UserName
            };
            EditedUserStatus status =await _user.EditUserAsync(user);
            switch (status)
            {
                case EditedUserStatus.UpdatingUserSuccessfully:
                    await _unitOfWork.SaveAllChangesAsync(false);
                    return RedirectToAction(MVC.Admin.User.ActionNames.Index, MVC.Admin.User.Name);
                case EditedUserStatus.UserNameExist:
                    ModelState.AddModelError("UserName", "این نام کاربری  قبلا ثبت شده است");
                    return View(MVC.Admin.User.Views.Edit, model);
                case EditedUserStatus.PhoneNumberExist:
                    ModelState.AddModelError("PhoneNumber", "این شماره همراه قبلا ثبت شده است");
                    return View(MVC.Admin.User.Views.Edit, model);
                default:
                    ModelState.AddModelError("", "مشکل در ثبت اطلاعات، لطفا مجددا تلاش کنید");
                    return View(MVC.Admin.User.Views.Edit, model);
            }
        }
        #endregion

        #region Delete
        [HttpGet]
        [Route("Delete/{Id:long}")]
        [OutputCache(CacheProfile ="nostore")]
        public virtual async Task<ActionResult> Delete(Int64? Id)
        {
            if (Id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User model =await _user.GetUserByIdAsync(Id.Value);

            if (model == null)
                return HttpNotFound();

            UserViewModel m = new UserViewModel
            {
                FullName = model.FirstName + " " + model.LastName,
                Id=model.id
            };
          
            return View(MVC.Admin.User.Views.Delete, m);
        }

        [HttpPost]
        [OutputCache(CacheProfile ="nostore")]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Delete([Bind(Include ="id")]UserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(MVC.Admin.User.Views.Delete, model);
            await _comment.RemoveUserComments(model.Id);
            await _user.RemoveAsync(model.Id);
            await _unitOfWork.SaveAllChangesAsync();
            return RedirectToAction(MVC.Admin.User.ActionNames.Index, MVC.Admin.User.Name);
        }
        #endregion

        #region check for exist
        [HttpPost]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<JsonResult> CheckPhoneNumberIsExist(String PhoneNumber) =>
            await _user.ExistsByPhoneNumberAsync(PhoneNumber) ?
            Json(false) :
            Json(true);

        [HttpPost]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<JsonResult> CheckUserNameIsExist(String UserName) =>
            await _user.ExistsByUserNameAsync(UserName) ?
          Json(false) :
          Json(true);

        [HttpPost]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<JsonResult> EditCheckUserNameIsExist(String UserName, Int64 Id) =>
          await _user.ExistsByPhoneNumberAsync(UserName, Id) ?
        Json(false) :
        Json(true);

        [HttpPost]
        [OutputCache(CacheProfile = "nostore")]
        public virtual async Task<JsonResult> EditCheckPhoneNumberIsExist(String PhoneNumber, Int64 Id) =>
           await _user.ExistsByPhoneNumberAsync(PhoneNumber, Id) ?
           Json(false) :
           Json(true);
        #endregion
    }
}