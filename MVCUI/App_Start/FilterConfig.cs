using MVCUI.Filters;
using System.Web.Mvc;

namespace MVCUI.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new TurbolinksAttribute());
            filters.Add(new ElmahHandledErrorLoggerFilter());
            filters.Add(new HandleErrorAttribute());

#if DEBUG
            filters.Add(new DisableCache());
#endif

#if !DEBUG
            filters.Add(new CompressHtml());
            filters.Add(new ForceToWww("http://www.samteb.ir"));
#endif

        }
    }
}
