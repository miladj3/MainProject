using CaptchaMvc.Infrastructure;
using DataLayer.Context;
using IocConfig;
using MVCUI.App_Start;
using MVCUI.Searching;
using ServiceLayer.Interfaces;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using StructureMap.Web.Pipeline;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace MVCUI
{
    public class MvcApplication : System.Web.HttpApplication
    {

        #region Begin_start
        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)
                MiniProfiler.Start();
        }
        #endregion

        #region Application_Start
        protected void Application_Start()
        {
            try
            {
                //HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                AreaRegistration.RegisterAllAreas();
                GlobalConfiguration.Configure(WebApiConfig.Register);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
                MvcHandler.DisableMvcResponseHeader = true;
                ViewEngines.Engines.Clear();
                ViewEngines.Engines.Add(new RazorViewEngine());
                CaptchaUtils.CaptchaManager.StorageProvider = new CookieStorageProvider();
                ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
                //  SetDbInitializer();
                MiniProfilerEF6.Initialize();
                //LuceneProducts.CreateIndexes(SampleObjectFactory.Container.GetInstance<IProductService>().GetAllForAddLucene());
            }
            catch 
            {
                HttpRuntime.UnloadAppDomain();
                throw;
            }
        }
        #endregion

        #region Application_PreSendRequestHeaders
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            if (app.Equals(null) || app.Context.Equals(null))
                return;
            app.Context.Response.Headers.Remove("Server");
        }
        #endregion

        #region Application_AuthenticateRequest
        //TODO: check this

        //protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        //{
        //    if (Context.User.Equals(null))
        //        return;
        //    IUserService userService = SampleObjectFactory.Container.GetInstance<IUserService>();
        //    UserStatus statusUser = userService.GetStatus(Context.User.Identity.Name);

        //    if (statusUser.IsBaned)
        //        FormsAuthentication.SignOut();

        //    HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
        //    if (authCookie.Equals(null) || authCookie.Value=="")
        //        return;
        //    FormsAuthenticationTicket authTicker = FormsAuthentication.Decrypt(authCookie.Value);

        //    if (authTicker.Equals(null))
        //        return;

        //    var roles = authTicker.UserData.Split(',');
        //    if (statusUser.Role != roles[0])
        //        FormsAuthentication.SignOut();

        //    Context.User = new GenericPrincipal(Context.User.Identity, roles);
        //}
        #endregion

        #region Application_EndRequest
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            HttpContextLifecycle.DisposeAndClearAll();
            MiniProfiler.Stop();
        }
        #endregion

        #region Private Member
        public class StructureMapControllerFactory : DefaultControllerFactory
        {
            protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
            {
                if (controllerType == null)
                {
                    //TODO: CHECK
                    //var url = requestContext.HttpContext.Request.RawUrl;
                    ////string.Format("Page not found: {0}", url).LogException();

                    //requestContext.RouteData.Values["controller"] = MVC.Search.Name;
                    //requestContext.RouteData.Values["action"] = MVC.Search.ActionNames.Index;
                    //requestContext.RouteData.Values["keyword"] = url.GenerateSlug().Replace("-", " ");
                    //requestContext.RouteData.Values["categoryId"] = 0;
                    //return SampleObjectFactory.Container.GetInstance(typeof(SearchController)) as Controller;
                    throw new InvalidOperationException(string.Format("Page not found: {0}", requestContext.HttpContext.Request.RawUrl));
                }

                return SampleObjectFactory.Container.GetInstance(controllerType) as Controller;
            }
        }
        private void SetDbInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShopDbContext, DataLayer.Migrations.Configuration>());
            SampleObjectFactory.Container.GetInstance<IUnitOfWork>().ForceDatabaseInitialize();
        }
        #endregion
    }
}
