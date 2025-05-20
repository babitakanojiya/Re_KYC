using System;
using KMI.FRMWRK.DAL;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using KMI.FRMWRK.Multilingual;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Configuration;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class CommunicationLog_NEW : System.Web.UI.Page
    {

        DataSet dsMailDtls = new DataSet();
        DataSet dsMailattachment = new DataSet();
        Hashtable htable = new Hashtable();
        DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
        ErrorLog objErr = new ErrorLog();
        //CBCommunication.SendMailSMS objMailSMS = new CBCommunication.SendMailSMS();
        ///CBCommunication.SendMailSMS objsms = new CBCommunication.SendMailSMS();
        public string strMailRefNo = string.Empty;
        public string strSMSRefNo = string.Empty;
        public const string Constr = "CKYCConnectionString";
        string FilePath = string.Empty;
        ArrayList arrlist = new ArrayList();
        private string UserID = "";
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                UserID = Session["UserId"].ToString();
            }
            txtSrNo.Text = Request.QueryString["refno"].ToString();
        }

        public void btnSearch_click(object sender, EventArgs e)
        {
            //string SrNo = txtSrNo.Text;
            // txtSrNo.Text = Request.QueryString["SrvcReqHrdCode"].ToString();
            string SrNo = Request.QueryString["refno"];

            try
            {
                if (ddltypecomm.SelectedValue.Equals("1"))
                {

                    htable.Clear();
                    htable.Add("@SRNo", txtSrNo.Text);
                    htable.Add("@commtype", ddltypecomm.SelectedValue);
                    dsMailDtls = null;
                    dsMailDtls = objDAL.GetDataSet("Prc_GetMaildata_New", htable);
                    if (dsMailDtls.Tables.Count > 0)
                        if (dsMailDtls.Tables[0].Rows.Count > 0)
                        {
                            dvemail.Style.Add("display", "block");
                            divMailDtlContainer.Style.Add("display", "block");
                            dvsendbtn.Style.Add("display", "none");
                            dvsendsmsbtn.Style.Add("display", "none");
                            DVWhatsApp.Style.Add("display", "none");
                            dvsms.Style.Add("display", "none");
                            //                            lblsrcntdtl.Text = "Total Matching Records :" + dsMailDtls.Tables[1].Rows[0]["COUNTOFSR"].ToString();
                            gvmail.DataSource = dsMailDtls;
                            gvmail.DataBind();
                            Session["GridDataSet"] = dsMailDtls;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('No record found.');", true);
                            dvemail.Style.Add("display", "none");
                            dvsendbtn.Style.Add("display", "none");
                            dvsms.Style.Add("display", "none");
                            dvsendsmsbtn.Style.Add("display", "none");
                            divMailDtlContainer.Style.Add("display", "block");
                            Session["GridDataSet"] = null;

                        }


                }
                //Added by tushar fro wnatsApp 
                else if (ddltypecomm.SelectedValue.Equals("2"))
                {

                    htable.Clear();
                    htable.Add("@SRNo", txtSrNo.Text);
                    htable.Add("@commtype", ddltypecomm.SelectedValue);
                    dsMailDtls = null;
                    dsMailDtls = objDAL.GetDataSet("Prc_GetMaildata_New", htable);

                    if (dsMailDtls.Tables.Count > 0)
                        if (dsMailDtls.Tables[0].Rows.Count > 0)
                        {
                            dvemail.Style.Add("display", "none");
                            dvsendbtn.Style.Add("display", "none");
                            dvsms.Style.Add("display", "block");
                            divsms.Style.Add("display", "block");
                            DVWhatsApp.Style.Add("display", "none");
                            dvsendsmsbtn.Style.Add("display", "none");
                            //                          lblsmsconunt.Text = "Total Matching Records :" + dsMailDtls.Tables[1].Rows[0]["COUNTOFSR"].ToString();
                            grdsms.DataSource = dsMailDtls;
                            grdsms.DataBind();
                            Session["GridDataSet"] = dsMailDtls;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('No record found.');", true);
                            dvemail.Style.Add("display", "none");
                            dvsendbtn.Style.Add("display", "none");
                            dvsms.Style.Add("display", "none");
                            dvsendsmsbtn.Style.Add("display", "none");
                            divsms.Style.Add("display", "none");
                            Session["GridDataSet"] = null;


                        }

                }
                //Added by tushar for whatsapp
                else
                {

                    htable.Clear();
                    htable.Add("@SRNo", txtSrNo.Text);
                    htable.Add("@commtype", ddltypecomm.SelectedValue);
                    dsMailDtls = null;
                    dsMailDtls = objDAL.GetDataSet("Prc_GetMaildata_New", htable);

                    if (dsMailDtls.Tables.Count > 0)
                        if (dsMailDtls.Tables[0].Rows.Count > 0)
                        {
                            DVWhatsApp.Style.Add("display", "block");
                            DivWhatsApp.Style.Add("display", "block");
                            dvsendsmsbtn.Style.Add("display", "none");
                            dvsendbtn.Style.Add("display", "none");
                            dvemail.Style.Add("display", "none");
                            dvsms.Style.Add("display", "none");
                            Label3.Text = "Total Matching Records :" + dsMailDtls.Tables[1].Rows[0]["COUNTOFSR"].ToString();
                            GRDWhatsApp.DataSource = dsMailDtls;
                            GRDWhatsApp.DataBind();
                            Session["GridDataSet"] = dsMailDtls;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('No record found.');", true);
                            dvemail.Style.Add("display", "none");
                            dvsendbtn.Style.Add("display", "none");
                            dvsms.Style.Add("display", "none");
                            DVWhatsApp.Style.Add("display", "none");
                            dvsendsmsbtn.Style.Add("display", "none");
                            Session["GridDataSet"] = null;


                        }

                }
                //Ended by tushar  for whatsapp
            }
            catch (Exception ex)
            {

                objErr.LogErr(1, "CommunicationLogger.aspx.cs", "btnSearch_click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                throw ex;
            }

        }

        #region Email

        public void gvmail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //HtmlGenericControl div = e.Row.FindControl("divmailcontent") as HtmlGenericControl;
                    //HtmlGenericControl divMailAttachment = e.Row.FindControl("divMailAttachment") as HtmlGenericControl;
                    //div.InnerHtml = "<html>" + "<head>" + "<body>" + dsMailDtls.Tables[0].Rows[e.Row.RowIndex]["MailContent"].ToString() + "</body>" + "</head>" + "</html>";

                    HtmlGenericControl divMailAttachment = e.Row.FindControl("divMailAttachment") as HtmlGenericControl;
                    HtmlGenericControl div = e.Row.FindControl("divmailcontent") as HtmlGenericControl;
                    Label lbl = e.Row.FindControl("lblmsgrefno") as Label;
                    DataRow[] dr = dsMailDtls.Tables[0].Select("MailRefNo='" + lbl.Text + "'");
                    foreach (DataRow row in dr)
                    {
                        div.InnerHtml = "<html>" + "<head>" + "<body>" + row["MailContent"].ToString() + "<body>" + "<head>" + "<html>";
                    }

                    if (dsMailDtls.Tables[0].Rows.Count > 0)
                    {
                        //for (var i = 0; i <= dsMailDtls.Tables[0].Rows.Count - 1; i++)
                        //{
                        htable.Clear();
                        htable.Add("@MsgID", dsMailDtls.Tables[0].Rows[e.Row.RowIndex]["MailRefNo"].ToString());
                        //htable.Add("@SRNo", txtSrNo.Text);
                        htable.Add("@ModuleID", "");
                        htable.Add("@AppID", "1");
                        htable.Add("@MstrModuleCode", "7000");
                        htable.Add("@UserId", "");
                        dt = objDAL.GetDataTable("Prc_GetMailAttach", htable);

                        if (dsMailattachment.Tables.Count > 0)
                        {
                            if (dsMailattachment.Tables[0].Rows.Count > 0)
                            {
                                var OutString = "<table id='tblMailBody'>";
                                for (var j = 0; j <= dsMailattachment.Tables[0].Rows.Count - 1; j++)
                                {
                                    //var XMLExtAttach = dsMailattachment.Tables[0].Rows[j]["FilePath"].ToString();
                                    OutString += "<tr><td class='formcontentbc' align='left' style='width:100%;'>";
                                    OutString += dsMailattachment.Tables[0].Rows[j]["FilePath"].ToString();
                                    OutString += "</td></tr>";
                                }
                                OutString += "</table>";
                                divMailAttachment.InnerHtml = "<html>" + "<head>" + "<body>" + OutString + "</body>" + "</head>" + "</html>";


                            }
                        }
                        //}
                    }


                }
            }
            catch (Exception ex)
            {

                objErr.LogErr(1, "CommunicationLogger.aspx.cs", "gvmail_RowDataBound", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                throw ex;
            }

        }

        // To Resend Email added by Ashish
        public void btnresendemail_Click(object sender, EventArgs e)
        {
            int count = 0;
            try
            {
                GridView grd = (GridView)FindControl("gvmail");
                foreach (GridViewRow gvsrno in grd.Rows)
                {
                    CheckBox chk = (CheckBox)gvsrno.FindControl("chkSendMail");
                    if (chk != null && chk.Checked == true)
                    {
                        Label lblmsgrefno = (Label)gvsrno.FindControl("lblmsgrefno");
                        Label lblmsgtype = (Label)gvsrno.FindControl("lblmsgtype");
                        Label lblsenddate = (Label)gvsrno.FindControl("lblsenddate");
                        Label lbltomail = (Label)gvsrno.FindControl("lbltomail");
                        Label lblfrommail = (Label)gvsrno.FindControl("lblfrommail");
                        Label lblcc = (Label)gvsrno.FindControl("lblcc");
                        Label lblbcc = (Label)gvsrno.FindControl("lblbcc");
                        Label lblsubject = (Label)gvsrno.FindControl("lblsubject");
                        HtmlGenericControl div = (HtmlGenericControl)gvsrno.FindControl("divmailcontent");

                        string refno = lblmsgrefno.Text;
                        string msgtype = lblmsgtype.Text;
                        string senddate = lblsenddate.Text;
                        string tomail = lbltomail.Text;
                        string frommail = lblfrommail.Text;
                        string cc = lblcc.Text;
                        string bcc = lblbcc.Text;
                        string subject = lblsubject.Text;
                        ArrayList arrlist = new ArrayList();

                        htable.Clear();
                        htable.Add("@MsgID", refno);
                        htable.Add("@ModuleID", "");
                        htable.Add("@AppID", "1");
                        htable.Add("@MstrModuleCode", "7000");
                        htable.Add("@UserId", "");
                        dt = objDAL.GetDataTable("Prc_GetMailAttach", htable);
                        if (dsMailattachment.Tables.Count > 0)
                        {
                            if (dsMailattachment.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i <= dsMailattachment.Tables[0].Rows.Count - 1; i++)
                                {
                                    FilePath = dsMailattachment.Tables[0].Rows[i]["FilePath"].ToString();
                                    arrlist.Add(FilePath);
                                }
                            }
                        }

                        //added by prity for AR81
                        //                    strMailRefNo = objMailSMS.ReSendMail("1", "", "", "", frommail, tomail, cc, bcc, subject, div.InnerHtml, Session["UserID"].ToString(), "", arrlist, txtSrNo.Text, refno, "65"); commented by rutuja
                        count++;
                        if (!string.IsNullOrEmpty(strMailRefNo) && count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Mail Send Successfully.');", true);
                            CheckBox chk1 = (CheckBox)gvsrno.FindControl("chkSendMail");
                            chk1.Checked = false;

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Mail not send.Please try again.');", true);
                        }

                    }
                }
                if (count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Please select mails to send.');", true);
                }
            }
            catch (Exception ex)
            {

                objErr.LogErr(1, "CommunicationLogger.aspx.cs", "btnresendemail_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                throw ex;
            }


        }

        public void gvmail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvmail.PageIndex = e.NewPageIndex;
                dsMailDtls = null;
                dsMailDtls = (DataSet)(Session["GridDataSet"]);
                gvmail.DataSource = dsMailDtls;
                gvmail.DataBind();
            }

            catch (Exception ex)
            {

                objErr.LogErr(1, "CommunicationLogger.aspx.cs", "gvmail_PageIndexChanging", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                throw ex;
            }
        }

        #endregion  Email  

        #region Send SMS

        public void grdsms_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    //HtmlGenericControl div = e.Row.FindControl("divsmscontent") as HtmlGenericControl;
                    //div.InnerHtml = "<html>" + "<head>" + "<body>" + dsMailDtls.Tables[0].Rows[e.Row.RowIndex]["SMSContent"].ToString() + "<body>" + "<head>" + "<html>";

                    HtmlGenericControl div = e.Row.FindControl("divsmscontent") as HtmlGenericControl;
                    Label lbl = e.Row.FindControl("lblsmsgrefno") as Label;
                    DataRow[] dr = dsMailDtls.Tables[0].Select("SMSRefNo='" + lbl.Text + "'");
                    foreach (DataRow row in dr)
                    {
                        div.InnerHtml = "<html>" + "<head>" + "<body>" + row["SMSContent"].ToString() + "<body>" + "<head>" + "<html>";
                    }
                }
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "CommunicationLogger.aspx.cs", "grdsms_RowDataBound", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                throw ex;
            }
        }

        // TO Resend SMS Added By Ashish
        protected void btnsendsms_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                GridView grdsms = (GridView)FindControl("grdsms");
                foreach (GridViewRow gvsrno in grdsms.Rows)
                {
                    CheckBox chk = (CheckBox)gvsrno.FindControl("chkSendSMS");
                    if (chk != null && chk.Checked == true)
                    {

                        Label lblsmsgrefno = (Label)gvsrno.FindControl("lblsmsgrefno");
                        Label lblMsgtype = (Label)gvsrno.FindControl("lblMsgtype");
                        Label lblsmssenddate = (Label)gvsrno.FindControl("lblsmssenddate");
                        Label lblsmsto = (Label)gvsrno.FindControl("lblsmsto");
                        HtmlGenericControl div = (HtmlGenericControl)gvsrno.FindControl("divsmscontent");

                        string smsrefno = lblsmsgrefno.Text;
                        string Msgtype = lblMsgtype.Text;
                        string smssenddate = lblsmssenddate.Text;
                        string smsto = lblsmsto.Text;
                        string smscontent = div.InnerHtml;
                        string user = Session["UserID"].ToString();
                        ArrayList arrlist = new ArrayList();
                        smscontent = smscontent.Replace("<html><head><body>", "");
                        smscontent = smscontent.Replace("<body><head><html>", "");

                        //strSMSRefNo = objsms.ReSendSMS("CRM", smsrefno, "", smsto, smscontent, Session["UserID"].ToString(), txtSrNo.Text, smsrefno, "65"); commented by rutuja
                        count++;
                        if (!string.IsNullOrEmpty(strSMSRefNo) && count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('SMS Send Successfully.');", true);
                            CheckBox chk1 = (CheckBox)gvsrno.FindControl("chkSendSMS");
                            chk1.Checked = false;

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('SMS not send.Please try again.');", true);
                        }

                    }

                }
                if (count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Please select SMS to send.');", true);
                }
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "CommunicationLogger.aspx.cs", "btnsendsms_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                throw ex;
            }
        }

        public void grdsms_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdsms.PageIndex = e.NewPageIndex;
                dsMailDtls = null;
                dsMailDtls = (DataSet)(Session["GridDataSet"]);
                grdsms.DataSource = dsMailDtls;
                grdsms.DataBind();
            }

            catch (Exception ex)
            {

                objErr.LogErr(1, "CommunicationLogger.aspx.cs", "grdsms_PageIndexChanging", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                throw ex;
            }
        }

        #endregion Send SMS

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage.aspx");

        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            // Added BY ashish on 21st march 2016 
            ddltypecomm.ClearSelection();

            //  Response.Redirect("~/CommunicationLogger.aspx");
        }
    }
}