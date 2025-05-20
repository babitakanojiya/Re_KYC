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

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class CKYCUpload : System.Web.UI.Page
    {
        #region "Declare Variable"
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
        string strUploadFile = string.Empty;
        string strCheck = string.Empty;
        string strUserId = string.Empty;
        int AppId;
        DataTable objDt = new DataTable();
        DataSet dsresult = new DataSet();

        DataSet dsresnw = new DataSet();


        String FI_NO, CONSTI_TYPE, APP_TYPE, ACC_TYPE, CKYC_NO, PREFIX, FNAME, MNAME, LNAME, FULLNAME, MAIDEN_PREFIX, MAIDEN_FNAME, MAIDEN_MNAME,
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
                          GLOBAL_FLAG_IMG5, BRANCH_CODE_IMG5, IMAGE_DATA_IMG5, PAN_NO; // Added BY Pratik
        byte IMAGE_DATA_IMG1;


        Guid obj = Guid.NewGuid(); //by meena

        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserId"].ToString() != null)
                {
                    strUserId = Session["UserId"].ToString();
                }
                if (HttpContext.Current.Session["AppId"] != null)
                {
                    AppId = Convert.ToInt32(HttpContext.Current.Session["AppId"]);
                }
                olng = new MultilingualManager("DefaultConn", "CKYCUpload.aspx", Session["UserLangNum"].ToString());
                strUserLang = HttpContext.Current.Session["UserLangNum"].ToString();
                if (!IsPostBack)
                {
                    btnUpldFrmt.Enabled = false;
                    //  btnUpldFrmt.Enabled = false;
                    InitializeControl();
                    PopulateCasteCat();
                    btn_Process.Enabled = false;
                    btn_Validate.Enabled = false;
                }
                btn_Upload.Attributes.Add("onclick", "javascript:return StartProgressBar1()");
                btn_Process.Attributes.Add("onclick", "javascript:return StartProgressBar()");
                btn_Validate.Attributes.Add("onclick", "javascript:return StartProgressBar()");
                //ViewState["DocType"] = ddlUpload.SelectedItem.Text.ToString().Trim();

                //  DropDownList ddlUpload1 = Page.FindControl("ddlUpload") as DropDownList;
                // ddlUpload1.Attributes["disabled"] = "disabled";
                btnExport.Visible = false;  
            }
            catch (Exception ex)
            {
                if(Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(AppId, "CKYCUpload.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
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
                lbltitle.Text = olng.GetItemDesc("lblTitle");
                lblUpload.Text = olng.GetItemDesc("lblUpload");
                lblFileUpload.Text = olng.GetItemDesc("lblFileUpload");
                lblfilesize.Text = olng.GetItemDesc("lblfilesize");
                lblDesc.Text = olng.GetItemDesc("lblDesc");
                lblCountDesc.Text = olng.GetItemDesc("lblCountDesc");
                lblview.Text = olng.GetItemDesc("lblview");
                lblTotlCount.Text = olng.GetItemDesc("lblTotlCount");
                lblSuccessCount.Text = olng.GetItemDesc("lblSuccessCount");
                lblErrorCount.Text = olng.GetItemDesc("lblErrorCount");
                lblGridError.Text = olng.GetItemDesc("lblGridError");
                lblGridSuccess.Text = olng.GetItemDesc("lblGridSuccess");
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }

                else
                {
                    objErr.LogErr(AppId, "CKYCUpload.aspx.cs", "InitializeControl", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;

                }
            }
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
                tblErrorLog.Visible = false;
                griderror.Visible = false;
                btn_Validate.Enabled = false;
                btn_Process.Enabled = false;
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
                    objErr.LogErr(AppId, "CKYCUpload.aspx.cs", "ddlUpload_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region button process
        protected void btn_Process_Click(object sender, EventArgs e)
        {

            try
            {
                tblErrorLog.Visible = false;
                griderror.Visible = false;
                btnFailCase.Visible = false;
                btnExport.Visible = false;
                int a;

                //MOVE TEMP DATA TO HIST TABEL 1ST TIME

                htParam.Clear();
                htParam.Add("@Flag", "1");
                htParam.Add("@batchid", hdnBatchid.Value.ToString());
                htParam.Add("@DocType", ddlUpload.SelectedValue.ToString().Trim());
                objDt = objDAL.GetDataTable("Prc_GetValidRecordUpdCKYC", htParam);

                if (objDt.Rows.Count > 0)
                {
                    //Get history table name
                    htParam.Clear();
                    htParam.Add("@DocType", ddlUpload.SelectedValue.ToString().Trim());
                    htParam.Add("@Flag", '2');
                    strTblName = objDAL.ExecuteNonQuery("Prc_GetTempHistTbl", "@TblName", htParam);


                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(strconn))
                    {
                        bulkCopy.DestinationTableName = strTblName;
                        bulkCopy.WriteToServer(objDt);
                        DataSet dsupd = new DataSet();
                        dsupd.Tables.Add(objDt);
                        ViewState["dsresnw"] = dsupd.Copy();
                        htParam.Clear();
                        // ViewState["dsresnw"] = objDt.Copy();
                    }

                    //AFTER RELATIVE TABLE MOVE TEMP TABLE TO HIST TABLE 2ND TIME 
                    objDt.Clear();
                    htParam.Clear();
                    htParam.Add("@Flag", "2");
                    htParam.Add("@batchid", hdnBatchid.Value.ToString());
                    htParam.Add("@DocType", ddlUpload.SelectedValue.ToString().Trim());
                    objDt = objDAL.GetDataTable("Prc_GetValidRecordUpdCKYC", htParam);
                    if (ddlUpload.SelectedIndex.ToString() == "5")
                    {

                        SaveXMLDataInTbl();


                    }
                    //Update status
                    DataSet ds = new DataSet();
                    htParam.Clear();
                    ds.Clear();
                    htParam.Add("@DocType", ddlUpload.SelectedValue.ToString().Trim());
                    htParam.Add("@BatchID", hdnBatchid.Value.ToString());
                    htParam.Add("@CreatedBy", Session["UserID"].ToString());
                    htParam.Add("@JobFlag", "");
                    String strStatus = objDAL.ExecuteNonQuery("Prc_UpdateCKYCStatus", "@ReturnError", htParam);


                    ddlUpload.Enabled = true;
                    fileuploading.Enabled = true;
                    ds.Clear();

                }
                else
                {
                    strProcessFlag = "1";
                }


                ClientScript.RegisterStartupScript(typeof(Page), "Popup", "<script language=javascript>funAlertprocess('" + strProcessFlag + "')</script>");
                if (strProcessFlag == "1")
                {
                    lbl.Text = " No record processed.";
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('No record processed.');", true);
                }
                else
                {
                    lbl.Text = " File processed successfully.";
                    //  ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('File processed successfully.');", true);
                }

                //Delete Temp teble record
                htParam.Clear();
                htParam.Add("@batchid", hdnBatchid.Value);
                htParam.Add("@Doctype", ddlUpload.SelectedItem.Value.ToString());
                htParam.Add("@UserId", Session["UserID"].ToString().Trim());
                a = Convert.ToInt32(objDAL.ExecuteScalar("prc_DeleteTmpTbl", htParam));
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(AppId, "CKYCUpload.aspx.cs", "btn_Process_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
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

        #region button validate
        protected void btn_Validate_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                htParam.Clear();
                //  ds.Clear();
                htParam.Add("@BatchId", hdnBatchid.Value.ToString().Trim());
                htParam.Add("@UserId", Session["UserID"].ToString().Trim());
                htParam.Add("@DocType", ddlUpload.SelectedValue.ToString().Trim());
                strError = objDAL.ExecuteNonQuery("Prc_ValidateBulkUploadKYC", "@ReturnError", htParam);
                //ERROR COUNT
                if (strError == "1")
                {
                    //  ds.Clear();
                    htParam.Clear();
                    htParam.Add("@BatchId", hdnBatchid.Value.ToString());
                    htParam.Add("@DocType", ddlUpload.SelectedItem.Value);
                    ds = objDAL.GetDataSet("Prc_GetErrorCountCKYC", htParam);
                    //  lbl_Error.Text = "<strong>Batch Id :" + hdnBatchid.Value.ToString() + "</strong>";
                    // lblErrorRecord.Text = "Application Error Count : <strong>" + dsResult.Tables[0].Rows[0]["StatusCount"].ToString().Trim() + "</strong>";
                    lbltCountDesc.Text = ds.Tables[0].Rows[0]["TotalCount"].ToString().Trim();
                    lblSuccessCountDesc.Text = ds.Tables[0].Rows[0]["SuccessCount"].ToString().Trim();
                    lblErrorCountDesc.Text = ds.Tables[0].Rows[0]["ErrorCount"].ToString().Trim();
                    tblErrorLog.Visible = true;
                    if (ds.Tables[0].Rows[0]["TotalCount"].ToString().Trim() == ds.Tables[0].Rows[0]["ErrorCount"].ToString().Trim())
                    {
                        btn_Validate.Enabled = false;
                        btn_Process.Enabled = true;
                        //  ClientScript.RegisterStartupScript(typeof(Page), "Popup", "<script language=javascript>funAlertvalidate()</script>");
                        lbl.Text = " File validated successfully.No record to process, please view process log.";
                        // ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('File validated successfully.No record to process, please view process log.');", true);
                    }
                    else
                    {
                        btn_Validate.Enabled = false;
                        btn_Process.Enabled = true;
                        lbl.Text = " File validated successfully,please proceed for process.";
                        //  ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg(' File validated successfully,please proceed for process.');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "Popup", "<script language=javascript>AlertMsgs('Validation error')</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Validation error');", true);
                }
                ds.Clear();
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(AppId, "CKYCUpload.aspx.cs", "btn_Validate_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
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

        #region button upload
        protected void btn_Upload_Click(object sender, EventArgs e)
        {
            //Logger.LogInfo("Upload process starts");

            if (ddlUpload.SelectedIndex == 0)
            {
                //  strDwnld = "1";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select type to upload file.');", true);
                // divSearchDetails.Visible = false;
            }

            if (ddlUpload.SelectedItem.Value.ToString() != "Select")
            {
                try
                {
                    DataSet ds = new DataSet();
                    btnUpldFrmt.Enabled = false;
                    if (fileuploading.HasFile)
                    {
                        System.Threading.Thread.Sleep(2000);
                        string excelExtention = string.Empty;
                        excelExtention = System.IO.Path.GetExtension(fileuploading.PostedFile.FileName).ToLower();

                        if (excelExtention == ".txt" || excelExtention == ".csv" || excelExtention == ".xlsx")
                        {
                            htParam.Clear();
                            ds.Clear();
                            htParam.Add("@flag", "1");
                            htParam.Add("@Seqno", "1");
                            DataAccessLayer objCkyc = new DataAccessLayer("CKYCConnectionString");
                            ds = objCkyc.GetDataSet("Prc_getdata", htParam);
                            destDir = ds.Tables[0].Rows[0]["Path1"].ToString().Trim();
                            //Logger.LogInfo("Destination directory : " + destDir);
                            strUploadFile = System.IO.Path.GetFileName(fileuploading.PostedFile.FileName + "_" + System.DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + ".txt");
                            // destDir = @Server.MapPath("./AgnUpload");
                            fileName = System.IO.Path.GetFileName(fileuploading.PostedFile.FileName + " " + System.DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"));
                            destPath = System.IO.Path.Combine(destDir, strUploadFile);
                            fileuploading.PostedFile.SaveAs(destPath);
                            // ClientScript.RegisterStartupScript(typeof(Page), "Popup", "<script language=javascript>AlertMsgs('" + strPath + "')</script>");
                            //Logger.LogInfo("File uploading  directory : " + destPath);
                            //get temp table name
                            htParam.Clear();
                            htParam.Add("@DocType", ddlUpload.SelectedValue.ToString().Trim());
                            htParam.Add("@Flag", "1");
                            strTblName = objDAL.ExecuteNonQuery("Prc_GetTempHistTbl", "@TblName", htParam);

                            // get batch id
                            htParam.Clear();
                            htParam.Add("@DocType", ddlUpload.SelectedValue.ToString().Trim());
                            batchid = objDAL.ExecuteNonQuery("Prc_UpdtBatchId", "@Batch", htParam);

                            //insert into temp table
                            htParam.Clear();
                            htParam.Add("@Path", destDir + strUploadFile);
                            htParam.Add("@UserId", Session["UserID"].ToString());
                            htParam.Add("@Batchid", batchid);
                            if (ddlUpload.SelectedIndex.ToString() == "1")
                            {
                                objDAL.GetDataSet("prc_InsertUploadCKYCStg1", htParam);
                            }
                            else if (ddlUpload.SelectedIndex.ToString() == "2")
                            {
                                objDAL.GetDataSet("prc_InsertUploadCKYC", htParam);
                            }
                            else if (ddlUpload.SelectedIndex.ToString() == "3")
                            {
                                objDAL.GetDataSet("prc_InsertUploadCKYCPM", htParam);
                            }
                            else if (ddlUpload.SelectedIndex.ToString() == "4")
                            {
                                objDAL.GetDataSet("prc_InsertUploadCKYCCM", htParam);
                            }
                            //by meena start

                            else if (ddlUpload.SelectedIndex.ToString() == "5")
                            {
                                // Logger.LogInfo("Starts getting data from file");
                                ds.Clear();
                                xlsxFormat();
                                // Logger.LogInfo("Data readed successfully from file");
                                dsresult.Tables[0].Columns.Add("Status", typeof(string));
                                dsresult.Tables[0].Columns.Add("ErrorMesg", typeof(string));
                                dsresult.Tables[0].Columns.Add("createdBy", typeof(string));
                                dsresult.Tables[0].Columns.Add("createdDate", typeof(string));
                                dsresult.Tables[0].Columns.Add("updatedBy", typeof(string));
                                dsresult.Tables[0].Columns.Add("updatedDate", typeof(string));
                                dsresult.Tables[0].Columns.Add("Batchid", typeof(string));
                                dsresult.Tables[0].Columns.Add("UserId", typeof(string));

                                for (int i = 0; i < dsresult.Tables[0].Rows.Count; i++)
                                {
                                    dsresult.Tables[0].Rows[i]["Batchid"] = batchid;
                                    dsresult.Tables[0].Rows[i]["UserId"] = Session["UserID"].ToString();
                                    dsresult.Tables[0].Rows[i]["createdBy"] = Session["UserID"].ToString();
                                }

                                dsresult.Tables[0].Columns.Add("ProcessStatus", typeof(string));
                                //dsresult.Tables[0].Columns.Add("PHOTO", typeof(string));
                                //dsresult.Tables[0].Columns.Add("POI", typeof(string));
                                //dsresult.Tables[0].Columns.Add("POA", typeof(string));
                                //dsresult.Tables[0].Columns.Add("POI_POA", typeof(string));

                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(strconn))
                                {
                                    bulkCopy.DestinationTableName = strTblName;
                                    bulkCopy.WriteToServer(dsresult.Tables[0]);
                                    // ViewState["dsresnw"] = dsresult.Copy();
                                    // dsresnw.Tables[0].ImportRow(dsResult.Tables[0]);
                                    // dsresnw.Tables.Add(dsResult.Copy());
                                }
                            }
                            //by meena end 
                            else if (ddlUpload.SelectedIndex.ToString() == "7")
                            {
                                objDAL.GetDataSet("prc_InsUpdCKYCUnsolicited", htParam);
                            }

                            hdnBatchid.Value = batchid;

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

                            //  lbl.Text = "File uploaded successfully,please proceed for validation.";
                            // ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('File uploaded successfully,please proceed for validation.');", true);
                            btn_Upload.Enabled = false;
                            btn_Validate.Enabled = true;
                        }
                        else
                        {
                            // ClientScript.RegisterStartupScript(typeof(Page), "Popup", "<script language=javascript>AlertMsgs('Incorrect file format')</script>");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Incorrect file format');", true);
                        }

                    }

                    else
                    {
                        //ClientScript.RegisterStartupScript(typeof(Page), "Popup", "<script language=javascript>AlertMsgs('Please browse valid file to upload.')</script>");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please browse valid file to upload');", true);
                    }
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
                        objErr.LogErr(AppId, "CKYCUpload.aspx.cs", "btn_Upload_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
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
                //string strConName = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                //string strConnectionstring = String.Format(strConName, destPath,"Yes");
                string strConnectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + destPath + ";Extended Properties=\"Excel 12.0;ReadOnly=False;Persist Security Info=False;HDR=NO;IMEX=0;Importmixedtypes=text;typeguessrows=0;FMT=Delimited;\"";

                //string strConnectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + destPath+ ";Extended Properties=\"Excel 12.0;ReadOnly=False;Persist Security Info=False;HDR=NO;IMEX=0;Importmixedtypes=text;typeguessrows=0;FMT=Delimited;\"";
                // string strConnectionstring = "Driver={Driver do Microsoft Excel(*.xlsx)};dbq=" + destPath + ";defaultdir=" + destDir + ";driverid=790;fil=excel 8.0;filedsn=C:\\Program Files\\Common Files\\ODBC\\Data Sources\\Excel.dsn;maxbuffersize=2048;maxscanrows=8;pagetimeout=5;readonly=0;safetransactions=0;threads=3;uid='" + Session["UserID"].ToString() + "';usercommitsync=Yes";// use to connect to excel for reading data
                connOD = new OleDbConnection(strConnectionstring);
                connOD.Open();
                DataTable objDt = new DataTable();
                // Logger.LogInfo("Excel file connection : " + strConnectionstring);
                OdbcDataAdapter oleda = new OdbcDataAdapter();
                //  DataSet ds = new DataSet();
                DataTable dt = connOD.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                String[] excelSheets = new String[dt.Rows.Count];
                string sheetName = string.Empty;
                if (dt != null)
                {
                    excelSheets[0] = dt.Rows[0]["TABLE_NAME"].ToString();

                }
                sheetName = "SELECT * FROM [" + excelSheets[0] + "]";
                //Logger.LogInfo("Excel query : " + sheetName);
                command = new OleDbCommand(sheetName, connOD);
                OleDbDataAdapter Da = new OleDbDataAdapter(command);
                Da.Fill(dsresult);
                // Logger.LogInfo("Excel query output count : " + dsresult.Tables.Count.ToString() + " _ " + dsresult.Tables[0].Rows.Count.ToString());
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex);
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(AppId, "CKYCUpload.aspx.cs", "xlsxFormat", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }

        }
        #endregion

        #region button export error log
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                htParam.Clear();
                htParam.Add("@Batchid", hdnBatchid.Value);
                htParam.Add("@Flag", "1");
                //  ds = objDAL.GetDataSet("Prc_BindErrorGrid", htParam);
                objDt = objDAL.GetDataTable("Prc_BindErrorGrid", htParam);

                if (objDt.Rows.Count > 0)
                {
                    SetExcelFile();
                    string strData;
                    strHtml = "BatchID" + "\t" + "UniqueRef No." + "\t" + "ErrorDescription" +
                                     "\t" + "ErrorCode" + "\n";

                    for (int i = 0; i < objDt.Rows.Count; i++)
                    {

                        strData = objDt.Rows[i]["Batchid"].ToString()
                        + "\t" + objDt.Rows[i]["UniqueRefNo"].ToString()
                        + "\t" + objDt.Rows[i]["ErrorDesc"].ToString()
                        + "\t" + objDt.Rows[i]["ErrorCode"].ToString();

                        strHtml = strHtml + strData + "\n";
                    }
                    byte[] byteData = System.Text.Encoding.ASCII.GetBytes(strHtml.ToString());
                    char[] charData = System.Text.Encoding.ASCII.GetChars(byteData);
                    Response.Write(charData, 0, charData.Length);
                    Response.Flush();
                    Response.Close();


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
                    objErr.LogErr(AppId, "CKYCUpload.aspx.cs", "btnExport_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
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

        #region button cancel
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnBatchid.Value != null)
                {
                    //ds.Clear();
                    htParam.Clear();
                    htParam.Add("@BatchId", hdnBatchid.Value.ToString());
                    htParam.Add("@DocType", ddlUpload.SelectedItem.Value);
                    string count = objDAL.ExecuteNonQuery("Prc_GetStatus", "@Count", htParam);
                    hdnFileStsFlag.Value = count;
                    if (count != "")
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "popup", "<Script type='text/javascript' language='Javascript'>Confirm()</script>");
                    }
                    else
                    {
                        Response.Redirect("CKYCUpload.aspx");
                    }
                }
                else
                {
                    Response.Redirect("CKYCUpload.aspx");
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
                    objErr.LogErr(AppId, "CKYCUpload.aspx.cs", "btn_Cancel_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }

        }
        #endregion

        #region button Fail cases
        protected void btnFailCase_Click(object sender, EventArgs e)
        {
            try
            {
                string strHtml = string.Empty;
                string strExcel = string.Empty;

                string strData = string.Empty;
                string strExcelData = string.Empty;
                string strExcelColumnData = string.Empty;

                htParam.Clear();
                // ds.Clear();
                htParam.Add("@DocType", ddlUpload.SelectedItem.Value);
                htParam.Add("@BatchId", hdnBatchid.Value.ToString());
                // ds = objDAL.GetDataSet("Prc_GetFailCases", htParam);
                objDt = objDAL.GetDataTable("Prc_GetFailCases", htParam);
                if (objDt.Rows.Count > 0)
                {
                    //string filename = "DownloadExcel.xls";
                    string filename = ddlUpload.SelectedItem.Text.ToString().Trim() + ".xls";
                    System.IO.StringWriter tw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                    DataGrid dgGrid = new DataGrid();
                    dgGrid.DataSource = objDt;
                    dgGrid.DataBind();

                    dgGrid.RenderControl(hw);
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                    this.EnableViewState = false;
                    Response.Write(tw.ToString());
                    Response.End();
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
                    objErr.LogErr(AppId, "CKYCUpload.aspx.cs", "btnFailCase_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
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
                    objErr.LogErr(AppId, "CKYCUpload.aspx.cs", "btnUpldFrmt_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
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

        #region ErrorGrid_PageIndexChanging
        protected void ErrorGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // ds.Clear();
            htParam.Clear();
            htParam.Add("@Batchid", hdnBatchid.Value);
            htParam.Add("@Flag", "1");
            // ds = objDAL.GetDataSet("Prc_BindErrorGrid", htParam);
            objDt = objDAL.GetDataTable("Prc_BindErrorGrid", htParam);

            DataView dv = new DataView(objDt);
            GridView dgSource = (GridView)sender;

            dgSource.PageIndex = e.NewPageIndex;


            dgSource.DataSource = dv;
            dgSource.DataBind();
            ShowPageInformation();
            btnExport.Visible = true;

        }
        #endregion

        #region SuccessGrid_PageIndexChanging
        protected void SuccessGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // ds.Clear();
            htParam.Clear();
            htParam.Add("@Batchid", hdnBatchid.Value);
            htParam.Add("@Flag", "2");
            htParam.Add("@DocType", ddlUpload.SelectedItem.Value);
            //  ds = objDAL.GetDataSet("Prc_BindErrorGrid", htParam);
            objDt = objDAL.GetDataTable("Prc_BindErrorGrid", htParam);
            DataView dv = new DataView(objDt);
            GridView dgSource = (GridView)sender;

            dgSource.PageIndex = e.NewPageIndex;


            dgSource.DataSource = dv;
            dgSource.DataBind();
            ShowSuccessPageInformation();
            btnExport.Visible = true;
        }
        #endregion

        #region grdSap Show Page Information for GridView
        private void ShowPageInformation()
        {
            int intPageIndex = ErrorGrid.PageIndex + 1;
            lblPageInfo.Visible = true;
            lblPageInfo.Text = "Page " + intPageIndex.ToString() + " of " + ErrorGrid.PageCount;
        }
        #endregion

        #region grdSap Show Page Information for GridView
        private void ShowSuccessPageInformation()
        {
            int intPageIndex = SuccessGrid.PageIndex + 1;
            lblSPageInfo.Visible = true;
            lblSPageInfo.Text = "Page " + intPageIndex.ToString() + " of " + SuccessGrid.PageCount;
        }
        #endregion

        #region Success
        protected void lnkSuccess_Click(object sender, EventArgs e)
        {
            try
            {
                griderror.Visible = false;
                hdnFlagCheck.Value = "";
                htParam.Clear();
                htParam.Add("@Batchid", hdnBatchid.Value);
                htParam.Add("@DocType", ddlUpload.SelectedItem.Value);
                htParam.Add("@Flag", "2");
                //  ds = objDAL.GetDataSet("Prc_BindErrorGrid", htParam);
                objDt = objDAL.GetDataTable("Prc_BindErrorGrid", htParam);
                lblGridSuccess.Text = "Success Log Details";

                if (objDt != null)
                {

                    if (objDt.Rows.Count > 0)
                    {
                        hdnFlagCheck.Value = "Success";
                        SuccessGrid.DataSource = objDt;
                        SuccessGrid.DataBind();
                        SuccessGrid.Visible = true;
                        trErrorTitle.Visible = false;
                        trSuccess.Attributes.Add("style", "display:block;margin:1%");
                        trError.Attributes.Add("style", "display:none;");
                        trSuccessTitle.Visible = true;
                        tblErrorLog.Visible = true; //Error Log
                        btnFailCase.Visible = false;
                        btnExport.Visible = false;
                        lblErrMsg.Visible = false;
                        ShowSuccessPageInformation();
                    }
                    else
                    {
                        lblErrMsg.Text = "0 Record found";
                        lblErrMsg.Visible = true;
                        trErrorTitle.Visible = false;
                        trSuccess.Attributes.Add("style", "display:none");
                        trError.Attributes.Add("style", "display:none");
                        trSuccessTitle.Visible = false;
                        tblErrorLog.Visible = true; //Error log
                        ErrorGrid.Visible = false;
                        btnFailCase.Visible = false;
                        btnExport.Visible = false;
                        lblPageInfo.Visible = false;
                    }

                }
                else
                {
                    lblErrMsg.Text = "0 Record found";
                    lblErrMsg.Visible = true;
                    griderror.Visible = true;
                    trErrorTitle.Visible = false;
                    trSuccessTitle.Visible = false;
                    tblErrorLog.Visible = true; //Error log
                    ErrorGrid.Visible = false;
                    btnFailCase.Visible = false;
                    btnExport.Visible = false;
                    lblPageInfo.Visible = false;
                    trSuccess.Attributes.Add("style", "display:none");
                    trError.Attributes.Add("style", "display:none");
                }
                btn_Upload.Enabled = false;
                btn_Validate.Enabled = false;
                btn_Process.Enabled = true;
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(AppId, "CKYCUpload.aspx.cs", "lnkSuccess_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
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

        #region Error Link
        protected void lnkFail_Click(object sender, EventArgs e)
        {
            try
            {
                trSuccessTitle.Visible = false;
                hdnFlagCheck.Value = "";
                htParam.Clear();
                htParam.Add("@Batchid", hdnBatchid.Value);
                htParam.Add("@Flag", "1");
                // ds = objDAL.GetDataSet("Prc_BindErrorGrid", htParam);
                objDt = objDAL.GetDataTable("Prc_BindErrorGrid", htParam);
                //  lblGridError.Text = "Error Log Details";

                if (objDt != null)
                {
                    if (objDt.Rows.Count > 0)
                    {
                        ErrorGrid.DataSource = objDt;
                        ErrorGrid.DataBind();
                        ErrorGrid.Visible = true;
                        griderror.Visible = true; //Error grid
                        trErrorTitle.Visible = true;
                        trSuccessTitle.Visible = false;
                        tblErrorLog.Visible = true; //Error log
                        btnFailCase.Visible = true;
                        btnExport.Visible = true;
                        lblErrMsg.Visible = false;
                        ShowPageInformation();
                        hdnFlagCheck.Value = "Error";
                        trSuccess.Attributes.Add("style", "display:none");
                        trError.Attributes.Add("style", "display:block;margin:1%");
                    }
                    else
                    {
                        lblErrMsg.Text = "0 Record found";
                        lblErrMsg.Visible = true;
                        griderror.Visible = true; //Error grid
                        trErrorTitle.Visible = false;
                        trSuccessTitle.Visible = false;
                        tblErrorLog.Visible = true; //Error log
                        ErrorGrid.Visible = false;
                        SuccessGrid.Visible = false;
                        btnFailCase.Visible = false;
                        btnExport.Visible = false;
                        lblPageInfo.Visible = false;
                        trSuccess.Attributes.Add("style", "display:none");
                        trError.Attributes.Add("style", "display:none");
                    }

                }
                else
                {
                    lblErrMsg.Text = "0 Record found";
                    lblErrMsg.Visible = true;
                    griderror.Visible = true; //Error grid
                    trErrorTitle.Visible = false;
                    trSuccessTitle.Visible = false;
                    tblErrorLog.Visible = true; //Error log
                    ErrorGrid.Visible = false;
                    SuccessGrid.Visible = false;
                    btnFailCase.Visible = false;
                    btnExport.Visible = false;
                    lblPageInfo.Visible = false;
                    trSuccess.Attributes.Add("style", "display:none");
                    trError.Attributes.Add("style", "display:none");
                }
                btn_Upload.Enabled = false;
                btn_Validate.Enabled = false;
                btn_Process.Enabled = true;
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(AppId, "CKYCUpload.aspx.cs", "lnkFail_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
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

        private void SaveXMLDataInTbl()
        {
            try
            {
                dsresnw = (DataSet)ViewState["dsresnw"];
                for (int i = 0; i < dsresnw.Tables[0].Rows.Count; i++)
                {
                    FI_NO = (Convert.ToString(dsresnw.Tables[0].Rows[i]["FIREFNO_CKYC"]));//
                    CKYC_NO = (dsresnw.Tables[0].Rows[i]["FIREFNO_CKYC"].ToString().Trim());//
                    ACC_TYPE = (Convert.ToString(dsresnw.Tables[0].Rows[i]["ACC_TYPE"]));
                    CONSTI_TYPE = Convert.ToString(dsresnw.Tables[0].Rows[i]["CONSTTYPE"]);
                    APP_TYPE = Convert.ToString(dsresnw.Tables[0].Rows[i]["APP_TYPE"]);


                    PREFIX = dsresnw.Tables[0].Rows[i]["PREFIX"].ToString();
                    FNAME = dsresnw.Tables[0].Rows[i]["FNAME"].ToString();
                    MNAME = dsresnw.Tables[0].Rows[i]["MNAME"].ToString();
                    LNAME = dsresnw.Tables[0].Rows[i]["LNAME"].ToString();
                    FULLNAME = FNAME + " " + MNAME + " " + LNAME;

                    MAIDEN_PREFIX = dsresnw.Tables[0].Rows[i]["MAIDEN_PREFIX"].ToString().Trim();
                    MAIDEN_FNAME = dsresnw.Tables[0].Rows[i]["MAIDEN_FNAME"].ToString().Trim();
                    MAIDEN_MNAME = dsresnw.Tables[0].Rows[i]["MAIDEN_MNAME"].ToString().Trim();
                    MAIDEN_LNAME = dsresnw.Tables[0].Rows[i]["MAIDEN_LNAME"].ToString().Trim();
                    MAIDEN_FULLNAME = MAIDEN_FNAME + " " + MAIDEN_MNAME + " " + MAIDEN_LNAME;


                    FATHERSPOUSE_FLAG = "01";//dsresnw.Tables[0].Rows[i]["FS_FLAG"].ToString().Trim());

                    FATHER_PREFIX = dsresnw.Tables[0].Rows[i]["FATHER_PREFIX"].ToString().Trim();
                    FATHER_FNAME = dsresnw.Tables[0].Rows[i]["FATHER_FNAME"].ToString().Trim();
                    FATHER_MNAME = dsresnw.Tables[0].Rows[i]["FATHER_MNAME"].ToString().Trim();
                    FATHER_LNAME = dsresnw.Tables[0].Rows[i]["FATHER_LNAME"].ToString().Trim();
                    FATHER_FULLNAME = FATHER_FNAME + " " + FATHER_MNAME + " " + FATHER_LNAME;

                    MOTHER_PREFIX = dsresnw.Tables[0].Rows[i]["MOTHER_PREFIX"].ToString().Trim();
                    MOTHER_FNAME = dsresnw.Tables[0].Rows[i]["MOTHER_FNAME"].ToString().Trim();
                    MOTHER_MNAME = dsresnw.Tables[0].Rows[i]["MOTHER_MNAME"].ToString().Trim();
                    MOTHER_LNAME = dsresnw.Tables[0].Rows[i]["MOTHER_LNAME"].ToString().Trim();
                    MOTHER_FULLNAME = MOTHER_FNAME + " " + MOTHER_MNAME + " " + MOTHER_LNAME;

                    DOB = dsresnw.Tables[0].Rows[i]["DATE_OF_BIRTH_DATE_OF_INCORPORATION"].ToString().Trim();

                    GENDER = dsresnw.Tables[0].Rows[i]["GENDER"].ToString().Trim();
                    MARITAL_STATUS =dsresnw.Tables[0].Rows[i]["MARITAL_STATUS"].ToString().Trim();
                    NATIONALITY = dsresnw.Tables[0].Rows[i]["NATIONALITY"].ToString().Trim();
                  
                    //GetCKYCdata(ddlResStatus, dsresnw.Tables[0].Rows[i]["RESI_STATUS"].ToString(), "KResi");
                    //ddlOccuSubType.Text = dsresnw.Tables[0].Rows[i]["OCCUPATION"].ToString().Trim();
                    //GetCKYCdata(ddlOccuSubType, dsresnw.Tables[0].Rows[i]["RESI_STATUS"].ToString(), "KResi");
                    SUBOCCUPATION = dsresnw.Tables[0].Rows[i]["OCCUPATION_TYPE"].ToString().Trim();
                    if (dsresnw.Tables[0].Rows[i]["OCCUPATION_TYPE"].ToString() != "")
                    {
                        if (dsresnw.Tables[0].Rows[i]["OCCUPATION_TYPE"].ToString() == "O-01" || dsresnw.Tables[0].Rows[i]["OCCUPATION_TYPE"].ToString() == "O-02"
                            || dsresnw.Tables[0].Rows[i]["OCCUPATION_TYPE"].ToString() == "O-03" || dsresnw.Tables[0].Rows[i]["OCCUPATION_TYPE"].ToString() == "O-04"
                            || dsresnw.Tables[0].Rows[i]["OCCUPATION_TYPE"].ToString() == "O-05")
                        {
                            OCCUPATION = "O";
                        }

                        else if (dsresnw.Tables[0].Rows[i]["OCCUPATION_TYPE"].ToString() == "S-01" || dsresnw.Tables[0].Rows[i]["OCCUPATION_TYPE"].ToString() == "S-02"
                            || dsresnw.Tables[0].Rows[i]["OCCUPATION_TYPE"].ToString() == "S-03")
                        {
                            OCCUPATION = "S";
                        }
                        else if (dsresnw.Tables[0].Rows[i]["OCCUPATION_TYPE"].ToString() == "X-01")
                        {
                            OCCUPATION = "X";
                        }
                        else
                        {
                            OCCUPATION = "B";
                        }
                    }
                    JURI_FLAG = "";
                    RESI_STATUS = dsresnw.Tables[0].Rows[i]["RESIDENTIAL_STATUS"].ToString().Trim();
                    TAX_NUM = dsresnw.Tables[0].Rows[i]["FLAG_APPLICANT_RESIDENT_TAX_JURISDICTION_OUTSIDE_INDIA"].ToString().Trim();
                    BIRTH_PLACE = dsresnw.Tables[0].Rows[i]["CITY_PLANCE_OF_BIRTH"].ToString().Trim();
                    BIRTH_COUNTRY = dsresnw.Tables[0].Rows[i]["COUNTRY_OF_BIRTH"].ToString().Trim();



                    IDENT_TYPE_ID1 = dsresnw.Tables[0].Rows[i]["IDENT_TYPE_ID1"].ToString().Trim();
                    IDENT_NUM_ID1 = dsresnw.Tables[0].Rows[i]["IDENT_NUM_ID1"].ToString().Trim();
                    ID_EXPIRYDATE_ID1 = "";





                    RESI_STATUS = dsresnw.Tables[0].Rows[i]["RESIDENTIAL_STATUS"].ToString().Trim();
                    //GetCKYCdata(ddlProofOfAddress, dsresnw.Tables[0].Rows[i]["PERM_POA"].ToString(), "KAddrPrf");
                    //ddlAddressType.Text = dsresnw.Tables[0].Rows[i]["RESI_STATUS"].ToString().Trim();
                    PERM_POA = dsresnw.Tables[0].Rows[i]["PERM_POA"].ToString().Trim();
                    PERM_TYPE = "";
                    PERM_POAOTHERS =
                    PERM_LINE1 = dsresnw.Tables[0].Rows[i]["PERM_LINE1"].ToString().Trim();
                    PERM_LINE2 = dsresnw.Tables[0].Rows[i]["PERM_LINE2"].ToString().Trim();
                    PERM_LINE3 = dsresnw.Tables[0].Rows[i]["PERM_LINE3"].ToString().Trim();
                    PERM_CITY = dsresnw.Tables[0].Rows[i]["PERM_CITY"].ToString().Trim();
                    PERM_DIST = dsresnw.Tables[0].Rows[i]["PERM_DIST"].ToString().Trim();
                    PERM_PIN = dsresnw.Tables[0].Rows[i]["PERM_PIN"].ToString().Trim();
                    PERM_STATE = dsresnw.Tables[0].Rows[i]["PERM_STATE"].ToString().Trim();
                    PERM_COUNTRY = Convert.ToString(dsresnw.Tables[0].Rows[i]["PERM_COUNTRY"]);



                    PERM_CORRES_SAMEFLAG = dsresnw.Tables[0].Rows[i]["PERM_CORRES_SAMEFLAG"].ToString().Trim();

                    CORRES_LINE1 = Convert.ToString(dsresnw.Tables[0].Rows[i]["CORRES_LINE1"].ToString().Trim());
                    CORRES_LINE2 = Convert.ToString(dsresnw.Tables[0].Rows[i]["CORRES_LINE2"].ToString().Trim());
                    CORRES_LINE3 = Convert.ToString(dsresnw.Tables[0].Rows[i]["CORRES_LINE3"].ToString().Trim());
                    CORRES_CITY = Convert.ToString(dsresnw.Tables[0].Rows[i]["CORRES_CITY"].ToString().Trim());
                    CORRES_DIST = Convert.ToString(dsresnw.Tables[0].Rows[i]["CORRES_DIST"].ToString().Trim());
                    CORRES_PIN = Convert.ToString(dsresnw.Tables[0].Rows[i]["CORRES_PIN"].ToString().Trim());
                    CORRES_STATE = Convert.ToString(dsresnw.Tables[0].Rows[i]["CORRES_STATE"].ToString().Trim());
                    CORRES_COUNTRY = Convert.ToString(dsresnw.Tables[0].Rows[i]["CORRES_COUNTRY"].ToString().Trim());


                    JURI_SAME_FLAG = dsresnw.Tables[0].Rows[i]["JURISDICTION_PERM_CORRES_OVERSEASE_SAMEFLAG"].ToString().Trim();


                    JURI_LINE1 = "";
                    JURI_LINE2 = "";
                    JURI_LINE3 = "";
                    JURI_CITY = "";
                    // ddlDistrict2.Text = Convert.ToString(dsresnw.Tables[0].Rows[i]["JURI_DIST"].ToString().Trim());
                    JURI_PIN ="";
                    JURI_STATE = "";
                    JURI_COUNTRY = "";


                    //OFF_STD_CODE  = Convert.ToString(dsresnw.Tables[0].Rows[i]["std_officeTele"]);
                    //RESI_STD_CODE  = Convert.ToString(dsresnw.Tables[0].Rows[i]["std_resTele"]);
                    //MOB_CODE = Convert.ToString(dsresnw.Tables[0].Rows[i]["mobile_countryCode"]);
                    //FAX_CODE = Convert.ToString(dsresnw.Tables[0].Rows[i]["std_fax"]);




                    OFF_TEL_NUM = Convert.ToString(dsresnw.Tables[0].Rows[i]["OFF_TEL_NUM"]);
                    RESI_TEL_NUM = Convert.ToString(dsresnw.Tables[0].Rows[i]["RESI_TEL_NUM"]);
                    FAX_NO = Convert.ToString(dsresnw.Tables[0].Rows[i]["FAX_NO"]);
                    MOB_NUM = Convert.ToString(dsresnw.Tables[0].Rows[i]["MOB_NUM"]);

                    EMAIL = Convert.ToString(dsresnw.Tables[0].Rows[i]["EMAIL"]);

                    KYC_NAME = Convert.ToString(dsresnw.Tables[0].Rows[i]["KYC_NAME"]);
                    KYC_EMPCODE = Convert.ToString(dsresnw.Tables[0].Rows[i]["KYC_EMPCODE"]);
                    KYC_DESIGNATION = Convert.ToString(dsresnw.Tables[0].Rows[i]["KYC_DESIGNATION"]);
                    KYC_BRANCH = Convert.ToString(dsresnw.Tables[0].Rows[i]["KYC_BRANCH"]);
                    ORG_NAME = Convert.ToString(dsresnw.Tables[0].Rows[i]["ORG_NAME"]);
                    ORG_CODE = Convert.ToString(dsresnw.Tables[0].Rows[i]["ORG_CODE"]);
                    KYC_DATE = Convert.ToString(dsresnw.Tables[0].Rows[i]["KYC_DATE"]);


                    DOC_SUB = Convert.ToString(dsresnw.Tables[0].Rows[i]["DOC_SUB"]);
                    REMARKS = Convert.ToString(dsresnw.Tables[0].Rows[i]["REMARKS"]);
                    DEC_PLACE = Convert.ToString(dsresnw.Tables[0].Rows[i]["DEC_PLACE"]);
                    DEC_DATE = Convert.ToString(dsresnw.Tables[0].Rows[i]["DEC_DATE"]);
                    NUM_IDENTITY = Convert.ToString(dsresnw.Tables[0].Rows[i]["NUM_IDENTITY"]);
                    NUM_RELATED = Convert.ToString(dsresnw.Tables[0].Rows[i]["NUM_RELATED"]);
                    NUM_LOCALADDRESS = Convert.ToString(dsresnw.Tables[0].Rows[i]["NUMBER_OF_LOCAL_ADDRESSDETAILS"]);
                    NUM_IMAGES = Convert.ToString(dsresnw.Tables[0].Rows[i]["NUM_IMAGES"]);
                    NAME_UPDATE_FLAG = Convert.ToString(dsresnw.Tables[0].Rows[i]["APPLICANT_NAME_UPDATE_FLAG"]);
                    PERSONAL_UPDATE_FLAG = Convert.ToString(dsresnw.Tables[0].Rows[i]["PERSONAL_UPDATE_FLAG"]);
                    ADDRESS_UPDATE_FLAG = Convert.ToString(dsresnw.Tables[0].Rows[i]["ADDRESS_UPDATE_FLAG"]);
                    CONTACT_UPDATE_FLAG = Convert.ToString(dsresnw.Tables[0].Rows[i]["CONTACT_UPDATE_FLAG"]);
                    KYC_UPDATE_FLAG = Convert.ToString(dsresnw.Tables[0].Rows[i]["KYC_UPDATE_FLAG"]);
                    IDENTITY_UPDATE_FLAG = Convert.ToString(dsresnw.Tables[0].Rows[i]["IDENTITY_UPDATE_FLAG"]);
                    RELPERSON_UPDATE_FLAG = Convert.ToString(dsresnw.Tables[0].Rows[i]["RELPERSON_UPDATE_FLAG"]);
                    IMAGE_UPDATE_FLAG = Convert.ToString(dsresnw.Tables[0].Rows[i]["IMAGE_UPDATE_FLAG"]);

                    //Added BY Pratik
                    PAN_NO = Convert.ToString(dsresnw.Tables[0].Rows[i]["PAN"]);

                    htParam.Clear();
                    htParam.Add("@FIRefNo", FI_NO);
                    htParam.Add("@CONSTI_TYPE", CONSTI_TYPE);
                    htParam.Add("@APP_TYPE", APP_TYPE);
                    htParam.Add("@ACC_TYPE", ACC_TYPE);
                    htParam.Add("@CKYC_NO", CKYC_NO);
                    htParam.Add("@PREFIX", PREFIX);
                    htParam.Add("@FNAME", FNAME);
                    htParam.Add("@MNAME", MNAME);
                    htParam.Add("@LNAME", LNAME);
                    //htParam.Add("@FULLNAME", FULLNAME);
                    htParam.Add("@MAIDEN_PREFIX", MAIDEN_PREFIX);
                    htParam.Add("@MAIDEN_FNAME", MAIDEN_FNAME);
                    htParam.Add("@MAIDEN_MNAME", MAIDEN_MNAME);
                    htParam.Add("@MAIDEN_LNAME", MAIDEN_LNAME);
                    //htParam.Add("@MAIDEN_FULLNAME", MAIDEN_FULLNAME);
                    htParam.Add("@FATHERSPOUSE_FLAG", FATHERSPOUSE_FLAG);
                    htParam.Add("@FATHER_PREFIX", FATHER_PREFIX);
                    htParam.Add("@FATHER_FNAME", FATHER_FNAME);
                    htParam.Add("@FATHER_MNAME", FATHER_MNAME);
                    htParam.Add("@FATHER_LNAME", FATHER_LNAME);
                    //htParam.Add("@FATHER_FULLNAME", FATHER_FULLNAME);
                    htParam.Add("@MOTHER_PREFIX", MOTHER_PREFIX);
                    htParam.Add("@MOTHER_FNAME", MOTHER_FNAME);
                    htParam.Add("@MOTHER_MNAME", MOTHER_MNAME);
                    htParam.Add("@MOTHER_LNAME", MOTHER_LNAME);
                    //htParam.Add("@MOTHER_FULLNAME", MOTHER_FULLNAME);
                    htParam.Add("@GENDER", GENDER);
                    htParam.Add("@MARITAL_STATUS", "");
                    htParam.Add("@NATIONALITY", "");
                    htParam.Add("@OCCUPATION", "");
                    htParam.Add("@SUBOCCUPATION", "");
                    htParam.Add("@DOB", DOB);

                    htParam.Add("@RESI_STATUS", RESI_STATUS);
                    htParam.Add("@JURI_FLAG", JURI_FLAG);
                    htParam.Add("@TAX_NUM", TAX_NUM);
                    htParam.Add("@BIRTH_COUNTRY", BIRTH_COUNTRY);
                    htParam.Add("@BIRTH_PLACE", BIRTH_PLACE);
                    htParam.Add("@PERM_TYPE", PERM_TYPE);
                    htParam.Add("@PERM_LINE1", PERM_LINE1);
                    htParam.Add("@PERM_LINE2", PERM_LINE2);
                    htParam.Add("@PERM_LINE3", PERM_LINE3);
                    htParam.Add("@PERM_CITY", PERM_CITY);
                    htParam.Add("@PERM_DIST", PERM_DIST);
                    htParam.Add("@PERM_STATE", PERM_STATE);
                    htParam.Add("@PERM_COUNTRY", PERM_COUNTRY);
                    htParam.Add("@PERM_PIN", PERM_PIN);
                    htParam.Add("@PERM_POA", PERM_POA);
                    htParam.Add("@PERM_POAOTHERS", PERM_POAOTHERS);
                    htParam.Add("@PERM_CORRES_SAMEFLAG", PERM_CORRES_SAMEFLAG);
                    htParam.Add("@CORRES_LINE1", CORRES_LINE1);
                    htParam.Add("@CORRES_LINE2", CORRES_LINE2);
                    htParam.Add("@CORRES_LINE3", CORRES_LINE3);
                    htParam.Add("@CORRES_CITY", CORRES_CITY);
                    htParam.Add("@CORRES_DIST", CORRES_DIST);
                    htParam.Add("@CORRES_STATE", CORRES_STATE);
                    htParam.Add("@CORRES_COUNTRY", CORRES_COUNTRY);
                    htParam.Add("@CORRES_PIN", CORRES_PIN);
                    htParam.Add("@JURI_SAME_FLAG", "");
                    htParam.Add("@JURI_LINE1", "");
                    htParam.Add("@JURI_LINE2", "");
                    htParam.Add("@JURI_LINE3", "");
                    htParam.Add("@JURI_CITY", "");
                    htParam.Add("@JURI_STATE", "");
                    htParam.Add("@JURI_COUNTRY", "");
                    htParam.Add("@JURI_PIN", "");
                    htParam.Add("@RESI_STD_CODE", RESI_STD_CODE);
                    htParam.Add("@RESI_TEL_NUM", RESI_TEL_NUM);
                    htParam.Add("@OFF_STD_CODE", OFF_STD_CODE);
                    htParam.Add("@OFF_TEL_NUM", OFF_TEL_NUM);
                    htParam.Add("@MOB_CODE", MOB_CODE);
                    htParam.Add("@MOB_NUM", MOB_NUM);
                    htParam.Add("@FAX_CODE", FAX_CODE);
                    htParam.Add("@FAX_NO", FAX_NO);
                    htParam.Add("@EMAIL", EMAIL);
                    htParam.Add("@REMARKS", REMARKS);

                    htParam.Add("@DEC_DATE ", DEC_DATE);
                    htParam.Add("@DEC_PLACE", DEC_PLACE);
                    htParam.Add("@KYC_DATE ", KYC_DATE);
                    htParam.Add("@DOC_SUB", DOC_SUB);
                    htParam.Add("@KYC_NAME", KYC_NAME);
                    htParam.Add("@KYC_DESIGNATION", KYC_DESIGNATION);
                    htParam.Add("@KYC_BRANCH", KYC_BRANCH);
                    htParam.Add("@KYC_EMPCODE", KYC_EMPCODE);
                    htParam.Add("@ORG_NAME", ORG_NAME);
                    htParam.Add("@ORG_CODE", ORG_CODE);
                    htParam.Add("@NUM_IDENTITY", NUM_IDENTITY);
                    htParam.Add("@NUM_RELATED", NUM_RELATED);
                    htParam.Add("@NUM_LOCALADDRESS", NUM_LOCALADDRESS);
                    htParam.Add("@NUM_IMAGES", NUM_IMAGES);
                    htParam.Add("@NAME_UPDATE_FLAG", NAME_UPDATE_FLAG);
                    htParam.Add("@PERSONAL_UPDATE_FLAG", PERSONAL_UPDATE_FLAG);
                    htParam.Add("@ADDRESS_UPDATE_FLAG", ADDRESS_UPDATE_FLAG);
                    htParam.Add("@CONTACT_UPDATE_FLAG", CONTACT_UPDATE_FLAG);
                    htParam.Add("@KYC_UPDATE_FLAG", KYC_UPDATE_FLAG);
                    htParam.Add("@IDENTITY_UPDATE_FLAG", IDENTITY_UPDATE_FLAG);
                    htParam.Add("@RELPERSON_UPDATE_FLAG", RELPERSON_UPDATE_FLAG);
                    htParam.Add("@IMAGE_UPDATE_FLAG", IMAGE_UPDATE_FLAG);
                    // htParam.Add("@SEQUENCE_NO_ID1", SEQUENCE_NO_ID1);
                    htParam.Add("@IDENT_TYPE_ID1", IDENT_TYPE_ID1);
                    htParam.Add("@IDENT_NUM_ID1", IDENT_NUM_ID1);
                    htParam.Add("@ID_EXPIRYDATE_ID1", ID_EXPIRYDATE_ID1);
                    //Added BY Pratik
                    htParam.Add("@PAN_NO", PAN_NO);
                    htParam.Add("@createdBy", Session["UserID"].ToString());

                    htParam.Add("@uniqueID", obj.ToString());
                    //  htParam.Add("@batchid", hdnBatchid.Value.ToString());
                    DataAccessLayer objCkyc = new DataAccessLayer("CKYCConnectionString");
                    //  ds = objCkyc.GetDataSet("Prc_getdata", htParam);
                    dsresult = objCkyc.GetDataSet("prc_InsXMLResponseFI_KYCReg1", htParam);
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
                    objErr.LogErr(AppId, "CKYCUpload.aspx.cs", "SaveXMLDataInTbl", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }

    }
}