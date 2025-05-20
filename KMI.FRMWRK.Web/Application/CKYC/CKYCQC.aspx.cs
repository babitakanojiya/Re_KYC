using KMI.FRMWRK.DAL;
using KMI.FRMWRK.Multilingual;
using KMI.FRMWRK.Web.Application.CKYC.Common;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class CKYCQC : System.Web.UI.Page
    {
        #region Declare Veriables
        DataSet objds;
        string strPath;
        int BMaxImgSize1;

        Hashtable hTable = new Hashtable();
        string strPhotoExt = string.Empty;
        private string strDestPath = string.Empty;
        string strDocName = string.Empty;
        DataAccessLayer dataAccessLayer;
        private string strFileName1 = string.Empty;
        private string strFileName = string.Empty;
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
        public string FlagPageTyp;
        string refno;
        int AppId;
        #endregion

        #region PAGELOADEVENTS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                hdnRegRefNo.Value = Request.QueryString["RefNo"].ToString().Trim();//10000388
                if (Request.QueryString["PageFlag"].ToString() == "01")
                {
                    FlagPageTyp = "Indiviual";
                }
                else
                {
                    FlagPageTyp = "Legal";
                    Label12Imp.Visible = true;
                    Label12.Visible = true;
                    Label13Imp.Visible = true;
                    Label13.Visible = true;
                }
                // FlagPageTyp = Request.QueryString["PageFlag"].ToString();
                objDAL = new DataAccessLayer("CKYCConnectionString");
                if (!IsPostBack)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowProgressBarWithOutTimer('Loading... Please wait...');", true);
                    olng = new MultilingualManager("DefaultConn", "CKYCQC.aspx", Session["UserLangNum"].ToString());
                    strUserLang = HttpContext.Current.Session["UserLangNum"].ToString();
                    Session["CarrierCode"] = '2';
                    if (Session["UserId"].ToString() != null)
                    {
                        strUserId = Session["UserId"].ToString();
                    }
                    if (HttpContext.Current.Session["AppId"] != null)
                    {
                        AppId = Convert.ToInt32(HttpContext.Current.Session["AppId"]);
                    }
                    DisableAllControlonLoad();
                    InitializeControls();
                    BindAttestation();
                    FillDocumentReceived();
                    if (Request.QueryString["Status"].ToString() == "QC")
                    {
                        //chkCertifyCopy.Checked = true;
                        //divAddProof.Visible = false;
                        //PopulateAddressProofType();

                        //PopulateMaritalStatus();
                        subPopulateAccountType();
                        //Added by tushar for Account type
                        //PopulateRelatedPerson();
                        subPopulateGender();
                        subPopulateTitle();

                        PopulateProofIdentiy();
                        PopulatePinCode();
                        Fillcountrycd1();
                        FillDocumentReceived();
                        FillRequiredDataForCKYC();
                        if (ddlCountryCode.SelectedValue == "IN")
                        {
                            FillDistrictState(ddlPinCode, ddlDistrict, ddlState);
                        }
                        if (ddlCountryCode1.SelectedValue == "IN")
                        {
                            FillDistrictState(ddlPinCode1, ddlDistrict1, ddlState1);
                        }

                        // BindAttestation();

                        BindGrid();
                        BindGrid_RelatedData();
                        lblPassportNo.Text = "Document Number";
                        //if (ddlProofIdentity.SelectedIndex == 0)
                        //{
                        //    divIdProof.Visible = false;
                        //}
                        //else if (ddlProofIdentity.SelectedIndex == 1)
                        //{
                        //    divIdProof.Visible = true;
                        //    //lblPassportNo.Text = "Passport Number";
                        //    //llPassExpDate.Text = "Passport Expiry Date";
                        //    llPassExpDate.Visible = false;
                        //    txtPassExpDate.Visible = false;
                        //    divPass.Visible = true;
                        //    txtPassOthr.Visible = false;
                        //    txtPassNo.Visible = true;
                        //    txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        //    txtPassExpDate.Text = ViewState["strIdExpDate"].ToString();
                        //    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        //    txtPassNo.MaxLength = 15;
                        //    txtPassNo.Attributes.Remove("onblur");
                        //    txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");
                        //}
                        //else if (ddlProofIdentity.SelectedIndex == 2)
                        //{
                        //    divIdProof.Visible = true;
                        //    //lblPassportNo.Text = "Voter ID Card";
                        //    llPassExpDate.Visible = false;
                        //    txtPassExpDate.Visible = false;
                        //    txtPassOthr.Visible = false;
                        //    divPass.Visible = false;
                        //    txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        //    txtPassNo.Visible = true;
                        //    // FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        //    txtPassNo.MaxLength = 30;
                        //    txtPassNo.Attributes.Remove("onblur");
                        //}
                        //else if (ddlProofIdentity.SelectedIndex == 3)
                        //{
                        //    divIdProof.Visible = true;
                        //    //lblPassportNo.Text = "PAN Card";
                        //    llPassExpDate.Visible = false;
                        //    txtPassExpDate.Visible = false;
                        //    txtPassOthr.Visible = false;
                        //    divPass.Visible = false;
                        //    txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        //    txtPassNo.Visible = true;
                        //    //  FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //    //  FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        //    txtPassNo.MaxLength = 10;
                        //    txtPassNo.Attributes.Remove("onblur");
                        //    txtPassNo.Attributes.Add("onblur", "return fnValidatePAN(this)");
                        //}
                        //else if (ddlProofIdentity.SelectedIndex == 4)
                        //{
                        //    divIdProof.Visible = true;
                        //    //lblPassportNo.Text = "Driving Licence";
                        //    //llPassExpDate.Text = "Driving Licence Expiry Date";
                        //    llPassExpDate.Visible = false;
                        //    txtPassExpDate.Visible = false;
                        //    txtPassOthr.Visible = false;
                        //    divPass.Visible = true;
                        //    txtPassNo.Visible = true;
                        //    txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        //    txtPassExpDate.Text = ViewState["strIdExpDate"].ToString();
                        //    // FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        //    txtPassNo.MaxLength = 15;
                        //    txtPassNo.Attributes.Remove("onblur");
                        //}
                        //else if (ddlProofIdentity.SelectedIndex == 5)
                        //{
                        //    divIdProof.Visible = true;
                        //    //lblPassportNo.Text = "UID(Aadhaar)";
                        //    llPassExpDate.Visible = false;
                        //    txtPassExpDate.Visible = false;
                        //    txtPassOthr.Visible = false;
                        //    divPass.Visible = false;
                        //    txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        //    txtPassNo.Visible = true;
                        //    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                        //    txtPassNo.MaxLength = 12;
                        //    txtPassNo.Attributes.Remove("onblur");
                        //    txtPassNo.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                        //}
                        //else if (ddlProofIdentity.SelectedIndex == 6)
                        //{
                        //    divIdProof.Visible = true;
                        //    //lblPassportNo.Text = "NREGA Job Card";
                        //    llPassExpDate.Visible = false;
                        //    txtPassOthr.Visible = false;
                        //    divPass.Visible = false;
                        //    txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        //    txtPassNo.Visible = true;
                        //    // FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //    // FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        //    txtPassNo.MaxLength = 15;
                        //    txtPassNo.Attributes.Remove("onblur");

                        //}
                        //else if (ddlProofIdentity.SelectedIndex == 7)
                        //{
                        //    divIdProof.Visible = true;
                        //    //lblPassportNo.Text = "Others";
                        //    llPassExpDate.Text = "Identification Number";
                        //    txtPassExpDate.Visible = true;
                        //    llPassExpDate.Visible = true;
                        //    divPass.Visible = true;
                        //    txtPassNo.Text = ViewState["strIdName"].ToString();
                        //    txtPassOthr.Text = ViewState["strIdNumber"].ToString();
                        //    txtPassExpDate.Visible = false;
                        //    txtPassNo.Visible = true;
                        //    // FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //    // FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        //    txtPassNo.MaxLength = 15;
                        //    txtPassNo.Attributes.Remove("onblur");
                        //}
                        //else if (ddlProofIdentity.SelectedIndex == 8)
                        //{
                        //    divIdProof.Visible = true;
                        //    //lblPassportNo.Text = "Simplified Measures Account";
                        //    llPassExpDate.Text = "Identification Number";
                        //    txtPassExpDate.Visible = true;
                        //    llPassExpDate.Visible = true;
                        //    divPass.Visible = true;
                        //    txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        //    txtPassOthr.Text = ViewState["strSamDocNum"].ToString();
                        //    txtPassExpDate.Visible = false;
                        //    txtPassNo.Visible = true;
                        //    //  FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //    // FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        //    txtPassNo.MaxLength = 15;
                        //    txtPassNo.Attributes.Remove("onblur");
                        //}
                        //else
                        //{
                        //    divIdProof.Visible = true;
                        //    //lblPassportNo.Text = "Simplified Measures Account";
                        //    llPassExpDate.Text = "Identification Number";
                        //    txtPassExpDate.Visible = true;
                        //    llPassExpDate.Visible = true;
                        //    divPass.Visible = true;
                        //    txtPassNo.Text = ViewState["strIdNumber"].ToString();
                        //    txtPassOthr.Text = ViewState["strIdExpDate"].ToString();
                        //    txtPassExpDate.Visible = false;
                        //    txtPassNo.Visible = true;
                        //    // FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //    // FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        //    txtPassNo.MaxLength = 15;
                        //    txtPassNo.Attributes.Remove("onblur");
                        //}
                        //                        lblPassportNo.Text = "Document Number";
                        //Commented by kalyani hande start
                        //if (ddlProofOfAddress.SelectedIndex == 0)
                        //{
                        //    divAddProof.Visible = false;
                        //}
                        //else if (ddlProofOfAddress.SelectedIndex == 1)
                        //{
                        //    divAddProof.Visible = true;
                        //    lblPassportNoAdd.Text = "Passport Number";
                        //    //llPassExpDateAdd.Text = "Passport Expiry Date";
                        //    llPassExpDateAdd.Visible = false;
                        //    txtPassExpDateAdd.Visible = false;
                        //    divPassAdd.Visible = true;
                        //    txtPassOthrAdd.Visible = false;
                        //    txtPassNoAdd.Visible = true;
                        //    //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //    //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        //    txtPassNo.MaxLength = 15;
                        //    txtPassNo.Attributes.Remove("onblur");
                        //    txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");
                        //}
                        //else if (ddlProofOfAddress.SelectedIndex == 2)
                        //{
                        //    divAddProof.Visible = true;
                        //    lblPassportNoAdd.Text = "Driving Licence";
                        //    //llPassExpDateAdd.Text = "Driving Licence Expiry Date";
                        //    llPassExpDateAdd.Visible = false;
                        //    txtPassExpDateAdd.Visible = false;
                        //    txtPassOthrAdd.Visible = false;
                        //    divPassAdd.Visible = true;
                        //    txtPassNoAdd.Visible = true;
                        //    //  FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
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
                        //    //  FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                        //    txtPassNoAdd.MaxLength = 12;
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
                        //    // FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //    // FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        //    txtPassNo.MaxLength = 15;
                        //    txtPassNoAdd.Attributes.Remove("onblur");
                        //}
                        //else if (ddlProofOfAddress.SelectedIndex == 5)
                        //{
                        //    divAddProof.Visible = true;
                        //    lblPassportNoAdd.Text = "NREGA Job Card";
                        //    llPassExpDateAdd.Visible = false;
                        //    txtPassNoAdd.Visible = true;
                        //    txtPassExpDateAdd.Visible = false;
                        //    txtPassOthrAdd.Visible = false;
                        //    divPassAdd.Visible = false;
                        //    // FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                        //    // FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        //    txtPassNoAdd.MaxLength = 15;
                        //    txtPassNoAdd.Attributes.Remove("onblur");
                        //}
                        //else
                        //{
                        //    divAddProof.Visible = true;
                        //    lblPassportNoAdd.Text = "Document Name";
                        //    llPassExpDateAdd.Text = "Identification Number";
                        //    txtPassExpDateAdd.Visible = false;
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
                        //Commented by kalyani hande end

                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup1", "window.setTimeout('removeLoader()');", true);
                    }
                }

                // ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "HideProgressBar();", true);
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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        int AppID = 0;
        DataTable dt;

        #region Bind document
        private void BindGrid()
        {
            try
            {
                // System.Threading.Thread.Sleep(5000);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "HideProgressBar();", true);

                DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
                #region photo shuffle start added by rachana on 01-07-2013
                objht.Clear();
                objht.Add("@RegNo", Request.QueryString["refno"].ToString());
                objDt = objDAL.GetDataTable("prc_GetDocType", objht);
                //  objds = objDAL.GetDataSet("prc_GetDocType", objht, "CKYCConnectionString");
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
                //objht.Add("@RegNo", Request.QueryString["refno"].ToString());
                //objht.Add("@DOC_NAME", ViewState["DOC_NAME"]);
                //objDt = objDAL.GetDataTable("prc_GetImage", objht);
                ////objDt = objDAL.GetDataSet("prc_GetImage", objht, "CKYCConnectionString");
                //GridImage.DataSource = objDt;
                //GridImage.DataBind();
                //ViewState["Img"] = objDt;
                //ViewState["Img1"] = objDt;
                //if (ViewState["DOC_NAME"] != null)
                //{
                //    lblpanelheader.Text = ViewState["DOC_NAME"].ToString();
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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
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
                //dvProgressBar.Attributes.Add("style", "display:none");

                int intcode = Convert.ToInt32(ViewState["docCode"]);

                intcode = intcode + 1;
                if (intcode > 2)
                {
                    btnprev.Enabled = true;
                }

                // objds.Clear();
                objht.Clear();
                objht.Add("@RegNo", Request.QueryString["refno"].ToString());
                objDt = objDAL.GetDataTable("prc_GetDocType", objht);
                // objds = objDAL.GetDataSet("prc_GetDocType", objht, "CKYCConnectionString");
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

                Hashtable htParam = new Hashtable();
                htParam.Clear();
                DataSet dsResult1 = new DataSet();
                htParam.Add("@RegNo", Request.QueryString["refno"].ToString());
                htParam.Add("@DOC_NAME", ViewState["DOC_NAME"]);
                dsResult1 = objDAL.GetDataSet("prc_GetImage", htParam);
                if (intcode < objDt.Rows.Count + 1)
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
                lblpanelheader.Text = ViewState["DOC_NAME"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "HideProgressBar()", true);


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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
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
                objDt = objDAL.GetDataTable("prc_GetDocType", objht);
                if (intcode < objDt.Rows.Count + 1)
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
                //if (intcode < objDt.Rows.Count)
                //{
                //    btnnext.Enabled = true;
                //}

                //else
                //{
                //    btnnext.Enabled = false;
                //}
                GridImage.Visible = true;
                GridImage.DataSource = objDt;
                GridImage.DataBind();
                ViewState["Img"] = objDt;
                ViewState["Img1"] = objDt;
                ViewState["docCode"] = intcode;
                lblpanelheader.Text = ViewState["DOC_NAME"].ToString();

                if (intcode == 2)
                {
                    btnprev.Enabled = false;
                }

                else
                {
                    btnprev.Enabled = true;
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "HideProgressBar()", true);
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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
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
                    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }

        #region 'btnUpdate_Click' Event
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //lblmassage.Text = "</br></br>Reference No: XXXXX1425<br/>Entity Name:Tushar Shelke";

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + lblmassage.Text + "');", true);

                objht.Clear();
                objht.Add("@UserId", HttpContext.Current.Session["UserID"].ToString().Trim());
                objht.Add("@RefNo", Request.QueryString["refno"].ToString());
                objht.Add("@flag", "1"); //Flag changed to 3 by Rutuja on 17Mar 2021
                objht.Add("@QCRejectRemark", "");
                objDt = objDAL.GetDataTable("prc_UpdQCStatus", objht);

                if (Session["UserId"].ToString().Equals("checker"))
                {
                    hdnUpdate.Value = "QC approved successfully.";
                }
                else
                {
                    hdnUpdate.Value = "CKYC approved successfully.";
                }


                if (FlagPageTyp == "Indiviual")
                {
                    lblmassage.Text = hdnUpdate.Value + "</br></br>Reference No: " + Request.QueryString["refno"].ToString().Trim() + "<br/>Candidate Name: "
                         + cboTitle.SelectedValue + " " + txtGivenName.Text + " " + txtLastName.Text;

                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "popupAlert();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + lblmassage.Text + "');", true);
                }
                else if (FlagPageTyp == "Legal")
                {
                    //string strmsg;
                    //strmsg = 
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "AlertMsg('" + strmsg + "');", true);

                    lblmassage.Text = hdnUpdate.Value + "</br></br>Reference No: " + Request.QueryString["refno"].ToString().Trim() + "<br/>Entity Name: "
                        + txtKYCName.Text;

                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "popupAlert();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + lblmassage.Text + "');", true);
                }
                //  ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                btnUpdate.Enabled = false;
                btnReject.Enabled = false;
                //}
                //else
                //{
                //    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "alert('" + Res + "')", true);
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + Res + "');", true);
                //    return;
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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }

        }

        #endregion

        #region METHOD 'POPULATE DROPDOWNLIST'

        public void FillDistrictState(DropDownList ddl1, DropDownList ddl2, DropDownList ddl3)
        {
            try
            {
                //  objds.Clear();
                objht.Clear();
                objht.Add("@PinCode", ddl1.SelectedValue.ToString());
                objht.Add("@flag", System.DBNull.Value);
                DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
                //   objds = objDAL.GetDataSet("Prc_GetAddressCKYC", objht, "CKYCConnectionString");
                objDt = objDAL.GetDataTable("Prc_GetAddressCKYC", objht);
                if (objDt.Rows.Count > 0)
                {
                    ddl2.DataSource = objDt;
                    ddl2.DataTextField = "District";
                    ddl2.DataValueField = "District";
                    ddl2.DataBind();
                    ddl3.DataSource = objDt;
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
                    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
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
                oCommonUtility.GetCKYC(cboTitle3, "KTitle");
                cboTitle3.Items.Insert(0, new ListItem("Select", ""));
                //oCommonUtility.GetCKYC(ddlPrefix, "KTitle");
                //ddlPrefix.Items.Insert(0, new ListItem("Select", ""));

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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
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
                    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }

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
                    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        //Added by tushar for Account type

        //private void PopulateMaritalStatus()
        //{
        //    try
        //    {
        //        oCommonUtility.GetCKYC(ddlMaritalStatus, "KMstatus");
        //        ddlMaritalStatus.Items.Insert(0, new ListItem("Select", ""));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
        //        {
        //            Response.Redirect("~/ErrorSession.aspx");
        //        }
        //        else
        //        {
        //            string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
        //            System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
        //            objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
        //            throw ex;
        //        }
        //    }
        //}
        private void PopulateProofIdentiy()
        {
            try
            {
                oCommonUtility.GetCKYC(ddlProofIdentity, "KEntPoI");
                ddlProofIdentity.Items.Insert(0, new ListItem("Select", ""));
                ddlProofIdentity.SelectedIndex = 1;
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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }


        //private void PopulateAddressProofType()
        //{
        //    try
        //    {
        //        oCommonUtility.GetCKYC(ddlProofOfAddress, "KAddrPrf");
        //        ddlProofOfAddress.Items.Insert(0, new ListItem("Select", ""));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
        //        {
        //            Response.Redirect("~/ErrorSession.aspx");
        //        }
        //        else
        //        {
        //            string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
        //            System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
        //            objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
        //            throw ex;
        //        }
        //    }
        //}

        //private void PopulateRelatedPerson()
        //{
        //    try
        //    {
        //        //oCommonUtility.GetCKYC(ddlRelType, "KRelative");
        //        //ddlRelType.Items.Insert(0, new ListItem("Select", ""));
        //        Hashtable htParam = new Hashtable();
        //        htParam.Clear();
        //        htParam.Add("@LookupCode", "KEntRelative");
        //        FillDropdowns("prc_getDDLLookUpData", htParam, ddlRelType, "CKYCConnectionString", true);
        //        DataTable dtRel = new DataTable();
        //        Hashtable hTable = new Hashtable();
        //        DataAccessLayer dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
        //        dtRel.Clear();
        //        hTable.Clear();
        //        hTable.Add("@RegRefNo", hdnRegRefNo.Value);
        //        dtRel = dataAccessLayer.GetDataTable("Prc_getRelPersonDataForQc", hTable);

        //        if (dtRel.Rows.Count > 0)
        //        {
        //            divRelPersonDtls.Visible = true;
        //            ddlRelType.SelectedValue = dtRel.Rows[0]["RelType"].ToString();
        //            ddlRelType.Enabled = false;
        //            txtKYCNum.Text = dtRel.Rows[0]["RelCkycNo"].ToString();
        //            txtKYCNum.Enabled = false;
        //            ddlPrefix.Text = dtRel.Rows[0]["RelPreFix"].ToString();
        //            ddlPrefix.Enabled = false;
        //            txtFirstName.Text = dtRel.Rows[0]["RelFname"].ToString();
        //            txtFirstName.Enabled = false;
        //            txtMidddleName.Text = dtRel.Rows[0]["RelMName"].ToString();
        //            txtMidddleName.Enabled = false;
        //            txtSurName.Text = dtRel.Rows[0]["RelLname"].ToString();
        //            txtSurName.Enabled = false;
        //            ddlProofRelPerson.Text = dtRel.Rows[0]["IdType"].ToString();
        //            ddlProofRelPerson.Enabled = false;
        //            txtPassNo2.Text = dtRel.Rows[0]["IDNO"].ToString();
        //            txtPassNo2.Enabled = false;


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
        //            string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
        //            System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
        //            objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
        //            throw ex;
        //        }
        //    }
        //}

        private void PopulatePinCode()
        {
            try
            {


                objht.Clear();

                objht.Add("@PinCode", System.DBNull.Value);
                objht.Add("@flag", "P");
                //objds = objDAL.GetDataSet("Prc_GetAddressCKYC", objht, "CKYCConnectionString");
                DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
                objDt = objDAL.GetDataTable("Prc_GetAddressCKYC", objht);
                if (objDt.Rows.Count > 0)
                {
                    ddlPinCode.DataSource = objDt;
                    ddlPinCode1.DataSource = objDt;
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
                    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }


        #endregion

        #region Fill Sub Occupation Type Details
        public void FillSubOccuType(DropDownList ddl1, DropDownList ddl2)
        {
            try
            {
                //objds.Clear();
                objht.Clear();
                DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
                objht.Add("@OccFlag", ddl1.SelectedValue);
                //objds = objDAL.GetDataSet("Prc_GetSubOccType", objht, "CKYCConnectionString");
                objDt = objDAL.GetDataTable("Prc_GetSubOccType", objht);
                if (objDt.Rows.Count > 0)
                {
                    ddl2.DataSource = objDt;
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
                    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region Fill country code1
        public void Fillcountrycd1(DropDownList ddlIsoCountryCode)
        {
            try
            {
                // objds.Clear();
                objht.Clear();
                // objds = objDAL.GetDataSet("Prc_GetcountrycodeCKYC", objht, "CKYCConnectionString");
                objDt = objDAL.GetDataTable("Prc_GetcountrycodeCKYC", objht);
                if (objDt.Rows.Count > 0)
                {
                    ddlIsoCountryCode.DataSource = objds;
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
                    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region DROPDOWN 'ddlAddressType' SELECTEDINDEXCHANGED EVENT
        //protected void ddlAddressType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ddlAddressType.SelectedIndex != 0)
        //        {
        //            if (chkPerAddress.Checked == false)
        //            {
        //                chkPerAddress.Enabled = true;
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please Check CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS');", true);
        //                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Check CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS')", true);
        //                chkPerAddress.Focus();
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            chkPerAddress.Enabled = false;
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
        //            string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
        //            System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
        //            objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
        //            throw ex;
        //        }
        //    }

        //}
        #endregion

        #region rejection
        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                dvProgressBar.Attributes.Add("style", "display:none");
                objht.Clear();
                this.QCRejectMainDiv.Style.Add("display", "block");
                if (TxtQcRejectRemark.Text == "")
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please enter Reason for Rejection')", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please enter Reason for Rejection')", true);
                    TxtQcRejectRemark.Focus();
                    return;

                }
                // objds.Clear();
                objht.Add("@UserId", HttpContext.Current.Session["UserID"].ToString().Trim());
                objht.Add("@RefNo", Request.QueryString["refno"].ToString());
                objht.Add("@flag", "3");
                objht.Add("@QCRejectRemark", TxtQcRejectRemark.Text);
                //objds = objDAL.GetDataSet("prc_UpdQCStatus", objht, "CKYCConnectionString");
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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        protected void rbtFS_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtGivenName2.Enabled = true;
            txtMiddleName2.Enabled = true;
            txtLastName2.Enabled = true;
        }

        #region DROPDOWN 'chkTick' SELECTEDINDEXCHANGED EVENT
        protected void chkTick_Checked(object sender, EventArgs e)
        {
            try
            {
                if (chkTick.Checked == true)
                {
                    //chkAddResident.Checked = true;
                }
                else
                {
                    //chkAddResident.Checked = false;
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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        protected void ddlProofIdentity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtPassNo.Visible = false;
                txtPassNo.Text = string.Empty;
                if (ddlProofIdentity.SelectedIndex == 0)
                {
                    divIdProof.Visible = false;

                }

                else if (ddlProofIdentity.SelectedIndex == 1)
                {
                    divIdProof.Visible = true;
                    //lblPassportNo.Text = "Passport Number";
                    //llPassExpDate.Text = "Passport Expiry Date";
                    divPass.Visible = false;
                    txtPassOthr.Visible = false;
                    txtPassNo.Visible = true;
                    txtPassExpDate.Visible = false;
                    txtPassNo.MaxLength = 15;
                    txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");

                }
                else if (ddlProofIdentity.SelectedIndex == 2)
                {
                    divIdProof.Visible = true;
                    //lblPassportNo.Text = "Voter ID Card";
                    txtPassOthr.Visible = false;
                    divPass.Visible = false;
                    txtPassNo.Visible = true;
                    txtPassNo.Attributes.Remove("onblur");
                    txtPassExpDate.Visible = false;
                }
                else if (ddlProofIdentity.SelectedIndex == 3)
                {

                    divIdProof.Visible = true;
                    //lblPassportNo.Text = "PAN Card";
                    txtPassOthr.Visible = false;
                    divPass.Visible = false;
                    txtPassNo.Visible = true;
                    txtPassNo.MaxLength = 10;
                    txtPassExpDate.Visible = false;
                    txtPassNo.Attributes.Remove("onblur");
                    txtPassNo.Attributes.Add("onblur", "return fnValidatePAN(this)");
                }
                else if (ddlProofIdentity.SelectedIndex == 4)
                {

                    divIdProof.Visible = true;
                    //lblPassportNo.Text = "Driving Licence";
                    //llPassExpDate.Text = "Driving Licence Expiry Date";
                    txtPassOthr.Visible = false;
                    txtPassExpDate.Visible = false;
                    divPass.Visible = true;
                    txtPassNo.Visible = true;
                    txtPassNo.Attributes.Remove("onblur");

                }
                else if (ddlProofIdentity.SelectedIndex == 5)
                {
                    divIdProof.Visible = true;
                    //lblPassportNo.Text = "UID(Aadhaar)";
                    txtPassOthr.Visible = false;
                    divPass.Visible = false;
                    txtPassNo.Visible = true;
                    txtPassExpDate.Visible = false;
                    //  FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                    txtPassNo.MaxLength = 12;
                    txtPassNo.Attributes.Remove("onblur");
                    txtPassNo.Attributes.Add("onblur", "return fnValidateAdhar(this)");


                }
                else if (ddlProofIdentity.SelectedIndex == 6)
                {

                    divIdProof.Visible = true;
                    //lblPassportNo.Text = "NREGA Job Card";
                    txtPassNo.Visible = true;
                    txtPassOthr.Visible = false;
                    divPass.Visible = false;
                    txtPassExpDate.Visible = false;
                    txtPassNo.Attributes.Remove("onblur");
                }
                else if (ddlProofIdentity.SelectedIndex == 7)
                {

                    divIdProof.Visible = true;
                    //lblPassportNo.Text = "Document Name";
                    llPassExpDate.Text = "Identification Number";
                    llPassExpDate.Visible = true;
                    divPass.Visible = true;
                    txtPassExpDate.Visible = false;
                    txtPassOthr.Visible = true;
                    txtPassNo.Visible = true;
                    txtPassNo.Attributes.Remove("onblur");

                }
                else if (ddlProofIdentity.SelectedIndex == 8)
                {

                    divIdProof.Visible = true;
                    //lblPassportNo.Text = "Simplified Measures Account";
                    llPassExpDate.Text = "Identification Number";
                    divPass.Visible = true;
                    txtPassExpDate.Visible = false;
                    txtPassOthr.Visible = true;
                    txtPassNo.Visible = true;
                    txtPassNo.Attributes.Remove("onblur");
                }
                else
                {
                    divIdProof.Visible = true;
                    //lblPassportNo.Text = "Simplified Measures Account";
                    llPassExpDate.Text = "Identification Number";
                    divPass.Visible = true;
                    txtPassExpDate.Visible = false;
                    txtPassOthr.Visible = true;
                    txtPassNo.Visible = true;
                    txtPassNo.Attributes.Remove("onblur");
                }
                lblPassportNo.Text = "Document Number";
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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }


        }

        //#region DROPDOWN 'ddlProofOfAddress' SELECTEDINDEXCHANGED EVENT
        //protected void ddlProofOfAddress_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        txtPassOthrAdd.Visible = false;
        //        txtPassNoAdd.Visible = false;
        //        txtPassNoAdd.Text = string.Empty;
        //        txtPassExpDateAdd.Text = string.Empty;
        //        if (ddlProofOfAddress.SelectedIndex == 0)
        //        {
        //            divAddProof.Visible = false;

        //        }

        //        else if (ddlProofOfAddress.SelectedIndex == 1)
        //        {
        //            divAddProof.Visible = true;
        //            lblPassportNoAdd.Text = "Passport Number";
        //            //llPassExpDateAdd.Text = "Passport Expiry Date";
        //            llPassExpDateAdd.Visible = false;
        //            txtPassExpDateAdd.Visible = false;
        //            divPassAdd.Visible = true;
        //            txtPassOthrAdd.Visible = false;
        //            txtPassNoAdd.Visible = true;



        //            //txtPassNo.Text = ViewState["strPassNo"].ToString(); 
        //            //txtPassExpDate.Text = ViewState["strPassDate"].ToString();

        //            //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
        //            //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //            txtPassNo.MaxLength = 15;
        //            txtPassNo.Attributes.Remove("onblur");
        //            txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");

        //        }
        //        else if (ddlProofOfAddress.SelectedIndex == 2)
        //        {

        //            divAddProof.Visible = true;
        //            lblPassportNoAdd.Text = "Driving Licence";
        //            //llPassExpDateAdd.Text = "Driving Licence Expiry Date";
        //            llPassExpDateAdd.Visible = false;
        //            txtPassExpDateAdd.Visible = false;
        //            txtPassOthrAdd.Visible = false;
        //            divPassAdd.Visible = true;
        //            txtPassNoAdd.Visible = true;


        //            //txtPassNo.Text = ViewState["strDrivLic"].ToString();
        //            //txtPassExpDate.Text = ViewState["strDrivLicDate"].ToString();

        //            //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
        //            //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //            txtPassNoAdd.MaxLength = 15;
        //            txtPassNoAdd.Attributes.Remove("onblur");

        //        }
        //        else if (ddlProofOfAddress.SelectedIndex == 3)
        //        {
        //            divAddProof.Visible = true;
        //            lblPassportNoAdd.Text = "UID(Aadhaar)";
        //            llPassExpDateAdd.Visible = false;
        //            txtPassExpDateAdd.Visible = false;
        //            txtPassOthrAdd.Visible = false;
        //            divPassAdd.Visible = false;
        //            // txtPassNo.Text = ViewState["strUIDNo"].ToString();
        //            txtPassNoAdd.Visible = true;


        //            //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
        //            txtPassNoAdd.MaxLength = 12;
        //            txtPassNoAdd.Text = "";
        //            txtPassNoAdd.Attributes.Remove("onblur");
        //            txtPassNoAdd.Attributes.Add("onblur", "return fnValidateAdhar(this)");


        //        }
        //        else if (ddlProofOfAddress.SelectedIndex == 4)
        //        {
        //            divAddProof.Visible = true;
        //            lblPassportNoAdd.Text = "Voter ID Card";
        //            llPassExpDateAdd.Visible = false;
        //            txtPassExpDateAdd.Visible = false;
        //            txtPassOthrAdd.Visible = false;
        //            divPassAdd.Visible = false;
        //            //txtPassNo.Text = ViewState["strVoterId"].ToString();
        //            txtPassNoAdd.Visible = true;


        //            //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
        //            //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //            txtPassNo.MaxLength = 15;
        //            txtPassNoAdd.Attributes.Remove("onblur");


        //        }


        //        else if (ddlProofOfAddress.SelectedIndex == 5)
        //        {

        //            divAddProof.Visible = true;
        //            lblPassportNoAdd.Text = "NREGA Job Card";
        //            llPassExpDateAdd.Visible = false;
        //            txtPassNoAdd.Visible = true;

        //            txtPassOthrAdd.Visible = false;
        //            divPassAdd.Visible = false;

        //            //txtPassNo.Text = ViewState["strNRGEA"].ToString();

        //            //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
        //            //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //            txtPassNoAdd.MaxLength = 15;
        //            txtPassNoAdd.Attributes.Remove("onblur");
        //        }
        //        else
        //        {

        //            divAddProof.Visible = true;
        //            lblPassportNoAdd.Text = "Document Name";
        //            llPassExpDateAdd.Text = "Identification Number";
        //            txtPassExpDateAdd.Visible = true;
        //            llPassExpDateAdd.Visible = true;
        //            divPassAdd.Visible = true;
        //            llPassExpDateAdd.Visible = true;

        //            //txtPassNo.Text = ViewState["strOthr"].ToString();
        //            //txtPassOthr.Text = ViewState["strOthrNum"].ToString();
        //            txtPassExpDateAdd.Visible = false;
        //            txtPassOthrAdd.Visible = true;
        //            txtPassNoAdd.Visible = true;


        //            //FilteredTextBoxExtender6.FilterType = AjaxControlToolkit.FilterTypes.Custom;
        //            //FilteredTextBoxExtender6.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //            txtPassNoAdd.MaxLength = 15;
        //            txtPassNoAdd.Attributes.Remove("onblur");

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
        //            string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
        //            System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
        //            objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
        //            throw ex;
        //        }
        //    }


        //}
        //#endregion

        public void SaveButn(object sender, EventArgs e)
        {
            try
            {
                objht.Clear();
                //  objds.Clear();
                objht.Add("@RefNo", Request.QueryString["RefNo"].ToString().Trim());
                // httable.Add("@DocType", ViewState["docType"]);
                objht.Add("@Id", ViewState["hdndoccode"].ToString());//hdndoccode.Value.ToString ());
                objht.Add("@DocType", ViewState["DocName"].ToString());
                //ds_image = dataAccessRecruit.GetDataSetForPrcRecruit("prc_GetImagesforQC", httable);
                objds = objDAL.GetDataSet("prc_GetImagesforQC", objht);


                //string strpath = ds_image.Tables[0].Rows[0][2].ToString();
                //string imgname = ds_image.Tables[0].Rows[0][3].ToString();

                //convert into bite
                byte[] bytes = (byte[])objds.Tables[0].Rows[0]["IMAGE"];
                string base64String;
                //Byte[] bytes = (Byte[])dsImageDoc.Tables[0].Rows[0]["IMAGE"];
                if ((hdnDocType.Value == "PDF" ? FileUpload1.HasFile : FileUpload.HasFile) && hdnIsReUpload.Value == "Y")
                {
                    bytes = (hdnDocType.Value == "PDF" ? FileUpload1.FileBytes : FileUpload.FileBytes);
                    //bytes = FileUpload.FileBytes;
                    //  base64String = Convert.ToBase64String(bytes, 0, FileUpload.FileBytes.Length);
                }
                else { hdnDocAction.Value = "Rotated"; }

                base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                ViewState["vpath"] = base64String;
                img4.ImageUrl = "ImageCSharp.aspx?ImageID=" + objds.Tables[0].Rows[0]["ID"].ToString();
                if (!(hdnDocType.Value == "PDF" ? FileUpload1.FileName.EndsWith(".pdf") : FileUpload.FileName.EndsWith(".pdf")))
                {
                    System.Drawing.Image resim = new Bitmap(ToImage(bytes));

                    int degree = Convert.ToInt32((hdnRotateValue.Value == "" ? "0" : hdnRotateValue.Value));
                    int w = Convert.ToInt32((hdnHt.Value == "" ? resim.Height.ToString() : hdnHt.Value));
                    // w=Convert.ToInt32(w*2*0.1);
                    int h = Convert.ToInt32((hdnWt.Value == "" ? resim.Width.ToString() : hdnWt.Value));

                    if (degree != 0)
                    {
                        resim = cevir((Bitmap)resim, degree);
                    }
                    bytes = imageToByteArray(resim, w, h);
                }
                else
                {
                    if (hdnDocType.Value == "PDF" && FileUpload1.HasFile)
                    { FileUpload1.SaveAs("C:/temp/" + FileUpload1.FileName); }
                    else
                    { FileUpload.SaveAs("C:/temp/" + FileUpload.FileName); }
                    //FileUpload.SaveAs("C:/temp/" +FileUpload.FileName);
                    FileInfo file = new FileInfo("C:/temp/" + (hdnDocType.Value == "PDF" ? FileUpload1.FileName : FileUpload.FileName));
                    bytes = System.IO.File.ReadAllBytes("C:/temp/" + (hdnDocType.Value == "PDF" ? FileUpload1.FileName : FileUpload.FileName));
                    if (file.Exists)//check file exsit or not  
                    {
                        file.Delete();
                    }
                }
                objht.Clear();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                // objds.Clear();
                objht.Add("@ID", objds.Tables[0].Rows[0]["ID"].ToString());
                objht.Add("@ImgByte", bytes);
                objht.Add("@Action", hdnDocAction.Value.ToString());
                objht.Add("@UserId", HttpContext.Current.Session["UserID"].ToString().Trim());
                objht.Add("@RefNo", Request.QueryString["RefNo"].ToString().Trim());
                objht.Add("@ImgName", (hdnDocType.Value == "PDF" ? FileUpload1.FileName : FileUpload.FileName));
                //objds = objDAL.GetDataSet("Prc_UpdateImg", objht);
                dataAccessLayer.ExecuteNonQuery("Prc_UpdateImg", objht);

                ImageRendering();
                //ImageRendering(Request.QueryString["CndNo"].ToString());
                // lblpopup.Text = "Image saved successfully for <br/>";
                string strmsg = "Image saved successfully for  " + Convert.ToString(hdnImgId.Value);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + strmsg + "');", true);
                // ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }

        public static Bitmap cevir(Bitmap b, float angle)
        {

            // The original bitmap needs to be drawn onto a new bitmap which will probably be bigger 
            // because the corners of the original will move outside the original rectangle.
            // An easy way (OK slightly 'brute force') is to calculate the new bounding box is to calculate the positions of the 
            // corners after rotation and get the difference between the maximum and minimum x and y coordinates.
            float wOver2 = b.Width / 2.0f;
            float hOver2 = b.Height / 2.0f;
            float radians = -(float)(angle / 180.0 * Math.PI);
            // Get the coordinates of the corners, taking the origin to be the centre of the bitmap.
            PointF[] corners = new PointF[]{
            new PointF(-wOver2, -hOver2),
            new PointF(+wOver2, -hOver2),
            new PointF(+wOver2, +hOver2),
            new PointF(-wOver2, +hOver2)
        };

            for (int i = 0; i < 4; i++)
            {
                PointF p = corners[i];
                PointF newP = new PointF((float)(p.X * Math.Cos(radians) - p.Y * Math.Sin(radians)), (float)(p.X * Math.Sin(radians) + p.Y * Math.Cos(radians)));
                corners[i] = newP;
            }

            // Find the min and max x and y coordinates.
            float minX = corners[0].X;
            float maxX = minX;
            float minY = corners[0].Y;
            float maxY = minY;
            for (int i = 1; i < 4; i++)
            {
                PointF p = corners[i];
                minX = Math.Min(minX, p.X);
                maxX = Math.Max(maxX, p.X);
                minY = Math.Min(minY, p.Y);
                maxY = Math.Max(maxY, p.Y);
            }

            // Get the size of the new bitmap.
            SizeF newSize = new SizeF(maxX - minX, maxY - minY);
            // ...and create it.
            Bitmap returnBitmap = new Bitmap((int)Math.Ceiling(newSize.Width), (int)Math.Ceiling(newSize.Height));
            // Now draw the old bitmap on it.
            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                g.TranslateTransform(newSize.Width / 2.0f, newSize.Height / 2.0f);
                g.RotateTransform(angle);
                g.TranslateTransform(-b.Width / 2.0f, -b.Height / 2.0f);

                g.DrawImage(b, 0, 0);
            }

            return returnBitmap;

            //  return returnBitmap;
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn, int w, int h)
        {
            imageIn = new Bitmap(imageIn, w, h);
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public System.Drawing.Image ToImage(byte[] array)
        {
            System.Drawing.Image returnImage = null;
            MemoryStream ms = new MemoryStream(array);
            returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        public void ImageRendering()
        {
            try
            {
                string headerTable = string.Empty;
                string StrYT = string.Empty;
                string strDocName = string.Empty;
                string strImg = string.Empty;
                objht.Clear();
                objht.Add("@RegRefNo", Request.QueryString["refno"].ToString());
                objds = objDAL.GetDataSet("Prc_GetDocDtls", objht);
                if (objds != null && objds.Tables.Count > 0 && objds.Tables[0].Rows.Count > 0)
                {


                    for (int p = 0; p < objds.Tables[0].Rows.Count; p++)
                    {
                        if (objds.Tables[0].Rows[p]["DOC_NAME"].ToString() == "Photo")
                        {
                            strImg = "../../../image/Camera-icon.png";
                            strDocName = "Photo";
                            StrYT = "youtube";

                            ViewState["hdndoccode"] = objds.Tables[0].Rows[0]["SR_NO"].ToString().Trim();
                        }
                        if (objds.Tables[0].Rows[p]["DOC_NAME"].ToString() == "Signature")
                        {
                            strImg = "../../../image/signature-icon.png";
                            strDocName = "Signature";

                            StrYT = "twitter";
                        }
                        if (objds.Tables[0].Rows[p]["DOC_NAME"].ToString() == "POI")
                        {
                            strImg = "../../../image/ID-3-icon.png";
                            strDocName = "Proof of Identity";

                            StrYT = "facebook";
                        }
                        if (objds.Tables[0].Rows[p]["DOC_NAME"].ToString() == "POA")
                        {
                            strImg = "../../../image/Address-Book-2-icon.png";
                            strDocName = "Proof of Address";

                            StrYT = "download";
                        }
                        if (objds.Tables[0].Rows[p]["DOC_NAME"].ToString() == "KYC Form Page 1")
                        {
                            strImg = "../../../image/Document-icon.png";

                            strDocName = "KYC Form page 1";

                            StrYT = "callus";
                        }
                        if (objds.Tables[0].Rows[p]["DOC_NAME"].ToString() == "KYC Form page 2")
                        {
                            strImg = "../../../image/Document-icon.png";
                            strDocName = "KYC Form page 2";
                            StrYT = "Doc1";
                        }
                        if (objds.Tables[0].Rows[p]["DOC_NAME"].ToString() == "Related Person Identity")
                        {
                            strImg = "../../../image/Address-Book-icon.png";
                            strDocName = "Related Person Identity";
                            StrYT = "Doc2";
                        }

                        byte[] bytes = (byte[])objds.Tables[0].Rows[p]["IMAGE"];//GetData("SELECT Data FROM Images WHERE Id =" + id).Rows[0]["Data"];
                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        ViewState["hdndoccode"] = objds.Tables[0].Rows[0]["SR_NO"].ToString().Trim();
                        hdndoccode.Value = objds.Tables[0].Rows[p]["DOC_NAME"].ToString();
                        // Byte[] bytes = (Byte[])objds.Tables[0].Rows[0]["IMAGE"];
                        //objds.Clear();
                        objht.Clear();
                        objht.Add("@RegRefNo", Request.QueryString["refno"].ToString());

                        //ht.Add("@Id",objds.Tables[0].Rows[0]["SR_NO"].ToString().Trim());
                        objht.Add("@flag", "2");
                        objht.Add("@DocType", objds.Tables[0].Rows[p]["DOC_NAME"].ToString());
                        objds = objDAL.GetDataSet("Prc_GetDocNames", objht);

                        System.Drawing.Image image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(bytes));
                        int height = image.Height;
                        int width = image.Width;
                        int total = height * width;
                        string MstWidth = objds.Tables[0].Rows[0]["ImgWidth"].ToString().Trim();
                        string MstHeight = objds.Tables[0].Rows[0]["ImgHeight"].ToString().Trim();
                        ZinSize.Value = total.ToString();
                        ZoutSize.Value = objds.Tables[0].Rows[0]["MaxImgSize"].ToString().Trim();


                        id = objds.Tables[0].Rows[p]["SR_NO"].ToString().Trim();

                        string Doccode = objds.Tables[0].Rows[p]["DOC_CODE"].ToString().Trim();
                        string Imgsrc = "ImageCSharp.aspx?ImageID=" + id;
                        string Doctype = objds.Tables[0].Rows[p]["DOC_NAME"].ToString().Trim();
                        ViewState["DocName"] = Doctype;

                        //ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "showimage(" + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + ", 1);", true);    



                        //Image1.ImageUrl = "data:image/png;base64," + base64String;
                        //    headerTable +=
                        //  @"<div class='" + StrYT + " float-icons' data-toggle='tooltip' data-placement='left' data-original-title='" + StrYT + "'><img class='reminder'><p class='letter'><a href='data:image/png;base64," + base64String + "'  target='_blank' title='" + strDocName + "'><img src='" + strImg + "'  /></a><i class='fa fa-" + StrYT + "' style='padding-top: 10px;padding-right: 2px;'></i></p></div>";
                        //
                        headerTable +=
                       @"<div class='" + StrYT + " float-icons' data-toggle='tooltip' data-placement='left' data-original-title='" + StrYT + "'><img class='reminder'><p class='letter'><button Id=" + Doccode + " type='button' OnClick='return showimage(" + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + "," + 1 + ");'   class='btn btn-link' data-toggle=tooltip data-placement=bottom title='" + Doctype + "'></button><i class='fa fa-" + StrYT + "' style='padding-top: 10px;padding-right: 2px;'></i></p></div>";

                        //   headerTable +=
                        //@"<div class='" + StrYT + " float-icons' data-toggle='tooltip' data-placement='left' data-original-title='" + StrYT + "'><img class='reminder'><p class='letter'><button Id=" + Doccode + " type='button' OnClick='return showimage(" + id + "," + Doccode + "," + 100 + "," + 100 + "," + 100 + "," + 100 + "," + 100 + "," + 100 + "," + 1 + ");'   class='btn btn-link' data-toggle=tooltip data-placement=bottom title='" + Doctype + "'></button><i class='fa fa-" + StrYT + "' style='padding-top: 10px;padding-right: 2px;'></i></p></div>";

                    }
                    headerTable += @"<div id='floating-button' data-toggle='tooltip' data-placement='left' data-original-title='More' onclick='newmail()'>
                                       <p class='plus' style='border-radius: 70px;padding-top: 11px;font-size: 30px;'><span style='top: -6px;' class='glyphicon glyphicon-folder-close'></span></p>
                                       <img class='edit' style='left:3px;top:4px;height:38px;width:38px;' src='../../../image/Folder-Open.png'>
                                       </div>";
                    DivFloat.Controls.Add(new LiteralControl(headerTable));
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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }

        #region BindAttestation
        public void BindAttestation()
        {
            try
            {
                objht.Clear();
                objht.Add("@USERID", HttpContext.Current.Session["UserID"].ToString().Trim());
                // objds = objDAL.GetDataSet("Prc_GetAttestation", objht, "CKYCConnectionString");
                objDt = objDAL.GetDataTable("Prc_GetAttestation", objht);
                if (objDt.Rows.Count > 0)
                {
                    //ViewState["strUsrrole"] = Convert.ToString(objds.Tables[0].Rows[0]["USER_ROLE"]);
                    ViewState["strUsrrole"] = Convert.ToString(objDt.Rows[0]["USER_ROLE"]);
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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region Fill country code1
        public void Fillcountrycd1()
        {
            try
            {
                //objds.Clear();
                objht.Clear();
                DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
                // objds = objDAL.GetDataSet("Prc_GetcountrycodeCKYC", objht, "CKYCConnectionString");
                objDt = objDAL.GetDataTable("Prc_GetcountrycodeCKYC", objht);
                if (objDt.Rows.Count > 0)
                {
                    ddlIsoCountryCode2.DataSource = objDt;
                    ddlIsoCountryCode2.DataTextField = "Country_Desc";
                    ddlIsoCountryCode2.DataValueField = "Country_CODE";
                    ddlIsoCountryCode2.DataBind();
                    ddlIsoCountryCode2.Items.Insert(0, new ListItem("Select", string.Empty));


                    ddlCountryCode1.DataSource = objDt;
                    ddlCountryCode1.DataTextField = "Country_Desc";
                    ddlCountryCode1.DataValueField = "Country_CODE";
                    ddlCountryCode1.DataBind();
                    ddlCountryCode1.Items.Insert(0, new ListItem("Select", string.Empty));


                    ddlCountryCode.DataSource = objDt;
                    ddlCountryCode.DataTextField = "Country_Desc";
                    ddlCountryCode.DataValueField = "Country_CODE";
                    ddlCountryCode.DataBind();
                    ddlCountryCode.Items.Insert(0, new ListItem("Select", string.Empty));

                    ddlIsoCountry.DataSource = objDt;
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
                    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region METHOD "FillRequiredDataForCndPersonal"
        protected void FillRequiredDataForCKYC()
        {
            try
            {
                objht.Clear();
                cbNew.Checked = true;
                DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
                objht.Add("@RegRefNo", hdnRegRefNo.Value);
                objds = objDAL.GetDataSet("getSearchData_Web", objht);
                chkAppDeclare1.Checked = true;
                chkAppDeclare2.Checked = true;
                //Added by tushar for Account type
                if (Convert.ToString(objds.Tables[0].Rows[0]["AccType"]) == "01" || Convert.ToString(objds.Tables[0].Rows[0]["AccType"]) == "1")
                {
                    ddlAccountType.SelectedIndex = 1;
                    //chkNormal.Checked = true;
                }
                else if (Convert.ToString(objds.Tables[0].Rows[0]["AccType"]) == "02" || Convert.ToString(objds.Tables[0].Rows[0]["AccType"]) == "2")
                {
                    ddlAccountType.SelectedIndex = 2;
                    //chkSimplified.Checked = true;
                }
                else if (Convert.ToString(objds.Tables[0].Rows[0]["AccType"]) == "03" || Convert.ToString(objds.Tables[0].Rows[0]["AccType"]) == "3")
                {
                    ddlAccountType.SelectedIndex = 3;
                    //Chksmall.Checked = true;
                }
                else if (Convert.ToString(objds.Tables[0].Rows[0]["AccType"]) == "04" || Convert.ToString(objds.Tables[0].Rows[0]["AccType"]) == "4")
                {
                    ddlAccountType.SelectedIndex = 4;
                    //Chksmall.Checked = true;
                }
                else if (Convert.ToString(objds.Tables[0].Rows[0]["AccType"]) == "05" || Convert.ToString(objds.Tables[0].Rows[0]["AccType"]) == "5")
                {
                    ddlAccountType.SelectedIndex = 5;
                    //Chksmall.Checked = true;
                }
                //Added by tushar for Account type
                //Added By Shubham
                if (FlagPageTyp == "Legal")
                {

                    loadLegalEntityPageCtrl("Y");
                    oCommonUtility.GetCKYC(ddlProofIdentityLegal, "KEntPoI");
                    ddlProofIdentityLegal.Items.Insert(0, new ListItem("Select", ""));
                    ddlProofIdentityLegal.SelectedIndex = 1;
                    Label10.Text = "ADDRESS";
                    BindGridPOI();
                    GridView2.Visible = false;
                    //lblPassportNo.Visible = false;
                    //txtPassNo.Visible = false;
                }
                else
                {
                    loadLegalEntityPageCtrl("N");
                    Label10.Text = "PROOF OF IDENTITY AND ADDRESS";
                    BindGridPOI();
                    lblProof.Visible = false;
                    ddlProofIdentity.Visible = false;
                    //lblPassportNo.Visible = false;
                    //txtPassNo.Visible = false;
                    if (Convert.ToString(objds.Tables[0].Rows[0]["Form60Legal"]).ToString() == "Y")
                    {
                        chkPanForm.Checked = true;
                        chkPanForm.Enabled = false;
                        //                        txtPANNo.Text = "Applied For";
                        txtPANNo.Enabled = false;
                    }
                }
                txtTypeIdentiNo.Text = objds.Tables[0].Rows[0]["TAX_IDNUMBER"].ToString();
                if (txtTypeIdentiNo.Text != "")
                {
                    dvTINCntry.Attributes.Add("style", "display:block");
                    lblTINCountry.Text = "TIN/GST Issuing Country";
                    ddlTINCountry.SelectedItem.Text = ddlCountrOfInc.SelectedItem.Text.ToString();
                    ddlTINCountry.Enabled = false;
                }
                else
                {
                    dvTINCntry.Attributes.Add("style", "display:none");
                    lblTINCountry.Text = "";
                }
                lnkVrfy.Visible = false;
                ddlProofIdentityLegal.Visible = false;
                txtPOIVal.Visible = false;
                Label4.Visible = false;
                Label9.Visible = false;
                ddlProofIdentityLegal.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["IdType"]);
                txtPOIVal.Text = Convert.ToString(objds.Tables[0].Rows[0]["IdNumber"]).ToString();

                if ((Convert.ToString(objds.Tables[0].Rows[0]["CompType"]).ToString() == "01") || (Convert.ToString(objds.Tables[0].Rows[0]["CompType"]).ToString() == "1"))
                {
                    //ddlNatureOfBuss.DataTextField = "Individual";
                    ddlNatureOfBuss.Items.Insert(0, new ListItem("Individual", ""));
                }
                else
                {
                    ddlNatureOfBuss.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["CompType"]).ToString();
                }
                txtConstitutionTypeothers.Text = Convert.ToString(objds.Tables[0].Rows[0]["ConstitutionTypeothers"]).ToString();
                txtConstitutionTypeothers.Enabled = false;
                txtKYCName.Text = Convert.ToString(objds.Tables[0].Rows[0]["EntityName"]).ToString();
                txtDatOfInc.Text = Convert.ToString(objds.Tables[0].Rows[0]["DateofIncorporation"]).ToString();
                txtDtOfCom.Text = Convert.ToString(objds.Tables[0].Rows[0]["DateofCommencementofbusiness"]).ToString();
                txtPlaceOfInc.Text = Convert.ToString(objds.Tables[0].Rows[0]["PlaceofIncorporation"]).ToString();
                ddlCountrOfInc.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["CountryofIncorporation"]).ToString();
                txtMobile1.Text = Convert.ToString(objds.Tables[0].Rows[0]["MobCode2"]).ToString(); //MobCode2,MobileNo2,EmailID2
                txtMobile3.Text = Convert.ToString(objds.Tables[0].Rows[0]["MobileNo2"]).ToString(); //MobCode2,MobileNo2,EmailID2
                txtemail2.Text = Convert.ToString(objds.Tables[0].Rows[0]["EmailID2"]).ToString(); //MobCode2,MobileNo2,EmailID2
                if (Convert.ToString(objds.Tables[0].Rows[0]["Form60Legal"]).ToString() == "Y")
                {
                    chkPanForm.Checked = true;
                    chkPanFormLegal.Checked = true;
                    txtPanNoLegal.Text = "Applied For";
                    txtPanNoLegal.Enabled = false;
                }
                else
                {
                    chkPanForm.Checked = false;
                    chkPanFormLegal.Checked = false;
                    txtPanNoLegal.Text = objds.Tables[0].Rows[0]["PanLegal"].ToString();
                    txtPanNoLegal.Enabled = false;
                }
                //Ended By Shubham
                txtTypeIdentiNo.Text = objds.Tables[0].Rows[0]["TAX_IDNUMBER"].ToString();
                txtKYCNumber.Text = Convert.ToString(objds.Tables[0].Rows[0]["KYC_NO"]);
                txtRefNumber.Text = Convert.ToString(objds.Tables[0].Rows[0]["RegRefNo"]);

                // txtRefNumber.Text = Convert.ToString(objds.Tables[0].Rows[0]["FIRefNo"]);
                cboTitle.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["Prefix"]);
                txtGivenName.Text = Convert.ToString(objds.Tables[0].Rows[0]["FNAME"]);
                txtMiddleName.Text = Convert.ToString(objds.Tables[0].Rows[0]["MNAME"]);
                txtLastName.Text = Convert.ToString(objds.Tables[0].Rows[0]["LNAME"]);
                cboTitle1.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["MAID_PREFIX"]);
                txtGivenName1.Text = Convert.ToString(objds.Tables[0].Rows[0]["MAID_FNAME"]);
                txtMiddleName1.Text = Convert.ToString(objds.Tables[0].Rows[0]["MAID_MNAME"]);
                txtLastName1.Text = Convert.ToString(objds.Tables[0].Rows[0]["MAID_LNAME"]);

                if (Convert.ToString(objds.Tables[0].Rows[0]["FS_FLAG"]) == "01")
                {
                    rbtFS.SelectedValue = "F";
                }
                else
                {
                    rbtFS.SelectedValue = "S";
                }
                cboTitle2.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["FATHER_PREFIX"]);
                txtGivenName2.Text = Convert.ToString(objds.Tables[0].Rows[0]["FATHER_FNAME"]);
                txtMiddleName2.Text = Convert.ToString(objds.Tables[0].Rows[0]["FATHER_MNAME"]);
                txtLastName2.Text = Convert.ToString(objds.Tables[0].Rows[0]["FATHER_LNAME"]);
                cboTitle3.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["MOTHER_PREFIX"]);
                txtGivenName3.Text = Convert.ToString(objds.Tables[0].Rows[0]["MOTHER_FNAME"]);
                txtMiddleName3.Text = Convert.ToString(objds.Tables[0].Rows[0]["MOTHER_MNAME"]);
                txtLastName3.Text = Convert.ToString(objds.Tables[0].Rows[0]["MOTHER_LNAME"]);
                txtDOB.Text = Convert.ToString(objds.Tables[0].Rows[0]["DOB"]);
                //ddlMaritalStatus.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["MARITAL_STATUS"]);
                cboGender.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["GENDER"]);

                if (Convert.ToString(objds.Tables[0].Rows[0]["JurisdictionFlag"]) == "01")
                {
                    chkTick.Checked = true;
                }
                else
                {
                    chkTick.Checked = false;
                }
                ddlIsoCountryCode2.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["ISO_RFT_COUNTRYCODE"]);

                txtIDResTax.Text = Convert.ToString(objds.Tables[0].Rows[0]["TAX_IDNUMBER"]);
                txtDOBRes.Text = Convert.ToString(objds.Tables[0].Rows[0]["BIRTH_PLACE"]);
                ddlIsoCountry.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["ISO_BIRTHPLACE_CODE"]);

                //ddlProofIdentity.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["IdType"]);
                ViewState["strIdName"] = Convert.ToString(objds.Tables[0].Rows[0]["IdName"]);
                ViewState["strIdNumber"] = Convert.ToString(objds.Tables[0].Rows[0]["IdNumber"]);
                ViewState["strIdExpDate"] = Convert.ToString(objds.Tables[0].Rows[0]["IdExpDate"]);

                if (ddlProofIdentity.SelectedValue == "Z")
                {
                    txtPassOthr.Text = Convert.ToString(objds.Tables[0].Rows[0]["IdNumber"]);
                    txtPassNo.Text = Convert.ToString(objds.Tables[0].Rows[0]["IdName"]);
                }
                else
                {
                    //txtPassNo.Text = Convert.ToString(objds.Tables[0].Rows[0]["IdNumber"]);
                    txtPassExpDate.Text = Convert.ToString(objds.Tables[0].Rows[0]["IdExpDate"]);
                }
                ////addedon16jun
                //if (txtPassNo.Text != "")
                //{
                //    //                    lblPassportNo.Visible = true;
                //    //                  txtPassNo.Visible = true;
                //    divPassNo.Attributes.Add("style", "display:block");
                //    divPassNotxt.Attributes.Add("style", "display:block");
                //}
                //if (Convert.ToString(objds.Tables[0].Rows[0]["CnctType1"]) == "P1")
                //{
                //    chkPerAddress.Checked = true;
                //}
                //else
                //{
                //    chkPerAddress.Checked = false;
                //}
                //ddlProofOfAddress.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["PER_ADDPROOF"]);
                txtAddressLine1.Text = Convert.ToString(objds.Tables[0].Rows[0]["PER_ADDLINE1"]);
                txtAddressLine2.Text = Convert.ToString(objds.Tables[0].Rows[0]["PER_ADDLINE2"]);
                txtAddressLine3.Text = Convert.ToString(objds.Tables[0].Rows[0]["PER_ADDLINE3"]);
                txtCity.Text = Convert.ToString(objds.Tables[0].Rows[0]["PER_CITY"]);
                ddlPinCode.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["PER_PIN"]);
                FillDistrictState(ddlPinCode, ddlDistrict, ddlState);

                ddlState.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["PER_STATECODE"]);
                ddlDistrict.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["PER_DISTRICT"]);
                ddlCountryCode.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["PER_COUNTRY_CODE"]);
                if (Convert.ToString(objds.Tables[0].Rows[0]["PER_COUNTRY_CODE"]) != "IN")
                {
                    ddlState.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["PER_STATECODE"]);
                    ddlDistrict.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["PER_DISTRICT"]);
                    ddlPinCode.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["PER_PIN"]);
                }

                //ddlPinCode.Enabled = (ddlCountryCode.SelectedValue == "IN");
                ViewState["strAddIdName"] = Convert.ToString(objds.Tables[0].Rows[0]["AddIdName"]);
                ViewState["strAddIdNumber"] = Convert.ToString(objds.Tables[0].Rows[0]["AddIdNumber"]);
                ViewState["strAddIdExpDate"] = Convert.ToString(objds.Tables[0].Rows[0]["AddIdExpDate"]);

                //if (ddlProofOfAddress.SelectedValue == "99")
                //{
                //    txtPassOthrAdd.Text = Convert.ToString(objds.Tables[0].Rows[0]["AddIdNumber"]);
                //    txtPassNoAdd.Text = Convert.ToString(objds.Tables[0].Rows[0]["AddIdName"]);
                //}
                //else
                //{
                //    txtPassNoAdd.Text = Convert.ToString(objds.Tables[0].Rows[0]["AddIdNumber"]);
                //    txtPassExpDateAdd.Text = Convert.ToString(objds.Tables[0].Rows[0]["AddIdExpDate"]);
                //}

                //if (txtPassNo.Text == "")
                //{
                //    divAddProof.Visible = false;
                //}
                //else
                //{
                //    divAddProof.Visible = true;
                //}

                if (Convert.ToString(objds.Tables[0].Rows[0]["CnctType2"]) == "M1" && Convert.ToString(objds.Tables[0].Rows[0]["CUR_PIN"]) != "")
                {
                    chkLocalAddress.Checked = true;
                }
                else
                {
                    chkLocalAddress.Checked = false;
                }
                txtLocAddLine1.Text = Convert.ToString(objds.Tables[0].Rows[0]["CUR_ADDLINE1"]);
                txtLocAddLine2.Text = Convert.ToString(objds.Tables[0].Rows[0]["CUR_ADDLINE2"]);
                txtLocAddLine3.Text = Convert.ToString(objds.Tables[0].Rows[0]["CUR_ADDLINE3"]);
                txtCity1.Text = Convert.ToString(objds.Tables[0].Rows[0]["CUR_CITY"]);
                ddlDistrict1.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["CUR_DISTRICT"]);
                ddlPinCode1.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["CUR_PIN"]);

                FillDistrictState(ddlPinCode1, ddlDistrict1, ddlState1);
                ddlState1.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["CUR_STATECODE"]);
                ddlCountryCode1.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["CUR_COUNTRY_CODE"]);
                if (Convert.ToString(objds.Tables[0].Rows[0]["CUR_COUNTRY_CODE"]) != "IN")
                {
                    ddlState1.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["CUR_STATECODE"]);
                    ddlDistrict1.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["CUR_DISTRICT"]);
                    ddlPinCode1.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["CUR_PIN"]);
                }

                //ddlPinCode1.Enabled = (ddlCountryCode1.SelectedValue == "IN");

                if (Convert.ToString(objds.Tables[0].Rows[0]["CurSameAsFlag"]) == "Y" || Convert.ToString(objds.Tables[0].Rows[0]["CurSameAsFlag"]) == "01")
                {
                    chkCuurentAddress.Checked = true;
                }
                //ddlState2.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["FRN_STATECODE"]);
                //ddlDistrict2.SelectedItem.Text = Convert.ToString(objds.Tables[0].Rows[0]["FRN_STATECODE"]);
                txtTelOff.Text = Convert.ToString(objds.Tables[0].Rows[0]["std_officeTele"]);
                txtTelRes.Text = Convert.ToString(objds.Tables[0].Rows[0]["std_resTele"]);
                txtMobile.Text = Convert.ToString(objds.Tables[0].Rows[0]["mobile_countryCode"]);
                txtFax1.Text = Convert.ToString(objds.Tables[0].Rows[0]["std_fax"]);


                txtTelOff2.Text = Convert.ToString(objds.Tables[0].Rows[0]["OFF_TELE"]);
                txtTelRes2.Text = Convert.ToString(objds.Tables[0].Rows[0]["RES_TEL"]);

                txtFax2.Text = Convert.ToString(objds.Tables[0].Rows[0]["FAX"]);
                txtMobile2.Text = Convert.ToString(objds.Tables[0].Rows[0]["MOBILE"]);
                txtemail.Text = Convert.ToString(objds.Tables[0].Rows[0]["EMAILID"]);

                txtRemarks.Text = Convert.ToString(objds.Tables[0].Rows[0]["REMARK"]);
                txtPlace.Text = Convert.ToString(objds.Tables[0].Rows[0]["PLACE"]);
                txtDate.Text = Convert.ToString(objds.Tables[0].Rows[0]["APP_DATE"]);

                txtEmpName.Text = Convert.ToString(objds.Tables[0].Rows[0]["kycEmpName"]);
                txtEmpCode.Text = Convert.ToString(objds.Tables[0].Rows[0]["kycEmpCode"]);
                txtEmpDesignation.Text = Convert.ToString(objds.Tables[0].Rows[0]["kycEmpDesi"]);
                txtEmpBranch.Text = Convert.ToString(objds.Tables[0].Rows[0]["kycEmpBranch"]);
                txtInsName.Text = Convert.ToString(objds.Tables[0].Rows[0]["kycInstName"]);

                txtInsCode.Text = Convert.ToString(objds.Tables[0].Rows[0]["kycInstCode"]);
                txtDateKYCver.Text = Convert.ToString(objds.Tables[0].Rows[0]["kycVerDate"]);
                txtPANNo.Text = Convert.ToString(objds.Tables[0].Rows[0]["PAN_NO"]);
                //ddlDocReceived.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["kycCertDoc"]);
                if (Convert.ToString(objds.Tables[0].Rows[0]["kycCertDoc"]) == "01")
                {
                    ddlDocReceived.SelectedValue = "01";
                }
                else
                {
                    ddlDocReceived.SelectedValue = Convert.ToString(objds.Tables[0].Rows[0]["kycCertDoc"]);
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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region METHOD "InitializeControl()"
        private void InitializeControls()
        {
            try
            {
                lblAppType.Text = olng.GetItemDesc("lblAppType");
                //lblRefNumber.Text = olng.GetItemDesc("lblRefNumber");
                lblAccountType.Text = olng.GetItemDesc("lblAccountType");
                //lblKYCNumber.Text = olng.GetItemDesc("lblKYCNumber");
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
                // lblProofOfIdentity11.Text = olng.GetItemDesc("lblProofOfIdentity11");
                lblProof.Text = "Proof of Address";// olng.GetItemDesc("lblProof");
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

                lblTelOff1.Text = olng.GetItemDesc("lblTelOff1");
                lblTelRes.Text = olng.GetItemDesc("lblTelRes");
                lblMobile.Text = olng.GetItemDesc("lblMobile");
                //lblFax.Text = olng.GetItemDesc("lblFax"); commented by rutuja
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

                //                lblpfPersonal1.Text = olng.GetItemDesc("lblpfPersonal1"); commnented by Rutuja
                lblpfPersonal.Text = olng.GetItemDesc("lblpfPersonal");
                lbltick.Text = olng.GetItemDesc("lbltick");
                // lblProofOfIdentity11.Text = olng.GetItemDesc("lblProofOfIdentity11");
                //lblpfofAddr1.Text = olng.GetItemDesc("lblpfofAddr1");
                //lblpfofAddr2.Text = olng.GetItemDesc("lblpfofAddr2"); commneted by Rutuja
                //  lblDtlOfRtltpr.Text = olng.GetItemDesc("lblDtlOfRtltpr");
                lblRemarks.Text = olng.GetItemDesc("lblRemarks");
                lblattstn.Text = olng.GetItemDesc("lblattstn");
                lbldec.Text = olng.GetItemDesc("lbldec");
                lblAttesOfc.Text = olng.GetItemDesc("lblAttesOfc");
                lblOfcuseOnly.Text = olng.GetItemDesc("lblOfcuseOnly");
                lblInsDtls.Text = olng.GetItemDesc("lblInsDtls");
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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }

        #endregion

        #region DROPDOWN 'chkPerAddress'SELECTEDINDEXCHANGED EVENT
        protected void chkPerAddress_Checked(object sender, EventArgs e)
        {
            try
            {
                //if (chkPerAddress.Checked == true)
                //{
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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region Fill country code
        public void Fillcountrycd()
        {
            try
            {
                // objds.Clear();
                objht.Clear();
                //   objds = objDAL.GetDataSet("Prc_GetcountrycodeCKYC", objht, "CKYCConnectionString");
                objDt = objDAL.GetDataTable("Prc_GetcountrycodeCKYC", objht);
                if (objDt.Rows.Count > 0)
                {
                    ddlIsoCountryCodeOthr.DataSource = objDt;
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
                    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region DROPDOWN 'ddlAddressType' SELECTEDINDEXCHANGED EVENT
        #endregion

        #region DROPDOWN 'ddlPinCode' SELECTEDINDEXCHANGED EVENT
        protected void ddlPinCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string date;
                date = DateTime.Today.ToString("dd-MM-yyyy");
                FillDistrictState(ddlPinCode, ddlDistrict, ddlState);

                //if (chkPerAddress.Checked == false)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please Check CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS');", true);
                //    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please check CURRENT/PERMANENT/OVERSEAS ADDRESS DETAILS')", true);
                //    chkPerAddress.Focus();
                //    ddlPinCode.SelectedIndex = 0;
                //    return;
                //}


                //if (ddlProofOfAddress.SelectedIndex != 0)
                //{
                //    if (ddlProofOfAddress.SelectedIndex == 1)
                //    {
                //        if (txtPassNoAdd.Text == "")
                //        {
                //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please enter passport no')", true);
                //            txtPassNoAdd.Focus();
                //            ddlPinCode.SelectedIndex = 0;
                //            return;
                //        }
                //        if (txtPassExpDateAdd.Text == "")
                //        {
                //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please enter passport expiry date')", true);
                //            txtPassExpDateAdd.Focus();
                //            ddlPinCode.SelectedIndex = 0;
                //            return;
                //        }
                //        if (txtPassExpDateAdd.Text != "")
                //        {

                //            if (Convert.ToDateTime(date) > Convert.ToDateTime(txtPassExpDateAdd.Text))
                //            {
                //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('You cannot select past date as driving license expiry date')", true);
                //                txtPassExpDateAdd.Focus();
                //                ddlPinCode.SelectedIndex = 0;
                //                return;
                //            }
                //        }
                //    }

                //    if (ddlProofOfAddress.SelectedIndex == 2)
                //    {
                //        if (txtPassNoAdd.Text == "")
                //        {
                //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please enter driving licence no')", true);
                //            txtPassNoAdd.Focus();
                //            ddlPinCode.SelectedIndex = 0;
                //            return;
                //        }

                //        if (txtPassExpDateAdd.Text == "")
                //        {
                //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please enter driving licence expiry date')", true);
                //            txtPassExpDateAdd.Focus();
                //            ddlPinCode.SelectedIndex = 0;
                //            return;
                //        }
                //        if (txtPassExpDateAdd.Text != "")
                //        {
                //            if (Convert.ToDateTime(date) > Convert.ToDateTime(txtPassExpDateAdd.Text))
                //            {
                //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('You cannot select past date as driving license expiry date')", true);
                //                txtPassExpDateAdd.Focus();
                //                ddlPinCode.SelectedIndex = 0;
                //                return;
                //            }
                //        }
                //    }

                //    if (ddlProofOfAddress.SelectedIndex == 3)
                //    {
                //        if (txtPassNoAdd.Text == "")
                //        {
                //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please enter UID(Aadhaar)')", true);
                //            txtPassNoAdd.Focus();
                //            ddlPinCode.SelectedIndex = 0;
                //            return;
                //        }
                //    }
                //    if (ddlProofOfAddress.SelectedIndex == 4)
                //    {
                //        if (txtPassNoAdd.Text == "")
                //        {
                //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please enter voter id card')", true);
                //            txtPassNoAdd.Focus();
                //            ddlPinCode.SelectedIndex = 0;
                //            return;
                //        }
                //    }
                //    if (ddlProofOfAddress.SelectedIndex == 5)
                //    {
                //        if (txtPassNoAdd.Text == "")
                //        {
                //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please enter NREGA job card')", true);
                //            txtPassNoAdd.Focus();
                //            ddlPinCode.SelectedIndex = 0;
                //            return;
                //        }
                //    }
                //    if (ddlProofOfAddress.SelectedIndex == 6)
                //    {
                //        if (txtPassNoAdd.Text == "")
                //        {
                //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please enter other no of proof of Address')", true);
                //            txtPassNoAdd.Focus();
                //            ddlPinCode.SelectedIndex = 0;
                //            return;
                //        }
                //    }
                //}
                if (txtAddressLine1.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please enter permanent address line 1')", true);
                    txtAddressLine1.Focus();
                    ddlPinCode.SelectedIndex = 0;
                    return;
                }
                if (txtCity.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please enter permanent city/Town/Village')", true);
                    txtCity.Focus();
                    ddlPinCode.SelectedIndex = 0;
                    return;
                }
                if (ddlPinCode.SelectedIndex == 0 && chkTick.Checked == false)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmsg", "AlertMsg('Please enter permanent Pin/Post Code')", true);
                    ddlPinCode.Focus();
                    ddlPinCode.SelectedIndex = 0;
                    return;
                }
                //chkPerAddress.Enabled = false;
                ddlCountryCode.SelectedValue = "IN";
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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
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
                    ddlDistrict1.SelectedItem.Text = ddlDistrict.Text;
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
                    ddlDistrict1.SelectedItem.Text = "";
                    ddlPinCode1.SelectedIndex = 0;
                    ddlPinCode1.Enabled = true;
                    ddlState1.SelectedItem.Text = "";
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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region DROPDOWN 'ddlPinCode1' SELECTEDINDEXCHANGED EVENT
        protected void ddlPinCode1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
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
                    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        protected void FillDocumentReceived()
        {
            try
            {
                Hashtable htParam = new Hashtable();
                htParam.Clear();
                htParam.Add("@LookupCode", "DocReceived");
                if (FlagPageTyp == "Legal")
                {
                    htParam.Add("@ParamUsage", "LI");
                }
                else
                {
                    htParam.Add("@ParamUsage", "KI");
                }

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

        #region METHOD "DisableAllControlonLoad()"
        private void DisableAllControlonLoad()
        {
            try
            {
                ddlAccountType.Enabled = false;
                cboTitle.Enabled = false;
                txtGivenName.Enabled = false;
                txtDOB.Enabled = false;
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
                cboGender.Enabled = false;
                txtPANNo.Enabled = false;
                ddlIsoCountryCodeOthr.Enabled = false;
                chkTick.Enabled = false;
                ddlIsoCountryCode2.Enabled = false;
                txtIDResTax.Enabled = false;
                ddlIsoCountry.Enabled = false;
                ddlProofIdentity.Enabled = false;
                lnkVrfy.Enabled = false;
                txtPassNo.Enabled = false;
                txtPassOthr.Enabled = false;
                //chkPerAddress.Enabled = false;
                //ddlProofOfAddress.Enabled = false;
                //txtPassNoAdd.Enabled = false;
                //txtPassExpDateAdd.Enabled = false;
                txtPassExpDate.Enabled = false;
                txtAddressLine1.Enabled = false;
                txtAddressLine2.Enabled = false;
                txtAddressLine3.Enabled = false;
                txtCity.Enabled = false;
                ddlDistrict.Enabled = false;
                ddlPinCode.Enabled = false;
                ddlCountryCode.Enabled = false;
                chkLocalAddress.Enabled = false;
                txtLocAddLine2.Enabled = false;
                chkCuurentAddress.Enabled = false;
                txtLocAddLine1.Enabled = false;
                lblLocAddLine3.Enabled = false;
                txtLocAddLine3.Enabled = false;
                txtCity1.Enabled = false;
                ddlDistrict1.Enabled = false;
                ddlPinCode1.Enabled = false;
                ddlState1.Enabled = false;
                ddlCountryCode1.Enabled = false;
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
                txtDate.Enabled = false;
                txtPlace.Enabled = false;
                ddlDocReceived.Enabled = false;
                txtDateKYCver.Enabled = false;
                txtEmpName.Enabled = false;
                txtEmpCode.Enabled = false;
                txtEmpDesignation.Enabled = false;
                txtEmpBranch.Enabled = false;
                txtInsName.Enabled = false;
                txtInsCode.Enabled = false;
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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }

        #endregion


        //Added By Shubham for Merge Legal into Indiviual on Page load- View Data
        Hashtable htParam;
        protected void ddlNatureOfBuss_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNatureOfBuss.SelectedValue == "R")
            {
                txtConstitutionTypeothers.Enabled = true;
            }
            else
            {
                txtConstitutionTypeothers.Enabled = false;
            }
        }
        protected void FillCountryVal()
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                DataTable dt1 = new DataTable();
                htParam = new Hashtable();
                htParam.Clear();
                dt1 = objDAL.GetDataTable("Prc_GetcountrycodeCKYC", htParam);
                if (dt1.Rows.Count > 0)
                {
                    ddlCountrOfInc.DataSource = dt1;
                    ddlCountrOfInc.DataTextField = "Country_Desc";
                    ddlCountrOfInc.DataValueField = "Country_CODE";
                    ddlCountrOfInc.DataBind();
                    ddlCountrOfInc.Items.Insert(0, new ListItem("Select", string.Empty));
                    ddlCountrOfInc.SelectedValue = "IN";
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
                    // objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "FillCountry", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                //objDAL = null;
                //dt = null;
            }
        }
        protected void FillConstType()
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Clear();
                ht.Add("@LookupCode", "KConstTyp");
                ht.Add("@ParamUsage", "KL");
                FillDropdowns("prc_getDDLLookUpData", ht, ddlNatureOfBuss, "CKYCConnectionString", true);

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
                    // objErr.LogErr(AppID, "CKYCRelatedPrsn.aspx.cs", "FillConstType", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                //objDAL = null;
                //dt = null;
            }
        }
        public void loadLegalEntityPageCtrl(string flag)
        {
            if (flag == "Y")
            {
                //************************FOR OFFICE USE ONLY*********************
                //Indiviual CTRL
                lblAccountType.Visible = false;
                lblAccountTypeImp.Visible = false;
                ddlAccountType.Visible = false;
                lblKYCNumber.Visible = true;
                txtKYCName.Visible = true;
                txtKYCName.Enabled = false;
                Label2.Visible = false;
                //ConstitutionType.Visible = false;

                cbNewtxt.Visible = true;
                cbNew.Visible = false;
                cbUpdate.Visible = false;
                //Legal CTRL
                FillConstType();
                lblKYCNumber.Visible = true;
                txtKYCNumber.Visible = true;
                txtKYCNumber.Enabled = false;
                lblNatureOfBuss.Visible = true;
                lblNatureOfBussImp.Visible = true;
                ddlNatureOfBuss.Visible = true;
                ddlNatureOfBuss.Enabled = false;
                Label5.Visible = true;
                Label5Imp.Visible = true;
                txtConstitutionTypeothers.Visible = true;
                txtConstitutionTypeothers.Enabled = false;

                //*****************ENTITY DETAILS and Personal Details****************
                //Indiviual CTRL                
                divPersonal.Visible = false;
                //Legal CTRL
                FillCountryVal();
                lblpfPersonal.Text = "ENTITY DETAILS";
                divDetailOfEntity.Visible = true;
                // *************PROOF OF IDENTITY AND ADDRESS************
                // *************Contact DETAILS************
                //divMob2.Visible = true;
                divMob2.Attributes.Add("style", "display:block");
                lblFax.Visible = true;
                divFax.Visible = true;
                divFax.Attributes.Add("style", "display:block");
                //txtFax1.Visible = true;
                //txtFax2.Visible = true;
                //divEmail2.Visible = true;
                divEmail2.Attributes.Add("style", "display:block");
                // *************KYC VERIFICATION DETAILS************
                //Change for Legal
                lblattstn.Text = "ATTESTATION";
                lblKYCVerify.Text = "KYC VERIFICATION CARRIED OUT BY";
                //divIdVer.Visible = true;
                //POI Section
                divPOISection.Visible = true;
                divhide.Attributes.Add("style", "display:none");
            }
            if (flag == "N")
            {
                //****************FOR OFFICE USE ONLY ************************
                //Indiviual CTRL
                divhide.Attributes.Add("style", "display:block");
                lblAccountType.Visible = true;
                lblAccountTypeImp.Visible = true;
                ddlAccountType.Visible = true;
                cbNewtxt.Visible = true;
                cbNew.Visible = false;
                Label2.Visible = true;
                //ConstitutionType.Visible = true;
                //Legal CTRL
                lblKYCNumber.Visible = true;
                txtKYCNumber.Visible = true;
                txtKYCNumber.Enabled = false;
                cbUpdate.Visible = false;
                lblNatureOfBuss.Visible = true;
                lblNatureOfBussImp.Visible = false;
                ddlNatureOfBuss.Visible = true;
                ddlNatureOfBuss.Enabled = false;
                Label5.Visible = false;
                Label5Imp.Visible = false;
                txtConstitutionTypeothers.Visible = false;
                //****************ENTITY DETAILS and Personal Details***************
                //Indiviual CTRL                
                divPersonal.Visible = true;
                //Legal CTRL
                lblpfPersonal.Text = "PERSONAL DETAILS";
                divDetailOfEntity.Visible = false;

                // *************Contact DETAILS************
                //divMob2.Visible = false;
                divMob2.Attributes.Add("style", "display:none");
                divFax.Visible = false;
                divFax.Attributes.Add("style", "display:none");
                //txtFax1.Visible = false;
                //txtFax2.Visible = false;
                //divEmail2.Visible = false;
                divEmail2.Attributes.Add("style", "display:none");
                lblFax.Visible = false;

                // *************KYC VERIFICATION DETAILS************
                //divIdVer.Visible = false;
                //POI Section
                divPOISection.Visible = false;

            }
        }

        private void BindGridPOI()
        {
            DataTable dt = new DataTable();
            Hashtable ht = new Hashtable();
            CheckBox ck = new CheckBox();
            dt.Columns.Add(new System.Data.DataColumn("DOC ID NAME", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("DOC ID NUMBER", typeof(String)));
            //dt.Columns.Add(new System.Data.DataColumn("POI Document", typeof(String)));

            ht.Clear();
            objDAL = new DataAccessLayer("CKYCConnectionString");
            ht.Add("@RegRefNo", hdnRegRefNo.Value);
            dt = objDAL.GetDataTable("PRC_get_TX_kycPer_QC", ht);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    if (FlagPageTyp == "Legal")
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (dt.Rows[j]["IsPOA"].ToString() == "Y")
                            {
                                txtPassExpDate.Attributes.Add("style", "display:none");
                                ///ddlProofIdentity.Text = dt.Rows[j]["DocType"].ToString();
                                ddlProofIdentity.SelectedValue = dt.Rows[j]["DocID"].ToString();
                                ddlProofIdentity.Enabled = false;
                                if (dt.Rows[j]["DocType"].ToString() == "Other")
                                {
                                    divPassNo.Attributes.Add("style", "display:block;text-align: left;");
                                    divPassNotxt.Attributes.Add("style", "display:block");
                                }
                                txtPassNo.Text = dt.Rows[j]["DocNumber"].ToString();
                                //txtPOIVal.Text =dt.Rows[j]["DocNumber"].ToString();
                                dt.Rows[j].Delete();
                            }
                        }
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        GridView2.Visible = false;
                    }
                    else
                    {
                        txtPassExpDate.Attributes.Add("style", "display:none");
                        GridView1.Visible = false;
                        GridView2.DataSource = dt;
                        GridView2.DataBind();
                        for (int j = 0; j < GridView2.Rows.Count; j++)
                        {
                            CheckBox ChkPOIDocument = (CheckBox)GridView2.Rows[j].FindControl("ChkPOIDocument");
                            if (dt.Rows[j]["IsPOA"].ToString() == "Y")
                            {
                                ChkPOIDocument.Checked = true;
                            }
                        }
                    }
                }
            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            objht.Clear();
            objht.Add("@RegRefNo", hdnRegRefNo.Value);
            objDt = objDAL.GetDataTable("Prc_getRelRefNo", objht);

            string RelRefnNo = objDt.Rows[0]["RelRefno"].ToString();
            string RegRefNo = objDt.Rows[0]["RegRefNo"].ToString();
            string FIRefNo = objDt.Rows[0]["FIRefNo"].ToString();


            if (Request.QueryString["Status"].ToString() == "QC")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenRelatedPersonPageView(" + RelRefnNo + "," + RegRefNo + "," + FIRefNo + ");", true);
            }
            //else if (Request.QueryString["Status"].ToString() == "PMod")
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenPartialRelatedPersonPageView(" + RelRefnNo + "," + refno + ");", true);
            //}
        }

        protected void lnkdelete_Click(object sender, EventArgs e)
        {

        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            int i = Convert.ToInt32(clickedRow.RowIndex);
            DataTable dt = (DataTable)ViewState["DT"];

            objht.Clear();
            objht.Add("@RegRefNo", hdnRegRefNo.Value);
            objDt = objDAL.GetDataTable("Prc_getRelRefNo", objht);

            string RelRefnNo = objDt.Rows[i]["RelRefno"].ToString();
            string RegRefNo = objDt.Rows[i]["RegRefNo"].ToString();
            string FIRefNo = objDt.Rows[i]["FIRefNo"].ToString();

            if (Request.QueryString["Status"].ToString() == "QC")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "OpenRelatedPersonPageView('" + RelRefnNo + "','" + RegRefNo + "','" + FIRefNo + "','" + FlagPageTyp + "');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "OpenRelatedPersonPageView(" + RelRefnNo + "," + RegRefNo + "," + FIRefNo + ");", true);
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenRelatedPersonPageView(" + RelRefnNo + "," + RegRefNo + "," + FIRefNo + ");", true);
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {

        }

        #region Bind document
        private void BindGrid_RelatedData()
        {
            try
            {

                DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
                objht.Clear();
                objht.Add("@RegNo", Request.QueryString["refno"].ToString());
                objDt = objDAL.GetDataTable("Prc_getRelDataForQC", objht);

                if (objDt.Rows.Count > 0)
                {
                    gvMemDtls.DataSource = objDt;
                    gvMemDtls.DataBind();
                    divRelPersonDtls.Visible = true;
                }
                else
                {
                    divRelPersonDtls.Visible = true;
                    lblRelRecordShow.Visible = true;
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
                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
            finally
            {
                //con.Close();
            }

        }
        #endregion

        protected void gvDocDtls_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "View")
                {
                    //string Preview = e.CommandArgument.ToString().Trim();
                    string RefNo = txtRefNumber.Text.ToString();// = strRefNo;

                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                    Label lbldocName = (Label)row.FindControl("lbldocName");
                    Label lbldoccode = (Label)row.FindControl("lbldocCode");
                    ViewState["DocName"] = lbldocName.Text;
                    hdnRegRefNo.Value = RefNo.ToString();
                    string ShowImage = string.Empty;
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
                        hTable.Add("@flag", "2");
                        hTable.Add("@DocType", lbldocName.Text);
                        dt = dataAccessLayer.GetDataTable("Prc_GetDocNames", hTable);
                    }
                    ViewState["hdndoccode"] = dt.Rows[0]["SR_NO"].ToString().Trim();
                    Byte[] bytes = (Byte[])dt.Rows[0]["IMAGE"];
                    int height, width, total;
                    string MstWidth, MstHeight, fileType = dt.Rows[0]["IMAGE_name"].ToString();
                    var base64String = "";
                    //PDFCode
                    if (fileType.Contains(".pdf"))
                    {
                        Label15.Text = lbldocName.Text;
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

                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "alert('js called');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "showimage(" + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + ", 1);", true);
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "showimage(" + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + ", 1);", true);
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "UpdateMsg", "showimage(" + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + ", 1);", true);
                    }

                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "HideProgressBar();", true);
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
                    objErr.LogErr(1, "CKYCQC.aspx.cs", "gvDocDtls_RowCommand", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {

        }

        protected void btnCroppreview_Click(object sender, EventArgs e)
        {
            try
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "funcopencrop2();", true);
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CKYCQC.aspx.cs", "btnCroppreview_click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                // dataAccessLayer = null;
            }
        }

        protected void btnReupload_Click(object sender, EventArgs e)
        {
            img3.ImageUrl = Path.GetFileName(FileUpload.FileName);
            if (FileUpload.HasFile)
            {
                hdnDocType.Value = "IMG";
                hdnDocAction.Value = "ReUploaded";
                hdnIsReUpload.Value = "Y";
                SaveButn(this, e);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please Select the Document');", true);
            }
        }

        protected void btnReupload1_Click(object sender, EventArgs e)
        {
            img3.ImageUrl = Path.GetFileName(FileUpload1.FileName);
            if (FileUpload1.HasFile)
            {
                hdnDocType.Value = "PDF";
                hdnDocAction.Value = "ReUploaded";
                hdnIsReUpload.Value = "Y";
                SaveButn(this, e);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please Select the Document');", true);
            }

        }
    }
}
