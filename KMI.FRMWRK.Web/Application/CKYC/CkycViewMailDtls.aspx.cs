using KMI.FRMWRK.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class CkycViewMailDtls : System.Web.UI.Page
    {
        private string UserID = "";
        Hashtable objht = new Hashtable();
        DataSet ds = new DataSet();
        DataAccessLayer objDAL;
        string strAlertMsg = string.Empty;
        string strMailSMSFormatDtl = string.Empty;
        StringBuilder sbContent = new StringBuilder();
        StringBuilder sbSMSContent = new StringBuilder();
        StringBuilder sbMailContent = new StringBuilder();
        DataTable objDt = new DataTable();
        string strContent = string.Empty;








        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                UserID = Session["UserId"].ToString();

                ViewState["strCkycNo"] = Request.QueryString["Ckyc"].ToString(); ;

                objht.Clear();
                objDt.Clear();
                objht.Add("@CkycNo", Request.QueryString["Ckyc"].ToString());
                objDAL = new DataAccessLayer("CBFRMWRKonnectionString");
                ds = objDAL.GetDataSet("Prc_CBFRM_CBMail", objht, "CBFRMWRKonnectionString");
                if ( Request.QueryString["Flag"]=="KIN")
                {

                    Byte[] FileBuffer = (byte[])ds.Tables[0].Rows[0]["KINCard"];
                    if (FileBuffer != null)
                    {
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-length", FileBuffer.Length.ToString());
                        Response.BinaryWrite(FileBuffer);
                        divKIN.InnerHtml = $"<img src='data:image/png;base64,{FileBuffer}'/>";

                    }



                }
                else
                {
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dvemail.Style.Add("display", "block");
                            divMailDtlContainer.Style.Add("display", "block");
                           // dvsendbtn.Style.Add("display", "block");
                            //dvsendsmsbtn.Style.Add("display", "none");
                           // DVWhatsApp.Style.Add("display", "none");
                            //dvsms.Style.Add("display", "none");
                            //lblsrcntdtl.Text = "Total Matching Records :" + ds.Tables[1].Rows[0]["COUNTOFSR"].ToString();
                            gvmail.DataSource = ds;
                            gvmail.DataBind();
                            Session["GridDataSet"] = ds;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('No record found.');window.close();", true);
                            dvemail.Style.Add("display", "none");
                            //dvsendbtn.Style.Add("display", "none");
                            //dvsms.Style.Add("display", "none");
                            //dvsendsmsbtn.Style.Add("display", "none");
                           // divMailDtlContainer.Style.Add("display", "block");
                            Session["GridDataSet"] = null;

                        }


                }


            }





            }

        protected void gvmail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HtmlGenericControl divMailAttachment = e.Row.FindControl("divMailAttachment") as HtmlGenericControl;
                HtmlGenericControl div = e.Row.FindControl("divmailcontent") as HtmlGenericControl;
                Label lbl = e.Row.FindControl("lblmsgrefno") as Label;
                DataRow[] dr = ds.Tables[0].Select("CkycNo='" + lbl.Text + "'");
                foreach (DataRow row in dr)
                {
                    div.InnerHtml = "<html>" + "<head>" + "<body>" + row["Content"].ToString() + "<body>" + "<head>" + "<html>";
                }
            }
            }
    }

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/HomePage.aspx");

        //}
    }
