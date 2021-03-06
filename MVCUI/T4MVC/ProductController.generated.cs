// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace MVCUI.Controllers
{
    public partial class ProductController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ProductController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.JsonResult> SaveRating()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SaveRating);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.JsonResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.JsonResult> AddToPopulate()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.AddToPopulate);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.JsonResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.JsonResult> AddToCompare()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.AddToCompare);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.JsonResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult removeProductCompareFromHeader()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.removeProductCompareFromHeader);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult _SelectedProducctOfGP()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._SelectedProducctOfGP);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult _RelatedProduct()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._RelatedProduct);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult _GetProperties()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._GetProperties);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ProductController Actions { get { return MVC.Product; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Product";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Product";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string SaveRating = "SaveRating";
            public readonly string AddToPopulate = "AddToPopulate";
            public readonly string AddToCompare = "AddToCompare";
            public readonly string checkExistCookieForCompareList = "checkExistCookieForCompareList";
            public readonly string removeProductCompareFromHeader = "removeProductCompareFromHeader";
            public readonly string Compare = "Compare";
            public readonly string Index = "Index";
            public readonly string _SelectedProducctOfGP = "_SelectedProducctOfGP";
            public readonly string _RelatedProduct = "_RelatedProduct";
            public readonly string _GetProperties = "_GetProperties";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string SaveRating = "SaveRating";
            public const string AddToPopulate = "AddToPopulate";
            public const string AddToCompare = "AddToCompare";
            public const string checkExistCookieForCompareList = "checkExistCookieForCompareList";
            public const string removeProductCompareFromHeader = "removeProductCompareFromHeader";
            public const string Compare = "Compare";
            public const string Index = "Index";
            public const string _SelectedProducctOfGP = "_SelectedProducctOfGP";
            public const string _RelatedProduct = "_RelatedProduct";
            public const string _GetProperties = "_GetProperties";
        }


        static readonly ActionParamsClass_SaveRating s_params_SaveRating = new ActionParamsClass_SaveRating();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SaveRating SaveRatingParams { get { return s_params_SaveRating; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SaveRating
        {
            public readonly string productId = "productId";
            public readonly string value = "value";
            public readonly string sectionType = "sectionType";
        }
        static readonly ActionParamsClass_AddToPopulate s_params_AddToPopulate = new ActionParamsClass_AddToPopulate();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AddToPopulate AddToPopulateParams { get { return s_params_AddToPopulate; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AddToPopulate
        {
            public readonly string productId = "productId";
        }
        static readonly ActionParamsClass_AddToCompare s_params_AddToCompare = new ActionParamsClass_AddToCompare();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AddToCompare AddToCompareParams { get { return s_params_AddToCompare; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AddToCompare
        {
            public readonly string productId = "productId";
        }
        static readonly ActionParamsClass_removeProductCompareFromHeader s_params_removeProductCompareFromHeader = new ActionParamsClass_removeProductCompareFromHeader();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_removeProductCompareFromHeader removeProductCompareFromHeaderParams { get { return s_params_removeProductCompareFromHeader; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_removeProductCompareFromHeader
        {
            public readonly string productId = "productId";
        }
        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string productId = "productId";
            public readonly string name = "name";
        }
        static readonly ActionParamsClass__SelectedProducctOfGP s_params__SelectedProducctOfGP = new ActionParamsClass__SelectedProducctOfGP();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass__SelectedProducctOfGP _SelectedProducctOfGPParams { get { return s_params__SelectedProducctOfGP; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass__SelectedProducctOfGP
        {
            public readonly string categoryId = "categoryId";
            public readonly string take = "take";
        }
        static readonly ActionParamsClass__RelatedProduct s_params__RelatedProduct = new ActionParamsClass__RelatedProduct();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass__RelatedProduct _RelatedProductParams { get { return s_params__RelatedProduct; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass__RelatedProduct
        {
            public readonly string productId = "productId";
            public readonly string take = "take";
        }
        static readonly ActionParamsClass__GetProperties s_params__GetProperties = new ActionParamsClass__GetProperties();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass__GetProperties _GetPropertiesParams { get { return s_params__GetProperties; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass__GetProperties
        {
            public readonly string productId = "productId";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string _PropertiesProduct = "_PropertiesProduct";
                public readonly string _RelateProduct = "_RelateProduct";
                public readonly string _SelectedProductOfGP = "_SelectedProductOfGP";
                public readonly string _ShowProductRelatePartial = "_ShowProductRelatePartial";
                public readonly string _ShowProductSelectInGP = "_ShowProductSelectInGP";
                public readonly string Compare = "Compare";
                public readonly string Index = "Index";
            }
            public readonly string _PropertiesProduct = "~/Views/Product/_PropertiesProduct.cshtml";
            public readonly string _RelateProduct = "~/Views/Product/_RelateProduct.cshtml";
            public readonly string _SelectedProductOfGP = "~/Views/Product/_SelectedProductOfGP.cshtml";
            public readonly string _ShowProductRelatePartial = "~/Views/Product/_ShowProductRelatePartial.cshtml";
            public readonly string _ShowProductSelectInGP = "~/Views/Product/_ShowProductSelectInGP.cshtml";
            public readonly string Compare = "~/Views/Product/Compare.cshtml";
            public readonly string Index = "~/Views/Product/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ProductController : MVCUI.Controllers.ProductController
    {
        public T4MVC_ProductController() : base(Dummy.Instance) { }

        [NonAction]
        partial void SaveRatingOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long? productId, double? value, string sectionType);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.JsonResult> SaveRating(long? productId, double? value, string sectionType)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.SaveRating);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "productId", productId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "value", value);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "sectionType", sectionType);
            SaveRatingOverride(callInfo, productId, value, sectionType);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.JsonResult);
        }

        [NonAction]
        partial void AddToPopulateOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long? productId);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.JsonResult> AddToPopulate(long? productId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.AddToPopulate);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "productId", productId);
            AddToPopulateOverride(callInfo, productId);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.JsonResult);
        }

        [NonAction]
        partial void AddToCompareOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long? productId);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.JsonResult> AddToCompare(long? productId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.AddToCompare);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "productId", productId);
            AddToCompareOverride(callInfo, productId);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.JsonResult);
        }

        [NonAction]
        partial void checkExistCookieForCompareListOverride(T4MVC_System_Web_Mvc_ContentResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ContentResult checkExistCookieForCompareList()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ContentResult(Area, Name, ActionNames.checkExistCookieForCompareList);
            checkExistCookieForCompareListOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void removeProductCompareFromHeaderOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long productId);

        [NonAction]
        public override System.Web.Mvc.JsonResult removeProductCompareFromHeader(long productId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.removeProductCompareFromHeader);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "productId", productId);
            removeProductCompareFromHeaderOverride(callInfo, productId);
            return callInfo;
        }

        [NonAction]
        partial void CompareOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Compare()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Compare);
            CompareOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? productId, string name);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Index(long? productId, string name)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "productId", productId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "name", name);
            IndexOverride(callInfo, productId, name);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void _SelectedProducctOfGPOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? categoryId, int take);

        [NonAction]
        public override System.Web.Mvc.ActionResult _SelectedProducctOfGP(long? categoryId, int take)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._SelectedProducctOfGP);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "categoryId", categoryId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "take", take);
            _SelectedProducctOfGPOverride(callInfo, categoryId, take);
            return callInfo;
        }

        [NonAction]
        partial void _RelatedProductOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? productId, int take);

        [NonAction]
        public override System.Web.Mvc.ActionResult _RelatedProduct(long? productId, int take)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._RelatedProduct);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "productId", productId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "take", take);
            _RelatedProductOverride(callInfo, productId, take);
            return callInfo;
        }

        [NonAction]
        partial void _GetPropertiesOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? productId);

        [NonAction]
        public override System.Web.Mvc.ActionResult _GetProperties(long? productId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._GetProperties);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "productId", productId);
            _GetPropertiesOverride(callInfo, productId);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
