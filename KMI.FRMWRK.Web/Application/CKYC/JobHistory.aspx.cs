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
using System.Globalization;
using System.Linq;
using System.IO;
using System.Configuration;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class JobHistory : System.Web.UI.Page 
    {
        DataTable dt;
        Hashtable hTable = new Hashtable();
        ErrorLog objErr = new ErrorLog();
        DataAccessLayer dataAccessLayer;
        protected void Page_Load(object sender, EventArgs e)
        {
              
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            Hashtable ht = new Hashtable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            string JobName;
            JobName = ddlJobType.SelectedValue.ToString();
            ht.Add("@Flag", JobName);
            dt = dataAccessLayer.GetDataTable("prc_getJobHistory", ht);
            if (dt != null)
            {
                dgView.DataSource = dt;
                JobDiv.Attributes.Add("style", "display:block;");
                dgView.DataBind();
            }
            else
            {
                JobDiv.Attributes.Add("style", "display:none;");
                trRecord.Visible = true;
                lblMessage.Text = "No Record Found";
                lblMessage.Visible = true;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            JobDiv.Attributes.Add("style", "display:none;");
            ddlJobType.ClearSelection();
        }

        protected void dgView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dt = GetdataforJobHistory();
                DataView dv = new DataView(dt);
                GridView dgSource = (GridView)sender;
                dgSource.PageIndex = e.NewPageIndex;
                dgSource.DataSource = dv;
                dgSource.DataBind();
                ShowPageInformation();

            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "JobHistory.aspx.cs", "dgView_PageIndexChanging", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dt = null;
            }
        }
       protected DataTable GetdataforJobHistory()
        {
            dt = new DataTable();
            Hashtable ht = new Hashtable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            try
            {
                string JobName;
                JobName = ddlJobType.SelectedValue.ToString();
                ht.Add("@Flag", JobName);
                dt = dataAccessLayer.GetDataTable("prc_getJobHistory", ht);
                dgView.DataSource = dt;
                JobDiv.Attributes.Add("style", "display:block;");
                dgView.DataBind();
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "JodHistory.aspx.cs", "GetdataforJobHistory", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;

            }
            return dt;
        }
        private void ShowPageInformation()
        {
            try
            {
                int intPageIndex = dgView.PageIndex + 1;
                lblPageInfo.Text = "Page " + intPageIndex.ToString() + " of " + dgView.PageCount;
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "JobHistory.aspx.cs", "ShowPageInformation", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

    }
}