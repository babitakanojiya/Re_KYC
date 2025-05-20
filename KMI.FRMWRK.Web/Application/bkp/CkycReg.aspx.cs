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
    public partial class CkycReg : System.Web.UI.Page
    {
        #region Declare Veriables
        private MultilingualManager olng;
        Hashtable htParam = new Hashtable();
        ErrorLog objErr;

        private string Message = string.Empty;
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
        DataAccessLayer objDAL;
        #endregion

        #region Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Mode"] != null)
                {
                    if (Request.QueryString["Mode"].ToString() == "Mail")
                    {
                        Session["CarrierCode"] = '2';
                        olng = new MultilingualManager("DefaultConn", "CKYCReg.aspx", "01");
                        //strUserLang = HttpContext.Current.Session["UserLangNum"].ToString();
                        strUserId = Request.QueryString["UserID"].ToString();
                        //GetFIMissingFields();
                        Session["UserID"] = strUserId;
                        Session["UserGroupCode"] = "";
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
                    olng = new MultilingualManager("DefaultConn", "CKYCReg.aspx", Session["UserLangNum"].ToString());
                    strUserId = HttpContext.Current.Session["UserID"].ToString().Trim();
                }

                txtInsName.Enabled = false;
                txtInsCode.Enabled = false;
                if (!IsPostBack)
                {
                    InitializeControls();
                    FillDocumentReceived();
                    DataTable dtEntDetails = new DataAccessLayer().GetDataTable("Prc_GetEntitySetup", "DefaultConn");
                    if (dtEntDetails.Rows.Count > 0)
                    {
                        txtInsName.Text = Convert.ToString(dtEntDetails.Rows[0]["EntityName"]);
                        txtInsCode.Text = Convert.ToString(dtEntDetails.Rows[0]["InstitutionCode"]);
                    }
                    if (Request.QueryString["Status"].ToString() == "Reg")
                    {
                        BindAttestation();
                        divIdProof.Visible = false;
                        divAddProof.Visible = false;


                        PopulateAddressProofType();

                        subPopulateGender();
                        subPopulateTitle();
                        PopulateProofIdentiy();
                        PopulatePinCode();
                        Fillcountrycd1();

                        cbNew.Visible = true;
                        cbNew.Checked = true;
                        cbUpdate.Visible = false;
                        chkNormal.Checked = true;
                        btnKYCUpdate.Visible = false;
                        //chkCertifyCopy.Checked = true;
                        lnkViewRel.Enabled = false;
                        updflag();
                        txtKYCNumber.Visible = false;
                        lblKYCNumber.Visible = false;
                        btnPartialSave.Visible = true;
                        //dsRel = null;
                        Session["dsRel"] = null;
                    }
                    else if (Request.QueryString["Status"].ToString() == "Mod")
                    {
                        divchkDelRel.Visible = true;
                        divIdProof.Visible = false;


                        PopulateAddressProofType();
                        subPopulateGender();
                        subPopulateTitle();
                        PopulateProofIdentiy();
                        PopulatePinCode();
                        Fillcountrycd1();

                        FillRequiredDataForCKYC();
                        txtMobile.Enabled = true;
                        GetRelatedPersonDataForCKYC();
                        btnSave.Visible = false;
                        btnUpdate.Visible = true;
                        btnPartialSave.Visible = true;
                        btnPartialUpdate.Visible = false;
                        btnKYCUpdate.Visible = false;
                        cbNew.Visible = true;
                        cbNew.Checked = true;
                        cbUpdate.Visible = false;

                        updflag();
                        txtKYCNumber.Visible = false;
                        lblKYCNumber.Visible = false;

                        chkAppDeclare1.Checked = true;
                        chkAppDeclare2.Checked = true;
                        btnPartialSave.Visible = false;
                    }
                    else if (Request.QueryString["Status"].ToString() == "PMod")
                    {
                        divchkDelRel.Visible = true;
                        divIdProof.Visible = false;


                        PopulateAddressProofType();
                        subPopulateGender();
                        subPopulateTitle();
                        PopulateProofIdentiy();
                        PopulatePinCode();
                        Fillcountrycd1();
                        //GetFIMissingFields();

                        FillRequiredDataForPartialSave();
                        GetRelatedPersonPartialDataForCKYC();
                        btnSave.Visible = true;
                        btnUpdate.Visible = false; ;
                        btnPartialSave.Visible = true;
                        btnKYCUpdate.Visible = false;
                        cbNew.Visible = true;
                        cbNew.Checked = true;
                        cbUpdate.Visible = false;
                        updflag();
                        txtKYCNumber.Visible = false;
                        lblKYCNumber.Visible = false;
                        chkAppDeclare1.Checked = true;
                        chkAppDeclare2.Checked = true;
                        btnPartialSave.Visible = false;
                        btnPartialUpdate.Visible = true;
                    }
                    else if (Request.QueryString["Status"].ToString() == "view")
                    {
                        divIdProof.Visible = false;

                        PopulateAddressProofType();

                        subPopulateGender();
                        subPopulateTitle();
                        PopulateProofIdentiy();
                        PopulatePinCode();
                        Fillcountrycd1();
                        FillRequiredDataForCKYC();
                        GetRelatedPersonDataForCKYC();
                        btnSave.Visible = false;
                        btnUpdate.Visible = false;
                        btnPartialSave.Visible = false;
                        btnKYCUpdate.Visible = false;
                        updflag();
                        txtKYCNumber.Visible = false;
                        lblKYCNumber.Visible = false;
                    }
                    else if (Request.QueryString["Status"].ToString() == "KMod")
                    {
                        divchkDelRel.Visible = true;
                        divIdProof.Visible = false;

                        PopulateAddressProofType();
                        subPopulateGender();
                        subPopulateTitle();
                        PopulateProofIdentiy();
                        PopulatePinCode();
                        Fillcountrycd1();
                        FillRequiredDataForCKYC();
                        disablecntrl();
                        GetRelatedPersonDataForCKYC();
                        getupdateflag();
                        btnSave.Visible = false;
                        btnUpdate.Visible = true;
                        cbUpdate.Checked = true;
                        cbNew.Visible = false;
                        cbNew.Checked = false;
                        cbUpdate.Visible = true;
                        btnSave.Visible = false;
                        btnUpdate.Visible = false;
                        btnKYCUpdate.Visible = true;
                        btnPartialSave.Visible = false;
                        updflag();
                        txtKYCNumber.Visible = true;
                        lblKYCNumber.Visible = true;
                        chkAppDeclare1.Checked = true;
                        chkAppDeclare2.Checked = true;
                    }

                    FillDistrictState(ddlPinCode, ddlDistrict, ddlState);
                    FillDistrictState(ddlPinCode1, ddlDistrict1, ddlState1);
                    if (ddlProofIdentity.SelectedIndex == 0)
                    {
                        divIdProof.Visible = false;
                    }
                    else if (ddlProofIdentity.SelectedIndex == 1)
                    {
                        divIdProof.Visible = true;
                        lblPassportNo.Text = "Passport Number";
                        llPassExpDate.Text = "Passport Expiry Date";
                        llPassExpDate.Visible = true;
                        txtPassExpDate.Visible = true;
                        divPass.Visible = true;
                        txtPassOthr.Visible = false;
                        txtPassNo.Visible = true;
                        txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        txtPassExpDate.Text = ViewState["strIdExpDate"].ToString();
                        //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNo.MaxLength = 15;
                        txtPassNo.Attributes.Remove("onblur");
                        txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");
                    }
                    else if (ddlProofIdentity.SelectedIndex == 2)
                    {
                        divIdProof.Visible = true;
                        lblPassportNo.Text = "Voter ID Card";
                        llPassExpDate.Visible = false;
                        txtPassExpDate.Visible = false;
                        txtPassOthr.Visible = false;
                        divPass.Visible = false;
                        txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        txtPassNo.Visible = true;
                        //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNo.MaxLength = 15;
                        txtPassNo.Attributes.Remove("onblur");
                    }
                    else if (ddlProofIdentity.SelectedIndex == 3)
                    {
                        divIdProof.Visible = true;
                        lblPassportNo.Text = "PAN Card No";
                        llPassExpDate.Visible = false;
                        txtPassExpDate.Visible = false;
                        txtPassOthr.Visible = false;
                        divPass.Visible = false;
                        txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        txtPassNo.Visible = true;
                        //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        txtPassNo.MaxLength = 10;
                        txtPassNo.Attributes.Remove("onblur");
                        txtPassNo.Attributes.Add("onblur", "return fnValidatePAN(this)");
                    }
                    else if (ddlProofIdentity.SelectedIndex == 4)
                    {
                        divIdProof.Visible = true;
                        lblPassportNo.Text = "Driving Licence No";
                        llPassExpDate.Text = "Driving Licence Expiry Date";
                        llPassExpDate.Visible = true;
                        txtPassExpDate.Visible = true;
                        txtPassOthr.Visible = false;
                        divPass.Visible = true;
                        txtPassNo.Visible = true;
                        txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        txtPassExpDate.Text = ViewState["strIdExpDate"].ToString();
                        //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNo.MaxLength = 20;
                        txtPassNo.Attributes.Remove("onblur");
                    }
                    else if (ddlProofIdentity.SelectedIndex == 5)
                    {
                        divIdProof.Visible = true;
                        lblPassportNo.Text = "UID(Aadhaar)";
                        llPassExpDate.Visible = false;
                        txtPassExpDate.Visible = false;
                        txtPassOthr.Visible = false;
                        divPass.Visible = false;
                        txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        txtPassNo.Visible = true;
                        //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                        txtPassNo.MaxLength = 12;
                        txtPassNo.Attributes.Remove("onblur");
                        txtPassNo.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                    }
                    else if (ddlProofIdentity.SelectedIndex == 6)
                    {
                        divIdProof.Visible = true;
                        lblPassportNo.Text = "NREGA Job Card";
                        llPassExpDate.Visible = false;
                        txtPassOthr.Visible = false;
                        divPass.Visible = false;
                        txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        txtPassNo.Visible = true;
                        //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNo.MaxLength = 20;
                        txtPassNo.Attributes.Remove("onblur");

                    }
                    else if (ddlProofIdentity.SelectedIndex == 7)
                    {
                        divIdProof.Visible = true;
                        lblPassportNo.Text = "Others";
                        llPassExpDate.Text = "Identification Number";
                        txtPassExpDate.Visible = true;
                        llPassExpDate.Visible = true;
                        divPass.Visible = true;
                        txtPassNo.Text = ViewState["strIdName"].ToString();
                        txtPassOthr.Text = ViewState["strIdNumber"].ToString();
                        txtPassExpDate.Visible = false;
                        txtPassNo.Visible = true;
                        //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNo.MaxLength = 50;
                        txtPassNo.Attributes.Remove("onblur");
                    }
                    else if (ddlProofIdentity.SelectedIndex == 8)
                    {
                        divIdProof.Visible = true;
                        lblPassportNo.Text = "Simplified Measures Account - Document Type Code";
                        llPassExpDate.Text = "Identification Number";
                        txtPassExpDate.Visible = true;
                        llPassExpDate.Visible = true;
                        divPass.Visible = true;
                        txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        txtPassOthr.Text = ViewState["strSamDocNum"].ToString();
                        txtPassExpDate.Visible = false;
                        txtPassNo.Visible = true;
                        //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNo.MaxLength = 2;
                        txtPassNo.Attributes.Remove("onblur");
                    }
                    else
                    {
                        divIdProof.Visible = true;
                        lblPassportNo.Text = "Simplified Measures Account - Document Type Code";
                        llPassExpDate.Text = "Identification Number";
                        txtPassExpDate.Visible = true;
                        llPassExpDate.Visible = true;
                        divPass.Visible = true;
                        txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        txtPassOthr.Text = ViewState["strIdExpDate"].ToString();
                        txtPassExpDate.Visible = false;
                        txtPassNo.Visible = true;
                        //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNo.MaxLength = 2;
                        txtPassNo.Attributes.Remove("onblur");
                    }

                    if (ddlProofOfAddress.SelectedIndex == 0)
                    {
                        divAddProof.Visible = false;
                    }
                    else if (ddlProofOfAddress.SelectedIndex == 1)
                    {
                        divAddProof.Visible = true;
                        lblPassportNoAdd.Text = "Passport Number";
                        llPassExpDateAdd.Text = "Passport Expiry Date";
                        llPassExpDateAdd.Visible = true;
                        txtPassExpDateAdd.Visible = true;
                        divPassAdd.Visible = true;
                        txtPassOthrAdd.Visible = false;
                        txtPassNoAdd.Visible = true;
                        //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNo.MaxLength = 15;
                        txtPassNo.Attributes.Remove("onblur");
                        txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");
                    }
                    else if (ddlProofOfAddress.SelectedIndex == 2)
                    {
                        divAddProof.Visible = true;
                        lblPassportNoAdd.Text = "Driving Licence";
                        llPassExpDateAdd.Text = "Driving Licence Expiry Date";
                        llPassExpDateAdd.Visible = true;
                        txtPassExpDateAdd.Visible = true;
                        txtPassOthrAdd.Visible = false;
                        divPassAdd.Visible = true;
                        txtPassNoAdd.Visible = true;
                        //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNoAdd.MaxLength = 15;
                        txtPassNoAdd.Attributes.Remove("onblur");
                    }
                    else if (ddlProofOfAddress.SelectedIndex == 3)
                    {
                        divAddProof.Visible = true;
                        lblPassportNoAdd.Text = "UID(Aadhaar)";
                        llPassExpDateAdd.Visible = false;
                        txtPassExpDateAdd.Visible = false;
                        txtPassOthrAdd.Visible = false;
                        divPassAdd.Visible = false;
                        txtPassNoAdd.Visible = true;
                        //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                        txtPassNoAdd.MaxLength = 12;
                        txtPassNoAdd.Attributes.Remove("onblur");
                        txtPassNoAdd.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                    }
                    else if (ddlProofOfAddress.SelectedIndex == 4)
                    {
                        divAddProof.Visible = true;
                        lblPassportNoAdd.Text = "Voter ID Card";
                        llPassExpDateAdd.Visible = false;
                        txtPassExpDateAdd.Visible = false;
                        txtPassOthrAdd.Visible = false;
                        divPassAdd.Visible = false;
                        txtPassNoAdd.Visible = true;
                        //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNo.MaxLength = 15;
                        txtPassNoAdd.Attributes.Remove("onblur");
                    }
                    else if (ddlProofOfAddress.SelectedIndex == 5)
                    {
                        divAddProof.Visible = true;
                        lblPassportNoAdd.Text = "NREGA Job Card";
                        llPassExpDateAdd.Visible = false;
                        txtPassNoAdd.Visible = true;
                        txtPassExpDateAdd.Visible = false;
                        txtPassOthrAdd.Visible = false;
                        divPassAdd.Visible = false;
                        //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNoAdd.MaxLength = 15;
                        txtPassNoAdd.Attributes.Remove("onblur");
                    }
                    else
                    {
                        divAddProof.Visible = true;
                        lblPassportNoAdd.Text = "Document Name";
                        llPassExpDateAdd.Text = "Identification Number";
                        txtPassExpDateAdd.Visible = false;
                        llPassExpDateAdd.Visible = true;
                        divPassAdd.Visible = true;
                        llPassExpDateAdd.Visible = true; txtPassExpDateAdd.Visible = false;
                        txtPassOthrAdd.Visible = true;
                        //txtPassNoAdd.Visible = true; FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNoAdd.MaxLength = 15;
                        txtPassNoAdd.Attributes.Remove("onblur");
                    }

                    //btnUpdate.Attributes.Add("onClick", "javascript: return funCKYC()");
                    //btnSave.Attributes.Add("onClick", "javascript: return funCKYC();");
                }

                rbtFS.Attributes.Add("onchange", "CheckFatherSpouce('rdoFather')");
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
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
                if (chkCuurentAddress.Checked == true)
                {
                    FillDistrictState(ddlPinCode, ddlDistrict1, ddlState1);
                    chkLocalAddress.Checked = true;
                    txtLocAddLine1.Text = txtAddressLine1.Text;
                    txtLocAddLine1.Enabled = false;
                    txtLocAddLine2.Text = txtAddressLine2.Text;
                    txtLocAddLine2.Enabled = false;
                    txtLocAddLine3.Text = txtAddressLine3.Text;
                    txtLocAddLine3.Enabled = false;
                    txtCity1.Text = txtCity.Text;
                    txtCity1.Enabled = false;
                    ddlPinCode1.Text = ddlPinCode.Text;
                    ddlPinCode1.Enabled = false;
                    ddlCountryCode1.SelectedValue = ddlCountryCode.SelectedValue;
                    ddlCountryCode1.Enabled = false;
                    ddlDistrict1.SelectedValue = ddlDistrict.SelectedValue;
                    ddlDistrict1.Enabled = false;
                    ddlState1.SelectedValue = ddlState.SelectedValue;
                    ddlState1.Enabled = false;
                }
                else
                {
                    chkLocalAddress.Checked = false;
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
                    ddlPinCode1.Enabled = true;
                    ddlPinCode1.SelectedIndex = 0;
                    ddlDistrict1.Items.Clear();
                    ddlState1.Items.Clear();
                    ddlDistrict1.Items.Insert(0, new ListItem("Select", ""));
                    ddlState1.Items.Insert(0, new ListItem("Select", ""));
                    ddlDistrict1.SelectedIndex = 0;
                    ddlState1.SelectedIndex = 0;
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


        #region DROPDOWN 'ddlProofIdentity' SELECTEDINDEXCHANGED EVENT
        protected void ddlProofIdentity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtPassOthr.Visible = false;
                txtPassNo.Visible = false;
                txtPassNo.Text = string.Empty;
                txtPassExpDate.Text = string.Empty;
                if (ddlProofIdentity.SelectedIndex == 0)
                {
                    divIdProof.Visible = false;

                }
                else if (ddlProofIdentity.SelectedIndex == 1)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Passport Number";
                    llPassExpDate.Text = "Passport Expiry Date";
                    llPassExpDate.Visible = true;
                    txtPassExpDate.Visible = true;
                    divPass.Visible = true;
                    txtPassOthr.Visible = false;
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 15;
                    txtPassNo.Attributes.Remove("onblur");
                    txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");
                }
                else if (ddlProofIdentity.SelectedIndex == 2)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Voter ID Card";
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    txtPassOthr.Visible = false;
                    divPass.Visible = false;
                    txtPassNo.Visible = true;

                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 20;
                    txtPassNo.Attributes.Remove("onblur");
                    txtPassNo.Attributes.Add("onblur", "return ValidationVoterId(this)");
                }
                else if (ddlProofIdentity.SelectedIndex == 3)
                {

                    divIdProof.Visible = true;
                    lblPassportNo.Text = "PAN Card No";
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    txtPassOthr.Visible = false;
                    divPass.Visible = false;
                    txtPassNo.Visible = true;

                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 10;
                    txtPassNo.Attributes.Remove("onblur");
                    txtPassNo.Attributes.Add("onblur", "return fnValidatePAN(this)");
                }
                else if (ddlProofIdentity.SelectedIndex == 4)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Driving Licence No";
                    llPassExpDate.Text = "Driving Licence Expiry Date";
                    llPassExpDate.Visible = true;
                    txtPassExpDate.Visible = true;
                    txtPassOthr.Visible = false;
                    divPass.Visible = true;
                    txtPassNo.Visible = true;

                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 20;
                    txtPassNo.Attributes.Remove("onblur");
                    txtPassNo.Attributes.Add("onblur", "return ValidationDriving(this)");
                }
                else if (ddlProofIdentity.SelectedIndex == 5)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "UID(Aadhaar)";
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    txtPassOthr.Visible = false;
                    divPass.Visible = false;
                    txtPassNo.Visible = true;

                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                    txtPassNo.MaxLength = 12;
                    txtPassNo.Text = "";
                    txtPassNo.Attributes.Remove("onblur");
                    txtPassNo.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                }
                else if (ddlProofIdentity.SelectedIndex == 6)
                {

                    divIdProof.Visible = true;
                    lblPassportNo.Text = "NREGA Job Card";
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    txtPassNo.Visible = true;
                    txtPassOthr.Visible = false;
                    divPass.Visible = false;

                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 50;
                    txtPassNo.Attributes.Remove("onblur");
                }
                else if (ddlProofIdentity.SelectedIndex == 7)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Document Name";
                    llPassExpDate.Text = "Identification Number";
                    txtPassExpDate.Visible = true;
                    llPassExpDate.Visible = true;
                    divPass.Visible = true;
                    llPassExpDate.Visible = true;
                    txtPassExpDate.Visible = false;
                    txtPassOthr.Visible = true;
                    txtPassNo.Visible = true;

                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 20;
                    txtPassNo.Attributes.Remove("onblur");

                }
                else if (ddlProofIdentity.SelectedIndex == 8)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Simplified Measures Account - Document Type Code";
                    llPassExpDate.Text = "Identification Number";
                    txtPassExpDate.Visible = true;
                    llPassExpDate.Visible = true;
                    divPass.Visible = true;
                    txtPassExpDate.Visible = false;
                    txtPassOthr.Visible = true;
                    txtPassNo.Visible = true;

                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 2;
                    txtPassNo.Attributes.Remove("onblur");
                }
                else
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Simplified Measures Account - Document Type Code";
                    llPassExpDate.Text = "Identification Number";
                    txtPassExpDate.Visible = true;
                    llPassExpDate.Visible = true;
                    divPass.Visible = true;
                    txtPassExpDate.Visible = false;
                    txtPassOthr.Visible = true;
                    txtPassNo.Visible = true;

                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 2;
                    txtPassNo.Attributes.Remove("onblur");
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ddlProofIdentity_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region DROPDOWN 'ddlProofOfAddress' SELECTEDINDEXCHANGED EVENT
        protected void ddlProofOfAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtPassOthrAdd.Visible = false;
                txtPassNoAdd.Visible = false;
                txtPassNoAdd.Text = string.Empty;
                txtPassExpDateAdd.Text = string.Empty;
                if (ddlProofOfAddress.SelectedIndex == 0)
                {
                    divAddProof.Visible = false;
                }
                else if (ddlProofOfAddress.SelectedIndex == 1)
                {
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Passport Number";
                    llPassExpDateAdd.Text = "Passport Expiry Date";
                    llPassExpDateAdd.Visible = true;
                    txtPassExpDateAdd.Visible = true;
                    divPassAdd.Visible = true;
                    txtPassOthrAdd.Visible = false;
                    txtPassNoAdd.Visible = true;
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd.MaxLength = 15;
                    txtPassNoAdd.Attributes.Remove("onblur");
                    txtPassNoAdd.Attributes.Add("onblur", "return ValidationPassport(this)");
                }
                else if (ddlProofOfAddress.SelectedIndex == 2)
                {
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Driving Licence";
                    llPassExpDateAdd.Text = "Driving Licence Expiry Date";
                    llPassExpDateAdd.Visible = true;
                    txtPassExpDateAdd.Visible = true;
                    txtPassOthrAdd.Visible = false;
                    divPassAdd.Visible = true;
                    txtPassNoAdd.Visible = true;
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd.MaxLength = 15;
                    txtPassNoAdd.Attributes.Remove("onblur");
                    txtPassNoAdd.Attributes.Add("onblur", "return ValidationDriving(this)");
                }
                else if (ddlProofOfAddress.SelectedIndex == 3)
                {
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "UID(Aadhaar)";
                    llPassExpDateAdd.Visible = false;
                    txtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = false;
                    divPassAdd.Visible = false;
                    txtPassNoAdd.Visible = true;
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                    txtPassNoAdd.MaxLength = 12;
                    txtPassNoAdd.Text = "";
                    txtPassNoAdd.Attributes.Remove("onblur");
                    txtPassNoAdd.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                }
                else if (ddlProofOfAddress.SelectedIndex == 4)
                {
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Voter ID Card";
                    llPassExpDateAdd.Visible = false;
                    txtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = false;
                    divPassAdd.Visible = false;
                    txtPassNoAdd.Visible = true;
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd.MaxLength = 15;
                    txtPassNoAdd.Attributes.Remove("onblur");
                    txtPassNoAdd.Attributes.Add("onblur", "return ValidationVoterId(this)");
                }


                else if (ddlProofOfAddress.SelectedIndex == 5)
                {
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "NREGA Job Card";
                    llPassExpDateAdd.Visible = false;
                    txtPassNoAdd.Visible = true;
                    txtPassOthrAdd.Visible = false;
                    divPassAdd.Visible = false;
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd.MaxLength = 15;
                    txtPassNoAdd.Attributes.Remove("onblur");
                }
                else
                {
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Document Name";
                    llPassExpDateAdd.Text = "Identification Number";
                    txtPassExpDateAdd.Visible = true;
                    llPassExpDateAdd.Visible = true;
                    divPassAdd.Visible = true;
                    llPassExpDateAdd.Visible = true; txtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = true;
                    txtPassNoAdd.Visible = true;
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
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

        #region METHOD "FillRequiredDataForCndPersonal"
        protected void FillRequiredDataForCKYC()
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                htParam.Clear();
                htParam.Add("@RegRefNo", Request.QueryString["refno"].ToString());
                dt.Load(objDAL.ExecuteReader("getSearchData_Web", htParam));

                if (Convert.ToString(dt.Rows[0]["AccType"]) == "01")
                {
                    chkNormal.Checked = true;
                }
                else if (Convert.ToString(dt.Rows[0]["AccType"]) == "02")
                {
                    chkSimplified.Checked = true;
                }
                else if (Convert.ToString(dt.Rows[0]["AccType"]) == "03")
                {
                    Chksmall.Checked = true;
                }
                txtKYCNumber.Text = Convert.ToString(dt.Rows[0]["KYC_NO"]);
                txtRefNumber.Text = Convert.ToString(dt.Rows[0]["RegRefNo"]);
                cboTitle.SelectedValue = Convert.ToString(dt.Rows[0]["Prefix"]);
                txtGivenName.Text = Convert.ToString(dt.Rows[0]["FNAME"]);
                txtMiddleName.Text = Convert.ToString(dt.Rows[0]["MNAME"]);
                txtLastName.Text = Convert.ToString(dt.Rows[0]["LNAME"]);
                cboTitle1.SelectedValue = Convert.ToString(dt.Rows[0]["MAID_PREFIX"]);
                txtGivenName1.Text = Convert.ToString(dt.Rows[0]["MAID_FNAME"]);
                txtMiddleName1.Text = Convert.ToString(dt.Rows[0]["MAID_MNAME"]);
                txtLastName1.Text = Convert.ToString(dt.Rows[0]["MAID_LNAME"]);

                if (Convert.ToString(dt.Rows[0]["FS_FLAG"]) == "01")
                {
                    rbtFS.SelectedValue = "F";
                }
                else
                {
                    rbtFS.SelectedValue = "S";
                }
                cboTitle2.SelectedValue = Convert.ToString(dt.Rows[0]["FATHER_PREFIX"]);
                txtGivenName2.Text = Convert.ToString(dt.Rows[0]["FATHER_FNAME"]);
                txtMiddleName2.Text = Convert.ToString(dt.Rows[0]["FATHER_MNAME"]);
                txtLastName2.Text = Convert.ToString(dt.Rows[0]["FATHER_LNAME"]);
                cboTitle3.SelectedValue = Convert.ToString(dt.Rows[0]["MOTHER_PREFIX"]);
                txtGivenName3.Text = Convert.ToString(dt.Rows[0]["MOTHER_FNAME"]);
                txtMiddleName3.Text = Convert.ToString(dt.Rows[0]["MOTHER_MNAME"]);
                txtLastName3.Text = Convert.ToString(dt.Rows[0]["MOTHER_LNAME"]);
                txtDOB.Text = Convert.ToString(dt.Rows[0]["DOB"]);
                cboGender.SelectedValue = Convert.ToString(dt.Rows[0]["GENDER"]);

                if (Convert.ToString(dt.Rows[0]["JurisdictionFlag"]) == "01")
                {
                    chkTick.Checked = true;
                }
                else
                {
                    chkTick.Checked = false;
                }
                ddlIsoCountryCode2.SelectedValue = Convert.ToString(dt.Rows[0]["ISO_RFT_COUNTRYCODE"]);

                txtIDResTax.Text = Convert.ToString(dt.Rows[0]["TAX_IDNUMBER"]);
                txtDOBRes.Text = Convert.ToString(dt.Rows[0]["BIRTH_PLACE"]);
                ddlIsoCountry.SelectedValue = Convert.ToString(dt.Rows[0]["ISO_BIRTHPLACE_CODE"]);

                ddlProofIdentity.SelectedValue = Convert.ToString(dt.Rows[0]["IdType"]);
                ViewState["strIdName"] = Convert.ToString(dt.Rows[0]["IdName"]);
                ViewState["strIdNumber"] = Convert.ToString(dt.Rows[0]["IdNumber"]);
                ViewState["strIdExpDate"] = Convert.ToString(dt.Rows[0]["IdExpDate"]);

                if (ddlProofIdentity.SelectedValue == "Z")
                {
                    txtPassOthr.Text = Convert.ToString(dt.Rows[0]["IdNumber"]);
                    txtPassNo.Text = Convert.ToString(dt.Rows[0]["IdName"]);
                }
                else
                {
                    txtPassNo.Text = Convert.ToString(dt.Rows[0]["IdNumber"]);
                    txtPassExpDate.Text = Convert.ToString(dt.Rows[0]["IdExpDate"]);
                }

                if (Convert.ToString(dt.Rows[0]["CnctType1"]) == "P1")
                {
                    chkPerAddress.Checked = true;
                }
                else
                {
                    chkPerAddress.Checked = false;
                }
                ddlProofOfAddress.SelectedValue = Convert.ToString(dt.Rows[0]["PER_ADDPROOF"]);
                txtAddressLine1.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE1"]);
                txtAddressLine2.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE2"]);
                txtAddressLine3.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE3"]);
                txtCity.Text = Convert.ToString(dt.Rows[0]["PER_CITY"]);
                ddlDistrict.SelectedItem.Text = Convert.ToString(dt.Rows[0]["PER_DISTRICT"]);
                ddlPinCode.SelectedValue = Convert.ToString(dt.Rows[0]["PER_PIN"]);
                ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["PER_STATECODE"]);
                ddlCountryCode.SelectedValue = Convert.ToString(dt.Rows[0]["PER_COUNTRY_CODE"]);
                ViewState["strAddIdName"] = Convert.ToString(dt.Rows[0]["AddIdName"]);
                ViewState["strAddIdNumber"] = Convert.ToString(dt.Rows[0]["AddIdNumber"]);
                ViewState["strAddIdExpDate"] = Convert.ToString(dt.Rows[0]["AddIdExpDate"]);

                if (ddlProofOfAddress.SelectedValue == "99")
                {
                    txtPassOthrAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNumber"]);
                    txtPassNoAdd.Text = Convert.ToString(dt.Rows[0]["AddIdName"]);
                }
                else
                {
                    txtPassNoAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNumber"]);
                    txtPassExpDateAdd.Text = Convert.ToString(dt.Rows[0]["AddIdExpDate"]);
                }

                if (Convert.ToString(dt.Rows[0]["CnctType2"]) == "M1" && (dt.Rows[0]["CUR_PIN"]).ToString() != "")
                {
                    chkLocalAddress.Checked = true;
                }
                else
                {
                    chkLocalAddress.Checked = false;
                }
                txtLocAddLine1.Text = Convert.ToString(dt.Rows[0]["CUR_ADDLINE1"]);
                txtLocAddLine2.Text = Convert.ToString(dt.Rows[0]["CUR_ADDLINE2"]);
                txtLocAddLine3.Text = Convert.ToString(dt.Rows[0]["CUR_ADDLINE3"]);
                txtCity1.Text = Convert.ToString(dt.Rows[0]["CUR_CITY"]);
                ddlDistrict1.SelectedItem.Text = Convert.ToString(dt.Rows[0]["CUR_DISTRICT"]);
                ddlPinCode1.SelectedValue = Convert.ToString(dt.Rows[0]["CUR_PIN"]);
                ddlState1.SelectedValue = Convert.ToString(dt.Rows[0]["CUR_STATECODE"]);
                ddlCountryCode1.SelectedValue = Convert.ToString(dt.Rows[0]["CUR_COUNTRY_CODE"]);



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

                txtEmpName.Text = Convert.ToString(dt.Rows[0]["kycEmpName"]);
                txtEmpCode.Text = Convert.ToString(dt.Rows[0]["kycEmpCode"]);
                txtEmpDesignation.Text = Convert.ToString(dt.Rows[0]["kycEmpDesi"]);
                txtEmpBranch.Text = Convert.ToString(dt.Rows[0]["kycEmpBranch"]);
                txtInsName.Text = Convert.ToString(dt.Rows[0]["kycInstName"]);

                txtInsCode.Text = Convert.ToString(dt.Rows[0]["kycInstCode"]);
                txtDateKYCver.Text = Convert.ToString(dt.Rows[0]["kycVerDate"]);
                ddlDocReceived.SelectedValue = Convert.ToString(dt.Rows[0]["kycCertDoc"]);

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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "FillRequiredDataForCKYC", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region 'btnUpdate_Click' Event
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string Res;

                Res = objVal.Validation(chkNormal, chkSimplified, Chksmall, cboTitle, txtGivenName, txtLastName, rbtFS, cboTitle2, txtGivenName2, txtLastName2, cboTitle3, txtGivenName3, txtLastName3, txtDOB, cboGender,
                     chkTick, ddlIsoCountryCode2, txtIDResTax, txtDOBRes, ddlIsoCountry, ddlProofIdentity,
                    txtPassNo, txtPassExpDate, chkPerAddress, ddlProofOfAddress, txtAddressLine1, txtCity, ddlPinCode, chkLocalAddress, txtLocAddLine1,
                    txtCity1, ddlPinCode1, chkAppDeclare1, txtDate, txtPlace,
                    txtDateKYCver, txtEmpName, txtEmpCode, txtEmpDesignation, txtEmpBranch, txtInsName, txtInsCode, ddlIsoCountryCodeOthr, ddlIsoCountry, txtPassOthr, txtPanNo, ddlDocReceived);

                if (Res.Equals(""))
                {
                    if (txtDOB.Text != "")
                    {
                        string date;
                        date = DateTime.Today.ToString("dd\\/MM\\/yyyy");

                        //if (Convert.ToDateTime(date) < Convert.ToDateTime(txtDOB.Text))
                        //{
                        DateTime date1, date2;
                        date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        date2 = DateTime.ParseExact(txtDOB.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        if (date1 < date2)
                        {
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You cannot select future date')", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('You cannot select future date')", true);
                            return;
                        }
                    }

                    #region relatedpersonDSvalidation

                    //dsRel.Clear();
                    dt = new DataTable();
                    dt = (DataTable)Session["dsRel"];

                    if (chkAddRel.Checked == true)
                    {
                        if (dt == null)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please Add atleast One Relative Details')", true);
                            //chkAddRel.Focus();
                            chkAddRel.Checked = false;
                            return;
                        }

                        if (dt.Rows.Count == 0)
                        {
                            if ((dt.Rows[0]["RelationType"]).ToString() == "" && (dt.Rows[0]["PREFIX"]).ToString() == "" && (dt.Rows[0]["FNAME"]).ToString() == "" && (dt.Rows[0]["LNAME"]).ToString() == ""
                               && (dt.Rows[0]["fs_flag"]).ToString() == "" && (dt.Rows[0]["FATHER_PREFIX"]).ToString() == "" && (dt.Rows[0]["FATHER_FNAME"]).ToString() == "" && (dt.Rows[0]["FATHER_LNAME"]).ToString() == ""
                                && (dt.Rows[0]["MOTHER_PREFIX"]).ToString() == "" && (dt.Rows[0]["MOTHER_FNAME"]).ToString() == "" && (dt.Rows[0]["MOTHER_LNAME"]).ToString() == "" && (dt.Rows[0]["DOB"]).ToString() == "" &&
                                (dt.Rows[0]["GENDER"]).ToString() == "" && (dt.Rows[0]["MARITAL_STATUS"]).ToString() == "" && (dt.Rows[0]["CITIZENSHIP"]).ToString() == "" && (dt.Rows[0]["RESI_STATUS"]).ToString() == "")
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Add Relative Details')", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please Add Relative Details')", true);
                            //chkAddRel.Focus();
                            return;
                        }
                    }
                    #endregion

                    #region cndkycdetails


                    htParam.Clear();
                    if (cbNew.Checked == true)
                    {
                        htParam.Add("@AppType", "01");
                    }
                    else
                    {
                        htParam.Add("@AppType", "02");
                    }
                    if (chkNormal.Checked == true)
                    {
                        htParam.Add("@AccType", "01");
                    }
                    else if (chkSimplified.Checked == true)
                    {
                        htParam.Add("@AccType", "02");
                    }
                    else if (Chksmall.Checked == true)
                    {
                        htParam.Add("@AccType", "03");
                    }
                    htParam.Add("@RegRefNo", txtRefNumber.Text.ToString());
                    htParam.Add("@KYC_NO", txtKYCNumber.Text.ToString());
                    htParam.Add("@PREFIX", cboTitle.SelectedValue);
                    htParam.Add("@FNAME", txtGivenName.Text.Trim());
                    htParam.Add("@MNAME", txtMiddleName.Text.Trim());
                    htParam.Add("@LNAME", txtLastName.Text.Trim());
                    htParam.Add("@MAID_PREFIX", cboTitle1.SelectedValue);
                    htParam.Add("@MAID_FNAME", txtGivenName1.Text.Trim());
                    htParam.Add("@MAID_MNAME", txtMiddleName1.Text.Trim());
                    htParam.Add("@MAID_LNAME", txtLastName1.Text.Trim());

                    if (rbtFS.SelectedValue == "F")
                    {
                        htParam.Add("@fs_flag", "01");
                    }
                    else
                    {
                        htParam.Add("@fs_flag", "02");
                    }

                    htParam.Add("@FATHER_PREFIX", cboTitle2.SelectedValue);
                    htParam.Add("@FATHER_FNAME", txtGivenName2.Text.Trim());
                    htParam.Add("@FATHER_MNAME", txtMiddleName2.Text.Trim());
                    htParam.Add("@FATHER_LNAME", txtLastName2.Text.Trim());
                    htParam.Add("@MOTHER_PREFIX", cboTitle3.SelectedValue);
                    htParam.Add("@MOTHER_FNAME", txtGivenName3.Text);
                    htParam.Add("@MOTHER_MNAME", txtMiddleName2.Text);
                    htParam.Add("@MOTHER_LNAME", txtLastName3.Text);
                    htParam.Add("@DOB", txtDOB.Text);
                    htParam.Add("@GENDER", cboGender.SelectedValue);
                    htParam.Add("@MARITAL_STATUS", "");
                    htParam.Add("@CITIZENSHIP", "");
                    htParam.Add("@RESI_STATUS", "");
                    htParam.Add("@OccupationType", "");
                    if (chkTick.Checked == true)
                    {
                        htParam.Add("@JurisdictionFlag", "01");
                    }
                    else
                    {
                        htParam.Add("@JurisdictionFlag", "02");
                    }
                    htParam.Add("@TINIssuingCountry", ddlIsoCountryCode2.SelectedValue.Trim());
                    htParam.Add("@TIN", txtIDResTax.Text.Trim());
                    htParam.Add("@JurisdictionBirthPlace", txtDOBRes.Text.Trim());
                    htParam.Add("@JurisdictionCountryofBirth", ddlIsoCountry.SelectedValue.Trim());
                    htParam.Add("@IdType", ddlProofIdentity.SelectedValue);
                    if (ddlProofIdentity.SelectedIndex == 1)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", txtPassExpDate.Text.Trim());
                        htParam.Add("@IdName", System.DBNull.Value);

                    }
                    else if (ddlProofIdentity.SelectedIndex == 2)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", System.DBNull.Value);

                    }
                    else if (ddlProofIdentity.SelectedIndex == 3)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 4)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", txtPassExpDate.Text.Trim());
                        htParam.Add("@IdName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 5)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 6)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 7)
                    {
                        htParam.Add("@IdNumber", txtPassOthr.Text.Trim());
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", txtPassNo.Text.Trim());
                    }
                    else if (ddlProofIdentity.SelectedIndex == 8 || ddlProofIdentity.SelectedIndex == 9)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", System.DBNull.Value);
                    }
                    else
                    {
                        htParam.Add("@IdNumber", System.DBNull.Value);
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", System.DBNull.Value);
                    }


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



                    htParam.Add("@AddIdType", ddlProofOfAddress.SelectedValue);
                    if (chkPerAddress.Checked == true)
                    {
                        if (ddlProofOfAddress.SelectedIndex == 1)
                        {
                            htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                            htParam.Add("@AddIdExpDate", txtPassExpDateAdd.Text.Trim());
                            htParam.Add("@AddIdName", System.DBNull.Value);

                        }
                        else if (ddlProofOfAddress.SelectedIndex == 2)
                        {
                            htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                            htParam.Add("@AddIdExpDate", txtPassExpDateAdd.Text.Trim());
                            htParam.Add("@AddIdName", System.DBNull.Value);
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 3)
                        {
                            htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                            htParam.Add("@AddIdExpDate", System.DBNull.Value);
                            htParam.Add("@AddIdName", System.DBNull.Value);
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 4)
                        {
                            htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                            htParam.Add("@AddIdExpDate", System.DBNull.Value);
                            htParam.Add("@AddIdName", System.DBNull.Value);

                        }
                        else if (ddlProofOfAddress.SelectedIndex == 5)
                        {
                            htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                            htParam.Add("@AddIdExpDate", System.DBNull.Value);
                            htParam.Add("@AddIdName", System.DBNull.Value);
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 6)
                        {
                            htParam.Add("@AddIdNumber", txtPassOthrAdd.Text.Trim());
                            htParam.Add("@AddIdExpDate", System.DBNull.Value);
                            htParam.Add("@AddIdName", txtPassNoAdd.Text.Trim());

                        }
                        else
                        {
                            htParam.Add("@AddIdNumber", System.DBNull.Value);
                            htParam.Add("@AddIdExpDate", System.DBNull.Value);
                            htParam.Add("@AddIdName", System.DBNull.Value);
                        }
                    }
                    else
                    {
                        htParam.Add("@AddIdNumber", System.DBNull.Value);
                        htParam.Add("@AddIdExpDate", System.DBNull.Value);
                        htParam.Add("@AddIdName", System.DBNull.Value);
                    }

                    if (chkLocalAddress.Checked == true)
                    {
                        htParam.Add("@CnctType2", "M1");
                        htParam.Add("@SameAsPer", "01");//by meena 19/05/2017
                        htParam.Add("@CUR_ADDLINE1", txtLocAddLine1.Text);
                        htParam.Add("@CUR_ADDLINE2", txtLocAddLine2.Text);
                        htParam.Add("@CUR_ADDLINE3", txtLocAddLine3.Text);
                        htParam.Add("@CUR_CITY", txtCity1.Text.Trim());
                        htParam.Add("@CUR_DISTRICT", ddlDistrict1.SelectedValue);
                        htParam.Add("@CUR_PIN", ddlPinCode1.SelectedValue);
                        htParam.Add("@CUR_STATECODE", ddlState1.SelectedValue);
                        htParam.Add("@CUR_COUNTRY_CODE", ddlCountryCode1.SelectedValue);
                    }
                    else
                    {
                        htParam.Add("@CnctType2", "");
                        htParam.Add("@SameAsPer", "02");//by meena 19/05/2017
                        htParam.Add("@CUR_ADDLINE1", System.DBNull.Value);
                        htParam.Add("@CUR_ADDLINE2", System.DBNull.Value);
                        htParam.Add("@CUR_ADDLINE3", System.DBNull.Value);
                        htParam.Add("@CUR_CITY", System.DBNull.Value);
                        htParam.Add("@CUR_DISTRICT", System.DBNull.Value);
                        htParam.Add("@CUR_PIN", System.DBNull.Value);
                        htParam.Add("@CUR_STATECODE", System.DBNull.Value);
                        htParam.Add("@CUR_COUNTRY_CODE", System.DBNull.Value);
                    }


                    htParam.Add("@CnctType3", "");
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
                    htParam.Add("@std_fax", txtFax1.Text.Trim());

                    htParam.Add("@OFF_TELE", txtTelOff.Text);
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
                    htParam.Add("@kycInstName", txtInsName.Text.Trim());
                    htParam.Add("@kycInstCode", txtInsCode.Text.Trim());
                    htParam.Add("@MODIFIEDBY", strUserId.ToString());
                    htParam.Add("@Usages", "W");

                    objDAL = new DataAccessLayer("CKYCConnectionString");
                    DataTable dtResult = new DataTable();
                    dtResult = objDAL.GetDataTable("prc_updatekycdtls", htParam);

                    if (dtResult.Rows.Count > 0)
                    {
                        if (dtResult.Rows.Count > 0)
                        {
                            strRefNo = dtResult.Rows[0]["RegRefNo"].ToString();
                            string message = dtResult.Rows[0]["MESSAGE"].ToString();
                        }
                    }
                    #endregion

                    #region Save Members Details
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                //if (dsRel.Tables[0].Rows[i]["RelRefNo"] == "")
                                //{

                                htParam.Clear();
                                if (chkAddRel.Checked == true)
                                {
                                    htParam.Add("@RelAddDelFlag", "01");
                                }
                                else
                                {
                                    htParam.Add("@RelAddDelFlag", "02");
                                }


                                //htParam.Add("@RelAddDelFlag", dsRel.Tables[0].Rows[i]["AddDelFlagRel"]);
                                // htParam.Add("@RelatedPrsnKYCNo", dsRel.Tables[0].Rows[i]["RelatedPrsnKYCNo"]);
                                htParam.Add("@RelTYPE", dt.Rows[i]["RelationType"]);
                                htParam.Add("@RelCKYCNO", dt.Rows[i]["RelatedPrsnKYCNo"]);
                                htParam.Add("@RelPrefix", dt.Rows[i]["PrefixRel"]);
                                htParam.Add("@RelFName", dt.Rows[i]["FNameRel"]);
                                htParam.Add("@RelMName", dt.Rows[i]["MNameRel"]);
                                htParam.Add("@RelLName", dt.Rows[i]["LNameRel"]);
                                htParam.Add("@RelMaidPrefix", dt.Rows[i]["MaidPrefixRel"]);
                                htParam.Add("@RelMaidFNmae", dt.Rows[i]["MaidFNameRel"]);
                                htParam.Add("@RelMaidMNmae", dt.Rows[i]["MaidMNameRel"]);
                                htParam.Add("@RelMaidLNmae", dt.Rows[i]["MaidLNameRel"]);
                                htParam.Add("@FlagFatherorSpouse", dt.Rows[i]["FSFlagRel"]);
                                htParam.Add("@RelFatherPrefix", dt.Rows[i]["FatherPrefixRel"]);
                                htParam.Add("@RelFatheFName", dt.Rows[i]["FatherFNameRel"]);
                                htParam.Add("@RelFatheMName", dt.Rows[i]["FatherMNameRel"]);
                                htParam.Add("@RelFatheLName", dt.Rows[i]["FatherLNameRel"]);
                                htParam.Add("@RelMotherPrefix", dt.Rows[i]["MotherPrefixRel"]);
                                htParam.Add("@RelMotherFName", dt.Rows[i]["MotherFNameRel"]);
                                htParam.Add("@RelMotherMname", dt.Rows[i]["MotherMNameRel"]);
                                htParam.Add("@RelMotherLName", dt.Rows[i]["MotherLNameRel"]);
                                htParam.Add("@RelDOB", dt.Rows[i]["DOBRel"]);
                                htParam.Add("@RelGender", dt.Rows[i]["GenderRel"]);
                                htParam.Add("@RelMaritalStatus", dt.Rows[i]["MaritalStatusRel"]);

                                htParam.Add("@RelResistatus", dt.Rows[i]["ResiStatusRel"]);
                                htParam.Add("@RelOccuType", dt.Rows[i]["OccuTypeRel"]);
                                htParam.Add("@RelCitizenship", dt.Rows[i]["CitizenshipRel"]);


                                htParam.Add("@RelJurisdictionFlag", dt.Rows[i]["ResForTaxFlagRel"]);
                                htParam.Add("@RelISOCountryCode", dt.Rows[i]["ISOCountryCodeRel"]);
                                htParam.Add("@RelTIN", dt.Rows[i]["TaxIDNumberRel"]);
                                htParam.Add("@RelBirthCity", dt.Rows[i]["BirthCityRel"]);
                                htParam.Add("@RelISOBirthPlace", dt.Rows[i]["ISOBirthPlaceCodeRel"]);

                                htParam.Add("@IDType", dt.Rows[i]["IdType"]);
                                htParam.Add("@IDNum", dt.Rows[i]["IdNumber"]);
                                htParam.Add("@IDExpDate", dt.Rows[i]["IdExpDate"]);
                                htParam.Add("@IDName", dt.Rows[i]["IdName"]);



                                htParam.Add("@CnctType1", "P1");
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


                                //htParam.Add("@PER_ADDPROOF", dt.Rows[i]["AddIdTypeRel"]);

                                htParam.Add("@PER_IDNUMBAER", dt.Rows[i]["AddIdNumberRel"]);
                                htParam.Add("@PER_IDEXPDT", dt.Rows[i]["AddIdExpDateRel"]);
                                htParam.Add("@PER_IDOtherDocName", dt.Rows[i]["AddIdNameRel"]);




                                htParam.Add("@RelDecDate", dt.Rows[i]["DecDateRel"]);
                                htParam.Add("@RelDecPlace", dt.Rows[i]["DecPlaceRel"]);
                                htParam.Add("@RelkycEmpName", dt.Rows[i]["kycEmpNameRel"]);
                                htParam.Add("@RelkycEmpCode", dt.Rows[i]["kycEmpCodeRel"]);
                                htParam.Add("@RelkycEmpBranch", dt.Rows[i]["kycEmpBranchRel"]);
                                htParam.Add("@RelkycEmpDesi", dt.Rows[i]["kycEmpDesiRel"]);
                                htParam.Add("@RelkycVerDate", dt.Rows[i]["kycVerDateRel"]);

                                htParam.Add("@RelkycCertDoc", "01");

                                htParam.Add("@RelkycInstName", dt.Rows[i]["kycInstNameRel"]);
                                htParam.Add("@RelkycInstCode", dt.Rows[i]["kycInstCodeRel"]);
                                htParam.Add("@UpdateBy", strUserId.ToString());

                                if (Request.QueryString["Status"].ToString() == "Mod")
                                {
                                    htParam.Add("@RegRefNo", txtRefNumber.Text.ToString());
                                    htParam.Add("@RelRefNo", dt.Rows[i]["RelRefNo"]);
                                    objDAL.ExecuteNonQuery("prc_updKycRelPrsnDtls", htParam);

                                }

                                Session["dsRel"] = null;
                                //}
                                //else

                            }
                        }
                    }
                    #endregion
                    if (gvMemDtls.Visible == true)
                    {
                        foreach (GridViewRow row in gvMemDtls.Rows)
                        {
                            LinkButton lnkdelete = (LinkButton)row.FindControl("lnkdelete");
                            lnkdelete.Enabled = false;
                        }
                    }
                    btnUpdate.Enabled = false;
                    hdnUpdate.Value = "Candidate updated successfully";
                    msg = hdnUpdate.Value + "</br></br>Reference No: " + strRefNo.ToString().Trim() + "<br/>Candidate Name: "
                         + cboTitle.SelectedValue + " " + txtGivenName.Text + " " + txtLastName.Text;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "popup('" + msg + "');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('" + msg + "')", true);

                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Res + "')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('" + Res + "')", true);
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "btnUpdate_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region DROPDOWN 'ddlPinCode' SELECTEDINDEXCHANGED EVENT
        protected void ddlPinCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string date;
                date = DateTime.Today.ToString("dd\\/MM\\/yyyy");

                if (chkPerAddress.Checked == false)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please check CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please check current/permanent/overseas address details')", true);
                    //chkPerAddress.Focus();
                    ddlPinCode.SelectedIndex = 0;
                    return;
                }
                // Commented By Pratik
                //if (ddlAddressType.SelectedIndex == 0)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please select address type')", true);
                //    //ddlAddressType.Focus();
                //    ddlPinCode.SelectedIndex = 0;
                //    return;
                //}
                if (ddlProofOfAddress.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please select proof of address')", true);
                    ddlPinCode.SelectedIndex = 0;
                    return;
                }
                if (ddlProofOfAddress.SelectedIndex != 0)
                {
                    if (ddlProofOfAddress.SelectedIndex == 1)
                    {
                        if (txtPassNoAdd.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter passport number')", true);
                            txtPassNoAdd.Focus();
                            ddlPinCode.SelectedIndex = 0;
                            return;
                        }
                        //if (txtPassExpDateAdd.Text == "")
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter passport expiry date')", true);
                        //    txtPassExpDateAdd.Focus();
                        //    ddlPinCode.SelectedIndex = 0;
                        //    return;
                        //}
                        if (txtPassExpDateAdd.Text != "")
                        {
                            DateTime date1, date2;
                            date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            date2 = DateTime.ParseExact(txtPassExpDateAdd.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            if (date1 > date2)
                            {

                                //if (Convert.ToDateTime(date) > Convert.ToDateTime(txtPassExpDateAdd.Text))
                                //{
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You cannot select past date as driving license expiry date')", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('You cannot select past date as driving license expiry date')", true);
                                txtPassExpDateAdd.Focus();
                                ddlPinCode.SelectedIndex = 0;
                                return;
                            }
                        }
                    }

                    if (ddlProofOfAddress.SelectedIndex == 2)
                    {
                        if (txtPassNoAdd.Text == "")
                        {
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter driving licence no')", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter driving licence number')", true);
                            txtPassNoAdd.Focus();
                            ddlPinCode.SelectedIndex = 0;
                            return;
                        }

                        //if (txtPassExpDateAdd.Text == "")
                        //{
                        //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter driving licence expiry date')", true);
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter driving licence expiry date')", true);
                        //    txtPassExpDateAdd.Focus();
                        //    ddlPinCode.SelectedIndex = 0;
                        //    return;
                        //}
                        if (txtPassExpDateAdd.Text != "")
                        {
                            DateTime date1, date2;
                            date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            date2 = DateTime.ParseExact(txtPassExpDateAdd.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            if (date1 > date2)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('You cannot select past date as driving license expiry date')", true);
                                ddlPinCode.SelectedIndex = 0;
                                return;
                            }
                        }
                    }

                    if (ddlProofOfAddress.SelectedIndex == 3)
                    {
                        if (txtPassNoAdd.Text == "")
                        {
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter UID(Aadhaar)')", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter UID(aadhaar)')", true);
                            txtPassNoAdd.Focus();
                            ddlPinCode.SelectedIndex = 0;
                            return;
                        }
                    }
                    if (ddlProofOfAddress.SelectedIndex == 4)
                    {
                        if (txtPassNoAdd.Text == "")
                        {
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter voter id card')", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter voter id card')", true);
                            txtPassNoAdd.Focus();
                            ddlPinCode.SelectedIndex = 0;
                            return;
                        }
                    }
                    if (ddlProofOfAddress.SelectedIndex == 5)
                    {
                        if (txtPassNoAdd.Text == "")
                        {
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter NREGA job card')", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter NREGA job card')", true);
                            txtPassNoAdd.Focus();
                            ddlPinCode.SelectedIndex = 0;
                            return;
                        }
                    }
                    if (ddlProofOfAddress.SelectedIndex == 6)
                    {
                        if (txtPassNoAdd.Text == "")
                        {
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter other no of proof of Address')", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter other no of proof of Address')", true);
                            txtPassNoAdd.Focus();
                            ddlPinCode.SelectedIndex = 0;
                            return;
                        }
                    }
                }
                if (txtAddressLine1.Text == "")
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter permanent address line 1')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter permanent address line 1')", true);
                    txtAddressLine1.Focus();
                    ddlPinCode.SelectedIndex = 0;
                    return;
                }
                if (txtCity.Text == "")
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter permanent city/Town/Village')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter permanent city/Town/Village')", true);
                    txtCity.Focus();
                    ddlPinCode.SelectedIndex = 0;
                    return;
                }
                if (ddlPinCode.SelectedIndex == 0 && chkTick.Checked == false)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter permanent Pin/Post Code')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter permanent Pin/Post Code')", true);
                    ddlPinCode.Focus();
                    ddlPinCode.SelectedIndex = 0;
                    return;
                }
                chkPerAddress.Enabled = false;
                ddlCountryCode.SelectedValue = "IN";
                // Added BY Pratik
                FillDistrictState(ddlPinCode, ddlDistrict, ddlState);
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ddlPinCode_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
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
                Response.Redirect("CKYCSearch.aspx?status=view");
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

        #region DROPDOWN 'ddlPinCode1' SELECTEDINDEXCHANGED EVENT
        protected void ddlPinCode1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPinCode1.SelectedIndex == 0)
                {
                    ddlDistrict1.SelectedIndex = 0;
                    ddlState1.SelectedIndex = 0;
                    ddlCountryCode1.SelectedIndex = 0;
                    return;
                }

                if (chkLocalAddress.Checked == false)
                {
                    ddlPinCode1.SelectedIndex = 0;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please check CORRESPONDENCE/LOCAL ADDRESS DETAILS.');", true);
                    return;
                }



                FillDistrictState(ddlPinCode1, ddlDistrict1, ddlState1);
                ddlCountryCode1.SelectedValue = "IN";
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


        #region METHOD 'POPULATE DROPDOWNLIST'
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

        protected void FillDocumentReceived()
        {
            try
            {
                htParam.Clear();
                htParam.Add("@LookupCode", "DocReceived");
                htParam.Add("@ParamUsage", "KI");

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
        private void subPopulateTitle()
        {
            try
            {
                oCommonUtility.GetCKYC(cboTitle, "KTitle");
                cboTitle.Items.Insert(0, new ListItem("Select", ""));
                oCommonUtility.GetCKYC(cboTitle1, "KTitle");
                cboTitle1.Items.Insert(0, new ListItem("Select", ""));
                oCommonUtility.GetCKYC(cboTitle2, "KTitle");
                cboTitle2.Items.Insert(0, new ListItem("Select", ""));
                oCommonUtility.GetCKYC(cboTitle3, "KMTitle");
                cboTitle3.Items.Insert(0, new ListItem("Select", ""));

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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "subPopulateTitle", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        private void subPopulateGender()
        {
            try
            {
                oCommonUtility.GetCKYC(cboGender, "KGender");
                cboGender.Items.Insert(0, new ListItem("Select", ""));
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "subPopulateGender", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        private void PopulateProofIdentiy()
        {
            try
            {
                oCommonUtility.GetCKYC(ddlProofIdentity, "KId");
                ddlProofIdentity.Items.Insert(0, new ListItem("Select", ""));
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "PopulateProofIdentiy", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        private void PopulateAddressProofType()
        {
            try
            {
                oCommonUtility.GetCKYC(ddlProofOfAddress, "KAddrPrf");
                ddlProofOfAddress.Items.Insert(0, new ListItem("Select", ""));
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "PopulateAddressProofType", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        private void PopulatePinCode()
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                htParam.Clear();

                htParam.Add("@PinCode", System.DBNull.Value);
                htParam.Add("@flag", "P");
                dt = objDAL.GetDataTable("Prc_GetAddressCKYC", htParam);
                if (dt.Rows.Count > 0)
                {
                    ddlPinCode.DataSource = dt;
                    ddlPinCode1.DataSource = dt;
                    ddlPinCode.DataTextField = "PinCode";
                    ddlPinCode1.DataTextField = "PinCode";
                    ddlPinCode.DataBind();
                    ddlPinCode1.DataBind();
                    ddlPinCode.Items.Insert(0, new ListItem("Select", string.Empty));
                    ddlPinCode1.Items.Insert(0, new ListItem("Select", string.Empty));
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "PopulatePinCode", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dt = null;
            }
        }
        #endregion

        #region Fill Sub Occupation Type Details
        public void FillSubOccuType(DropDownList ddl1, DropDownList ddl2)
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                htParam.Clear();

                htParam.Add("@OccFlag", ddl1.SelectedValue);
                dt = objDAL.GetDataTable("Prc_GetSubOccType", htParam);
                if (dt.Rows.Count > 0)
                {
                    ddl2.DataSource = dt;
                    ddl2.DataTextField = "Occupation";
                    ddl2.DataValueField = "OccCode";
                    ddl2.DataBind();
                    ddl2.Items.Insert(0, new ListItem("Select", string.Empty));
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "FillSubOccuType", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
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

        #region Fill Sub Occupation Type Details
        public void FillDistrictState(DropDownList ddl1, DropDownList ddl2, DropDownList ddl3)
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                htParam.Clear();
                htParam.Add("@PinCode", ddl1.SelectedValue.ToString());
                htParam.Add("@flag", System.DBNull.Value);
                dt = objDAL.GetDataTable("Prc_GetAddressCKYC", htParam);
                if (dt.Rows.Count > 0)
                {
                    ddl2.DataSource = dt;
                    ddl2.DataTextField = "District";
                    ddl2.DataBind();
                    ddl3.DataSource = dt;
                    ddl3.DataTextField = "State_Name";
                    ddl3.DataValueField = "State_code";
                    ddl3.DataBind();
                }
                else
                {
                    ddl2.Items.Clear();
                    ddl2.Items.Insert(0, new ListItem("Select", ""));

                    ddl3.Items.Clear();
                    ddl3.Items.Insert(0, new ListItem("Select", ""));

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


        #region Save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string Res, kyc, regref;
                if (txtPassNo.Text != "")
                {
                    objDAL = new DataAccessLayer("CKYCConnectionString");
                    dt = new DataTable();
                    htParam.Clear();
                    htParam.Add("@IDNo", txtPassNo.Text.ToString().Trim());

                    dt = objDAL.GetDataTable("prc_verifyID", htParam);

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["result"].ToString() == "TRUE")
                        {
                            if (dt.Rows[0]["KYC_NO"].ToString() == "")
                            {
                                regref = dt.Rows[0]["RegRefNo"].ToString();

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select other Proof Of Identity as given " + lblPassportNo.Text + "ID number is already registered with this Refrence no." + regref + " ')", true);
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please select other Proof Of Identity as gi')", true);
                                //txtPassNo.Focus();
                                return;
                            }
                            else
                            {
                                kyc = dt.Rows[0]["KYC_NO"].ToString();
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select other Proof Of Identity as given " + lblPassportNo.Text + "ID number is already registered with this KYC no." + kyc + " ')", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select other Proof Of Identity as given " + lblPassportNo.Text + "ID number is already registered with this KYC no." + kyc + " ')", true);
                                //txtPassNo.Focus();
                                return;
                            }
                        }
                    }
                }

                Res = objVal.Validation(chkNormal, chkSimplified, Chksmall, cboTitle, txtGivenName, txtLastName, rbtFS, cboTitle2, txtGivenName2, txtLastName2, cboTitle3, txtGivenName3, txtLastName3, txtDOB, cboGender,
                    chkTick, ddlIsoCountryCode2, txtIDResTax, txtDOBRes, ddlIsoCountry, ddlProofIdentity,
                    txtPassNo, txtPassExpDate, chkPerAddress, ddlProofOfAddress, txtAddressLine1, txtCity, ddlPinCode, chkLocalAddress, txtLocAddLine1,
                    txtCity1, ddlPinCode1, chkAppDeclare1, txtDate, txtPlace,
                    txtDateKYCver, txtEmpName, txtEmpCode, txtEmpDesignation, txtEmpBranch, txtInsName, txtInsCode, ddlIsoCountryCodeOthr, ddlIsoCountry, txtPassOthr, txtPanNo, ddlDocReceived);  //txtPassOthr,

                if (Res.Equals(""))
                {
                    //if (txtDOB.Text != "")
                    //{
                    //    string date;
                    //    date = DateTime.Today.ToString("dd\\/MM\\/yyyy");

                    //    //if (Convert.ToDateTime(date) < Convert.ToDateTime(txtDOB.Text))
                    //    //{
                    //    DateTime date1, date2;
                    //    date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    //    date2 = DateTime.ParseExact(txtDOB.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    //    if (date1 < date2)
                    //    {
                    //        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You cannot select future date')", true);
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('You cannot select future date')", true);
                    //        return;
                    //    }
                    //}

                    //if (source == "tele_home")
                    //{
                    //    popup("");
                    //}
                    //else if (source == "tele_off")
                    //{
                    //    popup("Office STD code and Telephone number is mandatory");
                    //}
                    //else if (source == "mobile")
                    //{
                    //    popup("Mobile ISD code and mobile number is mandatory");
                    //}
                    //else if (source == "fax")
                    //{
                    //    popup("Fax STD code and fax number is mandatory");
                    //}


                    if (!chkPerAddress.Checked)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please check CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS')", true);
                        return;
                    }
                    else
                    {
                        if (txtAddressLine1.Text.Trim() == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please enter permenent adderess line 1')", true);
                            return;
                        }


                        if (txtCity.Text.Trim() == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please enter permenent City/Town/Village')", true);
                            return;
                        }

                        if (ddlCountryCode.SelectedIndex == 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please select country code ')", true);
                            return;
                        }

                        if (ddlCountryCode.SelectedValue == "IN")
                        {
                            if (ddlPinCode.SelectedIndex == 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please select pin/postcode')", true);
                                return;
                            }

                        }
                    }

                    if (!chkLocalAddress.Checked)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please check CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS')", true);
                        return;
                    }
                    else
                    {
                        if (txtLocAddLine1.Text.Trim() == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please enter permenent adderess line 1')", true);
                            return;
                        }


                        if (txtCity1.Text.Trim() == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please enter permenent City/Town/Village')", true);
                            return;
                        }

                        if (ddlCountryCode1.SelectedIndex == 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please select country code ')", true);
                            return;
                        }

                        if (ddlCountryCode1.SelectedValue == "IN")
                        {
                            if (ddlPinCode1.SelectedIndex == 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please select pin/postcode')", true);
                                return;
                            }

                        }
                    }


                    if ((txtTelOff.Text.Trim() == "" && txtTelOff2.Text.Trim() != "") || (txtTelOff.Text.Trim() != "" && txtTelOff2.Text.Trim() == ""))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Office STD code and Telephone number is mandatory')", true);
                        return;
                    }

                    if ((txtTelRes.Text.Trim() == "" && txtTelRes2.Text.Trim() != "") || (txtTelRes.Text.Trim() != "" && txtTelRes2.Text.Trim() == ""))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Resident STD code and Telephone number is mandatory')", true);
                        return;
                    }

                    if ((txtMobile.Text.Trim() == "" && txtMobile2.Text.Trim() != "") || (txtMobile.Text.Trim() != "" && txtMobile2.Text.Trim() == ""))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Mobile ISD code and mobile number is mandatory')", true);
                        return;
                    }

                    if ((txtFax1.Text.Trim() == "" && txtFax2.Text.Trim() != "") || (txtFax1.Text.Trim() != "" && txtFax2.Text.Trim() == ""))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Fax STD code and fax number is mandatory')", true);
                        return;
                    }

                    #region relatedpersonDSvalidation
                    DataTable dtRel = new DataTable();
                    //if (dsRel != null) { dsRel.Clear(); }

                    dtRel = (DataTable)Session["dsRel"];

                    if (chkAddRel.Checked == true)
                    {
                        //if (dsRel.Tables.Count == 0)
                        if (dtRel == null)
                        {
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Add atleast One Relative Details')", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please Add atleast One Relative Details')", true);
                            //chkAddRel.Focus();
                            chkAddRel.Checked = false;
                            return;
                        }
                    }
                    #endregion
                    #region cnd kyc Details
                    htParam.Clear();


                    if (cbNew.Checked == true)
                    {
                        htParam.Add("@AppType", "01");
                    }
                    else
                    {
                        htParam.Add("@AppType", "02");
                    }

                    if (chkNormal.Checked == true)
                    {
                        htParam.Add("@AccType", "01");
                    }
                    else if (chkSimplified.Checked == true)
                    {
                        htParam.Add("@AccType", "02");
                    }
                    else if (Chksmall.Checked == true)
                    {
                        htParam.Add("@AccType", "03");
                    }
                    htParam.Add("@PREFIX", cboTitle.SelectedValue);
                    htParam.Add("@FNAME", txtGivenName.Text.Trim());
                    htParam.Add("@MNAME", txtMiddleName.Text.Trim());
                    htParam.Add("@LNAME", txtLastName.Text.Trim());
                    htParam.Add("@MAID_PREFIX", cboTitle1.SelectedValue);
                    htParam.Add("@MAID_FNAME", txtGivenName1.Text.Trim());
                    htParam.Add("@MAID_MNAME", txtMiddleName1.Text.Trim());
                    htParam.Add("@MAID_LNAME", txtLastName1.Text.Trim());

                    if (rbtFS.SelectedValue == "F")
                    {
                        htParam.Add("@fs_flag", "01");
                    }
                    else
                    {
                        htParam.Add("@fs_flag", "02");
                    }
                    htParam.Add("@FATHER_PREFIX", cboTitle2.SelectedValue);
                    htParam.Add("@FATHER_FNAME", txtGivenName2.Text.Trim());
                    htParam.Add("@FATHER_MNAME", txtMiddleName2.Text.Trim());
                    htParam.Add("@FATHER_LNAME", txtLastName2.Text.Trim());
                    htParam.Add("@MOTHER_PREFIX", cboTitle3.SelectedValue);
                    htParam.Add("@MOTHER_FNAME", txtGivenName3.Text);
                    htParam.Add("@MOTHER_MNAME", txtMiddleName2.Text);
                    htParam.Add("@MOTHER_LNAME", txtLastName3.Text);
                    htParam.Add("@DOB", txtDOB.Text);
                    htParam.Add("@GENDER", cboGender.SelectedValue);
                    htParam.Add("@MARITAL_STATUS", "");
                    htParam.Add("@CITIZENSHIP", "");
                    htParam.Add("@PAN", txtPanNo.Text.Trim());
                    htParam.Add("@RESI_STATUS", "");

                    htParam.Add("@OccupationType", "");

                    if (chkTick.Checked == true)
                    {
                        htParam.Add("@JurisdictionFlag", "01");
                    }
                    else
                    {
                        htParam.Add("@JurisdictionFlag", "02");
                    }
                    htParam.Add("@TINIssuingCountry", ddlIsoCountryCode2.SelectedValue.Trim());
                    htParam.Add("@TIN", txtIDResTax.Text.Trim());
                    htParam.Add("@JurisdictionBirthPlace", txtDOBRes.Text.Trim());
                    htParam.Add("@JurisdictionCountryofBirth", ddlIsoCountry.SelectedValue);
                    htParam.Add("@IdType", ddlProofIdentity.SelectedValue);
                    if (ddlProofIdentity.SelectedIndex == 1)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", txtPassExpDate.Text.Trim());
                        htParam.Add("@IdName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 2)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 3)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 4)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", txtPassExpDate.Text.Trim());
                        htParam.Add("@IdName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 5)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 6)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 7)
                    {
                        htParam.Add("@IdNumber", txtPassOthr.Text.Trim());
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", txtPassNo.Text.Trim());
                    }
                    else if (ddlProofIdentity.SelectedIndex == 8 || ddlProofIdentity.SelectedIndex == 9)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", System.DBNull.Value);
                    }
                    else
                    {
                        htParam.Add("@IdNumber", System.DBNull.Value);
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", System.DBNull.Value);
                    }


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

                    htParam.Add("@AddIdType", ddlProofOfAddress.SelectedValue);
                    if (chkPerAddress.Checked == true)
                    {
                        if (ddlProofOfAddress.SelectedIndex == 1)
                        {
                            htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                            htParam.Add("@AddIdExpDate", txtPassExpDateAdd.Text.Trim());
                            htParam.Add("@AddIdName", System.DBNull.Value);

                        }
                        else if (ddlProofOfAddress.SelectedIndex == 2)
                        {
                            htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                            htParam.Add("@AddIdExpDate", txtPassExpDateAdd.Text.Trim());
                            htParam.Add("@AddIdName", System.DBNull.Value);
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 3)
                        {
                            htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                            htParam.Add("@AddIdExpDate", System.DBNull.Value);
                            htParam.Add("@AddIdName", System.DBNull.Value);
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 4)
                        {
                            htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                            htParam.Add("@AddIdExpDate", System.DBNull.Value);
                            htParam.Add("@AddIdName", System.DBNull.Value);

                        }
                        else if (ddlProofOfAddress.SelectedIndex == 5)
                        {
                            htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                            htParam.Add("@AddIdExpDate", System.DBNull.Value);
                            htParam.Add("@AddIdName", System.DBNull.Value);
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 6)
                        {
                            htParam.Add("@AddIdNumber", txtPassOthrAdd.Text.Trim());
                            htParam.Add("@AddIdExpDate", System.DBNull.Value);
                            htParam.Add("@AddIdName", txtPassNoAdd.Text.Trim());
                        }
                        else
                        {
                            htParam.Add("@AddIdNumber", System.DBNull.Value);
                            htParam.Add("@AddIdExpDate", System.DBNull.Value);
                            htParam.Add("@AddIdName", System.DBNull.Value);
                        }
                    }
                    else
                    {
                        htParam.Add("@AddIdNumber", System.DBNull.Value);
                        htParam.Add("@AddIdExpDate", System.DBNull.Value);
                        htParam.Add("@AddIdName", System.DBNull.Value);
                    }


                    if (chkLocalAddress.Checked == true)
                    {
                        htParam.Add("@CnctType2", "M1");
                        htParam.Add("@SameAsPer", "01");//by meena 19/05/2017
                        htParam.Add("@CUR_ADDLINE1", txtLocAddLine1.Text);
                        htParam.Add("@CUR_ADDLINE2", txtLocAddLine2.Text);
                        htParam.Add("@CUR_ADDLINE3", txtLocAddLine3.Text);
                        htParam.Add("@CUR_CITY", txtCity1.Text.Trim());
                        htParam.Add("@CUR_DISTRICT", ddlDistrict1.SelectedValue);
                        htParam.Add("@CUR_PIN", ddlPinCode1.SelectedValue);
                        htParam.Add("@CUR_STATECODE", ddlState1.SelectedValue);
                        htParam.Add("@CUR_COUNTRY_CODE", ddlCountryCode1.SelectedValue);
                    }
                    else
                    {
                        htParam.Add("@CnctType2", "");
                        htParam.Add("@SameAsPer", "02");//by meena 19/05/2017
                        htParam.Add("@CUR_ADDLINE1", System.DBNull.Value);
                        htParam.Add("@CUR_ADDLINE2", System.DBNull.Value);
                        htParam.Add("@CUR_ADDLINE3", System.DBNull.Value);
                        htParam.Add("@CUR_CITY", System.DBNull.Value);
                        htParam.Add("@CUR_DISTRICT", System.DBNull.Value);
                        htParam.Add("@CUR_PIN", System.DBNull.Value);
                        htParam.Add("@CUR_STATECODE", System.DBNull.Value);
                        htParam.Add("@CUR_COUNTRY_CODE", System.DBNull.Value);
                    }


                    htParam.Add("@CnctType3", "");
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
                    htParam.Add("@kycInstName", txtInsName.Text.Trim());
                    htParam.Add("@kycInstCode", txtInsCode.Text.Trim());
                    htParam.Add("@CREATEDBY", strUserId.ToString());
                    htParam.Add("@UpdateFlag", "N");
                    htParam.Add("@TKYCNO", "");
                    htParam.Add("@uniqueID", obj.ToString());
                    htParam.Add("@Usages", "W");
                    htParam.Add("@Status", Request.QueryString["Status"].ToString());
                    htParam.Add("@PartialRegRefNo", txtRefNumber.Text.ToString());

                    dt = null;

                    dt = objDAL.GetDataTable("prc_Inskycdtls_Web", htParam);

                    if (dt.Rows.Count > 0)
                    {
                        strRefNo = dt.Rows[0]["RegRefNo"].ToString();
                    }
                    #endregion

                    #region Save Members Details

                    if (dtRel != null)
                    {
                        if (dtRel.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtRel.Rows.Count; i++)
                            {

                                htParam.Clear();
                                htParam.Add("@RegRefNo", strRefNo);
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

                                htParam.Add("@ResForTaxFlagRel", dtRel.Rows[i]["ResForTaxFlagRel"]);
                                htParam.Add("@ISOCountryCodeRel", dtRel.Rows[i]["ISOCountryCodeRel"]);
                                htParam.Add("@TaxIDNumberRel", dtRel.Rows[i]["TaxIDNumberRel"]);
                                htParam.Add("@BirthCityRel", dtRel.Rows[i]["BirthCityRel"]);
                                htParam.Add("@ISOBirthPlaceCodeRel", dtRel.Rows[i]["ISOBirthPlaceCodeRel"]);

                                htParam.Add("@IdType", dtRel.Rows[i]["IdType"]);
                                htParam.Add("@IdNumber", dtRel.Rows[i]["IdNumber"]);
                                htParam.Add("@IDExpDate", dtRel.Rows[i]["IdExpDate"]);
                                htParam.Add("@IDName", dtRel.Rows[i]["IdName"]);

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
                                objDAL.ExecuteNonQuery("prc_InsKycRelatedPrsnDtls", htParam);

                                Session["dsRel"] = null;
                            }
                        }
                    }
                    #endregion

                    if (gvMemDtls.Visible == true)
                    {

                        foreach (GridViewRow row in gvMemDtls.Rows)
                        {
                            LinkButton lnkdelete = (LinkButton)row.FindControl("lnkdelete");
                            lnkdelete.Enabled = false;
                        }
                    }


                    hdnUpdate.Value = "KYC registered successfully.";

                    msg = hdnUpdate.Value + "</br></br>Reference No: " + strRefNo.ToString().Trim() + "<br/>Candidate Name: "
                          + cboTitle.SelectedValue + " " + txtGivenName.Text + " " + txtLastName.Text + "<br/><br/>Please Proceed for Document Upload";


                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "popup('" + msg + "');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);



                    btnSave.Enabled = false;
                    btnPartialSave.Enabled = false;
                    btnPartialUpdate.Enabled = false;

                    txtKYCNumber.Text = strRefNo.ToString().Trim();
                }

                else
                {
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "btnSave_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region Fill country code
        public void Fillcountrycd()
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                htParam.Clear();
                dt = objDAL.GetDataTable("Prc_GetcountrycodeCKYC", htParam);
                if (dt.Rows.Count > 0)
                {
                    dt.AsEnumerable().Where(r => r.Field<string>("Country_Desc") == "India").ToList().ForEach(row => row.Delete());
                    ddlIsoCountryCodeOthr.DataSource = dt;
                    ddlIsoCountryCodeOthr.DataTextField = "Country_Desc";
                    ddlIsoCountryCodeOthr.DataValueField = "Country_CODE";
                    ddlIsoCountryCodeOthr.DataBind();
                    ddlIsoCountryCodeOthr.Items.Insert(0, new ListItem("Select", string.Empty));
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "Fillcountrycd", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
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

        #region Fill country code1
        public void Fillcountrycd1()
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                htParam.Clear();
                dt = objDAL.GetDataTable("Prc_GetcountrycodeCKYC", htParam);
                if (dt.Rows.Count > 0)
                {
                    ddlIsoCountryCode2.DataSource = dt;
                    ddlIsoCountryCode2.DataTextField = "Country_Desc";
                    ddlIsoCountryCode2.DataValueField = "Country_CODE";
                    ddlIsoCountryCode2.DataBind();
                    ddlIsoCountryCode2.Items.Insert(0, new ListItem("Select", string.Empty));

                    ddlCountryCode1.DataSource = dt;
                    ddlCountryCode1.DataTextField = "Country_Desc";
                    ddlCountryCode1.DataValueField = "Country_CODE";
                    ddlCountryCode1.DataBind();
                    ddlCountryCode1.Items.Insert(0, new ListItem("Select", string.Empty));


                    ddlCountryCode.DataSource = dt;
                    ddlCountryCode.DataTextField = "Country_Desc";
                    ddlCountryCode.DataValueField = "Country_CODE";
                    ddlCountryCode.DataBind();
                    ddlCountryCode.Items.Insert(0, new ListItem("Select", string.Empty));

                    ddlIsoCountry.DataSource = dt;
                    ddlIsoCountry.DataTextField = "Country_Desc";
                    ddlIsoCountry.DataValueField = "Country_CODE";
                    ddlIsoCountry.DataBind();
                    ddlIsoCountry.Items.Insert(0, new ListItem("Select", string.Empty));
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "Fillcountrycd1", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dt = null;
                objDAL = null;
            }
        }
        #endregion

        #region Fill country code2
        public void Fillcountrycd2(DropDownList ddl1)
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                htParam.Clear();
                dt = objDAL.GetDataTable("Prc_GetcountrycodeCKYC", htParam);
                if (dt.Rows.Count > 0)
                {
                    ddl1.DataSource = dt;
                    ddl1.DataTextField = "Country_Desc";
                    ddl1.DataValueField = "Country_CODE";
                    ddl1.DataBind();
                    ddl1.Items.Insert(0, new ListItem("Select", string.Empty));
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "Fillcountrycd2", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
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
                    txtEmpName.Text = Convert.ToString(dt.Rows[0]["kycEmpName"]);
                    txtEmpCode.Text = Convert.ToString(dt.Rows[0]["kycEmpCode"]);
                    txtEmpDesignation.Text = Convert.ToString(dt.Rows[0]["kycEmpDesi"]);
                    txtEmpBranch.Text = Convert.ToString(dt.Rows[0]["kycEmpBranch"]);
                    //txtInsName.Text = Convert.ToString(dt.Rows[0]["kycInstName"]);
                    //txtInsCode.Text = Convert.ToString(dt.Rows[0]["kycInstCode"]);
                    txtDateKYCver.Text = Convert.ToString(dt.Rows[0]["kycVerDate"]);

                    txtEmpName.Enabled = false;
                    txtEmpCode.Enabled = false;
                    txtEmpDesignation.Enabled = false;
                    txtEmpBranch.Enabled = false;
                    txtInsName.Enabled = false;
                    txtInsCode.Enabled = false;
                    txtDateKYCver.Enabled = false;


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

        #region METHOD "disablecntrl"
        protected void disablecntrl()
        {
            try
            {
                txtKYCNumber.Enabled = false;
                cboTitle.Enabled = false;
                txtGivenName.Enabled = false;
                txtMiddleName.Enabled = false;
                txtLastName.Enabled = false;
                cboTitle1.Enabled = false;
                txtGivenName1.Enabled = false;
                txtMiddleName1.Enabled = false;
                txtLastName1.Enabled = false;
                rbtFS.Enabled = false;
                cboTitle2.Enabled = false;
                txtGivenName2.Enabled = false;
                txtMiddleName2.Enabled = false;
                txtLastName2.Enabled = false;
                cboTitle3.Enabled = false;
                txtGivenName3.Enabled = false;
                txtMiddleName3.Enabled = false;
                txtLastName3.Enabled = false;
                txtDOB.Enabled = false;
                cboGender.Enabled = false;

                ddlIsoCountryCodeOthr.Enabled = false;
                ddlIsoCountryCode2.Enabled = false;
                txtIDResTax.Enabled = false;
                txtDOBRes.Enabled = false;
                ddlIsoCountry.Enabled = false;
                ddlProofIdentity.Enabled = false;
                txtPassNo.Enabled = false;
                txtPassNoAdd.Enabled = false;
                txtPassExpDate.Enabled = false;
                txtPassExpDateAdd.Enabled = false;
                ddlProofOfAddress.Enabled = false;
                txtAddressLine1.Enabled = false;
                txtAddressLine2.Enabled = false;
                txtAddressLine3.Enabled = false;
                txtCity.Enabled = false;
                ddlDistrict.Enabled = false;
                ddlPinCode.Enabled = false;
                ddlState.Enabled = false;
                ddlCountryCode.Enabled = false;
                txtLocAddLine1.Enabled = false;
                txtLocAddLine2.Enabled = false;
                txtLocAddLine3.Enabled = false;
                ddlDistrict1.Enabled = false;
                ddlPinCode1.Enabled = false;
                ddlState1.Enabled = false;
                ddlCountryCode1.Enabled = false;

                txtCity1.Enabled = false;

                txtTelOff.Enabled = false;
                txtTelRes.Enabled = false;
                txtMobile.Enabled = false;
                txtFax1.Enabled = false;
                txtTelOff2.Enabled = false;
                txtTelRes2.Enabled = false;
                txtFax2.Enabled = false;
                txtMobile2.Enabled = false;
                txtemail.Enabled = false;
                txtRemarks.Enabled = false;
                txtPlace.Enabled = false;
                txtDate.Enabled = false;
                txtEmpName.Enabled = false;
                txtEmpCode.Enabled = false;
                txtEmpDesignation.Enabled = false;
                txtEmpBranch.Enabled = false;
                txtInsName.Enabled = false;

                txtInsCode.Enabled = false;
                txtDateKYCver.Enabled = false;
                ddlDocReceived.SelectedValue = Convert.ToString(dt.Rows[0]["kycCertDoc"]);

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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "disablecntrl", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region DROPDOWN 'ChkUpdPersonal' SELECTEDINDEXCHANGED EVENT
        protected void ChkUpdPersonal_Checked(object sender, EventArgs e)
        {
            try
            {
                cboTitle1.Enabled = true;
                txtGivenName1.Enabled = true;
                txtMiddleName1.Enabled = true;
                txtLastName1.Enabled = true;
                rbtFS.Enabled = true;
                cboTitle2.Enabled = true;
                txtGivenName2.Enabled = true;
                txtMiddleName2.Enabled = true;
                txtLastName2.Enabled = true;
                cboTitle3.Enabled = true;
                txtGivenName3.Enabled = true;
                txtMiddleName3.Enabled = true;
                txtLastName3.Enabled = true;
                txtDOB.Enabled = true;
                cboGender.Enabled = true;

                ddlIsoCountryCode2.Enabled = true;
                txtIDResTax.Enabled = true;
                txtDOBRes.Enabled = true;
                ddlIsoCountry.Enabled = true;
                ChkUpdPersonal.Enabled = false;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ChkUpdPersonal_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region DROPDOWN 'ChkUpdID' SELECTEDINDEXCHANGED EVENT
        protected void ChkUpdID_Checked(object sender, EventArgs e)
        {
            try
            {
                ddlProofIdentity.Enabled = true;
                txtPassNo.Enabled = true;
                txtPassExpDate.Enabled = true;
                txtPassExpDateAdd.Enabled = true;
                ChkUpdID.Enabled = false;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ChkUpdID_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
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
                ddlDistrict.Enabled = true;
                ddlPinCode.Enabled = true;
                ddlState.Enabled = true;
                ddlCountryCode.Enabled = true;

                txtLocAddLine1.Enabled = true;
                txtLocAddLine2.Enabled = true;
                txtLocAddLine3.Enabled = true;
                ddlDistrict1.Enabled = true;
                ddlPinCode1.Enabled = true;
                ddlState1.Enabled = true;
                ddlCountryCode1.Enabled = true;


                txtCity1.Enabled = true;


                ChkUpdAddr.Enabled = false;

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

        #region DROPDOWN 'ChkUpdContact' SELECTEDINDEXCHANGED EVENT
        protected void ChkUpdContact_Checked(object sender, EventArgs e)
        {
            try
            {
                txtTelOff.Enabled = true;
                txtTelRes.Enabled = true;
                txtMobile.Enabled = true;
                txtFax1.Enabled = true;

                txtTelOff2.Enabled = true;
                txtTelRes2.Enabled = true;
                txtFax2.Enabled = true;
                txtMobile2.Enabled = true;
                txtemail.Enabled = true;

                ChkUpdContact.Enabled = false;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ChkUpdContact_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        #endregion

        #region DROPDOWN 'ChkUpdRelated' SELECTEDINDEXCHANGED EVENT
        protected void ChkUpdRelated_Checked(object sender, EventArgs e)
        {
            try
            {
                ChkUpdRelated.Enabled = false;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ChkUpdRelated_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region DROPDOWN 'ChkUpdRemark' SELECTEDINDEXCHANGED EVENT
        protected void ChkUpdRemark_Checked(object sender, EventArgs e)
        {
            try
            {
                txtRemarks.Enabled = true;
                ChkUpdRemark.Enabled = false;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ChkUpdRemark_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region DROPDOWN 'ChkUpdName' SELECTEDINDEXCHANGED EVENT
        protected void ChkUpdName_Checked(object sender, EventArgs e)
        {
            try
            {
                cboTitle.Enabled = true;
                txtGivenName.Enabled = true;
                txtMiddleName.Enabled = true;
                txtLastName.Enabled = true;

                ChkUpdName.Enabled = false;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ChkUpdName_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region DROPDOWN 'ChkUpdControlPrsn_Checked' SELECTEDINDEXCHANGED EVENT
        protected void ChkUpdControlPrsn_Checked(object sender, EventArgs e)
        {
            try
            {
                txtPlace.Enabled = true;
                txtDate.Enabled = true;

                txtEmpName.Enabled = true;
                txtEmpCode.Enabled = true;
                txtEmpDesignation.Enabled = true;
                txtEmpBranch.Enabled = true;
                txtInsName.Enabled = true;

                txtInsCode.Enabled = true;
                txtDateKYCver.Enabled = true;
                //chkCertifyCopy.Checked = true;
                ChkUpdKYCVrfy.Enabled = false;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ChkUpdControlPrsn_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region SaveUpdateFlag
        protected void SaveUpdateFlag()
        {
            try
            {
                htParam.Clear();
                //objds.Clear();
                htParam.Add("@RegRefNo", txtRefNumber.Text.ToString());

                htParam.Add("@uniqueID", obj.ToString());
                htParam.Add("@CreatedBy", strUserId.ToString());

                if (ChkUpdName.Checked == true)
                {
                    htParam.Add("@UpdtAppName", "01");
                }
                else
                {
                    htParam.Add("@UpdtAppName", "02");
                }

                if (ChkUpdPersonal.Checked == true)
                {
                    htParam.Add("@UpdtPersonalDtl", "01");
                }
                else
                {
                    htParam.Add("@UpdtPersonalDtl", "02");
                }


                if (ChkUpdID.Checked == true)
                {
                    htParam.Add("@UpdtIDDtls", "01");
                }
                else
                {
                    htParam.Add("@UpdtIDDtls", "02");
                }

                if (ChkUpdAddr.Checked == true)
                {
                    htParam.Add("@UpdtAddrDtls", "01");
                }
                else
                {
                    htParam.Add("@UpdtAddrDtls", "02");
                }

                if (ChkUpdContact.Checked == true)
                {
                    htParam.Add("@UpdtContacDtls", "01");
                }
                else
                {
                    htParam.Add("@UpdtContacDtls", "02");
                }
                if (ChkUpdRelated.Checked == true)
                {
                    htParam.Add("@UpdtRlrDtls", "01");
                }
                else
                {
                    htParam.Add("@UpdtRlrDtls", "02");
                }
                if (ChkUpdRemark.Checked == true)
                {
                    htParam.Add("@UpdtRemark", "01");
                }
                else
                {
                    htParam.Add("@UpdtRemark", "02");
                }
                if (ChkUpdKYCVrfy.Checked == true)
                {
                    htParam.Add("@UpdtVerification", "01");
                }
                else
                {
                    htParam.Add("@UpdtVerification", "02");
                }

                //if (.Checked == true)
                //{
                //    htParam.Add("@UpdtControlPerson", "01");
                //}
                //else
                //{
                htParam.Add("@UpdtControlPerson", "02");
                //}
                //if (.Checked == true)
                //{
                //    htParam.Add("@UpdtControlPerson", "01");
                //}
                //else
                //{
                htParam.Add("@UpdtImage", "02");
                //}

                objDAL = new DataAccessLayer("CKYCConnectionString");
                objDAL.ExecuteNonQuery("prc_InsUpdateFlagedtls", htParam);
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "SaveUpdateFlag", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region 'btnKYCUpdate_Click' Event
        protected void btnKYCUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string Res;

                Res = objVal.Validation(chkNormal, chkSimplified, Chksmall, cboTitle, txtGivenName, txtLastName, rbtFS, cboTitle2, txtGivenName2, txtLastName2, cboTitle3, txtGivenName3, txtLastName3, txtDOB, cboGender,
                    chkTick, ddlIsoCountryCode2, txtIDResTax, txtDOBRes, ddlIsoCountry, ddlProofIdentity,
                    txtPassNo, txtPassExpDate, chkPerAddress, ddlProofOfAddress, txtAddressLine1, txtCity, ddlPinCode, chkLocalAddress, txtLocAddLine1,
                    txtCity1, ddlPinCode1, chkAppDeclare1, txtDate, txtPlace,
                    txtDateKYCver, txtEmpName, txtEmpCode, txtEmpDesignation, txtEmpBranch, txtInsName, txtInsCode, ddlIsoCountryCodeOthr, ddlIsoCountry, txtPassOthr, txtPanNo);

                if (Res.Equals(""))
                {
                    if (txtDOB.Text != "")
                    {
                        string date;
                        date = DateTime.Today.ToString("dd\\/MM\\/yyyy");
                        DateTime date1, date2;
                        date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        date2 = DateTime.ParseExact(txtDOB.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        if (date1 < date2)
                        {
                            //if (Convert.ToDateTime(date) < Convert.ToDateTime(txtDOB.Text))
                            //{
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You cannot select future date')", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('You cannot select future date')", true);
                            return;
                        }
                    }

                    #region relatedpersonDSvalidation

                    dt = new DataTable();
                    dt = (DataTable)Session["dsRel"];

                    if (chkAddRel.Checked == true)
                    {

                        if (gvMemDtls.Rows.Count <= 0)
                        {
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Add atleast One Relative Details')", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please Add atleast One Relative Details')", true);
                            //chkAddRel.Focus();
                            chkAddRel.Checked = false;
                            return;
                        }

                        if (dt.Rows.Count == 0)
                        {
                            if ((dt.Rows[0]["RelationType"]).ToString() == "" && (dt.Rows[0]["PREFIX"]).ToString() == "" && (dt.Rows[0]["FNAME"]).ToString() == "" && (dt.Rows[0]["LNAME"]).ToString() == ""
                               && (dt.Rows[0]["fs_flag"]).ToString() == "" && (dt.Rows[0]["FATHER_PREFIX"]).ToString() == "" && (dt.Rows[0]["FATHER_FNAME"]).ToString() == "" && (dt.Rows[0]["FATHER_LNAME"]).ToString() == ""
                                && (dt.Rows[0]["MOTHER_PREFIX"]).ToString() == "" && (dt.Rows[0]["MOTHER_FNAME"]).ToString() == "" && (dt.Rows[0]["MOTHER_LNAME"]).ToString() == "" && (dt.Rows[0]["DOB"]).ToString() == "" &&
                                (dt.Rows[0]["GENDER"]).ToString() == "" && (dt.Rows[0]["MARITAL_STATUS"]).ToString() == "" && (dt.Rows[0]["CITIZENSHIP"]).ToString() == "" && (dt.Rows[0]["RESI_STATUS"]).ToString() == "")
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Add Relative Details')", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please Add Relative Details')", true);
                            //chkAddRel.Focus();
                            return;
                        }
                    }
                    #endregion

                    #region cndkycdetails


                    htParam.Clear();
                    if (cbNew.Checked == true)
                    {
                        htParam.Add("@AppType", "01");
                    }
                    else if (cbUpdate.Checked == true)
                    {
                        htParam.Add("@AppType", "03");
                    }

                    if (chkNormal.Checked == true)
                    {
                        htParam.Add("@AccType", "01");
                    }
                    else if (chkSimplified.Checked == true)
                    {
                        htParam.Add("@AccType", "02");
                    }
                    else if (Chksmall.Checked == true)
                    {
                        htParam.Add("@AccType", "03");
                    }
                    htParam.Add("@RegRefNo", txtRefNumber.Text.ToString());
                    htParam.Add("@KYC_NO", txtKYCNumber.Text.ToString());
                    htParam.Add("@PREFIX", cboTitle.SelectedValue);
                    htParam.Add("@FNAME", txtGivenName.Text.Trim());
                    htParam.Add("@MNAME", txtMiddleName.Text.Trim());
                    htParam.Add("@LNAME", txtLastName.Text.Trim());
                    htParam.Add("@MAID_PREFIX", cboTitle1.SelectedValue);
                    htParam.Add("@MAID_FNAME", txtGivenName1.Text.Trim());
                    htParam.Add("@MAID_MNAME", txtMiddleName1.Text.Trim());
                    htParam.Add("@MAID_LNAME", txtLastName1.Text.Trim());

                    if (rbtFS.SelectedValue == "F")
                    {
                        htParam.Add("@fs_flag", "01");
                    }
                    else
                    {
                        htParam.Add("@fs_flag", "02");
                    }

                    htParam.Add("@FATHER_PREFIX", cboTitle2.SelectedValue);
                    htParam.Add("@FATHER_FNAME", txtGivenName2.Text.Trim());
                    htParam.Add("@FATHER_MNAME", txtMiddleName2.Text.Trim());
                    htParam.Add("@FATHER_LNAME", txtLastName2.Text.Trim());
                    htParam.Add("@MOTHER_PREFIX", cboTitle3.SelectedValue);
                    htParam.Add("@MOTHER_FNAME", txtGivenName3.Text);
                    htParam.Add("@MOTHER_MNAME", txtMiddleName2.Text);
                    htParam.Add("@MOTHER_LNAME", txtLastName3.Text);
                    htParam.Add("@DOB", txtDOB.Text);
                    htParam.Add("@GENDER", cboGender.SelectedValue);
                    htParam.Add("@MARITAL_STATUS", "");
                    htParam.Add("@CITIZENSHIP", "");
                    htParam.Add("@RESI_STATUS", "");
                    htParam.Add("@OccupationType", "");

                    if (chkTick.Checked == true)
                    {
                        htParam.Add("@JurisdictionFlag", "01");
                    }
                    else
                    {
                        htParam.Add("@JurisdictionFlag", "02");
                    }

                    htParam.Add("@TINIssuingCountry", ddlIsoCountryCode2.SelectedValue.Trim());
                    htParam.Add("@TIN", txtIDResTax.Text.Trim());
                    htParam.Add("@JurisdictionBirthPlace", txtDOBRes.Text.Trim());
                    htParam.Add("@JurisdictionCountryofBirth", ddlIsoCountry.SelectedValue.Trim());

                    htParam.Add("@IdType", ddlProofIdentity.SelectedValue);
                    if (ddlProofIdentity.SelectedIndex == 1)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", txtPassExpDate.Text.Trim());
                        htParam.Add("@IdName", System.DBNull.Value);

                    }
                    else if (ddlProofIdentity.SelectedIndex == 2)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", System.DBNull.Value);

                    }
                    else if (ddlProofIdentity.SelectedIndex == 3)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 4)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", txtPassExpDate.Text.Trim());
                        htParam.Add("@IdName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 5)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 6)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 7)
                    {
                        htParam.Add("@IdNumber", txtPassOthr.Text.Trim());
                        htParam.Add("@IdExpDate", System.DBNull.Value);//
                        htParam.Add("@IdName", txtPassNo.Text.Trim());


                    }
                    else if (ddlProofIdentity.SelectedIndex == 8 || ddlProofIdentity.SelectedIndex == 9)
                    {
                        htParam.Add("@IdNumber", txtPassNo.Text.Trim());
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", System.DBNull.Value);
                    }
                    else
                    {
                        htParam.Add("@IdNumber", System.DBNull.Value);
                        htParam.Add("@IdExpDate", System.DBNull.Value);
                        htParam.Add("@IdName", System.DBNull.Value);
                    }


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

                    htParam.Add("@AddIdType", ddlProofOfAddress.SelectedValue);
                    if (chkPerAddress.Checked == true)
                    {
                        if (ddlProofOfAddress.SelectedIndex == 1)
                        {
                            htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                            htParam.Add("@AddIdExpDate", txtPassExpDateAdd.Text.Trim());
                            htParam.Add("@AddIdName", System.DBNull.Value);

                        }
                        else if (ddlProofOfAddress.SelectedIndex == 2)
                        {
                            htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                            htParam.Add("@AddIdExpDate", txtPassExpDateAdd.Text.Trim());
                            htParam.Add("@AddIdName", System.DBNull.Value);
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 3)
                        {
                            htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                            htParam.Add("@AddIdExpDate", System.DBNull.Value);
                            htParam.Add("@AddIdName", System.DBNull.Value);
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 4)
                        {
                            htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                            htParam.Add("@AddIdExpDate", System.DBNull.Value);
                            htParam.Add("@AddIdName", System.DBNull.Value);

                        }
                        else if (ddlProofOfAddress.SelectedIndex == 5)
                        {
                            htParam.Add("@AddIdNumber", txtPassNoAdd.Text.Trim());
                            htParam.Add("@AddIdExpDate", System.DBNull.Value);
                            htParam.Add("@AddIdName", System.DBNull.Value);
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 6)
                        {
                            htParam.Add("@AddIdNumber", txtPassOthrAdd.Text.Trim());
                            htParam.Add("@AddIdExpDate", System.DBNull.Value);//
                            htParam.Add("@AddIdName", txtPassNoAdd.Text.Trim());

                        }
                        else
                        {
                            htParam.Add("@AddIdNumber", System.DBNull.Value);
                            htParam.Add("@AddIdExpDate", System.DBNull.Value);
                            htParam.Add("@AddIdName", System.DBNull.Value);
                        }
                    }
                    else
                    {
                        htParam.Add("@AddIdNumber", System.DBNull.Value);
                        htParam.Add("@AddIdExpDate", System.DBNull.Value);
                        htParam.Add("@AddIdName", System.DBNull.Value);
                    }

                    if (chkLocalAddress.Checked == true)
                    {
                        htParam.Add("@CnctType2", "M1");
                        htParam.Add("@SameAsPer", "01");//by meena 19/05/2017
                        htParam.Add("@CUR_ADDLINE1", txtLocAddLine1.Text);
                        htParam.Add("@CUR_ADDLINE2", txtLocAddLine2.Text);
                        htParam.Add("@CUR_ADDLINE3", txtLocAddLine3.Text);
                        htParam.Add("@CUR_CITY", txtCity1.Text.Trim());
                        htParam.Add("@CUR_DISTRICT", ddlDistrict1.SelectedValue);
                        htParam.Add("@CUR_PIN", ddlPinCode1.SelectedValue);
                        htParam.Add("@CUR_STATECODE", ddlState1.SelectedValue);
                        htParam.Add("@CUR_COUNTRY_CODE", ddlCountryCode1.SelectedValue);
                    }
                    else
                    {
                        htParam.Add("@CnctType2", "");
                        htParam.Add("@SameAsPer", "02");//by meena 19/05/2017
                        htParam.Add("@CUR_ADDLINE1", System.DBNull.Value);
                        htParam.Add("@CUR_ADDLINE2", System.DBNull.Value);
                        htParam.Add("@CUR_ADDLINE3", System.DBNull.Value);
                        htParam.Add("@CUR_CITY", System.DBNull.Value);
                        htParam.Add("@CUR_DISTRICT", System.DBNull.Value);
                        htParam.Add("@CUR_PIN", System.DBNull.Value);
                        htParam.Add("@CUR_STATECODE", System.DBNull.Value);
                        htParam.Add("@CUR_COUNTRY_CODE", System.DBNull.Value);
                    }


                    htParam.Add("@CnctType3", "");
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
                    htParam.Add("@std_fax", txtFax1.Text.Trim());

                    htParam.Add("@OFF_TELE", txtTelOff.Text);
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
                    htParam.Add("@kycInstName", txtInsName.Text.Trim());
                    htParam.Add("@kycInstCode", txtInsCode.Text.Trim());
                    htParam.Add("@MODIFIEDBY", strUserId.ToString());
                    htParam.Add("@Usages", "W");
                    htParam.Add("@Flag", Request.QueryString["Status"].ToString());

                    objDAL = new DataAccessLayer("CKYCConnectionString");
                    DataTable objdt = new DataTable();
                    objdt = objDAL.GetDataTable("prc_updatekycdtls", htParam);

                    if (objdt.Rows.Count > 0)
                    {
                        strRefNo = objdt.Rows[0]["RegRefNo"].ToString();
                        string message = objdt.Rows[0]["MESSAGE"].ToString();
                    }
                    #endregion

                    #region Save Members Details
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                htParam.Clear();
                                if (chkAddRel.Checked == true)
                                {
                                    htParam.Add("@RelAddDelFlag", "01");
                                }
                                else
                                {
                                    htParam.Add("@RelAddDelFlag", "02");
                                }


                                htParam.Add("@RelTYPE", dt.Rows[i]["RelationType"]);
                                htParam.Add("@RelCKYCNO", dt.Rows[i]["RelatedPrsnKYCNo"]);
                                htParam.Add("@RelPrefix", dt.Rows[i]["PrefixRel"]);
                                htParam.Add("@RelFName", dt.Rows[i]["FNameRel"]);
                                htParam.Add("@RelMName", dt.Rows[i]["MNameRel"]);
                                htParam.Add("@RelLName", dt.Rows[i]["LNameRel"]);
                                htParam.Add("@RelMaidPrefix", dt.Rows[i]["MaidPrefixRel"]);
                                htParam.Add("@RelMaidFNmae", dt.Rows[i]["MaidFNameRel"]);
                                htParam.Add("@RelMaidMNmae", dt.Rows[i]["MaidMNameRel"]);
                                htParam.Add("@RelMaidLNmae", dt.Rows[i]["MaidLNameRel"]);
                                htParam.Add("@FlagFatherorSpouse", dt.Rows[i]["FSFlagRel"]);
                                htParam.Add("@RelFatherPrefix", dt.Rows[i]["FatherPrefixRel"]);
                                htParam.Add("@RelFatheFName", dt.Rows[i]["FatherFNameRel"]);
                                htParam.Add("@RelFatheMName", dt.Rows[i]["FatherMNameRel"]);
                                htParam.Add("@RelFatheLName", dt.Rows[i]["FatherLNameRel"]);
                                htParam.Add("@RelMotherPrefix", dt.Rows[i]["MotherPrefixRel"]);
                                htParam.Add("@RelMotherFName", dt.Rows[i]["MotherFNameRel"]);
                                htParam.Add("@RelMotherMname", dt.Rows[i]["MotherMNameRel"]);
                                htParam.Add("@RelMotherLName", dt.Rows[i]["MotherLNameRel"]);
                                htParam.Add("@RelDOB", dt.Rows[i]["DOBRel"]);
                                htParam.Add("@RelGender", dt.Rows[i]["GenderRel"]);
                                htParam.Add("@RelMaritalStatus", dt.Rows[i]["MaritalStatusRel"]);

                                htParam.Add("@RelResistatus", dt.Rows[i]["ResiStatusRel"]);
                                htParam.Add("@RelOccuType", dt.Rows[i]["OccuTypeRel"]);
                                htParam.Add("@RelCitizenship", dt.Rows[i]["CitizenshipRel"]);


                                htParam.Add("@RelJurisdictionFlag", dt.Rows[i]["ResForTaxFlagRel"]);
                                htParam.Add("@RelISOCountryCode", dt.Rows[i]["ISOCountryCodeRel"]);
                                htParam.Add("@RelTIN", dt.Rows[i]["TaxIDNumberRel"]);
                                htParam.Add("@RelBirthCity", dt.Rows[i]["BirthCityRel"]);
                                htParam.Add("@RelISOBirthPlace", dt.Rows[i]["ISOBirthPlaceCodeRel"]);

                                htParam.Add("@IDType", dt.Rows[i]["IdType"]);
                                htParam.Add("@IDNum", dt.Rows[i]["IdNumber"]);
                                htParam.Add("@IDExpDate", dt.Rows[i]["IdExpDate"]);
                                htParam.Add("@IDName", dt.Rows[i]["IdName"]);

                                htParam.Add("@CnctType1", "P1");
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
                                htParam.Add("@PER_IDNUMBAER", dt.Rows[i]["AddIdNumberRel"]);
                                htParam.Add("@PER_IDEXPDT", dt.Rows[i]["AddIdExpDateRel"]);
                                htParam.Add("@PER_IDOtherDocName", dt.Rows[i]["AddIdNameRel"]);

                                htParam.Add("@RelDecDate", dt.Rows[i]["DecDateRel"]);
                                htParam.Add("@RelDecPlace", dt.Rows[i]["DecPlaceRel"]);
                                htParam.Add("@RelkycEmpName", dt.Rows[i]["kycEmpNameRel"]);
                                htParam.Add("@RelkycEmpCode", dt.Rows[i]["kycEmpCodeRel"]);
                                htParam.Add("@RelkycEmpBranch", dt.Rows[i]["kycEmpBranchRel"]);
                                htParam.Add("@RelkycEmpDesi", dt.Rows[i]["kycEmpDesiRel"]);
                                htParam.Add("@RelkycVerDate", dt.Rows[i]["kycVerDateRel"]);
                                htParam.Add("@RelkycCertDoc", "01");

                                htParam.Add("@RelkycInstName", dt.Rows[i]["kycInstNameRel"]);
                                htParam.Add("@RelkycInstCode", dt.Rows[i]["kycInstCodeRel"]);
                                htParam.Add("@UpdateBy", strUserId.ToString());

                                if (Request.QueryString["Status"].ToString() == "Mod")
                                {
                                    htParam.Add("@RegRefNo", txtRefNumber.Text.ToString());
                                    htParam.Add("@RelRefNo", dt.Rows[i]["RelRefNo"]);
                                    objDAL.ExecuteNonQuery("prc_updKycRelPrsnDtls", htParam);
                                }

                                Session["dsRel"] = null;
                            }
                        }
                    }
                    #endregion
                    SaveUpdateFlag();
                    if (gvMemDtls.Visible == true)
                    {

                        foreach (GridViewRow row in gvMemDtls.Rows)
                        {
                            LinkButton lnkdelete = (LinkButton)row.FindControl("lnkdelete");
                            lnkdelete.Enabled = false;
                        }
                    }

                    htParam.Clear();
                    htParam.Add("@UserId", HttpContext.Current.Session["UserID"].ToString().Trim());
                    htParam.Add("@RefNo", Request.QueryString["refno"].ToString().Trim());
                    objDAL.ExecuteNonQuery("prc_KycUpdStatusMvnt", htParam);


                    hdnUpdate.Value = "Candidate updated successfully";
                    msg = hdnUpdate.Value + "</br></br>Reference No: " + strRefNo.ToString().Trim() + "<br/>Candidate Name: "
                         + cboTitle.SelectedValue + " " + txtGivenName.Text + " " + txtLastName.Text;

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "popup('" + msg + "');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);

                    btnKYCUpdate.Enabled = false;
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Res + "')", true);
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "btnKYCUpdate_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region updflag
        protected void updflag()
        {
            try
            {
                if ((Request.QueryString["Status"].ToString() == "Reg") || (Request.QueryString["Status"].ToString() == "Mod") || (Request.QueryString["Status"].ToString() == "PMod"))
                {
                    ChkUpdName.Visible = false;
                    ChkUpdPersonal.Visible = false;
                    ChkUpdID.Visible = false;
                    ChkUpdAddr.Visible = false;
                    ChkUpdContact.Visible = false;
                    ChkUpdRelated.Visible = false;
                    ChkUpdRemark.Visible = false;
                    ChkUpdKYCVrfy.Visible = false;
                }
                else if (Request.QueryString["Status"].ToString() == "KMod")
                {
                    ChkUpdName.Visible = true;
                    ChkUpdPersonal.Visible = true;
                    ChkUpdID.Visible = true;
                    ChkUpdAddr.Visible = true;
                    ChkUpdContact.Visible = true;
                    ChkUpdRelated.Visible = true;
                    ChkUpdRemark.Visible = true;
                    ChkUpdKYCVrfy.Visible = true;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "updflag", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
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
                // Commented By Pratik
                //if (chkPerAddress.Checked == true)
                //{
                //    if (ddlCitizenship.SelectedIndex == 0)
                //    {
                //        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Citizenship')", true);
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select Citizenship')", true);
                //        //ddlCitizenship.Focus();
                //        return;
                //    }
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "chkPerAddress_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region 'btnAdd_Click' Event
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "btnAdd_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
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
                    if (chkAddRel.Checked == true && ChkUpdRelated.Checked == false)
                    {
                        chkAddRel.Checked = false;
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Check CONTACT DETAILS OF RELATED PERSON(All communication will be sent on provided MobileNo./Email-ID) checkbox')", true);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please Check CONTACT DETAILS OF RELATED PERSON(All communication will be sent on provided MobileNo./Email-ID) checkbox')", true);
                        return;
                    }
                }
                if (chkAddRel.Checked == true)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "OpenRelatedPersonPage();", true);
                    lnkViewRel.Enabled = true;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "chkAddRel_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }

        }
        #endregion

        #region chkchkDelRel_Checked'
        protected void chkDelRel_Checked(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Status"] == "KMod")
                {
                    if (chkDelRel.Checked == true)
                    {
                        chkDelRel.Checked = false;
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Check CONTACT DETAILS OF RELATED PERSON(All communication will be sent on provided MobileNo./Email-ID) checkbox')", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please Check CONTACT DETAILS OF RELATED PERSON(All communication will be sent on provided MobileNo./Email-ID) checkbox')", true);
                        return;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "chkchkDelRel_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }

        }
        #endregion

        #region relatieDetailsDsValidation
        protected void relatieDetailsDsValidation()
        {
        }
        #endregion

        #region btnAdd Event
        protected void btnAdd(object sender, EventArgs e)
        {

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

                Label lblMemMrtVal = (Label)e.Row.FindControl("lblMemMrtVal") as Label;
                Label lblMemCizVal = (Label)e.Row.FindControl("lblMemCizVal") as Label;
                Label lblMemResiVal = (Label)e.Row.FindControl("lblMemResiVal") as Label;
                Label lblMemOccVal = (Label)e.Row.FindControl("lblMemOccVal") as Label;
                LinkButton lnkView = (LinkButton)e.Row.FindControl("lnkView") as LinkButton;

                if (Request.QueryString["Status"].ToString() == "Reg")
                {
                    lnkView.Visible = false;
                }
                else
                {
                    lnkView.Visible = true;
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

                    if (Request.QueryString["Status"].ToString() == "Mod")
                    {
                        DataTable DTUpd = (DataTable)ViewState["DT"];
                        DT.Merge(DTUpd, true, MissingSchemaAction.Ignore);
                        DT.AcceptChanges();
                    }
                    if (DT.Rows.Count > 0)
                    {

                        if (Request.QueryString["Status"].ToString() == "Reg")
                        {
                            gvMemDtls.DataSource = DT;
                            gvMemDtls.DataBind();
                            gvMemDtls.Visible = true;
                            lblRelRecordShow.Visible = false;
                            chkAddRel.Enabled = false;
                            gvMemDtls.Columns[1].Visible = false;
                        }
                        else
                        {
                            gvMemDtls.DataSource = DT;
                            gvMemDtls.DataBind();
                            gvMemDtls.Visible = true;
                            lblRelRecordShow.Visible = false;
                            chkAddRel.Enabled = false;
                        }
                    }
                }
                else
                {
                    lblRelRecordShow.Visible = true;
                    lblRelRecordShow.ForeColor = System.Drawing.Color.Red;
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
                DataTable dt = (DataTable)ViewState["DT"];


                DataRow dr = dt.Rows[i];
                string RelRefnNo = Convert.ToString(dr[1]);

                dt.Rows[i].Delete();
                dt.AcceptChanges();

                if (Request.QueryString["Status"] == "Mod")
                {
                    string refno = Request.QueryString["refno"].ToString().Trim();
                    htParam.Clear();
                    //objds.Clear();

                    htParam.Add("@RegRefNo", refno);
                    htParam.Add("@RelRefNo", RelRefnNo);
                    htParam.Add("@ActionFlag", Request.QueryString["Status"].ToString().Trim());
                    htParam.Add("@UserID", strUserId.ToString());


                    // objds = objDAL.GetDataSet("prc_DelKycRelatedPrsnDtls", htParam, "CKYCConnectionString");
                }


                if (dt.Rows.Count > 0)
                {
                    gvMemDtls.DataSource = dt;
                    gvMemDtls.DataBind();

                    //dsRel.Tables.Add(dt);
                    ViewState["DT"] = dt;
                }
                else
                {
                    gvMemDtls.DataSource = dt;
                    gvMemDtls.DataBind();
                    Session["DsRel"] = null;
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

        #region METHOD "GetRelatedPersonDataForCKYC"
        protected void GetRelatedPersonDataForCKYC()
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                htParam.Clear();
                htParam.Add("@RegRefNo", Request.QueryString["refno"].ToString());

                htParam.Add("@ActionFlag", Request.QueryString["Status"].ToString());

                htParam.Add("@UserType", "");
                htParam.Add("@RelRefNo", "");

                dt = objDAL.GetDataTable("Prc_GetRelatedPersonDataForCKYC", htParam);

                if (dt.Rows.Count > 0)
                {
                    gvMemDtls.DataSource = dt;
                    gvMemDtls.DataBind();
                    Session["dsRel"] = dt;
                    ViewState["DT"] = dt;
                    gvMemDtls.Visible = true;
                    divchkDelRel.Visible = true;
                    chkAddRel.Checked = true;
                }
                else
                {

                    lblRelRecordShow.Visible = true;
                    lblRelRecordShow.ForeColor = System.Drawing.Color.Red;
                    divchkDelRel.Visible = false;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "GetRelatedPersonDataForCKYC", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
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

        #region ShowNoResultFound
        private void ShowNoResultFound(DataTable source, GridView gv)
        {
            try
            {
                source.Rows.Add(source.NewRow());
                gv.DataSource = source;
                gv.DataBind();
                int columnsCount = gv.Columns.Count;
                gv.Rows[0].Cells.Clear();
                gv.Rows[0].Cells.Add(new TableCell());
                gv.Rows[0].Cells[0].ColumnSpan = columnsCount;
                gv.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Left;
                gv.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
                gv.Rows[0].Cells[0].Font.Bold = true;
                gv.Rows[0].Cells[0].Text = "No Records Found!";
                source.Rows.Clear();
                gv.DataSource = null;
                gv.DataBind();
                gvMemDtls.Visible = false;
                // divMember.Visible = false;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ShowNoResultFound", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
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
                if (Request.QueryString["Status"].ToString() == "Mod")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenRelatedPersonPageView(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["Status"].ToString() == "PMod")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenPartialRelatedPersonPageView(" + RelRefnNo + "," + refno + ");", true);
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

        #region btnPartialSave
        protected void btnPartialSave_Click(object sender, EventArgs e)
        {
            try
            {
                string Res;
                Res = objVal.PersonalDtlsValidation(
                    chkNormal, chkSimplified, Chksmall, cboTitle, txtGivenName, txtLastName, cboTitle2, rbtFS, txtGivenName2, txtLastName2, cboTitle3, txtGivenName3,
                    txtLastName3, txtDOB, cboGender, ddlIsoCountryCodeOthr, null, "Candidate");

                if (Res.Equals(""))
                {

                    #region relatedpersonDSvalidation
                    dt = new DataTable();
                    dt = (DataTable)Session["dsRel"];


                    if (chkAddRel.Checked == true)
                    {
                        if (dt == null)
                        {
                            chkAddRel.Checked = false;
                            return;
                        }
                    }
                    #endregion
                    #region cnd kyc Partial Details
                    htParam.Clear();

                    if (cbNew.Checked == true)
                    {
                        htParam.Add("@APP_TYPE", "01");
                    }
                    else
                    {
                        htParam.Add("@ACC_TYPE", "02");
                    }

                    if (chkNormal.Checked == true)
                    {
                        htParam.Add("@ACC_TYPE", "01");
                    }
                    else if (chkSimplified.Checked == true)
                    {
                        htParam.Add("@ACC_TYPE", "02");
                    }
                    else if (Chksmall.Checked == true)
                    {
                        htParam.Add("@ACC_TYPE", "03");
                    }
                    htParam.Add("@Prefix", cboTitle.SelectedValue);
                    htParam.Add("@FName", txtGivenName.Text.Trim());
                    htParam.Add("@MName", txtMiddleName.Text.Trim());
                    htParam.Add("@LName", txtLastName.Text.Trim());
                    htParam.Add("@MAIDEN_PREFIX", cboTitle1.SelectedValue);
                    htParam.Add("@MAIDEN_FNAME", txtGivenName1.Text.Trim());
                    htParam.Add("@MAIDEN_MNAME", txtMiddleName1.Text.Trim());
                    htParam.Add("@MAIDEN_LNAME", txtLastName1.Text.Trim());

                    if (rbtFS.SelectedValue == "F")
                    {
                        htParam.Add("@FATHERSPOUSE_FLAG", "01");
                    }
                    else if (rbtFS.SelectedValue == "S")
                    {
                        htParam.Add("@FATHERSPOUSE_FLAG", "02");
                    }
                    else
                    {
                        htParam.Add("@FATHERSPOUSE_FLAG", System.DBNull.Value);
                    }
                    htParam.Add("@FATHER_PREFIX", cboTitle2.SelectedValue);
                    htParam.Add("@FATHER_FNAME", txtGivenName2.Text.Trim());
                    htParam.Add("@FATHER_MNAME", txtMiddleName2.Text.Trim());
                    htParam.Add("@FATHER_LNAME", txtLastName2.Text.Trim());
                    htParam.Add("@MOTHER_PREFIX", cboTitle3.SelectedValue);
                    htParam.Add("@MOTHER_FNAME", txtGivenName3.Text);
                    htParam.Add("@MOTHER_MNAME", txtMiddleName2.Text);
                    htParam.Add("@MOTHER_LNAME", txtLastName3.Text);
                    htParam.Add("@DOB", txtDOB.Text);
                    htParam.Add("@GENDER", cboGender.SelectedValue);
                    htParam.Add("@MARITAL_STATUS", "");//@
                    htParam.Add("@NATIONALITY", "");
                    htParam.Add("@RESI_STATUS", "");

                    htParam.Add("@OCCUPATION", "");
                    htParam.Add("@SUBOCCUPATION", "");

                    if (chkTick.Checked == true)
                    {
                        htParam.Add("@JurisdictionFlag", "01");
                    }
                    else if (chkTick.Checked == false)
                    {
                        htParam.Add("@JurisdictionFlag", "02");
                    }
                    else
                    {
                        htParam.Add("@JurisdictionFlag", System.DBNull.Value);
                    }
                    htParam.Add("@BIRTH_COUNTRY", ddlIsoCountryCode2.SelectedValue.Trim());
                    htParam.Add("@TAX_NUM", txtIDResTax.Text.Trim());
                    htParam.Add("@BIRTH_PLACE", txtDOBRes.Text.Trim());
                    htParam.Add("@TINIssuingCountry", ddlIsoCountryCode2.SelectedValue);
                    htParam.Add("@IDENT_TYPE_ID1", ddlProofIdentity.SelectedValue);

                    if (ddlProofIdentity.SelectedIndex == 1)
                    {
                        htParam.Add("@IDENT_NUM_ID1", txtPassNo.Text.Trim());
                        htParam.Add("@ID_EXPIRYDATE_ID1", txtPassExpDate.Text.Trim());
                        htParam.Add("@IDENT_NAME_ID1", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 2)
                    {
                        htParam.Add("@IDENT_NUM_ID1", txtPassNo.Text.Trim());
                        htParam.Add("@ID_EXPIRYDATE_ID1", System.DBNull.Value);
                        htParam.Add("@IDENT_NAME_ID1", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 3)
                    {
                        htParam.Add("@IDENT_NUM_ID1", txtPassNo.Text.Trim());
                        htParam.Add("@ID_EXPIRYDATE_ID1", System.DBNull.Value);
                        htParam.Add("@IDENT_NAME_ID1", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 4)
                    {
                        htParam.Add("@IDENT_NUM_ID1", txtPassNo.Text.Trim());
                        htParam.Add("@ID_EXPIRYDATE_ID1", txtPassExpDate.Text.Trim());
                        htParam.Add("@IDENT_NAME_ID1", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 5)
                    {
                        htParam.Add("@IDENT_NUM_ID1", txtPassNo.Text.Trim());
                        htParam.Add("@ID_EXPIRYDATE_ID1", System.DBNull.Value);
                        htParam.Add("@IDENT_NAME_ID1", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 6)
                    {
                        htParam.Add("@IDENT_NUM_ID1", txtPassNo.Text.Trim());
                        htParam.Add("@ID_EXPIRYDATE_ID1", System.DBNull.Value);
                        htParam.Add("@IDENT_NAME_ID1", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 7)
                    {
                        htParam.Add("@IDENT_NUM_ID1", txtPassOthr.Text.Trim());
                        htParam.Add("@ID_EXPIRYDATE_ID1", System.DBNull.Value);
                        htParam.Add("@IDENT_NAME_ID1", txtPassNo.Text.Trim());
                    }
                    else if (ddlProofIdentity.SelectedIndex == 8 || ddlProofIdentity.SelectedIndex == 9)
                    {
                        htParam.Add("@IDENT_NUM_ID1", txtPassNo.Text.Trim());
                        htParam.Add("@ID_EXPIRYDATE_ID1", System.DBNull.Value);
                        htParam.Add("@IDENT_NAME_ID1", System.DBNull.Value);
                    }
                    else
                    {
                        htParam.Add("@IDENT_NUM_ID1", System.DBNull.Value);
                        htParam.Add("@ID_EXPIRYDATE_ID1", System.DBNull.Value);
                        htParam.Add("@IDENT_NAME_ID1", System.DBNull.Value);
                    }

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

                    htParam.Add("@PERM_POA", ddlProofOfAddress.SelectedValue);
                    if (chkPerAddress.Checked == true)
                    {
                        if (ddlProofOfAddress.SelectedIndex == 1)
                        {
                            htParam.Add("@PERM_IDNUMBAER", txtPassNoAdd.Text.Trim());
                            htParam.Add("@PERM_IDEXPDT", txtPassExpDateAdd.Text.Trim());
                            htParam.Add("@PERM_POAOTHERS", System.DBNull.Value);

                        }
                        else if (ddlProofOfAddress.SelectedIndex == 2)
                        {
                            htParam.Add("@PERM_IDNUMBAER", txtPassNoAdd.Text.Trim());
                            htParam.Add("@PERM_IDEXPDT", txtPassExpDateAdd.Text.Trim());
                            htParam.Add("@PERM_POAOTHERS", System.DBNull.Value);
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 3)
                        {
                            htParam.Add("@PERM_IDNUMBAER", txtPassNoAdd.Text.Trim());
                            htParam.Add("@PERM_IDEXPDT", System.DBNull.Value);
                            htParam.Add("@PERM_POAOTHERS", System.DBNull.Value);
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 4)
                        {
                            htParam.Add("@PERM_IDNUMBAER", txtPassNoAdd.Text.Trim());
                            htParam.Add("@PERM_IDEXPDT", System.DBNull.Value);
                            htParam.Add("@PERM_POAOTHERS", System.DBNull.Value);

                        }
                        else if (ddlProofOfAddress.SelectedIndex == 5)
                        {
                            htParam.Add("@PERM_IDNUMBAER", txtPassNoAdd.Text.Trim());
                            htParam.Add("@PERM_IDEXPDT", System.DBNull.Value);
                            htParam.Add("@PERM_POAOTHERS", System.DBNull.Value);
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 6)
                        {
                            htParam.Add("@PERM_IDNUMBAER", txtPassOthrAdd.Text.Trim());
                            htParam.Add("@PERM_IDEXPDT", System.DBNull.Value);//
                            htParam.Add("@PERM_POAOTHERS", txtPassNoAdd.Text.Trim());
                        }
                        else
                        {
                            htParam.Add("@PERM_IDNUMBAER", System.DBNull.Value);
                            htParam.Add("@PERM_IDEXPDT", System.DBNull.Value);
                            htParam.Add("@PERM_POAOTHERS", System.DBNull.Value);
                        }
                    }
                    else
                    {
                        htParam.Add("@PERM_IDNUMBAER", System.DBNull.Value);
                        htParam.Add("@PERM_IDEXPDT", System.DBNull.Value);
                        htParam.Add("@PERM_POAOTHERS", System.DBNull.Value);
                    }

                    if (chkLocalAddress.Checked == true)
                    {
                        htParam.Add("@CnctType2", "M1");
                        htParam.Add("@PERM_CORRES_SAMEFLAG", "01");//by meena 19/05/2017
                        htParam.Add("@CORRES_LINE1", txtLocAddLine1.Text);
                        htParam.Add("@CORRES_LINE2", txtLocAddLine2.Text);
                        htParam.Add("@CORRES_LINE3", txtLocAddLine3.Text);
                        htParam.Add("@CORRES_CITY ", txtCity1.Text.Trim());
                        htParam.Add("@CORRES_DIST", ddlDistrict1.SelectedValue);
                        htParam.Add("@CORRES_PIN", ddlPinCode1.SelectedValue);
                        htParam.Add("@CORRES_STATE", ddlState1.SelectedValue);
                        htParam.Add("@CORRES_COUNTRY", ddlCountryCode1.SelectedValue);
                    }
                    else
                    {
                        htParam.Add("@CnctType2", "");
                        htParam.Add("@PERM_CORRES_SAMEFLAG", "02");//by meena 19/05/2017
                        htParam.Add("@CORRES_LINE1", System.DBNull.Value);
                        htParam.Add("@CORRES_LINE2", System.DBNull.Value);
                        htParam.Add("@CORRES_LINE3", System.DBNull.Value);
                        htParam.Add("@CORRES_CITY", System.DBNull.Value);
                        htParam.Add("@CORRES_DIST", System.DBNull.Value);
                        htParam.Add("@CORRES_PIN", System.DBNull.Value);
                        htParam.Add("@CORRES_STATE", System.DBNull.Value);
                        htParam.Add("@CORRES_COUNTRY", System.DBNull.Value);
                    }


                    htParam.Add("@CnctType3", "");
                    htParam.Add("@JURI_LINE1", System.DBNull.Value);
                    htParam.Add("@JURI_LINE2", System.DBNull.Value);
                    htParam.Add("@JURI_LINE3", System.DBNull.Value);
                    htParam.Add("@JURI_CITY", System.DBNull.Value);
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
                    PSTempRefNo = (objDAL.ExecuteScalar("Prc_InsCkycPartialDtls", htParam)).ToString();

                    #endregion

                    #region Save Members Details

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                htParam.Clear();
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

                    if (gvMemDtls.Visible == true)
                    {

                        foreach (GridViewRow row in gvMemDtls.Rows)
                        {
                            LinkButton lnkdelete = (LinkButton)row.FindControl("lnkdelete");
                            lnkdelete.Enabled = false;
                        }
                    }
                    hdnUpdate.Value = "Partial Data save succesfully.";

                    msg = hdnUpdate.Value + "</br></br>Temporary Reference No: " + PSTempRefNo.ToString().Trim() + "<br/>Candidate Name: "
                         + cboTitle.SelectedValue + " " + txtGivenName.Text + " " + txtLastName.Text + "<br/><br/>";
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

        #region METHOD "FillRequiredDataForPartialSave"
        protected void FillRequiredDataForPartialSave()
        {
            try
            {
                objDAL = new DataAccessLayer("STAGINGConnectionString");
                dt = new DataTable();
                htParam.Clear();
                htParam.Add("@PSTempRefNo", Request.QueryString["refno"].ToString());
                dt = objDAL.GetDataTable("getPartiaSearchData", htParam);

                if (Convert.ToString(dt.Rows[0]["AccType"]) == "01")
                {
                    chkNormal.Checked = true;
                }
                else if (Convert.ToString(dt.Rows[0]["AccType"]) == "02")
                {
                    chkSimplified.Checked = true;
                }
                else if (Convert.ToString(dt.Rows[0]["AccType"]) == "03")
                {
                    Chksmall.Checked = true;
                }
                txtKYCNumber.Text = Convert.ToString(dt.Rows[0]["KYC_NO"]);
                txtRefNumber.Text = Convert.ToString(dt.Rows[0]["PSTempRefNo"]);
                cboTitle.SelectedValue = Convert.ToString(dt.Rows[0]["Prefix"]);
                txtGivenName.Text = Convert.ToString(dt.Rows[0]["FNAME"]);
                txtMiddleName.Text = Convert.ToString(dt.Rows[0]["MNAME"]);
                txtLastName.Text = Convert.ToString(dt.Rows[0]["LNAME"]);
                cboTitle1.SelectedValue = Convert.ToString(dt.Rows[0]["MaidenPrefix"]);
                txtGivenName1.Text = Convert.ToString(dt.Rows[0]["MaidenFName"]);
                txtMiddleName1.Text = Convert.ToString(dt.Rows[0]["MaidenMName"]);
                txtLastName1.Text = Convert.ToString(dt.Rows[0]["MaidenLName"]);

                if (Convert.ToString(dt.Rows[0]["FSFlag"]) == "01")
                {
                    rbtFS.SelectedValue = "F";
                }
                else if (Convert.ToString(dt.Rows[0]["FSFlag"]) == "02")
                {
                    rbtFS.SelectedValue = "S";
                }
                cboTitle2.SelectedValue = Convert.ToString(dt.Rows[0]["FSPrefix"]);
                txtGivenName2.Text = Convert.ToString(dt.Rows[0]["FSFName"]);
                txtMiddleName2.Text = Convert.ToString(dt.Rows[0]["FSMName"]);
                txtLastName2.Text = Convert.ToString(dt.Rows[0]["FSLName"]);
                cboTitle3.SelectedValue = Convert.ToString(dt.Rows[0]["MotherPrefix"]);
                txtGivenName3.Text = Convert.ToString(dt.Rows[0]["MothersFName"]);
                txtMiddleName3.Text = Convert.ToString(dt.Rows[0]["MothersMName"]);
                txtLastName3.Text = Convert.ToString(dt.Rows[0]["MothersLName"]);
                txtDOB.Text = Convert.ToString(dt.Rows[0]["DOB"]);
                cboGender.SelectedValue = Convert.ToString(dt.Rows[0]["GENDER"]);

                ddlIsoCountryCodeOthr.SelectedValue = Convert.ToString(dt.Rows[0]["ISO_COUNTRYCODE"]);


                ddlIsoCountryCode2.SelectedValue = Convert.ToString(dt.Rows[0]["ISO_RFT_COUNTRYCODE"]);

                txtIDResTax.Text = Convert.ToString(dt.Rows[0]["TAX_IDNUMBER"]);
                txtDOBRes.Text = Convert.ToString(dt.Rows[0]["BIRTH_PLACE"]);
                ddlIsoCountry.SelectedValue = Convert.ToString(dt.Rows[0]["ISO_BIRTHPLACE_CODE"]);

                ddlProofIdentity.SelectedValue = Convert.ToString(dt.Rows[0]["IdType"]);
                ViewState["strIdName"] = Convert.ToString(dt.Rows[0]["IdName"]);
                ViewState["strIdNumber"] = Convert.ToString(dt.Rows[0]["IdNumber"]);
                ViewState["strIdExpDate"] = Convert.ToString(dt.Rows[0]["IdExpDate"]);

                if (ddlProofIdentity.SelectedValue == "Z")
                {
                    txtPassOthr.Text = Convert.ToString(dt.Rows[0]["IdNumber"]);
                    txtPassNo.Text = Convert.ToString(dt.Rows[0]["IdName"]);
                }
                else
                {

                    txtPassNo.Text = Convert.ToString(dt.Rows[0]["IdNumber"]);
                    txtPassExpDate.Text = Convert.ToString(dt.Rows[0]["IdExpDate"]);
                }

                if (Convert.ToString(dt.Rows[0]["CnctType1"]) == "P1")
                {
                    chkPerAddress.Checked = true;
                }
                else
                {
                    chkPerAddress.Checked = false;
                }
                ddlProofOfAddress.SelectedValue = Convert.ToString(dt.Rows[0]["PER_ADDPROOF"]);
                txtAddressLine1.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE1"]);
                txtAddressLine2.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE2"]);
                txtAddressLine3.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE3"]);
                txtCity.Text = Convert.ToString(dt.Rows[0]["PER_CITY"]);
                ddlDistrict.SelectedItem.Text = Convert.ToString(dt.Rows[0]["PER_DISTRICT"]);
                ddlPinCode.SelectedValue = Convert.ToString(dt.Rows[0]["PER_PIN"]);
                ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["PER_STATECODE"]);
                ddlCountryCode.SelectedValue = Convert.ToString(dt.Rows[0]["PER_COUNTRY_CODE"]);//

                ViewState["strAddIdName"] = Convert.ToString(dt.Rows[0]["AddIdName"]);
                ViewState["strAddIdNumber"] = Convert.ToString(dt.Rows[0]["AddIdNumber"]);
                ViewState["strAddIdExpDate"] = Convert.ToString(dt.Rows[0]["AddIdExpDate"]);

                if (ddlProofOfAddress.SelectedValue == "99")
                {
                    txtPassOthrAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNumber"]);
                    txtPassNoAdd.Text = Convert.ToString(dt.Rows[0]["AddIdName"]);
                }
                else
                {
                    txtPassNoAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNumber"]);
                    txtPassExpDateAdd.Text = Convert.ToString(dt.Rows[0]["AddIdExpDate"]);
                }

                if (Convert.ToString(dt.Rows[0]["CnctType2"]) == "M1" && (dt.Rows[0]["CUR_PIN"]).ToString() != "")
                {
                    chkLocalAddress.Checked = true;
                }
                else
                {
                    chkLocalAddress.Checked = false;
                }
                txtLocAddLine1.Text = Convert.ToString(dt.Rows[0]["CUR_ADDLINE1"]);
                txtLocAddLine2.Text = Convert.ToString(dt.Rows[0]["CUR_ADDLINE2"]);
                txtLocAddLine3.Text = Convert.ToString(dt.Rows[0]["CUR_ADDLINE3"]);
                txtCity1.Text = Convert.ToString(dt.Rows[0]["CUR_CITY"]);
                ddlDistrict1.SelectedItem.Text = Convert.ToString(dt.Rows[0]["CUR_DISTRICT"]);
                ddlPinCode1.SelectedValue = Convert.ToString(dt.Rows[0]["CUR_PIN"]);
                ddlState1.SelectedValue = Convert.ToString(dt.Rows[0]["CUR_STATECODE"]);
                ddlCountryCode1.SelectedValue = Convert.ToString(dt.Rows[0]["CUR_COUNTRY_CODE"]);



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

                txtEmpName.Text = Convert.ToString(dt.Rows[0]["kycEmpName"]);
                txtEmpCode.Text = Convert.ToString(dt.Rows[0]["kycEmpCode"]);
                txtEmpDesignation.Text = Convert.ToString(dt.Rows[0]["kycEmpDesi"]);
                txtEmpBranch.Text = Convert.ToString(dt.Rows[0]["kycEmpBranch"]);
                txtInsName.Text = Convert.ToString(dt.Rows[0]["kycInstName"]);

                txtInsCode.Text = Convert.ToString(dt.Rows[0]["kycInstCode"]);
                txtDateKYCver.Text = Convert.ToString(dt.Rows[0]["kycVerDate"]);
                ddlDocReceived.SelectedValue = Convert.ToString(dt.Rows[0]["kycCertDoc"]);
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "FillRequiredDataForPartialSave", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
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
                    divchkDelRel.Visible = true;
                    chkAddRel.Checked = true;

                    if (Request.QueryString["Status"].ToString() == "PMod")
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

        //#region validation
        //protected string validation()
        //{
        //    string msg = string.Empty;
        //    string date;
        //    date = DateTime.Today.ToString("d\\/MM\\/yyyy");

        //    try
        //    {
        //        if (chkNormal.Checked == false && chkSimplified.Checked == false && Chksmall.Checked == false)
        //        {
        //            msg = "Please select account type";
        //            return msg;
        //        }

        //        if (cboTitle.SelectedIndex == 0)
        //        {
        //            msg = "Please select title of name";
        //            return msg;
        //        }
        //        if (txtGivenName.Text == "")
        //        {
        //            msg = "Please enter first name";
        //            return msg;
        //        }
        //        if (txtLastName.Text == "")
        //        {
        //            msg = "Please enter last name";
        //            return msg;
        //        }
        //        if (cboTitle2.SelectedIndex == 0)
        //        {
        //            msg = "Please select title of father/spouse Name";
        //            return msg;
        //        }
        //        if (rbtFS.SelectedValue == "F")
        //        {
        //            if (cboTitle2.SelectedValue == "MRS" || cboTitle2.SelectedValue == "MS")
        //            {
        //                msg = "Invalid Title for Father/Spouse Name";
        //                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);
        //                return msg;
        //            }
        //        }
        //        else if (rbtFS.SelectedValue == "S")

        //            if (cboTitle2.SelectedValue == "MR")
        //            {
        //                msg = "Invalid Title for father/spouse Name";
        //                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);
        //                return msg;
        //            }
        //        if (txtGivenName2.Text == "")
        //        {
        //            msg = "Please enter first name of father/spouse";
        //            return msg;
        //        }
        //        if (txtLastName2.Text == "")
        //        {
        //            msg = "Please enter last name of father/spouse";
        //            return msg;
        //        }
        //        if (cboTitle3.SelectedIndex == 0)
        //        {
        //            msg = "Please select title of mother Name";
        //            return msg;
        //        }
        //        if (txtGivenName3.Text == "")
        //        {
        //            msg = "Please enter first name of mother";
        //            return msg;
        //        }

        //        if (txtLastName3.Text == "")
        //        {
        //            msg = "Please enter last name of mother";
        //            return msg;
        //        }

        //        if (txtDOB.Text == "")
        //        {
        //            msg = "Please enter date of birth";
        //            return msg;
        //        }
        //        if (txtDOB.Text != "")
        //        {
        //            string pattern = "^(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)[0-9]{2}$";
        //            Match match = Regex.Match(txtDOB.Text.ToString(), pattern);
        //            if (!match.Success)
        //            {
        //                msg = "Check driving license date format it must be in dd/mm/yyyy";
        //                return msg;
        //            }

        //            DateTime date1,date2;
        //            date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //            date2 = DateTime.ParseExact(txtDOB.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //            if (date1 < date2)
        //            {
        //                msg = "Date of Birth can not be future date";
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);
        //                return msg;
        //            }
        //        }
        //        if (cboGender.SelectedIndex == 0)
        //        {
        //            msg = "Please select gender";
        //            return msg;

        //        }
        //        if (ddlOccupation.SelectedIndex == 0)
        //        {
        //            msg = "Please select occupation type";
        //            return msg;
        //        }
        //        if (ddlOccupation.SelectedIndex != 0)
        //        {
        //            if (ddlOccuSubType.SelectedIndex == 0)
        //            {
        //                msg = "Please select occupation sub type";
        //                return msg;
        //            }
        //        }
        //        if (ddlMaritalStatus.SelectedIndex == 0)
        //        {
        //            msg = "Please select marital status";
        //            return msg;
        //        }

        //        if (cboTitle.SelectedValue == "MRS" && ddlMaritalStatus.SelectedValue == "02")
        //        {
        //            msg = "Invalid Title";
        //            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);
        //            return msg;
        //        }
        //        if (ddlCitizenship.SelectedIndex == 0)
        //        {
        //            msg = "Please select citizenship";
        //            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('" + msg + "')", true);
        //            return msg;
        //        }
        //        if (ddlResStatus.SelectedIndex == 0)
        //        {
        //            msg = "Please select residential status";
        //            return msg;
        //        }
        //        if (ddlCitizenship.SelectedIndex == 2)
        //        {
        //            if (ddlIsoCountryCodeOthr.SelectedIndex == 0)
        //            {
        //                msg = "Please select ISO 3166 country code";
        //                return msg;
        //            }
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
        //            objErr.LogErr(AppID, "CkycReg.aspx.cs", "validation", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
        //        }
        //    }
        //    return msg;
        //} 
        //#endregion

        #region btnPartialUpdate
        protected void btnPartialUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string Res;
                Res = objVal.PersonalDtlsValidation(
                    chkNormal, chkSimplified, Chksmall, cboTitle, txtGivenName, txtLastName, cboTitle2, rbtFS, txtGivenName2, txtLastName2, cboTitle3, txtGivenName3,
                    txtLastName3, txtDOB, cboGender, ddlIsoCountryCodeOthr, null, "Candidate");

                if (Res.Equals(""))
                {

                    #region relatedpersonDSvalidation
                    dt = new DataTable();
                    dt = (DataTable)Session["dsRel"];
                    if (chkAddRel.Checked == true)
                    {
                        if (dt == null)
                        {
                            chkAddRel.Checked = false;
                            return;
                        }
                    }
                    #endregion

                    #region cnd kyc Partial Details
                    htParam.Clear();

                    if (cbNew.Checked == true)
                    {
                        htParam.Add("@AppType", "01");
                    }
                    else
                    {
                        htParam.Add("@AppType", "02");
                    }

                    if (chkNormal.Checked == true)
                    {
                        htParam.Add("@AccountType", "01");
                    }
                    else if (chkSimplified.Checked == true)
                    {
                        htParam.Add("@AccountType", "02");
                    }
                    else if (Chksmall.Checked == true)
                    {
                        htParam.Add("@AccountType", "03");
                    }
                    htParam.Add("@Prefix", cboTitle.SelectedValue);
                    htParam.Add("@FName", txtGivenName.Text.Trim());
                    htParam.Add("@MName", txtMiddleName.Text.Trim());
                    htParam.Add("@LName", txtLastName.Text.Trim());
                    htParam.Add("@MaidenPrefix", cboTitle1.SelectedValue);
                    htParam.Add("@MaidenFName", txtGivenName1.Text.Trim());
                    htParam.Add("@MaidenMName", txtMiddleName1.Text.Trim());
                    htParam.Add("@MaidenLName", txtLastName1.Text.Trim());

                    if (rbtFS.SelectedValue == "F")
                    {
                        htParam.Add("@FSFlag", "01");
                    }
                    else if (rbtFS.SelectedValue == "S")
                    {
                        htParam.Add("@FSPrefix", "02");
                    }
                    else
                    {
                        htParam.Add("@FSPrefix", System.DBNull.Value);
                    }
                    htParam.Add("@FSPrefix", cboTitle2.SelectedValue);
                    htParam.Add("@FSFName", txtGivenName2.Text.Trim());
                    htParam.Add("@FSMName", txtMiddleName2.Text.Trim());
                    htParam.Add("@FSLName", txtLastName2.Text.Trim());
                    htParam.Add("@MotherPrefix", cboTitle3.SelectedValue);
                    htParam.Add("@MothersFName", txtGivenName3.Text);
                    htParam.Add("@MothersMName", txtMiddleName2.Text);
                    htParam.Add("@MothersLName", txtLastName3.Text);
                    htParam.Add("@DateofBirth", txtDOB.Text);
                    htParam.Add("@Gender", cboGender.SelectedValue);
                    htParam.Add("@Maritalstatus", "");//@
                    htParam.Add("@Nationality", "");
                    htParam.Add("@ResidentialStatus", "");
                    htParam.Add("@OccupationType", "");
                    if (chkTick.Checked == true)
                    {
                        htParam.Add("@JurisdictionFlag", "01");
                    }
                    else if (chkTick.Checked == false)
                    {
                        htParam.Add("@JurisdictionFlag", "02");
                    }
                    else
                    {
                        htParam.Add("@JurisdictionFlag", System.DBNull.Value);
                    }

                    htParam.Add("@JurisdictionCountryofBirth", ddlIsoCountryCode2.SelectedValue.Trim());
                    htParam.Add("@TIN", txtIDResTax.Text.Trim());
                    htParam.Add("@JurisdictionBirthPlace", txtDOBRes.Text.Trim());
                    htParam.Add("@TINIssuingCountry", ddlIsoCountryCode2.SelectedValue);
                    htParam.Add("@IDType", ddlProofIdentity.SelectedValue);
                    if (ddlProofIdentity.SelectedIndex == 1)
                    {
                        htParam.Add("@IDNum", txtPassNo.Text.Trim());
                        htParam.Add("@IDExpDate", txtPassExpDate.Text.Trim());
                        htParam.Add("@IDName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 2)
                    {
                        htParam.Add("@IDNum", txtPassNo.Text.Trim());
                        htParam.Add("@IDExpDate", System.DBNull.Value);
                        htParam.Add("@IDName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 3)
                    {
                        htParam.Add("@IDNum", txtPassNo.Text.Trim());
                        htParam.Add("@IDExpDate", System.DBNull.Value);
                        htParam.Add("@IDName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 4)
                    {
                        htParam.Add("@IDNum", txtPassNo.Text.Trim());
                        htParam.Add("@IDExpDate", txtPassExpDate.Text.Trim());
                        htParam.Add("@IDName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 5)
                    {
                        htParam.Add("@IDNum", txtPassNo.Text.Trim());
                        htParam.Add("@IDExpDate", System.DBNull.Value);
                        htParam.Add("@IDName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 6)
                    {
                        htParam.Add("@IDNum", txtPassNo.Text.Trim());
                        htParam.Add("@IDExpDate", System.DBNull.Value);
                        htParam.Add("@IDName", System.DBNull.Value);
                    }
                    else if (ddlProofIdentity.SelectedIndex == 7)
                    {
                        htParam.Add("@IDENT_NUM_ID1", txtPassOthr.Text.Trim());
                        htParam.Add("@IDExpDate", System.DBNull.Value);
                        htParam.Add("@IDName", txtPassNo.Text.Trim());
                    }
                    else if (ddlProofIdentity.SelectedIndex == 8 || ddlProofIdentity.SelectedIndex == 9)
                    {
                        htParam.Add("@IDNum", txtPassNo.Text.Trim());
                        htParam.Add("@IDExpDate", System.DBNull.Value);
                        htParam.Add("@IDName", System.DBNull.Value);
                    }
                    else
                    {
                        htParam.Add("@IDNum", System.DBNull.Value);
                        htParam.Add("@IDExpDate", System.DBNull.Value);
                        htParam.Add("@IDName", System.DBNull.Value);
                    }


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

                    if (chkPerAddress.Checked == true)
                    {
                        if (ddlProofOfAddress.SelectedIndex == 1)
                        {
                            htParam.Add("@PER_IDNUMBAER", txtPassNoAdd.Text.Trim());
                            htParam.Add("@PER_IDEXPDT", txtPassExpDateAdd.Text.Trim());
                            htParam.Add("@PER_IDOtherDocName", System.DBNull.Value);

                        }
                        else if (ddlProofOfAddress.SelectedIndex == 2)
                        {
                            htParam.Add("@PER_IDNUMBAER", txtPassNoAdd.Text.Trim());
                            htParam.Add("@PER_IDEXPDT", txtPassExpDateAdd.Text.Trim());
                            htParam.Add("@PER_IDOtherDocName", System.DBNull.Value);
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 3)
                        {
                            htParam.Add("@PER_IDNUMBAER", txtPassNoAdd.Text.Trim());
                            htParam.Add("@PER_IDEXPDT", System.DBNull.Value);
                            htParam.Add("@PER_IDOtherDocName", System.DBNull.Value);
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 4)
                        {
                            htParam.Add("@PER_IDNUMBAER", txtPassNoAdd.Text.Trim());
                            htParam.Add("@PER_IDEXPDT", System.DBNull.Value);
                            htParam.Add("@PER_IDOtherDocName", System.DBNull.Value);

                        }
                        else if (ddlProofOfAddress.SelectedIndex == 5)
                        {
                            htParam.Add("@PER_IDNUMBAER", txtPassNoAdd.Text.Trim());
                            htParam.Add("@PER_IDEXPDT", System.DBNull.Value);
                            htParam.Add("@PER_IDOtherDocName", System.DBNull.Value);
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 6)
                        {
                            htParam.Add("@PER_IDNUMBAER", txtPassOthrAdd.Text.Trim());
                            htParam.Add("@PER_IDEXPDT", System.DBNull.Value);//
                            htParam.Add("@PER_IDOtherDocName", txtPassNoAdd.Text.Trim());
                        }
                        else
                        {
                            htParam.Add("@PER_IDNUMBAER", System.DBNull.Value);
                            htParam.Add("@PER_IDEXPDT", System.DBNull.Value);
                            htParam.Add("@PER_IDOtherDocName", System.DBNull.Value);
                        }
                    }
                    else
                    {
                        htParam.Add("@PER_IDNUMBAER", System.DBNull.Value);
                        htParam.Add("@PER_IDEXPDT", System.DBNull.Value);
                        htParam.Add("@PER_IDOtherDocName", System.DBNull.Value);
                    }
                    if (chkLocalAddress.Checked == true)
                    {
                        htParam.Add("@CnctType2", "M1");
                        htParam.Add("@SameasCurrentAddresFlag", "01");//by meena 19/05/2017
                        htParam.Add("@CUR_ADDLINE1", txtLocAddLine1.Text);
                        htParam.Add("@CUR_ADDLINE2", txtLocAddLine2.Text);
                        htParam.Add("@CUR_ADDLINE3", txtLocAddLine3.Text);
                        htParam.Add("@CUR_CITY ", txtCity1.Text.Trim());
                        htParam.Add("@CUR_DISTRICT", ddlDistrict1.SelectedValue);
                        htParam.Add("@CUR_PIN", ddlPinCode1.SelectedValue);
                        htParam.Add("@CUR_STATECODE", ddlState1.SelectedValue);
                        htParam.Add("@CUR_COUNTRY_CODE", ddlCountryCode1.SelectedValue);
                    }
                    else
                    {
                        htParam.Add("@CnctType2", "");
                        htParam.Add("@SameasCurrentAddresFlag", "02");//by meena 19/05/2017
                        htParam.Add("@CUR_ADDLINE1", System.DBNull.Value);
                        htParam.Add("@CUR_ADDLINE2", System.DBNull.Value);
                        htParam.Add("@CUR_ADDLINE3", System.DBNull.Value);
                        htParam.Add("@CUR_CITY", System.DBNull.Value);
                        htParam.Add("@CUR_DISTRICT", System.DBNull.Value);
                        htParam.Add("@CUR_PIN", System.DBNull.Value);
                        htParam.Add("@CUR_STATECODE", System.DBNull.Value);
                        htParam.Add("@CUR_COUNTRY_CODE", System.DBNull.Value);
                    }


                    htParam.Add("@CnctType3", "");
                    //htParam.Add("@SameasCurrentAddresFlag", "02");
                    htParam.Add("@FRN_ADDLINE1", System.DBNull.Value);
                    htParam.Add("@FRN_ADDLINE2", System.DBNull.Value);
                    htParam.Add("@FRN_ADDLINE3", System.DBNull.Value);
                    htParam.Add("@FRN_CITY", System.DBNull.Value);
                    htParam.Add("@FRN_DISTRICT", System.DBNull.Value);
                    htParam.Add("@FRN_PIN", System.DBNull.Value);
                    htParam.Add("@FRN_STATECODE", System.DBNull.Value);
                    htParam.Add("@FRN_COUNTRY_CODE", System.DBNull.Value);

                    htParam.Add("@OfficeTelSTDCode", txtTelOff.Text.Trim());
                    htParam.Add("@TelSTDCode", txtTelRes.Text.Trim());
                    htParam.Add("@MobCode", txtMobile.Text.Trim());
                    htParam.Add("@FaxNoCode", txtFax1.Text);

                    htParam.Add("@OfficeTelNo", txtTelOff2.Text);
                    htParam.Add("@TelNo", txtTelRes2.Text);
                    htParam.Add("@FaxNo", txtFax2.Text);
                    htParam.Add("@MobileNo", txtMobile2.Text);
                    htParam.Add("@EmailID", txtemail.Text);


                    htParam.Add("@Remarks", txtRemarks.Text.Trim());
                    htParam.Add("@DateofDeclaration", txtDate.Text.Trim());
                    htParam.Add("@PlaceofDeclaration", txtPlace.Text.Trim());
                    htParam.Add("@KYCRecruiterName", txtEmpName.Text.Trim());
                    htParam.Add("@KYCRecruiterEMPcode", txtEmpCode.Text.Trim());
                    htParam.Add("@KYCRecruiterBranch", txtEmpBranch.Text.Trim());
                    htParam.Add("@KYCRecruiterDesignation", txtEmpDesignation.Text.Trim());
                    htParam.Add("@KYCVerificationDate", txtDateKYCver.Text.Trim());
                    htParam.Add("@TypeofDocumentSubmitted", ddlDocReceived.SelectedValue);

                    htParam.Add("@OrganisationName", txtInsName.Text.Trim());
                    htParam.Add("@OrganisationCode", txtInsCode.Text.Trim());
                    htParam.Add("@updatedBy", strUserId.ToString());

                    objDAL = new DataAccessLayer("STAGINGConnectionString");
                    DataTable dtResult = new DataTable();

                    if (Request.QueryString["Status"].ToString() == "PMod")
                    {
                        htParam.Add("@PSTempRefNo", txtRefNumber.Text.ToString());
                        dtResult = objDAL.GetDataTable("prc_updatekycPartialdtls", htParam);
                    }
                    if (dtResult.Rows.Count > 0)
                    {
                        PSTempRefNo = dtResult.Rows[0]["PSTempRefNo"].ToString();
                    }
                    #endregion

                    #region Save Members Details;
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                htParam.Clear();
                                if (chkAddRel.Checked == true)
                                {
                                    htParam.Add("@RelAddDelFlag", "01");
                                }
                                else
                                {
                                    htParam.Add("@RelAddDelFlag", "02");
                                }
                                htParam.Add("@RelTYPE", dt.Rows[i]["RelationType"]);
                                htParam.Add("@RelCKYCNO", dt.Rows[i]["RelatedPrsnKYCNo"]);
                                htParam.Add("@RelPrefix", dt.Rows[i]["PrefixRel"]);
                                htParam.Add("@RelFName", dt.Rows[i]["FNameRel"]);
                                htParam.Add("@RelMName", dt.Rows[i]["MNameRel"]);
                                htParam.Add("@RelLName", dt.Rows[i]["LNameRel"]);
                                htParam.Add("@RelMaidPrefix", dt.Rows[i]["MaidPrefixRel"]);
                                htParam.Add("@RelMaidFNmae", dt.Rows[i]["MaidFNameRel"]);
                                htParam.Add("@RelMaidMNmae", dt.Rows[i]["MaidMNameRel"]);
                                htParam.Add("@RelMaidLNmae", dt.Rows[i]["MaidLNameRel"]);
                                htParam.Add("@FlagFatherorSpouse", dt.Rows[i]["FSFlagRel"]);
                                htParam.Add("@RelFatherPrefix", dt.Rows[i]["FatherPrefixRel"]);
                                htParam.Add("@RelFatheFName", dt.Rows[i]["FatherFNameRel"]);
                                htParam.Add("@RelFatheMName", dt.Rows[i]["FatherMNameRel"]);
                                htParam.Add("@RelFatheLName", dt.Rows[i]["FatherLNameRel"]);
                                htParam.Add("@RelMotherPrefix", dt.Rows[i]["MotherPrefixRel"]);
                                htParam.Add("@RelMotherFName", dt.Rows[i]["MotherFNameRel"]);
                                htParam.Add("@RelMotherMname", dt.Rows[i]["MotherMNameRel"]);
                                htParam.Add("@RelMotherLName", dt.Rows[i]["MotherLNameRel"]);
                                htParam.Add("@RelDOB", dt.Rows[i]["DOBRel"]);
                                htParam.Add("@RelGender", dt.Rows[i]["GenderRel"]);
                                htParam.Add("@RelMaritalStatus", dt.Rows[i]["MaritalStatusRel"]);

                                htParam.Add("@RelResistatus", dt.Rows[i]["ResiStatusRel"]);
                                htParam.Add("@RelOccuType", dt.Rows[i]["OccuTypeRel"]);
                                htParam.Add("@RelCitizenship", dt.Rows[i]["CitizenshipRel"]);


                                htParam.Add("@RelJurisdictionFlag", dt.Rows[i]["ResForTaxFlagRel"]);
                                htParam.Add("@RelISOCountryCode", dt.Rows[i]["ISOCountryCodeRel"]);
                                htParam.Add("@RelTIN", dt.Rows[i]["TaxIDNumberRel"]);
                                htParam.Add("@RelBirthCity", dt.Rows[i]["BirthCityRel"]);
                                htParam.Add("@RelISOBirthPlace", dt.Rows[i]["ISOBirthPlaceCodeRel"]);

                                htParam.Add("@IDType", dt.Rows[i]["IdType"]);
                                htParam.Add("@IDNum", dt.Rows[i]["IdNumber"]);
                                htParam.Add("@IDExpDate", dt.Rows[i]["IdExpDate"]);
                                htParam.Add("@IDName", dt.Rows[i]["IdName"]);

                                htParam.Add("@CnctType1", "P1");
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

                                htParam.Add("@PER_IDNUMBAER", dt.Rows[i]["AddIdNumberRel"]);
                                htParam.Add("@PER_IDEXPDT", dt.Rows[i]["AddIdExpDateRel"]);
                                htParam.Add("@PER_IDOtherDocName", dt.Rows[i]["AddIdNameRel"]);
                                htParam.Add("@RelDecDate", dt.Rows[i]["DecDateRel"]);
                                htParam.Add("@RelDecPlace", dt.Rows[i]["DecPlaceRel"]);
                                htParam.Add("@RelkycEmpName", dt.Rows[i]["kycEmpNameRel"]);
                                htParam.Add("@RelkycEmpCode", dt.Rows[i]["kycEmpCodeRel"]);
                                htParam.Add("@RelkycEmpBranch", dt.Rows[i]["kycEmpBranchRel"]);
                                htParam.Add("@RelkycEmpDesi", dt.Rows[i]["kycEmpDesiRel"]);
                                htParam.Add("@RelkycVerDate", dt.Rows[i]["kycVerDateRel"]);
                                htParam.Add("@RelkycCertDoc", "01");
                                htParam.Add("@RelkycInstName", dt.Rows[i]["kycInstNameRel"]);
                                htParam.Add("@RelkycInstCode", dt.Rows[i]["kycInstCodeRel"]);
                                htParam.Add("@UpdateBy", strUserId.ToString());
                                if (Request.QueryString["Status"].ToString() == "PMod")
                                {
                                    objDAL = new DataAccessLayer("STAGINGConnectionString");
                                    htParam.Add("@PSTempRefNo", txtRefNumber.Text.ToString());
                                    htParam.Add("@PSTempRelRefNo", dt.Rows[i]["RelRefNo"]);
                                    objDAL.ExecuteNonQuery("prc_updKycRelPrsnPartialDtls", htParam);

                                }
                                Session["dsRel"] = null;
                            }
                        }
                    }
                    #endregion

                    if (gvMemDtls.Visible == true)
                    {
                        foreach (GridViewRow row in gvMemDtls.Rows)
                        {
                            LinkButton lnkdelete = (LinkButton)row.FindControl("lnkdelete");
                            lnkdelete.Enabled = false;
                        }
                    }
                    hdnUpdate.Value = "Partial Data Updated  succesfully.";

                    msg = hdnUpdate.Value + "</br></br>Temporary Reference No: " + txtRefNumber.Text.ToString().Trim() + "<br/>Candidate Name: "
                           + cboTitle.SelectedValue + " " + txtGivenName.Text + " " + txtLastName.Text + "<br/><br/>";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "popup('" + msg + "');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('" + msg + "')", true);

                    btnPartialUpdate.Enabled = false;
                    btnSave.Enabled = false;

                    txtKYCNumber.Text = strRefNo.ToString().Trim();
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please,atleast fill Personal detail ')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('" + Res + "')", true);
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "btnPartialUpdate_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
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

                DataRow dr = dt.Rows[i];

                string RelRefnNo = Convert.ToString(dr[1]);
                string refno = Request.QueryString["refno"].ToString().Trim();
                if (Request.QueryString["Status"].ToString() == "Mod")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenRelatedPersonPageEdit(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["Status"].ToString() == "PMod")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenPartialRelatedPersonPageEdit(" + RelRefnNo + "," + refno + ");", true);
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

        #region METHOD "getupdateflag"
        protected void getupdateflag()
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                htParam.Clear();
                htParam.Add("@RegRefNo", Request.QueryString["refno"].ToString());
                dt = objDAL.GetDataTable("getSearchData_Web", htParam);

                if (Convert.ToString(dt.Rows[0]["AccType"]) == "01")
                {
                    chkNormal.Checked = true;
                }
                else if (Convert.ToString(dt.Rows[0]["AccType"]) == "02")
                {
                    chkSimplified.Checked = true;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "getupdateflag", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
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

        #region METHOD "InitializeControl()"
        private void InitializeControls()
        {
            try
            {
                lblAppType.Text = olng.GetItemDesc("lblAppType");
                lblRefNumber.Text = olng.GetItemDesc("lblRefNumber");
                lblAccountType.Text = olng.GetItemDesc("lblAccountType");
                lblKYCNumber.Text = olng.GetItemDesc("lblKYCNumber");
                //LblPrefix.Text = olng.GetItemDesc("lblcategory");
                //LblFName.Text = olng.GetItemDesc("lblcategory");
                //LblMName.Text = olng.GetItemDesc("lblcategory");
                //LblLName.Text = olng.GetItemDesc("lblcategory");
                lblName.Text = olng.GetItemDesc("lblName");
                lblMaidenName.Text = olng.GetItemDesc("lblMaidenName");
                lblFatherName.Text = olng.GetItemDesc("lblFatherName");
                lblMotherName.Text = olng.GetItemDesc("lblMotherName");
                lbldob.Text = olng.GetItemDesc("lbldob");
                lblGender.Text = olng.GetItemDesc("lblGender");

                lblIsoCountryCodeOthr.Text = olng.GetItemDesc("lblIsoCountryCodeOthr");
                lblIsoCountryCode2.Text = olng.GetItemDesc("lblIsoCountryCode2");
                lblTaxIden.Text = olng.GetItemDesc("lblTaxIden");
                lblPlace.Text = olng.GetItemDesc("lblPlace");
                lblIsoContry.Text = olng.GetItemDesc("lblIsoContry");
                lblProofOfIdentity11.Text = olng.GetItemDesc("lblProofOfIdentity11");
                lblProof.Text = olng.GetItemDesc("lblProof");
                lblProofOfAddress.Text = olng.GetItemDesc("lblProofOfAddress");
                lblAddressLine1.Text = olng.GetItemDesc("lblAddressLine1");
                lblAddressLine2.Text = olng.GetItemDesc("lblAddressLine2");
                lblAddressLine3.Text = olng.GetItemDesc("lblAddressLine3");
                lblCity.Text = olng.GetItemDesc("lblCity");
                lblDistrict.Text = olng.GetItemDesc("lblDistrict");
                lblPinCode.Text = olng.GetItemDesc("lblPinCode");
                lblState.Text = olng.GetItemDesc("lblState");
                lblIsoCountryCode.Text = olng.GetItemDesc("lblIsoCountryCode");
                lblLocAddLine1.Text = olng.GetItemDesc("lblLocAddLine1");
                lblLocAddLine2.Text = "Address Line 2"; // olng.GetItemDesc("lblLocAddLine2");
                lblLocAddLine3.Text = "Address Line 3"; // olng.GetItemDesc("lblLocAddLine3");
                lblCity1.Text = olng.GetItemDesc("lblCity1");
                lblDistrict1.Text = olng.GetItemDesc("lblDistrict1");
                lblPin1.Text = olng.GetItemDesc("lblPin1");
                lblState1.Text = olng.GetItemDesc("lblState1");
                lblCountryCode1.Text = olng.GetItemDesc("lblCountryCode1");

                lblTelOff1.Text = olng.GetItemDesc("lblTelOff1");
                lblTelRes.Text = olng.GetItemDesc("lblTelRes");
                lblMobile.Text = olng.GetItemDesc("lblMobile");
                lblFax.Text = olng.GetItemDesc("lblFax");
                lblpfemail.Text = olng.GetItemDesc("lblpfemail");
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

                lblpfPersonal1.Text = olng.GetItemDesc("lblpfPersonal1");
                lblpfPersonal.Text = olng.GetItemDesc("lblpfPersonal");
                lbltick.Text = olng.GetItemDesc("lbltick");
                lblProofOfIdentity11.Text = olng.GetItemDesc("lblProofOfIdentity11");
                lblpfofAddr1.Text = olng.GetItemDesc("lblpfofAddr1");
                lblpfofAddr2.Text = olng.GetItemDesc("lblpfofAddr2");
                lblDtlOfRtltpr.Text = olng.GetItemDesc("lblDtlOfRtltpr");
                lblRemarks.Text = olng.GetItemDesc("lblRemarks");
                lblattstn.Text = olng.GetItemDesc("lblattstn");
                lbldec.Text = olng.GetItemDesc("lbldec");
                lblAttesOfc.Text = olng.GetItemDesc("lblAttesOfc");
                lblOfcuseOnly.Text = olng.GetItemDesc("lblOfcuseOnly");
                lblInsDtls.Text = olng.GetItemDesc("lblInsDtls");
                lblContactDetails.Text = olng.GetItemDesc("lblContactDetails");
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

        protected void ddlCountryCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (chkPerAddress.Checked == false)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please check CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS')", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "AlertMsg('Please check CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS')", true);
                    //chkPerAddress.Focus();
                    ddlCountryCode.SelectedIndex = 0;
                    return;
                }

                ddlDistrict.Items.Clear();
                ddlDistrict.Items.Insert(0, new ListItem("Select", ""));

                ddlState.Items.Clear();
                ddlState.Items.Insert(0, new ListItem("Select", ""));
                ddlPinCode.SelectedIndex = 0;

                ddlPinCode.Enabled = (ddlCountryCode.SelectedValue == "IN" || ddlCountryCode.SelectedIndex == 0);
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

        protected void ddlCountryCode1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (chkLocalAddress.Checked == false)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please check CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS')", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "AlertMsg('Please check CORRESPONDENCE/LOCAL ADDRESS DETAILS')", true);
                    //chkPerAddress.Focus();
                    ddlCountryCode1.SelectedIndex = 0;
                    return;
                }


                ddlDistrict1.Items.Clear();
                ddlDistrict1.Items.Insert(0, new ListItem("Select", ""));

                ddlState1.Items.Clear();
                ddlState1.Items.Insert(0, new ListItem("Select", ""));
                ddlPinCode1.SelectedIndex = 0;

                ddlPinCode1.Enabled = (ddlCountryCode1.SelectedValue == "IN" || ddlCountryCode1.SelectedIndex == 0);

                //if (ddlCountryCode1.SelectedValue != "IN")
                //{
                //    ddlPinCode1.SelectedIndex = 0;
                //    ddlDistrict1.SelectedIndex = 0;
                //    ddlState1.SelectedIndex = 0;

                //    ddlPinCode1.Enabled = false;
                //    ddlDistrict1.Enabled = false;
                //    ddlState1.Enabled = false;
                //}
                //else
                //{
                //    ddlPinCode1.SelectedIndex = 0;
                //    ddlDistrict1.SelectedIndex = 0;
                //    ddlState1.SelectedIndex = 0;

                //    ddlPinCode1.Enabled = true;
                //    ddlDistrict1.Enabled = true;
                //    ddlState1.Enabled = true;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "lnkEdit_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }


    }
}

//FillDistrictState(ddlPinCode, ddlDistrict, ddlState);
//FillDistrictState(ddlPinCode1, ddlDistrict1, ddlState1);
//FillDistrictState(ddlPinCode2, ddlDistrict2, ddlState2);