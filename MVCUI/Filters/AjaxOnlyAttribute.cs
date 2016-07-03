using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCUI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AjaxOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
                base.OnActionExecuting(filterContext);
            else
                throw new InvalidOperationException("ارسال فقط از طریق اژاکس قابل قبول میباشد\nThis operation can only be accessed via Ajax requests");
        }
    }
}
