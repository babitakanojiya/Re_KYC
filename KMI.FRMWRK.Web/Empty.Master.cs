using System;
using System.Web;
using KMI.FRMWRK.Security;
using KMI.FRMWRK.BAL;

namespace KMI.FRMWRK.Web
{
    public partial class Empty : System.Web.UI.MasterPage
    {

        #region Variable Declaration
        BAL.AuthorizationBAL objAuth;
        DAL.ErrorLog objErr;
        string strUserID = string.Empty;
        string strModuleId = string.Empty;
        string strAppId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        //protected void Page_Init(object sender, EventArgs e)
        //{
            
        //    try
        //    {
        //        objAuth = new BAL.AuthorizationBAL();

        //        if (Session["UserId"] == null)
        //        {
        //            Response.Redirect("~/ErrorSession.aspx");
        //        }
        //        else
        //        {
        //            strUserID = Session["UserId"].ToString();
        //        }

        //        if (Request.QueryString["ModuleId"] == null && Session["ModuleId"] == null)
        //        {
        //            Response.Redirect("~/ErrorSession.aspx");
        //        }
        //        else
        //        {
        //            Session["ModuleId"] = strModuleId = (Request.QueryString["ModuleId"] == null ? Session["ModuleId"].ToString() : Request.QueryString["ModuleId"].ToString());
        //            //strModuleId = Request.QueryString["ModuleId"].ToString();
        //        }

        //        if (Session["AppId"] != null)
        //        {
        //            strAppId = Session["AppId"].ToString();
        //        }


        //        if ((strUserID != null) && (strModuleId != null))
        //        {
        //            Boolean objboolAuth = objAuth.GetCheckAuthorization(strModuleId, strUserID);
        //            if (objboolAuth == false)
        //            {
        //                Response.Redirect("~/ErrorSession.aspx");
        //            }
        //        }
        //        else
        //        {
        //            Response.Redirect("~/ErrorSession.aspx");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        objErr = new DAL.ErrorLog();
        //        objErr.LogErr(Convert.ToInt32(strAppId), "Empty.Master.cs", "Page_Init", ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), strUserID, "USRMGMT");
        //        Response.Redirect("~/ErrorSession.aspx");
        //    }
        //    finally
        //    {
        //        objAuth = null;
        //    }
        //}

    }
}