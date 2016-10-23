using System;
using System.Web.Mvc;

namespace MVCUI.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override String AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
               name: "Admin_default",
               url: "Admin/{controller}/{action}/{id}",
                defaults: new { controller="Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new String[] { "MVCUI.Areas.Admin.Controllers" }
            );
        }
    }
}