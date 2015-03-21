﻿using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SmebyFX_blog.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
//            var context = ApplicationDbContext.Create();
//            if (!context.Users.Any(u => u.UserName == "lars@smebyfx.no"))
//            {
//                var store = new UserStore<ApplicationUser>(context);
//                var manager = new UserManager<ApplicationUser>(store);
//                var user = new ApplicationUser {UserName = "lars@smebyfx.no"};
//                manager.Create(user, "passord");
//            }

            Bootstrapper.Initialize();
        }
    }
}
