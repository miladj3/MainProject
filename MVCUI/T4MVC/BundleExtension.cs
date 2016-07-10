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
        }
        public static class Scripts
        {
            public static readonly String Modernizer = "~/bundles/modernizr";
            public static readonly String Jquery = "~/scripts/jquery";
            public static readonly String JqueryVal = "~/bundles/jqueryval";
            public static readonly String JsForAdminLayout = "~/admin/js";

        }
        public static class RenderSection
        {
            public static readonly String CssSection = "css";
            public static readonly String ScriptsSection = "scipts";
        }
    }
}
