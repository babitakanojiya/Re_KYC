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
using System.Web.Services;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class CKYCRelatedPrsn : System.Web.UI.Page
    {
        #region Declare Veriables
        private MultilingualManager olng;

        public static string FlagPageTyp = "";
        string RowIndex = "";
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
        DataTable DtAddRel = new DataTable();
        DataTable PartialDtAdd = new DataTable();
        DataTable dt;
        int AppID;
        CommonClass common = new CommonClass();
        string kycno = string.Empty;
        #endregion

        #region PAGELOADEVENTS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["kycno"] != null)
                {
                    kycno = Request.QueryString["kycno"].ToString();
                }
                if (Request.QueryString["FiRefNo"] != null)
                {
                    txtRefNumber.Text = Request.QueryString["FiRefNo"].ToString();
                }
                txtRefNumber.Enabled = false;

                if (Session["dsRel"] == null)// && IsPostBack ==true
                {
                    txtRelRefNumber.Text = txtRefNumber.Text + '_' + "R1";
                }
                else
                {
                    if (Session["dsRel"] != null)
                    {

                        DtAddRel = (DataTable)Session["dsRel"];
                        if (DtAddRel.Rows.Count != 0 && (Request.QueryString["Status"].ToString() != "View"))
                        {
                            string Val = "";
                            int RefCount = 0;
                            //txtRelRefNumber.Text = txtRefNumber.Text + '-' + "0" + (DtAdd.Rows.Count + 1);
                            for (int i = 0; i < DtAddRel.Rows.Count; i++)
                            {
                                Val = Convert.ToString(DtAddRel.Rows[i]["RelRefNo"]);
                            }
                            RefCount = Convert.ToInt32(Val.Substring(Val.IndexOf('R') + 1, Val.Length - (Val.IndexOf('R') + 1)));

                            txtRelRefNumber.Text = txtRefNumber.Text + '_' + "R" + (RefCount + 1);

                        }
                    }
                }
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


                if (Request.QueryString["FlagPageTyp"] != null)
                {
                    FlagPageTyp = Request.QueryString["FlagPageTyp"].ToString();
                    hdnPageFlag.Value = FlagPageTyp.ToString();
                }
                if (Request.QueryString["RowNo"] != null)
                {
                    RowIndex = Request.QueryString["RowNo"].ToString();
                }
                if (FlagPageTyp == "Legal")
                {
                    chkAppDeclare3.Text = "I/We hereby consent to receiving information from Central KYC Registry through SMS / Email on the above registered number / email address.";
                }
                else
                {
                    chkAppDeclare3.Text = "I hereby consent to receiving information from Central KYC Registry through SMS / Email on the above registered number / email address.";
                }
                Session["CarrierCode"] = '2';
                olng = new MultilingualManager("DefaultConn", "CKYCRelatedPrsn.aspx", Session["UserLangNum"].ToString());
                strUserId = HttpContext.Current.Session["UserID"].ToString().Trim();
                if (!IsPostBack)
                {
                    SetDataTable();
                    if (Request.QueryString["Status"].ToString() == "reg")
                    {
                        chkAppDeclare3.Enabled = true;
                        chkAppDeclare1.Enabled = true;
                        FillDocumentReceived();
                        BindAttestation();
                        fillcboTitle1();
                        fillcboTitle2();
                        FillCountry();
                        FillddlPageLoad();
                        divIdProof.Visible = false;
                        divAddProof.Visible = false;
                        divAddProof1.Visible = false;
                        PopulateRelatedPerson();
                        PopulateAddressProofType();
                        subPopulateGender();
                        subPopulateTitle();
                        PopulateProofIdentiy();
                        PopulatePinCode();
                        //added by ramesh
                        Fillcountrycd1();

                        FillStates();
                        //chkSelfCerti.Checked = true;
                    }
                    else if (Request.QueryString["Status"].ToString() == "Mod")
                    {
                        divIdProof.Visible = false;
                        PopulateRelatedPerson();
                        PopulateAddressProofType();
                        subPopulateGender();
                        subPopulateTitle();
                        PopulateProofIdentiy();
                        //PopulatePinCode();
                        Fillcountrycd1();
                        fillcboTitle2();
                        fillcboTitle1();
                        FillCountry();
                        FillDocumentReceived();
                        PopulatePinCode();
                        FillddlPageLoad();
                        FillRelatedPersondata();
                        ddlProofOfAddress_SelectedIndexChanged(this, e);
                        txtPassNoAdd.Text = ViewState["strIdNumber1"].ToString();
                        ddlProofOfAddress1_SelectedIndexChanged(this, e);
                        txtPassNoAdd1.Text = ViewState["strIdNumber2"].ToString();
                        //chkCuurentAddress_Checked(this,e);
                        ddlPinCode01_SelectedIndexChanged(this, e);
                        ddlPinCode_SelectedIndexChanged(this, e);
                        if (chkCuurentAddress.Checked == true)
                        {
                            chkCuurentAddress_Checked(this, e);
                        }


                        btnAdd.Visible = true;
                        disablecntrl();
                    }
                    else if (Request.QueryString["Status"].ToString() == "View")
                    {
                        disablecntrl();
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
                        FillDocumentReceived();
                        divIdProof.Visible = false;
                        divAddProof.Visible = false;
                        divAddProof1.Visible = false;
                        PopulateRelatedPerson();
                        PopulateAddressProofType();
                        PopulatePinCode();
                        subPopulateGender();
                        subPopulateTitle();
                        PopulateProofIdentiy();
                        //PopulatePinCode();
                        Fillcountrycd1();
                        FillCountry();
                        FillStates();
                        //FillRelatedPersonPartialdata();
                        FillRelatedPersonViewData();
                        BindGrid();
                        ddlRelType_SelectedIndexChanged(this, e);
                        txtNum.Text = ViewState["txtNum"].ToString();
                        ddlProofOfAddress_SelectedIndexChanged(this, e);
                        txtPassNoAdd.Text = ViewState["strPOIVal"].ToString();
                        ddlProofOfAddress1_SelectedIndexChanged(this, e);
                        txtPassNoAdd1.Text = ViewState["strPOAVal"].ToString();
                        //chkCuurentAddress_Checked(this,e);
                        ddlPinCode01_SelectedIndexChanged(this, e);
                        ddlPinCode_SelectedIndexChanged1(this, e);
                        if (chkCuurentAddress.Checked == true)
                        {
                            chkCuurentAddress_Checked(this, e);
                        }

                        btnPartialAdd.Visible = false;
                        btnAdd.Visible = false;
                        btnPSUpdate.Visible = false;
                        btnUpdate.Visible = false;


                        BindGridImage();
                        divImg.Visible = true;

                    }
                    else if (Request.QueryString["Status"].ToString() == "QC")
                    {
                        disablecntrl();
                        fillcboTitle1();
                        fillcboTitle2();
                        FillddlPageLoad();
                        FillDocumentReceived();
                        divIdProof.Visible = false;
                        divAddProof.Visible = false;
                        divAddProof1.Visible = false;
                        PopulateRelatedPerson();
                        PopulateAddressProofType();
                        PopulatePinCode();
                        subPopulateGender();
                        subPopulateTitle();
                        PopulateProofIdentiy();
                        //PopulatePinCode();
                        Fillcountrycd1();
                        FillCountry();
                        FillStates();
                        //FillRelatedPersonPartialdata();
                        FillRelatedPersonViewData();
                        //FillRelatedPersondata(); commented by Rutuja
                        ddlRelType_SelectedIndexChanged(this, e);
                        ddlProofOfAddress_SelectedIndexChanged(this, e);
                        txtPassNoAdd.Text = ViewState["strPOIVal"].ToString();
                        ddlProofOfAddress1_SelectedIndexChanged(this, e);
                        txtPassNoAdd1.Text = ViewState["strPOAVal"].ToString();
                        //chkCuurentAddress_Checked(this,e);
                        ddlPinCode01_SelectedIndexChanged(this, e);
                        ddlPinCode_SelectedIndexChanged1(this, e);
                        if (chkCuurentAddress.Checked == true)
                        {
                            chkCuurentAddress_Checked(this, e);
                        }

                        btnPartialAdd.Visible = false;
                        btnAdd.Visible = false;
                        btnPSUpdate.Visible = false;
                        btnUpdate.Visible = false;

                        txtNum.Text = ViewState["txtNum"].ToString();

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
                        divAddProof1.Visible = false;
                        PopulateRelatedPerson();
                        PopulateAddressProofType();
                        subPopulateGender();
                        subPopulateTitle();
                        PopulateProofIdentiy();
                        //PopulatePinCode();
                        Fillcountrycd1();
                        FillCountry();
                        FillStates();
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
                        PopulateAddressProofType();
                        PopulateRelatedPerson();
                        subPopulateGender();
                        subPopulateTitle();
                        PopulateProofIdentiy();
                        //PopulatePinCode();
                        Fillcountrycd1();
                        FillRequiredDataForCKYC();
                        btnAdd.Visible = false;
                    }

                    //FillDistrictState(ddlPinCode, ddlDistrict, ddlState);
                    if (ddlProofIdentity1.SelectedIndex == 0)
                    {
                        divIdProof.Visible = false;
                    }
                    else if (ddlProofIdentity1.SelectedIndex == 1)
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
                        //Filtered`Extender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNo.MaxLength = 15;
                        txtPassNo.Attributes.Remove("onblur");
                        txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");
                        //txtPassNo.Focus();
                    }
                    else if (ddlProofIdentity1.SelectedIndex == 2)
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
                    else if (ddlProofIdentity1.SelectedIndex == 3)
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
                    else if (ddlProofIdentity1.SelectedIndex == 4)
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
                    else if (ddlProofIdentity1.SelectedIndex == 5)
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
                    else if (ddlProofIdentity1.SelectedIndex == 6)
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
                    else if (ddlProofIdentity1.SelectedIndex == 7)
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
                    else if (ddlProofIdentity1.SelectedIndex == 8)
                    {
                        divIdProof.Visible = true;
                        lblPassportNo.Text = "Document Name";
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
                    else if (ddlProofOfAddress.SelectedItem.Text == "Passport")
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
                    else if (ddlProofOfAddress.SelectedItem.Text == "Driving Licence")
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
                    else if (ddlProofOfAddress.SelectedItem.Text == "Proof of Possession of Aadhaar")
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
                    else if (ddlProofOfAddress.SelectedItem.Text == "Voter ID Card")
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
                    else if (ddlProofOfAddress.SelectedItem.Text == "NREGA Job Card")
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
                        lblPassportNoAdd.Text = "Document Number";
                        llPassExpDateAdd.Text = "Identification Number";
                        txtPassExpDateAdd.Visible = false;
                        hidetxtPassExpDateAdd.Visible = false;
                        llPassExpDateAdd.Visible = true;
                        divPassAdd.Visible = true;
                        llPassExpDateAdd.Visible = false;
                        //txtPassExpDateAdd.Visible = false;
                        txtPassOthrAdd.Visible = false;
                        txtPassNoAdd.Visible = true;
                        //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        txtPassNoAdd.MaxLength = 15;
                        txtPassNoAdd.Attributes.Remove("onblur");
                    }


                    //btnAdd.Attributes.Add("onClick", "javascript: return funCKYC();");
                    txtPinCode.Attributes.Add("readonly", "readonly");
                    ddlPinCode1.Attributes.Add("readonly", "readonly");
                    txtDistrictname.Attributes.Add("readonly", "readonly");
                    txtDistrict1.Attributes.Add("readonly", "readonly");
                    txtDOB.Attributes.Add("readonly", "readonly");
                    txtPassExpDate.Attributes.Add("readonly", "readonly");
                    txtPassExpDateAdd.Attributes.Add("readonly", "readonly");
                    //txtDate.Attributes.Add("readonly", "readonly");
                    //txtDate3.Attributes.Add("readonly", "readonly");

                    if (Request.QueryString["Status"].ToString() == "reg")
                    {
                        divImg.Visible = false;
                        //txtDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                        //txtDate3.Text = DateTime.Today.ToString("dd-MM-yyyy");
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
                    //ddlState.DataSource = dt;
                    //ddlState.DataTextField = "STATE_Desc";
                    //ddlState.DataValueField = "STATE_CODE";
                    //ddlState.DataBind();
                    ddlState.Items.Insert(0, new ListItem("Select", string.Empty));

                    //ddlState1.DataSource = dt;
                    //ddlState1.DataTextField = "STATE_Desc";
                    //ddlState1.DataValueField = "STATE_CODE";
                    //ddlState1.DataBind();
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


        #region DROPDOWN 'ddlProofIdentity1' SELECTEDINDEXCHANGED EVENT
        protected void ddlProofIdentity1_SelectedIndexChanged(object sender, EventArgs e)
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
                if (ddlProofIdentity1.SelectedIndex == 0)
                {
                    divIdProof.Visible = false;

                }
                else if (ddlProofIdentity1.SelectedIndex == 1)
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
                else if (ddlProofIdentity1.SelectedIndex == 2)
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
                //else if (ddlProofIdentity1.SelectedIndex == 3)
                //{
                //    divIdProof.Visible = true;
                //    lblPassportNo.Text = "PAN Card";
                //    llPassExpDate.Visible = false;
                //    txtPassExpDate.Visible = false;
                //    hidePassExpDate.Visible = false;
                //    txtPassOthr.Visible = false;
                //    divPass.Visible = false;
                //    txtPassNo.Visible = true;
                //    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                //    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                //    txtPassNo.MaxLength = 10;
                //    //txtPassNo.Attributes.Remove("onblur");
                //    txtPassNo.Attributes.Add("onblur", "return fnValidatePAN(this)");
                //}
                else if (ddlProofIdentity1.SelectedIndex == 3)
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
                else if (ddlProofIdentity1.SelectedIndex == 4)
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
                else if (ddlProofIdentity1.SelectedIndex == 5)
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
                else if (ddlProofIdentity1.SelectedIndex == 6)
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
                else if (ddlProofIdentity1.SelectedIndex == 7)
                {

                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Document Name";
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
                    lblPassportNo.Text = "Document Name";
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "ddlProofIdentity1_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
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
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AddLoader('ddlProofOfAddressLoader')", true);
                txtPassOthrAdd.Visible = false;
                //txtPassNoAdd.Text = string.Empty;
                txtPassExpDateAdd.Text = string.Empty;
                txtPassNoAdd.Attributes.Add("onkeypress", "");
                txtPassNoAdd.Attributes.Add("style", "width:270px");
                //divAddProof1.Visible = true;
                if (ddlProofOfAddress.SelectedIndex == 0)
                {
                    divAddProof.Visible = false;
                    // divAddProof1.Visible=false;
                    txtPassNoAdd.Visible = false;
                }

                else if (ddlProofOfAddress.SelectedItem.Text == "Passport")
                {
                    //divAddNew.Visible = true;
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Passport Number";
                    txtPassOthrAdd.Visible = false;
                    txtPassNoAdd.Visible = true;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = false;
                    divPassAdd.Visible = false;
                    txtPassNoAdd.MaxLength = 8;
                    txtMaskCode.Visible = false;
                    txtMaskCode.Attributes.Add("width", "140px");
                    MaskCodeSpan.Attributes.Add("class", "");
                    txtPassNoAdd.Attributes.Remove("onblur");
                    txtPassNoAdd.Attributes.Add("onblur", "return ValidationPassport(this)");
                    //txtPassNo.Attributes.Add("onblur", "return ValidatePassport(" + txtPassNo.Text.Trim().ToString() + ")");
                }
                else if (ddlProofOfAddress.SelectedItem.Text == "Driving Licence")
                {
                    //divAddNew.Visible = true;
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Driving Licence";
                    txtPassOthrAdd.Visible = false;
                    txtPassNoAdd.Visible = true;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = false;
                    divPassAdd.Visible = false;
                    txtPassNoAdd.MaxLength = 15;
                    txtMaskCode.Visible = false;
                    txtMaskCode.Attributes.Add("width", "140px");
                    MaskCodeSpan.Attributes.Add("class", "");
                    txtPassNoAdd.Attributes.Remove("onblur");
                    txtPassNoAdd.Attributes.Add("onblur", "return ValidationDriving(this)");
                }
                else if (ddlProofOfAddress.SelectedItem.Text == "Proof of Possession of Aadhaar")
                {
                    //divAddNew.Visible = true;
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Proof of Possession of Aadhaar";
                    llPassExpDateAdd.Visible = false;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = false;
                    divPassAdd.Visible = false;
                    txtPassNoAdd.Visible = true;
                    txtPassNoAdd.MaxLength = 4;
                    //txtPassNoAdd.Text = "";
                    txtMaskCode.Visible = true;
                    txtMaskCode.Attributes.Add("style", "width:140px");
                    MaskCodeSpan.Attributes.Add("class", "input-group-addon input-group-addon-tel");
                    txtPassNoAdd.Attributes.Remove("onblur");
                    txtPassNoAdd.Attributes.Add("style", "");
                    txtPassNoAdd.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                    txtPassNoAdd.Attributes.Add("onkeypress", "fncInputNumericValuesOnly()");
                }
                else if (ddlProofOfAddress.SelectedItem.Text == "Voter ID Card")
                {
                    //divAddNew.Visible = true;
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Voter ID Card";
                    llPassExpDateAdd.Visible = false;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = false;
                    divPassAdd.Visible = false;
                    txtPassNoAdd.Visible = true;
                    txtMaskCode.Visible = false;
                    txtMaskCode.Attributes.Add("width", "140px");
                    MaskCodeSpan.Attributes.Add("class", "");
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd.MaxLength = 15;
                    txtPassNoAdd.Attributes.Remove("onblur");
                    txtPassNoAdd.Attributes.Add("onblur", "return ValidationVoterId(this)");
                }
                else if (ddlProofOfAddress.SelectedItem.Text == "NREGA Job Card")
                {
                    //divAddNew.Visible = true;
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "NREGA Job Card";
                    llPassExpDateAdd.Visible = false;
                    txtPassNoAdd.Visible = true;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;
                    txtMaskCode.Visible = false;
                    txtMaskCode.Attributes.Add("width", "140px");
                    MaskCodeSpan.Attributes.Add("class", "");
                    txtPassOthrAdd.Visible = false;
                    divPassAdd.Visible = false;
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd.MaxLength = 20;
                    txtPassNoAdd.Attributes.Remove("onblur");
                }
                else if (ddlProofOfAddress.SelectedItem.Text == "National Population Register Letter")
                {
                    divAddProof.Visible = true;
                    //lblPassportNoAdd.Text = "Document Name";
                    //llPassExpDateAdd.Text = "Identification Number";
                    //txtPassExpDateAdd.Visible = true;
                    llPassExpDateAdd.Visible = false;
                    divPassAdd.Visible = true;
                    //llPassExpDateAdd.Visible = true;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = true;
                    txtPassNoAdd.Visible = true;
                    txtMaskCode.Visible = false;
                    txtMaskCode.Attributes.Add("width", "140px");
                    MaskCodeSpan.Attributes.Add("class", "");
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd.MaxLength = 20;
                    txtPassNoAdd.Attributes.Remove("onblur");
                }
                else if (ddlProofOfAddress.SelectedItem.Text == "E-KYC Authentication")
                {
                    divAddProof.Visible = true;
                    //lblPassportNoAdd.Text = "Document Name";
                    //llPassExpDateAdd.Text = "Identification Number";
                    //txtPassExpDateAdd.Visible = true;
                    llPassExpDateAdd.Visible = false;
                    divPassAdd.Visible = true;
                    //llPassExpDateAdd.Visible = true;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = true;
                    txtPassNoAdd.Visible = true;
                    txtMaskCode.Visible = true;
                    txtMaskCode.Attributes.Add("style", "width:140px");
                    MaskCodeSpan.Attributes.Add("class", "input-group-addon input-group-addon-tel");
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    txtPassNoAdd.Attributes.Add("style", "");
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd.MaxLength = 4;
                    txtPassNoAdd.Attributes.Remove("onblur");
                    txtPassNoAdd.Attributes.Add("onblur", "return fnValidateEkyc(this)");
                    txtPassNoAdd.Attributes.Add("onkeypress", "fncInputNumericValuesOnly()");
                }
                else if (ddlProofOfAddress.SelectedItem.Text == "Offline Verification of Aadhaar" || ddlProofOfAddress.SelectedItem.Text == "Offline verification of Aadhaar")
                {
                    divAddProof.Visible = true;
                    llPassExpDateAdd.Visible = false;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = false;
                    divPassAdd.Visible = false;
                    txtPassNoAdd.Visible = true;
                    //txtPassNoAdd.Text = "";
                    txtMaskCode.Visible = true;
                    //divAddProof.Visible = true;
                    // lblPassportNoAdd.Text = "Document Name";
                    // llPassExpDateAdd.Text = "Identification Number";
                    //txtPassExpDateAdd.Visible = true;
                    //llPassExpDateAdd.Visible = true;
                    divPassAdd.Visible = true;
                    // llPassExpDateAdd.Visible = true;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = true;
                    txtPassNoAdd.Visible = true;
                    txtMaskCode.Visible = true;
                    txtPassNoAdd.Attributes.Remove("onblur");
                    txtMaskCode.Attributes.Add("style", "width:140px");
                    MaskCodeSpan.Attributes.Add("class", "input-group-addon input-group-addon-tel");
                    txtPassNoAdd.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                    txtPassNoAdd.Attributes.Add("onkeypress", "fncInputNumericValuesOnly()");
                    txtPassNoAdd.Attributes.Add("style", "");
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd.MaxLength = 4;
                }
                else
                {
                    //divAddProof.Visible = true;
                    //lblPassportNoAdd.Text = "Document Name";
                    //llPassExpDateAdd.Text = "Identification Number";
                    //txtPassExpDateAdd.Visible = true;
                    llPassExpDateAdd.Visible = false;
                    divPassAdd.Visible = true;
                    //llPassExpDateAdd.Visible = true;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = true;
                    txtPassNoAdd.Visible = true;
                    txtMaskCode.Visible = false;
                    txtMaskCode.Attributes.Add("width", "140px");
                    MaskCodeSpan.Attributes.Add("class", "");
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd.MaxLength = 15;
                    txtPassNoAdd.Attributes.Remove("onblur");
                }
                lblPassportNoAdd.Text = "Document Number";
                llPassExpDateAdd.Visible = false;
                txtPassOthrAdd.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "RemoveLoader('ddlProofOfAddressLoader')", true);
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
                if (txtKYCNum.Text == "")
                {
                    if (cboTitle.SelectedIndex != 0)
                    {
                        subPopulateGender();
                        if (cboTitle.SelectedValue == "MR")
                        {
                            cboGender.SelectedValue = "M";
                            rbtFS.Enabled = true;
                            rbtFS.ClearSelection();
                            cboTitle1.Enabled = false;
                            txtGivenName1.Enabled = false;
                            txtMiddleName1.Enabled = false;
                            txtLastName1.Enabled = false;
                        }
                        else if (cboTitle.SelectedValue == "MS")
                        {
                            cboGender.SelectedValue = "F";
                            rbtFS.SelectedValue = "F";
                            rbtFS.Enabled = false;
                            cboTitle1.Enabled = false;
                            txtGivenName1.Enabled = false;
                            txtMiddleName1.Enabled = false;
                            txtLastName1.Enabled = false;
                        }
                        else if (cboTitle.SelectedValue == "MRS")
                        {
                            cboGender.SelectedValue = "F";
                            rbtFS.Enabled = true;
                            rbtFS.ClearSelection();
                            cboTitle1.Enabled = true;
                            txtGivenName1.Enabled = true;
                            txtMiddleName1.Enabled = true;
                            txtLastName1.Enabled = true;
                        }
                        else
                        {
                            cboGender.SelectedValue = "";
                            rbtFS.Enabled = true;
                            cboTitle1.Enabled = true;
                            txtGivenName1.Enabled = true;
                            txtMiddleName1.Enabled = true;
                            txtLastName1.Enabled = true;
                        }
                    }
                    else
                    {
                        htParam.Clear();
                        htParam.Add("@Flag", "Gender");
                        htParam.Add("@value", cboTitle.SelectedValue);
                        FillDropdowns("prc_FillMarGender", htParam, cboGender, "CKYCConnectionString", true);

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
                cboGender.SelectedValue = Convert.ToString(dt.Rows[0]["GENDER"]);
                ddlIsoCountryCodeOthr.SelectedValue = Convert.ToString(dt.Rows[0]["ISO_COUNTRYCODE"]);
                ddlProofOfAddress.SelectedValue = Convert.ToString(dt.Rows[0]["IdType"]);
                ViewState["strIdName"] = Convert.ToString(dt.Rows[0]["IdName"]);
                ViewState["strIdNumber"] = Convert.ToString(dt.Rows[0]["IdNumber"]);
                ViewState["strIdExpDate"] = Convert.ToString(dt.Rows[0]["IdExpDate"]);
                if (ddlProofIdentity1.SelectedValue == "Z")
                {
                    txtPassOthr.Text = Convert.ToString(dt.Rows[0]["IdNumber"]);
                    txtPassNo.Text = Convert.ToString(dt.Rows[0]["IdName"]);
                }
                else
                {
                    txtPassNo.Text = Convert.ToString(dt.Rows[0]["IdNumber"]);
                    txtPassExpDate.Text = Convert.ToString(dt.Rows[0]["IdExpDate"]);
                }
                //Commented by Kalyani Hande start
                //if (Convert.ToString(dt.Rows[0]["CnctType1"]) == "P1")
                //{
                //    chkPerAddress.Checked = true;
                //}
                //else
                //{
                //    chkPerAddress.Checked = false;
                //}
                //Commented by Kalyani Hande end
                //ddlAddressType.SelectedValue = Convert.ToString(dt.Rows[0]["PER_ADDTYPE"]);
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
                //if (Convert.ToString(dt.Rows[0]["kycCertDoc"]) == "01")
                //{
                //    chkSelfCerti.Checked = true;
                //}
                //else
                //{
                //    chkSelfCerti.Checked = false;
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


        #region PopulateProofIdentiy
        private void PopulateProofIdentiy()
        {
            try
            {
                oCommonUtility.GetCKYC(ddlProofIdentity1, "KId");
                ddlProofIdentity1.Items.Insert(0, new ListItem("Select", ""));
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

        #region PopulateAddressProofType
        private void PopulateAddressProofType()
        {
            try
            {
                htParam.Clear();
                htParam.Add("@LookupCode", "KAddrPrfRP");
                FillDropdowns("prc_getDDLLookUpData", htParam, ddlProofOfAddress, "CKYCConnectionString", true);

                //oCommonUtility.GetCKYC(ddlProofOfAddress, "KId");//KAddrPrf
                //ddlProofOfAddress.Items.Insert(0, new ListItem("Select", ""));
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
                    ddlProofIdentity1, txtPassNo, txtPassExpDate, txtPassOthr,
                    chkAppDeclare1, ddlProofOfAddress, txtAddressLine1, txtCity, txtPinCode, txtDate3, txtDate,
                    txtEmpName, txtEmpCode, txtEmpDesignation, txtEmpBranch, txtInsName, txtInsCode, txtPassExpDateAdd, txtPassNoAdd, txtPlace, ddlDocReceived,
                    ddlState, txtPassOthrAdd, txtLocAddLine1, txtCity1, ddlState1,
                    ddlPinCode1, ddlCountryCode1, ddlProofOfAddress1, txtPassNoAdd1, txtRelRefNumber, ddlPinCode, chkCuurentAddress, ddlPinCode01, ddlNationality, txtNum,
                    txtTelOff2, txtTelOff, txtTelRes, txtTelRes2, txtMobile, txtMobile2, txtemail, ddlCountryCode, FlagPageTyp);//chkHigh, chkMedium, chkLow,ddlProofOfAddress1,
                    #region Commented
                    //Commented by Kalyani Hande start
                    //if (!chkPerAddress.Checked)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please check current/permanent/overseas address details')", true);
                    //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please check CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS')", true);
                    //    return;
                    //}
                    //else
                    //{
                    //    if ((txtTelOff.Text.Trim() == "" && txtTelOff2.Text.Trim() != "") || (txtTelOff.Text.Trim() != "" && txtTelOff2.Text.Trim() == ""))
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Office STD code and Telephone number is mandatory')", true);
                    //        return;
                    //    }

                    //    if ((txtTelRes.Text.Trim() == "" && txtTelRes2.Text.Trim() != "") || (txtTelRes.Text.Trim() != "" && txtTelRes2.Text.Trim() == ""))
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Resident STD code and Telephone number is mandatory')", true);
                    //        return;
                    //    }

                    //    if ((txtMobile.Text.Trim() == "" && txtMobile2.Text.Trim() != "") || (txtMobile.Text.Trim() != "" && txtMobile2.Text.Trim() == ""))
                    //       {
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Mobile ISD code and mobile number is mandatory')", true);
                    //        return;
                    //    }

                    //}
                    //Commented by Kalyani Hande end
                    #endregion
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
                        if (Request.QueryString["RowNo"] != null)
                        {
                            RowIndex = Request.QueryString["RowNo"].ToString();
                        }
                        if (RowIndex != "" && Session["dsRel"] != null)
                        {
                            for (int i = 0; i < DtAdd.Rows.Count; i++)
                            {
                                DataRow dr = DtAdd.Rows[i];
                                if (i == Convert.ToInt32(RowIndex))
                                { dr.Delete(); RowIndex = ""; }
                            }
                        }

                        DataRow dataRow = DtAdd.NewRow();
                        dataRow["FiRefNo"] = txtRefNumber.Text.Trim();
                        dataRow["RelRefNo"] = txtRelRefNumber.Text.Trim();
                        dataRow["RelatedPrsnKYCNo"] = txtKYCNum.Text.Trim();
                        dataRow["RelationType"] = ddlRelType.SelectedValue;


                        dataRow["RelPersonAs"] = "I";

                        dataRow["RelEntName"] = System.DBNull.Value;
                        dataRow["RelDtofIncorporation"] = System.DBNull.Value;
                        dataRow["RelDtofCommencementofbusi"] = System.DBNull.Value;
                        dataRow["RelPlaceofIncorportation"] = System.DBNull.Value;
                        dataRow["RelCountryofIncorporation"] = System.DBNull.Value;
                        dataRow["RelCountryofResAsperTaxLaws"] = System.DBNull.Value;
                        dataRow["RelIdType"] = System.DBNull.Value;
                        dataRow["RelPAN"] = System.DBNull.Value;
                        dataRow["RelTINIdNo"] = System.DBNull.Value;
                        dataRow["RelTINCountry"] = System.DBNull.Value;

                        dataRow["RelTelSTDCode"] = System.DBNull.Value;
                        dataRow["RelTelNo"] = System.DBNull.Value;
                        dataRow["RelOfficeTelSTDCode"] = System.DBNull.Value;
                        dataRow["RelOfficeTelNo"] = System.DBNull.Value;
                        dataRow["RelMobCode"] = System.DBNull.Value;
                        dataRow["RelMobileNo"] = System.DBNull.Value;
                        dataRow["RelFaxNoCode"] = System.DBNull.Value;
                        dataRow["RelFaxNo"] = System.DBNull.Value;
                        dataRow["RelEmailID"] = System.DBNull.Value;


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
                        //dataRow["MaritalStatusRel"] = "";
                        //dataRow["CitizenshipRel"] = "";
                        //dataRow["ResiStatusRel"] = "";

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

                        if (ddlNationality.SelectedIndex != 0)
                        {
                            dataRow["CitizenshipReltxt"] = ddlNationality.SelectedItem.Text;
                            dataRow["CitizenshipRel"] = ddlNationality.SelectedValue;
                        }
                        else
                        {
                            dataRow["CitizenshipReltxt"] = "";
                        }


                        dataRow["MaritalStatusReltxt"] = "";
                        //dataRow["MaritalStatusReltxt"] = "";

                        //dataRow["CitizenshipReltxt"] = "";
                        ////dataRow["CitizenshipReltxt"] = "";


                        dataRow["ResiStatusReltxt"] = "";
                        //dataRow["ResiStatusReltxt"] ="";


                        dataRow["OccuTypeReltxt"] = "";
                        //dataRow["OccuTypeReltxt"] = ddlOccuSubType.SelectedItem.Text;

                        //---end to show on grid

                        dataRow["OccuTypeRel"] = "";

                        dataRow["OccuSubTypeRel"] = "";



                        dataRow["ResForTaxFlagRel"] = "";
                        dataRow["ISOCountryCodeRel"] = "";
                        dataRow["TaxIDNumberRel"] = "";
                        dataRow["BirthCityRel"] = "";
                        dataRow["ISOBirthPlaceCodeRel"] = "";
                        dataRow["IdType"] = ddlProofOfAddress.SelectedValue;
                        if (ddlProofOfAddress.SelectedIndex == 1)
                        {
                            dataRow["IdNumber"] = common.ChkInput_AddMaskingVal(ddlProofOfAddress.SelectedItem.Text.ToString(), txtPassNoAdd.Text.Trim());
                            dataRow["IdExpDate"] = txtPassExpDate.Text.Trim();
                            dataRow["IdName"] = ddlProofOfAddress.SelectedItem.Text.ToString();

                        }
                        else if (ddlProofOfAddress.SelectedIndex == 2)
                        {
                            dataRow["IdNumber"] = common.ChkInput_AddMaskingVal(ddlProofOfAddress.SelectedItem.Text.ToString(), txtPassNoAdd.Text.Trim());
                            dataRow["IdExpDate"] = System.DBNull.Value;
                            dataRow["IdName"] = ddlProofOfAddress.SelectedItem.Text.ToString();

                        }
                        else if (ddlProofOfAddress.SelectedIndex == 3)
                        {
                            dataRow["IdNumber"] = common.ChkInput_AddMaskingVal(ddlProofOfAddress.SelectedItem.Text.ToString(), txtPassNoAdd.Text.Trim());
                            dataRow["IdExpDate"] = System.DBNull.Value;
                            dataRow["IdName"] = ddlProofOfAddress.SelectedItem.Text.ToString();
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 4)
                        {
                            dataRow["IdNumber"] = common.ChkInput_AddMaskingVal(ddlProofOfAddress.SelectedItem.Text.ToString(), txtPassNoAdd.Text.Trim());
                            dataRow["IdExpDate"] = txtPassExpDate.Text.Trim();
                            dataRow["IdName"] = ddlProofOfAddress.SelectedItem.Text.ToString();
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 5)
                        {
                            dataRow["IdNumber"] = common.ChkInput_AddMaskingVal(ddlProofOfAddress.SelectedItem.Text.ToString(), txtPassNoAdd.Text.Trim());
                            dataRow["IdExpDate"] = System.DBNull.Value;
                            dataRow["IdName"] = ddlProofOfAddress.SelectedItem.Text.ToString();
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 6)
                        {
                            dataRow["IdNumber"] = common.ChkInput_AddMaskingVal(ddlProofOfAddress.SelectedItem.Text.ToString(), txtPassNoAdd.Text.Trim());
                            dataRow["IdExpDate"] = System.DBNull.Value;
                            dataRow["IdName"] = ddlProofOfAddress.SelectedItem.Text.ToString();
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 7)
                        {
                            dataRow["IdNumber"] = common.ChkInput_AddMaskingVal(ddlProofOfAddress.SelectedItem.Text.ToString(), txtPassNoAdd.Text.Trim());
                            dataRow["IdExpDate"] = System.DBNull.Value;
                            dataRow["IdName"] = ddlProofOfAddress.SelectedItem.Text.ToString();
                        }
                        else if (ddlProofOfAddress.SelectedIndex == 8 || ddlProofOfAddress.SelectedIndex == 9)
                        {
                            dataRow["IdNumber"] = common.ChkInput_AddMaskingVal(ddlProofOfAddress.SelectedItem.Text.ToString(), txtPassNoAdd.Text.Trim());
                            dataRow["IdExpDate"] = System.DBNull.Value;
                            dataRow["IdName"] = ddlProofOfAddress.SelectedItem.Text.ToString();
                        }
                        else
                        {
                            dataRow["IdNumber"] = System.DBNull.Value;
                            dataRow["IdExpDate"] = System.DBNull.Value;
                            dataRow["IdName"] = ddlProofOfAddress.SelectedItem.Text.ToString();
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

                        //Commented by Kalyani Hande start
                        //if (chkPerAddress.Checked == true)
                        //{
                        dataRow["CnctTypeRel"] = "P1";
                        //dataRow["AdrTypeRel"] = "";
                        dataRow["AdrProfRel"] = ddlProofOfAddress.SelectedValue.Trim();
                        dataRow["Adr1Rel"] = txtAddressLine1.Text.Trim();
                        dataRow["Adr2Rel"] = txtAddressLine2.Text.Trim();
                        dataRow["Adr3Rel"] = txtAddressLine3.Text.Trim();
                        dataRow["CityRel"] = txtCity.Text.Trim();
                        //dataRow["DistrictRel"] = ddlDistrictname.SelectedItem.Text;
                        //dataRow["PostCodeRel"] = ddlPinCode.SelectedValue.ToString();

                        if (ddlCountryCode.SelectedValue == "IN")
                        {
                            //htParam.Add("@PER_STATECODE", ddlState.SelectedValue);
                            dataRow["StateCodeRel"] = ddlState.SelectedItem.Text;
                            dataRow["DistrictRel"] = ddlDistrictname.SelectedItem.Text;
                            dataRow["PostCodeRel"] = ddlPinCode.SelectedValue.ToString();
                        }
                        else
                        {
                            //htParam.Add("@PER_STATECODE", txtState.Text.Trim());
                            dataRow["StateCodeRel"] = txtddlState.Text.Trim();
                            dataRow["DistrictRel"] = txtddlDistrictname.Text;
                            dataRow["PostCodeRel"] = txtddlPinCode.Text;
                        }

                        //    //dataRow["StateCodeRel"] = ddlState.SelectedValue;
                        dataRow["CntryCodeRel"] = ddlCountryCode.SelectedValue;
                        //}
                        //else
                        //{
                        //    dataRow["CnctTypeRel"] = "";
                        //    dataRow["AdrTypeRel"] = System.DBNull.Value;
                        //    dataRow["AdrProfRel"] = System.DBNull.Value;
                        //    dataRow["Adr1Rel"] = System.DBNull.Value;
                        //    dataRow["Adr2Rel"] = System.DBNull.Value;
                        //    dataRow["Adr3Rel"] = System.DBNull.Value;
                        //    dataRow["CityRel"] = System.DBNull.Value;
                        //    dataRow["DistrictRel"] = System.DBNull.Value;
                        //    dataRow["PostCodeRel"] = System.DBNull.Value;
                        //    dataRow["StateCodeRel"] = System.DBNull.Value;
                        //    dataRow["CntryCodeRel"] = System.DBNull.Value;
                        //}
                        //Commented by Kalyani Hande end

                        if (chkCuurentAddress.Checked == true)
                        {
                            dataRow["SameasCurrentAddresFlagM1"] = "01";
                            //htParam.Add("@SameasCurrentAddresFlagM1", "01");
                        }
                        else
                        {
                            dataRow["SameasCurrentAddresFlagM1"] = "";
                        }

                        //Commented by kalyani Hande start
                        //if (chkLocalAddress.Checked == true)
                        //{
                        dataRow["CnctTypeRel1"] = "M1";
                        //dataRow["AdrTypeRel1"] = ddlAddressType1.SelectedValue.Trim();
                        //dataRow["AdrProfRel1"] = ddlProofOfAddress1.SelectedValue;
                        dataRow["Adr1Rel1"] = txtLocAddLine1.Text.Trim();
                        dataRow["Adr2Rel1"] = txtLocAddLine2.Text.Trim();
                        dataRow["Adr3Rel1"] = txtLocAddLine3.Text.Trim();
                        dataRow["CityRel1"] = txtCity1.Text.Trim();
                        //dataRow["DistrictRel1"] = ddlDistrict1.SelectedItem.Text;
                        //dataRow["PostCodeRel1"] = ddlPinCode01.SelectedValue;

                        if (ddlCountryCode1.SelectedValue == "IN")
                        {
                            //htParam.Add("@PER_STATECODE", ddlState.SelectedValue);
                            dataRow["StateCodeRel1"] = ddlState1.SelectedItem.Text;
                            dataRow["DistrictRel1"] = ddlDistrict1.SelectedItem.Text;
                            dataRow["PostCodeRel1"] = ddlPinCode01.SelectedValue;
                        }
                        else
                        {
                            //htParam.Add("@PER_STATECODE", txtState.Text.Trim());
                            dataRow["StateCodeRel1"] = txtddlState1.Text.Trim();
                            dataRow["DistrictRel1"] = txtddlDistrict1.Text;
                            dataRow["PostCodeRel1"] = txtddlPinCode01.Text;
                        }

                        //    //dataRow["StateCodeRel1"] = ddlState1.SelectedValue;
                        dataRow["CntryCodeRel1"] = ddlCountryCode1.SelectedValue;
                        //}
                        //else
                        //{
                        //    dataRow["CnctTypeRel1"] = "";
                        //    dataRow["AdrTypeRel1"] = System.DBNull.Value;
                        //    //dataRow["AdrProfRel1"] = System.DBNull.Value;
                        //    dataRow["Adr1Rel1"] = System.DBNull.Value;
                        //    dataRow["Adr2Rel1"] = System.DBNull.Value;
                        //    dataRow["Adr3Rel1"] = System.DBNull.Value;
                        //    dataRow["CityRel1"] = System.DBNull.Value;
                        //    dataRow["DistrictRel1"] = System.DBNull.Value;
                        //    dataRow["PostCodeRel1"] = System.DBNull.Value;
                        //    dataRow["StateCodeRel1"] = System.DBNull.Value;
                        //    dataRow["CntryCodeRel1"] = System.DBNull.Value;
                        //}
                        //Commented by kalyani Hande end


                        dataRow["SameasLocalAddressFlagJ1"] = "";

                        dataRow["SameasLocalAddressFlagJ2"] = "";

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

                        dataRow["AddIdTypeRel"] = ddlProofOfAddress1.SelectedValue.Trim();
                        dataRow["AddIdNameRel"] = ddlProofOfAddress1.SelectedItem.Text.ToString();
                        dataRow["AddIdNumberRel"] = common.ChkInput_AddMaskingVal(ddlProofOfAddress1.SelectedItem.Text.ToString(), txtPassNoAdd1.Text.Trim());

                        //Commented by Kalyani Hande start
                        //if (chkPerAddress.Checked == true)
                        //{
                        //    if (ddlProofOfAddress.SelectedIndex == 1)
                        //    {
                        //        dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                        //        dataRow["AddIdExpDateRel"] = txtPassExpDateAdd.Text.Trim();
                        //        dataRow["AddIdNameRel"] = System.DBNull.Value;
                        //    }

                        //    else if (ddlProofOfAddress.SelectedIndex == 2)
                        //    {
                        //        dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                        //        dataRow["AddIdExpDateRel"] = txtPassExpDateAdd.Text.Trim();
                        //        dataRow["AddIdNameRel"] = System.DBNull.Value;
                        //    }
                        //    else if (ddlProofOfAddress.SelectedIndex == 3)
                        //    {
                        //        dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                        //        dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                        //        dataRow["AddIdNameRel"] = System.DBNull.Value;
                        //    }
                        //    else if (ddlProofOfAddress.SelectedIndex == 4)
                        //    {
                        //        dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                        //        dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                        //        dataRow["AddIdNameRel"] = System.DBNull.Value;
                        //    }
                        //    else if (ddlProofOfAddress.SelectedIndex == 5)
                        //    {
                        //        dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                        //        dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                        //        dataRow["AddIdNameRel"] = System.DBNull.Value;
                        //    }
                        //    else if (ddlProofOfAddress.SelectedIndex == 6)
                        //    {
                        //        dataRow["AddIdNumberRel"] = txtPassOthrAdd.Text.Trim();
                        //        dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                        //        dataRow["AddIdNameRel"] = txtPassNoAdd.Text.Trim();
                        //    }

                        //    else
                        //    {
                        //        dataRow["AddIdNumberRel"] = System.DBNull.Value;
                        //        dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                        //        dataRow["AddIdNameRel"] = System.DBNull.Value;
                        //    }
                        //}
                        //else
                        //{
                        //    dataRow["AddIdNumberRel"] = System.DBNull.Value;
                        //    dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                        //    dataRow["AddIdNameRel"] = System.DBNull.Value;
                        //}
                        //Commented by Kalyani Hande end

                        dataRow["DecDateRel"] = txtDate.Text.Trim();
                        dataRow["DecPlaceRel"] = txtPlace.Text.Trim();
                        dataRow["kycEmpNameRel"] = txtEmpName.Text.Trim();
                        dataRow["kycEmpCodeRel"] = txtEmpCode.Text.Trim();
                        dataRow["kycEmpBranchRel"] = txtEmpBranch.Text.Trim();
                        dataRow["kycEmpDesiRel"] = txtEmpDesignation.Text.Trim();
                        dataRow["kycVerDateRel"] = txtDate3.Text.Trim();

                        dataRow["kycCertDocRel"] = ddlDocReceived.SelectedValue;
                        dataRow["kycInstNameRel"] = txtInsName.Text.Trim();
                        dataRow["kycInstCodeRel"] = txtInsCode.Text.Trim();
                        dataRow["SVFlag"] = "P";

                        //Added by tushar
                        dataRow["PanNo"] = (txtPanNo.Text == "Applied For" ? (chkPanForm.Checked == true ? "Y" : "N") : txtPanNo.Text.Trim());// Added by Shubham txtPanNo.Text.Trim();
                        dataRow["TelCtrCodeOff"] = txtTelOff.Text.Trim();
                        dataRow["TelOff"] = txtTelOff2.Text.Trim();
                        dataRow["TelCtrCodeRes"] = txtTelRes.Text.Trim();
                        dataRow["TelRes"] = txtTelRes2.Text.Trim();
                        dataRow["MobileCtrCode"] = txtMobile.Text.Trim();
                        dataRow["Mobile"] = txtMobile2.Text.Trim();
                        dataRow["Email"] = txtemail.Text.Trim();
                        dataRow["Remarks"] = txtRemarks.Text.Trim();
                        //Added by tushar

                        //AddedControl By Shubham
                        if (ddlRelType.SelectedItem.Text == "Director")
                        {
                            dataRow["AddIdNameRel"] = txtNum.Text.Trim();
                            // dataRow["RelPersTypDesc"] = txtNum.Text.Trim(); 
                        }
                        else if (ddlRelType.SelectedItem.Text == "Other")
                        {
                            //dataRow["AddIdNameRel"] = txtNum.Text.Trim();
                            dataRow["RelPersTypDesc"] = txtNum.Text.Trim();
                        }
                        else
                        { //dataRow["AddIdNameRel"] = "";
                        }
                        //dataRow["ProofOfAddress"] = ddlProofOfAddress.SelectedValue;
                        //dataRow["PassNoAdd"] = txtPassNoAdd.Text.Trim();
                        //dataRow["ProofOfAddress1"] = ddlProofOfAddress1.SelectedValue;
                        //dataRow["PassNoAdd1"] = txtPassNoAdd1.Text.Trim();
                        //AddedControl By Shubham
                        //dataRow["CREATEDBY"] =strUserId.ToString();

                        DtAdd.Rows.Add(dataRow);
                        //DataSet dsRel1 = new DataSet();
                        //dsRel1.Clear();
                        //dsRel1.Tables.Add(DtAdd);
                        Session["dsRel"] = DtAdd;
                        lblMsgConfirmYesNo.Text = "Relative Details added successfully....Do you want to add more?.";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "AsyncConfirmYesNo();", true);
                        ClearTextcntrl();
                        ddlRelType_SelectedIndexChanged(this, e);
                        //GetRelRefNo();
                        Session["PSSubmit"] = "N";
                        rbtFS.Items[0].Selected = false;
                        rbtFS.Items[1].Selected = false;
                        txtPanNo.Text = "";
                        txtPanNo.Enabled = true;
                        chkPanForm.Checked = false;
                        ddlProofOfAddress.SelectedIndex = 0;
                        ddlProofOfAddress_SelectedIndexChanged(this, e);
                        ddlProofOfAddress1.Items.Insert(0, new ListItem("Select", string.Empty));
                        ddlProofOfAddress1.SelectedIndex = 0;
                        ddlProofOfAddress1_SelectedIndexChanged(this, e);
                        ddlDistrict1.SelectedIndex = 0;
                        ddlDistrictname.SelectedIndex = 0;
                        ddlCountryCode.SelectedIndex = 0;
                        ddlCountryCode1.SelectedIndex = 0;
                        ddlDistrict1.Enabled = true;
                        ddlDistrict1.Items.Insert(0, new ListItem("Select", string.Empty));
                        ddlPinCode01.SelectedIndex = 0;
                        ddlPinCode01.Enabled = true;
                        txtTelOff.Text = "";
                        txtTelOff2.Text = "";
                        txtTelRes.Text = "";
                        txtTelRes2.Text = "";
                        txtMobile.Text = "";
                        txtMobile2.Text = "";
                        txtemail.Text = "";

                        txtRemarks.Text = "";
                        txtDate.Text = "";
                        txtPlace.Text = "";
                        ddlDocReceived.SelectedIndex = 0;
                        txtDate3.Text = "";
                        //txtDate3.Text = DateTime.Today.ToString("dd-MM-yyyy");
                        txtEmpName.Text = "";
                        txtEmpCode.Text = "";
                        txtEmpDesignation.Text = "";
                        txtEmpBranch.Text = "";
                        txtInsCode.Text = "";
                        txtInsName.Text = "";
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
                   ddlProofIdentity1, txtPassNo, txtPassExpDate, txtPassOthr,
                     chkAppDeclare1, ddlProofOfAddress, txtAddressLine1, txtCity, txtPinCode, txtDate3, txtDate,
                    txtEmpName, txtEmpCode, txtEmpDesignation, txtEmpBranch, txtInsName, txtInsCode, txtPassExpDateAdd, txtPassNoAdd, txtPlace,
                    ddlDocReceived, ddlState, txtPassOthrAdd, txtNum);//chkHigh, chkMedium, chkLow,


                    if (Res1.Equals(""))
                    {
                        //if (txtPanNo.Text == "")
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please Enter Pancard number')", true);
                        //    txtPanNo.Focus();
                        //    return;
                        //}


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
                        //AddedControl By Shubham
                        if (ddlRelType.SelectedItem.Text == "Director")
                        {
                            dataRow["AddIdNameRel"] = txtNum.Text.Trim();
                            // dataRow["RelPersTypDesc"] = txtNum.Text.Trim(); 
                        }
                        else if (ddlRelType.SelectedItem.Text == "Other")
                        {
                            //dataRow["AddIdNameRel"] = txtNum.Text.Trim();
                            dataRow["RelPersTypDesc"] = txtNum.Text.Trim();
                        }
                        else
                        { dataRow["AddIdNameRel"] = ""; }
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
                        dataRow["DOBRel"] = txtDOB.Text;
                        //dataRow["GenderRel"] = cboGender.SelectedValue;
                        //dataRow["MaritalStatusRel"] = "";
                        //dataRow["CitizenshipRel"] = "";
                        //dataRow["ResiStatusRel"] = "";

                        ////---to show on grid
                        dataRow["RelationTypetxt"] = ddlRelType.SelectedItem.Text;

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
                        //dataRow["TaxIDNumberRel"] = "";
                        //dataRow["BirthCityRel"] = "";
                        //dataRow["ISOBirthPlaceCodeRel"] = "";
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
                        dataRow["kycCertDocRel"] = ddlDocReceived.SelectedValue;
                        dataRow["kycInstNameRel"] = txtInsName.Text.Trim();
                        dataRow["kycInstCodeRel"] = txtInsCode.Text.Trim();
                        dataRow["kycVerDateRel"] = txtDate3.Text.Trim();
                        dataRow["kycEmpNameRel"] = txtEmpName.Text.Trim();
                        dataRow["kycEmpCodeRel"] = txtEmpCode.Text.Trim();
                        dataRow["kycEmpBranchRel"] = txtEmpBranch.Text.Trim();
                        dataRow["kycEmpDesiRel"] = txtEmpDesignation.Text.Trim();
                        DtAdd.Rows.Add(dataRow);
                        Session["dsRel"] = DtAdd;
                        lblMsgConfirmYesNo.Text = "Related Person Details added successfully....Do you want to add more?.";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "AsyncConfirmYesNo();", true);
                        btnAdd.Enabled = true;
                        ClearTextcntrl();
                        ddlRelType_SelectedIndexChanged(this, e);
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


                    ddlCountryCode.DataSource = dt;
                    ddlCountryCode.DataTextField = "Country_Desc";
                    ddlCountryCode.DataValueField = "Country_CODE";
                    ddlCountryCode.DataBind();
                    ddlCountryCode.Items.Insert(0, new ListItem("Select", string.Empty));


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
                htParam.Clear();
                htParam.Add("@USERID", strUserId.Trim());
                DataTable dtEntDetails = new DataAccessLayer().GetDataTable("Prc_GetEntitySetup", htParam, "DefaultConn");
                if (dtEntDetails.Rows.Count > 0)
                {
                    txtInsName.Text = Convert.ToString(dtEntDetails.Rows[0]["EntityName"]);
                    txtInsCode.Text = Convert.ToString(dtEntDetails.Rows[0]["InstitutionCode"]);
                }
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
                    // txtDate3.Text = Convert.ToString(dt.Rows[0]["kycVerDate"]);

                    txtEmpName.Enabled = false;
                    txtEmpCode.Enabled = false;
                    txtEmpDesignation.Enabled = false;
                    txtEmpBranch.Enabled = false;
                    txtInsName.Enabled = false;
                    txtInsCode.Enabled = false;
                    // txtDate3.Enabled = false;
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
                DisableControls();
                txtKYCNum.Enabled = false;
                ddlRelType.Enabled = false;
                cboTitle.Enabled = false;
                txtGivenName.Enabled = false;
                txtMiddleName.Enabled = false;
                txtLastName.Enabled = false;
                txtDate.Text = "";
                txtDate3.Text = "";
                txtPassNoAdd1.Enabled = false;
                txtPassNoAdd.Enabled = false;
                txtNum.Enabled = false;
                txtDOB.Enabled = false;
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
                //Commented by Kalyani Hande start
                //if (chkPerAddress.Checked == false)
                //{
                //    chkPerAddress.Enabled = true;
                //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Check CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS')", true);
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please check current/permanent/overseas address details')", true);
                //    chkPerAddress.Focus();
                //    return;
                //}
                //else
                //{
                //    chkPerAddress.Enabled = false;
                //}
                //Commented by Kalyani Hande end
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "FillSubOccuType", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
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
                if (FlagPageTyp == "Legal")
                {
                    htParam.Add("@LookupCode", "KEntRelative");
                }
                else
                {
                    htParam.Add("@LookupCode", "KRelative");
                }
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

                DtAdd.Columns.Add("RelPersonAs");
                DtAdd.Columns.Add("RelEntName");
                DtAdd.Columns.Add("RelDtofIncorporation");
                DtAdd.Columns.Add("RelDtofCommencementofbusi");
                DtAdd.Columns.Add("RelPlaceofIncorportation");
                DtAdd.Columns.Add("RelCountryofIncorporation");
                DtAdd.Columns.Add("RelCountryofResAsperTaxLaws");
                DtAdd.Columns.Add("RelIdType");
                DtAdd.Columns.Add("RelPAN");
                DtAdd.Columns.Add("RelTINIdNo");
                DtAdd.Columns.Add("RelTINCountry");
                DtAdd.Columns.Add("RelTelSTDCode");
                DtAdd.Columns.Add("RelTelNo");
                DtAdd.Columns.Add("RelOfficeTelSTDCode");
                DtAdd.Columns.Add("RelOfficeTelNo");
                DtAdd.Columns.Add("RelMobCode");
                DtAdd.Columns.Add("RelMobileNo");
                DtAdd.Columns.Add("RelFaxNoCode");
                DtAdd.Columns.Add("RelFaxNo");
                DtAdd.Columns.Add("RelEmailID");

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

                DtAdd.Columns.Add("OccuSubTypeRel");

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
                //Added by tushar
                DtAdd.Columns.Add("PanNo");
                DtAdd.Columns.Add("TelCtrCodeOff");
                DtAdd.Columns.Add("TelOff");
                DtAdd.Columns.Add("TelCtrCodeRes");
                DtAdd.Columns.Add("TelRes");
                DtAdd.Columns.Add("MobileCtrCode");
                DtAdd.Columns.Add("Mobile");
                DtAdd.Columns.Add("Email");
                DtAdd.Columns.Add("Remarks");
                DtAdd.Columns.Add("RelPersTypDesc");
                //Added by tushar

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
                //chkLocalAddress.Checked = false; //Commented by kalyani Hande 
                chkCuurentAddress.Checked = false;
                ddlState1.Enabled = true;
                ddlState1.SelectedIndex = 0;
                ddlProofOfAddress.Enabled = true;
                //txtRelRefNumber.Text = "";
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
                cboGender.SelectedIndex = 0;

                //ddlIsoCountry.SelectedIndex = 0;
                ddlProofIdentity1.SelectedIndex = 0;
                txtPassNo.Text = "";
                txtPassNoAdd.Text = "";
                txtPassExpDate.Text = "";
                //chkPerAddress.Checked = false;
                //chkPerAddress.Enabled = true;
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
                //chkDone.Enabled = false; //Commented by kalyani hande
                //rbtFS.Checked = false;
                //rbtFS.SelectedValue = "";
                divIdProof.Visible = false;
                divAddProof.Visible = false;

                txtLocAddLine1.Text = "";
                txtLocAddLine2.Text = "";
                txtLocAddLine3.Text = "";
                txtCity1.Text = "";
                ddlState.SelectedIndex = 0;
                ddlPinCode1.Text = "";

                txtDistrict1.Text = "";
                ddlCountryCode1.SelectedIndex = 0;


                ddlPinCode.SelectedIndex = 0;
                ddlCountryCode.SelectedIndex = 0;
                ddlNationality.SelectedIndex = 0;
                ddlState.Items.Insert(0, new ListItem("Select", string.Empty));
                ddlState1.Items.Insert(0, new ListItem("Select", string.Empty));
                ddlState.SelectedIndex = 0;
                ddlState1.SelectedIndex = 0;
                ddlDistrictname.Items.Insert(0, new ListItem("Select", string.Empty));
                ddlDistrict1.Items.Insert(0, new ListItem("Select", string.Empty));
                txtLocAddLine1.Enabled = true;
                txtLocAddLine2.Enabled = true;
                txtLocAddLine3.Enabled = true;
                txtCity1.Enabled = true;
                ddlState.Enabled = true;
                //ddlPinCode1.Enabled = true;
                //txtDistrict1.Enabled = true;
                ddlCountryCode1.Enabled = true;
                ddlProofOfAddress.SelectedIndex = 0;
                ddlProofOfAddress1.SelectedIndex = 0;
                txtEmpName.Enabled = true;
                txtEmpName.Text = "";
                txtEmpCode.Enabled = true;
                txtEmpCode.Text = "";
                txtEmpDesignation.Enabled = true;
                txtEmpDesignation.Text = "";
                txtEmpBranch.Text = "";
                txtEmpBranch.Enabled = true;
                txtInsName.Enabled = true;
                txtInsCode.Enabled = true;
                chkPanForm.Enabled = true;
                //txtDate3.Text = DateTime.Today.ToString("dd-MM-yyyy");

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


        #region ConfirmYes
        protected void ConfirmYes(object sender, EventArgs e)
        {
            EnableControls();
            //GetRelRefNo();
            ddlRelType.Focus();
            txtPlace.Enabled = true;
            txtDate.Enabled = true;
            txtDate3.Enabled = true;
            btnPSUpdate.Visible = false;
            btnUpdate.Visible = false;
            btnAdd.Visible = true;
            btnPartialAdd.Visible = false;


            divIdProof.Visible = false;
            divAddProof.Visible = false;
            btnPartialAdd.Enabled = false;
            // txtKYCNum.Enabled = false;
            //chkHigh.Enabled = false;
            //chkMedium.Enabled = false;
            //chkLow.Enabled = false;
            //txtDate3.Enabled = false;
            chkAppDeclare1.Enabled = true;
            //chkAppDeclare2.Enabled = false;
            chkAppDeclare3.Enabled = true;
            //txtDate.Enabled = false;
            FillStates();
            //ddlCountryCode.SelectedValue = "IN";

            ddlNationality.SelectedIndex = 0;
            ddlCountryCode.SelectedIndex = 0;
            ddlPinCode.SelectedIndex = 0;
            ddlDocReceived.SelectedIndex = 0;
            ddlDistrictname.SelectedIndex = 0;
            txtDate.Enabled = true;
            txtDate3.Enabled = true;
            //txtEmpName.Text = "";
            //txtEmpCode.Text = "";
            //txtEmpDesignation.Text = "";
            //txtEmpBranch.Text = "";
            //txtInsName.Text = "";
            //txtInsCode.Text = "";
            txtNum.Text = "";
            BindAttestation();
            //Session["PSSubmit"]

            if (Session["PSSubmit"] != null && Session["PSSubmit"].ToString() == "Y")
            {
                txtPlace.Enabled = true;
                ddlDocReceived.Enabled = true;
                txtEmpName.Enabled = false;
                txtEmpCode.Enabled = false;
                txtEmpDesignation.Enabled = false;
                txtEmpBranch.Enabled = false;
                txtInsName.Enabled = false;
                txtInsCode.Enabled = false;
            }
            else
            {
                //txtPlace.Enabled = false;
                txtEmpName.Enabled = false;
                txtEmpCode.Enabled = false;
                txtEmpDesignation.Enabled = false;
                txtEmpBranch.Enabled = false;
                txtInsName.Enabled = false;
                txtInsCode.Enabled = false;
            }

            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenRelatedPersonPageAsEntity();", true);
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
            dataRow["MARITAL_STATUS"] = "";
            dataRow["CITIZENSHIP"] = "";
            dataRow["RESI_STATUS"] = "";
            dataRow["OCCU_TYPE"] = "";

            dataRow["ResForTaxFlagRel"] = "";

            dataRow["ISO_COUNTRYCODE"] = ddlIsoCountryCodeOthr.SelectedItem;
            dataRow["TAX_IDNUMBER"] = "";
            dataRow["BIRTH_PLACE"] = "";
            dataRow["ISO_BIRTHPLACE_CODE"] = "";
            dataRow["IdType"] = ddlProofOfAddress.SelectedValue;
            if (ddlProofOfAddress.SelectedIndex == 1)
            {
                dataRow["IdNumber"] = txtPassNo.Text.Trim();
                dataRow["IdExpDate"] = txtPassExpDate.Text.Trim();
                dataRow["IdName"] = System.DBNull.Value;

            }
            else if (ddlProofOfAddress.SelectedIndex == 2)
            {
                dataRow["IdNumber"] = txtPassNo.Text.Trim();
                dataRow["IdExpDate"] = System.DBNull.Value;
                dataRow["IdName"] = System.DBNull.Value;

            }
            else if (ddlProofOfAddress.SelectedIndex == 3)
            {
                dataRow["IdNumber"] = txtPassNo.Text.Trim();
                dataRow["IdExpDate"] = System.DBNull.Value;
                dataRow["IdName"] = System.DBNull.Value;
            }
            else if (ddlProofOfAddress.SelectedIndex == 4)
            {
                dataRow["IdNumber"] = txtPassNo.Text.Trim();
                dataRow["IdExpDate"] = txtPassExpDate.Text.Trim();
                dataRow["IdName"] = System.DBNull.Value;
            }
            else if (ddlProofOfAddress.SelectedIndex == 5)
            {
                dataRow["IdNumber"] = txtPassNo.Text.Trim();
                dataRow["IdExpDate"] = System.DBNull.Value;
                dataRow["IdName"] = System.DBNull.Value;
            }
            else if (ddlProofOfAddress.SelectedIndex == 6)
            {
                dataRow["IdNumber"] = txtPassNo.Text.Trim();
                dataRow["IdExpDate"] = System.DBNull.Value;
                dataRow["IdName"] = System.DBNull.Value;
            }
            else if (ddlProofOfAddress.SelectedIndex == 7)
            {
                dataRow["IdNumber"] = txtPassOthr.Text.Trim();
                dataRow["IdExpDate"] = System.DBNull.Value;
                dataRow["IdName"] = txtPassNo.Text.Trim();
            }
            else if (ddlProofOfAddress.SelectedIndex == 8 || ddlProofOfAddress.SelectedIndex == 9)
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

            //Commented by Kalyani Hande start
            //if (chkPerAddress.Checked == true)
            //{
            //    dataRow["CnctType1"] = "P1";
            //    dataRow["PER_ADDTYPE"] = "";
            //    dataRow["PER_ADDPROOF"] = ddlProofOfAddress.SelectedValue;
            //    dataRow["PER_ADDLINE1"] = txtAddressLine1.Text.Trim();
            //    dataRow["PER_ADDLINE2"] = txtAddressLine2.Text.Trim();
            //    dataRow["PER_ADDLINE3"] = txtAddressLine3.Text.Trim();
            //    dataRow["PER_CITY"] = txtCity.Text.Trim();
            //    dataRow["PER_DISTRICT"] = txtDistrictname.Text;
            //    dataRow["PER_PIN"] = txtPinCode.Text;
            //    dataRow["PER_STATECODE"] = ddlState.SelectedValue;
            //    dataRow["PER_COUNTRY_CODE"] = ddlCountryCode.SelectedValue;
            //}
            //else
            //{
            //    dataRow["CnctType1"] = "";
            //    dataRow["PER_ADDTYPE"] = System.DBNull.Value;
            //    dataRow["PER_ADDPROOF"] = System.DBNull.Value;
            //    dataRow["PER_ADDLINE1"] = System.DBNull.Value;
            //    dataRow["PER_ADDLINE2"] = System.DBNull.Value;
            //    dataRow["PER_ADDLINE3"] = System.DBNull.Value;
            //    dataRow["PER_CITY"] = System.DBNull.Value;
            //    dataRow["PER_DISTRICT"] = System.DBNull.Value;
            //    dataRow["PER_PIN"] = System.DBNull.Value;
            //    dataRow["PER_STATECODE"] = System.DBNull.Value;
            //    dataRow["PER_COUNTRY_CODE"] = System.DBNull.Value;
            //}
            //Commented by Kalyani Hande end


            dataRow["AddIdType"] = ddlProofOfAddress.SelectedValue;

            //Commented by Kalyani Hande start
            //if (chkPerAddress.Checked == true)
            //{
            //    if (ddlProofOfAddress.SelectedIndex == 1)
            //    {
            //        dataRow["AddIdNumber"] = txtPassNoAdd.Text.Trim();
            //        dataRow["AddIdExpDate"] = txtPassExpDateAdd.Text.Trim();
            //        dataRow["AddIdName"] = System.DBNull.Value;
            //    }


            //    else if (ddlProofOfAddress.SelectedIndex == 2)
            //    {
            //        dataRow["AddIdNumber"] = txtPassNoAdd.Text.Trim();
            //        dataRow["AddIdExpDate"] = txtPassExpDateAdd.Text.Trim();
            //        dataRow["AddIdName"] = System.DBNull.Value;
            //    }
            //    else if (ddlProofOfAddress.SelectedIndex == 3)
            //    {
            //        dataRow["AddIdNumber"] = txtPassNoAdd.Text.Trim();
            //        dataRow["AddIdExpDate"] = System.DBNull.Value;
            //        dataRow["AddIdName"] = System.DBNull.Value;
            //    }
            //    else if (ddlProofOfAddress.SelectedIndex == 4)
            //    {
            //        dataRow["AddIdNumber"] = txtPassNoAdd.Text.Trim();
            //        dataRow["AddIdExpDate"] = System.DBNull.Value;
            //        dataRow["AddIdName"] = System.DBNull.Value;

            //    }
            //    else if (ddlProofOfAddress.SelectedIndex == 5)
            //    {
            //        dataRow["AddIdNumber"] = txtPassNoAdd.Text.Trim();
            //        dataRow["AddIdExpDate"] = System.DBNull.Value;
            //        dataRow["AddIdName"] = System.DBNull.Value;
            //    }
            //    else if (ddlProofOfAddress.SelectedIndex == 6)
            //    {
            //        dataRow["AddIdNumber"] = txtPassOthrAdd.Text.Trim();
            //        dataRow["AddIdExpDate"] = System.DBNull.Value;
            //        dataRow["AddIdName"] = txtPassNoAdd.Text.Trim();
            //    }

            //    else
            //    {
            //        dataRow["AddIdNumber"] = System.DBNull.Value;
            //        dataRow["AddIdExpDate"] = System.DBNull.Value;
            //        dataRow["AddIdName"] = System.DBNull.Value;
            //    }
            //}
            //else
            //{
            //    dataRow["AddIdNumber"] = System.DBNull.Value;
            //    dataRow["AddIdExpDate"] = System.DBNull.Value;
            //    dataRow["AddIdName"] = System.DBNull.Value;
            //}
            //Commented by Kalyani Hande end

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

                if (Session["dsRel"] != null)
                {
                    DataTable dtbl = new DataTable();
                    dtbl = (DataTable)Session["dsRel"];
                    DtAdd = dtbl.Rows[Convert.ToInt32(RowIndex)].Table;
                    int i = Convert.ToInt32(RowIndex);
                    txtRefNumber.Text = Convert.ToString(DtAdd.Rows[i]["FIRefNo"]);
                    txtRelRefNumber.Text = Convert.ToString(DtAdd.Rows[i]["RelRefNo"]);
                    txtKYCNum.Text = Convert.ToString(DtAdd.Rows[i]["RelatedPrsnKYCNo"]);
                    ddlRelType.SelectedValue = Convert.ToString(DtAdd.Rows[i]["RelationType"]);

                    cboTitle.SelectedValue = Convert.ToString(DtAdd.Rows[i]["PrefixRel"]);
                    txtGivenName.Text = Convert.ToString(DtAdd.Rows[i]["FNameRel"]);
                    txtMiddleName.Text = Convert.ToString(DtAdd.Rows[i]["MNameRel"]);
                    txtLastName.Text = Convert.ToString(DtAdd.Rows[i]["LNameRel"]);

                    cboTitle1.SelectedValue = Convert.ToString(DtAdd.Rows[i]["MaidPrefixRel"]);
                    txtGivenName1.Text = Convert.ToString(DtAdd.Rows[i]["MaidFNameRel"]);
                    txtMiddleName1.Text = Convert.ToString(DtAdd.Rows[i]["MaidMNameRel"]);
                    txtLastName1.Text = Convert.ToString(DtAdd.Rows[i]["MaidLNameRel"]);
                    ddlNationality.SelectedValue = Convert.ToString(DtAdd.Rows[i]["CitizenshipRel"]);
                    txtPanNo.Text = Convert.ToString(DtAdd.Rows[i]["PANNo"]);
                    if (Convert.ToString(DtAdd.Rows[i]["FSFlagRel"]) == "01")
                    {
                        rbtFS.SelectedValue = "F";
                    }
                    else
                    {
                        rbtFS.SelectedValue = "S";
                    }
                    cboTitle2.SelectedValue = Convert.ToString(DtAdd.Rows[i]["FatherPrefixRel"]);
                    txtGivenName2.Text = Convert.ToString(DtAdd.Rows[i]["FatherFNameRel"]);
                    txtMiddleName2.Text = Convert.ToString(DtAdd.Rows[i]["FatherMNameRel"]);
                    txtLastName2.Text = Convert.ToString(DtAdd.Rows[i]["FatherLNameRel"]);
                    cboTitle3.SelectedValue = Convert.ToString(DtAdd.Rows[i]["MotherPrefixRel"]);
                    txtGivenName3.Text = Convert.ToString(DtAdd.Rows[i]["MotherFNameRel"]);
                    txtMiddleName3.Text = Convert.ToString(DtAdd.Rows[i]["MotherMNameRel"]);
                    txtLastName3.Text = Convert.ToString(DtAdd.Rows[i]["MotherLNameRel"]);
                    txtDOB.Text = Convert.ToString(DtAdd.Rows[i]["DOBRel"]);
                    cboGender.SelectedValue = Convert.ToString(DtAdd.Rows[i]["GenderRel"]);


                    ddlIsoCountryCodeOthr.SelectedValue = Convert.ToString(DtAdd.Rows[i]["ISOCountryCodeRel"]);

                    PopulateAddressProofType();
                    ddlProofOfAddress.SelectedValue = Convert.ToString(DtAdd.Rows[i]["IdType"]);
                    txtPassNoAdd.Text = Convert.ToString(DtAdd.Rows[i]["IdNumber"]);
                    ViewState["strIdName"] = Convert.ToString(DtAdd.Rows[i]["IdName"]);
                    ViewState["strIdNumber1"] = Convert.ToString(DtAdd.Rows[i]["IdNumber"]);
                    ViewState["strIdExpDate"] = Convert.ToString(DtAdd.Rows[i]["IdExpDate"]);

                    if (ddlProofIdentity1.SelectedValue == "Z")
                    {
                        txtPassOthr.Text = Convert.ToString(DtAdd.Rows[i]["IdNumber"]);
                        txtPassNo.Text = Convert.ToString(DtAdd.Rows[i]["IdName"]);
                    }
                    else
                    {
                        txtPassNo.Text = Convert.ToString(DtAdd.Rows[i]["IdNumber"]);
                        txtPassExpDate.Text = Convert.ToString(DtAdd.Rows[i]["IdExpDate"]);
                    }

                    if (Convert.ToString(DtAdd.Rows[i]["SameasCurrentAddresFlagM1"]) == "01")
                    {
                        chkCuurentAddress.Checked = true;
                    }
                    else { chkCuurentAddress.Checked = false; }
                    divPassDateAdd1.Visible = false;
                    FillddlPageLoad();
                    ddlProofOfAddress1.SelectedValue = Convert.ToString(DtAdd.Rows[i]["AddIdTypeRel"]);
                    if (ddlProofOfAddress1.SelectedItem.Text == "Deemed Proof of Address- Document Type Code")
                    {
                        FillddlDeemAddrPrf();
                        ddlDeemProfofAddr.SelectedValue = Convert.ToString(DtAdd.Rows[i]["AddIdNumberRel"]);
                    }
                    else
                    {
                        txtPassNoAdd1.Text = Convert.ToString(DtAdd.Rows[i]["AddIdNumberRel"]);
                    }
                    ViewState["strIdNumber2"] = Convert.ToString(DtAdd.Rows[i]["AddIdNumberRel"]);
                    txtAddressLine1.Text = Convert.ToString(DtAdd.Rows[i]["Adr1Rel"]);
                    txtAddressLine2.Text = Convert.ToString(DtAdd.Rows[i]["Adr2Rel"]);
                    txtAddressLine3.Text = Convert.ToString(DtAdd.Rows[i]["Adr3Rel"]);
                    txtCity.Text = Convert.ToString(DtAdd.Rows[i]["CityRel"]);
                    txtDistrictname.Text = Convert.ToString(DtAdd.Rows[i]["DistrictRel"]);
                    txtPinCode.Text = Convert.ToString(DtAdd.Rows[i]["PostCodeRel"]);
                    ddlDistrictname.SelectedItem.Text = Convert.ToString(DtAdd.Rows[i]["DistrictRel"]);
                    ddlPinCode.SelectedValue = Convert.ToString(DtAdd.Rows[i]["PostCodeRel"]);
                    ddlState.SelectedItem.Text = Convert.ToString(DtAdd.Rows[i]["StateCodeRel"]);
                    ddlCountryCode.SelectedValue = Convert.ToString(DtAdd.Rows[i]["CntryCodeRel"]);
                    ViewState["strAddIdName"] = Convert.ToString(DtAdd.Rows[i]["AddIdNameRel"]);
                    ViewState["strAddIdNumber"] = Convert.ToString(DtAdd.Rows[i]["AddIdNumberRel"]);
                    ViewState["strAddIdExpDate"] = Convert.ToString(DtAdd.Rows[i]["AddIdExpDateRel"]);


                    txtLocAddLine1.Text = Convert.ToString(DtAdd.Rows[i]["Adr1Rel1"]);
                    txtLocAddLine2.Text = Convert.ToString(DtAdd.Rows[i]["Adr2Rel1"]);
                    txtLocAddLine3.Text = Convert.ToString(DtAdd.Rows[i]["Adr3Rel1"]);
                    txtCity1.Text = Convert.ToString(DtAdd.Rows[i]["CityRel1"]);
                    ddlDistrict1.SelectedItem.Text = Convert.ToString(DtAdd.Rows[i]["DistrictRel1"]);
                    ddlPinCode01.SelectedValue = Convert.ToString(DtAdd.Rows[i]["PostCodeRel1"]);
                    ddlState1.SelectedItem.Text = Convert.ToString(DtAdd.Rows[i]["StateCodeRel1"]);
                    ddlCountryCode1.SelectedValue = Convert.ToString(DtAdd.Rows[i]["CntryCodeRel1"]);

                    if (ddlProofOfAddress.SelectedValue == "99")
                    {
                        txtPassOthrAdd.Text = Convert.ToString(DtAdd.Rows[i]["AddIdNumberRel"]);
                        // txtPassNoAdd.Text = Convert.ToString(DtAdd.Rows[i]["AddIdNameRel"]);
                    }
                    else
                    {
                        // txtPassNoAdd.Text = Convert.ToString(DtAdd.Rows[i]["AddIdNumberRel"]);
                        txtPassExpDateAdd.Text = Convert.ToString(DtAdd.Rows[i]["AddIdExpDateRel"]);
                    }

                    txtTelOff.Text = Convert.ToString(DtAdd.Rows[i]["TelCtrCodeOff"]);
                    txtTelOff2.Text = Convert.ToString(DtAdd.Rows[i]["TelOff"]);
                    txtTelRes.Text = Convert.ToString(DtAdd.Rows[i]["TelCtrCodeRes"]);
                    txtTelRes2.Text = Convert.ToString(DtAdd.Rows[i]["TelRes"]);
                    txtMobile.Text = Convert.ToString(DtAdd.Rows[i]["MobileCtrCode"]);
                    txtMobile2.Text = Convert.ToString(DtAdd.Rows[i]["Mobile"]);
                    txtemail.Text = Convert.ToString(DtAdd.Rows[i]["Email"]);

                    txtPlace.Text = Convert.ToString(DtAdd.Rows[i]["DecPlaceRel"]);
                    txtDate.Text = Convert.ToString(DtAdd.Rows[i]["DecDateRel"]);
                    txtEmpName.Text = Convert.ToString(DtAdd.Rows[i]["kycEmpNameRel"]);
                    txtEmpCode.Text = Convert.ToString(DtAdd.Rows[i]["kycEmpCodeRel"]);
                    txtEmpDesignation.Text = Convert.ToString(DtAdd.Rows[i]["kycEmpDesiRel"]);
                    txtEmpBranch.Text = Convert.ToString(DtAdd.Rows[i]["kycEmpBranchRel"]);
                    txtInsName.Text = Convert.ToString(DtAdd.Rows[i]["kycInstNameRel"]);
                    txtRemarks.Text = Convert.ToString(DtAdd.Rows[i]["Remarks"]);
                    txtInsCode.Text = Convert.ToString(DtAdd.Rows[i]["kycInstCodeRel"]);
                    txtDate3.Text = Convert.ToString(DtAdd.Rows[i]["kycVerDateRel"]);
                    ddlDocReceived.SelectedValue = Convert.ToString(DtAdd.Rows[i]["kycCertDocRel"]);
                    if (ddlRelType.SelectedItem.Text == "Director")
                    {
                        lblNum.Text = "Direct Identification Number";
                        lblNum.Visible = true;
                        lblNumImp.Visible = true;
                        txtNum.Visible = true;
                        txtNum.MaxLength = 8;
                        txtNum.Text = Convert.ToString(DtAdd.Rows[i]["AddIdNameRel"]);
                        txtNum.Attributes.Add("onkeypress", "fncInputNumericValuesOnly()");
                    }
                    else if (ddlRelType.SelectedItem.Text == "Other")
                    {
                        lblNum.Text = "Other Description";
                        lblNum.Visible = true;
                        lblNumImp.Visible = true;
                        txtNum.Visible = true;
                        txtNum.MaxLength = 150;
                        txtNum.Text = Convert.ToString(DtAdd.Rows[i]["AddIdNameRel"]);
                        txtNum.Attributes.Add("onkeypress", "");
                    }
                    else
                    {
                        lblNum.Text = "";
                        lblNum.Visible = false;
                        lblNumImp.Visible = false;
                        txtNum.Visible = false;
                        txtNum.Attributes.Add("onkeypress", "");
                    }

                }
                else
                {
                    objDAL = new DataAccessLayer("CKYCConnectionString");
                    dt = new DataTable();
                    htParam.Clear();
                    htParam.Add("@RegRefNo", Request.QueryString["refno"].ToString());
                    htParam.Add("@RelRefNo", Request.QueryString["RelRefNo"].ToString().Trim());
                    htParam.Add("@ActionFlag", "Mod");
                    htParam.Add("@UserType", "");

                    dt = objDAL.GetDataTable("Prc_GetRelatedPersonDataForCKYC", htParam);

                    txtRefNumber.Text = Convert.ToString(dt.Rows[0]["FIRefNo"]);
                    txtRelRefNumber.Text = Convert.ToString(dt.Rows[0]["RelRefNo"]);
                    txtKYCNum.Text = Convert.ToString(dt.Rows[0]["RelatedPrsnKYCNo"]);
                    ddlRelType.SelectedValue = Convert.ToString(dt.Rows[0]["RelationType"]);
                    ddlRelType.Enabled = false;
                    cboTitle.SelectedValue = Convert.ToString(dt.Rows[0]["PrefixRel"]);
                    txtGivenName.Text = Convert.ToString(dt.Rows[0]["FNameRel"]);
                    txtMiddleName.Text = Convert.ToString(dt.Rows[0]["MNameRel"]);
                    txtLastName.Text = Convert.ToString(dt.Rows[0]["LNameRel"]);
                    cboTitle1.SelectedValue = Convert.ToString(dt.Rows[0]["MaidPrefixRel"]);
                    txtGivenName1.Text = Convert.ToString(dt.Rows[0]["MaidFNameRel"]);
                    txtMiddleName1.Text = Convert.ToString(dt.Rows[0]["MaidMNameRel"]);
                    txtLastName1.Text = Convert.ToString(dt.Rows[0]["MaidLNameRel"]);
                    ddlNationality.SelectedValue = Convert.ToString(dt.Rows[0]["CitizenshipRel"]);
                    ddlNationality.Enabled = false;
                    txtPanNo.Text = Convert.ToString(dt.Rows[0]["RelPAN"]);
                    if (Convert.ToString(dt.Rows[0]["FSFlagRel"]) == "01")
                    {
                        rbtFS.SelectedValue = "F";
                    }
                    else
                    {
                        rbtFS.SelectedValue = "S";
                    }
                    txtPanNo.Enabled = false;
                    chkPanForm.Enabled = false;
                    cboTitle2.SelectedValue = Convert.ToString(dt.Rows[0]["FatherPrefixRel"]);
                    txtGivenName2.Text = Convert.ToString(dt.Rows[0]["FatherFNameRel"]);
                    txtMiddleName2.Text = Convert.ToString(dt.Rows[0]["FatherMNameRel"]);
                    txtLastName2.Text = Convert.ToString(dt.Rows[0]["FatherLNameRel"]);
                    cboTitle3.SelectedValue = Convert.ToString(dt.Rows[0]["MotherPrefixRel"]);
                    txtGivenName3.Text = Convert.ToString(dt.Rows[0]["MotherFNameRel"]);
                    txtMiddleName3.Text = Convert.ToString(dt.Rows[0]["MotherMNameRel"]);
                    txtLastName3.Text = Convert.ToString(dt.Rows[0]["MotherLNameRel"]);
                    txtDOB.Text = Convert.ToString(dt.Rows[0]["DOBRel"]);
                    cboGender.SelectedValue = Convert.ToString(dt.Rows[0]["GenderRel"]);


                    ddlIsoCountryCodeOthr.SelectedValue = Convert.ToString(dt.Rows[0]["ISOCountryCodeRel"]);

                    //PopulateAddressProofType();
                    //ddlProofOfAddress.SelectedValue = Convert.ToString(dt.Rows[0]["IdType"]);
                    txtPassNoAdd.Text = Convert.ToString(dt.Rows[0]["IdNumber"]);
                    ViewState["strIdName"] = Convert.ToString(dt.Rows[0]["IdName"]);
                    ViewState["strIdNumber"] = Convert.ToString(dt.Rows[0]["IdNumber"]);
                    ViewState["strIdExpDate"] = Convert.ToString(dt.Rows[0]["IdExpDate"]);

                    if (ddlProofIdentity1.SelectedValue == "Z")
                    {
                        txtPassOthr.Text = Convert.ToString(dt.Rows[0]["IdNumber"]);
                        txtPassNo.Text = Convert.ToString(dt.Rows[0]["IdName"]);
                    }
                    else
                    {
                        txtPassNo.Text = Convert.ToString(dt.Rows[0]["IdNumber"]);
                        txtPassExpDate.Text = Convert.ToString(dt.Rows[0]["IdExpDate"]);
                    }


                    //Commented by Kalyani Hande start
                    //if (Convert.ToString(dt.Rows[0]["CnctTypeRel"]) == "P1")
                    //{
                    //    chkPerAddress.Checked = true;
                    //}
                    //else
                    //{
                    //    chkPerAddress.Checked = false;
                    //}
                    //Commented by Kalyani Hande end

                    //ddlAddressType.SelectedValue = Convert.ToString(dt.Rows[0]["AdrTypeRel"]);
                    if (Convert.ToString(dt.Rows[0]["SameAsCurrentAddresFlag"]) == "01")
                    {
                        chkCuurentAddress.Checked = true;
                    }
                    divPassDateAdd1.Visible = false;
                    ddlProofOfAddress1.SelectedValue = Convert.ToString(dt.Rows[0]["AdrProfRel"]);
                    txtPassNoAdd1.Text = Convert.ToString(dt.Rows[0]["IdNumber"]);
                    txtAddressLine1.Text = Convert.ToString(dt.Rows[0]["Adr1Rel"]);
                    txtAddressLine2.Text = Convert.ToString(dt.Rows[0]["Adr2Rel"]);
                    txtAddressLine3.Text = Convert.ToString(dt.Rows[0]["Adr3Rel"]);
                    txtCity.Text = Convert.ToString(dt.Rows[0]["CityRel"]);
                    txtDistrictname.Text = Convert.ToString(dt.Rows[0]["DistrictRel"]);
                    txtPinCode.Text = Convert.ToString(dt.Rows[0]["PostCodeRel"]);
                    ddlDistrictname.SelectedItem.Text = Convert.ToString(dt.Rows[0]["DistrictRel"]);
                    ddlPinCode.SelectedValue = Convert.ToString(dt.Rows[0]["PostCodeRel"]);
                    ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["StateCodeRel"]);
                    ddlCountryCode.SelectedValue = Convert.ToString(dt.Rows[0]["CntryCodeRel"]);
                    ViewState["strAddIdName"] = Convert.ToString(dt.Rows[0]["AddIdNameRel"]);
                    ViewState["strAddIdNumber"] = Convert.ToString(dt.Rows[0]["AddIdNumberRel"]);
                    ViewState["strAddIdExpDate"] = Convert.ToString(dt.Rows[0]["AddIdExpDateRel"]);

                    if (ddlProofOfAddress.SelectedValue == "99")
                    {
                        txtPassOthrAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNumberRel"]);
                        // txtPassNoAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNameRel"]);
                    }
                    else
                    {
                        // txtPassNoAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNumberRel"]);
                        txtPassExpDateAdd.Text = Convert.ToString(dt.Rows[0]["AddIdExpDateRel"]);
                    }

                    txtPlace.Text = Convert.ToString(dt.Rows[0]["DecPlaceRel"]);
                    txtDate.Text = Convert.ToString(dt.Rows[0]["DecDateRel"]);
                    txtEmpName.Text = Convert.ToString(dt.Rows[0]["kycEmpNameRel"]);
                    txtEmpCode.Text = Convert.ToString(dt.Rows[0]["kycEmpCodeRel"]);
                    txtEmpDesignation.Text = Convert.ToString(dt.Rows[0]["kycEmpDesiRel"]);
                    txtEmpBranch.Text = Convert.ToString(dt.Rows[0]["kycEmpBranchRel"]);
                    txtInsName.Text = Convert.ToString(dt.Rows[0]["kycInstNameRel"]);
                    txtRemarks.Text = Convert.ToString(dt.Rows[0]["Remarks"]);
                    txtInsCode.Text = Convert.ToString(dt.Rows[0]["kycInstCodeRel"]);
                    txtDate3.Text = Convert.ToString(dt.Rows[0]["kycVerDateRel"]);
                    ddlDocReceived.SelectedValue = Convert.ToString(dt.Rows[0]["kycCertDocRel"]);

                }
                //ddlOccuSubType.SelectedValue = Convert.ToString(dt.Rows[0]["OccuSubTypeRel"]);
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

                    dataRow["RelPersonAs"] = "I";

                    dataRow["RelEntName"] = System.DBNull.Value;
                    dataRow["RelDtofIncorporation"] = System.DBNull.Value;
                    dataRow["RelDtofCommencementofbusi"] = System.DBNull.Value;
                    dataRow["RelPlaceofIncorportation"] = System.DBNull.Value;
                    dataRow["RelCountryofIncorporation"] = System.DBNull.Value;
                    dataRow["RelCountryofResAsperTaxLaws"] = System.DBNull.Value;
                    dataRow["RelIdType"] = System.DBNull.Value;
                    dataRow["RelPAN"] = System.DBNull.Value;
                    dataRow["RelTINIdNo"] = System.DBNull.Value;
                    dataRow["RelTINCountry"] = System.DBNull.Value;

                    dataRow["RelTelSTDCode"] = System.DBNull.Value;
                    dataRow["RelTelNo"] = System.DBNull.Value;
                    dataRow["RelOfficeTelSTDCode"] = System.DBNull.Value;
                    dataRow["RelOfficeTelNo"] = System.DBNull.Value;
                    dataRow["RelMobCode"] = System.DBNull.Value;
                    dataRow["RelMobileNo"] = System.DBNull.Value;
                    dataRow["RelFaxNoCode"] = System.DBNull.Value;
                    dataRow["RelFaxNo"] = System.DBNull.Value;
                    dataRow["RelEmailID"] = System.DBNull.Value;

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
                    dataRow["MaritalStatusRel"] = "";
                    dataRow["CitizenshipRel"] = "";
                    dataRow["ResiStatusRel"] = "";

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


                    if (ddlNationality.SelectedIndex != 0)
                    {
                        dataRow["CitizenshipReltxt"] = ddlNationality.SelectedItem.Text;
                    }
                    else
                    {
                        dataRow["CitizenshipReltxt"] = "";
                    }

                    //dataRow["GenderReltxt"] = cboGender.SelectedItem.Text;


                    dataRow["MaritalStatusReltxt"] = "";
                    //dataRow["MaritalStatusReltxt"] = "";


                    //dataRow["CitizenshipReltxt"] = "";

                    dataRow["ResiStatusReltxt"] = "";
                    //dataRow["ResiStatusReltxt"] ="";


                    dataRow["OccuTypeReltxt"] = "";
                    //dataRow["OccuTypeReltxt"] = ddlOccuSubType.SelectedItem.Text;

                    //dataRow["RelationTypetxt"] = ddlRelType.SelectedItem.Text;

                    //dataRow["GenderReltxt"] = cboGender.SelectedItem.Text;
                    //dataRow["MaritalStatusReltxt"] = "";
                    //dataRow["CitizenshipReltxt"] = "";
                    //dataRow["ResiStatusReltxt"] ="";
                    //dataRow["OccuTypeReltxt"] = ddlOccuSubType.SelectedItem.Text;

                    //---end to show on grid

                    dataRow["OccuTypeRel"] = "";

                    dataRow["OccuSubTypeRel"] = "";



                    dataRow["ResForTaxFlagRel"] = "";
                    dataRow["ISOCountryCodeRel"] = "";
                    dataRow["TaxIDNumberRel"] = "";
                    dataRow["BirthCityRel"] = "";
                    dataRow["ISOBirthPlaceCodeRel"] = "";
                    dataRow["IdType"] = ddlProofOfAddress.SelectedValue;
                    if (ddlProofOfAddress.SelectedIndex == 1)
                    {
                        dataRow["IdNumber"] = txtPassNo.Text.Trim();
                        dataRow["IdExpDate"] = txtPassExpDate.Text.Trim();
                        dataRow["IdName"] = System.DBNull.Value;

                    }
                    else if (ddlProofOfAddress.SelectedIndex == 2)
                    {
                        dataRow["IdNumber"] = txtPassNo.Text.Trim();
                        dataRow["IdExpDate"] = System.DBNull.Value;
                        dataRow["IdName"] = System.DBNull.Value;

                    }
                    else if (ddlProofOfAddress.SelectedIndex == 3)
                    {
                        dataRow["IdNumber"] = txtPassNo.Text.Trim();
                        dataRow["IdExpDate"] = System.DBNull.Value;
                        dataRow["IdName"] = System.DBNull.Value;
                    }
                    else if (ddlProofOfAddress.SelectedIndex == 4)
                    {
                        dataRow["IdNumber"] = txtPassNo.Text.Trim();
                        dataRow["IdExpDate"] = txtPassExpDate.Text.Trim();
                        dataRow["IdName"] = System.DBNull.Value;
                    }
                    else if (ddlProofOfAddress.SelectedIndex == 5)
                    {
                        dataRow["IdNumber"] = txtPassNo.Text.Trim();
                        dataRow["IdExpDate"] = System.DBNull.Value;
                        dataRow["IdName"] = System.DBNull.Value;
                    }
                    else if (ddlProofOfAddress.SelectedIndex == 6)
                    {
                        dataRow["IdNumber"] = txtPassNo.Text.Trim();
                        dataRow["IdExpDate"] = System.DBNull.Value;
                        dataRow["IdName"] = System.DBNull.Value;
                    }
                    else if (ddlProofOfAddress.SelectedIndex == 7)
                    {
                        dataRow["IdNumber"] = txtPassOthr.Text.Trim();
                        dataRow["IdExpDate"] = System.DBNull.Value;
                        dataRow["IdName"] = txtPassNo.Text.Trim();
                    }
                    else if (ddlProofOfAddress.SelectedIndex == 8 || ddlProofOfAddress.SelectedIndex == 9)
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

                    //Commented by Kalyani Hande start
                    //if (chkPerAddress.Checked == true)
                    //{
                    //    dataRow["CnctTypeRel"] = "P1";
                    //    dataRow["AdrTypeRel"] = "";
                    //    dataRow["AdrProfRel"] = ddlProofOfAddress.SelectedValue.Trim();
                    //    dataRow["Adr1Rel"] = txtAddressLine1.Text.Trim();
                    //    dataRow["Adr2Rel"] = txtAddressLine2.Text.Trim();
                    //    dataRow["Adr3Rel"] = txtAddressLine3.Text.Trim();
                    //    dataRow["CityRel"] = txtCity.Text.Trim();
                    //    dataRow["DistrictRel"] = txtDistrictname.Text;
                    //    dataRow["PostCodeRel"] = txtPinCode.Text;

                    //    if (ddlCountryCode.SelectedValue == "IN")
                    //    {
                    //        //htParam.Add("@PER_STATECODE", ddlState.SelectedValue);
                    //        dataRow["StateCodeRel"] = ddlState.SelectedValue;
                    //    }
                    //    else
                    //    {
                    //        //htParam.Add("@PER_STATECODE", txtState.Text.Trim());
                    //        dataRow["StateCodeRel"] = txtState.Text.Trim();
                    //    }

                    //    //dataRow["StateCodeRel"] = ddlState.SelectedValue;
                    //    dataRow["CntryCodeRel"] = ddlCountryCode.SelectedValue;
                    //}
                    //else
                    //{
                    //    dataRow["CnctTypeRel"] = "";
                    //    dataRow["AdrTypeRel"] = System.DBNull.Value;
                    //    dataRow["AdrProfRel"] = System.DBNull.Value;
                    //    dataRow["Adr1Rel"] = System.DBNull.Value;
                    //    dataRow["Adr2Rel"] = System.DBNull.Value;
                    //    dataRow["Adr3Rel"] = System.DBNull.Value;
                    //    dataRow["CityRel"] = System.DBNull.Value;
                    //    dataRow["DistrictRel"] = System.DBNull.Value;
                    //    dataRow["PostCodeRel"] = System.DBNull.Value;
                    //    dataRow["StateCodeRel"] = System.DBNull.Value;
                    //    dataRow["CntryCodeRel"] = System.DBNull.Value;
                    //}
                    //Commented by Kalyani Hande end

                    if (chkCuurentAddress.Checked == true)
                    {
                        dataRow["SameasCurrentAddresFlagM1"] = "01";
                        //htParam.Add("@SameasCurrentAddresFlagM1", "01");
                    }
                    else
                    {
                        dataRow["SameasCurrentAddresFlagM1"] = "";
                    }

                    //Commented by kalyani Hande start
                    //if (chkLocalAddress.Checked == true)
                    //{
                    //    dataRow["CnctTypeRel1"] = "M1";
                    //    dataRow["AdrTypeRel1"] = "";
                    //    //dataRow["AdrProfRel1"] = ddlProofOfAddress.SelectedValue;
                    //    dataRow["Adr1Rel1"] = txtLocAddLine1.Text.Trim();
                    //    dataRow["Adr2Rel1"] = txtLocAddLine2.Text.Trim();
                    //    dataRow["Adr3Rel1"] = txtLocAddLine3.Text.Trim();
                    //    dataRow["CityRel1"] = txtCity1.Text.Trim();
                    //    dataRow["DistrictRel1"] = txtDistrict1.Text;
                    //    dataRow["PostCodeRel1"] = ddlPinCode1.Text;

                    //    if (ddlCountryCode1.SelectedValue == "IN")
                    //    {
                    //        //htParam.Add("@PER_STATECODE", ddlState.SelectedValue);
                    //        dataRow["StateCodeRel1"] = ddlState1.SelectedValue;
                    //    }
                    //    else
                    //    {
                    //        //htParam.Add("@PER_STATECODE", txtState.Text.Trim());
                    //        dataRow["StateCodeRel1"] = txtState1.Text.Trim();
                    //    }

                    //    //dataRow["StateCodeRel1"] = ddlState1.SelectedValue;
                    //    dataRow["CntryCodeRel1"] = ddlCountryCode1.SelectedValue;
                    //}
                    //else
                    //{
                    //    dataRow["CnctTypeRel1"] = "";
                    //    dataRow["AdrTypeRel1"] = System.DBNull.Value;
                    //    //dataRow["AdrProfRel1"] = System.DBNull.Value;
                    //    dataRow["Adr1Rel1"] = System.DBNull.Value;
                    //    dataRow["Adr2Rel1"] = System.DBNull.Value;
                    //    dataRow["Adr3Rel1"] = System.DBNull.Value;
                    //    dataRow["CityRel1"] = System.DBNull.Value;
                    //    dataRow["DistrictRel1"] = System.DBNull.Value;
                    //    dataRow["PostCodeRel1"] = System.DBNull.Value;
                    //    dataRow["StateCodeRel1"] = System.DBNull.Value;
                    //    dataRow["CntryCodeRel1"] = System.DBNull.Value;
                    //}
                    //Commented by kalyani Hande end



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


                    dataRow["AddIdTypeRel"] = ddlProofOfAddress.SelectedValue.Trim();

                    //Commented by Kalyani Hande start
                    //if (chkPerAddress.Checked == true)
                    //{
                    //    if (ddlProofOfAddress.SelectedIndex == 1)
                    //    {
                    //        dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                    //        dataRow["AddIdExpDateRel"] = txtPassExpDateAdd.Text.Trim();
                    //        dataRow["AddIdNameRel"] = System.DBNull.Value;
                    //    }

                    //    else if (ddlProofOfAddress.SelectedIndex == 2)
                    //    {
                    //        dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                    //        dataRow["AddIdExpDateRel"] = txtPassExpDateAdd.Text.Trim();
                    //        dataRow["AddIdNameRel"] = System.DBNull.Value;
                    //    }
                    //    else if (ddlProofOfAddress.SelectedIndex == 3)
                    //    {
                    //        dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                    //        dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                    //        dataRow["AddIdNameRel"] = System.DBNull.Value;
                    //    }
                    //    else if (ddlProofOfAddress.SelectedIndex == 4)
                    //    {
                    //        dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                    //        dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                    //        dataRow["AddIdNameRel"] = System.DBNull.Value;
                    //    }
                    //    else if (ddlProofOfAddress.SelectedIndex == 5)
                    //    {
                    //        dataRow["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                    //        dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                    //        dataRow["AddIdNameRel"] = System.DBNull.Value;
                    //    }
                    //    else if (ddlProofOfAddress.SelectedIndex == 6)
                    //    {
                    //        dataRow["AddIdNumberRel"] = txtPassOthrAdd.Text.Trim();
                    //        dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                    //        dataRow["AddIdNameRel"] = txtPassNoAdd.Text.Trim();
                    //    }

                    //    else
                    //    {
                    //        dataRow["AddIdNumberRel"] = System.DBNull.Value;
                    //        dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                    //        dataRow["AddIdNameRel"] = System.DBNull.Value;
                    //    }
                    //}
                    //else
                    //{
                    //    dataRow["AddIdNumberRel"] = System.DBNull.Value;
                    //    dataRow["AddIdExpDateRel"] = System.DBNull.Value;
                    //    dataRow["AddIdNameRel"] = System.DBNull.Value;
                    //}
                    //Commented by Kalyani Hande end

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
                    ddlRelType_SelectedIndexChanged(this, e);
                    ddlProofOfAddress_SelectedIndexChanged(this, e);
                    ddlProofOfAddress1_SelectedIndexChanged(this, e);
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
                    cboGender.SelectedValue = Convert.ToString(dtNew.Rows[idr]["GenderRel"]);
                    ddlNationality.SelectedValue = Convert.ToString(dtNew.Rows[idr]["GenderRel"]);




                    //ddlIsoCountryCodeOthr.SelectedValue = Convert.ToString(dtNew.Rows[idr]["ISOCountryCodeRel"]);
                    if (dtNew.Rows[idr]["ISOCountryCodeRel"].ToString() != "")
                    {
                        ddlIsoCountryCodeOthr.SelectedValue = Convert.ToString(dtNew.Rows[idr]["ISOCountryCodeRel"]);
                    }

                    ddlProofOfAddress.SelectedValue = Convert.ToString(dtNew.Rows[idr]["IdType"]);
                    ViewState["strIdName"] = Convert.ToString(dtNew.Rows[idr]["IdName"]);
                    ViewState["strIdNumber"] = Convert.ToString(dtNew.Rows[idr]["IdNumber"]);
                    ViewState["strIdExpDate"] = Convert.ToString(dtNew.Rows[idr]["IdExpDate"]);

                    if (ddlProofIdentity1.SelectedValue == "Z")
                    {
                        txtPassOthr.Text = Convert.ToString(dtNew.Rows[idr]["IdNumber"]);
                        txtPassNo.Text = Convert.ToString(dtNew.Rows[idr]["IdName"]);
                    }
                    else
                    {
                        txtPassNo.Text = Convert.ToString(dtNew.Rows[idr]["IdNumber"]);
                        txtPassExpDate.Text = Convert.ToString(dtNew.Rows[idr]["IdExpDate"]);
                    }

                    //Commented by Kalyani Hande start
                    //if (Convert.ToString(dtNew.Rows[idr]["CnctTypeRel"]) == "P1")
                    //{
                    //    chkPerAddress.Checked = true;
                    //}
                    //else
                    //{
                    //    chkPerAddress.Checked = false;
                    //}
                    //Commented by Kalyani Hande end

                    //ddlAddressType.SelectedValue = Convert.ToString(dtNew.Rows[idr]["AdrTypeRel"]);
                    ddlProofOfAddress.SelectedValue = Convert.ToString(dtNew.Rows[idr]["AdrProfRel"]);
                    txtAddressLine1.Text = Convert.ToString(dtNew.Rows[idr]["Adr1Rel"]);
                    txtAddressLine2.Text = Convert.ToString(dtNew.Rows[idr]["Adr2Rel"]);
                    txtAddressLine3.Text = Convert.ToString(dtNew.Rows[idr]["Adr3Rel"]);
                    txtCity.Text = Convert.ToString(dtNew.Rows[idr]["CityRel"]);
                    txtDistrictname.Text = Convert.ToString(dtNew.Rows[idr]["DistrictRel"]);
                    txtPinCode.Text = Convert.ToString(dtNew.Rows[idr]["PostCodeRel"]);
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
                    //ddlState.SelectedValue = Convert.ToString(dtNew.Rows[idr]["StateCodeRel"]);
                    ddlCountryCode.SelectedValue = Convert.ToString(dtNew.Rows[idr]["CntryCodeRel"]);

                    //Commented by kalyani Hande start
                    //if (dtNew.Rows[idr]["CnctTypeRel1"].ToString() != "" && dtNew.Rows[idr]["PostCodeRel1"].ToString() != "")
                    //{
                    //    chkLocalAddress.Checked = true;
                    //}
                    //else
                    //{
                    //    chkLocalAddress.Checked = false;
                    //}
                    //Commented by kalyani Hande end

                    //ddlAddressType1.SelectedValue = Convert.ToString(dtNew.Rows[idr]["AdrTypeRel1"]);
                    txtLocAddLine1.Text = Convert.ToString(dtNew.Rows[idr]["Adr1Rel1"]);
                    txtLocAddLine2.Text = Convert.ToString(dtNew.Rows[idr]["Adr2Rel1"]);
                    txtLocAddLine3.Text = Convert.ToString(dtNew.Rows[idr]["Adr3Rel1"]);
                    txtCity1.Text = Convert.ToString(dtNew.Rows[idr]["CityRel1"]);
                    txtDistrict1.Text = Convert.ToString(dtNew.Rows[idr]["DistrictRel1"]);
                    ddlPinCode1.Text = Convert.ToString(dtNew.Rows[idr]["PostCodeRel1"]);
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
                    //ddlState1.SelectedValue = Convert.ToString(dtNew.Rows[idr]["StateCodeRel1"]);
                    ddlCountryCode1.SelectedValue = Convert.ToString(dtNew.Rows[idr]["CntryCodeRel1"]);



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

                    //if (ddlProofIdentity1.SelectedItem.Text == ddlProofOfAddress.SelectedItem.Text)
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
                    cboGender.SelectedValue = Convert.ToString(dt.Rows[0]["GenderRel"]);
                    ddlIsoCountryCodeOthr.SelectedValue = Convert.ToString(dt.Rows[0]["ISOCountryCodeRel"]);

                    ddlProofOfAddress.SelectedValue = Convert.ToString(dt.Rows[0]["IdType"]);

                    //Commented by Kalyani Hande start
                    //if (Convert.ToString(dt.Rows[0]["CnctTypeRel"]) == "P1")
                    //{
                    //    chkPerAddress.Checked = true;
                    //}
                    //else
                    //{
                    //    chkPerAddress.Checked = false;
                    //}
                    //Commented by Kalyani Hande end

                    //ddlAddressType.SelectedValue = Convert.ToString(dt.Rows[0]["AdrTypeRel"]);
                    ddlProofOfAddress.SelectedValue = Convert.ToString(dt.Rows[0]["AdrProfRel"]);
                    txtAddressLine1.Text = Convert.ToString(dt.Rows[0]["Adr1Rel"]);
                    txtAddressLine2.Text = Convert.ToString(dt.Rows[0]["Adr2Rel"]);
                    txtAddressLine3.Text = Convert.ToString(dt.Rows[0]["Adr3Rel"]);
                    txtCity.Text = Convert.ToString(dt.Rows[0]["CityRel"]);
                    txtDistrictname.Text = Convert.ToString(dt.Rows[0]["DistrictRel"]);
                    txtPinCode.Text = Convert.ToString(dt.Rows[0]["PostCodeRel"]);
                    ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["StateCodeRel"]);
                    ddlCountryCode.SelectedValue = Convert.ToString(dt.Rows[0]["CntryCodeRel"]);

                    //Commented by kalyani Hande start
                    //if (dt.Rows[0]["CnctTypeRel1"].ToString() != "")
                    //{
                    //    chkLocalAddress.Checked = true;
                    //}
                    //else
                    //{
                    //    chkLocalAddress.Checked = false;
                    //}
                    //Commented by kalyani Hande end

                    //ddlAddressType1.SelectedValue = Convert.ToString(dt.Rows[0]["AdrTypeRel1"]);
                    txtLocAddLine1.Text = Convert.ToString(dt.Rows[0]["Adr1Rel1"]);
                    txtLocAddLine2.Text = Convert.ToString(dt.Rows[0]["Adr2Rel1"]);
                    txtLocAddLine3.Text = Convert.ToString(dt.Rows[0]["Adr3Rel1"]);
                    txtCity1.Text = Convert.ToString(dt.Rows[0]["CityRel1"]);
                    txtDistrict1.Text = Convert.ToString(dt.Rows[0]["DistrictRel1"]);
                    ddlPinCode1.Text = Convert.ToString(dt.Rows[0]["PostCodeRel1"]);
                    ddlState1.SelectedValue = Convert.ToString(dt.Rows[0]["StateCodeRel1"]);
                    ddlCountryCode1.SelectedValue = Convert.ToString(dt.Rows[0]["CntryCodeRel1"]);


                    ViewState["strIdName"] = Convert.ToString(dt.Rows[0]["IdName"]);
                    ViewState["strIdNumber"] = Convert.ToString(dt.Rows[0]["IdNumber"]);
                    ViewState["strIdExpDate"] = Convert.ToString(dt.Rows[0]["IdExpDate"]);

                    if (ddlProofIdentity1.SelectedValue == "Z")
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
                htParam.Add("@kycno", Request.QueryString["kycno"]);
                htParam.Add("@batchid", Request.QueryString["batchid"]);
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
                    cboGender.SelectedValue = Convert.ToString(dtNew.Rows[idr]["GenderRel"]);




                    //ddlIsoCountryCodeOthr.SelectedValue = Convert.ToString(dtNew.Rows[idr]["ISOCountryCodeRel"]);
                    if (dtNew.Rows[idr]["ISOCountryCodeRel"].ToString() != "")
                    {
                        ddlIsoCountryCodeOthr.SelectedValue = Convert.ToString(dtNew.Rows[idr]["ISOCountryCodeRel"]);
                    }

                    ddlProofOfAddress.SelectedValue = Convert.ToString(dtNew.Rows[idr]["IdType"]);
                    ViewState["strIdName"] = Convert.ToString(dtNew.Rows[idr]["IdName"]);
                    ViewState["strIdNumber"] = Convert.ToString(dtNew.Rows[idr]["IdNumber"]);
                    ViewState["strIdExpDate"] = Convert.ToString(dtNew.Rows[idr]["IdExpDate"]);

                    if (ddlProofIdentity1.SelectedValue == "Z")
                    {
                        txtPassOthr.Text = Convert.ToString(dtNew.Rows[idr]["IdNumber"]);
                        txtPassNo.Text = Convert.ToString(dtNew.Rows[idr]["IdName"]);
                    }
                    else
                    {
                        txtPassNo.Text = Convert.ToString(dtNew.Rows[idr]["IdNumber"]);
                        txtPassExpDate.Text = Convert.ToString(dtNew.Rows[idr]["IdExpDate"]);
                    }

                    //Commented by Kalyani Hande start
                    //if (Convert.ToString(dtNew.Rows[idr]["CnctTypeRel"]) == "P1")
                    //{
                    //    chkPerAddress.Checked = true;
                    //}
                    //else
                    //{
                    //    chkPerAddress.Checked = false;
                    //}
                    //Commented by Kalyani Hande end

                    //ddlAddressType.SelectedValue = Convert.ToString(dtNew.Rows[idr]["AdrTypeRel"].ToString().Trim());
                    ddlProofOfAddress.SelectedValue = Convert.ToString(dtNew.Rows[idr]["AdrProfRel"].ToString().Trim());
                    txtAddressLine1.Text = Convert.ToString(dtNew.Rows[idr]["Adr1Rel"]);
                    txtAddressLine2.Text = Convert.ToString(dtNew.Rows[idr]["Adr2Rel"]);
                    txtAddressLine3.Text = Convert.ToString(dtNew.Rows[idr]["Adr3Rel"]);
                    txtCity.Text = Convert.ToString(dtNew.Rows[idr]["CityRel"]);
                    txtDistrictname.Text = Convert.ToString(dtNew.Rows[idr]["DistrictRel"]);
                    txtPinCode.Text = Convert.ToString(dtNew.Rows[idr]["PostCodeRel"]);
                    ddlState.SelectedValue = Convert.ToString(dtNew.Rows[idr]["StateCodeRel"]);
                    ddlCountryCode.SelectedValue = Convert.ToString(dtNew.Rows[idr]["CntryCodeRel"]);
                    ddlCountryCode.Enabled = false;
                    //Commented by kalyani Hande start
                    //if (dtNew.Rows[idr]["CnctTypeRel1"].ToString() != "" && dtNew.Rows[idr]["PostCodeRel1"].ToString() != "")
                    //{
                    //    chkLocalAddress.Checked = true;
                    //}
                    //else
                    //{
                    //    chkLocalAddress.Checked = false;
                    //}
                    //Commented by kalyani Hande end

                    //ddlAddressType1.SelectedValue = Convert.ToString(dtNew.Rows[idr]["AdrTypeRel1"].ToString().Trim());
                    txtLocAddLine1.Text = Convert.ToString(dtNew.Rows[idr]["Adr1Rel1"]);
                    txtLocAddLine2.Text = Convert.ToString(dtNew.Rows[idr]["Adr2Rel1"]);
                    txtLocAddLine3.Text = Convert.ToString(dtNew.Rows[idr]["Adr3Rel1"]);
                    txtCity1.Text = Convert.ToString(dtNew.Rows[idr]["CityRel1"]);
                    txtDistrict1.Text = Convert.ToString(dtNew.Rows[idr]["DistrictRel1"]);
                    ddlPinCode1.Text = Convert.ToString(dtNew.Rows[idr]["PostCodeRel1"]);
                    ddlState1.SelectedValue = Convert.ToString(dtNew.Rows[idr]["StateCodeRel1"]);
                    ddlCountryCode1.SelectedValue = Convert.ToString(dtNew.Rows[idr]["CntryCodeRel1"]);


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

                    //if (ddlProofIdentity1.SelectedItem.Text == ddlProofOfAddress.SelectedItem.Text)
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


                    //ViewState["DtAdd"] = dtNewAddRel;
                }
                else
                {

                    //dtNewAddRel.Rows[0].Delete();
                    //dtNewAddRel.AcceptChanges();

                    txtRefNumber.Text = Convert.ToString(dt.Rows[0]["FiRefNo"]);
                    txtRelRefNumber.Text = Request.QueryString["RelRefNo"].ToString();
                    //txtRelRefNumber.Text = Convert.ToString(dt.Rows[0]["RelRefNo"]);
                    txtKYCNum.Text = Convert.ToString(dt.Rows[0]["RelatedPrsnKYCNo"]);
                    ddlRelType.SelectedValue = Convert.ToString(dt.Rows[0]["RelationType"]);
                    ddlRelType.Enabled = false;
                    if (ddlRelType.SelectedItem.Text == "Director" || ddlRelType.SelectedValue == "04")
                    {
                        ViewState["txtNum"] = Convert.ToString(dt.Rows[0]["TaxIDNumberRel"]);
                        // txtNum.Text = Convert.ToString(dt.Rows[0]["TaxIDNumberRel"]);
                        txtNum.Visible = true;
                    }
                    else if (true)
                    {
                        ViewState["txtNum"] = Convert.ToString(dt.Rows[0]["RelPersTypDesc"]);
                        // txtNum.Text = Convert.ToString(dt.Rows[0]["RelPersTypDesc"]);
                        txtNum.Visible = true;
                    }
                    else
                    {
                        txtNum.Text = "";
                        txtNum.Visible = false;
                    }
                    txtNum.Text = ViewState["txtNum"].ToString();
                    txtNum.Enabled = false;
                    cboTitle.SelectedValue = Convert.ToString(dt.Rows[0]["PrefixRel"]);
                    txtGivenName.Text = Convert.ToString(dt.Rows[0]["FNameRel"]);
                    txtMiddleName.Text = Convert.ToString(dt.Rows[0]["MNameRel"]);
                    txtLastName.Text = Convert.ToString(dt.Rows[0]["LNameRel"]);
                    cboTitle1.SelectedValue = Convert.ToString(dt.Rows[0]["MaidPrefixRel"]);
                    txtGivenName1.Text = Convert.ToString(dt.Rows[0]["MaidFNameRel"]);
                    txtMiddleName1.Text = Convert.ToString(dt.Rows[0]["MaidMNameRel"]);
                    txtLastName1.Text = Convert.ToString(dt.Rows[0]["MaidLNameRel"]);
                    if (Convert.ToString(dt.Rows[0]["FSFlagRel"]) == "01" || Convert.ToString(dt.Rows[0]["FSFlagRel"]) == "F")
                    {
                        rbtFS.SelectedValue = "F";
                    }
                    else if (Convert.ToString(dt.Rows[0]["FSFlagRel"]) == "")
                    {
                        rbtFS.Items[0].Selected = false;
                        rbtFS.Items[1].Selected = false;
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
                    cboGender.SelectedValue = Convert.ToString(dt.Rows[0]["GenderRel"]);
                    ddlNationality.SelectedValue = Convert.ToString(dt.Rows[0]["CitizenshipRel"]);
                    ddlNationality.Enabled = false;
                    txtPanNo.Text = Convert.ToString(dt.Rows[0]["RelPan"]);
                    txtPanNo.Enabled = false;
                    if (Convert.ToString(dt.Rows[0]["Form60Flag"]) == "Y")
                    {
                        chkPanForm.Checked = true; chkPanForm.Enabled = false;
                        txtPanNo.Text = "Applied For";
                        txtPanNo.Enabled = false;
                    }
                    else { chkPanForm.Checked = false; chkPanForm.Enabled = false; }
                    ddlIsoCountryCodeOthr.SelectedValue = Convert.ToString(dt.Rows[0]["ISOCountryCodeRel"]);

                    ddlProofOfAddress.SelectedValue = Convert.ToString(dt.Rows[0]["IdType"]);
                    if (Convert.ToString(dt.Rows[0]["IdType"]) == "")
                    {
                        ddlProofOfAddress.SelectedValue = Convert.ToString(dt.Rows[0]["AddIdTypeRel"]);
                    }
                    ViewState["strPOIVal"] = Convert.ToString(dt.Rows[0]["IdNumber"]);
                    ViewState["strPOAVal"] = Convert.ToString(dt.Rows[0]["AddIdNumberRel"]);
                    //Commented by Kalyani Hande start
                    //if (Convert.ToString(dt.Rows[0]["CnctTypeRel"]) == "P1")
                    //{
                    //    chkPerAddress.Checked = true;
                    //}
                    //else
                    //{
                    //    chkPerAddress.Checked = false;
                    //}
                    //Commented by Kalyani Hande end

                    //ddlAddressType.SelectedValue = Convert.ToString(dt.Rows[0]["AdrTypeRel"]);
                    if (Convert.ToString(dt.Rows[0]["SameasCurrentAddresFlagM1"]) == "01")
                    {
                        chkCuurentAddress.Checked = true;
                        ddlProofOfAddress1.SelectedValue = ddlProofOfAddress.SelectedValue;
                        ddlProofOfAddress1.SelectedItem.Text = ddlProofOfAddress.SelectedItem.Text;
                        ViewState["strPOAVal"] = Convert.ToString(dt.Rows[0]["AddIdNumberRel"]);
                    }
                    else
                    {
                        ddlProofOfAddress1.SelectedValue = Convert.ToString(dt.Rows[0]["AddIdTypeRel"]);
                        // ddlProofOfAddress1.SelectedValue = Convert.ToString(dt.Rows[0]["AdrProfRel1"]);
                        ViewState["strPOAVal"] = Convert.ToString(dt.Rows[0]["AddIdNumberRel"]);
                    }
                    txtAddressLine1.Text = Convert.ToString(dt.Rows[0]["Adr1Rel"]);
                    txtAddressLine2.Text = Convert.ToString(dt.Rows[0]["Adr2Rel"]);
                    txtAddressLine3.Text = Convert.ToString(dt.Rows[0]["Adr3Rel"]);
                    txtCity.Text = Convert.ToString(dt.Rows[0]["CityRel"]);
                    txtDistrictname.Text = Convert.ToString(dt.Rows[0]["DistrictRel"]);
                    txtPinCode.Text = Convert.ToString(dt.Rows[0]["PostCodeRel"]);
                    ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["StateCodeRel"]);
                    ddlCountryCode.SelectedValue = Convert.ToString(dt.Rows[0]["CntryCodeRel"]);

                    ddlPinCode.SelectedValue = Convert.ToString(dt.Rows[0]["PostCodeRel"]);
                    ddlDocReceived.SelectedValue = Convert.ToString(dt.Rows[0]["RelkycCertDoc"]);
                    ddlPinCode01.SelectedValue = Convert.ToString(dt.Rows[0]["PostCodeRel1"]);
                    //Commented by kalyani Hande start
                    //if (dt.Rows[0]["CnctTypeRel1"].ToString() != "")
                    //{
                    //    chkLocalAddress.Checked = true;
                    //}
                    //else
                    //{
                    //    chkLocalAddress.Checked = false;
                    //}
                    //Commented by kalyani Hande end

                    txtTelOff.Text = Convert.ToString(dt.Rows[0]["RelOfficeTelSTDCode"]);
                    txtTelOff2.Text = Convert.ToString(dt.Rows[0]["RelOfficeTelNo"]);

                    txtTelRes.Text = Convert.ToString(dt.Rows[0]["RelTelSTDCode"]);
                    txtTelRes2.Text = Convert.ToString(dt.Rows[0]["RelTelNo"]);

                    txtMobile.Text = Convert.ToString(dt.Rows[0]["RelMobCode"]);
                    txtMobile2.Text = Convert.ToString(dt.Rows[0]["RelMobileNo"]);
                    txtemail.Text = Convert.ToString(dt.Rows[0]["RelEmailID"]);


                    txtRemarks.Text = Convert.ToString(dt.Rows[0]["Remarks"]);
                    //ddlAddressType1.SelectedValue = Convert.ToString(dt.Rows[0]["AdrTypeRel1"]);
                    txtLocAddLine1.Text = Convert.ToString(dt.Rows[0]["Adr1Rel1"]);
                    txtLocAddLine2.Text = Convert.ToString(dt.Rows[0]["Adr2Rel1"]);
                    txtLocAddLine3.Text = Convert.ToString(dt.Rows[0]["Adr3Rel1"]);
                    txtCity1.Text = Convert.ToString(dt.Rows[0]["CityRel1"]);
                    txtDistrict1.Text = Convert.ToString(dt.Rows[0]["DistrictRel1"]);
                    ddlPinCode1.Text = Convert.ToString(dt.Rows[0]["PostCodeRel1"]);
                    ddlState1.SelectedValue = Convert.ToString(dt.Rows[0]["StateCodeRel1"]);
                    ddlCountryCode1.SelectedValue = Convert.ToString(dt.Rows[0]["CntryCodeRel1"]);


                    ViewState["strIdName"] = Convert.ToString(dt.Rows[0]["IdName"]);
                    ViewState["strIdNumber"] = Convert.ToString(dt.Rows[0]["IdNumber"]);
                    ViewState["strIdExpDate"] = Convert.ToString(dt.Rows[0]["IdExpDate"]);

                    if (ddlProofIdentity1.SelectedValue == "Z")
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
                    //if (txtKYCNum.Text != "")
                    //{
                    //    ddlDocReceived.SelectedValue = "";
                    //}
                    //else
                    //{
                    //    ddlDocReceived.SelectedValue = Convert.ToString(dt.Rows[0]["RelkycCertDoc"]);
                    //}
                    //ddlDocReceived.SelectedValue = Convert.ToString(dt.Rows[0]["RelkycCertDoc"]);
                    txtPlace.Text = Convert.ToString(dt.Rows[0]["DecPlaceRel"]);
                    txtDate.Text = Convert.ToString(dt.Rows[0]["DecDateRel"]);
                    txtEmpName.Text = Convert.ToString(dt.Rows[0]["kycEmpNameRel"]);
                    txtEmpCode.Text = Convert.ToString(dt.Rows[0]["kycEmpCodeRel"]);
                    txtEmpDesignation.Text = Convert.ToString(dt.Rows[0]["kycEmpDesiRel"]);
                    txtEmpBranch.Text = Convert.ToString(dt.Rows[0]["kycEmpBranchRel"]);
                    txtInsName.Text = Convert.ToString(dt.Rows[0]["kycInstNameRel"]);
                    txtInsCode.Text = Convert.ToString(dt.Rows[0]["kycInstCodeRel"]);
                    txtDate3.Text = Convert.ToString(dt.Rows[0]["kycVerDateRel"]);
                    chkAppDeclare1.Checked = true;
                    chkAppDeclare3.Checked = true;

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
                    BlankControlsForKYCNum();
                    ddlRelType_SelectedIndexChanged(this, e);
                    ddlProofOfAddress1_SelectedIndexChanged(this, e);
                    ddlPinCode01_SelectedIndexChanged(this, e);
                    ddlProofOfAddress_SelectedIndexChanged(this, e);
                    ddlPinCode_SelectedIndexChanged(this, e);
                    ddlProofIdentity1_SelectedIndexChanged(this, e);
                    ddlRelType_SelectedIndexChanged(this, e);
                    DisableControls();
                    chkAppDeclare1.Enabled = false;
                    chkAppDeclare3.Enabled = false;
                }
                else if (txtKYCNum.Text.Length == 0)
                {

                    BlankControl();
                    ddlRelType_SelectedIndexChanged(this, e);
                    ddlProofOfAddress_SelectedIndexChanged(this, e);
                    ddlRelType_SelectedIndexChanged(this, e);
                    ddlProofIdentity1_SelectedIndexChanged(this, e);
                    ddlPinCode_SelectedIndexChanged(this, e);
                    ddlPinCode01_SelectedIndexChanged(this, e);
                    ddlProofOfAddress1_SelectedIndexChanged(this, e);
                    EnableControls();
                    BindAttestation();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('KYC Number should be 14 digits only.');", true);
                    //EnableControls();
                }
            }
            else
            {
                EnableControls();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "RemoveLoader('txtKYCNumLoader')", true);
        }

        #region METHOD "DisableControls"
        protected void DisableControls()
        {
            try
            {
                txtEmpDesignation.Enabled = false;
                txtEmpCode.Enabled = false;
                txtEmpName.Enabled = false;
                CheckBox1.Enabled = false;
                txtEmpBranch.Enabled = false;
                txtInsName.Enabled = false;
                txtInsCode.Enabled = false;
                // chkLocalAddress.Enabled = false; //Commented by kalyani Hande 
                chkCuurentAddress.Enabled = false;
                txtDate3.Enabled = false;
                ddlProofIdentity1.Enabled = false;
                //chkPerAddress.Enabled = false; //Commented by Kalyani Hande start
                //ddlAddressType.Enabled = false;
                ddlProofOfAddress.Enabled = false;
                txtAddressLine1.Enabled = false;
                txtAddressLine2.Enabled = false;
                txtAddressLine3.Enabled = false;
                txtCity.Enabled = false;
                ddlState.Enabled = false;
                btnShow.Enabled = false;
                ddlNationality.Enabled = false;
                //txtPinCode.Enabled = false;
                ddlCountryCode.Enabled = false;
                txtPlace.Enabled = false;
                ddlDocReceived.Enabled = false;
                rbtFS.Enabled = false;
                txtPanNo.Enabled = false;
                chkPanForm.Enabled = false;
                ddlDistrictname.Enabled = false;
                ddlPinCode.Enabled = false;
                ddlProofOfAddress1.Enabled = false;
                ddlDistrict1.Enabled = false;
                ddlPinCode01.Enabled = false;
                txtTelOff.Enabled = false;
                txtTelOff2.Enabled = false;
                txtTelRes.Enabled = false;
                txtTelRes2.Enabled = false;
                txtMobile.Enabled = false;
                txtMobile2.Enabled = false;
                txtemail.Enabled = false;
                txtRemarks.Enabled = false;
                ddlRelType.Enabled = true;

                //ddlAddressType1.Enabled = false;
                txtLocAddLine1.Enabled = false;
                txtLocAddLine2.Enabled = false;
                txtLocAddLine3.Enabled = false;
                txtCity1.Enabled = false;
                ddlState1.Enabled = false;
                //ddlPinCode1.Enabled = false;
                //txtDistrict1.Enabled = false;
                ddlCountryCode1.Enabled = false;
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
                txtDate.Enabled = false;
                txtDate.Text = "";
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

        #region METHOD "BlankControlsForKYCNum"
        protected void BlankControlsForKYCNum()
        {
            //ClearTextcntrl();
            CheckBox1.Checked = false;
            //chkLocalAddress.Checked = false; //Commented by kalyani Hande 
            chkCuurentAddress.Checked = false;
            ddlState1.SelectedIndex = 0;
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
            cboGender.SelectedIndex = 0;
            txtPanNo.Text = "";
            ddlProofIdentity1.SelectedIndex = 0;
            txtPassNo.Text = "";
            txtPassNoAdd.Text = "";
            txtPassExpDate.Text = "";
            ddlProofOfAddress.SelectedIndex = 0;
            txtPassExpDateAdd.Text = "";
            txtAddressLine1.Text = "";
            txtAddressLine2.Text = "";
            txtAddressLine3.Text = "";
            txtCity.Text = "";
            txtDistrictname.Text = "";
            txtPinCode.Text = "";
            ddlState.SelectedItem.Text = "";
            rbtFS.Items[0].Selected = false;
            rbtFS.Items[1].Selected = false;
            divIdProof.Visible = false;
            divAddProof.Visible = false;

            txtLocAddLine1.Text = "";
            txtLocAddLine2.Text = "";
            txtLocAddLine3.Text = "";
            txtCity1.Text = "";
            ddlState.SelectedIndex = 0;
            ddlPinCode1.Text = "";
            ddlPinCode.SelectedIndex = 0;
            ddlPinCode01.SelectedIndex = 0;

            txtDistrict1.Text = "";
            ddlCountryCode1.SelectedIndex = 0;


            ddlPinCode.SelectedIndex = 0;
            ddlCountryCode.SelectedIndex = 0;
            ddlNationality.SelectedIndex = 0;
            ddlState.Items.Insert(0, new ListItem("Select", string.Empty));
            ddlState1.Items.Insert(0, new ListItem("Select", string.Empty));
            ddlState.SelectedIndex = 0;
            ddlState1.SelectedIndex = 0;
            ddlDistrictname.Items.Insert(0, new ListItem("Select", string.Empty));
            ddlDistrictname.SelectedIndex = 0;
            ddlDistrict1.Items.Insert(0, new ListItem("Select", string.Empty));
            ddlProofOfAddress.SelectedIndex = 0;
            ddlProofOfAddress1.Items.Insert(0, new ListItem("Select", string.Empty));
            ddlProofOfAddress1.SelectedIndex = 0;

            txtTelOff.Text = "";
            txtTelOff2.Text = "";
            txtTelRes.Text = "";
            txtTelRes2.Text = "";
            txtMobile.Text = "";
            txtMobile2.Text = "";
            txtemail.Text = "";

            txtRemarks.Text = "";
            txtDate.Text = "";
            txtPlace.Text = "";
            ddlDocReceived.SelectedIndex = 0;
            txtEmpName.Text = "";
            txtEmpCode.Text = "";
            txtEmpDesignation.Text = "";
            txtEmpBranch.Text = "";
            txtDate3.Text = "";
            txtInsName.Text = "";
            txtInsCode.Text = "";
        }
        #endregion

        protected void BlankControl()
        {
            CheckBox1.Checked = false;
            //chkLocalAddress.Checked = false; //Commented by kalyani Hande 
            chkCuurentAddress.Checked = false;
            //ddlState1.Enabled = true; //commented by rutuja on 11may
            ddlState1.SelectedIndex = 0;
            //ddlProofOfAddress.Enabled = true; //commented by rutuja on 11may
            //txtRelRefNumber.Text = "";
            //txtRefNumber.Text = "";
            //txtKYCNum.Text = "";
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
            cboGender.SelectedIndex = 0;
            txtPanNo.Text = "";
            //ddlIsoCountry.SelectedIndex = 0;
            ddlProofIdentity1.SelectedIndex = 0;
            txtPassNo.Text = "";
            txtPassNoAdd.Text = "";
            txtPassExpDate.Text = "";
            //chkPerAddress.Checked = false;
            //chkPerAddress.Enabled = true;

            txtPassExpDateAdd.Text = "";
            txtAddressLine1.Text = "";
            txtAddressLine2.Text = "";
            txtAddressLine3.Text = "";
            txtCity.Text = "";
            txtDistrictname.Text = "";
            txtPinCode.Text = "";
            ddlState.SelectedItem.Text = "";
            ////  ddlCountryCode.SelectedIndex = 0;
            //chkDone.Enabled = false; //Commented by kalyani hande
            //rbtFS.Checked = false;
            //rbtFS.SelectedValue = "";
            rbtFS.Items[0].Selected = false;
            rbtFS.Items[1].Selected = false;
            divIdProof.Visible = false;
            divAddProof.Visible = false;

            txtLocAddLine1.Text = "";
            txtLocAddLine2.Text = "";
            txtLocAddLine3.Text = "";
            txtCity1.Text = "";
            ddlState.SelectedIndex = 0;
            ddlPinCode1.Text = "";
            ddlPinCode.SelectedIndex = 0;

            ddlPinCode01.SelectedIndex = 0;
            ddlProofOfAddress.SelectedIndex = 0;
            txtDistrict1.Text = "";
            ddlCountryCode1.SelectedIndex = 0;


            ddlPinCode.SelectedIndex = 0;
            ddlCountryCode.SelectedIndex = 0;
            ddlNationality.SelectedIndex = 0;
            ddlState.Items.Insert(0, new ListItem("Select", string.Empty));
            ddlState1.Items.Insert(0, new ListItem("Select", string.Empty));
            ddlState.SelectedIndex = 0;
            ddlState1.SelectedIndex = 0;
            ddlDistrictname.Items.Insert(0, new ListItem("Select", string.Empty));
            ddlDistrictname.SelectedIndex = 0;
            ddlDistrict1.Items.Insert(0, new ListItem("Select", string.Empty));
            //txtLocAddLine1.Enabled = true; //commented by rutuja on 11may
            //txtLocAddLine2.Enabled = true; //commented by rutuja on 11may
            //txtLocAddLine3.Enabled = true; //commented by rutuja on 11may
            //txtCity1.Enabled = true; //commented by rutuja on 11may
            //ddlState.Enabled = true; //commented by rutuja on 11may
            //ddlPinCode1.Enabled = true;
            //txtDistrict1.Enabled = true;
            //ddlCountryCode1.Enabled = true; //commented by rutuja on 11may
            ddlProofOfAddress.SelectedIndex = 0;
            ddlProofOfAddress1.Items.Insert(0, new ListItem("Select", string.Empty));
            ddlProofOfAddress1.SelectedIndex = 0;
            txtTelOff.Text = "";
            txtTelOff2.Text = "";
            txtTelRes.Text = "";
            txtTelRes2.Text = "";
            txtMobile.Text = "";
            txtMobile2.Text = "";
            txtemail.Text = "";

            txtRemarks.Text = "";
            txtDate.Text = "";
            txtPlace.Text = "";
            ddlDocReceived.SelectedIndex = 0;
            txtEmpName.Text = "";
            txtEmpCode.Text = "";
            txtEmpDesignation.Text = "";
            txtEmpBranch.Text = "";
            txtDate3.Text = DateTime.Today.ToString("dd-MM-yyyy");

        }

        #region METHOD "EnableControls"
        protected void EnableControls()
        {
            try
            {
                CheckBox1.Enabled = true;
                // chkLocalAddress.Enabled = true; //Commented by kalyani Hande 
                chkCuurentAddress.Enabled = true;
                ddlProofIdentity1.Enabled = true;
                //chkPerAddress.Enabled = true; //Commented by Kalyani Hande start
                //ddlAddressType.Enabled = true;
                ddlProofOfAddress.Enabled = true;
                txtAddressLine1.Enabled = true;
                txtAddressLine2.Enabled = true;
                txtAddressLine3.Enabled = true;
                txtCity.Enabled = true;
                ddlState.Enabled = true;
                btnShow.Enabled = true;
                //txtPinCode.Enabled = true;
                ddlCountryCode.Enabled = true;
                txtPlace.Enabled = true;
                ddlDocReceived.Enabled = true;
                txtEmpName.Enabled = true;
                txtEmpCode.Enabled = true;
                txtEmpDesignation.Enabled = true;
                txtEmpBranch.Enabled = true;
                txtInsName.Enabled = true;
                txtInsCode.Enabled = true;
                ddlNationality.Enabled = true;

                //ddlAddressType1.Enabled = true;
                txtLocAddLine1.Enabled = true;
                txtLocAddLine2.Enabled = true;
                txtLocAddLine3.Enabled = true;
                txtCity1.Enabled = true;
                ddlState1.Enabled = true;
                //ddlPinCode1.Enabled = true;
                //txtDistrict1.Enabled = true;
                ddlCountryCode1.Enabled = true;
                rbtFS.Enabled = true;
                txtPanNo.Enabled = true;
                chkPanForm.Enabled = true;
                ddlDistrictname.Enabled = true;
                ddlPinCode.Enabled = true;
                ddlProofOfAddress1.Enabled = true;
                ddlDistrict1.Enabled = true;
                ddlPinCode01.Enabled = true;
                txtTelOff.Enabled = true;
                txtTelOff2.Enabled = true;
                txtTelRes.Enabled = true;
                txtTelRes2.Enabled = true;
                txtMobile.Enabled = true;
                txtMobile2.Enabled = true;
                txtemail.Enabled = true;
                txtRemarks.Enabled = true;
                ddlRelType.Enabled = true;



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
                txtDate.Enabled = true;
                txtDate.Attributes.Remove("class");
                txtDate.Attributes.Add("class", " form-control hasDatepicker");
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
                    //ddlAddressType1.SelectedValue = ddlAddressType.SelectedValue;
                    ddlProofOfAddress1.SelectedItem.Text = ddlProofOfAddress.SelectedItem.Text;
                    ddlProofOfAddress1_SelectedIndexChanged(this, e);
                    txtPassNoAdd1.Text = txtPassNoAdd.Text;
                    txtPassNoAdd1.Enabled = false;
                    //ddlAddressType1.Enabled = false;
                    ddlProofOfAddress1.Enabled = false;
                    // chkLocalAddress.Checked = true; //Commented by kalyani Hande 
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
                    btnsearchddlPinCode1.Enabled = false;
                    ddlCountryCode1.SelectedValue = ddlCountryCode.SelectedValue;
                    ddlCountryCode1.Enabled = false;
                    txtDistrict1.Text = txtDistrictname.Text;

                    ddlState1.Enabled = false;
                    //ddlState1.SelectedValue = ddlState.SelectedValue;
                    ddlState1.SelectedItem.Text = ddlState.SelectedItem.Text;

                    //ddlPinCode01.SelectedItem.Text = ddlPinCode.SelectedItem.Text;
                    ddlPinCode01.SelectedValue = ddlPinCode.SelectedValue;
                    ddlPinCode01.Enabled = false;
                    ddlPinCode01_SelectedIndexChanged(this, e);
                    ddlDistrict1.Enabled = false;
                    ddlDistrict1.SelectedValue = ddlDistrictname.SelectedValue;
                    ddlDistrict1.SelectedItem.Text = ddlDistrictname.SelectedItem.Text;
                    //txtDistrict1.Enabled = false;
                    //ddlState1.SelectedValue = ddlState.SelectedValue;
                    ddlState1.Enabled = false;

                    txtState1.Enabled = false;

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
                    ddlCountryCode1.Enabled = false;
                    ddlCountryCode1.SelectedValue = ddlCountryCode.SelectedValue;
                    #region If selected Country !=India
                    common.ChngStatDistPinOnCountryCode(ddlState1, txtddlState1, ddlDistrict1, txtddlDistrict1, ddlPinCode01, txtddlPinCode01,
                        (ddlCountryCode1.SelectedValue == "IN") ? "Y" : "N");
                    #endregion
                    if (ddlCountryCode1.SelectedValue == "IN")
                    {
                        ddlPinCode1.Text = ddlPinCode.Text;
                        ddlPinCode1.Enabled = false;
                        ddlDistrict1.SelectedValue = ddlDistrictname.SelectedValue;
                        ddlDistrict1.Enabled = false;
                        ddlState1.SelectedValue = ddlState.SelectedValue;
                        ddlState1.Enabled = false;
                    }
                    else
                    {

                        txtddlPinCode01.Text = txtddlPinCode.Text;
                        txtddlPinCode01.Enabled = false;
                        txtddlDistrict1.Text = txtddlDistrictname.Text;
                        txtddlDistrict1.Enabled = false;
                        txtddlState1.Text = txtddlState.Text;
                        txtddlState1.Enabled = false;
                    }
                }
                else
                {
                    ddlPinCode01.SelectedIndex = 0;
                    ddlProofOfAddress1.Items.Insert(0, new ListItem("Select", string.Empty));
                    //ddlPinCode01.Items.Insert(0, new ListItem("Select", string.Empty));
                    ddlPinCode01.Enabled = true;
                    ddlDistrict1.SelectedIndex = 0;
                    ddlDistrict1.Items.Insert(0, new ListItem("Select", string.Empty));
                    ddlDistrict1.Enabled = true;
                    ddlDistrict1.SelectedIndex = 0;
                    ddlState1.SelectedIndex = 0;
                    ddlState1.Items.Insert(0, new ListItem("Select", string.Empty));
                    ddlState1.Enabled = true;
                    FillddlPageLoad();//after unchk to bind ddlProofOfAddress1
                    ddlProofOfAddress1.SelectedIndex = 0;
                    ddlProofOfAddress1_SelectedIndexChanged(this, e);
                    txtPassNoAdd1.Text = "";
                    txtPassNoAdd1.Enabled = true;
                    //ddlAddressType1.SelectedIndex = 0;
                    ddlProofOfAddress1.SelectedIndex = 0;
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
                    //ddlPinCode1.Enabled = true;
                    //ddlState1.SelectedItem.Text = "";
                    ddlState1.SelectedIndex = 0;
                    //added by ramesh on dated 21-05-2018
                    //ddlAddressType1.Enabled = true;
                    ddlProofOfAddress1.Enabled = true;
                    ddlState1.Enabled = true;
                    btnsearchddlPinCode1.Enabled = true;


                    txtState1.Enabled = true;
                    dvState1.Visible = true;
                    txtState1.Visible = false;
                    txtState1.Text = string.Empty;
                    #region If selected Country !=India
                    common.ChngStatDistPinOnCountryCode(ddlState1, txtddlState1, ddlDistrict1, txtddlDistrict1, ddlPinCode01, txtddlPinCode01,
                        (ddlCountryCode1.SelectedValue == "IN") ? "Y" : "N");
                    #endregion
                    if (ddlCountryCode1.SelectedValue == "IN")
                    {
                        ddlPinCode01.Enabled = true;
                        ddlPinCode01.SelectedIndex = 0;
                        ddlDistrict1.Items.Clear();
                        ddlDistrict1.Enabled = true;
                        ddlState1.Items.Clear();
                        ddlState1.Enabled = true;
                        ddlDistrict1.Items.Insert(0, new ListItem("Select", ""));
                        ddlState1.Items.Insert(0, new ListItem("Select", ""));
                        ddlDistrict1.SelectedIndex = 0;
                        ddlState1.SelectedIndex = 0;

                        ddlPinCode01.Visible = true;
                        ddlDistrict1.Visible = true;
                        ddlState1.Visible = true;

                        txtddlPinCode01.Visible = false;
                        txtddlDistrict1.Visible = false;
                        txtddlState1.Visible = false;
                    }
                    else
                    {
                        txtddlPinCode01.Visible = true;
                        txtddlDistrict1.Visible = true;
                        txtddlState1.Visible = true;

                        ddlPinCode01.Visible = false;
                        ddlDistrict1.Visible = false;
                        ddlState1.Visible = false;

                        txtddlPinCode01.Text = "";
                        txtddlPinCode01.Enabled = true;
                        txtddlDistrict1.Text = "";
                        txtddlDistrict1.Enabled = true;
                        txtddlState1.Text = "";
                        txtddlState1.Enabled = true;
                    }

                    //end
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "RemoveLoader('chkCuurentAddressLoader')", true);
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "chkCuurentAddress_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
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
                //ddlAddressType.Enabled = true;
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
                ddlState1.Enabled = true;
                ddlCountryCode1.Enabled = true;


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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "ChkUpdAddr_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
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

        #region METHOD "InitializeControl()"
        private void InitializeControls()
        {
            try
            {

                //lblAddressType1.Text = olng.GetItemDesc("lblAddressType");
                //lblProofOfAddress1.Text = olng.GetItemDesc("lblProofOfAddress");
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "InitializeControls", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
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
            htParam.Add("@LookupCode", "KAddrPrf");
            FillDropdowns("prc_getDDLLookUpData", htParam, ddlProofOfAddress1, "CKYCConnectionString", true);
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

                    ddlNationality.DataSource = dt;
                    ddlNationality.DataTextField = "Country_Desc";
                    ddlNationality.DataValueField = "Country_CODE";
                    ddlNationality.DataBind();
                    ddlNationality.Items.Insert(0, new ListItem("Select", string.Empty));

                    if (FlagPageTyp == "Indiviual" || FlagPageTyp == "01")
                    {
                        ddlNationality.Visible = false;
                        lblNationality.Visible = false;
                        lblNationalityImp.Visible = false;
                    }
                    else
                    {
                        ddlNationality.Visible = true;
                        lblNationality.Visible = true;
                        lblNationalityImp.Visible = true;
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
                if (ddlProofIdentity1.SelectedIndex == 0)
                {
                    divAddProof.Visible = false;
                }
                if (ddlProofIdentity1.SelectedIndex == 1)
                {
                    //divAddProof.Visible = true;
                    //ddlProofOfAddress.SelectedIndex = ddlProofIdentity1.SelectedItem.Text;
                    ddlProofOfAddress.ClearSelection();
                    string selected = ddlProofIdentity1.SelectedItem.Text;
                    //ddlProofOfAddress.SelectedItem.Text = ddlProofIdentity1.SelectedItem.Text;
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
                if (ddlProofIdentity1.SelectedIndex == 2)
                {
                    ddlProofOfAddress.ClearSelection();
                    string selected = ddlProofIdentity1.SelectedItem.Text;
                    //ddlProofOfAddress.SelectedItem.Text = ddlProofIdentity1.SelectedItem.Text;
                    if (ddlProofOfAddress.Items.FindByText(selected) != null)
                    {
                        ddlProofOfAddress.Items.FindByText(selected).Selected = true;
                    }

                    //divAddProof.Visible = true;
                    //ddlProofOfAddress.SelectedIndex = ddlProofIdentity1.SelectedIndex;
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
                if (ddlProofIdentity1.SelectedIndex == 3)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Proof of address can not be same as above proof of identity because proof of address does not contain " + ddlProofIdentity1.SelectedItem.Text.ToString() + ".');", true);
                    //divAddProof.Visible = true;
                    //ddlProofOfAddress.SelectedIndex = ddlProofIdentity1.SelectedIndex;
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

                if (ddlProofIdentity1.SelectedIndex == 4)
                {
                    //divAddProof.Visible = true;

                    //ddlProofOfAddress.SelectedIndex = ddlProofIdentity1.SelectedIndex;
                    ddlProofOfAddress.ClearSelection();
                    string selected = ddlProofIdentity1.SelectedItem.Text;

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
                if (ddlProofIdentity1.SelectedIndex == 5)
                {
                    //divAddProof.Visible = true;
                    //ddlProofOfAddress.SelectedIndex = ddlProofIdentity1.SelectedIndex;
                    ddlProofOfAddress.ClearSelection();
                    string selected = ddlProofIdentity1.SelectedItem.Text;

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
                if (ddlProofIdentity1.SelectedIndex == 6)
                {
                    //divAddProof.Visible = true;
                    //ddlProofOfAddress.SelectedIndex = ddlProofIdentity1.SelectedIndex;
                    ddlProofOfAddress.ClearSelection();
                    string selected = ddlProofIdentity1.SelectedItem.Text;

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
                if (ddlProofIdentity1.SelectedIndex == 7)
                {
                    //divAddProof.Visible = true;
                    divPassAdd.Visible = true;
                    ddlProofOfAddress.ClearSelection();
                    string selected = ddlProofIdentity1.SelectedItem.Text;

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
                if (ddlProofIdentity1.SelectedIndex == 8)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Proof of address can not be same as above proof of identity because proof of address does not contain " + ddlProofIdentity1.SelectedItem.Text.ToString() + ".');", true);
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

        protected void chkLocalAddress_CheckedChanged(object sender, EventArgs e)
        {
            //Commented by kalyani Hande start
            //if (chkLocalAddress.Checked == false)
            //{
            //    chkCuurentAddress.Checked = false;
            //    chkCuurentAddress_Checked(this, EventArgs.Empty);
            //}
            //Commented by kalyani Hande end
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
                    DtAdd.Rows[idr]["RelationType"] = ddlRelType.SelectedValue.Trim();

                    DtAdd.Rows[idr]["RelPersonAs"] = "I";

                    DtAdd.Rows[idr]["RelEntName"] = System.DBNull.Value;
                    DtAdd.Rows[idr]["RelDtofIncorporation"] = System.DBNull.Value;
                    DtAdd.Rows[idr]["RelDtofCommencementofbusi"] = System.DBNull.Value;
                    DtAdd.Rows[idr]["RelPlaceofIncorportation"] = System.DBNull.Value;
                    DtAdd.Rows[idr]["RelCountryofIncorporation"] = System.DBNull.Value;
                    DtAdd.Rows[idr]["RelCountryofResAsperTaxLaws"] = System.DBNull.Value;
                    DtAdd.Rows[idr]["RelIdType"] = System.DBNull.Value;
                    DtAdd.Rows[idr]["RelPAN"] = System.DBNull.Value;
                    DtAdd.Rows[idr]["RelTINIdNo"] = System.DBNull.Value;
                    DtAdd.Rows[idr]["RelTINCountry"] = System.DBNull.Value;

                    DtAdd.Rows[idr]["RelTelSTDCode"] = System.DBNull.Value;
                    DtAdd.Rows[idr]["RelTelNo"] = System.DBNull.Value;
                    DtAdd.Rows[idr]["RelOfficeTelSTDCode"] = System.DBNull.Value;
                    DtAdd.Rows[idr]["RelOfficeTelNo"] = System.DBNull.Value;
                    DtAdd.Rows[idr]["RelMobCode"] = System.DBNull.Value;
                    DtAdd.Rows[idr]["RelMobileNo"] = System.DBNull.Value;
                    DtAdd.Rows[idr]["RelFaxNoCode"] = System.DBNull.Value;
                    DtAdd.Rows[idr]["RelFaxNo"] = System.DBNull.Value;
                    DtAdd.Rows[idr]["RelEmailID"] = System.DBNull.Value;

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
                    DtAdd.Rows[idr]["MaritalStatusRel"] = "";
                    DtAdd.Rows[idr]["CitizenshipRel"] = "";
                    DtAdd.Rows[idr]["ResiStatusRel"] = "";

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


                    DtAdd.Rows[idr]["MaritalStatusReltxt"] = "";

                    DtAdd.Rows[idr]["CitizenshipReltxt"] = "";
                    //DtAdd.Rows[idr]["CitizenshipReltxt"] = "";

                    DtAdd.Rows[idr]["ResiStatusReltxt"] = "";


                    DtAdd.Rows[idr]["OccuTypeReltxt"] = "";

                    DtAdd.Rows[idr]["OccuTypeRel"] = "";

                    DtAdd.Rows[idr]["OccuSubTypeRel"] = "";


                    DtAdd.Rows[idr]["ResForTaxFlagRel"] = "";
                    DtAdd.Rows[idr]["ISOCountryCodeRel"] = "";
                    DtAdd.Rows[idr]["TaxIDNumberRel"] = "";
                    DtAdd.Rows[idr]["BirthCityRel"] = "";
                    DtAdd.Rows[idr]["ISOBirthPlaceCodeRel"] = "";
                    DtAdd.Rows[idr]["IdType"] = ddlProofOfAddress.SelectedValue;
                    if (ddlProofOfAddress.SelectedIndex == 1)
                    {
                        DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                        DtAdd.Rows[idr]["IdExpDate"] = txtPassExpDate.Text.Trim();
                        DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;

                    }
                    else if (ddlProofOfAddress.SelectedIndex == 2)
                    {
                        DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                        DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;

                    }
                    else if (ddlProofOfAddress.SelectedIndex == 3)
                    {
                        DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                        DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                    }
                    else if (ddlProofOfAddress.SelectedIndex == 4)
                    {
                        DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                        DtAdd.Rows[idr]["IdExpDate"] = txtPassExpDate.Text.Trim();
                        DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                    }
                    else if (ddlProofOfAddress.SelectedIndex == 5)
                    {
                        DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                        DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                    }
                    else if (ddlProofOfAddress.SelectedIndex == 6)
                    {
                        DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                        DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                    }
                    else if (ddlProofOfAddress.SelectedIndex == 7)
                    {
                        DtAdd.Rows[idr]["IdNumber"] = txtPassOthr.Text.Trim();
                        DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["IdName"] = txtPassNo.Text.Trim();
                    }
                    else if (ddlProofOfAddress.SelectedIndex == 8 || ddlProofOfAddress.SelectedIndex == 9)
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
                    //Commented by Kalyani Hande start
                    //if (chkPerAddress.Checked == true)
                    //{
                    //    DtAdd.Rows[idr]["CnctTypeRel"] = "P1";
                    //    DtAdd.Rows[idr]["AdrTypeRel"] = "";
                    //    DtAdd.Rows[idr]["AdrProfRel"] = ddlProofOfAddress.SelectedValue.Trim();
                    //    DtAdd.Rows[idr]["Adr1Rel"] = txtAddressLine1.Text.Trim();
                    //    DtAdd.Rows[idr]["Adr2Rel"] = txtAddressLine2.Text.Trim();
                    //    DtAdd.Rows[idr]["Adr3Rel"] = txtAddressLine3.Text.Trim();
                    //    DtAdd.Rows[idr]["CityRel"] = txtCity.Text.Trim();
                    //    DtAdd.Rows[idr]["DistrictRel"] = txtDistrictname.Text;
                    //    DtAdd.Rows[idr]["PostCodeRel"] = txtPinCode.Text;

                    //    if (ddlCountryCode.SelectedValue == "IN")
                    //    {
                    //        //htParam.Add("@PER_STATECODE", ddlState.SelectedValue);
                    //        DtAdd.Rows[idr]["StateCodeRel"] = ddlState.SelectedValue;
                    //    }
                    //    else
                    //    {
                    //        //htParam.Add("@PER_STATECODE", txtState.Text.Trim());
                    //        DtAdd.Rows[idr]["StateCodeRel"] = txtState.Text.Trim();
                    //    }

                    //    //DtAdd.Rows[idr]["StateCodeRel"] = ddlState.SelectedValue;
                    //    DtAdd.Rows[idr]["CntryCodeRel"] = ddlCountryCode.SelectedValue;
                    //}
                    //else
                    //{
                    //    DtAdd.Rows[idr]["CnctTypeRel"] = "";
                    //    DtAdd.Rows[idr]["AdrTypeRel"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["AdrProfRel"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["Adr1Rel"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["Adr2Rel"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["Adr3Rel"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["CityRel"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["DistrictRel"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["PostCodeRel"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["StateCodeRel"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["CntryCodeRel"] = System.DBNull.Value;
                    //}
                    //Commented by Kalyani Hande end

                    if (chkCuurentAddress.Checked == true)
                    {
                        DtAdd.Rows[idr]["SameasCurrentAddresFlagM1"] = "01";
                        //htParam.Add("@SameasCurrentAddresFlagM1", "01");
                    }
                    else
                    {
                        DtAdd.Rows[idr]["SameasCurrentAddresFlagM1"] = "";
                    }

                    //Commented by kalyani Hande start
                    //if (chkLocalAddress.Checked == true)
                    //{
                    //    DtAdd.Rows[idr]["CnctTypeRel1"] = "M1";
                    //    DtAdd.Rows[idr]["AdrTypeRel1"] = "";
                    //    //dataRow["AdrProfRel1"] = ddlProofOfAddress.SelectedValue;
                    //    DtAdd.Rows[idr]["Adr1Rel1"] = txtLocAddLine1.Text.Trim();
                    //    DtAdd.Rows[idr]["Adr2Rel1"] = txtLocAddLine2.Text.Trim();
                    //    DtAdd.Rows[idr]["Adr3Rel1"] = txtLocAddLine3.Text.Trim();
                    //    DtAdd.Rows[idr]["CityRel1"] = txtCity1.Text.Trim();
                    //    DtAdd.Rows[idr]["DistrictRel1"] = txtDistrict1.Text;
                    //    DtAdd.Rows[idr]["PostCodeRel1"] = ddlPinCode1.Text;

                    //    if (ddlCountryCode1.SelectedValue == "IN")
                    //    {
                    //        //htParam.Add("@PER_STATECODE", ddlState.SelectedValue);
                    //        DtAdd.Rows[idr]["StateCodeRel1"] = ddlState1.SelectedValue;
                    //    }
                    //    else
                    //    {
                    //        //htParam.Add("@PER_STATECODE", txtState.Text.Trim());
                    //        DtAdd.Rows[idr]["StateCodeRel1"] = txtState1.Text.Trim();
                    //    }

                    //    //DtAdd.Rows[idr]["StateCodeRel1"] = ddlState1.SelectedValue;
                    //    DtAdd.Rows[idr]["CntryCodeRel1"] = ddlCountryCode1.SelectedValue;
                    //}
                    //else
                    //{
                    //    DtAdd.Rows[idr]["CnctTypeRel1"] = "";
                    //    DtAdd.Rows[idr]["AdrTypeRel1"] = System.DBNull.Value;
                    //    //dataRow["AdrProfRel1"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["Adr1Rel1"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["Adr2Rel1"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["Adr3Rel1"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["CityRel1"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["DistrictRel1"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["PostCodeRel1"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["StateCodeRel1"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["CntryCodeRel1"] = System.DBNull.Value;
                    //}
                    //Commented by kalyani Hande end

                    DtAdd.Rows[idr]["SameasLocalAddressFlagJ1"] = "";

                    DtAdd.Rows[idr]["SameasLocalAddressFlagJ2"] = "";

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

                    DtAdd.Rows[idr]["AddIdTypeRel"] = ddlProofOfAddress.SelectedValue.Trim();

                    //Commented by Kalyani Hande start
                    //if (chkPerAddress.Checked == true)
                    //{
                    //    if (ddlProofOfAddress.SelectedIndex == 1)
                    //    {
                    //        DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                    //        DtAdd.Rows[idr]["AddIdExpDateRel"] = txtPassExpDateAdd.Text.Trim();
                    //        DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                    //    }

                    //    else if (ddlProofOfAddress.SelectedIndex == 2)
                    //    {
                    //        DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                    //        DtAdd.Rows[idr]["AddIdExpDateRel"] = txtPassExpDateAdd.Text.Trim();
                    //        DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                    //    }
                    //    else if (ddlProofOfAddress.SelectedIndex == 3)
                    //    {
                    //        DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                    //        DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                    //        DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                    //    }
                    //    else if (ddlProofOfAddress.SelectedIndex == 4)
                    //    {
                    //        DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                    //        DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                    //        DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                    //    }
                    //    else if (ddlProofOfAddress.SelectedIndex == 5)
                    //    {
                    //        DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                    //        DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                    //        DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                    //    }
                    //    else if (ddlProofOfAddress.SelectedIndex == 6)
                    //    {
                    //        DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassOthrAdd.Text.Trim();
                    //        DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                    //        DtAdd.Rows[idr]["AddIdNameRel"] = txtPassNoAdd.Text.Trim();
                    //    }

                    //    else
                    //    {
                    //        DtAdd.Rows[idr]["AddIdNumberRel"] = System.DBNull.Value;
                    //        DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                    //        DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                    //    }
                    //}
                    //else
                    //{
                    //    DtAdd.Rows[idr]["AddIdNumberRel"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                    //    DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                    //}
                    //Commented by Kalyani Hande end

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
                    ddlRelType_SelectedIndexChanged(this, e);
                    ddlProofOfAddress_SelectedIndexChanged(this, e);
                    ddlProofOfAddress1_SelectedIndexChanged(this, e);
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
                    Res = objVal.EntityRelatedPrsnValidation(ddlRelType, cboTitle, txtGivenName, txtMiddleName, txtLastName, cboTitle2, rbtFS,
                    txtGivenName2, txtMiddleName2, txtLastName2, cboTitle3, txtGivenName3, txtLastName3, txtDOB, cboGender, txtPanNo, ddlIsoCountryCodeOthr, ddlProofIdentity1, txtPassNo,
                    txtPassExpDate, txtPassOthr,
                     chkAppDeclare1, ddlProofOfAddress, txtAddressLine1, txtCity, txtPinCode, txtDate3, txtDate,
                    txtEmpName, txtEmpCode, txtEmpDesignation, txtEmpBranch, txtInsName, txtInsCode, txtPassExpDateAdd, txtPassNoAdd, txtPlace,
                    ddlDocReceived, ddlState, txtPassOthrAdd, txtLocAddLine1, txtCity1, ddlState1,
                    ddlPinCode1, ddlCountryCode1, ddlProofOfAddress1, txtPassNoAdd1, txtRelRefNumber
                    , ddlPinCode, chkCuurentAddress, ddlPinCode01, ddlNationality, txtNum,
                     txtTelOff2, txtTelOff, txtTelRes, txtTelRes2, txtMobile, txtMobile2, txtemail, ddlCountryCode, FlagPageTyp);//chkHigh, chkMedium, chkLow,ddlProofOfAddress1,

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
                        DtAdd.Rows[idr]["RelationType"] = ddlRelType.SelectedValue.Trim();

                        DtAdd.Rows[idr]["RelPersonAs"] = "I";

                        DtAdd.Rows[idr]["RelEntName"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["RelDtofIncorporation"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["RelDtofCommencementofbusi"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["RelPlaceofIncorportation"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["RelCountryofIncorporation"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["RelCountryofResAsperTaxLaws"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["RelIdType"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["RelPAN"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["RelTINIdNo"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["RelTINCountry"] = System.DBNull.Value;

                        DtAdd.Rows[idr]["RelTelSTDCode"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["RelTelNo"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["RelOfficeTelSTDCode"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["RelOfficeTelNo"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["RelMobCode"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["RelMobileNo"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["RelFaxNoCode"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["RelFaxNo"] = System.DBNull.Value;
                        DtAdd.Rows[idr]["RelEmailID"] = System.DBNull.Value;

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
                        DtAdd.Rows[idr]["MaritalStatusRel"] = "";
                        DtAdd.Rows[idr]["CitizenshipRel"] = "";
                        DtAdd.Rows[idr]["ResiStatusRel"] = "";

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


                        DtAdd.Rows[idr]["MaritalStatusReltxt"] = "";

                        DtAdd.Rows[idr]["CitizenshipReltxt"] = "";

                        DtAdd.Rows[idr]["ResiStatusReltxt"] = "";

                        DtAdd.Rows[idr]["OccuTypeReltxt"] = "";
                        DtAdd.Rows[idr]["OccuTypeRel"] = "";
                        DtAdd.Rows[idr]["OccuSubTypeRel"] = "";

                        DtAdd.Rows[idr]["ResForTaxFlagRel"] = "";
                        DtAdd.Rows[idr]["ISOCountryCodeRel"] = "";
                        DtAdd.Rows[idr]["TaxIDNumberRel"] = "";
                        DtAdd.Rows[idr]["BirthCityRel"] = "";
                        DtAdd.Rows[idr]["ISOBirthPlaceCodeRel"] = "";
                        DtAdd.Rows[idr]["IdType"] = ddlProofOfAddress.SelectedValue;
                        if (ddlProofIdentity1.SelectedIndex == 1)
                        {
                            DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                            DtAdd.Rows[idr]["IdExpDate"] = txtPassExpDate.Text.Trim();
                            DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;

                        }
                        else if (ddlProofIdentity1.SelectedIndex == 2)
                        {
                            DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                            DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;

                        }
                        else if (ddlProofIdentity1.SelectedIndex == 3)
                        {
                            DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                            DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                        }
                        else if (ddlProofIdentity1.SelectedIndex == 4)
                        {
                            DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                            DtAdd.Rows[idr]["IdExpDate"] = txtPassExpDate.Text.Trim();
                            DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                        }
                        else if (ddlProofIdentity1.SelectedIndex == 5)
                        {
                            DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                            DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                        }
                        else if (ddlProofIdentity1.SelectedIndex == 6)
                        {
                            DtAdd.Rows[idr]["IdNumber"] = txtPassNo.Text.Trim();
                            DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["IdName"] = System.DBNull.Value;
                        }
                        else if (ddlProofIdentity1.SelectedIndex == 7)
                        {
                            DtAdd.Rows[idr]["IdNumber"] = txtPassOthr.Text.Trim();
                            DtAdd.Rows[idr]["IdExpDate"] = System.DBNull.Value;
                            DtAdd.Rows[idr]["IdName"] = txtPassNo.Text.Trim();
                        }
                        else if (ddlProofIdentity1.SelectedIndex == 8 || ddlProofIdentity1.SelectedIndex == 9)
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

                        //Commented by Kalyani Hande start
                        //if (chkPerAddress.Checked == true)
                        //{
                        //    DtAdd.Rows[idr]["CnctTypeRel"] = "P1";
                        //    DtAdd.Rows[idr]["AdrTypeRel"] = "";
                        //    DtAdd.Rows[idr]["AdrProfRel"] = ddlProofOfAddress.SelectedValue.Trim();
                        //    DtAdd.Rows[idr]["Adr1Rel"] = txtAddressLine1.Text.Trim();
                        //    DtAdd.Rows[idr]["Adr2Rel"] = txtAddressLine2.Text.Trim();
                        //    DtAdd.Rows[idr]["Adr3Rel"] = txtAddressLine3.Text.Trim();
                        //    DtAdd.Rows[idr]["CityRel"] = txtCity.Text.Trim();
                        //    DtAdd.Rows[idr]["DistrictRel"] = txtDistrictname.Text;
                        //    DtAdd.Rows[idr]["PostCodeRel"] = txtPinCode.Text;

                        //    if (ddlCountryCode.SelectedValue == "IN")
                        //    {
                        //        //htParam.Add("@PER_STATECODE", ddlState.SelectedValue);
                        //        DtAdd.Rows[idr]["StateCodeRel"] = ddlState.SelectedValue;
                        //    }
                        //    else
                        //    {
                        //        //htParam.Add("@PER_STATECODE", txtState.Text.Trim());
                        //        DtAdd.Rows[idr]["StateCodeRel"] = txtState.Text.Trim();
                        //    }

                        //    //DtAdd.Rows[idr]["StateCodeRel"] = ddlState.SelectedValue;
                        //    DtAdd.Rows[idr]["CntryCodeRel"] = ddlCountryCode.SelectedValue;
                        //}
                        //else
                        //{
                        //    DtAdd.Rows[idr]["CnctTypeRel"] = "";
                        //    DtAdd.Rows[idr]["AdrTypeRel"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["AdrProfRel"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["Adr1Rel"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["Adr2Rel"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["Adr3Rel"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["CityRel"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["DistrictRel"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["PostCodeRel"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["StateCodeRel"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["CntryCodeRel"] = System.DBNull.Value;
                        //}
                        //Commented by Kalyani Hande end

                        if (chkCuurentAddress.Checked == true)
                        {
                            DtAdd.Rows[idr]["SameasCurrentAddresFlagM1"] = "01";
                            //htParam.Add("@SameasCurrentAddresFlagM1", "01");
                        }
                        else
                        {
                            DtAdd.Rows[idr]["SameasCurrentAddresFlagM1"] = "";
                        }

                        //Commented by kalyani Hande start
                        //if (chkLocalAddress.Checked == true)
                        //{
                        //    DtAdd.Rows[idr]["CnctTypeRel1"] = "M1";
                        //    DtAdd.Rows[idr]["AdrTypeRel1"] = "";
                        //    //dataRow["AdrProfRel1"] = ddlProofOfAddress.SelectedValue;
                        //    DtAdd.Rows[idr]["Adr1Rel1"] = txtLocAddLine1.Text.Trim();
                        //    DtAdd.Rows[idr]["Adr2Rel1"] = txtLocAddLine2.Text.Trim();
                        //    DtAdd.Rows[idr]["Adr3Rel1"] = txtLocAddLine3.Text.Trim();
                        //    DtAdd.Rows[idr]["CityRel1"] = txtCity1.Text.Trim();
                        //    DtAdd.Rows[idr]["DistrictRel1"] = txtDistrict1.Text;
                        //    DtAdd.Rows[idr]["PostCodeRel1"] = ddlPinCode1.Text;

                        //    if (ddlCountryCode1.SelectedValue == "IN")
                        //    {
                        //        //htParam.Add("@PER_STATECODE", ddlState.SelectedValue);
                        //        DtAdd.Rows[idr]["StateCodeRel1"] = ddlState1.SelectedValue;
                        //    }
                        //    else
                        //    {
                        //        //htParam.Add("@PER_STATECODE", txtState.Text.Trim());
                        //        DtAdd.Rows[idr]["StateCodeRel1"] = txtState1.Text.Trim();
                        //    }

                        //    //DtAdd.Rows[idr]["StateCodeRel1"] = ddlState1.SelectedValue;
                        //    DtAdd.Rows[idr]["CntryCodeRel1"] = ddlCountryCode1.SelectedValue;
                        //}
                        //else
                        //{
                        //    DtAdd.Rows[idr]["CnctTypeRel1"] = "";
                        //    DtAdd.Rows[idr]["AdrTypeRel1"] = System.DBNull.Value;
                        //    //dataRow["AdrProfRel1"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["Adr1Rel1"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["Adr2Rel1"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["Adr3Rel1"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["CityRel1"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["DistrictRel1"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["PostCodeRel1"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["StateCodeRel1"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["CntryCodeRel1"] = System.DBNull.Value;
                        //}                        //if (chkLocalAddress.Checked == true)
                        //{
                        //    DtAdd.Rows[idr]["CnctTypeRel1"] = "M1";
                        //    DtAdd.Rows[idr]["AdrTypeRel1"] = "";
                        //    //dataRow["AdrProfRel1"] = ddlProofOfAddress.SelectedValue;
                        //    DtAdd.Rows[idr]["Adr1Rel1"] = txtLocAddLine1.Text.Trim();
                        //    DtAdd.Rows[idr]["Adr2Rel1"] = txtLocAddLine2.Text.Trim();
                        //    DtAdd.Rows[idr]["Adr3Rel1"] = txtLocAddLine3.Text.Trim();
                        //    DtAdd.Rows[idr]["CityRel1"] = txtCity1.Text.Trim();
                        //    DtAdd.Rows[idr]["DistrictRel1"] = txtDistrict1.Text;
                        //    DtAdd.Rows[idr]["PostCodeRel1"] = ddlPinCode1.Text;

                        //    if (ddlCountryCode1.SelectedValue == "IN")
                        //    {
                        //        //htParam.Add("@PER_STATECODE", ddlState.SelectedValue);
                        //        DtAdd.Rows[idr]["StateCodeRel1"] = ddlState1.SelectedValue;
                        //    }
                        //    else
                        //    {
                        //        //htParam.Add("@PER_STATECODE", txtState.Text.Trim());
                        //        DtAdd.Rows[idr]["StateCodeRel1"] = txtState1.Text.Trim();
                        //    }

                        //    //DtAdd.Rows[idr]["StateCodeRel1"] = ddlState1.SelectedValue;
                        //    DtAdd.Rows[idr]["CntryCodeRel1"] = ddlCountryCode1.SelectedValue;
                        //}
                        //else
                        //{
                        //    DtAdd.Rows[idr]["CnctTypeRel1"] = "";
                        //    DtAdd.Rows[idr]["AdrTypeRel1"] = System.DBNull.Value;
                        //    //dataRow["AdrProfRel1"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["Adr1Rel1"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["Adr2Rel1"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["Adr3Rel1"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["CityRel1"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["DistrictRel1"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["PostCodeRel1"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["StateCodeRel1"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["CntryCodeRel1"] = System.DBNull.Value;
                        //}
                        //Commented by kalyani Hande end


                        DtAdd.Rows[idr]["SameasLocalAddressFlagJ1"] = "";


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

                        DtAdd.Rows[idr]["AddIdTypeRel"] = ddlProofOfAddress.SelectedValue.Trim();

                        //Commented by Kalyani Hande start
                        //if (chkPerAddress.Checked == true)
                        //{
                        //    if (ddlProofOfAddress.SelectedIndex == 1)
                        //    {
                        //        DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                        //        DtAdd.Rows[idr]["AddIdExpDateRel"] = txtPassExpDateAdd.Text.Trim();
                        //        DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                        //    }

                        //    else if (ddlProofOfAddress.SelectedIndex == 2)
                        //    {
                        //        DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                        //        DtAdd.Rows[idr]["AddIdExpDateRel"] = txtPassExpDateAdd.Text.Trim();
                        //        DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                        //    }
                        //    else if (ddlProofOfAddress.SelectedIndex == 3)
                        //    {
                        //        DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                        //        DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                        //        DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                        //    }
                        //    else if (ddlProofOfAddress.SelectedIndex == 4)
                        //    {
                        //        DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                        //        DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                        //        DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                        //    }
                        //    else if (ddlProofOfAddress.SelectedIndex == 5)
                        //    {
                        //        DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassNoAdd.Text.Trim();
                        //        DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                        //        DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                        //    }
                        //    else if (ddlProofOfAddress.SelectedIndex == 6)
                        //    {
                        //        DtAdd.Rows[idr]["AddIdNumberRel"] = txtPassOthrAdd.Text.Trim();
                        //        DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                        //        DtAdd.Rows[idr]["AddIdNameRel"] = txtPassNoAdd.Text.Trim();
                        //    }

                        //    else
                        //    {
                        //        DtAdd.Rows[idr]["AddIdNumberRel"] = System.DBNull.Value;
                        //        DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                        //        DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                        //    }
                        //}
                        //else
                        //{
                        //    DtAdd.Rows[idr]["AddIdNumberRel"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["AddIdExpDateRel"] = System.DBNull.Value;
                        //    DtAdd.Rows[idr]["AddIdNameRel"] = System.DBNull.Value;
                        //}
                        //Commented by Kalyani Hande end

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
                    ddlProofIdentity1, txtPassNo, txtPassExpDate, txtPassOthr,
                     chkAppDeclare1, ddlProofOfAddress, txtAddressLine1, txtCity, txtPinCode, txtDate3, txtDate,
                    txtEmpName, txtEmpCode, txtEmpDesignation, txtEmpBranch, txtInsName, txtInsCode, txtPassExpDateAdd, txtPassNoAdd, txtPlace,
                    ddlDocReceived, ddlState, txtPassOthrAdd, txtNum);//chkHigh, chkMedium, chkLow,

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
                        //dataRow["MaritalStatusRel"] = "";
                        //dataRow["CitizenshipRel"] = "";
                        //dataRow["ResiStatusRel"] = "";

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
                        //dataRow["TaxIDNumberRel"] = "";
                        //dataRow["BirthCityRel"] = "";
                        //dataRow["ISOBirthPlaceCodeRel"] = "";
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
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "AsyncConfirmYesNo();", true);
                        btnAdd.Enabled = true;
                        ClearTextcntrl();
                        ddlRelType_SelectedIndexChanged(this, e);

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
                htParam.Add("@kycno", kycno);
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
                objht.Add("@kycno", kycno);
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
                objht.Add("@kycno", kycno);
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
            #region If selected Country !=India
            common.ChngStatDistPinOnCountryCode(ddlState, txtddlState, ddlDistrictname, txtddlDistrictname, ddlPinCode, txtddlPinCode,
                ((ddlCountryCode.SelectedValue == "IN") ? "Y" : "N"));
            #endregion
            txtState.Text = "";
            ddlState.SelectedIndex = 0;
            txtDistrictname.Text = "";
            txtPinCode.Text = "";
            if (ddlCountryCode.SelectedValue == "IN")
            {
                dvState.Visible = true;
                txtState.Visible = false;
                //txtPinCode.Enabled = false;
                //txtDistrictname.Enabled = false;
                txtDistrictname.Attributes.Add("readonly", "readonly");
                txtPinCode.Attributes.Add("readonly", "readonly");
            }
            else
            {
                dvState.Visible = false;
                txtState.Visible = true;
                //txtPinCode.Enabled = true;
                //txtDistrictname.Enabled = true;
                txtDistrictname.Attributes.Remove("readonly");
                txtPinCode.Attributes.Remove("readonly");
                //txtDistrictname.Attributes.Remove("readonly", "readonly");
                //txtPinCode.Attributes.Remove("readonly", "readonly");
            }


        }

        protected void ddlCountryCode1_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region If selected Country !=India
            common.ChngStatDistPinOnCountryCode(ddlState1, txtddlState1, ddlDistrict1, txtddlDistrict1, ddlPinCode01, txtddlPinCode01,
                (ddlCountryCode1.SelectedValue == "IN") ? "Y" : "N");
            #endregion
            txtDistrict1.Text = "";
            ddlState1.SelectedIndex = 0;
            txtState1.Text = "";
            ddlPinCode1.Text = "";
            if (ddlCountryCode1.SelectedValue == "IN")
            {
                dvState1.Visible = true;
                txtState1.Visible = false;
                //ddlPinCode1.Enabled = false;
                //txtDistrict1.Enabled = false;
                txtDistrict1.Attributes.Add("readonly", "readonly");
                ddlPinCode1.Attributes.Add("readonly", "readonly");
            }
            else
            {
                dvState1.Visible = false;
                txtState1.Visible = true;
                //ddlPinCode1.Enabled = true;
                //txtDistrict1.Enabled = true;
                txtDistrict1.Attributes.Remove("readonly");
                ddlPinCode1.Attributes.Remove("readonly");
            }
        }

        protected void ChkUpdContact_Checked(object sender, EventArgs e)
        {
            try
            {
                txtTelOff.Enabled = true;
                txtTelRes.Enabled = true;
                txtMobile.Enabled = true;

                txtTelOff2.Enabled = true;
                txtTelRes2.Enabled = true;
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "ChkUpdContact_Checked", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        protected void FillDocumentReceived()
        {
            try
            {
                htParam.Clear();
                htParam.Add("@LookupCode", "DocReceived");
                htParam.Add("@ParamUsage", "KI");

                FillDropdowns("prc_getDDLLookUpData", htParam, ddlDocReceived, "CKYCConnectionString", true);
                if (FlagPageTyp == "Legal")
                {
                    ddlDocReceived.Items.Remove(ddlDocReceived.Items.FindByText("Video-based KYC"));
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

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCountryCode.SelectedValue = "IN";

        }

        #region DROPDOWN 'ddlPinCode' SELECTEDINDEXCHANGED EVENT
        protected void ddlPinCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string date;
                date = DateTime.Today.ToString("dd\\/MM\\/yyyy");
                //FillDistrictState(ddlPinCode, ddlDistrict, ddlState);
                ddlCountryCode.SelectedValue = "IN";

                //Commented by Kalyani Hande start
                //if (chkPerAddress.Checked == false)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please check current/permanent/overseas address details')", true);
                //    txtPinCode.Text = "";
                //    return;
                //}
                //Commented by Kalyani Hande end

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
                if (txtPinCode.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please enter permanent pin/post code')", true);
                    txtPinCode.Text = "";
                    return;
                }
                //chkPerAddress.Enabled = false;//Commented by Kalyani Hande start
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

        protected void ddlState1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCountryCode1.SelectedValue = "IN";
        }
        //Added By Shubham
        public void ValidatPOA_POI()
        {
            if (ddlProofOfAddress.SelectedItem.Text == ddlProofOfAddress1.SelectedItem.Text && ddlProofOfAddress1.SelectedItem.Text != "Select" && chkCuurentAddress.Checked != true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('POI and POA Should be Different.');", true);
                ddlProofOfAddress1.SelectedIndex = 0;
            }
            else
                if (ddlProofOfAddress.SelectedItem.Text == "Offline Verification of Aadhaar" && ddlProofOfAddress1.SelectedItem.Text == "Offline verification of Aadhaar")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('POI and POA Should be Different.');", true);
                ddlProofOfAddress1.SelectedIndex = 0;
            }
            else { }
        }
        //Ended By Shubham
        protected void ddlProofOfAddress1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AddLoader('ddlProofOfAddress1Loader')", true);
                txtPassOthrAdd1.Visible = false;
                txtPassNoAdd1.Visible = false;
                txtPassNoAdd1.Text = string.Empty;
                txtPassExpDateAdd1.Text = string.Empty;
                ValidatPOA_POI();
                txtPassNoAdd1.Attributes.Add("onkeypress", "");
                txtPassNoAdd1.Attributes.Remove("onblur");
                txtPassNoAdd1.Attributes.Add("style", "width:270px");
                lblPassportNoAdd.Text = "Document Number";
                lblPassportNoAdd1.Text = "Document Number";
                ddlDeemProfofAddr.Visible = false;
                if (ddlProofOfAddress1.SelectedItem.Text == "Select")
                {
                    divAddProof1.Visible = false;
                }

                else if (ddlProofOfAddress1.SelectedItem.Text == "Passport")
                {
                    divAddProof1.Visible = true;
                    //lblPassportNoAdd1.Text = "Passport Number";
                    txtPassOthrAdd1.Visible = false;
                    txtPassNoAdd1.Visible = true;
                    txtPassExpDateAdd1.Visible = false;
                    hidetxtPassExpDateAdd1.Visible = false;
                    txtPassOthrAdd1.Visible = false;
                    divPassAdd1.Visible = false;
                    txtPassNoAdd1.MaxLength = 8;
                    txtMaskCode1.Visible = false;
                    txtMaskCode1.Attributes.Add("width", "140px");
                    MaskCodeSpan1.Attributes.Add("class", "");
                    txtPassNoAdd1.Attributes.Remove("onblur");
                    txtPassNoAdd1.Attributes.Add("onblur", "return ValidationPassport(this)");
                    //txtPassNo.Attributes.Add("onblur", "return ValidatePassport(" + txtPassNo.Text.Trim().ToString() + ")");
                }
                else if (ddlProofOfAddress1.SelectedItem.Text == "Driving Licence")
                {
                    divAddProof1.Visible = true;
                    //lblPassportNoAdd1.Text = "Driving Licence";
                    txtPassOthrAdd1.Visible = false;
                    txtPassNoAdd1.Visible = true;
                    txtPassExpDateAdd1.Visible = false;
                    hidetxtPassExpDateAdd1.Visible = false;
                    txtPassOthrAdd1.Visible = false;
                    divPassAdd1.Visible = false;
                    txtPassNoAdd1.MaxLength = 15;
                    txtMaskCode1.Visible = false;
                    txtMaskCode1.Attributes.Add("width", "140px");
                    MaskCodeSpan1.Attributes.Add("class", "");
                    txtPassNoAdd1.Attributes.Remove("onblur");
                    txtPassNoAdd1.Attributes.Add("onblur", "return ValidationDriving(this)");
                }
                else if (ddlProofOfAddress1.SelectedItem.Text == "Proof of Possession of Aadhaar")
                {
                    divAddProof1.Visible = true;
                    //lblPassportNoAdd1.Text = "Proof of Possession of Aadhaar";
                    llPassExpDateAdd1.Visible = false;
                    txtPassExpDateAdd1.Visible = false;
                    hidetxtPassExpDateAdd1.Visible = false;
                    txtPassOthrAdd1.Visible = false;
                    divPassAdd1.Visible = false;
                    txtPassNoAdd1.Visible = true;
                    txtPassNoAdd1.MaxLength = 4;
                    txtPassNoAdd1.Text = "";
                    txtMaskCode1.Visible = true;
                    txtMaskCode1.Attributes.Add("style", "width:140px");
                    MaskCodeSpan1.Attributes.Add("class", "input-group-addon input-group-addon-tel");
                    txtPassNoAdd1.Attributes.Remove("onblur");
                    txtPassNoAdd1.Attributes.Add("style", "");
                    txtPassNoAdd1.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                    txtPassNoAdd1.Attributes.Add("onkeypress", "fncInputNumericValuesOnly()");
                }
                else if (ddlProofOfAddress1.SelectedItem.Text == "Voter ID Card")
                {
                    divAddProof1.Visible = true;
                    //lblPassportNoAdd1.Text = "Voter ID Card";
                    llPassExpDateAdd1.Visible = false;
                    txtPassExpDateAdd1.Visible = false;
                    hidetxtPassExpDateAdd1.Visible = false;
                    txtPassOthrAdd1.Visible = false;
                    divPassAdd1.Visible = false;
                    txtPassNoAdd1.Visible = true;
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd1.MaxLength = 15;
                    txtMaskCode1.Visible = false;
                    txtMaskCode1.Attributes.Add("width", "140px");
                    MaskCodeSpan1.Attributes.Add("class", "");
                    txtPassNoAdd1.Attributes.Remove("onblur");
                    txtPassNoAdd1.Attributes.Add("onblur", "return ValidationVoterId(this)");
                }
                else if (ddlProofOfAddress1.SelectedItem.Text == "NREGA Job Card")
                {
                    divAddProof1.Visible = true;
                    //lblPassportNoAdd1.Text = "NREGA Job Card";
                    llPassExpDateAdd1.Visible = false;
                    txtPassNoAdd1.Visible = true;
                    txtPassExpDateAdd1.Visible = false;
                    hidetxtPassExpDateAdd1.Visible = false;

                    txtPassOthrAdd1.Visible = false;
                    divPassAdd1.Visible = false;
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd1.MaxLength = 20;
                    txtMaskCode1.Visible = false;
                    txtMaskCode1.Attributes.Add("width", "140px");
                    MaskCodeSpan1.Attributes.Add("class", "");
                    txtPassNoAdd1.Attributes.Remove("onblur");
                }
                else if (ddlProofOfAddress1.SelectedItem.Text == "National Population Register Letter")
                {
                    divAddProof1.Visible = true;
                    //lblPassportNoAdd.Text = "Document Name";
                    llPassExpDateAdd.Text = "Identification Number";
                    //txtPassExpDateAdd.Visible = true;
                    //llPassExpDateAdd.Visible = true;
                    llPassExpDateAdd1.Visible = false;
                    txtPassNoAdd1.Visible = true;
                    txtPassExpDateAdd1.Visible = false;
                    hidetxtPassExpDateAdd1.Visible = false;

                    txtPassOthrAdd1.Visible = false;
                    divPassAdd1.Visible = false;
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd1.MaxLength = 20;
                    txtMaskCode1.Visible = false;
                    txtMaskCode1.Attributes.Add("width", "140px");
                    MaskCodeSpan1.Attributes.Add("class", "");
                    txtPassNoAdd1.Attributes.Remove("onblur");
                }
                else if (ddlProofOfAddress1.SelectedItem.Text == "E-KYC Authentication")
                {
                    divAddProof1.Visible = true;
                    lblPassportNoAdd.Text = "Document Name";
                    llPassExpDateAdd.Text = "Identification Number";
                    //txtPassExpDateAdd.Visible = true;
                    llPassExpDateAdd.Visible = true;
                    divPassAdd.Visible = true;
                    //llPassExpDateAdd.Visible = true;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = true;
                    txtPassNoAdd.Visible = true;
                    txtPassNoAdd1.Visible = true;
                    txtMaskCode1.Visible = true;
                    txtMaskCode1.Attributes.Add("style", "width:140px");
                    MaskCodeSpan1.Attributes.Add("class", "input-group-addon input-group-addon-tel");
                    txtPassNoAdd1.Attributes.Add("style", "");
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd1.MaxLength = 4;
                    txtPassNoAdd.Attributes.Remove("onblur");
                    txtPassNoAdd1.Attributes.Add("onblur", "return fnValidateEkyc(this)");
                    txtPassNoAdd1.Attributes.Add("onkeypress", "fncInputNumericValuesOnly()");
                }
                else if (ddlProofOfAddress1.SelectedItem.Text == "Offline verification of Aadhaar" || ddlProofOfAddress1.SelectedItem.Text == "Offline Verification of Aadhaar")
                {
                    divAddProof1.Visible = true;
                    lblPassportNoAdd.Text = "Document Name";
                    llPassExpDateAdd.Text = "Identification Number";
                    //txtPassExpDateAdd.Visible = true;
                    llPassExpDateAdd.Visible = false;
                    divPassAdd.Visible = true;
                    //llPassExpDateAdd.Visible = true;
                    txtPassExpDateAdd.Visible = false;
                    hidetxtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = true;
                    txtPassNoAdd.Visible = true;
                    txtPassNoAdd1.Visible = true;
                    txtMaskCode1.Visible = true;
                    txtMaskCode1.Attributes.Add("style", "width:140px");
                    MaskCodeSpan1.Attributes.Add("class", "input-group-addon input-group-addon-tel");
                    txtPassNoAdd1.Attributes.Add("style", "");
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd1.MaxLength = 4;
                    txtPassNoAdd.Attributes.Remove("onblur");
                    txtPassNoAdd1.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                    txtPassNoAdd1.Attributes.Add("onkeypress", "fncInputNumericValuesOnly()");
                }
                else if (ddlProofOfAddress1.SelectedItem.Text == "Deemed Proof of Address- Document Type Code")
                {
                    ddlDeemProfofAddr.Visible = true;
                    divAddProof1.Visible = true;
                    FillddlDeemAddrPrf();
                    lblPassportNoAdd1.Text = "Document Name";
                    //llPassExpDateAdd1.Text = "Identification Number";
                    llPassExpDateAdd1.Visible = false;
                    txtPassNoAdd1.Visible = false;
                    txtPassExpDateAdd1.Visible = false;
                    hidetxtPassExpDateAdd1.Visible = false;

                    txtPassOthrAdd1.Visible = false;
                    divPassAdd1.Visible = false;
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd1.MaxLength = 2;
                    txtMaskCode1.Visible = false;
                    txtMaskCode1.Attributes.Add("width", "140px");
                    MaskCodeSpan1.Attributes.Add("class", "");
                    txtPassNoAdd1.Attributes.Remove("onblur");
                }
                else if (ddlProofOfAddress1.SelectedItem.Text == "Self Declaration")
                {
                    divAddProof1.Visible = false;
                    lblPassportNoAdd1.Text = "Document Name";
                    //llPassExpDateAdd1.Text = "Identification Number";
                    llPassExpDateAdd1.Visible = false;
                    txtPassNoAdd1.Visible = true;
                    txtPassExpDateAdd1.Visible = false;
                    hidetxtPassExpDateAdd1.Visible = false;

                    txtPassOthrAdd1.Visible = false;
                    divPassAdd1.Visible = false;
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd1.MaxLength = 2;
                    txtMaskCode1.Visible = false;
                    txtMaskCode1.Attributes.Add("width", "140px");
                    MaskCodeSpan1.Attributes.Add("class", "");
                    txtPassNoAdd1.Attributes.Remove("onblur");
                    txtPassOthrAdd1.Visible = false;
                    txtPassNoAdd1.Visible = false;
                    txtPassNoAdd1.Text = string.Empty;
                    txtPassExpDateAdd1.Text = string.Empty;
                }
                else
                {
                    divAddProof1.Visible = true;
                    lblPassportNoAdd1.Text = "Document Name";
                    llPassExpDateAdd1.Text = "Identification Number";
                    //txtPassExpDateAdd.Visible = true;
                    //llPassExpDateAdd1.Visible = true;
                    divPassAdd1.Visible = true;
                    //llPassExpDateAdd1.Visible = true;
                    txtPassExpDateAdd1.Visible = false;
                    hidetxtPassExpDateAdd1.Visible = false;
                    txtPassOthrAdd1.Visible = true;
                    txtPassNoAdd1.Visible = true;
                    txtMaskCode1.Visible = false;
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd1.MaxLength = 15;
                    txtMaskCode1.Visible = false;
                    txtMaskCode1.Attributes.Add("width", "140px");
                    MaskCodeSpan1.Attributes.Add("class", "");
                    txtPassNoAdd1.Attributes.Remove("onblur");
                }
                lblPassportNoAdd1.Visible = true;
                llPassExpDateAdd.Visible = false;
                txtPassExpDateAdd1.Visible = false;
                txtPassOthrAdd.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "RemoveLoader('ddlProofOfAddress1Loader')", true);
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

        private void BindGrid(int rowcount)
        {

            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add(new System.Data.DataColumn("DOC ID NAME", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("DOC ID NUMBER", typeof(String)));

            if (ViewState["CurrentData"] != null)
            {
                for (int i = 0; i < rowcount + 1; i++)
                {
                    dt = (DataTable)ViewState["CurrentData"];
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.NewRow();
                        dr[0] = dt.Rows[0][0].ToString();
                    }
                }
                dr = dt.NewRow();
                dr[0] = ddlProofOfAddress.SelectedItem;
                dr[1] = txtPassNoAdd.Text;
                //dr[2] = TextBox3.Text;
                dt.Rows.Add(dr);
            }

            else
            {
                dr = dt.NewRow();
                dr[0] = ddlProofOfAddress.SelectedItem;
                dr[1] = txtPassNoAdd.Text;
                //dr[2] = TextBox3.Text;
                dt.Rows.Add(dr);
            }
            // If ViewState has a data then use the value as the DataSource
            if (ViewState["CurrentData"] != null)
            {
                GridView1.DataSource = (DataTable)ViewState["CurrentData"];
                GridView1.DataBind();
            }
            else
            {
                // Bind GridView with the initial data assocaited in the DataTable
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            // Store the DataTable in ViewState to retain the values
            ViewState["CurrentData"] = dt;
            SaveDocDtls(txtRelRefNumber.Text.ToString(), ddlProofIdentity1.SelectedValue.ToString(), ddlProofIdentity1.SelectedItem.Text.ToString(), txtPassNoAdd.Text.ToString());
        }
        public void SaveDocDtls(string FIREFNO_CKYC, string DocID, string DocDesc, string DocValue)
        {
            try
            {
                htParam.Clear();
                htParam.Add("@FIREFNO_CKYC", FIREFNO_CKYC);
                htParam.Add("@DocID", DocID);
                htParam.Add("@DocDesc", DocDesc);
                htParam.Add("@DocValue", DocValue);
                htParam.Add("@CreatedBy", strUserId);
                objDAL = new DataAccessLayer("CKYCConnectionString");
                objDAL.ExecuteNonQuery("PRC_InsTX_CKYC_TblRegDocVal", htParam);
            }
            catch (Exception ex)
            {
                objErr = new ErrorLog();
                objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "SaveDocDtls", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");

            }
        }
        protected void btnAddPOI_Click(object sender, EventArgs e)
        {
            //DataTable ddlCurr = ddlProofIdentity1;
            if (txtPassNoAdd.Text.ToString().Trim() != "")
            {
                if (ViewState["CurrentData"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentData"];
                    int count = dt.Rows.Count;
                    //BindGrid(count);
                }
                else
                {
                    //BindGrid(1);
                }
                ddlProofOfAddress.Items.RemoveAt(ddlProofIdentity1.SelectedIndex);
                ddlProofOfAddress.SelectedIndex = 0;
                ddlProofIdentity1.Focus();
                txtPassNo.Visible = false;
                txtPassNo.Text = string.Empty;
                divIdProof.Visible = false;
                lblPassportNo.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please Enter " + lblPassportNoAdd.Text.ToString() + "');", true);
            }
            //ddlProofIdentity1_SelectedIndexChanged(sender, e);
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
                    ddlPinCode.DataTextField = "PinCode";
                    ddlPinCode.DataBind();
                    ddlPinCode.Items.Insert(0, new ListItem("Select", string.Empty));
                    ddlPinCode01.DataSource = dt;
                    ddlPinCode01.DataTextField = "PinCode";
                    ddlPinCode01.DataBind();
                    ddlPinCode01.Items.Insert(0, new ListItem("Select", string.Empty));
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "PopulatePinCode", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dt = null;
            }
        }
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "FillDistrictState", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;
                dt = null;
            }
        }
        protected void ddlPinCode_SelectedIndexChanged1(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AddLoader('ddlPinCodeLoader')", true);
            ddlState.Items.Clear();
            ddlDistrictname.Items.Clear();
            FillDistrictState(ddlPinCode, ddlDistrictname, ddlState);
            txtPinCode.Text = ddlPinCode.SelectedItem.Text.ToString();
            txtDistrictname.Text = ddlDistrictname.SelectedItem.Text.ToString();
            ddlCountryCode.SelectedValue = "IN";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "RemoveLoader('ddlPinCodeLoader')", true);
        }

        protected void ddlPinCode01_SelectedIndexChanged(object sender, EventArgs e)
        {

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AddLoader('ddlPinCode01Loader')", true);
            ddlState1.Items.Clear();
            ddlDistrict1.Items.Clear();
            FillDistrictState(ddlPinCode01, ddlDistrict1, ddlState1);
            txtDistrict1.Text = ddlDistrict1.SelectedItem.Text.ToString();
            ddlPinCode1.Text = ddlPinCode.SelectedItem.Text.ToString();
            ddlCountryCode1.SelectedValue = "IN";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "RemoveLoader('ddlPinCode01Loader')", true);
        }

        protected void chkPanForm_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPanForm.Checked == true)
            {
                txtPanNo.Text = "";
                txtPanNo.Text = "Applied For";
                txtPanNo.Enabled = false;
            }
            else if (chkPanForm.Checked == false)
            {
                txtPanNo.Text = "";
                txtPanNo.Enabled = true;
                chkPanForm.Enabled = true;
            }
        }

        protected void txtPanNo_TextChanged(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AddLoader('txtPanNoLoader')", true);
            if (txtPanNo.Text != "")
            {
                chkPanForm.Enabled = false;
                txtPanNo.Enabled = true;
            }
            else if (txtPanNo.Text == "")
            {
                chkPanForm.Enabled = true;
                txtPanNo.Enabled = true;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "RemoveLoader('txtPanNoLoader')", true);
        }
        //Added by rutuja
        private void GetRelRefNo()
        {
            string str = "";

            objDAL = new DataAccessLayer("CKYCConnectionString");
            dt = new DataTable();
            string abc = txtRelRefNumber.Text;
            htParam.Clear();
            txtRefNumber.Text = Request.QueryString["FiRefNo"].ToString();
            htParam.Add("@RefNo", "RelPerRefNo");
            htParam.Add("@RelRefFlagPage", abc);
            dt = objDAL.GetDataTable("Prc_GetFIRefNoByInitialConfg", htParam);
            str = dt.Rows[0]["RelPerRefNo"].ToString();
            if (str == "F")
            {
                txtRelRefNumber.Text = "";
                txtRelRefNumber.Enabled = true;
            }
            else
            {
                txtRelRefNumber.Text = txtRefNumber.Text + '_' + str;
            }
        }
        //Added by Shubham 11/03/2021
        protected void FillddlDeemAddrPrf()
        {
            htParam.Clear();
            htParam.Add("@LookupCode", "KDeemProfofAddr");
            //FillDropdowns("prc_getDDLLookUpData", htParam, ddlDeemProfofAddr, "CKYCConnectionString", true);
            //htParam.Clear();
            dt = new DataTable();
            objDAL = new DataAccessLayer("CKYCConnectionString");
            dt.Clear();
            dt = objDAL.GetDataTable("prc_getDDLLookUpData", htParam, "CKYCConnectionString");
            if (dt.Rows.Count > 0)
            {
                ddlDeemProfofAddr.Items.Clear();
                ViewState["ddlDeemAddrPrf"] = dt;
                string min = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i][1]).Length >= 120)
                    {
                        min = dt.Rows[i][1].ToString().Substring(0, 100) + "...";
                        dt.Rows[i][1] = min.ToString();
                    }
                }
                ddlDeemProfofAddr.DataSource = dt;
                ddlDeemProfofAddr.DataTextField = "ParamDesc1";
                ddlDeemProfofAddr.DataValueField = "ParamValue";
                ddlDeemProfofAddr.DataBind();
                dt = objDAL.GetDataTable("prc_getDDLLookUpData", htParam, "CKYCConnectionString");
                for (int d = 0; d < ddlDeemProfofAddr.Items.Count; d++)
                {
                    ddlDeemProfofAddr.Items[d].Attributes.Add("title", dt.Rows[d][1].ToString());
                }
            }
            ddlDeemProfofAddr.Items.Insert(0, new ListItem("Select", ""));


        }

        protected void ddlDeemProfofAddr_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPassNoAdd1.Text = ddlDeemProfofAddr.SelectedValue.ToString();
        }

        protected void ddlRelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRelType.SelectedIndex == 0)
            {
                lblNum.Text = "";
                lblNum.Visible = false;
                lblNumImp.Visible = false;
                txtNum.Visible = false;
                txtNum.Attributes.Add("onkeypress", "");
            }
            else if (ddlRelType.SelectedItem.Text == "Director")
            {
                lblNum.Text = "Direct Identification Number";
                lblNum.Visible = true;
                lblNumImp.Visible = true;
                txtNum.Visible = true;
                txtNum.MaxLength = 8;
                txtNum.Text = "";
                txtNum.Attributes.Add("onkeypress", "fncInputNumericValuesOnly()");
            }
            else if (ddlRelType.SelectedItem.Text == "Other")
            {
                lblNum.Text = "Other Description";
                lblNum.Visible = true;
                lblNumImp.Visible = true;
                txtNum.Visible = true;
                txtNum.MaxLength = 150;
                txtNum.Text = "";
                txtNum.Attributes.Add("onkeypress", "");
            }
            else
            {
                lblNum.Text = "";
                lblNum.Visible = false;
                lblNumImp.Visible = false;
                txtNum.Visible = false;
                txtNum.Attributes.Add("onkeypress", "");
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "RemoveLoader('ddlRelTypeLoader')", true);
        }

        protected void txtNum_TextChanged(object sender, EventArgs e)
        {
            if (ddlRelType.SelectedValue == "04")
            {
                if (txtNum.Text.Length != 8)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Direct Identification Number must be 8 digits');", true);
                }
            }
        }

        protected void ddlNationality_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "RemoveLoader('ddlNationalityLoader')", true);
        }

        protected void ddlDocReceived_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "RemoveLoader('ddlDocReceivedLoader')", true);
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static string getHeaderbyIDMethod(string id)
        {
            try
            {
                string ID;
                ID = id;

                DataAccessLayer dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                //DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                System.Collections.Hashtable ht = new System.Collections.Hashtable();
                ht.Add("@SegCode", ID);
                if (FlagPageTyp == "Legal")
                {
                    ht.Add("@KYCCategory", "02");
                }
                else
                {
                    ht.Add("@KYCCategory", "01");
                }

                ds = dataAccessLayer.GetDataSet("Prc_CBFrm_GetFrmSegmentDtls", ht);
                string str = ds.Tables[0].Rows[0]["Desc"].ToString();
                // string JSONString = JSONConvert.SerializeObject(ds);
                return str;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }

        protected void gvDocDtls_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "View")
                {
                    //string Preview = e.CommandArgument.ToString().Trim();
                    string RefNo = "";// txtCKYCRefNo.Text.ToString();// = strRefNo;

                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                    Label lbldocName = (Label)row.FindControl("lbldocName");
                    Label lbldocTyp = (Label)row.FindControl("lbldocTyp");
                    Label lbldoccode = (Label)row.FindControl("lbldocCode");
                    ViewState["DocName"] = lbldocName.Text;
                    hdnRegRefNo.Value = RefNo.ToString();
                    string ShowImage = string.Empty;
                    string id = "";
                    DataAccessLayer dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                    dt = new DataTable();
                    dt = null;
                    Hashtable hTable = new Hashtable();
                    hTable.Clear();
                    if (FlagPageTyp == "Legal")
                    {
                        hTable.Add("@RegRefNo", RefNo);
                        hTable.Add("@flag", "2");
                        hTable.Add("@DocType", lbldocName.Text);
                        dt = dataAccessLayer.GetDataTable("Prc_GetDocNames", hTable);
                    }
                    else
                    {
                        hTable.Add("@RegRefNo", RefNo);
                        hTable.Add("@flag", (Request.QueryString["batchid"].ToString() != null ? "9" : "2"));
                        hTable.Add("@DocType", lbldocName.Text);
                        hTable.Add("@TypeofDoc", lbldocTyp.Text);
                        hTable.Add("@kycno", Request.QueryString["kycno"].ToString());
                        hTable.Add("@batchid", Request.QueryString["batchid"].ToString());
                        dt = dataAccessLayer.GetDataTable("Prc_GetDocNames", hTable);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        Byte[] bytes = (Byte[])dt.Rows[0]["IMAGE"];
                        int height, width, total;
                        string MstWidth, MstHeight, fileType = dt.Rows[0]["IMAGE_name"].ToString();
                        var base64String = "";
                        //PDFCode
                        if (fileType.Contains(".pdf"))
                        {
                            Label19.Text = lbldocName.Text;
                            width = 0;
                            height = 0;
                            base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        }
                        else
                        {
                            Label17.Text = lbldocName.Text;
                            System.Drawing.Image image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(bytes));
                            height = image.Height;
                            width = image.Width;
                        }

                        total = height * width;
                        ZinSize.Value = total.ToString();

                        MstWidth = dt.Rows[0]["ImgWidth"].ToString().Trim();
                        MstHeight = dt.Rows[0]["ImgHeight"].ToString().Trim();
                        ZoutSize.Value = dt.Rows[0]["MaxImgSize"].ToString().Trim();
                        id = dt.Rows[0]["SR_NO"].ToString().Trim();
                        string Doccode = dt.Rows[0]["DOC_CODE"].ToString().Trim();
                        string Imgsrc = "ImageCSharp.aspx?ImageID=" + id;
                        string Doctype = dt.Rows[0]["DOC_NAME"].ToString().Trim();
                        //PDFCode
                        if (fileType.Contains(".pdf"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "showpdf('" + base64String + "'," + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + ", 1);", true);
                            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "UpdateMsg", "showpdf('" + base64String + "'," + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + ", 1);", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "showimage(" + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + ", 1);", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "showimage(" + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + ", 1);", true);
                            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "UpdateMsg", "showimage(" + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + ", 1);", true);
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
                    objErr.LogErr(1, "CKYCRelatedPrsn.aspx.cs", "gvDocDtls_RowCommand", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        protected void btnView_Click(object sender, EventArgs e)
        {

        }
        #region Bind document
        private void BindGrid()
        {
            try
            {
                DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
                Hashtable objht = new Hashtable();
                DataTable objDt = new DataTable();
                #region photo shuffle start added by rachana on 01-07-2013
                objht.Clear();
                objht.Add("@RegNo", Request.QueryString["refno"].ToString());
                objht.Add("@RelRefNo", txtRelRefNumber.Text.ToString());
                objht.Add("@kycno", (txtKYCNum.Text.ToString() == "" ? kycno : txtKYCNum.Text.ToString()));
                objht.Add("@Batchid", Request.QueryString["batchid"].ToString());
                objDt = objDAL.GetDataTable("prc_GetDocType", objht);
                gvDocDtls.DataSource = objDt;
                gvDocDtls.DataBind();
                if (objDt.Rows.Count > 0)
                {
                    ViewState["DOC_NAME"] = objDt.Rows[0]["DOC_NAME"].ToString();
                    ViewState["DocNo"] = objDt.Rows[0]["DOC_CODE"].ToString();
                    ViewState["docCode"] = objDt.Rows[0]["SHORTCODE"].ToString();
                }
                #endregion
                objDt.Clear();
                objht.Clear();
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "USRMGMT");
                    throw ex;
                }
            }
            finally
            {
            }

        }
        #endregion
    }
}
