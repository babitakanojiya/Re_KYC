using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KMI.FRMWRK.Multilingual;
using KMI.FRMWRK.Web.Application.CKYC.Common;
using KMI.FRMWRK.DAL;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class CKYCRelatedPrsnView : System.Web.UI.Page
    {
        #region Declare Veriables
        private MultilingualManager olng;
        Hashtable htParam = new Hashtable();
        Hashtable objht = new Hashtable();
        DataTable objDt = new DataTable();
        DataAccessLayer objDAL;
        CommonUtility oCommonUtility = new CommonUtility();
        ErrorLog objErr;
        DataSet dsResult = new DataSet();
        string UserID = string.Empty;
        CkycValidtion objVal = new CkycValidtion();
        string strUserId;
        Guid obj = Guid.NewGuid();
        DataTable DtAdd = new DataTable();
        DataTable PartialDtAdd = new DataTable();
        DataTable dt;
        int AppID;
        #endregion

        #region PAGELOADEVENTS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserLangNum"] == null || Session["CarrierCode"] == null || Session["LanguageCode"] == null)
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                if (Session["AppID"] != null)
                {
                    AppID = Convert.ToInt32(Session["AppID"]);
                }
                if (Session["UserId"] != null)
                {
                    strUserId = Session["UserId"].ToString();
                }
                Session["CarrierCode"] = '2';
                olng = new MultilingualManager("DefaultConn", "CKYCRelatedPrsn.aspx", Session["UserLangNum"].ToString());
                strUserId = HttpContext.Current.Session["UserID"].ToString().Trim();
                if (!IsPostBack)
                {
                    SetDataTable();
                    if (Request.QueryString["Status"].ToString() == "reg")
                    {
                        txtDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                        txtDate3.Text = DateTime.Today.ToString("dd-MM-yyyy");
                        BindAttestation();
                        fillcboTitle1();
                        fillcboTitle2();
                        FillCountry();
                        FillddlPageLoad();
                        divIdProof.Visible = false;
                        divAddProof.Visible = false;
                        PopoulateCitizenship();
                        PopulateMaritalStatus();
                        PopulateRelatedPerson();
                        PopulateAddressProofType();
                        PopulateAddressType();
                        PopulateOccupationType();
                        subPopulateGender();
                        subPopulateTitle();
                        PopulateResidentialStatus();
                        PopulateProofIdentiy();
                        //  PopulatePinCode();
                        //added by ramesh
                        Fillcountrycd1();
                        ddlIsoCountry.SelectedValue = "IN";
                        FillStates();
                        //chkSelfCerti.Checked = true;
                    }
                    else if (Request.QueryString["Status"].ToString() == "Mod")
                    {
                        divIdProof.Visible = false;
                        PopoulateCitizenship();
                        PopulateMaritalStatus();
                        PopulateRelatedPerson();
                        PopulateAddressProofType();
                        PopulateAddressType();
                        PopulateOccupationType();
                        subPopulateGender();
                        subPopulateTitle();
                        PopulateResidentialStatus();
                        PopulateProofIdentiy();
                        //PopulatePinCode();
                        Fillcountrycd1();
                        ddlIsoCountry.SelectedValue = "IN";

                        ddlCitizenship_SelectedIndexChanged(this, EventArgs.Empty);
                        FillRelatedPersondata();
                        btnAdd.Visible = true;
                    }
                    else if (Request.QueryString["Status"].ToString() == "View")
                    {
                        //divIdProof.Visible = false;
                        //PopoulateCitizenship();
                        //PopulateMaritalStatus();
                        //PopulateRelatedPerson();
                        //PopulateAddressProofType();
                        //PopulateAddressType();
                        //PopulateOccupationType();
                        //subPopulateGender();
                        //subPopulateTitle();
                        //PopulateResidentialStatus();
                        //PopulateProofIdentiy();
                        ////PopulatePinCode();
                        //Fillcountrycd1();
                        //ddlCitizenship_SelectedIndexChanged(this, EventArgs.Empty);
                        //FillRelatedPersondata();
                        //btnAdd.Visible = false;
                        //btnPartialAdd.Visible = false;
                        fillcboTitle1();
                        fillcboTitle2();
                        FillddlPageLoad();
                        divIdProof.Visible = false;
                        divAddProof.Visible = false;
                        PopoulateCitizenship();
                        PopulateMaritalStatus();
                        PopulateRelatedPerson();
                        PopulateAddressProofType();
                        PopulateAddressType();
                        PopulateOccupationType();
                        subPopulateGender();
                        subPopulateTitle();
                        PopulateResidentialStatus();
                        PopulateProofIdentiy();
                        //PopulatePinCode();
                        Fillcountrycd1();
                        FillCountry();
                        FillStates();
                        ddlIsoCountry.SelectedValue = "IN";
                        ddlCitizenship_SelectedIndexChanged(this, EventArgs.Empty);
                        //FillRelatedPersonPartialdata();
                        FillRelatedPersonViewData();
                        btnPartialAdd.Visible = false;
                        btnAdd.Visible = false;
                        btnPSUpdate.Visible = false;
                        btnUpdate.Visible = false;
                        disablecntrl();

                        BindGridImage();
                        divImg.Visible = true;

                    }
                    else if (Request.QueryString["Status"].ToString() == "PMod")
                    {
                        fillcboTitle1();
                        fillcboTitle2();
                        FillddlPageLoad();
                        divIdProof.Visible = false;
                        divAddProof.Visible = false;
                        PopoulateCitizenship();
                        PopulateMaritalStatus();
                        PopulateRelatedPerson();
                        PopulateAddressProofType();
                        PopulateAddressType();
                        PopulateOccupationType();
                        subPopulateGender();
                        subPopulateTitle();
                        PopulateResidentialStatus();
                        PopulateProofIdentiy();
                        //PopulatePinCode();
                        Fillcountrycd1();
                        FillCountry();
                        FillStates();
                        ddlIsoCountry.SelectedValue = "IN";
                        ddlCitizenship_SelectedIndexChanged(this, EventArgs.Empty);
                        FillRelatedPersonPartialdata();
                        //btnAdd.Visible = false; //Commented By Akash

                        if (Request.QueryString["Action"].ToString() != null)
                        {
                            if (Request.QueryString["Action"].ToString() == "View")
                            {
                                btnPartialAdd.Visible = false;
                                btnAdd.Visible = false;
                            }
                            else if (Request.QueryString["Action"].ToString() == "Edit")
                            {
                                //btnPartialAdd.Visible = false;
                                //btnAdd.Visible = false;
                                //btnPSUpdate.Visible = true;
                                //btnUpdate.Visible = true;

                                btnPartialAdd.Visible = false;
                                btnAdd.Visible = false;
                                btnPSUpdate.Visible = true;
                                btnUpdate.Visible = true;
                            }
                        }
                    }
                    else if (Request.QueryString["Status"].ToString() == "KMod")
                    {
                        divIdProof.Visible = false;
                        PopoulateCitizenship();
                        PopulateMaritalStatus();
                        PopulateAddressProofType();
                        PopulateRelatedPerson();
                        PopulateAddressType();
                        PopulateOccupationType();
                        subPopulateGender();
                        subPopulateTitle();
                        PopulateResidentialStatus();
                        PopulateProofIdentiy();
                        //PopulatePinCode();
                        Fillcountrycd1();
                        ddlCitizenship_SelectedIndexChanged(this, EventArgs.Empty);
                        FillRequiredDataForCKYC();
                        btnAdd.Visible = false;
                    }

                    //FillDistrictState(ddlPinCode, ddlDistrict, ddlState);
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
                        hidePassExpDate.Visible = true;
                        txtPassExpDate.Visible = true;
                        divPass.Visible = true;
                        txtPassOthr.Visible = false;
                        txtPassNo.Visible = true;
                        //txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        //txtPassExpDate.Text = ViewState["strIdExpDate"].ToString();
                        //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNo.MaxLength = 15;
                        txtPassNo.Attributes.Remove("onblur");
                        txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");
                        //txtPassNo.Focus();
                    }
                    else if (ddlProofIdentity.SelectedIndex == 2)
                    {
                        divIdProof.Visible = true;
                        lblPassportNo.Text = "Voter ID Card";
                        llPassExpDate.Visible = false;
                        txtPassExpDate.Visible = false;
                        txtPassOthr.Visible = false;
                        hidePassExpDate.Visible = false;
                        divPass.Visible = false;
                        //txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        txtPassNo.Visible = true;
                        //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNo.MaxLength = 15;
                        txtPassNo.Attributes.Remove("onblur");
                        txtPassNo.Attributes.Add("onblur", "return ValidationVoterId(this)");
                    }
                    else if (ddlProofIdentity.SelectedIndex == 3)
                    {
                        divIdProof.Visible = true;
                        lblPassportNo.Text = "PAN Card";
                        llPassExpDate.Visible = false;
                        txtPassExpDate.Visible = false;
                        hidePassExpDate.Visible = false;
                        txtPassOthr.Visible = false;
                        divPass.Visible = false;
                        txtPassNo.Text = ViewState["strIdNumber"].ToString();
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
                        lblPassportNo.Text = "Driving Licence";
                        llPassExpDate.Text = "Driving Licence Expiry Date";
                        llPassExpDate.Visible = true;
                        hidePassExpDate.Visible = true;
                        txtPassExpDate.Visible = true;
                        txtPassOthr.Visible = false;
                        divPass.Visible = true;
                        txtPassNo.Visible = true;
                        //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNo.MaxLength = 15;
                        txtPassNo.Attributes.Remove("onblur");
                        txtPassNo.Attributes.Add("onblur", "return ValidationDriving(this)");
                    }
                    else if (ddlProofIdentity.SelectedIndex == 5)
                    {
                        divIdProof.Visible = true;
                        lblPassportNo.Text = "Proof of Possession of Aadhaar";
                        llPassExpDate.Visible = false;
                        txtPassExpDate.Visible = false;
                        hidePassExpDate.Visible = false;
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
                        hidePassExpDate.Visible = false;
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
                        //txtPassExpDate.Visible = true;
                        hidePassExpDate.Visible = false;
                        llPassExpDate.Visible = true;
                        divPass.Visible = true;
                        txtPassNo.Text = ViewState["strIdName"].ToString();
                        txtPassOthr.Text = ViewState["strIdNumber"].ToString();
                        txtPassExpDate.Visible = false;
                        txtPassNo.Visible = true;
                        //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNo.MaxLength = 15;
                        txtPassNo.Attributes.Remove("onblur");
                    }
                    else if (ddlProofIdentity.SelectedIndex == 8)
                    {
                        divIdProof.Visible = true;
                        lblPassportNo.Text = "Simplified Measures Account";
                        llPassExpDate.Text = "Identification Number";
                        //txtPassExpDate.Visible = true;
                        hidePassExpDate.Visible = false;
                        divPass.Visible = true;
                        txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        txtPassOthr.Text = ViewState["strSamDocNum"].ToString();
                        txtPassExpDate.Visible = false;
                        txtPassNo.Visible = true;
                        //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNo.MaxLength = 15;
                        txtPassNo.Attributes.Remove("onblur");
                    }
                    else
                    {
                        //divIdProof.Visible = true;
                        //lblPassportNo.Text = "Simplified Measures Account";
                        //llPassExpDate.Text = "Identification Number";
                        //txtPassExpDate.Visible = true;
                        //llPassExpDate.Visible = true;
                        //divPass.Visible = true;
                        //txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        //txtPassOthr.Text = ViewState["strIdExpDate"].ToString();
                        //txtPassExpDate.Visible = false;
                        //txtPassNo.Visible = true;
                        ////FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        ////FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        //txtPassNo.MaxLength = 15;
                        //txtPassNo.Attributes.Remove("onblur");
                    }

                    if (ddlProofOfAddress.SelectedIndex == 0)
                    {
                        divAddProof.Visible = false;
                    }
                    else if (ddlProofOfAddress.SelectedIndex == 1)
                    {
                        //divAddProof.Visible = true;
                        lblPassportNoAdd.Text = "Passport Number";
                        llPassExpDateAdd.Text = "Passport Expiry Date";
                        llPassExpDateAdd.Visible = true;
                        txtPassExpDateAdd.Visible = true;
                        hidetxtPassExpDateAdd.Visible = true;
                        divPassAdd.Visible = true;
                        txtPassOthrAdd.Visible = false;
                        txtPassNoAdd.Visible = true;
                        //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNoAdd.MaxLength = 15;
                        txtPassNoAdd.Attributes.Remove("onblur");
                        txtPassNoAdd.Attributes.Add("onblur", "return ValidationPassport(this)");
                        //txtPassNo.Attributes.Add("onblur", "return ValidatePassport(" + txtPassNo.Text.Trim().ToString() + ")");
                    }
                    else if (ddlProofOfAddress.SelectedIndex == 2)
                    {
                        //divAddProof.Visible = true;
                        lblPassportNoAdd.Text = "Driving Licence";
                        llPassExpDateAdd.Text = "Driving Licence Expiry Date";
                        llPassExpDateAdd.Visible = true;
                        txtPassExpDateAdd.Visible = true;
                        hidetxtPassExpDateAdd.Visible = true;
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
                        //divAddProof.Visible = true;
                        lblPassportNoAdd.Text = "Proof of Possession of Aadhaar";
                        llPassExpDateAdd.Visible = false;
                        txtPassExpDateAdd.Visible = false;
                        hidetxtPassExpDateAdd.Visible = false;
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
                        //divAddProof.Visible = true;
                        lblPassportNoAdd.Text = "Voter ID Card";
                        llPassExpDateAdd.Visible = false;
                        txtPassExpDateAdd.Visible = false;
                        txtPassOthrAdd.Visible = false;
                        hidetxtPassExpDateAdd.Visible = false;
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
                        //divAddProof.Visible = true;
                        lblPassportNoAdd.Text = "NREGA Job Card";
                        llPassExpDateAdd.Visible = false;
                        txtPassNoAdd.Visible = true;
                        txtPassExpDateAdd.Visible = false;
                        hidetxtPassExpDateAdd.Visible = false;
                        txtPassOthrAdd.Visible = false;
                        divPassAdd.Visible = false;
                        //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNoAdd.MaxLength = 20;
                        txtPassNoAdd.Attributes.Remove("onblur");
                    }
                    else
                    {
                        //divAddProof.Visible = true;
                        lblPassportNoAdd.Text = "Document Name";
                        llPassExpDateAdd.Text = "Identification Number";
                        txtPassExpDateAdd.Visible = false;
                        hidetxtPassExpDateAdd.Visible = false;
                        llPassExpDateAdd.Visible = true;
                        divPassAdd.Visible = true;
                        llPassExpDateAdd.Visible = true;
                        //txtPassExpDateAdd.Visible = false;
                        txtPassOthrAdd.Visible = true;
                        txtPassNoAdd.Visible = true;
                        //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNoAdd.MaxLength = 15;
                        txtPassNoAdd.Attributes.Remove("onblur");
                    }
                    txtDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                    txtDate3.Text = DateTime.Today.ToString("dd-MM-yyyy");
                    //btnAdd.Attributes.Add("onClick", "javascript: return funCKYC();");
                    txtPinCode.Attributes.Add("readonly", "readonly");
                    ddlPinCode1.Attributes.Add("readonly", "readonly");
                    ddlPinCode2.Attributes.Add("readonly", "readonly");
                    txtDistrictname.Attributes.Add("readonly", "readonly");
                    txtDistrict1.Attributes.Add("readonly", "readonly");
                    txtDistrict2.Attributes.Add("readonly", "readonly");
                    txtDOB.Attributes.Add("readonly", "readonly");
                    txtPassExpDate.Attributes.Add("readonly", "readonly");
                    txtPassExpDateAdd.Attributes.Add("readonly", "readonly");
                    txtDate.Attributes.Add("readonly", "readonly");
                    txtDate3.Attributes.Add("readonly", "readonly");

                    if (Request.QueryString["Status"].ToString() != "View")
                    {
                        divImg.Visible = false;
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

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

                    ddlState2.DataSource = dt;
                    ddlState2.DataTextField = "STATE_Desc";
                    ddlState2.DataValueField = "STATE_CODE";
                    ddlState2.DataBind();
                    ddlState2.Items.Insert(0, new ListItem("Select", string.Empty));

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


        #region DROPDOWN 'ddlProofIdentity' SELECTEDINDEXCHANGED EVENT
        protected void ddlProofIdentity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox1.Checked = false;
                divAddProof.Visible = false;
                divPassAdd.Visible = false;
                ddlProofOfAddress.Enabled = true;
                ddlProofOfAddress.ClearSelection();
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
                    hidePassExpDate.Visible = true;
                    divPass.Visible = true;
                    txtPassOthr.Visible = false;
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 15;
                    txtPassNo.Attributes.Remove("onblur");
                    txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");
                    //txtPassNo.Attributes.Add("onblur", "return ValidatePassport(" + txtPassNo.Text.Trim().ToString() + ")");
                }
                else if (ddlProofIdentity.SelectedIndex == 2)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Voter ID Card";
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    hidePassExpDate.Visible = false;
                    txtPassOthr.Visible = false;
                    divPass.Visible = false;
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 15;
                    txtPassNo.Attributes.Remove("onblur");
                    txtPassNo.Attributes.Add("onblur", "return ValidationVoterId(this)");
                }
                else if (ddlProofIdentity.SelectedIndex == 3)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "PAN Card";
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    hidePassExpDate.Visible = false;
                    txtPassOthr.Visible = false;
                    divPass.Visible = false;
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 10;
                    //txtPassNo.Attributes.Remove("onblur");
                    txtPassNo.Attributes.Add("onblur", "return fnValidatePAN(this)");
                }
                else if (ddlProofIdentity.SelectedIndex == 4)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Driving Licence";
                    llPassExpDate.Text = "Driving Licence Expiry Date";
                    llPassExpDate.Visible = true;
                    txtPassExpDate.Visible = true;
                    hidePassExpDate.Visible = true;
                    txtPassOthr.Visible = false;
                    divPass.Visible = true;
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 15;
                    txtPassNo.Attributes.Remove("onblur");
                    txtPassNo.Attributes.Add("onblur", "return ValidationDriving(this)");

                }
                else if (ddlProofIdentity.SelectedIndex == 5)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Proof of Possession of Aadhaar";
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    hidePassExpDate.Visible = false;
                    txtPassOthr.Visible = false;
                    divPass.Visible = false;
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                    txtPassNo.MaxLength = 12;
                    txtPassNo.Text = "";
                    txtPassNo.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                }
                else if (ddlProofIdentity.SelectedIndex == 6)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "NREGA Job Card";
                    llPassExpDate.Visible = false;
                    txtPassNo.Visible = true;
                    txtPassExpDate.Visible = false;
                    txtPassOthr.Visible = false;
                    hidePassExpDate.Visible = false;
                    divPass.Visible = false;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 20;
                    txtPassNo.Attributes.Remove("onblur");
                }
                else if (ddlProofIdentity.SelectedIndex == 7)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Document Name";
                    llPassExpDate.Text = "Identification Number";
                    //txtPassExpDate.Visible = true;
                    llPassExpDate.Visible = true;
                    divPass.Visible = true;
                    llPassExpDate.Visible = true;
                    txtPassExpDate.Visible = false;
                    hidePassExpDate.Visible = false;
                    txtPassOthr.Visible = true;
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 15;
                    txtPassNo.Attributes.Remove("onblur");
                }
                else if (ddlProofIdentity.SelectedIndex == 8)
                {

                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Simplified Measures Account";
                    llPassExpDate.Text = "Identification Number";
                    //txtPassExpDate.Visible = true;
                    llPassExpDate.Visible = true;
                    divPass.Visible = true;
                    txtPassExpDate.Visible = false;
                    hidePassExpDate.Visible = false;
                    txtPassOthr.Visible = true;
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 15;
                    txtPassNo.Attributes.Remove("onblur");
                }
                else
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Simplified Measures Account";
                    llPassExpDate.Text = "Identification Number";
                    //txtPassExpDate.Visible = true;
                    llPassExpDate.Visible = true;
                    divPass.Visible = true;
                    txtPassExpDate.Visible = false;
                    hidePassExpDate.Visible = false;
                    txtPassOthr.Visible = true;
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 15;
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "ddlProofIdentity_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
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
                    //divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Passport Number";
                    llPassExpDateAdd.Text = "Passport Expiry Date";
                    llPassExpDateAdd.Visible = true;
                    hidetxtPassExpDateAdd.Visible = true;
                    txtPassExpDateAdd.Visible = true;
                    divPassAdd.Visible = true;
                    txtPassOthrAdd.Visible = false;
                    txtPassNoAdd.Visible = true;
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd.MaxLength = 8;
                    txtPassNoAdd.Attributes.Remove("onblur");
                    txtPassNoAdd.Attributes.Add("onblur", "return ValidationPassport(this)");
                    //txtPassNo.Attributes.Add("onblur", "return ValidatePassport(" + txtPassNo.Text.Trim().ToString() + ")");
                }
                else if (ddlProofOfAddress.SelectedIndex == 2)
                {
                    //divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Driving Licence";
                    llPassExpDateAdd.Text = "Driving Licence Expiry Date";
                    llPassExpDateAdd.Visible = true;
                    hidetxtPassExpDateAdd.Visible = true;
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
                    //divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Proof of Possession of Aadhaar";
                    llPassExpDateAdd.Visible = false;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;
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
                    //divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Voter ID Card";
                    llPassExpDateAdd.Visible = false;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;
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
                    //divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "NREGA Job Card";
                    llPassExpDateAdd.Visible = false;
                    txtPassNoAdd.Visible = true;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;

                    txtPassOthrAdd.Visible = false;
                    divPassAdd.Visible = false;
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd.MaxLength = 20;
                    txtPassNoAdd.Attributes.Remove("onblur");
                }
                else
                {
                    //divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Document Name";
                    llPassExpDateAdd.Text = "Identification Number";
                    //txtPassExpDateAdd.Visible = true;
                    llPassExpDateAdd.Visible = true;
                    divPassAdd.Visible = true;
                    llPassExpDateAdd.Visible = true;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "ddlProofOfAddress_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        protected void cboTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTitle.SelectedIndex == 0)
                {
                    subPopulateGender();
                    PopulateMaritalStatus();
                }
                else
                {
                    htParam.Clear();
                    htParam.Add("@Flag", "Gender");
                    htParam.Add("@value", cboTitle.SelectedValue);
                    FillDropdowns("prc_FillMarGender", htParam, cboGender, "CKYCConnectionString", true);

                    htParam.Clear();
                    htParam.Add("@Flag", "Marital");
                    htParam.Add("@value", cboTitle.SelectedValue);
                    FillDropdowns("prc_FillMarGender", htParam, ddlMaritalStatus, "CKYCConnectionString", true);
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "cboTitle_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        #region METHOD "FillRequiredDataForCndPersonal"
        protected void FillRequiredDataForCKYC()
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                htParam.Clear();
                htParam.Add("@RegRefNo", Request.QueryString["refno"].ToString());
                dt = objDAL.GetDataTable("getSearchData_Web", htParam);
                txtKYCNum.Text = Convert.ToString(dt.Rows[0]["RLT_KYC_NUMBER"]);
                ddlRelType.SelectedValue = Convert.ToString(dt.Rows[0]["RTL_PERSONTYPE"]);
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
                ddlMaritalStatus.SelectedValue = Convert.ToString(dt.Rows[0]["MARITAL_STATUS"]);
                cboGender.SelectedValue = Convert.ToString(dt.Rows[0]["GENDER"]);
                ddlCitizenship.SelectedValue = Convert.ToString(dt.Rows[0]["CITIZENSHIP"]);
                ddlResStatus.SelectedValue = Convert.ToString(dt.Rows[0]["RESI_STATUS"]);
                ddlOccupation.SelectedValue = Convert.ToString(dt.Rows[0]["OCCU_TYPE"]);
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
                ddlAddressType.SelectedValue = Convert.ToString(dt.Rows[0]["PER_ADDTYPE"]);
                ddlProofOfAddress.SelectedValue = Convert.ToString(dt.Rows[0]["PER_ADDPROOF"]);
                txtAddressLine1.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE1"]);
                txtAddressLine2.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE2"]);
                txtAddressLine3.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE3"]);
                txtCity.Text = Convert.ToString(dt.Rows[0]["PER_CITY"]);
                txtDistrictname.Text = Convert.ToString(dt.Rows[0]["PER_DISTRICT"]);
                txtPinCode.Text = Convert.ToString(dt.Rows[0]["PER_PIN"]);
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
                txtPlace.Text = Convert.ToString(dt.Rows[0]["PLACE"]);
                txtDate.Text = Convert.ToString(dt.Rows[0]["APP_DATE"]);

                txtEmpName.Text = Convert.ToString(dt.Rows[0]["kycEmpName"]);
                txtEmpCode.Text = Convert.ToString(dt.Rows[0]["kycEmpCode"]);
                txtEmpDesignation.Text = Convert.ToString(dt.Rows[0]["kycEmpDesi"]);
                txtEmpBranch.Text = Convert.ToString(dt.Rows[0]["kycEmpBranch"]);
                txtInsName.Text = Convert.ToString(dt.Rows[0]["kycInstName"]);
                txtInsCode.Text = Convert.ToString(dt.Rows[0]["kycInstCode"]);
                txtDate3.Text = Convert.ToString(dt.Rows[0]["kycVerDate"]);

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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "FillRequiredDataForCKYC", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;
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
                //FillDistrictState(ddlPinCode, ddlDistrict, ddlState);
                ddlCountryCode.SelectedValue = "IN";

                if (ddlAddressType.SelectedIndex != 0)
                {
                    if (chkPerAddress.Checked == false)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please check current/permanent/overseas address details')", true);
                        txtPinCode.Text = "";
                        return;
                    }
                }
                if (ddlAddressType.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select address type')", true);
                    txtPinCode.Text = "";
                    return;
                }
                if (ddlProofOfAddress.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select proof of address')", true);
                    txtPinCode.Text = "";
                    return;
                }
                if (ddlProofOfAddress.SelectedIndex != 0)
                {
                    if (ddlProofOfAddress.SelectedIndex == 1)
                    {
                        if (txtPassNoAdd.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please enter passport number')", true);
                            txtPinCode.Text = "";
                            return;
                        }
                        if (txtPassExpDateAdd.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please enter passport expiry date')", true);
                            txtPinCode.Text = "";
                            return;
                        }
                        if (txtPassExpDateAdd.Text != "")
                        {
                            DateTime date1, date2;
                            date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            date2 = DateTime.ParseExact(txtPassExpDateAdd.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            if (date1 > date2)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('You cannot select past date as driving license expiry date')", true);
                                txtPinCode.Text = "";
                                return;
                            }
                        }
                    }
                    if (ddlProofOfAddress.SelectedIndex == 2)
                    {
                        if (txtPassNoAdd.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please enter driving licence number')", true);
                            txtPinCode.Text = "";
                            return;
                        }
                        if (txtPassExpDateAdd.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please enter driving licence expiry date')", true);
                            txtPinCode.Text = "";
                            return;
                        }
                        if (txtPassExpDateAdd.Text != "")
                        {
                            DateTime date1, date2;
                            date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            date2 = DateTime.ParseExact(txtPassExpDateAdd.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            if (date1 > date2)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('You cannot select past date as driving license expiry date')", true);
                                txtPinCode.Text = "";
                                return;
                            }
                        }
                    }

                    if (ddlProofOfAddress.SelectedIndex == 3)
                    {
                        if (txtPassNoAdd.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please enter Proof of Possession of Aadhaar')", true);
                            txtPinCode.Text = "";
                            return;
                        }
                    }
                    if (ddlProofOfAddress.SelectedIndex == 4)
                    {
                        if (txtPassNoAdd.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please enter voter id card')", true);
                            txtPinCode.Text = "";
                            return;
                        }
                    }
                    if (ddlProofOfAddress.SelectedIndex == 5)
                    {
                        if (txtPassNoAdd.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please enter NREGA job card')", true);
                            txtPinCode.Text = "";
                            return;
                        }
                    }
                    if (ddlProofOfAddress.SelectedIndex == 6)
                    {
                        if (txtPassNoAdd.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please enter other no of proof of Address')", true);
                            txtPinCode.Text = "";
                            return;
                        }
                    }
                }
                if (txtAddressLine1.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please enter permanent address line 1')", true);
                    txtPinCode.Text = "";
                    return;
                }
                if (txtCity.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please enter permanent city/town/village')", true);
                    txtPinCode.Text = "";
                    return;
                }
                if (txtPinCode.Text == "" && chkTick.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please enter permanent pin/post code')", true);
                    txtPinCode.Text = "";
                    return;
                }
                chkPerAddress.Enabled = false;
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "ddlPinCode_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region METHOD 'POPULATE DROPDOWNLIST'
        private void subPopulateTitle()
        {
            try
            {
                oCommonUtility.GetCKYC(cboTitle, "KTitle");
                cboTitle.Items.Insert(0, new ListItem("Select", ""));
                //oCommonUtility.GetCKYC(cboTitle1, "KTitle");
                //cboTitle1.Items.Insert(0, new ListItem("Select", ""));
                //oCommonUtility.GetCKYC(cboTitle2, "KTitle");
                //cboTitle2.Items.Insert(0, new ListItem("Select", ""));
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "subPopulateTitle", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region subPopulateGender
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "subPopulateGender", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region PopulateMaritalStatus
        private void PopulateMaritalStatus()
        {
            try
            {
                oCommonUtility.GetCKYC(ddlMaritalStatus, "KMstatus");
                ddlMaritalStatus.Items.Insert(0, new ListItem("Select", ""));
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "PopulateMaritalStatus", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region MyRegion
        private void PopoulateCitizenship()
        {
            try
            {
                oCommonUtility.GetCKYC(ddlCitizenship, "KCitizn");
                ddlCitizenship.Items.Insert(0, new ListItem("Select", ""));
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "PopoulateCitizenship", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region PopulateResidentialStatus
        private void PopulateResidentialStatus()
        {
            try
            {
                oCommonUtility.GetCKYC(ddlResStatus, "KResi");
                ddlResStatus.Items.Insert(0, new ListItem("Select", ""));
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "PopulateResidentialStatus", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region PopulateOccupationType
        private void PopulateOccupationType()
        {
            try
            {
                oCommonUtility.GetCKYC(ddlOccupation, "KOcc");
                ddlOccupation.Items.Insert(0, new ListItem("Select", ""));
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "PopulateOccupationType", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region PopulateProofIdentiy
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "PopulateProofIdentiy", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region PopulateAddressType
        private void PopulateAddressType()
        {
            try
            {
                oCommonUtility.GetCKYC(ddlAddressType, "KAddr");//"KAddr"
                ddlAddressType.Items.Insert(0, new ListItem("Select", ""));
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "PopulateAddressType", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region PopulateAddressProofType
        private void PopulateAddressProofType()
        {
            try
            {
                oCommonUtility.GetCKYC(ddlProofOfAddress, "KAddrPrf");//KAddrPrf
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "PopulateAddressProofType", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion



        #region Add
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            { 
                if (txtKYCNum.Text.Trim().Length != 14)
                {

                    string Res;
                    Res = objVal.EntityRelatedPrsnValidation(ddlRelType, cboTitle, txtGivenName, txtMiddleName, txtLastName, cboTitle2, rbtFS,
                    txtGivenName2, txtMiddleName2, txtLastName2, cboTitle3, txtGivenName3, txtLastName3, txtDOB, cboGender, txtPanNo, ddlIsoCountryCodeOthr,
                    ddlProofIdentity, txtPassNo, txtPassExpDate, txtPassOthr,
                     chkAppDeclare1, ddlProofOfAddress, txtAddressLine1, txtCity, txtPinCode, txtDate3, txtDate,
                    txtEmpName, txtEmpCode, txtEmpDesignation, txtEmpBranch, txtInsName, txtInsCode, txtPassExpDateAdd, txtPassNoAdd, txtPlace,
                   ddlDocReceived, ddlState, txtPassOthrAdd, txtLocAddLine1, txtCity1, ddlState1,
                    ddlPinCode1, ddlCountryCode1, ddlProofOfAddress, txtPassNoAdd, txtRelRefNumber,
                    ddlDocReceived, chkCuurentAddress, ddlDocReceived, ddlNationality, txtNum,
                     txtTelOff2, txtTelOff, txtTelRes, txtTelRes2, txtMobile, txtMobile2, txtemail, ddlCountryCode,"");//chkHigh, chkMedium, chkLow,ddlProofOfAddress1,
                    if (Res.Equals(""))
                    {
                        if (txtDOB.Text != "")
                        {
                            string date;
                            date = DateTime.Today.ToString("dd\\-MM\\-yyyy");
                            DateTime date1, date2;
                            date1 = DateTime.ParseExact(date.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            date2 = DateTime.ParseExact(txtDOB.Text.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            if (date1 < date2)
                            {
                                //if (Convert.ToDateTime(date) < Convert.ToDateTime(txtDOB.Text))
                                //{
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You cannot select future date')", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('You cannot select future date')", true);
                                return;
                            }
                        }

                        if (Session["dsRel"] != null)
                        {
                            DtAdd = (DataTable)Session["dsRel"];
                        }
                        else
                        {
                            DtAdd = (DataTable)ViewState["DtAdd"];
                        }

                        DataRow dataRow = DtAdd.NewRow();
                        dataRow["FiRefNo"] = txtRefNumber.Text.Trim();
                        dataRow["RelRefNo"] = txtRelRefNumber.Text.Trim();
                        dataRow["RelatedPrsnKYCNo"] = txtKYCNum.Text.Trim();
                        dataRow["RelationType"] = ddlRelType.SelectedValue;
                        dataRow["PrefixRel"] = cboTitle.SelectedValue;
                        dataRow["FNameRel"] = txtGivenName.Text.Trim();
                        dataRow["MNameRel"] = txtMiddleName.Text.Trim();
                        dataRow["LNameRel"] = txtLastName.Text.Trim();
                        dataRow["MaidPrefixRel"] = cboTitle1.SelectedValue;
                        dataRow["MaidFNameRel"] = txtGivenName1.Text.Trim();
                        dataRow["MaidMNameRel"] = txtMiddleName1.Text.Trim();
                        dataRow["MaidLNameRel"] = txtLastName1.Text.Trim();

                        //ddlIsoCountryCode2.SelectedValue

                        if (rbtFS.SelectedValue == "F")
                        {
                            dataRow["FSFlagRel"] = "01";
                        }
                        else
                        {
                            dataRow["FSFlagRel"] = "02";
                        }
                        dataRow["FatherPrefixRel"] = cboTitle2.SelectedValue;
                        dataRow["FatherFNameRel"] = txtGivenName2.Text.Trim();
                        dataRow["FatherMNameRel"] = txtMiddleName2.Text.Trim();
                        dataRow["FatherLNameRel"] = txtLastName2.Text.Trim();
                        dataRow["MotherPrefixRel"] = cboTitle3.SelectedValue;
                        dataRow["MotherFNameRel"] = txtGivenName3.Text;
                        dataRow["MotherMNameRel"] = txtMiddleName2.Text;
                        dataRow["MotherLNameRel"] = txtLastName3.Text;
                        dataRow["DOBRel"] = txtDOB.Text;
                        dataRow["GenderRel"] = cboGender.SelectedValue;
                        dataRow["MaritalStatusRel"] = ddlMaritalStatus.SelectedValue;
                        dataRow["CitizenshipRel"] = ddlCitizenship.SelectedValue;
                        dataRow["ResiStatusRel"] = ddlResStatus.SelectedValue;

                        //---to show on grid
                        if (ddlRelType.SelectedIndex != 0)
                        {
                            dataRow["RelationTypetxt"] = ddlRelType.SelectedItem.Text;
                        }
                        else
                        {
                            dataRow["RelationTypetxt"] = "";
                        }

                        if (cboGender.SelectedIndex != 0)
                        {
                            dataRow["GenderReltxt"] = cboGender.SelectedItem.Text;
                        }
                        else
                        {
                            dataRow["GenderReltxt"] = "";
                        }

                        //dataRow["GenderReltxt"] = cboGender.SelectedItem.Text;

                        if (ddlMaritalStatus.SelectedIndex != 0)
                        {
                            dataRow["MaritalStatusReltxt"] = ddlMaritalStatus.SelectedItem.Text;
                        }
                        else
                        {
                            dataRow["MaritalStatusReltxt"] = "";
                        }
                        //dataRow["MaritalStatusReltxt"] = ddlMaritalStatus.SelectedItem.Text;

                        if (ddlCitizenship.SelectedIndex != 0)
                        {
                            dataRow["CitizenshipReltxt"] = ddlCitizenship.SelectedItem.Text;
                        }
                        else
                        {
                            dataRow["CitizenshipReltxt"] = "";
                        }
                        //dataRow["CitizenshipReltxt"] = ddlCitizenship.SelectedItem.Text;

                        if (ddlResStatus.SelectedIndex != 0)
                        {
                            dataRow["ResiStatusReltxt"] = ddlResStatus.SelectedItem.Text;
                        }
                        else
                        {
                            dataRow["ResiStatusReltxt"] = "";
                        }
                        //dataRow["ResiStatusReltxt"] = ddlResStatus.SelectedItem.Text;

                        if (ddlOccuSubType.SelectedIndex != 0)
                        {
                            dataRow["OccuTypeReltxt"] = ddlOccuSubType.SelectedItem.Text;
                        }
                        else
                        {
                            dataRow["OccuTypeReltxt"] = "";
                        }
                        //dataRow["OccuTypeReltxt"] = ddlOccuSubType.SelectedItem.Text;

                        //---end to show on grid

                        dataRow["OccuTypeRel"] = ddlOccupation.SelectedValue;


                        if (chkTick.Checked == true)
                        {
                            dataRow["ResForTaxFlagRel"] = "01";
                        }
                        else
                        {
                            dataRow["ResForTaxFlagRel"] = "02";
                        }
                        dataRow["ISOCountryCodeRel"] = ddlIsoCountryCode2.SelectedValue;
                        dataRow["TaxIDNumberRel"] = txtIDResTax.Text.Trim();
                        dataRow["BirthCityRel"] = txtDOBRes.Text.Trim();
                        dataRow["ISOBirthPlaceCodeRel"] = ddlIsoCountry.SelectedValue;
                        dataRow["IdType"] = ddlProofIdentity.SelectedValue;
                        if (ddlProofIdentity.SelectedIndex == 1)
                        {
                            dataRow["IdNumber"] = txtPassNo.Text.Trim();
                            dataRow["IdExpDate"] = txtPassExpDate.Text.Trim();
                            dataRow["IdName"] = System.DBNull.Value;

                        }
                        else if (ddlProofIdentity.SelectedIndex == 2)
                        {
                            dataRow["IdNumber"] = txtPassNo.Text.Trim();
                            dataRow["IdExpDate"] = System.DBNull.Value;
                            dataRow["IdName"] = System.DBNull.Value;

                        }
                        else if (ddlProofIdentity.SelectedIndex == 3)
                        {
                            dataRow["IdNumber"] = txtPassNo.Text.Trim();
                            dataRow["IdExpDate"] = System.DBNull.Value;
                            dataRow["IdName"] = System.DBNull.Value;
                        }
                        else if (ddlProofIdentity.SelectedIndex == 4)
                        {
                            dataRow["IdNumber"] = txtPassNo.Text.Trim();
                            dataRow["IdExpDate"] = txtPassExpDate.Text.Trim();
                            dataRow["IdName"] = System.DBNull.Value;
                        }
                        else if (ddlProofIdentity.SelectedIndex == 5)
                        {
                            dataRow["IdNumber"] = txtPassNo.Text.Trim();
                            dataRow["IdExpDate"] = System.DBNull.Value;
                            dataRow["IdName"] = System.DBNull.Value;
                        }
                        else if (ddlProofIdentity.SelectedIndex == 6)
                        {
                            dataRow["IdNumber"] = txtPassNo.Text.Trim();
                            dataRow["IdExpDate"] = System.DBNull.Value;
                            dataRow["IdName"] = System.DBNull.Value;
                        }
                        else if (ddlProofIdentity.SelectedIndex == 7)
                        {
                            dataRow["IdNumber"] = txtPassOthr.Text.Trim();
                            dataRow["IdExpDate"] = System.DBNull.Value;
                            dataRow["IdName"] = txtPassNo.Text.Trim();
                        }
                        else if (ddlProofIdentity.SelectedIndex == 8 || ddlProofIdentity.SelectedIndex == 9)
                        {
                            dataRow["IdNumber"] = txtPassNo.Text.Trim();
                            dataRow["IdExpDate"] = System.DBNull.Value;
                            dataRow["IdName"] = System.DBNull.Value;
                        }
                        else
                        {
                            dataRow["IdNumber"] = System.DBNull.Value;
                            dataRow["IdExpDate"] = System.DBNull.Value;
                            dataRow["IdName"] = System.DBNull.Value;
                        }

                        if (CheckBox1.Checked == true)
                        {
                            dataRow["SameasPOIAddresFlagP1"] = "01";
                            // htParam.Add("@SameasPOIAddresFlagP1", "01");
                        }
                        else
                        {
                            dataRow["SameasPOIAddresFlagP1"] = "";
                        }

                        if (chkPerAddress.Checked == true)
                        {
                            dataRow["CnctTypeRel"] = "P1";
                            dataRow["AdrTypeRel"] = ddlAddressType.SelectedValue;
                            dataRow["AdrProfRel"] = ddlProofOfAddress.SelectedValue;
                            dataRow["Adr1Rel"] = txtAddressLine1.Text.Trim();
                            dataRow["Adr2Rel"] = txtAddressLine2.Text.Trim();
                            dataRow["Adr3Rel"] = txtAddressLine3.Text.Trim();
                            dataRow["CityRel"] = txtCity.Text.Trim();
                            dataRow["DistrictRel"] = txtDistrictname.Text;
                            dataRow["PostCodeRel"] = txtPinCode.Text;
                            dataRow["StateCodeRel"] = ddlState.SelectedValue;
                            dataRow["CntryCodeRel"] = ddlCountryCode.SelectedValue;
                        }
                        else
                        {
                            dataRow["CnctTypeRel"] = "";
                            dataRow["AdrTypeRel"] = System.DBNull.Value;
                            dataRow["AdrProfRel"] = System.DBNull.Value;
                            dataRow["Adr1Rel"] = System.DBNull.Value;
                            dataRow["Adr2Rel"] = System.DBNull.Value;
                            dataRow["Adr3Rel"] = System.DBNull.Value;
                            dataRow["CityRel"] = System.DBNull.Value;
                            dataRow["DistrictRel"] = System.DBNull.Value;
                            dataRow["PostCodeRel"] = System.DBNull.Value;
                            dataRow["StateCodeRel"] = System.DBNull.Value;
                            dataRow["CntryCodeRel"] = System.DBNull.Value;
                        }

                        if (chkCuurentAddress.Checked == true)
                        {
                            dataRow["SameasCurrentAddresFlagM1"] = "01";
                            //htParam.Add("@SameasCurrentAddresFlagM1", "01");
                        }
                        else
                        {
                            dataRow["SameasCurrentAddresFlagM1"] = "";
                        }

                        if (chkLocalAddress.Checked == true)
                        {
                            dataRow["CnctTypeRel1"] = "M1";
                            dataRow["AdrTypeRel1"] = ddlAddressType1.SelectedValue;
                            //dataRow["AdrProfRel1"] = ddlProofOfAddress.SelectedValue;
                            dataRow["Adr1Rel1"] = txtLocAddLine1.Text.Trim();
                            dataRow["Adr2Rel1"] = txtLocAddLine2.Text.Trim();
                            dataRow["Adr3Rel1"] = txtLocAddLine3.Text.Trim();
                            dataRow["CityRel1"] = txtCity1.Text.Trim();
                            dataRow["DistrictRel1"] = txtDistrict1.Text;
                            dataRow["PostCodeRel1"] = ddlPinCode1.Text;
                            dataRow["StateCodeRel1"] = ddlState1.SelectedValue;
                            dataRow["CntryCodeRel1"] = ddlCountryCode1.SelectedValue;
                        }
                        else
                        {
                            dataRow["CnctTypeRel1"] = "";
                            dataRow["AdrTypeRel1"] = System.DBNull.Value;
                            //dataRow["AdrProfRel1"] = System.DBNull.Value;
                            dataRow["Adr1Rel1"] = System.DBNull.Value;
                            dataRow["Adr2Rel1"] = System.DBNull.Value;
                            dataRow["Adr3Rel1"] = System.DBNull.Value;
                            dataRow["CityRel1"] = System.DBNull.Value;
                            dataRow["DistrictRel1"] = System.DBNull.Value;
                            dataRow["PostCodeRel1"] = System.DBNull.Value;
                            dataRow["StateCodeRel1"] = System.DBNull.Value;
                            dataRow["CntryCodeRel1"] = System.DBNull.Value;
                        }

                        if (chkCurrentAdd.Checked == true)
                        {
                            dataRow["SameasLocalAddressFlagJ1"] = "01";
                            //htParam.Add("@SameasLocalAddressFlagJ1", "01");
                        }
                        else
                        {
                            dataRow["SameasLocalAddressFlagJ1"] = "";
                        }

                        if (chkCorresAdd.Checked == true)
                        {
                            dataRow["SameasLocalAddressFlagJ2"] = "01";
                            // htParam.Add("@SameasLocalAddressFlagJ2", "01");
                        }
                        else
                        {
                            dataRow["SameasLocalAddressFlagJ2"] = "";
                        }

                        if (chkAddResident.Checked == true)
                        {
                            dataRow["CnctTypeRel2"] = "J1";
                            dataRow["AdrTypeRel2"] = ddlAddressType2.SelectedValue;
                            //dataRow["AdrProfRel1"] = ddlProofOfAddress.SelectedValue;
                            dataRow["Adr1Rel2"] = txtAddLine1.Text.Trim();
                            dataRow["Adr2Rel2"] = txtAddLine2.Text.Trim();
                            dataRow["Adr3Rel2"] = txtAddLine3.Text.Trim();
                            dataRow["CityRel2"] = txtCity2.Text.Trim();
                            dataRow["DistrictRel2"] = txtDistrict2.Text;
                            dataRow["PostCodeRel2"] = ddlPinCode2.Text;
                            dataRow["StateCodeRel2"] = ddlState2.SelectedValue;
                            dataRow["CntryCodeRel2"] = ddlIsoCountryCode.SelectedValue;
                        }
                        else
                        {
                            dataRow["CnctTypeRel2"] = "";
                            dataRow["AdrTypeRel2"] = System.DBNull.Value;
                            //dataRow["AdrProfRel1"] = System.DBNull.Value;
                            dataRow["Adr1Rel2"] = System.DBNull.Value;
                            dataRow["Adr2Rel2"] = System.DBNull.Value;
                            dataRow["Adr3Rel2"] = System.DBNull.Value;
                            dataRow["CityRel2"] = System.DBNull.Value;
                            dataRow["DistrictRel2"] = System.DBNull.Value;
                            dataRow["PostCodeRel2"] = System.DBNull.Value;
                            dataRow["StateCodeRel2"] = System.DBNull.Value;
                            dataRow["CntryCodeRel2"] = System.DBNull.Value;
                        }

                        dataRow["AddIdTypeRel"] = ddlProofOfAddress.SelectedValue;
                        if (chkPerAddress.Checked == true)
                        {
                            if (ddlProofOfAddress.SelectedIndex == 1)
                            {
                                dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                                dataRow["AddIdExpDateRel"] = txtPassExpDateAdd.Text.Trim();
                                dataRow["AddIdNameRel"] = System.DBNull.Value;
                            }

                            else if (ddlProofOfAddress.SelectedIndex == 2)
                            {
                                dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                                dataRow["AddIdExpDateRel"] = txtPassExpDateAdd.Text.Trim();
                                dataRow["AddIdNameRel"] = System.DBNull.Value;
                            }
                            else if (ddlProofOfAddress.SelectedIndex == 3)
                            {
                                dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                                dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                                dataRow["AddIdNameRel"] = System.DBNull.Value;
                            }
                            else if (ddlProofOfAddress.SelectedIndex == 4)
                            {
                                dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                                dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                                dataRow["AddIdNameRel"] = System.DBNull.Value;
                            }
                            else if (ddlProofOfAddress.SelectedIndex == 5)
                            {
                                dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                                dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                                dataRow["AddIdNameRel"] = System.DBNull.Value;
                            }
                            else if (ddlProofOfAddress.SelectedIndex == 6)
                            {
                                dataRow["AddIdNumberRel"] = txtPassOthrAdd.Text.Trim();
                                dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                                dataRow["AddIdNameRel"] = txtPassNoAdd.Text.Trim();
                            }

                            else
                            {
                                dataRow["AddIdNumberRel"] = System.DBNull.Value;
                                dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                                dataRow["AddIdNameRel"] = System.DBNull.Value;
                            }
                        }
                        else
                        {
                            dataRow["AddIdNumberRel"] = System.DBNull.Value;
                            dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                            dataRow["AddIdNameRel"] = System.DBNull.Value;
                        }

                        dataRow["DecDateRel"] = txtDate.Text.Trim();
                        dataRow["DecPlaceRel"] = txtPlace.Text.Trim();
                        dataRow["kycEmpNameRel"] = txtEmpName.Text.Trim();
                        dataRow["kycEmpCodeRel"] = txtEmpCode.Text.Trim();
                        dataRow["kycEmpBranchRel"] = txtEmpBranch.Text.Trim();
                        dataRow["kycEmpDesiRel"] = txtEmpDesignation.Text.Trim();
                        dataRow["kycVerDateRel"] = txtDate3.Text.Trim();

                        dataRow["kycCertDocRel"] = System.DBNull.Value;
                        dataRow["kycInstNameRel"] = txtInsName.Text.Trim();
                        dataRow["kycInstCodeRel"] = txtInsCode.Text.Trim();
                        dataRow["SVFlag"] = "P";

                        //dataRow["CREATEDBY"] =strUserId.ToString();

                        DtAdd.Rows.Add(dataRow);
                        //DataSet dsRel1 = new DataSet();
                        //dsRel1.Clear();
                        //dsRel1.Tables.Add(DtAdd);
                        Session["dsRel"] = DtAdd;
                        lblMsgConfirmYesNo.Text = "Relative Details added successfully....Do you want to add more?.";
                        ClearTextcntrl();
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "AsyncConfirmYesNo();", true);
                        Session["PSSubmit"] = "N";
                    }
                    else
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "AlertMsg('" + Res + "')", true);
                        //     ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsgs", "AlertMsg('" + Res + "')", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + Res + "');", true);
                        return;
                    }
                }
                #region 14 DIGIT KYC NUMBER SAVE FUNCTIONALITY START
                else
                {
                    string Res1;
                    Res1 = objVal.RelatedPrsnValidationAccorKYC(ddlRelType, cboTitle, txtGivenName, txtLastName, cboTitle2, rbtFS,
                    txtGivenName2, txtLastName2, cboTitle3, txtGivenName3, txtLastName3, txtDOB, cboGender,
                   ddlIsoCountryCodeOthr,
                   ddlProofIdentity, txtPassNo, txtPassExpDate, txtPassOthr,
                     chkAppDeclare1, ddlProofOfAddress, txtAddressLine1, txtCity, txtPinCode, txtDate3, txtDate,
                    txtEmpName, txtEmpCode, txtEmpDesignation, txtEmpBranch, txtInsName, txtInsCode, txtPassExpDateAdd, txtPassNoAdd, txtPlace,
                    ddlDocReceived, ddlState, txtPassOthrAdd,txtNum);//chkHigh, chkMedium, chkLow,

                    if (Res1.Equals(""))
                    {
                        DtAdd = (DataTable)ViewState["DtAdd"];
                        DataRow dataRow = DtAdd.NewRow();
                        dataRow["FiRefNo"] = txtRefNumber.Text.Trim();
                        dataRow["RelRefNo"] = txtRelRefNumber.Text.Trim();
                        dataRow["RelatedPrsnKYCNo"] = txtKYCNum.Text.Trim();
                        dataRow["RelationType"] = ddlRelType.SelectedValue;
                        dataRow["PrefixRel"] = cboTitle.SelectedValue;
                        dataRow["FNameRel"] = txtGivenName.Text.Trim();
                        dataRow["MNameRel"] = txtMiddleName.Text.Trim();
                        dataRow["LNameRel"] = txtLastName.Text.Trim();
                        //dataRow["MaidPrefixRel"] = cboTitle1.SelectedValue;
                        //dataRow["MaidFNameRel"] = txtGivenName1.Text.Trim();
                        //dataRow["MaidMNameRel"] = txtMiddleName1.Text.Trim();
                        //dataRow["MaidLNameRel"] = txtLastName1.Text.Trim();

                        //if (rbtFS.SelectedValue == "F")
                        //{
                        //    dataRow["FSFlagRel"] = "01";
                        //}
                        //else
                        //{
                        //    dataRow["FSFlagRel"] = "02";
                        //}
                        //dataRow["FatherPrefixRel"] = cboTitle2.SelectedValue;
                        //dataRow["FatherFNameRel"] = txtGivenName2.Text.Trim();
                        //dataRow["FatherMNameRel"] = txtMiddleName2.Text.Trim();
                        //dataRow["FatherLNameRel"] = txtLastName2.Text.Trim();
                        //dataRow["MotherPrefixRel"] = cboTitle3.SelectedValue;
                        //dataRow["MotherFNameRel"] = txtGivenName3.Text;
                        //dataRow["MotherMNameRel"] = txtMiddleName2.Text;
                        //dataRow["MotherLNameRel"] = txtLastName3.Text;
                        //dataRow["DOBRel"] = txtDOB.Text;
                        //dataRow["GenderRel"] = cboGender.SelectedValue;
                        //dataRow["MaritalStatusRel"] = ddlMaritalStatus.SelectedValue;
                        //dataRow["CitizenshipRel"] = ddlCitizenship.SelectedValue;
                        //dataRow["ResiStatusRel"] = ddlResStatus.SelectedValue;

                        ////---to show on grid
                        //dataRow["RelationTypetxt"] = ddlRelType.SelectedItem;

                        //dataRow["GenderReltxt"] = cboGender.SelectedItem;
                        //dataRow["MaritalStatusReltxt"] = ddlMaritalStatus.SelectedItem;
                        //dataRow["CitizenshipReltxt"] = ddlCitizenship.SelectedItem;
                        //dataRow["ResiStatusReltxt"] = ddlResStatus.SelectedItem;
                        //dataRow["OccuTypeReltxt"] = ddlOccupation.SelectedItem;

                        ////---end to show on grid

                        //dataRow["OccuTypeRel"] = ddlOccupation.SelectedValue;


                        //if (chkTick.Checked == true)
                        //{
                        //    dataRow["ResForTaxFlagRel"] = "01";
                        //}
                        //else
                        //{
                        //    dataRow["ResForTaxFlagRel"] = "02";
                        //}
                        //dataRow["ISOCountryCodeRel"] = ddlIsoCountryCodeOthr.SelectedValue;
                        //dataRow["TaxIDNumberRel"] = txtIDResTax.Text.Trim();
                        //dataRow["BirthCityRel"] = txtDOBRes.Text.Trim();
                        //dataRow["ISOBirthPlaceCodeRel"] = ddlIsoCountry.SelectedValue;
                        //dataRow["DecDateRel"] = txtDate.Text.Trim();
                        //dataRow["DecPlaceRel"] = txtPlace.Text.Trim();
                        //dataRow["kycEmpNameRel"] = txtEmpName.Text.Trim();
                        //dataRow["kycEmpCodeRel"] = txtEmpCode.Text.Trim();
                        //dataRow["kycEmpBranchRel"] = txtEmpBranch.Text.Trim();
                        //dataRow["kycEmpDesiRel"] = txtEmpDesignation.Text.Trim();
                        //dataRow["kycVerDateRel"] = txtDate3.Text.Trim();
                        //if (chkSelfCerti.Checked == true)
                        //{
                        //    dataRow["kycCertDocRel"] = "01";
                        //}
                        //else
                        //{
                        //    dataRow["kycCertDocRel"] = System.DBNull.Value;
                        //}
                        dataRow["kycInstNameRel"] = txtInsName.Text.Trim();
                        dataRow["kycInstCodeRel"] = txtInsCode.Text.Trim();
                        DtAdd.Rows.Add(dataRow);
                        Session["dsRel"] = DtAdd;
                        lblMsgConfirmYesNo.Text = "Related Person Details added successfully....Do you want to add more?.";
                        ClearTextcntrl();
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "AsyncConfirmYesNo();", true);
                        btnAdd.Enabled = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + Res1 + "');", true);
                        return;
                    }
                }
                #endregion 14 DIGIT KYC NUMBER SAVE FUNCTIONALITY END
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "btnAdd_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "Fillcountrycd", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "Fillcountrycd1", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "Fillcountrycd2", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
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
                    //txtEmpName.Text = Convert.ToString(dt.Rows[0]["kycEmpName"]);
                    //txtEmpCode.Text = Convert.ToString(dt.Rows[0]["kycEmpCode"]);
                    //txtEmpDesignation.Text = Convert.ToString(dt.Rows[0]["kycEmpDesi"]);
                    //txtEmpBranch.Text = Convert.ToString(dt.Rows[0]["kycEmpBranch"]);
                    txtInsName.Text = Convert.ToString(dt.Rows[0]["kycInstName"]);
                    txtInsCode.Text = Convert.ToString(dt.Rows[0]["kycInstCode"]);
                    //txtDate3.Text = Convert.ToString(dt.Rows[0]["kycVerDate"]);

                    //txtEmpName.Enabled = false;
                    //txtEmpCode.Enabled = false;
                    //txtEmpDesignation.Enabled = false;
                    //txtEmpBranch.Enabled = false;
                    //txtInsName.Enabled = false;
                    //txtInsCode.Enabled = false;
                    //txtDate3.Enabled = false;
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "BindAttestation", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
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

        #region METHOD "disablecntrl"
        protected void disablecntrl()
        {
            try
            {
                // txtKYCNum.Enabled = false;
                ddlRelType.Enabled = false;

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
                ddlMaritalStatus.Enabled = false;
                cboGender.Enabled = false;
                ddlCitizenship.Enabled = false;
                ddlResStatus.Enabled = false;
                ddlOccupation.Enabled = false;
                ddlIsoCountryCodeOthr.Enabled = false;
                ddlIsoCountryCode2.Enabled = false;
                txtIDResTax.Enabled = false;
                txtDOBRes.Enabled = false;
                ddlIsoCountry.Enabled = false;
                ddlProofIdentity.Enabled = false;
                txtPassNo.Enabled = false;
                txtPassNoAdd.Enabled = false;
                txtPassExpDate.Enabled = false;
                ddlAddressType.Enabled = false;
                ddlProofOfAddress.Enabled = false;
                txtAddressLine1.Enabled = false;
                txtAddressLine2.Enabled = false;
                txtAddressLine3.Enabled = false;
                txtCity.Enabled = false;
                txtDistrictname.Enabled = false;
                txtPinCode.Enabled = false;
                ddlState.Enabled = false;
                txtState.Enabled = false;
                //ddlCountryCode.Enabled = false;
                txtPlace.Enabled = false;
                txtDate.Enabled = false;
                txtEmpName.Enabled = false;
                txtEmpCode.Enabled = false;
                txtEmpDesignation.Enabled = false;
                txtEmpBranch.Enabled = false;
                txtInsName.Enabled = false;
                txtInsCode.Enabled = false;
                txtDate3.Enabled = false;

                txtKYCNum.Enabled = false;
                txtRefNumber.Enabled = false;
                ddlOccuSubType.Enabled = false;
                chkTick.Enabled = false;
                txtPassOthr.Enabled = false;
                CheckBox1.Enabled = false;
                //btnShow.   //onClick event set to null
                chkLocalAddress.Enabled = false;
                chkCuurentAddress.Enabled = false;
                ddlAddressType1.Enabled = false;
                txtLocAddLine1.Enabled = false;
                txtLocAddLine2.Enabled = false;
                txtLocAddLine3.Enabled = false;
                txtCity1.Enabled = false;
                ddlState1.Enabled = false;
                txtState1.Enabled = false;
                //btnsearchddlPinCode1    //onClick event set to null
                ddlCountryCode1.Enabled = false;
                chkAddResident.Enabled = false;
                chkCurrentAdd.Enabled = false;
                chkCorresAdd.Enabled = false;
                ddlAddressType2.Enabled = false;
                txtAddLine1.Enabled = false;
                txtAddLine2.Enabled = false;
                txtAddLine3.Enabled = false;
                txtCity2.Enabled = false;
                ddlState2.Enabled = false;
                txtState2.Enabled = false;
                //btnsearchddlPinCode2.   //onClick event set to null
                ddlIsoCountryCode.Enabled = false;
                ddlDocReceived.Enabled = false;

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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "disablecntrl", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region DROPDOWN 'ddlAddressType' SELECTEDINDEXCHANGED EVENT
        protected void ddlAddressType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAddressType.SelectedIndex != 0)
                {
                    if (chkPerAddress.Checked == false)
                    {
                        chkPerAddress.Enabled = true;
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Check CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS')", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please check current/permanent/overseas address details')", true);
                        chkPerAddress.Focus();
                        return;
                    }
                }
                else
                {
                    chkPerAddress.Enabled = false;
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "ddlAddressType_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
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
                    if (ddlCitizenship.SelectedIndex == 0)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Citizenship')", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select Nationality')", true);
                        ddlCitizenship.Focus();
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "chkPerAddress_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region DROPDOWN 'chkPerAddress'SELECTEDINDEXCHANGED EVENT
        protected void chkTick_Checked(object sender, EventArgs e)
        {
            try
            {
                //if (chkPerAddress.Checked == true)
                //{
                //    if (ddlCitizenship.SelectedIndex == 0)
                //    {
                //        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Citizenship')", true);
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select Citizenship')", true);
                //        ddlCitizenship.Focus();
                //        return;
                //    }
                //}
                //spnAddDtls.Visible = true;
                //spnISO3166.Visible = true;
                //spnISOCntryCodeBrth.Visible = true;
                //spnPlaceOfBirth.Visible = true;
                //spnTINNo.Visible = true;


                //added by ramesh on dated 21-05-2018
                if (chkTick.Checked == true)
                {
                    spnAddDtls.Visible = true;
                    spnISO3166.Visible = true;
                    spnISOCntryCodeBrth.Visible = true;
                    spnPlaceOfBirth.Visible = true;
                    spnTINNo.Visible = true;
                    ddlIsoCountryCode2.Focus();
                }
                else
                {
                    spnAddDtls.Visible = false;
                    spnISO3166.Visible = false;
                    spnISOCntryCodeBrth.Visible = false;
                    spnPlaceOfBirth.Visible = false;
                    spnTINNo.Visible = false;
                }
                //end

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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "chkPerAddress_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        protected void ddlOccupation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlOccupation.SelectedValue == "B" || ddlOccupation.SelectedValue == "X")
                {
                    ddlOccuSubType.Visible = true;
                    divOccuSubType.Visible = true;
                    FillSubOccuType(ddlOccupation, ddlOccuSubType);
                    ddlOccuSubType.SelectedIndex = 1;
                }
                else
                {
                    FillSubOccuType(ddlOccupation, ddlOccuSubType);
                    ddlOccuSubType.Visible = true;
                    divOccuSubType.Visible = true;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ddlOccupation_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }


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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "FillSubOccuType", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
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


        #region PopulateRelatedPerson
        private void PopulateRelatedPerson()
        {
            try
            {
                //oCommonUtility.GetCKYC(ddlRelType, "KEntRelative");
                //ddlRelType.Items.Insert(0, new ListItem("Select", ""));

                htParam.Clear();
                htParam.Add("@LookupCode", "KEntRelative");
                FillDropdowns("prc_getDDLLookUpData", htParam, ddlRelType, "CKYCConnectionString", true);
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "PopulateRelatedPerson", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

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
        public void FillDropdowns1(string strQuery, Hashtable htable, DropDownList ddl, string strDBKey, bool isSelect)
        {
            dt = new DataTable();
            objDAL = new DataAccessLayer("CKYCConnectionString");
            dt.Clear();
            dt = objDAL.GetDataTable(strQuery, htable, strDBKey);
            if (dt.Rows.Count > 0)
            {
                ddl.Items.Clear();
                ddl.DataSource = dt;
                ddl.DataTextField = "ParamDesc";
                ddl.DataValueField = "ParamValue";
                ddl.DataBind();
            }
            if (isSelect)
                ddl.Items.Insert(0, new ListItem("Select", ""));
        }

        #region PartialSetDataTable
        public void PartialSetDataTable()
        {
            try
            {
                PartialDtAdd.Columns.Add("RelRefNo");
                PartialDtAdd.Columns.Add("FiRefNo");// added by daksh
                PartialDtAdd.Columns.Add("RelatedPrsnKYCNo");
                PartialDtAdd.Columns.Add("RelationType");
                PartialDtAdd.Columns.Add("PrefixRel");
                PartialDtAdd.Columns.Add("FNameRel");
                PartialDtAdd.Columns.Add("MNameRel");
                PartialDtAdd.Columns.Add("LNameRel");
                PartialDtAdd.Columns.Add("MaidPrefixRel");
                PartialDtAdd.Columns.Add("MaidFNameRel");
                PartialDtAdd.Columns.Add("MaidMNameRel");
                PartialDtAdd.Columns.Add("MaidLNameRel");
                PartialDtAdd.Columns.Add("FatherPrefixRel");
                PartialDtAdd.Columns.Add("FatherFNameRel");
                PartialDtAdd.Columns.Add("FatherMNameRel");
                PartialDtAdd.Columns.Add("FatherLNameRel");
                PartialDtAdd.Columns.Add("MotherPrefixRel");
                PartialDtAdd.Columns.Add("MotherFNameRel");
                PartialDtAdd.Columns.Add("MotherMNameRel");
                PartialDtAdd.Columns.Add("MotherLNameRel");
                PartialDtAdd.Columns.Add("DOBRel");
                PartialDtAdd.Columns.Add("GenderRel");
                PartialDtAdd.Columns.Add("MaritalStatusRel");
                PartialDtAdd.Columns.Add("CitizenshipRel");
                PartialDtAdd.Columns.Add("ResiStatusRel");
                PartialDtAdd.Columns.Add("OccuTypeRel");
                PartialDtAdd.Columns.Add("ResForTaxFlagRel");
                PartialDtAdd.Columns.Add("ISOCountryCodeRel");
                PartialDtAdd.Columns.Add("ISO_RFT_COUNTRYCODE");
                PartialDtAdd.Columns.Add("TaxIDNumberRel");
                PartialDtAdd.Columns.Add("BirthCityRel");
                PartialDtAdd.Columns.Add("ISOBirthPlaceCodeRel");
                PartialDtAdd.Columns.Add("IdType");
                PartialDtAdd.Columns.Add("IdName");
                PartialDtAdd.Columns.Add("IdNumber");
                PartialDtAdd.Columns.Add("IdExpDate");
                PartialDtAdd.Columns.Add("CnctTypeRel");
                PartialDtAdd.Columns.Add("AdrTypeRel");
                PartialDtAdd.Columns.Add("AdrProfRel");
                PartialDtAdd.Columns.Add("Adr1Rel");
                PartialDtAdd.Columns.Add("Adr2Rel");
                PartialDtAdd.Columns.Add("Adr3Rel");
                PartialDtAdd.Columns.Add("CityRel");
                PartialDtAdd.Columns.Add("DistrictRel");
                PartialDtAdd.Columns.Add("PostCodeRel");
                PartialDtAdd.Columns.Add("StateCodeRel");
                PartialDtAdd.Columns.Add("CntryCodeRel");
                //added by daksh
                PartialDtAdd.Columns.Add("CnctTypeRel1");

                PartialDtAdd.Columns.Add("AdrTypeRel1");
                PartialDtAdd.Columns.Add("Adr1Rel1");
                PartialDtAdd.Columns.Add("Adr2Rel1");
                PartialDtAdd.Columns.Add("Adr3Rel1");
                PartialDtAdd.Columns.Add("CityRel1");
                PartialDtAdd.Columns.Add("DistrictRel1");
                PartialDtAdd.Columns.Add("PostCodeRel1");
                PartialDtAdd.Columns.Add("StateCodeRel1");
                PartialDtAdd.Columns.Add("CntryCodeRel1");
                PartialDtAdd.Columns.Add("CnctTypeRel2");
                PartialDtAdd.Columns.Add("AdrTypeRel2");
                PartialDtAdd.Columns.Add("Adr1Rel2");
                PartialDtAdd.Columns.Add("Adr2Rel2");
                PartialDtAdd.Columns.Add("Adr3Rel2");
                PartialDtAdd.Columns.Add("CityRel2");
                PartialDtAdd.Columns.Add("DistrictRel2");
                PartialDtAdd.Columns.Add("PostCodeRel2");
                PartialDtAdd.Columns.Add("StateCodeRel2");
                PartialDtAdd.Columns.Add("CntryCodeRel2");
                //end
                PartialDtAdd.Columns.Add("EMAILID");
                PartialDtAdd.Columns.Add("REMARK");
                PartialDtAdd.Columns.Add("DecDateRel");
                PartialDtAdd.Columns.Add("DecPlaceRel");
                PartialDtAdd.Columns.Add("kycEmpNameRel");
                PartialDtAdd.Columns.Add("kycEmpCodeRel");
                PartialDtAdd.Columns.Add("kycEmpBranchRel");
                PartialDtAdd.Columns.Add("kycEmpDesiRel");
                PartialDtAdd.Columns.Add("kycVerDateRel");
                PartialDtAdd.Columns.Add("kycCertDocRel");
                PartialDtAdd.Columns.Add("kycInstNameRel");
                PartialDtAdd.Columns.Add("kycInstCodeRel");
                PartialDtAdd.Columns.Add("FSFlagRel");
                PartialDtAdd.Columns.Add("AddIdTypeRel");
                PartialDtAdd.Columns.Add("AddIdNameRel");
                PartialDtAdd.Columns.Add("AddIdNumberRel");
                PartialDtAdd.Columns.Add("AddIdExpDateRel");
                PartialDtAdd.Columns.Add("RelationTypetxt");
                PartialDtAdd.Columns.Add("GenderReltxt");
                PartialDtAdd.Columns.Add("MaritalStatusReltxt");
                PartialDtAdd.Columns.Add("CitizenshipReltxt");
                PartialDtAdd.Columns.Add("ResiStatusReltxt");
                PartialDtAdd.Columns.Add("OccuTypeReltxt");

                ViewState["PartialDtAdd"] = PartialDtAdd;
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "SetDataTable", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region SetDataTable
        public void SetDataTable()
        {
            try
            {
                DtAdd.Columns.Add("RelRefNo");
                DtAdd.Columns.Add("FiRefNo");// added by daksh
                DtAdd.Columns.Add("RelatedPrsnKYCNo");
                DtAdd.Columns.Add("RelationType");
                DtAdd.Columns.Add("PrefixRel");
                DtAdd.Columns.Add("FNameRel");
                DtAdd.Columns.Add("MNameRel");
                DtAdd.Columns.Add("LNameRel");
                DtAdd.Columns.Add("MaidPrefixRel");
                DtAdd.Columns.Add("MaidFNameRel");
                DtAdd.Columns.Add("MaidMNameRel");
                DtAdd.Columns.Add("MaidLNameRel");
                DtAdd.Columns.Add("FatherPrefixRel");
                DtAdd.Columns.Add("FatherFNameRel");
                DtAdd.Columns.Add("FatherMNameRel");
                DtAdd.Columns.Add("FatherLNameRel");
                DtAdd.Columns.Add("MotherPrefixRel");
                DtAdd.Columns.Add("MotherFNameRel");
                DtAdd.Columns.Add("MotherMNameRel");
                DtAdd.Columns.Add("MotherLNameRel");
                DtAdd.Columns.Add("DOBRel");
                DtAdd.Columns.Add("GenderRel");
                DtAdd.Columns.Add("MaritalStatusRel");
                DtAdd.Columns.Add("CitizenshipRel");
                DtAdd.Columns.Add("ResiStatusRel");
                DtAdd.Columns.Add("OccuTypeRel");
                DtAdd.Columns.Add("ResForTaxFlagRel");
                DtAdd.Columns.Add("ISOCountryCodeRel");
                DtAdd.Columns.Add("ISO_RFT_COUNTRYCODE");
                DtAdd.Columns.Add("TaxIDNumberRel");
                DtAdd.Columns.Add("BirthCityRel");
                DtAdd.Columns.Add("ISOBirthPlaceCodeRel");
                DtAdd.Columns.Add("IdType");
                DtAdd.Columns.Add("IdName");
                DtAdd.Columns.Add("IdNumber");
                DtAdd.Columns.Add("IdExpDate");
                DtAdd.Columns.Add("CnctTypeRel");
                DtAdd.Columns.Add("AdrTypeRel");
                DtAdd.Columns.Add("AdrProfRel");
                DtAdd.Columns.Add("Adr1Rel");
                DtAdd.Columns.Add("Adr2Rel");
                DtAdd.Columns.Add("Adr3Rel");
                DtAdd.Columns.Add("CityRel");
                DtAdd.Columns.Add("DistrictRel");
                DtAdd.Columns.Add("PostCodeRel");
                DtAdd.Columns.Add("StateCodeRel");
                DtAdd.Columns.Add("CntryCodeRel");
                //added by daksh
                DtAdd.Columns.Add("CnctTypeRel1");

                DtAdd.Columns.Add("AdrTypeRel1");
                DtAdd.Columns.Add("Adr1Rel1");
                DtAdd.Columns.Add("Adr2Rel1");
                DtAdd.Columns.Add("Adr3Rel1");
                DtAdd.Columns.Add("CityRel1");
                DtAdd.Columns.Add("DistrictRel1");
                DtAdd.Columns.Add("PostCodeRel1");
                DtAdd.Columns.Add("StateCodeRel1");
                DtAdd.Columns.Add("CntryCodeRel1");
                DtAdd.Columns.Add("CnctTypeRel2");
                DtAdd.Columns.Add("AdrTypeRel2");
                DtAdd.Columns.Add("Adr1Rel2");
                DtAdd.Columns.Add("Adr2Rel2");
                DtAdd.Columns.Add("Adr3Rel2");
                DtAdd.Columns.Add("CityRel2");
                DtAdd.Columns.Add("DistrictRel2");
                DtAdd.Columns.Add("PostCodeRel2");
                DtAdd.Columns.Add("StateCodeRel2");
                DtAdd.Columns.Add("CntryCodeRel2");
                //end
                DtAdd.Columns.Add("EMAILID");
                DtAdd.Columns.Add("REMARK");
                DtAdd.Columns.Add("DecDateRel");
                DtAdd.Columns.Add("DecPlaceRel");
                DtAdd.Columns.Add("kycEmpNameRel");
                DtAdd.Columns.Add("kycEmpCodeRel");
                DtAdd.Columns.Add("kycEmpBranchRel");
                DtAdd.Columns.Add("kycEmpDesiRel");
                DtAdd.Columns.Add("kycVerDateRel");
                DtAdd.Columns.Add("kycCertDocRel");
                DtAdd.Columns.Add("kycInstNameRel");
                DtAdd.Columns.Add("kycInstCodeRel");
                DtAdd.Columns.Add("FSFlagRel");
                DtAdd.Columns.Add("AddIdTypeRel");
                DtAdd.Columns.Add("AddIdNameRel");
                DtAdd.Columns.Add("AddIdNumberRel");
                DtAdd.Columns.Add("AddIdExpDateRel");
                DtAdd.Columns.Add("RelationTypetxt");
                DtAdd.Columns.Add("GenderReltxt");
                DtAdd.Columns.Add("MaritalStatusReltxt");
                DtAdd.Columns.Add("CitizenshipReltxt");
                DtAdd.Columns.Add("ResiStatusReltxt");
                DtAdd.Columns.Add("OccuTypeReltxt");
                DtAdd.Columns.Add("SVFlag");

                //same as flag
                DtAdd.Columns.Add("SameasPOIAddresFlagP1");
                DtAdd.Columns.Add("SameasCurrentAddresFlagM1");
                DtAdd.Columns.Add("SameasLocalAddressFlagJ1");
                DtAdd.Columns.Add("SameasLocalAddressFlagJ2");
                //end

                ViewState["DtAdd"] = DtAdd;
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "SetDataTable", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region METHOD "disablecntrl"
        protected void ClearTextcntrl()
        {
            try
            {
                CheckBox1.Checked = false;
                chkLocalAddress.Checked = false;
                chkCuurentAddress.Checked = false;
                ddlState1.Enabled = true;
                ddlState1.SelectedIndex = 0;
                ddlProofOfAddress.Enabled = true;
                txtRelRefNumber.Text = "";
                txtRefNumber.Text = "";
                txtKYCNum.Text = "";
                //txtKYCNum.Enabled = true;
                ddlRelType.SelectedIndex = 0;
                cboTitle.SelectedIndex = 0;
                txtGivenName.Text = "";
                txtMiddleName.Text = "";
                txtLastName.Text = "";
                cboTitle1.SelectedIndex = 0;
                txtGivenName1.Text = "";
                txtMiddleName1.Text = "";
                txtLastName1.Text = "";
                //rbtFS.SelectedValue = "";
                cboTitle2.SelectedIndex = 0;
                txtGivenName2.Text = "";
                txtMiddleName2.Text = "";
                txtLastName2.Text = "";
                cboTitle3.SelectedIndex = 0;
                txtGivenName3.Text = "";
                txtMiddleName3.Text = "";
                txtLastName3.Text = "";
                txtDOB.Text = "";
                ddlMaritalStatus.SelectedIndex = 0;
                cboGender.SelectedIndex = 0;
                ddlCitizenship.SelectedIndex = 0;
                ddlResStatus.SelectedIndex = 0;
                ddlOccupation.SelectedIndex = 0;
                ddlOccuSubType.SelectedIndex = 0;
                ddlIsoCountryCode2.SelectedIndex = 0;
                txtIDResTax.Text = "";
                txtDOBRes.Text = "";
                //ddlIsoCountry.SelectedIndex = 0;
                ddlProofIdentity.SelectedIndex = 0;
                txtPassNo.Text = "";
                txtPassNoAdd.Text = "";
                txtPassExpDate.Text = "";
                //chkPerAddress.Checked = false;
                //chkPerAddress.Enabled = true;
                ddlAddressType.SelectedIndex = 0;
                ddlProofOfAddress.SelectedIndex = 0;
                txtPassExpDateAdd.Text = "";
                txtAddressLine1.Text = "";
                txtAddressLine2.Text = "";
                txtAddressLine3.Text = "";
                txtCity.Text = "";
                txtDistrictname.Text = "";
                txtPinCode.Text = "";
                ddlState.SelectedItem.Text = "";
                ////  ddlCountryCode.SelectedIndex = 0;
                chkDone.Enabled = false;
                //rbtFS.Checked = false;
                rbtFS.SelectedValue = "";
                divIdProof.Visible = false;
                divAddProof.Visible = false;

                ddlAddressType1.SelectedIndex = 0;
                txtLocAddLine1.Text = "";
                txtLocAddLine2.Text = "";
                txtLocAddLine3.Text = "";
                txtCity1.Text = "";
                ddlState.SelectedIndex = 0;
                ddlPinCode1.Text = "";
                txtDistrict1.Text = "";
                ddlCountryCode1.SelectedIndex = 0;

                ddlAddressType2.SelectedIndex = 0;
                txtAddLine1.Text = "";
                txtAddLine2.Text = "";
                txtAddLine3.Text = "";
                txtCity2.Text = "";
                ddlState2.SelectedIndex = 0;
                ddlPinCode2.Text = "";
                txtDistrict2.Text = "";
                ddlIsoCountryCode.SelectedIndex = 0;

                ddlAddressType1.Enabled = true;
                txtLocAddLine1.Enabled = true;
                txtLocAddLine2.Enabled = true;
                txtLocAddLine3.Enabled = true;
                txtCity1.Enabled = true;
                ddlState.Enabled = true;
                ddlPinCode1.Enabled = true;
                txtDistrict1.Enabled = true;
                ddlCountryCode1.Enabled = true;

                ddlAddressType2.Enabled = true;
                txtAddLine1.Enabled = true;
                txtAddLine2.Enabled = true;
                txtAddLine3.Enabled = true;
                txtCity2.Enabled = true;
                ddlState2.Enabled = true;
                ddlPinCode2.Enabled = true;
                txtDistrict2.Enabled = true;
                ddlIsoCountryCode.Enabled = true;




                //txtKYCNum.Enabled = false;

                //chkAppDeclare1.Checked = false;
                //chkAppDeclare2.Checked = false;
                //txtPlace.Text = "";
                //txtDate.Text = "";
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "ClearTextcntrl", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region ddlCitizenship_SelectedIndexChanged
        protected void ddlCitizenship_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCitizenship.SelectedValue == "OCIZ")
                {
                    Fillcountrycd();
                    lblIsoCountryCodeOthr.Visible = true;
                    spnISOCntryCode.Visible = true;
                    ddlIsoCountryCodeOthr.Visible = true;
                    ddlCountryCode.SelectedIndex = 0;
                    ddlCountryCode.Enabled = true;
                }
                else
                {
                    lblIsoCountryCodeOthr.Visible = false;
                    spnISOCntryCode.Visible = false;
                    ddlIsoCountryCodeOthr.Visible = false;
                    ddlCountryCode.SelectedValue = "IN";
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "ddlCitizenship_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }

        }
        #endregion

        //#region Fill Sub Occupation Type Details
        //public void FillDistrictState(DropDownList ddl1, DropDownList ddl2, DropDownList ddl3)
        //{
        //    try
        //    {
        //        objDAL = new DataAccessLayer("CKYCConnectionString");
        //        dt = new DataTable();
        //        htParam.Clear();
        //        htParam.Add("@PinCode", ddl1.SelectedValue.ToString());
        //        htParam.Add("@flag", System.DBNull.Value);
        //        dt = objDAL.GetDataTable("Prc_GetAddressCKYC", htParam);
        //        if (dt.Rows.Count > 0)
        //        {
        //            ddl2.DataSource = dt;
        //            ddl2.DataTextField = "District";
        //            ddl2.DataValueField = "District";
        //            ddl2.DataBind();
        //            ddl3.DataSource = dt;
        //            ddl3.DataTextField = "State_Name";
        //            ddl3.DataValueField = "State_code";
        //            ddl3.DataBind();
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
        //            objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "FillDistrictState", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
        //        }
        //    }
        //    finally
        //    {
        //        objDAL = null;
        //        dt = null;
        //    }
        //}
        //#endregion

        //#region validation
        //protected string validation()
        //{
        //    string msg = string.Empty;
        //    string date;
        //    date = DateTime.Today.ToString("dd\\/MM\\/yyyy");
        //    try
        //    {
        //        if (ddlRelType.SelectedIndex == 0)
        //        {
        //            msg = "Please select Related Person Type";
        //            ddlRelType.Focus();
        //            return msg;
        //        }
        //        if (cboTitle.SelectedIndex == 0)
        //        {
        //            msg = "Please select title of name";
        //            cboTitle.Focus();
        //            return msg;
        //        }
        //        if (txtGivenName.Text == "")
        //        {
        //            msg = "Please enter first name";
        //            txtGivenName.Focus();
        //            return msg;
        //        }
        //        if (txtLastName.Text == "")
        //        {
        //            msg = "Please enter last name";
        //            chkAppDeclare1.Focus();
        //            return msg;
        //        }
        //        if (cboTitle2.SelectedIndex == 0)
        //        {
        //            msg = "Please select title of father/spouse Name";
        //            rbtFS.Focus();
        //            return msg;
        //        }
        //        if (rbtFS.SelectedValue == "F")
        //        {
        //            if (rbtFS.SelectedValue == "MRS" || rbtFS.SelectedValue == "MS")
        //            {
        //                msg = "Invalid Title of Father/Spouse Name";
        //                rbtFS.Focus();
        //                return msg;
        //            }
        //        }

        //        if (txtGivenName2.Text == "")
        //        {
        //            msg = "Please enter first name of father/spouse";
        //            cboTitle2.Focus();
        //            return msg;
        //        }
        //        if (txtLastName2.Text == "")
        //        {
        //            msg = "Please enter last name of father/spouse";
        //            txtLastName2.Focus();
        //            return msg;
        //        }
        //        if (cboTitle3.SelectedIndex == 0)
        //        {
        //            msg = "Please select title of mother Name";
        //            cboTitle3.Focus();
        //            return msg;
        //        }
        //        if (txtGivenName3.Text == "")
        //        {
        //            msg = "Please enter first name of mother";
        //            txtGivenName3.Focus();
        //            return msg;
        //        }

        //        if (txtLastName3.Text == "")
        //        {
        //            msg = "Please enter last name of mother";
        //            txtLastName3.Focus();
        //            return msg;
        //        }

        //        if (txtDOB.Text == "")
        //        {
        //            msg = "Please enter date of birth";
        //            txtDOB.Focus();
        //            return msg;
        //        }
        //        if (txtDOB.Text != "")
        //        {
        //            string pattern = "^(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)[0-9]{2}$";
        //            Match match = Regex.Match(txtDOB.Text.ToString(), pattern);
        //            if (!match.Success)
        //            {
        //                msg = "Check DOB date format it must be in dd/mm/yyyy";
        //                return msg;
        //            }

        //            DateTime date1, date2;
        //            date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //            date2 = DateTime.ParseExact(txtDOB.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //            if (date1 < date2)
        //            {
        //                msg = "You cannot select future date";
        //                txtDOB.Focus();
        //                return msg;
        //            }
        //        }
        //        if (cboGender.SelectedIndex == 0)
        //        {
        //            msg = "Please select gender";
        //            cboGender.Focus();
        //            return msg;
        //        }
        //        if (ddlOccupation.SelectedIndex == 0)
        //        {
        //            msg = "Please select occupation type";
        //            ddlOccupation.Focus();
        //            return msg;
        //        }
        //        if (ddlMaritalStatus.SelectedIndex == 0)
        //        {
        //            msg = "Please select marital status";
        //            ddlMaritalStatus.Focus();
        //            return msg;
        //        }

        //        if (cboTitle.SelectedValue == "MRS" && ddlMaritalStatus.SelectedValue == "02")
        //        {
        //            msg = "Invalid Title";
        //            cboTitle.Focus();
        //            return msg;
        //        }
        //        if (ddlCitizenship.SelectedIndex == 0)
        //        {
        //            msg = "Please select citizenship";
        //            ddlCitizenship.Focus();
        //            return msg;
        //        }
        //        if (ddlResStatus.SelectedIndex == 0)
        //        {
        //            msg = "Please select residential status";
        //            ddlResStatus.Focus();
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
        //        if (chkTick.Checked == true)
        //        {
        //            if (ddlIsoCountryCode2.SelectedIndex == 0)
        //            {
        //                msg = "Please select ISO 3166 country code of jurisdiction of residence";
        //                return msg;
        //            }
        //            if (txtIDResTax.Text == "")
        //            {
        //                msg = "Please enter tax identification number or equivalent(if issued by jurisdiction)";
        //                return msg;
        //            }
        //            if (txtDOBRes.Text == "")
        //            {
        //                msg = "Please enter place/city of birth of jurisdiction of residence";
        //                return msg;
        //            }
        //            if (ddlIsoCountry.SelectedIndex == 0)
        //            {
        //                msg = "Please select ISO 3166 country code of birth";
        //                return msg;
        //            }
        //        }
        //        if (ddlProofIdentity.SelectedIndex == 0)
        //        {
        //            msg = "Please select proof of identity";
        //            ddlProofIdentity.Focus();
        //            return msg;
        //        }

        //        if (ddlProofIdentity.SelectedIndex != 0)
        //        {
        //            if (ddlProofIdentity.SelectedIndex == 1)
        //            {
        //                if (txtPassNo.Text == "")
        //                {
        //                    msg = "Please enter passport number";
        //                    ddlProofIdentity.Focus();
        //                    return msg;
        //                }
        //                if (txtPassExpDate.Text == "")
        //                {
        //                    msg = "Please enter passport expiry date";
        //                    txtPassExpDate.Focus();
        //                    return msg;
        //                }
        //                if (txtPassExpDate.Text != "")
        //                {
        //                    string pattern = "^(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)[0-9]{2}$";
        //                    Match match = Regex.Match(txtPassExpDate.Text.ToString(), pattern);
        //                    if (!match.Success)
        //                    {
        //                        msg = "Check driving license date format it must be in dd/mm/yyyy";
        //                        return msg;
        //                    }

        //                    DateTime date1, date2;
        //                    date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //                    date2 = DateTime.ParseExact(txtPassExpDate.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //                    if (date1 > date2)
        //                    {
        //                        msg = "You cannot select past date as driving license expiry date";
        //                        txtPassExpDate.Focus();
        //                        return msg;
        //                    }
        //                }
        //            }

        //            if (ddlProofIdentity.SelectedIndex == 2)
        //            {
        //                if (txtPassNo.Text == "")
        //                {
        //                    msg = "Please enter voter id card";
        //                    txtPassNo.Focus();
        //                    return msg;
        //                }
        //            }
        //            if (ddlProofIdentity.SelectedIndex == 3)
        //            {
        //                if (txtPassNo.Text == "")
        //                {
        //                    msg = "Please enter pan card";
        //                    txtPassNo.Focus();
        //                    return msg;
        //                }

        //            }
        //            if (ddlProofIdentity.SelectedIndex == 4)
        //            {
        //                if (txtPassNo.Text == "")
        //                {
        //                    msg = "Please enter driving licence number";
        //                    txtPassNo.Focus();
        //                    return msg;
        //                }
        //                if (txtPassExpDate.Text == "")
        //                {
        //                    msg = "Please enter driving licence expiry date";
        //                    txtPassExpDate.Focus();
        //                    return msg;
        //                }
        //                if (txtPassExpDate.Text != "")
        //                {
        //                    string pattern = "^(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)[0-9]{2}$";
        //                    Match match = Regex.Match(txtPassExpDate.Text.ToString(), pattern);
        //                    if (!match.Success)
        //                    {
        //                        msg = "Check driving license date format it must be in dd/mm/yyyy";
        //                        return msg;
        //                    }
        //                    DateTime date1, date2;
        //                    date1 = DateTime.ParseExact(date.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //                    date2 = DateTime.ParseExact(txtPassExpDate.Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //                    if (date1 > date2)
        //                    {
        //                        msg = "You cannot select past date as driving license expiry date";
        //                        txtPassExpDate.Focus();
        //                        return msg;
        //                    }
        //                }
        //            }

        //            if (ddlProofIdentity.SelectedIndex == 5)
        //            {
        //                if (txtPassNo.Text == "")
        //                {
        //                    msg = "Please enter UID(aadhaar)";
        //                    txtPassNo.Focus();
        //                    return msg;
        //                }
        //            }
        //            if (ddlProofIdentity.SelectedIndex == 6)
        //            {
        //                if (txtPassNo.Text == "")
        //                {
        //                    msg = "Please enter NREGA job card";
        //                    txtPassNo.Focus();
        //                    return msg;
        //                }
        //            }
        //            if (ddlProofIdentity.SelectedIndex == 7)
        //            {
        //                if (txtPassNo.Text == "")
        //                {
        //                    msg = "Please enter document name";
        //                    txtPassNo.Focus();
        //                    return msg;
        //                }
        //            }
        //            if (ddlProofIdentity.SelectedIndex == 8)
        //            {
        //                if (txtPassNo.Text == "")
        //                {
        //                    msg = "Please enter simplified measures account";
        //                    txtPassNo.Focus();
        //                    return msg;
        //                }
        //            }
        //        }
        //        if (ddlAddressType.SelectedIndex != 0)
        //        {
        //            if (chkPerAddress.Checked == false)
        //            {
        //                msg = "Please check current/permanent/overseas address details";
        //                chkPerAddress.Focus();
        //                return msg;
        //            }
        //        }
        //        if (ddlAddressType.SelectedIndex == 0)
        //        {
        //            msg = "Please select address type";
        //            chkAppDeclare1.Focus();
        //            return msg;
        //        }
        //        if (ddlProofOfAddress.SelectedIndex == 0)
        //        {
        //            msg = "Please select proof of address";
        //            chkAppDeclare1.Focus();
        //            return msg;
        //        }
        //        if (txtAddressLine1.Text == "")
        //        {
        //            msg = "Please enter permanent address line 1";
        //            txtAddressLine1.Focus();
        //            return msg;
        //        }
        //        if (txtCity.Text == "")
        //        {
        //            msg = "Please enter permanent city/town/village";
        //            txtCity.Focus();
        //            return msg;
        //        }
        //        if (ddlPinCode.SelectedIndex == 0 && chkTick.Checked == false)
        //        {
        //            msg = "Please enter permanent pin/post code";
        //            ddlPinCode.Focus();
        //            return msg;
        //        }
        //        if (chkAppDeclare1.Checked == false)
        //        {
        //            msg = "Please check application declaration";
        //            chkAppDeclare1.Focus();
        //            return msg;
        //        }
        //        if (txtDate3.Text == "")
        //        {
        //            msg = " Please enter KYC verification carried out Date";
        //            txtCity.Focus();
        //            return msg;
        //        }
        //        if (txtEmpName.Text == "")
        //        {
        //            msg = " Please enter KYC verification carried out Employee Name";
        //            txtCity.Focus();
        //            return msg;
        //        }
        //        if (txtEmpCode.Text == "")
        //        {
        //            msg = " Please enter KYC verification carried out Employee Code";
        //            txtCity.Focus();
        //            return msg;
        //        }
        //        if (txtEmpDesignation.Text == "")
        //        {
        //            msg = " Please enter KYC verification carried out Employee Designation";
        //            txtCity.Focus();
        //            return msg;
        //        }
        //        if (txtEmpBranch.Text == "")
        //        {
        //            msg = " Please enter KYC verification carried out Employee Branch";
        //            txtCity.Focus();
        //            return msg;
        //        }
        //        if (txtInsName.Text == "")
        //        {
        //            msg = " Please enter KYC verification carried out Institution Name";
        //            txtCity.Focus();
        //            return msg;
        //        }
        //        if (txtInsCode.Text == "")
        //        {
        //            msg = msg = " Please enter KYC verification carried out Institution Code";
        //            txtCity.Focus();
        //            return msg;
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
        //            objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "validation", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
        //        }
        //    }
        //    return msg;
        //}
        //#endregion

        #region ConfirmYes
        protected void ConfirmYes(object sender, EventArgs e)
        {
            ddlRelType.Focus();
            btnPSUpdate.Visible = false;
            btnUpdate.Visible = false;
            btnAdd.Visible = true;
            btnPartialAdd.Visible = true;

            divIdProof.Visible = false;
            divAddProof.Visible = false;
            btnPartialAdd.Enabled = true;
            // txtKYCNum.Enabled = false;
            ddlDocReceived.Enabled = false;
            //chkHigh.Enabled = false;
            //chkMedium.Enabled = false;
            //chkLow.Enabled = false;
            txtEmpName.Enabled = false;
            txtDate3.Enabled = false;
            txtEmpCode.Enabled = false;
            txtEmpDesignation.Enabled = false;
            txtEmpBranch.Enabled = false;
            txtInsName.Enabled = false;
            txtInsCode.Enabled = false;
            chkAppDeclare1.Enabled = false;
            chkAppDeclare2.Enabled = false;
            chkAppDeclare3.Enabled = false;
            txtDate.Enabled = false;
            txtPlace.Enabled = false;
            FillStates();
            ddlCountryCode.SelectedValue = "IN";


        }
        #endregion

        #region ConfirmNo
        protected void ConfirmNo(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Closepopup();", true);
            btnAdd.Enabled = false;
            btnPartialAdd.Enabled = false;

            btnPSUpdate.Visible = false;
            btnUpdate.Visible = false;
            btnAdd.Visible = false;
            btnPartialAdd.Visible = false;
        }
        #endregion

        #region ShowOnGridView
        protected void ShowOnGridView()
        {
            DataRow dataRow = DtAdd.NewRow();
            dataRow["RelatedPrsnKYCNo"] = txtKYCNum.Text.Trim();
            dataRow["RelationType"] = ddlRelType.SelectedItem.Text;
            dataRow["PREFIX"] = cboTitle.SelectedValue;
            dataRow["FNAME"] = txtGivenName.Text.Trim();
            dataRow["MNAME"] = txtMiddleName.Text.Trim();
            dataRow["LNAME"] = txtLastName.Text.Trim();
            dataRow["MAID_PREFIX"] = cboTitle1.SelectedValue;
            dataRow["MAID_FNAME"] = txtGivenName1.Text.Trim();
            dataRow["MAID_MNAME"] = txtMiddleName1.Text.Trim();
            dataRow["MAID_LNAME"] = txtLastName1.Text.Trim();

            if (rbtFS.SelectedValue == "F")
            {
                dataRow["fs_flag"] = "01";
            }
            else
            {
                dataRow["fs_flag"] = "02";
            }
            dataRow["FATHER_PREFIX"] = cboTitle2.SelectedValue;
            dataRow["FATHER_FNAME"] = txtGivenName2.Text.Trim();
            dataRow["FATHER_MNAME"] = txtMiddleName2.Text.Trim();
            dataRow["FATHER_LNAME"] = txtLastName2.Text.Trim();
            dataRow["MOTHER_PREFIX"] = cboTitle3.SelectedValue;
            dataRow["MOTHER_FNAME"] = txtGivenName3.Text;
            dataRow["MOTHER_MNAME"] = txtMiddleName2.Text;
            dataRow["MOTHER_LNAME"] = txtLastName3.Text;
            dataRow["DOB"] = txtDOB.Text;
            dataRow["GENDER"] = cboGender.SelectedItem;
            dataRow["MARITAL_STATUS"] = ddlMaritalStatus.SelectedItem.Text;
            dataRow["CITIZENSHIP"] = ddlCitizenship.SelectedItem.Text;
            dataRow["RESI_STATUS"] = ddlResStatus.SelectedItem.Text;
            dataRow["OCCU_TYPE"] = ddlOccupation.SelectedItem.Text;
            if (chkTick.Checked == true)
            {
                dataRow["ResForTaxFlagRel"] = "01";
            }
            else
            {
                dataRow["ResForTaxFlagRel"] = "02";
            }

            dataRow["ISO_COUNTRYCODE"] = ddlIsoCountryCodeOthr.SelectedItem;
            dataRow["TAX_IDNUMBER"] = txtIDResTax.Text.Trim();
            dataRow["BIRTH_PLACE"] = txtDOBRes.Text.Trim();
            dataRow["ISO_BIRTHPLACE_CODE"] = ddlIsoCountry.SelectedValue;
            dataRow["IdType"] = ddlProofIdentity.SelectedValue;
            if (ddlProofIdentity.SelectedIndex == 1)
            {
                dataRow["IdNumber"] = txtPassNo.Text.Trim();
                dataRow["IdExpDate"] = txtPassExpDate.Text.Trim();
                dataRow["IdName"] = System.DBNull.Value;

            }
            else if (ddlProofIdentity.SelectedIndex == 2)
            {
                dataRow["IdNumber"] = txtPassNo.Text.Trim();
                dataRow["IdExpDate"] = System.DBNull.Value;
                dataRow["IdName"] = System.DBNull.Value;

            }
            else if (ddlProofIdentity.SelectedIndex == 3)
            {
                dataRow["IdNumber"] = txtPassNo.Text.Trim();
                dataRow["IdExpDate"] = System.DBNull.Value;
                dataRow["IdName"] = System.DBNull.Value;
            }
            else if (ddlProofIdentity.SelectedIndex == 4)
            {
                dataRow["IdNumber"] = txtPassNo.Text.Trim();
                dataRow["IdExpDate"] = txtPassExpDate.Text.Trim();
                dataRow["IdName"] = System.DBNull.Value;
            }
            else if (ddlProofIdentity.SelectedIndex == 5)
            {
                dataRow["IdNumber"] = txtPassNo.Text.Trim();
                dataRow["IdExpDate"] = System.DBNull.Value;
                dataRow["IdName"] = System.DBNull.Value;
            }
            else if (ddlProofIdentity.SelectedIndex == 6)
            {
                dataRow["IdNumber"] = txtPassNo.Text.Trim();
                dataRow["IdExpDate"] = System.DBNull.Value;
                dataRow["IdName"] = System.DBNull.Value;
            }
            else if (ddlProofIdentity.SelectedIndex == 7)
            {
                dataRow["IdNumber"] = txtPassOthr.Text.Trim();
                dataRow["IdExpDate"] = System.DBNull.Value;
                dataRow["IdName"] = txtPassNo.Text.Trim();
            }
            else if (ddlProofIdentity.SelectedIndex == 8 || ddlProofIdentity.SelectedIndex == 9)
            {
                dataRow["IdNumber"] = txtPassNo.Text.Trim();
                dataRow["IdExpDate"] = System.DBNull.Value;
                dataRow["IdName"] = System.DBNull.Value;
            }
            else
            {
                dataRow["IdNumber"] = System.DBNull.Value;
                dataRow["IdExpDate"] = System.DBNull.Value;
                dataRow["IdName"] = System.DBNull.Value;
            }

            if (chkPerAddress.Checked == true)
            {
                dataRow["CnctType1"] = "P1";
                dataRow["PER_ADDTYPE"] = ddlAddressType.SelectedValue;
                dataRow["PER_ADDPROOF"] = ddlProofOfAddress.SelectedValue;
                dataRow["PER_ADDLINE1"] = txtAddressLine1.Text.Trim();
                dataRow["PER_ADDLINE2"] = txtAddressLine2.Text.Trim();
                dataRow["PER_ADDLINE3"] = txtAddressLine3.Text.Trim();
                dataRow["PER_CITY"] = txtCity.Text.Trim();
                dataRow["PER_DISTRICT"] = txtDistrictname.Text;
                dataRow["PER_PIN"] = txtPinCode.Text;
                dataRow["PER_STATECODE"] = ddlState.SelectedValue;
                dataRow["PER_COUNTRY_CODE"] = ddlCountryCode.SelectedValue;
            }
            else
            {
                dataRow["CnctType1"] = "";
                dataRow["PER_ADDTYPE"] = System.DBNull.Value;
                dataRow["PER_ADDPROOF"] = System.DBNull.Value;
                dataRow["PER_ADDLINE1"] = System.DBNull.Value;
                dataRow["PER_ADDLINE2"] = System.DBNull.Value;
                dataRow["PER_ADDLINE3"] = System.DBNull.Value;
                dataRow["PER_CITY"] = System.DBNull.Value;
                dataRow["PER_DISTRICT"] = System.DBNull.Value;
                dataRow["PER_PIN"] = System.DBNull.Value;
                dataRow["PER_STATECODE"] = System.DBNull.Value;
                dataRow["PER_COUNTRY_CODE"] = System.DBNull.Value;
            }
            dataRow["AddIdType"] = ddlProofOfAddress.SelectedValue;
            if (chkPerAddress.Checked == true)
            {
                if (ddlProofOfAddress.SelectedIndex == 1)
                {
                    dataRow["AddIdNumber"] = txtPassNoAdd.Text.Trim();
                    dataRow["AddIdExpDate"] = txtPassExpDateAdd.Text.Trim();
                    dataRow["AddIdName"] = System.DBNull.Value;
                }


                else if (ddlProofOfAddress.SelectedIndex == 2)
                {
                    dataRow["AddIdNumber"] = txtPassNoAdd.Text.Trim();
                    dataRow["AddIdExpDate"] = txtPassExpDateAdd.Text.Trim();
                    dataRow["AddIdName"] = System.DBNull.Value;
                }
                else if (ddlProofOfAddress.SelectedIndex == 3)
                {
                    dataRow["AddIdNumber"] = txtPassNoAdd.Text.Trim();
                    dataRow["AddIdExpDate"] = System.DBNull.Value;
                    dataRow["AddIdName"] = System.DBNull.Value;
                }
                else if (ddlProofOfAddress.SelectedIndex == 4)
                {
                    dataRow["AddIdNumber"] = txtPassNoAdd.Text.Trim();
                    dataRow["AddIdExpDate"] = System.DBNull.Value;
                    dataRow["AddIdName"] = System.DBNull.Value;

                }
                else if (ddlProofOfAddress.SelectedIndex == 5)
                {
                    dataRow["AddIdNumber"] = txtPassNoAdd.Text.Trim();
                    dataRow["AddIdExpDate"] = System.DBNull.Value;
                    dataRow["AddIdName"] = System.DBNull.Value;
                }
                else if (ddlProofOfAddress.SelectedIndex == 6)
                {
                    dataRow["AddIdNumber"] = txtPassOthrAdd.Text.Trim();
                    dataRow["AddIdExpDate"] = System.DBNull.Value;
                    dataRow["AddIdName"] = txtPassNoAdd.Text.Trim();
                }

                else
                {
                    dataRow["AddIdNumber"] = System.DBNull.Value;
                    dataRow["AddIdExpDate"] = System.DBNull.Value;
                    dataRow["AddIdName"] = System.DBNull.Value;
                }
            }
            else
            {
                dataRow["AddIdNumber"] = System.DBNull.Value;
                dataRow["AddIdExpDate"] = System.DBNull.Value;
                dataRow["AddIdName"] = System.DBNull.Value;
            }
            dataRow["APP_DATE"] = txtDate.Text.Trim();
            dataRow["PLACE"] = txtPlace.Text.Trim();
            dataRow["kycEmpName"] = txtEmpName.Text.Trim();
            dataRow["kycEmpCode"] = txtEmpCode.Text.Trim();
            dataRow["kycEmpBranch"] = txtEmpBranch.Text.Trim();
            dataRow["kycEmpDesi"] = txtEmpDesignation.Text.Trim();
            dataRow["kycVerDate"] = txtDate3.Text.Trim();

            dataRow["kycCertDoc"] = System.DBNull.Value;
            dataRow["kycInstName"] = txtInsName.Text.Trim();
            dataRow["kycInstCode"] = txtInsCode.Text.Trim();
            //dataRow["CREATEDBY"] =strUserId.ToString();

            DtAdd.Rows.Add(dataRow);
            Session["DtAdd"] = DtAdd;
        }
        #endregion

        #region METHOD "FillRelatedPersondata"
        protected void FillRelatedPersondata()
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                htParam.Clear();
                htParam.Add("@RegRefNo", Request.QueryString["refno"].ToString());
                htParam.Add("@RelRefNo", Request.QueryString["RelRefNo"].ToString().Trim());
                htParam.Add("@ActionFlag", "Mod");
                htParam.Add("@UserType", "");

                dt = objDAL.GetDataTable("Prc_GetRelatedPersonDataForCKYC", htParam);

                txtRelRefNumber.Text = Convert.ToString(dt.Rows[0]["RelRefNo"]);
                txtKYCNum.Text = Convert.ToString(dt.Rows[0]["RelatedPrsnKYCNo"]);
                ddlRelType.SelectedValue = Convert.ToString(dt.Rows[0]["RelationType"]);
                cboTitle.SelectedValue = Convert.ToString(dt.Rows[0]["PrefixRel"]);
                txtGivenName.Text = Convert.ToString(dt.Rows[0]["FNameRel"]);
                txtMiddleName.Text = Convert.ToString(dt.Rows[0]["MNameRel"]);
                txtLastName.Text = Convert.ToString(dt.Rows[0]["LNameRel"]);
                cboTitle1.SelectedValue = Convert.ToString(dt.Rows[0]["MaidPrefixRel"]);
                txtGivenName1.Text = Convert.ToString(dt.Rows[0]["MaidFNameRel"]);
                txtMiddleName1.Text = Convert.ToString(dt.Rows[0]["MaidMNameRel"]);
                txtLastName1.Text = Convert.ToString(dt.Rows[0]["MaidLNameRel"]);

                if (Convert.ToString(dt.Rows[0]["FSFlagRel"]) == "01")
                {
                    rbtFS.SelectedValue = "F";
                }
                else
                {
                    rbtFS.SelectedValue = "S";
                }
                cboTitle2.SelectedValue = Convert.ToString(dt.Rows[0]["FatherPrefixRel"]);
                txtGivenName2.Text = Convert.ToString(dt.Rows[0]["FatherFNameRel"]);
                txtMiddleName2.Text = Convert.ToString(dt.Rows[0]["FatherMNameRel"]);
                txtLastName2.Text = Convert.ToString(dt.Rows[0]["FatherLNameRel"]);
                cboTitle3.SelectedValue = Convert.ToString(dt.Rows[0]["MotherPrefixRel"]);
                txtGivenName3.Text = Convert.ToString(dt.Rows[0]["MotherFNameRel"]);
                txtMiddleName3.Text = Convert.ToString(dt.Rows[0]["MotherMNameRel"]);
                txtLastName3.Text = Convert.ToString(dt.Rows[0]["MotherLNameRel"]);
                txtDOB.Text = Convert.ToString(dt.Rows[0]["DOBRel"]);
                ddlMaritalStatus.SelectedValue = Convert.ToString(dt.Rows[0]["MaritalStatusRel"]);
                cboGender.SelectedValue = Convert.ToString(dt.Rows[0]["GenderRel"]);
                ddlCitizenship.SelectedValue = Convert.ToString(dt.Rows[0]["CitizenshipRel"]);
                ddlResStatus.SelectedValue = Convert.ToString(dt.Rows[0]["ResiStatusRel"]);
                ddlOccupation.SelectedValue = Convert.ToString(dt.Rows[0]["OccuTypeRel"]);
                ddlIsoCountryCodeOthr.SelectedValue = Convert.ToString(dt.Rows[0]["ISOCountryCodeRel"]);
                ddlIsoCountryCode2.SelectedValue = Convert.ToString(dt.Rows[0]["ISOCountryCodeRel"]);

                txtIDResTax.Text = Convert.ToString(dt.Rows[0]["TaxIDNumberRel"]);
                txtDOBRes.Text = Convert.ToString(dt.Rows[0]["BirthCityRel"]);
                ddlIsoCountry.SelectedValue = Convert.ToString(dt.Rows[0]["ISOBirthPlaceCodeRel"]);

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


                if (Convert.ToString(dt.Rows[0]["CnctTypeRel"]) == "P1")
                {
                    chkPerAddress.Checked = true;
                }
                else
                {
                    chkPerAddress.Checked = false;
                }

                ddlAddressType.SelectedValue = Convert.ToString(dt.Rows[0]["AdrTypeRel"]);
                ddlProofOfAddress.SelectedValue = Convert.ToString(dt.Rows[0]["AdrProfRel"]);
                txtAddressLine1.Text = Convert.ToString(dt.Rows[0]["Adr1Rel"]);
                txtAddressLine2.Text = Convert.ToString(dt.Rows[0]["Adr2Rel"]);
                txtAddressLine3.Text = Convert.ToString(dt.Rows[0]["Adr3Rel"]);
                txtCity.Text = Convert.ToString(dt.Rows[0]["CityRel"]);
                txtDistrictname.Text = Convert.ToString(dt.Rows[0]["DistrictRel"]);
                txtPinCode.Text = Convert.ToString(dt.Rows[0]["PostCodeRel"]);
                ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["StateCodeRel"]);
                ddlCountryCode.SelectedValue = Convert.ToString(dt.Rows[0]["CntryCodeRel"]);
                ViewState["strAddIdName"] = Convert.ToString(dt.Rows[0]["AddIdNameRel"]);
                ViewState["strAddIdNumber"] = Convert.ToString(dt.Rows[0]["AddIdNumberRel"]);
                ViewState["strAddIdExpDate"] = Convert.ToString(dt.Rows[0]["AddIdExpDateRel"]);

                if (ddlProofOfAddress.SelectedValue == "99")
                {
                    txtPassOthrAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNumberRel"]);
                    txtPassNoAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNameRel"]);
                }
                else
                {
                    txtPassNoAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNumberRel"]);
                    txtPassExpDateAdd.Text = Convert.ToString(dt.Rows[0]["AddIdExpDateRel"]);
                }

                txtPlace.Text = Convert.ToString(dt.Rows[0]["DecPlaceRel"]);
                txtDate.Text = Convert.ToString(dt.Rows[0]["DecDateRel"]);
                txtEmpName.Text = Convert.ToString(dt.Rows[0]["kycEmpNameRel"]);
                txtEmpCode.Text = Convert.ToString(dt.Rows[0]["kycEmpCodeRel"]);
                txtEmpDesignation.Text = Convert.ToString(dt.Rows[0]["kycEmpDesiRel"]);
                txtEmpBranch.Text = Convert.ToString(dt.Rows[0]["kycEmpBranchRel"]);
                txtInsName.Text = Convert.ToString(dt.Rows[0]["kycInstNameRel"]);

                txtInsCode.Text = Convert.ToString(dt.Rows[0]["kycInstCodeRel"]);
                txtDate3.Text = Convert.ToString(dt.Rows[0]["kycVerDateRel"]);


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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "FillRelatedPersondata", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
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

        #region btnPartialAdd
        protected void btnPartialAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string Res;
                Res = "";
                //objVal.PersonalDtlsValidation(
                //    null, null, null, cboTitle, txtGivenName, txtLastName, cboTitle2, rbtFS, txtGivenName2, txtLastName2, cboTitle3, txtGivenName3,
                //    txtLastName3, txtDOB, cboGender, ddlOccupation, null, ddlMaritalStatus, ddlCitizenship, ddlResStatus, ddlIsoCountryCodeOthr, ddlRelType, "RelatedPrsn");

                if (Res.Equals(""))
                {
                    //DataTable dtView = new DataTable();
                    //DataTable dtSession = new DataTable();
                    if (Request.QueryString["Status"].ToString() == "PMod")
                    {

                    }
                    //DtAdd = dtView;
                    //DtAdd = (DataTable)Session["dsRel"];

                    if (Session["dsRel"] != null)
                    {
                        DtAdd = (DataTable)Session["dsRel"];
                    }
                    else
                    {
                        DtAdd = (DataTable)ViewState["DtAdd"];
                    }

                    DataRow dataRow = DtAdd.NewRow();
                    dataRow["FiRefNo"] = txtRefNumber.Text.Trim();
                    dataRow["RelRefNo"] = txtRelRefNumber.Text.Trim();
                    dataRow["RelatedPrsnKYCNo"] = txtKYCNum.Text.Trim();
                    dataRow["RelationType"] = ddlRelType.SelectedValue;
                    dataRow["PrefixRel"] = cboTitle.SelectedValue;
                    dataRow["FNameRel"] = txtGivenName.Text.Trim();
                    dataRow["MNameRel"] = txtMiddleName.Text.Trim();
                    dataRow["LNameRel"] = txtLastName.Text.Trim();
                    dataRow["MaidPrefixRel"] = cboTitle1.SelectedValue;
                    dataRow["MaidFNameRel"] = txtGivenName1.Text.Trim();
                    dataRow["MaidMNameRel"] = txtMiddleName1.Text.Trim();
                    dataRow["MaidLNameRel"] = txtLastName1.Text.Trim();

                    //ddlIsoCountryCode2.SelectedValue

                    if (rbtFS.SelectedValue == "F")
                    {
                        dataRow["FSFlagRel"] = "01";
                    }
                    else
                    {
                        dataRow["FSFlagRel"] = "02";
                    }
                    dataRow["FatherPrefixRel"] = cboTitle2.SelectedValue;
                    dataRow["FatherFNameRel"] = txtGivenName2.Text.Trim();
                    dataRow["FatherMNameRel"] = txtMiddleName2.Text.Trim();
                    dataRow["FatherLNameRel"] = txtLastName2.Text.Trim();
                    dataRow["MotherPrefixRel"] = cboTitle3.SelectedValue;
                    dataRow["MotherFNameRel"] = txtGivenName3.Text;
                    dataRow["MotherMNameRel"] = txtMiddleName2.Text;
                    dataRow["MotherLNameRel"] = txtLastName3.Text;
                    dataRow["DOBRel"] = txtDOB.Text;
                    dataRow["GenderRel"] = cboGender.SelectedValue;
                    dataRow["MaritalStatusRel"] = ddlMaritalStatus.SelectedValue;
                    dataRow["CitizenshipRel"] = ddlCitizenship.SelectedValue;
                    dataRow["ResiStatusRel"] = ddlResStatus.SelectedValue;

                    //---to show on grid

                    if (ddlRelType.SelectedIndex != 0)
                    {
                        dataRow["RelationTypetxt"] = ddlRelType.SelectedItem.Text;
                    }
                    else
                    {
                        dataRow["RelationTypetxt"] = "";
                    }

                    if (cboGender.SelectedIndex != 0)
                    {
                        dataRow["GenderReltxt"] = cboGender.SelectedItem.Text;
                    }
                    else
                    {
                        dataRow["GenderReltxt"] = "";
                    }

                    //dataRow["GenderReltxt"] = cboGender.SelectedItem.Text;

                    if (ddlMaritalStatus.SelectedIndex != 0)
                    {
                        dataRow["MaritalStatusReltxt"] = ddlMaritalStatus.SelectedItem.Text;
                    }
                    else
                    {
                        dataRow["MaritalStatusReltxt"] = "";
                    }
                    //dataRow["MaritalStatusReltxt"] = ddlMaritalStatus.SelectedItem.Text;

                    if (ddlCitizenship.SelectedIndex != 0)
                    {
                        dataRow["CitizenshipReltxt"] = ddlCitizenship.SelectedItem.Text;
                    }
                    else
                    {
                        dataRow["CitizenshipReltxt"] = "";
                    }
                    //dataRow["CitizenshipReltxt"] = ddlCitizenship.SelectedItem.Text;

                    if (ddlResStatus.SelectedIndex != 0)
                    {
                        dataRow["ResiStatusReltxt"] = ddlResStatus.SelectedItem.Text;
                    }
                    else
                    {
                        dataRow["ResiStatusReltxt"] = "";
                    }
                    //dataRow["ResiStatusReltxt"] = ddlResStatus.SelectedItem.Text;

                    if (ddlOccuSubType.SelectedIndex != 0)
                    {
                        dataRow["OccuTypeReltxt"] = ddlOccuSubType.SelectedItem.Text;
                    }
                    else
                    {
                        dataRow["OccuTypeReltxt"] = "";
                    }
                    //dataRow["OccuTypeReltxt"] = ddlOccuSubType.SelectedItem.Text;

                    //dataRow["RelationTypetxt"] = ddlRelType.SelectedItem.Text;

                    //dataRow["GenderReltxt"] = cboGender.SelectedItem.Text;
                    //dataRow["MaritalStatusReltxt"] = ddlMaritalStatus.SelectedItem.Text;
                    //dataRow["CitizenshipReltxt"] = ddlCitizenship.SelectedItem.Text;
                    //dataRow["ResiStatusReltxt"] = ddlResStatus.SelectedItem.Text;
                    //dataRow["OccuTypeReltxt"] = ddlOccuSubType.SelectedItem.Text;

                    //---end to show on grid

                    dataRow["OccuTypeRel"] = ddlOccupation.SelectedValue;


                    if (chkTick.Checked == true)
                    {
                        dataRow["ResForTaxFlagRel"] = "01";
                    }
                    else
                    {
                        dataRow["ResForTaxFlagRel"] = "02";
                    }
                    dataRow["ISOCountryCodeRel"] = ddlIsoCountryCode2.SelectedValue;
                    dataRow["TaxIDNumberRel"] = txtIDResTax.Text.Trim();
                    dataRow["BirthCityRel"] = txtDOBRes.Text.Trim();
                    dataRow["ISOBirthPlaceCodeRel"] = ddlIsoCountry.SelectedValue;
                    dataRow["IdType"] = ddlProofIdentity.SelectedValue;
                    if (ddlProofIdentity.SelectedIndex == 1)
                    {
                        dataRow["IdNumber"] = txtPassNo.Text.Trim();
                        dataRow["IdExpDate"] = txtPassExpDate.Text.Trim();
                        dataRow["IdName"] = System.DBNull.Value;

                    }
                    else if (ddlProofIdentity.SelectedIndex == 2)
                    {
                        dataRow["IdNumber"] = txtPassNo.Text.Trim();
                        dataRow["IdExpDate"] = System.DBNull.Value;
                        dataRow["IdName"] = System.DBNull.Value;

                    }
                    else if (ddlProofIdentity.SelectedIndex == 3)
                    {
                        dataRow["IdNumber"] = txtPassNo.Text.Trim();
                        dataRow["IdExpDate"] = System.DBNull.Value;
                        dataRow["IdName"] = System.DBNull.Value;
                    }
                    else if (ddlProofIdentity.SelectedIndex == 4)
                    {
                        dataRow["IdNumber"] = txtPassNo.Text.Trim();
                        dataRow["IdExpDate"] = txtPassExpDate.Text.Trim();
                        dataRow["IdName"] = System.DBNull.Value;
                    }
                    else if (ddlProofIdentity.SelectedIndex == 5)
                    {
                        dataRow["IdNumber"] = txtPassNo.Text.Trim();
                        dataRow["IdExpDate"] = System.DBNull.Value;
                        dataRow["IdName"] = System.DBNull.Value;
                    }
                    else if (ddlProofIdentity.SelectedIndex == 6)
                    {
                        dataRow["IdNumber"] = txtPassNo.Text.Trim();
                        dataRow["IdExpDate"] = System.DBNull.Value;
                        dataRow["IdName"] = System.DBNull.Value;
                    }
                    else if (ddlProofIdentity.SelectedIndex == 7)
                    {
                        dataRow["IdNumber"] = txtPassOthr.Text.Trim();
                        dataRow["IdExpDate"] = System.DBNull.Value;
                        dataRow["IdName"] = txtPassNo.Text.Trim();
                    }
                    else if (ddlProofIdentity.SelectedIndex == 8 || ddlProofIdentity.SelectedIndex == 9)
                    {
                        dataRow["IdNumber"] = txtPassNo.Text.Trim();
                        dataRow["IdExpDate"] = System.DBNull.Value;
                        dataRow["IdName"] = System.DBNull.Value;
                    }
                    else
                    {
                        dataRow["IdNumber"] = System.DBNull.Value;
                        dataRow["IdExpDate"] = System.DBNull.Value;
                        dataRow["IdName"] = System.DBNull.Value;
                    }

                    if (CheckBox1.Checked == true)
                    {
                        dataRow["SameasPOIAddresFlagP1"] = "01";
                        // htParam.Add("@SameasPOIAddresFlagP1", "01");
                    }
                    else
                    {
                        dataRow["SameasPOIAddresFlagP1"] = "";
                    }

                    if (chkPerAddress.Checked == true)
                    {
                        dataRow["CnctTypeRel"] = "P1";
                        dataRow["AdrTypeRel"] = ddlAddressType.SelectedValue;
                        dataRow["AdrProfRel"] = ddlProofOfAddress.SelectedValue;
                        dataRow["Adr1Rel"] = txtAddressLine1.Text.Trim();
                        dataRow["Adr2Rel"] = txtAddressLine2.Text.Trim();
                        dataRow["Adr3Rel"] = txtAddressLine3.Text.Trim();
                        dataRow["CityRel"] = txtCity.Text.Trim();
                        dataRow["DistrictRel"] = txtDistrictname.Text;
                        dataRow["PostCodeRel"] = txtPinCode.Text;
                        dataRow["StateCodeRel"] = ddlState.SelectedValue;
                        dataRow["CntryCodeRel"] = ddlCountryCode.SelectedValue;
                    }
                    else
                    {
                        dataRow["CnctTypeRel"] = "";
                        dataRow["AdrTypeRel"] = System.DBNull.Value;
                        dataRow["AdrProfRel"] = System.DBNull.Value;
                        dataRow["Adr1Rel"] = System.DBNull.Value;
                        dataRow["Adr2Rel"] = System.DBNull.Value;
                        dataRow["Adr3Rel"] = System.DBNull.Value;
                        dataRow["CityRel"] = System.DBNull.Value;
                        dataRow["DistrictRel"] = System.DBNull.Value;
                        dataRow["PostCodeRel"] = System.DBNull.Value;
                        dataRow["StateCodeRel"] = System.DBNull.Value;
                        dataRow["CntryCodeRel"] = System.DBNull.Value;
                    }

                    if (chkCuurentAddress.Checked == true)
                    {
                        dataRow["SameasCurrentAddresFlagM1"] = "01";
                        //htParam.Add("@SameasCurrentAddresFlagM1", "01");
                    }
                    else
                    {
                        dataRow["SameasCurrentAddresFlagM1"] = "";
                    }

                    if (chkLocalAddress.Checked == true)
                    {
                        dataRow["CnctTypeRel1"] = "M1";
                        dataRow["AdrTypeRel1"] = ddlAddressType1.SelectedValue;
                        //dataRow["AdrProfRel1"] = ddlProofOfAddress.SelectedValue;
                        dataRow["Adr1Rel1"] = txtLocAddLine1.Text.Trim();
                        dataRow["Adr2Rel1"] = txtLocAddLine2.Text.Trim();
                        dataRow["Adr3Rel1"] = txtLocAddLine3.Text.Trim();
                        dataRow["CityRel1"] = txtCity1.Text.Trim();
                        dataRow["DistrictRel1"] = txtDistrict1.Text;
                        dataRow["PostCodeRel1"] = ddlPinCode1.Text;
                        dataRow["StateCodeRel1"] = ddlState1.SelectedValue;
                        dataRow["CntryCodeRel1"] = ddlCountryCode1.SelectedValue;
                    }
                    else
                    {
                        dataRow["CnctTypeRel1"] = "";
                        dataRow["AdrTypeRel1"] = System.DBNull.Value;
                        //dataRow["AdrProfRel1"] = System.DBNull.Value;
                        dataRow["Adr1Rel1"] = System.DBNull.Value;
                        dataRow["Adr2Rel1"] = System.DBNull.Value;
                        dataRow["Adr3Rel1"] = System.DBNull.Value;
                        dataRow["CityRel1"] = System.DBNull.Value;
                        dataRow["DistrictRel1"] = System.DBNull.Value;
                        dataRow["PostCodeRel1"] = System.DBNull.Value;
                        dataRow["StateCodeRel1"] = System.DBNull.Value;
                        dataRow["CntryCodeRel1"] = System.DBNull.Value;
                    }

                    if (chkCurrentAdd.Checked == true)
                    {
                        dataRow["SameasLocalAddressFlagJ1"] = "01";
                        //htParam.Add("@SameasLocalAddressFlagJ1", "01");
                    }
                    else
                    {
                        dataRow["SameasLocalAddressFlagJ1"] = "";
                    }

                    if (chkCorresAdd.Checked == true)
                    {
                        dataRow["SameasLocalAddressFlagJ2"] = "01";
                        // htParam.Add("@SameasLocalAddressFlagJ2", "01");
                    }
                    else
                    {
                        dataRow["SameasLocalAddressFlagJ2"] = "";
                    }

                    if (chkAddResident.Checked == true)
                    {
                        dataRow["CnctTypeRel2"] = "J1";
                        dataRow["AdrTypeRel2"] = ddlAddressType2.SelectedValue;
                        //dataRow["AdrProfRel1"] = ddlProofOfAddress.SelectedValue;
                        dataRow["Adr1Rel2"] = txtAddLine1.Text.Trim();
                        dataRow["Adr2Rel2"] = txtAddLine2.Text.Trim();
                        dataRow["Adr3Rel2"] = txtAddLine3.Text.Trim();
                        dataRow["CityRel2"] = txtCity2.Text.Trim();
                        dataRow["DistrictRel2"] = txtDistrict2.Text;
                        dataRow["PostCodeRel2"] = ddlPinCode2.Text;
                        dataRow["StateCodeRel2"] = ddlState2.SelectedValue;
                        dataRow["CntryCodeRel2"] = ddlIsoCountryCode.SelectedValue;
                    }
                    else
                    {
                        dataRow["CnctTypeRel2"] = "";
                        dataRow["AdrTypeRel2"] = System.DBNull.Value;
                        //dataRow["AdrProfRel1"] = System.DBNull.Value;
                        dataRow["Adr1Rel2"] = System.DBNull.Value;
                        dataRow["Adr2Rel2"] = System.DBNull.Value;
                        dataRow["Adr3Rel2"] = System.DBNull.Value;
                        dataRow["CityRel2"] = System.DBNull.Value;
                        dataRow["DistrictRel2"] = System.DBNull.Value;
                        dataRow["PostCodeRel2"] = System.DBNull.Value;
                        dataRow["StateCodeRel2"] = System.DBNull.Value;
                        dataRow["CntryCodeRel2"] = System.DBNull.Value;
                    }


                    dataRow["AddIdTypeRel"] = ddlProofOfAddress.SelectedValue;
                    if (chkPerAddress.Checked == true)
                    {
                        if (ddlProofOfAddress.SelectedIndex == 1)
                        {
                            dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                            dataRow["AddIdExpDateRel"] = txtPassExpDateAdd.Text.Trim();
                            dataRow["AddIdNameRel"] = System.DBNull.Value;
                        }

                        else if (ddlProofOfAddress.SelectedIndex == 2)
                        {
                            dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                            dataRow["AddIdExpDateRel"] = txtPassExpDateAdd.Text.Trim();
                            dataRow["AddIdNameRel"] = System.DBNull.Value;
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 3)
                        {
                            dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                            dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                            dataRow["AddIdNameRel"] = System.DBNull.Value;
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 4)
                        {
                            dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                            dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                            dataRow["AddIdNameRel"] = System.DBNull.Value;
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 5)
                        {
                            dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                            dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                            dataRow["AddIdNameRel"] = System.DBNull.Value;
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 6)
                        {
                            dataRow["AddIdNumberRel"] = txtPassOthrAdd.Text.Trim();
                            dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                            dataRow["AddIdNameRel"] = txtPassNoAdd.Text.Trim();
                        }

                        else
                        {
                            dataRow["AddIdNumberRel"] = System.DBNull.Value;
                            dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                            dataRow["AddIdNameRel"] = System.DBNull.Value;
                        }
                    }
                    else
                    {
                        dataRow["AddIdNumberRel"] = System.DBNull.Value;
                        dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                        dataRow["AddIdNameRel"] = System.DBNull.Value;
                    }

                    dataRow["DecDateRel"] = txtDate.Text.Trim();
                    dataRow["DecPlaceRel"] = txtPlace.Text.Trim();
                    dataRow["kycEmpNameRel"] = txtEmpName.Text.Trim();
                    dataRow["kycEmpCodeRel"] = txtEmpCode.Text.Trim();
                    dataRow["kycEmpBranchRel"] = txtEmpBranch.Text.Trim();
                    dataRow["kycEmpDesiRel"] = txtEmpDesignation.Text.Trim();
                    dataRow["kycVerDateRel"] = txtDate3.Text.Trim();

                    dataRow["kycCertDocRel"] = System.DBNull.Value;
                    dataRow["kycInstNameRel"] = txtInsName.Text.Trim();
                    dataRow["kycInstCodeRel"] = txtInsCode.Text.Trim();
                    dataRow["SVFlag"] = "P";

                    //dataRow["CREATEDBY"] =strUserId.ToString();

                    DtAdd.Rows.Add(dataRow);
                    //DataSet dsRel1 = new DataSet();
                    //dsRel1.Clear();
                    //dsRel1.Tables.Add(DtAdd);
                    Session["dsRel"] = DtAdd;
                    lblMsgConfirmYesNo.Text = "Relative Details Partially added successfully....Do you want to add more?.";
                    ClearTextcntrl();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "AsyncConfirmYesNo();", true);

                    //if (Request.QueryString["Status"].ToString() == "PMod")
                    //{
                    //    if (Request.QueryString["Action"].ToString() == "Edit")
                    //    {
                    //        btnAdd.Enabled = true;
                    //        btnPartialAdd.Enabled = false;
                    //    }
                    //}
                    Session["PSSubmit"] = "Y";

                }
                else
                {
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "btnPartialAdd_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region METHOD "FillRelatedPersondata"
        protected void FillRelatedPersonPartialdata()
        {
            try
            {
                objDAL = new DataAccessLayer("STAGINGConnectionString");
                dt = new DataTable();

                //DataTable dtNewAddRel = new DataTable();
                //dtNewAddRel = (DataTable)Session["dsRel"];

                DataTable dtNew = new DataTable();
                dtNew = (DataTable)Session["dsRel"];

                htParam.Clear();
                htParam.Add("@PSTempRefNo", Request.QueryString["refno"].ToString());
                htParam.Add("@PSTempRelRefNo", Request.QueryString["RelRefNo"].ToString().Trim());
                htParam.Add("@ActionFlag", "PMod");
                htParam.Add("@UserType", "");
                dt = objDAL.GetDataTable("Prc_GetRelatedPersonPartialDataForCKYC", htParam);

                if (Request.QueryString["RelRefNo"].ToString().Trim() == "0")
                {
                    //Request.QueryString["drNo"].ToString().Trim()
                    int idr;
                    idr = Convert.ToInt32(Request.QueryString["drNo"]);

                    txtRefNumber.Text = Convert.ToString(dtNew.Rows[idr]["FiRefNo"]);

                    //ddlOccuSubType.SelectedValue = 
                    //dtNewAddRel.Rows[idr].Delete();
                    //dtNewAddRel.AcceptChanges();
                    //dtNewAddRel.

                    txtRelRefNumber.Text = Convert.ToString(dtNew.Rows[idr]["RelRefNo"]);
                    txtKYCNum.Text = Convert.ToString(dtNew.Rows[idr]["RelatedPrsnKYCNo"]);
                    ddlRelType.SelectedValue = Convert.ToString(dtNew.Rows[idr]["RelationType"]);
                    cboTitle.SelectedValue = Convert.ToString(dtNew.Rows[idr]["PrefixRel"]);
                    txtGivenName.Text = Convert.ToString(dtNew.Rows[idr]["FNameRel"]);
                    txtMiddleName.Text = Convert.ToString(dtNew.Rows[idr]["MNameRel"]);
                    txtLastName.Text = Convert.ToString(dtNew.Rows[idr]["LNameRel"]);
                    cboTitle1.SelectedValue = Convert.ToString(dtNew.Rows[idr]["MaidPrefixRel"]);
                    txtGivenName1.Text = Convert.ToString(dtNew.Rows[idr]["MaidFNameRel"]);
                    txtMiddleName1.Text = Convert.ToString(dtNew.Rows[idr]["MaidMNameRel"]);
                    txtLastName1.Text = Convert.ToString(dtNew.Rows[idr]["MaidLNameRel"]);
                    if (Convert.ToString(dtNew.Rows[idr]["FSFlagRel"]) == "01")
                    {
                        rbtFS.SelectedValue = "F";
                    }
                    else
                    {
                        rbtFS.SelectedValue = "S";
                    }
                    cboTitle2.SelectedValue = Convert.ToString(dtNew.Rows[idr]["FatherPrefixRel"]);   //Need to save prefix while partial data save
                    txtGivenName2.Text = Convert.ToString(dtNew.Rows[idr]["FatherFNameRel"]);
                    txtMiddleName2.Text = Convert.ToString(dtNew.Rows[idr]["FatherMNameRel"]);
                    txtLastName2.Text = Convert.ToString(dtNew.Rows[idr]["FatherLNameRel"]);
                    cboTitle3.SelectedValue = Convert.ToString(dtNew.Rows[idr]["MotherPrefixRel"]);
                    txtGivenName3.Text = Convert.ToString(dtNew.Rows[idr]["MotherFNameRel"]);
                    txtMiddleName3.Text = Convert.ToString(dtNew.Rows[idr]["MotherMNameRel"]);
                    txtLastName3.Text = Convert.ToString(dtNew.Rows[idr]["MotherLNameRel"]);
                    txtDOB.Text = Convert.ToString(dtNew.Rows[idr]["DOBRel"]);
                    ddlMaritalStatus.SelectedValue = Convert.ToString(dtNew.Rows[idr]["MaritalStatusRel"]);
                    cboGender.SelectedValue = Convert.ToString(dtNew.Rows[idr]["GenderRel"]);
                    ddlCitizenship.SelectedValue = Convert.ToString(dtNew.Rows[idr]["CitizenshipRel"]);
                    ddlResStatus.SelectedValue = Convert.ToString(dtNew.Rows[idr]["ResiStatusRel"]);
                    ddlOccupation.SelectedValue = Convert.ToString(dtNew.Rows[idr]["OccuTypeRel"]);



                    //ddlIsoCountryCodeOthr.SelectedValue = Convert.ToString(dtNew.Rows[idr]["ISOCountryCodeRel"]);
                    if (dtNew.Rows[idr]["ISOCountryCodeRel"].ToString() != "")
                    {
                        ddlIsoCountryCodeOthr.SelectedValue = Convert.ToString(dtNew.Rows[idr]["ISOCountryCodeRel"]);
                    }

                    if (dtNew.Rows[idr]["ISOBirthPlaceCodeRel"].ToString() != "")
                    {
                        ddlIsoCountryCode2.SelectedValue = Convert.ToString(dtNew.Rows[idr]["ISOBirthPlaceCodeRel"]);
                    }

                    txtIDResTax.Text = Convert.ToString(dtNew.Rows[idr]["TaxIDNumberRel"]);
                    txtDOBRes.Text = Convert.ToString(dtNew.Rows[idr]["BirthCityRel"]);
                    ddlIsoCountry.SelectedValue = Convert.ToString(dtNew.Rows[idr]["ISOBirthPlaceCodeRel"]);
                    ddlProofIdentity.SelectedValue = Convert.ToString(dtNew.Rows[idr]["IdType"]);
                    ViewState["strIdName"] = Convert.ToString(dtNew.Rows[idr]["IdName"]);
                    ViewState["strIdNumber"] = Convert.ToString(dtNew.Rows[idr]["IdNumber"]);
                    ViewState["strIdExpDate"] = Convert.ToString(dtNew.Rows[idr]["IdExpDate"]);

                    if (ddlProofIdentity.SelectedValue == "Z")
                    {
                        txtPassOthr.Text = Convert.ToString(dtNew.Rows[idr]["IdNumber"]);
                        txtPassNo.Text = Convert.ToString(dtNew.Rows[idr]["IdName"]);
                    }
                    else
                    {
                        txtPassNo.Text = Convert.ToString(dtNew.Rows[idr]["IdNumber"]);
                        txtPassExpDate.Text = Convert.ToString(dtNew.Rows[idr]["IdExpDate"]);
                    }

                    if (Convert.ToString(dtNew.Rows[idr]["CnctTypeRel"]) == "P1")
                    {
                        chkPerAddress.Checked = true;
                    }
                    else
                    {
                        chkPerAddress.Checked = false;
                    }

                    ddlAddressType.SelectedValue = Convert.ToString(dtNew.Rows[idr]["AdrTypeRel"]);
                    ddlProofOfAddress.SelectedValue = Convert.ToString(dtNew.Rows[idr]["AdrProfRel"]);
                    txtAddressLine1.Text = Convert.ToString(dtNew.Rows[idr]["Adr1Rel"]);
                    txtAddressLine2.Text = Convert.ToString(dtNew.Rows[idr]["Adr2Rel"]);
                    txtAddressLine3.Text = Convert.ToString(dtNew.Rows[idr]["Adr3Rel"]);
                    txtCity.Text = Convert.ToString(dtNew.Rows[idr]["CityRel"]);
                    txtDistrictname.Text = Convert.ToString(dtNew.Rows[idr]["DistrictRel"]);
                    txtPinCode.Text = Convert.ToString(dtNew.Rows[idr]["PostCodeRel"]);
                    ddlState.SelectedValue = Convert.ToString(dtNew.Rows[idr]["StateCodeRel"]);
                    ddlCountryCode.SelectedValue = Convert.ToString(dtNew.Rows[idr]["CntryCodeRel"]);

                    if (dtNew.Rows[idr]["CnctTypeRel1"].ToString() != "" && dtNew.Rows[idr]["PostCodeRel1"].ToString() != "")
                    {
                        chkLocalAddress.Checked = true;
                    }
                    else
                    {
                        chkLocalAddress.Checked = false;
                    }
                    ddlAddressType1.SelectedValue = Convert.ToString(dtNew.Rows[idr]["AdrTypeRel1"]);
                    txtLocAddLine1.Text = Convert.ToString(dtNew.Rows[idr]["Adr1Rel1"]);
                    txtLocAddLine2.Text = Convert.ToString(dtNew.Rows[idr]["Adr2Rel1"]);
                    txtLocAddLine3.Text = Convert.ToString(dtNew.Rows[idr]["Adr3Rel1"]);
                    txtCity1.Text = Convert.ToString(dtNew.Rows[idr]["CityRel1"]);
                    txtDistrict1.Text = Convert.ToString(dtNew.Rows[idr]["DistrictRel1"]);
                    ddlPinCode1.Text = Convert.ToString(dtNew.Rows[idr]["PostCodeRel1"]);
                    ddlState1.SelectedValue = Convert.ToString(dtNew.Rows[idr]["StateCodeRel1"]);
                    ddlCountryCode1.SelectedValue = Convert.ToString(dtNew.Rows[idr]["CntryCodeRel1"]);

                    if (dtNew.Rows[idr]["CnctTypeRel2"].ToString() != "" && dtNew.Rows[idr]["PostCodeRel2"].ToString() != "")
                    {
                        chkAddResident.Checked = true;
                    }
                    else
                    {
                        chkAddResident.Checked = false;
                    }

                    ddlAddressType2.SelectedValue = Convert.ToString(dtNew.Rows[idr]["AdrTypeRel2"]);
                    txtAddLine1.Text = Convert.ToString(dtNew.Rows[idr]["Adr1Rel2"]);
                    txtAddLine2.Text = Convert.ToString(dtNew.Rows[idr]["Adr2Rel2"]);
                    txtAddLine3.Text = Convert.ToString(dtNew.Rows[idr]["Adr3Rel2"]);
                    txtCity2.Text = Convert.ToString(dtNew.Rows[idr]["CityRel2"]);
                    txtDistrict2.Text = Convert.ToString(dtNew.Rows[idr]["DistrictRel2"]);
                    ddlPinCode2.Text = Convert.ToString(dtNew.Rows[idr]["PostCodeRel2"]);
                    ddlState2.SelectedValue = Convert.ToString(dtNew.Rows[idr]["StateCodeRel2"]);
                    ddlIsoCountryCode.SelectedValue = Convert.ToString(dtNew.Rows[idr]["CntryCodeRel2"]);




                    ViewState["strAddIdName"] = Convert.ToString(dtNew.Rows[idr]["AddIdNameRel"]);
                    ViewState["strAddIdNumber"] = Convert.ToString(dtNew.Rows[idr]["AddIdNumberRel"]);
                    ViewState["strAddIdExpDate"] = Convert.ToString(dtNew.Rows[idr]["AddIdExpDateRel"]);


                    if (ddlProofOfAddress.SelectedValue == "99")
                    {
                        txtPassOthrAdd.Text = Convert.ToString(dtNew.Rows[idr]["AddIdNumberRel"]);
                        txtPassNoAdd.Text = Convert.ToString(dtNew.Rows[idr]["AddIdNameRel"]);
                    }
                    else
                    {
                        txtPassNoAdd.Text = Convert.ToString(dtNew.Rows[idr]["AddIdNumberRel"]);
                        txtPassExpDateAdd.Text = Convert.ToString(dtNew.Rows[idr]["AddIdExpDateRel"]);
                    }
                    txtPlace.Text = Convert.ToString(dtNew.Rows[idr]["DecPlaceRel"]);
                    txtDate.Text = Convert.ToString(dtNew.Rows[idr]["DecDateRel"]);
                    txtEmpName.Text = Convert.ToString(dtNew.Rows[idr]["kycEmpNameRel"]);
                    txtEmpCode.Text = Convert.ToString(dtNew.Rows[idr]["kycEmpCodeRel"]);
                    txtEmpDesignation.Text = Convert.ToString(dtNew.Rows[idr]["kycEmpDesiRel"]);
                    txtEmpBranch.Text = Convert.ToString(dtNew.Rows[idr]["kycEmpBranchRel"]);
                    txtInsName.Text = Convert.ToString(dtNew.Rows[idr]["kycInstNameRel"]);
                    txtInsCode.Text = Convert.ToString(dtNew.Rows[idr]["kycInstCodeRel"]);
                    txtDate3.Text = Convert.ToString(dtNew.Rows[idr]["kycVerDateRel"]);



                    //if (ddlProofIdentity.SelectedItem.Text == ddlProofOfAddress.SelectedItem.Text)
                    //{
                    //    CheckBox1.Checked = true;
                    //}
                    //else
                    //{
                    //    CheckBox1.Checked = false;
                    //}

                    if (Convert.ToString(dtNew.Rows[idr]["SameasPOIAddresFlagP1"]) == "01")
                    {
                        CheckBox1.Checked = true;
                    }
                    else
                    {
                        CheckBox1.Checked = false;
                    }

                    if (Convert.ToString(dtNew.Rows[idr]["SameasCurrentAddresFlagM1"]) == "01")
                    {
                        chkCuurentAddress.Checked = true;
                    }
                    else
                    {
                        chkCuurentAddress.Checked = false;
                    }

                    if (Convert.ToString(dtNew.Rows[idr]["SameasLocalAddressFlagJ1"]) == "01")
                    {
                        chkCurrentAdd.Checked = true;
                    }
                    else
                    {
                        chkCurrentAdd.Checked = false;
                    }

                    if (Convert.ToString(dtNew.Rows[idr]["SameasLocalAddressFlagJ2"]) == "01")
                    {
                        chkCorresAdd.Checked = true;
                    }
                    else
                    {
                        chkCorresAdd.Checked = false;
                    }

                    if (ddlOccupation.SelectedValue == "B" || ddlOccupation.SelectedValue == "X")
                    {
                        FillSubOccuType(ddlOccupation, ddlOccuSubType);
                        ddlOccuSubType.SelectedIndex = 1;
                    }
                    else
                    {
                        FillSubOccuType(ddlOccupation, ddlOccuSubType);
                    }

                    //ViewState["DtAdd"] = dtNewAddRel;
                }
                else
                {

                    //dtNewAddRel.Rows[0].Delete();
                    //dtNewAddRel.AcceptChanges();

                    txtRefNumber.Text = Convert.ToString(dt.Rows[0]["FiRefNo"]);

                    txtRelRefNumber.Text = Convert.ToString(dt.Rows[0]["RelRefNo"]);
                    txtKYCNum.Text = Convert.ToString(dt.Rows[0]["RelatedPrsnKYCNo"]);
                    ddlRelType.SelectedValue = Convert.ToString(dt.Rows[0]["RelationType"]);
                    cboTitle.SelectedValue = Convert.ToString(dt.Rows[0]["PrefixRel"]);
                    txtGivenName.Text = Convert.ToString(dt.Rows[0]["FNameRel"]);
                    txtMiddleName.Text = Convert.ToString(dt.Rows[0]["MNameRel"]);
                    txtLastName.Text = Convert.ToString(dt.Rows[0]["LNameRel"]);
                    cboTitle1.SelectedValue = Convert.ToString(dt.Rows[0]["MaidPrefixRel"]);
                    txtGivenName1.Text = Convert.ToString(dt.Rows[0]["MaidFNameRel"]);
                    txtMiddleName1.Text = Convert.ToString(dt.Rows[0]["MaidMNameRel"]);
                    txtLastName1.Text = Convert.ToString(dt.Rows[0]["MaidLNameRel"]);
                    if (Convert.ToString(dt.Rows[0]["FSFlagRel"]) == "01")
                    {
                        rbtFS.SelectedValue = "F";
                    }
                    else
                    {
                        rbtFS.SelectedValue = "S";
                    }
                    cboTitle2.SelectedValue = Convert.ToString(dt.Rows[0]["FatherPrefixRel"]);
                    txtGivenName2.Text = Convert.ToString(dt.Rows[0]["FatherFNameRel"]);
                    txtMiddleName2.Text = Convert.ToString(dt.Rows[0]["FatherMNameRel"]);
                    txtLastName2.Text = Convert.ToString(dt.Rows[0]["FatherLNameRel"]);
                    cboTitle3.SelectedValue = Convert.ToString(dt.Rows[0]["MotherPrefixRel"]);
                    txtGivenName3.Text = Convert.ToString(dt.Rows[0]["MotherFNameRel"]);
                    txtMiddleName3.Text = Convert.ToString(dt.Rows[0]["MotherMNameRel"]);
                    txtLastName3.Text = Convert.ToString(dt.Rows[0]["MotherLNameRel"]);
                    txtDOB.Text = Convert.ToString(dt.Rows[0]["DOBRel"]);
                    ddlMaritalStatus.SelectedValue = Convert.ToString(dt.Rows[0]["MaritalStatusRel"]);
                    cboGender.SelectedValue = Convert.ToString(dt.Rows[0]["GenderRel"]);
                    ddlCitizenship.SelectedValue = Convert.ToString(dt.Rows[0]["CitizenshipRel"]);
                    ddlResStatus.SelectedValue = Convert.ToString(dt.Rows[0]["ResiStatusRel"]);
                    ddlOccupation.SelectedValue = Convert.ToString(dt.Rows[0]["OccuTypeRel"]);



                    if (dt.Rows[0]["ISOCountryCodeRel"] != "")
                    {
                        ddlIsoCountryCodeOthr.SelectedValue = Convert.ToString(dt.Rows[0]["ISOCountryCodeRel"]);
                    }

                    if (dt.Rows[0]["ISOBirthPlaceCodeRel"].ToString() != "")
                    {
                        ddlIsoCountryCode2.SelectedValue = Convert.ToString(dt.Rows[0]["ISOBirthPlaceCodeRel"]);
                    }


                    txtIDResTax.Text = Convert.ToString(dt.Rows[0]["TaxIDNumberRel"]);
                    txtDOBRes.Text = Convert.ToString(dt.Rows[0]["BirthCityRel"]);
                    ddlIsoCountry.SelectedValue = Convert.ToString(dt.Rows[0]["ISOBirthPlaceCodeRel"]);
                    ddlProofIdentity.SelectedValue = Convert.ToString(dt.Rows[0]["IdType"]);

                    if (Convert.ToString(dt.Rows[0]["CnctTypeRel"]) == "P1")
                    {
                        chkPerAddress.Checked = true;
                    }
                    else
                    {
                        chkPerAddress.Checked = false;
                    }

                    ddlAddressType.SelectedValue = Convert.ToString(dt.Rows[0]["AdrTypeRel"]);
                    ddlProofOfAddress.SelectedValue = Convert.ToString(dt.Rows[0]["AdrProfRel"]);
                    txtAddressLine1.Text = Convert.ToString(dt.Rows[0]["Adr1Rel"]);
                    txtAddressLine2.Text = Convert.ToString(dt.Rows[0]["Adr2Rel"]);
                    txtAddressLine3.Text = Convert.ToString(dt.Rows[0]["Adr3Rel"]);
                    txtCity.Text = Convert.ToString(dt.Rows[0]["CityRel"]);
                    txtDistrictname.Text = Convert.ToString(dt.Rows[0]["DistrictRel"]);
                    txtPinCode.Text = Convert.ToString(dt.Rows[0]["PostCodeRel"]);
                    ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["StateCodeRel"]);
                    ddlCountryCode.SelectedValue = Convert.ToString(dt.Rows[0]["CntryCodeRel"]);

                    if (dt.Rows[0]["CnctTypeRel1"].ToString() != "")
                    {
                        chkLocalAddress.Checked = true;
                    }
                    else
                    {
                        chkLocalAddress.Checked = false;
                    }
                    ddlAddressType1.SelectedValue = Convert.ToString(dt.Rows[0]["AdrTypeRel1"]);
                    txtLocAddLine1.Text = Convert.ToString(dt.Rows[0]["Adr1Rel1"]);
                    txtLocAddLine2.Text = Convert.ToString(dt.Rows[0]["Adr2Rel1"]);
                    txtLocAddLine3.Text = Convert.ToString(dt.Rows[0]["Adr3Rel1"]);
                    txtCity1.Text = Convert.ToString(dt.Rows[0]["CityRel1"]);
                    txtDistrict1.Text = Convert.ToString(dt.Rows[0]["DistrictRel1"]);
                    ddlPinCode1.Text = Convert.ToString(dt.Rows[0]["PostCodeRel1"]);
                    ddlState1.SelectedValue = Convert.ToString(dt.Rows[0]["StateCodeRel1"]);
                    ddlCountryCode1.SelectedValue = Convert.ToString(dt.Rows[0]["CntryCodeRel1"]);

                    if (dt.Rows[0]["CnctTypeRel2"].ToString() != "")
                    {
                        chkAddResident.Checked = true;
                    }
                    else
                    {
                        chkAddResident.Checked = false;
                    }

                    ddlAddressType2.SelectedValue = Convert.ToString(dt.Rows[0]["AdrTypeRel2"]);
                    txtAddLine1.Text = Convert.ToString(dt.Rows[0]["Adr1Rel2"]);
                    txtAddLine2.Text = Convert.ToString(dt.Rows[0]["Adr2Rel2"]);
                    txtAddLine3.Text = Convert.ToString(dt.Rows[0]["Adr3Rel2"]);
                    txtCity2.Text = Convert.ToString(dt.Rows[0]["CityRel2"]);
                    txtDistrict2.Text = Convert.ToString(dt.Rows[0]["DistrictRel2"]);
                    ddlPinCode2.Text = Convert.ToString(dt.Rows[0]["PostCodeRel2"]);
                    ddlState2.SelectedValue = Convert.ToString(dt.Rows[0]["StateCodeRel2"]);
                    ddlIsoCountryCode.SelectedValue = Convert.ToString(dt.Rows[0]["CntryCodeRel2"]);


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


                    ViewState["strAddIdName"] = Convert.ToString(dt.Rows[0]["AddIdNameRel"]);
                    ViewState["strAddIdNumber"] = Convert.ToString(dt.Rows[0]["AddIdNumberRel"]);
                    ViewState["strAddIdExpDate"] = Convert.ToString(dt.Rows[0]["AddIdExpDateRel"]);


                    if (ddlProofOfAddress.SelectedValue == "99")
                    {
                        txtPassOthrAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNumberRel"]);
                        txtPassNoAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNameRel"]);
                    }
                    else
                    {
                        txtPassNoAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNumberRel"]);
                        txtPassExpDateAdd.Text = Convert.ToString(dt.Rows[0]["AddIdExpDateRel"]);
                    }
                    txtPlace.Text = Convert.ToString(dt.Rows[0]["DecPlaceRel"]);
                    txtDate.Text = Convert.ToString(dt.Rows[0]["DecDateRel"]);
                    txtEmpName.Text = Convert.ToString(dt.Rows[0]["kycEmpNameRel"]);
                    txtEmpCode.Text = Convert.ToString(dt.Rows[0]["kycEmpCodeRel"]);
                    txtEmpDesignation.Text = Convert.ToString(dt.Rows[0]["kycEmpDesiRel"]);
                    txtEmpBranch.Text = Convert.ToString(dt.Rows[0]["kycEmpBranchRel"]);
                    txtInsName.Text = Convert.ToString(dt.Rows[0]["kycInstNameRel"]);
                    txtInsCode.Text = Convert.ToString(dt.Rows[0]["kycInstCodeRel"]);
                    txtDate3.Text = Convert.ToString(dt.Rows[0]["kycVerDateRel"]);

                    if (ddlOccupation.SelectedValue == "B" || ddlOccupation.SelectedValue == "X")
                    {
                        FillSubOccuType(ddlOccupation, ddlOccuSubType);
                        ddlOccuSubType.SelectedIndex = 1;
                    }
                    else
                    {
                        FillSubOccuType(ddlOccupation, ddlOccuSubType);
                    }

                    //ViewState["DtAdd"] = dtNewAddRel;
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "FillRelatedPersonPartialdata", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region METHOD "FillRelatedPersonDataForView"
        protected void FillRelatedPersonViewData()
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();

                //DataTable dtNewAddRel = new DataTable();
                //dtNewAddRel = (DataTable)Session["dsRel"];

                DataTable dtNew = new DataTable();
                dtNew = (DataTable)Session["dsRel"];

                htParam.Clear();
                htParam.Add("@PSTempRefNo", Request.QueryString["refno"].ToString());
                htParam.Add("@PSTempRelRefNo", Request.QueryString["RelRefNo"].ToString().Trim());
                htParam.Add("@ActionFlag", "PMod");
                htParam.Add("@UserType", "");
                dt = objDAL.GetDataTable("Prc_GetRelatedPersonPartialDataForCKYC", htParam);

                if (Request.QueryString["RelRefNo"].ToString().Trim() == "0")
                {
                    //Request.QueryString["drNo"].ToString().Trim()
                    int idr;
                    idr = Convert.ToInt32(Request.QueryString["drNo"]);

                    txtRefNumber.Text = Convert.ToString(dtNew.Rows[idr]["FiRefNo"]);

                    //ddlOccuSubType.SelectedValue = 
                    //dtNewAddRel.Rows[idr].Delete();
                    //dtNewAddRel.AcceptChanges();
                    //dtNewAddRel.

                    txtRelRefNumber.Text = Convert.ToString(dtNew.Rows[idr]["RelRefNo"]);
                    txtKYCNum.Text = Convert.ToString(dtNew.Rows[idr]["RelatedPrsnKYCNo"]);
                    ddlRelType.SelectedValue = Convert.ToString(dtNew.Rows[idr]["RelationType"]);
                    cboTitle.SelectedValue = Convert.ToString(dtNew.Rows[idr]["PrefixRel"]);
                    txtGivenName.Text = Convert.ToString(dtNew.Rows[idr]["FNameRel"]);
                    txtMiddleName.Text = Convert.ToString(dtNew.Rows[idr]["MNameRel"]);
                    txtLastName.Text = Convert.ToString(dtNew.Rows[idr]["LNameRel"]);
                    cboTitle1.SelectedValue = Convert.ToString(dtNew.Rows[idr]["MaidPrefixRel"]);
                    txtGivenName1.Text = Convert.ToString(dtNew.Rows[idr]["MaidFNameRel"]);
                    txtMiddleName1.Text = Convert.ToString(dtNew.Rows[idr]["MaidMNameRel"]);
                    txtLastName1.Text = Convert.ToString(dtNew.Rows[idr]["MaidLNameRel"]);
                    if (Convert.ToString(dtNew.Rows[idr]["FSFlagRel"]) == "01")
                    {
                        rbtFS.SelectedValue = "F";
                    }
                    else
                    {
                        rbtFS.SelectedValue = "S";
                    }
                    cboTitle2.SelectedValue = Convert.ToString(dtNew.Rows[idr]["FatherPrefixRel"]);   //Need to save prefix while partial data save
                    txtGivenName2.Text = Convert.ToString(dtNew.Rows[idr]["FatherFNameRel"]);
                    txtMiddleName2.Text = Convert.ToString(dtNew.Rows[idr]["FatherMNameRel"]);
                    txtLastName2.Text = Convert.ToString(dtNew.Rows[idr]["FatherLNameRel"]);
                    cboTitle3.SelectedValue = Convert.ToString(dtNew.Rows[idr]["MotherPrefixRel"]);
                    txtGivenName3.Text = Convert.ToString(dtNew.Rows[idr]["MotherFNameRel"]);
                    txtMiddleName3.Text = Convert.ToString(dtNew.Rows[idr]["MotherMNameRel"]);
                    txtLastName3.Text = Convert.ToString(dtNew.Rows[idr]["MotherLNameRel"]);
                    txtDOB.Text = Convert.ToString(dtNew.Rows[idr]["DOBRel"]);
                    ddlMaritalStatus.SelectedValue = Convert.ToString(dtNew.Rows[idr]["MaritalStatusRel"]);
                    cboGender.SelectedValue = Convert.ToString(dtNew.Rows[idr]["GenderRel"]);
                    ddlCitizenship.SelectedValue = Convert.ToString(dtNew.Rows[idr]["CitizenshipRel"]);
                    ddlResStatus.SelectedValue = Convert.ToString(dtNew.Rows[idr]["ResiStatusRel"]);
                    ddlOccupation.SelectedValue = Convert.ToString(dtNew.Rows[idr]["OccuTypeRel"]);



                    //ddlIsoCountryCodeOthr.SelectedValue = Convert.ToString(dtNew.Rows[idr]["ISOCountryCodeRel"]);
                    if (dtNew.Rows[idr]["ISOCountryCodeRel"].ToString() != "")
                    {
                        ddlIsoCountryCodeOthr.SelectedValue = Convert.ToString(dtNew.Rows[idr]["ISOCountryCodeRel"]);
                    }

                    if (dtNew.Rows[idr]["ISOBirthPlaceCodeRel"].ToString() != "")
                    {
                        ddlIsoCountryCode2.SelectedValue = Convert.ToString(dtNew.Rows[idr]["ISOBirthPlaceCodeRel"]);
                    }

                    txtIDResTax.Text = Convert.ToString(dtNew.Rows[idr]["TaxIDNumberRel"]);
                    txtDOBRes.Text = Convert.ToString(dtNew.Rows[idr]["BirthCityRel"]);

                    if (Convert.ToString(dtNew.Rows[idr]["ISOBirthPlaceCodeRel"]) != "" && Convert.ToString(dtNew.Rows[idr]["TaxIDNumberRel"]) != "" && Convert.ToString(dtNew.Rows[idr]["BirthCityRel"]) != "")
                    {
                        chkTick.Checked = true;
                    }
                    else
                    {
                        chkTick.Checked = false;
                    }

                    ddlIsoCountry.SelectedValue = Convert.ToString(dtNew.Rows[idr]["ISOBirthPlaceCodeRel"]);
                    ddlProofIdentity.SelectedValue = Convert.ToString(dtNew.Rows[idr]["IdType"]);
                    ViewState["strIdName"] = Convert.ToString(dtNew.Rows[idr]["IdName"]);
                    ViewState["strIdNumber"] = Convert.ToString(dtNew.Rows[idr]["IdNumber"]);
                    ViewState["strIdExpDate"] = Convert.ToString(dtNew.Rows[idr]["IdExpDate"]);

                    if (ddlProofIdentity.SelectedValue == "Z")
                    {
                        txtPassOthr.Text = Convert.ToString(dtNew.Rows[idr]["IdNumber"]);
                        txtPassNo.Text = Convert.ToString(dtNew.Rows[idr]["IdName"]);
                    }
                    else
                    {
                        txtPassNo.Text = Convert.ToString(dtNew.Rows[idr]["IdNumber"]);
                        txtPassExpDate.Text = Convert.ToString(dtNew.Rows[idr]["IdExpDate"]);
                    }

                    if (Convert.ToString(dtNew.Rows[idr]["CnctTypeRel"]) == "P1")
                    {
                        chkPerAddress.Checked = true;
                    }
                    else
                    {
                        chkPerAddress.Checked = false;
                    }

                    ddlAddressType.SelectedValue = Convert.ToString(dtNew.Rows[idr]["AdrTypeRel"]);
                    ddlProofOfAddress.SelectedValue = Convert.ToString(dtNew.Rows[idr]["AdrProfRel"]);
                    txtAddressLine1.Text = Convert.ToString(dtNew.Rows[idr]["Adr1Rel"]);
                    txtAddressLine2.Text = Convert.ToString(dtNew.Rows[idr]["Adr2Rel"]);
                    txtAddressLine3.Text = Convert.ToString(dtNew.Rows[idr]["Adr3Rel"]);
                    txtCity.Text = Convert.ToString(dtNew.Rows[idr]["CityRel"]);
                    txtDistrictname.Text = Convert.ToString(dtNew.Rows[idr]["DistrictRel"]);
                    txtPinCode.Text = Convert.ToString(dtNew.Rows[idr]["PostCodeRel"]);
                    //ddlState.SelectedValue = Convert.ToString(dtNew.Rows[idr]["StateCodeRel"]);
                    if (Convert.ToString(dtNew.Rows[idr]["CntryCodeRel"]) == "IN")
                    {
                        dvState.Visible = true;
                        txtState.Visible = false;
                        ddlState.SelectedValue = Convert.ToString(dtNew.Rows[idr]["StateCodeRel"]);
                    }
                    else
                    {
                        dvState.Visible = false;
                        txtState.Visible = true;
                        txtState.Text = Convert.ToString(dtNew.Rows[idr]["StateCodeRel"]);
                    }
                    ddlCountryCode.SelectedValue = Convert.ToString(dtNew.Rows[idr]["CntryCodeRel"]);

                    if (dtNew.Rows[idr]["CnctTypeRel1"].ToString() != "" && dtNew.Rows[idr]["PostCodeRel1"].ToString() != "")
                    {
                        chkLocalAddress.Checked = true;
                    }
                    else
                    {
                        chkLocalAddress.Checked = false;
                    }
                    ddlAddressType1.SelectedValue = Convert.ToString(dtNew.Rows[idr]["AdrTypeRel1"]);
                    txtLocAddLine1.Text = Convert.ToString(dtNew.Rows[idr]["Adr1Rel1"]);
                    txtLocAddLine2.Text = Convert.ToString(dtNew.Rows[idr]["Adr2Rel1"]);
                    txtLocAddLine3.Text = Convert.ToString(dtNew.Rows[idr]["Adr3Rel1"]);
                    txtCity1.Text = Convert.ToString(dtNew.Rows[idr]["CityRel1"]);
                    txtDistrict1.Text = Convert.ToString(dtNew.Rows[idr]["DistrictRel1"]);
                    ddlPinCode1.Text = Convert.ToString(dtNew.Rows[idr]["PostCodeRel1"]);
                    //ddlState1.SelectedValue = Convert.ToString(dtNew.Rows[idr]["StateCodeRel1"]);
                    if (Convert.ToString(dtNew.Rows[idr]["CntryCodeRel1"]) == "IN")
                    {
                        dvState1.Visible = true;
                        txtState1.Visible = false;
                        ddlState1.SelectedValue = Convert.ToString(dtNew.Rows[idr]["StateCodeRel1"]);
                    }
                    else
                    {
                        dvState1.Visible = false;
                        txtState1.Visible = true;
                        txtState1.Text = Convert.ToString(dtNew.Rows[idr]["StateCodeRel1"]);
                    }
                    ddlCountryCode1.SelectedValue = Convert.ToString(dtNew.Rows[idr]["CntryCodeRel1"]);

                    if (dtNew.Rows[idr]["CnctTypeRel2"].ToString() != "" && dtNew.Rows[idr]["PostCodeRel2"].ToString() != "")
                    {
                        chkAddResident.Checked = true;
                    }
                    else
                    {
                        chkAddResident.Checked = false;
                    }

                    ddlAddressType2.SelectedValue = Convert.ToString(dtNew.Rows[idr]["AdrTypeRel2"]);
                    txtAddLine1.Text = Convert.ToString(dtNew.Rows[idr]["Adr1Rel2"]);
                    txtAddLine2.Text = Convert.ToString(dtNew.Rows[idr]["Adr2Rel2"]);
                    txtAddLine3.Text = Convert.ToString(dtNew.Rows[idr]["Adr3Rel2"]);
                    txtCity2.Text = Convert.ToString(dtNew.Rows[idr]["CityRel2"]);
                    txtDistrict2.Text = Convert.ToString(dtNew.Rows[idr]["DistrictRel2"]);
                    ddlPinCode2.Text = Convert.ToString(dtNew.Rows[idr]["PostCodeRel2"]);
                    //ddlState2.SelectedValue = Convert.ToString(dtNew.Rows[idr]["StateCodeRel2"]);
                    if (Convert.ToString(dtNew.Rows[idr]["CntryCodeRel2"]) == "IN")
                    {
                        dvState2.Visible = true;
                        txtState2.Visible = false;
                        ddlState2.SelectedValue = Convert.ToString(dtNew.Rows[idr]["StateCodeRel2"]);
                    }
                    else
                    {
                        dvState2.Visible = false;
                        txtState2.Visible = true;
                        txtState2.Text = Convert.ToString(dtNew.Rows[idr]["StateCodeRel2"]);
                    }
                    ddlIsoCountryCode.SelectedValue = Convert.ToString(dtNew.Rows[idr]["CntryCodeRel2"]);

                    ViewState["strAddIdName"] = Convert.ToString(dtNew.Rows[idr]["AddIdNameRel"]);
                    ViewState["strAddIdNumber"] = Convert.ToString(dtNew.Rows[idr]["AddIdNumberRel"]);
                    ViewState["strAddIdExpDate"] = Convert.ToString(dtNew.Rows[idr]["AddIdExpDateRel"]);

                    if (ddlProofOfAddress.SelectedValue == "99")
                    {
                        txtPassOthrAdd.Text = Convert.ToString(dtNew.Rows[idr]["AddIdNumberRel"]);
                        txtPassNoAdd.Text = Convert.ToString(dtNew.Rows[idr]["AddIdNameRel"]);
                    }
                    else
                    {
                        txtPassNoAdd.Text = Convert.ToString(dtNew.Rows[idr]["AddIdNumberRel"]);
                        txtPassExpDateAdd.Text = Convert.ToString(dtNew.Rows[idr]["AddIdExpDateRel"]);
                    }
                    txtPlace.Text = Convert.ToString(dtNew.Rows[idr]["DecPlaceRel"]);
                    txtDate.Text = Convert.ToString(dtNew.Rows[idr]["DecDateRel"]);
                    txtEmpName.Text = Convert.ToString(dtNew.Rows[idr]["kycEmpNameRel"]);
                    txtEmpCode.Text = Convert.ToString(dtNew.Rows[idr]["kycEmpCodeRel"]);
                    txtEmpDesignation.Text = Convert.ToString(dtNew.Rows[idr]["kycEmpDesiRel"]);
                    txtEmpBranch.Text = Convert.ToString(dtNew.Rows[idr]["kycEmpBranchRel"]);
                    txtInsName.Text = Convert.ToString(dtNew.Rows[idr]["kycInstNameRel"]);
                    txtInsCode.Text = Convert.ToString(dtNew.Rows[idr]["kycInstCodeRel"]);
                    txtDate3.Text = Convert.ToString(dtNew.Rows[idr]["kycVerDateRel"]);


                    //if (ddlProofIdentity.SelectedItem.Text == ddlProofOfAddress.SelectedItem.Text)
                    //{
                    //    CheckBox1.Checked = true;
                    //}
                    //else
                    //{
                    //    CheckBox1.Checked = false;
                    //}

                    if (Convert.ToString(dtNew.Rows[idr]["SameasPOIAddresFlagP1"]) == "01")
                    {
                        CheckBox1.Checked = true;
                    }
                    else
                    {
                        CheckBox1.Checked = false;
                    }

                    if (Convert.ToString(dtNew.Rows[idr]["SameasCurrentAddresFlagM1"]) == "01")
                    {
                        chkCuurentAddress.Checked = true;
                    }
                    else
                    {
                        chkCuurentAddress.Checked = false;
                    }

                    if (Convert.ToString(dtNew.Rows[idr]["SameasLocalAddressFlagJ1"]) == "01")
                    {
                        chkCurrentAdd.Checked = true;
                    }
                    else
                    {
                        chkCurrentAdd.Checked = false;
                    }

                    if (Convert.ToString(dtNew.Rows[idr]["SameasLocalAddressFlagJ2"]) == "01")
                    {
                        chkCorresAdd.Checked = true;
                    }
                    else
                    {
                        chkCorresAdd.Checked = false;
                    }

                    if (ddlOccupation.SelectedValue == "B" || ddlOccupation.SelectedValue == "X")
                    {
                        FillSubOccuType(ddlOccupation, ddlOccuSubType);
                        ddlOccuSubType.SelectedIndex = 1;
                    }
                    else
                    {
                        FillSubOccuType(ddlOccupation, ddlOccuSubType);
                    }

                    //ViewState["DtAdd"] = dtNewAddRel;
                }
                else
                {

                    //dtNewAddRel.Rows[0].Delete();
                    //dtNewAddRel.AcceptChanges();

                    txtRefNumber.Text = Convert.ToString(dt.Rows[0]["FiRefNo"]);

                    txtRelRefNumber.Text = Convert.ToString(dt.Rows[0]["RelRefNo"]);
                    txtKYCNum.Text = Convert.ToString(dt.Rows[0]["RelatedPrsnKYCNo"]);
                    ddlRelType.SelectedValue = Convert.ToString(dt.Rows[0]["RelationType"]);
                    cboTitle.SelectedValue = Convert.ToString(dt.Rows[0]["PrefixRel"]);
                    txtGivenName.Text = Convert.ToString(dt.Rows[0]["FNameRel"]);
                    txtMiddleName.Text = Convert.ToString(dt.Rows[0]["MNameRel"]);
                    txtLastName.Text = Convert.ToString(dt.Rows[0]["LNameRel"]);
                    cboTitle1.SelectedValue = Convert.ToString(dt.Rows[0]["MaidPrefixRel"]);
                    txtGivenName1.Text = Convert.ToString(dt.Rows[0]["MaidFNameRel"]);
                    txtMiddleName1.Text = Convert.ToString(dt.Rows[0]["MaidMNameRel"]);
                    txtLastName1.Text = Convert.ToString(dt.Rows[0]["MaidLNameRel"]);
                    if (Convert.ToString(dt.Rows[0]["FSFlagRel"]) == "01")
                    {
                        rbtFS.SelectedValue = "F";
                    }
                    else
                    {
                        rbtFS.SelectedValue = "S";
                    }
                    cboTitle2.SelectedValue = Convert.ToString(dt.Rows[0]["FatherPrefixRel"]);
                    txtGivenName2.Text = Convert.ToString(dt.Rows[0]["FatherFNameRel"]);
                    txtMiddleName2.Text = Convert.ToString(dt.Rows[0]["FatherMNameRel"]);
                    txtLastName2.Text = Convert.ToString(dt.Rows[0]["FatherLNameRel"]);
                    cboTitle3.SelectedValue = Convert.ToString(dt.Rows[0]["MotherPrefixRel"]);
                    txtGivenName3.Text = Convert.ToString(dt.Rows[0]["MotherFNameRel"]);
                    txtMiddleName3.Text = Convert.ToString(dt.Rows[0]["MotherMNameRel"]);
                    txtLastName3.Text = Convert.ToString(dt.Rows[0]["MotherLNameRel"]);
                    txtDOB.Text = Convert.ToString(dt.Rows[0]["DOBRel"]);
                    ddlMaritalStatus.SelectedValue = Convert.ToString(dt.Rows[0]["MaritalStatusRel"]);
                    cboGender.SelectedValue = Convert.ToString(dt.Rows[0]["GenderRel"]);
                    ddlCitizenship.SelectedValue = Convert.ToString(dt.Rows[0]["CitizenshipRel"]);
                    ddlResStatus.SelectedValue = Convert.ToString(dt.Rows[0]["ResiStatusRel"]);
                    ddlOccupation.SelectedValue = Convert.ToString(dt.Rows[0]["OccuTypeRel"]);

                    ddlIsoCountryCodeOthr.SelectedValue = Convert.ToString(dt.Rows[0]["ISOCountryCodeRel"]);

                    if (dt.Rows[0]["ISOBirthPlaceCodeRel"].ToString() != "")
                    {
                        ddlIsoCountryCode2.SelectedValue = Convert.ToString(dt.Rows[0]["ISOBirthPlaceCodeRel"]);
                    }


                    txtIDResTax.Text = Convert.ToString(dt.Rows[0]["TaxIDNumberRel"]);
                    txtDOBRes.Text = Convert.ToString(dt.Rows[0]["BirthCityRel"]);
                    ddlIsoCountry.SelectedValue = Convert.ToString(dt.Rows[0]["ISOBirthPlaceCodeRel"]);
                    ddlProofIdentity.SelectedValue = Convert.ToString(dt.Rows[0]["IdType"]);

                    if (Convert.ToString(dt.Rows[0]["ISOBirthPlaceCodeRel"]) != "" && Convert.ToString(dt.Rows[0]["TaxIDNumberRel"]) != "" && Convert.ToString(dt.Rows[0]["BirthCityRel"]) != "")
                    {
                        chkTick.Checked = true;
                    }
                    else
                    {
                        chkTick.Checked = false;
                    }


                    if (Convert.ToString(dt.Rows[0]["CnctTypeRel"]) == "P1")
                    {
                        chkPerAddress.Checked = true;
                    }
                    else
                    {
                        chkPerAddress.Checked = false;
                    }

                    ddlAddressType.SelectedValue = Convert.ToString(dt.Rows[0]["AdrTypeRel"]);
                    ddlProofOfAddress.SelectedValue = Convert.ToString(dt.Rows[0]["AdrProfRel"]);
                    txtAddressLine1.Text = Convert.ToString(dt.Rows[0]["Adr1Rel"]);
                    txtAddressLine2.Text = Convert.ToString(dt.Rows[0]["Adr2Rel"]);
                    txtAddressLine3.Text = Convert.ToString(dt.Rows[0]["Adr3Rel"]);
                    txtCity.Text = Convert.ToString(dt.Rows[0]["CityRel"]);
                    txtDistrictname.Text = Convert.ToString(dt.Rows[0]["DistrictRel"]);
                    txtPinCode.Text = Convert.ToString(dt.Rows[0]["PostCodeRel"]);
                    ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["StateCodeRel"]);
                    ddlCountryCode.SelectedValue = Convert.ToString(dt.Rows[0]["CntryCodeRel"]);

                    if (dt.Rows[0]["CnctTypeRel1"].ToString() != "")
                    {
                        chkLocalAddress.Checked = true;
                    }
                    else
                    {
                        chkLocalAddress.Checked = false;
                    }
                    ddlAddressType1.SelectedValue = Convert.ToString(dt.Rows[0]["AdrTypeRel1"]);
                    txtLocAddLine1.Text = Convert.ToString(dt.Rows[0]["Adr1Rel1"]);
                    txtLocAddLine2.Text = Convert.ToString(dt.Rows[0]["Adr2Rel1"]);
                    txtLocAddLine3.Text = Convert.ToString(dt.Rows[0]["Adr3Rel1"]);
                    txtCity1.Text = Convert.ToString(dt.Rows[0]["CityRel1"]);
                    txtDistrict1.Text = Convert.ToString(dt.Rows[0]["DistrictRel1"]);
                    ddlPinCode1.Text = Convert.ToString(dt.Rows[0]["PostCodeRel1"]);
                    ddlState1.SelectedValue = Convert.ToString(dt.Rows[0]["StateCodeRel1"]);
                    ddlCountryCode1.SelectedValue = Convert.ToString(dt.Rows[0]["CntryCodeRel1"]);

                    if (dt.Rows[0]["CnctTypeRel2"].ToString() != "")
                    {
                        chkAddResident.Checked = true;
                    }
                    else
                    {
                        chkAddResident.Checked = false;
                    }

                    ddlAddressType2.SelectedValue = Convert.ToString(dt.Rows[0]["AdrTypeRel2"]);
                    txtAddLine1.Text = Convert.ToString(dt.Rows[0]["Adr1Rel2"]);
                    txtAddLine2.Text = Convert.ToString(dt.Rows[0]["Adr2Rel2"]);
                    txtAddLine3.Text = Convert.ToString(dt.Rows[0]["Adr3Rel2"]);
                    txtCity2.Text = Convert.ToString(dt.Rows[0]["CityRel2"]);
                    txtDistrict2.Text = Convert.ToString(dt.Rows[0]["DistrictRel2"]);
                    ddlPinCode2.Text = Convert.ToString(dt.Rows[0]["PostCodeRel2"]);
                    ddlState2.SelectedValue = Convert.ToString(dt.Rows[0]["StateCodeRel2"]);
                    ddlIsoCountryCode.SelectedValue = Convert.ToString(dt.Rows[0]["CntryCodeRel2"]);


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


                    ViewState["strAddIdName"] = Convert.ToString(dt.Rows[0]["AddIdNameRel"]);
                    ViewState["strAddIdNumber"] = Convert.ToString(dt.Rows[0]["AddIdNumberRel"]);
                    ViewState["strAddIdExpDate"] = Convert.ToString(dt.Rows[0]["AddIdExpDateRel"]);


                    if (ddlProofOfAddress.SelectedValue == "99")
                    {
                        txtPassOthrAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNumberRel"]);
                        txtPassNoAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNameRel"]);
                    }
                    else
                    {
                        txtPassNoAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNumberRel"]);
                        txtPassExpDateAdd.Text = Convert.ToString(dt.Rows[0]["AddIdExpDateRel"]);
                    }
                    txtPlace.Text = Convert.ToString(dt.Rows[0]["DecPlaceRel"]);
                    txtDate.Text = Convert.ToString(dt.Rows[0]["DecDateRel"]);
                    txtEmpName.Text = Convert.ToString(dt.Rows[0]["kycEmpNameRel"]);
                    txtEmpCode.Text = Convert.ToString(dt.Rows[0]["kycEmpCodeRel"]);
                    txtEmpDesignation.Text = Convert.ToString(dt.Rows[0]["kycEmpDesiRel"]);
                    txtEmpBranch.Text = Convert.ToString(dt.Rows[0]["kycEmpBranchRel"]);
                    txtInsName.Text = Convert.ToString(dt.Rows[0]["kycInstNameRel"]);
                    txtInsCode.Text = Convert.ToString(dt.Rows[0]["kycInstCodeRel"]);
                    txtDate3.Text = Convert.ToString(dt.Rows[0]["kycVerDateRel"]);

                    if (ddlOccupation.SelectedValue == "B" || ddlOccupation.SelectedValue == "X")
                    {
                        FillSubOccuType(ddlOccupation, ddlOccuSubType);
                        ddlOccuSubType.SelectedIndex = 1;
                    }
                    else
                    {
                        FillSubOccuType(ddlOccupation, ddlOccuSubType);
                    }

                    //ViewState["DtAdd"] = dtNewAddRel;
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "FillRelatedPersonPartialdata", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion



        //protected void chkHigh_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkHigh.Checked == true)
        //    {
        //        chkMedium.Checked = false;
        //        chkLow.Checked = false;
        //    }
        //}

        //protected void chkMedium_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkMedium.Checked == true)
        //    {
        //        chkHigh.Checked = false;
        //        chkLow.Checked = false;
        //    }
        //}

        //protected void chkLow_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkLow.Checked == true)
        //    {
        //        chkMedium.Checked = false;
        //        chkHigh.Checked = false;
        //    }
        //}
        //added by ramesh
        protected void fillcboTitle1()
        {
            htParam.Clear();
            htParam.Add("@LookupCode", "KTitle");
            htParam.Add("@Flag", "2");
            FillDropdowns1("Prc_GetValueCKYC", htParam, cboTitle1, "CKYCConnectionString", true);
            htParam.Clear();
        }
        protected void fillcboTitle2()
        {
            htParam.Clear();
            htParam.Add("@LookupCode", "KTitle");
            htParam.Add("@Flag", "1");
            FillDropdowns1("Prc_GetValueCKYC", htParam, cboTitle2, "CKYCConnectionString", true);
            htParam.Clear();
        }

        protected void txtKYCNum_TextChanged(object sender, EventArgs e)
        {
            if (txtKYCNum != null)
            {
                if (txtKYCNum.Text.Trim().Length == 14)
                {
                    DisableControls();
                }
                else
                {
                    EnableControls();
                }
            }
        }

        #region METHOD "DisableControls"
        protected void DisableControls()
        {
            try
            {
                chkTick.Enabled = false;
                CheckBox1.Enabled = false;
                chkLocalAddress.Enabled = false;
                chkCuurentAddress.Enabled = false;
                chkAddResident.Enabled = false;
                chkCurrentAdd.Enabled = false;
                chkCorresAdd.Enabled = false;
                ddlIsoCountryCode2.Enabled = false;
                txtIDResTax.Enabled = false;
                txtDOBRes.Enabled = false;
                ddlIsoCountry.Enabled = false;
                ddlProofIdentity.Enabled = false;
                chkPerAddress.Enabled = false;
                ddlAddressType.Enabled = false;
                ddlProofOfAddress.Enabled = false;
                txtAddressLine1.Enabled = false;
                txtAddressLine2.Enabled = false;
                txtAddressLine3.Enabled = false;
                txtCity.Enabled = false;
                ddlState.Enabled = false;
                btnShow.Enabled = false;
                txtPinCode.Enabled = false;
                ddlCountryCode.Enabled = false;
                txtPlace.Enabled = false;
                //chkSelfCerti.Enabled = false;
                //chkTrueCopies.Enabled = false;
                //chkNotary.Enabled = false;
                ddlDocReceived.Enabled = false;


                ddlAddressType1.Enabled = false;
                txtLocAddLine1.Enabled = false;
                txtLocAddLine2.Enabled = false;
                txtLocAddLine3.Enabled = false;
                txtCity1.Enabled = false;
                ddlState1.Enabled = false;
                ddlPinCode1.Enabled = false;
                txtDistrict1.Enabled = false;
                ddlCountryCode1.Enabled = false;

                ddlAddressType2.Enabled = false;
                txtAddLine1.Enabled = false;
                txtAddLine2.Enabled = false;
                txtAddLine3.Enabled = false;
                txtCity2.Enabled = false;
                ddlState2.Enabled = false;
                ddlPinCode2.Enabled = false;
                txtDistrict2.Enabled = false;
                ddlIsoCountryCode.Enabled = false;

                cboTitle1.Enabled = false;
                txtGivenName1.Enabled = false;
                txtMiddleName1.Enabled = false;
                txtLastName1.Enabled = false;
                cboTitle2.Enabled = false;
                txtGivenName2.Enabled = false;
                txtMiddleName2.Enabled = false;
                txtLastName2.Enabled = false;
                cboTitle3.Enabled = false;
                txtGivenName3.Enabled = false;
                txtMiddleName3.Enabled = false;
                txtLastName3.Enabled = false;
                cboGender.Enabled = false;
                ddlMaritalStatus.Enabled = false;
                ddlCitizenship.Enabled = false;
                ddlResStatus.Enabled = false;
                ddlOccupation.Enabled = false;
                ddlOccuSubType.Enabled = false;
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "disablecntrl", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region METHOD "EnableControls"
        protected void EnableControls()
        {
            try
            {
                chkTick.Enabled = true;
                CheckBox1.Enabled = true;
                chkLocalAddress.Enabled = true;
                chkCuurentAddress.Enabled = true;
                chkAddResident.Enabled = true;
                chkCurrentAdd.Enabled = true;
                chkCorresAdd.Enabled = true;
                ddlIsoCountryCode2.Enabled = true;
                txtIDResTax.Enabled = true;
                txtDOBRes.Enabled = true;
                ddlIsoCountry.Enabled = true;
                ddlProofIdentity.Enabled = true;
                chkPerAddress.Enabled = true;
                ddlAddressType.Enabled = true;
                ddlProofOfAddress.Enabled = true;
                txtAddressLine1.Enabled = true;
                txtAddressLine2.Enabled = true;
                txtAddressLine3.Enabled = true;
                txtCity.Enabled = true;
                ddlState.Enabled = true;
                btnShow.Enabled = true;
                txtPinCode.Enabled = true;
                ddlCountryCode.Enabled = true;
                txtPlace.Enabled = true;
                //chkSelfCerti.Enabled = true;
                //chkTrueCopies.Enabled = true;
                //chkNotary.Enabled = true;
                ddlDocReceived.Enabled = true;
                txtEmpName.Enabled = true;
                txtEmpCode.Enabled = true;
                txtEmpDesignation.Enabled = true;
                txtEmpBranch.Enabled = true;
                txtInsName.Enabled = true;
                txtInsCode.Enabled = true;

                ddlAddressType1.Enabled = true;
                txtLocAddLine1.Enabled = true;
                txtLocAddLine2.Enabled = true;
                txtLocAddLine3.Enabled = true;
                txtCity1.Enabled = true;
                ddlState1.Enabled = true;
                ddlPinCode1.Enabled = true;
                txtDistrict1.Enabled = true;
                ddlCountryCode1.Enabled = true;

                ddlAddressType2.Enabled = true;
                txtAddLine1.Enabled = true;
                txtAddLine2.Enabled = true;
                txtAddLine3.Enabled = true;
                txtCity2.Enabled = true;
                ddlState2.Enabled = true;
                ddlPinCode2.Enabled = true;
                txtDistrict2.Enabled = true;
                ddlIsoCountryCode.Enabled = true;

                cboTitle1.Enabled = true;
                txtGivenName1.Enabled = true;
                txtMiddleName1.Enabled = true;
                txtLastName1.Enabled = true;
                cboTitle2.Enabled = true;
                txtGivenName2.Enabled = true;
                txtMiddleName2.Enabled = true;
                txtLastName2.Enabled = true;
                cboTitle3.Enabled = true;
                txtGivenName3.Enabled = true;
                txtMiddleName3.Enabled = true;
                txtLastName3.Enabled = true;
                cboGender.Enabled = true;
                ddlMaritalStatus.Enabled = true;
                ddlCitizenship.Enabled = true;
                ddlResStatus.Enabled = true;
                ddlOccupation.Enabled = true;
                ddlOccuSubType.Enabled = true;
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "disablecntrl", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
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
                    ddlAddressType1.SelectedValue = ddlAddressType.SelectedValue;
                    //ddlProofOfAddress1.SelectedValue = ddlProofOfAddress.SelectedValue;
                    ddlAddressType1.Enabled = false;
                    // ddlProofOfAddress1.Enabled = false;
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
                    ddlPinCode1.Enabled = false;
                    btnsearchddlPinCode1.Enabled = false;
                    ddlCountryCode1.SelectedValue = ddlCountryCode.SelectedValue;
                    ddlCountryCode1.Enabled = false;
                    txtDistrict1.Text = txtDistrictname.Text;
                    txtDistrict1.Enabled = false;
                    ddlState1.SelectedValue = ddlState.SelectedValue;
                    ddlState1.Enabled = false;
                }
                else
                {
                    ddlAddressType1.SelectedIndex = 0;
                    //ddlProofOfAddress1.SelectedIndex = 0;
                    //chkLocalAddress.Checked = false;
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
                    ddlPinCode1.Enabled = true;
                    //ddlState1.SelectedItem.Text = "";
                    ddlState1.SelectedIndex = 0;
                    //added by ramesh on dated 21-05-2018
                    ddlAddressType1.Enabled = true;
                    //ddlProofOfAddress1.Enabled = true;
                    ddlState1.Enabled = true;
                    btnsearchddlPinCode1.Enabled = true;
                    //end
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

        #region DROPDOWN 'chkCurentAddress'SELECTEDINDEXCHANGED EVENT
        protected void chkCurrentAdd_Checked(object sender, EventArgs e)
        {
            try
            {
                //chkCuurentAddress.Checked = true;
                if (chkCurrentAdd.Checked == true)
                {
                    if (ddlCountryCode.SelectedValue != "IN")
                    {
                        chkAddResident.Checked = true;
                        chkCorresAdd.Checked = false;
                        //FillDistrictState(ddlPinCode, ddlDistrict2, ddlState2);
                        ddlAddressType2.SelectedValue = ddlAddressType.SelectedValue;
                        //ddlProofOfAddress2.SelectedValue = ddlProofOfAddress.SelectedValue;
                        ddlAddressType2.Enabled = false;
                        //ddlProofOfAddress2.Enabled = false;
                        txtAddLine1.Text = txtAddressLine1.Text;
                        txtAddLine1.Enabled = false;
                        txtAddLine2.Text = txtAddressLine2.Text;
                        txtAddLine2.Enabled = false;
                        txtAddLine3.Text = txtAddressLine3.Text;
                        txtAddLine3.Enabled = false;
                        txtCity2.Text = txtCity.Text;
                        txtCity2.Enabled = false;
                        ddlPinCode2.Text = txtPinCode.Text;
                        ddlPinCode2.Enabled = false;

                        btnsearchddlPinCode2.Enabled = false;
                        ddlIsoCountryCode.SelectedValue = ddlCountryCode.SelectedValue;
                        ddlIsoCountryCode.Enabled = false;
                        txtDistrict2.Text = txtDistrictname.Text;
                        txtDistrict2.Enabled = false;
                        ddlState2.SelectedValue = ddlState.SelectedValue;//ddlState1
                        //ddlState2.SelectedValue = ddlState.SelectedValue;
                        ddlState2.Enabled = false;
                    }
                    else
                    {
                        chkCurrentAdd.Checked = false;
                        ddlAddressType2.SelectedIndex = 0;
                        //ddlProofOfAddress2.SelectedIndex = 0;
                        txtAddLine1.Text = string.Empty;
                        txtAddLine1.Enabled = true;
                        txtAddLine2.Text = string.Empty;
                        txtAddLine2.Enabled = true;
                        txtAddLine3.Text = string.Empty;
                        txtAddLine3.Enabled = true;
                        txtCity2.Text = string.Empty;
                        txtCity2.Enabled = true;
                        ddlIsoCountryCode.SelectedIndex = 0;
                        ddlIsoCountryCode.Enabled = true;
                        txtDistrict2.Text = "";
                        ddlPinCode2.Text = "";
                        ddlPinCode2.Enabled = true;
                        btnsearchddlPinCode2.Enabled = true;
                        //ddlState2.SelectedItem.Text = "";
                        ddlState2.SelectedIndex = 0;
                        //added by Ramesh on Dated 21-05-2018
                        ddlAddressType2.Enabled = true;
                        //ddlProofOfAddress2.Enabled = true;
                        ddlState2.Enabled = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Country is India in Current / Permanent / Overseas Address details.');", true);
                    }
                }
                else
                {
                    ddlAddressType2.SelectedIndex = 0;
                    //ddlProofOfAddress2.SelectedIndex = 0;
                    txtAddLine1.Text = string.Empty;
                    txtAddLine1.Enabled = true;
                    txtAddLine2.Text = string.Empty;
                    txtAddLine2.Enabled = true;
                    txtAddLine3.Text = string.Empty;
                    txtAddLine3.Enabled = true;
                    txtCity2.Text = string.Empty;
                    txtCity2.Enabled = true;
                    ddlIsoCountryCode.SelectedIndex = 0;
                    ddlIsoCountryCode.Enabled = true;
                    txtDistrict2.Text = "";
                    ddlPinCode2.Text = "";
                    ddlPinCode2.Enabled = true;
                    btnsearchddlPinCode2.Enabled = true;
                    //ddlState2.SelectedItem.Text = "";
                    ddlState2.SelectedIndex = 0;
                    //added by Ramesh on Dated 21-05-2018
                    ddlAddressType2.Enabled = true;
                    //ddlProofOfAddress2.Enabled = true;
                    ddlState2.Enabled = true;
                    //End
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "chkCurrentAdd_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region DROPDOWN 'chkCorresAdd'SELECTEDINDEXCHANGED EVENT
        protected void chkCorresAdd_Checked(object sender, EventArgs e)
        {
            try
            {
                if (chkCorresAdd.Checked == true)
                {
                    if (ddlCountryCode1.SelectedValue != "IN")
                    {
                        chkCurrentAdd.Checked = false;
                        chkAddResident.Checked = true;
                        ddlAddressType2.Text = ddlAddressType1.Text;
                        txtAddLine1.Text = txtLocAddLine1.Text;
                        txtAddLine1.Enabled = false;
                        txtAddLine2.Text = txtLocAddLine2.Text;
                        txtAddLine2.Enabled = false;
                        txtAddLine3.Text = txtLocAddLine3.Text;
                        txtAddLine3.Enabled = false;
                        txtCity2.Text = txtCity1.Text;
                        txtCity2.Enabled = false;
                        ddlPinCode2.Text = ddlPinCode1.Text;
                        ddlPinCode2.Enabled = false;
                        ddlIsoCountryCode.SelectedIndex = ddlCountryCode1.SelectedIndex;
                        ddlIsoCountryCode.Enabled = false;
                        txtDistrict2.Text = txtDistrict1.Text;
                        txtDistrict2.Enabled = false;
                        ddlState2.SelectedValue = ddlState1.SelectedValue;
                        //ddlState2.SelectedItem.Text = ddlState1.Text;
                        ddlState2.Enabled = false;
                        btnsearchddlPinCode2.Enabled = false;
                    }
                    else
                    {
                        ddlAddressType2.Enabled = true;
                        ddlAddressType2.SelectedIndex = 0;
                        chkCorresAdd.Checked = false;
                        txtAddLine1.Text = string.Empty;
                        txtAddLine1.Enabled = true;
                        txtAddLine2.Text = string.Empty;
                        txtAddLine2.Enabled = true;
                        txtAddLine3.Text = string.Empty;
                        txtAddLine3.Enabled = true;
                        txtCity2.Text = string.Empty;
                        txtCity2.Enabled = true;
                        ddlIsoCountryCode.SelectedIndex = 0;
                        ddlIsoCountryCode.Enabled = true;
                        ddlState2.Enabled = true;
                        ddlIsoCountryCode.Enabled = true;
                        txtDistrict2.Text = "";
                        ddlPinCode2.Text = "";
                        ddlPinCode2.Enabled = true;
                        ddlState2.SelectedIndex = 0;   //SelectedItem.Text = "";
                        btnsearchddlPinCode2.Enabled = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Country is India in Correspondence / Local Address details.');", true);
                    }
                }
                else
                {
                    ddlAddressType2.Enabled = true;
                    ddlAddressType2.SelectedIndex = 0;
                    txtAddLine1.Text = string.Empty;
                    txtAddLine1.Enabled = true;
                    txtAddLine2.Text = string.Empty;
                    txtAddLine2.Enabled = true;
                    txtAddLine3.Text = string.Empty;
                    txtAddLine3.Enabled = true;
                    txtCity2.Text = string.Empty;
                    txtCity2.Enabled = true;
                    ddlIsoCountryCode.SelectedIndex = 0;
                    ddlIsoCountryCode.Enabled = true;
                    ddlState2.Enabled = true;
                    ddlIsoCountryCode.Enabled = true;
                    txtDistrict2.Text = "";
                    ddlPinCode2.Text = "";
                    ddlPinCode2.Enabled = true;
                    ddlState2.SelectedIndex = 0;   //SelectedItem.Text = "";
                    btnsearchddlPinCode2.Enabled = true;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "chkCorresAdd_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
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
                ddlAddressType.Enabled = true;
                ddlProofOfAddress.Enabled = true;
                txtPassNoAdd.Enabled = true;

                txtAddressLine1.Enabled = true;
                txtAddressLine2.Enabled = true;
                txtAddressLine3.Enabled = true;
                txtCity.Enabled = true;
                txtDistrictname.Enabled = true;
                txtPinCode.Enabled = true;
                ddlState.Enabled = true;
                ddlCountryCode.Enabled = true;

                txtLocAddLine1.Enabled = true;
                txtLocAddLine2.Enabled = true;
                txtLocAddLine3.Enabled = true;
                txtCity2.Enabled = true;
                txtDistrict1.Enabled = true;
                ddlPinCode1.Enabled = true;
                ddlState1.Enabled = true;
                ddlCountryCode1.Enabled = true;

                txtAddLine1.Enabled = true;
                txtAddLine2.Enabled = true;
                txtAddLine3.Enabled = true;
                txtCity1.Enabled = true;
                txtDistrict2.Enabled = true;
                ddlPinCode2.Enabled = true;
                ddlState2.Enabled = true;
                ddlIsoCountryCode.Enabled = true;

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

        protected void GetModelData(object sender, EventArgs e)
        {
            if (ddlState.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select the state.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "OpenStateWindow('Flag1');", true);
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
            }
        }

        protected void GetModelData2(object sender, EventArgs e)
        {
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "openModal();", true);
            if (ddlState2.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select the state of Jurisdiction Address Details.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "OpenStateWindow2('Flag3');", true);
            }
        }

        #region METHOD "InitializeControl()"
        private void InitializeControls()
        {
            try
            {

                lblAddressType1.Text = olng.GetItemDesc("lblAddressType");
                //lblProofOfAddress1.Text = olng.GetItemDesc("lblProofOfAddress");
                lblAddressType2.Text = olng.GetItemDesc("lblAddressType");
                //lblProofOfAddress2.Text = olng.GetItemDesc("lblProofOfAddress");
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
                lblAddLine1.Text = olng.GetItemDesc("lblJuriAddLine1");
                lblAddLine2.Text = olng.GetItemDesc("lblJuriAddLine2");
                lblAddLine3.Text = olng.GetItemDesc("lblJuriAddLine3");
                lblCity2.Text = olng.GetItemDesc("lblJurCity1");
                lblDistrict2.Text = olng.GetItemDesc("lblJurDistrict1");
                lblPin2.Text = olng.GetItemDesc("lblJurPin1");
                lblState2.Text = olng.GetItemDesc("lblJurState1");
                lblIsoCountry2.Text = olng.GetItemDesc("lblJurCountryCode1");
                //lblTelOff1.Text = olng.GetItemDesc("lblTelOff1");
                //lblTelRes.Text = olng.GetItemDesc("lblTelRes");
                //lblMobile.Text = olng.GetItemDesc("lblMobile");
                //lblFax.Text = olng.GetItemDesc("lblFax");
                ////lblpfemail.Text = olng.GetItemDesc("lblpfemail");
                //lblRemarks.Text = olng.GetItemDesc("lblRemarks");
                ////chkAppDeclare1.Text = olng.GetItemDesc("LblDECLARATION");
                //lblDate.Text = olng.GetItemDesc("lblDate");
                //lblPlace1.Text = olng.GetItemDesc("lblPlace1");
                //lblDocRec.Text = olng.GetItemDesc("lblDocRec");
                //lblKYCVerify.Text = olng.GetItemDesc("lblKYCVerify");
                //lblDate3.Text = olng.GetItemDesc("lblAtstionDate");
                //lblEmpName.Text = olng.GetItemDesc("lblEmpName");
                //lblEmpCode.Text = olng.GetItemDesc("lblEmpCode");
                //lblEmpDesignation.Text = olng.GetItemDesc("lblEmpDesignation");
                //lblEmpBranch.Text = olng.GetItemDesc("lblEmpBranch");
                //lblInsCode.Text = olng.GetItemDesc("lblInsCode");
                //lblInsName.Text = olng.GetItemDesc("lblInsName");

                //lblpfPersonal1.Text = olng.GetItemDesc("lblpfPersonal1");
                //lblpfPersonal.Text = olng.GetItemDesc("lblpfPersonal");
                //lbltick.Text = olng.GetItemDesc("lbltick");
                //lblProofOfIdentity11.Text = olng.GetItemDesc("lblProofOfIdentity11");
                //lblpfofAddr1.Text = olng.GetItemDesc("lblpfofAddr1");
                //lblpfofAddr2.Text = olng.GetItemDesc("lblpfofAddr2");
                //lblDtlOfRtltpr.Text = olng.GetItemDesc("lblDtlOfRtltpr");
                //lblDtlOfCtrlpr.Text = olng.GetItemDesc("lblDtlOfCtrlpr");
                //lblRemarks.Text = olng.GetItemDesc("lblRemarks");
                //lblattstn.Text = olng.GetItemDesc("lblattstn");
                //lbldec.Text = olng.GetItemDesc("lbldec");
                //lblAttesOfc.Text = olng.GetItemDesc("lblAttesOfc");
                //lblOfcuseOnly.Text = olng.GetItemDesc("lblOfcuseOnly");
                //lblInsDtls.Text = olng.GetItemDesc("lblInsDtls");
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

        protected void FillddlPageLoad()
        {
            //htParam.Clear();
            //htParam.Add("@LookupCode", "KEntConstTyp");
            //FillDropdowns("prc_getDDLLookUpData", htParam, ddlNatureOfBuss, "CKYCConnectionString", true);

            htParam.Clear();
            htParam.Add("@LookupCode", "KAddr");
            FillDropdowns("prc_getDDLLookUpData", htParam, ddlAddressType1, "CKYCConnectionString", true);
            FillDropdowns("prc_getDDLLookUpData", htParam, ddlAddressType2, "CKYCConnectionString", true);

            htParam.Clear();
            htParam.Add("@LookupCode", "KEntPoA");
            //FillDropdowns("prc_getDDLLookUpData", htParam, ddlProofOfAddress1, "CKYCConnectionString", true);
            //FillDropdowns("prc_getDDLLookUpData", htParam, ddlProofOfAddress2, "CKYCConnectionString", true);

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


                    //ddlCountryCode.DataSource = dt;
                    //ddlCountryCode.DataTextField = "Country_Desc";
                    //ddlCountryCode.DataValueField = "Country_CODE";
                    //ddlCountryCode.DataBind();
                    //ddlCountryCode.Items.Insert(0, new ListItem("Select", string.Empty));

                    ddlCountryCode1.DataSource = dt;
                    ddlCountryCode1.DataTextField = "Country_Desc";
                    ddlCountryCode1.DataValueField = "Country_CODE";
                    ddlCountryCode1.DataBind();
                    ddlCountryCode1.Items.Insert(0, new ListItem("Select", string.Empty));

                    ddlIsoCountryCode.DataSource = dt;
                    ddlIsoCountryCode.DataTextField = "Country_Desc";
                    ddlIsoCountryCode.DataValueField = "Country_CODE";
                    ddlIsoCountryCode.DataBind();
                    ddlIsoCountryCode.Items.Insert(0, new ListItem("Select", string.Empty));



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

        protected void SameIdentityProof_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked == true)
            {
                if (ddlProofIdentity.SelectedIndex == 0)
                {
                    divAddProof.Visible = false;
                }
                if (ddlProofIdentity.SelectedIndex == 1)
                {
                    //divAddProof.Visible = true;
                    //ddlProofOfAddress.SelectedIndex = ddlProofIdentity.SelectedItem.Text;
                    ddlProofOfAddress.ClearSelection();
                    string selected = ddlProofIdentity.SelectedItem.Text;
                    //ddlProofOfAddress.SelectedItem.Text = ddlProofIdentity.SelectedItem.Text;
                    if (ddlProofOfAddress.Items.FindByText(selected) != null)
                    {
                        ddlProofOfAddress.Items.FindByText(selected).Selected = true;
                    }
                    divPassAdd.Visible = true;
                    llPassExpDateAdd.Visible = true;
                    lblPassportNoAdd.Text = "Passport Number";
                    llPassExpDateAdd.Text = "Passport Expiry Date";
                    txtPassNoAdd.Text = txtPassNo.Text;
                    txtPassExpDateAdd.Text = txtPassExpDate.Text;
                    hidetxtPassExpDateAdd.Visible = true;
                    txtPassNoAdd.Enabled = false;
                    txtPassExpDateAdd.Enabled = false;
                    txtPassOthrAdd.Visible = false;
                    ddlProofOfAddress.Enabled = false;
                }
                if (ddlProofIdentity.SelectedIndex == 2)
                {
                    ddlProofOfAddress.ClearSelection();
                    string selected = ddlProofIdentity.SelectedItem.Text;
                    //ddlProofOfAddress.SelectedItem.Text = ddlProofIdentity.SelectedItem.Text;
                    if (ddlProofOfAddress.Items.FindByText(selected) != null)
                    {
                        ddlProofOfAddress.Items.FindByText(selected).Selected = true;
                    }

                    //divAddProof.Visible = true;
                    //ddlProofOfAddress.SelectedIndex = ddlProofIdentity.SelectedIndex;
                    lblPassportNoAdd.Text = "Voter ID Card";
                    llPassExpDateAdd.Visible = false;
                    txtPassNoAdd.Text = txtPassNo.Text;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;
                    txtPassNoAdd.Enabled = false;
                    //txtPassExpDateAdd.Enabled = false;
                    txtPassOthrAdd.Visible = false;
                    ddlProofOfAddress.Enabled = false;
                }
                if (ddlProofIdentity.SelectedIndex == 3)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Proof of address can not be same as above proof of identity because proof of address does not contain " + ddlProofIdentity.SelectedItem.Text.ToString() + ".');", true);
                    //divAddProof.Visible = true;
                    //ddlProofOfAddress.SelectedIndex = ddlProofIdentity.SelectedIndex;
                    //lblPassportNoAdd.Text = "PAN Card";
                    //llPassExpDateAdd.Visible = false;
                    //txtPassNoAdd.Text = txtPassNo.Text;
                    //txtPassExpDateAdd.Visible = false;
                    //hidetxtPassExpDateAdd.Visible = false;
                    //txtPassNoAdd.Enabled = false;
                    ////txtPassExpDateAdd.Enabled = false;
                    //txtPassOthrAdd.Visible = false;
                    //ddlProofOfAddress.Enabled = false;
                }

                if (ddlProofIdentity.SelectedIndex == 4)
                {
                    //divAddProof.Visible = true;

                    //ddlProofOfAddress.SelectedIndex = ddlProofIdentity.SelectedIndex;
                    ddlProofOfAddress.ClearSelection();
                    string selected = ddlProofIdentity.SelectedItem.Text;

                    if (ddlProofOfAddress.Items.FindByText(selected) != null)
                    {
                        ddlProofOfAddress.Items.FindByText(selected).Selected = true;
                    }
                    divPassAdd.Visible = true;
                    txtPassExpDateAdd.Visible = true;
                    llPassExpDateAdd.Visible = true;
                    lblPassportNoAdd.Text = "Driving Licence";
                    llPassExpDateAdd.Text = "Driving Licence Expiry Date";
                    txtPassNoAdd.Text = txtPassNo.Text;
                    txtPassExpDateAdd.Text = txtPassExpDate.Text;
                    hidetxtPassExpDateAdd.Visible = true;
                    txtPassNoAdd.Enabled = false;
                    txtPassExpDateAdd.Enabled = true;
                    txtPassOthrAdd.Visible = false;
                    ddlProofOfAddress.Enabled = false;
                }
                if (ddlProofIdentity.SelectedIndex == 5)
                {
                    //divAddProof.Visible = true;
                    //ddlProofOfAddress.SelectedIndex = ddlProofIdentity.SelectedIndex;
                    ddlProofOfAddress.ClearSelection();
                    string selected = ddlProofIdentity.SelectedItem.Text;

                    if (ddlProofOfAddress.Items.FindByText(selected) != null)
                    {
                        ddlProofOfAddress.Items.FindByText(selected).Selected = true;
                    }
                    lblPassportNoAdd.Text = "Proof of Possession of Aadhaar";
                    llPassExpDateAdd.Visible = false;
                    txtPassNoAdd.Text = txtPassNo.Text;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;
                    txtPassNoAdd.Enabled = false;
                    //txtPassExpDateAdd.Enabled = false;
                    txtPassOthrAdd.Visible = false;
                    ddlProofOfAddress.Enabled = false;
                }
                if (ddlProofIdentity.SelectedIndex == 6)
                {
                    //divAddProof.Visible = true;
                    //ddlProofOfAddress.SelectedIndex = ddlProofIdentity.SelectedIndex;
                    ddlProofOfAddress.ClearSelection();
                    string selected = ddlProofIdentity.SelectedItem.Text;

                    if (ddlProofOfAddress.Items.FindByText(selected) != null)
                    {
                        ddlProofOfAddress.Items.FindByText(selected).Selected = true;
                    }
                    lblPassportNoAdd.Text = "NREGA Job Card";
                    llPassExpDateAdd.Visible = false;
                    txtPassNoAdd.Text = txtPassNo.Text;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;
                    txtPassNoAdd.Enabled = false;
                    //txtPassExpDateAdd.Enabled = false;
                    txtPassOthrAdd.Visible = false;
                    ddlProofOfAddress.Enabled = false;
                }
                if (ddlProofIdentity.SelectedIndex == 7)
                {
                    //divAddProof.Visible = true;
                    divPassAdd.Visible = true;
                    ddlProofOfAddress.ClearSelection();
                    string selected = ddlProofIdentity.SelectedItem.Text;

                    if (ddlProofOfAddress.Items.FindByText(selected) != null)
                    {
                        ddlProofOfAddress.Items.FindByText(selected).Selected = true;
                    }
                    //ddlProofOfAddress.Text = "Others";
                    llPassExpDateAdd.Visible = true;
                    lblPassportNoAdd.Text = "Others";
                    llPassExpDateAdd.Text = "Identification Number";
                    txtPassNoAdd.Text = txtPassNo.Text;
                    txtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = true;
                    txtPassOthrAdd.Text = txtPassOthr.Text;
                    txtPassOthrAdd.Enabled = false;
                    hidetxtPassExpDateAdd.Visible = false;
                    txtPassNoAdd.Enabled = false;
                    //txtPassExpDateAdd.Enabled = false;

                    ddlProofOfAddress.Enabled = false;
                }
                if (ddlProofIdentity.SelectedIndex == 8)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Proof of address can not be same as above proof of identity because proof of address does not contain " + ddlProofIdentity.SelectedItem.Text.ToString() + ".');", true);
                    //divAddProof.Visible = true;
                    //ddlProofOfAddress.Text = "Simplified Measures Account";
                    //lblPassportNoAdd.Text = "Simplified Measures Account";
                    //llPassExpDateAdd.Text = "Identification Number";
                    //txtPassNoAdd.Text = txtPassNo.Text;
                    //txtPassExpDateAdd.Visible = false;
                    //txtPassOthrAdd.Visible = true;
                    //txtPassOthrAdd.Text = txtPassOthr.Text;
                    //txtPassOthrAdd.Enabled = false;
                    //hidetxtPassExpDateAdd.Visible = false;
                    //txtPassNoAdd.Enabled = false;
                    ////txtPassExpDateAdd.Enabled = false;

                    //ddlProofOfAddress.Enabled = false;
                }
            }
            else
            {
                divAddProof.Visible = false;
                divPassAdd.Visible = false;
                ddlProofOfAddress.SelectedIndex = 0;
                ddlProofOfAddress.Enabled = true;

            }
        }



        protected void ddlAddressType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAddressType1.SelectedIndex != 0)
                {
                    if (chkLocalAddress.Checked == false)
                    {
                        chkLocalAddress.Enabled = true;
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Check CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS')", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please Check correspondence / local address details')", true);
                        ddlAddressType1.SelectedIndex = 0;
                        chkLocalAddress.Focus();
                        return;
                    }
                }
                else
                {
                    chkLocalAddress.Enabled = false;
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "ddlAddressType1_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        protected void ddlAddressType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAddressType2.SelectedIndex != 0)
                {
                    if (chkAddResident.Checked == false)
                    {
                        chkAddResident.Enabled = true;
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Check CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS')", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please check jurisdiction address details')", true);
                        ddlAddressType2.SelectedIndex = 0;
                        chkAddResident.Focus();
                        return;
                    }
                }
                else
                {
                    chkAddResident.Enabled = false;
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "ddlAddressType1_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        protected void chkLocalAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLocalAddress.Checked == false)
            {
                chkCuurentAddress.Checked = false;
                chkCuurentAddress_Checked(this, EventArgs.Empty);
            }
        }

        protected void chkAddResident_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddResident.Checked == false)
            {
                if (chkCurrentAdd.Checked == true)
                {
                    chkCurrentAdd.Checked = false;
                    chkCurrentAdd_Checked(this, EventArgs.Empty);
                }

                if (chkCorresAdd.Checked == true)
                {
                    chkCorresAdd.Checked = false;
                    chkCorresAdd_Checked(this, EventArgs.Empty);
                }

                if (chkCurrentAdd.Checked == false && chkCorresAdd.Checked == false)
                {
                    chkCurrentAdd.Checked = false;
                    chkCurrentAdd_Checked(this, EventArgs.Empty);
                }

            }
        }

        #region btnPartialUpdate
        protected void btnPSUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string Res;
                Res = "";
                //objVal.PersonalDtlsValidation(
                //    null, null, null, cboTitle, txtGivenName, txtLastName, cboTitle2, rbtFS, txtGivenName2, txtLastName2, cboTitle3, txtGivenName3,
                //    txtLastName3, txtDOB, cboGender, ddlOccupation, null, ddlMaritalStatus, ddlCitizenship, ddlResStatus, ddlIsoCountryCodeOthr, ddlRelType, "RelatedPrsn");

                if (Res.Equals(""))
                {
                    //DtAdd = dtView;
                    //DtAdd = (DataTable)Session["dsRel"];

                    if (Session["dsRel"] != null)
                    {
                        DtAdd = (DataTable)Session["dsRel"];
                    }
                    else
                    {
                        DtAdd = (DataTable)ViewState["DtAdd"];
                    }

                    int idr;
                    idr = Convert.ToInt32(Request.QueryString["drNo"]);

                    //DataRow dataRow = DtAdd.NewRow();
                    DtAdd.Rows[idr]["FiRefNo"] = txtRefNumber.Text.Trim();
                    DtAdd.Rows[idr]["RelRefNo"] = txtRelRefNumber.Text.Trim();
                    DtAdd.Rows[idr]["RelatedPrsnKYCNo"] = txtKYCNum.Text.Trim();
                    DtAdd.Rows[idr]["RelationType"] = ddlRelType.SelectedValue;
                    DtAdd.Rows[idr]["PrefixRel"] = cboTitle.SelectedValue;
                    DtAdd.Rows[idr]["FNameRel"] = txtGivenName.Text.Trim();
                    DtAdd.Rows[idr]["MNameRel"] = txtMiddleName.Text.Trim();
                    DtAdd.Rows[idr]["LNameRel"] = txtLastName.Text.Trim();
                    DtAdd.Rows[idr]["MaidPrefixRel"] = cboTitle1.SelectedValue;
                    DtAdd.Rows[idr]["MaidFNameRel"] = txtGivenName1.Text.Trim();
                    DtAdd.Rows[idr]["MaidMNameRel"] = txtMiddleName1.Text.Trim();
                    DtAdd.Rows[idr]["MaidLNameRel"] = txtLastName1.Text.Trim();

                    //ddlIsoCountryCode2.SelectedValue

                    if (rbtFS.SelectedValue == "F")
                    {
                        DtAdd.Rows[idr]["FSFlagRel"] = "01";
                    }
                    else
                    {
                        DtAdd.Rows[idr]["FSFlagRel"] = "02";
                    }
                    DtAdd.Rows[idr]["FatherPrefixRel"] = cboTitle2.SelectedValue;
                    DtAdd.Rows[idr]["FatherFNameRel"] = txtGivenName2.Text.Trim();
                    DtAdd.Rows[idr]["FatherMNameRel"] = txtMiddleName2.Text.Trim();
                    DtAdd.Rows[idr]["FatherLNameRel"] = txtLastName2.Text.Trim();
                    DtAdd.Rows[idr]["MotherPrefixRel"] = cboTitle3.SelectedValue;
                    DtAdd.Rows[idr]["MotherFNameRel"] = txtGivenName3.Text;
                    DtAdd.Rows[idr]["MotherMNameRel"] = txtMiddleName3.Text;
                    DtAdd.Rows[idr]["MotherLNameRel"] = txtLastName3.Text;
                    DtAdd.Rows[idr]["DOBRel"] = txtDOB.Text;
                    DtAdd.Rows[idr]["GenderRel"] = cboGender.SelectedValue;
                    DtAdd.Rows[idr]["MaritalStatusRel"] = ddlMaritalStatus.SelectedValue;
                    DtAdd.Rows[idr]["CitizenshipRel"] = ddlCitizenship.SelectedValue;
                    DtAdd.Rows[idr]["ResiStatusRel"] = ddlResStatus.SelectedValue;

                    //---to show on grid

                    if (ddlRelType.SelectedIndex != 0)
                    {
                        DtAdd.Rows[idr]["RelationTypetxt"] = ddlRelType.SelectedItem.Text;
                    }
                    else
                    {
                        DtAdd.Rows[idr]["RelationTypetxt"] = "";
                    }

                    if (cboGender.SelectedIndex != 0)
                    {
                        DtAdd.Rows[idr]["GenderReltxt"] = cboGender.SelectedItem.Text;
                    }
                    else
                    {
                        DtAdd.Rows[idr]["GenderReltxt"] = "";
                    }

                    //DtAdd.Rows[idr]["GenderReltxt"] = cboGender.SelectedItem.Text;

                    if (ddlMaritalStatus.SelectedIndex != 0)
                    {
                        DtAdd.Rows[idr]["MaritalStatusReltxt"] = ddlMaritalStatus.SelectedItem.Text;
                    }
                    else
                    {
                        DtAdd.Rows[idr]["MaritalStatusReltxt"] = "";
                    }
                    //DtAdd.Rows[idr]["MaritalStatusReltxt"] = ddlMaritalStatus.SelectedItem.Text;

                    if (ddlCitizenship.SelectedIndex != 0)
                    {
                        DtAdd.Rows[idr]["CitizenshipReltxt"] = ddlCitizenship.SelectedItem.Text;
                    }
                    else
                    {
                        DtAdd.Rows[idr]["CitizenshipReltxt"] = "";
                    }
                    //DtAdd.Rows[idr]["CitizenshipReltxt"] = ddlCitizenship.SelectedItem.Text;

                    if (ddlResStatus.SelectedIndex != 0)
                    {
                        DtAdd.Rows[idr]["ResiStatusReltxt"] = ddlResStatus.SelectedItem.Text;
                    }
                    else
                    {
                        DtAdd.Rows[idr]["ResiStatusReltxt"] = "";
                    }
                    //DtAdd.Rows[idr]["ResiStatusReltxt"] = ddlResStatus.SelectedItem.Text;

                    if (ddlOccuSubType.SelectedIndex != 0)
                    {
                        DtAdd.Rows[idr]["OccuTypeReltxt"] = ddlOccuSubType.SelectedItem.Text;
                    }
                    else
                    {
                        DtAdd.Rows[idr]["OccuTypeReltxt"] = "";
                    }
                    //DtAdd.Rows[idr]["OccuTypeReltxt"] = ddlOccuSubType.SelectedItem.Text;

                    //DtAdd.Rows[idr]["RelationTypetxt"] = ddlRelType.SelectedItem.Text;

                    //DtAdd.Rows[idr]["GenderReltxt"] = cboGender.SelectedItem.Text;
                    //DtAdd.Rows[idr]["MaritalStatusReltxt"] = ddlMaritalStatus.SelectedItem.Text;
                    //DtAdd.Rows[idr]["CitizenshipReltxt"] = ddlCitizenship.SelectedItem.Text;
                    //DtAdd.Rows[idr]["ResiStatusReltxt"] = ddlResStatus.SelectedItem.Text;
                    //DtAdd.Rows[idr]["OccuTypeReltxt"] = ddlOccuSubType.SelectedItem.Text;

                    //---end to show on grid

                    DtAdd.Rows[idr]["OccuTypeRel"] = ddlOccupation.SelectedValue;


                    if (chkTick.Checked == true)
                    {
                        DtAdd.Rows[idr]["ResForTaxFlagRel"] = "01";
                    }
                    else
                    {
                        DtAdd.Rows[idr]["ResForTaxFlagRel"] = "02";
                    }
                    DtAdd.Rows[idr]["ISOCountryCodeRel"] = ddlIsoCountryCode2.SelectedValue;
                    DtAdd.Rows[idr]["TaxIDNumberRel"] = txtIDResTax.Text.Trim();
                    DtAdd.Rows[idr]["BirthCityRel"] = txtDOBRes.Text.Trim();
                    DtAdd.Rows[idr]["ISOBirthPlaceCodeRel"] = ddlIsoCountry.SelectedValue;
                    DtAdd.Rows[idr]["IdType"] = ddlProofIdentity.SelectedValue;
                    if (ddlProofIdentity.SelectedIndex == 1)
                    {
                        DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                        DtAdd.Rows[idr]["IdExpDate"] = txtPassExpDate.Text.Trim();
                        DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;

                    }
                    else if (ddlProofIdentity.SelectedIndex == 2)
                    {
                        DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                        DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;

                    }
                    else if (ddlProofIdentity.SelectedIndex == 3)
                    {
                        DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                        DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                    }
                    else if (ddlProofIdentity.SelectedIndex == 4)
                    {
                        DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                        DtAdd.Rows[idr]["IdExpDate"] = txtPassExpDate.Text.Trim();
                        DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                    }
                    else if (ddlProofIdentity.SelectedIndex == 5)
                    {
                        DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                        DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                    }
                    else if (ddlProofIdentity.SelectedIndex == 6)
                    {
                        DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                        DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                    }
                    else if (ddlProofIdentity.SelectedIndex == 7)
                    {
                        DtAdd.Rows[idr]["IdNumber"] = txtPassOthr.Text.Trim();
                        DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["IdName"] = txtPassNo.Text.Trim();
                    }
                    else if (ddlProofIdentity.SelectedIndex == 8 || ddlProofIdentity.SelectedIndex == 9)
                    {
                        DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                        DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                    }
                    else
                    {
                        DtAdd.Rows[idr]["IdNumber"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                    }

                    if (CheckBox1.Checked == true)
                    {
                        DtAdd.Rows[idr]["SameasPOIAddresFlagP1"] = "01";
                        // htParam.Add("@SameasPOIAddresFlagP1", "01");
                    }
                    else
                    {
                        DtAdd.Rows[idr]["SameasPOIAddresFlagP1"] = "";
                    }

                    if (chkPerAddress.Checked == true)
                    {
                        DtAdd.Rows[idr]["CnctTypeRel"] = "P1";
                        DtAdd.Rows[idr]["AdrTypeRel"] = ddlAddressType.SelectedValue;
                        DtAdd.Rows[idr]["AdrProfRel"] = ddlProofOfAddress.SelectedValue;
                        DtAdd.Rows[idr]["Adr1Rel"] = txtAddressLine1.Text.Trim();
                        DtAdd.Rows[idr]["Adr2Rel"] = txtAddressLine2.Text.Trim();
                        DtAdd.Rows[idr]["Adr3Rel"] = txtAddressLine3.Text.Trim();
                        DtAdd.Rows[idr]["CityRel"] = txtCity.Text.Trim();
                        DtAdd.Rows[idr]["DistrictRel"] = txtDistrictname.Text;
                        DtAdd.Rows[idr]["PostCodeRel"] = txtPinCode.Text;
                        DtAdd.Rows[idr]["StateCodeRel"] = ddlState.SelectedValue;
                        DtAdd.Rows[idr]["CntryCodeRel"] = ddlCountryCode.SelectedValue;
                    }
                    else
                    {
                        DtAdd.Rows[idr]["CnctTypeRel"] = "";
                        DtAdd.Rows[idr]["AdrTypeRel"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["AdrProfRel"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["Adr1Rel"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["Adr2Rel"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["Adr3Rel"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["CityRel"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["DistrictRel"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["PostCodeRel"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["StateCodeRel"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["CntryCodeRel"] = System.DBNull.Value;
                    }

                    if (chkCuurentAddress.Checked == true)
                    {
                        DtAdd.Rows[idr]["SameasCurrentAddresFlagM1"] = "01";
                        //htParam.Add("@SameasCurrentAddresFlagM1", "01");
                    }
                    else
                    {
                        DtAdd.Rows[idr]["SameasCurrentAddresFlagM1"] = "";
                    }

                    if (chkLocalAddress.Checked == true)
                    {
                        DtAdd.Rows[idr]["CnctTypeRel1"] = "M1";
                        DtAdd.Rows[idr]["AdrTypeRel1"] = ddlAddressType1.SelectedValue;
                        //dataRow["AdrProfRel1"] = ddlProofOfAddress.SelectedValue;
                        DtAdd.Rows[idr]["Adr1Rel1"] = txtLocAddLine1.Text.Trim();
                        DtAdd.Rows[idr]["Adr2Rel1"] = txtLocAddLine2.Text.Trim();
                        DtAdd.Rows[idr]["Adr3Rel1"] = txtLocAddLine3.Text.Trim();
                        DtAdd.Rows[idr]["CityRel1"] = txtCity1.Text.Trim();
                        DtAdd.Rows[idr]["DistrictRel1"] = txtDistrict1.Text;
                        DtAdd.Rows[idr]["PostCodeRel1"] = ddlPinCode1.Text;
                        DtAdd.Rows[idr]["StateCodeRel1"] = ddlState1.SelectedValue;
                        DtAdd.Rows[idr]["CntryCodeRel1"] = ddlCountryCode1.SelectedValue;
                    }
                    else
                    {
                        DtAdd.Rows[idr]["CnctTypeRel1"] = "";
                        DtAdd.Rows[idr]["AdrTypeRel1"] = System.DBNull.Value;
                        //dataRow["AdrProfRel1"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["Adr1Rel1"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["Adr2Rel1"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["Adr3Rel1"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["CityRel1"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["DistrictRel1"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["PostCodeRel1"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["StateCodeRel1"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["CntryCodeRel1"] = System.DBNull.Value;
                    }

                    if (chkCurrentAdd.Checked == true)
                    {
                        DtAdd.Rows[idr]["SameasLocalAddressFlagJ1"] = "01";
                        //htParam.Add("@SameasLocalAddressFlagJ1", "01");
                    }
                    else
                    {
                        DtAdd.Rows[idr]["SameasLocalAddressFlagJ1"] = "";
                    }

                    if (chkCorresAdd.Checked == true)
                    {
                        DtAdd.Rows[idr]["SameasLocalAddressFlagJ2"] = "01";
                        // htParam.Add("@SameasLocalAddressFlagJ2", "01");
                    }
                    else
                    {
                        DtAdd.Rows[idr]["SameasLocalAddressFlagJ2"] = "";
                    }

                    if (chkAddResident.Checked == true)
                    {
                        DtAdd.Rows[idr]["CnctTypeRel2"] = "J1";
                        DtAdd.Rows[idr]["AdrTypeRel2"] = ddlAddressType2.SelectedValue;
                        //dataRow["AdrProfRel1"] = ddlProofOfAddress.SelectedValue;
                        DtAdd.Rows[idr]["Adr1Rel2"] = txtAddLine1.Text.Trim();
                        DtAdd.Rows[idr]["Adr2Rel2"] = txtAddLine2.Text.Trim();
                        DtAdd.Rows[idr]["Adr3Rel2"] = txtAddLine3.Text.Trim();
                        DtAdd.Rows[idr]["CityRel2"] = txtCity2.Text.Trim();
                        DtAdd.Rows[idr]["DistrictRel2"] = txtDistrict2.Text;
                        DtAdd.Rows[idr]["PostCodeRel2"] = ddlPinCode2.Text;
                        DtAdd.Rows[idr]["StateCodeRel2"] = ddlState2.SelectedValue;
                        DtAdd.Rows[idr]["CntryCodeRel2"] = ddlIsoCountryCode.SelectedValue;
                    }
                    else
                    {
                        DtAdd.Rows[idr]["CnctTypeRel2"] = "";
                        DtAdd.Rows[idr]["AdrTypeRel2"] = System.DBNull.Value;
                        //dataRow["AdrProfRel1"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["Adr1Rel2"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["Adr2Rel2"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["Adr3Rel2"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["CityRel2"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["DistrictRel2"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["PostCodeRel2"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["StateCodeRel2"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["CntryCodeRel2"] = System.DBNull.Value;
                    }


                    DtAdd.Rows[idr]["AddIdTypeRel"] = ddlProofOfAddress.SelectedValue;
                    if (chkPerAddress.Checked == true)
                    {
                        if (ddlProofOfAddress.SelectedIndex == 1)
                        {
                            DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                            DtAdd.Rows[idr]["AddIdExpDateRel"] = txtPassExpDateAdd.Text.Trim();
                            DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                        }

                        else if (ddlProofOfAddress.SelectedIndex == 2)
                        {
                            DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                            DtAdd.Rows[idr]["AddIdExpDateRel"] = txtPassExpDateAdd.Text.Trim();
                            DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 3)
                        {
                            DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                            DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 4)
                        {
                            DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                            DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 5)
                        {
                            DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                            DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 6)
                        {
                            DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassOthrAdd.Text.Trim();
                            DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["AddIdNameRel"] = txtPassNoAdd.Text.Trim();
                        }

                        else
                        {
                            DtAdd.Rows[idr]["AddIdNumberRel"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                        }
                    }
                    else
                    {
                        DtAdd.Rows[idr]["AddIdNumberRel"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                    }

                    DtAdd.Rows[idr]["DecDateRel"] = txtDate.Text.Trim();
                    DtAdd.Rows[idr]["DecPlaceRel"] = txtPlace.Text.Trim();
                    DtAdd.Rows[idr]["kycEmpNameRel"] = txtEmpName.Text.Trim();
                    DtAdd.Rows[idr]["kycEmpCodeRel"] = txtEmpCode.Text.Trim();
                    DtAdd.Rows[idr]["kycEmpBranchRel"] = txtEmpBranch.Text.Trim();
                    DtAdd.Rows[idr]["kycEmpDesiRel"] = txtEmpDesignation.Text.Trim();
                    DtAdd.Rows[idr]["kycVerDateRel"] = txtDate3.Text.Trim();

                    DtAdd.Rows[idr]["kycCertDocRel"] = System.DBNull.Value;
                    DtAdd.Rows[idr]["kycInstNameRel"] = txtInsName.Text.Trim();
                    DtAdd.Rows[idr]["kycInstCodeRel"] = txtInsCode.Text.Trim();
                    DtAdd.Rows[idr]["SVFlag"] = "P";

                    //dataRow["CREATEDBY"] =strUserId.ToString();

                    //DtAdd.Rows.Add(dataRow);
                    //DataSet dsRel1 = new DataSet();
                    //dsRel1.Clear();
                    //dsRel1.Tables.Add(DtAdd);
                    Session["dsRel"] = DtAdd;
                    lblMsgConfirmYesNo.Text = "Relative Details Partially updated successfully....Do you want to add more?.";
                    ClearTextcntrl();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "AsyncConfirmYesNo();", true);

                    Session["PSSubmit"] = "Y";

                    //if (Request.QueryString["Status"].ToString() == "PMod")
                    //{
                    //    if (Request.QueryString["Action"].ToString() == "Edit")
                    //    {
                    //        btnAdd.Enabled = true;
                    //        btnPartialAdd.Enabled = false;
                    //    }
                    //}


                }
                else
                {
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "btnPartialAdd_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region btnUpdate
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKYCNum.Text.Trim().Length != 14)
                {

                    string Res;
                    Res = objVal.EntityRelatedPrsnValidation(ddlRelType, cboTitle, txtGivenName, txtMiddleName ,txtLastName, cboTitle2, rbtFS,
                    txtGivenName2, txtMiddleName2 ,txtLastName2, cboTitle3, txtGivenName3, txtLastName3, txtDOB, cboGender, txtPanNo, ddlIsoCountryCodeOthr,
                    ddlProofIdentity, txtPassNo, txtPassExpDate, txtPassOthr,
                     chkAppDeclare1, ddlProofOfAddress, txtAddressLine1, txtCity, txtPinCode, txtDate3, txtDate,
                    txtEmpName, txtEmpCode, txtEmpDesignation, txtEmpBranch, txtInsName, txtInsCode, txtPassExpDateAdd, txtPassNoAdd, txtPlace,
                   ddlDocReceived, ddlState, txtPassOthrAdd,txtLocAddLine1, txtCity1, ddlState1,
                    ddlPinCode1, ddlCountryCode1, ddlProofOfAddress, txtPassNoAdd, txtRelRefNumber,
                    ddlDocReceived, chkCuurentAddress, ddlDocReceived, ddlNationality,txtNum,
                     txtTelOff2, txtTelOff, txtTelRes, txtTelRes2, txtMobile, txtMobile2, txtemail, ddlCountryCode,"");//chkHigh, chkMedium, chkLow,ddlProofOfAddress1,
                    if (Res.Equals(""))
                    {
                        if (txtDOB.Text != "")
                        {
                            string date;
                            date = DateTime.Today.ToString("dd\\-MM\\-yyyy");
                            DateTime date1, date2;
                            date1 = DateTime.ParseExact(date.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            date2 = DateTime.ParseExact(txtDOB.Text.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            if (date1 < date2)
                            {
                                //if (Convert.ToDateTime(date) < Convert.ToDateTime(txtDOB.Text))
                                //{
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You cannot select future date')", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('You cannot select future date')", true);
                                return;
                            }
                        }
                        //DtAdd = (DataTable)Session["dsRel"];

                        if (Session["dsRel"] != null)
                        {
                            DtAdd = (DataTable)Session["dsRel"];
                        }
                        else
                        {
                            DtAdd = (DataTable)ViewState["DtAdd"];
                        }

                        int idr;
                        idr = Convert.ToInt32(Request.QueryString["drNo"]);

                        //DataRow dataRow = DtAdd.NewRow();
                        DtAdd.Rows[idr]["FiRefNo"] = txtRefNumber.Text.Trim();
                        DtAdd.Rows[idr]["RelRefNo"] = txtRelRefNumber.Text.Trim();
                        DtAdd.Rows[idr]["RelatedPrsnKYCNo"] = txtKYCNum.Text.Trim();
                        DtAdd.Rows[idr]["RelationType"] = ddlRelType.SelectedValue;
                        DtAdd.Rows[idr]["PrefixRel"] = cboTitle.SelectedValue;
                        DtAdd.Rows[idr]["FNameRel"] = txtGivenName.Text.Trim();
                        DtAdd.Rows[idr]["MNameRel"] = txtMiddleName.Text.Trim();
                        DtAdd.Rows[idr]["LNameRel"] = txtLastName.Text.Trim();
                        DtAdd.Rows[idr]["MaidPrefixRel"] = cboTitle1.SelectedValue;
                        DtAdd.Rows[idr]["MaidFNameRel"] = txtGivenName1.Text.Trim();
                        DtAdd.Rows[idr]["MaidMNameRel"] = txtMiddleName1.Text.Trim();
                        DtAdd.Rows[idr]["MaidLNameRel"] = txtLastName1.Text.Trim();

                        //ddlIsoCountryCode2.SelectedValue

                        if (rbtFS.SelectedValue == "F")
                        {
                            DtAdd.Rows[idr]["FSFlagRel"] = "01";
                        }
                        else
                        {
                            DtAdd.Rows[idr]["FSFlagRel"] = "02";
                        }
                        DtAdd.Rows[idr]["FatherPrefixRel"] = cboTitle2.SelectedValue;
                        DtAdd.Rows[idr]["FatherFNameRel"] = txtGivenName2.Text.Trim();
                        DtAdd.Rows[idr]["FatherMNameRel"] = txtMiddleName2.Text.Trim();
                        DtAdd.Rows[idr]["FatherLNameRel"] = txtLastName2.Text.Trim();
                        DtAdd.Rows[idr]["MotherPrefixRel"] = cboTitle3.SelectedValue;
                        DtAdd.Rows[idr]["MotherFNameRel"] = txtGivenName3.Text;
                        DtAdd.Rows[idr]["MotherMNameRel"] = txtMiddleName3.Text;
                        DtAdd.Rows[idr]["MotherLNameRel"] = txtLastName3.Text;
                        DtAdd.Rows[idr]["DOBRel"] = txtDOB.Text;
                        DtAdd.Rows[idr]["GenderRel"] = cboGender.SelectedValue;
                        DtAdd.Rows[idr]["MaritalStatusRel"] = ddlMaritalStatus.SelectedValue;
                        DtAdd.Rows[idr]["CitizenshipRel"] = ddlCitizenship.SelectedValue;
                        DtAdd.Rows[idr]["ResiStatusRel"] = ddlResStatus.SelectedValue;

                        //---to show on grid

                        if (ddlRelType.SelectedIndex != 0)
                        {
                            DtAdd.Rows[idr]["RelationTypetxt"] = ddlRelType.SelectedItem.Text;
                        }
                        else
                        {
                            DtAdd.Rows[idr]["RelationTypetxt"] = "";
                        }

                        if (cboGender.SelectedIndex != 0)
                        {
                            DtAdd.Rows[idr]["GenderReltxt"] = cboGender.SelectedItem.Text;
                        }
                        else
                        {
                            DtAdd.Rows[idr]["GenderReltxt"] = "";
                        }

                        //DtAdd.Rows[idr]["GenderReltxt"] = cboGender.SelectedItem.Text;

                        if (ddlMaritalStatus.SelectedIndex != 0)
                        {
                            DtAdd.Rows[idr]["MaritalStatusReltxt"] = ddlMaritalStatus.SelectedItem.Text;
                        }
                        else
                        {
                            DtAdd.Rows[idr]["MaritalStatusReltxt"] = "";
                        }
                        //DtAdd.Rows[idr]["MaritalStatusReltxt"] = ddlMaritalStatus.SelectedItem.Text;

                        if (ddlCitizenship.SelectedIndex != 0)
                        {
                            DtAdd.Rows[idr]["CitizenshipReltxt"] = ddlCitizenship.SelectedItem.Text;
                        }
                        else
                        {
                            DtAdd.Rows[idr]["CitizenshipReltxt"] = "";
                        }
                        //DtAdd.Rows[idr]["CitizenshipReltxt"] = ddlCitizenship.SelectedItem.Text;

                        if (ddlResStatus.SelectedIndex != 0)
                        {
                            DtAdd.Rows[idr]["ResiStatusReltxt"] = ddlResStatus.SelectedItem.Text;
                        }
                        else
                        {
                            DtAdd.Rows[idr]["ResiStatusReltxt"] = "";
                        }
                        //DtAdd.Rows[idr]["ResiStatusReltxt"] = ddlResStatus.SelectedItem.Text;

                        if (ddlOccuSubType.SelectedIndex != 0)
                        {
                            DtAdd.Rows[idr]["OccuTypeReltxt"] = ddlOccuSubType.SelectedItem.Text;
                        }
                        else
                        {
                            DtAdd.Rows[idr]["OccuTypeReltxt"] = "";
                        }
                        //DtAdd.Rows[idr]["OccuTypeReltxt"] = ddlOccuSubType.SelectedItem.Text;

                        //DtAdd.Rows[idr]["RelationTypetxt"] = ddlRelType.SelectedItem.Text;

                        //DtAdd.Rows[idr]["GenderReltxt"] = cboGender.SelectedItem.Text;
                        //DtAdd.Rows[idr]["MaritalStatusReltxt"] = ddlMaritalStatus.SelectedItem.Text;
                        //DtAdd.Rows[idr]["CitizenshipReltxt"] = ddlCitizenship.SelectedItem.Text;
                        //DtAdd.Rows[idr]["ResiStatusReltxt"] = ddlResStatus.SelectedItem.Text;
                        //DtAdd.Rows[idr]["OccuTypeReltxt"] = ddlOccuSubType.SelectedItem.Text;

                        //---end to show on grid

                        DtAdd.Rows[idr]["OccuTypeRel"] = ddlOccupation.SelectedValue;


                        if (chkTick.Checked == true)
                        {
                            DtAdd.Rows[idr]["ResForTaxFlagRel"] = "01";
                        }
                        else
                        {
                            DtAdd.Rows[idr]["ResForTaxFlagRel"] = "02";
                        }
                        DtAdd.Rows[idr]["ISOCountryCodeRel"] = ddlIsoCountryCode2.SelectedValue;
                        DtAdd.Rows[idr]["TaxIDNumberRel"] = txtIDResTax.Text.Trim();
                        DtAdd.Rows[idr]["BirthCityRel"] = txtDOBRes.Text.Trim();
                        DtAdd.Rows[idr]["ISOBirthPlaceCodeRel"] = ddlIsoCountry.SelectedValue;
                        DtAdd.Rows[idr]["IdType"] = ddlProofIdentity.SelectedValue;
                        if (ddlProofIdentity.SelectedIndex == 1)
                        {
                            DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                            DtAdd.Rows[idr]["IdExpDate"] = txtPassExpDate.Text.Trim();
                            DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;

                        }
                        else if (ddlProofIdentity.SelectedIndex == 2)
                        {
                            DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                            DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;

                        }
                        else if (ddlProofIdentity.SelectedIndex == 3)
                        {
                            DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                            DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                        }
                        else if (ddlProofIdentity.SelectedIndex == 4)
                        {
                            DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                            DtAdd.Rows[idr]["IdExpDate"] = txtPassExpDate.Text.Trim();
                            DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                        }
                        else if (ddlProofIdentity.SelectedIndex == 5)
                        {
                            DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                            DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                        }
                        else if (ddlProofIdentity.SelectedIndex == 6)
                        {
                            DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                            DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                        }
                        else if (ddlProofIdentity.SelectedIndex == 7)
                        {
                            DtAdd.Rows[idr]["IdNumber"] = txtPassOthr.Text.Trim();
                            DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["IdName"] = txtPassNo.Text.Trim();
                        }
                        else if (ddlProofIdentity.SelectedIndex == 8 || ddlProofIdentity.SelectedIndex == 9)
                        {
                            DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                            DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                        }
                        else
                        {
                            DtAdd.Rows[idr]["IdNumber"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                        }

                        if (CheckBox1.Checked == true)
                        {
                            DtAdd.Rows[idr]["SameasPOIAddresFlagP1"] = "01";
                            // htParam.Add("@SameasPOIAddresFlagP1", "01");
                        }
                        else
                        {
                            DtAdd.Rows[idr]["SameasPOIAddresFlagP1"] = "";
                        }

                        if (chkPerAddress.Checked == true)
                        {
                            DtAdd.Rows[idr]["CnctTypeRel"] = "P1";
                            DtAdd.Rows[idr]["AdrTypeRel"] = ddlAddressType.SelectedValue;
                            DtAdd.Rows[idr]["AdrProfRel"] = ddlProofOfAddress.SelectedValue;
                            DtAdd.Rows[idr]["Adr1Rel"] = txtAddressLine1.Text.Trim();
                            DtAdd.Rows[idr]["Adr2Rel"] = txtAddressLine2.Text.Trim();
                            DtAdd.Rows[idr]["Adr3Rel"] = txtAddressLine3.Text.Trim();
                            DtAdd.Rows[idr]["CityRel"] = txtCity.Text.Trim();
                            DtAdd.Rows[idr]["DistrictRel"] = txtDistrictname.Text;
                            DtAdd.Rows[idr]["PostCodeRel"] = txtPinCode.Text;
                            DtAdd.Rows[idr]["StateCodeRel"] = ddlState.SelectedValue;
                            DtAdd.Rows[idr]["CntryCodeRel"] = ddlCountryCode.SelectedValue;
                        }
                        else
                        {
                            DtAdd.Rows[idr]["CnctTypeRel"] = "";
                            DtAdd.Rows[idr]["AdrTypeRel"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["AdrProfRel"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["Adr1Rel"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["Adr2Rel"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["Adr3Rel"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["CityRel"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["DistrictRel"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["PostCodeRel"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["StateCodeRel"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["CntryCodeRel"] = System.DBNull.Value;
                        }

                        if (chkCuurentAddress.Checked == true)
                        {
                            DtAdd.Rows[idr]["SameasCurrentAddresFlagM1"] = "01";
                            //htParam.Add("@SameasCurrentAddresFlagM1", "01");
                        }
                        else
                        {
                            DtAdd.Rows[idr]["SameasCurrentAddresFlagM1"] = "";
                        }

                        if (chkLocalAddress.Checked == true)
                        {
                            DtAdd.Rows[idr]["CnctTypeRel1"] = "M1";
                            DtAdd.Rows[idr]["AdrTypeRel1"] = ddlAddressType1.SelectedValue;
                            //dataRow["AdrProfRel1"] = ddlProofOfAddress.SelectedValue;
                            DtAdd.Rows[idr]["Adr1Rel1"] = txtLocAddLine1.Text.Trim();
                            DtAdd.Rows[idr]["Adr2Rel1"] = txtLocAddLine2.Text.Trim();
                            DtAdd.Rows[idr]["Adr3Rel1"] = txtLocAddLine3.Text.Trim();
                            DtAdd.Rows[idr]["CityRel1"] = txtCity1.Text.Trim();
                            DtAdd.Rows[idr]["DistrictRel1"] = txtDistrict1.Text;
                            DtAdd.Rows[idr]["PostCodeRel1"] = ddlPinCode1.Text;
                            DtAdd.Rows[idr]["StateCodeRel1"] = ddlState1.SelectedValue;
                            DtAdd.Rows[idr]["CntryCodeRel1"] = ddlCountryCode1.SelectedValue;
                        }
                        else
                        {
                            DtAdd.Rows[idr]["CnctTypeRel1"] = "";
                            DtAdd.Rows[idr]["AdrTypeRel1"] = System.DBNull.Value;
                            //dataRow["AdrProfRel1"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["Adr1Rel1"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["Adr2Rel1"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["Adr3Rel1"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["CityRel1"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["DistrictRel1"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["PostCodeRel1"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["StateCodeRel1"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["CntryCodeRel1"] = System.DBNull.Value;
                        }

                        if (chkCurrentAdd.Checked == true)
                        {
                            DtAdd.Rows[idr]["SameasLocalAddressFlagJ1"] = "01";
                            //htParam.Add("@SameasLocalAddressFlagJ1", "01");
                        }
                        else
                        {
                            DtAdd.Rows[idr]["SameasLocalAddressFlagJ1"] = "";
                        }

                        if (chkCorresAdd.Checked == true)
                        {
                            DtAdd.Rows[idr]["SameasLocalAddressFlagJ2"] = "01";
                            // htParam.Add("@SameasLocalAddressFlagJ2", "01");
                        }
                        else
                        {
                            DtAdd.Rows[idr]["SameasLocalAddressFlagJ2"] = "";
                        }

                        if (chkAddResident.Checked == true)
                        {
                            DtAdd.Rows[idr]["CnctTypeRel2"] = "J1";
                            DtAdd.Rows[idr]["AdrTypeRel2"] = ddlAddressType2.SelectedValue;
                            //dataRow["AdrProfRel1"] = ddlProofOfAddress.SelectedValue;
                            DtAdd.Rows[idr]["Adr1Rel2"] = txtAddLine1.Text.Trim();
                            DtAdd.Rows[idr]["Adr2Rel2"] = txtAddLine2.Text.Trim();
                            DtAdd.Rows[idr]["Adr3Rel2"] = txtAddLine3.Text.Trim();
                            DtAdd.Rows[idr]["CityRel2"] = txtCity2.Text.Trim();
                            DtAdd.Rows[idr]["DistrictRel2"] = txtDistrict2.Text;
                            DtAdd.Rows[idr]["PostCodeRel2"] = ddlPinCode2.Text;
                            DtAdd.Rows[idr]["StateCodeRel2"] = ddlState2.SelectedValue;
                            DtAdd.Rows[idr]["CntryCodeRel2"] = ddlIsoCountryCode.SelectedValue;
                        }
                        else
                        {
                            DtAdd.Rows[idr]["CnctTypeRel2"] = "";
                            DtAdd.Rows[idr]["AdrTypeRel2"] = System.DBNull.Value;
                            //dataRow["AdrProfRel1"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["Adr1Rel2"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["Adr2Rel2"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["Adr3Rel2"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["CityRel2"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["DistrictRel2"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["PostCodeRel2"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["StateCodeRel2"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["CntryCodeRel2"] = System.DBNull.Value;
                        }


                        DtAdd.Rows[idr]["AddIdTypeRel"] = ddlProofOfAddress.SelectedValue;
                        if (chkPerAddress.Checked == true)
                        {
                            if (ddlProofOfAddress.SelectedIndex == 1)
                            {
                                DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                                DtAdd.Rows[idr]["AddIdExpDateRel"] = txtPassExpDateAdd.Text.Trim();
                                DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                            }

                            else if (ddlProofOfAddress.SelectedIndex == 2)
                            {
                                DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                                DtAdd.Rows[idr]["AddIdExpDateRel"] = txtPassExpDateAdd.Text.Trim();
                                DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                            }
                            else if (ddlProofOfAddress.SelectedIndex == 3)
                            {
                                DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                                DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                                DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                            }
                            else if (ddlProofOfAddress.SelectedIndex == 4)
                            {
                                DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                                DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                                DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                            }
                            else if (ddlProofOfAddress.SelectedIndex == 5)
                            {
                                DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                                DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                                DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                            }
                            else if (ddlProofOfAddress.SelectedIndex == 6)
                            {
                                DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassOthrAdd.Text.Trim();
                                DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                                DtAdd.Rows[idr]["AddIdNameRel"] = txtPassNoAdd.Text.Trim();
                            }

                            else
                            {
                                DtAdd.Rows[idr]["AddIdNumberRel"] = System.DBNull.Value;
                                DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                                DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                            }
                        }
                        else
                        {
                            DtAdd.Rows[idr]["AddIdNumberRel"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                        }

                        DtAdd.Rows[idr]["DecDateRel"] = txtDate.Text.Trim();
                        DtAdd.Rows[idr]["DecPlaceRel"] = txtPlace.Text.Trim();
                        DtAdd.Rows[idr]["kycEmpNameRel"] = txtEmpName.Text.Trim();
                        DtAdd.Rows[idr]["kycEmpCodeRel"] = txtEmpCode.Text.Trim();
                        DtAdd.Rows[idr]["kycEmpBranchRel"] = txtEmpBranch.Text.Trim();
                        DtAdd.Rows[idr]["kycEmpDesiRel"] = txtEmpDesignation.Text.Trim();
                        DtAdd.Rows[idr]["kycVerDateRel"] = txtDate3.Text.Trim();

                        DtAdd.Rows[idr]["kycCertDocRel"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["kycInstNameRel"] = txtInsName.Text.Trim();
                        DtAdd.Rows[idr]["kycInstCodeRel"] = txtInsCode.Text.Trim();
                        DtAdd.Rows[idr]["SVFlag"] = "P";

                        //dataRow["CREATEDBY"] =strUserId.ToString();

                        //DtAdd.Rows.Add(dataRow);
                        //DataSet dsRel1 = new DataSet();
                        //dsRel1.Clear();
                        //dsRel1.Tables.Add(DtAdd);
                        Session["dsRel"] = DtAdd;
                        lblMsgConfirmYesNo.Text = "Relative Details updated successfully....Do you want to add more?.";
                        ClearTextcntrl();
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "AsyncConfirmYesNo();", true);

                        Session["PSSubmit"] = "N";

                        //if (Request.QueryString["Status"].ToString() == "PMod")
                        //{
                        //    if (Request.QueryString["Action"].ToString() == "Edit")
                        //    {
                        //        btnAdd.Enabled = true;
                        //        btnPartialAdd.Enabled = false;
                        //    }
                        //}
                    }
                    else
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "AlertMsg('" + Res + "')", true);
                        //     ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsgs", "AlertMsg('" + Res + "')", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + Res + "');", true);
                        return;
                    }
                }
                #region 14 DIGIT KYC NUMBER SAVE FUNCTIONALITY START
                else
                {
                    string Res1;
                    Res1 = objVal.RelatedPrsnValidationAccorKYC(ddlRelType, cboTitle, txtGivenName, txtLastName, cboTitle2, rbtFS,
                    txtGivenName2, txtLastName2, cboTitle3, txtGivenName3, txtLastName3, txtDOB, cboGender, ddlIsoCountryCodeOthr,
                    ddlProofIdentity, txtPassNo, txtPassExpDate, txtPassOthr,
                     chkAppDeclare1, ddlProofOfAddress, txtAddressLine1, txtCity, txtPinCode, txtDate3, txtDate,
                    txtEmpName, txtEmpCode, txtEmpDesignation, txtEmpBranch, txtInsName, txtInsCode, txtPassExpDateAdd, txtPassNoAdd, txtPlace,
                    ddlDocReceived, ddlState, txtPassOthrAdd,txtNum);//chkHigh, chkMedium, chkLow,

                    if (Res1.Equals(""))
                    {
                        DtAdd = (DataTable)ViewState["DtAdd"];
                        DataRow dataRow = DtAdd.NewRow();
                        dataRow["FiRefNo"] = txtRefNumber.Text.Trim();
                        dataRow["RelRefNo"] = txtRelRefNumber.Text.Trim();
                        dataRow["RelatedPrsnKYCNo"] = txtKYCNum.Text.Trim();
                        dataRow["RelationType"] = ddlRelType.SelectedValue;
                        dataRow["PrefixRel"] = cboTitle.SelectedValue;
                        dataRow["FNameRel"] = txtGivenName.Text.Trim();
                        dataRow["MNameRel"] = txtMiddleName.Text.Trim();
                        dataRow["LNameRel"] = txtLastName.Text.Trim();
                        //dataRow["MaidPrefixRel"] = cboTitle1.SelectedValue;
                        //dataRow["MaidFNameRel"] = txtGivenName1.Text.Trim();
                        //dataRow["MaidMNameRel"] = txtMiddleName1.Text.Trim();
                        //dataRow["MaidLNameRel"] = txtLastName1.Text.Trim();

                        //if (rbtFS.SelectedValue == "F")
                        //{
                        //    dataRow["FSFlagRel"] = "01";
                        //}
                        //else
                        //{
                        //    dataRow["FSFlagRel"] = "02";
                        //}
                        //dataRow["FatherPrefixRel"] = cboTitle2.SelectedValue;
                        //dataRow["FatherFNameRel"] = txtGivenName2.Text.Trim();
                        //dataRow["FatherMNameRel"] = txtMiddleName2.Text.Trim();
                        //dataRow["FatherLNameRel"] = txtLastName2.Text.Trim();
                        //dataRow["MotherPrefixRel"] = cboTitle3.SelectedValue;
                        //dataRow["MotherFNameRel"] = txtGivenName3.Text;
                        //dataRow["MotherMNameRel"] = txtMiddleName2.Text;
                        //dataRow["MotherLNameRel"] = txtLastName3.Text;
                        //dataRow["DOBRel"] = txtDOB.Text;
                        //dataRow["GenderRel"] = cboGender.SelectedValue;
                        //dataRow["MaritalStatusRel"] = ddlMaritalStatus.SelectedValue;
                        //dataRow["CitizenshipRel"] = ddlCitizenship.SelectedValue;
                        //dataRow["ResiStatusRel"] = ddlResStatus.SelectedValue;

                        ////---to show on grid
                        //dataRow["RelationTypetxt"] = ddlRelType.SelectedItem;

                        //dataRow["GenderReltxt"] = cboGender.SelectedItem;
                        //dataRow["MaritalStatusReltxt"] = ddlMaritalStatus.SelectedItem;
                        //dataRow["CitizenshipReltxt"] = ddlCitizenship.SelectedItem;
                        //dataRow["ResiStatusReltxt"] = ddlResStatus.SelectedItem;
                        //dataRow["OccuTypeReltxt"] = ddlOccupation.SelectedItem;

                        ////---end to show on grid

                        //dataRow["OccuTypeRel"] = ddlOccupation.SelectedValue;


                        //if (chkTick.Checked == true)
                        //{
                        //    dataRow["ResForTaxFlagRel"] = "01";
                        //}
                        //else
                        //{
                        //    dataRow["ResForTaxFlagRel"] = "02";
                        //}
                        //dataRow["ISOCountryCodeRel"] = ddlIsoCountryCodeOthr.SelectedValue;
                        //dataRow["TaxIDNumberRel"] = txtIDResTax.Text.Trim();
                        //dataRow["BirthCityRel"] = txtDOBRes.Text.Trim();
                        //dataRow["ISOBirthPlaceCodeRel"] = ddlIsoCountry.SelectedValue;
                        //dataRow["DecDateRel"] = txtDate.Text.Trim();
                        //dataRow["DecPlaceRel"] = txtPlace.Text.Trim();
                        //dataRow["kycEmpNameRel"] = txtEmpName.Text.Trim();
                        //dataRow["kycEmpCodeRel"] = txtEmpCode.Text.Trim();
                        //dataRow["kycEmpBranchRel"] = txtEmpBranch.Text.Trim();
                        //dataRow["kycEmpDesiRel"] = txtEmpDesignation.Text.Trim();
                        //dataRow["kycVerDateRel"] = txtDate3.Text.Trim();
                        //if (chkSelfCerti.Checked == true)
                        //{
                        //    dataRow["kycCertDocRel"] = "01";
                        //}
                        //else
                        //{
                        //    dataRow["kycCertDocRel"] = System.DBNull.Value;
                        //}
                        dataRow["kycInstNameRel"] = txtInsName.Text.Trim();
                        dataRow["kycInstCodeRel"] = txtInsCode.Text.Trim();
                        DtAdd.Rows.Add(dataRow);
                        Session["dsRel"] = DtAdd;
                        lblMsgConfirmYesNo.Text = "Related Person Details added successfully....Do you want to add more?.";
                        ClearTextcntrl();
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "AsyncConfirmYesNo();", true);
                        btnAdd.Enabled = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + Res1 + "');", true);
                        return;
                    }
                }
                #endregion 14 DIGIT KYC NUMBER SAVE FUNCTIONALITY END
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "btnAdd_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region Bind document
        private void BindGridImage()
        {
            try
            {
                #region photo shuffle start added by rachana on 01-07-2013

                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                //htParam.Clear();
                //htParam.Add("@USERID", strUserId.Trim());
                //dt = objDAL.GetDataTable("Prc_GetAttestation", htParam);

                htParam.Clear();
                htParam.Add("@RegNo", Request.QueryString["RelRefNo"].ToString());
                dt = objDAL.GetDataTable("prc_GetDocType", htParam);
                //  objds = objDAL.GetDataSet("prc_GetDocType", objht, "CKYCConnectionString");

                if (dt.Rows.Count > 0)
                {
                    ViewState["DOC_NAME"] = dt.Rows[0]["DOC_NAME"].ToString();
                    ViewState["DocNo"] = dt.Rows[0]["DOC_CODE"].ToString();
                    ViewState["docCode"] = dt.Rows[0]["SHORTCODE"].ToString();
                }
                #endregion

                dt.Clear();
                htParam.Clear();
                htParam.Add("@RegNo", Request.QueryString["RelRefNo"].ToString());
                htParam.Add("@DOC_NAME", ViewState["DOC_NAME"]);
                dt = objDAL.GetDataTable("prc_GetImage", htParam);
                //objDt = objDAL.GetDataSet("prc_GetImage", objht, "CKYCConnectionString");
                GridImage.DataSource = dt;
                GridImage.DataBind();
                ViewState["Img"] = dt;
                ViewState["Img1"] = dt;
                if (ViewState["DOC_NAME"] != null)
                {
                    lblpanelheader.Text = ViewState["DOC_NAME"].ToString();
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
                    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    objErr.LogErr(AppID, "CKYCEntView.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
            finally
            {
                //con.Close();
            }

        }
        #endregion

        protected void GridImage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hdnid = (HiddenField)e.Row.FindControl("hdnid");
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
                    //string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    //System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    //objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    //throw ex;
                }
            }
        }

        protected void btnnext_Click(object sender, EventArgs e)
        {


            try
            {
                int intcode = Convert.ToInt32(ViewState["docCode"]);

                //int intcode1 = intcode + 1;
                //if (intcode > 1)
                //{
                //    btnprev.Enabled = true;
                //}

                // objds.Clear();
                // objht.Clear();
                objht.Add("@RegNo", Request.QueryString["RelRefNo"].ToString());
                objDAL = new DataAccessLayer("CKYCConnectionString");
                objDt = objDAL.GetDataTable("prc_GetDocType", objht);
                // objds = objDAL.GetDataSet("prc_GetDocType", objht, "CKYCConnectionString");
                for (int i = 0; i < objDt.Rows.Count; i++)
                {
                    string str = objDt.Rows[i]["SHORTCODE"].ToString();
                    if (Convert.ToInt32(ViewState["docCode"]) == Convert.ToInt32(str))
                    {
                        string doctype = objDt.Rows[i]["DOC_NAME"].ToString();
                        ViewState["DOC_NAME"] = objDt.Rows[i]["DOC_NAME"].ToString();
                        hdnDocId.Value = objDt.Rows[i]["SHORTCODE"].ToString();//01
                    }
                }

                Hashtable htParam = new Hashtable();
                htParam.Clear();
                DataSet dsResult1 = new DataSet();
                htParam.Add("@RegNo", Request.QueryString["RelRefNo"].ToString());
                htParam.Add("@DOC_NAME", ViewState["DOC_NAME"]);
                dsResult1 = objDAL.GetDataSet("prc_GetImage", htParam);
                if (intcode < dsResult1.Tables[0].Rows.Count)
                {
                    btnnext.Enabled = true;
                }

                else
                {
                    btnnext.Enabled = false;
                }
                GridImage.Visible = true;
                GridImage.DataSource = dsResult1;
                GridImage.DataBind();
                ViewState["Img"] = dsResult1;
                ViewState["Img1"] = dsResult1;
                ViewState["docCode"] = intcode;
                if (ViewState["DOC_NAME"] != null)
                {
                    lblpanelheader.Text = ViewState["DOC_NAME"].ToString();
                }
                divloaderqc.Attributes.Add("style", "display:none");

            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    objErr.LogErr(AppID, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }

        }

        protected void btnprev_Click(object sender, EventArgs e)
        {
            try
            {
                int intcode = Convert.ToInt32(ViewState["docCode"]);
                intcode = intcode - 1;



                //objds.Clear();
                objht.Clear();
                objht.Add("@RegNo", Request.QueryString["RelRefNo"].ToString());
                // objds = objDAL.GetDataSet("prc_GetDocType", objht, "CKYCConnectionString");
                objDAL = new DataAccessLayer("CKYCConnectionString");
                objDt = objDAL.GetDataTable("prc_GetDocType", objht);
                if (intcode < objDt.Rows.Count)
                {
                    btnnext.Enabled = true;
                }
                else
                {
                    btnnext.Enabled = false;
                }
                for (int i = 0; i < objDt.Rows.Count; i++)
                {
                    string str = objDt.Rows[i]["SHORTCODE"].ToString();
                    if (intcode == Convert.ToInt32(str))
                    {
                        string doctype = objDt.Rows[i]["DOC_NAME"].ToString();
                        ViewState["DOC_NAME"] = objDt.Rows[i]["DOC_NAME"].ToString();
                        hdnDocId.Value = objDt.Rows[i]["SHORTCODE"].ToString();//01
                    }
                }
                objDt.Clear();
                objht.Clear();
                objht.Add("@RegNo", Request.QueryString["RelRefNo"].ToString());
                objht.Add("@DOC_NAME", ViewState["DOC_NAME"]);
                //  objds = objDAL.GetDataSet("prc_GetImage", objht, "CKYCConnectionString");
                objDt = objDAL.GetDataTable("prc_GetImage", objht);
                if (intcode < objDt.Rows.Count)
                {
                    btnnext.Enabled = true;
                }

                else
                {
                    btnnext.Enabled = false;
                }
                GridImage.Visible = true;
                GridImage.DataSource = objDt;
                GridImage.DataBind();
                ViewState["Img"] = objDt;
                ViewState["Img1"] = objDt;
                ViewState["docCode"] = intcode;
                if (ViewState["DOC_NAME"] != null)
                {
                    lblpanelheader.Text = ViewState["DOC_NAME"].ToString();
                }

                if (intcode == 1)
                {
                    btnprev.Enabled = false;
                }

                else
                {
                    btnprev.Enabled = true;
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
                    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    objErr.LogErr(AppID, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }

        protected void ddlCountryCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtState.Text = "";
            ddlState.SelectedIndex = 0;
            txtDistrictname.Text = "";
            txtPinCode.Text = "";
            if (ddlCountryCode.SelectedValue == "IN")
            {
                dvState.Visible = true;
                txtState.Visible = false;
                txtPinCode.Enabled = false;
                txtDistrictname.Enabled = false;
                txtDistrictname.Attributes.Add("readonly", "readonly");
                txtPinCode.Attributes.Add("readonly", "readonly");
            }
            else
            {
                dvState.Visible = false;
                txtState.Visible = true;
                txtPinCode.Enabled = true;
                txtDistrictname.Enabled = true;
                txtDistrictname.Attributes.Remove("readonly");
                txtPinCode.Attributes.Remove("readonly");
                //txtDistrictname.Attributes.Remove("readonly", "readonly");
                //txtPinCode.Attributes.Remove("readonly", "readonly");
            }


        }

        protected void ddlCountryCode1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDistrict1.Text = "";
            ddlState1.SelectedIndex = 0;
            txtState1.Text = "";
            ddlPinCode1.Text = "";
            if (ddlCountryCode1.SelectedValue == "IN")
            {
                dvState1.Visible = true;
                txtState1.Visible = false;
                ddlPinCode1.Enabled = false;
                txtDistrict1.Enabled = false;
                txtDistrict1.Attributes.Add("readonly", "readonly");
                ddlPinCode1.Attributes.Add("readonly", "readonly");
            }
            else
            {
                dvState1.Visible = false;
                txtState1.Visible = true;
                ddlPinCode1.Enabled = true;
                txtDistrict1.Enabled = true;
                txtDistrict1.Attributes.Remove("readonly");
                ddlPinCode1.Attributes.Remove("readonly");
            }
        }

        protected void ddlIsoCountryCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDistrict2.Text = "";
            ddlState2.SelectedIndex = 0;
            txtState2.Text = "";
            ddlPinCode2.Text = "";
            if (ddlIsoCountryCode.SelectedValue == "IN")
            {
                dvState2.Visible = true;
                txtState2.Visible = false;
                ddlPinCode2.Enabled = false;
                txtDistrict2.Enabled = false;
                txtDistrict2.Attributes.Add("readonly", "readonly");
                ddlPinCode2.Attributes.Add("readonly", "readonly");
            }
            else
            {
                dvState2.Visible = false;
                txtState2.Visible = true;
                ddlPinCode2.Enabled = true;
                txtDistrict2.Enabled = true;
                txtDistrict2.Attributes.Remove("readonly");
                ddlPinCode2.Attributes.Remove("readonly");
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
    }
}