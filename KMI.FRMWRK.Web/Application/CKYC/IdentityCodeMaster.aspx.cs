using System;
using KMI.FRMWRK.DAL;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KMI.FRMWRK.Multilingual;
using System.Data.SqlClient;
using KMI.FRMWRK.Web.Application.CKYC.Common;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class IdentityCodeMaster : System.Web.UI.Page
    {
        Hashtable hTable = new Hashtable();
        DataAccessLayer objDAL;
        DataTable dt;
        int AppID;
        ErrorLog objErr;
        string UserID = string.Empty;
        string CustType = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["CustType"] != null)
                {
                    CustType = Request.QueryString["CustType"].ToString();//added by shubham
                }
            }
            catch (Exception ex)
            { }
            BindCandidateMvmt();
        }

        #region BindCandidateMvmt
        private void BindCandidateMvmt()
        {
            try
            {
                hTable.Clear();
                objDAL = new DataAccessLayer("CKYCConnectionString");
                if (CustType == "02")
                {
                    hTable.Add("@LookupCode", "KEntPoI");//02
                }
                else
                {
                    hTable.Add("@LookupCode", "KId");//01
                }
                dt = objDAL.GetDataTable("Prc_GetIdentity_Code_Master", hTable);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        gvIdentityCodeMaster.DataSource = dt;
                        gvIdentityCodeMaster.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr = new ErrorLog();
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;

            }
        }
        #endregion
    }
}