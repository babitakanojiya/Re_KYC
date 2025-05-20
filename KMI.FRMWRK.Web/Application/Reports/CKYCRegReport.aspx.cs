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
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;

namespace KMI.FRMWRK.Web.Application.Reports
{
    public partial class CKYCRegReport : System.Web.UI.Page
    {

        #region Declarartion
        DataTable dt;
        Hashtable hTable = new Hashtable();
        ErrorLog objErr = new ErrorLog();
        DataAccessLayer dataAccessLayer;
        private string Message = string.Empty;
        string strUsrrole = string.Empty;
        private MultilingualManager olng;
        private string strUserLang;
        string strAppID = string.Empty;
        string strModuleID = string.Empty;
        CommonUtility oCommonUtility = new CommonUtility();
        string UserID = string.Empty;
        string msg = string.Empty;
        public string date;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                oCommonUtility.FillNoOfRecDropDown(ddlShwRecrds);
            }
        }

        #region GetDataTableCKYC
        protected DataTable GetDataTableCKYC()
        {
            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            try
            {
                dt.Clear();
                hTable.Clear();

                if (txtDateFrom.Text.Trim() != "")
                {
                    //hTable.Add("@CreateFrmDtim", DateTime.Parse(txtDateFrom.Text.Trim()).ToString("dd-MM-yyyy"));
                    hTable.Add("@CreateFrmDtim", DateTime.Parse(txtDateFrom.Text.Trim()));
                }
                else
                {
                    hTable.Add("@CreateFrmDtim ", System.DBNull.Value);
                }

                if (txtDateTo.Text.Trim() != "")
                {
                    //hTable.Add("@CreateToDtim", DateTime.Parse(txtDateTo.Text.Trim()).ToString("dd-MM-yyyy"));
                    hTable.Add("@CreateToDtim", DateTime.Parse(txtDateTo.Text.Trim()));
                }
                else
                {
                    hTable.Add("@CreateToDtim ", System.DBNull.Value);
                }

                hTable.Add("@UserId", HttpContext.Current.Session["UserId"]);

                dt = dataAccessLayer.GetDataTable("Prc_getSearchRegReportData", hTable);

            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "GetDataTableCKYC", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
                hTable.Clear();
            }
            return dt;
        }
        #endregion

        #region Bind DataGrid
        protected void BindDataGrid()
        {
            try
            {
                dgView.PageSize = Convert.ToInt32(ddlShwRecrds.SelectedValue.Trim());
                DataTable dtResult_Kyc = new DataTable();
                dtResult_Kyc = GetDataTableCKYC();
                if (dtResult_Kyc != null)
                {
                    //#region BinddataGrid
                    if (dtResult_Kyc.Rows.Count > 0)
                    {
                        //dgView.Columns[4].Visible = false;
                        trDgViewDtl.Visible = true;
                        trtitle.Visible = true;
                        // trgridsponsorship.Visible = true;
                        lblMessage.Visible = false;

                        dgView.DataSource = dtResult_Kyc;
                        dgView.DataBind();
                        ViewState["grid"] = dtResult_Kyc;

                        //if (Request.QueryString["Status"].ToString() == "QC")
                        //{
                        //    lblprospectsearch.Text = "CKYC QC Search Results";

                        //}
                        //if (Request.QueryString["Status"].ToString() == "KMod")
                        //{
                        //    lblprospectsearch.Text = "CKYC Modification Search Results";

                        //}
                        //if (Request.QueryString["Status"].ToString() == "Mod")
                        //{
                        //    lblprospectsearch.Text = "CKYC Modification Search Results";
                        //    dgView.Columns[4].Visible = false;

                        //}
                        //if (Request.QueryString["Status"].ToString() == "DocUpld")
                        //{
                        //    lblprospectsearch.Text = "CKYC Document Upload Search Results";
                        //    dgView.Columns[4].Visible = false;

                        //}
                        //if (Request.QueryString["Status"].ToString() == "view")
                        //{
                        //    lblprospectsearch.Text = "CKYC View Search Results";
                        //    dgView.Columns[4].Visible = false;

                        //}
                        //if (Request.QueryString["Status"].ToString() == "PMS")
                        //{
                        //    lblprospectsearch.Text = "CKYC Probable Match Search Results";
                        //}
                        //if (Request.QueryString["Status"].ToString() == "view")
                        //{
                        //    dgView.Columns[4].Visible = true;
                        //}
                        //if (Request.QueryString["Status"].ToString() == "PMod")
                        //{
                        //    dgView.Columns[4].Visible = true;
                        //    dgView.Columns[3].Visible = false;
                        //    dgView.Columns[4].Visible = false;
                        //    dgView.Columns[1].HeaderText = "Temporary Reference No";
                        //}
                        //if (Request.QueryString["Status"].ToString() == "LMod")
                        //{
                        //    dgView.Columns[4].Visible = true;
                        //    dgView.Columns[3].Visible = false;
                        //    dgView.Columns[4].Visible = false;
                        //    dgView.Columns[1].HeaderText = "Temporary Reference No";
                        //}

                        //dgView.DataBind();
                        btnExport.Visible = true;
                        trnote.Visible = true;
                        dgView.Visible = true;
                    }
                    else
                    {
                        btnExport.Visible = false;
                        trDgViewDtl.Visible = false;
                        trtitle.Visible = false;
                        dgView.DataSource = null;
                        dgView.DataBind();
                        trRecord.Visible = true;
                        lblMessage.Text = "0 Record Found";
                        lblMessage.Visible = true;
                    }
                }
                else
                {
                    btnExport.Visible = false;
                    trDgViewDtl.Visible = false;
                    trtitle.Visible = false;
                    dgView.DataSource = null;
                    dgView.DataBind();
                    trRecord.Visible = true;
                    lblMessage.Text = "0 Record Found";
                    lblMessage.Visible = true;

                    //if (Request.QueryString["Status"].ToString() == "KMod")
                    //{
                    //    Message = "0 Record Found<br/> <br/>Do You Want To download Data from CERSAI System.";
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);

                    //}
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CkycSearch.aspx.cs", "BindDataGrid", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion

        #region GetDataTableCKYC
        protected void ddlShwRecrds_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataGrid();
        }
        #endregion

        #region  dgView PageIndexChanging
        protected void dgView_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
                    objErr.LogErr(1, "CKYCDownload.aspx.cs", "dgView_PageIndexChanging", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dt = null;
            }
        }
        #endregion

        #region dgView Show Page Information for GridView
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
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "ShowPageInformation", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region dgView Sorting Event
        protected void dgView_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                dt = GetDataTableCKYC();
                DataView dv = new DataView(dt);
                GridView dgSource = (GridView)sender;
                string strSort = string.Empty;
                string strASC = string.Empty;
                if (dgSource.Attributes["SortExpression"] != null)
                {
                    strSort = dgSource.Attributes["SortExpression"].ToString();
                }
                if (dgSource.Attributes["SortASC"] != null)
                {
                    strASC = dgSource.Attributes["SortASC"].ToString();
                }
                dgSource.Attributes["SortExpression"] = e.SortExpression;
                dgSource.Attributes["SortASC"] = "Yes";
                if (e.SortExpression == strSort)
                {
                    if (strASC == "Yes")
                    {
                        dgSource.Attributes["SortASC"] = "No";
                    }
                    else
                    {
                        dgSource.Attributes["SortASC"] = "Yes";
                    }
                }

                dv.Sort = dgSource.Attributes["SortExpression"];
                if (dgSource.Attributes["SortASC"] == "No")
                {
                    dv.Sort += " DESC";
                }
                dgSource.PageIndex = 0;
                dgSource.DataSource = dv;
                dgSource.DataBind();
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
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "dgView_Sorting", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;

            }
        }
        #endregion

        #region dgView RowDataBound
        protected void dgView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //LinkButton lnkView = (LinkButton)e.Row.FindControl("lblView");
                    //Label lblKYCSTATUS = (Label)e.Row.FindControl("lblKYCSTATUS");
                    //if (Request.QueryString["Status"].ToString() == "Reg")
                    //{
                    //    lnkView.Text = "Edit";
                    //}
                    //if (Request.QueryString["Status"].ToString() == "Mod")
                    //{
                    //    lnkView.Text = "Edit";
                    //}

                    //if (Request.QueryString["Status"].ToString() == "View")
                    //{
                    //    lnkView.Text = "View Details";
                    //}
                    //if (Request.QueryString["Status"].ToString() == "QC")
                    //{
                    //    lnkView.Text = "Quality Check";
                    //}
                    //if (Request.QueryString["Status"].ToString() == "KMod")
                    //{
                    //    lnkView.Text = "Edit";
                    //}
                    //if (Request.QueryString["Status"].ToString() == "Reg")
                    //{
                    //    lnkView.Text = "Edit";
                    //}
                    //if (Request.QueryString["Status"].ToString() == "DocUpld")
                    //{
                    //    lnkView.Text = "Upload Document";
                    //}

                    //if (Request.QueryString["Status"].ToString() == "chkr1")
                    //{
                    //    lnkView.Text = "CKYC Approval";
                    //}
                    //if (Request.QueryString["Status"].ToString() == "PMS")
                    //{
                    //    dgView.Columns[6].Visible = true;
                    //    lnkView.Text = "Details Search";
                    //}
                    //if (Request.QueryString["Status"].ToString() == "PMod")
                    //{
                    //    lnkView.Text = "Edit";
                    //}
                    //if (Request.QueryString["Status"].ToString() == "LMod")
                    //{
                    //    lnkView.Text = "Edit";
                    //}
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
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "dgView_RowDataBound", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region GridView Row Created Change Event
        protected void dgView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType.Equals(DataControlRowType.Pager))
                {
                    SetPagerButtonStates(dgView, e.Row, this, "ddlPageSelectorL", "ddlPageSelectorR");
                }

                if (e.Row.RowType.Equals(DataControlRowType.DataRow))
                {
                    dgView.UseAccessibleHeader = true;
                    dgView.HeaderRow.TableSection = TableRowSection.TableHeader;
                    TableCellCollection cells = dgView.HeaderRow.Cells;
                    cells[0].Attributes.Add("data-hide", "phone");
                    cells[1].Attributes.Add("data-class", "expand");
                    cells[2].Attributes.Add("data-hide", "phone");
                    //cells[3].Attributes.Add("data-hide", "phone");
                    //cells[4].Attributes.Add("data-hide", "phone");
                    //cells[5].Attributes.Add("data-hide", "phone");
                    cells[3].Attributes.Add("data-hide", "phone,tablet");
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
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "dgView_RowCreated", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region btnSearch_Click Event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //hdnRefNo.Value = txtRefNo.Text.Trim();
                //hdnKycNo.Value = txtKycNo.Text.Trim();

                //int DayDiff = 0;

                if (txtDateFrom.Text.ToString().Trim() == "")
                {
                    msg = "Please enter Registration Date From";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);
                    return;
                }

                if (txtDateTo.Text.ToString().Trim() == "")
                {
                    msg = "Please enter Registration Date To";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);
                    return;
                }

                if (txtDateFrom.Text.ToString().Trim() != "" && txtDateTo.Text.ToString().Trim() != "")
                {
                    if (Convert.ToDateTime(txtDateTo.Text) < Convert.ToDateTime(txtDateFrom.Text))
                    {
                        msg = "Registration Date From should be less than Registration Date To";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);
                        return;
                    }

                    if ((Convert.ToDateTime(txtDateTo.Text) - Convert.ToDateTime(txtDateFrom.Text)).TotalDays >= 90)
                    {
                        msg = "Difference between both the dates should be lesser or equals to 90 days";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);
                        return;
                    }

                    //(EndDate - StartDate).TotalDays;
                }
                if (txtDateFrom.Text != "")
                {
                    date = DateTime.Today.ToString("dd\\-MM\\-yyyy");
                    DateTime date1, date2;
                    date1 = DateTime.ParseExact(date.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    date2 = DateTime.ParseExact(txtDateFrom.Text.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (date1 < date2)
                    {
                        msg = "Registration date from can not be future date";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);
                        return;
                    }
                }
                if (txtDateTo.Text != "")
                {
                    date = DateTime.Today.ToString("dd\\-MM\\-yyyy");
                    DateTime date1, date2;
                    date1 = DateTime.ParseExact(date.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    date2 = DateTime.ParseExact(txtDateTo.Text.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (date1 < date2)
                    {
                        msg = "Registration date to can not be future date";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);
                        return;
                    }
                }



                BindDataGrid();

                //if (txtPan.Text.ToString().Trim() != "")
                //{
                //    if (txtPan.Text.Length < 5)
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Minimum 5 characters required for PAN No');", true);
                //        return;
                //    }
                //}
                //Response.Redirect("~/Application/CKYC/CkycSearch.aspx?Status=LMod&ModuleId=3000101");    //?Status=LMod&ModuleId=3000101
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "btnSearch_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region btnClear_Click Event
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                //txtRefNo.Text = string.Empty;
                //txtDTRegTo.Text = "";
                //txtKycNo.Text = "";
                //txtName.Text = "";
                //txtPan.Text = "";
                //txtIdno.Text = "";
                //txtDTRegFrom.Text = "";
                //txtDTRegTo.Text = "";
                //txtSurname.Text = "";
                // ddlIdType.SelectedIndex = 0;

                txtDateFrom.Text = "";
                txtDateTo.Text = "";
                lblMessage.Visible = false;
                dgView.Visible = false;
                ddlShwRecrds.SelectedIndex = 0;
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

        #region GetSearchData
        protected void GetSearchData(GridView grd)
        {
            try
            {
                if (ViewState["SearchBindGrid"] != null)
                {
                    dt = (DataTable)ViewState["SearchBindGrid"];
                    grd.DataSource = dt;
                    grd.DataBind();
                }
                else
                {
                    this.BindDataGrid();
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CkycSearch.aspx.cs", "GetSearchData", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion

        #region GridView Left And Right Button Indexing Change Event
        protected void ddlPageSelectorL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgView.EditIndex = -1;
                dgView.PageIndex = ((DropDownList)sender).SelectedIndex;
                GetSearchData(dgView);
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "ddlPageSelectorL_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        protected void ddlPageSelectorR_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgView.EditIndex = -1;
                dgView.PageIndex = ((DropDownList)sender).SelectedIndex;
                GetSearchData(dgView);
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "ddlPageSelectorR_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
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
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "SetPagerButtonStates", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        protected void btnExport_Click(object sender, EventArgs e)
        {
            //DataSet ds = new DataSet();
            //ds = (DataSet)ViewState["grid"];
            //ExportDataSetToExcel(ds);

            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["grid"];
            //dt.Columns["Marks"].ColumnName = "SubjectMarks";
            dt.Columns["RegRefNo"].ColumnName = "Registration Reference No";
            dt.Columns["FIRefNo"].ColumnName = "FI Reference No";
            dt.Columns["EntityName"].ColumnName = "Entity Name";
            dt.Columns["DateofIncorporation"].ColumnName = "Date of Incorporation";
            dt.Columns["PlaceofIncorporation"].ColumnName = "Place of Incorporation";
            dt.Columns["PAN"].ColumnName = "PAN No";
            dt.Columns["CndStatus"].ColumnName = "Registration Status";
            dt.Columns["EntCurAddress"].ColumnName = "Registered Current Address";
            dt.Columns["EntCorAddress"].ColumnName = "Registered Correspondence Address";
            dt.Columns["RelName"].ColumnName = "Related Person: Name";
            dt.Columns["RelType"].ColumnName = "Related Person: Type";
            dt.Columns["RelCurAddress"].ColumnName = "Related Person: Address";
            dt.Columns["RelMobNo"].ColumnName = "Related Person: Mobile No";
            dt.Columns["RelTelNo"].ColumnName = "Related Person: Telephone No";
            dt.Columns["RelEmailID"].ColumnName = "Related Person: E-Mail Address";
            dt.Columns["CtrlName"].ColumnName = "Controlling Person: Name";
            dt.Columns["CtrlCurAddress"].ColumnName = "Controlling Person: Address";
            dt.Columns["CtrlMobNo"].ColumnName = "Controlling Person: Mobile No";
            dt.Columns["CtrlTelNo"].ColumnName = "Controlling Person: Telephone No";
            dt.Columns["CtrlEmailID"].ColumnName = "Controlling Person: E-mail Address";
            dt.Columns["EntPOI"].ColumnName = "Legal Entity: Proof of Identity";
            dt.Columns["EntPOA"].ColumnName = "Legal Entity: Proof of Address";
            dt.Columns["RelPOI"].ColumnName = "Related Person: Proof of Identity";
            dt.Columns["RelPOA"].ColumnName = "Related Person: Proof of Address";
            
            ExportCSV(dt, "Registration Report");
        }

        public int ExportCSV(DataTable data, string fileName)
        {
            int Rest = 0;
            try
            {
                HttpContext context = HttpContext.Current;
                context.Response.Clear();
                context.Response.ContentType = "text/csv";
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".csv");

                //rite column header names
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    if (i > 0)
                    {
                        context.Response.Write(",");
                    }
                    context.Response.Write(data.Columns[i].ColumnName);
                }
                context.Response.Write(Environment.NewLine);
                //Write data
                foreach (DataRow row in data.Rows)
                {
                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        if (i > 0)
                        {
                            //row[i] = row[i].ToString().Replace(",", "");
                            context.Response.Write(",");

                            if (row[i].ToString() == "2252719")
                            {

                                string str = "12042468";
                            }
                        }
                        string strWrite = row[i].ToString();
                        strWrite = strWrite.Replace("<br>", "");
                        strWrite = strWrite.Replace("<br/>", "");
                        strWrite = strWrite.Replace("\n", "");
                        strWrite = strWrite.Replace("\t", "");
                        strWrite = strWrite.Replace("\r", "");
                        strWrite = strWrite.Replace(",", "");
                        strWrite = strWrite.Replace("\"", "");


                        context.Response.Write(strWrite);
                    }
                    context.Response.Write(Environment.NewLine);
                }
                context.Response.Flush();
                context.Response.End();
            }
            catch (Exception ex)
            {

            }
            return Rest;


        }

        private void ExportDataSetToExcel(DataSet ds)
        {
            //Creae an Excel application instance
            Excel.Application excelApp = new Excel.Application();

            //Create an Excel workbook instance and open it from the predefined location
            Excel.Workbook excelWorkBook = excelApp.Workbooks.Open(@"D:\RegCases.xlsx");

            foreach (DataTable table in ds.Tables)
            {
                //Add a new worksheet to workbook with the Datatable name
                Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = table.TableName;

                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                }

                for (int j = 0; j < table.Rows.Count; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                    }
                }
            }

            excelWorkBook.Save();
            excelWorkBook.Close();
            excelApp.Quit();

        }

    }
}