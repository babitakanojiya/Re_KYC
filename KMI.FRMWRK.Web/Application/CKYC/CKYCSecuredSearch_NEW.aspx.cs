using System;
using KMI.FRMWRK.DAL;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KMI.FRMWRK.Multilingual;
using System.Data.SqlClient;
using System.Xml;
using KMI.FRMWRK.Web.Application.CKYC.Common;
using System.Globalization;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class CKYCSecuredSearch_NEW : System.Web.UI.Page
    {
        #region Declarartion
        XmlDocument xmlInPut = new XmlDocument();
        XmlDocument xmlOutPut = new XmlDocument();
        String strout = String.Empty;
        String stroutNew = String.Empty;
        Hashtable htParam = new Hashtable();
        Hashtable hTable = new Hashtable();
        ErrorLog objErr = new ErrorLog();
        DataAccessLayer dataAccessLayer;
        string strHtml = string.Empty;
        string strAppID = string.Empty;
        int AppID;
        string UserID = string.Empty;
        DataTable dt;
        DataTable ctrldt;
        DataAccessLayer objDAL;
        string strSQL = "";
        string strXML = "";
        string strOutput = "";
        string refno = string.Empty;
        DataSet dsResult = new DataSet();

        string strPathDoc = "H:\\CKYC_Vss App\\DWNKYC\\CKYCDWNKYC";
        string strpathserver = string.Empty;
        string strFormat = string.Empty;
        string FileName = string.Empty;
        XmlDocument Objxml = new XmlDocument();
        DataSet xmlDS = new DataSet();
        String Kycno;
        CkycSearch ObjComm = new CkycSearch();


        //ServiceReference1.IService Sc = new ServiceReference1.ServiceClient();

        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AppID"] != null)
            {
                strAppID = Session["AppID"].ToString();
            }

            if (!IsPostBack)
            {
                FillddlPageLoad();
                divIdProof.Visible = false;
                trdgHdr.Visible = false;
                divSearchDetails.Attributes.Add("style", "display:none");
            }
            //divSearchDetails.Visible = false;
            
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
                }
                else if (ddlCertifiecopy.SelectedValue == "A")
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Passport Number";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 60;
                    divAdharD.Visible = false;
                    divAdharD2.Visible = false;
                    div8.Visible = false;
                    //chkSameAsPOI.Checked = false;
                    //ddlProofOfAddress.SelectedIndex = 0;
                    //ddlProofOfAddress.Enabled = true;
                    //txtPassNo.Focus();
                    //txtPassNo.Attributes.Remove("onblur");
                    //txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");
                }
                else if (ddlCertifiecopy.SelectedValue == "B")
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Voter ID Card";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 60;
                    divAdharD.Visible = false;
                    divAdharD2.Visible = false;
                    div8.Visible = false;
                    //if (ddlProofOfAddress.SelectedValue != "PA02")
                    //{
                    //chkSameAsPOI.Checked = false;
                    //ddlProofOfAddress.SelectedIndex = 0;
                    //ddlProofOfAddress.Enabled = true;
                    //}

                    //txtPassNo.Focus();
                    //txtPassNo.Attributes.Remove("onblur");
                }
                else if (ddlCertifiecopy.SelectedValue == "C")
                {

                    divIdProof.Visible = true;
                    lblPassportNo.Text = "PAN Card";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 50;
                    txtPassNo.Focus();
                    divAdharD.Visible = false;
                    divAdharD2.Visible = false;
                    div8.Visible = false;
                    //chkSameAsPOI.Checked = false;
                    //ddlProofOfAddress.SelectedIndex = 0;
                    //ddlProofOfAddress.Enabled = true;
                    // txtPassNo.Attributes.Remove("onblur");
                    //txtPassNo.Attributes.Add("onblur", "return fnValidatePAN(this)");
                }
                else if (ddlCertifiecopy.SelectedValue == "D")
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Driving Licence";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                    //FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    txtPassNo.MaxLength = 50;
                    divAdharD.Visible = false;
                    divAdharD2.Visible = false;
                    div8.Visible = false;
                    //chkSameAsPOI.Checked = false;
                    //ddlProofOfAddress.SelectedIndex = 0;
                    //ddlProofOfAddress.Enabled = true;
                    //txtPassNo.Focus();
                    //txtPassNo.Attributes.Remove("onblur");
                }
                else if (ddlCertifiecopy.SelectedValue == "E")
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Proof of Possession of Aadhaar";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                    txtPassNo.MaxLength = 50;
                    divAdharD.Visible = true;
                    divAdharD2.Visible = true;
                    div8.Visible = true;
                    // txtPassNo.Focus();
                    //chkSameAsPOI.Checked = false;
                    //ddlProofOfAddress.SelectedIndex = 0;
                    //ddlProofOfAddress.Enabled = true;
                    //txtPassNo.Attributes.Remove("onblur");
                    //txtPassNo.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                }
                else if (ddlCertifiecopy.SelectedValue == "F")
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "NREGA Job Card";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                    txtPassNo.MaxLength = 50;
                    divAdharD.Visible = false;
                    divAdharD2.Visible = false;
                    div8.Visible = false;
                    // txtPassNo.Focus();
                    //chkSameAsPOI.Checked = false;
                    //ddlProofOfAddress.SelectedIndex = 0;
                    //ddlProofOfAddress.Enabled = true;
                    //txtPassNo.Attributes.Remove("onblur");
                    //txtPassNo.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                }
                else if (ddlCertifiecopy.SelectedValue == "Z")
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Others";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                    txtPassNo.MaxLength = 50;
                    divAdharD.Visible = false;
                    divAdharD2.Visible = false;
                    div8.Visible = false;
                    // txtPassNo.Focus();
                    //ddlProofOfAddress.SelectedIndex = 0;
                    //ddlProofOfAddress.Enabled = true;
                    //txtPassNo.Attributes.Remove("onblur");
                    //txtPassNo.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                }
                else if (ddlCertifiecopy.SelectedValue == "S01")
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Simplified Measures Account 1";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                    txtPassNo.MaxLength = 50;
                    divAdharD.Visible = false;
                    divAdharD2.Visible = false;
                    div8.Visible = false;
                    // txtPassNo.Focus();
                    //ddlProofOfAddress.SelectedIndex = 0;
                    //ddlProofOfAddress.Enabled = true;
                    //txtPassNo.Attributes.Remove("onblur");
                    //txtPassNo.Attributes.Add("onblur", "return fnValidateAdhar(this)");
                }
                else if (ddlCertifiecopy.SelectedValue == "S02")
                {
                    divIdProof.Visible = true;
                    lblPassportNo.Text = "Simplified Measures Account 2";
                    txtPassNo.Visible = true;
                    //FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                    txtPassNo.MaxLength = 50;
                    divAdharD.Visible = false;
                    divAdharD2.Visible = false;
                    div8.Visible = false;
                    // txtPassNo.Focus();
                    //chkSameAsPOI.Checked = false;
                    //ddlProofOfAddress.SelectedIndex = 0;
                    //ddlProofOfAddress.Enabled = true;
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
                    divAdharD.Visible = false;
                    divAdharD2.Visible = false;
                    div8.Visible = false;
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider..');", true);
                }
            }
        }

        protected void FillddlPageLoad()
        {
            //htParam.Clear();
            //htParam.Add("@LookupCode", "KEntConstTyp");
            //FillDropdowns("prc_getDDLLookUpData", htParam, ddlNatureOfBuss, "CKYCConnectionString", true);          
            htParam.Clear();
            htParam.Add("@LookupCode", "KId");
            FillDropdowns("prc_getDDLLookUpData_New", htParam, ddlCertifiecopy, "CKYCConnectionString", true);
            //ddlCertifiecopy.Items.Insert(0, new ListItem("Select", string.Empty));
            htParam.Clear();

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(5000);

                if (ddlCertifiecopy.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select proof of identity')", true);
                    return;
                }

                if (ddlCertifiecopy.SelectedIndex != 0)
                {
                    if (ddlCertifiecopy.SelectedValue == "A")
                    {
                        if (txtPassNo.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter passport no')", true);
                            return;
                        }
                    }

                    if (ddlCertifiecopy.SelectedValue == "B")
                    {
                        if (txtPassNo.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter voter id card')", true);
                            return;
                        }

                    }
                    if (ddlCertifiecopy.SelectedValue == "C")
                    {
                        if (txtPassNo.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter pan card')", true);
                            return;
                        }

                    }
                    if (ddlCertifiecopy.SelectedValue == "D")
                    {
                        if (txtPassNo.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter driving licence no')", true);
                            return;
                        }
                    }

                    if (ddlCertifiecopy.SelectedValue == "E")
                    {
                        if (txtPassNo.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter UID(Aadhaar)')", true);
                            return;
                        }

                    }
                    if (ddlCertifiecopy.SelectedValue == "F")
                    {
                        if (txtPassNo.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter NREGA job card')", true);
                            return;
                        }
                    }
                    if (ddlCertifiecopy.SelectedValue == "Z")
                    {
                        if (txtPassNo.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter other no of proof of identity')", true);
                            return;
                        }
                    }
                    if (ddlCertifiecopy.SelectedValue == "S01")
                    {
                        if (txtPassNo.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter simplified measures account')", true);
                            return;

                        }
                    }
                    if (ddlCertifiecopy.SelectedValue == "S02")
                    {
                        if (txtPassNo.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter simplified measures account')", true);
                            return;

                        }
                    }

                }
                dsResult.Clear();
                dt = new DataTable();
                objDAL = new DataAccessLayer("CKYCConnectionString");
                htParam.Clear();
                htParam.Add("@IDType", ddlCertifiecopy.SelectedValue);
                htParam.Add("@IDNO", txtPassNo.Text.ToString().Trim());
                //dsResult = dataAccessRecruit.GetDataSetForPrcDBConn("Prc_GetxmlReqforSearch", htParam, "CKYCConnectionString");
                dt = objDAL.GetDataTable("Prc_GetxmlReqforSearch", htParam, "CKYCConnectionString");

                if (dt.Rows.Count > 0)
                {
                    #region Insert into KycVrfySyncLog
                    strXML = dt.Rows[0]["Data"].ToString().Trim();
                    string str = strXML;

                    xmlInPut.LoadXml(str);

                    htParam.Add("@ReqParameter", strXML.Trim());
                    htParam.Add("@CreatedBy", HttpContext.Current.Session["UserId"].ToString().Trim());
                    //dsResult = dataAccessRecruit.GetDataSetForPrcDBConn("Prc_InsKycVrfySyncLog", htParam, "CKYCConnectionString");
                    dsResult = objDAL.GetDataSet("Prc_InsKycVrfySyncLog", htParam, "CKYCConnectionString");
                    refno = dsResult.Tables[0].Rows[0]["Refno"].ToString().Trim();
                    #endregion KycVrfySyncLog

                    string str1 = VerifyKyc(str);
                    xmlDS = ConvertXMLToDataSet(str1);

                    //dsResult.Clear();
                    //string docid = xmlDS.Tables[2].Rows[0]["id_no"].ToString().ToString();
                    //string doctype = xmlDS.Tables[2].Rows[0]["id_type"].ToString().ToString();

                    //htParam.Add("@idtype", doctype);
                    //htParam.Add("@idno", docid);
                    ////dsresult = dataaccessrecruit.getdatasetforprcdbconn("prc_getxmlresponseforsearch", htparam, "ckycconnectionstring");
                    //dsResult = objDAL.GetDataSet("prc_getxmlresponseforsearch", htParam, "CKYCConnectionString");
                    //strOutput = str1;
                    htParam.Clear();

                    #region Update Response into the MailSMSSyncLog table
                    htParam.Clear();
                    dsResult.Clear();
                    htParam.Add("@Refno", refno);
                    htParam.Add("@ResponseParameter", strOutput);
                    //dsResult = dataAccessRecruit.GetDataSetForPrcDBConn("Prc_InsResponseKycVrfySyncLog", htParam, "CKYCConnectionString");
                    dsResult = objDAL.GetDataSet("Prc_InsResponseKycVrfySyncLog", htParam, "CKYCConnectionString");

                    #endregion

                    if (xmlDS.Tables.Count > 3)
                    {
                        ViewState["CKYC_NO"] = xmlDS.Tables[3].Rows[0]["CKYC_NO"].ToString();
                        trdgHdr.Visible = true;
                        //divSearchDetails.Visible = true;
                  
                        //dgDownload.DataSource = xmlDS.Tables[3].Rows[0][0].ToString();
                        //DataTable dt = new DataTable();
                        //xmlDS.Tables[3].Columns.RemoveAt(8);
                        //dt = xmlDS.Tables[3];
                        //dt.Columns.RemoveAt(4);
                        //dt.Columns.Remove("imgType");
                        //dt.Columns.Remove("UPDATED_DATE");

                        dgDownload.DataSource = xmlDS.Tables[3];
                        dgDownload.DataBind();
                        //dgDownload.Columns[5].Visible = false;

                        //ViewState["grid"] = dstable.Tables[0];

                        lblMessage.Visible = false;
                        divSearchDetails.Visible = true;
                        btnExport.Visible = true;
                        divSearchDetails.Attributes.Add("style", "display:block");
                        trdg.Visible = true;
                        dgDownload.Visible = true;
                        //divDob.Visible = true;

                    }
                    else
                    {
                        //btnExport.Visible = false;
                        lblMessage.Visible = true;
                        lblMessage.Text = "O record found";
                    }
                }
                //divloaderqc.Attributes.Add("style", "display:none");
            }


            catch (Exception ex)
            {
                objErr.LogErr(1, "CKYCSecuredSearch_NEW.aspx.cs", "btnSearch_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider...');", true);
            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlCertifiecopy.SelectedIndex = 0;
            txtPassNo.Text = "";
            txtname.Text = "";
            txtmname.Text = "";
            txtlname.Text = "";
            dob.Text = "";
            ddlgender.SelectedIndex = 0;

        }

        public DataSet ConvertXMLToDataSet(string xmlData)
        {
            System.IO.StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet ds = new DataSet();
                stream = new System.IO.StringReader(xmlData);
                // Load the XmlTextReader from the stream
                reader = new XmlTextReader(stream);
                ds.ReadXml(reader);
                return ds;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }

        protected void CommandBtn_Click(Object sender, CommandEventArgs e)
        {
            Session["GridImage"] = e.CommandArgument;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "window.open('../../Application/CKyc_Image.aspx?','_blank', 'toolbar=yes,scrollbars=yes,resizable=yes,top=10,left=10,width=500,height=500')", true);
        }

        public String VerifyKyc(String str)
        {
            //str ="<REQ_ROOT><HEADER><FI_CODE>IN106</FI_CODE><REQUEST_ID>2</REQUEST_ID><VERSION>1.0</VERSION></HEADER><CKYC_INQ><SESSION_KEY>123</SESSION_KEY><PID>44333</PID><PID_DATA><DATE_TIME>12-04-2017 15:25:47</DATE_TIME><ID_NO>KACPH5899V</ID_NO><ID_TYPE>C</ID_TYPE></PID_DATA></CKYC_INQ></REQ_ROOT>";
            if (str != "")
            {
                xmlInPut.LoadXml(str);
                XmlDocument Objxml = new XmlDocument();
                // Objxml.InnerXml = str;

                DataSet xmlDS = new DataSet();


                xmlDS = ConvertXMLToDataSet(str);

                String DocID = xmlDS.Tables[2].Rows[0]["ID_NO"].ToString().Trim();
                String DocType = xmlDS.Tables[2].Rows[0]["ID_TYPE"].ToString().Trim();

                String[] seperator = DocID.Split('|');

                htParam.Clear();

                htParam.Add("@IDType", DocType);
                if (DocType == "E")
                {
                    htParam.Add("@IDNO", DocID);
                    htParam.Add("@Name", txtname.Text + " " + txtmname.Text + " " + txtlname.Text);
                    htParam.Add("@DOB", dob.Text.ToString());
                    htParam.Add("@Gender", ddlgender.SelectedValue);
                }
                else
                {
                    htParam.Add("@IDNO", DocID);
                }

                dt = objDAL.GetDataTable("Prc_GetxmlResponseforSearch", htParam, "CKYCConnectionString");
                //ds = dataAccessRecruit.GetDataSetForPrcDBConn("Prc_GetxmlResponseforSearch", htParam, "CKYC");
                //String[] strout =ds.Tables[0].Rows[0][0].ToString().Trim(); 
                strout = dt.Rows[0][0].ToString().Trim();

            }

            else
            {
                strout = "0 record found ";

            }
            return strout;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            objDAL = new DataAccessLayer("CKYCConnectionString");
            dsResult.Clear();
            htParam.Clear();
            htParam.Add("@IDType", "");
            htParam.Add("@IDNO", ViewState["CKYC_NO"].ToString());
            dsResult = objDAL.GetDataSet("Prc_GetxmlReqforDownload", htParam, "CKYCConnectionString");
            //dt = objDAL.GetDataTable("Prc_GetxmlReqforDownload", htParam, "CKYCConnectionString");

            if (dsResult.Tables.Count > 0)
            {

                #region Insert into KycVrfySyncLog
                strXML = dsResult.Tables[0].Rows[0]["Data"].ToString().Trim();
                String str = strXML;

                htParam.Add("@ReqParameter", strXML.Trim());
                htParam.Add("@CreatedBy", HttpContext.Current.Session["UserId"].ToString().Trim());
                //dsResult = dataAccessRecruit.GetDataSetForPrcDBConn("Prc_InsKycVrfySyncLog", htParam, "CKYCConnectionString");
                dsResult = objDAL.GetDataSet("Prc_InsKycVrfySyncLog", htParam, "CKYCConnectionString");
                refno = dsResult.Tables[0].Rows[0]["Refno"].ToString().Trim();
                #endregion KycVrfySyncLog

                String str2 = Download(str);

                xmlDS = ConvertXMLToDataSet(str2);

                htParam.Clear();

                #region Update Response into the MailSMSSyncLog table
                htParam.Clear();
                dsResult.Clear();
                htParam.Add("@Refno", refno);

                htParam.Add("@ResponseParameter", str2);
                //dsResult = dataAccessRecruit.GetDataSetForPrcDBConn("Prc_InsResponseKycVrfySyncLog", htParam, "CKYCConnectionString");
                dsResult = objDAL.GetDataSet("Prc_InsResponseKycVrfySyncLog", htParam, "CKYCConnectionString");
                #endregion


                Session["sessDataSet"] = xmlDS;
                string st = "q";
                if (st == "q")
                {
                    Response.Redirect("CKYCView.aspx?Status=Service&refno=" + ViewState["CKYC_NO"].ToString() + "", false);
                }
                else
                {
                    DataTable ds = new DataTable();
                    ds = xmlDS.Tables[4];
                    DataTable ds1 = new DataTable();
                    ds1 = xmlDS.Tables[6];
                    DataTable ds2 = new DataTable();
                    ds2 = xmlDS.Tables[8];

                    ds = MergeColumns(ds, ds1);
                    //ds = MergeColumns(ds, ds2);
                    if (xmlDS.Tables.Count > 3)
                    {
                        GridDownloadResponse.DataSource = ds;
                        GridDownloadResponse.DataBind();
                        Dwnld.Visible = true;
                        trdg.Visible = true;
                        div1.Attributes.Add("style", "display:block");
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "O record found";
                    }
                }
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "O record found";
            }
        }

        public String Download(String str)
        {
            if (str != "")
            {
                xmlInPut.LoadXml(str);
                XmlDocument Objxml = new XmlDocument();
                // Objxml.InnerXml = str;
                DataSet xmlDS = new DataSet();

                xmlDS = ConvertXMLToDataSet(str);

                String DocID = xmlDS.Tables[2].Rows[0]["ID_NO"].ToString().Trim();
                String DocType = xmlDS.Tables[2].Rows[0]["ID_TYPE"].ToString().Trim();

                dsResult.Clear();
                htParam.Clear();
                dt = new DataTable();
                objDAL = new DataAccessLayer("CKYCConnectionString");

                DataTable ds = new DataTable();
                htParam.Add("@IDType", DocType);
                htParam.Add("@IDNO", DocID);

                ds = objDAL.GetDataTable("Prc_GetxmlResponseforDownload", htParam, "CKYCConnectionString");

                strout = ds.Rows[0][0].ToString().Trim();
            }

            else
            {
                strout = "0 record found ";

            }
            return strout;
        }

        public DataTable MergeColumns(DataTable dt1, DataTable dt2)
        {
            DataTable result = new DataTable();
            foreach (DataColumn dc in dt1.Columns)
            {
                result.Columns.Add(new DataColumn(dc.ColumnName, dc.DataType));
            }
            foreach (DataColumn dc in dt2.Columns)
            {
                result.Columns.Add(new DataColumn(dc.ColumnName, dc.DataType));
            }
            for (int i = 0; i < Math.Max(dt1.Rows.Count, dt2.Rows.Count); i++)
            {
                DataRow dr = result.NewRow();
                if (i < dt1.Rows.Count)
                {
                    for (int c = 0; c < dt1.Columns.Count; c++)
                    {
                        dr[c] = dt1.Rows[i][c];
                    }
                }
                if (i < dt2.Rows.Count)
                {
                    for (int c = 0; c < dt2.Columns.Count; c++)
                    {
                        dr[dt1.Columns.Count + c] = dt2.Rows[i][c];
                    }
                }
                result.Rows.Add(dr);
            }
            return result;
        }

    }

}