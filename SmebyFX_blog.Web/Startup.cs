//using Microsoft.AspNet.Identity;
//using Microsoft.Owin;
//using Microsoft.Owin.Security.Cookies;
//using Owin;
//using SmebyFX_blog.Web.Authentication;
//
//[assembly: OwinStartup(typeof(SmebyFX_blog.Web.Startup))]
//
//namespace SmebyFX_blog.Web
//{
//    public class Startup
//    {
//        public void Configuration(IAppBuilder app)
//        {
//            app.CreatePerOwinContext(ApplicationDbContext.Create);
//            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
//            app.UseCookieAuthentication(new CookieAuthenticationOptions
//            {
//                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
//                LoginPath = new PathString("/Admin/Login")
//            });
//        }
//    }
//}
