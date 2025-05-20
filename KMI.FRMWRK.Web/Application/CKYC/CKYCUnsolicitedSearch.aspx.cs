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
       public partial class CKYCUnsolicitedSearch : System.Web.UI.Page
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
        CommonUtility oCommonUtility = new CommonUtility();
        string strAppID = string.Empty;
        string strModuleID = string.Empty;
        string UserID = string.Empty;
        Guid obj = Guid.NewGuid();
        DataSet dsRel = new DataSet();
        string strTempRefNo = string.Empty;
        DataTable dtResult_Kyc = new DataTable();


        #endregion

        #region Page_Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (HttpContext.Current.Session["UserId"] == null)
                {
                    Response.Redirect("~/ErrorSession.aspx", true);
                }
              
                if (Session["UserId"] != null)
                {
                    UserID = Session["UserId"].ToString();
                }
                if (Session["AppID"] != null)
                {
                    strAppID = Session["AppID"].ToString();
                }
                if (Session["ModuleID"] != null)
                {
                    strModuleID = Session["ModuleID"].ToString();
                }
                olng = new MultilingualManager("DefaultConn", "CKYCUnsolicitedSearch.aspx", Session["UserLangNum"].ToString());
                strUserLang = HttpContext.Current.Session["UserLangNum"].ToString();

                if (!IsPostBack)
                {
                    trDgViewDtl.Visible = false;
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
                    objErr.LogErr(1,  "CKYCUnsolicitedSearch.aspx", "Page_Load",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        } 
        #endregion



        #region Bind DataGrid
        protected void BindDataGrid()
        {
            try
            {
                //dgView.PageSize = Convert.ToInt32(ddlShwRecrds.SelectedValue.Trim());
          
                dtResult_Kyc = GetDataTableCKYC();
                if (dtResult_Kyc != null)
                {
                    #region BinddataGrid
                    if (dtResult_Kyc.Rows.Count > 0)
                    {

                        trDgViewDtl.Visible = true;
                        trtitle.Visible = true;
                        //trgridUnsolicited.Visible = true;
                        lblMessage.Visible = false;

                        dgView.DataSource = dtResult_Kyc;
                        dgView.DataBind();
                        ViewState["grid"] = dtResult_Kyc;
                        dgView.DataBind();
                    }
                    else
                    {
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
                    trDgViewDtl.Visible = false;
                    trtitle.Visible = false;
                    dgView.DataSource = null;
                    dgView.DataBind();
                    trRecord.Visible = true;
                    lblMessage.Text = "0 Record Found";
                    lblMessage.Visible = true;


                }
                #endregion
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1,  "CkycSearch.aspx.cs", "BindDataGrid",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion 
        #region GetDataTableCKYC()
        protected DataTable GetDataTableCKYC()
        {
            try
            {
                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt.Clear();
                hTable.Clear();

                hTable.Add("@BatchId", txtBatchId.Text.Trim());
                hTable.Add("@CKYCNO", txtKycNo.Text.Trim());        
                dt = dataAccessLayer.GetDataTable("Prc_getUnsolicitedSearchList", hTable);
                
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                      
                    }
                    else
                    {
                        dt = null;
                    }
                }
                else
                {
                    dt = null;
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
                    objErr.LogErr(1,  "CKYCUnsolicitedSearch.aspx", "GetDataTableCKYC",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
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
        #region btnSearch_Click Event 
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            BindDataGrid();
        }
        #endregion

        #region btnClear_Click Event
        protected void btnClear_Click(object sender, EventArgs e)
        {
            //txtBatchId.Text = "";
            //txtKycNo.Text = "";
            //lblMessage.Visible = false;
            Response.Redirect("CKYCUnsolicitedSearch.aspx");

        } 
        #endregion

        #region dgView Sorting Event
        protected void dgView_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
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
                DataTable dt = GetDataTableCKYC();

                DataView dv = new DataView(dt);
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
                    objErr.LogErr(1,  "CKYCUnsolicitedSearch.aspx", "dgView_Sorting",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
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
                    DataTable dtCurrentTable = (DataTable)ViewState["SearchBindGrid"];
                    grd.DataSource = dtCurrentTable;
                    grd.DataBind();
                }
                else
                    this.BindDataGrid();

            }

            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1,  "CKYCUnsolicitedSearch.aspx.cs", "Page_Load",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
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
                    objErr.LogErr(1,  "CKYCUnsolicitedSearch.aspx", "Page_Load",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
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
                    objErr.LogErr(1,  "CKYCUnsolicitedSearch.aspx", "Page_Load",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
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
                    objErr.LogErr(1,  "CKYCUnsolicitedSearch.aspx", "Page_Load",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
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
                    objErr.LogErr(1,  "CKYCUnsolicitedSearch.aspx", "Page_Load",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
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
                    objErr.LogErr(1,  "CKYCUnsolicitedSearch.aspx.cs", "dgView_PageIndexChanging",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
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
                    objErr.LogErr(1,  "CKYCUnsolicitedSearch.aspx.cs", "ShowPageInformation",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion

    }
}