using MVCUI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Contact")]
    [Route("{action}")]
    [T4MVC]
    [SiteAuthorize(Roles = "admin")]
    public partial class OrderController : Controller
    {
        [HttpGet]
        [Route("Index")]
        [OutputCache(CacheProfile ="long")]
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}