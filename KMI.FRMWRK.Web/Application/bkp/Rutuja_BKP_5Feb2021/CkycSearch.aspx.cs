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
    public partial class CkycSearch : System.Web.UI.Page
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

        #region Page Load
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
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                olng = new MultilingualManager("DefaultConn", "CKYCSearch.aspx", Session["UserLangNum"].ToString());
                strUserLang = HttpContext.Current.Session["UserLangNum"].ToString();
                if (!IsPostBack)
                {
                    oCommonUtility.FillNoOfRecDropDown(ddlShwRecrds);
                    trDgViewDtl.Visible = false;
                    if (Request.QueryString["Status"].ToString() == "QC")
                    {
                        lblTitle.Text = "CKYC QC Search";
                    }
                    else if (Request.QueryString["Status"].ToString() == "chkr1")
                    {
                        lblTitle.Text = "CKYC Approval Search";
                    }
                    else if (Request.QueryString["Status"].ToString() == "view")
                    {
                        lblTitle.Text = "CKYC View Search";
                    }
                    else if (Request.QueryString["Status"].ToString() == "KMod")
                    {
                        lblTitle.Text = "CKYC Update Modification Search";
                        btnAddnew.Visible = false;
                    }

                    else if (Request.QueryString["Status"].ToString() == "Mod")
                    {
                        lblTitle.Text = "CKYC Modification Search";
                        btnAddnew.Visible = false;
                    }

                    else if (Request.QueryString["Status"].ToString() == "DocUpld")
                    {
                        lblTitle.Text = "CKYC Document Upload Search";
                    }
                    else if (Request.QueryString["Status"].ToString() == "PMS")
                    {
                        lblTitle.Text = "CKYC Probable Match Search";
                    }
                    else if (Request.QueryString["status"].Trim().ToString() == "LMod")
                    {
                        lblTitle.Text = "CKYC Partial Registration Search";
                        lblCASId.Text = "Temporary Reference No";
                        btnAddnew.Visible = true;
                    }
                    else if (Request.QueryString["status"].ToString() == "PMod")
                    {
                        lblTitle.Text = "CKYC Partial Registration Search";
                        lblCASId.Text = "Temporary Reference No";
                        btnAddnew.Visible = true;
                    }
                    else if (Request.QueryString["Status"].ToString() == "Reg")
                    {
                        lblTitle.Text = "CKYC  Search";
                        btnAddnew.Visible = true;
                    }
                    else
                    {
                        lblTitle.Text = "CKYC Search";
                    }

                    txtPan.Attributes.Add("onblur", "return fnValidatePAN(this)");
                }
                txtIdno.Enabled = false;
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
        #endregion

        #region GetDataTableCKYC
        protected void ddlShwRecrds_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataGrid();
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
                    #region BinddataGrid
                    if (dtResult_Kyc.Rows.Count > 0)
                    {
                        dgView.Columns[4].Visible = false;
                        trDgViewDtl.Visible = true;
                        trtitle.Visible = true;
                        // trgridsponsorship.Visible = true;
                        lblMessage.Visible = false;

                        dgView.DataSource = dtResult_Kyc;
                        dgView.DataBind();
                        ViewState["grid"] = dtResult_Kyc;


                        if (Request.QueryString["Status"].ToString() == "QC")
                        {
                            lblprospectsearch.Text = "CKYC QC Search Results";

                        }
                        if (Request.QueryString["Status"].ToString() == "KMod")
                        {
                            lblprospectsearch.Text = "CKYC Modification Search Results";

                        }
                        if (Request.QueryString["Status"].ToString() == "Mod")
                        {
                            lblprospectsearch.Text = "CKYC Modification Search Results";
                            dgView.Columns[4].Visible = false;

                        }
                        if (Request.QueryString["Status"].ToString() == "DocUpld")
                        {
                            lblprospectsearch.Text = "CKYC Document Upload Search Results";
                            dgView.Columns[4].Visible = false;

                        }
                        if (Request.QueryString["Status"].ToString() == "view")
                        {
                            lblprospectsearch.Text = "CKYC View Search Results";
                            dgView.Columns[4].Visible = false;

                        }
                        if (Request.QueryString["Status"].ToString() == "PMS")
                        {
                            lblprospectsearch.Text = "CKYC Probable Match Search Results";
                        }
                        if (Request.QueryString["Status"].ToString() == "view")
                        {
                            dgView.Columns[4].Visible = true;
                        }
                        if (Request.QueryString["Status"].ToString() == "PMod")
                        {
                            dgView.Columns[4].Visible = true;
                            dgView.Columns[3].Visible = false;
                            dgView.Columns[4].Visible = false;
                            dgView.Columns[1].HeaderText = "Temporary Reference No";
                        }
                        if (Request.QueryString["Status"].ToString() == "LMod")
                        {
                            dgView.Columns[4].Visible = true;
                            dgView.Columns[3].Visible = false;
                            dgView.Columns[4].Visible = false;
                            dgView.Columns[1].HeaderText = "Temporary Reference No";
                        }

                        dgView.DataBind();
                        trnote.Visible = true;
                        dgView.Visible = true;
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

                    if (Request.QueryString["Status"].ToString() == "KMod")
                    {
                        Message = "0 Record Found<br/> <br/>Do You Want To download Data from CERSAI System.";
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);

                    }
                }
                #endregion

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
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion 

        #region GetDataTableCKYC
        protected DataTable GetDataTableCKYC()
        {
            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            try
            {
                dt.Clear();
                hTable.Clear();
                if (Request.QueryString["Status"].ToString() == "PMod")
                {
                    hTable.Add("@PsTempRegRefNo", txtRefNo.Text.Trim());
                    hTable.Add("@FIRefNo", System.DBNull.Value);
                }
                else if (Request.QueryString["Status"].ToString() == "LMod")
                {
                    hTable.Add("@PsTempRegRefNo", txtRefNo.Text.Trim());
                    hTable.Add("@FIRefNo", System.DBNull.Value);
                }
                else
                {
                    hTable.Add("@PsTempRegRefNo", System.DBNull.Value);
                    hTable.Add("@FIRefNo", txtRefNo.Text.Trim());
                }

                hTable.Add("@KYCNo", txtKycNo.Text.Trim());
                hTable.Add("@Name", txtName.Text.Trim());


                //if (ddlIdType.SelectedItem.Text == "Select")
                //{
                //    hTable.Add("@IDType", System.DBNull.Value);

                //}
                //else
                //{
                //    hTable.Add("@IDType", ddlIdType.SelectedValue);
                //}
                hTable.Add("@PAN", txtPan.Text.Trim());
                hTable.Add("@ActionFlag", Request.QueryString["Status"].ToString().Trim());
                if (txtDTRegFrom.Text.Trim() != "")
                {
                    hTable.Add("@CreateFrmDtim", DateTime.Parse(txtDTRegFrom.Text.Trim()).ToString("dd-MM-yyyy"));
                }
                else
                {
                    hTable.Add("@CreateFrmDtim ", System.DBNull.Value);
                }

                if (txtDTRegTo.Text.Trim() != "")
                {
                    hTable.Add("@CreateToDtim", DateTime.Parse(txtDTRegTo.Text.Trim()).ToString("dd-MM-yyyy"));
                }
                else
                {
                    hTable.Add("@CreateToDtim ", System.DBNull.Value);
                }


                if (Request.QueryString["Status"].ToString() == "DocUpld")
                {
                    hTable.Add("@UserId", HttpContext.Current.Session["UserId"]);
                    dt = dataAccessLayer.GetDataTable("Prc_getSearchList_new", hTable);
                    hTable = null;
                }

                else
                {

                    hTable.Add("@UserId", HttpContext.Current.Session["UserId"]);
                    dt = dataAccessLayer.GetDataTable("Prc_getSearchList_new", hTable);

                    hTable = null;
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
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "GetDataTableCKYC", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
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
            try
            {
                hdnRefNo.Value = txtRefNo.Text.Trim();
                hdnKycNo.Value = txtKycNo.Text.Trim();
                if (txtDTRegFrom.Text.ToString().Trim() != "" && txtDTRegTo.Text.ToString().Trim() != "")
                {
                    if (Convert.ToDateTime(txtDTRegTo.Text) < Convert.ToDateTime(txtDTRegFrom.Text))
                    {
                        msg = "Registration Date From should be less than Registration Date To";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);
                        return;
                    }
                }
                if (txtDTRegFrom.Text != "")
                {
                    date = DateTime.Today.ToString("dd\\-MM\\-yyyy");
                    DateTime date1, date2;
                    date1 = DateTime.ParseExact(date.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    date2 = DateTime.ParseExact(txtDTRegFrom.Text.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (date1 <= date2)
                    {
                        msg = "Registration date from can not be future date";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);
                        return;
                    }
                }
                if (txtDTRegTo.Text != "")
                {
                    date = DateTime.Today.ToString("dd\\-MM\\-yyyy");
                    DateTime date1, date2;
                    date1 = DateTime.ParseExact(date.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    date2 = DateTime.ParseExact(txtDTRegTo.Text.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    if (date1 <= date2)
                    {
                        msg = "Registration date to can not be future date";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);
                        return;
                    }
                }

                if (txtPan.Text.ToString().Trim() != "")
                {
                    if (txtPan.Text.Length < 5)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Minimum 5 characters required for PAN No');", true);
                        return;
                    }
                }
                BindDataGrid();
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
                txtRefNo.Text = string.Empty;
                txtDTRegTo.Text = "";
                txtKycNo.Text = "";
                txtName.Text = "";
                txtPan.Text = "";
                txtIdno.Text = "";
                txtDTRegFrom.Text = "";
                txtDTRegTo.Text = "";
                txtSurname.Text = "";
                // ddlIdType.SelectedIndex = 0;
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


        #region dgView RowCommand
        protected void dgView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                String ConstType = "";
                //GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                string refno = e.CommandArgument.ToString().Trim();
                hTable.Clear();
                hTable.Add("@RegRefNo", refno);

                if (Request.QueryString["Status"].ToString() != "LMod")
                {
                    if ((refno != "Prev") && (refno != "Next") && (refno != "First") && (refno != "Last"))
                    {
                        ConstType = dataAccessLayer.ExecuteScalar("Prc_GetConstType", hTable).ToString();
                    }
                }
                //string ConstType = dt.Rows[0]["ConstitutionType"].ToString().Trim();


                if (e.CommandName == "Edit" || e.CommandName == "Short" || e.CommandName == "View" || e.CommandName == "KMod" || e.CommandName == "QC" || e.CommandName == "KMod"
                    || e.CommandName == "PMod" || e.CommandName == "Mod" || e.CommandName == "DocUpld" || e.CommandName == "PMS")
                {
                    if (Request.QueryString["status"].ToString() == "view")
                    {
                        Response.Redirect("CKYCView.aspx?Status=view&refno=" + refno, false);

                        //Response.Redirect("CKYCEntView.aspx?Status=view&refno=" + refno, false);
                    }
                    if (Request.QueryString["status"].ToString() == "Update")
                    {
                        Response.Redirect("CKYCView.aspx?Status=view&refno=" + refno, false);

                        //Response.Redirect("CKYCEntView.aspx?Status=view&refno=" + refno, false);
                    }
                    if (Request.QueryString["Status"].ToString() != "LMod")
                    {
                        if (Request.QueryString["Status"].ToString() == "QC" || Request.QueryString["Status"].ToString() == "chkr1")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "OpenQCPage(" + refno + ");", true);
                            //  Response.Redirect("CKYCQC.aspx?Status=QC&refno=" + refno, false);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenQCPage(" + refno + ");", true);
                        }
                    }

                    //if (Request.QueryString["Status"].ToString() == "QC" || Request.QueryString["Status"].ToString() == "chkr1" )
                    //{
                    //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenQCPage(" + refno + ");", true);
                    //    //  Response.Redirect("CKYCQC.aspx?Status=QC&refno=" + refno, false);
                    //}

                    if (Request.QueryString["Status"].ToString() == "KMod")
                    {
                        Response.Redirect("LegalEntityDtls.aspx?Status=KMod&refno=" + refno, false);
                    }
                    if (Request.QueryString["Status"].ToString() == "PMod")
                    {
                        Response.Redirect("LegalEntityDtls.aspx?Status=PMod&refno=" + refno, false);
                    }
                    if (Request.QueryString["Status"].ToString() == "LMod")
                    {
                        Response.Redirect("LegalEntityDtls.aspx?Status=LMod&refno=" + refno, false);
                    }
                    if (Request.QueryString["Status"].ToString() == "Mod")
                    {
                        Response.Redirect("LegalEntityDtls.aspx?Status=Mod&refno=" + refno, false);
                    }
                    if (Request.QueryString["Status"].ToString() == "DocUpld")
                    {
                        if (ConstType == "02")
                        {
                            Response.Redirect("CKYCEntDocUpld.aspx?Status=Docupld&refno=" + refno, false);
                        }
                        else
                        {
                            Response.Redirect("CKYCDocupld.aspx?Status=Docupld&refno=" + refno, false);
                        }  
                    }
                    if (Request.QueryString["Status"].ToString() == "PMS")
                    {
                        if (e.CommandName == "Short")
                        {
                            Response.Redirect("ShortPortableSearch.aspx?Status=PMS&refno=" + refno, false);
                        }
                        else
                        {
                            Response.Redirect("CkycPMSVerify.aspx?Status=PMS&refno=" + refno, false);
                        }
                    }
                    if (Request.QueryString["Status"].ToString() == "chkr1")
                    {
                        Response.Redirect("CKYCQC.aspx?Status=QC&refno=" + refno, false);
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
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "dgView_RowCommand", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
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
                    LinkButton lnkView = (LinkButton)e.Row.FindControl("lblView");
                    Label lblKYCSTATUS = (Label)e.Row.FindControl("lblKYCSTATUS");
                    if (Request.QueryString["Status"].ToString() == "Reg")
                    {
                        lnkView.Text = "Edit";
                    }
                    if (Request.QueryString["Status"].ToString() == "Mod")
                    {
                        lnkView.Text = "Edit";
                    }

                    if (Request.QueryString["Status"].ToString() == "View")
                    {
                        lnkView.Text = "View Details";
                    }
                    if (Request.QueryString["Status"].ToString() == "QC")
                    {
                        lnkView.Text = "Quality Check";
                    }
                    if (Request.QueryString["Status"].ToString() == "KMod")
                    {
                        lnkView.Text = "Edit";
                    }
                    if (Request.QueryString["Status"].ToString() == "Reg")
                    {
                        lnkView.Text = "Edit";
                    }
                    if (Request.QueryString["Status"].ToString() == "DocUpld")
                    {
                        lnkView.Text = "Upload Document";
                    }

                    if (Request.QueryString["Status"].ToString() == "chkr1")
                    {
                        lnkView.Text = "CKYC Approval";
                    }
                    if (Request.QueryString["Status"].ToString() == "PMS")
                    {
                        dgView.Columns[6].Visible = true;
                        lnkView.Text = "Details Search";
                    }
                    if (Request.QueryString["Status"].ToString() == "PMod")
                    {
                        lnkView.Text = "Edit";
                    }
                    if (Request.QueryString["Status"].ToString() == "LMod")
                    {
                        lnkView.Text = "Edit";
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
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "dgView_RowDataBound", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
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

        #region ddlIdType_SelectedIndexChanged Event
        protected void ddlIdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlIdType.Text != "Select")
                {
                    txtIdno.Enabled = true;
                }
                else
                {
                    txtIdno.Enabled = false;
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
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "ddlIdType_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
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



        #region GridInbox_RowCommand Event
        protected void GridInbox_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ViewCFR")
                {
                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                    Label lblAppNo = (Label)row.FindControl("lblAppNo");
                    Response.Redirect("ApproveQC.aspx?TrnRequest=CFRRespond&refno=" + lblAppNo.Text, false);
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
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "GridInbox_RowCommand", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region GridResponded_RowCommand Event
        protected void GridResponded_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewCFR")
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                Label lblAppNo = (Label)row.FindControl("lblAppNo");
                Response.Redirect("ApproveQC.aspx?TrnRequest=CFRRespond1&refno=" + lblAppNo.Text, false);
            }
        }
        #endregion

        #region btnAddnew_Click Event
        protected void btnAddnew_Click(object sender, EventArgs e)
        {
            try

            {
                //if (Request.QueryString["Status"] == "Reg")
                //{
                //    Response.Redirect("LegalEntityDtls.aspx?status=Reg", false);
                //}
                //else
                //{
                Response.Redirect("LegalEntityDtls.aspx?status=Reg", false);
                //}
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "btnAddnew_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion




        #region btnYes_Click Event
        protected void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("CKYCSecuredSearch.aspx?Mode=KMod");
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "btnYes_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion



        #region Page Size Selection Handling
        protected void ddlPageSize_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["grid"] != null)
                {
                    dt = (DataTable)ViewState["grid"];
                    dgView.DataSource = dt;
                    dgView.DataBind();
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
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "ddlPageSize_OnSelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
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
                    objErr.LogErr(1, "CkycSearch.aspx.cs", "dgView_RowCreated", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
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

        protected void btnsyncFile_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "OpenZipFilePage();", true);
        }
    }

}