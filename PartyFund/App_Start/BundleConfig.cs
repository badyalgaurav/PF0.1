using System.Web;
using System.Web.Optimization;

namespace PartyFund
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.unobtrusive*",
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //for  scripts
            bundles.Add(new ScriptBundle("~/Content/Vendor/JSFiles").Include(
                //vendor script
                       "~/Content/vendor/jquery/dist/jqueryMin.js",
                       "~/Content/vendor/jquery-validation/jqueryValidateMin.js",
                       "~/Content/vendor/jquery-ui/jqueryUiMin.js",
                       "~/Content/vendor/slimScroll/jquerySlimscrollMin.js",
                       "~/Content/vendor/bootstrap/dist/js/bootstrapMin.js",
                       "~/Content/vendor/metisMenu/dist/metisMenu.js",
                       "~/Content/vendor/iCheck/icheckMin.js",
                       "~/Content/vendor/sparkline/index.js",
                       "~/Content/vendor/datatables/media/js/jquery.dataTablesMin.js",
                       "~/Content/vendor/datatables_plugins/integration/bootstrap/3/dataTables.bootstrapMin.js",
                       "~/Content/vendor/toastr/build/toastrMin.js",
                //toasterOptions(manually options)
                       "~/Content/scripts/toastrOptions.js",
                //App Script
                       "~/Content/scripts/homer.js"
                       ));
            //For  css
            bundles.Add(new StyleBundle("~/Content/vendor/css").Include(
                //Vendor Style
                "~/Content/vendor/fontawesome/css/font-awesome.css",
                "~/Content/vendor/metisMenu/dist/metisMenu.css",
                "~/Content/vendor/animate.css/animate.css",
                "~/Content/vendor/bootstrap/dist/css/bootstrap.css",
                "~/Content/vendor/sweetalert/lib/sweet-alert.css",
                "~/Content/vendor/toastr/build/toastrMin.css",
                "~/Content/vendor/datatables_plugins/integration/bootstrap/3/dataTables.bootstrap.css",
                //App Style
                "~/Content/fonts/pe-icon-7-stroke/css/pe-icon-7-stroke.css",
                "~/Content/fonts/pe-icon-7-stroke/css/helper.css",
                "~/Content/styles/style.css"
                ));



        }
    }
}