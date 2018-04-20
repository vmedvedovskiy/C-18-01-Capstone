using System.Web;
using System.Web.Optimization;

namespace C_18_01_Capstone.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/register").Include(
                        "~/Scripts/register.js"));


            bundles.Add(new ScriptBundle("~/bundles/jquery-validation").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                              "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/bundles/login").Include(
                            "~/Content/login.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}