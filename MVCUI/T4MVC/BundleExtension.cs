using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Links
{
    public static partial class BundleExtension
    {
        public static class Style
        {
            public static readonly String MainCss_Css= "~/MainCss/Css";
            public static readonly String fonts = "~/fonts";
            public static readonly String StyleOfAdminLayout = "~/adminContent/css";
            public static readonly String fileInputUploadCSS = "~/fileinp/css";
            public static readonly String CkeEditoreCSS = "~/editor/css";
            public static readonly String PagedStyleCss = "~/paged/css";
        }
        public static class Scripts
        {
            public static readonly String Modernizer = "~/bundles/modernizr";
            public static readonly String Jquery = "~/scripts/jquery";
            public static readonly String JqueryVal = "~/bundles/jqueryval";
            public static readonly String JsForAdminLayout = "~/admin/js";
            public static readonly String fileInputUploadJS = "~/fileinp/js";
            public static readonly String CkeEditoreJS = "~/editor/js";
            public static readonly String adminJS="~/admin/js";
            public static readonly String _starRating_Compare_AddToPopulate_AddToCart = "~/_starRating_Compare_AddToPopulate_AddToCart/js";
        }
        public static class RenderSection
        {
            public static readonly String CssSection = "css";
            public static readonly String ScriptsSection = "scipts";
            public static readonly String MetaTagSection = "metatags";
        }

        public static class PathAbsolouteUploadPicture
        {
            public static readonly String PathUploadPicture = "~/UploadFiles";
        }

        public static class CookiesName
        {
            public static readonly String CookieNameOfCompareProduct = "CompareProduct";
            public static readonly String CountCompareList = "CountProductInCompareList";
            public static readonly String CookieNameOfcheckoutTotal = "checkoutTotal";
            public static readonly String CookieNameOfcheckoutModel = "checkoutModel";
        }
    }
}
