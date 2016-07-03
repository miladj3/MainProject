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
            BundleTable.EnableOptimizations = false;
            bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
               "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/admin/js").Include(
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

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

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
                "~/Content/mycss.css",
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

            bundles.Add(new StyleBundle("~/adminContent/css").Include(
               "~/Content/bootstrap.min.css",
               "~/Content/mycss.css",
               "~/Content/bootstrap-select.min.css",
                "~/Content/sweet-alert.css",
                "~/Content/animate.min.css",
                "~/Content/plugins/metisMenu/metisMenu.min.css",
                "~/Content/plugins/timeline.css",
                "~/Content/sb-admin-2.css",
                "~/Content/plugins/morris.css",
                "~/Content/font-awesome.min.css"
                ));


            bundles.Add(new StyleBundle("~/editor/css").Include(
             "~/Scripts/ckeditor/contents.css"
             ));
            bundles.Add(new ScriptBundle("~/editor/js").Include(
               "~/Scripts/ckeditor/ckeditor.js"));


            //var jqueryBundle = new ScriptBundle("~/scripts/jquery",
            //    "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.2.0.min.js")
            //{
            //    CdnFallbackExpression = "window.jquery"
            //}.Include("~/Scripts/jquery-{version}.js");

            //var fontAwsome = new StyleBundle("~/fonts", "https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css").Include("~/Content/font-awesome.min.css");
            //bundles.Add(fontAwsome);
        }
    }
}
