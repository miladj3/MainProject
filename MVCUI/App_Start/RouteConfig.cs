using MVCUI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("Content/{*pathInfo}");
            routes.IgnoreRoute("Scripts/{*pathInfo}");
            routes.IgnoreRoute("UploadFiles/{*pathInfo}");
            routes.IgnoreRoute("fonts/{*pathInfo}");
            routes.IgnoreRoute("robots.txt");
            routes.IgnoreRoute("img/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.RouteExistingFiles = true;
            routes.LowercaseUrls = true;
            routes.MapMvcAttributeRoutes();
            AreaRegistration.RegisterAllAreas();


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, area = "" },
                namespaces: new[] { "MVCUI.Controllers" }
            );

            routes.MapRoute(
                name: "CatchAllRoute",
                url: "{*url}",
                defaults: new { controller = "Search", action = "Search", word = UrlParameter.Optional, area = "" },
                 namespaces: new[] { "MVCUI.Controllers" },
                 constraints: new { word = new UrlConstraint() });
        }
    }
}
