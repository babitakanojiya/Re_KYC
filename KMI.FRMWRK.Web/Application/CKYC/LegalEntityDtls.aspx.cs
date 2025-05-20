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

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class LegalEntityDtls : System.Web.UI.Page
    {
        #region Declare Veriables
        private MultilingualManager olng;
        Hashtable htParam = new Hashtable();
        ErrorLog objErr;

        private string Message = string.Empty;
        string related = null;
        string controlling = null;
        string strRefNo = string.Empty;
        string PSTempRefNo = string.Empty;
        string PSTempRelRefNo = string.Empty;
        CkycValidtion objVal = new CkycValidtion();
        DataSet objds = new DataSet();
        string strUserId;
        Guid obj = Guid.NewGuid();
        //DataSet dsRel = new DataSet();
        //string lngcode;
        CommonUtility oCommonUtility = new CommonUtility();
        string msg;

        //private string strUserLang;
        int AppID;
        //string strModuleID = string.Empty;
        //string strMstrModuleCode = string.Empty;
        string UserID = string.Empty;

        DataTable dt;
        DataTable ctrldt;
        DataAccessLayer objDAL;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Mode"] != null)
            {


                if (Request.QueryString["Mode"].ToString() == "Mail")
                {
                    Session["CarrierCode"] = '2';
                    olng = new MultilingualManager("DefaultConn", "LegalEntityDtls.aspx", "01");

                    //strUserLang = HttpContext.Current.Session["UserLangNum"].ToString();
                    strUserId = "a"; //Request.QueryString["UserID"].ToString();
                    //GetFIMissingFields();
                    Session["UserID"] = strUserId;
                    Session["UserGroupCode"] = "";
                    ViewState["District"] = txtDistrictname.Text;

                }
            }
            else
            {
                if (HttpContext.Current.Session["UserId"] == null)
                {
                    Response.Redirect("~/ErrorSession.aspx", true);
                }
                if (Session["AppID"] != null)
                {
                    AppID = Convert.ToInt32(Session["AppID"]);
                }
                //if (Session["ModuleID"] != null)
                //{
                //    strModuleID = Session["ModuleID"].ToString();
                //}
                //if (Session["MstrModuleCode"] != null)
                //{
                //    strMstrModuleCode = Session["MstrModuleCode"].ToString();
                //}
                if (Session["UserId"] != null)
                {
                    UserID = Session["UserId"].ToString();
                }
                //lngcode = Session["UserLangNum"].ToString();
                Session["CarrierCode"] = '2';
                olng = new MultilingualManager("DefaultConn", "LegalEntityDtls.aspx", Session["UserLangNum"].ToString());
                strUserId = HttpContext.Current.Session["UserID"].ToString().Trim();
            }

            txtInsName.Enabled = false;
            txtInsCode.Enabled = false;

            if (!IsPostBack)
            {
                if (Session["PSSubmit"] != null)
                {
                    Session["PSSubmit"] = null;
                }
                InitializeControls();
                htParam.Clear();
                htParam.Add("@USERID", strUserId.Trim());
                DataTable dtEntDetails = new DataAccessLayer().GetDataTable("Prc_GetEntitySetup", htParam, "DefaultConn");
        
                if (dtEntDetails.Rows.Count > 0)
                {
                    txtInsName.Text = Convert.ToString(dtEntDetails.Rows[0]["EntityName"]);
                    txtInsCode.Text = Convert.ToString(dtEntDetails.Rows[0]["InstitutionCode"]);
                }
                if (Request.QueryString["status"].ToString() == "Reg")
                {
                    if (Session["dsRel"] != null)
                    {
                        Session["dsRel"] = null;
                    }

                    if (Session["dsCtrl"] != null)
                    {
                        Session["dsCtrl"] = null;
                    }

                    if (Session["dsRel"] == null)
                    {
                        lnkViewRel.Attributes.Add("style", "display:none");
                    }


                    //  InitializeControls();
                    FillddlPageLoad();
                    FillCountry();
                    FillStates();
                    FillConstType();
                    FillDocumentReceived();
                    //PopulatePinCode();
                    BindAttestation();

                    //chkPerAddress.Checked = true;
                    //chkPerAddress.Enabled = false;
                    txtDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                    txtDateKYCver.Text = DateTime.Today.ToString("dd-MM-yyyy");

                    //chkAddCtrl.Enabled = false;
                }
                else if (Request.QueryString["status"].ToString() == "LMod")
                {
                    divchkDelRel.Visible = true;
                    divIdProof.Visible = false;

                    FillddlPageLoad();
                    FillCountry();
                    FillStates();
                    FillConstType();
                    FillDocumentReceived();
                    //PopulatePinCode();
                    BindAttestation();
                    //GetFIMissingFields();
                    //ddlCitizenship_SelectedIndexChanged(this, EventArgs.Empty);

                    FillRequiredDataForPartialSave();
                    GetRelatedPersonPartialDataForCKYC();
                    GetCtrlPrsnPartialDataForCKYC();

                    //btnPartialSave.Visible = true;

                    cbNew.Visible = true;
                    cbNew.Checked = true;
                    cbUpdate.Visible = false;
                    //updflag();

                    txtKYCNumber.Visible = false;
                    lblKYCNumber.Visible = false;
                    chkAppDeclare1.Checked = true;
                    chkAppDeclare2.Checked = true;
                    btnSave.Visible = true;
                    btnPartialSave.Visible = false;
                    btnUpdate.Visible = false;
                    btnKYCUpdate.Visible = false;
                    btnPartialUpdate.Visible = true;
                }
                else if (Request.QueryString["status"].ToString() == "view")
                {
                    divchkDelRel.Visible = true;
                    divIdProof.Visible = false;

                    FillddlPageLoad();
                    FillCountry();
                    FillStates();
                    FillConstType();
                    FillDocumentReceived();
                    //PopulatePinCode();
                    BindAttestation();
                    //GetFIMissingFields();
                    //ddlCitizenship_SelectedIndexChanged(this, EventArgs.Empty);

                    FillRequiredDataForPartialSave();
                    GetRelatedPersonPartialDataForCKYC();
                    GetCtrlPrsnPartialDataForCKYC();

                    //btnPartialSave.Visible = true;

                    cbNew.Visible = true;
                    cbNew.Checked = true;
                    cbUpdate.Visible = false;
                    //updflag();

                    txtKYCNumber.Visible = false;
                    lblKYCNumber.Visible = false;
                    chkAppDeclare1.Checked = true;
                    chkAppDeclare2.Checked = true;
                    btnSave.Visible = false;
                    btnPartialSave.Visible = false;
                    btnUpdate.Visible = false;
                    btnKYCUpdate.Visible = false;
                    btnPartialUpdate.Visible = false;
                }

                //ddlOccupation_SelectedIndexChanged(this, EventArgs.Empty);
                if (Request.QueryString["status"].ToString() != "LMod" && Request.QueryString["status"].ToString() != "view")
                {
                    FillDistrictState(txtPinCode, txtDistrictname, ddlState);
                    FillDistrictState(ddlPinCode1, txtDistrict1, ddlState1);
                    //FillDistrictState(ddlPinCode2, txtDistrict2, ddlState2);
                    chkAppDeclare1.Checked = true;
                    chkAppDeclare2.Checked = true;
                    btnSave.Visible = true;
                    btnPartialSave.Visible = true;
                    btnUpdate.Visible = false;
                    btnKYCUpdate.Visible = false;
                    btnPartialUpdate.Visible = false;
                }
                if (ddlCertifiecopy.SelectedIndex == 0)
                {
                    divIdProof.Visible = false;

                }
                else if (ddlCertifiecopy.SelectedIndex == 1)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Certificate Of Incorporation No.";
                    txtPassNo.Visible = true;
                    txtPassNo.MaxLength = 60;
                    //txtPassNo.Focus();
                    //txtPassNo.Attributes.Remove("onblur");
                    //txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");
                }
                else if (ddlCertifiecopy.SelectedIndex == 2)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Registration Certificate No.";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 60;
                    //txtPassNo.Focus();
                    //txtPassNo.Attributes.Remove("onblur");
                }
                else if (ddlCertifiecopy.SelectedIndex == 3)
                {

                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Resolution of Board / Managing Committee No.";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 25;
                    //txtPassNo.Focus();
                    //txtPassNo.Attributes.Remove("onblur");
                    //txtPassNo.Attributes.Add("onblur", "return fnValidatePAN(this)");
                }
                else if (ddlCertifiecopy.SelectedIndex == 4)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Memorandum and Article of Association No.";
                    txtPassNo.Visible = true;
                    //txtPassNo.Focus();
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 25;
                    //txtPassNo.Attributes.Remove("onblur");
                }
                else if (ddlCertifiecopy.SelectedIndex == 5)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Official Valid Documents No.";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                    txtPassNo.MaxLength = 25;
                    //txtPassNo.Focus();
                    //txtPassNo.Text = "";
                    //txtPassNo.Attributes.Remove("onblur");
                    //txtPassNo.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                }
                else
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Simplified Measures Account - Document Type Code";
                    txtPassNo.Visible = true;
                    //txtPassNo.Focus();
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 25;
                    //txtPassNo.Attributes.Remove("onblur");
                }


                txtDatOfInc.Attributes.Add("readonly", "readonly");
                txtDtOfCom.Attributes.Add("readonly", "readonly");
                txtDate.Attributes.Add("readonly", "readonly");
                txtDateKYCver.Attributes.Add("readonly", "readonly");

                txtDistrictname.Attributes.Add("readonly", "readonly");
                txtDistrict1.Attributes.Add("readonly", "readonly");
                //txtDistrict2.Attributes.Add("readonly", "readonly");
                txtPinCode.Attributes.Add("readonly", "readonly");
                ddlPinCode1.Attributes.Add("readonly", "readonly");
                //ddlPinCode2.Attributes.Add("readonly", "readonly");

                txtConstitutionTypeothers.Enabled = false;
            }





            //ddlState2.Attributes.Add("readonly", "readonly");
            DataTable dt1, dt2 = new DataTable();
            if (Session["dsRel"] != null)
            {
                if (IsPostBack)
                {
                    //dt1 = (DataTable)Session["dsRel"];
                    //related = dt1.Rows[0]["SVFlag"].ToString();
                }
            }
            if (Session["dsCtrl"] != null)
            {
                if (IsPostBack)
                {
                    //dt2 = (DataTable)Session["dsCtrl"];
                    //controlling = dt2.Rows[0]["SVFlag"].ToString();
                }
            }
            //if (related == "P" && controlling == "" || related == "" && controlling == "C" || related == "P" && controlling == "C")
            //{
            //    btnSave.Enabled = false;
            //}
            //else
            //{
            //    btnSave.Enabled = true;
            //}

            if (txtTypeIdentiNo.Text != "")
            {
                //dvTINCntry.Visible = true;
                dvTINCntry.Attributes.Add("style", "display:block");
            }
            else
            {
                //dvTINCntry.Visible = false;
                dvTINCntry.Attributes.Add("style", "display:none");
            }

            if (Session["PSSubmit"] != null && Session["PSSubmit"].ToString() == "Y")
            {
                btnSave.Enabled = false;
                //btnPartialUpdate.Enabled = true;
            }
            else
            {
                btnSave.Enabled = true;
                //btnPartialSave.Enabled = true;
            }

            //txtDistrictname.Attributes.Add("readonly", "readonly");
            //txtDistrict1.Attributes.Add("readonly", "readonly");
            //txtDistrict2.Attributes.Add("readonly", "readonly");
            //txtPinCode.Attributes.Add("readonly", "readonly");
            //ddlPinCode1.Attributes.Add("readonly", "readonly");
            //ddlPinCode2.Attributes.Add("readonly", "readonly");

        }

        #region BindAttestation
        public void BindAttestation()
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                htParam.Clear();
                htParam.Add("@USERID", strUserId.Trim());
                dt = objDAL.GetDataTable("Prc_GetAttestation", htParam);
                if (dt.Rows.Count > 0)
                {
                    //txtEmpName.Text = Convert.ToString(dt.Rows[0]["kycEmpName"]);
                    //txtEmpCode.Text = Convert.ToString(dt.Rows[0]["kycEmpCode"]);
                    //txtEmpDesignation.Text = Convert.ToString(dt.Rows[0]["kycEmpDesi"]);
                    //txtEmpBranch.Text = Convert.ToString(dt.Rows[0]["kycEmpBranch"]);
                    txtInsName.Text = Convert.ToString(dt.Rows[0]["kycInstName"]);
                    txtInsCode.Text = Convert.ToString(dt.Rows[0]["kycInstCode"]);
                    //txtDateKYCver.Text = Convert.ToString(dt.Rows[0]["kycVerDate"]);

                    //txtEmpName.Enabled = false;
                    //txtEmpCode.Enabled = false;
                    //txtEmpDesignation.Enabled = false;
                    //txtEmpBranch.Enabled = false;
                    //txtInsName.Enabled = false;
                    //txtInsCode.Enabled = false;
                    //txtDateKYCver.Enabled = false;


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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "BindAttestation", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dt = null;
            }
        }
        #endregion

        #region chkAddRel_Checked'
        protected void chkAddRel_Checked(object sender, EventArgs e)
        {
            try
            {

                if (Request.QueryString["Status"] == "KMod")
                {

                }
                if (chkAddRel.Checked == true)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "OpenRelatedPersonPageAsEntity();", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "OpenEntityRelatedPersonPage();", true);

                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenRelatedPersonPage();", true);
                    lnkViewRel.Attributes.Add("style", "display:block");
                    lnkViewRel.Enabled = true;
                    lnkViewRel.Visible = true;
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
                    //objErr = new ErrorLog();
                    //objErr.LogErr(AppID, "CkycReg.aspx.cs", "chkAddRel_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }

        }
        #endregion

        #region SameIdentityProof_CheckedChanged'
        protected void SameIdentityProof_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if (Request.QueryString["Status"] == "KMod")
                //{

                //}
                if (chkSameAsPOI.Checked == true)
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenRelatedPersonPage();", true);
                    //lnkViewRel.Attributes.Add("style", "display:block");
                    //lnkViewRel.Enabled = true;
                    if (ddlCertifiecopy.SelectedIndex == 0)
                    {
                        ddlProofOfAddress.SelectedIndex = 0;
                        ddlProofOfAddress.Enabled = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select Proof of Identity');", true);
                    }
                    else
                    {
                        if (ddlCertifiecopy.SelectedValue == "PI01")
                        {
                            ddlProofOfAddress.SelectedValue = "PA01";
                            ddlProofOfAddress.Enabled = false;
                        }
                        else if (ddlCertifiecopy.SelectedValue == "PI02")
                        {
                            ddlProofOfAddress.SelectedValue = "PA02";
                            ddlProofOfAddress.Enabled = false;
                        }
                        else
                        {
                            ddlProofOfAddress.SelectedIndex = 0;
                            chkSameAsPOI.Checked = false;
                            ddlProofOfAddress.Enabled = true;
                            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenRelatedPersonPage();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Proof of address can not be same as above proof of identity because proof of address does not contain " + ddlCertifiecopy.SelectedItem.Text.ToString() + "');", true);
                        }
                    }
                    if (txtPassNoAdd.Enabled == true)
                    {
                        divAddProof.Visible = false;
                        txtPassNoAdd.Visible = false;
                        txtPassExpDateAdd.Visible = false;
                        lblPassportNoAdd.Visible = false;
                        llPassExpDateAdd.Visible = false;
                    }

                }
                else
                {
                    ddlProofOfAddress.SelectedIndex = 0;
                    ddlProofOfAddress.Enabled = true;
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
                    objErr.LogErr(AppID, "LegalEntityDtls.aspx.cs", "SameIdentityProof_CheckedChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }

        }
        #endregion




        #region lnkViewRel_Click
        protected void lnkViewRel_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["dsRel"] != null)
                {
                    DataTable DT = new DataTable();
                    DT = (DataTable)Session["dsRel"];
                    ViewState["DT"] = DT;

                    if (Request.QueryString["status"].ToString() == "Mod")
                    {
                        DataTable DTUpd = (DataTable)ViewState["DT"];
                        DT.Merge(DTUpd, true, MissingSchemaAction.Ignore);
                        DT.AcceptChanges();
                    }
                    if (DT.Rows.Count > 0)
                    {

                        if (Request.QueryString["status"].ToString() == "Reg")
                        {
                            //gvMemDtls.DataSource = null;
                            gvMemDtls.DataSource = DT;
                            gvMemDtls.DataBind();
                            gvMemDtls.Visible = true;
                            lblRelRecordShow.Visible = false;
                            chkAddRel.Enabled = false;   //Need to modify once checked
                            gvMemDtls.Columns[1].Visible = false;
                            lnkViewRel.Visible = true;  //Need to modify once checked
                        }
                        else if (Request.QueryString["status"].ToString() == "LMod")
                        {
                            //gvMemDtls.DataSource = null;
                            gvMemDtls.DataSource = DT;
                            gvMemDtls.DataBind();
                            gvMemDtls.Visible = true;
                            lblRelRecordShow.Visible = false;
                            chkAddRel.Enabled = false;
                            gvMemDtls.Columns[1].Visible = false;
                            lnkViewRel.Visible = true;
                        }
                        else
                        {
                            gvMemDtls.DataSource = DT;
                            gvMemDtls.DataBind();
                            gvMemDtls.Visible = true;
                            lblRelRecordShow.Visible = false;
                            chkAddRel.Enabled = false;
                            lnkViewRel.Visible = false;
                        }


                    }
                    else
                    {
                        lblRelRecordShow.Visible = true;
                        lblRelRecordShow.ForeColor = System.Drawing.Color.Red;
                        chkAddRel.Enabled = true;
                        chkAddRel.Checked = false;
                        lnkViewRel.Visible = false;
                    }
                }
                else
                {
                    lblRelRecordShow.Visible = true;
                    lblRelRecordShow.ForeColor = System.Drawing.Color.Red;
                    chkAddRel.Enabled = true;
                    chkAddRel.Checked = false;
                    lnkViewRel.Visible = false;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "relatieDetailsDsValidation", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region lnkViewCtrl_Click
        protected void lnkViewCtrl_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["dsCtrl"] != null)
                {
                    DataTable DT = new DataTable();
                    DT = (DataTable)Session["dsCtrl"];

                    ViewState["DTCtrl"] = DT;

                    if (Request.QueryString["status"].ToString() == "Mod")
                    {
                        DataTable DTUpd = (DataTable)ViewState["DTCtrl"];
                        DT.Merge(DTUpd, true, MissingSchemaAction.Ignore);
                        DT.AcceptChanges();
                    }
                    if (DT.Rows.Count > 0)
                    {

                        if (Request.QueryString["status"].ToString() == "Reg")
                        {

                        }
                        else if (Request.QueryString["status"].ToString() == "LMod")
                        {

                        }
                        else
                        {
                        }
                    }
                    else
                    {

                    }
                }
                else
                {

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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "relatieDetailsDsValidation", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion



        #region lnkdelete_Click
        protected void lnkdelete_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
                int i = Convert.ToInt32(clickedRow.RowIndex);
                //DataTable dt = (DataTable)ViewState["DT"];    //Commented by akash
                DataTable dt = (DataTable)Session["dsRel"]; //Added by akash
                DataRow dr = dt.Rows[i];
                string RelRefnNo = Convert.ToString(dr[1]);
                dt.Rows[i].Delete();
                dt.AcceptChanges();

                if (Request.QueryString["Status"] == "Mod")
                {
                    string refno = Request.QueryString["refno"].ToString().Trim();
                    htParam.Clear();
                    htParam.Add("@RegRefNo", refno);
                    htParam.Add("@RelRefNo", RelRefnNo);
                    htParam.Add("@ActionFlag", Request.QueryString["status"].ToString().Trim());
                    htParam.Add("@UserID", strUserId.ToString());
                }

                if (dt.Rows.Count > 0)
                {
                    gvMemDtls.DataSource = dt;
                    gvMemDtls.DataBind();
                    ViewState["DT"] = dt;
                    Session["dsRel"] = dt;
                    chkAddRel.Enabled = false;       //Added by akash
                    lnkViewRel.Visible = true;     //Added by akash
                    lblRelRecordShow.Visible = false;        //Added by akash
                }
                else
                {
                    gvMemDtls.DataSource = dt;
                    gvMemDtls.DataBind();
                    Session["dsRel"] = null;
                    chkAddRel.Enabled = true;       //Added by akash
                    lnkViewRel.Visible = false;     //Added by akash
                    lblRelRecordShow.Visible = true;        //Added by akash
                    lblRelRecordShow.ForeColor = System.Drawing.Color.Red;
                    chkAddRel.Checked = false;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "lnkdelete_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region lnkdelete_Click
        protected void lnkdeleteCtrl_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
                int i = Convert.ToInt32(clickedRow.RowIndex);
                //DataTable dt = (DataTable)ViewState["DTCtrl"];
                DataTable dt = (DataTable)Session["dsCtrl"];
                DataRow dr = dt.Rows[i];
                string RelRefnNo = Convert.ToString(dr[1]);
                dt.Rows[i].Delete();
                dt.AcceptChanges();

                if (Request.QueryString["Status"] == "Mod")
                {
                    string refno = Request.QueryString["refno"].ToString().Trim();
                    htParam.Clear();
                    htParam.Add("@RegRefNo", refno);
                    htParam.Add("@RelRefNo", RelRefnNo);
                    htParam.Add("@ActionFlag", Request.QueryString["status"].ToString().Trim());
                    htParam.Add("@UserID", strUserId.ToString());
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "lnkdelete_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region lnkView_Click
        protected void lnkView_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
                int i = Convert.ToInt32(clickedRow.RowIndex);
                DataTable dt = (DataTable)ViewState["DT"];

                DataRow dr = dt.Rows[i];

                string RelRefnNo = Convert.ToString(dr[1]);
                string refno = Request.QueryString["refno"].ToString().Trim();
                if (Request.QueryString["status"].ToString() == "Mod")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OpenRelatedPersonPageView(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "PMod")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OpenPartialRelatedPersonPageView(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "view")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OpenRelatedPersonPageViewNew(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "LMod")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OpenPartialRelatedPersonPageView(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "Reg")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OpenPartialRelatedPersonPageView(" + RelRefnNo + "," + refno + ");", true);
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "lnkView_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region lnkView_Click
        protected void lnkView1_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
                int i = Convert.ToInt32(clickedRow.RowIndex);
                DataTable dt = (DataTable)ViewState["DTCtrl"];

                DataRow dr = dt.Rows[i];

                string RelRefnNo = Convert.ToString(dr[1]);
                string refno = Request.QueryString["refno"].ToString().Trim();
                if (Request.QueryString["status"].ToString() == "Mod")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OpenControllingPersonPageView(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "PMod")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OpenPartialControllingPersonPageView(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "view")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OpenControllingPersonPageViewNew(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "LMod")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OpenPartialControllingPersonPageView(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "Reg")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OpenPartialControllingPersonPageView(" + RelRefnNo + "," + refno + ");", true);
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "lnkView_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region lnkEdit_Click
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
                int i = Convert.ToInt32(clickedRow.RowIndex);
                DataTable dt = (DataTable)ViewState["DT"];
                DataTable dtNew = (DataTable)Session["dsRel"];



                DataRow dr = dt.Rows[i];

                string RelRefnNo = Convert.ToString(dr[1]);

                string refno = "0";

                if (Request.QueryString["refno"] != null)
                {
                    refno = Request.QueryString["refno"].ToString().Trim();
                }
                //else
                //{
                //    string refno = "";
                //}

                if (Request.QueryString["status"].ToString() == "Mod")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OpenRelatedPersonPageEdit(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "PMod")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OpenPartialRelatedPersonPageEdit(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "LMod")
                {
                    //if (RelRefnNo == "")
                    //{
                    RelRefnNo = "0";
                    if (dtNew.Rows[i]["RelPersonAs"].ToString() == "I")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OpenPartialRelatedPersonPageEditNew(" + RelRefnNo + "," + refno + "," + i + ");", true);
                    }

                    if (dtNew.Rows[i]["RelPersonAs"].ToString() == "E")
                    {
                        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenPartialEntRelatedPersonPageEditNew(" + RelRefnNo + "," + refno + "," + i + ");", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "OpenPartialEntRelatedPersonPageEditNew(" + RelRefnNo + "," + refno + "," + i + ");", true);
                    }
                    //}
                    //else
                    //{
                    //   ScriptManager.RegisterStartupScript(this,this.GetType(), "alert", "OpenPartialRelatedPersonPageEdit(" + RelRefnNo + "," + refno + ");", true);
                    //}

                }
                else if (Request.QueryString["status"].ToString() == "Reg")
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenPartialRelatedPersonPageEdit(" + RelRefnNo + "," + refno + ");", true);
                    RelRefnNo = "0";
                    if (dtNew.Rows[i]["RelPersonAs"].ToString() == "I")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OpenPartialRelatedPersonPageEditNew(" + RelRefnNo + "," + refno + "," + i + ");", true);
                    }

                    if (dtNew.Rows[i]["RelPersonAs"].ToString() == "E")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "OpenPartialEntRelatedPersonPageEditNew(" + RelRefnNo + "," + refno + "," + i + ");", true);
                        //ScriptManager.RegisterStartupScript(this.GetType(), "alert", "OpenPartialEntRelatedPersonPageEditNew(" + RelRefnNo + "," + refno + "," + i + ");", true);
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "lnkEdit_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region lnkEdit_Click
        protected void lnkEditCtrl_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
                int i = Convert.ToInt32(clickedRow.RowIndex);
                DataTable dt = (DataTable)ViewState["DTCtrl"];

                DataRow dr = dt.Rows[i];

                string RelRefnNo = Convert.ToString(dr[1]);
                //string refno = Request.QueryString["refno"].ToString().Trim();

                string refno = "0";

                if (Request.QueryString["refno"] != null)
                {
                    refno = Request.QueryString["refno"].ToString().Trim();
                }

                if (Request.QueryString["status"].ToString() == "Mod")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OpenControllingPersonPageEdit(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "PMod")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OpenPartialControllingPersonPageEdit(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "LMod")
                {
                    RelRefnNo = "0";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OpenPartialControllingPersonPageEditNew(" + RelRefnNo + "," + refno + "," + i + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "Reg")
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenPartialControllingPersonPageEdit(" + RelRefnNo + "," + refno + ");", true);
                    RelRefnNo = "0";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "OpenPartialControllingPersonPageEditNew(" + RelRefnNo + "," + refno + "," + i + ");", true);
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "lnkEdit_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region DROPDOWN 'chkPerAddress'SELECTEDINDEXCHANGED EVENT
        protected void chkPerAddress_Checked(object sender, EventArgs e)
        {
            try
            {
                if (chkPerAddress.Checked == true)
                {
                    chkSameAsPOI.Enabled = true;

                    ddlProofOfAddress.Enabled = true;
                    txtPassNoAdd.Enabled = true;
                    txtPassExpDateAdd.Enabled = true;
                    txtAddressLine1.Enabled = true;
                    txtAddressLine2.Enabled = true;
                    txtAddressLine3.Enabled = true;
                    txtCity.Enabled = true;
                    ddlState.Enabled = true;
                    txtPinCode.Enabled = true;
                    txtDistrictname.Enabled = true;
                    ddlCountryCode.Enabled = true;
                }
                else
                {
                    chkSameAsPOI.Enabled = false;

                    ddlProofOfAddress.Enabled = false;
                    txtPassNoAdd.Enabled = false;
                    txtPassExpDateAdd.Enabled = false;
                    txtAddressLine1.Enabled = false;
                    txtAddressLine2.Enabled = false;
                    txtAddressLine3.Enabled = false;
                    txtCity.Enabled = false;
                    ddlState.Enabled = false;
                    txtPinCode.Enabled = false;
                    txtDistrictname.Enabled = false;
                    ddlCountryCode.Enabled = false;

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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "chkPerAddress_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region Fill Sub Occupation Type Details
        public void FillDistrictState(TextBox ddl1, TextBox ddl2, DropDownList ddl3)
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                htParam.Clear();
                htParam.Add("@PinCode", ddl1.Text.ToString());
                htParam.Add("@flag", System.DBNull.Value);
                dt = objDAL.GetDataTable("Prc_GetAddressCKYC", htParam);
                if (dt.Rows.Count > 0)
                {
                    //ddl2.DataSource = dt;
                    ddl2.Text = dt.Rows[0]["District"].ToString();
                    //ddl2.DataBind();
                    ddl3.DataSource = dt;
                    ddl3.DataTextField = "State_Name";
                    ddl3.DataValueField = "State_code";
                    ddl3.DataBind();
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "FillDistrictState", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;
                dt = null;
            }
        }
        #endregion


        #region DROPDOWN 'ddlProofOfAddress' SELECTEDINDEXCHANGED EVENT
        protected void ddlProofOfAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlProofOfAddress.SelectedValue == "99")
                {
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Other";
                    llPassExpDateAdd.Text = "Identification Number(Others)";
                    lblPassportNoAdd.Visible = true;
                    llPassExpDateAdd.Visible = true;
                    txtPassExpDateAdd.Visible = true;
                    divPassAdd.Visible = true;
                    txtPassOthrAdd.Visible = false;
                    txtPassNoAdd.Visible = true;
                    txtPassExpDateAdd.MaxLength = 75;
                }
                else
                {
                    divAddProof.Visible = false;
                    //lblPassportNoAdd.Text = "Document Name";
                    //llPassExpDateAdd.Text = "Identification Number";
                    txtPassExpDateAdd.Visible = false;
                    llPassExpDateAdd.Visible = false;
                    divPassAdd.Visible = false;
                    llPassExpDateAdd.Visible = false;
                    txtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = false;
                    txtPassNoAdd.Visible = false;
                    txtPassNoAdd.MaxLength = 15;
                    txtPassNoAdd.Attributes.Remove("onblur");
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ddlProofOfAddress_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        //#region DROPDOWN 'ddlPinCode' SELECTEDINDEXCHANGED EVENT
        //protected void ddlPinCode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string date;
        //        date = DateTime.Today.ToString("dd\\/MM\\/yyyy");
        //        FillDistrictState(ddlPinCode, ddlDistrict, ddlState);

        //        if (ddlAddressType.SelectedIndex != 0)
        //        {
        //            if (chkPerAddress.Checked == false)
        //            {
        //                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please check CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS')", true);
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please check current/permanent/overseas address details')", true);
        //                //chkPerAddress.Focus();
        //                ddlPinCode.SelectedIndex = 0;
        //                return;
        //            }
        //        }
        //        if (ddlAddressType.SelectedIndex == 0)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please select address type')", true);
        //            //ddlAddressType.Focus();
        //            ddlPinCode.SelectedIndex = 0;
        //            return;
        //        }
        //        if (ddlProofOfAddress.SelectedIndex == 0)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please select proof of address')", true);
        //            ddlPinCode.SelectedIndex = 0;
        //            return;
        //        }
        //        if (ddlProofOfAddress.SelectedIndex != 0)
        //        {
        //            if (ddlProofOfAddress.SelectedIndex == 1)
        //            {
        //                if (txtPassNoAdd.Text == "")
        //                {
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter passport number')", true);
        //                    txtPassNoAdd.Focus();
        //                    ddlPinCode.SelectedIndex = 0;
        //                    return;
        //                }
        //                if (txtPassExpDateAdd.Text == "")
        //                {
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter passport expiry date')", true);
        //                    txtPassExpDateAdd.Focus();
        //                    ddlPinCode.SelectedIndex = 0;
        //                    return;
        //                }
        //                if (txtPassExpDateAdd.Text != "")
        //                {
        //                    DateTime date1, date2;
        //                    date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //                    date2 = DateTime.ParseExact(txtPassExpDateAdd.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //                    if (date1 > date2)
        //                    {

        //                        //if (Convert.ToDateTime(date) > Convert.ToDateTime(txtPassExpDateAdd.Text))
        //                        //{
        //                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You cannot select past date as driving license expiry date')", true);
        //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('You cannot select past date as driving license expiry date')", true);
        //                        txtPassExpDateAdd.Focus();
        //                        ddlPinCode.SelectedIndex = 0;
        //                        return;
        //                    }
        //                }
        //            }

        //            if (ddlProofOfAddress.SelectedIndex == 2)
        //            {
        //                if (txtPassNoAdd.Text == "")
        //                {
        //                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter driving licence no')", true);
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter driving licence number')", true);
        //                    txtPassNoAdd.Focus();
        //                    ddlPinCode.SelectedIndex = 0;
        //                    return;
        //                }

        //                if (txtPassExpDateAdd.Text == "")
        //                {
        //                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter driving licence expiry date')", true);
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter driving licence expiry date')", true);
        //                    txtPassExpDateAdd.Focus();
        //                    ddlPinCode.SelectedIndex = 0;
        //                    return;
        //                }
        //                if (txtPassExpDateAdd.Text != "")
        //                {
        //                    DateTime date1, date2;
        //                    date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //                    date2 = DateTime.ParseExact(txtPassExpDateAdd.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //                    if (date1 > date2)
        //                    {
        //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('You cannot select past date as driving license expiry date')", true);
        //                        ddlPinCode.SelectedIndex = 0;
        //                        return;
        //                    }
        //                }
        //            }

        //            if (ddlProofOfAddress.SelectedIndex == 3)
        //            {
        //                if (txtPassNoAdd.Text == "")
        //                {
        //                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter UID(Aadhaar)')", true);
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter UID(aadhaar)')", true);
        //                    txtPassNoAdd.Focus();
        //                    ddlPinCode.SelectedIndex = 0;
        //                    return;
        //                }
        //            }
        //            if (ddlProofOfAddress.SelectedIndex == 4)
        //            {
        //                if (txtPassNoAdd.Text == "")
        //                {
        //                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter voter id card')", true);
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter voter id card')", true);
        //                    txtPassNoAdd.Focus();
        //                    ddlPinCode.SelectedIndex = 0;
        //                    return;
        //                }
        //            }
        //            if (ddlProofOfAddress.SelectedIndex == 5)
        //            {
        //                if (txtPassNoAdd.Text == "")
        //                {
        //                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter NREGA job card')", true);
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter NREGA job card')", true);
        //                    txtPassNoAdd.Focus();
        //                    ddlPinCode.SelectedIndex = 0;
        //                    return;
        //                }
        //            }
        //            if (ddlProofOfAddress.SelectedIndex == 6)
        //            {
        //                if (txtPassNoAdd.Text == "")
        //                {
        //                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter other no of proof of Address')", true);
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter other no of proof of Address')", true);
        //                    txtPassNoAdd.Focus();
        //                    ddlPinCode.SelectedIndex = 0;
        //                    return;
        //                }
        //            }
        //        }
        //        if (txtAddressLine1.Text == "")
        //        {
        //            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter permanent address line 1')", true);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter permanent address line 1')", true);
        //            txtAddressLine1.Focus();
        //            ddlPinCode.SelectedIndex = 0;
        //            return;
        //        }
        //        if (txtCity.Text == "")
        //        {
        //            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter permanent city/Town/Village')", true);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter permanent city/Town/Village')", true);
        //            txtCity.Focus();
        //            ddlPinCode.SelectedIndex = 0;
        //            return;
        //        }
        //        //if (ddlPinCode.SelectedIndex == 0 && chkTick.Checked == false)
        //        //{
        //        //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter permanent Pin/Post Code')", true);
        //        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter permanent Pin/Post Code')", true);
        //        //    ddlPinCode.Focus();
        //        //    ddlPinCode.SelectedIndex = 0;
        //        //    return;
        //        //}
        //        chkPerAddress.Enabled = false;
        //        ddlCountryCode.SelectedValue = "IN";
        //    }
        //    catch (Exception ex)
        //    {
        //        if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
        //        {
        //            Response.Redirect("~/ErrorSession.aspx");
        //        }
        //        else
        //        {
        //            objErr = new ErrorLog();
        //            objErr.LogErr(AppID, "CkycReg.aspx.cs", "ddlPinCode_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
        //        }
        //    }
        //}
        //#endregion

        #region DROPDOWN 'ddlPinCode1' SELECTEDINDEXCHANGED EVENT
        protected void ddlPinCode1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //FillDistrictState(ddlPinCode1, ddlDistrict1, ddlState1);
                //ddlCountryCode1.SelectedValue = "IN";
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ddlPinCode1_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region DROPDOWN 'ddlPinCode2' SELECTEDINDEXCHANGED EVENT
        protected void ddlPinCode2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //FillDistrictState(ddlPinCode2, ddlDistrict2, ddlState2);
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ddlPinCode2_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region 'btnCancel_Click' Event
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("CKYCSearch.aspx?status=LMod");
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "btnCancel_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion



        #region DROPDOWN 'chkCuurentAddress' SELECTEDINDEXCHANGED EVENT
        protected void chkCuurentAddress_Checked(object sender, EventArgs e)
        {
            try
            {
                // ViewState["District"] = txtDistrictname.Text;
                if (chkCuurentAddress.Checked == true)
                {
                    //FillDistrictState(ddlPinCode, ddlDistrict1, ddlState1);
                    //ddlProofOfAddress1.SelectedValue = ddlProofOfAddress.SelectedValue;
                    //ddlProofOfAddress1.Enabled = false;
                    chkLocalAddress.Checked = true;
                    txtLocAddLine1.Text = txtAddressLine1.Text;
                    txtLocAddLine1.Enabled = false;
                    txtLocAddLine2.Text = txtAddressLine2.Text;
                    txtLocAddLine2.Enabled = false;
                    txtLocAddLine3.Text = txtAddressLine3.Text;
                    txtLocAddLine3.Enabled = false;
                    txtCity1.Text = txtCity.Text;
                    txtCity1.Enabled = false;
                    ddlPinCode1.Text = txtPinCode.Text;

                    //ddlPinCode1.Enabled = false;
                    ddlPinCode1.Attributes.Add("readonly", "readonly");// = false;
                    btnsearchddlPinCode1.Enabled = false;
                    ddlCountryCode1.SelectedValue = ddlCountryCode.SelectedValue;
                    ddlCountryCode1.Enabled = false;
                    txtDistrict1.Text = txtDistrictname.Text;
                    //txtDistrict1.Enabled = false;
                    ddlState1.Enabled = false;
                    txtDistrict1.Attributes.Add("readonly", "readonly");
                    txtState1.Enabled = false;
                    //txtState1.Attributes.Add("readonly", "readonly");

                    txtDistrictname.Attributes.Add("readonly", "readonly");
                    txtPinCode.Attributes.Add("readonly", "readonly");

                    if (ddlCountryCode.SelectedValue == "IN")
                    {

                        dvState1.Visible = true;
                        txtState1.Visible = false;
                        ddlState1.SelectedValue = ddlState.SelectedValue;
                    }
                    else
                    {
                        dvState1.Visible = false;
                        txtState1.Visible = true;
                        txtState1.Text = txtState.Text;
                    }
                }
                else
                {
                    //ddlProofOfAddress1.SelectedIndex = 0;
                    //chkLocalAddress.Checked = true;
                    txtLocAddLine1.Text = string.Empty;
                    txtLocAddLine1.Enabled = true;
                    txtLocAddLine2.Text = string.Empty;
                    txtLocAddLine2.Enabled = true;
                    txtLocAddLine3.Text = string.Empty;
                    txtLocAddLine3.Enabled = true;
                    txtCity1.Text = string.Empty;
                    txtCity1.Enabled = true;
                    ddlCountryCode1.SelectedIndex = 0;
                    ddlCountryCode1.Enabled = true;
                    txtDistrict1.Text = "";
                    ddlPinCode1.Text = "";
                    //ddlPinCode1.Enabled = true;
                    //ddlPinCode1.Attributes.Remove("readonly");
                    btnsearchddlPinCode1.Enabled = true;
                    //ddlState1.SelectedItem.Text = "";
                    ddlState1.SelectedIndex = 0;
                    //added by ramesh on dated 21-05-2018
                    //ddlProofOfAddress1.Enabled = true;
                    //ddlState1.Enabled = true;
                    //end
                    //txtDistrict1.Attributes.Remove("readonly");
                    ddlState1.Enabled = true;
                    //ddlState1.Attributes.Remove("readonly");
                    txtState1.Enabled = true;
                    //txtState1.Attributes.Remove("readonly");
                    dvState1.Visible = true;
                    txtState1.Visible = false;
                    txtState1.Text = string.Empty;

                    //txtDistrictname.Attributes.Remove("readonly");
                    //txtPinCode.Attributes.Remove("readonly");

                    //if (ddlCountryCode.SelectedValue == "IN")
                    //{
                    //    dvState1.Visible = true;
                    //    txtState1.Visible = false;
                    //    ddlState1.SelectedValue = ddlState.SelectedValue;
                    //}
                    //else
                    //{
                    //    dvState1.Visible = false;
                    //    txtState1.Visible = true;
                    //    txtState1.Text = txtState.Text;
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
                    objErr = new ErrorLog();
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "chkCuurentAddress_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region DROPDOWN 'ChkUpdAddr' SELECTEDINDEXCHANGED EVENT
        protected void ChkUpdAddr_Checked(object sender, EventArgs e)
        {
            try
            {
                ddlProofOfAddress.Enabled = true;
                txtPassNoAdd.Enabled = true;

                txtAddressLine1.Enabled = true;
                txtAddressLine2.Enabled = true;
                txtAddressLine3.Enabled = true;
                txtCity.Enabled = true;
                //txtDistrictname.Enabled = true;
                //txtPinCode.Enabled = true;
                ddlState.Enabled = true;
                ddlCountryCode.Enabled = true;

                txtLocAddLine1.Enabled = true;
                txtLocAddLine2.Enabled = true;
                txtLocAddLine3.Enabled = true;
                //txtDistrict1.Enabled = true;
                //ddlPinCode1.Enabled = true;
                ddlState1.Enabled = true;
                ddlCountryCode1.Enabled = true;
                txtCity1.Enabled = true;
                //txtDistrict2.Enabled = true;
                //ddlPinCode2.Enabled = true;
                //ChkUpdAddr.Enabled = false;

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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ChkUpdAddr_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion


        #region gvMemDtls_RowDataBound Event
        protected void gvMemDtls_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRelRefNo = (Label)e.Row.FindControl("lblRelRefNo") as Label;
                Label lblRelTypVal = (Label)e.Row.FindControl("lblRelTypVal") as Label;
                Label lblNameVal = (Label)e.Row.FindControl("lblNameVal") as Label;
                Label lblMemDOBVal = (Label)e.Row.FindControl("lblMemDOBVal") as Label;
                Label lblMemGender = (Label)e.Row.FindControl("lblMemGender") as Label;

                //Label lblMemMrtVal = (Label)e.Row.FindControl("lblMemMrtVal") as Label;
                //Label lblMemCizVal = (Label)e.Row.FindControl("lblMemCizVal") as Label;
                //Label lblMemResiVal = (Label)e.Row.FindControl("lblMemResiVal") as Label;
                //Label lblMemOccVal = (Label)e.Row.FindControl("lblMemOccVal") as Label;
                LinkButton lnkView = (LinkButton)e.Row.FindControl("lnkView") as LinkButton;

                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit") as LinkButton;
                LinkButton lnkdelete = (LinkButton)e.Row.FindControl("lnkdelete") as LinkButton;

                if (Request.QueryString["Status"].ToString() == "LMod")
                {
                    //gvMemDtls.Columns[10].Visible = true;
                    lnkView.Visible = false;
                    lnkEdit.Visible = true;
                    lnkdelete.Visible = true;
                }
                else if (Request.QueryString["Status"].ToString() == "view")
                {
                    //gvMemDtls.Columns[10].Visible = true;
                    lnkView.Visible = true;
                    lnkEdit.Visible = false;
                    lnkdelete.Visible = false;
                }
                else if (Request.QueryString["Status"].ToString() == "Reg")
                {
                    //gvMemDtls.Columns[10].Visible = false;
                    //lnkView.Visible = false;
                    //lnkEdit.Visible = false;
                    //lnkdelete.Visible = false;
                    //gvMemDtls.Columns[10].Visible = true;
                    lnkView.Visible = false;
                    lnkEdit.Visible = true;
                    lnkdelete.Visible = true;
                }
                else
                {
                    //gvMemDtls.Columns[10].Visible = false;
                    lnkView.Visible = true;
                }
            }

        }
        #endregion

        #region gvMemDtls_RowDataBound Event
        protected void gvCtrlPrson_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRelRefNo = (Label)e.Row.FindControl("lblRelRefNo") as Label;
                Label lblRelTypVal = (Label)e.Row.FindControl("lblRelTypVal") as Label;
                Label lblNameVal = (Label)e.Row.FindControl("lblNameVal") as Label;
                Label lblMemDOBVal = (Label)e.Row.FindControl("lblMemDOBVal") as Label;
                Label lblMemGender = (Label)e.Row.FindControl("lblMemGender") as Label;

                Label lblMemMrtVal = (Label)e.Row.FindControl("lblMemMrtVal") as Label;
                Label lblMemCizVal = (Label)e.Row.FindControl("lblMemCizVal") as Label;
                Label lblMemResiVal = (Label)e.Row.FindControl("lblMemResiVal") as Label;
                Label lblMemOccVal = (Label)e.Row.FindControl("lblMemOccVal") as Label;
                LinkButton lnkView = (LinkButton)e.Row.FindControl("lnkView") as LinkButton;

                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit") as LinkButton;
                LinkButton lnkdelete = (LinkButton)e.Row.FindControl("lnkdelete") as LinkButton;

                if (Request.QueryString["Status"].ToString() == "LMod")
                {
                    //gvCtrlPrson.Columns[10].Visible = true;
                    lnkView.Visible = false;
                    lnkEdit.Visible = true;
                    lnkdelete.Visible = true;
                }
                else if (Request.QueryString["Status"].ToString() == "Reg")
                {
                    //gvCtrlPrson.Columns[10].Visible = false;
                    //lnkView.Visible = false;
                    //lnkEdit.Visible = false;
                    //lnkdelete.Visible = false;
                    //gvCtrlPrson.Columns[10].Visible = true;
                    lnkView.Visible = false;
                    lnkEdit.Visible = true;
                    lnkdelete.Visible = true;
                }
                else if (Request.QueryString["Status"].ToString() == "view")
                {
                    //gvCtrlPrson.Columns[10].Visible = true;
                    lnkView.Visible = true;
                    lnkEdit.Visible = false;
                    lnkdelete.Visible = false;
                }
                else
                {
                    //gvCtrlPrson.Columns[10].Visible = false;
                    lnkView.Visible = true;
                }


                //if (Request.QueryString["Status"].ToString() == "Reg")
                //{
                //    lnkView.Visible = false;
                //}
                //else if (Request.QueryString["Status"].ToString() == "LMod")
                //{
                //    lnkView.Visible = false;
                //}
                //else
                //{
                //    lnkView.Visible = true;
                //}
            }

        }
        #endregion

        //#region DROPDOWN 'ChkUpdRelated' SELECTEDINDEXCHANGED EVENT
        //protected void ChkUpdRelated_Checked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ChkUpdRelated.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
        //        {
        //            Response.Redirect("~/ErrorSession.aspx");
        //        }
        //        else
        //        {
        //            //objErr = new ErrorLog();
        //            //objErr.LogErr(AppID, "CkycReg.aspx.cs", "ChkUpdRelated_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
        //            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
        //        }
        //    }
        //}
        //#endregion

        //#region DROPDOWN 'ChkUpdControlling' SELECTEDINDEXCHANGED EVENT
        //protected void ChkUpdControlling_Checked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ChkUpdControlling.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
        //        {
        //            Response.Redirect("~/ErrorSession.aspx");
        //        }
        //        else
        //        {
        //            //objErr = new ErrorLog();
        //            //objErr.LogErr(AppID, "CkycReg.aspx.cs", "ChkUpdRelated_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
        //            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
        //        }
        //    }
        //}
        //#endregion

        #region METHOD "InitializeControl()"
        private void InitializeControls()
        {
            try
            {
                olng = new MultilingualManager("DefaultConn", "LegalEntityDtls.aspx", Session["UserLangNum"].ToString());
                //olng = new MultilingualManager("DefaultConn", "LegalEntityDtls.aspx", "01");

                lblAppType.Text = olng.GetItemDesc("lblAppType");
                lblRefNumber.Text = olng.GetItemDesc("lblRefNumber");
                lblKYCNumber.Text = olng.GetItemDesc("lblKYCNumber");
                lblNatureOfBuss.Text = olng.GetItemDesc("lblNatureOfBuss");

                lblKYCName.Text = olng.GetItemDesc("lblKYCName");
                lblDatOfInc.Text = olng.GetItemDesc("lblDatOfInc");
                lblDateOfCom.Text = olng.GetItemDesc("lblDateOfCom");
                lblPlaceOfIncor.Text = olng.GetItemDesc("lblPlaceOfIncor");
                lblCountrOfInc.Text = olng.GetItemDesc("lblCountrOfInc");
                //lblTypeIdentiNo.Text = olng.GetItemDesc("lblTypeIdentiNo");
                lblTINCountry.Text = olng.GetItemDesc("lblTINCountry");
                lblPanNo.Text = olng.GetItemDesc("lblPanNo");
                lblCertifiecopy.Text = olng.GetItemDesc("lblCertifiecopy");
                lblRiskCategory.Text = olng.GetItemDesc("lblRiskCategory");
                lblIdVerif.Text = olng.GetItemDesc("lblIdVerif");


                //LblPrefix.Text = olng.GetItemDesc("lblcategory");
                //LblFName.Text = olng.GetItemDesc("lblcategory");
                //LblMName.Text = olng.GetItemDesc("lblcategory");
                //LblLName.Text = olng.GetItemDesc("lblcategory");
                //lblName.Text = olng.GetItemDesc("lblName");
                //lblMaidenName.Text = olng.GetItemDesc("lblMaidenName");
                //lblFatherName.Text = olng.GetItemDesc("lblFatherName");
                //lblMotherName.Text = olng.GetItemDesc("lblMotherName");
                //lbldob.Text = olng.GetItemDesc("lbldob");
                //lblGender.Text = olng.GetItemDesc("lblGender");
                //lblOccupation.Text = olng.GetItemDesc("lblOccupation");
                //lblOccuSubType.Text = olng.GetItemDesc("lblOccuSubType");
                //lblMarStatus.Text = olng.GetItemDesc("lblMarStatus");
                //lblCitizenship.Text = olng.GetItemDesc("lblCitizenship");
                //lblResStatus.Text = olng.GetItemDesc("lblResStatus");
                //lblIsoCountryCodeOthr.Text = olng.GetItemDesc("lblIsoCountryCodeOthr");
                //lblIsoCountryCode2.Text = olng.GetItemDesc("lblIsoCountryCode2");
                //lblTaxIden.Text = olng.GetItemDesc("lblTaxIden");
                //lblPlace.Text = olng.GetItemDesc("lblPlace");
                //lblIsoContry.Text = olng.GetItemDesc("lblIsoContry");
                //lblProofOfIdentity11.Text = olng.GetItemDesc("lblProofOfIdentity11");
                //lblProof.Text = olng.GetItemDesc("lblProof");
                lblProofOfAddress.Text = olng.GetItemDesc("lblProofOfAddress");
                //lblProofOfAddress1.Text = olng.GetItemDesc("lblProofOfAddress");
                lblAddressLine1.Text = olng.GetItemDesc("lblAddressLine1");
                lblAddressLine2.Text = olng.GetItemDesc("lblAddressLine2");
                lblAddressLine3.Text = olng.GetItemDesc("lblAddressLine3");
                lblCity.Text = olng.GetItemDesc("lblCity");
                lblDistrict.Text = olng.GetItemDesc("lblDistrict");
                lblPinCode.Text = olng.GetItemDesc("lblPinCode");
                lblState.Text = olng.GetItemDesc("lblState");
                lblIsoCountryCode.Text = olng.GetItemDesc("lblIsoCountryCode");
                lblLocAddLine1.Text = olng.GetItemDesc("lblLocAddLine1");
                lblLocAddLine2.Text = olng.GetItemDesc("lblLocAddLine2");
                lblLocAddLine3.Text = olng.GetItemDesc("lblLocAddLine3");
                lblCity1.Text = olng.GetItemDesc("lblCity1");
                lblDistrict1.Text = olng.GetItemDesc("lblDistrict1");
                lblPin1.Text = olng.GetItemDesc("lblPin1");
                lblState1.Text = olng.GetItemDesc("lblState1");
                lblCountryCode1.Text = olng.GetItemDesc("lblCountryCode1");
                lblTelOff1.Text = olng.GetItemDesc("lblTelOff1");
                lblTelRes.Text = olng.GetItemDesc("lblTelRes");
                lblMobile.Text = olng.GetItemDesc("lblMobile");
                lblFax.Text = olng.GetItemDesc("lblFax");
                //lblpfemail.Text = olng.GetItemDesc("lblpfemail");
                lblRemarks.Text = olng.GetItemDesc("lblRemarks");
                //chkAppDeclare1.Text = olng.GetItemDesc("LblDECLARATION");
                lblDate.Text = olng.GetItemDesc("lblDate");
                lblPlace1.Text = olng.GetItemDesc("lblPlace1");
                lblDocRec.Text = olng.GetItemDesc("lblDocRec");
                lblKYCVerify.Text = olng.GetItemDesc("lblKYCVerify");
                lblDate3.Text = olng.GetItemDesc("lblAtstionDate");
                lblEmpName.Text = olng.GetItemDesc("lblEmpName");
                lblEmpCode.Text = olng.GetItemDesc("lblEmpCode");
                lblEmpDesignation.Text = olng.GetItemDesc("lblEmpDesignation");
                lblEmpBranch.Text = olng.GetItemDesc("lblEmpBranch");
                lblInsCode.Text = olng.GetItemDesc("lblInsCode");
                lblInsName.Text = olng.GetItemDesc("lblInsName");

                //lblpfPersonal1.Text = olng.GetItemDesc("lblpfPersonal1");
                //lblpfPersonal.Text = olng.GetItemDesc("lblpfPersonal");
                //lbltick.Text = olng.GetItemDesc("lbltick");
                //lblProofOfIdentity11.Text = olng.GetItemDesc("lblProofOfIdentity11");
                //lblpfofAddr1.Text = olng.GetItemDesc("lblpfofAddr1");
                lblpfofAddr2.Text = "PROOF OF ADDRESS (POA) ";
                lblDtlOfRtltpr.Text = "RELATED PERSONS";
                //lblDtlOfCtrlpr.Text = olng.GetItemDesc("lblDtlOfCtrlpr");
                lblRemarks.Text = "OTHER DETAILS";
                lblattstn.Text = olng.GetItemDesc("lblattstn");
                lbldec.Text = olng.GetItemDesc("lbldec");
                lblAttesOfc.Text = olng.GetItemDesc("lblAttesOfc");
                lblOfcuseOnly.Text = olng.GetItemDesc("lblOfcuseOnly");
                lblInsDtls.Text = olng.GetItemDesc("lblInsDtls");
                //lblContactDetails.Text = olng.GetItemDesc("lblContactDetails");
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "InitializeControls", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        //private void PopulatePinCode()
        //{
        //    try
        //    {
        //        objDAL = new DataAccessLayer("CKYCConnectionString");
        //        dt = new DataTable();
        //        htParam.Clear();

        //        htParam.Add("@PinCode", System.DBNull.Value);
        //        htParam.Add("@flag", "P");
        //        dt = objDAL.GetDataTable("Prc_GetAddressCKYC", htParam);
        //        if (dt.Rows.Count > 0)
        //        {
        //            ddlPinCode.DataSource = dt;
        //            ddlPinCode1.DataSource = dt;
        //            ddlPinCode2.DataSource = dt;
        //            ddlPinCode.DataTextField = "PinCode";
        //            ddlPinCode1.DataTextField = "PinCode";
        //            ddlPinCode2.DataTextField = "PinCode";
        //            ddlPinCode.DataBind();
        //            ddlPinCode1.DataBind();
        //            ddlPinCode2.DataBind();
        //            ddlPinCode.Items.Insert(0, new ListItem("Select", string.Empty));
        //            ddlPinCode1.Items.Insert(0, new ListItem("Select", string.Empty));
        //            ddlPinCode2.Items.Insert(0, new ListItem("Select", string.Empty));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
        //        {
        //            Response.Redirect("~/ErrorSession.aspx");
        //        }
        //        else
        //        {
        //            objErr = new ErrorLog();
        //            objErr.LogErr(AppID, "CkycReg.aspx.cs", "PopulatePinCode", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
        //        }
        //    }
        //    finally
        //    {
        //        dt = null;
        //    }
        //}

        protected void FillddlPageLoad()
        {
            //htParam.Clear();
            //htParam.Add("@LookupCode", "KEntConstTyp");
            //FillDropdowns("prc_getDDLLookUpData", htParam, ddlNatureOfBuss, "CKYCConnectionString", true);

            htParam.Clear();
            htParam.Add("@LookupCode", "KEntIdentTyp");

            htParam.Clear();
            htParam.Add("@LookupCode", "KEntPoI");
            FillDropdowns("prc_getDDLLookUpData", htParam, ddlCertifiecopy, "CKYCConnectionString", true);

            htParam.Clear();
            htParam.Add("@LookupCode", "KEntPoA");
            FillDropdowns("prc_getDDLLookUpData", htParam, ddlProofOfAddress, "CKYCConnectionString", true);
            //FillDropdowns("prc_getDDLLookUpData", htParam, ddlProofOfAddress1, "CKYCConnectionString", true);

            htParam.Clear();

        }

        protected void FillCountry()
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                htParam.Clear();
                dt = objDAL.GetDataTable("Prc_GetcountrycodeCKYC", htParam);
                if (dt.Rows.Count > 0)
                {
                    ddlCountrOfInc.DataSource = dt;
                    ddlCountrOfInc.DataTextField = "Country_Desc";
                    ddlCountrOfInc.DataValueField = "Country_CODE";
                    ddlCountrOfInc.DataBind();
                    ddlCountrOfInc.Items.Insert(0, new ListItem("Select", string.Empty));
                    ddlCountrOfInc.SelectedValue = "IN";

                    ddlTINCountry.DataSource = dt;
                    ddlTINCountry.DataTextField = "Country_Desc";
                    ddlTINCountry.DataValueField = "Country_CODE";
                    ddlTINCountry.DataBind();
                    ddlTINCountry.Items.Insert(0, new ListItem("Select", string.Empty));
                    //ddlTINCountry.SelectedValue = "IN";   //105

                    ddlCountryCode.DataSource = dt;
                    ddlCountryCode.DataTextField = "Country_Desc";
                    ddlCountryCode.DataValueField = "Country_CODE";
                    ddlCountryCode.DataBind();
                    ddlCountryCode.Items.Insert(0, new ListItem("Select", string.Empty));

                    ddlCountryCode1.DataSource = dt;
                    ddlCountryCode1.DataTextField = "Country_Desc";
                    ddlCountryCode1.DataValueField = "Country_CODE";
                    ddlCountryCode1.DataBind();
                    ddlCountryCode1.Items.Insert(0, new ListItem("Select", string.Empty));

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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "FillCountry", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;
                dt = null;
            }
        }

        protected void FillStates()
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                htParam.Clear();
                dt = objDAL.GetDataTable("Prc_GetStateCodeCKYC", htParam);
                if (dt.Rows.Count > 0)
                {
                    ddlState.DataSource = dt;
                    ddlState.DataTextField = "STATE_Desc";
                    ddlState.DataValueField = "STATE_CODE";
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, new ListItem("Select", string.Empty));

                    ddlState1.DataSource = dt;
                    ddlState1.DataTextField = "STATE_Desc";
                    ddlState1.DataValueField = "STATE_CODE";
                    ddlState1.DataBind();
                    ddlState1.Items.Insert(0, new ListItem("Select", string.Empty));

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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "FillCountry", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;
                dt = null;
            }
        }

        protected void FillDocumentReceived()
        {
            try
            {
                htParam.Clear();
                htParam.Add("@LookupCode", "DocReceived");
                htParam.Add("@ParamUsage", "KL");
                FillDropdowns("prc_getDDLLookUpData", htParam, ddlDocReceived, "CKYCConnectionString", true);

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
                    objErr.LogErr(AppID, "LegalEntityDtls.aspx.cs", "FillDocumentReceived", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;
                dt = null;
            }
        }


        protected void FillConstType()
        {
            try
            {
                htParam.Clear();
                htParam.Add("@LookupCode", "KConstTyp");
                htParam.Add("@ParamUsage", "KL");
                FillDropdowns("prc_getDDLLookUpData", htParam, ddlNatureOfBuss, "CKYCConnectionString", true);

                //if (ddlNatureOfBuss.Items.Count > 1)
                //{
                //    ddlNatureOfBuss.SelectedIndex = 2;
                //}

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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "FillConstType", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;
                dt = null;
            }
        }



        public void FillDropdowns(string strQuery, Hashtable htable, DropDownList ddl, string strDBKey, bool isSelect)
        {
            dt = new DataTable();
            objDAL = new DataAccessLayer("CKYCConnectionString");
            dt.Clear();
            dt = objDAL.GetDataTable(strQuery, htable, strDBKey);
            if (dt.Rows.Count > 0)
            {
                ddl.Items.Clear();
                ddl.DataSource = dt;
                ddl.DataTextField = "ParamDesc1";
                ddl.DataValueField = "ParamValue";
                ddl.DataBind();
            }
            if (isSelect)
                ddl.Items.Insert(0, new ListItem("Select", ""));
        }

        protected void GetModelData(object sender, EventArgs e)
        {
            if (ddlState.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select the state.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "OpenStateWindow('Flag1');", true);
                ddlCountryCode.SelectedValue = "IN";
            }

        }

        protected void GetModelData1(object sender, EventArgs e)
        {
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "openModal();", true);
            if (ddlState1.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select the state of Correspondence Address Details.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "OpenStateWindow1('Flag2');", true);
                ddlCountryCode1.SelectedValue = "IN";
            }
        }


        #region Partial Save & Save Events
        protected void btnPartialSave_Click(object sender, EventArgs e)
        {
            try
            {
                string Res;
                Res = "";//objVal.PersonalDtlsValidation(
                //chkNormal, chkSimplified, Chksmall, cboTitle, txtGivenName, txtLastName, cboTitle2, rbtFS, txtGivenName2, txtLastName2, cboTitle3, txtGivenName3,
                //txtLastName3, txtDOB, cboGender, ddlOccupation, ddlOccuSubType, ddlMaritalStatus, ddlCitizenship, ddlResStatus, ddlIsoCountryCodeOthr, null, "Candidate");

                if (txtRefNumber.Text.ToString() == "")
                {
                    Res = "Please enter FI Reference Number";
                }

                if (txtKYCName.Text.ToString() == "")
                {
                    Res = "Please enter entity name";
                }

                if (Res.Equals(""))
                {

                    #region relatedpersonDSvalidation
                    dt = new DataTable();
                    dt = (DataTable)Session["dsRel"];
                    ctrldt = new DataTable();
                    ctrldt = (DataTable)Session["dsCtrl"];


                    if (chkAddRel.Checked == true)
                    {
                        if (dt == null)
                        {
                            chkAddRel.Checked = false;
                            return;
                        }
                    }
                    #endregion

                    #region  Entity Partial Details


                    htParam.Clear();
                    htParam.Add("@FiRefNo", txtRefNumber.Text.Trim());
                    if (cbNew.Checked == true)
                    {
                        htParam.Add("@APP_TYPE", "01");
                    }
                    else
                    {
                        htParam.Add("@APP_TYPE", "02");
                    }
                    htParam.Add("@ACC_TYPE_FLG", "");

                    htParam.Add("@ACC_TYPE", "");
                    htParam.Add("@CompType", ddlNatureOfBuss.SelectedValue.Trim());
                    htParam.Add("@PAN", txtPanNo.Text.ToString());
                    htParam.Add("@EntName", txtKYCName.Text.Trim());
                    htParam.Add("@DtofIncorporation", txtDatOfInc.Text.Trim());
                    htParam.Add("@DtofCommencementofbusi", txtDtOfCom.Text.Trim());
                    htParam.Add("@PlaceofIncorportation", txtPlaceOfInc.Text.Trim());
                    htParam.Add("@CountryofIncorporation", ddlCountrOfInc.SelectedValue.Trim());
                    htParam.Add("@CountryOfRsidens", "");
                    htParam.Add("@IdentyType", "");
                    htParam.Add("@TAX_NUM", txtTypeIdentiNo.Text.Trim());
                    htParam.Add("@TINIssuingCountry", ddlTINCountry.SelectedValue);
                    htParam.Add("@NoOfControlPrsnOI", "");

                    htParam.Add("@IDENT_NUM_ID1", ddlCertifiecopy.SelectedValue);
                    htParam.Add("@IDNO", txtPassNo.Text.Trim());

                    if (chkPerAddress.Checked == true)
                    {
                        htParam.Add("@CnctType1", "P1");
                        htParam.Add("@PERM_TYPE", "");
                        htParam.Add("@PERM_LINE1", txtAddressLine1.Text.Trim());
                        htParam.Add("@PERM_LINE2", txtAddressLine2.Text.Trim());
                        htParam.Add("@PERM_LINE3", txtAddressLine3.Text.Trim());
                        htParam.Add("@PERM_CITY", txtCity.Text.Trim());
                        htParam.Add("@PERM_DIST", txtDistrictname.Text);
                        htParam.Add("@PERM_PIN", txtPinCode.Text);

                        if (ddlCountryCode.SelectedValue == "IN")
                        {
                            htParam.Add("@PERM_STATE", ddlState.SelectedValue);
                        }
                        else
                        {
                            htParam.Add("@PERM_STATE", txtState.Text.Trim());
                        }

                        //htParam.Add("@PERM_STATE", ddlState.SelectedValue);
                        htParam.Add("@PERM_COUNTRY", ddlCountryCode.SelectedValue);
                        if (chkSameAsPOI.Checked == true)
                        {
                            htParam.Add("@SameasPOIAddresFlagP1", "01");
                        }
                        else
                        {
                            htParam.Add("@SameasPOIAddresFlagP1", "");
                        }
                    }
                    else
                    {
                        htParam.Add("@CnctType1", "");
                        htParam.Add("@PERM_TYPE", System.DBNull.Value);
                        htParam.Add("@PERM_LINE1", System.DBNull.Value);
                        htParam.Add("@PERM_LINE2", System.DBNull.Value);
                        htParam.Add("@PERM_LINE3", System.DBNull.Value);
                        htParam.Add("@PERM_CITY", System.DBNull.Value);
                        htParam.Add("@PERM_DIST", System.DBNull.Value);
                        htParam.Add("@PERM_PIN", System.DBNull.Value);
                        htParam.Add("@PERM_STATE", System.DBNull.Value);
                        htParam.Add("@PERM_COUNTRY", System.DBNull.Value);
                    }

                    htParam.Add("@PERM_POA", ddlProofOfAddress.SelectedValue.Trim());

                    //if (chkPerAddress.Checked == true)
                    //{
                    //        htParam.Add("@PERM_IDNUMBAER", System.DBNull.Value);
                    //        htParam.Add("@PERM_IDEXPDT", System.DBNull.Value);
                    //        htParam.Add("@PERM_POAOTHERS", System.DBNull.Value);
                    //}
                    //else
                    //{
                    htParam.Add("@PERM_IDNUMBAER", System.DBNull.Value);
                    htParam.Add("@PERM_IDEXPDT", System.DBNull.Value);
                    htParam.Add("@PERM_POAOTHERS", System.DBNull.Value);
                    //}

                    if (chkCuurentAddress.Checked == true)
                    {
                        htParam.Add("@SameasCurrentAddresFlagM1", "01");
                    }
                    else
                    {
                        htParam.Add("@SameasCurrentAddresFlagM1", "");
                    }

                    if (chkLocalAddress.Checked == true)
                    {
                        htParam.Add("@CnctType2", "M1");
                        htParam.Add("@PERM_CORRES_SAMEFLAG", "01");//by meena 19/05/2017
                        htParam.Add("@CORRES_TYPE", "");
                        htParam.Add("@CORRES_POA", "");
                        htParam.Add("@CORRES_LINE1", txtLocAddLine1.Text);
                        htParam.Add("@CORRES_LINE2", txtLocAddLine2.Text);
                        htParam.Add("@CORRES_LINE3", txtLocAddLine3.Text);
                        htParam.Add("@CORRES_CITY ", txtCity1.Text.Trim());
                        htParam.Add("@CORRES_DIST", txtDistrict1.Text);
                        htParam.Add("@CORRES_PIN", ddlPinCode1.Text);

                        if (ddlCountryCode1.SelectedValue == "IN")
                        {
                            htParam.Add("@CORRES_STATE", ddlState1.SelectedValue);
                        }
                        else
                        {
                            htParam.Add("@CORRES_STATE", txtState1.Text.Trim());
                        }

                        //htParam.Add("@CORRES_STATE", ddlState1.SelectedValue);
                        htParam.Add("@CORRES_COUNTRY", ddlCountryCode1.SelectedValue);
                    }
                    else
                    {
                        htParam.Add("@CnctType2", "");
                        htParam.Add("@PERM_CORRES_SAMEFLAG", "02");//by meena 19/05/2017
                        htParam.Add("@CORRES_TYPE", System.DBNull.Value);
                        htParam.Add("@CORRES_POA", System.DBNull.Value);
                        htParam.Add("@CORRES_LINE1", System.DBNull.Value);
                        htParam.Add("@CORRES_LINE2", System.DBNull.Value);
                        htParam.Add("@CORRES_LINE3", System.DBNull.Value);
                        htParam.Add("@CORRES_CITY", System.DBNull.Value);
                        htParam.Add("@CORRES_DIST", System.DBNull.Value);
                        htParam.Add("@CORRES_PIN", System.DBNull.Value);
                        htParam.Add("@CORRES_STATE", System.DBNull.Value);
                        htParam.Add("@CORRES_COUNTRY", System.DBNull.Value);
                    }

                    htParam.Add("@SameasLocalAddressFlagJ1", "");

                    htParam.Add("@SameasLocalAddressFlagJ2", "");


                    htParam.Add("@CnctType3", "");
                    htParam.Add("@JURI_TYPE", System.DBNull.Value);
                    //htParam.Add("@CORRES_POA", ddlProofOfAddress1.SelectedValue);
                    htParam.Add("@JURI_LINE1", System.DBNull.Value);
                    htParam.Add("@JURI_LINE2", System.DBNull.Value);
                    htParam.Add("@JURI_LINE3", System.DBNull.Value);
                    htParam.Add("@JURI_CITY", System.DBNull.Value);
                    htParam.Add("@JURI_DIST", System.DBNull.Value);
                    htParam.Add("@JURI_PIN", System.DBNull.Value);
                    htParam.Add("@JURI_STATE", System.DBNull.Value);
                    htParam.Add("@JURI_COUNTRY", System.DBNull.Value);
                    htParam.Add("@OFF_STD_CODE", txtTelOff.Text.Trim());
                    htParam.Add("@RESI_STD_CODE", txtTelRes.Text.Trim());
                    htParam.Add("@MOB_CODE", txtMobile.Text.Trim());
                    htParam.Add("@FAX_CODE", txtFax1.Text);

                    htParam.Add("@OFF_TEL_NUM", txtTelOff2.Text);
                    htParam.Add("@RESI_TEL_NUM", txtTelRes2.Text);
                    htParam.Add("@FAX_NO", txtFax2.Text);
                    htParam.Add("@MOB_NUM", txtMobile2.Text);
                    htParam.Add("@EMAIL", txtemail.Text);
                    htParam.Add("@Remarks", txtRemarks.Text.Trim());
                    htParam.Add("@DEC_DATE", txtDate.Text.Trim());
                    htParam.Add("@DEC_PLACE", txtPlace.Text.Trim());
                    htParam.Add("@KYC_NAME", txtEmpName.Text.Trim());
                    htParam.Add("@KYC_EMPCODE", txtEmpCode.Text.Trim());
                    htParam.Add("@KYC_BRANCH", txtEmpBranch.Text.Trim());
                    htParam.Add("@KYC_DESIGNATION", txtEmpDesignation.Text.Trim());
                    htParam.Add("@KYC_DATE", txtDateKYCver.Text.Trim());
                    htParam.Add("@DOC_SUB", ddlDocReceived.SelectedValue);

                    htParam.Add("@ORG_NAME", txtInsName.Text.Trim());
                    htParam.Add("@ORG_CODE", txtInsCode.Text.Trim());
                    htParam.Add("@CreatedBy", strUserId.ToString());

                    //htParam.Add("@TKYCNO", "");
                    //htParam.Add("@Uniqueno", obj.ToString());
                    //htParam.Add("@Usages", "W");
                    //htParam.Add("@Mode", Request.QueryString["Status"].ToString());//Reg or Mod

                    //if (Request.QueryString["Status"].ToString() == "PMod")
                    //{
                    //    htParam.Add("@PSTempRefNo", txtRefNumber.Text.ToString());
                    //    objds = objDAL.GetDataSet("prc_updatekycPartialdtls", htParam, "STAGINGConnectionString");

                    //}
                    //else if (Request.QueryString["Status"].ToString() == "Reg")
                    //{ 
                    //objds = objDAL.GetDataSet("Prc_InsCkycPartialDtls", htParam, "STAGINGConnectionString");
                    //}

                    //if (objds.Tables.Count > 0)
                    //{
                    //    if (objds.Tables[0].Rows.Count > 0)
                    //    {
                    //        PSTempRefNo = objds.Tables[0].Rows[0]["PSTempRefNo"].ToString();
                    //    }
                    //}
                    objDAL = new DataAccessLayer("STAGINGConnectionString");
                    PSTempRefNo = (objDAL.ExecuteScalar("Prc_InsPartialEntkycdtls_Web", htParam)).ToString(); //Prc_InsCkycPartialDtls

                    #endregion

                    #region Related Partial Details

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                htParam.Clear();
                                htParam.Add("@FiRefNo", dt.Rows[i]["FiRefNo"]);
                                htParam.Add("@PSTempRefNo", PSTempRefNo);
                                if (chkAddRel.Checked == true)
                                {
                                    htParam.Add("@AddDelFlagRel", "01");
                                }
                                else
                                {
                                    htParam.Add("@AddDelFlagRel", System.DBNull.Value);
                                }

                                htParam.Add("@RelatedPrsnKYCNo", dt.Rows[i]["RelatedPrsnKYCNo"]);
                                htParam.Add("@RelationType", dt.Rows[i]["RelationType"]);
                                htParam.Add("@RelCKYCNO", dt.Rows[i]["RelatedPrsnKYCNo"]);

                                //--
                                htParam.Add("@RelPersonAs", dt.Rows[i]["RelPersonAs"]);

                                htParam.Add("@RelEntName", dt.Rows[i]["RelEntName"]);
                                htParam.Add("@RelDtofIncorporation", dt.Rows[i]["RelDtofIncorporation"]);
                                htParam.Add("@RelDtofCommencementofbusi", dt.Rows[i]["RelDtofCommencementofbusi"]);
                                htParam.Add("@RelPlaceofIncorportation", dt.Rows[i]["RelPlaceofIncorportation"]);
                                htParam.Add("@RelCountryofIncorporation", dt.Rows[i]["RelCountryofIncorporation"]);
                                htParam.Add("@RelCountryofResAsperTaxLaws", dt.Rows[i]["RelCountryofResAsperTaxLaws"]);
                                htParam.Add("@RelIdType", dt.Rows[i]["RelIdType"]);
                                htParam.Add("@RelPAN", dt.Rows[i]["RelPAN"]);
                                htParam.Add("@RelTINIdNo", dt.Rows[i]["RelTINIdNo"]);
                                htParam.Add("@RelTINCountry", dt.Rows[i]["RelTINCountry"]);

                                htParam.Add("@RelTelSTDCode", dt.Rows[i]["RelTelSTDCode"]);
                                htParam.Add("@RelTelNo", dt.Rows[i]["RelTelNo"]);
                                htParam.Add("@RelOfficeTelSTDCode", dt.Rows[i]["RelOfficeTelSTDCode"]);
                                htParam.Add("@RelOfficeTelNo", dt.Rows[i]["RelOfficeTelNo"]);
                                htParam.Add("@RelMobCode", dt.Rows[i]["RelMobCode"]);
                                htParam.Add("@RelMobileNo", dt.Rows[i]["RelMobileNo"]);
                                htParam.Add("@RelFaxNoCode", dt.Rows[i]["RelFaxNoCode"]);
                                htParam.Add("@RelFaxNo", dt.Rows[i]["RelFaxNo"]);
                                htParam.Add("@RelEmailID", dt.Rows[i]["RelEmailID"]);
                                //--

                                htParam.Add("@PrefixRel", dt.Rows[i]["PrefixRel"]);
                                htParam.Add("@FNameRel", dt.Rows[i]["FNameRel"]);
                                htParam.Add("@MNameRel", dt.Rows[i]["MNameRel"]);
                                htParam.Add("@LNameRel", dt.Rows[i]["LNameRel"]);
                                htParam.Add("@MaidPrefixRel", dt.Rows[i]["MaidPrefixRel"]);
                                htParam.Add("@MaidFNameRel", dt.Rows[i]["MaidFNameRel"]);
                                htParam.Add("@MaidMNameRel", dt.Rows[i]["MaidMNameRel"]);
                                htParam.Add("@MaidLNameRel", dt.Rows[i]["MaidLNameRel"]);
                                htParam.Add("@FSFlagRel", dt.Rows[i]["FSFlagRel"]);
                                htParam.Add("@FatherPrefixRel", dt.Rows[i]["FatherPrefixRel"]);
                                htParam.Add("@FatherFNameRel", dt.Rows[i]["FatherFNameRel"]);
                                htParam.Add("@FatherMNameRel", dt.Rows[i]["FatherMNameRel"]);
                                htParam.Add("@FatherLNameRel", dt.Rows[i]["FatherLNameRel"]);
                                htParam.Add("@MotherPrefixRel", dt.Rows[i]["MotherPrefixRel"]);
                                htParam.Add("@MotherFNameRel", dt.Rows[i]["MotherFNameRel"]);
                                htParam.Add("@MotherMNameRel", dt.Rows[i]["MotherMNameRel"]);
                                htParam.Add("@MotherLNameRel", dt.Rows[i]["MotherLNameRel"]);
                                htParam.Add("@DOBRel", dt.Rows[i]["DOBRel"]);
                                htParam.Add("@GenderRel", dt.Rows[i]["GenderRel"]);
                                htParam.Add("@MaritalStatusRel", dt.Rows[i]["MaritalStatusRel"]);
                                htParam.Add("@CitizenshipRel", dt.Rows[i]["CitizenshipRel"]);
                                htParam.Add("@ResiStatusRel", dt.Rows[i]["ResiStatusRel"]);
                                htParam.Add("@OccuTypeRel", dt.Rows[i]["OccuTypeRel"]);

                                htParam.Add("@OccuSubTypeRel", dt.Rows[i]["OccuSubTypeRel"]);

                                htParam.Add("@ResForTaxFlagRel", dt.Rows[i]["ResForTaxFlagRel"]);
                                htParam.Add("@ISOCountryCodeRel", dt.Rows[i]["ISOCountryCodeRel"]);
                                htParam.Add("@TaxIDNumberRel", dt.Rows[i]["TaxIDNumberRel"]);
                                htParam.Add("@BirthCityRel", dt.Rows[i]["BirthCityRel"]);
                                htParam.Add("@ISOBirthPlaceCodeRel", dt.Rows[i]["ISOBirthPlaceCodeRel"]);
                                htParam.Add("@IdType", dt.Rows[i]["IdType"]);
                                htParam.Add("@IdNumber", dt.Rows[i]["IdNumber"]);
                                htParam.Add("@IDExpDate", dt.Rows[i]["IdExpDate"]);
                                htParam.Add("@IDName", dt.Rows[i]["IdName"]);

                                htParam.Add("@CnctTypeRel", "P1");
                                htParam.Add("@PER_ADDTYPE", dt.Rows[i]["AdrTypeRel"]);
                                htParam.Add("@PER_ADDPROOF", dt.Rows[i]["AdrProfRel"]);
                                htParam.Add("@PER_ADDLINE1", dt.Rows[i]["Adr1Rel"]);
                                htParam.Add("@PER_ADDLINE2", dt.Rows[i]["Adr2Rel"]);
                                htParam.Add("@PER_ADDLINE3", dt.Rows[i]["Adr3Rel"]);
                                htParam.Add("@PER_CITY", dt.Rows[i]["CityRel"]);
                                htParam.Add("@PER_DISTRICT", dt.Rows[i]["DistrictRel"]);
                                htParam.Add("@PER_PIN", dt.Rows[i]["PostCodeRel"]);
                                htParam.Add("@PER_STATECODE", dt.Rows[i]["StateCodeRel"]);
                                htParam.Add("@PER_COUNTRY_CODE", dt.Rows[i]["CntryCodeRel"]);

                                //added by daksh
                                htParam.Add("@CnctTypeRel1", "M1");
                                htParam.Add("@corPER_ADDTYPE", dt.Rows[i]["AdrTypeRel1"]);
                                //htParam.Add("@PER_ADDPROOF", dt.Rows[i]["AdrProfRel1"]);
                                htParam.Add("@corPER_ADDLINE1", dt.Rows[i]["Adr1Rel1"]);
                                htParam.Add("@corPER_ADDLINE2", dt.Rows[i]["Adr2Rel1"]);
                                htParam.Add("@corPER_ADDLINE3", dt.Rows[i]["Adr3Rel1"]);
                                htParam.Add("@corPER_CITY", dt.Rows[i]["CityRel1"]);
                                htParam.Add("@corPER_DISTRICT", dt.Rows[i]["DistrictRel1"]);
                                htParam.Add("@corPER_PIN", dt.Rows[i]["PostCodeRel1"]);
                                htParam.Add("@corPER_STATECODE", dt.Rows[i]["StateCodeRel1"]);
                                htParam.Add("@corPER_COUNTRY_CODE", dt.Rows[i]["CntryCodeRel1"]);

                                htParam.Add("@CnctTypeRel2", "J1");
                                htParam.Add("@jurPER_ADDTYPE", dt.Rows[i]["AdrTypeRel2"]);
                                //htParam.Add("@PER_ADDPROOF", dt.Rows[i]["AdrProfRel"]);
                                htParam.Add("@jurPER_ADDLINE1", dt.Rows[i]["Adr1Rel2"]);
                                htParam.Add("@jurPER_ADDLINE2", dt.Rows[i]["Adr2Rel2"]);
                                htParam.Add("@jurPER_ADDLINE3", dt.Rows[i]["Adr3Rel2"]);
                                htParam.Add("@jurPER_CITY", dt.Rows[i]["CityRel2"]);
                                htParam.Add("@jurPER_DISTRICT", dt.Rows[i]["DistrictRel2"]);
                                htParam.Add("@jurPER_PIN", dt.Rows[i]["PostCodeRel2"]);
                                htParam.Add("@jurPER_STATECODE", dt.Rows[i]["StateCodeRel2"]);
                                htParam.Add("@jurPER_COUNTRY_CODE", dt.Rows[i]["CntryCodeRel2"]);

                                htParam.Add("@SameasPOIAddresFlagP1", dt.Rows[i]["SameasPOIAddresFlagP1"]);
                                htParam.Add("@SameasCurrentAddresFlagM1", dt.Rows[i]["SameasCurrentAddresFlagM1"]);
                                htParam.Add("@SameasLocalAddressFlagJ1", dt.Rows[i]["SameasLocalAddressFlagJ1"]);
                                htParam.Add("@SameasLocalAddressFlagJ2", dt.Rows[i]["SameasLocalAddressFlagJ2"]);

                                //end

                                htParam.Add("@AddIdType", dt.Rows[i]["AddIdTypeRel"]);
                                htParam.Add("@PERM_IDNUMBAER", dt.Rows[i]["AddIdNumberRel"]);
                                htParam.Add("@PERM_IDEXPDT", dt.Rows[i]["AddIdExpDateRel"]);
                                htParam.Add("@PERM_POAOTHERS", dt.Rows[i]["AddIdNameRel"]);
                                htParam.Add("@DecDateRel", dt.Rows[i]["DecDateRel"]);
                                htParam.Add("@DecPlaceRel", dt.Rows[i]["DecPlaceRel"]);
                                htParam.Add("@kycEmpNameRel", dt.Rows[i]["kycEmpNameRel"]);
                                htParam.Add("@kycEmpCodeRel", dt.Rows[i]["kycEmpCodeRel"]);
                                htParam.Add("@kycEmpBranchRel", dt.Rows[i]["kycEmpBranchRel"]);
                                htParam.Add("@kycEmpDesiRel", dt.Rows[i]["kycEmpDesiRel"]);
                                htParam.Add("@kycVerDateRel", dt.Rows[i]["kycVerDateRel"]);
                                htParam.Add("@kycCertDocRel", "01");
                                htParam.Add("@kycInstNameRel", dt.Rows[i]["kycInstNameRel"]);
                                htParam.Add("@kycInstCodeRel", dt.Rows[i]["kycInstCodeRel"]);
                                htParam.Add("@CreateBy", strUserId.ToString());
                                objDAL.ExecuteNonQuery("prc_InsKycRelPrsnPartialDtls", htParam);
                                Session["dsRel"] = null;
                            }
                        }
                    }
                    #endregion


                    #region Partial save Controlling Person Details

                    if (ctrldt != null)
                    {
                        if (ctrldt.Rows.Count > 0)
                        {
                            for (int i = 0; i < ctrldt.Rows.Count; i++)
                            {

                                htParam.Clear();
                                htParam.Add("@PSTempRefNo", PSTempRefNo);
                                htParam.Add("@FiRefNo", ctrldt.Rows[i]["FiRefNo"]);

                                htParam.Add("@AddDelFlagRel ", "");

                                htParam.Add("@RelatedPrsnKYCNo", ctrldt.Rows[i]["RelatedPrsnKYCNo"]);
                                htParam.Add("@RelationType", ctrldt.Rows[i]["RelationType"]);
                                htParam.Add("@RelCKYCNO", dt.Rows[i]["RelatedPrsnKYCNo"]);
                                htParam.Add("@PrefixRel", ctrldt.Rows[i]["PrefixRel"]);
                                htParam.Add("@FNameRel", ctrldt.Rows[i]["FNameRel"]);
                                htParam.Add("@MNameRel", ctrldt.Rows[i]["MNameRel"]);
                                htParam.Add("@LNameRel", ctrldt.Rows[i]["LNameRel"]);
                                htParam.Add("@MaidPrefixRel", ctrldt.Rows[i]["MaidPrefixRel"]);
                                htParam.Add("@MaidFNameRel", ctrldt.Rows[i]["MaidFNameRel"]);
                                htParam.Add("@MaidMNameRel", ctrldt.Rows[i]["MaidMNameRel"]);
                                htParam.Add("@MaidLNameRel", ctrldt.Rows[i]["MaidLNameRel"]);
                                htParam.Add("@FSFlagRel", ctrldt.Rows[i]["FSFlagRel"]);
                                htParam.Add("@FatherPrefixRel", ctrldt.Rows[i]["FatherPrefixRel"]);
                                htParam.Add("@FatherFNameRel", ctrldt.Rows[i]["FatherFNameRel"]);
                                htParam.Add("@FatherMNameRel", ctrldt.Rows[i]["FatherMNameRel"]);
                                htParam.Add("@FatherLNameRel", ctrldt.Rows[i]["FatherLNameRel"]);
                                htParam.Add("@MotherPrefixRel", ctrldt.Rows[i]["MotherPrefixRel"]);
                                htParam.Add("@MotherFNameRel", ctrldt.Rows[i]["MotherFNameRel"]);
                                htParam.Add("@MotherMNameRel", ctrldt.Rows[i]["MotherMNameRel"]);
                                htParam.Add("@MotherLNameRel", ctrldt.Rows[i]["MotherLNameRel"]);
                                htParam.Add("@DOBRel", ctrldt.Rows[i]["DOBRel"]);
                                htParam.Add("@GenderRel", ctrldt.Rows[i]["GenderRel"]);
                                htParam.Add("@MaritalStatusRel", ctrldt.Rows[i]["MaritalStatusRel"]);
                                htParam.Add("@CitizenshipRel", ctrldt.Rows[i]["CitizenshipRel"]);
                                htParam.Add("@ResiStatusRel", ctrldt.Rows[i]["ResiStatusRel"]);
                                htParam.Add("@OccuTypeRel", ctrldt.Rows[i]["OccuTypeRel"]);

                                htParam.Add("@ResForTaxFlagRel", ctrldt.Rows[i]["ResForTaxFlagRel"]);
                                htParam.Add("@ISOCountryCodeRel", ctrldt.Rows[i]["ISOCountryCodeRel"]);
                                htParam.Add("@TaxIDNumberRel", ctrldt.Rows[i]["TaxIDNumberRel"]);
                                htParam.Add("@BirthCityRel", ctrldt.Rows[i]["BirthCityRel"]);
                                htParam.Add("@ISOBirthPlaceCodeRel", ctrldt.Rows[i]["ISOBirthPlaceCodeRel"]);

                                htParam.Add("@IdType", ctrldt.Rows[i]["IdType"]);
                                htParam.Add("@IdNumber", ctrldt.Rows[i]["IdNumber"]);
                                htParam.Add("@IDExpDate", ctrldt.Rows[i]["IdExpDate"]);
                                htParam.Add("@IDName", ctrldt.Rows[i]["IdName"]);

                                htParam.Add("@CnctTypeRel", "P1");
                                htParam.Add("@PER_ADDTYPE", ctrldt.Rows[i]["AdrTypeRel"]);
                                htParam.Add("@PER_ADDPROOF", ctrldt.Rows[i]["AdrProfRel"]);
                                htParam.Add("@PER_ADDLINE1", ctrldt.Rows[i]["Adr1Rel"]);
                                htParam.Add("@PER_ADDLINE2", ctrldt.Rows[i]["Adr2Rel"]);
                                htParam.Add("@PER_ADDLINE3", ctrldt.Rows[i]["Adr3Rel"]);
                                htParam.Add("@PER_CITY", ctrldt.Rows[i]["CityRel"]);
                                htParam.Add("@PER_DISTRICT", ctrldt.Rows[i]["DistrictRel"]);
                                htParam.Add("@PER_PIN", ctrldt.Rows[i]["PostCodeRel"]);
                                htParam.Add("@PER_STATECODE", ctrldt.Rows[i]["StateCodeRel"]);
                                htParam.Add("@PER_COUNTRY_CODE", ctrldt.Rows[i]["CntryCodeRel"]);

                                //added by daksh
                                htParam.Add("@CnctTypeRel1", "M1");
                                htParam.Add("@corPER_ADDTYPE", ctrldt.Rows[i]["AdrTypeRel1"]);
                                //htParam.Add("@PER_ADDPROOF", dtRel.Rows[i]["AdrProfRel1"]);
                                htParam.Add("@corPER_ADDLINE1", ctrldt.Rows[i]["Adr1Rel1"]);
                                htParam.Add("@corPER_ADDLINE2", ctrldt.Rows[i]["Adr2Rel1"]);
                                htParam.Add("@corPER_ADDLINE3", ctrldt.Rows[i]["Adr3Rel1"]);
                                htParam.Add("@corPER_CITY", ctrldt.Rows[i]["CityRel1"]);
                                htParam.Add("@corPER_DISTRICT", ctrldt.Rows[i]["DistrictRel1"]);
                                htParam.Add("@corPER_PIN", ctrldt.Rows[i]["PostCodeRel1"]);
                                htParam.Add("@corPER_STATECODE", ctrldt.Rows[i]["StateCodeRel1"]);
                                htParam.Add("@corPER_COUNTRY_CODE", ctrldt.Rows[i]["CntryCodeRel1"]);

                                htParam.Add("@CnctTypeRel2", "J1");
                                htParam.Add("@jurPER_ADDTYPE", ctrldt.Rows[i]["AdrTypeRel2"]);
                                //htParam.Add("@PER_ADDPROOF", ctrldt.Rows[i]["AdrProfRel"]);
                                htParam.Add("@jurPER_ADDLINE1", ctrldt.Rows[i]["Adr1Rel2"]);
                                htParam.Add("@jurPER_ADDLINE2", ctrldt.Rows[i]["Adr2Rel2"]);
                                htParam.Add("@jurPER_ADDLINE3", ctrldt.Rows[i]["Adr3Rel2"]);
                                htParam.Add("@jurPER_CITY", ctrldt.Rows[i]["CityRel2"]);
                                htParam.Add("@jurPER_DISTRICT", ctrldt.Rows[i]["DistrictRel2"]);
                                htParam.Add("@jurPER_PIN", ctrldt.Rows[i]["PostCodeRel2"]);
                                htParam.Add("@jurPER_STATECODE", ctrldt.Rows[i]["StateCodeRel2"]);
                                htParam.Add("@jurPER_COUNTRY_CODE", ctrldt.Rows[i]["CntryCodeRel2"]);

                                htParam.Add("@SameasPOIAddresFlagP1", ctrldt.Rows[i]["SameasPOIAddresFlagP1"]);
                                htParam.Add("@SameasCurrentAddresFlagM1", ctrldt.Rows[i]["SameasCurrentAddresFlagM1"]);
                                htParam.Add("@SameasLocalAddressFlagJ1", ctrldt.Rows[i]["SameasLocalAddressFlagJ1"]);
                                htParam.Add("@SameasLocalAddressFlagJ2", ctrldt.Rows[i]["SameasLocalAddressFlagJ2"]);

                                //end


                                htParam.Add("@AddIdType", ctrldt.Rows[i]["AddIdTypeRel"]);
                                htParam.Add("@PERM_IDNUMBAER", ctrldt.Rows[i]["AddIdNumberRel"]);
                                htParam.Add("@PERM_IDEXPDT", ctrldt.Rows[i]["AddIdExpDateRel"]);
                                htParam.Add("@PERM_POAOTHERS", ctrldt.Rows[i]["AddIdNameRel"]);

                                //New to add contact details start
                                htParam.Add("@OccuSubTypeRel", ctrldt.Rows[i]["OccuSubTypeRel"]);
                                htParam.Add("@TelSTDCodeRel", ctrldt.Rows[i]["TelSTDCode"]);
                                htParam.Add("@TelNoRel", ctrldt.Rows[i]["TelNo"]);
                                htParam.Add("@OfficeTelSTDCodeRel", ctrldt.Rows[i]["OfficeTelSTDCode"]);
                                htParam.Add("@OfficeTelNoRel", ctrldt.Rows[i]["OfficeTelNo"]);
                                htParam.Add("@MobCodeRel", ctrldt.Rows[i]["MobCode"]);
                                htParam.Add("@MobileNoRel", ctrldt.Rows[i]["MobileNo"]);
                                htParam.Add("@FaxNoCodeRel", ctrldt.Rows[i]["FaxNoCode"]);
                                htParam.Add("@FaxNoRel", ctrldt.Rows[i]["FaxNo"]);
                                htParam.Add("@EmailIDRel", ctrldt.Rows[i]["EMAILID"]);
                                //New to add contact details end

                                htParam.Add("@DecDateRel", ctrldt.Rows[i]["DecDateRel"]);
                                htParam.Add("@DecPlaceRel", ctrldt.Rows[i]["DecPlaceRel"]);
                                htParam.Add("@kycEmpNameRel", ctrldt.Rows[i]["kycEmpNameRel"]);
                                htParam.Add("@kycEmpCodeRel", ctrldt.Rows[i]["kycEmpCodeRel"]);
                                htParam.Add("@kycEmpBranchRel", ctrldt.Rows[i]["kycEmpBranchRel"]);
                                htParam.Add("@kycEmpDesiRel", ctrldt.Rows[i]["kycEmpDesiRel"]);
                                htParam.Add("@kycVerDateRel", ctrldt.Rows[i]["kycVerDateRel"]);
                                htParam.Add("@kycCertDocRel", "01");
                                htParam.Add("@kycInstNameRel", ctrldt.Rows[i]["kycInstNameRel"]);
                                htParam.Add("@kycInstCodeRel", ctrldt.Rows[i]["kycInstCodeRel"]);
                                htParam.Add("@CreateBy", strUserId.ToString());
                                // htParam.Add("@PAN", txtPanNo.Text.ToString());
                                objDAL.ExecuteNonQuery("prc_InsKycCtrlPrsnPartialDtls", htParam);
                                Session["dsCtrl"] = null;
                            }
                        }
                    }

                    #endregion  controlling partial save end

                    //if (gvMemDtls.Visible == true)
                    //{

                    //    foreach (GridViewRow row in gvMemDtls.Rows)
                    //    {
                    //        LinkButton lnkdelete = (LinkButton)row.FindControl("lnkdelete");
                    //        lnkdelete.Enabled = false;
                    //    }
                    //}
                    hdnUpdate.Value = "Partial Data saved succesfully.";

                    msg = hdnUpdate.Value + "</br></br>Temporary Reference No: " + PSTempRefNo.ToString().Trim() + "<br/> Entity Name: "
                         + txtKYCName.Text + "<br/><br/>";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "popup('" + msg + "');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);

                    btnPartialSave.Enabled = false;
                    btnSave.Enabled = false;
                    txtKYCNumber.Text = strRefNo.ToString().Trim();
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please,atleast fill Personal detail ')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + Res + "')", true);
                    return;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "btnPartialSave_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }


        public void SetLogger(string strMessageIsn)
        {
            htParam.Clear();
            htParam.Add("@Message", strMessageIsn);
            objDAL.ExecuteNonQuery("prc_InsTempLog", htParam);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string Res, kyc, regref;
                if (txtPanNo.Text != "")
                {
                    objDAL = new DataAccessLayer("CKYCConnectionString");
                    dt = new DataTable();
                    htParam.Clear();
                    htParam.Add("@IDNo", txtPanNo.Text.ToString().Trim());

                    dt = objDAL.GetDataTable("prc_verifyID", htParam);

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["result"].ToString() == "TRUE")
                        {
                            if (dt.Rows[0]["KYC_NO"].ToString() == "")
                            {
                                regref = dt.Rows[0]["RegRefNo"].ToString();

                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select other Proof Of Identity as given " + lblPanNo.Text + "ID number is already registered with this Refrence no." + regref + " ')", true);
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please select other Proof Of Identity as gi')", true);
                                //txtPassNo.Focus();

                                msg = "Please select other Proof Of Identity as given " + lblPanNo.Text + " ID number is already registered with this Refrence no. " + regref;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true); //"AlertMsg('" + msg + "')"
                                return;
                            }
                            else
                            {
                                kyc = dt.Rows[0]["KYC_NO"].ToString();
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select other Proof Of Identity as given " + lblPassportNo.Text + "ID number is already registered with this KYC no." + kyc + " ')", true);
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select other Proof Of Identity as given " + lblPanNo.Text + "ID number is already registered with this KYC no." + kyc + " ')", true);

                                msg = "Please select other Proof Of Identity as given " + lblPanNo.Text + " ID number is already registered with this KYC no. " + kyc;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true); //"AlertMsg('" + msg + "')"
                                //txtPassNo.Focus();
                                return;
                            }
                        }
                    }
                }

                Res = objVal.LegalEntityDtlsValidation(ddlNatureOfBuss, txtConstitutionTypeothers, txtRefNumber,
                    txtKYCName, txtDatOfInc, txtDtOfCom, txtPlaceOfInc, ddlCountrOfInc, txtTypeIdentiNo,
                    ddlTINCountry, txtPanNo, ddlCertifiecopy, chkPerAddress, chkLocalAddress, chkCuurentAddress,
                    ddlProofOfAddress, txtAddressLine1, ddlState, ddlState1, ddlCountryCode, ddlState1, ddlState1,
                    ddlCountryCode1, txtCity, txtLocAddLine1, txtCity1,
                    txtTelOff, txtTelOff2, txtTelRes, txtTelRes2, txtMobile, txtMobile2,txtMobile1,txtMobile3, txtFax1, txtFax2,
                    chkAddRel, chkAppDeclare1, chkAppDeclare2, chkAppDeclare3, txtDate, txtPlace, new CheckBox(), new CheckBox(), new CheckBox(),
                    chkHigh, chkMedium, chkLow, txtPassNo, ddlDocReceived,chkDone, txtDateKYCver, txtEmpName, txtEmpCode,
                    txtEmpDesignation, txtEmpBranch, txtInsName, txtInsCode,gvMemDtls, txtPassNoAdd, txtemail2);

                if (Res.Equals(""))
                {


                    #region relatedpersonDSvalidation
                    DataTable dtRel = new DataTable();
                    DataTable dtCtrl1 = new DataTable();
                    //if (dsRel != null) { dsRel.Clear(); }

                    dtRel = (DataTable)Session["dsRel"];
                    dtCtrl1 = (DataTable)Session["dsCtrl"];

                    if (chkAddRel.Checked == true)
                    {
                        //if (dsRel.Tables.Count == 0)
                        if (dtRel == null)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please Add atleast One Relative Details')", true);
                            //chkAddRel.Focus();
                            chkAddRel.Checked = true;
                            return;
                        }
                    }

                    #endregion

                    #region ControllingpersonDSvalidation
                    DataTable dtCtrl = new DataTable();
                    //if (dsRel != null) { dsRel.Clear(); }

                    dtCtrl = (DataTable)Session["dsCtrl"];

                    //if (chkAddCtrl.Checked == true)
                    //{
                    //    //if (dsRel.Tables.Count == 0)
                    //    if (dtCtrl == null)
                    //    {
                    //        if (ddlCountryOfRsidens.SelectedValue == "IN")
                    //        {
                    //            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Add atleast One Relative Details')", true);
                    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please Add atleast One Controlling Details')", true);
                    //            //chkAddRel.Focus();
                    //            chkAddRel.Checked = false;
                    //            return;
                    //        }
                    //    }
                    //}
                    #endregion

                    #region Save Entity Details
                    htParam.Clear();

                    htParam.Add("@FiRefNo", txtRefNumber.Text.Trim());
                    if (cbNew.Checked == true)
                    {
                        htParam.Add("@AppType", "01");
                    }
                    else
                    {
                        htParam.Add("@AppType", "02");
                    }


                    htParam.Add("@ACC_TYPE_FLG", "");


                    htParam.Add("@AccType", "");
                    htParam.Add("@CompType", ddlNatureOfBuss.SelectedValue.Trim());


                    htParam.Add("@CKYCNo", txtKYCNumber.Text.Trim());

                    htParam.Add("@EntName", txtKYCName.Text.Trim());
                    htParam.Add("@DtofIncorporation", txtDatOfInc.Text.Trim());
                    htParam.Add("@DtofCommencementofbusi", txtDtOfCom.Text.Trim());
                    htParam.Add("@PlaceofIncorportation", txtPlaceOfInc.Text.Trim());
                    htParam.Add("@CountryofIncorporation", ddlCountrOfInc.SelectedValue.Trim());
                    htParam.Add("@CountryOfRsidens", "");
                    htParam.Add("@IdentyType", "");
                    htParam.Add("@TAX_NUM", txtTypeIdentiNo.Text.Trim());
                    htParam.Add("@TINIssuingCountry", ddlTINCountry.SelectedValue);
                    htParam.Add("@NoOfControlPrsnOI", "");

                    htParam.Add("@IDENT_NUM_ID1", ddlCertifiecopy.SelectedValue);
                    htParam.Add("@IDNO", txtPassNo.Text.Trim());


                    //if(chkSameAsPOI.Checked == true)
                    //{
                    //    htParam.Add("@SameasPOIAddresFlagP1","01");
                    //}
                    //else
                    //{
                    //    htParam.Add("@SameasPOIAddresFlagP1", System.DBNull.Value);
                    //}


                    if (chkPerAddress.Checked == true)
                    {
                        htParam.Add("@CnctType1", "P1");
                        htParam.Add("@PER_ADDTYPE", "");
                        htParam.Add("@PER_ADDPROOF", ddlProofOfAddress.SelectedValue.Trim());
                        htParam.Add("@PER_ADDLINE1", txtAddressLine1.Text.Trim());
                        htParam.Add("@PER_ADDLINE2", txtAddressLine2.Text.Trim());
                        htParam.Add("@PER_ADDLINE3", txtAddressLine3.Text.Trim());
                        htParam.Add("@PER_CITY", txtCity.Text.Trim());
                        htParam.Add("@PER_DISTRICT", txtDistrictname.Text.Trim());
                        htParam.Add("@PER_PIN", txtPinCode.Text.Trim());

                        if (ddlCountryCode.SelectedValue == "IN")
                        {
                            htParam.Add("@PER_STATECODE", ddlState.SelectedValue);
                        }
                        else
                        {
                            htParam.Add("@PER_STATECODE", txtState.Text.Trim());
                        }

                        htParam.Add("@PER_COUNTRY_CODE", ddlCountryCode.SelectedValue);
                        if (chkSameAsPOI.Checked == true)
                        {
                            htParam.Add("@SameasPOIAddresFlagP1", "01");
                        }
                        else
                        {
                            htParam.Add("@SameasPOIAddresFlagP1", "");
                        }

                    }
                    else
                    {
                        htParam.Add("@CnctType1", "");
                        htParam.Add("@PER_ADDTYPE", System.DBNull.Value);
                        htParam.Add("@PER_ADDPROOF", System.DBNull.Value);
                        htParam.Add("@PER_ADDLINE1", System.DBNull.Value);
                        htParam.Add("@PER_ADDLINE2", System.DBNull.Value);
                        htParam.Add("@PER_ADDLINE3", System.DBNull.Value);
                        htParam.Add("@PER_CITY", System.DBNull.Value);
                        htParam.Add("@PER_DISTRICT", System.DBNull.Value);
                        htParam.Add("@PER_PIN", System.DBNull.Value);
                        htParam.Add("@PER_STATECODE", System.DBNull.Value);
                        htParam.Add("@PER_COUNTRY_CODE", System.DBNull.Value);
                    }

                    #region commented

                    //htParam.Add("@AddIdType", ddlProofOfAddress.SelectedValue);
                    //if (chkPerAddress.Checked == true)
                    //{
                    //    if (ddlProofOfAddress.SelectedIndex == 1)
                    //    {
                    //        htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                    //        htParam.Add("@AddIdExpDate", txtPassExpDateAdd.Text.Trim());
                    //        htParam.Add("@AddIdName", System.DBNull.Value);

                    //    }
                    //    else if (ddlProofOfAddress.SelectedIndex == 2)
                    //    {
                    //        htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                    //        htParam.Add("@AddIdExpDate", txtPassExpDateAdd.Text.Trim());
                    //        htParam.Add("@AddIdName", System.DBNull.Value);
                    //    }
                    //    else if (ddlProofOfAddress.SelectedIndex == 3)
                    //    {
                    //        htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                    //        htParam.Add("@AddIdExpDate", System.DBNull.Value);
                    //        htParam.Add("@AddIdName", System.DBNull.Value);
                    //    }
                    //    else if (ddlProofOfAddress.SelectedIndex == 4)
                    //    {
                    //        htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                    //        htParam.Add("@AddIdExpDate", System.DBNull.Value);
                    //        htParam.Add("@AddIdName", System.DBNull.Value);

                    //    }
                    //    else if (ddlProofOfAddress.SelectedIndex == 5)
                    //    {
                    //        htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                    //        htParam.Add("@AddIdExpDate", System.DBNull.Value);
                    //        htParam.Add("@AddIdName", System.DBNull.Value);
                    //    }
                    //    else if (ddlProofOfAddress.SelectedIndex == 6)
                    //    {
                    //        htParam.Add("@AddIdNumber", txtPassOthrAdd.Text.Trim());
                    //        htParam.Add("@AddIdExpDate", System.DBNull.Value);
                    //        htParam.Add("@AddIdName", txtPassNoAdd.Text.Trim());
                    //    }
                    //    else
                    //    {
                    //        htParam.Add("@AddIdNumber", System.DBNull.Value);
                    //        htParam.Add("@AddIdExpDate", System.DBNull.Value);
                    //        htParam.Add("@AddIdName", System.DBNull.Value);
                    //    }
                    //}
                    //else
                    //{
                    //    htParam.Add("@AddIdNumber", System.DBNull.Value);
                    //    htParam.Add("@AddIdExpDate", System.DBNull.Value);
                    //    htParam.Add("@AddIdName", System.DBNull.Value);
                    //}

                    #endregion commented

                    if (chkCuurentAddress.Checked == true)
                    {
                        htParam.Add("@SameasCurrentAddresFlagM1", "01");
                    }
                    else
                    {
                        htParam.Add("@SameasCurrentAddresFlagM1", "");
                    }

                    if (chkLocalAddress.Checked == true)
                    {
                        htParam.Add("@CnctType2", "M1");
                        //htParam.Add("@SameAsPer", "01");//by meena 19/05/2017   
                        htParam.Add("@cAddType", "");
                        htParam.Add("@cAddIdType", "");
                        htParam.Add("@CUR_ADDLINE1", txtLocAddLine1.Text);
                        htParam.Add("@CUR_ADDLINE2", txtLocAddLine2.Text);
                        htParam.Add("@CUR_ADDLINE3", txtLocAddLine3.Text);
                        htParam.Add("@CUR_CITY", txtCity1.Text.Trim());
                        htParam.Add("@CUR_DISTRICT", txtDistrict1.Text.Trim());
                        htParam.Add("@CUR_PIN", ddlPinCode1.Text.Trim());

                        if (ddlCountryCode1.SelectedValue == "IN")
                        {
                            htParam.Add("@CUR_STATECODE", ddlState1.SelectedValue);
                        }
                        else
                        {
                            htParam.Add("@CUR_STATECODE", txtState1.Text.Trim());
                        }
                        //htParam.Add("@CUR_STATECODE", ddlState1.SelectedValue);
                        htParam.Add("@CUR_COUNTRY_CODE", ddlCountryCode1.SelectedValue);
                        //if (chkCuurentAddress.Checked == true)
                        //{
                        //    htParam.Add("@SameasCurrentAddresFlagM1", "01");
                        //}
                        //else
                        //{
                        //    htParam.Add("@SameasCurrentAddresFlagM1", System.DBNull.Value);
                        //}
                    }
                    else
                    {
                        htParam.Add("@CnctType2", "");
                        //htParam.Add("@SameAsPer", "02");//by meena 19/05/2017
                        htParam.Add("@cAddType", System.DBNull.Value);
                        htParam.Add("@cAddIdType", System.DBNull.Value);
                        htParam.Add("@CUR_ADDLINE1", System.DBNull.Value);
                        htParam.Add("@CUR_ADDLINE2", System.DBNull.Value);
                        htParam.Add("@CUR_ADDLINE3", System.DBNull.Value);
                        htParam.Add("@CUR_CITY", System.DBNull.Value);
                        htParam.Add("@CUR_DISTRICT", System.DBNull.Value);
                        htParam.Add("@CUR_PIN", System.DBNull.Value);
                        htParam.Add("@CUR_STATECODE", System.DBNull.Value);
                        htParam.Add("@CUR_COUNTRY_CODE", System.DBNull.Value);
                    }


                    htParam.Add("@SameasLocalAddressFlagJ1", "");

                    htParam.Add("@SameasLocalAddressFlagJ2", "");


                    htParam.Add("@CnctType3", "");
                    htParam.Add("@fAddType", System.DBNull.Value);
                    htParam.Add("@fAddIdType", System.DBNull.Value);
                    htParam.Add("@FRN_ADDLINE1", System.DBNull.Value);
                    htParam.Add("@FRN_ADDLINE2", System.DBNull.Value);
                    htParam.Add("@FRN_ADDLINE3", System.DBNull.Value);
                    htParam.Add("@FRN_CITY", System.DBNull.Value);
                    htParam.Add("@FRN_DISTRICT", System.DBNull.Value);
                    htParam.Add("@FRN_PIN", System.DBNull.Value);
                    htParam.Add("@FRN_STATECODE", System.DBNull.Value);
                    htParam.Add("@FRN_COUNTRY_CODE", System.DBNull.Value);

                    htParam.Add("@std_officeTele", txtTelOff.Text.Trim());
                    htParam.Add("@std_resTele", txtTelRes.Text.Trim());
                    htParam.Add("@mobile_countryCode", txtMobile.Text.Trim());
                    htParam.Add("@std_fax", txtFax1.Text);

                    htParam.Add("@OFF_TELE", txtTelOff2.Text);
                    htParam.Add("@RES_TEL", txtTelRes2.Text);
                    htParam.Add("@FAX", txtFax2.Text);
                    htParam.Add("@MOBILE", txtMobile2.Text);
                    htParam.Add("@EMAILID", txtemail.Text);


                    htParam.Add("@REMARK", txtRemarks.Text.Trim());
                    htParam.Add("@APP_DATE", txtDate.Text.Trim());
                    htParam.Add("@PLACE", txtPlace.Text.Trim());
                    htParam.Add("@kycEmpName", txtEmpName.Text.Trim());
                    htParam.Add("@kycEmpCode", txtEmpCode.Text.Trim());
                    htParam.Add("@kycEmpBranch", txtEmpBranch.Text.Trim());
                    htParam.Add("@kycEmpDesi", txtEmpDesignation.Text.Trim());
                    htParam.Add("@kycVerDate", txtDateKYCver.Text.Trim());
                    htParam.Add("@kycCertDoc", ddlDocReceived.SelectedValue);
                    //if (chkSelfCerti.Checked == true)
                    //{
                    //    htParam.Remove("@kycCertDoc");
                    //    htParam.Add("@kycCertDoc", "01");
                    //}
                    //else if (chkTrueCopies.Checked == true)
                    //{
                    //    htParam.Remove("@kycCertDoc");
                    //    htParam.Add("@kycCertDoc", "02");
                    //}
                    //else if (chkNotary.Checked == true)
                    //{
                    //    htParam.Remove("@kycCertDoc");
                    //    htParam.Add("@kycCertDoc", "03");
                    //}

                    htParam.Add("@kycInstName", txtInsName.Text.Trim());
                    htParam.Add("@kycInstCode", txtInsCode.Text.Trim());
                    htParam.Add("@CREATEDBY", strUserId.ToString());
                    htParam.Add("@UpdateFlag", "N");
                    htParam.Add("@TKYCNO", "");
                    htParam.Add("@uniqueID", obj.ToString());
                    htParam.Add("@Usages", "W");
                    htParam.Add("@Status", Request.QueryString["status"].ToString());
                    htParam.Add("@PartialRegRefNo", txtRefNumber.Text.ToString());
                    htParam.Add("@PAN", txtPanNo.Text.ToString());

                    dt = null;
                    objDAL = new DataAccessLayer("CKYCConnectionString");
                    dt = objDAL.GetDataTable("prc_InsEntkycdtls_Web", htParam);

                    if (dt.Rows.Count > 0)
                    {
                        strRefNo = dt.Rows[0]["RegRefNo"].ToString();
                    }
                    #endregion

                    #region Save Related Members Details

                    if (dtRel != null)
                    {
                        if (dtRel.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtRel.Rows.Count; i++)
                            {

                                htParam.Clear();
                                htParam.Add("@RegRefNo", strRefNo);
                                htParam.Add("@FiRefNo", dtRel.Rows[i]["FiRefNo"]);
                                if (chkAddRel.Checked == true)
                                {
                                    htParam.Add("@AddDelFlagRel", "01");
                                }
                                else if (chkAddRel.Checked == false)
                                {
                                    htParam.Add("@AddDelFlagRel", "02");
                                }

                                htParam.Add("@RelatedPrsnKYCNo", dtRel.Rows[i]["RelatedPrsnKYCNo"]);
                                htParam.Add("@RelationType", dtRel.Rows[i]["RelationType"]);
                                //--
                                htParam.Add("@RelPersonAs", dtRel.Rows[i]["RelPersonAs"]);

                                htParam.Add("@RelEntName", dtRel.Rows[i]["RelEntName"]);
                                htParam.Add("@RelDtofIncorporation", dtRel.Rows[i]["RelDtofIncorporation"]);
                                htParam.Add("@RelDtofCommencementofbusi", dtRel.Rows[i]["RelDtofCommencementofbusi"]);
                                htParam.Add("@RelPlaceofIncorportation", dtRel.Rows[i]["RelPlaceofIncorportation"]);
                                htParam.Add("@RelCountryofIncorporation", dtRel.Rows[i]["RelCountryofIncorporation"]);
                                htParam.Add("@RelCountryofResAsperTaxLaws", dtRel.Rows[i]["RelCountryofResAsperTaxLaws"]);
                                htParam.Add("@RelIdType", dtRel.Rows[i]["RelIdType"]);
                                htParam.Add("@RelPAN", dtRel.Rows[i]["RelPAN"]);
                                htParam.Add("@RelTINIdNo", dtRel.Rows[i]["RelTINIdNo"]);
                                htParam.Add("@RelTINCountry", dtRel.Rows[i]["RelTINCountry"]);

                                htParam.Add("@RelTelSTDCode", dtRel.Rows[i]["RelTelSTDCode"]);
                                htParam.Add("@RelTelNo", dtRel.Rows[i]["RelTelNo"]);
                                htParam.Add("@RelOfficeTelSTDCode", dtRel.Rows[i]["RelOfficeTelSTDCode"]);
                                htParam.Add("@RelOfficeTelNo", dtRel.Rows[i]["RelOfficeTelNo"]);
                                htParam.Add("@RelMobCode", dtRel.Rows[i]["RelMobCode"]);
                                htParam.Add("@RelMobileNo", dtRel.Rows[i]["RelMobileNo"]);
                                htParam.Add("@RelFaxNoCode", dtRel.Rows[i]["RelFaxNoCode"]);
                                htParam.Add("@RelFaxNo", dtRel.Rows[i]["RelFaxNo"]);
                                htParam.Add("@RelEmailID", dtRel.Rows[i]["RelEmailID"]);
                                //--
                                htParam.Add("@PrefixRel", dtRel.Rows[i]["PrefixRel"]);
                                htParam.Add("@FNameRel", dtRel.Rows[i]["FNameRel"]);
                                htParam.Add("@MNameRel", dtRel.Rows[i]["MNameRel"]);
                                htParam.Add("@LNameRel", dtRel.Rows[i]["LNameRel"]);
                                htParam.Add("@MaidPrefixRel", dtRel.Rows[i]["MaidPrefixRel"]);
                                htParam.Add("@MaidFNameRel", dtRel.Rows[i]["MaidFNameRel"]);
                                htParam.Add("@MaidMNameRel", dtRel.Rows[i]["MaidMNameRel"]);
                                htParam.Add("@MaidLNameRel", dtRel.Rows[i]["MaidLNameRel"]);
                                htParam.Add("@FSFlagRel", dtRel.Rows[i]["FSFlagRel"]);
                                htParam.Add("@FatherPrefixRel", dtRel.Rows[i]["FatherPrefixRel"]);
                                htParam.Add("@FatherFNameRel", dtRel.Rows[i]["FatherFNameRel"]);
                                htParam.Add("@FatherMNameRel", dtRel.Rows[i]["FatherMNameRel"]);
                                htParam.Add("@FatherLNameRel", dtRel.Rows[i]["FatherLNameRel"]);
                                htParam.Add("@MotherPrefixRel", dtRel.Rows[i]["MotherPrefixRel"]);
                                htParam.Add("@MotherFNameRel", dtRel.Rows[i]["MotherFNameRel"]);
                                htParam.Add("@MotherMNameRel", dtRel.Rows[i]["MotherMNameRel"]);
                                htParam.Add("@MotherLNameRel", dtRel.Rows[i]["MotherLNameRel"]);
                                htParam.Add("@DOBRel", dtRel.Rows[i]["DOBRel"]);
                                htParam.Add("@GenderRel", dtRel.Rows[i]["GenderRel"]);
                                htParam.Add("@MaritalStatusRel", dtRel.Rows[i]["MaritalStatusRel"]);
                                htParam.Add("@CitizenshipRel", dtRel.Rows[i]["CitizenshipRel"]);
                                htParam.Add("@ResiStatusRel", dtRel.Rows[i]["ResiStatusRel"]);
                                htParam.Add("@OccuTypeRel", dtRel.Rows[i]["OccuTypeRel"]);

                                htParam.Add("@OccuSubTypeRel", dtRel.Rows[i]["OccuSubTypeRel"]);

                                htParam.Add("@ResForTaxFlagRel", dtRel.Rows[i]["ResForTaxFlagRel"]);
                                htParam.Add("@ISOCountryCodeRel", dtRel.Rows[i]["ISOCountryCodeRel"]);
                                htParam.Add("@TaxIDNumberRel", dtRel.Rows[i]["TaxIDNumberRel"]);
                                htParam.Add("@BirthCityRel", dtRel.Rows[i]["BirthCityRel"]);
                                htParam.Add("@ISOBirthPlaceCodeRel", dtRel.Rows[i]["ISOBirthPlaceCodeRel"]);

                                htParam.Add("@IdType", dtRel.Rows[i]["IdType"]);
                                htParam.Add("@IdNumber", dtRel.Rows[i]["IdNumber"]);
                                htParam.Add("@IDExpDate", dtRel.Rows[i]["IdExpDate"]);
                                htParam.Add("@IDName", dtRel.Rows[i]["IdName"]);


                                htParam.Add("@SameasPOIAddresFlagP1", dtRel.Rows[i]["SameasPOIAddresFlagP1"]);
                                htParam.Add("@CnctTypeRel", "P1");
                                htParam.Add("@PER_ADDTYPE", dtRel.Rows[i]["AdrTypeRel"]);
                                htParam.Add("@PER_ADDPROOF", dtRel.Rows[i]["AdrProfRel"]);
                                htParam.Add("@PER_ADDLINE1", dtRel.Rows[i]["Adr1Rel"]);
                                htParam.Add("@PER_ADDLINE2", dtRel.Rows[i]["Adr2Rel"]);
                                htParam.Add("@PER_ADDLINE3", dtRel.Rows[i]["Adr3Rel"]);
                                htParam.Add("@PER_CITY", dtRel.Rows[i]["CityRel"]);
                                htParam.Add("@PER_DISTRICT", dtRel.Rows[i]["DistrictRel"]);
                                htParam.Add("@PER_PIN", dtRel.Rows[i]["PostCodeRel"]);
                                htParam.Add("@PER_STATECODE", dtRel.Rows[i]["StateCodeRel"]);
                                htParam.Add("@PER_COUNTRY_CODE", dtRel.Rows[i]["CntryCodeRel"]);

                                //added by daksh
                                htParam.Add("@SameasCurrentAddresFlagM1", dtRel.Rows[i]["SameasCurrentAddresFlagM1"]);
                                htParam.Add("@CnctTypeRel1", "M1");
                                htParam.Add("@corPER_ADDTYPE", dtRel.Rows[i]["AdrTypeRel1"]);
                                //htParam.Add("@PER_ADDPROOF", dtRel.Rows[i]["AdrProfRel1"]);
                                htParam.Add("@corPER_ADDLINE1", dtRel.Rows[i]["Adr1Rel1"]);
                                htParam.Add("@corPER_ADDLINE2", dtRel.Rows[i]["Adr2Rel1"]);
                                htParam.Add("@corPER_ADDLINE3", dtRel.Rows[i]["Adr3Rel1"]);
                                htParam.Add("@corPER_CITY", dtRel.Rows[i]["CityRel1"]);
                                htParam.Add("@corPER_DISTRICT", dtRel.Rows[i]["DistrictRel1"]);
                                htParam.Add("@corPER_PIN", dtRel.Rows[i]["PostCodeRel1"]);
                                htParam.Add("@corPER_STATECODE", dtRel.Rows[i]["StateCodeRel1"]);
                                htParam.Add("@corPER_COUNTRY_CODE", dtRel.Rows[i]["CntryCodeRel1"]);


                                htParam.Add("@SameasLocalAddressFlagJ1", dtRel.Rows[i]["SameasLocalAddressFlagJ1"]);
                                htParam.Add("@SameasLocalAddressFlagJ2", dtRel.Rows[i]["SameasLocalAddressFlagJ2"]);
                                htParam.Add("@CnctTypeRel2", "J1");
                                htParam.Add("@jurPER_ADDTYPE", dtRel.Rows[i]["AdrTypeRel2"]);
                                //htParam.Add("@PER_ADDPROOF", dtRel.Rows[i]["AdrProfRel"]);
                                htParam.Add("@jurPER_ADDLINE1", dtRel.Rows[i]["Adr1Rel2"]);
                                htParam.Add("@jurPER_ADDLINE2", dtRel.Rows[i]["Adr2Rel2"]);
                                htParam.Add("@jurPER_ADDLINE3", dtRel.Rows[i]["Adr3Rel2"]);
                                htParam.Add("@jurPER_CITY", dtRel.Rows[i]["CityRel2"]);
                                htParam.Add("@jurPER_DISTRICT", dtRel.Rows[i]["DistrictRel2"]);
                                htParam.Add("@jurPER_PIN", dtRel.Rows[i]["PostCodeRel2"]);
                                htParam.Add("@jurPER_STATECODE", dtRel.Rows[i]["StateCodeRel2"]);
                                htParam.Add("@jurPER_COUNTRY_CODE", dtRel.Rows[i]["CntryCodeRel2"]);

                                //end

                                htParam.Add("@AddIdType", dtRel.Rows[i]["AddIdTypeRel"]);
                                htParam.Add("@AddIdNumber", dtRel.Rows[i]["AddIdNumberRel"]);
                                htParam.Add("@AddIdExpDate", dtRel.Rows[i]["AddIdExpDateRel"]);
                                htParam.Add("@AddIdName", dtRel.Rows[i]["AddIdNameRel"]);
                                htParam.Add("@DecDateRel", dtRel.Rows[i]["DecDateRel"]);
                                htParam.Add("@DecPlaceRel", dtRel.Rows[i]["DecPlaceRel"]);
                                htParam.Add("@kycEmpNameRel", dtRel.Rows[i]["kycEmpNameRel"]);
                                htParam.Add("@kycEmpCodeRel", dtRel.Rows[i]["kycEmpCodeRel"]);
                                htParam.Add("@kycEmpBranchRel", dtRel.Rows[i]["kycEmpBranchRel"]);
                                htParam.Add("@kycEmpDesiRel", dtRel.Rows[i]["kycEmpDesiRel"]);
                                htParam.Add("@kycVerDateRel", dtRel.Rows[i]["kycVerDateRel"]);
                                htParam.Add("@kycCertDocRel", "01");
                                htParam.Add("@kycInstNameRel", dtRel.Rows[i]["kycInstNameRel"]);
                                htParam.Add("@kycInstCodeRel", dtRel.Rows[i]["kycInstCodeRel"]);
                                htParam.Add("@CreateBy", strUserId.ToString());
                                //htParam.Add("@PAN", txtPanNo.Text.ToString());
                                objDAL.ExecuteNonQuery("prc_InsKycRelatedPrsnDtls", htParam);

                                Session["dsRel"] = null;
                            }
                        }
                    }
                    #endregion

                    #region Save Controlling Person Details

                    if (dtCtrl != null)
                    {
                        if (dtCtrl.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtCtrl.Rows.Count; i++)
                            {

                                htParam.Clear();
                                htParam.Add("@RegRefNo", strRefNo);
                                htParam.Add("@FiRefNo", dtCtrl.Rows[i]["FiRefNo"]);

                                htParam.Add("@AddDelFlagCtrl", "");

                                htParam.Add("@ControlPrsnKYCNo", dtCtrl.Rows[i]["RelatedPrsnKYCNo"]);
                                htParam.Add("@ControlType", dtCtrl.Rows[i]["RelationType"]);

                                htParam.Add("@PrefixCtrl", dtCtrl.Rows[i]["PrefixRel"]);
                                htParam.Add("@FNameCtrl", dtCtrl.Rows[i]["FNameRel"]);
                                htParam.Add("@MNameCtrl", dtCtrl.Rows[i]["MNameRel"]);
                                htParam.Add("@LNameCtrl", dtCtrl.Rows[i]["LNameRel"]);
                                htParam.Add("@MaidPrefixCtrl", dtCtrl.Rows[i]["MaidPrefixRel"]);
                                htParam.Add("@MaidFNameCtrl", dtCtrl.Rows[i]["MaidFNameRel"]);
                                htParam.Add("@MaidMNameCtrl", dtCtrl.Rows[i]["MaidMNameRel"]);
                                htParam.Add("@MaidLNameCtrl", dtCtrl.Rows[i]["MaidLNameRel"]);
                                htParam.Add("@FSFlagCtrl", dtCtrl.Rows[i]["FSFlagRel"]);
                                htParam.Add("@FatherPrefixCtrl", dtCtrl.Rows[i]["FatherPrefixRel"]);
                                htParam.Add("@FatherFNameCtrl", dtCtrl.Rows[i]["FatherFNameRel"]);
                                htParam.Add("@FatherMNameCtrl", dtCtrl.Rows[i]["FatherMNameRel"]);
                                htParam.Add("@FatherLNameCtrl", dtCtrl.Rows[i]["FatherLNameRel"]);
                                htParam.Add("@MotherPrefixCtrl", dtCtrl.Rows[i]["MotherPrefixRel"]);
                                htParam.Add("@MotherFNameCtrl", dtCtrl.Rows[i]["MotherFNameRel"]);
                                htParam.Add("@MotherMNameCtrl", dtCtrl.Rows[i]["MotherMNameRel"]);
                                htParam.Add("@MotherLNameCtrl", dtCtrl.Rows[i]["MotherLNameRel"]);
                                htParam.Add("@DOBCtrl", dtCtrl.Rows[i]["DOBRel"]);
                                htParam.Add("@GenderCtrl", dtCtrl.Rows[i]["GenderRel"]);
                                htParam.Add("@MaritalStatusCtrl", dtCtrl.Rows[i]["MaritalStatusRel"]);
                                htParam.Add("@CitizenshipCtrl", dtCtrl.Rows[i]["CitizenshipRel"]);
                                htParam.Add("@ResiStatusCtrl", dtCtrl.Rows[i]["ResiStatusRel"]);
                                htParam.Add("@OccuTypeCtrl", dtCtrl.Rows[i]["OccuTypeRel"]);

                                htParam.Add("@ResForTaxFlagCtrl", dtCtrl.Rows[i]["ResForTaxFlagRel"]);
                                htParam.Add("@ISOCountryCodeCtrl", dtCtrl.Rows[i]["ISOCountryCodeRel"]);
                                htParam.Add("@TaxIDNumberCtrl", dtCtrl.Rows[i]["TaxIDNumberRel"]);
                                htParam.Add("@BirthCityCtrl", dtCtrl.Rows[i]["BirthCityRel"]);
                                htParam.Add("@ISOBirthPlaceCodeCtrl", dtCtrl.Rows[i]["ISOBirthPlaceCodeRel"]);

                                htParam.Add("@IdType", dtCtrl.Rows[i]["IdType"]);
                                htParam.Add("@IdNumber", dtCtrl.Rows[i]["IdNumber"]);
                                htParam.Add("@IDExpDate", dtCtrl.Rows[i]["IdExpDate"]);
                                htParam.Add("@IDName", dtCtrl.Rows[i]["IdName"]);

                                htParam.Add("@CnctTypeCtrl", "P1");
                                htParam.Add("@PER_ADDTYPE", dtCtrl.Rows[i]["AdrTypeRel"]);
                                htParam.Add("@PER_ADDPROOF", dtCtrl.Rows[i]["AdrProfRel"]);
                                htParam.Add("@PER_ADDLINE1", dtCtrl.Rows[i]["Adr1Rel"]);
                                htParam.Add("@PER_ADDLINE2", dtCtrl.Rows[i]["Adr2Rel"]);
                                htParam.Add("@PER_ADDLINE3", dtCtrl.Rows[i]["Adr3Rel"]);
                                htParam.Add("@PER_CITY", dtCtrl.Rows[i]["CityRel"]);
                                htParam.Add("@PER_DISTRICT", dtCtrl.Rows[i]["DistrictRel"]);
                                htParam.Add("@PER_PIN", dtCtrl.Rows[i]["PostCodeRel"]);
                                htParam.Add("@PER_STATECODE", dtCtrl.Rows[i]["StateCodeRel"]);
                                htParam.Add("@PER_COUNTRY_CODE", dtCtrl.Rows[i]["CntryCodeRel"]);

                                //added by daksh
                                htParam.Add("@CnctTypeRel1", "M1");
                                htParam.Add("@corPER_ADDTYPE", dtCtrl.Rows[i]["AdrTypeRel1"]);
                                //htParam.Add("@PER_ADDPROOF", dtRel.Rows[i]["AdrProfRel1"]);
                                htParam.Add("@corPER_ADDLINE1", dtCtrl.Rows[i]["Adr1Rel1"]);
                                htParam.Add("@corPER_ADDLINE2", dtCtrl.Rows[i]["Adr2Rel1"]);
                                htParam.Add("@corPER_ADDLINE3", dtCtrl.Rows[i]["Adr3Rel1"]);
                                htParam.Add("@corPER_CITY", dtCtrl.Rows[i]["CityRel1"]);
                                htParam.Add("@corPER_DISTRICT", dtCtrl.Rows[i]["DistrictRel1"]);
                                htParam.Add("@corPER_PIN", dtCtrl.Rows[i]["PostCodeRel1"]);
                                htParam.Add("@corPER_STATECODE", dtCtrl.Rows[i]["StateCodeRel1"]);
                                htParam.Add("@corPER_COUNTRY_CODE", dtCtrl.Rows[i]["CntryCodeRel1"]);

                                htParam.Add("@CnctTypeRel2", "J1");
                                htParam.Add("@jurPER_ADDTYPE", dtCtrl.Rows[i]["AdrTypeRel2"]);
                                //htParam.Add("@PER_ADDPROOF", dtCtrl.Rows[i]["AdrProfRel"]);
                                htParam.Add("@jurPER_ADDLINE1", dtCtrl.Rows[i]["Adr1Rel2"]);
                                htParam.Add("@jurPER_ADDLINE2", dtCtrl.Rows[i]["Adr2Rel2"]);
                                htParam.Add("@jurPER_ADDLINE3", dtCtrl.Rows[i]["Adr3Rel2"]);
                                htParam.Add("@jurPER_CITY", dtCtrl.Rows[i]["CityRel2"]);
                                htParam.Add("@jurPER_DISTRICT", dtCtrl.Rows[i]["DistrictRel2"]);
                                htParam.Add("@jurPER_PIN", dtCtrl.Rows[i]["PostCodeRel2"]);
                                htParam.Add("@jurPER_STATECODE", dtCtrl.Rows[i]["StateCodeRel2"]);
                                htParam.Add("@jurPER_COUNTRY_CODE", dtCtrl.Rows[i]["CntryCodeRel2"]);

                                htParam.Add("@SameasPOIAddresFlagP1", dtCtrl.Rows[i]["SameasPOIAddresFlagP1"]);
                                htParam.Add("@SameasCurrentAddresFlagM1", dtCtrl.Rows[i]["SameasCurrentAddresFlagM1"]);
                                htParam.Add("@SameasLocalAddressFlagJ1", dtCtrl.Rows[i]["SameasLocalAddressFlagJ1"]);
                                htParam.Add("@SameasLocalAddressFlagJ2", dtCtrl.Rows[i]["SameasLocalAddressFlagJ2"]);

                                //end
                                htParam.Add("@AddIdType", dtCtrl.Rows[i]["AddIdTypeRel"]);
                                htParam.Add("@AddIdNumber", dtCtrl.Rows[i]["AddIdNumberRel"]);
                                htParam.Add("@AddIdExpDate", dtCtrl.Rows[i]["AddIdExpDateRel"]);
                                htParam.Add("@AddIdName", dtCtrl.Rows[i]["AddIdNameRel"]);

                                //New to add contact details start
                                htParam.Add("@OccuSubTypeCtrl", dtCtrl.Rows[i]["OccuSubTypeRel"]);
                                htParam.Add("@TelSTDCodeCtrl", dtCtrl.Rows[i]["TelSTDCode"]);
                                htParam.Add("@TelNoCtrl", dtCtrl.Rows[i]["TelNo"]);
                                htParam.Add("@OfficeTelSTDCodeCtrl", dtCtrl.Rows[i]["OfficeTelSTDCode"]);
                                htParam.Add("@OfficeTelNoCtrl", dtCtrl.Rows[i]["OfficeTelNo"]);
                                htParam.Add("@MobCodeCtrl", dtCtrl.Rows[i]["MobCode"]);
                                htParam.Add("@MobileNoCtrl", dtCtrl.Rows[i]["MobileNo"]);
                                htParam.Add("@FaxNoCodeCtrl", dtCtrl.Rows[i]["FaxNoCode"]);
                                htParam.Add("@FaxNoCtrl", dtCtrl.Rows[i]["FaxNo"]);
                                htParam.Add("@EmailIDCtrl", dtCtrl.Rows[i]["EMAILID"]);
                                //New to add contact details end

                                htParam.Add("@DecDateCtrl", dtCtrl.Rows[i]["DecDateRel"]);
                                htParam.Add("@DecPlaceCtrl", dtCtrl.Rows[i]["DecPlaceRel"]);
                                htParam.Add("@kycEmpNameCtrl", dtCtrl.Rows[i]["kycEmpNameRel"]);
                                htParam.Add("@kycEmpCodeCtrl", dtCtrl.Rows[i]["kycEmpCodeRel"]);
                                htParam.Add("@kycEmpBranchCtrl", dtCtrl.Rows[i]["kycEmpBranchRel"]);
                                htParam.Add("@kycEmpDesiCtrl", dtCtrl.Rows[i]["kycEmpDesiRel"]);
                                htParam.Add("@kycVerDateCtrl", dtCtrl.Rows[i]["kycVerDateRel"]);
                                htParam.Add("@kycCertDocCtrl", "01");
                                htParam.Add("@kycInstNameCtrl", dtCtrl.Rows[i]["kycInstNameRel"]);
                                htParam.Add("@kycInstCodeCtrl", dtCtrl.Rows[i]["kycInstCodeRel"]);
                                htParam.Add("@CreateBy", strUserId.ToString());
                                // htParam.Add("@PAN", txtPanNo.Text.ToString());
                                objDAL.ExecuteNonQuery("prc_InsKycControlPrsnDtls", htParam);

                                Session["dsCtrl"] = null;
                            }
                        }
                    }
                    #endregion

                    if (gvMemDtls.Visible == true)
                    {

                        foreach (GridViewRow row in gvMemDtls.Rows)
                        {
                            //LinkButton lnkdelete = (LinkButton)row.FindControl("lnkdelete");
                            //lnkdelete.Enabled = false;
                        }
                    }


                    hdnUpdate.Value = "<center> KYC registered successfully.";

                    msg = hdnUpdate.Value + "</br></br>Reference No: " + strRefNo.ToString().Trim() + "<br/> Entity Name: "
                          + txtKYCName.Text + "<br/><br/>Please proceed for document upload </center>";


                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "popup('" + msg + "');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsgInfo('" + msg + "')", true);



                    btnSave.Enabled = false;
                    btnPartialSave.Enabled = false;
                    //btnPartialUpdate.Enabled = false;

                    txtKYCNumber.Text = strRefNo.ToString().Trim();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + Res + "')", true);
                    //txtKYCName.Focus();
                    return;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "btnSave_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        #endregion

        #region PartialUpdate
        protected void btnPartialUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string Res;
                Res = "";//objVal.PersonalDtlsValidation(
                //chkNormal, chkSimplified, Chksmall, cboTitle, txtGivenName, txtLastName, cboTitle2, rbtFS, txtGivenName2, txtLastName2, cboTitle3, txtGivenName3,
                //txtLastName3, txtDOB, cboGender, ddlOccupation, ddlOccuSubType, ddlMaritalStatus, ddlCitizenship, ddlResStatus, ddlIsoCountryCodeOthr, null, "Candidate");

                if (txtRefNumber.Text.ToString() == "")
                {
                    Res = "Please enter FI Reference Number";
                }

                if (txtKYCName.Text.ToString() == "")
                {
                    Res = "Please enter entity name";
                }


                if (Res.Equals(""))
                {

                    #region relatedpersonDSvalidation
                    dt = new DataTable();
                    dt = (DataTable)Session["dsRel"];
                    ctrldt = new DataTable();
                    ctrldt = (DataTable)Session["dsCtrl"];


                    if (chkAddRel.Checked == true)
                    {
                        if (dt == null)
                        {
                            chkAddRel.Checked = false;
                            return;
                        }
                    }
                    #endregion

                    #region  Entity Partial Details

                    htParam.Clear();
                    htParam.Add("@PSTempRefNo", Request.QueryString["refno"].ToString());
                    htParam.Add("@FiRefNo", txtRefNumber.Text.Trim());
                    if (cbNew.Checked == true)
                    {
                        htParam.Add("@APP_TYPE", "01");
                    }
                    else
                    {
                        htParam.Add("@APP_TYPE", "02");
                    }


                    htParam.Add("@ACC_TYPE_FLG", "");
                    htParam.Add("@ACC_TYPE", "");

                    htParam.Add("@CompType", ddlNatureOfBuss.SelectedValue.Trim());
                    htParam.Add("@PAN", txtPanNo.Text.ToString());
                    htParam.Add("@EntName", txtKYCName.Text.Trim());
                    htParam.Add("@DtofIncorporation", txtDatOfInc.Text.Trim());
                    htParam.Add("@DtofCommencementofbusi", txtDtOfCom.Text.Trim());
                    htParam.Add("@PlaceofIncorportation", txtPlaceOfInc.Text.Trim());
                    htParam.Add("@CountryofIncorporation", ddlCountrOfInc.SelectedValue.Trim());
                    htParam.Add("@CountryOfRsidens", "");
                    htParam.Add("@IdentyType", "");
                    htParam.Add("@TAX_NUM", txtTypeIdentiNo.Text.Trim());
                    htParam.Add("@TINIssuingCountry", ddlTINCountry.SelectedValue);
                    htParam.Add("@NoOfControlPrsnOI", "");

                    htParam.Add("@IDENT_NUM_ID1", ddlCertifiecopy.SelectedValue);
                    htParam.Add("@IDNO", txtPassNo.Text.Trim());

                    if (chkPerAddress.Checked == true)
                    {
                        htParam.Add("@CnctType1", "P1");
                        htParam.Add("@PERM_TYPE", "");
                        htParam.Add("@PERM_LINE1", txtAddressLine1.Text.Trim());
                        htParam.Add("@PERM_LINE2", txtAddressLine2.Text.Trim());
                        htParam.Add("@PERM_LINE3", txtAddressLine3.Text.Trim());
                        htParam.Add("@PERM_CITY", txtCity.Text.Trim());
                        htParam.Add("@PERM_DIST", txtDistrictname.Text);
                        htParam.Add("@PERM_PIN", txtPinCode.Text);

                        if (ddlCountryCode.SelectedValue == "IN")
                        {
                            htParam.Add("@PERM_STATE", ddlState.SelectedValue);
                        }
                        else
                        {
                            htParam.Add("@PERM_STATE", txtState.Text.Trim());
                        }

                        //htParam.Add("@PERM_STATE", ddlState.SelectedValue);
                        htParam.Add("@PERM_COUNTRY", ddlCountryCode.SelectedValue);
                        if (chkSameAsPOI.Checked == true)
                        {
                            htParam.Add("@SameasPOIAddresFlagP1", "01");
                        }
                        else
                        {
                            htParam.Add("@SameasPOIAddresFlagP1", "");
                        }
                    }
                    else
                    {
                        htParam.Add("@CnctType1", "");
                        htParam.Add("@PERM_TYPE", System.DBNull.Value);
                        htParam.Add("@PERM_LINE1", System.DBNull.Value);
                        htParam.Add("@PERM_LINE2", System.DBNull.Value);
                        htParam.Add("@PERM_LINE3", System.DBNull.Value);
                        htParam.Add("@PERM_CITY", System.DBNull.Value);
                        htParam.Add("@PERM_DIST", System.DBNull.Value);
                        htParam.Add("@PERM_PIN", System.DBNull.Value);
                        htParam.Add("@PERM_STATE", System.DBNull.Value);
                        htParam.Add("@PERM_COUNTRY", System.DBNull.Value);
                    }

                    htParam.Add("@PERM_POA", ddlProofOfAddress.SelectedValue.Trim());

                    //if (chkPerAddress.Checked == true)
                    //{
                    //        htParam.Add("@PERM_IDNUMBAER", System.DBNull.Value);
                    //        htParam.Add("@PERM_IDEXPDT", System.DBNull.Value);
                    //        htParam.Add("@PERM_POAOTHERS", System.DBNull.Value);
                    //}
                    //else
                    //{
                    htParam.Add("@PERM_IDNUMBAER", System.DBNull.Value);
                    htParam.Add("@PERM_IDEXPDT", System.DBNull.Value);
                    htParam.Add("@PERM_POAOTHERS", System.DBNull.Value);
                    //}

                    if (chkCuurentAddress.Checked == true)
                    {
                        htParam.Add("@SameasCurrentAddresFlagM1", "01");
                    }
                    else
                    {
                        htParam.Add("@SameasCurrentAddresFlagM1", "");
                    }

                    if (chkLocalAddress.Checked == true)
                    {
                        htParam.Add("@CnctType2", "M1");
                        htParam.Add("@PERM_CORRES_SAMEFLAG", "01");//by meena 19/05/2017
                        htParam.Add("@CORRES_TYPE", "");
                        htParam.Add("@CORRES_POA", "");
                        htParam.Add("@CORRES_LINE1", txtLocAddLine1.Text);
                        htParam.Add("@CORRES_LINE2", txtLocAddLine2.Text);
                        htParam.Add("@CORRES_LINE3", txtLocAddLine3.Text);
                        htParam.Add("@CORRES_CITY ", txtCity1.Text.Trim());
                        htParam.Add("@CORRES_DIST", txtDistrict1.Text);
                        htParam.Add("@CORRES_PIN", ddlPinCode1.Text);

                        if (ddlCountryCode1.SelectedValue == "IN")
                        {
                            htParam.Add("@CORRES_STATE", ddlState1.SelectedValue);
                        }
                        else
                        {
                            htParam.Add("@CORRES_STATE", txtState1.Text.Trim());
                        }

                        //htParam.Add("@CORRES_STATE", ddlState1.SelectedValue);
                        htParam.Add("@CORRES_COUNTRY", ddlCountryCode1.SelectedValue);
                    }
                    else
                    {
                        htParam.Add("@CnctType2", "");
                        htParam.Add("@PERM_CORRES_SAMEFLAG", "02");//by meena 19/05/2017
                        htParam.Add("@CORRES_TYPE", System.DBNull.Value);
                        htParam.Add("@CORRES_POA", System.DBNull.Value);
                        htParam.Add("@CORRES_LINE1", System.DBNull.Value);
                        htParam.Add("@CORRES_LINE2", System.DBNull.Value);
                        htParam.Add("@CORRES_LINE3", System.DBNull.Value);
                        htParam.Add("@CORRES_CITY", System.DBNull.Value);
                        htParam.Add("@CORRES_DIST", System.DBNull.Value);
                        htParam.Add("@CORRES_PIN", System.DBNull.Value);
                        htParam.Add("@CORRES_STATE", System.DBNull.Value);
                        htParam.Add("@CORRES_COUNTRY", System.DBNull.Value);
                    }


                    htParam.Add("@SameasLocalAddressFlagJ1", "");

                    htParam.Add("@SameasLocalAddressFlagJ2", "");


                    htParam.Add("@CnctType3", "");
                    htParam.Add("@JURI_TYPE", System.DBNull.Value);
                    //htParam.Add("@CORRES_POA", ddlProofOfAddress1.SelectedValue);
                    htParam.Add("@JURI_LINE1", System.DBNull.Value);
                    htParam.Add("@JURI_LINE2", System.DBNull.Value);
                    htParam.Add("@JURI_LINE3", System.DBNull.Value);
                    htParam.Add("@JURI_CITY", System.DBNull.Value);
                    htParam.Add("@JURI_PIN", System.DBNull.Value);
                    htParam.Add("@JURI_DISTRICT", System.DBNull.Value);
                    htParam.Add("@JURI_STATE", System.DBNull.Value);
                    htParam.Add("@JURI_COUNTRY", System.DBNull.Value);

                    htParam.Add("@OFF_STD_CODE", txtTelOff.Text.Trim());
                    htParam.Add("@RESI_STD_CODE", txtTelRes.Text.Trim());
                    htParam.Add("@MOB_CODE", txtMobile.Text.Trim());
                    htParam.Add("@FAX_CODE", txtFax1.Text);

                    htParam.Add("@OFF_TEL_NUM", txtTelOff2.Text);
                    htParam.Add("@RESI_TEL_NUM", txtTelRes2.Text);
                    htParam.Add("@FAX_NO", txtFax2.Text);
                    htParam.Add("@MOB_NUM", txtMobile2.Text);
                    htParam.Add("@EMAIL", txtemail.Text);
                    htParam.Add("@Remarks", txtRemarks.Text.Trim());
                    htParam.Add("@DEC_DATE", txtDate.Text.Trim());
                    htParam.Add("@DEC_PLACE", txtPlace.Text.Trim());
                    htParam.Add("@KYC_NAME", txtEmpName.Text.Trim());
                    htParam.Add("@KYC_EMPCODE", txtEmpCode.Text.Trim());
                    htParam.Add("@KYC_BRANCH", txtEmpBranch.Text.Trim());
                    htParam.Add("@KYC_DESIGNATION", txtEmpDesignation.Text.Trim());
                    htParam.Add("@KYC_DATE", txtDateKYCver.Text.Trim());
                    htParam.Add("@DOC_SUB", ddlDocReceived.SelectedValue);


                    htParam.Add("@ORG_NAME", txtInsName.Text.Trim());
                    htParam.Add("@ORG_CODE", txtInsCode.Text.Trim());
                    htParam.Add("@CreatedBy", strUserId.ToString());

                    //htParam.Add("@TKYCNO", "");
                    //htParam.Add("@Uniqueno", obj.ToString());
                    //htParam.Add("@Usages", "W");
                    //htParam.Add("@Mode", Request.QueryString["Status"].ToString());//Reg or Mod

                    //if (Request.QueryString["Status"].ToString() == "PMod")
                    //{
                    //    htParam.Add("@PSTempRefNo", txtRefNumber.Text.ToString());
                    //    objds = objDAL.GetDataSet("prc_updatekycPartialdtls", htParam, "STAGINGConnectionString");

                    //}
                    //else if (Request.QueryString["Status"].ToString() == "Reg")
                    //{ 
                    //objds = objDAL.GetDataSet("Prc_InsCkycPartialDtls", htParam, "STAGINGConnectionString");
                    //}

                    //if (objds.Tables.Count > 0)
                    //{
                    //    if (objds.Tables[0].Rows.Count > 0)
                    //    {
                    //        PSTempRefNo = objds.Tables[0].Rows[0]["PSTempRefNo"].ToString();
                    //    }
                    //}
                    objDAL = new DataAccessLayer("STAGINGConnectionString");
                    PSTempRefNo = (objDAL.ExecuteScalar("Prc_updPartialEntkycdtls_Web", htParam)).ToString(); //Prc_InsCkycPartialDtls

                    #endregion

                    #region Related Partial Details

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                htParam.Clear();
                                htParam.Add("@FiRefNo", dt.Rows[i]["FiRefNo"]);

                                htParam.Add("@PSTempRelRefNo", dt.Rows[i]["RelRefNo"]);

                                htParam.Add("@PSTempRefNo", Request.QueryString["refno"].ToString());
                                //htParam.Add("@PSTempRefNo", PSTempRefNo);
                                if (chkAddRel.Checked == true)
                                {
                                    htParam.Add("@AddDelFlagRel", "01");
                                }
                                else
                                {
                                    htParam.Add("@AddDelFlagRel", System.DBNull.Value);
                                }

                                htParam.Add("@RelatedPrsnKYCNo", dt.Rows[i]["RelatedPrsnKYCNo"]);
                                htParam.Add("@RelationType", dt.Rows[i]["RelationType"]);
                                htParam.Add("@RelCKYCNO", dt.Rows[i]["RelatedPrsnKYCNo"]);

                                //--
                                htParam.Add("@RelPersonAs", dt.Rows[i]["RelPersonAs"]);

                                htParam.Add("@RelEntName", dt.Rows[i]["RelEntName"]);
                                htParam.Add("@RelDtofIncorporation", dt.Rows[i]["RelDtofIncorporation"]);
                                htParam.Add("@RelDtofCommencementofbusi", dt.Rows[i]["RelDtofCommencementofbusi"]);
                                htParam.Add("@RelPlaceofIncorportation", dt.Rows[i]["RelPlaceofIncorportation"]);
                                htParam.Add("@RelCountryofIncorporation", dt.Rows[i]["RelCountryofIncorporation"]);
                                htParam.Add("@RelCountryofResAsperTaxLaws", dt.Rows[i]["RelCountryofResAsperTaxLaws"]);
                                htParam.Add("@RelIdType", dt.Rows[i]["RelIdType"]);
                                htParam.Add("@RelPAN", dt.Rows[i]["RelPAN"]);
                                htParam.Add("@RelTINIdNo", dt.Rows[i]["RelTINIdNo"]);
                                htParam.Add("@RelTINCountry", dt.Rows[i]["RelTINCountry"]);

                                htParam.Add("@RelTelSTDCode", dt.Rows[i]["RelTelSTDCode"]);
                                htParam.Add("@RelTelNo", dt.Rows[i]["RelTelNo"]);
                                htParam.Add("@RelOfficeTelSTDCode", dt.Rows[i]["RelOfficeTelSTDCode"]);
                                htParam.Add("@RelOfficeTelNo", dt.Rows[i]["RelOfficeTelNo"]);
                                htParam.Add("@RelMobCode", dt.Rows[i]["RelMobCode"]);
                                htParam.Add("@RelMobileNo", dt.Rows[i]["RelMobileNo"]);
                                htParam.Add("@RelFaxNoCode", dt.Rows[i]["RelFaxNoCode"]);
                                htParam.Add("@RelFaxNo", dt.Rows[i]["RelFaxNo"]);
                                htParam.Add("@RelEmailID", dt.Rows[i]["RelEmailID"]);
                                //--

                                htParam.Add("@PrefixRel", dt.Rows[i]["PrefixRel"]);
                                htParam.Add("@FNameRel", dt.Rows[i]["FNameRel"]);
                                htParam.Add("@MNameRel", dt.Rows[i]["MNameRel"]);
                                htParam.Add("@LNameRel", dt.Rows[i]["LNameRel"]);
                                htParam.Add("@MaidPrefixRel", dt.Rows[i]["MaidPrefixRel"]);
                                htParam.Add("@MaidFNameRel", dt.Rows[i]["MaidFNameRel"]);
                                htParam.Add("@MaidMNameRel", dt.Rows[i]["MaidMNameRel"]);
                                htParam.Add("@MaidLNameRel", dt.Rows[i]["MaidLNameRel"]);
                                htParam.Add("@FSFlagRel", dt.Rows[i]["FSFlagRel"]);
                                htParam.Add("@FatherPrefixRel", dt.Rows[i]["FatherPrefixRel"]);
                                htParam.Add("@FatherFNameRel", dt.Rows[i]["FatherFNameRel"]);
                                htParam.Add("@FatherMNameRel", dt.Rows[i]["FatherMNameRel"]);
                                htParam.Add("@FatherLNameRel", dt.Rows[i]["FatherLNameRel"]);
                                htParam.Add("@MotherPrefixRel", dt.Rows[i]["MotherPrefixRel"]);
                                htParam.Add("@MotherFNameRel", dt.Rows[i]["MotherFNameRel"]);
                                htParam.Add("@MotherMNameRel", dt.Rows[i]["MotherMNameRel"]);
                                htParam.Add("@MotherLNameRel", dt.Rows[i]["MotherLNameRel"]);
                                htParam.Add("@DOBRel", dt.Rows[i]["DOBRel"]);
                                htParam.Add("@GenderRel", dt.Rows[i]["GenderRel"]);
                                htParam.Add("@MaritalStatusRel", dt.Rows[i]["MaritalStatusRel"]);
                                htParam.Add("@CitizenshipRel", dt.Rows[i]["CitizenshipRel"]);
                                htParam.Add("@ResiStatusRel", dt.Rows[i]["ResiStatusRel"]);
                                htParam.Add("@OccuTypeRel", dt.Rows[i]["OccuTypeRel"]);

                                htParam.Add("@OccuSubTypeRel", dt.Rows[i]["OccuSubTypeRel"]);

                                htParam.Add("@ResForTaxFlagRel", dt.Rows[i]["ResForTaxFlagRel"]);
                                htParam.Add("@ISOCountryCodeRel", dt.Rows[i]["ISOCountryCodeRel"]);
                                htParam.Add("@TaxIDNumberRel", dt.Rows[i]["TaxIDNumberRel"]);
                                htParam.Add("@BirthCityRel", dt.Rows[i]["BirthCityRel"]);
                                htParam.Add("@ISOBirthPlaceCodeRel", dt.Rows[i]["ISOBirthPlaceCodeRel"]);
                                htParam.Add("@IdType", dt.Rows[i]["IdType"]);
                                htParam.Add("@IdNumber", dt.Rows[i]["IdNumber"]);
                                htParam.Add("@IDExpDate", dt.Rows[i]["IdExpDate"]);
                                htParam.Add("@IDName", dt.Rows[i]["IdName"]);

                                htParam.Add("@CnctTypeRel", "P1");
                                htParam.Add("@PER_ADDTYPE", dt.Rows[i]["AdrTypeRel"]);
                                htParam.Add("@PER_ADDPROOF", dt.Rows[i]["AdrProfRel"]);
                                htParam.Add("@PER_ADDLINE1", dt.Rows[i]["Adr1Rel"]);
                                htParam.Add("@PER_ADDLINE2", dt.Rows[i]["Adr2Rel"]);
                                htParam.Add("@PER_ADDLINE3", dt.Rows[i]["Adr3Rel"]);
                                htParam.Add("@PER_CITY", dt.Rows[i]["CityRel"]);
                                htParam.Add("@PER_DISTRICT", dt.Rows[i]["DistrictRel"]);
                                htParam.Add("@PER_PIN", dt.Rows[i]["PostCodeRel"]);
                                htParam.Add("@PER_STATECODE", dt.Rows[i]["StateCodeRel"]);
                                htParam.Add("@PER_COUNTRY_CODE", dt.Rows[i]["CntryCodeRel"]);

                                //added by daksh
                                htParam.Add("@CnctTypeRel1", "M1");
                                htParam.Add("@corPER_ADDTYPE", dt.Rows[i]["AdrTypeRel1"]);
                                //htParam.Add("@PER_ADDPROOF", dt.Rows[i]["AdrProfRel1"]);
                                htParam.Add("@corPER_ADDLINE1", dt.Rows[i]["Adr1Rel1"]);
                                htParam.Add("@corPER_ADDLINE2", dt.Rows[i]["Adr2Rel1"]);
                                htParam.Add("@corPER_ADDLINE3", dt.Rows[i]["Adr3Rel1"]);
                                htParam.Add("@corPER_CITY", dt.Rows[i]["CityRel1"]);
                                htParam.Add("@corPER_DISTRICT", dt.Rows[i]["DistrictRel1"]);
                                htParam.Add("@corPER_PIN", dt.Rows[i]["PostCodeRel1"]);
                                htParam.Add("@corPER_STATECODE", dt.Rows[i]["StateCodeRel1"]);
                                htParam.Add("@corPER_COUNTRY_CODE", dt.Rows[i]["CntryCodeRel1"]);

                                htParam.Add("@CnctTypeRel2", "J1");
                                htParam.Add("@jurPER_ADDTYPE", dt.Rows[i]["AdrTypeRel2"]);
                                //htParam.Add("@PER_ADDPROOF", dt.Rows[i]["AdrProfRel"]);
                                htParam.Add("@jurPER_ADDLINE1", dt.Rows[i]["Adr1Rel2"]);
                                htParam.Add("@jurPER_ADDLINE2", dt.Rows[i]["Adr2Rel2"]);
                                htParam.Add("@jurPER_ADDLINE3", dt.Rows[i]["Adr3Rel2"]);
                                htParam.Add("@jurPER_CITY", dt.Rows[i]["CityRel2"]);
                                htParam.Add("@jurPER_DISTRICT", dt.Rows[i]["DistrictRel2"]);
                                htParam.Add("@jurPER_PIN", dt.Rows[i]["PostCodeRel2"]);
                                htParam.Add("@jurPER_STATECODE", dt.Rows[i]["StateCodeRel2"]);
                                htParam.Add("@jurPER_COUNTRY_CODE", dt.Rows[i]["CntryCodeRel2"]);

                                htParam.Add("@SameasPOIAddresFlagP1", dt.Rows[i]["SameasPOIAddresFlagP1"]);
                                htParam.Add("@SameasCurrentAddresFlagM1", dt.Rows[i]["SameasCurrentAddresFlagM1"]);
                                htParam.Add("@SameasLocalAddressFlagJ1", dt.Rows[i]["SameasLocalAddressFlagJ1"]);
                                htParam.Add("@SameasLocalAddressFlagJ2", dt.Rows[i]["SameasLocalAddressFlagJ2"]);

                                //end

                                htParam.Add("@AddIdType", dt.Rows[i]["AddIdTypeRel"]);
                                htParam.Add("@PERM_IDNUMBAER", dt.Rows[i]["AddIdNumberRel"]);
                                htParam.Add("@PERM_IDEXPDT", dt.Rows[i]["AddIdExpDateRel"]);
                                htParam.Add("@PERM_POAOTHERS", dt.Rows[i]["AddIdNameRel"]);
                                htParam.Add("@DecDateRel", dt.Rows[i]["DecDateRel"]);
                                htParam.Add("@DecPlaceRel", dt.Rows[i]["DecPlaceRel"]);
                                htParam.Add("@kycEmpNameRel", dt.Rows[i]["kycEmpNameRel"]);
                                htParam.Add("@kycEmpCodeRel", dt.Rows[i]["kycEmpCodeRel"]);
                                htParam.Add("@kycEmpBranchRel", dt.Rows[i]["kycEmpBranchRel"]);
                                htParam.Add("@kycEmpDesiRel", dt.Rows[i]["kycEmpDesiRel"]);
                                htParam.Add("@kycVerDateRel", dt.Rows[i]["kycVerDateRel"]);
                                htParam.Add("@kycCertDocRel", "01");
                                htParam.Add("@kycInstNameRel", dt.Rows[i]["kycInstNameRel"]);
                                htParam.Add("@kycInstCodeRel", dt.Rows[i]["kycInstCodeRel"]);
                                htParam.Add("@CreateBy", strUserId.ToString());
                                objDAL.ExecuteNonQuery("prc_updKycRelatedPrsnDtls", htParam);
                                Session["dsRel"] = null;
                            }
                        }
                    }
                    #endregion

                    #region Partial save Controlling Person Details

                    if (ctrldt != null)
                    {
                        if (ctrldt.Rows.Count > 0)
                        {
                            for (int i = 0; i < ctrldt.Rows.Count; i++)
                            {

                                htParam.Clear();
                                //htParam.Add("@PSTempRefNo", PSTempRefNo);
                                htParam.Add("@PSTempRefNo", Request.QueryString["refno"].ToString());

                                htParam.Add("@PSTempRelRefNo", ctrldt.Rows[i]["RelRefNo"]);

                                htParam.Add("@FiRefNo", ctrldt.Rows[i]["FiRefNo"]);

                                htParam.Add("@AddDelFlagRel ", "");

                                htParam.Add("@RelatedPrsnKYCNo", ctrldt.Rows[i]["RelatedPrsnKYCNo"]);
                                htParam.Add("@RelationType", ctrldt.Rows[i]["RelationType"]);
                                htParam.Add("@RelCKYCNO", dt.Rows[i]["RelatedPrsnKYCNo"]);
                                htParam.Add("@PrefixRel", ctrldt.Rows[i]["PrefixRel"]);
                                htParam.Add("@FNameRel", ctrldt.Rows[i]["FNameRel"]);
                                htParam.Add("@MNameRel", ctrldt.Rows[i]["MNameRel"]);
                                htParam.Add("@LNameRel", ctrldt.Rows[i]["LNameRel"]);
                                htParam.Add("@MaidPrefixRel", ctrldt.Rows[i]["MaidPrefixRel"]);
                                htParam.Add("@MaidFNameRel", ctrldt.Rows[i]["MaidFNameRel"]);
                                htParam.Add("@MaidMNameRel", ctrldt.Rows[i]["MaidMNameRel"]);
                                htParam.Add("@MaidLNameRel", ctrldt.Rows[i]["MaidLNameRel"]);
                                htParam.Add("@FSFlagRel", ctrldt.Rows[i]["FSFlagRel"]);
                                htParam.Add("@FatherPrefixRel", ctrldt.Rows[i]["FatherPrefixRel"]);
                                htParam.Add("@FatherFNameRel", ctrldt.Rows[i]["FatherFNameRel"]);
                                htParam.Add("@FatherMNameRel", ctrldt.Rows[i]["FatherMNameRel"]);
                                htParam.Add("@FatherLNameRel", ctrldt.Rows[i]["FatherLNameRel"]);
                                htParam.Add("@MotherPrefixRel", ctrldt.Rows[i]["MotherPrefixRel"]);
                                htParam.Add("@MotherFNameRel", ctrldt.Rows[i]["MotherFNameRel"]);
                                htParam.Add("@MotherMNameRel", ctrldt.Rows[i]["MotherMNameRel"]);
                                htParam.Add("@MotherLNameRel", ctrldt.Rows[i]["MotherLNameRel"]);
                                htParam.Add("@DOBRel", ctrldt.Rows[i]["DOBRel"]);
                                htParam.Add("@GenderRel", ctrldt.Rows[i]["GenderRel"]);
                                htParam.Add("@MaritalStatusRel", ctrldt.Rows[i]["MaritalStatusRel"]);
                                htParam.Add("@CitizenshipRel", ctrldt.Rows[i]["CitizenshipRel"]);
                                htParam.Add("@ResiStatusRel", ctrldt.Rows[i]["ResiStatusRel"]);
                                htParam.Add("@OccuTypeRel", ctrldt.Rows[i]["OccuTypeRel"]);

                                htParam.Add("@ResForTaxFlagRel", ctrldt.Rows[i]["ResForTaxFlagRel"]);
                                htParam.Add("@ISOCountryCodeRel", ctrldt.Rows[i]["ISOCountryCodeRel"]);
                                htParam.Add("@TaxIDNumberRel", ctrldt.Rows[i]["TaxIDNumberRel"]);
                                htParam.Add("@BirthCityRel", ctrldt.Rows[i]["BirthCityRel"]);
                                htParam.Add("@ISOBirthPlaceCodeRel", ctrldt.Rows[i]["ISOBirthPlaceCodeRel"]);

                                htParam.Add("@IdType", ctrldt.Rows[i]["IdType"]);
                                htParam.Add("@IdNumber", ctrldt.Rows[i]["IdNumber"]);
                                htParam.Add("@IDExpDate", ctrldt.Rows[i]["IdExpDate"]);
                                htParam.Add("@IDName", ctrldt.Rows[i]["IdName"]);

                                htParam.Add("@CnctTypeRel", "P1");
                                htParam.Add("@PER_ADDTYPE", ctrldt.Rows[i]["AdrTypeRel"]);
                                htParam.Add("@PER_ADDPROOF", ctrldt.Rows[i]["AdrProfRel"]);
                                htParam.Add("@PER_ADDLINE1", ctrldt.Rows[i]["Adr1Rel"]);
                                htParam.Add("@PER_ADDLINE2", ctrldt.Rows[i]["Adr2Rel"]);
                                htParam.Add("@PER_ADDLINE3", ctrldt.Rows[i]["Adr3Rel"]);
                                htParam.Add("@PER_CITY", ctrldt.Rows[i]["CityRel"]);
                                htParam.Add("@PER_DISTRICT", ctrldt.Rows[i]["DistrictRel"]);
                                htParam.Add("@PER_PIN", ctrldt.Rows[i]["PostCodeRel"]);
                                htParam.Add("@PER_STATECODE", ctrldt.Rows[i]["StateCodeRel"]);
                                htParam.Add("@PER_COUNTRY_CODE", ctrldt.Rows[i]["CntryCodeRel"]);

                                //added by daksh
                                htParam.Add("@CnctTypeRel1", "M1");
                                htParam.Add("@corPER_ADDTYPE", ctrldt.Rows[i]["AdrTypeRel1"]);
                                //htParam.Add("@PER_ADDPROOF", dtRel.Rows[i]["AdrProfRel1"]);
                                htParam.Add("@corPER_ADDLINE1", ctrldt.Rows[i]["Adr1Rel1"]);
                                htParam.Add("@corPER_ADDLINE2", ctrldt.Rows[i]["Adr2Rel1"]);
                                htParam.Add("@corPER_ADDLINE3", ctrldt.Rows[i]["Adr3Rel1"]);
                                htParam.Add("@corPER_CITY", ctrldt.Rows[i]["CityRel1"]);
                                htParam.Add("@corPER_DISTRICT", ctrldt.Rows[i]["DistrictRel1"]);
                                htParam.Add("@corPER_PIN", ctrldt.Rows[i]["PostCodeRel1"]);
                                htParam.Add("@corPER_STATECODE", ctrldt.Rows[i]["StateCodeRel1"]);
                                htParam.Add("@corPER_COUNTRY_CODE", ctrldt.Rows[i]["CntryCodeRel1"]);

                                htParam.Add("@CnctTypeRel2", "J1");
                                htParam.Add("@jurPER_ADDTYPE", ctrldt.Rows[i]["AdrTypeRel2"]);
                                //htParam.Add("@PER_ADDPROOF", ctrldt.Rows[i]["AdrProfRel"]);
                                htParam.Add("@jurPER_ADDLINE1", ctrldt.Rows[i]["Adr1Rel2"]);
                                htParam.Add("@jurPER_ADDLINE2", ctrldt.Rows[i]["Adr2Rel2"]);
                                htParam.Add("@jurPER_ADDLINE3", ctrldt.Rows[i]["Adr3Rel2"]);
                                htParam.Add("@jurPER_CITY", ctrldt.Rows[i]["CityRel2"]);
                                htParam.Add("@jurPER_DISTRICT", ctrldt.Rows[i]["DistrictRel2"]);
                                htParam.Add("@jurPER_PIN", ctrldt.Rows[i]["PostCodeRel2"]);
                                htParam.Add("@jurPER_STATECODE", ctrldt.Rows[i]["StateCodeRel2"]);
                                htParam.Add("@jurPER_COUNTRY_CODE", ctrldt.Rows[i]["CntryCodeRel2"]);

                                htParam.Add("@SameasPOIAddresFlagP1", ctrldt.Rows[i]["SameasPOIAddresFlagP1"]);
                                htParam.Add("@SameasCurrentAddresFlagM1", ctrldt.Rows[i]["SameasCurrentAddresFlagM1"]);
                                htParam.Add("@SameasLocalAddressFlagJ1", ctrldt.Rows[i]["SameasLocalAddressFlagJ1"]);
                                htParam.Add("@SameasLocalAddressFlagJ2", ctrldt.Rows[i]["SameasLocalAddressFlagJ2"]);

                                //end


                                htParam.Add("@AddIdType", ctrldt.Rows[i]["AddIdTypeRel"]);
                                htParam.Add("@PERM_IDNUMBAER", ctrldt.Rows[i]["AddIdNumberRel"]);
                                htParam.Add("@PERM_IDEXPDT", ctrldt.Rows[i]["AddIdExpDateRel"]);
                                htParam.Add("@PERM_POAOTHERS", ctrldt.Rows[i]["AddIdNameRel"]);

                                //New to add contact details start
                                htParam.Add("@OccuSubTypeRel", ctrldt.Rows[i]["OccuSubTypeRel"]);
                                htParam.Add("@TelSTDCodeRel", ctrldt.Rows[i]["TelSTDCode"]);
                                htParam.Add("@TelNoRel", ctrldt.Rows[i]["TelNo"]);
                                htParam.Add("@OfficeTelSTDCodeRel", ctrldt.Rows[i]["OfficeTelSTDCode"]);
                                htParam.Add("@OfficeTelNoRel", ctrldt.Rows[i]["OfficeTelNo"]);
                                htParam.Add("@MobCodeRel", ctrldt.Rows[i]["MobCode"]);
                                htParam.Add("@MobileNoRel", ctrldt.Rows[i]["MobileNo"]);
                                htParam.Add("@FaxNoCodeRel", ctrldt.Rows[i]["FaxNoCode"]);
                                htParam.Add("@FaxNoRel", ctrldt.Rows[i]["FaxNo"]);
                                htParam.Add("@EmailIDRel", ctrldt.Rows[i]["EMAILID"]);
                                //New to add contact details end

                                htParam.Add("@DecDateRel", ctrldt.Rows[i]["DecDateRel"]);
                                htParam.Add("@DecPlaceRel", ctrldt.Rows[i]["DecPlaceRel"]);
                                htParam.Add("@kycEmpNameRel", ctrldt.Rows[i]["kycEmpNameRel"]);
                                htParam.Add("@kycEmpCodeRel", ctrldt.Rows[i]["kycEmpCodeRel"]);
                                htParam.Add("@kycEmpBranchRel", ctrldt.Rows[i]["kycEmpBranchRel"]);
                                htParam.Add("@kycEmpDesiRel", ctrldt.Rows[i]["kycEmpDesiRel"]);
                                htParam.Add("@kycVerDateRel", ctrldt.Rows[i]["kycVerDateRel"]);
                                htParam.Add("@kycCertDocRel", "01");
                                htParam.Add("@kycInstNameRel", ctrldt.Rows[i]["kycInstNameRel"]);
                                htParam.Add("@kycInstCodeRel", ctrldt.Rows[i]["kycInstCodeRel"]);
                                htParam.Add("@CreateBy", strUserId.ToString());
                                // htParam.Add("@PAN", txtPanNo.Text.ToString());
                                objDAL.ExecuteNonQuery("prc_updKycCtrlPrsnPartialDtls", htParam);
                                Session["dsCtrl"] = null;
                            }
                        }
                    }

                    #endregion  controlling partial save end

                    hdnUpdate.Value = "Partial data updated succesfully.";

                    msg = hdnUpdate.Value + "</br></br>Temporary Reference No: " + PSTempRefNo.ToString().Trim() + "<br/> Entity Name: "
                         + txtKYCName.Text + "<br/><br/>";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "popup('" + msg + "');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);

                    btnPartialSave.Enabled = false;
                    btnSave.Enabled = false;
                    txtKYCNumber.Text = strRefNo.ToString().Trim();
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please,atleast fill Personal detail ')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + Res + "')", true);
                    return;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "btnPartialSave_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        protected void ddlCertifiecopy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtPassNo.Visible = false;
                txtPassNo.Text = string.Empty;
                if (ddlCertifiecopy.SelectedIndex == 0)
                {
                    divIdProof.Visible = false;
                    chkSameAsPOI.Checked = false;
                    ddlProofOfAddress.SelectedIndex = 0;
                    ddlProofOfAddress.Enabled = true;

                }
                else if (ddlCertifiecopy.SelectedIndex == 1)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Certificate Of Incorporation No.";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 60;
                    chkSameAsPOI.Checked = false;
                    ddlProofOfAddress.SelectedIndex = 0;
                    ddlProofOfAddress.Enabled = true;
                    //txtPassNo.Focus();
                    //txtPassNo.Attributes.Remove("onblur");
                    //txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");
                }
                else if (ddlCertifiecopy.SelectedIndex == 2)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Registration Certificate No.";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 60;

                    //if (ddlProofOfAddress.SelectedValue != "PA02")
                    //{
                    chkSameAsPOI.Checked = false;
                    ddlProofOfAddress.SelectedIndex = 0;
                    ddlProofOfAddress.Enabled = true;
                    //}

                    //txtPassNo.Focus();
                    //txtPassNo.Attributes.Remove("onblur");
                }
                else if (ddlCertifiecopy.SelectedIndex == 3)
                {

                    divIdProof.Visible = false;
                    lblPassportNo.Text = "";
                    txtPassNo.Visible = false;
                }
                else if (ddlCertifiecopy.SelectedIndex == 4)
                {
                    divIdProof.Visible = false;
                    lblPassportNo.Text = "";
                    txtPassNo.Visible = false;
                }
                else if (ddlCertifiecopy.SelectedIndex == 5)
                {
                    divIdProof.Visible = false;
                    lblPassportNo.Text = "";
                    txtPassNo.Visible = false;
                }
                else
                {
                    divIdProof.Visible = false;
                    lblPassportNo.Text = "";
                    txtPassNo.Visible = false;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 50;
                    //txtPassNo.Focus();
                    //txtPassNo.Attributes.Remove("onblur");
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
                    objErr.LogErr(AppID, "LegalEntityDtls.aspx.cs", "ddlCertifiecopy_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        protected void chkHigh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHigh.Checked == true)
            {
                chkMedium.Checked = false;
                chkLow.Checked = false;
            }
        }

        protected void chkMedium_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMedium.Checked == true)
            {
                chkHigh.Checked = false;
                chkLow.Checked = false;
            }
        }

        protected void chkLow_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLow.Checked == true)
            {
                chkMedium.Checked = false;
                chkHigh.Checked = false;
            }
        }

        protected void txtRefNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtRefNumber.Text.ToString() != "")
            {
                string strExists = "";
                string msg = "";
                strExists = ChkDedupReg(txtRefNumber.Text.ToString(), "FI", "1");
                if (strExists == "T")
                {
                    //DisableLinkButton(btnSave);
                    //DisableLinkButton(btnPartialSave);
                    msg = "FI Reference Number ( " + txtRefNumber.Text.ToString() + " ) is already exist. ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true); //"AlertMsg('" + msg + "')"
                    txtRefNumber.Text = "";
                    spnValidRefNo.Attributes.Add("style", "display:none; color: green !important; padding-left: 1% !important;");
                    //btnSave.Enabled = false;
                    //btnPartialSave.Enabled = false;
                    return;
                }
                else
                {
                    spnValidRefNo.Attributes.Add("style", "display:block; color: green !important; padding-left: 1% !important;");
                    //EnableLinkButton(btnSave);
                    //EnableLinkButton(btnPartialSave);
                    //btnSave.Enabled = true;
                    //btnPartialSave.Enabled = true;
                }

                if (strExists == "F")
                {

                }
            }
            else
            {
                spnValidRefNo.Attributes.Add("style", "display:none; color: green !important; padding-left: 1% !important;");
                EnableLinkButton(btnSave);
                EnableLinkButton(btnPartialSave);
                //btnSave.Enabled = true;
                //btnPartialSave.Enabled = true;
            }
        }  //

        public static void EnableLinkButton(LinkButton linkButton)
        {
            linkButton.Attributes.Remove("href");
            linkButton.Attributes.CssStyle[HtmlTextWriterStyle.Color] = "white";
            linkButton.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "pointer";
            if (linkButton.Enabled != true)
            {
                linkButton.Enabled = true;
            }

            if (linkButton.OnClientClick != null)
            {
                linkButton.OnClientClick = null;
            }
        }

        public static void DisableLinkButton(LinkButton linkButton)
        {
            linkButton.Attributes.Remove("href");
            linkButton.Attributes.CssStyle[HtmlTextWriterStyle.Color] = "gray";
            linkButton.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";
            if (linkButton.Enabled != false)
            {
                linkButton.Enabled = false;
            }

            if (linkButton.OnClientClick != null)
            {
                linkButton.OnClientClick = null;
            }
        }

        protected void txtDatOfInc_TextChanged(object sender, EventArgs e)
        {
            txtPlaceOfInc.Text = "";
        }

        //protected void txtKYCNumber_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtKYCNumber.Text.ToString() != "")
        //    {
        //        string strExists = "";
        //        strExists = ChkDedupReg(txtKYCNumber.Text.ToString(), "CKYC", "1");
        //        if (strExists == "T")
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('This CKYC Number ( '" + txtKYCNumber.Text.ToString() + "' ) is already exist.')", true);
        //            return;
        //        }

        //        if (strExists == "F")
        //        {

        //        }
        //    }
        //}

        private string ChkDedupReg(string strFIRefNo, string strRegFlag, string strType)
        {
            string str = "";


            objDAL = new DataAccessLayer("CKYCConnectionString");
            dt = new DataTable();
            htParam.Clear();
            htParam.Add("@RefNo", strFIRefNo);
            htParam.Add("@RegFlag", strRegFlag);
            htParam.Add("@Type", strType);
            dt = objDAL.GetDataTable("prcCheckDeDupReg", htParam);

            //htParam.Clear();
            //htParam = new Hashtable();
            //dt = new DataTable();
            //dt.Clear();

            // htParam.Add("@PAN", txtPanNo.Text.ToString());
            //dt = objDAL.GetDataTable("prcCheckDeDupReg", htParam);
            str = dt.Rows[0]["Exists"].ToString();
            return str;
        }

        protected void txtPassNo_TextChanged(object sender, EventArgs e)
        {
            string strExists = "";

            objDAL = new DataAccessLayer("CKYCConnectionString");
            dt = new DataTable();
            htParam.Clear();
            //htParam.Add("@RefNo", txtRefNumber.Text.ToString());
            htParam.Add("@RegFlag", "POI");
            htParam.Add("@IDType", ddlCertifiecopy.SelectedValue);
            htParam.Add("@IDNo", txtPassNo.Text.ToString());
            dt = objDAL.GetDataTable("prcCheckDeDupPOIReg", htParam);

            //htParam.Clear();
            //htParam = new Hashtable();
            //dt = new DataTable();
            //dt.Clear();

            // htParam.Add("@PAN", txtPanNo.Text.ToString());
            //dt = objDAL.GetDataTable("prcCheckDeDupReg", htParam);
            strExists = dt.Rows[0]["Exists"].ToString();
            if (txtPassNo.Text != "")
            {
                if (strExists == "T")
                {
                    //DisableLinkButton(btnSave);
                    //DisableLinkButton(btnPartialSave);
                    msg = "Identification Number ( " + txtPassNo.Text.ToString() + " ) is already exist.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true); //"AlertMsg('" + msg + "')"
                    //btnSave.Enabled = false;
                    //btnPartialSave.Enabled = false;
                    txtPassNo.Text = "";
                    return;
                }
                else
                {
                    //EnableLinkButton(btnSave);
                    //EnableLinkButton(btnPartialSave);
                    //btnSave.Enabled = true;
                    //btnPartialSave.Enabled = true;
                }
            }
            //return strExists;
        }

        protected void txtPanNo_TextChanged(object sender, EventArgs e)
        {
            string strExists = "";

            objDAL = new DataAccessLayer("CKYCConnectionString");
            dt = new DataTable();
            htParam.Clear();
            //htParam.Add("@RefNo", txtRefNumber.Text.ToString());
            htParam.Add("@RegFlag", "PAN");
            //htParam.Add("@IDType", ddlCertifiecopy.SelectedValue);
            htParam.Add("@IDNo", txtPanNo.Text.ToString());
            dt = objDAL.GetDataTable("prcCheckDeDupPOIReg", htParam);

            //htParam.Clear();
            //htParam = new Hashtable();
            //dt = new DataTable();
            //dt.Clear();

            // htParam.Add("@PAN", txtPanNo.Text.ToString());
            //dt = objDAL.GetDataTable("prcCheckDeDupReg", htParam);
            strExists = dt.Rows[0]["Exists"].ToString();
            if (txtPanNo.Text != "")
            {
                chkPanForm.Checked = false;
                chkPanForm.Enabled = false;
                if (strExists == "T")
                {
                    //DisableLinkButton(btnSave);
                    //DisableLinkButton(btnPartialSave);
                    msg = "PAN Number ( " + txtPanNo.Text.ToString() + " ) is already exist.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true); //"AlertMsg('" + msg + "')"
                    //btnSave.Enabled = false;
                    //btnPartialSave.Enabled = false;
                    txtPanNo.Text = "";
                    return;
                }
                else
                {
                    //chkPanForm.Enabled = false;
                    //EnableLinkButton(btnSave);
                    //EnableLinkButton(btnPartialSave);
                    //btnSave.Enabled = true;
                    //btnPartialSave.Enabled = true;
                }
            }
            else
            {
                chkPanForm.Checked = false;
                chkPanForm.Enabled = true;
            }
        }

        protected void txtTypeIdentiNo_TextChanged(object sender, EventArgs e)
        {
            string strExists = "";

            objDAL = new DataAccessLayer("CKYCConnectionString");
            dt = new DataTable();
            htParam.Clear();
            //htParam.Add("@RefNo", txtRefNumber.Text.ToString());
            htParam.Add("@RegFlag", "TIN");
            //htParam.Add("@IDType", ddlCertifiecopy.SelectedValue);
            htParam.Add("@IDNo", txtTypeIdentiNo.Text.ToString());
            dt = objDAL.GetDataTable("prcCheckDeDupPOIReg", htParam);

            //htParam.Clear();
            //htParam = new Hashtable();
            //dt = new DataTable();
            //dt.Clear();

            // htParam.Add("@PAN", txtPanNo.Text.ToString());
            //dt = objDAL.GetDataTable("prcCheckDeDupReg", htParam);
            strExists = dt.Rows[0]["Exists"].ToString();
            if (txtTypeIdentiNo.Text != "")
            {
                if (strExists == "T")
                {
                    //dvTINCntry.Visible = false;
                    //txtTypeIdentiNo.Text = "";
                    DisableLinkButton(btnSave);
                    DisableLinkButton(btnPartialSave);
                    msg = "TIN Number ( " + txtTypeIdentiNo.Text.ToString() + " ) is already exist.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true); //"AlertMsg('" + msg + "')"
                    txtTypeIdentiNo.Text = "";
                    dvTINCntry.Attributes.Add("style", "display:none");
                    //btnSave.Enabled = false;
                    //btnPartialSave.Enabled = false;
                    return;
                }
                else
                {
                    //dvTINCntry.Visible = true;
                    dvTINCntry.Attributes.Add("style", "display:block");
                    EnableLinkButton(btnSave);
                    EnableLinkButton(btnPartialSave);
                    //btnSave.Enabled = true;
                    //btnPartialSave.Enabled = true;
                }
            }
            else
            {
                EnableLinkButton(btnSave);
                EnableLinkButton(btnPartialSave);
            }
        }

        //protected void ddlCitizenship_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ddlCitizenship.SelectedValue == "OCIZ")
        //        {
        //            Fillcountrycd();
        //            lblIsoCountryCodeOthr.Visible = true;
        //            asteriskIsoCountryCodeOthr.Visible = true;
        //            ddlIsoCountryCodeOthr.Visible = true;

        //            ddlCountryCode1.SelectedIndex = 0;
        //            ddlCountryCode.SelectedIndex = 0;

        //            ddlCountryCode1.Enabled = true;
        //            ddlCountryCode.Enabled = true;

        //            txtMobile.Enabled = true;
        //            txtMobile.Text = "";
        //        }
        //        else
        //        {
        //            lblIsoCountryCodeOthr.Visible = false;
        //            asteriskIsoCountryCodeOthr.Visible = false;
        //            ddlIsoCountryCodeOthr.Visible = false;

        //            ddlCountryCode1.SelectedValue = "IN";
        //            ddlCountryCode.SelectedValue = "IN";

        //            ddlCountryCode1.Enabled = false;
        //            ddlCountryCode.Enabled = false;

        //            txtMobile.Enabled = false;
        //            txtMobile.Text = "91";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
        //        {
        //            Response.Redirect("~/ErrorSession.aspx");
        //        }
        //        else
        //        {
        //            objErr = new ErrorLog();
        //            objErr.LogErr(AppID, "CkycReg.aspx.cs", "ddlCitizenship_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
        //        }
        //    }
        //}

        #region METHOD "FillRequiredDataForPartialSave"
        protected void FillRequiredDataForPartialSave()
        {
            try
            {

                objDAL = new DataAccessLayer("STAGINGConnectionString");
                dt = new DataTable();
                htParam.Clear();
                htParam.Add("@PSTempRefNo", Request.QueryString["refno"].ToString());
                dt = objDAL.GetDataTable("getLegPartialSearchData", htParam);

                cbNew.Visible = true;
                cbNew.Checked = true;

                if ((dt.Rows[0]["AccountHolderTypeFlag"]).ToString() == "1")
                {
                    //chkUSReport.Checked = true;
                    //ddlAccHolderType.SelectedValue = Convert.ToString(dt.Rows[0]["AccountHolderType"]);
                }

                if ((dt.Rows[0]["AccountHolderTypeFlag"]).ToString() == "2")
                {
                }

                #region
                objDAL = new DataAccessLayer("STAGINGConnectionString");
                htParam.Clear();
                dt.Clear();
                htParam.Add("@PSTempRefNo", Request.QueryString["refno"].ToString());
                dt = objDAL.GetDataTable("getLegPartialSearchData", htParam);


                txtRefNumber.Text = Convert.ToString(dt.Rows[0]["FIRefNo"]);
                txtKYCNumber.Text = Convert.ToString(dt.Rows[0]["CKYCNO"]);
                // txtRefNumber.Text = Convert.ToString(dt.Rows[0]["PSTempRefNo"]);
                // ddlNatureOfBuss.Text = Convert.ToString(dt.Rows[0][""]);
                // ddlAccHolderType.SelectedValue = Convert.ToString(dt.Rows[0][""]);
                txtKYCName.Text = Convert.ToString(dt.Rows[0]["EntityName"]);
                txtDatOfInc.Text = Convert.ToString(dt.Rows[0]["DateofIncorporation"]);
                txtDtOfCom.Text = Convert.ToString(dt.Rows[0]["DateofCommencementofbusiness"]);
                txtPlaceOfInc.Text = Convert.ToString(dt.Rows[0]["PlaceofIncorporation"]);
                txtPanNo.Text = Convert.ToString(dt.Rows[0]["PAN"]);
                txtTypeIdentiNo.Text = Convert.ToString(dt.Rows[0]["TIN"]);
                ddlTINCountry.Text = Convert.ToString(dt.Rows[0]["TINIssuingCountry"]);
                ddlCertifiecopy.SelectedValue = Convert.ToString(dt.Rows[0]["IdType"]);



                //if (ddlCertifiecopy.SelectedIndex != 0)
                //{

                //}

                if (ddlCertifiecopy.SelectedIndex == 0)
                {
                    divIdProof.Visible = false;
                    txtPassNo.Text = "";
                }
                else if (ddlCertifiecopy.SelectedIndex == 1)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Certificate Of Incorporation No.";
                    txtPassNo.Visible = true;
                    txtPassNo.MaxLength = 60;
                    txtPassNo.Text = Convert.ToString(dt.Rows[0]["IdNumber"]);
                    //txtPassNo.Focus();
                    //txtPassNo.Attributes.Remove("onblur");
                    //txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");
                }
                else if (ddlCertifiecopy.SelectedIndex == 2)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Registration Certificate No.";
                    txtPassNo.Visible = true;
                    txtPassNo.Text = Convert.ToString(dt.Rows[0]["IdNumber"]);
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 60;
                    //txtPassNo.Focus();
                    //txtPassNo.Attributes.Remove("onblur");
                }
                else if (ddlCertifiecopy.SelectedIndex == 3)
                {

                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Resolution of Board / Managing Committee No.";
                    txtPassNo.Visible = true;
                    txtPassNo.Text = Convert.ToString(dt.Rows[0]["IdNumber"]);
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 25;
                    //txtPassNo.Focus();
                    //txtPassNo.Attributes.Remove("onblur");
                    //txtPassNo.Attributes.Add("onblur", "return fnValidatePAN(this)");
                }
                else if (ddlCertifiecopy.SelectedIndex == 4)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Memorandum and Article of Association No.";
                    txtPassNo.Visible = true;
                    txtPassNo.Text = Convert.ToString(dt.Rows[0]["IdNumber"]);
                    //txtPassNo.Focus();
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 25;
                    //txtPassNo.Attributes.Remove("onblur");
                }
                else if (ddlCertifiecopy.SelectedIndex == 5)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Official Valid Documents No.";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                    txtPassNo.MaxLength = 25;
                    //txtPassNo.Focus();

                    txtPassNo.Text = Convert.ToString(dt.Rows[0]["IdNumber"]);
                    //txtPassNo.Text = "";

                    //txtPassNo.Attributes.Remove("onblur");
                    //txtPassNo.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                }
                else
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Simplified Measures Account - Document Type Code";
                    txtPassNo.Visible = true;
                    txtPassNo.Text = Convert.ToString(dt.Rows[0]["IdNumber"]);

                    //txtPassNo.Focus();
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 25;
                    //txtPassNo.Attributes.Remove("onblur");
                }

                ////ddlIdentyType.SelectedValue = Convert.ToString(dt.Rows[0][""]);
                // ddlNumberOfPerson.SelectedValue = Convert.ToString(dt.Rows[0]["IdType"]);

                //proof of identity
                //ddlCertifiecopy.SelectedValue = Convert.ToString(dt.Rows[0][""]);
                //ViewState["strIdName"] = Convert.ToString(dt.Rows[0]["IdName"]);
                //ViewState["strIdNumber"] = Convert.ToString(dt.Rows[0]["IdNumber"]);

                //txtPassNo.Text = Convert.ToString(dt.Rows[0]["IdNumber"]);

                //Entity details
                //ddlAddressType.SelectedValue = Convert.ToString(dt.Rows[0]["CnctType1"]);
                //ddlProofOfAddress.SelectedValue = Convert.ToString(dt.Rows[0]["PER_ADDPROOF"]);

                //ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["PER_STATECODE"]);
                //txtPinCode.Text = Convert.ToString(dt.Rows[0]["PER_PIN"]);
                //ddlCountryCode.SelectedValue = Convert.ToString(dt.Rows[0]["PER_COUNTRY_CODE"]);
                #endregion
                //proof of address

                if (Convert.ToString(dt.Rows[0]["CnctType1"]) == "P1")
                {
                    chkPerAddress.Checked = true;
                }
                else
                {
                    chkPerAddress.Checked = false;
                }
                if (Convert.ToString(dt.Rows[0]["CnctType1"]) == "P1")
                {
                    chkCuurentAddress.Checked = true;
                }
                else
                {
                    chkCuurentAddress.Checked = false;
                }
                ddlProofOfAddress.SelectedValue = Convert.ToString(dt.Rows[0]["PER_ADDPROOF"].ToString().Trim());
                txtAddressLine1.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE1"]);
                txtAddressLine2.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE2"]);
                txtAddressLine3.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE3"]);
                txtCity.Text = Convert.ToString(dt.Rows[0]["PER_CITY"]);

                if (Convert.ToString(dt.Rows[0]["PER_COUNTRY_CODE"]) == "IN")
                {
                    dvState.Visible = true;
                    txtState.Visible = false;
                    ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["PER_STATECODE"]);
                }
                else
                {
                    dvState.Visible = false;
                    txtState.Visible = true;
                    txtState.Text = Convert.ToString(dt.Rows[0]["PER_STATECODE"]);
                }

                //ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["PER_STATECODE"]);
                txtPinCode.Text = Convert.ToString(dt.Rows[0]["PER_PIN"]);
                txtDistrictname.Text = Convert.ToString(dt.Rows[0]["PER_DISTRICT"]);
                ddlCountryCode.SelectedValue = Convert.ToString(dt.Rows[0]["PER_COUNTRY_CODE"]);
                if (Convert.ToString(dt.Rows[0]["CnctType2"]) == "M1" && (dt.Rows[0]["CUR_PIN"]).ToString() != "")
                {
                    chkLocalAddress.Checked = true;
                }
                else
                {
                    chkLocalAddress.Checked = false;
                }
                if (Convert.ToString(dt.Rows[0]["CnctType2"]) == "M1" && (dt.Rows[0]["CUR_PIN"]).ToString() != "")
                {
                    chkCuurentAddress.Checked = true;
                }
                else
                {
                    chkCuurentAddress.Checked = false;
                }
                //ddlProofOfAddress1.SelectedValue = Convert.ToString(dt.Rows[0]["CUR_ADDPROOF"].ToString().Trim());
                txtLocAddLine1.Text = Convert.ToString(dt.Rows[0]["CUR_ADDLINE1"]);
                txtLocAddLine2.Text = Convert.ToString(dt.Rows[0]["CUR_ADDLINE2"]);
                txtLocAddLine3.Text = Convert.ToString(dt.Rows[0]["CUR_ADDLINE3"]);
                txtCity1.Text = Convert.ToString(dt.Rows[0]["CUR_CITY"]);
                if (Convert.ToString(dt.Rows[0]["CUR_COUNTRY_CODE"]) == "IN")
                {
                    dvState1.Visible = true;
                    txtState1.Visible = false;
                    ddlState1.SelectedValue = Convert.ToString(dt.Rows[0]["CUR_STATECODE"]);
                }
                else
                {
                    dvState1.Visible = false;
                    txtState1.Visible = true;
                    txtState1.Text = Convert.ToString(dt.Rows[0]["CUR_STATECODE"]);
                }
                //ddlState1.SelectedValue = Convert.ToString(dt.Rows[0]["CUR_STATECODE"]);
                ddlPinCode1.Text = Convert.ToString(dt.Rows[0]["CUR_PIN"]);
                txtDistrict1.Text = Convert.ToString(dt.Rows[0]["CUR_DISTRICT"]);
                ddlCountryCode1.SelectedValue = Convert.ToString(dt.Rows[0]["CUR_COUNTRY_CODE"]);



                if (Convert.ToString(dt.Rows[0]["SameasPOIAddresFlagP1"]) == "01")
                {
                    chkSameAsPOI.Checked = true;
                }
                else
                {
                    chkSameAsPOI.Checked = false;
                }

                if (Convert.ToString(dt.Rows[0]["SameasCurrentAddresFlagM1"]) == "01")
                {
                    chkCuurentAddress.Checked = true;
                }
                else
                {
                    chkCuurentAddress.Checked = false;
                }

                txtTelOff.Text = Convert.ToString(dt.Rows[0]["std_officeTele"]);
                txtTelRes.Text = Convert.ToString(dt.Rows[0]["std_resTele"]);
                txtMobile.Text = Convert.ToString(dt.Rows[0]["mobile_countryCode"]);

                txtFax1.Text = Convert.ToString(dt.Rows[0]["std_fax"]);
                txtTelOff2.Text = Convert.ToString(dt.Rows[0]["OFF_TELE"]);
                txtTelRes2.Text = Convert.ToString(dt.Rows[0]["RES_TEL"]);

                txtFax2.Text = Convert.ToString(dt.Rows[0]["FAX"]);
                txtMobile2.Text = Convert.ToString(dt.Rows[0]["MOBILE"]);
                txtemail.Text = Convert.ToString(dt.Rows[0]["EMAILID"]);



                txtRemarks.Text = Convert.ToString(dt.Rows[0]["REMARK"]);
                txtPlace.Text = Convert.ToString(dt.Rows[0]["PLACE"]);
                txtDate.Text = Convert.ToString(dt.Rows[0]["APP_DATE"]);



                //txtEmpName.Text = Convert.ToString(dt.Rows[0]["kycEmpName"]);
                //txtEmpCode.Text = Convert.ToString(dt.Rows[0]["kycEmpCode"]);
                //txtEmpDesignation.Text = Convert.ToString(dt.Rows[0]["kycEmpDesi"]);
                //txtEmpBranch.Text = Convert.ToString(dt.Rows[0]["kycEmpBranch"]);
                //txtInsName.Text = Convert.ToString(dt.Rows[0]["kycInstName"]);

                //txtInsCode.Text = Convert.ToString(dt.Rows[0]["kycInstCode"]);
                //txtDateKYCver.Text = Convert.ToString(dt.Rows[0]["kycVerDate"]);


                //ddlIsoCountryCode2.SelectedValue = Convert.ToString(dt.Rows[0]["ISO_RFT_COUNTRYCODE"]);

                //txtIDResTax.Text = Convert.ToString(dt.Rows[0]["TAX_IDNUMBER"]);
                //txtDOBRes.Text = Convert.ToString(dt.Rows[0]["BIRTH_PLACE"]);
                //ddlIsoCountry.SelectedValue = Convert.ToString(dt.Rows[0]["ISO_BIRTHPLACE_CODE"]);


                txtPlace.Text = Convert.ToString(dt.Rows[0]["PLACE"]);
                txtDate.Text = Convert.ToString(dt.Rows[0]["APP_DATE"]);

                txtEmpName.Text = Convert.ToString(dt.Rows[0]["kycEmpName"]);
                txtEmpCode.Text = Convert.ToString(dt.Rows[0]["kycEmpCode"]);
                txtEmpDesignation.Text = Convert.ToString(dt.Rows[0]["kycEmpDesi"]);
                txtEmpBranch.Text = Convert.ToString(dt.Rows[0]["kycEmpBranch"]);
                txtInsName.Text = Convert.ToString(dt.Rows[0]["kycInstName"]);

                txtInsCode.Text = Convert.ToString(dt.Rows[0]["kycInstCode"]);
                txtDateKYCver.Text = Convert.ToString(dt.Rows[0]["kycVerDate"]);
                ddlDocReceived.SelectedValue = Convert.ToString(dt.Rows[0]["kycCertDoc"]);
                //FillStates();
            }
            catch (Exception ex)
            {
                //if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                //{
                //    Response.Redirect("~/ErrorSession.aspx");
                //}
                //else
                //{
                //    //objErr = new ErrorLog();
                //    //objErr.LogErr(AppID, "CkycReg.aspx.cs", "FillRequiredDataForPartialSave", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                //}
            }
        }
        #endregion

        #region METHOD "GetRelatedPersonPartialDataForCKYC"
        protected void GetRelatedPersonPartialDataForCKYC()
        {
            try
            {
                objDAL = new DataAccessLayer("STAGINGConnectionString");
                dt = new DataTable();
                htParam.Clear();
                htParam.Add("@PSTempRefNo", Request.QueryString["refno"].ToString());
                htParam.Add("@ActionFlag", Request.QueryString["Status"].ToString());
                htParam.Add("@UserType", "");
                htParam.Add("@PSTempRelRefNo", "");

                dt = objDAL.GetDataTable("Prc_GetRelatedPersonPartialDataForCKYC", htParam);

                if (dt.Rows.Count > 0)
                {
                    gvMemDtls.DataSource = dt;
                    gvMemDtls.DataBind();
                    Session["dsRel"] = dt;
                    ViewState["DT"] = dt;
                    gvMemDtls.Visible = true;
                    divchkDelRel.Visible = false;   //Added By Akash
                    chkAddRel.Checked = true;
                    chkAddRel.Enabled = false;

                    if (Request.QueryString["Status"].ToString() == "LMod")
                    {
                        gvMemDtls.Columns[1].HeaderText = "Relative Temporary Reference No";
                    }
                    else if (Request.QueryString["Status"].ToString() == "PMod")
                    {
                        gvMemDtls.Columns[1].HeaderText = "Relative Reference No";
                    }
                }
                else
                {
                    lblRelRecordShow.Visible = true;
                    lblRelRecordShow.ForeColor = System.Drawing.Color.Red;
                    divchkDelRel.Visible = false;
                    chkAddRel.Checked = false;
                    chkAddRel.Enabled = true;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "GetRelatedPersonPartialDataForCKYC", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region METHOD "GetCtrlPrsnPartialDataForCKYC"
        protected void GetCtrlPrsnPartialDataForCKYC()
        {
            try
            {
                objDAL = new DataAccessLayer("STAGINGConnectionString");
                dt = new DataTable();
                htParam.Clear();
                htParam.Add("@PSTempRefNo", Request.QueryString["refno"].ToString());
                htParam.Add("@ActionFlag", Request.QueryString["Status"].ToString());
                htParam.Add("@UserType", "");
                htParam.Add("@PSTempRelRefNo", "");

                dt = objDAL.GetDataTable("Prc_GetCtrlPrsnPartialDataForCKYC", htParam);

                if (dt.Rows.Count > 0)
                {
                    //gvCtrlPrson.DataSource = dt;
                    //gvCtrlPrson.DataBind();
                    Session["dsCtrl"] = dt;
                    ViewState["DTCtrl"] = dt;
                    //gvCtrlPrson.Visible = true;
                    //div8.Visible = false;   //Added By Akash
                    //chkAddCtrl.Checked = true;
                    //chkAddCtrl.Enabled = false;

                    if (Request.QueryString["Status"].ToString() == "LMod")
                    {
                        //gvCtrlPrson.Columns[1].HeaderText = "Controlling Temporary Reference No";
                    }
                    else if (Request.QueryString["Status"].ToString() == "PMod")
                    {
                        //gvCtrlPrson.Columns[1].HeaderText = "Controlling Reference No";
                    }
                }
                else
                {
                    //Label2.Visible = true;
                    //Label2.ForeColor = System.Drawing.Color.Red;
                    //div8.Visible = false;   //Added By Akash
                    //chkAddCtrl.Checked = false;
                    //chkAddCtrl.Enabled = true;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "GetRelatedPersonPartialDataForCKYC", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region updflag
        //protected void updflag()
        //{
        //    try
        //    {
        //        if ((Request.QueryString["Status"].ToString() == "Reg") || (Request.QueryString["Status"].ToString() == "Mod") || (Request.QueryString["Status"].ToString() == "PMod") || (Request.QueryString["Status"].ToString() == "LMod"))
        //        {
        //            ChkUpdName.Visible = false;
        //            ChkUpdPersonal.Visible = false;
        //            ChkUpdID.Visible = false;
        //            ChkUpdAddr.Visible = false;
        //            ChkUpdContact.Visible = false;
        //            ChkUpdRelated.Visible = false;
        //            ChkUpdRemark.Visible = false;
        //            ChkUpdKYCVrfy.Visible = false;
        //        }
        //        else if (Request.QueryString["Status"].ToString() == "KMod")
        //        {
        //            ChkUpdName.Visible = true;
        //            ChkUpdPersonal.Visible = true;
        //            ChkUpdID.Visible = true;
        //            ChkUpdAddr.Visible = true;
        //            ChkUpdContact.Visible = true;
        //            ChkUpdRelated.Visible = true;
        //            ChkUpdRemark.Visible = true;
        //            ChkUpdKYCVrfy.Visible = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
        //        {
        //            Response.Redirect("~/ErrorSession.aspx");
        //        }
        //        else
        //        {
        //            objErr = new ErrorLog();
        //            objErr.LogErr(AppID, "CkycReg.aspx.cs", "updflag", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
        //        }
        //    }
        //}
        #endregion

        protected void chkLocalAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLocalAddress.Checked == true)
            {
                chkCuurentAddress.Enabled = true;
                //ddlProofOfAddress1.Enabled = true;
                txtLocAddLine1.Enabled = true;
                txtLocAddLine2.Enabled = true;
                txtLocAddLine3.Enabled = true;
                txtCity1.Enabled = true;
                ddlState1.Enabled = true;
                ddlPinCode1.Enabled = true;
                txtDistrict1.Enabled = true;
                ddlCountryCode1.Enabled = true;
            }
            else
            {
                chkCuurentAddress.Enabled = false;

                //ddlProofOfAddress1.Enabled = false;
                txtLocAddLine1.Enabled = false;
                txtLocAddLine2.Enabled = false;
                txtLocAddLine3.Enabled = false;
                txtCity1.Enabled = false;
                ddlState1.Enabled = false;
                ddlPinCode1.Enabled = false;
                txtDistrict1.Enabled = false;
                ddlCountryCode1.Enabled = false;
            }
        }

        protected void ddlCountryCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountryCode.SelectedValue == "IN")
            {

            }
            else if (ddlCountryCode.SelectedValue != "IN")
            {
                dvState.Visible = false;
                ddlState.SelectedIndex = 0;
                ddlState.Visible = false;
                txtState.Visible = true;
                txtPinCode.Text = "";
                txtPinCode.Attributes.Remove("readonly");
                txtDistrictname.Text = "";
                txtDistrictname.Enabled = true;
                txtDistrictname.Attributes.Remove("readonly");
            }
        }

        protected void ddlCountryCode1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountryCode1.SelectedValue == "IN")
            {

            }
            else if (ddlCountryCode1.SelectedValue != "IN")
            {
                dvState1.Visible = false;
                ddlState1.SelectedIndex = 0;
                ddlState1.Visible = false;
                txtState1.Visible = true;
                ddlPinCode1.Text = "";
                ddlPinCode1.Attributes.Remove("readonly");
                txtDistrict1.Text = "";
                txtDistrict1.Enabled = true;
                txtDistrict1.Attributes.Remove("readonly");
            }
        }


        protected void ddlNatureOfBuss_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtConstitutionTypeothers.Text = "";
            //txtConstitutionTypeothers.Enabled = (ddlNatureOfBuss.SelectedValue == "R");

            if (ddlNatureOfBuss.SelectedValue == "R")
            {
                txtConstitutionTypeothers.Enabled = true;
            }
            else
            {
                txtConstitutionTypeothers.Enabled = false;
            }
        }


        protected void chkPanForm_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPanForm.Checked == true)
            {
                txtPanNo.Text = "";
                txtPanNo.Enabled = false;
            }
            else if (chkPanForm.Checked == false)
            {
                txtPanNo.Text = "";
                txtPanNo.Enabled = true;
            }
        }
    }
}