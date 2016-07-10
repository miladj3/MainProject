using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace MVCUI.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            BundleTable.Bundles.UseCdn = true;
            //TODO: after enable this , remove tag  <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" rel="stylesheet" />
            BundleTable.EnableOptimizations = false;  
            bundles.UseCdn = true;

          

            bundles.Add(new ScriptBundle("~/bundles/masterjs").Include(
                "~/Scripts/MyScripts/site.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/jquery-MVC-RemoveRow.js",
                 "~/Scripts/sweet-alert.min.js",
                "~/Scripts/jquery.autocomplete.min.js",
                "~/Scripts/cloud-zoom.js",
                "~/Scripts/star-rating.min.js",
                "~/Scripts/turbolinks.min.js",
                "~/Scripts/jquery.sliderPro.min.js",
                "~/Scripts/bootstrap-select.min.js",
                "~/Scripts/noty/packaged/jquery.noty.packaged.min.js",
                "~/Scripts/lazysizes.min.js",
                "~/Scripts/respond.js"));
            bundles.Add(new StyleBundle("~/fileinp/css").Include(
               "~/Content/fileinput.min.css"
               ));
            bundles.Add(new ScriptBundle("~/fileinp/js").Include(
               "~/Scripts/fileinput.min.js"));

            bundles.Add(new ScriptBundle("~/customerBundle/js").Include(
                "~/Scripts/starRating-plugin.js",
                "~/Scripts/MyScripts/AddToCart-plugin.js",
                "~/Scripts/MyScripts/AddToWishList-plugin.js",
                "~/Scripts/jquery.cookie.js",
                "~/Scripts/MyScripts/AddToCompareList-plugin.js",
                "~/Scripts/noty/packaged/jquery.noty.packaged.min.js",
                "~/Scripts/MyScripts/customer-actions.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/master.css",
                "~/Content/slideshow.css",
                "~/Content/cloud-zoom.css",
                "~/Content/slider-pro.min.css",
                "~/Content/star-rating.min.css",
                "~/Content/font-awesome.min.css",
                "~/Content/jquery.autocomplete.css",
                "~/Content/bootstrap-select.min.css",
                 "~/Content/product.css",
                 "~/Content/animate.min.css",
                "~/Content/sweet-alert.css"));

            bundles.Add(new StyleBundle("~/Search/css").Include(
                "~/Content/search.css"));
            

            bundles.Add(new StyleBundle("~/editor/css").Include(
             "~/Scripts/ckeditor/contents.css"
             ));
            bundles.Add(new ScriptBundle("~/editor/js").Include(
               "~/Scripts/ckeditor/ckeditor.js"));

            //############# Main Js 

            bundles.Add(new ScriptBundle(Links.BundleExtension.Scripts.Modernizer).Include(
              "~/Scripts/modernizr-*"));
            
            bundles.Add(new ScriptBundle(Links.BundleExtension.Scripts.JqueryVal)
                .Include("~/Scripts/jquery.validate*")
                .Include("~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle(Links.BundleExtension.Scripts.Jquery,
                "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.2.0.min.js")
            {
                CdnFallbackExpression = "window.jquery"
            }.Include("~/Scripts/jquery-{version}.js"));

            //############# Main Style

            bundles.Add(new StyleBundle(Links.BundleExtension.Style.MainCss_Css)
                .Include("~/Content/My_style/Reset_Css_Persian.min.css")
                .Include("~/Content/My_style/_MainCss.min.css"));

            bundles.Add(new StyleBundle(Links.BundleExtension.Style.fonts,
               "https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css")
               .Include("~/Content/font-awesome.min.css"));

            //############## Style & Js Admin Layout

            bundles.Add(new StyleBundle(Links.BundleExtension.Style.StyleOfAdminLayout).Include(
               "~/Content/bootstrap.min.css",
               "~/Content/bootstrap-select.min.css",
                "~/Content/sweet-alert.css",
                "~/Content/animate.min.css",
                "~/Content/plugins/metisMenu/metisMenu.min.css",
                "~/Content/plugins/timeline.css",
                "~/Content/sb-admin-2.css",
                "~/Content/plugins/morris.css"
                ));

            bundles.Add(new ScriptBundle(Links.BundleExtension.Scripts.JsForAdminLayout).Include(
              "~/Scripts/MyScripts/admin.js",
              "~/Scripts/MyScripts/site.js",
              "~/Scripts/lazysizes.min.js",
              "~/Scripts/jquery-MVC-RemoveRow.js",
               "~/Scripts/sweet-alert.min.js",
              "~/Scripts/json2.js",
              "~/Scripts/plugins/metisMenu/metisMenu.min.js",
             "~/Scripts/MyScripts/sb-admin-2.js",
              "~/Scripts/bootstrap.min.js",
              "~/Scripts/star-rating.min.js",
              "~/Scripts/jquery.sliderPro.min.js",
              "~/Scripts/bootstrap-select.min.js",
              "~/Scripts/noty/packaged/jquery.noty.packaged.min.js",
              "~/Scripts/noty/jquery.noty.js",
              "~/Scripts/respond.js"
              ));
        }
    }
}
