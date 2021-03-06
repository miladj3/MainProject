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
namespace MVCUI.Areas.Admin.Controllers
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
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Edit()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.JsonResult> Delete()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.Delete);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.JsonResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> FillAttributes()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.FillAttributes);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> AddPictures()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddPictures);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.RedirectToRouteResult> AddPicture()
        {
            var callInfo = new T4MVC_System_Web_Mvc_RedirectToRouteResult(Area, Name, ActionNames.AddPicture);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.RedirectToRouteResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> EditPictures()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditPictures);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> EditPicture()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditPicture);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ProductController Actions { get { return MVC.Admin.Product; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Admin";
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
            public readonly string Index = "Index";
            public readonly string List = "List";
            public readonly string Create = "Create";
            public readonly string Edit = "Edit";
            public readonly string Delete = "Delete";
            public readonly string FillAttributes = "FillAttributes";
            public readonly string AddPictures = "AddPictures";
            public readonly string AddPicture = "AddPicture";
            public readonly string EditPictures = "EditPictures";
            public readonly string EditPicture = "EditPicture";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string List = "List";
            public const string Create = "Create";
            public const string Edit = "Edit";
            public const string Delete = "Delete";
            public const string FillAttributes = "FillAttributes";
            public const string AddPictures = "AddPictures";
            public const string AddPicture = "AddPicture";
            public const string EditPictures = "EditPictures";
            public const string EditPicture = "EditPicture";
        }


        static readonly ActionParamsClass_List s_params_List = new ActionParamsClass_List();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_List ListParams { get { return s_params_List; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_List
        {
            public readonly string freeSend = "freeSend";
            public readonly string delete = "delete";
            public readonly string term = "term";
            public readonly string page = "page";
            public readonly string PageCount = "PageCount";
            public readonly string Order = "Order";
            public readonly string ProductOrderBy = "ProductOrderBy";
            public readonly string categoryId = "categoryId";
            public readonly string type = "type";
        }
        static readonly ActionParamsClass_Create s_params_Create = new ActionParamsClass_Create();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Create CreateParams { get { return s_params_Create; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Create
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Edit s_params_Edit = new ActionParamsClass_Edit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Edit EditParams { get { return s_params_Edit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Edit
        {
            public readonly string id = "id";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Delete s_params_Delete = new ActionParamsClass_Delete();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Delete DeleteParams { get { return s_params_Delete; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Delete
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_FillAttributes s_params_FillAttributes = new ActionParamsClass_FillAttributes();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_FillAttributes FillAttributesParams { get { return s_params_FillAttributes; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_FillAttributes
        {
            public readonly string productId = "productId";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_AddPictures s_params_AddPictures = new ActionParamsClass_AddPictures();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AddPictures AddPicturesParams { get { return s_params_AddPictures; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AddPictures
        {
            public readonly string productId = "productId";
        }
        static readonly ActionParamsClass_AddPicture s_params_AddPicture = new ActionParamsClass_AddPicture();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AddPicture AddPictureParams { get { return s_params_AddPicture; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AddPicture
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_EditPictures s_params_EditPictures = new ActionParamsClass_EditPictures();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EditPictures EditPicturesParams { get { return s_params_EditPictures; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EditPictures
        {
            public readonly string productId = "productId";
        }
        static readonly ActionParamsClass_EditPicture s_params_EditPicture = new ActionParamsClass_EditPicture();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EditPicture EditPictureParams { get { return s_params_EditPicture; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EditPicture
        {
            public readonly string model = "model";
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
                public readonly string _List = "_List";
                public readonly string AddPictures = "AddPictures";
                public readonly string Create = "Create";
                public readonly string Edit = "Edit";
                public readonly string EditPictures = "EditPictures";
                public readonly string FillAttributes = "FillAttributes";
                public readonly string Index = "Index";
            }
            public readonly string _List = "~/Areas/Admin/Views/Product/_List.cshtml";
            public readonly string AddPictures = "~/Areas/Admin/Views/Product/AddPictures.cshtml";
            public readonly string Create = "~/Areas/Admin/Views/Product/Create.cshtml";
            public readonly string Edit = "~/Areas/Admin/Views/Product/Edit.cshtml";
            public readonly string EditPictures = "~/Areas/Admin/Views/Product/EditPictures.cshtml";
            public readonly string FillAttributes = "~/Areas/Admin/Views/Product/FillAttributes.cshtml";
            public readonly string Index = "~/Areas/Admin/Views/Product/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ProductController : MVCUI.Areas.Admin.Controllers.ProductController
    {
        public T4MVC_ProductController() : base(Dummy.Instance) { }

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
        partial void ListOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo, bool freeSend, bool delete, string term, int page, int PageCount, DomainClasses.Enums.Order Order, DomainClasses.Enums.ProductOrderBy ProductOrderBy, long categoryId, DomainClasses.Enums.ProductType type);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult List(bool freeSend, bool delete, string term, int page, int PageCount, DomainClasses.Enums.Order Order, DomainClasses.Enums.ProductOrderBy ProductOrderBy, long categoryId, DomainClasses.Enums.ProductType type)
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.List);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "freeSend", freeSend);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "delete", delete);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "term", term);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "PageCount", PageCount);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Order", Order);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "ProductOrderBy", ProductOrderBy);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "categoryId", categoryId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "type", type);
            ListOverride(callInfo, freeSend, delete, term, page, PageCount, Order, ProductOrderBy, categoryId, type);
            return callInfo;
        }

        [NonAction]
        partial void CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Create()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
            CreateOverride(callInfo);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, ViewModel.ViewModel.Admin.Product.AddProductViewModel model);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Create(ViewModel.ViewModel.Admin.Product.AddProductViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            CreateOverride(callInfo, model);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? id);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Edit(long? id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EditOverride(callInfo, id);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, ViewModel.ViewModel.Admin.Product.EditProductViewModel model);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Edit(ViewModel.ViewModel.Admin.Product.EditProductViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            EditOverride(callInfo, model);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void DeleteOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long? id);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.JsonResult> Delete(long? id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.Delete);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DeleteOverride(callInfo, id);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.JsonResult);
        }

        [NonAction]
        partial void FillAttributesOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? productId);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> FillAttributes(long? productId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.FillAttributes);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "productId", productId);
            FillAttributesOverride(callInfo, productId);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void FillAttributesOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, System.Collections.Generic.IEnumerable<ViewModel.ViewModel.Admin.Product.FillProductAttributesViewModel> model);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.JsonResult> FillAttributes(System.Collections.Generic.IEnumerable<ViewModel.ViewModel.Admin.Product.FillProductAttributesViewModel> model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.FillAttributes);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            FillAttributesOverride(callInfo, model);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.JsonResult);
        }

        [NonAction]
        partial void AddPicturesOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? productId);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> AddPictures(long? productId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddPictures);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "productId", productId);
            AddPicturesOverride(callInfo, productId);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void AddPictureOverride(T4MVC_System_Web_Mvc_RedirectToRouteResult callInfo, ViewModel.ViewModel.Admin.Product.AddProductPicturesViewModel model);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.RedirectToRouteResult> AddPicture(ViewModel.ViewModel.Admin.Product.AddProductPicturesViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_RedirectToRouteResult(Area, Name, ActionNames.AddPicture);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            AddPictureOverride(callInfo, model);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.RedirectToRouteResult);
        }

        [NonAction]
        partial void EditPicturesOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long? productId);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> EditPictures(long? productId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditPictures);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "productId", productId);
            EditPicturesOverride(callInfo, productId);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

        [NonAction]
        partial void EditPictureOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Collections.Generic.IEnumerable<DomainClasses.Entities.ProductImage> model);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> EditPicture(System.Collections.Generic.IEnumerable<DomainClasses.Entities.ProductImage> model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditPicture);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            EditPictureOverride(callInfo, model);
            return System.Threading.Tasks.Task.FromResult(callInfo as System.Web.Mvc.ActionResult);
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
