using KMI.FRMWRK.DAL;
using KMI.FRMWRK.Multilingual;
using KMI.FRMWRK.Web.Application.CKYC.Common;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;


namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class CKYCView : System.Web.UI.Page
    {
        #region Declarations
         DataAccessLayer objDAL;
        //DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
        string PSTempRefNo = string.Empty;
        Hashtable hTable = new Hashtable();
        Hashtable htParam = new Hashtable();
        ErrorLog objErr = new ErrorLog();
        DataAccessLayer dataAccessLayer;
        DataTable dt;
        private string Message = string.Empty;
        string strUsrrole = string.Empty;
        private MultilingualManager olng;
        private string strUserLang;
        string strAppID = string.Empty;
        string strModuleID = string.Empty;
        string UserID = string.Empty;
        Guid obj = Guid.NewGuid();
        DataSet dsRel = new DataSet();
        int AppID;
        string strTempRefNo = string.Empty;
        string refno;
        CommonUtility oCommonUtility = new CommonUtility();
        CkycValidtion objVal = new CkycValidtion();


        String PREFIX, FNAME, MNAME, LNAME, FULLNAME, MAIDEN_PREFIX, MAIDEN_FNAME, MAIDEN_MNAME,
                      MAIDEN_LNAME, MAIDEN_FULLNAME, FATHERSPOUSE_FLAG, FATHER_PREFIX, FATHER_FNAME, FATHER_MNAME, FATHER_LNAME, FATHER_FULLNAME,
                      MOTHER_PREFIX, MOTHER_FNAME, MOTHER_MNAME, MOTHER_LNAME, MOTHER_FULLNAME, GENDER, MARITAL_STATUS, NATIONALITY, OCCUPATION, SUBOCCUPATION, DOB,
                      RESI_STATUS, JURI_FLAG, TAX_NUM, BIRTH_COUNTRY, BIRTH_PLACE, PERM_TYPE, PERM_LINE1, PERM_LINE2, PERM_LINE3, PERM_CITY, PERM_DIST,
                      PERM_STATE, PERM_COUNTRY, PERM_PIN, PERM_POA, PERM_POAOTHERS, PERM_CORRES_SAMEFLAG, CORRES_LINE1, CORRES_LINE2, CORRES_LINE3,
                      CORRES_CITY, CORRES_DIST, CORRES_STATE, CORRES_COUNTRY, CORRES_PIN, JURI_SAME_FLAG, JURI_LINE1, JURI_LINE2, JURI_LINE3, JURI_CITY,
                      JURI_STATE, JURI_COUNTRY, JURI_PIN, RESI_STD_CODE, RESI_TEL_NUM, OFF_STD_CODE, OFF_TEL_NUM, MOB_CODE, MOB_NUM, FAX_CODE, FAX_NO, EMAIL,
                      REMARKS, DEC_DATE, DEC_PLACE, KYC_DATE, DOC_SUB, KYC_NAME, KYC_DESIGNATION, KYC_BRANCH, KYC_EMPCODE, ORG_NAME, ORG_CODE,
                      NUM_IDENTITY, NUM_RELATED, NUM_LOCALADDRESS, NUM_IMAGES, NAME_UPDATE_FLAG, PERSONAL_UPDATE_FLAG, ADDRESS_UPDATE_FLAG,
                      CONTACT_UPDATE_FLAG, KYC_UPDATE_FLAG, IDENTITY_UPDATE_FLAG, RELPERSON_UPDATE_FLAG, IMAGE_UPDATE_FLAG, SEQUENCE_NO_ID1,
                      IDENT_TYPE_ID1, IDENT_NUM_ID1, ID_EXPIRYDATE_ID1, IDPROOF_SUBMITTED_ID1, IDVER_STATUS_ID1, SEQUENCE_NO_ID2, IDENT_TYPE_ID2,
                      IDENT_NUM_ID2, ID_EXPIRYDATE_ID2, IDPROOF_SUBMITTED_ID2, IDVER_STATUS_ID2, SEQUENCE_NO_REL, REL_TYPE, CKYC_NO_REL, PREFIX_REL,
                      FNAME_REL, MNAME_REL, LNAME_REL, PAN_REL, UID_REL, VOTERID_REL, NREGA_REL, PASSPORT_REL, PASSPORT_EXP_REL, DRIVING_LICENCE_REL,
                      OTHERID_NAME_REL, OTHERID_NO_REL, SIMPLIFIED_CODE_REL, SIMPLIFIED_NO_REL, DEC_DATE_REL, DEC_PLACE_REL, ORG_NAME_REL, ORG_CODE_REL,
                      SEQUENCE_NO_L_ADDR, BRANCH_CODE_L_ADDR, ADDR_LINE1_L, ADDR_LINE2_L, ADDR_LINE3_L, ADDR_CITY_L, ADDR_DIST_L, ADDR_PIN_L, ADDR_STATE_L,
                      ADDR_COUNTRY_L, RESI_STD_CODE_L, RESI_TEL_NUM_L, OFF_STD_CODE_L, OFF_TEL_NUM_L, FAX_CODE_L, FAX_NO_L, EMAIL_L, DEC_DATE_L,
                      DEC_PLACE_L, SEQUENCE_NO_IMG1, IMAGE_TYPE_IMG1, IMAGE_CODE_IMG1, GLOBAL_FLAG_IMG1, BRANCH_CODE_IMG1,
                      SEQUENCE_NO_IMG2, IMAGE_TYPE_IMG2, IMAGE_CODE_IMG2, GLOBAL_FLAG_IMG2, BRANCH_CODE_IMG2, IMAGE_DATA_IMG2, SEQUENCE_NO_IMG3,
                      IMAGE_TYPE_IMG3, IMAGE_CODE_IMG3, GLOBAL_FLAG_IMG3, BRANCH_CODE_IMG3, IMAGE_DATA_IMG3, SEQUENCE_NO_IMG4, IMAGE_TYPE_IMG4,
                      IMAGE_CODE_IMG4, GLOBAL_FLAG_IMG4, BRANCH_CODE_IMG4, IMAGE_DATA_IMG4, SEQUENCE_NO_IMG5, IMAGE_TYPE_IMG5, IMAGE_CODE_IMG5,
                      GLOBAL_FLAG_IMG5, BRANCH_CODE_IMG5, IMAGE_DATA_IMG5;
        byte IMAGE_DATA_IMG1;
        #endregion

        #region DROPDOWN 'chkCuurentAddress' SELECTEDINDEXCHANGED EVENT
        protected void chkCuurentAddress_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (chkCuurentAddress.Checked == true)
                {
                    ddlProofIdentity.Text= ddlProofOfAddress1.SelectedItem.Text;
                    ddlProofIdentity1.SelectedItem.Text = ddlProofOfAddress1.SelectedItem.Text;
                    ddlProofIdentity_SelectedIndexChanged(this, e);
                    ddlProofIdentity1.Enabled = false;
                    ddlProofIdentity1.Visible = true;
                    txtPassNo.Visible = true;
                    txtPassNo.Enabled = false;
                    txtMaskCode1.Visible = true;
                    lblPassportNo.Text = lblPassportNoAdd.Text;
                    txtPassNo.Text = txtPassNoAdd.Text;
                    txtPassNo.Visible = true;
                    FillDistrictState(DDPinCode, DDDistrict1, DDState1);
                    //chkLocalAddress.Checked = true;
                    txtLocAddLine1.Text = txtAddressLine1.Text;
                    txtLocAddLine1.Enabled = false;
                    txtLocAddLine2.Text = txtAddressLine2.Text;
                    txtLocAddLine2.Enabled = false;
                    txtLocAddLine3.Text = txtAddressLine3.Text;
                    txtLocAddLine3.Enabled = false;
                    txtCity1.Text = txtCity.Text;
                    txtCity1.Enabled = false;
                    DDPinCode1.Text = DDPinCode.Text;
                    DDPinCode1.Enabled = false;
                    ddlCountryCode1.SelectedValue = ddlCountryCode.SelectedValue;
                    ddlCountryCode1.Enabled = false;
                    DDDistrict1.SelectedValue = DDDistrict.SelectedValue;
                    DDDistrict1.Enabled = false;
                    DDState1.SelectedValue = DDState.SelectedValue;
                    DDState1.Enabled = false;
                 
                }
                else
                {

                    ddlProofIdentity1.Enabled = true;
                    ddlProofIdentity1.Visible = true;
                    ddlProofIdentity1.Items.Insert(0, new ListItem("Select", ""));
                    ddlProofIdentity1.SelectedIndex = 0;
                    ddlProofIdentity_SelectedIndexChanged(this, e);
                    ddlProofOfAddress1.Text = string.Empty;
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
                    DDPinCode1.Enabled = true;
                    DDPinCode1.SelectedIndex = 0;
                    DDDistrict1.Items.Clear();
                    DDState1.Items.Clear();
                    DDDistrict1.Items.Insert(0, new ListItem("Select", ""));
                    DDState1.Items.Insert(0, new ListItem("Select", ""));
                    DDDistrict1.SelectedIndex = 0;
                    DDState1.SelectedIndex = 0;
                   
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

        protected void DDPinCode1_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (DDPinCode1.SelectedIndex == 0)
                {
                    DDDistrict1.SelectedIndex = 0;
                    DDState1.SelectedIndex = 0;
                    ddlCountryCode1.SelectedIndex = 0;
                    return;
                }

                FillDistrictState(DDPinCode1, DDDistrict1, DDState1);
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

        protected void DDPinCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string date;
                date = DateTime.Today.ToString("dd\\/MM\\/yyyy");

                if (txtAddressLine1.Text == "")
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter permanent address line 1')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter permanent address line 1')", true);
                    txtAddressLine1.Focus();
                  //  DDPinCode.SelectedIndex = 0;
                    return;
                }
                if (txtCity.Text == "")
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter permanent city/Town/Village')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter permanent city/Town/Village')", true);
                    txtCity.Focus();
                   // DDPinCode.SelectedIndex = 0;
                    return;
                }
                if (DDPinCode.SelectedIndex == 0 && chkTick.Checked == false)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter permanent Pin/Post Code')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please enter permanent Pin/Post Code')", true);
                    DDPinCode.Focus();
                    //DDPinCode.SelectedIndex = 0;
                    return;
                }
                //chkPerAddress.Enabled = false;
                ddlCountryCode.SelectedValue = "IN";
                // Added BY Pratik
                FillDistrictState(DDPinCode, DDDistrict, DDState);
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

        #region PAGELOADEVENTS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //AjaxPro.Utility.RegisterTypeForAjax(typeof(KMI.FRMWRK.Web.Application.CKYC.CKYCView));
                    //AjaxPro.Utility.RegisterTypeForAjax(typeof(CKYCView));

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

                    Session["CarrierCode"] = '2';
                    olng = new MultilingualManager("DefaultConn", "CKYCView.aspx", Session["UserLangNum"].ToString());
                    strUserLang = HttpContext.Current.Session["UserLangNum"].ToString();
                    InitializeControls();
                    FillDocumentReceived();
                    subPopulateTitle();
                    subPopulateGender();
                    PopulateProofIdentiy();
                    subPopulateAccountType();
                    PopulateAddressProofType1();
                    FillDistrictState(DDPinCode, DDDistrict, DDState);
                    FillDistrictState(DDPinCode1, DDDistrict1, DDState1);
                    Fillcountrycd1();
                    refno = Request.QueryString["refno"].ToString().Trim();

                    if (Request.QueryString["Status"].ToString() == "Service")
                    {
                        // System.Threading.Thread.Sleep(5000);
                        DataSet ds = new DataSet();
                        ds = (DataSet)Session["sessDataSet"];
                        PopulateCndPersonalDtlsforservice();
                        // ImageRendering(refno);
                        Disables();
                        divmvmt.Visible = false;
                        divImageBind.Visible = false;
                    }

                    if (Request.QueryString["Status"].ToString() == "update")
                    {
                       // divIdProof.Visible = false;

                        FillRequiredDataForCKYC();
                        disablecntrl();
                        GetRelatedPersonDataForCKYC();
                        ImageRendering(refno.ToString());
                        BindCandidateMvmt();
                        divchkAddRel.Visible = false;
                        txtKYCNumber.Visible = true;
                        lblKYCNumber.Visible = true;
                        PopulatePinCode();
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCView.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong.....');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion

        #region 'btnUpdate_Click' Event
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            objDAL = new DataAccessLayer("CKYCConnectionString");
            Hashtable htParam = new Hashtable();
            DataSet dsbtnUpd = new DataSet();
            string Res;
            string msg = string.Empty; //updated successfully";



            if (checkAppNameDtlupdFlag.Checked==true)
            {
                if (txtGivenName.Text.Trim() == "" && txtMiddleName.Text.Trim() == "" && txtLastName.Text.Trim()=="" && cboTitlee.SelectedIndex.ToString() == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('All Fields of Applicant Name Section Is Mandatory')", true);
                    return;
                }
                htParam.Clear();
                htParam.Add("@RegRefNo", txtRefNumber.Text.ToString());
                htParam.Add("@CREATEDBY", Session["UserID"].ToString().Trim());
                htParam.Add("@PREFIX", cboTitlee.SelectedValue);
                htParam.Add("@FNAME", txtGivenName.Text.Trim());
                htParam.Add("@MNAME", txtMiddleName.Text.Trim());
                htParam.Add("@LNAME", txtLastName.Text.Trim());
                htParam.Add("@UpdateFlag", "UPD_AN");
                dsbtnUpd = objDAL.GetDataSet("prc_InskycUpdtDtls", htParam);
                if (dsbtnUpd.Tables.Count > 0 && dsbtnUpd.Tables[0].Rows.Count > 0)
                {
                    msg = msg + "Application Name ";
                }
                    checkAppNameDtlupdFlag.Checked = false;
                GetEnblDsblCtrl("UPD_AN", "N");

            }

            if (checkPersonalDtlupdFlag.Checked == true)
            {
            
                Res = objVal.PersonalDtlsValidation(
                    ddlAccountType, cboTitle11, txtGivenName1, txtLastName1, cboTitle22, rbtFS, txtGivenName2, txtLastName2, cboTitle33, txtGivenName3,
                    txtLastName3, txtDOB1, cboGender1, cboGender1, null, "");
                if (Res.Equals(""))
                {
                    htParam.Clear();
                    htParam.Add("@RegRefNo", txtRefNumber.Text.ToString());
                    htParam.Add("@CREATEDBY", Session["UserID"].ToString().Trim());
                    htParam.Add("@MAID_PREFIX", cboTitle11.SelectedValue);
                    htParam.Add("@MAID_FNAME", txtGivenName1.Text.Trim());
                    htParam.Add("@MAID_MNAME", txtMiddleName1.Text.Trim());
                    htParam.Add("@MAID_LNAME", txtLastName1.Text.Trim());

                    htParam.Add("@FATHER_PREFIX", cboTitle22.SelectedValue);
                    htParam.Add("@FATHER_FNAME", txtGivenName2.Text.Trim());
                    htParam.Add("@FATHER_MNAME", txtMiddleName2.Text.Trim());
                    htParam.Add("@FATHER_LNAME", txtLastName2.Text.Trim());

                    htParam.Add("@MOTHER_PREFIX", cboTitle33.SelectedValue);
                    htParam.Add("@MOTHER_FNAME", txtGivenName3.Text.Trim());
                    htParam.Add("@MOTHER_MNAME", txtMiddleName3.Text.Trim());
                    htParam.Add("@MOTHER_LNAME", txtLastName3.Text.Trim());

                    htParam.Add("@DOB", txtDOB1.Text.Trim());
                    htParam.Add("@GENDER", cboGender1.SelectedValue);

                    if (chkPanForm.Checked == true)
                    {
                        htParam.Add("@PAN", txtPanNo.Text.Trim());

                    }
                    htParam.Add("@UpdateFlag", "UPD_PD");
                    dsbtnUpd = objDAL.GetDataSet("prc_InskycUpdtDtls", htParam);
                    if (dsbtnUpd.Tables.Count > 0 && dsbtnUpd.Tables[0].Rows.Count > 0)
                    {
                        msg = msg + "Personal Details ";
                    }
                    checkPersonalDtlupdFlag.Checked = false;
                    GetEnblDsblCtrl("UPD_PD", "N");
                }
                else
                {
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + Res + "')", true);
                    return;
                }
            }
            if (checkAddressDtlupdFlag.Checked == true)
            {
                if (ddlProofOfAddress1.SelectedItem.ToString() == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Select Document Type ')", true);
                    return;
                }
                if (txtPassNoAdd.Text == "")
                {
                    string DocName= "Please Enter " + lblPassportNoAdd .Text.ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please Enter Document Number')", true);
                    return;
                }
                if (txtAddressLine1.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Enter Address Line1')", true);
                    return;
                }
                if (txtCity.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Enter City')", true);
                    return;
                }
                if (DDDistrict.SelectedItem.ToString() == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Select District')", true);
                    return;
                }
                if (DDPinCode.SelectedItem.ToString() == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Select Pin code')", true);
                    return;
                }
                if (DDState.SelectedItem.ToString() == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Select state')", true);
                    return;
                }
                if (ddlCountryCode.SelectedItem.ToString() == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Select Country')", true);
                    return;
                }

                //**Current Addr
                if (ddlProofIdentity1.SelectedItem.ToString() == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Select Current Address Document Type ')", true);
                    return;
                }
                if (txtPassNo.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Enter Current Address Document Number')", true);
                    return;
                }
                if (txtLocAddLine1.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Enter Current Address Line1')", true);
                    return;
                }
                if (txtCity1.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Enter Current Address  City')", true);
                    return;
                }
                if (DDDistrict1.SelectedItem.ToString() == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Select Current Address District')", true);
                    return;
                }
                if (DDPinCode1.SelectedItem.ToString() == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Select Current Address Pin code')", true);
                    return;
                }
                if (DDState1.SelectedItem.ToString() == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Select Current Address State')", true);
                    return;
                }
                if (ddlCountryCode1.SelectedItem.ToString() == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Select Current Address Country')", true);
                    return;
                }


                htParam.Clear();
                SaveDocDtls(txtRefNumber.Text.ToString(), ddlProofOfAddress1.SelectedValue.ToString(), ddlProofOfAddress1.SelectedItem.Text , txtPassNoAdd.Text.Trim(), "PA");
                htParam.Clear();
                htParam.Add("@RegRefNo", txtRefNumber.Text.ToString());
                htParam.Add("@CREATEDBY", Session["UserID"].ToString().Trim());
                htParam.Add("@UpdateFlag", "UPD_AD");
                htParam.Add("@CnctType1", "PA");
                htParam.Add("@PER_ADDLINE1", txtAddressLine1.Text.Trim());
                htParam.Add("@PER_ADDLINE2", txtAddressLine2.Text.Trim());
                htParam.Add("@PER_ADDLINE3", txtAddressLine3.Text.Trim());
                htParam.Add("@PER_CITY", txtCity.Text.Trim());
                htParam.Add("@PER_DISTRICT", DDDistrict.SelectedValue);
                htParam.Add("@PER_PIN", DDPinCode.SelectedValue);
                htParam.Add("@PER_STATECODE", DDState.SelectedValue);
                htParam.Add("@PER_COUNTRY_CODE", ddlCountryCode.SelectedValue);

                //if(chkCuurentAddress.Checked==true)
                //{
                if (ddlProofIdentity1.SelectedIndex != '0')
                {
                    SaveDocDtls(txtRefNumber.Text.ToString(), ddlProofIdentity1.SelectedValue.ToString(), ddlProofIdentity1.SelectedItem.Text, txtPassNo.Text.Trim(), "CA");

                }// htParam.Clear();
                    htParam.Add("@CnctType2", "CA");
                    htParam.Add("@SameAsPer", "01");//by meena 19/05/2017
                    htParam.Add("@CUR_ADDLINE1", txtLocAddLine1.Text);
                    htParam.Add("@CUR_ADDLINE2", txtLocAddLine2.Text);
                    htParam.Add("@CUR_ADDLINE3", txtLocAddLine3.Text);
                    htParam.Add("@CUR_CITY", txtCity1.Text.Trim());
                    htParam.Add("@CUR_DISTRICT", DDDistrict1.SelectedValue);
                    htParam.Add("@CUR_PIN", DDPinCode1.SelectedValue);
                    htParam.Add("@CUR_STATECODE", DDState1.SelectedValue);
                    htParam.Add("@CUR_COUNTRY_CODE", ddlCountryCode1.SelectedValue);
               // }

                dsbtnUpd = objDAL.GetDataSet("prc_InskycUpdtDtls", htParam);
                if (dsbtnUpd.Tables.Count > 0 && dsbtnUpd.Tables[0].Rows.Count > 0)
                {
                    msg = msg + "Address Details ";
                }
                checkPersonalDtlupdFlag.Checked = false;
                GetEnblDsblCtrl("UPD_AD", "N");

            }
            if (checkContactDtlupdFlag.Checked == true)
            {
                htParam.Clear();
               
                htParam.Add("@RegRefNo", txtRefNumber.Text.ToString());
                htParam.Add("@CREATEDBY", Session["UserID"].ToString().Trim());
                htParam.Add("@std_officeTele", txtTelOff.Text.Trim());
                htParam.Add("@std_resTele", txtTelRes.Text.Trim());
                htParam.Add("@mobile_countryCode", txtMobile.Text.Trim());
                htParam.Add("@std_fax", txtFax1.Text);

                htParam.Add("@OFF_TELE", txtTelOff2.Text);
                htParam.Add("@RES_TEL", txtTelRes2.Text);
                htParam.Add("@FAX", txtFax2.Text);
                htParam.Add("@MOBILE", txtMobile2.Text);
                htParam.Add("@EMAILID", txtemail.Text);
                htParam.Add("@UpdateFlag", "UPD_CD");
                dsbtnUpd = objDAL.GetDataSet("prc_InskycUpdtDtls", htParam);
                if (dsbtnUpd.Tables.Count > 0 && dsbtnUpd.Tables[0].Rows.Count > 0)
                {
                    msg = msg + "Contact Details ";
                }
                checkContactDtlupdFlag.Checked = true;
                GetEnblDsblCtrl("UPD_CD", "N");
            }
            if(checkOtherupdFlag.Checked==true)
            {
                htParam.Clear();
                htParam.Add("@RegRefNo", txtRefNumber.Text.ToString());
                htParam.Add("@CREATEDBY", Session["UserID"].ToString().Trim());
                htParam.Add("@REMARK", txtRemarks.Text.Trim());
                htParam.Add("@UpdateFlag", "UPD_OD");
                dsbtnUpd = objDAL.GetDataSet("prc_InskycUpdtDtls", htParam);
                if (dsbtnUpd.Tables.Count > 0 && dsbtnUpd.Tables[0].Rows.Count > 0)
                {
                    msg = msg + "Other Details ";
                }
                checkOtherupdFlag.Checked = true;
                GetEnblDsblCtrl("UPD_OD", "N");
            }
            if(checkvrifiDtlupdFlag.Checked==true)
            {
                if (txtPlace.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Enter Place of Declaration')", true);
                    return;
                }
                if (ddlDocReceived.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Select Document Received')", true);
                    return;
                }
                if (txtDateKYCver.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Enter KYC Date.')", true);
                    return;
                }
                if (txtEmpName.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Enter Employee Name')", true);
                    return;
                }
                if (txtEmpCode.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Enter Employee Code')", true);
                    return;
                }
                if (txtEmpDesignation.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Enter Employee Designation')", true);
                    return;
                }
                if (txtEmpBranch.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Enter Employee Branch')", true);
                    return;
                }
                if (txtInsName.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Enter Institution Name')", true);
                    return;
                }
                if (txtInsCode.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AlertMsg('Please Enter Institution Code')", true);
                    return;
                }

                htParam.Clear();
                htParam.Add("@RegRefNo", txtRefNumber.Text.ToString());
                htParam.Add("@CREATEDBY", Session["UserID"].ToString().Trim());
                htParam.Add("@PLACE", txtPlace.Text.Trim());
                htParam.Add("@kycVerDate", txtDateKYCver.Text.Trim());
                htParam.Add("@kycCertDoc", ddlDocReceived.SelectedValue);
                htParam.Add("@kycEmpName", txtEmpName.Text.Trim());
                htParam.Add("@kycEmpCode", txtEmpCode.Text.Trim());
                htParam.Add("@kycEmpBranch", txtEmpBranch.Text.Trim());
                htParam.Add("@kycEmpDesi", txtEmpDesignation.Text.Trim());
                htParam.Add("@kycInstName", txtInsName.Text.Trim());
                htParam.Add("@kycInstCode", txtInsCode.Text.Trim());
                htParam.Add("@UpdateFlag", "UPD_KYC");
                dsbtnUpd = objDAL.GetDataSet("prc_InskycUpdtDtls", htParam);
                if (dsbtnUpd.Tables.Count > 0 && dsbtnUpd.Tables[0].Rows.Count > 0)
                {
                    msg = msg + "KYC Verification Details ";
                }
                checkvrifiDtlupdFlag.Checked = false;
                GetEnblDsblCtrl("UPD_KYC", "N");

            }
            //dsbtnUpd = objDAL.GetDataSet("prc_InskycUpdtDtls", htParam);
            if (msg==string.Empty)
            {
                
            }
            else
            {
                msg = msg + "Updated Successfully.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);
                return;
            }
        }

        #endregion
        protected void checkvrifiDtlupdFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkvrifiDtlupdFlag.Checked == true)
            {
                GetEnblDsblCtrl("UPD_KYC", "Y");
                ddlDocReceived.Enabled = true;
            }
            else
            {
                GetEnblDsblCtrl("UPD_KYC", "N");
                ddlDocReceived.Enabled=false;
            }
        }

        #region 'btnCancel_Click' Event
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Status"].ToString() == "view")
                {
                    Response.Redirect("CKYCSearch.aspx?status=view");

                }
                else if (Request.QueryString["Status"].ToString() == "Service")
                {
                    Response.Redirect("CKYCSecuredSearch.aspx");

                }
                if (Request.QueryString["Status"].ToString() == "update")
                {
                   // Response.Redirect("CKYCSecuredSearch.aspx");
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCView.aspx.cs", "", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;

            }
        }
        #endregion

        #region Disables
        private void Disables()
        {
            try
            {
                cboTitle.Enabled = false;
                txtGivenName.Enabled = false;
                txtMiddleName.Enabled = false;
                txtLastName.Enabled = false;

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
            }
            catch (Exception ex)
            {

                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCView.aspx.cs", "", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion

        #region BindCandidateMvmt
        private void BindCandidateMvmt()
        {
            try
            {
                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt = null;
                hTable.Clear();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                hTable.Add("@Refno", refno.ToString());
                dt = dataAccessLayer.GetDataTable("Prc_GetKycStatusMvmt", hTable);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dgCndMvmt.DataSource = dt;
                        dgCndMvmt.DataBind();
                    }
                    else
                    {
                        cndmvmt.Visible = false;
                        divCndMvmt.Visible = false;
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCView.aspx.cs", "BindCandidateMvmt", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;

            }
        }
        #endregion

        #region ImageRendering
        public void ImageRendering(string strRefNo)
        {
            try
            {
                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt = null;
                hTable.Clear();
                string ShowImage = string.Empty;
                int count = 0;
                int m = 0;
                int n = 6;
                int approve = 0;
                DataSet dsImageDoc = new DataSet();
                hTable.Add("@RefNo", strRefNo);
                hTable.Add("@FlagQC", "2");
                hTable.Add("@Doccode", System.DBNull.Value);

                dt = dataAccessLayer.GetDataTable("prc_GetDocType_CKYC", hTable);
                if (dt.Rows.Count > 0)
                {
                    int num = dt.Rows.Count;
                    if (num < 6)
                    {
                        n = num;
                    }
                    int divide = num / 6;
                    int num1 = num % 6;
                    if (num1 != 0)
                    {
                        divide = divide + 1;
                    }
                    int idoc = 0;
                    for (int i = 0; i < divide; i++)
                    {
                        ShowImage = ShowImage + "<div class='row' style='background-color:WhiteSmoke;padding: 1%;'>";
                        if (i != 0)
                        {
                            count = ((num - 6) / 6);
                            if (count != 0)
                            {
                                num = num - 6;
                                n = 6;
                            }
                            else
                            {
                                n = num % 6;
                            }
                        }
                        for (int j = m; j < n; j++)
                        {
                            hTable.Clear();
                            dsImageDoc.Clear();
                            hTable.Add("@RefNo", strRefNo);
                            hTable.Add("@FlagQC", "3");
                            hTable.Add("@Doccode", dt.Rows[idoc]["DocCode"].ToString());
                            dsImageDoc = dataAccessLayer.GetDataSet("prc_GetDocType_CKYC", hTable);

                            Byte[] bytes = (Byte[])dsImageDoc.Tables[0].Rows[0]["IMAGE"];
                            string Flag = dsImageDoc.Tables[0].Rows[0]["Flag"].ToString().Trim();
                            string Flag1 = string.Empty;
                            if (Flag == "")
                            {
                                Flag1 = "1";
                            }
                            else if (Flag == "C" || Flag == "R")
                            {
                                Flag1 = "2";
                            }
                            else
                            {
                                Flag1 = "3";
                            }
                            System.Drawing.Image image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(bytes));
                            int height = image.Height;
                            int width = image.Width;
                            int total = height * width;
                            string MstWidth = dsImageDoc.Tables[0].Rows[0]["width"].ToString().Trim();
                            string MstHeight = dsImageDoc.Tables[0].Rows[0]["height"].ToString().Trim();
                            ZinSize.Value = total.ToString();
                            ZoutSize.Value = dsImageDoc.Tables[0].Rows[0]["ImgSize"].ToString().Trim();

                            string serverfilename = dsImageDoc.Tables[0].Rows[0]["ServerFileName"].ToString().Trim();
                            string id = "CKYC" + dsImageDoc.Tables[0].Rows[0]["ID"].ToString().Trim();
                            string Doccode = dsImageDoc.Tables[0].Rows[0]["DocCode"].ToString().Trim();
                            string Imgsrc = "ImageCSharp.aspx?ImageID=" + id;
                            string Doctype = dsImageDoc.Tables[0].Rows[0]["DocType"].ToString().Trim();
                            ShowImage = ShowImage + "  <div class='col-md-2 portfolio-item' >";
                            ShowImage = ShowImage + " <img id=" + id + " class='imgheight' height='50px' alt width=32 height=32  src=" + Imgsrc + ">";

                            ShowImage = ShowImage + " <ul class=list-inline><li>  ";
                            ShowImage = ShowImage + " <button Id=" + Doccode + " type='button' OnClick='return showimage(" + dsImageDoc.Tables[0].Rows[0]["ID"] + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + "," + Flag1 + ");'   class='btn btn-link' data-toggle=tooltip data-placement=bottom ";
                            if (Flag != "")
                            {
                                if (Flag == "A")
                                {
                                    Flag = "green";
                                    approve = approve + 1;
                                }
                                else if (Flag == "R")
                                {
                                    Flag = "red";
                                }
                                else if (Flag == "C")
                                {
                                    Flag = "darkorange";
                                }

                                ShowImage = ShowImage + " title='" + Doctype + "'><font color=" + Flag + ">" + serverfilename + "</font></button></li> </ul>";
                            }
                            else
                            {
                                ShowImage = ShowImage + " title='" + Doctype + "'>" + serverfilename + "</button></li> </ul>";
                            }
                            ShowImage = ShowImage + " </div>";
                            idoc = idoc + 1;

                        }
                        ShowImage = ShowImage + "</div>";

                    }
                    approvedoc.InnerHtml = Convert.ToString(approve);
                    divPhoto.Controls.Add(new LiteralControl(ShowImage));
                    chkOtherCFR.Checked = false;
                    hdnCFR.Value = "N";
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCView.aspx.cs", "ImageRendering", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;

            }
        }
        #endregion

        #region PopulateCndPersonalDtlsforservice
        private void PopulateCndPersonalDtlsforservice()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = (DataSet)Session["sessDataSet"];
                txtKYCNumber.Text = (ds.Tables[4].Rows[0]["CKYC_NO"].ToString());
                if (Convert.ToString(ds.Tables[4].Rows[0]["Acc_Type"]) == "01")
                {
                    chkNormal.Checked = true;
                }
                else if (Convert.ToString(ds.Tables[4].Rows[0]["Acc_Type"]) == "02")
                {
                    chkSimplified.Checked = true;
                }
                else if (Convert.ToString(ds.Tables[4].Rows[0]["Acc_Type"]) == "03")
                {
                    Chksmall.Checked = true;
                }


                if (Convert.ToString(ds.Tables[4].Rows[0]["CONSTI_TYPE"]) == "01")
                {
                    cbNew.Checked = true;

                }

                else if (Convert.ToString(ds.Tables[4].Rows[0]["CONSTI_TYPE"]) == "02")
                {
                    cbUpdate.Checked = true;

                }
                cboTitle.Text = ds.Tables[4].Rows[0]["PREFIX"].ToString();
                txtGivenName.Text = ds.Tables[4].Rows[0]["FNAME"].ToString();
                txtMiddleName.Text = ds.Tables[4].Rows[0]["MNAME"].ToString();
                txtLastName.Text = ds.Tables[4].Rows[0]["LNAME"].ToString();

                cboTitle1.Text = ds.Tables[4].Rows[0]["MAIDEN_PREFIX"].ToString();
                txtGivenName1.Text = ds.Tables[4].Rows[0]["MAIDEN_FNAME"].ToString();
                txtMiddleName1.Text = ds.Tables[4].Rows[0]["MAIDEN_MNAME"].ToString();
                txtLastName1.Text = ds.Tables[4].Rows[0]["MAIDEN_LNAME"].ToString();

                cboTitle2.Text = ds.Tables[4].Rows[0]["FATHER_PREFIX"].ToString();
                txtGivenName2.Text = ds.Tables[4].Rows[0]["FATHER_FNAME"].ToString();
                txtMiddleName2.Text = ds.Tables[4].Rows[0]["FATHER_MNAME"].ToString();
                txtLastName2.Text = ds.Tables[4].Rows[0]["FATHER_LNAME"].ToString();


                cboTitle3.Text = ds.Tables[4].Rows[0]["MOTHER_PREFIX"].ToString();
                txtGivenName3.Text = ds.Tables[4].Rows[0]["MOTHER_FNAME"].ToString();
                txtMiddleName3.Text = ds.Tables[4].Rows[0]["MOTHER_MNAME"].ToString();
                txtLastName3.Text = ds.Tables[4].Rows[0]["MOTHER_LNAME"].ToString();

                txtDOB.Text = ds.Tables[4].Rows[0]["DOB"].ToString();

                GetCKYCdata(cboGender, ds.Tables[4].Rows[0]["GENDER"].ToString(), "KGender");
                GetCKYCdata(ddlMaritalStatus, ds.Tables[4].Rows[0]["MARITAL_STATUS"].ToString(), "KMstatus");
                GetCKYCdata(ddlCitizenship, ds.Tables[4].Rows[0]["NATIONALITY"].ToString(), "KCitizn");
                GetCKYCdata(ddlResStatus, ds.Tables[4].Rows[0]["RESI_STATUS"].ToString(), "KResi");
                GetCKYCdata(ddlOccuSubType, ds.Tables[4].Rows[0]["RESI_STATUS"].ToString(), "KResi");
                ddlOccuSubType.Text = ds.Tables[4].Rows[0]["OCCUPATION"].ToString();
                if (ds.Tables[4].Rows[0]["OCCUPATION"].ToString() != "")
                {
                    if (ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "O-01" || ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "O-02"
                        || ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "O-03" || ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "O-04"
                        || ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "O-05")
                    {
                        ddlOccupation.Text = "Others";
                        if (ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "O-01")
                        {
                            ddlOccuSubType.Text = "Professional";

                        }
                        else if (ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "O-02")
                        {
                            ddlOccuSubType.Text = "Self Employed";
                        }
                        else if (ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "O-03")
                        {
                            ddlOccuSubType.Text = "Retired";

                        }
                        else if (ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "O-04")
                        {
                            ddlOccuSubType.Text = "Housewife";

                        }
                        else
                        {
                            ddlOccuSubType.Text = "Student";
                        }
                    }
                    else if (ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "S-01" || ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "S-02"
                        || ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "S-03")
                    {
                        ddlOccupation.Text = "Service";
                        if (ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "S-01")
                        {
                            ddlOccuSubType.Text = "Public Sector";

                        }
                        else if (ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "S-02")
                        {
                            ddlOccuSubType.Text = "Private Sector";

                        }
                        else if (ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "S-03")
                        {
                            ddlOccuSubType.Text = "Government Sector";

                        }
                    }
                    else if (ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "X-01")
                    {
                        ddlOccupation.Text = "Non Categorised";
                        ddlOccuSubType.Text = "Non Categorised";
                    }
                    else
                    {
                        ddlOccupation.Text = "Business";
                        ddlOccuSubType.Text = "Business";
                    }
                }
                txtIDResTax.Text = ds.Tables[4].Rows[0]["TAX_NUM"].ToString();
                txtDOBRes.Text = ds.Tables[4].Rows[0]["BIRTH_PLACE"].ToString();
                txtIsoCountry.Text = ds.Tables[4].Rows[0]["BIRTH_COUNTRY"].ToString();



                if (ds.Tables[6].Rows[0]["IDENT_TYPE"].ToString() == "A")
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Passport Number";
                    llPassExpDate.Text = "Passport Expiry Date";
                    llPassExpDate.Visible = true;
                    ddlProofIdentity.Text = "Passport";

                    txtPassNo.Text = ds.Tables[6].Rows[0]["IDENT_NUM"].ToString();
                    txtPassExpDate.Text = ds.Tables[6].Rows[0]["ID_EXPIRYDATE"].ToString();

                }
                else if (ds.Tables[6].Rows[0]["IDENT_TYPE"].ToString() == "B")
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Voter ID Card";
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    divPass.Visible = false;
                    ddlProofIdentity.Text = "Voter ID Card";

                    txtPassNo.Text = ds.Tables[6].Rows[0]["IDENT_NUM"].ToString();
                }
                else if (ds.Tables[6].Rows[0]["IDENT_TYPE"].ToString() == "C")
                {
                    lblPassportNo.Text = "PAN Card";
                    btnAddTrnsfr.Visible = true;
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    divPass.Visible = false;

                    ddlProofIdentity.Text = "PAN";
                    txtPassNo.Text = ds.Tables[6].Rows[0]["IDENT_NUM"].ToString();

                }
                else if (ds.Tables[6].Rows[0]["IDENT_TYPE"].ToString() == "D")
                {
                    divIdProof.Visible = true;
                    btnAddTrnsfr.Visible = true;
                    lblPassportNo.Text = "Driving Licence";
                    llPassExpDate.Text = "DrivingLicence Expiry Date";
                    ddlProofIdentity.Text = "Driving License";
                    txtPassNo.Text = ds.Tables[6].Rows[0]["IDENT_NUM"].ToString();
                    txtPassExpDate.Text = ds.Tables[6].Rows[0]["ID_EXPIRYDATE"].ToString();
                }
                else if (ds.Tables[6].Rows[0]["IDENT_TYPE"].ToString() == "E")
                {
                    lblPassportNo.Text = "UID(Aadhaar)";
                    btnAddTrnsfr.Visible = true;
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    divPass.Visible = false;
                    ddlProofIdentity.Text = "UID(Aadhaar)";

                    txtPassNo.Text = ds.Tables[6].Rows[0]["IDENT_NUM"].ToString();
                }
                else if (ds.Tables[6].Rows[0]["IDENT_TYPE"].ToString() == "F")
                {
                    divIdProof.Visible = true;
                    btnAddTrnsfr.Visible = true;
                    lblPassportNo.Text = "NREGA Job Card";
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    divPass.Visible = false;
                    ddlProofIdentity.Text = "NREGA Job Card";
                    txtPassNo.Text = ds.Tables[6].Rows[0]["IDENT_NUM"].ToString();
                }
                else if (ds.Tables[6].Rows[0]["IDENT_TYPE"].ToString() == "Z")
                {
                    divIdProof.Visible = true;
                    btnAddTrnsfr.Visible = true;
                    lblPassportNo.Text = "Others";
                    llPassExpDate.Text = "Identification Number";
                    ddlProofIdentity.Text = "Other";
                    txtPassNo.Text = ds.Tables[6].Rows[0]["IDENT_NUM"].ToString();
                    txtPassExpDate.Text = ds.Tables[6].Rows[0]["ID_EXPIRYDATE"].ToString();
                }

                else
                {
                    btnAddTrnsfr.Visible = false;
                    divIdProof.Visible = false;
                }

                //GetCKYCdata(ddlAddressType, ds.Tables[4].Rows[0]["RESI_STATUS"].ToString(), "KAddr");
                GetCKYCdata(ddlProofOfAddress, ds.Tables[4].Rows[0]["PERM_POA"].ToString(), "KAddrPrf");
                txtAddressLine1.Text = ds.Tables[4].Rows[0]["PERM_LINE1"].ToString();
                txtAddressLine2.Text = ds.Tables[4].Rows[0]["PERM_LINE2"].ToString();
                txtAddressLine3.Text = ds.Tables[4].Rows[0]["PERM_LINE3"].ToString();
                txtCity.Text = ds.Tables[4].Rows[0]["PERM_CITY"].ToString();
                ddlDistrict.Text = ds.Tables[4].Rows[0]["PERM_DIST"].ToString();
                ddlPinCode.Text = ds.Tables[4].Rows[0]["PERM_PIN"].ToString();
                ddlState.Text = ds.Tables[4].Rows[0]["PERM_STATE"].ToString();

                txtCountryCode.Text = Convert.ToString(ds.Tables[4].Rows[0]["PERM_COUNTRY"]);



                if (ds.Tables[4].Rows[0]["PERM_CORRES_SAMEFLAG"].ToString() == "Y")
                {
                    chkCuurentAddress.Checked = true;
                }
                else
                {
                    chkCuurentAddress.Checked = false;
                }
                txtLocAddLine1.Text = Convert.ToString(ds.Tables[4].Rows[0]["CORRES_LINE1"]);
                txtLocAddLine2.Text = Convert.ToString(ds.Tables[4].Rows[0]["CORRES_LINE2"]);
                txtLocAddLine3.Text = Convert.ToString(ds.Tables[4].Rows[0]["CORRES_LINE3"]);
                txtCity1.Text = Convert.ToString(ds.Tables[4].Rows[0]["CORRES_CITY"]);
                ddlDistrict1.Text = Convert.ToString(ds.Tables[4].Rows[0]["CORRES_DIST"]);
                ddlPinCode1.Text = Convert.ToString(ds.Tables[4].Rows[0]["CORRES_PIN"]);
                ddlState1.Text = Convert.ToString(ds.Tables[4].Rows[0]["CORRES_STATE"]);
                txtCountryCode1.Text = Convert.ToString(ds.Tables[4].Rows[0]["CORRES_COUNTRY"]);


                if (ds.Tables[4].Rows[0]["JURI_SAME_FLAG"].ToString() == "1")
                {
                    chkCurrentAdd.Checked = true;
                }
                else if (ds.Tables[4].Rows[0]["JURI_SAME_FLAG"].ToString() == "2")
                {
                    //chkCorresAdd.Checked = true;
                }
                else
                {
                    chkCurrentAdd.Checked = false;
                    //chkCorresAdd.Checked = false;
                }

                txtAddLine1.Text = Convert.ToString(ds.Tables[4].Rows[0]["JURI_LINE1"]);
                txtAddLine2.Text = Convert.ToString(ds.Tables[4].Rows[0]["JURI_LINE2"]);
                txtAddLine3.Text = Convert.ToString(ds.Tables[4].Rows[0]["JURI_LINE3"]);
                txtCity2.Text = Convert.ToString(ds.Tables[4].Rows[0]["JURI_CITY"]);
                ddlDistrict2.Text = Convert.ToString(ds.Tables[4].Rows[0]["JURI_DIST"]);
                ddlPinCode2.Text = Convert.ToString(ds.Tables[4].Rows[0]["JURI_PIN"]);
                ddlState2.Text = Convert.ToString(ds.Tables[4].Rows[0]["JURI_STATE"]);
                txtIsoCountryCode.Text = Convert.ToString(ds.Tables[4].Rows[0]["JURI_COUNTRY"]);


                txtTelOff2.Text = Convert.ToString(ds.Tables[4].Rows[0]["OFF_TEL_NUM"]);
                txtTelRes2.Text = Convert.ToString(ds.Tables[4].Rows[0]["RESI_TEL_NUM"]);

                txtFax2.Text = Convert.ToString(ds.Tables[4].Rows[0]["FAX_NO"]);
                txtMobile2.Text = Convert.ToString(ds.Tables[4].Rows[0]["MOB_NUM"]);
                txtemail.Text = Convert.ToString(ds.Tables[4].Rows[0]["EMAIL"]);
                txtEmpName.Text = Convert.ToString(ds.Tables[4].Rows[0]["KYC_NAME"]);
                txtEmpCode.Text = Convert.ToString(ds.Tables[4].Rows[0]["KYC_EMPCODE"]);
                txtEmpDesignation.Text = Convert.ToString(ds.Tables[4].Rows[0]["KYC_DESIGNATION"]);
                txtEmpBranch.Text = Convert.ToString(ds.Tables[4].Rows[0]["KYC_BRANCH"]);
                txtInsName.Text = Convert.ToString(ds.Tables[4].Rows[0]["ORG_NAME"]);
                txtInsCode.Text = Convert.ToString(ds.Tables[4].Rows[0]["ORG_CODE"]);
                txtDateKYCver.Text = Convert.ToString(ds.Tables[4].Rows[0]["KYC_DATE"]);

                txtEmpName.Enabled = false;
                txtEmpCode.Enabled = false;
                txtEmpDesignation.Enabled = false;
                txtEmpBranch.Enabled = false;
                txtInsName.Enabled = false;
                txtInsCode.Enabled = false;
                txtDateKYCver.Enabled = false;

                txtRemarks.Text = Convert.ToString(ds.Tables[4].Rows[0]["REMARKS"]);
                txtPlace.Text = Convert.ToString(ds.Tables[4].Rows[0]["DEC_PLACE"]);

                txtDate.Text = Convert.ToString(ds.Tables[4].Rows[0]["DEC_DATE"]);

                SaveXMLDataInTbl();
            }
            catch (Exception ex)
            {

                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCView.aspx.cs", "PopulateCndPersonalDtlsforservice", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;

            }
        }
        #endregion

        #region GetCKYCdata
        public void GetCKYCdata(TextBox txtbox, string prmvalue, string lookupcode)
        {
            try
            {

                hTable.Clear();
                dt = null;
                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");

                hTable.Add("@LookUpCode", lookupcode);
                hTable.Add("@Paramvalue", prmvalue);
                dt = dataAccessLayer.GetDataTable("Prc_GetCKYCValue", hTable);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ParamDesc"].ToString() != "")
                    {
                        txtbox.Text = dt.Rows[0]["ParamDesc"].ToString();
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCView.aspx.cs", "GetCKYCdata", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.....');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;

            }
        }
        #endregion
        #region SaveXMLDataInTbl
        private void SaveXMLDataInTbl()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = (DataSet)Session["sessDataSet"];

                PREFIX = ds.Tables[4].Rows[0]["PREFIX"].ToString();
                FNAME = ds.Tables[4].Rows[0]["FNAME"].ToString();
                MNAME = ds.Tables[4].Rows[0]["MNAME"].ToString();
                LNAME = ds.Tables[4].Rows[0]["LNAME"].ToString();
                FULLNAME = FNAME + " " + MNAME + " " + LNAME;

                MAIDEN_PREFIX = ds.Tables[4].Rows[0]["MAIDEN_PREFIX"].ToString();
                MAIDEN_FNAME = ds.Tables[4].Rows[0]["MAIDEN_FNAME"].ToString();
                MAIDEN_MNAME = ds.Tables[4].Rows[0]["MAIDEN_MNAME"].ToString();
                MAIDEN_LNAME = ds.Tables[4].Rows[0]["MAIDEN_LNAME"].ToString();
                MAIDEN_FULLNAME = MAIDEN_FNAME + " " + MAIDEN_MNAME + " " + MAIDEN_LNAME;

                FATHER_PREFIX = ds.Tables[4].Rows[0]["FATHER_PREFIX"].ToString();
                FATHER_FNAME = ds.Tables[4].Rows[0]["FATHER_FNAME"].ToString();
                FATHER_MNAME = ds.Tables[4].Rows[0]["FATHER_MNAME"].ToString();
                FATHER_LNAME = ds.Tables[4].Rows[0]["FATHER_LNAME"].ToString();
                FATHER_FULLNAME = FATHER_FNAME + " " + FATHER_MNAME + " " + FATHER_LNAME;

                MOTHER_PREFIX = ds.Tables[4].Rows[0]["MOTHER_PREFIX"].ToString();
                MOTHER_FNAME = ds.Tables[4].Rows[0]["MOTHER_FNAME"].ToString();
                MOTHER_MNAME = ds.Tables[4].Rows[0]["MOTHER_MNAME"].ToString();
                MOTHER_LNAME = ds.Tables[4].Rows[0]["MOTHER_LNAME"].ToString();
                MOTHER_FULLNAME = MOTHER_FNAME + " " + MOTHER_MNAME + " " + MOTHER_LNAME;


                hTable.Clear();
                hTable.Add("@CONSTI_TYPE", Convert.ToString(ds.Tables[4].Rows[0]["CONSTI_TYPE"]));
                hTable.Add("@APP_TYPE", Convert.ToString(ds.Tables[4].Rows[0]["CONSTI_TYPE"]));
                hTable.Add("@ACC_TYPE", (Convert.ToString(ds.Tables[4].Rows[0]["Acc_Type"])));
                hTable.Add("@CKYC_NO", (ds.Tables[4].Rows[0]["CKYC_NO"].ToString()));
                hTable.Add("@PREFIX", ds.Tables[4].Rows[0]["PREFIX"].ToString());
                hTable.Add("@FNAME", ds.Tables[4].Rows[0]["FNAME"].ToString());
                hTable.Add("@MNAME", ds.Tables[4].Rows[0]["MNAME"].ToString());
                hTable.Add("@LNAME", LNAME);
                hTable.Add("@FULLNAME", FNAME + " " + MNAME + " " + LNAME);
                hTable.Add("@MAIDEN_PREFIX", ds.Tables[4].Rows[0]["MAIDEN_PREFIX"].ToString());
                hTable.Add("@MAIDEN_FNAME", ds.Tables[4].Rows[0]["MAIDEN_FNAME"].ToString());
                hTable.Add("@MAIDEN_MNAME", ds.Tables[4].Rows[0]["MAIDEN_MNAME"].ToString());
                hTable.Add("@MAIDEN_LNAME", ds.Tables[4].Rows[0]["MAIDEN_LNAME"].ToString());
                hTable.Add("@MAIDEN_FULLNAME", MAIDEN_FNAME + " " + MAIDEN_MNAME + " " + MAIDEN_LNAME);
                hTable.Add("@FATHERSPOUSE_FLAG", ds.Tables[4].Rows[0]["FATHERSPOUSE_FLAG"].ToString());
                hTable.Add("@FATHER_PREFIX", ds.Tables[4].Rows[0]["FATHER_PREFIX"].ToString());
                hTable.Add("@FATHER_FNAME", ds.Tables[4].Rows[0]["FATHER_FNAME"].ToString());
                hTable.Add("@FATHER_MNAME", ds.Tables[4].Rows[0]["FATHER_MNAME"].ToString());
                hTable.Add("@FATHER_LNAME", ds.Tables[4].Rows[0]["FATHER_LNAME"].ToString());
                hTable.Add("@FATHER_FULLNAME", FATHER_FNAME + " " + FATHER_MNAME + " " + FATHER_LNAME);
                hTable.Add("@MOTHER_PREFIX", ds.Tables[4].Rows[0]["MOTHER_PREFIX"].ToString());
                hTable.Add("@MOTHER_FNAME", ds.Tables[4].Rows[0]["MOTHER_FNAME"].ToString());
                hTable.Add("@MOTHER_MNAME", ds.Tables[4].Rows[0]["MOTHER_MNAME"].ToString());
                hTable.Add("@MOTHER_LNAME", ds.Tables[4].Rows[0]["MOTHER_LNAME"].ToString());
                hTable.Add("@MOTHER_FULLNAME", MOTHER_FNAME + " " + MOTHER_MNAME + " " + MOTHER_LNAME);
                hTable.Add("@GENDER", ds.Tables[4].Rows[0]["GENDER"].ToString());
                hTable.Add("@MARITAL_STATUS", ds.Tables[4].Rows[0]["MARITAL_STATUS"].ToString());
                hTable.Add("@NATIONALITY", ds.Tables[4].Rows[0]["NATIONALITY"].ToString());
                if (ds.Tables[4].Rows[0]["OCCUPATION"].ToString() != "")
                {
                    if (ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "O-01" || ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "O-02"
                        || ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "O-03" || ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "O-04"
                        || ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "O-05")
                    {
                        OCCUPATION = "O";
                    }

                    else if (ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "S-01" || ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "S-02"
                        || ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "S-03")
                    {
                        OCCUPATION = "S";

                    }
                    else if (ds.Tables[4].Rows[0]["OCCUPATION"].ToString() == "X-01")
                    {
                        OCCUPATION = "X";
                    }
                    else
                    {
                        OCCUPATION = "B";
                    }
                }

                hTable.Add("@OCCUPATION", OCCUPATION);
                hTable.Add("@SUBOCCUPATION", ds.Tables[4].Rows[0]["OCCUPATION"].ToString());
                hTable.Add("@DOB", DOB);
                hTable.Add("@RESI_STATUS", ds.Tables[4].Rows[0]["RESI_STATUS"].ToString());
                hTable.Add("@JURI_FLAG", JURI_FLAG);
                hTable.Add("@TAX_NUM", ds.Tables[4].Rows[0]["TAX_NUM"].ToString());
                hTable.Add("@BIRTH_COUNTRY", ds.Tables[4].Rows[0]["BIRTH_COUNTRY"].ToString());
                hTable.Add("@BIRTH_PLACE", ds.Tables[4].Rows[0]["BIRTH_PLACE"].ToString());
                hTable.Add("@PERM_TYPE", ds.Tables[4].Rows[0]["PERM_TYPE"].ToString());
                hTable.Add("@PERM_LINE1", ds.Tables[4].Rows[0]["PERM_LINE1"].ToString());
                hTable.Add("@PERM_LINE2", ds.Tables[4].Rows[0]["PERM_LINE2"].ToString());
                hTable.Add("@PERM_LINE3", ds.Tables[4].Rows[0]["PERM_LINE3"].ToString());
                hTable.Add("@PERM_CITY", ds.Tables[4].Rows[0]["PERM_CITY"].ToString());
                hTable.Add("@PERM_DIST", ds.Tables[4].Rows[0]["PERM_DIST"].ToString());
                hTable.Add("@PERM_STATE", ds.Tables[4].Rows[0]["PERM_STATE"].ToString());
                hTable.Add("@PERM_COUNTRY", Convert.ToString(ds.Tables[4].Rows[0]["PERM_COUNTRY"]));
                hTable.Add("@PERM_PIN", ds.Tables[4].Rows[0]["PERM_PIN"].ToString());
                hTable.Add("@PERM_POA", PERM_POA);
                hTable.Add("@PERM_POAOTHERS", PERM_POAOTHERS);
                hTable.Add("@PERM_CORRES_SAMEFLAG", ds.Tables[4].Rows[0]["PERM_CORRES_SAMEFLAG"].ToString());
                hTable.Add("@CORRES_LINE1", Convert.ToString(ds.Tables[4].Rows[0]["CORRES_LINE1"]));
                hTable.Add("@CORRES_LINE2", Convert.ToString(ds.Tables[4].Rows[0]["CORRES_LINE2"]));
                hTable.Add("@CORRES_LINE3", Convert.ToString(ds.Tables[4].Rows[0]["CORRES_LINE3"]));
                hTable.Add("@CORRES_CITY", Convert.ToString(ds.Tables[4].Rows[0]["CORRES_CITY"]));
                hTable.Add("@CORRES_DIST", Convert.ToString(ds.Tables[4].Rows[0]["CORRES_DIST"]));
                hTable.Add("@CORRES_STATE", Convert.ToString(ds.Tables[4].Rows[0]["CORRES_STATE"]));
                hTable.Add("@CORRES_COUNTRY", Convert.ToString(ds.Tables[4].Rows[0]["CORRES_COUNTRY"]));
                hTable.Add("@CORRES_PIN", Convert.ToString(ds.Tables[4].Rows[0]["CORRES_PIN"]));
                hTable.Add("@JURI_SAME_FLAG", ds.Tables[4].Rows[0]["JURI_SAME_FLAG"].ToString());
                hTable.Add("@JURI_LINE1", Convert.ToString(ds.Tables[4].Rows[0]["JURI_LINE1"]));
                hTable.Add("@JURI_LINE2", Convert.ToString(ds.Tables[4].Rows[0]["JURI_LINE2"]));
                hTable.Add("@JURI_LINE3", Convert.ToString(ds.Tables[4].Rows[0]["JURI_LINE3"]));
                hTable.Add("@JURI_CITY", Convert.ToString(ds.Tables[4].Rows[0]["JURI_CITY"]));
                hTable.Add("@JURI_STATE", Convert.ToString(ds.Tables[4].Rows[0]["JURI_STATE"]));
                hTable.Add("@JURI_COUNTRY", Convert.ToString(ds.Tables[4].Rows[0]["JURI_COUNTRY"]));
                hTable.Add("@JURI_PIN", Convert.ToString(ds.Tables[4].Rows[0]["JURI_PIN"]));
                hTable.Add("@RESI_STD_CODE", Convert.ToString(ds.Tables[4].Rows[0]["RESI_STD_CODE"]));
                hTable.Add("@RESI_TEL_NUM", Convert.ToString(ds.Tables[4].Rows[0]["RESI_TEL_NUM"]));
                hTable.Add("@OFF_STD_CODE", Convert.ToString(ds.Tables[4].Rows[0]["OFF_STD_CODE"]));
                hTable.Add("@OFF_TEL_NUM", Convert.ToString(ds.Tables[4].Rows[0]["OFF_TEL_NUM"]));
                hTable.Add("@MOB_CODE", Convert.ToString(ds.Tables[4].Rows[0]["MOB_CODE"]));
                hTable.Add("@MOB_NUM", Convert.ToString(ds.Tables[4].Rows[0]["MOB_NUM"]));
                hTable.Add("@FAX_CODE", Convert.ToString(ds.Tables[4].Rows[0]["FAX_CODE"]));
                hTable.Add("@FAX_NO", Convert.ToString(ds.Tables[4].Rows[0]["FAX_NO"]));
                hTable.Add("@EMAIL", Convert.ToString(ds.Tables[4].Rows[0]["EMAIL"]));
                hTable.Add("@REMARKS", Convert.ToString(ds.Tables[4].Rows[0]["REMARKS"]));
                hTable.Add("@DEC_DATE ", Convert.ToString(ds.Tables[4].Rows[0]["DEC_DATE"]));
                hTable.Add("@DEC_PLACE", Convert.ToString(ds.Tables[4].Rows[0]["DEC_PLACE"]));
                hTable.Add("@KYC_DATE ", Convert.ToString(ds.Tables[4].Rows[0]["KYC_DATE"]));
                hTable.Add("@DOC_SUB", Convert.ToString(ds.Tables[4].Rows[0]["DOC_SUB"]));
                hTable.Add("@KYC_NAME", Convert.ToString(ds.Tables[4].Rows[0]["KYC_NAME"]));
                hTable.Add("@KYC_DESIGNATION", Convert.ToString(ds.Tables[4].Rows[0]["KYC_DESIGNATION"]));
                hTable.Add("@KYC_BRANCH", Convert.ToString(ds.Tables[4].Rows[0]["KYC_BRANCH"]));
                hTable.Add("@KYC_EMPCODE", Convert.ToString(ds.Tables[4].Rows[0]["KYC_EMPCODE"]));
                hTable.Add("@ORG_NAME", Convert.ToString(ds.Tables[4].Rows[0]["ORG_NAME"]));
                hTable.Add("@ORG_CODE", Convert.ToString(ds.Tables[4].Rows[0]["ORG_CODE"]));
                hTable.Add("@NUM_IDENTITY", Convert.ToString(ds.Tables[4].Rows[0]["NUM_IDENTITY"]));
                hTable.Add("@NUM_RELATED", Convert.ToString(ds.Tables[4].Rows[0]["NUM_RELATED"]));
                hTable.Add("@NUM_LOCALADDRESS", Convert.ToString(ds.Tables[4].Rows[0]["NUM_LOCALADDRESS"]));
                hTable.Add("@NUM_IMAGES", Convert.ToString(ds.Tables[4].Rows[0]["NUM_IMAGES"]));
                hTable.Add("@NAME_UPDATE_FLAG", Convert.ToString(ds.Tables[4].Rows[0]["NAME_UPDATE_FLAG"]));
                hTable.Add("@PERSONAL_UPDATE_FLAG", Convert.ToString(ds.Tables[4].Rows[0]["PERSONAL_UPDATE_FLAG"]));
                hTable.Add("@ADDRESS_UPDATE_FLAG", Convert.ToString(ds.Tables[4].Rows[0]["ADDRESS_UPDATE_FLAG"]));
                hTable.Add("@CONTACT_UPDATE_FLAG", Convert.ToString(ds.Tables[4].Rows[0]["CONTACT_UPDATE_FLAG"]));
                hTable.Add("@KYC_UPDATE_FLAG", Convert.ToString(ds.Tables[4].Rows[0]["KYC_UPDATE_FLAG"]));
                hTable.Add("@IDENTITY_UPDATE_FLAG", Convert.ToString(ds.Tables[4].Rows[0]["DENTITY_UPDATE_FLAG"]));
                hTable.Add("@RELPERSON_UPDATE_FLAG", Convert.ToString(ds.Tables[4].Rows[0]["RELPERSON_UPDATE_FLAG"]));
                hTable.Add("@IMAGE_UPDATE_FLAG", Convert.ToString(ds.Tables[4].Rows[0]["IMAGE_UPDATE_FLAG"]));
                hTable.Add("@SEQUENCE_NO_ID1", SEQUENCE_NO_ID1);
                hTable.Add("@IDENT_TYPE_ID1", Convert.ToString(ds.Tables[6].Rows[0]["IDENT_TYPE"]));
                hTable.Add("@IDENT_NUM_ID1", Convert.ToString(ds.Tables[6].Rows[0]["IDENT_NUM"]));
                hTable.Add("@ID_EXPIRYDATE_ID1", Convert.ToString(ds.Tables[6].Rows[0]["ID_EXPIRYDATE"]));
                hTable.Add("@IDPROOF_SUBMITTED_ID1", Convert.ToString(ds.Tables[6].Rows[0]["IDPROOF_SUBMITTED"]));
                hTable.Add("@IDVER_STATUS_ID1", Convert.ToString(ds.Tables[6].Rows[0]["IDVER_STATUS"]));
                hTable.Add("@SEQUENCE_NO_ID2", SEQUENCE_NO_ID2);
                hTable.Add("@IDENT_TYPE_ID2", IDENT_TYPE_ID2);
                hTable.Add("@IDENT_NUM_ID2", IDENT_NUM_ID2);
                hTable.Add("@ID_EXPIRYDATE_ID2", ID_EXPIRYDATE_ID2);
                hTable.Add("@IDPROOF_SUBMITTED_ID2", IDPROOF_SUBMITTED_ID2);
                hTable.Add("@IDVER_STATUS_ID2", IDVER_STATUS_ID2);
                hTable.Add("@SEQUENCE_NO_REL", SEQUENCE_NO_REL);
                hTable.Add("@REL_TYPE", REL_TYPE);
                hTable.Add("@CKYC_NO_REL", CKYC_NO_REL);
                hTable.Add("@PREFIX_REL", PREFIX_REL);
                hTable.Add("@FNAME_REL", FNAME_REL);
                hTable.Add("@MNAME_REL", MNAME_REL);
                hTable.Add("@LNAME_REL", LNAME_REL);
                hTable.Add("@PAN_REL", PAN_REL);
                hTable.Add("@UID_REL", UID_REL);
                hTable.Add("@VOTERID_REL", VOTERID_REL);
                hTable.Add("@NREGA_REL", NREGA_REL);
                hTable.Add("@PASSPORT_REL", PASSPORT_REL);
                hTable.Add("@PASSPORT_EXP_REL", PASSPORT_EXP_REL);
                hTable.Add("@DRIVING_LICENCE_REL", DRIVING_LICENCE_REL);
                hTable.Add("@OTHERID_NAME_REL", OTHERID_NAME_REL);
                hTable.Add("@OTHERID_NO_REL", OTHERID_NO_REL);
                hTable.Add("@SIMPLIFIED_CODE_REL", SIMPLIFIED_CODE_REL);
                hTable.Add("@SIMPLIFIED_NO_REL", SIMPLIFIED_NO_REL);
                hTable.Add("@DEC_DATE_REL", DEC_DATE_REL);
                hTable.Add("@DEC_PLACE_REL", DEC_PLACE_REL);
                hTable.Add("@ORG_NAME_REL", ORG_NAME_REL);
                hTable.Add("@ORG_CODE_REL", ORG_CODE_REL);

                hTable.Add("@SEQUENCE_NO_L_ADDR", SEQUENCE_NO_L_ADDR);
                hTable.Add("@BRANCH_CODE_L_ADDR", BRANCH_CODE_L_ADDR);
                hTable.Add("@ADDR_LINE1_L", ADDR_LINE1_L);
                hTable.Add("@ADDR_LINE2_L", ADDR_LINE2_L);
                hTable.Add("@ADDR_LINE3_L", ADDR_LINE3_L);
                hTable.Add("@ADDR_CITY_L", ADDR_CITY_L);
                hTable.Add("@ADDR_DIST_L", ADDR_DIST_L);
                hTable.Add("@ADDR_PIN_L", ADDR_PIN_L);
                hTable.Add("@ADDR_STATE_L", ADDR_STATE_L);
                hTable.Add("@ADDR_COUNTRY_L", ADDR_COUNTRY_L);
                hTable.Add("@RESI_STD_CODE_L", RESI_STD_CODE_L);
                hTable.Add("@RESI_TEL_NUM_L", RESI_TEL_NUM_L);
                hTable.Add("@OFF_STD_CODE_L", OFF_STD_CODE_L);
                hTable.Add("@OFF_TEL_NUM_L", OFF_TEL_NUM_L);
                hTable.Add("@FAX_CODE_L", FAX_CODE_L);
                hTable.Add("@FAX_NO_L", FAX_NO_L);
                hTable.Add("@EMAIL_L", EMAIL_L);
                hTable.Add("@DEC_DATE_L", DEC_DATE_L);
                hTable.Add("@DEC_PLACE_L", DEC_PLACE_L);

                hTable.Add("@SEQUENCE_NO_IMG1", "1"); //SEQUENCE_NO_IMG1
                hTable.Add("@IMAGE_TYPE_IMG1", Convert.ToString(ds.Tables[8].Rows[0]["IMAGE_TYPE"]));
                hTable.Add("@IMAGE_CODE_IMG1", Convert.ToString(ds.Tables[8].Rows[0]["IMAGE_CODE"]));
                hTable.Add("@GLOBAL_FLAG_IMG1", Convert.ToString(ds.Tables[8].Rows[0]["GLOBAL_FLAG"]));
                hTable.Add("@BRANCH_CODE_IMG1", Convert.ToString(ds.Tables[8].Rows[0]["BRANCH_CODE"]));
                hTable.Add("@IMAGE_DATA_IMG1", IMAGE_DATA_IMG1);
                hTable.Add("@SEQUENCE_NO_IMG2", "2");//,SEQUENCE_NO_IMG2
                hTable.Add("@IMAGE_TYPE_IMG2", IMAGE_TYPE_IMG2);
                hTable.Add("@IMAGE_CODE_IMG2", IMAGE_CODE_IMG2);
                hTable.Add("@GLOBAL_FLAG_IMG2", GLOBAL_FLAG_IMG2);
                hTable.Add("@BRANCH_CODE_IMG2", BRANCH_CODE_IMG2);
                hTable.Add("@IMAGE_DATA_IMG2", IMAGE_DATA_IMG2);
                hTable.Add("@SEQUENCE_NO_IMG3", "3"); //SEQUENCE_NO_IMG3
                hTable.Add("@IMAGE_TYPE_IMG3", IMAGE_TYPE_IMG3);
                hTable.Add("@IMAGE_CODE_IMG3", IMAGE_CODE_IMG3);
                hTable.Add("@GLOBAL_FLAG_IMG3", GLOBAL_FLAG_IMG3);
                hTable.Add("@BRANCH_CODE_IMG3", BRANCH_CODE_IMG3);
                hTable.Add("@IMAGE_DATA_IMG3", IMAGE_DATA_IMG3);
                hTable.Add("@SEQUENCE_NO_IMG4", "4");//SEQUENCE_NO_IMG4
                hTable.Add("@IMAGE_TYPE_IMG4", IMAGE_TYPE_IMG4);
                hTable.Add("@IMAGE_CODE_IMG4", IMAGE_CODE_IMG4);
                hTable.Add("@GLOBAL_FLAG_IMG4", GLOBAL_FLAG_IMG4);
                hTable.Add("@BRANCH_CODE_IMG4", BRANCH_CODE_IMG4);
                hTable.Add("@IMAGE_DATA_IMG4", IMAGE_DATA_IMG4);
                hTable.Add("@SEQUENCE_NO_IMG5", "5");//SEQUENCE_NO_IMG5
                hTable.Add("@IMAGE_TYPE_IMG5", IMAGE_TYPE_IMG5);
                hTable.Add("@IMAGE_CODE_IMG5", IMAGE_CODE_IMG5);
                hTable.Add("@GLOBAL_FLAG_IMG5", GLOBAL_FLAG_IMG5);
                hTable.Add("@BRANCH_CODE_IMG5", BRANCH_CODE_IMG5);
                hTable.Add("@IMAGE_DATA_IMG5", IMAGE_DATA_IMG5);
                hTable.Add("@createdBy", UserID.ToString());

                //ds = dataAccessLayer.GetDataSet("prc_InsXMLResponse", hTable, "STAGINGConnectionString");

                objDAL = new DataAccessLayer("STAGINGConnectionString");
                strTempRefNo = (objDAL.ExecuteScalar("prc_InsXMLResponse", hTable)).ToString(); //Prc_InsCkycPartialDtls
                dt = objDAL.GetDataTable("prc_InsXMLResponse", hTable);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["TempRegRefNo"].ToString() != "")
                    {
                        strTempRefNo = dt.Rows[0]["TempRegRefNo"].ToString();
                    }
                }

                //if (ds.Tables.Count > 0)
                //{
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        strTempRefNo = ds.Tables[0].Rows[0]["TempRegRefNo"].ToString();
                //    }
                //}

                hTable.Add("@PartialRegRefNo", strTempRefNo);
                hTable.Add("@uniqueID", obj.ToString());
                hTable.Add("@Usages", "W");
                hTable.Add("@Status", Request.QueryString["Status"].ToString());

                //ds = dataAccessLayer.GetDataSet("prc_InsXMLResponse_KYCReg", hTable, "CKYCConnectionString");

                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();

                dt = objDAL.GetDataTable("prc_InsXMLResponse_KYCReg", hTable);
            }
            catch (Exception ex)
            {

                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCView.aspx.cs", "SaveXMLDataInTbl", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;

            }
        }
        #endregion
        #region METHOD "FillRequiredDataForCndPersonal"
        protected void FillRequiredDataForCKYC()
        {
            try
            {
                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt = null;
                hTable.Clear();
                hTable.Add("@ActionFlag", "KMod");
                hTable.Add("@RegRefNo", refno.ToString());
                //hTable.Add("@UserID", UserID.ToString());
                hTable.Add("@UserType", System.DBNull.Value);
                dt = dataAccessLayer.GetDataTable("Prc_GetCkycViewData", hTable);

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
                ddlAccountType.SelectedValue = dt.Rows[0]["AccType"].ToString();
                ddlAccountType.Enabled = false;
                txtKYCNumber.Text = Convert.ToString(dt.Rows[0]["KYC_NO"]);
                txtRefNumber.Text = Convert.ToString(dt.Rows[0]["RegRefNo"]);
                cboTitle.Text = Convert.ToString(dt.Rows[0]["Prefix"]);
                txtGivenName.Text = Convert.ToString(dt.Rows[0]["FNAME"]);
                txtMiddleName.Text = Convert.ToString(dt.Rows[0]["MNAME"]);
                txtLastName.Text = Convert.ToString(dt.Rows[0]["LNAME"]);
                cboTitle1.Text = Convert.ToString(dt.Rows[0]["MAID_PREFIX"]);
                txtGivenName1.Text = Convert.ToString(dt.Rows[0]["MAID_FNAME"]);
                txtMiddleName1.Text = Convert.ToString(dt.Rows[0]["MAID_MNAME"]);
                txtLastName1.Text = Convert.ToString(dt.Rows[0]["MAID_LNAME"]);

                if (Convert.ToString(dt.Rows[0]["FS_FLAG"]) == "01")
                {
                    rbtFS.Text = "F";
                }
                else
                {
                    rbtFS.Text = "S";
                }
                cboTitle2.Text = Convert.ToString(dt.Rows[0]["FATHER_PREFIX"]);
                txtGivenName2.Text = Convert.ToString(dt.Rows[0]["FATHER_FNAME"]);
                txtMiddleName2.Text = Convert.ToString(dt.Rows[0]["FATHER_MNAME"]);
                txtLastName2.Text = Convert.ToString(dt.Rows[0]["FATHER_LNAME"]);
                cboTitle3.Text = Convert.ToString(dt.Rows[0]["MOTHER_PREFIX"]);
                txtGivenName3.Text = Convert.ToString(dt.Rows[0]["MOTHER_FNAME"]);
                txtMiddleName3.Text = Convert.ToString(dt.Rows[0]["MOTHER_MNAME"]);
                txtLastName3.Text = Convert.ToString(dt.Rows[0]["MOTHER_LNAME"]);
                txtDOB.Text = Convert.ToString(dt.Rows[0]["DOB"]);
                ddlMaritalStatus.Text = Convert.ToString(dt.Rows[0]["MARITAL_STATUS"]);
                cboGender.Text = Convert.ToString(dt.Rows[0]["GENDER"]);
                ddlCitizenship.Text = Convert.ToString(dt.Rows[0]["CITIZENSHIP"]);
                ddlResStatus.Text = Convert.ToString(dt.Rows[0]["RESI_STATUS"]);
                ddlOccupation.Text = Convert.ToString(dt.Rows[0]["OCCU_TYPE"]);
                ddlOccuSubType.Text = Convert.ToString(dt.Rows[0]["OCCU_SUB_TYPE"]);


                if (Convert.ToString(dt.Rows[0]["OCCU_TYPE"]) == "Business" || Convert.ToString(dt.Rows[0]["OCCU_TYPE"]) == "Non Categorised")
                {
                    divOccuSubType.Visible = false;
                    ddlOccuSubType.Visible = false;
                }
                else
                {
                    divOccuSubType.Visible = true;
                    ddlOccuSubType.Visible = true;
                }
                ddlIsoCountryCodeOthr.Text = Convert.ToString(dt.Rows[0]["ISO_COUNTRYCODE"]);
                txtIsoCountryCode2.Text = Convert.ToString(dt.Rows[0]["ISO_RFT_COUNTRYCODE"]);

                txtIDResTax.Text = Convert.ToString(dt.Rows[0]["TAX_IDNUMBER"]);
                txtDOBRes.Text = Convert.ToString(dt.Rows[0]["BIRTH_PLACE"]);
                txtIsoCountry.Text = Convert.ToString(dt.Rows[0]["ISO_BIRTHPLACE_CODE"]);

                ddlProofIdentity.Text = Convert.ToString(dt.Rows[0]["IdType"]);
                ViewState["strIdName"] = Convert.ToString(dt.Rows[0]["IdName"]);
                ViewState["strIdNumber"] = Convert.ToString(dt.Rows[0]["IdNumber"]);
                ViewState["strIdExpDate"] = Convert.ToString(dt.Rows[0]["IdExpDate"]);


                txtPassOthr.Visible = false;
                if (dt.Rows[0]["IdType"].ToString() == "A")
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Passport Number";
                    llPassExpDate.Text = "Passport Expiry Date";
                    llPassExpDate.Visible = true;

                    ddlProofIdentity.Text = "Passport Number";
                    txtPassNo.Text = dt.Rows[0]["IdNumber"].ToString();
                    txtPassExpDate.Text = dt.Rows[0]["IdExpDate"].ToString();

                }
                else if (dt.Rows[0]["IdType"].ToString() == "B")
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Voter ID Card";
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    divPass.Visible = false;


                    ddlProofIdentity.Text = "Voter ID Card";
                    txtPassNo.Text = dt.Rows[0]["IdNumber"].ToString();
                }
                else if (dt.Rows[0]["IdType"].ToString() == "D")
                {
                    divIdProof.Visible = true;
                    btnAddTrnsfr.Visible = true;
                    lblPassportNo.Text = "Driving Licence";
                    llPassExpDate.Text = "DrivingLicence Expiry Date";

                    ddlProofIdentity.Text = "Driving Licence";
                    txtPassNo.Text = dt.Rows[0]["IdNumber"].ToString();
                    txtPassExpDate.Text = dt.Rows[0]["IdExpDate"].ToString();
                }
                else if (dt.Rows[0]["IdType"].ToString() == "E")
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "UID(Aadhaar)";
                    btnAddTrnsfr.Visible = true;
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    divPass.Visible = false;


                    ddlProofIdentity.Text = "UID(Aadhaar)";
                    txtPassNo.Text = dt.Rows[0]["IdNumber"].ToString();
                    txtPassNo.Visible = true;

                }
                else if (dt.Rows[0]["IdType"].ToString() == "F")
                {
                    divIdProof.Visible = true;
                    btnAddTrnsfr.Visible = true;
                    lblPassportNo.Text = "NREGA Job Card";
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    divPass.Visible = false;

                    ddlProofIdentity.Text = "NREGA Job Card";
                    txtPassNo.Text = dt.Rows[0]["IdNumber"].ToString();
                    txtPassExpDate.Text = dt.Rows[0]["IdExpDate"].ToString();
                }
                else if (dt.Rows[0]["IdType"].ToString() == "Z")
                {
                    divIdProof.Visible = true;
                    btnAddTrnsfr.Visible = true;
                    lblPassportNo.Text = "Others";
                    llPassExpDate.Text = "Identification Number";

                    txtPassOthr.Visible = false;
                    ddlProofIdentity.Text = "Other";
                    txtPassNo.Text = dt.Rows[0]["IdName"].ToString();
                    txtPassExpDate.Text = dt.Rows[0]["IdNumber"].ToString();

                }
                else if (dt.Rows[0]["IdType"].ToString() == "C")
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "PAN Card";
                    btnAddTrnsfr.Visible = true;
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    divPass.Visible = false;

                    ddlProofIdentity.Text = "PAN Card";
                    txtPassNo.Text = dt.Rows[0]["IdNumber"].ToString();

                }
                else
                {
                    btnAddTrnsfr.Visible = false;
                    divIdProof.Visible = false;
                }


                if (Convert.ToString(dt.Rows[0]["CnctType1"]) == "P1")
                {
                    chkPerAddress.Checked = true;

                }
                else
                {
                    chkPerAddress.Checked = false;
                }
                //ddlAddressType.Text = Convert.ToString(dt.Rows[0]["PER_ADDTYPE"]);
                ddlProofOfAddress.Text = Convert.ToString(dt.Rows[0]["PER_ADDPROOFDesc"]);
                divAddProof.Visible = false;

                if (dt.Rows[0]["PER_ADDPROOF"].ToString().Trim() == "02")
                {
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Passport Number";
                    //llPassExpDateAdd.Text = "Passport Expiry Date";
                    llPassExpDateAdd.Visible = false;
                    txtPassExpDateAdd.Visible = true;
                    divPassAdd.Visible = true;
                    txtPassOthrAdd.Visible = false;
                    txtPassNoAdd.Visible = true;

                    txtPassNo.MaxLength = 15;
                    txtPassNo.Attributes.Remove("onblur");
                    txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");
                }
                else if (dt.Rows[0]["PER_ADDPROOF"].ToString().Trim() == "03")
                {

                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Driving Licence";
                    //llPassExpDateAdd.Text = "Driving Licence Expiry Date";
                    llPassExpDateAdd.Visible = false;
                    txtPassExpDateAdd.Visible = true;
                    txtPassOthrAdd.Visible = false;
                    divPassAdd.Visible = true;
                    txtPassNoAdd.Visible = true;
                    txtPassNoAdd.MaxLength = 15;
                    txtPassNoAdd.Attributes.Remove("onblur");
                }
                else if (dt.Rows[0]["PER_ADDPROOF"].ToString().Trim() == "01")
                {
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "UID(Aadhaar)";
                    llPassExpDateAdd.Visible = false;
                    txtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = false;
                    divPassAdd.Visible = false;
                    txtPassNoAdd.Visible = true;
                    txtPassNoAdd.MaxLength = 12;
                    txtPassNoAdd.Attributes.Remove("onblur");
                    txtPassNoAdd.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                }
                else if (dt.Rows[0]["PER_ADDPROOF"].ToString().Trim() == "04")
                {
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Voter ID Card";
                    llPassExpDateAdd.Visible = false;
                    txtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = false;
                    divPassAdd.Visible = false;
                    txtPassNoAdd.Visible = true;
                    txtPassNo.MaxLength = 15;
                    txtPassNoAdd.Attributes.Remove("onblur");
                }
                else if (dt.Rows[0]["PER_ADDPROOF"].ToString().Trim() == "05")
                {
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "NREGA Job Card";
                    llPassExpDateAdd.Visible = false;
                    txtPassNoAdd.Visible = true;
                    txtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = false;
                    divPassAdd.Visible = false;
                    txtPassNoAdd.MaxLength = 15;
                    txtPassNoAdd.Attributes.Remove("onblur");
                }
                else
                {
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Document Number";
                    //llPassExpDateAdd.Text = "Identification Number";
                    txtPassExpDateAdd.Visible = false;
                    llPassExpDateAdd.Visible = false;
                    divPassAdd.Visible = true;
                    llPassExpDateAdd.Visible = false;
                    txtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = true;
                    txtPassNoAdd.Visible = true;
                    txtPassNoAdd.MaxLength = 15;
                    txtPassNoAdd.Attributes.Remove("onblur");
                }
                txtAddressLine1.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE1"]);
                txtAddressLine2.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE2"]);
                txtAddressLine3.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE3"]);
                txtCity.Text = Convert.ToString(dt.Rows[0]["PER_CITY"]);
                ddlDistrict.Text = Convert.ToString(dt.Rows[0]["PER_DISTRICT"]);
                ddlPinCode.Text = Convert.ToString(dt.Rows[0]["PER_PIN"]);
                ddlState.Text = Convert.ToString(dt.Rows[0]["PER_STATECODE"]);
                txtCountryCode.Text = Convert.ToString(dt.Rows[0]["PER_COUNTRY_CODE"]);

                ViewState["strAddIdName"] = Convert.ToString(dt.Rows[0]["AddIdName"]);
                ViewState["strAddIdNumber"] = Convert.ToString(dt.Rows[0]["AddIdNumber"]);
                ViewState["strAddIdExpDate"] = Convert.ToString(dt.Rows[0]["AddIdExpDate"]);

                if (dt.Rows[0]["PER_ADDPROOF"].ToString() == "99")
                {

                    txtPassOthrAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNumber"]);
                    txtPassNoAdd.Text = Convert.ToString(dt.Rows[0]["AddIdName"]);
                }
                else
                {
                    txtPassNoAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNumber"]);
                    txtPassExpDateAdd.Text = Convert.ToString(dt.Rows[0]["AddIdExpDate"]);
                }

                if (Convert.ToString(dt.Rows[0]["CnctType2"]) == "M1")
                {
                    chkLocalAddress.Checked = true;
                }
                else
                {
                    chkLocalAddress.Checked = false;
                }

                if (Convert.ToString(dt.Rows[0]["CurSameAsFlag"]) == "01")
                {
                    chkCuurentAddress.Checked = true;
                }
                else
                {
                    chkCuurentAddress.Checked = false;
                }
                txtLocAddLine1.Text = Convert.ToString(dt.Rows[0]["CUR_ADDLINE1"]);
                txtLocAddLine2.Text = Convert.ToString(dt.Rows[0]["CUR_ADDLINE2"]);
                txtLocAddLine3.Text = Convert.ToString(dt.Rows[0]["CUR_ADDLINE3"]);
                txtCity1.Text = Convert.ToString(dt.Rows[0]["CUR_CITY"]);
                ddlDistrict1.Text = Convert.ToString(dt.Rows[0]["CUR_DISTRICT"]);
                ddlPinCode1.Text = Convert.ToString(dt.Rows[0]["CUR_PIN"]);
                ddlState1.Text = Convert.ToString(dt.Rows[0]["CUR_STATECODE"]);
                txtCountryCode1.Text = Convert.ToString(dt.Rows[0]["CUR_COUNTRY_CODE"]);

                if (Convert.ToString(dt.Rows[0]["CnctType3"]) == "J1")
                {
                    chkAddResident.Checked = true;
                }
                else
                {
                    chkAddResident.Checked = false;
                }
                txtAddLine1.Text = Convert.ToString(dt.Rows[0]["FRN_ADDLINE1"]);
                txtAddLine2.Text = Convert.ToString(dt.Rows[0]["FRN_ADDLINE2"]);
                txtAddLine3.Text = Convert.ToString(dt.Rows[0]["FRN_ADDLINE3"]);
                txtCity2.Text = Convert.ToString(dt.Rows[0]["FRN_CITY"]);
                ddlDistrict2.Text = Convert.ToString(dt.Rows[0]["FRN_DISTRICT"]);
                ddlPinCode2.Text = Convert.ToString(dt.Rows[0]["FRN_PIN"]);
                ddlState2.Text = Convert.ToString(dt.Rows[0]["FRN_STATECODE"]);
                txtIsoCountryCode.Text = Convert.ToString(dt.Rows[0]["FRN_COUNTRY_CODE"]);

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

                ddlDocReceived.SelectedValue = Convert.ToString(dt.Rows[0]["kycCertDoc"]);
                txtEmpName.Text = Convert.ToString(dt.Rows[0]["kycEmpName"]);
                txtEmpCode.Text = Convert.ToString(dt.Rows[0]["kycEmpCode"]);
                txtEmpDesignation.Text = Convert.ToString(dt.Rows[0]["kycEmpDesi"]);
                txtEmpBranch.Text = Convert.ToString(dt.Rows[0]["kycEmpBranch"]);
                txtInsName.Text = Convert.ToString(dt.Rows[0]["kycInstName"]);

                txtInsCode.Text = Convert.ToString(dt.Rows[0]["kycInstCode"]);
                txtDateKYCver.Text = Convert.ToString(dt.Rows[0]["kycVerDate"]);

                if (Convert.ToString(dt.Rows[0]["kycCertDoc"]) == "01")
                {
                    chkCertifyCopy.Checked = true;
                }
                else
                {
                    chkCertifyCopy.Checked = false;
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCView.aspx.cs", "FillRequiredDataForCKYC", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;

            }
        }
        #endregion

        #region METHOD "disablecntrl"
        protected void disablecntrl()
        {
            try
            {
                txtPassExpDateAdd.Enabled = false;
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
                ddlMaritalStatus.Enabled = false;
                cboGender.Enabled = false;
                ddlCitizenship.Enabled = false;
                ddlResStatus.Enabled = false;
                ddlOccupation.Enabled = false;
                ddlOccuSubType.Enabled = false;
                txtIsoCountryCode2.Enabled = false;
                txtIDResTax.Enabled = false;
                txtDOBRes.Enabled = false;
                txtIsoCountry.Enabled = false;
                ddlProofIdentity.Enabled = false;
                txtPassNo.Enabled = false;
                txtPassNoAdd.Enabled = false;
                txtPassExpDate.Enabled = false;
                txtPassOthrAdd.Enabled = false;
                //ddlAddressType.Enabled = false;
                ddlProofOfAddress.Enabled = false;
                txtAddressLine1.Enabled = false;
                txtAddressLine2.Enabled = false;
                txtAddressLine3.Enabled = false;
                txtCity.Enabled = false;
                ddlDistrict.Enabled = false;
                ddlPinCode.Enabled = false;
                ddlState.Enabled = false;
                txtCountryCode.Enabled = false;
                txtLocAddLine1.Enabled = false;
                txtLocAddLine2.Enabled = false;
                txtLocAddLine3.Enabled = false;
                txtCity2.Enabled = false;
                ddlDistrict1.Enabled = false;
                ddlPinCode1.Enabled = false;
                ddlState1.Enabled = false;
                txtCountryCode1.Enabled = false;
                txtAddLine1.Enabled = false;
                txtAddLine2.Enabled = false;
                txtAddLine3.Enabled = false;
                txtCity1.Enabled = false;
                ddlDistrict2.Enabled = false;
                ddlPinCode2.Enabled = false;
                ddlState2.Enabled = false;
                txtIsoCountryCode.Enabled = false;
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
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCView.aspx.cs", "disablecntrl", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion
        #region gvMemDtls_RowDataBound
        protected void gvMemDtls_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
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
                    LinkButton lnkdelete = (LinkButton)e.Row.FindControl("lnkdelete") as LinkButton;
                    if (Request.QueryString["Status"].ToString() == "Reg")
                    {
                        lnkView.Visible = false;
                    }
                    else
                    {
                        lnkView.Visible = true;
                    }

                    if (Request.QueryString["Status"].ToString() == "view")
                    {
                        lnkdelete.Visible = false;
                    }
                    else
                    {
                        lnkdelete.Visible = true;
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCView.aspx.cs", "gvMemDtls_RowDataBound", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
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
                    DataSet ds = (DataSet)Session["dsRel"];
                    dt = null;
                    ViewState["DT"] = dt;

                    if (Request.QueryString["Status"].ToString() == "Mod")
                    {
                        DataTable DTUpd = (DataTable)ViewState["DT"];
                        dt.Merge(DTUpd, true, MissingSchemaAction.Ignore);
                        dt.AcceptChanges();
                    }

                    if (dt.Rows.Count > 0)
                    {
                        if (Request.QueryString["Status"].ToString() == "Reg")
                        {
                            gvMemDtls.DataSource = dt;
                            gvMemDtls.DataBind();
                            gvMemDtls.Visible = true;
                            lblRelRecordShow.Visible = false;
                            chkAddRel.Enabled = false;
                            gvMemDtls.Columns[1].Visible = false;
                        }
                        else
                        {
                            gvMemDtls.DataSource = dt;
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCView.aspx.cs", "lnkViewRel_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
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
                    hTable.Clear();
                    dt.Clear();

                    hTable.Add("@RegRefNo", refno);
                    hTable.Add("@RelRefNo", RelRefnNo);
                    hTable.Add("@ActionFlag", Request.QueryString["Status"].ToString().Trim());
                    hTable.Add("@UserID", UserID.ToString());
                    dt = dataAccessLayer.GetDataTable("prc_DelKycRelatedPrsnDtls", hTable);
                }

                if (dt.Rows.Count > 0)
                {
                    gvMemDtls.DataSource = dt;
                    gvMemDtls.DataBind();

                    dsRel.Tables.Add(dt);
                    ViewState["DT"] = dsRel.Tables[0];
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCView.aspx.cs", "lnkdelete_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;

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
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenRelatedPersonPageView(" + RelRefnNo + "," + refno + ");", true);
            }
            catch (Exception ex)
            {

                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCView.aspx.cs", "lnkView_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion

        #region METHOD "GetRelatedPersonDataForCKYC"
        protected void GetRelatedPersonDataForCKYC()
        {
            try
            {
                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt = null;
                hTable.Clear();
                hTable.Add("@RegRefNo", refno.ToString());
                hTable.Add("@ActionFlag", "KMod");
                hTable.Add("@UserType", "");
                hTable.Add("@RelRefNo", "");

                dt = dataAccessLayer.GetDataTable("Prc_GetRelatedPersonDataForCKYC", hTable);

                if (dt.Rows.Count > 0)
                {
                    gvMemDtls.DataSource = dt;
                    gvMemDtls.DataBind();
                    Session["dsRel"] = dt;
                    ViewState["DT"] = dt;
                    gvMemDtls.Visible = true;
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCView.aspx.cs", "GetRelatedPersonDataForCKYC", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;

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
                lblKYCNumber.Text = "CKYC Number";
                lblName.Text = olng.GetItemDesc("lblName");
                lblMaidenName.Text = olng.GetItemDesc("lblMaidenName");
                lblFatherName.Text = "Father / Spouse Name";
                lblMotherName.Text = olng.GetItemDesc("lblMotherName");
                lbldob.Text = olng.GetItemDesc("lbldob");
                lblGender.Text = olng.GetItemDesc("lblGender");
                lblOccupation.Text = olng.GetItemDesc("lblOccupation");
                lblOccuSubType.Text = olng.GetItemDesc("lblOccuSubType");
                lblMarStatus.Text = olng.GetItemDesc("lblMarStatus");
                lblCitizenship.Text = olng.GetItemDesc("lblCitizenship");
                lblResStatus.Text = olng.GetItemDesc("lblResStatus");
                lblIsoCountryCodeOthr.Text = olng.GetItemDesc("lblIsoCountryCodeOthr");
                lblIsoCountryCode2.Text = olng.GetItemDesc("lblIsoCountryCode2");
                lblTaxIden.Text = olng.GetItemDesc("lblTaxIden");
                lblPlace.Text = olng.GetItemDesc("lblPlace");
                lblIsoContry.Text = olng.GetItemDesc("lblIsoContry");
                lblProofOfIdentity11.Text = olng.GetItemDesc("lblProofOfIdentity11");
               // lblProof.Text = olng.GetItemDesc("lblProof");
                //lblAddressType.Text = olng.GetItemDesc("lblAddressType");
                //lblProofOfAddress.Text = olng.GetItemDesc("lblProofOfAddress");
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
                lblpfemail.Text = olng.GetItemDesc("lblpfemail");
                lblRemarks.Text = olng.GetItemDesc("lblRemarks");
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
                lblpfPersonal.Text = olng.GetItemDesc("lblpfPersonal");
                lbltick.Text = olng.GetItemDesc("lbltick");
                lblProofOfIdentity11.Text = "PROOF OF IDENTITY (POI)";
                //lblpfofAddr1.Text = olng.GetItemDesc("lblpfofAddr1");
                lblpfofAddr2.Text = "PROOF OF IDENTITY AND ADDRESS";
                lblDtlOfRtltpr.Text = "RELATED PERSONS";
                lblRemarks.Text = "OTHER DETAILS";
                lblattstn.Text = "KYC VERIFICATION DETAILS";
                lbldec.Text = olng.GetItemDesc("lbldec");
                lblAttesOfc.Text = "ATTESTATION / FOR OFFICE USE ONLY";
                lblOfcuseOnly.Text = olng.GetItemDesc("lblOfcuseOnly");
                lblInsDtls.Text = olng.GetItemDesc("lblInsDtls");
                lblContactDetails.Text = "CONTACT DETAILS (All communication will be sent on provided MobileNo./Email-ID)";
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCView.aspx.cs", "InitializeControls", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        protected void checkAppNameDtlupdFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAppNameDtlupdFlag.Checked == true)
            {
                //cboTitle.Visible = false;
                //cboTitlee.Visible = true;

                //cboTitle.Enabled = true;
                //txtGivenName.Enabled = true;
                //txtMiddleName.Enabled = true;
                //txtLastName.Enabled = true;

                GetEnblDsblCtrl("UPD_AN", "Y");
                //  ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "GetEnblDsblCtrlsts('UPD_AN','Y')", true);

            }
            else
            {
                //cboTitle.Visible = true;
                //cboTitlee.Visible = false;


                //cboTitle.Enabled = false;
                //txtGivenName.Enabled = false;
                //txtMiddleName.Enabled = false;
                //txtLastName.Enabled = false;

                GetEnblDsblCtrl("UPD_AN", "N");
            }
        }

        //[Ajaxpro.AjaxMethod()]
        public System.Data.DataTable GetMandatorySpanId(string ID)
        {
            try
            {
                #region Mandatory Field Enable
                dt = new DataTable();
                dt = null;
                hTable.Clear();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");

                hTable.Add("@Actionflag", ID.ToString());

                dt = dataAccessLayer.GetDataTable("getMandFieldbyCheckID", hTable);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                        }
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
                    objErr = new ErrorLog();
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCView.aspx.cs", "InitializeControls", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            return dt;
        }

        protected void checkAddressDtlupdFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAddressDtlupdFlag.Checked == true)
            {
                //ddlProofOfAddress.Enabled = true;

                //ddlProofOfAddress.Visible = false;
                //ddlProofOfAddress1.Visible = true;

                //txtAddressLine1.Enabled = true;
                //txtAddressLine2.Enabled = true;
                //txtAddressLine3.Enabled = true;
                //txtCity.Enabled = true;
                //ddlDistrict.Enabled = true;
                //ddlPinCode.Enabled = true;
                //ddlState.Enabled = true;
                //txtCountryCode.Enabled = true;
                //txtLocAddLine1.Enabled = true;
                //txtLocAddLine2.Enabled = true;
                //txtLocAddLine3.Enabled = true;
                //txtCity1.Enabled = true;
                //ddlState2.Enabled = true;
                //ddlPinCode2.Enabled = true;

                GetEnblDsblCtrl("UPD_AD", "Y");
            }
            else
            {
                GetEnblDsblCtrl("UPD_AD", "N");
                //ddlProofOfAddress.Enabled = false;

                //ddlProofOfAddress.Visible = true;
                //ddlProofOfAddress1.Visible = false;

                //txtAddressLine1.Enabled = false;
                //txtAddressLine2.Enabled = false;
                //txtAddressLine3.Enabled = false;
                //txtCity.Enabled = false;
                //ddlDistrict.Enabled = false;
                //ddlPinCode.Enabled = false;
                //ddlState.Enabled = false;
                //txtCountryCode.Enabled = false;
                //txtLocAddLine1.Enabled = false;
                //txtLocAddLine2.Enabled = false;
                //txtLocAddLine3.Enabled = false;
                //txtCity1.Enabled = false;
                //ddlState2.Enabled = false;
                //ddlPinCode2.Enabled = false;
            }
        }

        protected void checkPersonalDtlupdFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPersonalDtlupdFlag.Checked == true)
            {
                 GetEnblDsblCtrl("UPD_PD", "Y");

                //cboTitle1.Enabled = true;

                //cboTitle11.Visible = true;
                //cboTitle1.Visible = false;

                //cboTitle22.Visible = true;
                //cboTitle2.Visible = false;

                //cboTitle33.Visible = true;
                //cboTitle3.Visible = false;

                //cboGender1.Visible = true;
                //cboGender.Visible = false;

                DivDob.Attributes.Add("style", "display:none");
                DivDob1.Attributes.Add("style", "display:block");

                //txtGivenName1.Enabled = true;
                //txtMiddleName1.Enabled = true;
                //txtLastName1.Enabled = true;
                //cboTitle2.Enabled = true;
                //txtGivenName2.Enabled = true;
                //txtMiddleName2.Enabled = true;
                //txtLastName2.Enabled = true;
                //cboTitle3.Enabled = true;
                //txtGivenName3.Enabled = true;
                //txtMiddleName3.Enabled = true;
                //txtLastName3.Enabled = true;
                //txtDOB.Enabled = true;
                //cboGender.Enabled = true;
            }
            else
            {
                GetEnblDsblCtrl("UPD_PD", "N");
                //cboTitle1.Enabled = false;s

                //cboTitle11.Visible = false;
                //cboTitle1.Visible = true;

                //cboTitle22.Visible = false;
                //cboTitle2.Visible = true;

                //cboTitle33.Visible = false;
                //cboTitle3.Visible = true;

                //DivDob.Attributes.Add("style", "display:block");
                //DivDob1.Attributes.Add("style", "display:none");

                //cboGender1.Visible = false;
                //cboGender.Visible = true;

                //txtGivenName1.Enabled = false;
                //txtMiddleName1.Enabled = false;
                //txtLastName1.Enabled = false;
                //cboTitle2.Enabled = false;
                //txtGivenName2.Enabled = false;
                //txtMiddleName2.Enabled = false;
                //txtLastName2.Enabled = false;
                //cboTitle3.Enabled = false;
                //txtGivenName3.Enabled = false;
                //txtMiddleName3.Enabled = false;
                //txtLastName3.Enabled = false;
                //txtDOB.Enabled = false;
                //cboGender.Enabled = false;
            }
        }

        protected void checkContactDtlupdFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkContactDtlupdFlag.Checked == true)
            {
                //txtTelOff.Enabled = true;
                //txtTelOff2.Enabled = true;
                //txtTelRes.Enabled = true;
                //txtTelRes2.Enabled = true;
                //txtMobile.Enabled = true;
                //txtMobile2.Enabled = true;
                //txtFax1.Enabled = true;
                //txtFax2.Enabled = true;
                //txtemail.Enabled = true;

                GetEnblDsblCtrl("UPD_CD", "Y");
            }
            else
            {
                //txtTelOff.Enabled = false;
                //txtTelOff2.Enabled = false;
                //txtTelRes.Enabled = false;
                //txtTelRes2.Enabled = false;
                //txtMobile.Enabled = false;
                //txtMobile2.Enabled = false;
                //txtFax1.Enabled = false;
                //txtFax2.Enabled = false;
                //txtemail.Enabled = false;
                GetEnblDsblCtrl("UPD_CD", "N");
            }
        }


        protected void checkOtherupdFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkOtherupdFlag.Checked == true)
            {
                GetEnblDsblCtrl("UPD_OD", "Y");
            }
            else
            {
                GetEnblDsblCtrl("UPD_OD", "N");
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

        protected void txtPanNo_TextChanged(object sender, EventArgs e)
        {
            if (txtPanNo.Text != "")
            {
                chkPanForm.Enabled = false;
            }
            else
            {
                chkPanForm.Enabled = true;
            }
        }

        private void subPopulateTitle()
        {
            try
            {
                oCommonUtility.GetCKYC(cboTitlee, "KTitle");
                cboTitlee.Items.Insert(0, new ListItem("Select", ""));
                oCommonUtility.GetCKYC(cboTitle11, "KTitle");
                cboTitle11.Items.Insert(0, new ListItem("Select", ""));
                oCommonUtility.GetCKYC(cboTitle22, "KTitle");
                cboTitle22.Items.Insert(0, new ListItem("Select", ""));
                oCommonUtility.GetCKYC(cboTitle33, "KMTitle");
                cboTitle33.Items.Insert(0, new ListItem("Select", ""));

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
                oCommonUtility.GetCKYC(cboGender1, "KGender");
                cboGender1.Items.Insert(0, new ListItem("Select", ""));
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "PopulateProofIdentiy", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        protected void checkProofofIDDtlupdFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (checkProofofIDDtlupdFlag.Checked == true)
            {
                //ddlProofIdentity1.Visible = true;
                //ddlProofIdentity.Visible = false;
                //txtPassNo.Enabled = true;
                GetEnblDsblCtrl("UPD_POI", "Y");
            }
            else
            {
                //ddlProofIdentity1.Visible = false;
                //ddlProofIdentity.Visible = true;
                //txtPassNo.Enabled = false;
                GetEnblDsblCtrl("UPD_POI", "N");
            }

        }

        #region DROPDOWN 'ddlProofIdentity' SELECTEDINDEXCHANGED EVENT
        protected void ddlProofIdentity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
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
                    //llPassExpDate.Text = "Passport Expiry Date";
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    divPass.Visible = true;
                    txtPassOthr.Visible = false;
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 15;
                    txtPassNo.Attributes.Remove("onblur");
                    txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");
                }
                else if (ddlProofIdentity1.SelectedIndex == 2)
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
                else if (ddlProofIdentity1.SelectedIndex == 3)
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
                else if (ddlProofIdentity1.SelectedIndex == 4)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Driving Licence No";
                    //llPassExpDate.Text = "Driving Licence Expiry Date";
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    txtPassOthr.Visible = false;
                    divPass.Visible = true;
                    txtPassNo.Visible = true;

                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 20;
                    txtPassNo.Attributes.Remove("onblur");
                    txtPassNo.Attributes.Add("onblur", "return ValidationDriving(this)");
                }
                else if (ddlProofIdentity1.SelectedIndex == 5)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Proof of Possession of Aadhaar";
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
                else if (ddlProofIdentity1.SelectedIndex == 6)
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
                    txtPassNo.MaxLength = 40;
                    txtPassNo.Attributes.Remove("onblur");
                }
                else if (ddlProofIdentity1.SelectedIndex == 7)
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
                else if (ddlProofIdentity1.SelectedIndex == 8)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Document Name";
                    llPassExpDate.Text = "Identification Number";
                    txtPassExpDate.Visible = true;
                    llPassExpDate.Visible = true;
                    divPass.Visible = true;
                    txtPassExpDate.Visible = false;
                    txtPassOthr.Visible = true;
                    txtPassNo.Visible = true;

                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 20;
                    txtPassNo.Attributes.Remove("onblur");
                }
                else
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Document Name";
                    llPassExpDate.Text = "Identification Number";
                    txtPassExpDate.Visible = true;
                    llPassExpDate.Visible = true;
                    divPass.Visible = true;
                    txtPassExpDate.Visible = false;
                    txtPassOthr.Visible = true;
                    txtPassNo.Visible = true;

                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 20;
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

        //Added by tushar for Account type
        private void subPopulateAccountType()
        {
            try
            {
                oCommonUtility.GetCKYC(ddlAccountType, "KAccTyp");
                ddlAccountType.Items.Insert(0, new ListItem("Select", ""));
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "subPopulateAccountType", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        //Added by tushar for Account type

        //protected void ddlProofOfAddress1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        txtPassOthrAdd1.Visible = false;
        //        txtPassNoAdd1.Visible = false;
        //        txtPassNoAdd1.Text = string.Empty;
        //        txtPassExpDateAdd1.Text = string.Empty;
        //        if (ddlProofOfAddress1.SelectedIndex == 0)
        //        {
        //            divAddProof1.Visible = false;
        //        }
        //        else if (ddlProofOfAddress1.SelectedIndex == 1)
        //        {
        //            divAddProof1.Visible = true;
        //            lblPassportNoAdd1.Text = "Passport Number";
        //            //llPassExpDateAdd.Text = "Passport Expiry Date";
        //            llPassExpDateAdd1.Visible = false;
        //            txtPassExpDateAdd1.Visible = false;
        //            divPassAdd1.Visible = true;
        //            txtPassOthrAdd1.Visible = false;
        //            txtPassNoAdd1.Visible = true;
        //            //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
        //            //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //            txtPassNoAdd1.MaxLength = 15;
        //            txtPassNoAdd1.Attributes.Remove("onblur");
        //            txtPassNoAdd1.Attributes.Add("onblur", "return ValidationPassport(this)");
        //        }
        //        else if (ddlProofOfAddress1.SelectedIndex == 2)
        //        {
        //            divAddProof1.Visible = true;
        //            lblPassportNoAdd1.Text = "Driving Licence";
        //            //llPassExpDateAdd.Text = "Driving Licence Expiry Date";
        //            llPassExpDateAdd1.Visible = false;
        //            txtPassExpDateAdd1.Visible = false;
        //            txtPassOthrAdd1.Visible = false;
        //            divPassAdd1.Visible = true;
        //            txtPassNoAdd1.Visible = true;
        //            //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
        //            //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //            txtPassNoAdd1.MaxLength = 15;
        //            txtPassNoAdd1.Attributes.Remove("onblur");
        //            txtPassNoAdd1.Attributes.Add("onblur", "return ValidationDriving(this)");
        //        }
        //        else if (ddlProofOfAddress1.SelectedIndex == 3)
        //        {
        //            divAddProof1.Visible = true;
        //            lblPassportNoAdd1.Text = "Proof of Possession of Aadhaar";
        //            llPassExpDateAdd1.Visible = false;
        //            txtPassExpDateAdd1.Visible = false;
        //            txtPassOthrAdd1.Visible = false;
        //            divPassAdd1.Visible = false;
        //            txtPassNoAdd1.Visible = true;
        //            //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
        //            txtPassNoAdd1.MaxLength = 12;
        //            txtPassNoAdd1.Text = "";
        //            txtPassNoAdd1.Attributes.Remove("onblur");
        //            txtPassNoAdd1.Attributes.Add("onblur", "return fnValidateAdhar(this)");
        //        }
        //        else if (ddlProofOfAddress1.SelectedIndex == 4)
        //        {
        //            divAddProof1.Visible = true;
        //            lblPassportNoAdd1.Text = "Voter ID Card";
        //            llPassExpDateAdd1.Visible = false;
        //            txtPassExpDateAdd1.Visible = false;
        //            txtPassOthrAdd1.Visible = false;
        //            divPassAdd1.Visible = false;
        //            txtPassNoAdd1.Visible = true;
        //            //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
        //            //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //            txtPassNoAdd1.MaxLength = 15;
        //            txtPassNoAdd1.Attributes.Remove("onblur");
        //            txtPassNoAdd1.Attributes.Add("onblur", "return ValidationVoterId(this)");
        //        }


        //        else if (ddlProofOfAddress1.SelectedIndex == 5)
        //        {
        //            divAddProof1.Visible = true;
        //            lblPassportNoAdd1.Text = "NREGA Job Card";
        //            llPassExpDateAdd1.Visible = false;
        //            txtPassNoAdd1.Visible = true;
        //            txtPassOthrAdd1.Visible = false;
        //            divPassAdd1.Visible = false;
        //            //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
        //            //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //            txtPassNoAdd1.MaxLength = 40;
        //            txtPassNoAdd1.Attributes.Remove("onblur");
        //        }
        //        else
        //        {
        //            divAddProof1.Visible = true;
        //            lblPassportNoAdd1.Text = "Document Name";
        //            llPassExpDateAdd1.Text = "Identification Number";
        //            txtPassExpDateAdd1.Visible = true;
        //            llPassExpDateAdd1.Visible = true;
        //            divPassAdd1.Visible = true;
        //            llPassExpDateAdd1.Visible = true;
        //            txtPassExpDateAdd1.Visible = false;
        //            txtPassOthrAdd1.Visible = true;
        //            txtPassNoAdd1.Visible = true;
        //            //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
        //            //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //            txtPassNoAdd1.MaxLength = 15;
        //            txtPassNoAdd1.Attributes.Remove("onblur");
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
        //            objErr.LogErr(AppID, "CkycReg.aspx.cs", "ddlProofOfAddress_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
        //        }
        //    }
        //}

        private void PopulateAddressProofType1()
        {
            try
            {
                oCommonUtility.GetCKYC(ddlProofOfAddress1, "KAddrPrf");
                ddlProofOfAddress1.Items.Insert(0, new ListItem("Select", ""));
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

        protected void ddlProofOfAddress1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtPassOthrAdd.Visible = false;
                txtPassNoAdd.Visible = false;
                txtPassNoAdd.Text = string.Empty;
                txtPassExpDateAdd.Text = string.Empty;
                if (ddlProofOfAddress1.SelectedIndex == 0)
                {
                    divAddProof.Visible = false;
                }
                else if (ddlProofOfAddress1.SelectedIndex == 1)
                {
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Passport Number";
                    //llPassExpDateAdd.Text = "Passport Expiry Date";
                    llPassExpDateAdd.Visible = false;
                    txtPassExpDateAdd.Visible = false;
                    divPassAdd.Visible = true;
                    txtPassOthrAdd.Visible = false;
                    txtPassNoAdd.Visible = true;
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd.MaxLength = 15;
                    txtPassNoAdd.Attributes.Remove("onblur");
                    txtPassNoAdd.Attributes.Add("onblur", "return ValidationPassport(this)");
                }
                else if (ddlProofOfAddress1.SelectedIndex == 2)
                {
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Driving Licence";
                    //llPassExpDateAdd.Text = "Driving Licence Expiry Date";
                    llPassExpDateAdd.Visible = false;
                    txtPassExpDateAdd.Visible = false;
                    txtPassOthrAdd.Visible = false;
                    divPassAdd.Visible = true;
                    txtPassNoAdd.Visible = true;
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd.MaxLength = 15;
                    txtPassNoAdd.Attributes.Remove("onblur");
                    txtPassNoAdd.Attributes.Add("onblur", "return ValidationDriving(this)");
                }
                else if (ddlProofOfAddress1.SelectedIndex == 3)
                {
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "Proof of Possession of Aadhaar";
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
                else if (ddlProofOfAddress1.SelectedIndex == 4)
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
                else if (ddlProofOfAddress1.SelectedIndex == 5)
                {
                    divAddProof.Visible = true;
                    lblPassportNoAdd.Text = "NREGA Job Card";
                    llPassExpDateAdd.Visible = false;
                    txtPassNoAdd.Visible = true;
                    txtPassOthrAdd.Visible = false;
                    divPassAdd.Visible = false;
                    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNoAdd.MaxLength = 40;
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
                    llPassExpDateAdd.Visible = true;
                    txtPassExpDateAdd.Visible = false;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ddlProofOfAddress1_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        //added BY  PRITY on 24 FEB 2021
        //[AjaxPro.AjaxMethod()]
        //public System.Data.DataSet GetEnblDsblCtrlsts(string strUpdate_Code, string strChkBox_Flag)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        objDAL = new DataAccessLayer("CKYCConnectionString");
        //        Hashtable htParam = new Hashtable();
        //        string CTRL_ID;

        //        ds.Clear();
        //        htParam.Add("@Update_Code", strUpdate_Code.ToString());
        //        htParam.Add("@USER_ID", HttpContext.Current.Session["UserID"].ToString());
        //        htParam.Add("@ChkBox_Flag", strChkBox_Flag.ToString());
        //        ds = objDAL.GetDataSet("Prc_GetEnblDsbl_sts", htParam);
        //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            {
        //                if (ds.Tables[0].Rows[i]["Ctrl_Type"].ToString() == "DD")
        //                {

        //                    ContentPlaceHolder MainContent = Page.Master.FindControl("EmptyPagePlaceholder") as ContentPlaceHolder;
        //                    CTRL_ID = ds.Tables[0].Rows[i]["Ctrl_Id_Name"].ToString();
        //                    DropDownList ddl = (DropDownList)MainContent.FindControl(CTRL_ID);
        //                    ddl.Visible = Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"].ToString());

        //                }


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
        //            objErr.LogErr(AppID, "Ckycview.aspx.cs", "GetEnblDsblCtrlsts", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
        //        }
        //    }

        //    return ds;
        //}

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
                    DDPinCode .DataSource = dt;
                    DDPinCode1.DataSource = dt;
                    DDPinCode.DataTextField = "PinCode";
                    DDPinCode1.DataTextField = "PinCode";
                    DDPinCode.DataBind();
                    DDPinCode1.DataBind();
                    DDPinCode.Items.Insert(0, new ListItem("Select", string.Empty));
                    DDPinCode1.Items.Insert(0, new ListItem("Select", string.Empty));
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


        protected void ddlCountryCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                DDDistrict.Items.Clear();
                DDDistrict.Items.Insert(0, new ListItem("Select", ""));

                DDState.Items.Clear();
                DDState.Items.Insert(0, new ListItem("Select", ""));
                DDPinCode.SelectedIndex = 0;

                DDPinCode.Enabled = (ddlCountryCode.SelectedValue == "IN" || ddlCountryCode.SelectedIndex == 0);
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
                DDDistrict1.Items.Clear();
                DDDistrict1.Items.Insert(0, new ListItem("Select", ""));

                DDState1.Items.Clear();
                DDState1.Items.Insert(0, new ListItem("Select", ""));
                DDPinCode1.SelectedIndex = 0;

                DDPinCode1.Enabled = (ddlCountryCode1.SelectedValue == "IN" || ddlCountryCode1.SelectedIndex == 0);

               
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




        public void SaveDocDtls(string FIREFNO_CKYC, string DocID, string DocDesc, string DocValue,string UPD_FLAG)
        {
            try
            {
                Hashtable htDoc = new Hashtable();
                htDoc.Clear();
                htDoc.Add("@FIREFNO_CKYC", FIREFNO_CKYC);
                htDoc.Add("@DocID", DocID);
                htDoc.Add("@DocDesc", DocDesc);
                htDoc.Add("@DocValue", DocValue);
                htDoc.Add("@CreatedBy", Session["UserID"].ToString().Trim());
                htDoc.Add("@UPD_FLAG", UPD_FLAG);
                objDAL = new DataAccessLayer("CKYCConnectionString");
                objDAL.ExecuteNonQuery("Prc_TX_CKYC_TblRegDocDtls_UPD", htDoc);
            }
            catch (Exception ex)
            {
                objErr = new ErrorLog();
                objErr.LogErr(AppID, "CkycReg.aspx.cs", "SaveDocDtls", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");

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
                    objErr.LogErr(AppID, "LegalEntityDtls.aspx.cs", "FillDocumentReceived", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
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

        public void GetEnblDsblCtrl(string strUpdate_Code, string strChkBox_Flag)
        {
            DataSet dsEnblDsbl = new DataSet();
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                Hashtable htEnblDsbl = new Hashtable();
                string CTRL_ID;
                dsEnblDsbl.Clear();
                htEnblDsbl.Clear();
                htEnblDsbl.Add("@Update_Code", strUpdate_Code.ToString());
                htEnblDsbl.Add("@USER_ID", HttpContext.Current.Session["UserID"].ToString());
                htEnblDsbl.Add("@ChkBox_Flag", strChkBox_Flag.ToString());
                dsEnblDsbl = objDAL.GetDataSet("Prc_GetEnblDsbl_sts", htEnblDsbl);
                if (dsEnblDsbl.Tables.Count > 0 && dsEnblDsbl.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsEnblDsbl.Tables[0].Rows.Count; i++)
                    {
                        ContentPlaceHolder MainContent = Page.Master.FindControl("EmptyPagePlaceholder") as ContentPlaceHolder;
                        CTRL_ID = dsEnblDsbl.Tables[0].Rows[i]["Ctrl_Id_Name"].ToString();

                        if (dsEnblDsbl.Tables[0].Rows[i]["Ctrl_Type"].ToString().Trim() == "DD")
                        {
                            DropDownList CtrlId = (DropDownList)MainContent.FindControl(CTRL_ID);
                            CtrlId.Visible = Convert.ToBoolean(dsEnblDsbl.Tables[0].Rows[i]["IsActive"].ToString());

                        }
                        if (dsEnblDsbl.Tables[0].Rows[i]["Ctrl_Type"].ToString().Trim() == "TX")
                        {
                            TextBox CtrlId = (TextBox)MainContent.FindControl(CTRL_ID);
                            CtrlId.Visible = Convert.ToBoolean(dsEnblDsbl.Tables[0].Rows[i]["IsActive"].ToString());

                        }
                        if (dsEnblDsbl.Tables[0].Rows[i]["Ctrl_Type"].ToString().Trim() == "TXE")
                        {
                            TextBox CtrlId = (TextBox)MainContent.FindControl(CTRL_ID);
                            CtrlId.Enabled = Convert.ToBoolean(dsEnblDsbl.Tables[0].Rows[i]["IsActive"].ToString());

                        }
                    }
                }


                //ds.Tables[0].DefaultView.RowFilter = "ChkBox_Flag = '"+ strChkBox_Flag + "'";
                //DataTable dt = (ds.Tables[0].DefaultView).ToTable();

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    ContentPlaceHolder MainContent = Page.Master.FindControl("EmptyPagePlaceholder") as ContentPlaceHolder;
                //    CTRL_ID = dt.Rows[i]["Ctrl_Id_Name"].ToString();

                //    if (dt.Rows[i]["Ctrl_Type"].ToString() == "DD")
                //    {
                //        DropDownList CtrlId = (DropDownList)MainContent.FindControl(CTRL_ID);
                //        CtrlId.Visible = Convert.ToBoolean(dt.Rows[i]["IsActive"].ToString());

                //    }
                //    if (ds.Tables[0].Rows[i]["Ctrl_Type"].ToString() == "TX")
                //    {
                //        TextBox CtrlId = (TextBox)MainContent.FindControl(CTRL_ID);
                //        CtrlId.Visible = Convert.ToBoolean(dt.Rows[i]["IsActive"].ToString());

                //    }
                //}

                //ds.Tables[0].DefaultView.RowFilter = "ChkBox_Flag <> '" + strChkBox_Flag + "'";
                //DataTable dtN = (ds.Tables[0].DefaultView).ToTable();




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
                    objErr.LogErr(AppID, "Ckycview.aspx.cs", "GetEnblDsblCtrlsts", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }


        }


    }
}