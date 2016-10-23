using MVCUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Helpers
{
    public static class UrlGenerator
    {
        public static MvcHtmlString ReturnUrl(this HtmlHelper htmlHelper, HttpContextBase contextBase, UrlHelper urlHelper)
        {
            String currentUrl = contextBase.Request.RawUrl;
           if (currentUrl.Equals("/"))
               currentUrl = urlHelper.Action(MVC.Home.ActionNames.Index, MVC.Home.Name);

            return MvcHtmlString.Create(currentUrl);
        }
    }
}
