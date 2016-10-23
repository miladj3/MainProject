using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace MVCUI.Filters
{
    public class CompressHtml : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpRequestBase _request = filterContext.HttpContext.Request;

            if (filterContext.IsChildAction)
                return;

            String _acceptEncoding = _request.Headers["Accept-Encoding"];

            if (String.IsNullOrEmpty(_acceptEncoding))
                return;

            _acceptEncoding = _acceptEncoding.ToUpperInvariant();

            HttpResponseBase _response = filterContext.HttpContext.Response;
            if (_acceptEncoding.Contains("GZIP"))
            {
                _response.Filter = new System.IO.Compression.GZipStream(_response.Filter, System.IO.Compression.CompressionMode.Compress);
                _response.Headers.Remove("Content-encoding");
                _response.AppendHeader("Content-encoding", "gzip");
            }
            else if (_acceptEncoding.Contains("DEFLATE"))
            {
                _response.Filter = new System.IO.Compression.DeflateStream(_response.Filter, System.IO.Compression.CompressionMode.Compress);
                _response.Headers.Remove("Content-encoding");
                _response.AppendHeader("Content-encoding", "deflate");
            }

            // Allow proxy servers to cache encoded and unencoded versions separately
            _response.AppendHeader("Vary", "Content-Encoding");
        }

    }
}