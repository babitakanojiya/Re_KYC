using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace KMI.FRMWRK.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_End(object sender, EventArgs e)
        {
            // Code that runs on application shutdown
        }

        void Application_Error(object sender, EventArgs e)
        {
            if (HttpContext.Current.AllErrors.GetLength(0) > 0)
            {
                Exception[] oAllErrors = HttpContext.Current.AllErrors;
                HttpContext.Current.ClearError();

                string sErrURL = null;

                for (int i = 0; i < oAllErrors.GetLength(0); i++)
                {

                    string sStackTrace = null;

                    string[] sArrStackTrace = oAllErrors[i].GetBaseException().StackTrace.Split(Environment.NewLine.ToCharArray());
                    for (int iST = 0; iST < sArrStackTrace.GetLength(0); iST++)
                    {
                        if (sArrStackTrace[iST].Contains(":line "))
                            sStackTrace += Environment.NewLine + sArrStackTrace[iST];
                    }

                    string sErrMsg = oAllErrors[i].GetBaseException().Message;

                    string sTypeName = oAllErrors[i].GetBaseException().TargetSite.DeclaringType.ToString() + " - " + oAllErrors[i].GetBaseException().TargetSite.ToString();
                }

            }
        }

        void Session_Start(object sender, EventArgs e)
        {


          
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }
    }
}