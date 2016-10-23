using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVCUI.Extentions
{
    public static class CookieHelper
    {
        public static void AddCookie(this HttpContextBase httpcontext, String cookieName, String value)
        {
            httpcontext.AddCookie(cookieName, value, DateTime.Now.AddDays(30));
        }

        public static void AddCookie(this HttpContextBase httpContext, String cookieName, String value, DateTime expire, Boolean httpOnly = false)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = httpContext.Server.UrlEncode(value);
            cookie.Expires = expire;
            cookie.HttpOnly = httpOnly;

            httpContext.Response.Cookies.Set(cookie);
        }

        public static void RemoveCookie(this HttpContextBase httpcontext, String cookieName)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Expires = DateTime.Now.AddDays(-1);
            httpcontext.Response.Cookies.Set(cookie);
        }

        public static void UpdateCookie(this HttpContextBase httpcontext, String cookieName, String value, Boolean httpOnly = false)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = httpcontext.Server.UrlEncode(value);
            cookie.HttpOnly = httpOnly;

            httpcontext.Response.Cookies.Set(cookie);
        }
        
        public static String GetCookieValue(this HttpContextBase httpcontext, String cookieName)
        {
            HttpCookie cookie = httpcontext.Request.Cookies[cookieName];
            if (cookie==null)
                return String.Empty; ;
            return httpcontext.Server.UrlDecode(cookie.Value);
        }
    }
}
