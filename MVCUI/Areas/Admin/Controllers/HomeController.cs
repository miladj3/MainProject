using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCUI.Filters;
namespace MVCUI.Areas.Admin.Controllers
{
    //[RouteArea("Admin")]
    //[RoutePrefix("Home")]
    //[Route("{action}/{id?}")]
    // [SiteAuthorize(Roles ="admin")]
    public partial class HomeController : Controller
    {
        //[Route]
        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}