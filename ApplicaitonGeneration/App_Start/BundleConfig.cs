using System.Web.Optimization;

namespace ApplicationGeneration
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/javascript/jquery-{version}.js"
                        //"~/bower_components/react/react.js",
                        //"~/bower_components/react/react-dom.js",
                        //"~/bower_components/react/react-dom-server.js",
                        //"~/bower_components/react/react-with-addons.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/javascript/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/javascript/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      //"~/bower_components/react-bootstrap/react-bootstrap.js",
                      "~/Scripts/javascript/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/bundles/main").Include(
            //        "~/Scripts/reactjs/document.jsx"));
        }
    }
}
