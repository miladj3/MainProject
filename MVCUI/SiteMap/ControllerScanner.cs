using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.SiteMap
{
    public class ControllerScanner
    {
        public static List<String> ScanAllControllers(HttpRequestBase requestBase)
        {
            Assembly _assembly = Assembly.GetAssembly(typeof(MvcApplication));
            var controllerActionList = _assembly.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Select(x =>
                new
                {
                    Controller = x.DeclaringType.Name.Replace("Controller", ""),
                    ReturnType = x.ReturnType.Name
                })
                .OrderBy(x => x.Controller).Distinct().ToList();

            if (requestBase.Url == null)
                return null;
            String url = requestBase.Url.GetLeftPart(UriPartial.Authority);
            return controllerActionList.Select(controller => $"{url}/{controller.Controller}").ToList();
        }
    }
}