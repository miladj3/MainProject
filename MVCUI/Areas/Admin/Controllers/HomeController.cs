using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCUI.Filters;
namespace MVCUI.Areas.Admin.Controllers
{
    [T4MVC]
    [RouteArea("Admin")]
    [RoutePrefix("Home")]
    [Route("{action}")]
    [SiteAuthorize(Roles = "admin")]
    public partial class HomeController : Controller
    {
        [HttpGet]
        [Route("Index")]
        public virtual ActionResult Index()
        {
            return View();
        }
    
        [ChildActionOnly]
        public virtual PartialViewResult _sideBar()
        {
            return PartialView(MVC.Admin.Home.Views._SideBarPartial);
        }
    }
}