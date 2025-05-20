using System;
using System.Web;

namespace KMI.FRMWRK.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Constructor)]
    public class AppActivityAttribute: Attribute
    {
        public string AppID { get; set; }
        public string AppName { get; set; }
        public string ModuleCode { get; set; }
        public string PageName { get; set; }
        public string FuncName { get; set; }

        public AppActivityAttribute(string appID)
        {
            HttpContext.Current.Request.Params.Add("AppID", appID);
            HttpContext.Current.Request.Params.Add("AppName", AppName);
            HttpContext.Current.Request.Params.Add("ModuleCode", ModuleCode);
            HttpContext.Current.Request.Params.Add("PageName", PageName);
            HttpContext.Current.Request.Params.Add("FuncName", FuncName);
        }

    }
}