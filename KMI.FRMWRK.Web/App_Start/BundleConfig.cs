using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace KMI.FRMWRK.Web
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

            #region Framework stuff here - added by kalpak 

            bundles.Add(new StyleBundle("~/bundles/FRMWRKcss").Include(
                  "~/Content/Bootstrap/css/bootstrap.min.css",
                  "~/Content/Bootstrap/css/font-awesome.min.css",
                  "~/Content/Bootstrap/css/animate.min.css",
                  "~/Content/Bootstrap/css/bootstrap-datetime.css",
                  "~/Content/kmi.framework.css"));

            bundles.Add(new ScriptBundle("~/bundles/FRMWRKjs").Include(
                  "~/Content/Bootstrap/js/jquery-3.2.1.js",
                   "~/Content/Bootstrap/js/bootstrap.min.js",   
                   "~/Scripts/kmi.common.js",
                   "~/Content/Bootstrap/js/bootstrap-datepicker.min.1.4.1.js",
                   "~/Scripts/kmi.framework.js"));

            #endregion

            #region CKYC stuff here - added by Pravin 
            bundles.Add(new ScriptBundle("~/bundles/CKYCValidationjs").Include(
                   "~/Content/Bootstrap/js/jquery-ui.js",
                   "~/Application/CKYC/Common/Scripts/kmi.validation.js",
                   "~/Content/Bootstrap/js/footable.js"
                   ));

            bundles.Add(new StyleBundle("~/bundles/CKYCcss").Include(
                "~/Content/Bootstrap/css/jquery-ui.css",
                "~/Content/Bootstrap/css/footable.min.css",
                "~/Application/CKYC/Common/CSS/CKYC.css"
                  ));
            #endregion

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