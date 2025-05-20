using KMI.FRMWRK.DAL;
using KMI.FRMWRK.Multilingual;
using KMI.FRMWRK.Web.Application.CKYC.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class CKYCRltdPrsnQC : System.Web.UI.Page
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
        DataSet objds = new DataSet();
        string strUserId;
        Guid obj = Guid.NewGuid();
        CommonUtility oCommonUtility = new CommonUtility();
        string msg;
        int AppID;
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
                subPopulateTitle();
                FillStates();
                subPopulateGender();
                PopulateMaritalStatus();
                PopoulateCitizenship();
                PopulateResidentialStatus();
                PopulateOccupationType();
                Fillcountrycd1();
                PopulateAddressType();
                PopulateRelatedPerson();
                PopulateAddressProofType();
                PopulateProofIdentiy();
                FillRequiredDataForCKYC();
                
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
                    //txtPassNo.Text = ViewState["strIdNumber"].ToString();
                    //txtPassExpDate.Text = ViewState["strIdExpDate"].ToString();
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
                    //txtPassNo.Text = ViewState["strIdNumber"].ToString();
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 15;
                    txtPassNo.Attributes.Remove("onblur");
                }
                else if (ddlProofIdentity.SelectedIndex == 3)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "PAN Card";
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
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
                    txtPassExpDate.Visible = true;
                    txtPassOthr.Visible = false;
                    divPass.Visible = true;
                    txtPassNo.Visible = true;
                    //txtPassNo.Text = ViewState["strIdNumber"].ToString();
                    //txtPassExpDate.Text = ViewState["strIdExpDate"].ToString();
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 15;
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
                    //txtPassNo.Text = ViewState["strIdNumber"].ToString();
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
                    //txtPassNo.Text = ViewState["strIdNumber"].ToString();
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 15;
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
                    //txtPassNo.Text = ViewState["strIdName"].ToString();
                    //txtPassOthr.Text = ViewState["strIdNumber"].ToString();
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
                    txtPassExpDate.Visible = true;
                    llPassExpDate.Visible = true;
                    divPass.Visible = true;
                    //txtPassNo.Text = ViewState["strIdNumber"].ToString();
                    //txtPassOthr.Text = ViewState["strSamDocNum"].ToString();
                    txtPassExpDate.Visible = false;
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 15;
                    txtPassNo.Attributes.Remove("onblur");
                }
                else
                {
                   
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
        }


        #region METHOD "FillRequiredDataForCndPersonal"
        protected void FillRequiredDataForCKYC()
        {
            try
            {
                DataSet ds = new DataSet();
                objDAL = new DataAccessLayer("CKYCConnectionString");
                objht.Clear();
                objht.Add("@RegRefNo", Request.QueryString["refno"].ToString()); //Request.QueryString["refno"].ToString()
                ds = objDAL.GetDataSet("getSearchData_RelatedPrsnDetails", objht);//getSearchData_Web
                txtRelRefNumber.Text = Convert.ToString(ds.Tables[0].Rows[0]["RelRefno"]);
                ddlRelType.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["RelType"]).Trim().ToString();
                cboTitle.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["Prefix"]).Trim().ToString();
                txtGivenName.Text = Convert.ToString(ds.Tables[0].Rows[0]["FName"]);
                txtMiddleName.Text = Convert.ToString(ds.Tables[0].Rows[0]["MName"]);
                txtLastName.Text = Convert.ToString(ds.Tables[0].Rows[0]["LName"]);
                cboTitle1.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["RelMaidPrefix"]).Trim().ToString();
                txtGivenName1.Text = Convert.ToString(ds.Tables[0].Rows[0]["RelMaidFNmae"]);
                txtMiddleName1.Text = Convert.ToString(ds.Tables[0].Rows[0]["RelMaidMNmae"]);
                txtLastName1.Text = Convert.ToString(ds.Tables[0].Rows[0]["RelMaidLNmae"]);
                if (Convert.ToString(ds.Tables[0].Rows[0]["FATHERSPOUSEFLAG"]) == "1")
                {
                    rbtFS.SelectedValue = "F";
                }
                else
                {
                    rbtFS.SelectedValue = "S";
                }
                //rbtFS.SelectedItem.Value = Convert.ToString(ds.Tables[0].Rows[0]["FATHERSPOUSEFLAG"]);
                cboTitle2.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["FatherPrefix"]).Trim().ToString();
                txtGivenName2.Text = Convert.ToString(ds.Tables[0].Rows[0]["RelFatherFName"]);
                txtMiddleName2.Text = Convert.ToString(ds.Tables[0].Rows[0]["RelFatherMName"]);
                txtLastName2.Text = Convert.ToString(ds.Tables[0].Rows[0]["RelFatherLName"]);
                cboTitle3.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["RelMotherPrefix"]).Trim().ToString();
                txtGivenName3.Text = Convert.ToString(ds.Tables[0].Rows[0]["RelMotherFName"]);
                txtMiddleName3.Text = Convert.ToString(ds.Tables[0].Rows[0]["RelMotherMname"]);
                txtLastName3.Text = Convert.ToString(ds.Tables[0].Rows[0]["RelMotherLName"]);
                txtDOB.Text = Convert.ToString(ds.Tables[0].Rows[0]["RelDOB"]);
                cboGender.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["Gender"]).Trim().ToString();
                ddlMaritalStatus.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["MaritalStatus"]).Trim().ToString();
                ddlCitizenship.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["CitizenShip"]).Trim().ToString();
                ddlResStatus.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["ResiStatus"]).Trim().ToString();
                ddlOccupation.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OccuType"]).Trim().ToString();
                ddlOccuSubType.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OccuType"]).Trim().ToString();
                ddlProofIdentity.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["IdType"]).Trim().ToString();
                txtPassNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["IDNo"]);
                txtPassExpDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["IDExpDate"]);
                ////////////////////////////////////
                ddlProofIdentity.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OccuType"]).Trim().ToString();
                ddlAddressType.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["AddressType"]).Trim().ToString();
                chkPerAddress.Checked = true;
                txtPassNoAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProfofAddrnum"]);
                txtPassExpDateAdd.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProfofAddrexpdate"]);
                ddlProofOfAddress.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["ProfofAddrtypecode"]).Trim().ToString();
                txtAddressLine1.Text = Convert.ToString(ds.Tables[0].Rows[0]["AddLine1"]);
                txtAddressLine2.Text = Convert.ToString(ds.Tables[0].Rows[0]["AddLine2"]);
                txtAddressLine3.Text = Convert.ToString(ds.Tables[0].Rows[0]["AddLine3"]);
                txtCity.Text = Convert.ToString(ds.Tables[0].Rows[0]["City"]);
                txtDistrictname.Text = Convert.ToString(ds.Tables[0].Rows[0]["District"]);
                txtPinCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["PinCode"]);
                ddlState.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["StateCode"]).Trim().ToString();
                ddlCountryCode.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["CountryCode"]).Trim().ToString();
                chkAppDeclare1.Checked = true;
                chkAppDeclare2.Checked = true;
                chkAppDeclare3.Checked = true;
                chkSelfCerti.Checked = true;
                chkHigh.Checked = true;
                chkDone.Checked = true;
                txtDate.Text =  Convert.ToString(ds.Tables[0].Rows[0]["RelDecDate"]);
                txtPlace.Text = Convert.ToString(ds.Tables[0].Rows[0]["RelDecPlace"]);
                txtEmpName.Text = Convert.ToString(ds.Tables[0].Rows[0]["RelkycEmpName"]);
                txtDate3.Text = Convert.ToString(ds.Tables[0].Rows[0]["RelkycVerDate"]);
                txtEmpCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["RelkycEmpCode"]);
                txtEmpDesignation.Text = Convert.ToString(ds.Tables[0].Rows[0]["RelkycEmpDesi"]);
                txtEmpBranch.Text = Convert.ToString(ds.Tables[0].Rows[0]["EmpBranch"]);
                txtInsName.Text = Convert.ToString(ds.Tables[0].Rows[0]["RelkycInstName"]);
                txtInsCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["RelkycInstCode"]);
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

        #region METHOD 'POPULATE DROPDOWNLIST'
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

        #region PopulateAddressType
        private void PopulateAddressType()
        {
            try
            {
                oCommonUtility.GetCKYC(ddlAddressType, "KAddr");
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
                    objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "PopulateAddressProofType", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
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
                objht.Clear();
                dt = objDAL.GetDataTable("Prc_GetStateCodeCKYC", objht);
                if (dt.Rows.Count > 0)
                {
                    ddlState.DataSource = dt;
                    ddlState.DataTextField = "STATE_Desc";
                    ddlState.DataValueField = "STATE_CODE";
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, new ListItem("Select", string.Empty));

                    //ddlState1.DataSource = dt;
                    //ddlState1.DataTextField = "STATE_Desc";
                    //ddlState1.DataValueField = "STATE_CODE";
                    //ddlState1.DataBind();
                    //ddlState1.Items.Insert(0, new ListItem("Select", string.Empty));

                    //ddlState2.DataSource = dt;
                    //ddlState2.DataTextField = "STATE_Desc";
                    //ddlState2.DataValueField = "STATE_CODE";
                    //ddlState2.DataBind();
                    //ddlState2.Items.Insert(0, new ListItem("Select", string.Empty));

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

        #region Fill country code1
        public void Fillcountrycd1()
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                objht.Clear();
                dt = objDAL.GetDataTable("Prc_GetcountrycodeCKYC", objht);
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

        #region PopulateRelatedPerson
        private void PopulateRelatedPerson()
        {
            try
            {
                //oCommonUtility.GetCKYC(ddlRelType, "KEntRelative");
                //ddlRelType.Items.Insert(0, new ListItem("Select", ""));

                objht.Clear();
                objht.Add("@LookupCode", "KEntRelative");
                FillDropdowns("prc_getDDLLookUpData", objht, ddlRelType, "CKYCConnectionString", true);
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/CKYCLegalEntityQC.aspx");
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
    }
}