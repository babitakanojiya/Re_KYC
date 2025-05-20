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
    public partial class CKYCEntView : System.Web.UI.Page
    {
        #region Declare Veriables
        private MultilingualManager olng;
        Hashtable htParam = new Hashtable();
        Hashtable objht = new Hashtable();
        DataTable objDt = new DataTable();
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
            if (!IsPostBack)
            {
                if (Session["PSSubmit"] != null)
                {
                    Session["PSSubmit"] = null;
                }
                InitializeControls();
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

                    if (Session["dsCtrl"] == null)
                    {
                        lnkViewCtrl.Attributes.Add("style", "display:none");
                    }

                    //  InitializeControls();
                    FillddlPageLoad();
                    FillCountry();
                    FillStates();
                    FillConstType();
                    //PopulatePinCode();
                    BindAttestation();

                    chkPerAddress.Checked = true;
                    chkPerAddress.Enabled = false;
                    txtDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                    txtDateKYCver.Text = DateTime.Today.ToString("dd-MM-yyyy");

                    chkAddCtrl.Enabled = false;
                }
                else if (Request.QueryString["status"].ToString() == "LMod")
                {
                    divchkDelRel.Visible = true;
                    divIdProof.Visible = false;

                    FillddlPageLoad();
                    FillCountry();
                    FillStates();
                    FillConstType();
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
                    BindGridImage();
                    FillddlPageLoad();
                    FillCountry();
                    FillStates();
                    FillConstType();
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

                    disablecntrl();
                    lnkViewRel.Visible = false;
                    lnkViewCtrl.Visible = false;

                    //BindGridImage();
                    divImg.Visible = true;

                }

                //ddlOccupation_SelectedIndexChanged(this, EventArgs.Empty);
                if (Request.QueryString["status"].ToString() != "LMod" && Request.QueryString["status"].ToString() != "view")
                {
                    FillDistrictState(txtPinCode, txtDistrictname, ddlState);
                    FillDistrictState(ddlPinCode1, txtDistrict1, ddlState1);
                    FillDistrictState(ddlPinCode2, txtDistrict2, ddlState2);
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
                    txtPassNo.MaxLength = 25;
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
                    txtPassNo.MaxLength = 25;
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
                    txtPassNo.MaxLength = 25;
                    //txtPassNo.Attributes.Remove("onblur");
                }

                txtDistrictname.Attributes.Add("readonly", "readonly");
                txtDistrict1.Attributes.Add("readonly", "readonly");
                txtDistrict2.Attributes.Add("readonly", "readonly");
                txtDatOfInc.Attributes.Add("readonly", "readonly");
                txtDtOfCom.Attributes.Add("readonly", "readonly");
                txtDate.Attributes.Add("readonly", "readonly");
                txtDateKYCver.Attributes.Add("readonly", "readonly");
                txtPinCode.Attributes.Add("readonly", "readonly");
                ddlPinCode1.Attributes.Add("readonly", "readonly");
                ddlPinCode2.Attributes.Add("readonly", "readonly");

                if (Request.QueryString["status"].ToString() != "view")
                {
                    divImg.Visible = false;
                }

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

            if (Session["PSSubmit"] != null && Session["PSSubmit"] == "Y")
            {
                btnSave.Enabled = false;
                //btnPartialUpdate.Enabled = true;
            }
            else
            {
                btnSave.Enabled = true;
                //btnPartialSave.Enabled = true;
            }

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
                    //if (chkAddRel.Checked == true && ChkUpdRelated.Checked == false)
                    //{
                    //    chkAddRel.Checked = false;
                    //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Check CONTACT DETAILS OF RELATED PERSON(All communication will be sent on provided MobileNo./Email-ID) checkbox')", true);
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please Check CONTACT DETAILS OF RELATED PERSON(All communication will be sent on provided MobileNo./Email-ID) checkbox')", true);
                    //    return;
                    //}
                }
                if (chkAddRel.Checked == true)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenRelatedPersonPage();", true);
                    lnkViewRel.Attributes.Add("style", "display:block");
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


        #region chkAddCtrl_Checked'
        protected void chkAddCtrl_Checked(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Status"] == "KMod")
                {
                    //if (chkAddCtrl.Checked == true && ChkUpdControlling.Checked == false)
                    //{
                    //    chkAddCtrl.Checked = false;
                    //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Check CONTACT DETAILS OF RELATED PERSON(All communication will be sent on provided MobileNo./Email-ID) checkbox')", true);
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please Check CONTACT DETAILS OF CONTROLLING PERSON (All communication will be sent on provided MobileNo. / Email-ID) checkbox')", true);
                    //    return;
                    //}
                }
                if (chkAddCtrl.Checked == true)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenControllingPersonPage();", true);
                    lnkViewCtrl.Attributes.Add("style", "display:block");
                    lnkViewCtrl.Enabled = true;
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
                            chkAddRel.Enabled = true;
                            gvMemDtls.Columns[1].Visible = false;
                            lnkViewRel.Visible = false;
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
                            gvCtrlPrson.DataSource = DT;
                            gvCtrlPrson.DataBind();
                            gvCtrlPrson.Visible = true;
                            Label2.Visible = false;
                            chkAddCtrl.Enabled = false;
                            gvCtrlPrson.Columns[1].Visible = false;
                        }
                        else if (Request.QueryString["status"].ToString() == "LMod")
                        {
                            //gvMemDtls.DataSource = null;
                            gvCtrlPrson.DataSource = DT;
                            gvCtrlPrson.DataBind();
                            gvCtrlPrson.Visible = true;
                            Label2.Visible = false;
                            chkAddCtrl.Enabled = false;
                            gvCtrlPrson.Columns[1].Visible = false;
                            lnkViewCtrl.Visible = true;
                        }
                        else
                        {
                            gvCtrlPrson.DataSource = DT;
                            gvCtrlPrson.DataBind();
                            gvCtrlPrson.Visible = true;
                            Label2.Visible = false;
                            chkAddCtrl.Enabled = false;
                        }
                    }
                }
                else
                {
                    Label2.Visible = true;
                    Label2.ForeColor = System.Drawing.Color.Red;
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
                }
                else
                {
                    gvMemDtls.DataSource = dt;
                    gvMemDtls.DataBind();
                    Session["dsRel"] = null;
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
                DataTable dt = (DataTable)ViewState["DTCtrl"];
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
                    gvCtrlPrson.DataSource = dt;
                    gvCtrlPrson.DataBind();
                    ViewState["DTCtrl"] = dt;
                    Session["dsCtrl"] = dt;
                }
                else
                {
                    gvCtrlPrson.DataSource = dt;
                    gvCtrlPrson.DataBind();
                    Session["dsCtrl"] = null;
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
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenRelatedPersonPageView(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "PMod")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenPartialRelatedPersonPageView(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "view")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenRelatedPersonPageViewNew(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "LMod")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenPartialRelatedPersonPageView(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "Reg")
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
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenControllingPersonPageView(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "PMod")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenPartialControllingPersonPageView(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "view")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenControllingPersonPageViewNew(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "LMod")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenPartialControllingPersonPageView(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "Reg")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenPartialControllingPersonPageView(" + RelRefnNo + "," + refno + ");", true);
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

                DataRow dr = dt.Rows[i];

                string RelRefnNo = Convert.ToString(dr[1]);
                string refno = Request.QueryString["refno"].ToString().Trim();
                if (Request.QueryString["status"].ToString() == "Mod")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenRelatedPersonPageEdit(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "PMod")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenPartialRelatedPersonPageEdit(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "LMod")
                {
                    //if (RelRefnNo == "")
                    //{
                    RelRefnNo = "0";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenPartialRelatedPersonPageEditNew(" + RelRefnNo + "," + refno + "," + i + ");", true);
                    //}
                    //else
                    //{
                    //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenPartialRelatedPersonPageEdit(" + RelRefnNo + "," + refno + ");", true);
                    //}

                }
                else if (Request.QueryString["status"].ToString() == "Reg")
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
                string refno = Request.QueryString["refno"].ToString().Trim();
                if (Request.QueryString["status"].ToString() == "Mod")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenControllingPersonPageEdit(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "PMod")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenPartialControllingPersonPageEdit(" + RelRefnNo + "," + refno + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "LMod")
                {
                    RelRefnNo = "0";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenPartialControllingPersonPageEditNew(" + RelRefnNo + "," + refno + "," + i + ");", true);
                }
                else if (Request.QueryString["status"].ToString() == "Reg")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenPartialControllingPersonPageEdit(" + RelRefnNo + "," + refno + ");", true);
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
                    //if (ddlCitizenship.SelectedIndex == 0)
                    //{
                    //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Citizenship')", true);
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select Citizenship')", true);
                    //    //ddlCitizenship.Focus();
                    //    return;
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
                        //chkPerAddress.Focus();
                        //chkPerAddress.Focus();
                        ddlAddressType.SelectedIndex = 0;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ddlAddressType_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please check correspondence / local address details')", true);
                        //chkPerAddress.Focus();
                        //chkPerAddress.Focus();
                        ddlAddressType1.SelectedIndex = 0;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ddlAddressType1_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
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
                        //chkPerAddress.Focus();
                        //chkPerAddress.Focus();
                        ddlAddressType2.SelectedIndex = 0;
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
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "ddlAddressType2_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
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
                //if (ddlProofOfAddress.SelectedIndex == 0)
                //{
                //    divAddProof.Visible = false;
                //}
                //else if (ddlProofOfAddress.SelectedIndex == 1)
                //{
                //    divAddProof.Visible = true;
                //    lblPassportNoAdd.Text = "Passport Number";
                //    llPassExpDateAdd.Text = "Passport Expiry Date";
                //    llPassExpDateAdd.Visible = true;
                //    txtPassExpDateAdd.Visible = true;
                //    divPassAdd.Visible = true;
                //    txtPassOthrAdd.Visible = false;
                //    txtPassNoAdd.Visible = true;
                //    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                //    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                //    //txtPassNo.MaxLength = 15;
                //    //txtPassNo.Attributes.Remove("onblur");
                //    //txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");
                //}
                //else if (ddlProofOfAddress.SelectedIndex == 2)
                //{
                //    divAddProof.Visible = true;
                //    lblPassportNoAdd.Text = "Driving Licence";
                //    llPassExpDateAdd.Text = "Driving Licence Expiry Date";
                //    llPassExpDateAdd.Visible = true;
                //    txtPassExpDateAdd.Visible = true;
                //    txtPassOthrAdd.Visible = false;
                //    divPassAdd.Visible = true;
                //    txtPassNoAdd.Visible = true;
                //    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                //    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                //    txtPassNoAdd.MaxLength = 15;
                //    txtPassNoAdd.Attributes.Remove("onblur");
                //}
                //else if (ddlProofOfAddress.SelectedIndex == 3)
                //{
                //    divAddProof.Visible = true;
                //    lblPassportNoAdd.Text = "UID(Aadhaar)";
                //    llPassExpDateAdd.Visible = false;
                //    txtPassExpDateAdd.Visible = false;
                //    txtPassOthrAdd.Visible = false;
                //    divPassAdd.Visible = false;
                //    txtPassNoAdd.Visible = true;
                //    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                //    txtPassNoAdd.MaxLength = 12;
                //    txtPassNoAdd.Text = "";
                //    txtPassNoAdd.Attributes.Remove("onblur");
                //    txtPassNoAdd.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                //}
                //else if (ddlProofOfAddress.SelectedIndex == 4)
                //{
                //    divAddProof.Visible = true;
                //    lblPassportNoAdd.Text = "Voter ID Card";
                //    llPassExpDateAdd.Visible = false;
                //    txtPassExpDateAdd.Visible = false;
                //    txtPassOthrAdd.Visible = false;
                //    divPassAdd.Visible = false;
                //    txtPassNoAdd.Visible = true;
                //    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                //    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                //    //txtPassNo.MaxLength = 15;
                //    txtPassNoAdd.Attributes.Remove("onblur");
                //}


                //else if (ddlProofOfAddress.SelectedIndex == 5)
                //{
                //    divAddProof.Visible = true;
                //    lblPassportNoAdd.Text = "NREGA Job Card";
                //    llPassExpDateAdd.Visible = false;
                //    txtPassNoAdd.Visible = true;
                //    txtPassOthrAdd.Visible = false;
                //    divPassAdd.Visible = false;
                //    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                //    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                //    txtPassNoAdd.MaxLength = 20;
                //    txtPassNoAdd.Attributes.Remove("onblur");
                //}
                //else
                //{
                //    divAddProof.Visible = true;
                //    lblPassportNoAdd.Text = "Document Name";
                //    llPassExpDateAdd.Text = "Identification Number";
                //    txtPassExpDateAdd.Visible = true;
                //    llPassExpDateAdd.Visible = true;
                //    divPassAdd.Visible = true;
                //    llPassExpDateAdd.Visible = true; txtPassExpDateAdd.Visible = false;
                //    txtPassOthrAdd.Visible = true;
                //    txtPassNoAdd.Visible = true;
                //    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                //    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                //    txtPassNoAdd.MaxLength = 15;
                //    txtPassNoAdd.Attributes.Remove("onblur");
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
                //Response.Redirect("CKYCSearch.aspx?status=LMod");
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
                    ddlProofOfAddress1.SelectedValue = ddlProofOfAddress.SelectedValue;
                    ddlAddressType1.Enabled = false;
                    ddlProofOfAddress1.Enabled = false;
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
                    ddlProofOfAddress1.SelectedIndex = 0;
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
                    ddlPinCode1.Enabled = true;
                    btnsearchddlPinCode1.Enabled = true;
                    //ddlState1.SelectedItem.Text = "";
                    ddlState1.SelectedIndex = 0;
                    //added by ramesh on dated 21-05-2018
                    ddlAddressType1.Enabled = true;
                    ddlProofOfAddress1.Enabled = true;
                    ddlState1.Enabled = true;
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

        #region DROPDOWN 'chkCuurentAddress'SELECTEDINDEXCHANGED EVENT
        protected void chkCurrentAdd_Checked(object sender, EventArgs e)
        {
            try
            {
                if (chkCurrentAdd.Checked == true)
                {
                    if (ddlCountryCode.SelectedValue != "IN")
                    {
                        chkAddResident.Checked = true;
                        chkCorresAdd.Checked = false;
                        //FillDistrictState(ddlPinCode, ddlDistrict2, ddlState2);

                        ddlAddressType2.SelectedValue = ddlAddressType.SelectedValue;
                        ddlProofOfAddress2.SelectedValue = ddlProofOfAddress.SelectedValue;
                        ddlAddressType2.Enabled = false;
                        ddlProofOfAddress2.Enabled = false;
                        txtAddLine1.Text = txtAddressLine1.Text;
                        txtAddLine1.Enabled = false;
                        txtAddLine2.Text = txtAddressLine2.Text;
                        txtAddLine2.Enabled = false;
                        txtAddLine3.Text = txtAddressLine3.Text;
                        txtAddLine3.Enabled = false;
                        txtCity2.Text = txtCity.Text;
                        txtCity2.Enabled = false;
                        ddlState2.SelectedValue = ddlState.SelectedValue;
                        ddlState2.Enabled = false;
                        ddlPinCode2.Text = txtPinCode.Text;
                        ddlPinCode2.Enabled = false;
                        btnsearchddlPinCode2.Enabled = false;
                        txtDistrict2.Text = txtDistrictname.Text;
                        txtDistrict2.Enabled = false;
                        ddlIsoCountryCode.SelectedValue = ddlCountryCode.SelectedValue;
                        ddlIsoCountryCode.Enabled = false;
                    }
                    else
                    {
                        chkCurrentAdd.Checked = false;
                        ddlAddressType2.SelectedIndex = 0;
                        ddlProofOfAddress2.SelectedIndex = 0;
                        ddlAddressType2.Enabled = true;
                        ddlProofOfAddress2.Enabled = true;
                        txtAddLine1.Text = "";
                        txtAddLine1.Enabled = true;
                        txtAddLine2.Text = "";
                        txtAddLine2.Enabled = true;
                        txtAddLine3.Text = "";
                        txtAddLine3.Enabled = true;
                        txtCity2.Text = "";
                        txtCity2.Enabled = true;
                        ddlState2.SelectedIndex = 0;
                        ddlState2.Enabled = true;
                        ddlPinCode2.Text = "";
                        //ddlPinCode2.Enabled = false;
                        btnsearchddlPinCode2.Enabled = true;
                        txtDistrict2.Text = "";
                        //txtDistrict2.Enabled = false;
                        ddlIsoCountryCode.SelectedIndex = 0;
                        ddlIsoCountryCode.Enabled = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Country is India in Current / Permanent / Overseas Address details.');", true);
                    }
                }
                else
                {
                    ddlAddressType2.SelectedIndex = 0;
                    ddlProofOfAddress2.SelectedIndex = 0;
                    ddlAddressType2.Enabled = true;
                    ddlProofOfAddress2.Enabled = true;
                    txtAddLine1.Text = "";
                    txtAddLine1.Enabled = true;
                    txtAddLine2.Text = "";
                    txtAddLine2.Enabled = true;
                    txtAddLine3.Text = "";
                    txtAddLine3.Enabled = true;
                    txtCity2.Text = "";
                    txtCity2.Enabled = true;
                    ddlState2.SelectedIndex = 0;
                    ddlState2.Enabled = true;
                    ddlPinCode2.Text = "";
                    //ddlPinCode2.Enabled = false;
                    btnsearchddlPinCode2.Enabled = true;
                    txtDistrict2.Text = "";
                    //txtDistrict2.Enabled = false;
                    ddlIsoCountryCode.SelectedIndex = 0;
                    ddlIsoCountryCode.Enabled = true;
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
                        chkAddResident.Checked = true;
                        chkCurrentAdd.Checked = false;

                        ddlAddressType2.SelectedValue = ddlAddressType1.SelectedValue;
                        ddlProofOfAddress2.SelectedValue = ddlProofOfAddress1.SelectedValue;
                        ddlAddressType2.Enabled = false;
                        ddlProofOfAddress2.Enabled = false;
                        txtAddLine1.Text = txtLocAddLine1.Text;
                        txtAddLine1.Enabled = false;
                        txtAddLine2.Text = txtLocAddLine2.Text;
                        txtAddLine2.Enabled = false;
                        txtAddLine3.Text = txtLocAddLine3.Text;
                        txtAddLine3.Enabled = false;
                        txtCity2.Text = txtCity1.Text;
                        txtCity2.Enabled = false;
                        ddlState2.SelectedValue = ddlState1.SelectedValue;
                        ddlState2.Enabled = false;
                        ddlPinCode2.Text = ddlPinCode1.Text;
                        ddlPinCode2.Enabled = false;
                        btnsearchddlPinCode2.Enabled = false;
                        txtDistrict2.Text = txtDistrict1.Text;
                        txtDistrict2.Enabled = false;
                        ddlIsoCountryCode.SelectedValue = ddlCountryCode1.SelectedValue;
                        ddlIsoCountryCode.Enabled = false;
                    }
                    else
                    {
                        chkCorresAdd.Checked = false;
                        ddlAddressType2.SelectedIndex = 0;
                        ddlProofOfAddress2.SelectedIndex = 0;
                        ddlAddressType2.Enabled = true;
                        ddlProofOfAddress2.Enabled = true;
                        txtAddLine1.Text = "";
                        txtAddLine1.Enabled = true;
                        txtAddLine2.Text = "";
                        txtAddLine2.Enabled = true;
                        txtAddLine3.Text = "";
                        txtAddLine3.Enabled = true;
                        txtCity2.Text = "";
                        txtCity2.Enabled = true;
                        ddlState2.SelectedIndex = 0;
                        ddlState2.Enabled = true;
                        ddlPinCode2.Text = "";
                        //ddlPinCode2.Enabled = false;
                        btnsearchddlPinCode2.Enabled = true;
                        txtDistrict2.Text = "";
                        //txtDistrict2.Enabled = false;
                        ddlIsoCountryCode.SelectedIndex = 0;
                        ddlIsoCountryCode.Enabled = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Country is India in Correspondence / Local Address details.');", true);
                    }
                }
                else
                {
                    ddlAddressType2.SelectedIndex = 0;
                    ddlProofOfAddress2.SelectedIndex = 0;
                    ddlAddressType2.Enabled = true;
                    ddlProofOfAddress2.Enabled = true;
                    txtAddLine1.Text = "";
                    txtAddLine1.Enabled = true;
                    txtAddLine2.Text = "";
                    txtAddLine2.Enabled = true;
                    txtAddLine3.Text = "";
                    txtAddLine3.Enabled = true;
                    txtCity2.Text = "";
                    txtCity2.Enabled = true;
                    ddlState2.SelectedIndex = 0;
                    ddlState2.Enabled = true;
                    ddlPinCode2.Text = "";
                    //ddlPinCode2.Enabled = false;
                    btnsearchddlPinCode2.Enabled = true;
                    txtDistrict2.Text = "";
                    //txtDistrict2.Enabled = false;
                    ddlIsoCountryCode.SelectedIndex = 0;
                    ddlIsoCountryCode.Enabled = true;
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

                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit") as LinkButton;
                LinkButton lnkdelete = (LinkButton)e.Row.FindControl("lnkdelete") as LinkButton;

                if (Request.QueryString["Status"].ToString() == "LMod")
                {
                    gvMemDtls.Columns[10].Visible = true;
                    lnkView.Visible = false;
                }
                else if (Request.QueryString["Status"].ToString() == "view")
                {
                    gvMemDtls.Columns[10].Visible = true;
                    lnkView.Visible = true;
                    lnkEdit.Visible = false;
                    lnkdelete.Visible = false;
                }
                else if (Request.QueryString["Status"].ToString() == "Reg")
                {
                    lnkView.Visible = false;

                }
                else
                {
                    gvMemDtls.Columns[10].Visible = false;
                    lnkView.Visible = true;
                }


                //if (Request.QueryString["Status"].ToString() == "Reg")
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
                    gvCtrlPrson.Columns[10].Visible = true;
                    lnkView.Visible = false;
                }
                else if (Request.QueryString["Status"].ToString() == "Reg")
                {
                    lnkView.Visible = false;
                }
                else if (Request.QueryString["Status"].ToString() == "view")
                {
                    gvCtrlPrson.Columns[10].Visible = true;
                    lnkView.Visible = true;
                    lnkEdit.Visible = false;
                    lnkdelete.Visible = false;
                }
                else
                {
                    gvCtrlPrson.Columns[10].Visible = false;
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
                lblAccountType.Text = olng.GetItemDesc("lblAccountType");
                lblAccountSubType.Text = olng.GetItemDesc("lblAccountSubType");
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

        protected void chkUSReport_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOtherReport.Checked == true)
            {
                chkOtherReport.Checked = false;
            }

            if (chkUSReport.Checked == true)
            {
                htParam.Clear();
                htParam.Add("@LookupCode", "KEntAccHolTypUsRept");
                FillDropdowns("prc_getDDLLookUpData", htParam, ddlAccHolderType, "CKYCConnectionString", true);
            }
            else
            {
                ddlAccHolderType.Items.Clear();
                ddlAccHolderType.Items.Insert(0, new ListItem("Select", ""));
            }
        }

        protected void chkSelfCerti_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelfCerti.Checked == true)
            {
                chkTrueCopies.Checked = false;
                chkNotary.Checked = false;
            }
            //else
            //{

            //}

            //if (chkUSReport.Checked == true)
            //{
            //    htParam.Clear();
            //    htParam.Add("@LookupCode", "KEntAccHolTypUsRept");
            //    FillDropdowns("prc_getDDLLookUpData", htParam, ddlAccHolderType, "CKYCConnectionString", true);
            //}
            //else
            //{
            //    ddlAccHolderType.Items.Clear();
            //    ddlAccHolderType.Items.Insert(0, new ListItem("Select", ""));
            //}
        }

        protected void chkTrueCopies_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTrueCopies.Checked == true)
            {
                chkSelfCerti.Checked = false;
                chkNotary.Checked = false;
            }

            //if (chkUSReport.Checked == true)
            //{
            //    htParam.Clear();
            //    htParam.Add("@LookupCode", "KEntAccHolTypUsRept");
            //    FillDropdowns("prc_getDDLLookUpData", htParam, ddlAccHolderType, "CKYCConnectionString", true);
            //}
            //else
            //{
            //    ddlAccHolderType.Items.Clear();
            //    ddlAccHolderType.Items.Insert(0, new ListItem("Select", ""));
            //}
        }

        protected void chkNotary_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNotary.Checked == true)
            {
                chkSelfCerti.Checked = false;
                chkTrueCopies.Checked = false;
            }

            //if (chkUSReport.Checked == true)
            //{
            //    htParam.Clear();
            //    htParam.Add("@LookupCode", "KEntAccHolTypUsRept");
            //    FillDropdowns("prc_getDDLLookUpData", htParam, ddlAccHolderType, "CKYCConnectionString", true);
            //}
            //else
            //{
            //    ddlAccHolderType.Items.Clear();
            //    ddlAccHolderType.Items.Insert(0, new ListItem("Select", ""));
            //}
        }

        protected void FillddlPageLoad()
        {
            //htParam.Clear();
            //htParam.Add("@LookupCode", "KEntConstTyp");
            //FillDropdowns("prc_getDDLLookUpData", htParam, ddlNatureOfBuss, "CKYCConnectionString", true);

            htParam.Clear();
            htParam.Add("@LookupCode", "KEntIdentTyp");
            FillDropdowns("prc_getDDLLookUpData", htParam, ddlIdentyType, "CKYCConnectionString", true);

            htParam.Clear();
            htParam.Add("@LookupCode", "KEntPoI");
            FillDropdowns("prc_getDDLLookUpData", htParam, ddlCertifiecopy, "CKYCConnectionString", true);

            htParam.Clear();
            htParam.Add("@LookupCode", "KAddr");
            FillDropdowns("prc_getDDLLookUpData", htParam, ddlAddressType, "CKYCConnectionString", true);
            FillDropdowns("prc_getDDLLookUpData", htParam, ddlAddressType1, "CKYCConnectionString", true);
            FillDropdowns("prc_getDDLLookUpData", htParam, ddlAddressType2, "CKYCConnectionString", true);

            htParam.Clear();
            htParam.Add("@LookupCode", "KEntPoA");
            FillDropdowns("prc_getDDLLookUpData", htParam, ddlProofOfAddress, "CKYCConnectionString", true);
            FillDropdowns("prc_getDDLLookUpData", htParam, ddlProofOfAddress1, "CKYCConnectionString", true);
            FillDropdowns("prc_getDDLLookUpData", htParam, ddlProofOfAddress2, "CKYCConnectionString", true);

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

                    ddlCountryOfRsidens.DataSource = dt;
                    ddlCountryOfRsidens.DataTextField = "Country_Desc";
                    ddlCountryOfRsidens.DataValueField = "Country_CODE";
                    ddlCountryOfRsidens.DataBind();
                    ddlCountryOfRsidens.Items.Insert(0, new ListItem("Select", string.Empty));
                    ddlCountryOfRsidens.SelectedValue = "IN";   //105

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

        protected void FillConstType()
        {
            try
            {
                htParam.Clear();
                htParam.Add("@LookupCode", "KConstTyp");
                FillDropdowns("prc_getDDLLookUpData", htParam, ddlNatureOfBuss, "CKYCConnectionString", true);

                if (ddlNatureOfBuss.Items.Count > 1)
                {
                    ddlNatureOfBuss.SelectedIndex = 2;
                }




                htParam.Clear();
                htParam.Add("@LookupCode", "KNoofPerson");
                FillDropdowns("prc_getDDLLookUpData", htParam, ddlNumberOfPerson, "CKYCConnectionString", true);





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

        protected void chkOtherReport_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUSReport.Checked == true)
            {
                chkUSReport.Checked = false;
            }

            if (chkOtherReport.Checked == true)
            {
                htParam.Clear();
                htParam.Add("@LookupCode", "KEntAccHolTypOthRept");
                FillDropdowns("prc_getDDLLookUpData", htParam, ddlAccHolderType, "CKYCConnectionString", true);
            }
            else
            {
                ddlAccHolderType.Items.Clear();
                ddlAccHolderType.Items.Insert(0, new ListItem("Select", ""));
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

                    if (chkUSReport.Checked == true)
                    {
                        htParam.Add("@ACC_TYPE_FLG", "01");
                    }

                    if (chkOtherReport.Checked == true)
                    {
                        htParam.Add("@ACC_TYPE_FLG", "02");
                    }

                    if (ddlAccHolderType.SelectedValue != "")
                    {
                        htParam.Add("@ACC_TYPE", ddlAccHolderType.SelectedValue);
                    }
                    htParam.Add("@CompType", ddlNatureOfBuss.SelectedValue.Trim());
                    htParam.Add("@PAN", txtPanNo.Text.ToString());
                    htParam.Add("@EntName", txtKYCName.Text.Trim());
                    htParam.Add("@DtofIncorporation", txtDatOfInc.Text.Trim());
                    htParam.Add("@DtofCommencementofbusi", txtDtOfCom.Text.Trim());
                    htParam.Add("@PlaceofIncorportation", txtPlaceOfInc.Text.Trim());
                    htParam.Add("@CountryofIncorporation", ddlCountrOfInc.SelectedValue.Trim());
                    htParam.Add("@CountryOfRsidens", ddlCountryOfRsidens.SelectedValue.Trim());
                    htParam.Add("@IdentyType", ddlIdentyType.SelectedValue.Trim());
                    htParam.Add("@TAX_NUM", txtTypeIdentiNo.Text.Trim());
                    htParam.Add("@TINIssuingCountry", ddlTINCountry.SelectedValue);
                    htParam.Add("@NoOfControlPrsnOI", ddlNumberOfPerson.SelectedValue);

                    htParam.Add("@IDENT_NUM_ID1", ddlCertifiecopy.SelectedValue);
                    htParam.Add("@IDNO", txtPassNo.Text.Trim());

                    if (chkPerAddress.Checked == true)
                    {
                        htParam.Add("@CnctType1", "P1");
                        htParam.Add("@PERM_TYPE", ddlAddressType.SelectedValue);
                        htParam.Add("@PERM_LINE1", txtAddressLine1.Text.Trim());
                        htParam.Add("@PERM_LINE2", txtAddressLine2.Text.Trim());
                        htParam.Add("@PERM_LINE3", txtAddressLine3.Text.Trim());
                        htParam.Add("@PERM_CITY", txtCity.Text.Trim());
                        htParam.Add("@PERM_DIST", txtDistrictname.Text);
                        htParam.Add("@PERM_PIN", txtPinCode.Text);
                        htParam.Add("@PERM_STATE", ddlState.SelectedValue);
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

                    htParam.Add("@PERM_POA", ddlProofOfAddress.SelectedValue);

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
                        htParam.Add("@CORRES_TYPE", ddlAddressType1.SelectedValue);
                        htParam.Add("@CORRES_POA", ddlProofOfAddress1.SelectedValue);
                        htParam.Add("@CORRES_LINE1", txtLocAddLine1.Text);
                        htParam.Add("@CORRES_LINE2", txtLocAddLine2.Text);
                        htParam.Add("@CORRES_LINE3", txtLocAddLine3.Text);
                        htParam.Add("@CORRES_CITY ", txtCity1.Text.Trim());
                        htParam.Add("@CORRES_DIST", txtDistrict1.Text);
                        htParam.Add("@CORRES_PIN", ddlPinCode1.Text);
                        htParam.Add("@CORRES_STATE", ddlState1.SelectedValue);
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

                    if (chkCurrentAdd.Checked == true)
                    {
                        htParam.Add("@SameasLocalAddressFlagJ1", "01");
                    }
                    else
                    {
                        htParam.Add("@SameasLocalAddressFlagJ1", "");
                    }

                    if (chkCorresAdd.Checked == true)
                    {
                        htParam.Add("@SameasLocalAddressFlagJ2", "01");
                    }
                    else
                    {
                        htParam.Add("@SameasLocalAddressFlagJ2", "");
                    }

                    if (chkAddResident.Checked == true)
                    {
                        htParam.Add("@CnctType3", "J1");
                        htParam.Add("@JURI_TYPE", ddlAddressType2.SelectedValue);
                        //htParam.Add("@CORRES_POA", ddlProofOfAddress1.SelectedValue);
                        htParam.Add("@JURI_LINE1", txtAddLine1.Text.Trim());
                        htParam.Add("@JURI_LINE2", txtAddLine2.Text.Trim());
                        htParam.Add("@JURI_LINE3", txtAddLine3.Text.Trim());
                        htParam.Add("@JURI_CITY", txtCity2.Text.Trim());
                        htParam.Add("@JURI_DIST", txtDistrict2.Text.Trim());
                        htParam.Add("@JURI_PIN", ddlPinCode2.Text);
                        htParam.Add("@JURI_STATE", ddlState2.SelectedValue);
                        htParam.Add("@JURI_COUNTRY", ddlIsoCountryCode.SelectedValue);
                    }
                    else
                    {
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
                    }
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

                    if (chkSelfCerti.Checked == true)
                    {
                        htParam.Remove("@DOC_SUB");
                        htParam.Add("@DOC_SUB", "01");
                    }
                    else if (chkTrueCopies.Checked == true)
                    {
                        htParam.Remove("@DOC_SUB");
                        htParam.Add("@DOC_SUB", "02");
                    }
                    else if (chkNotary.Checked == true)
                    {
                        htParam.Remove("@DOC_SUB");
                        htParam.Add("@DOC_SUB", "03");
                    }

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
                                if (chkAddCtrl.Checked == true)
                                {
                                    htParam.Add("@AddDelFlagRel ", "01");
                                }
                                else if (chkAddCtrl.Checked == false)
                                {
                                    htParam.Add("@AddDelFlagRel ", "02");
                                }

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

                // Res = ""; // objVal.Validation(chkNormal, chkSimplified, Chksmall, cboTitle, txtGivenName, txtLastName, rbtFS, cboTitle2, txtGivenName2, txtLastName2, cboTitle3, txtGivenName3, txtLastName3, txtDOB, cboGender,
                //ddlMaritalStatus, ddlCitizenship, ddlResStatus, ddlOccupation, ddlOccuSubType, chkTick, ddlIsoCountryCode2, txtIDResTax, txtDOBRes, ddlIsoCountry, ddlProofIdentity,
                //txtPassNo, txtPassExpDate, chkPerAddress, ddlAddressType, ddlProofOfAddress, txtAddressLine1, txtCity, ddlPinCode, chkLocalAddress, txtLocAddLine1,
                //txtCity1, ddlPinCode1, chkAddResident, txtAddLine1, txtCity2, ddlIsoCountryCode, chkAppDeclare1, txtDate, txtPlace,
                //txtDateKYCver, txtEmpName, txtEmpCode, txtEmpDesignation, txtEmpBranch, txtInsName, txtInsCode, ddlIsoCountryCodeOthr, ddlIsoCountry, txtPassOthr);  //txtPassOthr,
                //Res = "";
                Res = objVal.LegalEntityDtlsValidation(ddlNatureOfBuss, txtConstitutionTypeothers, txtRefNumber,
                    txtKYCName, txtDatOfInc, txtDtOfCom, txtPlaceOfInc, ddlCountrOfInc, txtTypeIdentiNo,
                    ddlTINCountry, txtPanNo, ddlCertifiecopy, chkPerAddress, chkLocalAddress, chkCuurentAddress,
                    ddlProofOfAddress, txtAddressLine1, ddlState, ddlState1, ddlCountryCode, ddlState1, ddlState1,
                    ddlCountryCode1, txtCity, txtLocAddLine1, txtCity1,
                     txtTelOff, txtTelOff2, txtTelRes, txtTelRes2, txtMobile, txtMobile2, txtMobile1, txtMobile3, txtFax1, txtFax2,
                    chkAddRel, chkAppDeclare1, chkAppDeclare2, chkAppDeclare3, txtDate, txtPlace, new CheckBox(), new CheckBox(), new CheckBox(),
                    chkHigh, chkMedium, chkLow, txtPassNo, new DropDownList(),chkDone, txtDateKYCver, txtEmpName, txtEmpCode,
                    txtEmpDesignation, txtEmpBranch, txtInsName, txtInsCode,gvMemDtls, txtPassNoAdd, new TextBox());

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
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Add atleast One Relative Details')", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please Add atleast One Relative Details')", true);
                            //chkAddRel.Focus();
                            chkAddRel.Checked = true;
                            return;
                        }
                    }

                    if (chkAddCtrl.Checked == true)
                    {
                        //if (dsRel.Tables.Count == 0)
                        if (dtCtrl1 == null)
                        {
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Add atleast One Relative Details')", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please Add Controlling Details')", true);
                            //chkAddRel.Focus();
                            chkAddCtrl.Checked = true;
                            return;
                        }

                        //int txtCtrlNo = Convert.ToInt32(ddlNumberOfPerson.SelectedValue);
                        //int dtCtrlCnt = dtCtrl.Rows.Count;

                        //if (dtCtrlCnt != txtCtrlNo)
                        //{
                        //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Add atleast One Relative Details')", true);
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "AlertMsg('Please Add '" + ddlNumberOfPerson.SelectedItem.Text + "' Controlling Details')", true);
                        //    //chkAddRel.Focus();
                        //    chkAddCtrl.Checked = true;
                        //    return;
                        //}
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

                    if (chkUSReport.Checked == true)
                    {
                        htParam.Add("@ACC_TYPE_FLG", "01");
                    }

                    if (chkOtherReport.Checked == true)
                    {
                        htParam.Add("@ACC_TYPE_FLG", "02");
                    }

                    if (ddlAccHolderType.SelectedValue != "")
                    {
                        htParam.Add("@AccType", ddlAccHolderType.SelectedValue);
                    }
                    htParam.Add("@CompType", ddlNatureOfBuss.SelectedValue.Trim());


                    htParam.Add("@CKYCNo", txtKYCNumber.Text.Trim());

                    htParam.Add("@EntName", txtKYCName.Text.Trim());
                    htParam.Add("@DtofIncorporation", txtDatOfInc.Text.Trim());
                    htParam.Add("@DtofCommencementofbusi", txtDtOfCom.Text.Trim());
                    htParam.Add("@PlaceofIncorportation", txtPlaceOfInc.Text.Trim());
                    htParam.Add("@CountryofIncorporation", ddlCountrOfInc.SelectedValue.Trim());
                    htParam.Add("@CountryOfRsidens", ddlCountryOfRsidens.SelectedValue.Trim());
                    htParam.Add("@IdentyType", ddlIdentyType.SelectedValue.Trim());
                    htParam.Add("@TAX_NUM", txtTypeIdentiNo.Text.Trim());
                    htParam.Add("@TINIssuingCountry", ddlTINCountry.SelectedValue);
                    htParam.Add("@NoOfControlPrsnOI", ddlNumberOfPerson.SelectedValue);

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
                        htParam.Add("@PER_ADDTYPE", ddlAddressType.SelectedValue);
                        htParam.Add("@PER_ADDPROOF", ddlProofOfAddress.SelectedValue);
                        htParam.Add("@PER_ADDLINE1", txtAddressLine1.Text.Trim());
                        htParam.Add("@PER_ADDLINE2", txtAddressLine2.Text.Trim());
                        htParam.Add("@PER_ADDLINE3", txtAddressLine3.Text.Trim());
                        htParam.Add("@PER_CITY", txtCity.Text.Trim());
                        htParam.Add("@PER_DISTRICT", txtDistrictname.Text);
                        htParam.Add("@PER_PIN", txtPinCode.Text);
                        htParam.Add("@PER_STATECODE", ddlState.SelectedValue);
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
                        htParam.Add("@cAddType", ddlAddressType1.SelectedValue);
                        htParam.Add("@cAddIdType", ddlProofOfAddress1.SelectedValue);
                        htParam.Add("@CUR_ADDLINE1", txtLocAddLine1.Text);
                        htParam.Add("@CUR_ADDLINE2", txtLocAddLine2.Text);
                        htParam.Add("@CUR_ADDLINE3", txtLocAddLine3.Text);
                        htParam.Add("@CUR_CITY", txtCity1.Text.Trim());
                        htParam.Add("@CUR_DISTRICT", txtDistrict1.Text);
                        htParam.Add("@CUR_PIN", ddlPinCode1.Text);
                        htParam.Add("@CUR_STATECODE", ddlState1.SelectedValue);
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

                    if (chkCurrentAdd.Checked == true)
                    {
                        htParam.Add("@SameasLocalAddressFlagJ1", "01");
                    }
                    else
                    {
                        htParam.Add("@SameasLocalAddressFlagJ1", "");
                    }

                    if (chkCorresAdd.Checked == true)
                    {
                        htParam.Add("@SameasLocalAddressFlagJ2", "01");
                    }
                    else
                    {
                        htParam.Add("@SameasLocalAddressFlagJ2", "");
                    }

                    if (chkAddResident.Checked == true)
                    {
                        htParam.Add("@CnctType3", "J1");
                        htParam.Add("@fAddType", ddlAddressType2.SelectedValue);
                        htParam.Add("@fAddIdType", ddlProofOfAddress2.SelectedValue);
                        htParam.Add("@FRN_ADDLINE1", txtAddLine1.Text.Trim());
                        htParam.Add("@FRN_ADDLINE2", txtAddLine2.Text.Trim());
                        htParam.Add("@FRN_ADDLINE3", txtAddLine3.Text.Trim());
                        htParam.Add("@FRN_CITY", txtCity2.Text.Trim());
                        htParam.Add("@FRN_DISTRICT", txtDistrict2.Text);
                        htParam.Add("@FRN_PIN", ddlPinCode2.Text);
                        htParam.Add("@FRN_STATECODE", ddlState.SelectedValue);
                        htParam.Add("@FRN_COUNTRY_CODE", ddlIsoCountryCode.SelectedValue);

                        //if (chkCurrentAdd.Checked == true)
                        //{
                        //    htParam.Add("@SameasLocalAddressFlagJ1", "01");
                        //}
                        //else
                        //{
                        //    htParam.Add("@SameasLocalAddressFlagJ1", System.DBNull.Value);
                        //}

                        //if (chkCorresAdd.Checked == true)
                        //{
                        //    htParam.Add("@SameasLocalAddressFlagJ2", "01");
                        //}
                        //else
                        //{
                        //    htParam.Add("@SameasLocalAddressFlagJ2", System.DBNull.Value);
                        //}
                    }
                    else
                    {
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
                    }


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

                    if (chkSelfCerti.Checked == true)
                    {
                        htParam.Remove("@kycCertDoc");
                        htParam.Add("@kycCertDoc", "01");
                    }
                    else if (chkTrueCopies.Checked == true)
                    {
                        htParam.Remove("@kycCertDoc");
                        htParam.Add("@kycCertDoc", "02");
                    }
                    else if (chkNotary.Checked == true)
                    {
                        htParam.Remove("@kycCertDoc");
                        htParam.Add("@kycCertDoc", "03");
                    }

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
                                if (chkAddCtrl.Checked == true)
                                {
                                    htParam.Add("@AddDelFlagCtrl", "01");
                                }
                                else if (chkAddCtrl.Checked == false)
                                {
                                    htParam.Add("@AddDelFlagCtrl", "02");
                                }

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

                    if (gvCtrlPrson.Visible == true)
                    {

                        foreach (GridViewRow row in gvCtrlPrson.Rows)
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

                    if (chkUSReport.Checked == true)
                    {
                        htParam.Add("@ACC_TYPE_FLG", "01");
                    }

                    if (chkOtherReport.Checked == true)
                    {
                        htParam.Add("@ACC_TYPE_FLG", "02");
                    }

                    if (ddlAccHolderType.SelectedValue != "")
                    {
                        htParam.Add("@ACC_TYPE", ddlAccHolderType.SelectedValue);
                    }
                    htParam.Add("@CompType", ddlNatureOfBuss.SelectedValue.Trim());
                    htParam.Add("@PAN", txtPanNo.Text.ToString());
                    htParam.Add("@EntName", txtKYCName.Text.Trim());
                    htParam.Add("@DtofIncorporation", txtDatOfInc.Text.Trim());
                    htParam.Add("@DtofCommencementofbusi", txtDtOfCom.Text.Trim());
                    htParam.Add("@PlaceofIncorportation", txtPlaceOfInc.Text.Trim());
                    htParam.Add("@CountryofIncorporation", ddlCountrOfInc.SelectedValue.Trim());
                    htParam.Add("@CountryOfRsidens", ddlCountryOfRsidens.SelectedValue.Trim());
                    htParam.Add("@IdentyType", ddlIdentyType.SelectedValue.Trim());
                    htParam.Add("@TAX_NUM", txtTypeIdentiNo.Text.Trim());
                    htParam.Add("@TINIssuingCountry", ddlTINCountry.SelectedValue);
                    htParam.Add("@NoOfControlPrsnOI", ddlNumberOfPerson.SelectedValue);

                    htParam.Add("@IDENT_NUM_ID1", ddlCertifiecopy.SelectedValue);
                    htParam.Add("@IDNO", txtPassNo.Text.Trim());

                    if (chkPerAddress.Checked == true)
                    {
                        htParam.Add("@CnctType1", "P1");
                        htParam.Add("@PERM_TYPE", ddlAddressType.SelectedValue);
                        htParam.Add("@PERM_LINE1", txtAddressLine1.Text.Trim());
                        htParam.Add("@PERM_LINE2", txtAddressLine2.Text.Trim());
                        htParam.Add("@PERM_LINE3", txtAddressLine3.Text.Trim());
                        htParam.Add("@PERM_CITY", txtCity.Text.Trim());
                        htParam.Add("@PERM_DIST", txtDistrictname.Text);
                        htParam.Add("@PERM_PIN", txtPinCode.Text);
                        htParam.Add("@PERM_STATE", ddlState.SelectedValue);
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

                    htParam.Add("@PERM_POA", ddlProofOfAddress.SelectedValue);

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
                        htParam.Add("@CORRES_TYPE", ddlAddressType1.SelectedValue);
                        htParam.Add("@CORRES_POA", ddlProofOfAddress1.SelectedValue);
                        htParam.Add("@CORRES_LINE1", txtLocAddLine1.Text);
                        htParam.Add("@CORRES_LINE2", txtLocAddLine2.Text);
                        htParam.Add("@CORRES_LINE3", txtLocAddLine3.Text);
                        htParam.Add("@CORRES_CITY ", txtCity1.Text.Trim());
                        htParam.Add("@CORRES_DIST", txtDistrict1.Text);
                        htParam.Add("@CORRES_PIN", ddlPinCode1.Text);
                        htParam.Add("@CORRES_STATE", ddlState1.SelectedValue);
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

                    if (chkCurrentAdd.Checked == true)
                    {
                        htParam.Add("@SameasLocalAddressFlagJ1", "01");
                    }
                    else
                    {
                        htParam.Add("@SameasLocalAddressFlagJ1", "");
                    }

                    if (chkCorresAdd.Checked == true)
                    {
                        htParam.Add("@SameasLocalAddressFlagJ2", "01");
                    }
                    else
                    {
                        htParam.Add("@SameasLocalAddressFlagJ2", "");
                    }

                    if (chkAddResident.Checked == true)
                    {
                        htParam.Add("@CnctType3", "J1");
                        htParam.Add("@JURI_TYPE", ddlAddressType2.SelectedValue);
                        //htParam.Add("@CORRES_POA", ddlProofOfAddress1.SelectedValue);
                        htParam.Add("@JURI_LINE1", txtAddLine1.Text.Trim());
                        htParam.Add("@JURI_LINE2", txtAddLine2.Text.Trim());
                        htParam.Add("@JURI_LINE3", txtAddLine3.Text.Trim());
                        htParam.Add("@JURI_CITY", txtCity2.Text.Trim());
                        htParam.Add("@JURI_PIN", ddlPinCode2.Text);
                        htParam.Add("@JURI_DISTRICT", txtDistrict2.Text);
                        htParam.Add("@JURI_STATE", ddlState2.SelectedValue);
                        htParam.Add("@JURI_COUNTRY", ddlIsoCountryCode.SelectedValue);
                    }
                    else
                    {
                        htParam.Add("@CnctType3", "");
                        htParam.Add("@JURI_TYPE", System.DBNull.Value);
                        //htParam.Add("@CORRES_POA", ddlProofOfAddress1.SelectedValue);
                        htParam.Add("@JURI_LINE1", System.DBNull.Value);
                        htParam.Add("@JURI_LINE2", System.DBNull.Value);
                        htParam.Add("@JURI_LINE3", System.DBNull.Value);
                        htParam.Add("@JURI_CITY", System.DBNull.Value);
                        htParam.Add("@JURI_PIN", System.DBNull.Value);
                        htParam.Add("@JURI_DISTRICT", txtDistrict2.Text);
                        htParam.Add("@JURI_STATE", System.DBNull.Value);
                        htParam.Add("@JURI_COUNTRY", System.DBNull.Value);
                    }
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

                    if (chkSelfCerti.Checked == true)
                    {
                        htParam.Remove("@DOC_SUB");
                        htParam.Add("@DOC_SUB", "01");
                    }
                    else if (chkTrueCopies.Checked == true)
                    {
                        htParam.Remove("@DOC_SUB");
                        htParam.Add("@DOC_SUB", "02");
                    }
                    else if (chkNotary.Checked == true)
                    {
                        htParam.Remove("@DOC_SUB");
                        htParam.Add("@DOC_SUB", "03");
                    }

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
                                if (chkAddCtrl.Checked == true)
                                {
                                    htParam.Add("@AddDelFlagRel ", "01");
                                }
                                else if (chkAddCtrl.Checked == false)
                                {
                                    htParam.Add("@AddDelFlagRel ", "02");
                                }

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
                    txtPassNo.MaxLength = 50;
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
                    txtPassNo.MaxLength = 50;

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

                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Resolution of Board / Managing Committee No.";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 50;
                    txtPassNo.Focus();
                    chkSameAsPOI.Checked = false;
                    ddlProofOfAddress.SelectedIndex = 0;
                    ddlProofOfAddress.Enabled = true;
                    // txtPassNo.Attributes.Remove("onblur");
                    //txtPassNo.Attributes.Add("onblur", "return fnValidatePAN(this)");
                }
                else if (ddlCertifiecopy.SelectedIndex == 4)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Memorandum and Article of Association No.";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 50;
                    chkSameAsPOI.Checked = false;
                    ddlProofOfAddress.SelectedIndex = 0;
                    ddlProofOfAddress.Enabled = true;
                    //txtPassNo.Focus();
                    //txtPassNo.Attributes.Remove("onblur");
                }
                else if (ddlCertifiecopy.SelectedIndex == 5)
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Official Valid Documents No.";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                    txtPassNo.MaxLength = 50;
                    // txtPassNo.Focus();
                    txtPassNo.Text = "";
                    chkSameAsPOI.Checked = false;
                    ddlProofOfAddress.SelectedIndex = 0;
                    ddlProofOfAddress.Enabled = true;
                    //txtPassNo.Attributes.Remove("onblur");
                    //txtPassNo.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                }
                else
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "";
                    txtPassNo.Visible = true;
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

        protected void ddlNumberOfPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(ddlNumberOfPerson.SelectedIndex);
            if (a == 0 || a == 00)
            {
                chkAddCtrl.Enabled = false;
            }
            if (a > 0)
            {
                chkAddCtrl.Enabled = true;
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
                    //EnableLinkButton(btnSave);
                    //EnableLinkButton(btnPartialSave);
                    //btnSave.Enabled = true;
                    //btnPartialSave.Enabled = true;
                }
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

                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                htParam.Clear();
                htParam.Add("@PSTempRefNo", Request.QueryString["refno"].ToString());
                dt = objDAL.GetDataTable("getLegPartialSearchData", htParam);

                cbNew.Visible = true;
                cbNew.Checked = true;

                if ((dt.Rows[0]["AccountHolderTypeFlag"]).ToString() == "1")
                {
                    chkUSReport.Checked = true;
                    //ddlAccHolderType.SelectedValue = Convert.ToString(dt.Rows[0]["AccountHolderType"]);
                }

                if ((dt.Rows[0]["AccountHolderTypeFlag"]).ToString() == "2")
                {
                    chkOtherReport.Checked = true;
                }

                #region

                if (chkUSReport.Checked == true)
                {
                    if (chkOtherReport.Checked == true)
                    {
                        chkOtherReport.Checked = false;
                    }

                    if (chkUSReport.Checked == true)
                    {
                        htParam.Clear();
                        htParam.Add("@LookupCode", "KEntAccHolTypUsRept");
                        FillDropdowns("prc_getDDLLookUpData", htParam, ddlAccHolderType, "CKYCConnectionString", true);
                    }
                    else
                    {
                        ddlAccHolderType.Items.Clear();
                        ddlAccHolderType.Items.Insert(0, new ListItem("Select", ""));
                    }
                }
                if (chkOtherReport.Checked == true)
                {
                    if (chkUSReport.Checked == true)
                    {
                        chkUSReport.Checked = false;
                    }

                    if (chkOtherReport.Checked == true)
                    {
                        htParam.Clear();
                        htParam.Add("@LookupCode", "KEntAccHolTypOthRept");
                        FillDropdowns("prc_getDDLLookUpData", htParam, ddlAccHolderType, "CKYCConnectionString", true);
                    }
                    else
                    {
                        ddlAccHolderType.Items.Clear();
                        ddlAccHolderType.Items.Insert(0, new ListItem("Select", ""));
                    }
                }
                #endregion

                #region

                objDAL = new DataAccessLayer("CKYCConnectionString");
                htParam.Clear();
                dt.Clear();
                htParam.Add("@PSTempRefNo", Request.QueryString["refno"].ToString());
                dt = objDAL.GetDataTable("getLegPartialSearchData", htParam);

                ddlAccHolderType.SelectedValue = dt.Rows[0]["AccountHolderType"].ToString();
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
                ddlCountryOfRsidens.SelectedValue = Convert.ToString(dt.Rows[0]["CountryofresidenceasperTaxLaws"]);
                txtTypeIdentiNo.Text = Convert.ToString(dt.Rows[0]["TIN"]);
                ddlTINCountry.Text = Convert.ToString(dt.Rows[0]["TINIssuingCountry"]);
                ddlIdentyType.SelectedValue = Convert.ToString(dt.Rows[0]["AccIdType"]);
                ddlNumberOfPerson.SelectedValue = Convert.ToString(dt.Rows[0]["NoofCtrlPrsn"]);
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
                    txtPassNo.MaxLength = 25;
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
                    txtPassNo.MaxLength = 25;
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
                ddlAddressType.SelectedValue = Convert.ToString(dt.Rows[0]["PER_ADDTYPE"]);
                ddlProofOfAddress.SelectedValue = Convert.ToString(dt.Rows[0]["PER_ADDPROOF"]);
                txtAddressLine1.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE1"]);
                txtAddressLine2.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE2"]);
                txtAddressLine3.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE3"]);
                txtCity.Text = Convert.ToString(dt.Rows[0]["PER_CITY"]);
                //ddlState.SelectedValue = Convert.ToString(dt.Rows[0]["PER_STATECODE"]);

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
                ddlAddressType1.SelectedValue = Convert.ToString(dt.Rows[0]["CUR_ADDTYPE"]);
                ddlProofOfAddress1.SelectedValue = Convert.ToString(dt.Rows[0]["CUR_ADDPROOF"]);
                txtLocAddLine1.Text = Convert.ToString(dt.Rows[0]["CUR_ADDLINE1"]);
                txtLocAddLine2.Text = Convert.ToString(dt.Rows[0]["CUR_ADDLINE2"]);
                txtLocAddLine3.Text = Convert.ToString(dt.Rows[0]["CUR_ADDLINE3"]);
                txtCity1.Text = Convert.ToString(dt.Rows[0]["CUR_CITY"]);
                //ddlState1.SelectedValue = Convert.ToString(dt.Rows[0]["CUR_STATECODE"]);
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

                ddlPinCode1.Text = Convert.ToString(dt.Rows[0]["CUR_PIN"]);
                txtDistrict1.Text = Convert.ToString(dt.Rows[0]["CUR_DISTRICT"]);
                ddlCountryCode1.SelectedValue = Convert.ToString(dt.Rows[0]["CUR_COUNTRY_CODE"]);

                if (Convert.ToString(dt.Rows[0]["CnctType3"]) == "J1" && (dt.Rows[0]["FRN_COUNTRY_CODE"]).ToString() != "")
                {
                    chkAddResident.Checked = true;
                }
                else
                {
                    chkAddResident.Checked = false;
                }
                ddlAddressType2.SelectedValue = Convert.ToString(dt.Rows[0]["FRN_ADDTYPE"]);
                // ddlProofOfAddress2.SelectedValue = Convert.ToString(dt.Rows[0]["PER_ADDPROOF"]);
                txtAddLine1.Text = Convert.ToString(dt.Rows[0]["FRN_ADDLINE1"]);
                txtAddLine2.Text = Convert.ToString(dt.Rows[0]["FRN_ADDLINE2"]);
                txtAddLine3.Text = Convert.ToString(dt.Rows[0]["FRN_ADDLINE3"]);
                txtCity2.Text = Convert.ToString(dt.Rows[0]["FRN_CITY"]);
                //ddlState2.SelectedValue = Convert.ToString(dt.Rows[0]["FRN_STATECODE"]);
                if (Convert.ToString(dt.Rows[0]["FRN_COUNTRY_CODE"]) == "IN")
                {
                    dvState2.Visible = true;
                    txtState2.Visible = false;
                    ddlState2.SelectedValue = Convert.ToString(dt.Rows[0]["FRN_STATECODE"]);
                }
                else
                {
                    dvState2.Visible = false;
                    txtState2.Visible = true;
                    txtState2.Text = Convert.ToString(dt.Rows[0]["FRN_STATECODE"]);
                }

                ddlPinCode2.Text = Convert.ToString(dt.Rows[0]["FRN_PIN"]);
                txtDistrict2.Text = Convert.ToString(dt.Rows[0]["FRN_DISTRICT"]);
                ddlIsoCountryCode.SelectedValue = Convert.ToString(dt.Rows[0]["FRN_COUNTRY_CODE"]);

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

                if (Convert.ToString(dt.Rows[0]["SameasLocalAddressFlagJ1"]) == "01")
                {
                    chkCurrentAdd.Checked = true;
                }
                else
                {
                    chkCurrentAdd.Checked = false;
                }

                if (Convert.ToString(dt.Rows[0]["SameasLocalAddressFlagJ2"]) == "01")
                {
                    chkCorresAdd.Checked = true;
                }
                else
                {
                    chkCorresAdd.Checked = false;
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

                if (Convert.ToString(dt.Rows[0]["kycCertDoc"]) == "01")
                {
                    chkSelfCerti.Checked = true;
                }
                else
                {
                    chkSelfCerti.Checked = false;
                }

                if (Convert.ToString(dt.Rows[0]["kycCertDoc"]) == "02")
                {
                    chkTrueCopies.Checked = true;
                }
                else
                {
                    chkTrueCopies.Checked = false;
                }

                if (Convert.ToString(dt.Rows[0]["kycCertDoc"]) == "03")
                {
                    chkNotary.Checked = true;
                }
                else
                {
                    chkNotary.Checked = false;
                }

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
                objDAL = new DataAccessLayer("CKYCConnectionString");
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
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = new DataTable();
                htParam.Clear();
                htParam.Add("@PSTempRefNo", Request.QueryString["refno"].ToString());
                htParam.Add("@ActionFlag", Request.QueryString["Status"].ToString());
                htParam.Add("@UserType", "");
                htParam.Add("@PSTempRelRefNo", "");

                dt = objDAL.GetDataTable("Prc_GetCtrlPrsnPartialDataForCKYC", htParam);

                if (dt.Rows.Count > 0)
                {
                    gvCtrlPrson.DataSource = dt;
                    gvCtrlPrson.DataBind();
                    Session["dsCtrl"] = dt;
                    ViewState["DTCtrl"] = dt;
                    gvCtrlPrson.Visible = true;
                    div8.Visible = false;   //Added By Akash
                    chkAddCtrl.Checked = true;
                    chkAddCtrl.Enabled = false;

                    if (Request.QueryString["Status"].ToString() == "LMod")
                    {
                        gvCtrlPrson.Columns[1].HeaderText = "Controlling Temporary Reference No";
                    }
                    else if (Request.QueryString["Status"].ToString() == "PMod")
                    {
                        gvCtrlPrson.Columns[1].HeaderText = "Controlling Reference No";
                    }
                }
                else
                {
                    Label2.Visible = true;
                    Label2.ForeColor = System.Drawing.Color.Red;
                    div8.Visible = false;   //Added By Akash
                    chkAddCtrl.Checked = false;
                    chkAddCtrl.Enabled = true;
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

        #region METHOD "disablecntrl"
        protected void disablecntrl()
        {
            try
            {
                txtRefNumber.Enabled = false;
                chkUSReport.Enabled = false;
                chkOtherReport.Enabled = false;
                ddlAccHolderType.Enabled = false;
                txtKYCName.Enabled = false;
                txtPlaceOfInc.Enabled = false;
                ddlCountrOfInc.Enabled = false;
                ddlCountryOfRsidens.Enabled = false;
                txtPanNo.Enabled = false;
                ddlIdentyType.Enabled = false;
                txtTypeIdentiNo.Enabled = false;
                ddlTINCountry.Enabled = false;
                ddlNumberOfPerson.Enabled = false;
                ddlCertifiecopy.Enabled = false;
                txtPassNo.Enabled = false;
                chkPerAddress.Enabled = false;
                chkSameAsPOI.Enabled = false;
                ddlAddressType.Enabled = false;
                ddlProofOfAddress.Enabled = false;
                txtAddressLine1.Enabled = false;
                txtAddressLine2.Enabled = false;
                txtAddressLine3.Enabled = false;
                txtCity.Enabled = false;
                ddlState.Enabled = false;
                txtState.Enabled = false;
                ddlCountryCode.Enabled = false;
                chkLocalAddress.Enabled = false;
                chkCuurentAddress.Enabled = false;
                ddlAddressType1.Enabled = false;
                ddlProofOfAddress1.Enabled = false;
                txtLocAddLine1.Enabled = false;
                txtLocAddLine2.Enabled = false;
                txtLocAddLine3.Enabled = false;
                txtCity1.Enabled = false;
                ddlState1.Enabled = false;
                txtState1.Enabled = false;
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
                ddlIsoCountryCode.Enabled = false;

                txtTelOff.Enabled = false;
                txtTelOff2.Enabled = false;
                txtTelRes.Enabled = false;
                txtTelRes2.Enabled = false;
                txtMobile.Enabled = false;
                txtMobile2.Enabled = false;
                txtFax1.Enabled = false;
                txtFax2.Enabled = false;
                txtemail.Enabled = false;
                txtRemarks.Enabled = false;
                txtPlace.Enabled = false;
                chkSelfCerti.Enabled = false;
                chkTrueCopies.Enabled = false;
                chkNotary.Enabled = false;
                txtEmpName.Enabled = false;
                txtEmpCode.Enabled = false;
                txtEmpDesignation.Enabled = false;
                txtEmpBranch.Enabled = false;
                txtInsName.Enabled = false;
                txtInsCode.Enabled = false;

                chkAddRel.Enabled = false;
                chkAddCtrl.Enabled = false;
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
                htParam.Add("@RegNo", Request.QueryString["refno"].ToString());
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
                htParam.Add("@RegNo", Request.QueryString["refno"].ToString());
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
                objht.Clear();
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

        #endregion

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
    }
}