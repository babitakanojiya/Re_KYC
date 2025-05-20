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

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class ExecutionHistory : System.Web.UI.Page
    {
        DataTable dt;
        Hashtable hTable = new Hashtable();
        ErrorLog objErr = new ErrorLog();
        DataAccessLayer dataAccessLayer;
        private string strAppID;
        private string strModuleID;
        private string PageFlag;
        private MultilingualManager olng;
        private string strUserLang;
        string strHtml = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"] == null)
                {
                    Response.Redirect("~/ErrorSession.aspx", true);
                }

                if (HttpContext.Current.Session["UserId"] == null)
                {
                    Response.Redirect("~/ErrorSession.aspx", true);
                }
                if (Session["AppID"] != null)
                {
                    strAppID = Session["AppID"].ToString();
                }
                if (Session["ModuleID"] != null)
                {
                    strModuleID = Session["ModuleID"].ToString();
                }
                if (Request.QueryString["FLAG"] != null)
                {
                    PageFlag = Request.QueryString["FLAG"].ToString();
                }
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                strUserLang = HttpContext.Current.Session["UserLangNum"].ToString();
                if (!IsPostBack)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "ShowProgressBar('Loading..Please wait');", true);
                    //System.Threading.Thread.Sleep(5000);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "HideProgressBar();", true);
                    ddlActType.SelectedIndex = 0;
                    rdoState.SelectedIndex = 0;
                    ddlActType_SelectedIndexChanged(null, null);
                    if (ddlActType.SelectedIndex == 0)
                    {
                        bindGridForFiNo(1, "");
                    }
                    else
                    {
                        bindGridForBatchID(1, "", "");
                    }
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "HideProgressBar();", true);
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CkycSearch.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        public DataTable GetDataSetForBatchID(int flag, string batchid, string refno)
        {
            dt = new DataTable();
            Hashtable ht = new Hashtable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            try
            {
                ht.Add("@flag", flag);
                ht.Add("@BatchID", batchid);
                ht.Add("@FiRefNo", refno);
                ht.Add("@startdate", txtStartDate.Text);
                ht.Add("@enddate", txtEndDate.Text);
                ht.Add("@mode", rdoState.SelectedValue);
                dt = dataAccessLayer.GetDataTable("PRC_GetActivityDataByBatchID", ht);
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "ExecutionHistory.aspx.cs", "GetDataSetForBatchID", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
            return dt;
        }

        public DataTable GetDataSetForFiNo(int flag, string refno)
        {
            dt = new DataTable();
            Hashtable ht = new Hashtable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            try
            {
                ht.Add("@flag", flag);
                ht.Add("@FiRefNo", refno);
                ht.Add("@startdate", txtStartDate.Text);
                ht.Add("@enddate", txtEndDate.Text);
                ht.Add("@mode", rdoState.SelectedValue);
                ht.Add("@PageFlag", PageFlag);
                dt = dataAccessLayer.GetDataTable("PRC_GetActivityDataByRefNo", ht);
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "ExecutionHistory.aspx.cs", "GetDataSetForFiNo", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
            return dt;
        }

        public void bindGridForFiNo(int flag, string refno)
        {
            dt = new DataTable();
            try
            {
                dt = GetDataSetForFiNo(flag, refno);
                if (dt.Rows.Count > 0)
                {
                    dgView.Visible = true;
                    dgBatchView.Visible = false;
                    trDgViewDtl.Visible = true;
                    trRecord.Visible = false;
                    //lblMessage.Text = "0 Record Found";
                    lblMessage.Visible = false;
                }
                else
                {
                    dgView.Visible = false;
                    dgBatchView.Visible = false;
                    trDgViewDtl.Visible = false;
                    trRecord.Visible = true;
                    lblMessage.Text = "0 Record Found";
                    lblMessage.Visible = true;
                }
                ViewState["Data"] = dt;
                dgView.DataSource = dt;
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
                    objErr.LogErr(1, "ExecutionHistory.aspx.cs", "bindGrid", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        public void bindGridForBatchID(int flag, string batchID, string refno)
        {
            dt = new DataTable();
            try
            {
                dt = GetDataSetForBatchID(flag, batchID, refno);
                if (dt.Rows.Count > 0)
                {
                    dgView.Visible = false;
                    dgBatchView.Visible = true;
                    trDgViewDtl.Visible = true;
                    trRecord.Visible = false;
                    //lblMessage.Text = "0 Record Found";
                    lblMessage.Visible = false;
                }
                else
                {
                    dgView.Visible = false;
                    dgBatchView.Visible = false;
                    trDgViewDtl.Visible = false;
                    trRecord.Visible = true;
                    lblMessage.Text = "0 Record Found";
                    lblMessage.Visible = true;
                }
                ViewState["Data"] = dt;
                dgBatchView.DataSource = dt;
                dgBatchView.DataBind();
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "ExecutionHistory.aspx.cs", "bindGrid", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        protected void dgView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            try
            {
               
                dt = (DataTable)ViewState["Data"];
                DataView dv = new DataView(dt);
                dgView.DataSource = dt;
                GridView dgSource = (GridView)sender;
                dgView.PageIndex = e.NewPageIndex;
                dgView.DataSource = dv;
                dgView.DataBind();
                ShowPageInformationForFirefNo();
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "ExecutionHistory.aspx.cs", "bindGrid", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        protected void dgView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridView InnerGridView = (GridView)((e.Row) as GridViewRow).FindControl("dgInnerDiv");
                    dt = GetDataSetForFiNo(2, dgView.DataKeys[e.Row.RowIndex][0].ToString());
                    InnerGridView.DataSource = dt;
                    InnerGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "ExecutionHistory.aspx.cs", "bindGrid", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        protected void ddlActType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSearchText.InnerText = ddlActType.SelectedValue == "1" ? "FI Ref. No." : "Batch ID";
            txtSearchNo.Text = "";
        }

        protected void dgBatchView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            try
            {
                dt = (DataTable)ViewState["Data"];
                DataView dv = new DataView(dt);
                dgBatchView.DataSource = dt;
                GridView dgSource = (GridView)sender;
                dgBatchView.PageIndex = e.NewPageIndex;
                dgBatchView.DataSource = dv;
                dgBatchView.DataBind();
                ShowPageInformation();
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "ExecutionHistory.aspx.cs", "bindGrid", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        protected void dgBatchView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridView InnerGridView = (GridView)((e.Row) as GridViewRow).FindControl("dgLevel1View");
                    dt = GetDataSetForBatchID(1, dgBatchView.DataKeys[e.Row.RowIndex][0].ToString(), "");
                    InnerGridView.DataSource = dt;
                    InnerGridView.DataBind();

                    string actType = dgBatchView.DataKeys[e.Row.RowIndex][2].ToString();
                    if (actType.Trim().ToLower() == "batch uploaded")
                    {
                        (((e.Row) as GridViewRow).FindControl("btnViewReg") as LinkButton).Visible = false;
                        (((e.Row) as GridViewRow).FindControl("divContainer") as Control).Visible = false;
                    }
                    if (actType.Trim().ToLower() == "batch validated")
                    {
                        string StatusFS = dgBatchView.DataKeys[e.Row.RowIndex][3].ToString();
                        if (StatusFS.Trim().ToLower() == "fail")
                        {
                            (((e.Row) as GridViewRow).FindControl("btnViewReg") as LinkButton).Enabled = true;
                            // (((e.Row) as GridViewRow).FindControl("divContainer") as Control).Visible = false;
                        }
                        else
                        {
                            (((e.Row) as GridViewRow).FindControl("btnViewReg") as LinkButton).Enabled = false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "ExecutionHistory.aspx.cs", "bindGrid", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        protected void dgLevel1View_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridView dgLevel1Views = ((e.Row) as GridViewRow).NamingContainer as GridView;
                    GridView InnerGridView = (GridView)((e.Row) as GridViewRow).FindControl("dgLevel2View");
                    dt = GetDataSetForBatchID(3, dgLevel1Views.DataKeys[e.Row.RowIndex][0].ToString(), dgLevel1Views.DataKeys[e.Row.RowIndex][1].ToString());
                    InnerGridView.DataSource = dt;
                    InnerGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "ExecutionHistory.aspx.cs", "bindGrid", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //btnSearch.Attributes.Add("onclick", "ShowProgressBar();");
            ////System.Threading.Thread.Sleep(5000);
            //btnSearch.Attributes.Add("onclick", "HideProgressBar();");
            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            try
            {
                if (ddlActType.SelectedIndex == 0)
                {
                    if (txtSearchNo.Text != "")
                    {
                        bindGridForFiNo(4, txtSearchNo.Text);
                    }
                    else
                    {
                        bindGridForFiNo(1, txtSearchNo.Text);
                    } 
                }
                else
                {
                    if (txtSearchNo.Text != "")
                    {
                        bindGridForBatchID(4, txtSearchNo.Text,"");
                    }
                    else
                    {
                        bindGridForBatchID(1, txtSearchNo.Text, "");
                    }

                }
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "ExecutionHistory.aspx.cs", "bindGrid", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        protected void CommandBtn_Click(Object sender, CommandEventArgs e)
        {
            //Session["GridImage"] = e.CommandArgument;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "window.open('../../Application/CKyc_Image.aspx?','_blank', 'toolbar=yes,scrollbars=yes,resizable=yes,top=10,left=10,width=500,height=500')", true);
        }

        protected void CommandBtn_Click1(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "OpenImgWindow('Flag1');", true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "window.open('../../Application/CKYC/ExecutionHistoImage.aspx?','_blank', 'toolbar=yes,scrollbars=yes,resizable=yes,left=400,top=230,width=600,height=400')", true);
        }

        protected void btnViewReg_Click1(object sender, EventArgs e)
        {
            //Response.Redirect("CKYCView.aspx?Status=view&refno=" + 10000133, false);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "window.open('../../Application/CKYC/CKYCView.aspx?Status=view&refno=" + 10000133 + "','_blank', 'toolbar = yes, scrollbars = yes, resizable = yes')", true);
        }

        protected void dgBatchView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DwnldError")
                {
                    hTable = new Hashtable();
                    dataAccessLayer = new DataAccessLayer("UpdDwnldConnectionString");
                    hTable.Clear();

                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    //Two command argument
                    string strbatchid = commandArgs[0].ToString();
                    //string strbatchFilename = commandArgs[1].ToString();

                    hTable.Add("@Batchid", strbatchid);
                    //hTable.Add("@Flag", "1");
                    //hTable.Add("@DocType", "CKYCFIReg");
                    dt = dataAccessLayer.GetDataTable("Prc_BindErrorGrid_LegalExecution", hTable);
                    if (dt.Rows.Count > 0)
                    {
                        SetExcelFile(strbatchid);
                        string strData;
                        strHtml = "BatchID" + "\t" + "UniqueRef No." + "\t" + "ErrorDescription" + "\t" + "ErrorCode" + "\n";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            strData = dt.Rows[i]["Batchid"].ToString()
                            + "\t" + dt.Rows[i]["UniqueRefNo"].ToString()
                            + "\t" + dt.Rows[i]["ErrorDesc"].ToString()
                            + "\t" + dt.Rows[i]["ErrorCode"].ToString();

                            strHtml = strHtml + strData + "\n";
                        }
                        byte[] byteData = System.Text.Encoding.ASCII.GetBytes(strHtml.ToString());
                        char[] charData = System.Text.Encoding.ASCII.GetChars(byteData);
                        Response.Write(charData, 0, charData.Length);
                        Response.Flush();
                        Response.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "ExecutionHistory.aspx.cs", "dgView_RowCommand", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        #region Set Excel File
        //xls file format
        protected void SetExcelFile(string strbatchid)
        {
            string attachment = "attachment; filename=" + strbatchid + "_" + DateTime.Now.ToString("MM/dd/yyyy") + "" + ".xls";

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            Response.Buffer = true;
            //Response.BufferOutput = true;
            // Response.ClearContent();
            Response.ContentType = "application/Microsoft Excel 97- Excel 2008 & 5.0/95 Workbook";
            Response.AddHeader("content-disposition", attachment);

        }




        //csv file format
        //protected void setSCVFile()
        //{
        //    string attachment = "attachment; filename=" + ViewState["DocType"] + ".csv";
        //    Response.ClearContent();
        //    Response.AddHeader("content-disposition", attachment);
        //    Response.ContentType = "application/Microsoft Excel 97-2003 Workbook";
        //}
        #endregion

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlActType.SelectedIndex = 1;
            txtSearchNo.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            dgBatchView.Visible = false;
            dgView.Visible = false;
            trDgViewDtl.Visible = false;
            trRecord.Visible = false;
            //lblMessage.Text = "0 Record Found";
            lblMessage.Visible = false;
        }
        private void ShowPageInformation()
        {
            try
            {
                int intPageIndex = dgBatchView.PageIndex + 1;
                lblPageInfo.Text = "Page " + intPageIndex.ToString() + " of " + dgBatchView.PageCount;
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "ExecutionHistory.aspx.cs", "ShowPageInformation", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        private void ShowPageInformationForFirefNo()
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
                    objErr.LogErr(1, "Executionistory.aspx.cs", "ShowPageInformationForFirefNo", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        protected void btnDay_Click(object sender, EventArgs e)
        {
            //btnDay.Attributes.Add("onclick", "ShowProgressBar();");
            ////System.Threading.Thread.Sleep(5000);
            //btnDay.Attributes.Add("onclick", "HideProgressBar();");
            if (ddlActType.SelectedIndex == 0)
            {
                bindGridForFiNo(6, "");
            }
            else
            {
                bindGridForBatchID(6, "", "");
            }
        }

        protected void btnWeek_Click(object sender, EventArgs e)
        {
            //btnWeek.Attributes.Add("onclick", "ShowProgressBar();");
            ////System.Threading.Thread.Sleep(5000);
            //btnWeek.Attributes.Add("onclick", "HideProgressBar();");
            if (ddlActType.SelectedIndex == 0)
            {
                bindGridForFiNo(7, "");
            }
            else
            {
                bindGridForBatchID(7, "", "");
            }
        }

        protected void btnMonth_Click(object sender, EventArgs e)
        {
            //btnMonth.Attributes.Add("onclick", "ShowProgressBar();");
            ////System.Threading.Thread.Sleep(5000);
            //btnMonth.Attributes.Add("onclick", "HideProgressBar();");
            if (ddlActType.SelectedIndex == 0)
            {
                bindGridForFiNo(8, "");
            }
            else
            {
                bindGridForBatchID(8, "", "");
            }
        }

        protected void btnQuarter_Click(object sender, EventArgs e)
        {
            //btnQuarter.Attributes.Add("onclick", "ShowProgressBar();");
            ////System.Threading.Thread.Sleep(5000);
            //btnQuarter.Attributes.Add("onclick", "HideProgressBar();");
            if (ddlActType.SelectedIndex == 0)
            {
                bindGridForFiNo(9, "");
            }
            else
            {
                bindGridForBatchID(9, "", "");
            }
        }
    }
}