using System.Web;
using System.Web.Optimization;

namespace Audit
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/jquery-1.10.2.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Angular").Include("~/Scripts/angular.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Layout").Include("~/Scripts/Views/LayoutModule.js"));

            bundles.Add(new ScriptBundle("~/Scripts/AuditLayout").IncludeDirectory("~/Scripts/Views", "*.js", true));

            bundles.Add(new StyleBundle("~/Content/css").IncludeDirectory("~/Content/Audit", "*.css", true));

            bundles.Add(new StyleBundle("~/Content/metro").IncludeDirectory("~/Content/Metro", "*.css", true));


        }
    }
}
