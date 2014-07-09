using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmebyFX_blog.DBMigration;
using SmebyFX_blog.Web.Authentication;

namespace SmebyFX_blog.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Migrate.MigrateToLatestVersion(ConfigurationManager.ConnectionStrings["blogDBconnectionString"].ConnectionString);

            var context = ApplicationDbContext.Create();
            if (!context.Users.Any(u => u.UserName == "lars@smebyfx.no"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser {UserName = "lars@smebyfx.no"};
                manager.Create(user, "passord");
            }
        }
    }
}
