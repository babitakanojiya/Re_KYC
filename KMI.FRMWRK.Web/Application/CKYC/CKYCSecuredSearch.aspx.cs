using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using INSCL.App_Code;
using INSCL.DAL;
using System.Data.SqlClient;
using Insc.Common.Multilingual;
using System.Text;
using DataAccessClassDAL;
using Microsoft.SqlServer;
using System.Xml;
using System.Net;
using iTextSharp.text;
using System.IO.Compression;
using System.Drawing.Imaging;
using Ionic.Zip;
using System.IO;



public partial class Application_Isys_Recruit_CKYCSecuredSearch : System.Web.UI.Page
{
    private static Microsoft.Office.Interop.Excel.Workbook mWorkBook;
    private static Microsoft.Office.Interop.Excel.Sheets mWorkSheets;
    private static Microsoft.Office.Interop.Excel.Worksheet mWSheet1;
    private static Microsoft.Office.Interop.Excel.Application oXL;


    #region Page Declaration
    XmlDocument xmlInPut = new XmlDocument();
    XmlDocument xmlOutPut = new XmlDocument();

    DataSet dsExport = new DataSet();

    SqlConnection sqlCon;
    string ConnectionString;
    SqlCommand cmd;
    string strSQL = "";
    string strXML = "";
    string strOutput = "";
    string refno = string.Empty;

    private DataAccessClass dataAccessRecruit = new DataAccessClass();
    ErrLog objErr = new ErrLog();
    Hashtable htParam = new Hashtable();
    DataTable dtResult = new DataTable();
    DataSet dsResult = new DataSet();
    String result;
    DataSet dsRes = new DataSet();

    string strPathDoc = "H:\\CKYC_Vss App\\DWNKYC\\CKYCDWNKYC";
    string strpathserver = string.Empty;
    string strFormat = string.Empty;
    string FileName = string.Empty;
    XmlDocument Objxml = new XmlDocument();
    DataSet xmlDS = new DataSet();
    String Kycno;

    // ServiceReference1.ServiceClient Sc = new ServiceReference1.ServiceClient();

    ServiceReference1.IService Sc = new ServiceReference1.ServiceClient();
    //  CKYCServiceProxy.ServiceClient Sc = new CKYCServiceProxy.ServiceClient();
    // servicere




    private INSCL.App_Code.CommonUtility oCommonUtility = new INSCL.App_Code.CommonUtility();

    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {


            if (Session["UserLangNum"] == null || Session["CarrierCode"] == null || Session["LanguageCode"] == null)
            {
                Response.Redirect("~/ErrorSession.aspx");
            }
            Session["CarrierCode"] = '2';


            if (!IsPostBack)
            {
                divSearchDetails.Visible = false;
                btnExport.Visible = false;
                PopulateProofIdentiy();
                divIdProof.Visible = false;
            }

        }
        catch (Exception ex)
        {
            string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
            System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
            string sRet = oInfo.Name;
            System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
            String LogClassName = method.ReflectedType.Name;
            objErr.LogErr("I-Sys Suite", sRet, method.Name.ToString(), ex.Message.ToString(), HttpContext.Current.Session["UserID"].ToString().Trim());

            throw (ex);
        }
    }
    #endregion

    private void PopulateProofIdentiy()
    {
        try
        {
            oCommonUtility.GetCKYC(ddlProofIdentity, "KId");
            ddlProofIdentity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", ""));

        }

        catch (Exception ex)
        {
            string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
            System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
            string sRet = oInfo.Name;
            System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
            String LogClassName = method.ReflectedType.Name;
            objErr.LogErr("ISYS-RGIC", sRet, method.Name.ToString(), ex.Message.ToString(), HttpContext.Current.Session["UserID"].ToString().Trim());
        }
    }

    #region DROPDOWN 'ddlProofIdentity_SelectedIndexChanged EVENT
    protected void ddlProofIdentity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            txtPassNo.Visible = false;
            txtPassNo.Text = string.Empty;
            btnExport.Visible = false;
            divSearchDetails.Visible = false;
            lblMessage.Visible = false;


            if (ddlProofIdentity.SelectedValue == "Select")
            {
                divIdProof.Visible = false;

            }

            else if (ddlProofIdentity.SelectedValue == "A")
            {
                divIdProof.Visible = true;
                lblPassportNo.Text = "Passport Number";

                txtPassNo.Visible = true;

                FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                txtPassNo.MaxLength = 15;
                txtPassNo.Attributes.Remove("onblur");
                txtPassNo.Attributes.Add("onblur", "return ValidationPassport(this)");

            }
            else if (ddlProofIdentity.SelectedValue == "B")
            {
                divIdProof.Visible = true;
                lblPassportNo.Text = "Voter ID Card";

                //txtPassNo.Text = ViewState["strVoterId"].ToString();
                txtPassNo.Visible = true;

                FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                txtPassNo.MaxLength = 15;
                txtPassNo.Attributes.Remove("onblur");


            }
            else if (ddlProofIdentity.SelectedValue == "C")
            {

                divIdProof.Visible = true;
                lblPassportNo.Text = "PAN Card";

                txtPassNo.Visible = true;

                FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                txtPassNo.MaxLength = 10;
                txtPassNo.Attributes.Remove("onblur");
                txtPassNo.Attributes.Add("onblur", "return fnValidatePAN(this)");
            }
            else if (ddlProofIdentity.SelectedValue == "D")
            {

                divIdProof.Visible = true;
                lblPassportNo.Text = "Driving Licence";

                txtPassNo.Visible = true;

                //txtPassNo.Text = ViewState["strDrivLic"].ToString();
                //txtPassExpDate.Text = ViewState["strDrivLicDate"].ToString();

                FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                txtPassNo.MaxLength = 15;
                txtPassNo.Attributes.Remove("onblur");

            }
            else if (ddlProofIdentity.SelectedValue == "E")
            {
                divIdProof.Visible = true;
                lblPassportNo.Text = "UID(Aadhaar)";

                txtPassNo.Visible = true;

                FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                txtPassNo.MaxLength = 12;
                txtPassNo.Text = "";
                txtPassNo.Attributes.Remove("onblur");
                txtPassNo.Attributes.Add("onblur", "return fnValidateAdhar(this)");


            }
            else if (ddlProofIdentity.SelectedValue == "F")
            {

                divIdProof.Visible = true;
                lblPassportNo.Text = "NREGA Job Card";

                txtPassNo.Visible = true;



                FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                txtPassNo.MaxLength = 15;
                txtPassNo.Attributes.Remove("onblur");
            }
            else if (ddlProofIdentity.SelectedValue == "Z")
            {

                divIdProof.Visible = true;
                lblPassportNo.Text = "Others";

                txtPassNo.Visible = true;

                FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                txtPassNo.MaxLength = 15;
                txtPassNo.Attributes.Remove("onblur");

            }
            else if (ddlProofIdentity.SelectedValue == "S01")
            {

                divIdProof.Visible = true;
                lblPassportNo.Text = "Simplified Measures Account";

                txtPassNo.Visible = true;

                FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                txtPassNo.MaxLength = 15;
                txtPassNo.Attributes.Remove("onblur");


            }
            else
            {
                divIdProof.Visible = true;
                lblPassportNo.Text = "Simplified Measures Account";

                txtPassNo.Visible = true;

                FilteredTextBoxExtender12.FilterType = AjaxControlToolkit.FilterTypes.Custom;
                FilteredTextBoxExtender12.ValidChars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                txtPassNo.MaxLength = 15;
                txtPassNo.Attributes.Remove("onblur");


            }
        }
        catch (Exception ex)
        {
            string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
            System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
            string sRet = oInfo.Name;
            System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
            String LogClassName = method.ReflectedType.Name;
            objErr.LogErr("ISYS-RGIC", sRet, method.Name.ToString(), ex.Message.ToString(), HttpContext.Current.Session["UserID"].ToString().Trim());
        }


    }
    # endregion


    #region Set Excel File
    protected void SetExcelFile()
    {
        string attachment = "attachment; filename=Sheet1.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/Microsoft Excel 97- Excel 2008 & 5.0/95 Workbook";
    }
    #endregion




    protected void btnpopcancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CKYCSecuredSearch.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(5000);

            if (ddlProofIdentity.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select proof of identity')", true);

                return;

            }

            if (ddlProofIdentity.SelectedIndex != 0)
            {
                if (ddlProofIdentity.SelectedIndex == 1)
                {
                    if (txtPassNo.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter passport no')", true);
                        return;

                    }


                }

                if (ddlProofIdentity.SelectedIndex == 2)
                {
                    if (txtPassNo.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter voter id card')", true);
                        return;

                    }

                }
                if (ddlProofIdentity.SelectedIndex == 3)
                {
                    if (txtPassNo.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter pan card')", true);
                        return;

                    }

                }
                if (ddlProofIdentity.SelectedIndex == 4)
                {
                    if (txtPassNo.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter driving licence no')", true);
                        return;



                    }



                }

                if (ddlProofIdentity.SelectedIndex == 5)
                {
                    if (txtPassNo.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter UID(Aadhaar)')", true);
                        return;


                    }

                }
                if (ddlProofIdentity.SelectedIndex == 6)
                {
                    if (txtPassNo.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter NREGA job card')", true);
                        return;

                    }

                }
                if (ddlProofIdentity.SelectedIndex == 7)
                {
                    if (txtPassNo.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter other no of proof of identity')", true);
                        return;

                    }

                }
                if (ddlProofIdentity.SelectedIndex == 8)
                {
                    if (txtPassNo.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter simplified measures account')", true);
                        return;

                    }

                }

            }
            dsResult.Clear();
            htParam.Clear();
            htParam.Add("@IDType", ddlProofIdentity.SelectedValue);
            htParam.Add("@IDNO", txtPassNo.Text.ToString().Trim());
            dsResult = dataAccessRecruit.GetDataSetForPrcDBConn("Prc_GetxmlReqforSearch", htParam, "CKYCConnectionString");

            if (dsResult.Tables.Count > 0)
            {
                #region Insert into KycVrfySyncLog
                strXML = dsResult.Tables[0].Rows[0]["Data"].ToString().Trim();
                string str = strXML;




                // xmlInPut.LoadXml(str);

                htParam.Add("@ReqParameter", strXML.Trim());
                htParam.Add("@CreatedBy", HttpContext.Current.Session["UserId"].ToString().Trim());
                dsResult = dataAccessRecruit.GetDataSetForPrcDBConn("Prc_InsKycVrfySyncLog", htParam, "CKYCConnectionString");
                refno = dsResult.Tables[0].Rows[0]["Refno"].ToString().Trim();
                #endregion KycVrfySyncLog
                #region Send the XML as a HTTP POST Request to the Webservice URL
                //Uri uri = new Uri(strURL, UriKind.Absolute);
                //  strOutput = postXMLData(uri, strXML);

                // Sc.VerifyKyc(str);
                #endregion


                string str1 = Sc.VerifyKyc(str);

                xmlDS = ConvertXMLToDataSet(str1);




                ////dsResult.Clear();
                ////String DocID = xmlDS.Tables[2].Rows[0]["ID_NO"].ToString().Trim();
                ////String DocType = xmlDS.Tables[2].Rows[0]["ID_TYPE"].ToString().Trim();
                ////htParam.Clear();
                ////htParam.Add("@IDType", DocType);
                ////htParam.Add("@IDNO", DocID);
                ////dsResult = dataAccessRecruit.GetDataSetForPrcDBConn("Prc_GetxmlResponseforSearch", htParam, "CKYCConnectionString");
                // strOutput = str1;
                htParam.Clear();


                #region Update Response into the MailSMSSyncLog table
                htParam.Clear();
                dsResult.Clear();
                htParam.Add("@Refno", refno);

                htParam.Add("@ResponseParameter", strOutput);
                dsResult = dataAccessRecruit.GetDataSetForPrcDBConn("Prc_InsResponseKycVrfySyncLog", htParam, "CKYCConnectionString");


                #endregion

                if (xmlDS.Tables.Count > 3)
                {
                    ViewState["CKYC_NO"] = xmlDS.Tables[3].Rows[0]["CKYC_NO"].ToString();
                    trdgHdr.Visible = true;
                    trdg.Visible = true;
                    dgDownload.Visible = true;
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
                    divDob.Visible = true;

                }
                else
                {
                    btnExport.Visible = false;
                    lblMessage.Visible = true;
                    lblMessage.Text = "O record found";
                }
            }
            divloaderqc.Attributes.Add("style", "display:none");
        }


        catch (Exception ex)
        {
            string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
            System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
            string sRet = oInfo.Name;
            System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
            String LogClassName = method.ReflectedType.Name;
            objErr.LogErr("I-Sys Suite", sRet, method.Name.ToString(), ex.Message.ToString(), HttpContext.Current.Session["UserId"].ToString().Trim());
            throw (ex);
        }

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

    //Added by usha on 01.10.016
    //#region postXMLData function to send the XML as a HTTP POST Request to the Webservice URL
    //public string postXMLData(Uri destinationUrl, string requestXml)
    //{
    //    string responseStr = "";
    //    try
    //    {
    //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl + requestXml);
    //        byte[] bytes;
    //        ////bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
    //        request.ContentType = "text/xml; encoding='utf-8'";
    //        ////request.ContentLength = bytes.Length;
    //        request.Method = "POST";
    //        Stream requestStream = request.GetRequestStream();
    //        /////requestStream.Write(bytes, 0, bytes.Length);
    //        requestStream.Close();
    //        HttpWebResponse response;
    //        response = (HttpWebResponse)request.GetResponse();
    //        if (response.StatusCode == HttpStatusCode.OK)
    //        {
    //            Stream responseStream = response.GetResponseStream();
    //            responseStr = new StreamReader(responseStream).ReadToEnd();
    //            ////return responseStr;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
    //        System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
    //        string sRet = oInfo.Name;
    //        System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
    //        String LogClassName = method.ReflectedType.Name;
    //        objErr.LogErr("I-Sys Suite", sRet, method.Name.ToString(), ex.Message.ToString(), HttpContext.Current.Session["UserId"].ToString().Trim());
    //        throw (ex);
    //    }
    //    return responseStr;
    //}
    //#endregion

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkNormal.Checked == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select DOB check box')", true);
                return;
            }

            if (txtDob.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select DOB')", true);

                return;

            }
            dsResult.Clear();
            htParam.Clear();
            htParam.Add("@IDType", txtDob.Text.ToString().Trim());
            htParam.Add("@IDNO", ViewState["CKYC_NO"].ToString());
            dsResult = dataAccessRecruit.GetDataSetForPrcDBConn("Prc_GetxmlReqforDownload", htParam, "CKYCConnectionString");

            if (dsResult.Tables.Count > 0)
            {
                #region Insert into KycVrfySyncLog
                strXML = dsResult.Tables[0].Rows[0]["Data"].ToString().Trim();
                String str = strXML;




                htParam.Add("@ReqParameter", strXML.Trim());
                htParam.Add("@CreatedBy", HttpContext.Current.Session["UserId"].ToString().Trim());
                dsResult = dataAccessRecruit.GetDataSetForPrcDBConn("Prc_InsKycVrfySyncLog", htParam, "CKYCConnectionString");
                refno = dsResult.Tables[0].Rows[0]["Refno"].ToString().Trim();
                #endregion KycVrfySyncLog

                String str2 = Sc.Download(str);


                xmlDS = ConvertXMLToDataSet(str2);

                htParam.Clear();

                #region Update Response into the MailSMSSyncLog table
                htParam.Clear();
                dsResult.Clear();
                htParam.Add("@Refno", refno);

                htParam.Add("@ResponseParameter", str2);
                dsResult = dataAccessRecruit.GetDataSetForPrcDBConn("Prc_InsResponseKycVrfySyncLog", htParam, "CKYCConnectionString");
                #endregion


                Session["sessDataSet"] = xmlDS;
                string st = "q";
                if (st == "q")
                {

                    Response.Redirect("CKYCView.aspx?Status=Service", false);

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



            //htParam.Clear();
            //dsResult.Clear();
            //htParam.Add("@IDType", ddlProofIdentity.SelectedValue);
            //htParam.Add("@IDNO", txtPassNo.Text.ToString().Trim());
            // dsResult = dataAccessRecruit.GetDataSetForPrcDBConn("Prc_GetxmlResponseforDownload_usa", htParam, "CKYCConnectionString");
            //strOutput = dsResult.Tables[0].Rows[0][0].ToString().Trim();
            //  xmlDS=    ConvertXMLToDataSet1(strOutput);



            //ds.Merge(ds1);

            //xmlDS.Tables[4].Merge(xmlDS.Tables[6]);//, true, MissingSchemaAction.Add);




        }
        catch (Exception ex)
        {
            string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
            System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
            string sRet = oInfo.Name;
            System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
            String LogClassName = method.ReflectedType.Name;
            objErr.LogErr("I-Sys Suite", sRet, method.Name.ToString(), ex.Message.ToString(), HttpContext.Current.Session["UserID"].ToString().Trim());
            throw (ex);
        }
        finally
        {

        }
    }

    public DataSet ConvertXMLToDataSet1(string xmlData)
    {
        StringReader stream = null;
        XmlTextReader reader = null;
        try
        {
            DataSet DSxml = new DataSet();
            stream = new StringReader(xmlData);

            reader = new XmlTextReader(stream);
            DSxml.ReadXml(reader);
            return DSxml;
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