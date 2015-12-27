using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Exam.Common.Mappings;
using Exam.Data;
using Exam.Data.Migrations;

namespace Exam.App
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ExamDbContext, Configuration>());
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var autoMapperConfig = new AutoMapperConfig(new List<Assembly> { Assembly.GetExecutingAssembly() });
            autoMapperConfig.Execute();
        }
    }
}
