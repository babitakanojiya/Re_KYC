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

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class CKYCViewDetails : System.Web.UI.Page
    {
        #region Declarations
        Hashtable hTable = new Hashtable();
        ErrorLog objErr = new ErrorLog();
        //DataAccessLayer dataAccessLayer;
        DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
        CommonUtility oCommonUtility = new CommonUtility();
        DataTable dt;
        int AppID;
        private string Message = string.Empty;
        string strUsrrole = string.Empty;
        private MultilingualManager olng;
        private string strUserLang;
        string strAppID = string.Empty;
        string strModuleID = string.Empty;
        string UserID = string.Empty;
        Guid obj = Guid.NewGuid();
        DataSet dsRel = new DataSet();
        string strTempRefNo = string.Empty;
        string refno;
        string kycno;
        public static string FlagPageTyp = "";
        int AppId;
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

        protected void btnView_Click(object sender, EventArgs e)
        {

        }

        protected void gvDocDtls_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "View")
                {
                    //string Preview = e.CommandArgument.ToString().Trim();
                    string RefNo = txtCKYCRefNo.Text.ToString();// = strRefNo;

                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                    Label lbldocName = (Label)row.FindControl("lbldocName");
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
                        if (Request.QueryString["flag"].ToString().Trim() != "" && Request.QueryString["flag"].ToString().Trim() == "DWNL")
                        {
                            kycno = Request.QueryString["kycno"].ToString().Trim();
                            hTable.Add("@kycno", kycno);
                            hTable.Add("@IMAGECODE", lbldoccode.Text);
                            dt = dataAccessLayer.GetDataTable("Prc_GetDocNamesforDNWL", hTable);
                        }
                        else
                        {
                            hTable.Add("@RegRefNo", RefNo);
                            hTable.Add("@flag", (hdnBRANCH_No.Value != null ? "9" : "2"));
                            hTable.Add("@DocType", lbldocName.Text);
                            hTable.Add("@kycno", txtCKYCRefNo.Text.ToString());
                            hTable.Add("@Batchid", hdnBRANCH_No.Value);
                            dt = dataAccessLayer.GetDataTable("Prc_GetDocNames", hTable);

                        }
                    }
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
                        hdndocName.Value= lbldocName.Text;
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
                    string flag = Request.QueryString["flag"].ToString().Trim();
                    string Imgsrc = "ImageCSharp.aspx?ImageID=" + id;
                    string Doctype = dt.Rows[0]["DOC_NAME"].ToString().Trim();


                    //if (Request.QueryString["flag"].ToString().Trim() != "" && Request.QueryString["flag"].ToString().Trim() == "DWNL")
                    //{
                    //    //string Imgsrc = "ImageCSharp.aspx?ImageID=" + id + "&flag=" + Request.QueryString["flag"].ToString().Trim();
                    //    string Imgsrc = "ImageCSharp.aspx?ImageID=" + id + "&flag=" + flag;


                    //}
                    //else
                    //{

                    //PDFCode
                    if (fileType.Contains(".pdf"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "showpdf('" + base64String + "'," + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + ", 1);", true);
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "UpdateMsg", "showpdf('" + base64String + "'," + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + ", 1);", true);
                    }
                    else
                    {
                          if (Request.QueryString["flag"].ToString().Trim() != "" && Request.QueryString["flag"].ToString().Trim() == "DWNL")
                        {

                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsgDwnld", "showimageDNWL(" + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + "," + kycno + " );", true);
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "showimageDNWL(" + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + ", " + kycno + ",1,"+ hdndocName.Value + ");", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "showimageDNWL(" + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + ", " + kycno + ",1,'" + hdndocName.Value + "');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "showimage(" + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + ", 1);", true);

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
                    objErr.LogErr(1, "CKYCViewDetails.aspx.cs", "gvDocDtls_RowCommand", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        byte IMAGE_DATA_IMG1;
        #endregion

        #region PAGELOADEVENTS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
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
                    olng = new MultilingualManager("DefaultConn", "CKYCViewDetails.aspx", Session["UserLangNum"].ToString());
                    strUserLang = HttpContext.Current.Session["UserLangNum"].ToString();
                    InitializeControls();
                    // if (Request.QueryString["refno"].ToString().Trim() != "") // commit by pallavi on 14122023

                    if (Request.QueryString["refno"] != null || Request.QueryString["refno"].ToString().Trim() != "")
                    {
                        refno = Request.QueryString["refno"].ToString().Trim();
                        hdnRegRefNo.Value = refno;
                    }
                    if (Request.QueryString["kycno"].ToString().Trim() != "")
                    {
                        kycno = Request.QueryString["kycno"].ToString().Trim();
                    }
                    if (Request.QueryString["Status"].ToString() == "Service")
                    {
                        // System.Threading.Thread.Sleep(5000);
                        DataSet ds = new DataSet();
                        ds = (DataSet)Session["sessDataSet"];
                        PopulateCndPersonalDtlsforservice();
                        // ImageRendering(refno);
                        Disables();
                        mvmt.Visible = false;  //changed by rutuja today
                        ddd.Visible = false; //changed by rutuja today
                    }

                    if (Request.QueryString["Status"].ToString() == "view")
                    {

                        if (Request.QueryString["flag"].ToString().Trim() != "" && Request.QueryString["flag"].ToString().Trim() == "DWNL")
                        {
                            lblNatureOfBussImp.Visible = false;
                            Label5Imp.Visible = false;
                            POPAccountType();
                            GetPersonalDetails();
                            BindGridPOIDNWL();
                            FillDocumentReceived();
                            disablecntrl();
                            BindGridUPDDOC();
                        }

                        else
                        {

                            dvTINCntry.Attributes.Add("style", "display:block");//Added by ajay 1-27-2022
                            ddlTINCountry.Enabled = false;
                            divIdProof.Visible = false;
                            subPopulateAccountType(); //added by rutuja 
                            FillDocumentReceived(); //added by rutuja
                            FillRequiredDataForCKYC();
                            disablecntrl();
                            //FillddlPageLoad();
                            //PopulateCndPersonalDtlsforservice(); //added by rutuja
                            GetRelatedPersonDataForCKYC();
                            if (refno != null)
                            {
                                // ImageRendering(refno.ToString());
                            }
                            BindGrid();
                            BindCandidateMvmt();
                            divchkAddRel.Visible = false;
                            //txtKYCNumber.Visible = false;
                            //lblKYCNumber.Visible = false;
                        }
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCViewDetails.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong.....');", true);
                }
            }
            finally
            {
                objDAL = null;
            }
        }
        #endregion
        //Added by Rutuja for Account type
        private void subPopulateAccountType()
        {
            try
            {
                Hashtable htParam = new Hashtable();
                htParam.Clear();
                htParam.Clear();
                htParam.Add("@Flag", "Y");
                dt = objDAL.GetDataTable("Prc_GetAccountTypeDtls", htParam);
                if (dt.Rows.Count > 0)
                {
                    oCommonUtility.FillDropDown(ddlAccountType, dt, "AccountTypeCode", "AccountTypeDesc");
                }
                dt = null;

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
                    //                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        public void FillDropdowns(string strQuery, Hashtable htable, DropDownList ddl, string strDBKey, bool isSelect)
        {
            DataTable dtFill = new DataTable();
            objDAL = new DataAccessLayer("CKYCConnectionString");
            dtFill.Clear();
            dtFill = objDAL.GetDataTable(strQuery, htable, strDBKey);
            if (dtFill.Rows.Count > 0)
            {
                ddl.Items.Clear();
                ddl.DataSource = dtFill;
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

                Hashtable htParam = new Hashtable();
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
                    //objErr.LogErr(AppID, "CKYCVieDetails.aspx.cs", "FillDocumentReceived", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;
                dt = null;
            }
        }

        //ended by rutuja for Account type

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
            }
            catch (Exception ex)
            {

                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCViewDetails.aspx.cs", "", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;

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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCViewDetails.aspx.cs", "", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;
            }
        }
        #endregion

        #region BindCandidateMvmt
        private void BindCandidateMvmt()
        {
            try
            {
                dt = new DataTable();
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = null;
                hTable.Clear();
                objDAL = new DataAccessLayer("CKYCConnectionString");
                if (refno != null)
                {
                    hTable.Add("@Refno", refno.ToString());
                }
                else
                {
                    hTable.Add("@Refno", "");
                }
                dt = objDAL.GetDataTable("Prc_GetKycStatusMvmt", hTable);

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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCViewDetails.aspx.cs", "BindCandidateMvmt", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;

            }
        }
        #endregion

        #region ImageRendering
        public void ImageRendering(string strRefNo)
        {
            try
            {
                dt = new DataTable();
                objDAL = new DataAccessLayer("CKYCConnectionString");
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

                dt = objDAL.GetDataTable("prc_GetDocType_CKYC", hTable);
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
                            dsImageDoc = objDAL.GetDataSet("prc_GetDocType_CKYC", hTable);

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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCViewDetails.aspx.cs", "ImageRendering", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;

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
                //below commented by rutuja
                //if (Convert.ToString(ds.Tables[4].Rows[0]["Acc_Type"]) == "01")
                //{
                //    chkNormal.Checked = true;
                //}
                //else if (Convert.ToString(ds.Tables[4].Rows[0]["Acc_Type"]) == "02")
                //{
                //    chkSimplified.Checked = true;
                //}
                //else if (Convert.ToString(ds.Tables[4].Rows[0]["Acc_Type"]) == "03")
                //{
                //    Chksmall.Checked = true;
                //}



                if (Convert.ToString(ds.Tables[4].Rows[0]["CONSTI_TYPE"]) == "01")
                {
                    cbNew.Checked = true;

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
                    //lblPassportNo.Text = "Passport Number";
                    llPassExpDate.Text = "Passport Expiry Date";
                    llPassExpDate.Visible = false;

                    txtPassNo.Text = ds.Tables[6].Rows[0]["IDENT_NUM"].ToString();
                    txtPassExpDate.Text = ds.Tables[6].Rows[0]["ID_EXPIRYDATE"].ToString();

                }
                else if (ds.Tables[6].Rows[0]["IDENT_TYPE"].ToString() == "B")
                {
                    divIdProof.Visible = true;
                    //lblPassportNo.Text = "Voter ID Card";
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    divPass.Visible = false;

                    txtPassNo.Text = ds.Tables[6].Rows[0]["IDENT_NUM"].ToString();
                }
                else if (ds.Tables[6].Rows[0]["IDENT_TYPE"].ToString() == "C")
                {
                    //lblPassportNo.Text = "PAN Card";
                    btnAddTrnsfr.Visible = true;
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    divPass.Visible = false;

                    txtPassNo.Text = ds.Tables[6].Rows[0]["IDENT_NUM"].ToString();

                }
                else if (ds.Tables[6].Rows[0]["IDENT_TYPE"].ToString() == "D")
                {
                    divIdProof.Visible = true;
                    btnAddTrnsfr.Visible = true;
                    //lblPassportNo.Text = "Driving Licence";
                    llPassExpDate.Text = "DrivingLicence Expiry Date";
                    txtPassNo.Text = ds.Tables[6].Rows[0]["IDENT_NUM"].ToString();
                    txtPassExpDate.Text = ds.Tables[6].Rows[0]["ID_EXPIRYDATE"].ToString();
                }
                else if (ds.Tables[6].Rows[0]["IDENT_TYPE"].ToString() == "E")
                {
                    //lblPassportNo.Text = "UID(Aadhaar)";
                    btnAddTrnsfr.Visible = true;
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    divPass.Visible = false;
                    //ddlProofIdentity.Text = "UID(Aadhaar)";

                    txtPassNo.Text = ds.Tables[6].Rows[0]["IDENT_NUM"].ToString();
                }
                else if (ds.Tables[6].Rows[0]["IDENT_TYPE"].ToString() == "F")
                {
                    divIdProof.Visible = true;
                    btnAddTrnsfr.Visible = true;
                    //lblPassportNo.Text = "NREGA Job Card";
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    divPass.Visible = false;
                    //ddlProofIdentity.Text = "NREGA Job Card";
                    txtPassNo.Text = ds.Tables[6].Rows[0]["IDENT_NUM"].ToString();
                }
                else if (ds.Tables[6].Rows[0]["IDENT_TYPE"].ToString() == "Z")
                {
                    divIdProof.Visible = true;
                    btnAddTrnsfr.Visible = true;
                    //lblPassportNo.Text = "Others";
                    llPassExpDate.Text = "Identification Number";
                    //ddlProofIdentity.Text = "Other";
                    txtPassNo.Text = ds.Tables[6].Rows[0]["IDENT_NUM"].ToString();
                    txtPassExpDate.Text = ds.Tables[6].Rows[0]["ID_EXPIRYDATE"].ToString();
                }

                else
                {
                    btnAddTrnsfr.Visible = false;
                    divIdProof.Visible = false;
                }
                lblPassportNo.Text = "Document Number";
                //commented by kalyani hande start
                //GetCKYCdata(ddlAddressType, ds.Tables[4].Rows[0]["RESI_STATUS"].ToString(), "KAddr");
                //GetCKYCdata(ddlProofOfAddress, ds.Tables[4].Rows[0]["PERM_POA"].ToString(), "KAddrPrf");
                //commented by kalyani hande end
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

                //below commented by rutuja
                //if (ds.Tables[4].Rows[0]["JURI_SAME_FLAG"].ToString() == "1")
                //{
                //    chkCurrentAdd.Checked = true;
                //}
                //else if (ds.Tables[4].Rows[0]["JURI_SAME_FLAG"].ToString() == "2")
                //{
                //    //chkCorresAdd.Checked = true;
                //}
                //else
                //{
                //    chkCurrentAdd.Checked = false;
                //    //chkCorresAdd.Checked = false;
                //}

                //txtAddLine1.Text = Convert.ToString(ds.Tables[4].Rows[0]["JURI_LINE1"]);
                //txtAddLine2.Text = Convert.ToString(ds.Tables[4].Rows[0]["JURI_LINE2"]);
                //txtAddLine3.Text = Convert.ToString(ds.Tables[4].Rows[0]["JURI_LINE3"]);
                //txtCity2.Text = Convert.ToString(ds.Tables[4].Rows[0]["JURI_CITY"]);
                //ddlDistrict2.Text = Convert.ToString(ds.Tables[4].Rows[0]["JURI_DIST"]);
                //ddlPinCode2.Text = Convert.ToString(ds.Tables[4].Rows[0]["JURI_PIN"]);
                //ddlState2.Text = Convert.ToString(ds.Tables[4].Rows[0]["JURI_STATE"]);
                //txtIsoCountryCode.Text = Convert.ToString(ds.Tables[4].Rows[0]["JURI_COUNTRY"]);
                //ended commented by rutuja

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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCViewDetails.aspx.cs", "PopulateCndPersonalDtlsforservice", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;

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

                hTable.Add("@LookUpCode", lookupcode);
                hTable.Add("@Paramvalue", prmvalue);
                dt = objDAL.GetDataTable("Prc_GetCKYCValue", hTable);
                txtbox.Text = dt.Rows[0]["ParamDesc"].ToString();
            }
            catch (Exception ex)
            {

                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCViewDetails.aspx.cs", "GetCKYCdata", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;

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
                hTable.Add("@FATHERSPOUSE_FLAG", ds.Tables[0].Rows[0]["FS_FLAG"].ToString());
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
                hTable.Add("@RESI_STD_CODE", Convert.ToString(ds.Tables[0].Rows[0]["std_resTele"]));
                hTable.Add("@RESI_TEL_NUM", Convert.ToString(ds.Tables[4].Rows[0]["RESI_TEL_NUM"]));
                hTable.Add("@OFF_STD_CODE", Convert.ToString(ds.Tables[0].Rows[0]["std_officeTele"]));
                hTable.Add("@OFF_TEL_NUM", Convert.ToString(ds.Tables[4].Rows[0]["OFF_TEL_NUM"]));
                hTable.Add("@MOB_CODE", Convert.ToString(ds.Tables[0].Rows[0]["mobile_countryCode"]));
                hTable.Add("@MOB_NUM", Convert.ToString(ds.Tables[4].Rows[0]["MOB_NUM"]));
                hTable.Add("@FAX_CODE", Convert.ToString(ds.Tables[0].Rows[0]["std_fax"]));
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
                hTable.Add("@IMAGE_TYPE_IMG1", Convert.ToString(ds.Tables[10].Rows[0]["IMAGE_TYPE"]));
                hTable.Add("@IMAGE_CODE_IMG1", Convert.ToString(ds.Tables[10].Rows[0]["IMAGE_CODE"]));
                hTable.Add("@GLOBAL_FLAG_IMG1", Convert.ToString(ds.Tables[10].Rows[0]["GLOBAL_FLAG"]));
                hTable.Add("@BRANCH_CODE_IMG1", Convert.ToString(ds.Tables[10].Rows[0]["BRANCH_CODE"]));
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

                ds = objDAL.GetDataSet("prc_InsXMLResponse", hTable, "STAGINGConnectionString");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        strTempRefNo = ds.Tables[0].Rows[0]["TempRegRefNo"].ToString();
                    }
                }
                hTable.Add("@PartialRegRefNo", strTempRefNo);
                hTable.Add("@uniqueID", obj.ToString());
                hTable.Add("@Usages", "W");
                hTable.Add("@Status", Request.QueryString["Status"].ToString());
                ds = objDAL.GetDataSet("prc_InsXMLResponse_KYCReg", hTable, "CKYCConnectionString");
            }
            catch (Exception ex)
            {

                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCViewDetails.aspx.cs", "SaveXMLDataInTbl", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;

            }
        }
        #endregion
        #region METHOD "FillRequiredDataForCndPersonal"
        protected void FillRequiredDataForCKYC()
        {
            try
            {
                AppDeclare1.Checked = true;
                chkAppDeclare2.Checked = true;
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = null;
                hTable.Clear();
                hTable.Add("@ActionFlag", "KMod");
                if (refno != null)
                {
                    hTable.Add("@RegRefNo", refno.ToString());
                    //hTable.Add("@kycno", kycno);
                    //added by ajay 1-27-2022 start
                    if (kycno == null)
                    {
                        hTable.Add("@kycno", "");
                    }
                    else
                    {
                        hTable.Add("@kycno", kycno);

                    }
                    //added by ajay 1-27-2022 start
                }
                else
                {
                    hTable.Add("@RegRefNo", "");
                    hTable.Add("@kycno", kycno);

                }
                //hTable.Add("@UserID", UserID.ToString());
                hTable.Add("@UserType", System.DBNull.Value);
                dt = objDAL.GetDataTable("Prc_GetCkycViewData", hTable);
                hdnBRANCH_No.Value = Convert.ToString(dt.Rows[0]["BRANCH_No"]).ToString();
                if (Convert.ToString(dt.Rows[0]["CustType"]) == "02")
                {
                    FlagPageTyp = "Legal";
                    loadLegalEntityPageCtrl("Y");
                    BindGridPOI();
                    GridView2.Visible = false;
                    lblCommlogr.Visible = false;
                }
                else
                {
                    FlagPageTyp = "Indiviual";
                    loadLegalEntityPageCtrl("N");
                    BindGridPOI();
                    GridView2.Visible = true;
                    lblCommlogr.Visible = false;
                }
                if (Convert.ToString(dt.Rows[0]["AccType"]) == "01" || Convert.ToString(dt.Rows[0]["AccType"]) == "1")
                {
                    ddlAccountType.SelectedIndex = 1;
                    //chkNormal.Checked = true;
                }
                else if (Convert.ToString(dt.Rows[0]["AccType"]) == "02" || Convert.ToString(dt.Rows[0]["AccType"]) == "2")
                {
                    ddlAccountType.SelectedIndex = 2;
                    //chkSimplified.Checked = true;
                }
                else if (Convert.ToString(dt.Rows[0]["AccType"]) == "03" || Convert.ToString(dt.Rows[0]["AccType"]) == "3")
                {
                    ddlAccountType.SelectedIndex = 3;
                    //Chksmall.Checked = true;
                }
                else if (Convert.ToString(dt.Rows[0]["AccType"]) == "04" || Convert.ToString(dt.Rows[0]["AccType"]) == "4")
                {
                    ddlAccountType.SelectedIndex = 4;
                    //Chksmall.Checked = true;
                }
                else if (Convert.ToString(dt.Rows[0]["AccType"]) == "05" || Convert.ToString(dt.Rows[0]["AccType"]) == "5")
                {
                    ddlAccountType.SelectedIndex = 5;
                    //Chksmall.Checked = true;
                }
                hdnBRANCH_No.Value = Convert.ToString(dt.Rows[0]["BRANCH_No"]).ToString();
                ddlNatureOfBuss.SelectedValue = Convert.ToString(dt.Rows[0]["CompType"]).ToString();
                txtConstitutionTypeothers.Text = Convert.ToString(dt.Rows[0]["ConstitutionTypeothers"]).ToString();
                if (Convert.ToString(dt.Rows[0]["CompType"]) == "01" || Convert.ToString(dt.Rows[0]["CompType"]) == "1")
                {
                    ddlNatureOfBuss.Items.Insert(0, new ListItem("Individual", ""));
                    // ddlNatureOfBuss.SelectedIndex = 01;
                }
                txtConstitutionTypeothers.Enabled = false;
                txtKYCName.Text = Convert.ToString(dt.Rows[0]["EntityName"]).ToString();
                txtDatOfInc.Text = Convert.ToString(dt.Rows[0]["DateofIncorporation"]).ToString();
                txtDtOfCom.Text = Convert.ToString(dt.Rows[0]["DateofCommencementofbusiness"]).ToString();
                txtPlaceOfInc.Text = Convert.ToString(dt.Rows[0]["PlaceofIncorporation"]).ToString();
                ddlCountrOfInc.SelectedValue = Convert.ToString(dt.Rows[0]["CountryofIncorporation"]).ToString();
                txtMobile1.Text = Convert.ToString(dt.Rows[0]["MobCode2"]).ToString(); //MobCode2,MobileNo2,EmailID2
                txtMobile3.Text = Convert.ToString(dt.Rows[0]["MobileNo2"]).ToString(); //MobCode2,MobileNo2,EmailID2
                txtemail2.Text = Convert.ToString(dt.Rows[0]["EmailID2"]).ToString(); //MobCode2,MobileNo2,EmailID2
                ddlTINCountry.SelectedItem.Text = ddlCountrOfInc.SelectedItem.Text; //added by ajay 1-27-2022
                if (dt.Rows[0]["PanLegal"] != null)
                    txtPanNoLegal.Text = Convert.ToString(dt.Rows[0]["PanLegal"]).ToString();

                if (Convert.ToString(dt.Rows[0]["Form60Legal"]).ToString() == "Y")
                {
                    chkPanFormLegal.Checked = true;
                }
                else { chkPanFormLegal.Checked = false; }
                //txtPanNoLegal.Text= Convert.ToString(dt.Rows[0]["PAN"]).ToString();
                txtTypeIdentiNo.Text = Convert.ToString(dt.Rows[0]["TAX_IDNUMBER"]).ToString();
                //added by rutuja
                if (Convert.ToString(dt.Rows[0]["AccType"]) == "01")
                {
                    ddlAccountType.SelectedIndex = 1;
                    // chkNormal.Checked = true;
                }
                else if (Convert.ToString(dt.Rows[0]["AccType"]) == "02")
                {
                    ddlAccountType.SelectedIndex = 2;
                    //chkSimplified.Checked = true;
                }
                else if (Convert.ToString(dt.Rows[0]["AccType"]) == "03")
                {
                    ddlAccountType.SelectedIndex = 3;
                    //Chksmall.Checked = true;
                }
                else if (Convert.ToString(dt.Rows[0]["AccType"]) == "04")
                {
                    ddlAccountType.SelectedIndex = 4;
                    //Chksmall.Checked = true;
                }
                else if (Convert.ToString(dt.Rows[0]["AccType"]) == "05")
                {
                    ddlAccountType.SelectedIndex = 5;
                    //Chksmall.Checked = true;
                }
                //ended by rutuja
                txtKYCNumber.Text = Convert.ToString(dt.Rows[0]["KYC_NO"]);
                txtRefNumber.Text = Convert.ToString(dt.Rows[0]["FIRefNo"]);
                txtCKYCRefNo.Text = Convert.ToString(dt.Rows[0]["RegRefNo"]);
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

                // ddlProofIdentity.Text = Convert.ToString(dt.Rows[0]["IdName"]);
                ddlProofIdentity.Enabled = false;
                ViewState["strIdName"] = Convert.ToString(dt.Rows[0]["IdName"]);
                ViewState["strIdNumber"] = Convert.ToString(dt.Rows[0]["IdNumber"]);
                ViewState["strIdExpDate"] = Convert.ToString(dt.Rows[0]["IdExpDate"]);


                txtPassOthr.Visible = false;
                if (dt.Rows[0]["IdType"].ToString() == "A")
                {
                    divIdProof.Visible = true;
                    //lblPassportNo.Text = "Passport Number";
                    llPassExpDate.Text = "Passport Expiry Date";
                    llPassExpDate.Visible = false;

                    //ddlProofIdentity.Text = "Passport Number";
                    txtPassNo.Text = dt.Rows[0]["IdNumber"].ToString();
                    txtPassExpDate.Text = dt.Rows[0]["IdExpDate"].ToString();

                }
                else if (dt.Rows[0]["IdType"].ToString() == "B")
                {
                    divIdProof.Visible = true;
                    //lblPassportNo.Text = "Voter ID Card";
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    divPass.Visible = false;


                    //ddlProofIdentity.Text = "Voter ID Card";
                    txtPassNo.Text = dt.Rows[0]["IdNumber"].ToString();
                }
                else if (dt.Rows[0]["IdType"].ToString() == "D")
                {
                    divIdProof.Visible = true;
                    btnAddTrnsfr.Visible = true;
                    //lblPassportNo.Text = "Driving Licence";
                    llPassExpDate.Text = "DrivingLicence Expiry Date";

                    //ddlProofIdentity.Text = "Driving Licence";
                    txtPassNo.Text = dt.Rows[0]["IdNumber"].ToString();
                    txtPassExpDate.Text = dt.Rows[0]["IdExpDate"].ToString();
                }
                else if (dt.Rows[0]["IdType"].ToString() == "E")
                {
                    divIdProof.Visible = true;
                    //lblPassportNo.Text = "UID(Aadhaar)";
                    btnAddTrnsfr.Visible = true;
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    divPass.Visible = false;


                    //ddlProofIdentity.Text = "UID(Aadhaar)";
                    txtPassNo.Text = dt.Rows[0]["IdNumber"].ToString();
                    txtPassNo.Visible = true;

                }
                else if (dt.Rows[0]["IdType"].ToString() == "F")
                {
                    divIdProof.Visible = true;
                    btnAddTrnsfr.Visible = true;
                    //lblPassportNo.Text = "NREGA Job Card";
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    divPass.Visible = false;

                    //ddlProofIdentity.Text = "NREGA Job Card";
                    txtPassNo.Text = dt.Rows[0]["IdNumber"].ToString();
                    txtPassExpDate.Text = dt.Rows[0]["IdExpDate"].ToString();
                }
                else if (dt.Rows[0]["IdType"].ToString() == "Z")
                {
                    divIdProof.Visible = true;
                    btnAddTrnsfr.Visible = true;
                    //lblPassportNo.Text = "Others";
                    llPassExpDate.Text = "Identification Number";

                    txtPassOthr.Visible = false;
                    //ddlProofIdentity.Text = "Other";
                    txtPassNo.Text = dt.Rows[0]["IdName"].ToString();
                    txtPassExpDate.Text = dt.Rows[0]["IdNumber"].ToString();

                }
                else if (dt.Rows[0]["IdType"].ToString() == "C")
                {
                    divIdProof.Visible = true;
                    //lblPassportNo.Text = "PAN Card";
                    btnAddTrnsfr.Visible = true;
                    llPassExpDate.Visible = false;
                    txtPassExpDate.Visible = false;
                    divPass.Visible = false;

                    //ddlProofIdentity.Text = "PAN Card";
                    txtPassNo.Text = dt.Rows[0]["IdNumber"].ToString();

                }
                else
                {
                    btnAddTrnsfr.Visible = false;
                    divIdProof.Visible = false;
                }
                lblPassportNo.Text = "Document Number";

                //Commented by kalyani hande start
                //if (Convert.ToString(dt.Rows[0]["CnctType1"]) == "P1")
                //{
                //    chkPerAddress.Checked = true;

                //}
                //else
                //{
                //    chkPerAddress.Checked = false;
                //}
                //ddlAddressType.Text = Convert.ToString(dt.Rows[0]["PER_ADDTYPE"]);
                //ddlProofOfAddress.Text = Convert.ToString(dt.Rows[0]["PER_ADDPROOFDesc"]);
                //divAddProof.Visible = false;

                //if (dt.Rows[0]["PER_ADDPROOF"].ToString().Trim() == "02")
                //{
                //    divAddProof.Visible = true;
                //    lblPassportNoAdd.Text = "Passport Number";
                //    llPassExpDateAdd.Text = "Passport Expiry Date";
                //    llPassExpDateAdd.Visible = true;
                //    txtPassExpDateAdd.Visible = true;
                //    divPassAdd.Visible = true;
                //    txtPassOthrAdd.Visible = false;
                //    txtPassNoAdd.Visible = true;

                //    txtPassNo.MaxLength = 15;
                //    txtPassNo.Attributes.Remove("onblur");
                //    txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");
                //}
                //else if (dt.Rows[0]["PER_ADDPROOF"].ToString().Trim() == "03")
                //{

                //    divAddProof.Visible = true;
                //    lblPassportNoAdd.Text = "Driving Licence";
                //    llPassExpDateAdd.Text = "Driving Licence Expiry Date";
                //    llPassExpDateAdd.Visible = true;
                //    txtPassExpDateAdd.Visible = true;
                //    txtPassOthrAdd.Visible = false;
                //    divPassAdd.Visible = true;
                //    txtPassNoAdd.Visible = true;
                //    txtPassNoAdd.MaxLength = 15;
                //    txtPassNoAdd.Attributes.Remove("onblur");
                //}
                //else if (dt.Rows[0]["PER_ADDPROOF"].ToString().Trim() == "01")
                //{
                //    divAddProof.Visible = true;
                //    lblPassportNoAdd.Text = "UID(Aadhaar)";
                //    llPassExpDateAdd.Visible = false;
                //    txtPassExpDateAdd.Visible = false;
                //    txtPassOthrAdd.Visible = false;
                //    divPassAdd.Visible = false;
                //    txtPassNoAdd.Visible = true;
                //    txtPassNoAdd.MaxLength = 12;
                //    txtPassNoAdd.Attributes.Remove("onblur");
                //    txtPassNoAdd.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                //}
                //else if (dt.Rows[0]["PER_ADDPROOF"].ToString().Trim() == "04")
                //{
                //    divAddProof.Visible = true;
                //    lblPassportNoAdd.Text = "Voter ID Card";
                //    llPassExpDateAdd.Visible = false;
                //    txtPassExpDateAdd.Visible = false;
                //    txtPassOthrAdd.Visible = false;
                //    divPassAdd.Visible = false;
                //    txtPassNoAdd.Visible = true;
                //    txtPassNo.MaxLength = 15;
                //    txtPassNoAdd.Attributes.Remove("onblur");
                //}
                //else if (dt.Rows[0]["PER_ADDPROOF"].ToString().Trim() == "05")
                //{
                //    divAddProof.Visible = true;
                //    lblPassportNoAdd.Text = "NREGA Job Card";
                //    llPassExpDateAdd.Visible = false;
                //    txtPassNoAdd.Visible = true;
                //    txtPassExpDateAdd.Visible = false;
                //    txtPassOthrAdd.Visible = false;
                //    divPassAdd.Visible = false;
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
                //    llPassExpDateAdd.Visible = true;
                //    txtPassExpDateAdd.Visible = false;
                //    txtPassOthrAdd.Visible = true;
                //    txtPassNoAdd.Visible = true;
                //    txtPassNoAdd.MaxLength = 15;
                //    txtPassNoAdd.Attributes.Remove("onblur");
                //}

                //Commented by kalyani hande end
                txtAddressLine1.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE1"]);
                txtAddressLine2.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE2"]);
                txtAddressLine3.Text = Convert.ToString(dt.Rows[0]["PER_ADDLINE3"]);
                txtCity.Text = Convert.ToString(dt.Rows[0]["PER_CITY"]);
                ddlDistrict.Text = Convert.ToString(dt.Rows[0]["PER_DISTRICT"]);
                ddlPinCode.Text = Convert.ToString(dt.Rows[0]["PER_PIN"]);
                ddlState.Text = Convert.ToString(dt.Rows[0]["PER_STATECODE"]);
                // ddlState.Text = Convert.ToString(dt.Rows[0]["PER_STATEDesc"]);
                if (ddlState.Text == "")
                {
                    ddlState.Text = Convert.ToString(dt.Rows[0]["PER_STATEDesc"]);

                }
                txtCountryCode.Text = Convert.ToString(dt.Rows[0]["PER_COUNTRY_CODE"]);

                ViewState["strAddIdName"] = Convert.ToString(dt.Rows[0]["AddIdName"]);
                ViewState["strAddIdNumber"] = Convert.ToString(dt.Rows[0]["AddIdNumber"]);
                ViewState["strAddIdExpDate"] = Convert.ToString(dt.Rows[0]["AddIdExpDate"]);

                //Commented by kalyani hande start
                //if (dt.Rows[0]["PER_ADDPROOF"].ToString() == "99")
                //{

                //    txtPassOthrAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNumber"]);
                //    txtPassNoAdd.Text = Convert.ToString(dt.Rows[0]["AddIdName"]);
                //}
                //else
                //{
                //    txtPassNoAdd.Text = Convert.ToString(dt.Rows[0]["AddIdNumber"]);
                //    txtPassExpDateAdd.Text = Convert.ToString(dt.Rows[0]["AddIdExpDate"]);
                //}
                //Commented by kalyani hande end

                if (Convert.ToString(dt.Rows[0]["CnctType2"]) == "M1")
                {
                    chkLocalAddress.Checked = true;
                }
                else
                {
                    chkLocalAddress.Checked = false;
                }

                if (Convert.ToString(dt.Rows[0]["CurSameAsFlag"]) == "Y") //done Y by Rutuja on 21jul2021
                {
                    chkCuurentAddress.Checked = true;
                }
                else
                {
                    chkCuurentAddress.Checked = false;
                }
                if (Convert.ToString(dt.Rows[0]["CurSameAsFlag"]) == "01")
                {
                    chkCuurentAddress.Checked = true;
                }
                txtLocAddLine1.Text = Convert.ToString(dt.Rows[0]["CUR_ADDLINE1"]);
                txtLocAddLine2.Text = Convert.ToString(dt.Rows[0]["CUR_ADDLINE2"]);
                txtLocAddLine3.Text = Convert.ToString(dt.Rows[0]["CUR_ADDLINE3"]);
                txtCity1.Text = Convert.ToString(dt.Rows[0]["CUR_CITY"]);
                ddlDistrict1.Text = Convert.ToString(dt.Rows[0]["CUR_DISTRICT"]);
                ddlPinCode1.Text = Convert.ToString(dt.Rows[0]["CUR_PIN"]);
                ddlState1.Text = Convert.ToString(dt.Rows[0]["CUR_STATECODE"]);
                //ddlState1.Text = Convert.ToString(dt.Rows[0]["CUR_STATEDesc"]);
                if (ddlState1.Text == "")
                {
                    ddlState1.Text = Convert.ToString(dt.Rows[0]["CUR_STATEDesc"]);

                }
                txtCountryCode1.Text = Convert.ToString(dt.Rows[0]["CUR_COUNTRY_CODE"]);

                //below commented by rutuja
                //if (Convert.ToString(dt.Rows[0]["CnctType3"]) == "J1")
                //{
                //    chkAddResident.Checked = true;
                //}
                //else
                //{
                //    chkAddResident.Checked = false;
                //}
                //txtAddLine1.Text = Convert.ToString(dt.Rows[0]["FRN_ADDLINE1"]);
                //txtAddLine2.Text = Convert.ToString(dt.Rows[0]["FRN_ADDLINE2"]);
                //txtAddLine3.Text = Convert.ToString(dt.Rows[0]["FRN_ADDLINE3"]);
                //txtCity2.Text = Convert.ToString(dt.Rows[0]["FRN_CITY"]);
                //ddlDistrict2.Text = Convert.ToString(dt.Rows[0]["FRN_DISTRICT"]);
                //ddlPinCode2.Text = Convert.ToString(dt.Rows[0]["FRN_PIN"]);
                //ddlState2.Text = Convert.ToString(dt.Rows[0]["FRN_STATECODE"]);
                //txtIsoCountryCode.Text = Convert.ToString(dt.Rows[0]["FRN_COUNTRY_CODE"]);
                //ended commented by rutuja

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
                txtPanNo.Text = Convert.ToString(dt.Rows[0]["PAN_NO"]); //added by rutuja
                ddlDocReceived.SelectedValue = Convert.ToString(dt.Rows[0]["kycCertDoc"]);  //added by rutuja

                if (Convert.ToString(dt.Rows[0]["PAN_NO"]) == "FORM60")
                {
                    chkPanForm.Checked = true;
                }
                else
                {
                    chkPanForm.Checked = false;
                }

                if (Convert.ToString(dt.Rows[0]["AppType"]) == "01")
                {
                    cbNew.Checked = true;
                }
                //Added by Shubham parab
                if (FlagPageTyp == "Legal")
                {
                    //ddlProofIdentity.Text = dt.Rows[0]["IDDesc"].ToString();
                    //ddlProofIdentity.Visible = true;
                    divIdProof.Visible = true;
                    //txtPassNo.Text = dt.Rows[0]["IdNumber"].ToString();
                    txtPassNo.Visible = true;
                    divPassNotxt.Visible = true;
                }
                else
                {
                    ddlProofIdentity.Visible = false;
                    lblProof.Visible = false;
                }
                //Ended by Shubham
            }
            catch (Exception ex)
            {

                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCViewDetails.aspx.cs", "FillRequiredDataForCKYC", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;

            }
        }
        #endregion

        #region METHOD "disablecntrl"
        protected void disablecntrl()
        {
            try
            {
                ddlAccountType.Enabled = false; //added by rutuja
                //txtPassExpDateAdd.Enabled = false;
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
                //ddlProofIdentity.Enabled = false;
                txtPassNo.Enabled = false;
                //txtPassNoAdd.Enabled = false;
                txtPassExpDate.Enabled = false;
                //txtPassOthrAdd.Enabled = false;
                //ddlAddressType.Enabled = false;
                //ddlProofOfAddress.Enabled = false;
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
                ddlDistrict1.Enabled = false;
                ddlPinCode1.Enabled = false;
                ddlState1.Enabled = false;
                txtCountryCode1.Enabled = false;
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
                //below added by rutuja
                txtPanNo.Enabled = false;
                //chkPerAddress.Enabled = false;
                chkLocalAddress.Enabled = false;
                chkCuurentAddress.Enabled = false;
                AppDeclare1.Enabled = false;
                chkAppDeclare2.Enabled = false;
                ddlDocReceived.Enabled = false;
                //ended added by rutuja
                //                below commented by rutuja
                //chkAddResident.Enabled = false;
                //chkAddResident.Enabled = false;
                //chkCorresAdd.Enabled = false;
                //txtAddLine1.Enabled = false;
                //txtAddLine2.Enabled = false;
                //txtAddLine3.Enabled = false;
                //txtCity2.Enabled = false;
                //ddlDistrict2.Enabled = false;
                //ddlPinCode2.Enabled = false;
                //ddlState2.Enabled = false;
                //txtIsoCountryCode.Enabled = false;
                // ended commented by rutuja
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCViewDetails.aspx.cs", "disablecntrl", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCViewDetails.aspx.cs", "gvMemDtls_RowDataBound", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCViewDetails.aspx.cs", "lnkViewRel_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;
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
                    dt = objDAL.GetDataTable("prc_DelKycRelatedPrsnDtls", hTable);
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCViewDetails.aspx.cs", "lnkdelete_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;

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
                Label lblRefNo = (Label)gvMemDtls.Rows[i].FindControl("lblRefNo");
                string RelRefnNo = lblRefNo.Text;//Convert.ToString(dr[1]);

                string refno = Request.QueryString["refno"].ToString().Trim();
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenRelatedPersonPageView(" + RelRefnNo + "," + refno + ");", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "OpenRelatedPersonPageView('" + RelRefnNo + "','" + refno + "','" + FlagPageTyp + "','" + hdnBRANCH_No.Value + "','" + txtCKYCRefNo.Text + "');", true);
            }
            catch (Exception ex)
            {

                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCViewDetails.aspx.cs", "lnkView_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;
            }
        }
        #endregion

        #region METHOD "GetRelatedPersonDataForCKYC"
        protected void GetRelatedPersonDataForCKYC()
        {
            try
            {
                dt = new DataTable();
                objDAL = new DataAccessLayer("CKYCConnectionString");
                dt = null;
                hTable.Clear();
                if (refno != null)
                {
                    hTable.Add("@RegRefNo", refno.ToString());
                    //added by ajay 1-27-2022 start
                    if (kycno == null)
                    {
                        hTable.Add("@kycno", "");
                    }
                    else
                    {
                        hTable.Add("@kycno", kycno);

                    }
                    //added by ajay 1-27-2022 start
                }
                else
                {
                    hTable.Add("@RegRefNo", "");
                    hTable.Add("@kycno", kycno);
                }
                hTable.Add("@ActionFlag", "KMod");
                hTable.Add("@UserType", "");
                hTable.Add("@RelRefNo", "");
                hTable.Add("@Batchid", hdnBRANCH_No.Value);

                dt = objDAL.GetDataTable("Prc_GetRelatedPersonDataForView", hTable);

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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCViewDetails.aspx.cs", "GetRelatedPersonDataForCKYC", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;

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
                lblName.Text = olng.GetItemDesc("lblName");
                lblMaidenName.Text = olng.GetItemDesc("lblMaidenName");
                lblFatherName.Text = olng.GetItemDesc("lblFatherName");
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
                //lblProofOfIdentity11.Text = olng.GetItemDesc("lblProofOfIdentity11");
                //lblProof.Text = "Document Type";// olng.GetItemDesc("lblProof");
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

                //lblpfPersonal1.Text = olng.GetItemDesc("lblpfPersonal1"); commented by rutuja
                lblpfPersonal.Text = olng.GetItemDesc("lblpfPersonal");
                lbltick.Text = olng.GetItemDesc("lbltick");
                //lblProofOfIdentity11.Text = olng.GetItemDesc("lblProofOfIdentity11");
                //lblpfofAddr1.Text = olng.GetItemDesc("lblpfofAddr1");
                //lblpfofAddr2.Text = olng.GetItemDesc("lblpfofAddr2"); commented by rutuja
                lblDtlOfRtltpr.Text = olng.GetItemDesc("lblDtlOfRtltpr");
                lblRemarks.Text = olng.GetItemDesc("lblRemarks");
                lblattstn.Text = olng.GetItemDesc("lblattstn");
                lbldec.Text = olng.GetItemDesc("lbldec");
                lblAttesOfc.Text = olng.GetItemDesc("lblAttesOfc");
                lblOfcuseOnly.Text = olng.GetItemDesc("lblOfcuseOnly");
                lblInsDtls.Text = olng.GetItemDesc("lblInsDtls");
                lblContactDetails.Text = olng.GetItemDesc("lblContactDetails");
                //below commented by rutuja
                //lblAddLine1.Text = olng.GetItemDesc("lblJuriAddLine1");
                //lblAddLine2.Text = olng.GetItemDesc("lblJuriAddLine2");
                //lblAddLine3.Text = olng.GetItemDesc("lblJuriAddLine3");
                //lblCity2.Text = olng.GetItemDesc("lblJurCity1");
                //lblDistrict2.Text = olng.GetItemDesc("lblJurDistrict1");
                //lblPin2.Text = olng.GetItemDesc("lblJurPin1");
                //lblState2.Text = olng.GetItemDesc("lblJurState1");
                //lblIsoCountry2.Text = olng.GetItemDesc("lblJurCountryCode1");
                //ended commented by rutuja
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
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCViewDetails.aspx.cs", "InitializeControls", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        protected void OpenCommunicationLoggerPage(object sender, EventArgs e)
        {
            // var SrvcreqCode = queryString("refno");

            //    SrvcReqHrdCode
            refno = Request.QueryString["refno"].ToString().Trim();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "OpenCommLogPage(" + refno + ");", true);
            //Response.Redirect("CommunicationLog_NEW.aspx?refno=" + refno,false);


        }

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
                divhide.Attributes.Add("style", "display:none");
                lblAccountType.Visible = false;
                lblAccountTypeImp.Visible = false;
                ddlAccountType.Visible = false;
                lblKYCNumber.Visible = true;
                txtKYCName.Visible = true;
                txtKYCName.Enabled = false;
                Label2.Visible = false;
                //ConstitutionType.Visible = false;
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
                lblProofOfIdentity11.Text = "Address";
                divRegOffAddr.Visible = true;
                divLocalAddr.Visible = true;
                chkCuurentAddress.Text = "Same as above mentioned address";
                // *************Contact DETAILS************
                divMob2.Visible = true;
                lblFax.Visible = true;
                divFax.Visible = true;
                txtFax1.Visible = true;
                txtFax2.Visible = true;
                faxdiv.Visible = true;
                divEmail2.Visible = true;
                // *************KYC VERIFICATION DETAILS************
                //Change for Legal
                lblattstn.Text = "ATTESTATION";
                lblKYCVerify.Text = "KYC VERIFICATION CARRIED OUT BY";
                //divIdVer.Visible = true;
                divPOISection.Visible = true;
            }
            if (flag == "N")
            {
                //****************FOR OFFICE USE ONLY ************************
                //Indiviual CTRL
                divhide.Attributes.Add("style", "display:block");
                lblAccountType.Visible = true;
                lblAccountTypeImp.Visible = true;
                ddlAccountType.Visible = true;

                Label2.Visible = true;
                //ConstitutionType.Visible = true;
                //Legal CTRL
                lblKYCNumber.Visible = false;
                txtKYCNumber.Visible = false;
                lblNatureOfBuss.Visible = true;  //by rutuja on 20jul2021
                lblNatureOfBussImp.Visible = false;
                ddlNatureOfBuss.Visible = true; //by rutuja on 20jul2021
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

                // *************PROOF OF IDENTITY AND ADDRESS************
                lblProofOfIdentity11.Text = "PROOF OF IDENTITY AND ADDRESS*";
                chkCuurentAddress.Text = "Same as above mentioned address";
                divRegOffAddr.Visible = false;
                divLocalAddr.Visible = false;
                // *************Contact DETAILS************
                divMob2.Visible = false;
                divFax.Visible = false;
                txtFax1.Visible = false;
                faxdiv.Visible = false;
                txtFax2.Visible = false;
                divEmail2.Visible = false;
                lblFax.Visible = true;

                // *************KYC VERIFICATION DETAILS************
                //divIdVer.Visible = false;
                divPOISection.Visible = false;

            }
        }
        //Endded By Shubham for Merge Legal into Indiviual on Page load
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
            ht.Add("@kycno", kycno);
            ht.Add("@Batchid", hdnBRANCH_No.Value);
            dt = objDAL.GetDataTable("PRC_get_TX_kycPer", ht);

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
                                ddlProofIdentity.Text = dt.Rows[j]["DocType"].ToString();
                                ddlProofIdentity.Enabled = false;
                                txtPassNo.Text = dt.Rows[j]["DocNumber"].ToString();
                                dt.Rows[j].Delete();
                            }
                        }
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        GridView2.Visible = false;
                    }
                    else
                    {
                        GridView1.Visible = false;
                        GridView2.DataSource = dt;
                        GridView2.DataBind();
                        divPassNo.Visible = false;
                        divPass.Visible = false;
                        txtPassNo.Visible = false;
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

        //protected void FillddlPageLoad()
        //{

        //    htParam.Clear();
        //    htParam.Add("@LookupCode", "KEntPoA");
        //    FillDropdowns("prc_getDDLLookUpData", htParam, ddlProofOfAddress, "CKYCConnectionString", true);
        //    htParam.Clear();

        //}
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
                objht.Add("@kycno", (kycno == null ? "" : kycno.ToString()));
                objht.Add("@Batchid", hdnBRANCH_No.Value);
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
                    objErr.LogErr(AppId, "CKYCViewDetails.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "USRMGMT");
                    throw ex;
                }
            }
            finally
            {
            }

        }
        #endregion

        protected void GetPersonalDetails()
        {

            try
            {
                htParam = new Hashtable();
                DataSet ds = new DataSet();
                objDAL = new DataAccessLayer("CKYCConnectionString");
                htParam.Add("@kycno", kycno);
                ds = objDAL.GetDataSet("Prc_GetPersonalDatafordNWLView", htParam);

                //txtKYCNumber.Text = (ds.Tables[4].Rows[0]["CKYC_NO"].ToString());

                //if (Convert.ToString(ds.Tables[4].Rows[0]["CONSTI_TYPE"]) == "01")
                //    {
                //        cbNew.Checked = true;

                //    }
                //loadLegalEntityPageCtrl("Y");
                txtKYCNumber.Text = ds.Tables[0].Rows[0]["FIREFNO_CKYC"].ToString();
                cboTitle.Text = ds.Tables[0].Rows[0]["PREFIX"].ToString();
                txtGivenName.Text = ds.Tables[0].Rows[0]["FNAME"].ToString();
                txtMiddleName.Text = ds.Tables[0].Rows[0]["MNAME"].ToString();
                txtLastName.Text = ds.Tables[0].Rows[0]["LNAME"].ToString();

                cboTitle1.Text = ds.Tables[0].Rows[0]["MAIDEN_PREFIX"].ToString();
                txtGivenName1.Text = ds.Tables[0].Rows[0]["MAIDEN_FNAME"].ToString();
                txtMiddleName1.Text = ds.Tables[0].Rows[0]["MAIDEN_MNAME"].ToString();
                txtLastName1.Text = ds.Tables[0].Rows[0]["MAIDEN_LNAME"].ToString();

                cboTitle2.Text = ds.Tables[0].Rows[0]["FATHER_PREFIX"].ToString();
                txtGivenName2.Text = ds.Tables[0].Rows[0]["FATHER_FNAME"].ToString();
                txtMiddleName2.Text = ds.Tables[0].Rows[0]["FATHER_MNAME"].ToString();
                txtLastName2.Text = ds.Tables[0].Rows[0]["FATHER_LNAME"].ToString();
                cboTitle3.Text = ds.Tables[0].Rows[0]["MOTHER_PREFIX"].ToString();
                txtGivenName3.Text = ds.Tables[0].Rows[0]["MOTHER_FNAME"].ToString();
                txtMiddleName3.Text = ds.Tables[0].Rows[0]["MOTHER_MNAME"].ToString();
                txtLastName3.Text = ds.Tables[0].Rows[0]["MOTHER_LNAME"].ToString();
                txtPanNo.Text = ds.Tables[0].Rows[0]["PAN"].ToString();
                txtDOB.Text = ds.Tables[0].Rows[0]["DATE_OF_BIRTH_DATE_OF_INCORPORATION"].ToString();
                txtPanNoLegal.Text = ds.Tables[0].Rows[0]["PAN"].ToString();
                cboGender.Text = ds.Tables[0].Rows[0]["GENDER"].ToString();
                txtMobile.Text = ds.Tables[0].Rows[0]["MOB_CODE"].ToString();
                txtTelOff2.Text = ds.Tables[0].Rows[0]["OFF_TEL_NUM"].ToString();
                txtTelOff.Text = ds.Tables[0].Rows[0]["OFF_STD_CODE"].ToString();
                txtAddressLine1.Text = ds.Tables[0].Rows[0]["PERM_LINE1"].ToString();
                txtAddressLine2.Text = ds.Tables[0].Rows[0]["PERM_LINE2"].ToString();
                txtAddressLine3.Text = ds.Tables[0].Rows[0]["PERM_LINE3"].ToString();
                txtCity.Text = ds.Tables[0].Rows[0]["PERM_CITY"].ToString();
                ddlDistrict.Text = ds.Tables[0].Rows[0]["PERM_DIST"].ToString();
                ddlPinCode.Text = ds.Tables[0].Rows[0]["PERM_PIN"].ToString();
                ddlState.Text = ds.Tables[0].Rows[0]["PERM_STATE"].ToString();
                txtLocAddLine1.Text = Convert.ToString(ds.Tables[0].Rows[0]["CORRES_LINE1"]);
                txtLocAddLine2.Text = Convert.ToString(ds.Tables[0].Rows[0]["CORRES_LINE2"]);
                txtLocAddLine3.Text = Convert.ToString(ds.Tables[0].Rows[0]["CORRES_LINE3"]);
                txtCity1.Text = Convert.ToString(ds.Tables[0].Rows[0]["CORRES_CITY"]);
                ddlDistrict1.Text = Convert.ToString(ds.Tables[0].Rows[0]["CORRES_DIST"]);
                ddlPinCode1.Text = Convert.ToString(ds.Tables[0].Rows[0]["CORRES_PIN"]);
                ddlState1.Text = Convert.ToString(ds.Tables[0].Rows[0]["CORRES_STATE"]);
                txtCountryCode1.Text = Convert.ToString(ds.Tables[0].Rows[0]["CORRES_COUNTRY"]);
                txtTelOff2.Text = Convert.ToString(ds.Tables[0].Rows[0]["OFF_TEL_NUM"]);
                txtTelRes2.Text = Convert.ToString(ds.Tables[0].Rows[0]["RESI_TEL_NUM"]);
                txtFax2.Text = Convert.ToString(ds.Tables[0].Rows[0]["FAX_NO"]);
                txtMobile2.Text = Convert.ToString(ds.Tables[0].Rows[0]["MOB_NUM"]);
                txtemail.Text = Convert.ToString(ds.Tables[0].Rows[0]["EMAIL"]);
                txtDateKYCver.Text = ds.Tables[0].Rows[0]["DEC_DATE"].ToString();
                txtEmpName.Text = ds.Tables[0].Rows[0]["KYC_NAME"].ToString();
                txtEmpCode.Text = ds.Tables[0].Rows[0]["KYC_EMPCODE"].ToString();
                txtEmpDesignation.Text = ds.Tables[0].Rows[0]["KYC_DESIGNATION"].ToString();
                txtEmpBranch.Text = ds.Tables[0].Rows[0]["KYC_BRANCH"].ToString();
                txtInsName.Text = ds.Tables[0].Rows[0]["ORG_NAME"].ToString();
                txtInsCode.Text = ds.Tables[0].Rows[0]["ORG_CODE"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
                txtPlace.Text = ds.Tables[0].Rows[0]["DEC_PLACE"].ToString();
                txtDate.Text = ds.Tables[0].Rows[0]["DEC_DATE"].ToString();
                ddlDocReceived.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["DOC_SUB"]);
                ddlAccountType.SelectedValue = ds.Tables[0].Rows[0]["ACC_TYPE"].ToString();

                divDetailOfEntity.Visible = false;
                lblProof.Visible = false;
                ddlProofIdentity.Visible = false;
                divRegOffAddr.Visible = false;

                //txtEmpName.Enabled = false;
                //txtEmpCode.Enabled = false;
                //txtEmpDesignation.Enabled = false;
                //txtEmpBranch.Enabled = false;
                //txtInsName.Enabled = false;
                //txtInsCode.Enabled = false;
                //txtDateKYCver.Enabled = false;

                //SaveXMLDataInTbl();
            }
            catch (Exception ex)
            {

                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(Convert.ToInt32(strAppID), "CKYCViewDetails.aspx.cs", "PopulateCndPersonalDtlsforservice", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                objDAL = null;

            }


        }
        private void POPAccountType()
        {
            try
            {
                Hashtable htParam = new Hashtable();
                htParam.Clear();
                htParam.Clear();
                htParam.Add("@Flag", "Y");
                dt = objDAL.GetDataTable("Prc_GetAccountTypeDtls", htParam);
                if (dt.Rows.Count > 0)
                {
                    oCommonUtility.FillDropDown(ddlAccountType, dt, "AccountTypeCode", "AccountTypeDesc");
                }
                dt = null;

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
                    //                    objErr.LogErr(AppId, "CKYCQC.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }

        private void BindGridPOIDNWL()
        {
            DataTable dt = new DataTable();
            Hashtable ht = new Hashtable();
            CheckBox ck = new CheckBox();
            dt.Columns.Add(new System.Data.DataColumn("DOC ID NAME", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("DOC ID NUMBER", typeof(String)));
            //dt.Columns.Add(new System.Data.DataColumn("POI Document", typeof(String)));

            ht.Clear();
            objDAL = new DataAccessLayer("CKYCConnectionString");
            ht.Add("@kycno", kycno);
            dt = objDAL.GetDataTable("PRC_get_TX_kycPerDNWL", ht); 

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
                                ddlProofIdentity.Text = dt.Rows[j]["DocType"].ToString();
                                ddlProofIdentity.Enabled = false;
                                txtPassNo.Text = dt.Rows[j]["DocNumber"].ToString();
                                dt.Rows[j].Delete();
                            }
                        }
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        GridView2.Visible = false;
                    }
                    else
                    {
                        GridView1.Visible = false;
                        GridView2.DataSource = dt;
                        GridView2.DataBind();
                        divPassNo.Visible = false;
                        divPass.Visible = false;
                        txtPassNo.Visible = false;
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


        private void BindGridUPDDOC()
        {
            try
            {
                DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
                Hashtable objht = new Hashtable();
                DataTable objDt = new DataTable();
                objht.Clear();
                objht.Add("@kycno", (kycno == null ? "" : kycno.ToString()));
                objDt = objDAL.GetDataTable("prc_GetDocTypeFORDWNL", objht);
                gvDocDtls.DataSource = objDt;
                gvDocDtls.DataBind();
                if (objDt.Rows.Count > 0)
                {
                    ViewState["DOC_NAME"] = objDt.Rows[0]["DOC_NAME"].ToString();
                    ViewState["DocNo"] = objDt.Rows[0]["DOC_CODE"].ToString();
                    ViewState["docCode"] = objDt.Rows[0]["SHORTCODE"].ToString();
                }
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
                    objErr.LogErr(AppId, "CKYCViewDetails.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "USRMGMT");
                    throw ex;
                }
            }
            finally
            {
            }

        }



        //protected void FillConstDNWLType()
        //{
        //    try
        //    {
        //        Hashtable ht = new Hashtable();
        //        ht.Clear();
        //        ht.Add("@LookupCode", "KConstTyp");
        //        ht.Add("@ParamUsage", "KL");
        //        FillDropdowns("prc_getDDLLookUpData", ht, ddlNatureOfBuss, "CKYCConnectionString", true);
        //    }
        //    catch
        //    {

        //    }
        //}

        //public void FillDropdownview(string strQuery, Hashtable htable, DropDownList ddl, string strDBKey, bool isSelect)
        //{
        //    DataTable dtFill = new DataTable();
        //    objDAL = new DataAccessLayer("CKYCConnectionString");
        //    dtFill.Clear();
        //    dtFill = objDAL.GetDataTable(strQuery, htable, strDBKey);
        //    if (dtFill.Rows.Count > 0)
        //    {
        //        ddl.Items.Clear();
        //        ddl.DataSource = dtFill;
        //        ddl.DataTextField = "ParamDesc1";
        //        ddl.DataValueField = "ParamValue";
        //        ddl.DataBind();
        //    }
        //    if (isSelect)
        //        ddl.Items.Insert(0, new ListItem("Select", ""));
        //}
    }
}