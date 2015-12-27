namespace Caching.App
{
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Caching.Data;

    using Configuration = Caching.Data.Migrations.Configuration;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());

            AutoMapperConfig.Execute();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            string cn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlCacheDependencyAdmin.EnableTableForNotifications(cn, new[] { "AspNetUsers" });
        }

        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            if (arg == "host")
            {
                return context.Request.Headers["host"];
            }

            return String.Empty;
        }
    }
}