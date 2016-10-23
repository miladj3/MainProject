using MVCUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace MVCUI.Filters
{
    public class UrlConstraint : IRouteConstraint
    {
        public Boolean Match(HttpContextBase httpContext, Route route, String parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            String url = httpContext.Request.RawUrl;
            values["word"]= url.GenerateSlug().Replace("-", " ");
            return true;
        }
    }
}