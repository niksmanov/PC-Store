using PCstore.Data;
using PCstore.Data.Migrations;
using PCstore.Web.App_Start;
using System.Data.Entity;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PCstore.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MsSqlDbContext, Configuration>());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var mapper = new AutoMapperConfig();
            mapper.Execute(Assembly.GetExecutingAssembly());
        }
    }
}
