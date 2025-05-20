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
    public partial class CKYCLegalEntityQC : System.Web.UI.Page
    {
        #region Declare Veriables
        private MultilingualManager olng;
        Hashtable objht = new Hashtable();
        ErrorLog objErr;
        DataTable objDt = new DataTable();
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



        protected void Page_Load(object sender, EventArgs e)
        {

            if (HttpContext.Current.Session["UserId"] == null)
            {
                Response.Redirect("~/ErrorSession.aspx", true);
            }
            if (Session["AppID"] != null)
            {
                AppID = Convert.ToInt32(Session["AppID"]);
            }
            if (Session["UserId"] != null)
            {
                UserID = Session["UserId"].ToString();
            }
            //lngcode = Session["UserLangNum"].ToString();
            //Session["CarrierCode"] = '2';
            olng = new MultilingualManager("DefaultConn", "LegalEntityDtls.aspx", Session["UserLangNum"].ToString());
            strUserId = HttpContext.Current.Session["UserID"].ToString().Trim();
            if (!IsPostBack)
            {
                FillddlPageLoad();
                //PopulateddlCertifiecopy();
                BindGrid();
                BindGridImage();
                BindGridCntrlprsn();
                InitializeControls();
                FillRequiredDataForCKYC();
                if (ddlCertifiecopy.SelectedIndex == 0)
                {
                    divIdProof.Visible = false;

                }
                else if (ddlCertifiecopy.SelectedIndex == 1)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Certificate Of Incorporation No.";
                    txtPassNo.Visible = true;
                    txtPassNo.MaxLength = 15;
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
                    txtPassNo.MaxLength = 20;
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
                    txtPassNo.MaxLength = 10;
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
                    txtPassNo.MaxLength = 20;
                    //txtPassNo.Attributes.Remove("onblur");
                }
                else if (ddlCertifiecopy.SelectedIndex == 5)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Official Valid Documents No.";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                    txtPassNo.MaxLength = 12;
                    //txtPassNo.Focus();
                    txtPassNo.Text = "";
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
                    txtPassNo.MaxLength = 2;
                    //txtPassNo.Attributes.Remove("onblur");
                }
            }

        }

        #region BindGrid
        public void BindGrid()
        {
            try
            {
                string refno = Request.QueryString["refno"].ToString();
                if (refno != "")
                {

                    objDAL = new DataAccessLayer("CKYCConnectionString");
                    objht.Add("@ActionFlag", "1");
                    objht.Add("@RegRefNo", refno);

                    objds = objDAL.GetDataSet("getCKYCRelPrsnDtls", objht);
                    gvMemDtls.DataSource = objds.Tables[0];
                    gvMemDtls.DataBind();
                }
                else
                {
                    gvMemDtls.Visible = false;
                }
            }

            catch (Exception ex)
            {

            }
        }
        #endregion

        #region BindGridCntrlprsn
        public void BindGridCntrlprsn()
        {
            try
            {
                string refno = Request.QueryString["refno"].ToString();
                if (refno != "")
                {
                    objds.Clear();
                    objht.Clear();
                    objDAL = new DataAccessLayer("CKYCConnectionString");
                    objht.Add("@ActionFlag", "2");
                    objht.Add("@RegRefNo", refno);
                    objds = objDAL.GetDataSet("getCKYCRelPrsnDtls", objht);
                    gvCtrlPrson.DataSource = objds.Tables[0];
                    gvCtrlPrson.DataBind();
                }
                else
                {
                    gvCtrlPrson.Visible = false;
                }
            }

            catch (Exception ex)
            {

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
                lblNatureOfBuss.Text = olng.GetItemDesc("lblNatureOfBuss");

                lblKYCName.Text = olng.GetItemDesc("lblKYCName");
                lblDatOfInc.Text = olng.GetItemDesc("lblDatOfInc");
                lblDateOfCom.Text = olng.GetItemDesc("lblDateOfCom");
                lblPlaceOfIncor.Text = olng.GetItemDesc("lblPlaceOfIncor");
                lblCountrOfInc.Text = olng.GetItemDesc("lblCountrOfInc");
                lblCountryOfRsidens.Text = olng.GetItemDesc("lblCountryOfRsidens");
                lblIdentyType.Text = olng.GetItemDesc("lblIdentyType");
                lblTypeIdentiNo.Text = olng.GetItemDesc("lblTypeIdentiNo");
                lblTINCountry.Text = olng.GetItemDesc("lblTINCountry");
                lblPanNo.Text = olng.GetItemDesc("lblPanNo");
                lblNumberOfPerson.Text = olng.GetItemDesc("lblNumberOfPerson");
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
                lblAddressType.Text = olng.GetItemDesc("lblAddressType");
                lblProofOfAddress.Text = olng.GetItemDesc("lblProofOfAddress");
                lblAddressType1.Text = olng.GetItemDesc("lblAddressType");
                lblProofOfAddress1.Text = olng.GetItemDesc("lblProofOfAddress");
                lblAddressType2.Text = olng.GetItemDesc("lblAddressType");
                lblProofOfAddress2.Text = olng.GetItemDesc("lblProofOfAddress");
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
                lblpfofAddr2.Text = olng.GetItemDesc("lblpfofAddr2");
                lblDtlOfRtltpr.Text = olng.GetItemDesc("lblDtlOfRtltpr");
                lblDtlOfCtrlpr.Text = olng.GetItemDesc("lblDtlOfCtrlpr");
                lblRemarks.Text = olng.GetItemDesc("lblRemarks");
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

        #region METHOD "FillRequiredDataForCndPersonal"
        protected void FillRequiredDataForCKYC()
        {
            try
            {
                DataSet ds = new DataSet();
                objDAL = new DataAccessLayer("CKYCConnectionString");
                objht.Clear();
                objht.Add("@RegRefNo", Request.QueryString["refno"].ToString()); //Request.QueryString["refno"].ToString()
                objds = objDAL.GetDataSet("getSearchData_EntityDetails", objht);//getSearchData_Web

                if (Convert.ToString(objds.Tables[0].Rows[0]["AccType"]) == "F4")
                {
                    chkUSReport.Checked = true;
                }
                else
                {
                    chkUSReport.Checked = true;
                }

                txtKYCNumber.Text = Convert.ToString(objds.Tables[0].Rows[0]["KYC_NO"]);
                txtRefNumber.Text = Convert.ToString(objds.Tables[0].Rows[0]["RegRefNo"]);
                string acctype = Convert.ToString(objds.Tables[0].Rows[0]["AccType"]).Trim().ToString();
                objht.Clear();
                objht.Add("@LookupCode", "KEntAccHolTypOthRept");
                objht.Add("@value", acctype);
                objht.Add("@flag", "1");

                ds = objDAL.GetDataSet("prc_getDDLLookUpData", objht);
                ddlAccHolderType.SelectedItem.Text = Convert.ToString(ds.Tables[0].Rows[0]["ParamDesc1"]).Trim().ToString();
                txtKYCName.Text = Convert.ToString(objds.Tables[0].Rows[0]["EntityName"]);
                txtDatOfInc.Text = Convert.ToString(objds.Tables[0].Rows[0]["DatOfInc"]);
                txtDtOfCom.Text = Convert.ToString(objds.Tables[0].Rows[0]["DateOfCOB"]);
                txtPlaceOfInc.Text = Convert.ToString(objds.Tables[0].Rows[0]["PlaceOfInc"]);
                txtTypeIdentiNo.Text = Convert.ToString(objds.Tables[0].Rows[0]["Tin"]);
                txtPanNo.Text = Convert.ToString(objds.Tables[0].Rows[0]["Pan"]);
                ddlCountrOfInc.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["CountryOfInc"]);
                ddlCountryOfRsidens.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["CountryOfResidence"]);
                //ddlIdentyType.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["CountryOfResidence"]);
                ddlTINCountry.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["TINIssuingCountry"]);
                ddlCertifiecopy.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["POI"]);
                txtPassNo.Text = Convert.ToString(objds.Tables[0].Rows[0]["IDNO"]).Trim().ToString();
                //string POI = Convert.ToString(objds.Tables[0].Rows[0]["POI"]).Trim().ToString();
                //ds.Clear();
                //objht.Clear();
                //objht.Add("@LookupCode", "KEntPoI");
                //objht.Add("@value", POI);
                //objht.Add("@flag", "1");
                //ds = objDAL.GetDataSet("prc_getDDLLookUpData", objht);
                //ddlCertifiecopy.SelectedItem.Text = Convert.ToString(ds.Tables[0].Rows[0]["ParamDesc1"]).Trim().ToString();
                //ddlCertifiecopy.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[0]["ParamDesc1"]).Trim().ToString(), (ddlCertifiecopy.Items.Count).ToString()));
                //ddlCertifiecopy.SelectedIndex = ddlCertifiecopy.Items.Count-1;
                if (Convert.ToString(objds.Tables[0].Rows[0]["CnctType1"]) == "P1")
                {
                    chkPerAddress.Checked = true;
                }
                else
                {
                    chkPerAddress.Checked = false;
                }
                
                ddlAddressType.Text = Convert.ToString(objds.Tables[0].Rows[0]["PER_ADDTYPE"]).Trim().ToString();
                ddlProofOfAddress.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["PER_ADDPROOF"]);
                txtAddressLine1.Text = Convert.ToString(objds.Tables[0].Rows[0]["PER_ADDLINE1"]);
                txtAddressLine2.Text = Convert.ToString(objds.Tables[0].Rows[0]["PER_ADDLINE2"]);
                txtAddressLine3.Text = Convert.ToString(objds.Tables[0].Rows[0]["PER_ADDLINE3"]);
                //ddlIdentyType.
                txtCity.Text = Convert.ToString(objds.Tables[0].Rows[0]["PER_CITY"]);
                txtDistrictname.Text = Convert.ToString(objds.Tables[0].Rows[0]["PER_DISTRICT"]);
                txtPinCode.Text = Convert.ToString(objds.Tables[0].Rows[0]["PER_PIN"]);
                ddlState.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["PER_STATECODE"]);
                ddlCountryCode.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["PER_COUNTRY_CODE"]);

                ViewState["strAddIdName"] = Convert.ToString(objds.Tables[0].Rows[0]["AddIdName"]);
                ViewState["strAddIdNumber"] = Convert.ToString(objds.Tables[0].Rows[0]["AddIdNumber"]);
                ViewState["strAddIdExpDate"] = Convert.ToString(objds.Tables[0].Rows[0]["AddIdExpDate"]);

                if (ddlProofOfAddress.SelectedValue == "99")
                {
                    txtPassOthrAdd.Text = Convert.ToString(objds.Tables[0].Rows[0]["AddIdNumber"]);
                    txtPassNoAdd.Text = Convert.ToString(objds.Tables[0].Rows[0]["AddIdName"]);
                }
                else
                {
                    txtPassNoAdd.Text = Convert.ToString(objds.Tables[0].Rows[0]["AddIdNumber"]);
                    txtPassExpDateAdd.Text = Convert.ToString(objds.Tables[0].Rows[0]["AddIdExpDate"]);
                }

                if (Convert.ToString(objds.Tables[0].Rows[0]["CnctType2"]) == "M1" && Convert.ToString(objds.Tables[0].Rows[0]["CUR_PIN"]) != "")
                {
                    chkLocalAddress.Checked = true;
                }
                else
                {
                    chkLocalAddress.Checked = false;
                }
                chkCuurentAddress.Checked = true;
                ddlAddressType1.Text = Convert.ToString(objds.Tables[0].Rows[0]["CUR_ADDTYPE"]).Trim().ToString();
                ddlProofOfAddress1.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["FRN_ADDPROOF"]);
                txtLocAddLine1.Text = Convert.ToString(objds.Tables[0].Rows[0]["CUR_ADDLINE1"]);
                txtLocAddLine2.Text = Convert.ToString(objds.Tables[0].Rows[0]["CUR_ADDLINE2"]);
                txtLocAddLine3.Text = Convert.ToString(objds.Tables[0].Rows[0]["CUR_ADDLINE3"]);
                txtCity1.Text = Convert.ToString(objds.Tables[0].Rows[0]["CUR_CITY"]);
                txtDistrict1.Text = Convert.ToString(objds.Tables[0].Rows[0]["CUR_DISTRICT"]);
                ddlPinCode1.Text = Convert.ToString(objds.Tables[0].Rows[0]["CUR_PIN"]);
                ddlState1.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["CUR_STATECODE"]);
                ddlCountryCode1.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["CUR_COUNTRY_CODE"]);

                if (Convert.ToString(objds.Tables[0].Rows[0]["CnctType3"]) == "J1")
                {
                    chkAddResident.Checked = true;
                }
                else
                {
                    chkAddResident.Checked = false;
                }
                chkCurrentAdd.Checked = true;
                chkCorresAdd.Checked = true;
                chkAddCtrl.Checked = true;
                ddlAddressType2.Text = Convert.ToString(objds.Tables[0].Rows[0]["FRN_ADDTYPE"]).Trim().ToString();
                ddlProofOfAddress2.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["FRN_ADDPROOF"]);
                txtAddLine1.Text = Convert.ToString(objds.Tables[0].Rows[0]["FRN_ADDLINE1"]);
                txtAddLine2.Text = Convert.ToString(objds.Tables[0].Rows[0]["FRN_ADDLINE2"]);
                txtAddLine3.Text = Convert.ToString(objds.Tables[0].Rows[0]["FRN_ADDLINE3"]);
                txtCity2.Text = Convert.ToString(objds.Tables[0].Rows[0]["FRN_CITY"]);
                txtDistrict2.Text = Convert.ToString(objds.Tables[0].Rows[0]["FRN_DISTRICT"]);
                ddlPinCode2.Text = Convert.ToString(objds.Tables[0].Rows[0]["FRN_PIN"]);
                ddlState2.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["FRN_STATECODE"]);
                ddlIsoCountryCode.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["FRN_COUNTRY_CODE"]);
                txtFax1.Text = Convert.ToString(objds.Tables[0].Rows[0]["std_fax"]);
                txtTelOff2.Text = Convert.ToString(objds.Tables[0].Rows[0]["OFF_TELE"]);
                txtTelRes2.Text = Convert.ToString(objds.Tables[0].Rows[0]["RES_TEL"]);
                txtFax2.Text = Convert.ToString(objds.Tables[0].Rows[0]["FAX"]);
                txtMobile2.Text = Convert.ToString(objds.Tables[0].Rows[0]["MOBILE"]);
                txtemail.Text = Convert.ToString(objds.Tables[0].Rows[0]["EMAILID"]);
                chkAddRel.Checked = true;
                txtRemarks.Text = Convert.ToString(objds.Tables[0].Rows[0]["REMARK"]);
                chkAppDeclare1.Checked = true;
                chkAppDeclare2.Checked = true;
                chkAppDeclare3.Checked = true;
                txtPlace.Text = Convert.ToString(objds.Tables[0].Rows[0]["PLACE"]);
                txtDate.Text = Convert.ToString(objds.Tables[0].Rows[0]["APP_DATE"]);
                txtEmpName.Text = Convert.ToString(objds.Tables[0].Rows[0]["kycEmpName"]);
                txtEmpCode.Text = Convert.ToString(objds.Tables[0].Rows[0]["kycEmpCode"]);
                txtEmpDesignation.Text = Convert.ToString(objds.Tables[0].Rows[0]["kycEmpDesi"]);
                txtEmpBranch.Text = Convert.ToString(objds.Tables[0].Rows[0]["kycEmpBranch"]);
                txtInsName.Text = Convert.ToString(objds.Tables[0].Rows[0]["kycInstName"]);
                txtInsCode.Text = Convert.ToString(objds.Tables[0].Rows[0]["kycInstCode"]);
                txtDateKYCver.Text = Convert.ToString(objds.Tables[0].Rows[0]["kycVerDate"]);

                //if (Convert.ToString(objds.Tables[0].Rows[0]["kycCertDoc"]) == "01")
                //{
                //    chkCertifyCopy.Checked = true;
                //}
                //else
                //{
                //    chkCertifyCopy.Checked = false;
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
                    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    objErr.LogErr(AppID, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        protected void FillddlPageLoad()
        {
            //objht.Clear();
            //objht.Add("@LookupCode", "KEntConstTyp");
            //FillDropdowns("prc_getDDLLookUpData", objht, ddlNatureOfBuss, "CKYCConnectionString", true);
            objht.Clear();
            objht.Add("@LookupCode", "KEntAccHolTypOthRept");
            FillDropdowns("prc_getDDLLookUpData", objht, ddlAccHolderType, "CKYCConnectionString", true);

            objht.Clear();
            objht.Add("@LookupCode", "KEntIdentTyp");
            FillDropdowns("prc_getDDLLookUpData", objht, ddlIdentyType, "CKYCConnectionString", true);

            objht.Clear();
            objht.Add("@LookupCode", "KEntPoI");
            FillDropdowns("prc_getDDLLookUpData", objht, ddlCertifiecopy, "CKYCConnectionString", true);

            objht.Clear();
            objht.Add("@LookupCode", "KAddr");
            FillDropdowns("prc_getDDLLookUpData", objht, ddlAddressType, "CKYCConnectionString", true);
            FillDropdowns("prc_getDDLLookUpData", objht, ddlAddressType1, "CKYCConnectionString", true);
            FillDropdowns("prc_getDDLLookUpData", objht, ddlAddressType2, "CKYCConnectionString", true);

            objht.Clear();
            objht.Add("@LookupCode", "KEntPoA");
            FillDropdowns("prc_getDDLLookUpData", objht, ddlProofOfAddress, "CKYCConnectionString", true);
            FillDropdowns("prc_getDDLLookUpData", objht, ddlProofOfAddress1, "CKYCConnectionString", true);
            FillDropdowns("prc_getDDLLookUpData", objht, ddlProofOfAddress2, "CKYCConnectionString", true);

            objht.Clear();

        }

        private void PopulateddlCertifiecopy()
        {
            try
            {
                oCommonUtility.GetCKYC(ddlCertifiecopy, "KEntPoI");
                ddlCertifiecopy.Items.Insert(0, new ListItem("Select", ""));
                //oCommonUtility.GetCKYC(ddlProofRelPerson, "KId");
                //ddlProofRelPerson.Items.Insert(0, new ListItem("Select", ""));
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

        #region Bind document
        private void BindGridImage()
        {
            try
            {
                #region photo shuffle start added by rachana on 01-07-2013
                objht.Clear();
                objht.Add("@RegNo", Request.QueryString["refno"].ToString());
                objDt = objDAL.GetDataTable("prc_GetDocType", objht);
                //  objds = objDAL.GetDataSet("prc_GetDocType", objht, "CKYCConnectionString");

                if (objDt.Rows.Count > 0)
                {
                    ViewState["DOC_NAME"] = objDt.Rows[0]["DOC_NAME"].ToString();
                    ViewState["DocNo"] = objDt.Rows[0]["DOC_CODE"].ToString();
                    ViewState["docCode"] = objDt.Rows[0]["SHORTCODE"].ToString();
                }
                #endregion
                objDt.Clear();
                objht.Clear();
                objht.Add("@RegNo", Request.QueryString["refno"].ToString());
                objht.Add("@DOC_NAME", ViewState["DOC_NAME"]);
                objDt = objDAL.GetDataTable("prc_GetImage", objht);
                //objDt = objDAL.GetDataSet("prc_GetImage", objht, "CKYCConnectionString");
                GridImage.DataSource = objDt;
                GridImage.DataBind();
                ViewState["Img"] = objDt;
                ViewState["Img1"] = objDt;
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
                    objErr.LogErr(AppID, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
            finally
            {
                //con.Close();
            }

        }
        #endregion

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
                objht.Add("@RegNo", Request.QueryString["refno"].ToString());
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
                htParam.Add("@RegNo", Request.QueryString["refno"].ToString());
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
                objht.Add("@RegNo", Request.QueryString["refno"].ToString());
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
                objht.Add("@RegNo", Request.QueryString["refno"].ToString());
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

        #region 'btnUpdate_Click' Event
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string Res = string.Empty;

                //Res = objVal.Validation(chkNormal, chkSimplified, Chksmall, cboTitle, txtGivenName, txtLastName, rbtFS, cboTitle2, txtGivenName2, txtLastName2, cboTitle3, txtGivenName3, txtLastName3, txtDOB, cboGender,
                //       ddlMaritalStatus, ddlCitizenship, ddlResStatus, ddlOccupation, ddlOccuSubType, chkTick, ddlIsoCountryCode2, txtIDResTax, txtDOBRes, ddlIsoCountry, ddlProofIdentity,
                //       txtPassNo, txtPassExpDate, chkPerAddress, ddlAddressType, ddlProofOfAddress, txtAddressLine1, txtCity, ddlPinCode, chkLocalAddress, txtLocAddLine1,
                //       txtCity1, ddlPinCode1, chkAddResident, txtAddLine1, txtCity2, ddlIsoCountryCode, chkAppDeclare1);//txtPassOthr,


                if (Res.Equals(""))
                {
                    objht.Clear();
                    objht.Add("@RegRefNo", txtRefNumber.Text.ToString());
                    objht.Add("@KYC_NO", txtKYCNumber.Text.ToString());
                    objht.Add("@MODIFIEDBY", HttpContext.Current.Session["UserID"].ToString().Trim());
                    //objht.Add("@Usages", "W");

                    //commented by amruta for not use user master
                    if (Session["UserId"].ToString().Equals("checker"))
                    {
                        objht.Add("@Flag", "ChkrQC");
                    }
                    else
                    {
                        objht.Add("@Flag", "Chkr1QC");
                    }
                    objDAL = new DataAccessLayer("CKYCConnectionString");
                    objDt = objDAL.GetDataTable("prc_updateEntityQCkycdtls", objht);
                    if (Session["UserId"].ToString().Equals("checker"))
                    {
                        hdnUpdate.Value = "QC approved successfully.";
                    }
                    else
                    {
                        hdnUpdate.Value = "CKYC approved successfully.";
                    }


                    string strmsg;
                    strmsg = hdnUpdate.Value + "</br></br>Reference No: " + Request.QueryString["refno"].ToString().Trim();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + strmsg + "');", true);

                    //  ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                    btnUpdate.Enabled = false;
                    btnReject.Enabled = false;
                }
                else
                {
                    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "alert('" + Res + "')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + Res + "');", true);
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
                    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    objErr.LogErr(AppID, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }

        }

        #endregion

        #region rejection
        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                objht.Clear();
                // objds.Clear();
                objht.Add("@UserId", HttpContext.Current.Session["UserID"].ToString().Trim());
                objht.Add("@RefNo", Request.QueryString["refno"].ToString());
                objht.Add("@flag", "2");
                objht.Add("@QCRejectRemark", "");
                objDAL = new DataAccessLayer("CKYCConnectionString");
                objDt = objDAL.GetDataTable("prc_UpdQCStatus", objht);
                string strmsg = "QC rejected successfully" + "<br/><br/> Reference No: " + Request.QueryString["refno"].ToString().Trim();

                // ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + strmsg + "');", true);
                btnUpdate.Enabled = false;
                btnReject.Enabled = false;
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
        #endregion

        protected void lnkEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                string refno = Request.QueryString["refno"].ToString();
                if (e.CommandName != "")
                {
                    string RelRefNo = e.CommandName.ToString().Trim();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenRltdPrsnQCPage(" + RelRefNo + "," + refno + ");", true);
                }
            }
            catch
            {

            }
        }

        protected void lnkEdit_Command1(object sender, CommandEventArgs e)
        {
            try
            {
                string refno = Request.QueryString["refno"].ToString();
                if (e.CommandName != "")
                {
                    string RelRefNo = e.CommandName.ToString().Trim();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenCntrlPrsnQCPage(" + RelRefNo + "," + refno + ");", true);
                }
            }
            catch
            {

            }
        }
    }
}