using CaptchaMvc.Infrastructure;
using DataLayer.Context;
using IocConfig;
using MVCUI.App_Start;
using MVCUI.Controllers;
using MVCUI.Extentions;
using MVCUI.Helpers;
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
#if DEBUG
            if (Request.IsLocal)
                MiniProfiler.Start();
#endif
        }
        #endregion

        #region Application_Start
        protected void Application_Start()
        {
            try
            {
                CacheManager.InvalidateChildActionsCache();
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                GlobalConfiguration.Configure(WebApiConfig.Register);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
                MvcHandler.DisableMvcResponseHeader = true;
                ViewEngines.Engines.Clear();
                ViewEngines.Engines.Add(new RazorViewEngine());
                CaptchaUtils.CaptchaManager.StorageProvider = new CookieStorageProvider();
                ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
#if DEBUG
                HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
                SetDbInitializer();
#endif
                // MiniProfilerEF6.Initialize();
                LuceneProducts.CreateIndexes(SampleObjectFactory.Container.GetInstance<IProductService>().GetAllForAddLucene());
            }
            catch
            {
                // سبب ری استارت برنامه و آغاز مجدد آن با درخواست بعدی می‌شود
                HttpRuntime.UnloadAppDomain();
                throw;
            }
        }
        #endregion

        #region Application_PreSendRequestHeaders
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            if (app == null || app.Context == null)
                return;
            app.Context.Response.Headers.Remove("Server");
        }
        #endregion

        #region Application_AuthenticateRequest
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (Context.User == null) return;

            IUserService userService = SampleObjectFactory.Container.GetInstance<IUserService>();
            var t = User.Identity.Name;
            UserStatus statusUser = userService.GetStatus(Context.User.Identity.Name);

            if (statusUser.IsBaned)
                FormsAuthentication.SignOut();

            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie.Equals(null) || authCookie.Value == "")
                return;

            FormsAuthenticationTicket authTicker = FormsAuthentication.Decrypt(authCookie.Value);

            if (authTicker.Equals(null))
                return;

            String[] roles = authTicker.UserData.Split(',');
            GenericIdentity userIdentity = new GenericIdentity(User.Identity.Name);
            GenericPrincipal userPrincipal = new GenericPrincipal(userIdentity, roles);

            if (statusUser.Role != roles[0])
                FormsAuthentication.SignOut();

            Context.User = userPrincipal;
        }
        #endregion

        #region Application_EndRequest
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            HttpContextLifecycle.DisposeAndClearAll();
           if(Request.IsLocal)
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
                    var url = requestContext.HttpContext.Request.RawUrl;

                    requestContext.RouteData.Values["controller"] = MVC.Search.Name;
                    requestContext.RouteData.Values["action"] = MVC.Search.ActionNames.Search;
                    requestContext.RouteData.Values["word"] = url.GenerateSlug().Replace("-", " ");
                    return SampleObjectFactory.Container.GetInstance(typeof(SearchController)) as Controller;

                    throw new InvalidOperationException(string.Format("Page not found: {0}", requestContext.HttpContext.Request.RawUrl));
                }
                return SampleObjectFactory.Container.GetInstance(controllerType) as Controller;
            }
        }
        private void SetDbInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShopDbContext, DataLayer.Migrations.Configuration>());
            // Database.SetInitializer<ShopDbContext>(null);
            SampleObjectFactory.Container.GetInstance<IUnitOfWork>().ForceDatabaseInitialize();
        }
        #endregion
    }
}
