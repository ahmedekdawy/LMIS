using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace LMIS.Portal
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                            "~/Scripts/WebForms/WebForms.js",
                            "~/Scripts/WebForms/WebUIValidation.js",
                            "~/Scripts/WebForms/MenuStandards.js",
                            "~/Scripts/WebForms/Focus.js",
                            "~/Scripts/WebForms/GridView.js",
                            "~/Scripts/WebForms/DetailsView.js",
                            "~/Scripts/WebForms/TreeView.js",
                            "~/Scripts/WebForms/WebParts.js"));

            // Order is very important for these files to work, they have explicit dependencies
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/inputmask").Include(
                    "~/Scripts/jquery.inputmask/jquery.inputmask.js",
                    "~/Scripts/jquery.inputmask/jquery.inputmask.extensions.js",
                    "~/Scripts/jquery.inputmask/jquery.inputmask.date.extensions.js",
                    "~/Scripts/jquery.inputmask/jquery.inputmask.regex.extensions.js",
                    "~/Scripts/jquery.inputmask/jquery.inputmask.phone.extensions.js",
                    "~/Scripts/jquery.inputmask/jquery.inputmask.numeric.extensions.js"));

            bundles.Add(new ScriptBundle("~/bundles/noty").Include(
                    "~/Scripts/noty/jquery.noty.js",
                    "~/Scripts/noty/themes/default.js",
                    "~/Scripts/noty/themes/bootstrap.js",
                    "~/Scripts/noty/themes/spinner.js",
                    "~/Scripts/noty/layouts/top.js",
                    "~/Scripts/noty/layouts/center.js",
                    "~/Scripts/noty/layouts/inline.js"));

            ScriptManager.ScriptResourceMapping.AddDefinition(
                "respond",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/respond.min.js",
                    DebugPath = "~/Scripts/respond.js",
                });
        }
    }
}