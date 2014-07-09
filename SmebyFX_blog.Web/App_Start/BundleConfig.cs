using System.Web.Optimization;

namespace SmebyFX_blog.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(CssBundle());
            bundles.Add(JsLibsBundle());
            bundles.Add(JsAdmin());
        }

        private static Bundle CssBundle()
        {
            return new Bundle("~/Content/Style")
                .Include("~/Content/Style/SmebyFX_blog.min.css");
        }

        private static Bundle JsLibsBundle()
        {
            return new Bundle("~/Scripts/Libs")
                .Include("~/Scripts/Libs/*.js");
        }

        private static Bundle JsAdmin()
        {
            return new Bundle("~/Scripts/Admin")
                .Include("~/Scripts/Admin/*.js");
        }
    }
}