using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCUI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SiteAuthorizeAttribute : AuthorizeAttribute
    {

        protected override Boolean AuthorizeCore(HttpContextBase httpContext)
        {

            //HttpCookie cookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            //if (cookie != null)
            //{
            //    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
            //    String roleName = ticket.UserData;
            //    if (roleName.Equals(this.Roles))
            //        return true;
            //}

            return base.AuthorizeCore(httpContext);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        protected override HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            return base.OnCacheAuthorization(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                throw new UnauthorizedAccessException();
            }
            else
            {
                handleAjaxRequest(filterContext);
                base.HandleUnauthorizedRequest(filterContext);
            }
        }

        private void handleAjaxRequest(AuthorizationContext filterContext)
        {
            HttpContextBase _h = filterContext.HttpContext;
            if (!_h.Request.IsAjaxRequest())
                return;
            _h.Response.StatusCode = (Int32)HttpStatusCode.Forbidden;
            _h.Response.End();
        }
    }
}
