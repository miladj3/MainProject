using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class ForceToWww : ActionFilterAttribute
    {
        private readonly String _url;
        private ActionExecutingContext _filterContext;
        private HttpRequestBase _request;

        public ForceToWww(String url)
        {
            _url = new Uri(url).Host.ToLowerInvariant();
        }

        private Boolean CanIgnoreRequest
        {
            get
            {
                Uri url = _request.Url;
                return url != null &&
                            (_filterContext.IsChildAction ||
                            _filterContext.HttpContext.Request.IsAjaxRequest() ||
                            url.AbsoluteUri.Contains("?"));
            }
        }

        private Boolean IsDomainSetCorrectly
        {
            get
            {
                return (_request.Url != null) && (_request.Url.Host == _url);
            }
        }

        private Boolean IsLocalRequet
        {
            get
            {
                return _request.IsLocal;
            }
        }

        private Boolean IsRootRequest
        {
            get
            {
                 Uri url = _request.Url;
                return (url != null) && (url.AbsolutePath.Equals("/"));
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _filterContext = filterContext;
            _request = filterContext.RequestContext.HttpContext.Request;
            ModifyUrlAndRedirect();

            base.OnActionExecuting(filterContext);
        }

        private void ModifyUrlAndRedirect()
        {
            if (this.IsDomainSetCorrectly || this.IsLocalRequet || this.IsRootRequest)
                return;
            Uri url = _request.Url;
            if (url.Equals(null))
                return;
            UriBuilder newUri = new UriBuilder(url);
            newUri.Host = _url;
            String absoluteUrl = HttpUtility.UrlDecode(newUri.Uri.AbsoluteUri.ToString(CultureInfo.InvariantCulture));
            if (String.IsNullOrEmpty(absoluteUrl))
                return;
            String redirectUrl = absoluteUrl.ToLowerInvariant();
            redirectUrl = AvoidTrailingSlashes(redirectUrl);
            _filterContext.Controller.ViewBag.CanonicalUrl = redirectUrl;

            _filterContext.Result = new RedirectResult(redirectUrl, true);
        }

        private String AvoidTrailingSlashes(String redirectUrl)
        {
            if (!this.IsRootRequest && _url.EndsWith("/"))
                redirectUrl = redirectUrl.TrimEnd(new[] { '/' });
            return redirectUrl;
        }
    }
}
