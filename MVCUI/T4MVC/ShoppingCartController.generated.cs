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
    public partial class ShoppingCartController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ShoppingCartController(Dummy d) { }

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
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.JsonResult> AddToCart()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.AddToCart);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.JsonResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.JsonResult> RemoveFromCart()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.RemoveFromCart);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.JsonResult);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ShoppingCartController Actions { get { return MVC.ShoppingCart; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "ShoppingCart";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "ShoppingCart";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string AddToCart = "AddToCart";
            public readonly string RemoveFromCart = "RemoveFromCart";
            public readonly string ShowShoppingCardWhenRefreshPage = "ShowShoppingCardWhenRefreshPage";
            public readonly string Index = "Index";
            public readonly string PersonDetail = "PersonDetail";
            public readonly string CompleteOrder = "CompleteOrder";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string AddToCart = "AddToCart";
            public const string RemoveFromCart = "RemoveFromCart";
            public const string ShowShoppingCardWhenRefreshPage = "ShowShoppingCardWhenRefreshPage";
            public const string Index = "Index";
            public const string PersonDetail = "PersonDetail";
            public const string CompleteOrder = "CompleteOrder";
        }


        static readonly ActionParamsClass_AddToCart s_params_AddToCart = new ActionParamsClass_AddToCart();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AddToCart AddToCartParams { get { return s_params_AddToCart; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AddToCart
        {
            public readonly string productId = "productId";
            public readonly string value = "value";
        }
        static readonly ActionParamsClass_RemoveFromCart s_params_RemoveFromCart = new ActionParamsClass_RemoveFromCart();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_RemoveFromCart RemoveFromCartParams { get { return s_params_RemoveFromCart; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_RemoveFromCart
        {
            public readonly string shoppingId = "shoppingId";
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
                public readonly string _ShowShoppingCardWhenRefreshPage = "_ShowShoppingCardWhenRefreshPage";
                public readonly string CompleteOrder = "CompleteOrder";
                public readonly string Index = "Index";
                public readonly string PersonDetail = "PersonDetail";
            }
            public readonly string _ShowShoppingCardWhenRefreshPage = "~/Views/ShoppingCart/_ShowShoppingCardWhenRefreshPage.cshtml";
            public readonly string CompleteOrder = "~/Views/ShoppingCart/CompleteOrder.cshtml";
            public readonly string Index = "~/Views/ShoppingCart/Index.cshtml";
            public readonly string PersonDetail = "~/Views/ShoppingCart/PersonDetail.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ShoppingCartController : MVCUI.Controllers.ShoppingCartController
    {
        public T4MVC_ShoppingCartController() : base(Dummy.Instance) { }

        [NonAction]
        partial void AddToCartOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long? productId, decimal? value);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.JsonResult> AddToCart(long? productId, decimal? value)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.AddToCart);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "productId", productId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "value", value);
            AddToCartOverride(callInfo, productId, value);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.JsonResult);
        }

        [NonAction]
        partial void RemoveFromCartOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long? shoppingId);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.JsonResult> RemoveFromCart(long? shoppingId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.RemoveFromCart);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "shoppingId", shoppingId);
            RemoveFromCartOverride(callInfo, shoppingId);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.JsonResult);
        }

        [NonAction]
        partial void ShowShoppingCardWhenRefreshPageOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ShowShoppingCardWhenRefreshPage()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ShowShoppingCardWhenRefreshPage);
            ShowShoppingCardWhenRefreshPageOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void PersonDetailOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.PartialViewResult> PersonDetail()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.PersonDetail);
            PersonDetailOverride(callInfo);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.PartialViewResult);
        }

        [NonAction]
        partial void CompleteOrderOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.PartialViewResult> CompleteOrder()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.CompleteOrder);
            CompleteOrderOverride(callInfo);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.PartialViewResult);
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
