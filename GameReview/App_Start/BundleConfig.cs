using System.Web;
using System.Web.Optimization;

namespace GameReview
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                "~/Scripts/Slick/slick.js",
                "~/Scripts/Datatables/jquery.dataTables.js",
                "~/Scripts/Datatables/dataTables.bootstrap4.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Slick/slick.css",
                      "~/Content/Slick/slick-theme.css",
                      "~/Content/Datatables/css/datatables.bootstrap4.css",
                      "~/Content/site.css"));
        }
    }
}
