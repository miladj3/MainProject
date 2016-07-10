using DataLayer.Context;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Controllers
{
    [RoutePrefix("Customer")]
    [Route("{action}")]
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
        [HttpGet]
        public virtual ActionResult Login(String returnUrl)
        {
            ViewBag.Title = "ورود کاربر";
            ViewBag.retunUrl = returnUrl;
          ///  if (Request.IsAjaxRequest())

            return View();
        }
        #endregion

        #region register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async virtual Task<ActionResult> Register()
        {
            return View();
        }
        #endregion
    }
}