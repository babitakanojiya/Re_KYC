
#region namespace
using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using KMI.FRMWRK.DAL;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;
using KMI.FRMWRK.Multilingual;
#endregion namespace

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class CKYCExcelUpload : System.Web.UI.Page
    {
        #region Declare Variable
        private MultilingualManager olng;
        private string strUserLang;
        Hashtable htParam = new Hashtable();
        DataAccessLayer objDAL = new DataAccessLayer("UpdDwnldConnectionString");
        ErrorLog objErr = new ErrorLog();
        private string strconn = ConfigurationManager.ConnectionStrings["UpdDwnldConnectionString"].ConnectionString.ToString();
        private string destDir = string.Empty;
        private string fileName = string.Empty;
        private string destPath = string.Empty;
        string batchid = string.Empty;
        string strHtml = string.Empty;
        string strProcessFlag = string.Empty;
        string strError = string.Empty;
        string strTblName = string.Empty;
        string strTblName_Rltd = string.Empty;
        string strTblName_Doc = string.Empty;
        public string custtype; //added by rutuja for custtype
        public string StrCustType;  //added by rutuja for custtype

        string strTblName_Update = string.Empty;
        string strTblName_Rltd_Update = string.Empty;
        string strTblName_Doc_Update = string.Empty;

        string strUploadFile = string.Empty;
        string strCheck = string.Empty;
        string strUserId = string.Empty;
        int AppId;
        DataTable objDt = new DataTable();
        DataSet dsresult = new DataSet();
        DataSet dsresnw = new DataSet();
        Guid obj = Guid.NewGuid();
        #endregion

        #region Page_Load 
        protected void Page_Load(object sender, EventArgs e)
        {
            olng = new MultilingualManager("DefaultConn", "CKYCUpload.aspx", Session["UserLangNum"].ToString());
            if (!IsPostBack)
            {
                PopulateCasteCat();
                InitializeControl();
            }

        }
        #endregion

        #region button upload
        protected void btn_Upload_Click(object sender, EventArgs e)
        {
            if (ddlUpload.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select type to upload file.');", true);
            }
            if (ddlUpload.SelectedItem.Value.ToString() != "Select")
            {
                try
                {
                    //Added by tushar for Bulk Upload
                    //if (ddlUpload.SelectedValue != "CKYCBulkUpdate")
                    //{
                        DataSet ds = new DataSet();
                        DataSet DsNEw = new DataSet();
                        DataTable RegistrationTable = new DataTable();
                        DataTable RelatedPersonTable = new DataTable();
                        DataTable POA_POITable = new DataTable();
                        DataTable ProcessTable = new DataTable();
                        string strdocType = string.Empty;

                        btnUpldFrmt.Enabled = false;
                        if (fileuploading.HasFile)
                        {
                            System.Threading.Thread.Sleep(2000);
                            string excelExtention = string.Empty;
                            excelExtention = System.IO.Path.GetExtension(fileuploading.PostedFile.FileName).ToLower();
                            if (excelExtention == ".txt" || excelExtention == ".csv" || excelExtention == ".xlsx" || excelExtention == ".xls")
                            {
                                htParam.Clear();
                                ds.Clear();
                                htParam.Add("@flag", "1");
                                htParam.Add("@Seqno", "1");
                                DataAccessLayer objCkyc = new DataAccessLayer("CKYCConnectionString");
                                ds = objCkyc.GetDataSet("Prc_getdata", htParam);
                                destDir = @"C:\HostedApplications\CKYC\Demo\CKYC_DEMO_DEV_APP\CKYC\File\ExcelFile\";
                                //destDir = "E:\\KMIFTP\\CkycKmi\\Request\\";
                                strUploadFile = System.IO.Path.GetFileName(fileuploading.PostedFile.FileName + "_" + System.DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + ".txt");
                                // destDir = @Server.MapPath("./AgnUpload");
                                fileName = System.IO.Path.GetFileName(fileuploading.PostedFile.FileName + " " + System.DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"));
                                destPath = System.IO.Path.Combine(destDir, strUploadFile);
                                fileuploading.PostedFile.SaveAs(destPath);
                                #region
                                if (fileName.Contains("Registration") == true)
                                {
                                    strdocType = "CKYCFIReg";
                                }
                                else if (fileName.Contains("Search") == true)
                                {
                                    strdocType = "CKYCBulkSrch";
                                }
                                else if (fileName.Contains("Download") == true)
                                {
                                    strdocType = "CKYCBulkDwnld";
                                }
                                else if (fileName.Contains("Update") == true)
                                {
                                    strdocType = "CKYCBulkUpdate";
                                }

                                if (strdocType != "CKYCBulkUpdate")
                                {
                                    //get temp table name
                                    htParam.Clear();
                                    htParam.Add("@DocType", strdocType);
                                    htParam.Add("@Flag", "1");
                                    strTblName = objDAL.ExecuteNonQuery("Prc_GetTempHistTbl", "@TblName", htParam);

                                    //get temp table name for Related Person (TX_TblTmpCKYCRelatedPerson)
                                    htParam.Clear();
                                    htParam.Add("@DocType", strdocType);
                                    htParam.Add("@Flag", "3");
                                    strTblName_Rltd = objDAL.ExecuteNonQuery("Prc_GetTempHistTbl", "@TblName", htParam);

                                    //get temp table name for POA_POI (TX_TblTmpCKYCRelatedPerson)
                                    htParam.Clear();
                                    htParam.Add("@DocType", strdocType);
                                    htParam.Add("@Flag", "4");
                                    strTblName_Doc = objDAL.ExecuteNonQuery("Prc_GetTempHistTbl", "@TblName", htParam);
                                }
                                else if (strdocType == "CKYCBulkUpdate")
                                {
                                    //get temp table name
                                    htParam.Clear();
                                    htParam.Add("@DocType", strdocType);
                                    htParam.Add("@Flag", "5");
                                    strTblName = objDAL.ExecuteNonQuery("Prc_GetTempHistTbl", "@TblName", htParam);

                                    //get temp table name for Related Person (TX_TblTmpCKYCRelatedPerson)
                                    htParam.Clear();
                                    htParam.Add("@DocType", strdocType);
                                    htParam.Add("@Flag", "6");
                                    strTblName_Rltd = objDAL.ExecuteNonQuery("Prc_GetTempHistTbl", "@TblName", htParam);

                                    //get temp table name for POA_POI (TX_TblTmpCKYCRelatedPerson)
                                    htParam.Clear();
                                    htParam.Add("@DocType", strdocType);
                                    htParam.Add("@Flag", "7");
                                    strTblName_Doc = objDAL.ExecuteNonQuery("Prc_GetTempHistTbl", "@TblName", htParam);
                                }
                                // get batch id
                                htParam.Clear();
                                htParam.Add("@DocType", strdocType);
                                batchid = objDAL.ExecuteNonQuery("Prc_UpdtBatchId", "@Batch", htParam);

                                //insert into temp table
                                htParam.Clear();
                                htParam.Add("@Path", destDir + strUploadFile);
                                htParam.Add("@UserId", strUserId);
                                htParam.Add("@Batchid", batchid);

                                xlsxFormat();

                                if (dsresult.Tables["Registration"] != null)
                                {
                                    ProcessTable = dsresult.Tables["Registration"];
                                }
                                else if (dsresult.Tables["Search"] != null)
                                {
                                    ProcessTable = dsresult.Tables["Search"];

                                    //DataTable dt = new DataTable();
                                    //dt = dsresult.Tables["Search"].Clone();

                                    //DataTable dts = new DataTable();
                                    //dts = dsresult.Tables["Search"].Copy();
                                }
                                else if (dsresult.Tables["Download"] != null)
                                {
                                    ProcessTable = dsresult.Tables["Download"];
                                }
                                else if (dsresult.Tables["Update"] != null)
                                {
                                    ProcessTable = dsresult.Tables["Update"];
                                }
                                //below added by rutuja for custtype differentiate
                                if (custtype == "01")
                                {
                                    StrCustType = "02";
                                }
                                else if (custtype == "02")
                                {
                                    StrCustType = "01";
                                }
                                //string strCustType=string.Empty;
                                DataRow[] foundCustType = ProcessTable.Select("CUST_TYPE = '" + StrCustType + "'");
                                if (foundCustType.Length > 0)
                                {
                                    htParam.Clear();
                                    //htParam.Add("@Path", destDir + strUploadFile);
                                    //htParam.Add("@UserId", strUserId);
                                    htParam.Add("@Batch", batchid);
                                    htParam.Add("@DocType", strdocType);
                                    htParam.Add("@CustType", custtype);
                                    objDAL.GetDataSet("Prc_ErrorEntryForCustType", htParam);
                                }
                                //ended by rutuja
                                else
                                {

                                    //ProcessTable.Columns.Add("RELATED_FIREFNO", typeof(string));Commented by tushar 
                                    ProcessTable.Columns.Add("MODE", typeof(string));
                                    ProcessTable.Columns.Add("Status", typeof(string));
                                    ProcessTable.Columns.Add("ErrorMesg", typeof(string));
                                    ProcessTable.Columns.Add("createdBy", typeof(string));
                                    ProcessTable.Columns.Add("createdDate", typeof(string));
                                    ProcessTable.Columns.Add("updatedBy", typeof(string));
                                    ProcessTable.Columns.Add("updatedDate", typeof(string));
                                    ProcessTable.Columns.Add("BatchID", typeof(string));
                                    ProcessTable.Columns.Add("USERID", typeof(string));
                                    ProcessTable.Columns.Add("ProcessStatus", typeof(string));


                                    for (int i = 0; i < ProcessTable.Rows.Count; i++)
                                    {
                                        ProcessTable.Rows[i]["BatchID"] = batchid;
                                        ProcessTable.Rows[i]["USERID"] = "MAKER";
                                        ProcessTable.Rows[i]["createdBy"] = "MAKER";
                                        ProcessTable.Rows[i]["MODE"] = "SFTP";
                                        //ProcessTable.Rows[i]["ProcessStatus"] = "Uploaded"; Commented by tushar 
                                    }

                                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(strconn))
                                    {
                                        bulkCopy.DestinationTableName = strTblName;
                                        bulkCopy.WriteToServer(ProcessTable);
                                    }

                                    if ((strdocType == "CKYCFIReg") || (strdocType == "CKYCBulkUpdate"))
                                    {
                                        if (dsresult.Tables["RelatedPerson"] != null)
                                        {
                                            RelatedPersonTable = dsresult.Tables["RelatedPerson"];
                                            RelatedPersonTable.Columns.Add("MODE", typeof(string));
                                            RelatedPersonTable.Columns.Add("STATUS", typeof(string));
                                            RelatedPersonTable.Columns.Add("CREATEDBY", typeof(string));
                                            RelatedPersonTable.Columns.Add("CREATEDDATE", typeof(string));
                                            RelatedPersonTable.Columns.Add("BATCHID", typeof(string));
                                            RelatedPersonTable.Columns.Add("ProcessStatus", typeof(string));

                                            for (int i = 0; i < RelatedPersonTable.Rows.Count; i++)
                                            {
                                                RelatedPersonTable.Rows[i]["MODE"] = "SFTP";
                                                RelatedPersonTable.Rows[i]["STATUS"] = "";
                                                RelatedPersonTable.Rows[i]["CREATEDBY"] = "MAKER";
                                                RelatedPersonTable.Rows[i]["BATCHID"] = batchid;
                                            }

                                            //Related Person Save Detail Code Added by tushar
                                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(strconn))
                                            {
                                                bulkCopy.DestinationTableName = strTblName_Rltd;
                                                bulkCopy.WriteToServer(RelatedPersonTable);
                                            }
                                        }

                                        if (dsresult.Tables["POI_POA"] != null)
                                        {
                                            POA_POITable = dsresult.Tables["POI_POA"];
                                            POA_POITable.Columns.Add("BATCHID", typeof(string));
                                            POA_POITable.Columns.Add("ISAVAILABLE", typeof(string));

                                            for (int i = 0; i < POA_POITable.Rows.Count; i++)
                                            {
                                                POA_POITable.Rows[i]["BATCHID"] = batchid;
                                                POA_POITable.Rows[i]["ISAVAILABLE"] = "";
                                            }


                                            //POA_POI Save Detail Code Added by tushar
                                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(strconn))
                                            {
                                                bulkCopy.DestinationTableName = strTblName_Doc;
                                                bulkCopy.WriteToServer(POA_POITable);
                                                //ViewState["dsresnw_Doc"] = POA_POITable.Copy();
                                            }
                                        }
                                    }
                                    #endregion

                                    hdnBatchid.Value = batchid;
                                    htParam.Clear();
                                    htParam.Add("@UserId", Session["UserId"].ToString().Trim());
                                    htParam.Add("@RecrdCnt", dsresult.Tables[0].Rows.Count.ToString());
                                    htParam.Add("@BatchID", hdnBatchid.Value.ToString());
                                    htParam.Add("@Action", "Upd");
                                    htParam.Add("@DocType", ddlUpload.SelectedValue.ToString().Trim());
                                    objDAL.GetDataSet("prc_InsUpldDwnldLog", htParam);

                                    //daksh
                                    htParam.Clear();
                                    htParam.Add("@BatchID", hdnBatchid.Value.ToString());
                                    htParam.Add("@DocType", ddlUpload.SelectedValue.ToString().Trim());
                                    htParam.Add("@FileName", fileuploading.FileName);
                                    htParam.Add("@ProcessSeq", "Uploaded");
                                    htParam.Add("@ProcName", null);
                                    //htParam.Add("@Status", "Uploaded");
                                    htParam.Add("@Status", "WIP");
                                    htParam.Add("@NoofRecords", dsresult.Tables[0].Rows.Count.ToString());
                                    htParam.Add("@SuccessCount", "0");
                                    htParam.Add("@FailCount", "0");
                                    htParam.Add("@CreatedBy", Session["UserID"].ToString());
                                    //htParam.Add("CreateDtim",);
                                    htParam.Add("@UpdatedBy", Session["UserID"].ToString());
                                    //htParam.Add("@Up")
                                    objDAL.GetDataSet("prc_InsBatchStatus", htParam);

                                    lbl.Text = "File uploaded successfully. <br />For future reference batchid is " + hdnBatchid.Value + ". <br /> Please check below mention path. <br />Legal Entity >> Customer Registration >> BULK UPLOAD STATUS";

                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Incorrect file format');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please browse valid file to upload');", true);
                        }
                    //}

                    dsresult.Clear();
                }
                catch (Exception ex)
                {
                    if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                    {
                        Response.Redirect("~/ErrorSession.aspx");
                    }
                    else
                    {
                        objErr.LogErr(AppId, "CKYCExcelUpload.aspx.cs", "btn_Upload_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                        throw ex;
                    }
                }
                finally
                {
                    htParam.Clear();
                    objDt.Clear();
                }
            }
        }
        #endregion

        #region xlsxFormat
        public void xlsxFormat()
        {
            try
            {
                OleDbConnection connOD;
                OleDbCommand command;
                string strConnectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + destPath + ";Extended Properties=\"Excel 8.0;ReadOnly=False;Persist Security Info=False;HDR=NO;IMEX=0;Importmixedtypes=text;typeguessrows=0;FMT=Delimited;\"";

                connOD = new OleDbConnection(strConnectionstring);
                connOD.Open();
                DataTable objDt = new DataTable();

                OdbcDataAdapter oleda = new OdbcDataAdapter();
                DataTable dt = connOD.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                String[] excelSheets = new String[dt.Rows.Count];
                string sheetName = string.Empty;
                string ProcessName = string.Empty;

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            excelSheets[i] = dt.Rows[i]["TABLE_NAME"].ToString();
                            //excelSheets[0] = dt.Rows[0]["TABLE_NAME"].ToString();
                            //excelSheets[1] = dt.Rows[1]["TABLE_NAME"].ToString();
                            //excelSheets[2] = dt.Rows[2]["TABLE_NAME"].ToString();

                            ProcessName = dt.Rows[i]["TABLE_NAME"].ToString().Replace("$", "");
                            dsresult.Tables.Add(ProcessName);
                            //dsresult.Tables.Add("RelatedPerson");
                            //dsresult.Tables.Add("POI_POA");
                            //dsresult.Tables.Add("Registration");

                            if (dt.Rows[i]["TABLE_NAME"].ToString() == excelSheets[i])
                            {
                                sheetName = "SELECT * FROM [" + excelSheets[i] + "]";
                                command = new OleDbCommand(sheetName, connOD);
                                OleDbDataAdapter Da = new OleDbDataAdapter(command);
                                Da.Fill(dsresult.Tables[ProcessName]);
                            }
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
                    objErr.LogErr(AppId, "CKYCExcelUpload.aspx.cs", "xlsxFormat", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }

        }
        #endregion

        #region ddlUpload_SelectedIndexChanged
        protected void ddlUpload_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (ddlUpload.SelectedItem.Value.ToString() == "Select")
                //{
                //    btnUpldFrmt.Enabled = false;
                //}

                if (ddlUpload.SelectedIndex.Equals(1))
                {
                    btnUpldFrmt.Enabled = false;
                }
                htParam.Clear();

                htParam.Add("@DocType", ddlUpload.SelectedValue.ToString().Trim());
                htParam.Add("@Flag", "2");
                objDt = objDAL.GetDataTable("Prc_GetDocDetails", htParam);


                if (objDt.Rows.Count > 0)
                {
                    btn_Upload.Enabled = true;
                }
                else
                {
                    btn_Upload.Enabled = false;
                }


                btnUpldFrmt.Enabled = true;
                btn_Cancel.Enabled = true;

                if (ddlUpload.SelectedIndex.ToString() == "2")
                {
                    btn_Upload.Enabled = true;
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
                    objErr.LogErr(AppId, "CKYCExcelUpload.aspx.cs", "ddlUpload_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region upload format
        protected void btnUpldFrmt_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlUpload.SelectedItem.Value.ToString().Trim() == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select document type to download blank file format.');", true);
                    //ClientScript.RegisterStartupScript(typeof(Page), "Popup", "<script language=JavaScript>AlertMsgs('Please select document type to download blank file format.');</Script>");
                    strCheck = "0";
                }
                else
                {
                    strCheck = "1";
                }

                if (strCheck == "1")
                {
                    htParam.Clear();
                    htParam.Add("@DocType", ddlUpload.SelectedItem.Value);
                    objDt = objDAL.GetDataTable("Prc_GetBlankFormat", htParam);
                    //  ds = objDAL.GetDataSet("Prc_GetBlankFormat", htParam, "UpdDwnldConnectionString");


                    if (objDt.Rows.Count > 0)
                    {
                        if (ddlUpload.SelectedItem.Value.ToString().Trim() == "ULICUPD")
                        {
                            SetExcelFile();
                        }
                        else
                        {
                            setSCVFile();
                        }
                        string strHtml = string.Empty;
                        string strExcel = string.Empty;

                        for (int i = 0; i < objDt.Rows.Count; i++)
                        {

                            strHtml = objDt.Rows[i]["name"].ToString();

                            strExcel = strExcel + strHtml + "\t";

                        }

                        byte[] byteData = System.Text.Encoding.ASCII.GetBytes(strExcel.ToString());
                        char[] charData = System.Text.Encoding.ASCII.GetChars(byteData);
                        Response.Write(charData, 0, charData.Length);
                        Response.Flush();
                        Response.Close();

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
                    objErr.LogErr(AppId, "CKYCEcelUpload.aspx.cs", "btnUpldFrmt_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
            finally
            {
                htParam.Clear();
                objDt.Clear();
            }
        }
        #endregion

        #region Set Excel File
        //xls file format
        protected void SetExcelFile()
        {
            string attachment = "attachment; filename=" + ViewState["DocType"] + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/Microsoft Excel 97- Excel 2008 & 5.0/95 Workbook";
        }

        //csv file format
        protected void setSCVFile()
        {
            string attachment = "attachment; filename=" + ViewState["DocType"] + ".csv";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/Microsoft Excel 97-2003 Workbook";
        }
        #endregion

        #region PopulateCasteCat
        private void PopulateCasteCat()
        {
            try
            {
                htParam.Add("@UserId", Session["UserID"].ToString());
                htParam.Add("@Flag", "CKYCUpload");
                objDt = objDAL.GetDataTable("Prc_GetDocName", htParam);
                if (objDt.Rows.Count > 0)
                {
                    ddlUpload.DataSource = objDt;
                    ddlUpload.DataTextField = "DocDesc";
                    ddlUpload.DataValueField = "DocType";
                    ddlUpload.DataBind();
                    ddlUpload.Items.Insert(0, "Select");
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
                    objErr.LogErr(AppId, "CKYCUpload.aspx.cs", "PopulateCasteCat", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region InitializeControl Method
        private void InitializeControl()
        {
            try
            {
                lbltitle.Text = olng.GetItemDesc("lbltitle");
                lblUpload.Text = olng.GetItemDesc("lblUpload");
                lblFileUpload.Text = olng.GetItemDesc("lblFileUpload");
                lblfilesize.Text = olng.GetItemDesc("lblfilesize");
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }

                else
                {
                    objErr.LogErr(AppId, "CKYCExcelUpload.aspx.cs", "InitializeControl", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;

                }
            }
        }
        #endregion


        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HighCharts.html");
        }
    }
}