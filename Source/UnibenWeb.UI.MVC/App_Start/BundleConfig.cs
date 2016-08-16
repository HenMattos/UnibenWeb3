﻿using System.Web;
using System.Web.Optimization;

namespace UnibenWeb.UI.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                                    "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin-lte").Include(
                "~/admin-lte/js/app.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/css/font-awesome.css",
                      "~/Content/ionicons/ionicons.css",
                      "~/admin-lte/css/AdminLTE.css",
                      "~/admin-lte/css/skins/skin-green.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/scripts/DataTables")
                //.IncludeDirectory("~/Scripts/DataTables", "*.js", true));
                .Include("~/Scripts/DataTables/datatables.js"));

            bundles.Add(new StyleBundle("~/bundles/Content/DataTables")
                //.IncludeDirectory("~/Content/DataTables/css", "*.css", true));
                .Include("~/Scripts/DataTables/datatables.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
