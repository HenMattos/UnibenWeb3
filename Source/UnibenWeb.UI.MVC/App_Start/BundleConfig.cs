using System.Web;
using System.Web.Optimization;

namespace UnibenWeb.UI.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        // Use the development version of Modernizr to develop with and learn from. Then, when you're
        // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
        // Set EnableOptimizations to false for debugging. For more information,
        // visit http://go.microsoft.com/fwlink/?LinkId=301862

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/admin-lte/js/app.js",
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/pace.js",
                "~/Scripts/modernizr-*",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"
                ));

            bundles.Add(new StyleBundle("~/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css",
                "~/Content/site.css",
                "~/Content/font-awesome.css",
                "~/Content/ionicons/ionicons.css", // cade?
                "~/Content/pace.css",
                "~/Content/datatables.css",
                "~/Content/select2.css",
                "~/admin-lte/css/AdminLTE.css",
                "~/admin-lte/css/skins/skin-green.css"
                ));

            bundles.Add(new ScriptBundle("~/js/select2").Include("~/Scripts/select2.js"));
            bundles.Add(new ScriptBundle("~/js/datatables").Include("~/Scripts/datatables.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
