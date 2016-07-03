using MVCUI.Filters;
using System.Web.Mvc;

namespace MVCUI.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //TODO: Remove Comment ForceTo WWW
            //  filters.Add(new ForceToWww("http://localhost"));
            filters.Add(new TurbolinksAttribute());
            filters.Add(new ElmahHandledErrorLoggerFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
