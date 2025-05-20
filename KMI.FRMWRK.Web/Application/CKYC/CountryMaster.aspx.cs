using System;
using KMI.FRMWRK.DAL;
using System.Collections;
using System.Web.UI.HtmlControls;
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

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class CountryMaster : System.Web.UI.Page
    {
        #region Declaration
        DataSet dsRslt;
        Hashtable objht = new Hashtable();
        DataTable objDt = new DataTable();
        private MultilingualManager olng;
        DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
        CommonUtility oCommonUtility = new CommonUtility();
        ErrorLog objErr = new ErrorLog();
        string strrefno;//by meena
        CkycValidtion objVal = new CkycValidtion();
        string id = string.Empty;
        static int image_height;
        static int image_width;
        static int max_height;
        static int max_width;
        static byte[] data;
        private string strUserLang;
        string strUserId = string.Empty;
        string strAlertMsg = string.Empty;
        int AppId;

        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtCountryCode.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('btnSearch').click();return false;}} else {return true}; ");
                txtCountryDesc.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('btnSearch').click();return false;}} else {return true}; ");
                ddlActiveChk.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('btnSearch').click();return false;}} else {return true}; ");
                if (!IsPostBack)
                {
                    this.BindGrid();
                }
                lblErr.Text = string.Empty;
                lblErr.Visible = false;
                dvMstRef.Visible = false;
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }
        #endregion

        #region BindGrid
        private void BindGrid()
        {
            try
            {
                objht.Clear();
                objDt.Clear();
                objht.Add("@Country_CODE", string.Empty);
                objht.Add("@Country_Desc", string.Empty);
                objht.Add("@IsActive", string.Empty);
                dsRslt = objDAL.GetDataSet("Prc_GetMSCountry", objht);
                ViewState["SearchBindGrid"] = dsRslt.Tables[0];
                gvMstStateSearch.DataSource = dsRslt.Tables[0];
                gvMstStateSearch.DataBind();
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }
        #endregion

        #region Search_Clear_Cancel_Events
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/HomePage.aspx");
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCountryDesc.Text.Length >= 4 || txtCountryDesc.Text.Length == 0)
                {
                    gvMstStateSearch.EditIndex = -1;
                    gvMstStateSearch.DataSource = null;
                    gvMstStateSearch.DataBind();
                    objht.Clear();
                    objDt.Clear();
                    objht.Add("@Country_CODE", txtCountryCode.Text.ToString());
                    objht.Add("@Country_Desc", txtCountryDesc.Text.ToString());
                    objht.Add("@IsActive", ddlActiveChk.SelectedValue);
                    dsRslt = objDAL.GetDataSet("Prc_GetMSCountry", objht);
                    ViewState["SearchBindGrid"] = objDt.Copy();
                    gvMstStateSearch.DataSource = dsRslt;
                    gvMstStateSearch.DataBind();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }
        #endregion

        #region chkAlls_CheckedChanged Event
        protected void chkAlls_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    GridView gvSrDtls = (GridView)FindControl("gvMstStateSearch");
            //    foreach (GridViewRow gvsrno in gvSrDtls.Rows)
            //    {

            //        CheckBox chkAll = (CheckBox)gvMstStateSearch.HeaderRow.FindControl("chkAll");
            //        CheckBox ChkSelect = (CheckBox)gvsrno.FindControl("chkSoftLock");
            //        if (chkAll.Checked)
            //        {

            //            ChkSelect.Checked = true;

            //        }
            //        else
            //        {
            //            ChkSelect.Checked = false;
            //        }
            //    }


            //}
            //catch (Exception ex)
            //{
            //    objErrLog.LogErr("CBFRMRGIC", "MstStateCode.aspx.cs", "chkAlls_CheckedChanged", ex.Message.ToString(), Session["UserID"].ToString());
            //    throw ex;
            //}
            //finally
            //{

            //}
        }
        #endregion

        #region Delete All Records Option
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lnkDelete.OnClientClick = "if (!confirm('Are you sure you want to delete this record?')) return false;";
                GridView gvSrDtls = (GridView)FindControl("gvMstStateSearch");

                foreach (GridViewRow gvsrno in gvMstStateSearch.Rows)
                {
                    CheckBox chk = (CheckBox)gvsrno.FindControl("chkSoftLock");
                    if (chk.Checked)
                    {
                        //ht.Clear();
                        //ds.Clear();
                        Label lbl = (Label)gvsrno.FindControl("lblStateID");

                        objht.Add("@StateId", lbl.Text.ToString());

                        dsRslt = objDAL.GetDataSet("Prc_CBFRMDelMstState", objht, "DefaultConn");
                        if (dsRslt.Tables[1].Rows[0]["Flag"].ToString() == "F")
                        {
                            if (dsRslt.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsRslt.Tables[0].Rows.Count; i++)
                                {
                                    lblErr.Visible = true;
                                    lblErr.Text = lblErr.Text + "You cannot delete this record. <br /> There is a reference in database: " + dsRslt.Tables[0].Rows[i]["PKTABLE_QUALIFIER"].ToString() + ", Table: " + dsRslt.Tables[0].Rows[i]["desciption"].ToString() + ", Column: " + dsRslt.Tables[0].Rows[i]["FKCOLUMN_NAME"].ToString() + "." + "<br />";
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "PopupModal1();", true);
                        }

                    }
                    else
                    {
                    }
                    this.BindGrid();
                }
            }
            catch (Exception ex)
            {
                {
                    objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region Button Add New Click Events
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                gvMstStateSearch.EditIndex = -1;
                txtCountryCode.Text = string.Empty;
                txtCountryDesc.Text = string.Empty;
                ddlActiveChk.SelectedIndex = 0;
                this.BindGrid();
                Label lblCreatedBy = ((Label)gvMstStateSearch.FooterRow.FindControl("lblCreateBy"));
                Label lblCreateDTim = ((Label)gvMstStateSearch.FooterRow.FindControl("lblCreateDate"));
                dvMstRef.Visible = false;
                gvMstStateSearch.FooterRow.Visible = true;
            }
            catch (Exception ex)
            {
                {
                    objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }

        }
        #endregion

        #region Save Event
        protected void lnkSave_Click(object sender, EventArgs e)
        {
            try
            {
                objht.Clear();
                objDt.Clear();
                string strStateDesc = ((TextBox)gvMstStateSearch.FooterRow.FindControl("txtStateDescFt")).Text.ToString().Trim();
                string strIsActive = ((DropDownList)gvMstStateSearch.FooterRow.FindControl("ddlIsActive")).SelectedValue;
                if (strStateDesc.Equals(string.Empty))
                {
                    strAlertMsg = "Please enter state name.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "FnAlert('" + strAlertMsg + "');", true);
                    ((TextBox)gvMstStateSearch.FooterRow.FindControl("txtStateDescFt")).Focus();
                    return;
                }
                else

                    objht.Add("@Action", "INS");
                objht.Add("@StateDesc", strStateDesc);
                if (strIsActive.Equals(string.Empty))
                {
                    strAlertMsg = "Please select Is Active.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "FnAlert('" + strAlertMsg + "');", true);
                    ((DropDownList)gvMstStateSearch.FooterRow.FindControl("ddlIsActive")).Focus();
                    return;
                }
                else
                    objht.Add("@IsActive", strIsActive);
                objht.Add("@CreatedBy", Session["UserID"].ToString().Trim());
                //string strFlagExists = objDAL.GetDataSet("Prc_CBInsUpdStateDtls", objht, "DefaultConn");
                //if (strFlagExists.Equals("True"))
                //{
                //    stralertmsg = "record already exists.";
                //    scriptmanager.registerstartupscript(this, this.gettype(), uniqueid, "fnalert('" + stralertmsg + "');", true);
                //    ((textbox)gvmststatesearch.footerrow.findcontrol("txtstatedescft")).focus();
                //    return;
                //}
                //else
                //{
                //    stralertmsg = "record added successfully.";
                //    scriptmanager.registerstartupscript(this, this.gettype(), uniqueid, "fnalert('" + stralertmsg + "');", true);
                //}
                ((TextBox)gvMstStateSearch.FooterRow.FindControl("txtStateDescFt")).Text = string.Empty;
                ((DropDownList)gvMstStateSearch.FooterRow.FindControl("ddlIsActive")).SelectedIndex = 0;
                gvMstStateSearch.FooterRow.Visible = false;
                this.BindGrid();
            }
            catch (Exception ex)
            {
                {
                    objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region Editing Event
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                objht.Clear();
                GridViewRow row = gvMstStateSearch.Rows[e.NewEditIndex];
                int ParamValue = Convert.ToInt32(gvMstStateSearch.DataKeys[e.NewEditIndex].Values[0]);
                lblRefAlert.Text = string.Empty;
                objht.Add("@PrimaryKeyValue", ParamValue);
                objht.Add("@UserId", Session["UserID"].ToString());
                objht.Add("@TableName", "MS_Country");
                objht.Add("@ColumnName", hdnPrimaryColumn.Value);
                objht.Add("@DbName", "CKYC");
                objht.Add("@Flag", "Edit");
                gvMstStateSearch.EditIndex = e.NewEditIndex;
                GetSearchData(gvMstStateSearch);
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }
        #endregion

        #region Page Size Selection Handling
        protected void ddlPageSize_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvMstStateSearch.PageSize = Convert.ToInt32(ddlPageSize.SelectedItem.Text.ToString());
                GetSearchData(gvMstStateSearch);
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }

        }
        #endregion

        #region Update Event
        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                //objht.Clear();
                //objds.Clear();

                //txtStaCode.Text = string.Empty;
                //txtStaDesc.Text = string.Empty;
                //ddlActiveChk.SelectedIndex = 0;

                ////string strFlagExists = string.Empty;
                //GridViewRow row = gvMstStateSearch.Rows[e.RowIndex];
                //int StateID = Convert.ToInt32(gvMstStateSearch.DataKeys[e.RowIndex].Values[0]);
                //string strIsActive = (row.FindControl("txtIsActive") as DropDownList).SelectedValue;
                //strCeaseFlag = (row.FindControl("lblIsActivert") as Label).Text;

                //if (strCeaseFlag == "Yes")
                //{
                //    strCeaseFlag = "Y";
                //}
                //else
                //{
                //    strCeaseFlag = "N";
                //}

                //if (lblRefAlert.Text != "" && strIsActive == "N" && strIsActive != strCeaseFlag)
                //{
                //    if (strIsActive != strCeaseFlag)
                //    {
                //        hdnPrimaryValue.Value = StateID.ToString();
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "UpdateConfirm('" + StateID + "');", true);
                //        return;
                //    }
                //}

                //else
                //{
                //    string strStateDesc = (row.FindControl("txtStateDesc") as TextBox).Text.ToString().Trim();

                //    string strFlagExists = string.Empty;
                //    strDesc = (row.FindControl("lblStateDesc") as Label).Text.Trim();

                //    if (strStateDesc.Equals(string.Empty))
                //    {

                //        strAlertMsg = "Please enter state name.";
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "FnAlert('" + strAlertMsg + "');", true);
                //        (row.FindControl("txtStateDesc") as TextBox).Focus();
                //        return;
                //    }


                //    objht.Clear();
                //    objht.Add("@Action", "UPD");
                //    objht.Add("@StateId", StateID);

                //    //if (strIsActive.Equals(string.Empty))
                //    //{
                //    //    strAlertMsg = "Please select Is Active.";
                //    //    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "FnAlert('" + strAlertMsg + "');", true);
                //    //    (row.FindControl("txtIsActive") as DropDownList).Focus();
                //    //    return;
                //    //}
                //    //else
                //    //{
                //    //    if (strCeaseFlag != strIsActive)
                //    //    {
                //    //        objht.Add("@Flag", "1");
                //    //    }
                //    //    else
                //    //    {
                //    //        objht.Add("@Flag", string.Empty);
                //    //    }
                //    //}
                //    objht.Add("@IsActive", strIsActive);

                //    objht.Add("@CreatedBy", Session["UserID"].ToString().Trim());
                //    objht.Add("@StateDesc", strStateDesc);
                //    //strFlagExists = objDAL.exec_Scaler("Prc_CBInsUpdStateDtls", objht, "DefaultConn");
                //    if (strFlagExists.Equals("True"))
                //    {
                //        //strAlertMsg = "Record already exists.";
                //        //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "FnAlert('" + strAlertMsg + "');", true);
                //        (row.FindControl("txtStateDesc") as TextBox).Focus();
                //        return;
                //    }
                //    else
                //    {
                //        //strAlertMsg = "Record updated successfully.";
                //        //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "FnAlert('" + strAlertMsg + "');", true);
                //    }
                //    gvMstStateSearch.EditIndex = -1;
                //    //btnSearch_Click(this, EventArgs.Empty);
                //    this.BindGrid();
                //}
            }
            catch (Exception ex)
            {
                {
                    objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region  Cancel Editing
        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            try
            {
                gvMstStateSearch.EditIndex = -1;
                GetSearchData(gvMstStateSearch);
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }
        #endregion

        #region Get Primary Column
        protected void GetPrimaryColumn()
        {
            try
            {
                string strPrimary = string.Empty;
                objht.Clear();
                //objht.Add("@UserId", );
                //objht.Add("@TableName", );
                //objht.Add("@DbName", );
                //strPrimary = objGm.GetPrimayKeyForTable( Session["UserID"].ToString(),"USRMGMT","MstState");
                //strTable = strPrimary.Split(';');
                if (!(strPrimary.Equals(string.Empty)))
                {
                    hdnPrimaryColumn.Value = strPrimary;
                }
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }
        #endregion

        #region Delete Individual Record
        protected void DeleteButn_Click(object sender, EventArgs e)
        {
            try
            {
                objht.Clear();
                dsRslt.Clear();
                txtCountryCode.Text = string.Empty;
                txtCountryDesc.Text = string.Empty;
                ddlActiveChk.SelectedIndex = 0;
                gvMstStateSearch.EditIndex = -1;
                GetSearchData(gvMstStateSearch);
                int StateId = Convert.ToInt32(hdnPrimaryValue.Value);
                string strIsActive = hdnIsActive.Value.ToString();
                lblRefAlert.Text = string.Empty;
                objht.Add("@PrimaryKeyValue", StateId);
                objht.Add("@UserId", Session["UserID"].ToString());
                objht.Add("@TableName", "MS_State");
                objht.Add("@ColumnName", hdnPrimaryColumn.Value);
                objht.Add("@DbName", "CKYC");
                //int iRet = objGm.CheckMstReferences(objht, strIsActive, Session["UserID"].ToString(), "Delete", out strRetRef, out strRetForAdmin);
                //if (iRet == 0)
                //{
                //    lblErr.Visible = true;
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "FnAlert('" + strRetRef + "');", true);
                //    lblRefAlert.Text = strRetForAdmin;
                //    lblRefAlert.Visible = true;
                //    dvMstRef.Visible = true;
                //}
                //else
                //{
                //    dvMstRef.Visible = false;
                //    strAlertMsg = "Record deleted successfully.";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "FnAlert('" + strAlertMsg + "');", true);
                //}
                btnSearch_Click(this, EventArgs.Empty);
                GetSearchData(gvMstStateSearch);
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }
        #endregion

        #region To Perform Delete Event
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            try
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "DelConfirm();", true);
                //if (hdnPrimaryValue.Value == "true")
                //{
                //    ht.Clear();
                //    ds.Clear();
                //    GridViewRow row = gvMstStateSearch.Rows[e.RowIndex];
                //    int StateID = Convert.ToInt32(gvMstStateSearch.DataKeys[e.RowIndex].Values[0]);
                //    ht.Add("@StateId", StateID);
                //    ht.Add("@UserId", Session["UserId"].ToString().Trim());
                //    ds = objDAL.GetDataSetForPrcDBConn("Prc_CBFRMDelSrvcReqType", ht, "DefaultConn");
                //    if (ds.Tables[1].Rows[0]["Flag"].ToString() == "F")
                //    {
                //        if (ds.Tables[0].Rows.Count > 0)
                //        {
                //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //            {
                //                lblErr.Visible = true;
                //                lblErr.Text = lblErr.Text + "You cannot delete this record. <br /> There is a reference in database: " + ds.Tables[0].Rows[i]["PKTABLE_QUALIFIER"].ToString() + ", Table: " + ds.Tables[0].Rows[i]["desciption"].ToString() + ", Column: " + ds.Tables[0].Rows[i]["FKCOLUMN_NAME"].ToString() + "." + "<br />";
                //            }
                //        }
                //    }
                //    else
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "PopupModal1();", true);
                //    }
                //    this.BindGrid();
                //}
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }
        #endregion

        #region On Row DataBound of grid
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvMstStateSearch.EditIndex)
                {
                    ((LinkButton)e.Row.FindControl("lbUpdate")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lnkCancelBtn")).Visible = false;
                    ((HtmlGenericControl)e.Row.FindControl("spnUpdate")).Visible = false;
                }
                if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex == gvMstStateSearch.EditIndex)
                {
                    TextBox txtStateDesc = (TextBox)e.Row.FindControl("txtCountryDesc");
                    txtStateDesc.Text = ((e.Row.FindControl("lblCountryDesc") as Label).Text).ToString();
                    //DropDownList ddlIsAct = (DropDownList)e.Row.FindControl("txtIsActive");
                    //if ((e.Row.FindControl("lblIsActivert") as Label).Text.ToString() != "")
                    //{
                    //    ddlIsAct.Items.FindByText((e.Row.FindControl("lblIsActivert") as Label).Text).Selected = true;
                    //}
                    //else
                    //{
                    //    ddlIsAct.Items.FindByValue((e.Row.FindControl("lblIsActivert") as Label).Text).Selected = true;
                    //}
                    Label lblUpdate = (Label)e.Row.FindControl("lblUpdate");
                    Label lblUpdateTime = (Label)e.Row.FindControl("lblUpdateTime");
                    ((LinkButton)e.Row.FindControl("lnkDeleteGrd")).Visible = false;
                    ((HtmlGenericControl)e.Row.FindControl("spnEdit")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lbEdit")).Visible = false;
                    if (!(lblRefAlert.Text.Equals(string.Empty)))
                    {
                        DataTable dt = new DataTable();
                        dt.Clear();
                        objht.Clear();
                        objht.Add("@TableName", "MS_Country");
                        objht.Add("@DBName", "CKYC");
                        //dt = objDAL.exec_proc("Prc_CBFRMGetDynmcFieldsForAddAtt", objht, "DefaultConn");
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvMstStateSearch.Columns.Count; i++)
                            {
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    if (dt.Rows[j]["ColumnName"].ToString() == gvMstStateSearch.Columns[i].HeaderText)
                                    {
                                        e.Row.Cells[i].Enabled = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }
        #endregion

        #region GridView Page Index Change Event
        protected void gvMstStateSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvMstStateSearch.EditIndex = -1;
                gvMstStateSearch.PageIndex = e.NewPageIndex;
                GetSearchData(gvMstStateSearch);
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }
        #endregion

        #region ddlPageSelectorL Event
        protected void ddlPageSelectorL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvMstStateSearch.EditIndex = -1;
                gvMstStateSearch.PageIndex = ((DropDownList)sender).SelectedIndex;
                GetSearchData(gvMstStateSearch);
                dvMstRef.Visible = false;
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }
        #endregion

        #region ddlPageSelectorR Event
        protected void ddlPageSelectorR_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvMstStateSearch.EditIndex = -1;
                gvMstStateSearch.PageIndex = ((DropDownList)sender).SelectedIndex;
                GetSearchData(gvMstStateSearch);
                dvMstRef.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GridView Row Created Event
        protected void gvMstStateSearch_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    SetPagerButtonStates(gvMstStateSearch, e.Row, this, "ddlPageSelectorL", "ddlPageSelectorR");
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    gvMstStateSearch.UseAccessibleHeader = true;
                    gvMstStateSearch.HeaderRow.TableSection = TableRowSection.TableHeader;
                    TableCellCollection cells = gvMstStateSearch.HeaderRow.Cells;
                    cells[1].Attributes.Add("data-class", "expand");
                    cells[2].Attributes.Add("data-hide", "phone");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SetPagerButtonStates
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
                string strPgeIndx = Convert.ToString(gridView.PageIndex + 1) + " of " + gridView.PageCount.ToString();//Initialize the Page Information.
                Label lblpageindx = (Label)gvPagerRow.FindControl("lblpageindx");
                lblpageindx.Text += strPgeIndx;
                Label lblpageindx2 = (Label)gvPagerRow.FindControl("lblpageindx2");
                lblpageindx2.Text += strPgeIndx;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        #endregion

        #region lnkCustomFields_Click event
        protected void lnkCustomFields_Click(object sender, EventArgs e)
        {
            try
            {
                if (lnkCustomFields.Text == "Show custom fields")
                {
                    //objGm.VisibilityOfCustom("MstState", "USRMGMT", gvMstStateSearch, lnkCustomFields, true, "Hide custom fields");
                    return;
                }
                //objGm.VisibilityOfCustom("MstState", "USRMGMT", gvMstStateSearch, lnkCustomFields, false, "Show custom fields");
                GetSearchData(gvMstStateSearch);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        #endregion

        #region gvMstStateSearch_Sorting event
        protected void gvMstStateSearch_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {

                gvMstStateSearch.EditIndex = -1;
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

                if (e.SortExpression.Equals(strSort))
                {
                    if (strASC.Equals("Yes"))
                    {
                        dgSource.Attributes["SortASC"] = "No";
                    }
                    else
                    {
                        dgSource.Attributes["SortASC"] = "Yes";
                    }
                }
                if (ViewState["SearchBindGrid"] != null)
                {
                    DataTable dt = (DataTable)ViewState["SearchBindGrid"];
                    DataView dv = new DataView(dt);
                    dv.Sort = dgSource.Attributes["SortExpression"];

                    if (dgSource.Attributes["SortASC"] == "No")
                    {
                        dv.Sort += " DESC";
                    }

                    dgSource.PageIndex = 0;
                    dgSource.DataSource = dv;
                    dgSource.DataBind();
                    ViewState["SearchBindGrid"] = dv.ToTable();
                }
                else
                    this.BindGrid();
            }

            catch (Exception ex)
            {
                throw ex;
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
                    this.BindGrid();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //#region UpdateButton
        //protected void UpdateButn_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        objht.Clear();
        //        objht.Add("@PrimaryKeyValue", hdnPrimaryValue.Value);
        //        objht.Add("@UserId", Session["UserID"].ToString());
        //        objht.Add("@TableName", "MstState");
        //        objht.Add("@ColumnName", hdnPrimaryColumn.Value);
        //        objht.Add("@DbName", "USRMGMT");
        //        //objDAL.exec_command("Prc_UpdateAllReferencesGeneric_Nik", objht, "DefaultConn");
        //        //objGm.GetAllForeginUpdate(objht, Session["UserID"].ToString());
        //        gvMstStateSearch.EditIndex = -1;
        //        strAlertMsg = "All records updated successfully.";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "FnAlert('" + strAlertMsg + "');", true);
        //        btnSearch_Click(this, EventArgs.Empty);
        //        GetSearchData(gvMstStateSearch);

        //    }
        //    catch (Exception ex)
        //    {
        //        //objErrLog.LogErr("CBFRMRGIC", "MstState.aspx.cs", "UpdateButn_Click", ex.Message.ToString(), Session["UserID"].ToString());
        //        throw ex;
        //    }
        //}
        //#endregion
    }
}