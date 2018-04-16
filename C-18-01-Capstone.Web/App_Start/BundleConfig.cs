using System.Web;
using System.Web.Optimization;

namespace C_18_01_Capstone.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/jquery-{version}.min.js",
                        "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}