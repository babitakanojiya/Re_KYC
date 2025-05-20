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
    public partial class UploadSearch : System.Web.UI.Page
    {
        #region Declarartion
        DataTable dt;
        Hashtable hTable = new Hashtable();
        ErrorLog objErr = new ErrorLog();
        DataAccessLayer dataAccessLayer;
        string strHtml = string.Empty;
        string strAppID = string.Empty;
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AppID"] != null)
            {
                strAppID = Session["AppID"].ToString();
            }
        }
        #endregion

        #region btnSearch_Click
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindDataGrid();

        }
        #endregion

        #region Bind DataGrid
        protected void BindDataGrid()
        {
            try
            {
                //dgUpldSearch.PageSize = Convert.ToInt32(ddlShwRecrds.SelectedValue.Trim());

                dt = new DataTable();
                dt = GetDataTableCKYC();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dgUpldSearch.DataSource = dt;
                        dgUpldSearch.DataBind();
                        ViewState["dgUpldSearch"] = dt;
                        dgUpldSearch.Visible = true;
                        trDgViewDtl.Visible = true;
                    }
                    else
                    {
                        trDgViewDtl.Visible = false;
                        trtitle.Visible = false;
                        dgUpldSearch.DataSource = null;
                        dgUpldSearch.DataBind();
                        trRecord.Visible = true;
                        lblMessage.Text = "0 Record Found";
                        lblMessage.Visible = true;
                    }
                }
                else
                {
                    trDgViewDtl.Visible = false;
                    trtitle.Visible = false;
                    dgUpldSearch.DataSource = null;
                    dgUpldSearch.DataBind();
                    trRecord.Visible = true;
                    lblMessage.Text = "0 Record Found";
                    lblMessage.Visible = true;
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "UploadSearch.aspx.cs", "BindDataGrid", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dt = null;
                dataAccessLayer = null;
            }
        }
        #endregion

        #region GetDataTableCKYC
        protected DataTable GetDataTableCKYC()
        {
            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("UpdDwnldConnectionString");
            try
            {
                dt.Clear();
                hTable.Clear();
                hTable.Add("@BatchID", txtBatchid.Text.Trim());
                hTable.Add("@CreatedBy", txtUploadedBy.Text.Trim());
                hTable.Add("@UserId", HttpContext.Current.Session["UserID"].ToString().Trim());//Added by Prathamesh on 201802002

                if (txtUploadFrm.Text.Trim() != "")
                {
                    //   hTable.Add("@UpdFrmDate", DateTime.Parse(txtUploadFrm.Text.Trim()).ToString("MM/DD/yyyy"));
                      hTable.Add("@UpdFrmDate", txtUploadFrm.Text.Trim());
                }
                else
                {
                    hTable.Add("@UpdFrmDate", System.DBNull.Value);
                }

                if (txtUploadTo.Text.Trim() != "")
                {
                    //                    hTable.Add("@UpdToDate", DateTime.Parse(txtUploadTo.Text.Trim()).ToString("MM/DD/yyyy"));
                    hTable.Add("@UpdToDate", txtUploadTo.Text.Trim());
                }
                else
                {
                    hTable.Add("@UpdToDate", System.DBNull.Value);
                }

                dt = dataAccessLayer.GetDataTable("Prc_GetBtchStatus", hTable);
                hTable = null;
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "UploadSearch.aspx.cs", "GetDataTableCKYC", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;

            }
            return dt;
        }
        #endregion

        #region GridView Row Created Change Event
        protected void dgUpldSearch_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType.Equals(DataControlRowType.Pager))
                {
                    SetPagerButtonStates(dgUpldSearch, e.Row, this, "ddlPageSelectorL", "ddlPageSelectorR");
                }

                if (e.Row.RowType.Equals(DataControlRowType.DataRow))
                {
                    dgUpldSearch.UseAccessibleHeader = true;
                    dgUpldSearch.HeaderRow.TableSection = TableRowSection.TableHeader;
                    TableCellCollection cells = dgUpldSearch.HeaderRow.Cells;
                    cells[0].Attributes.Add("data-hide", "phone");
                    cells[1].Attributes.Add("data-class", "expand");
                    cells[2].Attributes.Add("data-hide", "phone");
                    cells[3].Attributes.Add("data-hide", "phone");
                    cells[4].Attributes.Add("data-hide", "phone");
                    cells[5].Attributes.Add("data-hide", "phone");
                    cells[6].Attributes.Add("data-hide", "phone,tablet");
                    //cells[7].Attributes.Add("data-hide", "phone,tablet");
                    //cells[8].Attributes.Add("data-hide", "phone,tablet");
                    //cells[9].Attributes.Add("data-hide", "phone,tablet");

                    // cells[15].Attributes.Add("data-hide", "phone,tablet");
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
                    objErr.LogErr(1, "UploadSearch.aspx.cs", "dgView_RowCreated", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region GridView Set Pager ButtonStates
        public void SetPagerButtonStates(GridView gridView, GridViewRow gvPagerRow, Page page, string DDlPagerL, string DDlPagerR)
        {
            try
            {
                int pageIndexL = gridView.PageIndex;
                int pageCountL = gridView.PageCount;
                int pageIndexR = gridView.PageIndex;
                int pageCountR = gridView.PageCount;//Initialize the variables

                ImageButton btnFirstL = (ImageButton)gvPagerRow.FindControl("ImgbtnFirst");
                ImageButton btnPreviousL = (ImageButton)gvPagerRow.FindControl("ImgbtnPrevious");
                ImageButton btnNextL = (ImageButton)gvPagerRow.FindControl("ImgbtnNext");
                ImageButton btnLastL = (ImageButton)gvPagerRow.FindControl("ImgbtnLast");
                ImageButton btnFirstR = (ImageButton)gvPagerRow.FindControl("ImgbtnFirst1");
                ImageButton btnPreviousR = (ImageButton)gvPagerRow.FindControl("ImgbtnPrevious1");
                ImageButton btnNextR = (ImageButton)gvPagerRow.FindControl("ImgbtnNext1");
                ImageButton btnLastR = (ImageButton)gvPagerRow.FindControl("ImgbtnLast1");//Find the controls

                btnFirstL.Visible = btnPreviousL.Visible = (pageIndexL != 0);
                btnNextL.Visible = btnLastL.Visible = (pageIndexL < (pageCountL - 1));
                btnFirstR.Visible = btnPreviousR.Visible = (pageIndexR != 0);
                btnNextR.Visible = btnLastR.Visible = (pageIndexR < (pageCountR - 1));//Manage the Buttons according to page number

                DropDownList ddlPageSelectorL = (DropDownList)gvPagerRow.FindControl(DDlPagerL);
                ddlPageSelectorL.Items.Clear();
                DropDownList ddlPageSelectorR = (DropDownList)gvPagerRow.FindControl(DDlPagerR);
                ddlPageSelectorR.Items.Clear();//Find Dropdowns

                for (int i = 1; i <= gridView.PageCount; i++)
                {
                    ddlPageSelectorL.Items.Add(i.ToString());
                    ddlPageSelectorR.Items.Add(i.ToString());
                }//Fill those dropdowns

                ddlPageSelectorL.SelectedIndex = pageIndexL;
                ddlPageSelectorR.SelectedIndex = pageIndexR;
                //Initialize the dropdowns

                string strPgeIndx = Convert.ToString(gridView.PageIndex + 1) + " of "
                                    + gridView.PageCount.ToString();//Initialize the Page Information.

                Label lblpageindx = (Label)gvPagerRow.FindControl("lblpageindx");
                lblpageindx.Text += strPgeIndx;
                Label lblpageindx2 = (Label)gvPagerRow.FindControl("lblpageindx2");
                lblpageindx2.Text += strPgeIndx;
                //Fill the Page Information section
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "UploadSearch.aspx.cs", "SetPagerButtonStates", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region  dgUpldSearch PageIndexChanging
        protected void dgUpldSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dt = GetDataTableCKYC();
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
                    objErr.LogErr(1, "UploadSearch.aspx.cs", "dgView_PageIndexChanging", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dt = null;
            }
        }
        #endregion

        #region dgUpldSearch Show Page Information for GridView
        private void ShowPageInformation()
        {
            try
            {
                int intPageIndex = dgUpldSearch.PageIndex + 1;
                lblPageInfo.Text = "Page " + intPageIndex.ToString() + " of " + dgUpldSearch.PageCount;
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "UploadSearch.aspx.cs", "ShowPageInformation", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region dgView_RowCommand Event
        protected void dgUpldSearch_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    string strbatchFilename = commandArgs[1].ToString();

                    hTable.Add("@Batchid", strbatchid);
                    hTable.Add("@Flag", "1");
                    hTable.Add("@DocType", "CKYCFIReg");
                    dt = dataAccessLayer.GetDataTable("Prc_BindErrorGrid", hTable);
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
                    objErr.LogErr(1, "UploadSearch.aspx.cs", "dgView_RowCommand", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion

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
           
           // Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/Microsoft Excel 97- Excel 2008 & 5.0/95 Workbook";
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

        #region dgUpldSearch_RowDataBound
        protected void dgUpldSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell row in e.Row.Cells)
                {
                    DataRowView dataRow = (DataRowView)e.Row.DataItem;
                    string FailCount = dataRow["FailCount"].ToString();

                    if (FailCount == "0")
                    {
                        LinkButton lnkbtnerrorDWN = (e.Row.FindControl("lnkbtnerrorDWN") as LinkButton);
                        lnkbtnerrorDWN.Enabled = false;
                    }
                    else
                    {
                        LinkButton lnkbtnerrorDWN = (e.Row.FindControl("lnkbtnerrorDWN") as LinkButton);
                        lnkbtnerrorDWN.Enabled = true;
                    }
                }
            }
        }
        #endregion dgUpldSearch_RowDataBound

        #region btnClear_Click Event
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                //txtRefNo.Text = string.Empty;
                txtBatchid.Text = "";
                txtUploadedBy.Text = "";
                txtUploadFrm.Text = "";
                txtUploadTo.Text = "";
                // ddlIdType.SelectedIndex = 0;
                lblMessage.Visible = false;
                dgUpldSearch.Visible = false;
                trDgViewDtl.Visible = false;
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "btnClear_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion
    }
}