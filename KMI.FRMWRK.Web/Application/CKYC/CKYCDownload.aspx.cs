#region namespaces
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using KMI.FRMWRK.DAL;
using KMI.FRMWRK.Multilingual;
using Ionic.Zip;// added for zip file 

#endregion

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class CKYCDownload : System.Web.UI.Page
    {


        #region Page Declaration
        DataSet ds;
        Hashtable htParam = new Hashtable();
        DataTable objDt = new DataTable();
        DataAccessLayer objDAL = new DataAccessLayer("UpdDwnldConnectionString");
        ErrorLog objErr = new ErrorLog();
        private MultilingualManager olng;
        private string strUserLang;
        string strPathDoc = string.Empty;//= @"F:\KMIFTP\CkycKmi\Request\";
        string strpathserver = string.Empty;
        private string strconn = ConfigurationManager.ConnectionStrings["UpdDwnldConnectionString"].ConnectionString.ToString();

        DataSet dsExport = new DataSet();
        DataTable dtResult = new DataTable();
        DataSet dsRes = new DataSet();
        string FileName = string.Empty;
        string strBatchId = string.Empty;
        string strDwnld = string.Empty;
        String strTblName = string.Empty;
        string strTblHstName = string.Empty;
        string strValue = string.Empty;
        string strPhotoPath = string.Empty;
        string strFormat = string.Empty;
        string strmsg = string.Empty;
        string strUserId = string.Empty;
        int AppId;
        // string strPathDoc = System.Configuration.ConfigurationManager.AppSettings["DownloadExcel"].ToString();
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
                olng = new MultilingualManager("DefaultConn", "CKYCDownload.aspx", Session["UserLangNum"].ToString());
                strUserLang = HttpContext.Current.Session["UserLangNum"].ToString();
                Session["CarrierCode"] = '2';
                if (!IsPostBack)
                {
                    InitializeControl();
                    PopulateDRFType();
                    chkfiledwnld.Visible = false;
                    btnExport.Visible = false;
                    btncnfrm.Visible = false;
                    btnfail.Visible = false;
                }
                btndwnld.Attributes.Add("onclick", "javascript:return StartProgressBar()");
                if (hdnEnbl.Value == "1")
                {
                    if (chkfiledwnld.Checked == true)
                    {
                        btncnfrm.Visible = true;
                        btnfail.Visible = true;
                    }
                    else
                    {
                        btncnfrm.Visible = false;
                        btnfail.Visible = false;
                    }
                    hdnEnbl.Value = "0";
                    btnExport.Visible = true;
                    chkfiledwnld.Visible = true;
                    chkfiledwnld.Enabled = true;
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
                    objErr.LogErr(AppId, "CKYCDownload.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;

                }
            }
        }
        #endregion

        #region initializing controls
        private void InitializeControl()
        {
            try
            {

                lbltitle.Text = olng.GetItemDesc("lbltitle");
                lblUpload.Text = olng.GetItemDesc("lblUpload");
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(AppId, "CKYCDownload.aspx.cs", "InitializeControl", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region PopulateDRFType
        private void PopulateDRFType()
        {
            try
            {
                htParam.Clear();

                htParam.Add("@UserId", Session["UserID"].ToString());
                htParam.Add("@flag", "1");
                // ds = objDAL.GetDataSet("Prc_GetDRFDocName", htParam, "UpdDwnldConnectionString");
                objDt = objDAL.GetDataTable("Prc_GetDRFDocName", htParam);

                if (objDt.Rows.Count > 0)
                {
                    ddldwnload.DataSource = objDt;
                    ddldwnload.DataTextField = "DocDesc";
                    ddldwnload.DataValueField = "DocType";
                    ddldwnload.DataBind();
                    ddldwnload.Items.Insert(0, "Select");
                }

                //ds.Clear();
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(AppId, "CKYCDownload.aspx.cs", "InitializeControl", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
            finally
            {
                htParam = null;
                objDt = null;
            }
        }
        #endregion


        #region button Export
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                strBatchId = hdnBatchId.Value;
                //ds = objDAL.GetDataSet("PrcGetAllRecrdDWN", htParam, "UpdDwnldConnectionString");
                //if (ds.Tables.Count > 0)
                //{
                //    for (int j = 0; j < ds.Tables.Count; j++)
                //    {
                //ddldwnload.SelectedValue = ds.Tables[j].Rows[0][1].ToString();
                //ds.Clear();
                htParam.Clear();
                htParam.Add("@DocType", ddldwnload.SelectedValue.ToString().Trim());
                htParam.Add("@UserId", Session["UserID"].ToString());
                objDt = objDAL.GetDataTable("Prc_GetDwnldRecord", htParam);

                if (objDt.Rows.Count > 0)
                {

                    string RegRefNo = objDt.Rows[0]["RegRefNo"].ToString();
                    if (ddldwnload.SelectedValue.ToString().Trim().Equals("DNDCM") || ddldwnload.SelectedValue.ToString().Trim().Equals("DNDNM"))
                    {
                        FileName = objDt.Rows[0]["InstCode"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + "RECON." + hdnBatchId.Value;
                    }
                    else
                    {
                        FileName = objDt.Rows[0]["InstCode"].ToString() + "_" + objDt.Rows[0]["EmpBranch"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + "IA003419" + "_" + "U" + hdnBatchId.Value;
                    }
                    BindDocument(RegRefNo);

                }
                //    }
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
                    objErr.LogErr(AppId, "CKYCDownload.aspx.cs", "btnExport_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
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

        #region bind document
        private void BindDocument(string RegRefNo)
        {

            try
            {
                htParam.Clear();
                // ds.Clear();
                htParam.Add("@flag", "1");
                htParam.Add("@Seqno", "1");
                DataAccessLayer objCkyc = new DataAccessLayer("CKYCConnectionString");
                // ds = objDAL.GetDataSet("Prc_getdata", htParam, "CKYCConnectionString");
                objDt = objCkyc.GetDataTable("Prc_getdata", htParam);
                strPathDoc = objDt.Rows[0]["Path1"].ToString().Trim();
                string strFilePathDoc = string.Empty;
                htParam.Clear();
                objDt.Clear();
                //  ds.Clear();
                htParam.Add("@DocType", ddldwnload.SelectedValue.ToString().Trim());
                htParam.Add("@Batchid", hdnBatchId.Value.Trim());
                htParam.Add("@UserId", Session["UserID"].ToString());
                objDt = objDAL.GetDataTable("Prc_GetDwnldRecordforCSV", htParam);
                //ds = objDAL.GetDataSet("Prc_GetDwnldRecordforCSV", htParam, "UpdDwnldConnectionString");
                string strText = string.Empty;

                for (int cnt = 0; cnt < objDt.Rows.Count; cnt++)
                {
                    strText = strText + objDt.Rows[cnt][1].ToString().Trim() + "|" + Environment.NewLine;
                    strText = strText + "\n";
                }

                if (objDt.Rows.Count > 0)
                {
                    htParam.Clear();
                    string filename = FileName + ".txt";
                    string strTrgFile = FileName + ".trg";
                    System.IO.StringWriter tw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                    DataGrid dgGrid = new DataGrid();
                    if (ddldwnload.SelectedValue.ToString().Equals("DNDCM") || ddldwnload.SelectedValue.ToString().Trim().Equals("DNDNM"))
                    {
                        strFilePathDoc = strPathDoc + "\\" + filename;
                        System.IO.File.WriteAllText(strFilePathDoc, strText);
                        strFilePathDoc = strPathDoc + "\\" + strTrgFile;
                        System.IO.File.Create(strFilePathDoc);
                        // zip.Save(strPathDoc + "\\" + strTrgFile);// added by amruta for .trg file
                    }

                    else
                    {
                        strFormat = "Zip Format";

                        if (strFormat == "Zip Format")
                        {
                            string strPhoto = string.Empty;
                            int cnt2 = 1;
                            DataSet dsCnt = new DataSet();
                            htParam.Clear();
                            //ds.Clear();
                            htParam.Add("@DocType", ddldwnload.SelectedValue.ToString().Trim());
                            htParam.Add("@UserId", Session["UserID"].ToString());
                            //   ds = objDAL.GetDataSet("Prc_GetDwnldRecord", htParam, "UpdDwnldConnectionString");
                            objDt = objDAL.GetDataTable("Prc_GetDwnldRecord", htParam);
                            for (int cnt1 = 0; cnt1 < objDt.Rows.Count; cnt1++)
                            {
                                string RegrefNo1 = string.Empty;

                                RegrefNo1 = objDt.Rows[cnt1][0].ToString().Trim();
                                htParam.Clear();
                                htParam.Add("@RegNo", RegrefNo1);
                                strFilePathDoc = strPathDoc + "\\" + FileName;
                                Directory.CreateDirectory(strFilePathDoc);
                                if (Directory.Exists(strFilePathDoc))
                                {
                                    if (Directory.Exists(strFilePathDoc))
                                    {
                                        if (cnt2 == 1)
                                        {
                                            strPhoto = strFilePathDoc + "\\" + RegRefNo;
                                        }
                                        else
                                        {
                                            strPhoto = strFilePathDoc + "\\" + RegrefNo1;
                                        }
                                        if (!Directory.Exists(strPhoto))
                                        {
                                            if (cnt2 == 1)
                                            {
                                                Directory.CreateDirectory(strPhoto);
                                                hdnpath.Value = strPhoto;
                                                cnt2 = cnt2 + 1;
                                            }
                                            else
                                            {
                                                strPhoto = strFilePathDoc + "\\" + RegrefNo1;
                                                Directory.CreateDirectory(strPhoto);
                                            }
                                        }
                                    }

                                }


                                System.IO.File.WriteAllText(strFilePathDoc + "\\" + filename, strText);

                                htParam.Add("@fLAG", "1");
                                DataSet dsimg = new DataSet();
                                dsimg = objCkyc.GetDataSet("prc_GetImage", htParam);
                                if (dsimg.Tables.Count > 0)
                                {

                                    for (int j = 0; j < dsimg.Tables.Count; j++)
                                    {
                                        if (dsimg.Tables[j].Rows.Count > 0)
                                        {
                                            for (int i = 0; i < dsimg.Tables[j].Rows.Count; i++)
                                            {
                                                strPhotoPath = string.Empty;
                                                int cntcnd = 1 + cnt1;
                                                if (cnt2 == 1)
                                                {
                                                    strPhotoPath = strPhoto + "\\" + RegRefNo + '_' + dsimg.Tables[j].Rows[i]["DOC_NAME"] + ".jpg";
                                                }
                                                else
                                                {
                                                    strPhotoPath = strPhoto + "\\" + RegrefNo1 + '_' + dsimg.Tables[j].Rows[i]["DOC_NAME"] + ".jpg";
                                                }
                                                FileInfo fi = new FileInfo(strPhotoPath);
                                                using (FileStream fileStream = fi.Open(FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                                                {
                                                    Byte[] bytes = (Byte[])dsimg.Tables[j].Rows[i]["IMAGE"];
                                                    fileStream.Write(bytes, 0, bytes.Length);
                                                }
                                            }
                                        }
                                    }

                                }
                                using (ZipFile zip = new ZipFile())
                                {
                                    int cntcnd = 1 + cnt1;
                                    if (cnt2 == 1)
                                    {
                                        zip.AddDirectory(strPhoto, RegRefNo);
                                    }
                                    else
                                    {
                                        zip.AddDirectory(strPhoto, RegrefNo1);
                                    }

                                    zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                                    zip.Save(strPhoto + ".zip");
                                    Directory.Delete(strPhoto, true);    //Mrunal
                                    //ds.Clear();
                                }

                            }

                        }
                        using (ZipFile zip = new ZipFile())
                        {
                            zip.AddDirectory(strPathDoc + "\\" + FileName, FileName);

                            zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                            zip.Save(strPathDoc + "\\" + FileName + ".zip");
                            zip.Save(strPathDoc + "\\" + strTrgFile);// added by amruta for .trg file
                            string str11 = strPathDoc + "\\" + FileName;
                            Directory.Delete(str11, true);
                        }

                        strpathserver = strPathDoc + "\\" + FileName + ".zip";
                        WebClient req = new WebClient();
                        HttpResponse response = HttpContext.Current.Response;
                        response.Clear();
                        response.ClearContent();
                        response.ClearHeaders();
                        response.Buffer = true;
                        response.AddHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(strpathserver) + "");
                        byte[] data = req.DownloadData((strpathserver));
                        response.BinaryWrite(data);
                        Response.Flush();
                        Response.SuppressContent = true;
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
                    objErr.LogErr(AppId, "CKYCDownload.aspx.cs", "BindDocument", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
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

        #region bind grid
        void BindDataGrid()
        {
            try
            {
                dtResult = GetDataTable();
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        trdgHdr.Visible = true;
                        trdg.Visible = true;
                        dgDownload.Visible = true;
                        dgDownload.DataSource = dtResult;
                        dgDownload.DataBind();
                        btnExport.Visible = true;
                        chkfiledwnld.Visible = true;

                        divSearchDetails.Attributes.Add("style", "display:block");
                    }
                    else
                    {
                        trdgHdr.Visible = false;
                        trdg.Visible = false;
                        dgDownload.Visible = false;

                        divSearchDetails.Visible = false;

                        btnExport.Visible = false;
                        btncnfrm.Visible = false;
                        btnfail.Visible = false;
                        chkfiledwnld.Visible = false;
                        divSearchDetails.Attributes.Add("style", "display:none");
                    }
                }
                else
                {
                    trdgHdr.Visible = false;
                    trdg.Visible = false;
                    dgDownload.Visible = false;

                    btnExport.Visible = false;
                    btncnfrm.Visible = false;
                    btnfail.Visible = false;
                    chkfiledwnld.Visible = false;
                    divSearchDetails.Attributes.Add("style", "display:none");
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
                    objErr.LogErr(AppId, "CKYCDownload.aspx.cs", "BindDataGrid", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region  dgDownload PageIndexChanging
        protected void dgDownload_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                objDt = GetDataTable();
                DataView dv = new DataView(objDt);
                GridView dgSource = (GridView)sender;
                dgSource.PageIndex = e.NewPageIndex;
                dgSource.DataSource = dv;
                dgSource.DataBind();
                ShowPageInformation();
                btnExport.Visible = true;
                chkfiledwnld.Visible = true;
                if (chkfiledwnld.Checked == true)
                {
                    btncnfrm.Visible = true;
                    btnfail.Visible = true;
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
                    objErr.LogErr(AppId, "CKYCDownload.aspx.cs", "dgDownload_PageIndexChanging", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region GetDataTable()
        protected DataTable GetDataTable()
        {
            try
            {
                // ds.Clear();
                htParam.Clear();
                htParam.Add("@DocType", ddldwnload.SelectedValue.ToString().Trim());
                htParam.Add("@BatchId", hdnBatchId.Value);
                objDt = objDAL.GetDataTable("Prc_GetTmptoHst", htParam);
                // ds = objDAL.GetDataSet("Prc_GetTmptoHst", htParam, "UpdDwnldConnectionString");
                ViewState["SearchBindGrid"] = objDt.Copy();

                if (objDt.Rows.Count > 0)
                {
                    lblSearch.Text = ddldwnload.SelectedItem.Text.ToString();
                    trdgHdr.Visible = true;
                    trdg.Visible = true;
                    dgDownload.Visible = true;
                    dgDownload.DataSource = objDt;
                    dgDownload.DataBind();
                    btnExport.Visible = true;
                    btncnfrm.Visible = true;
                    btnfail.Visible = true;
                    btncan.Visible = false;
                    chkfiledwnld.Visible = true;
                    chkfiledwnld.Checked = false;

                }
                else
                {
                    trdgHdr.Visible = false;
                    trdg.Visible = false;
                    dgDownload.Visible = false;
                    btnExport.Visible = false;
                    chkfiledwnld.Visible = false;
                    btncnfrm.Visible = false;
                    btnfail.Visible = false;
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
                    objErr.LogErr(AppId, "CKYCDownload.aspx.cs", "GetDataTable", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
            return objDt;
        }
        #endregion

        #region grdSap Show Page Information for GridView
        private void ShowPageInformation()
        {
            int intPageIndex = dgDownload.PageIndex + 1;
        }
        #endregion

        #region chkfiledwnld
        protected void chkfiledwnld_CheckedChanged(object sender, EventArgs e)
        {
            if (chkfiledwnld.Checked == true)
            {
                btncnfrm.Visible = true;
                btncnfrm.Enabled = true;
                btnExport.Visible = true;
                chkfiledwnld.Visible = true;
                btnfail.Visible = true;
                btnfail.Enabled = true;
            }
            else
            {
                btncnfrm.Visible = false;
                btncnfrm.Enabled = false;
                btnExport.Visible = true;
                chkfiledwnld.Visible = true;
                btnfail.Visible = false;
                btnfail.Enabled = false;
            }
        }
        #endregion

        #region button Fail
        protected void btnFail_Click(object sender, EventArgs e)
        {
            try
            {
                htParam.Clear();
                //ds.Clear();
                htParam.Add("@DocType", ddldwnload.SelectedValue.ToString().Trim());
                objDt = objDAL.GetDataTable("Prc_GetTrunctbl", htParam);
                //ds = objDAL.GetDataSet("Prc_GetTrunctbl", htParam, "UpdDwnldConnectionString");
                Response.Redirect("CKYCDownload.aspx");
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(AppId, "CKYCDownload.aspx.cs", "btnFail_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region button Confirm
        protected void btncnfrm_Click(object sender, EventArgs e)
        {
            try
            {
                //GET HIST TABLE NAME
                htParam.Clear();
                htParam.Add("@DocType", ddldwnload.SelectedValue.ToString().Trim());
                htParam.Add("@Flag", "2");
                strTblHstName = objDAL.ExecuteNonQuery("Prc_GetDwnldTblName", "@TblName", htParam);

                //UPDATE HIST TABLE NAME TO UPLDQWNLDLOG

                htParam.Clear();
                htParam.Add("@DocType", ddldwnload.SelectedValue.ToString().Trim());
                htParam.Add("@BatchID", hdnBatchId.Value);
                ds = objDAL.GetDataSet("prc_InsHstUpldDwnldLog", htParam);
                ds.Clear();
                htParam.Clear();
                htParam.Add("@BatchId", hdnBatchId.Value);
                htParam.Add("@ConfirmBy", Session["UserID"].ToString());
                htParam.Add("@Flag", "3");
                ds = objDAL.GetDataSet("prc_DECLog", htParam);
                //MOVE DATA FROM TEMP TABLE TO HIST TABLE
                ds.Clear();
                htParam.Clear();
                htParam.Add("@DocType", ddldwnload.SelectedValue.ToString().Trim());
                ds = objDAL.GetDataSet("Prc_GetHsttoTmp", htParam);
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(strconn))
                {
                    bulkCopy.DestinationTableName = strTblHstName;
                    bulkCopy.WriteToServer(ds.Tables[0]);
                }
                ds.Clear();
                ds = objDAL.GetDataSet("Prc_GetTrunctbl", htParam);
                string strBatchId = hdnBatchId.Value;

                strmsg = "Thank you for your confirmation. Note: Please keep the BatchId for furture reference. BatchId: " + Server.HtmlEncode(strBatchId) + ".";


                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + strmsg + "');", true);


                divSearchDetails.Visible = false;
                btnExport.Visible = false;
                chkfiledwnld.Visible = false;
                btncnfrm.Visible = false;
                btnfail.Visible = false;
                htParam.Clear();
                htParam.Add("@DocType", ddldwnload.SelectedValue.ToString().Trim());
                htParam.Add("@BatchID", hdnBatchId.Value);
                htParam.Add("@CreatedBy", Session["UserID"].ToString());
                htParam.Add("@Flag", "1");
                String strStatus = objDAL.ExecuteNonQuery("Prc_UpdateCndStatus", "@CndStatus", htParam);

                ddldwnload.SelectedIndex = 0;
                //RECORD DATA MOVEMENT
                htParam.Clear();
                DataAccessLayer objckyc = new DataAccessLayer("CKYCConnectionString");
                htParam.Add("@kycSts", strStatus);
                htParam.Add("@CreatedBy", Session["UserID"].ToString());
                htParam.Add("@Description", ddldwnload.SelectedItem.Text.ToString());
                // ds = objDAL.GetDataSet("Prc_InsCndKYCStatusMvmt", htParam, "CKYCConnectionString");
                string strSts = (string)objckyc.ExecuteScalar("Prc_InsCndKYCStatusMvmt", htParam);

            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(AppId, "CKYCDownload.aspx.cs", "btncnfrm_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region button Download
        protected void btndwnld_Click(object sender, EventArgs e)
        {
            try
            {
                divSearchDetails.Visible = true;
                dgDownload.DataSource = null;
                dgDownload.DataBind();


                if (ddldwnload.SelectedIndex == 0)
                {
                    strDwnld = "1";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select type to download file.');", true);
                    divSearchDetails.Visible = false;
                }

                else
                {
                    if (strDwnld != "1")
                    {

                        //GET TEMP TABLE NAME
                        htParam.Clear();
                        htParam.Add("@DocType", ddldwnload.SelectedValue.ToString().Trim());
                        htParam.Add("@Flag", "1");
                        strTblName = objDAL.ExecuteNonQuery("Prc_GetDwnldTblName", "@TblName", htParam);

                        htParam.Clear();
                        htParam.Add("@DocType", ddldwnload.SelectedValue.ToString().Trim());
                        htParam.Add("@UserId", Session["UserID"].ToString());
                        objDt = objDAL.GetDataTable("Prc_GetDwnldRecord", htParam);
                        //ds = objDAL.GetDataSet("Prc_GetDwnldRecord", htParam, "UpdDwnldConnectionString");
                        string RecrdCnt = objDt.Rows.Count.ToString();
                        if (objDt.Rows.Count > 0)
                        {
                            //insert in temp table
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(strconn))//strconn
                            {
                                bulkCopy.DestinationTableName = strTblName;
                                bulkCopy.WriteToServer(objDt);
                            }
                            //GET BATCHID
                            htParam.Clear();
                            htParam.Add("@DocType", ddldwnload.SelectedValue.ToString().Trim());
                            strBatchId = objDAL.ExecuteNonQuery("Prc_UpdtBatchId", "@Batch", htParam);
                            hdnBatchId.Value = strBatchId.Trim();
                            htParam.Clear();
                            objDt.Clear();
                            //Hashtable htParamDwnld = new Hashtable();
                            //DataSet dsDwnld = new DataSet();
                            htParam.Add("@BatchId", strBatchId);
                            htParam.Add("@DocType", ddldwnload.SelectedValue.ToString().Trim());
                            htParam.Add("@DwnldBy", Session["UserID"].ToString());
                            htParam.Add("@Flag", "1");
                            objDt = objDAL.GetDataTable("prc_DECLog", htParam);
                            strValue = "1";
                            hdncheck.Value = "1";
                            divSearchDetails.Attributes.Add("style", "display:block");
                        }
                        else
                        {
                            trdgHdr.Visible = false;
                            trdg.Visible = false;
                            btnExport.Visible = false;
                            btncnfrm.Visible = false;
                            chkfiledwnld.Visible = false;
                            btnfail.Visible = false;
                            divSearchDetails.Attributes.Add("style", "display:none");
                            lblerror.Visible = true;
                            //New added bhau 21/03/2018
                            lblerror.Text = "0 record found";
                        }



                        //INSERT ENTRY TO UPLDDWNLDLOG
                        if (strValue == "1")
                        {
                            htParam.Clear();
                            objDt.Clear();
                            htParam.Add("@BatchID", strBatchId.ToString());
                            htParam.Add("@DocType", ddldwnload.SelectedValue.ToString().Trim());
                            htParam.Add("@UserId", Session["UserId"].ToString().Trim());
                            htParam.Add("@RecrdCnt", RecrdCnt);
                            htParam.Add("@Action", "Dwnld");
                            ds = objDAL.GetDataSet("prc_InsUpldDwnldLog", htParam);
                            BindDataGrid();
                        }
                        btncnfrm.Visible = false;

                        btnfail.Visible = false;

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
                    objErr.LogErr(AppId, "CKYCDownload.aspx.cs", "btndwnld_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion



        #region ddldwnload_SelectedIndexChanged
        protected void ddldwnload_SelectedIndexChanged(object sender, EventArgs e)
        {

            divSearchDetails.Visible = false;
            chkfiledwnld.Visible = false;
            btnExport.Visible = false;
            dgDownload.PageIndex = 0;
            btncnfrm.Visible = false;
            btnfail.Visible = false;


        }
        #endregion

        #region Page Size Selection Handling
        protected void ddlPageSize_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ViewState["SearchBindGrid"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["SearchBindGrid"];
                    dgDownload.DataSource = dtCurrentTable;
                    dgDownload.DataBind();
                }
                else
                    this.BindDataGrid();
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(AppId, "CKYCDownload.aspx.cs", "ddlPageSize_OnSelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region GridView Left And Right Button Indexing Change Event
        protected void ddlPageSelectorL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgDownload.EditIndex = -1;
                dgDownload.PageIndex = ((DropDownList)sender).SelectedIndex;
                GetSearchData(dgDownload);
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(AppId, "CKYCDownload.aspx.cs", "ddlPageSelectorL_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }

        protected void ddlPageSelectorR_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgDownload.EditIndex = -1;
                dgDownload.PageIndex = ((DropDownList)sender).SelectedIndex;
                GetSearchData(dgDownload);
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(AppId, "CKYCDownload.aspx.cs", "ddlPageSelectorR_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region GetSearchData
        protected void GetSearchData(GridView grd)
        {
            try
            {
                if (ViewState["SearchBindGrid"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["SearchBindGrid"];
                    grd.DataSource = dtCurrentTable;
                    grd.DataBind();
                }
                else
                    this.BindDataGrid();

            }

            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(AppId, "CKYCDownload.aspx.cs", "GetSearchData", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region GridView Row Created Change Event
        protected void dgDownload_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType.Equals(DataControlRowType.Pager))
                {
                    SetPagerButtonStates(dgDownload, e.Row, this, "ddlPageSelectorL", "ddlPageSelectorR");
                }

                if (e.Row.RowType.Equals(DataControlRowType.DataRow))
                {
                    dgDownload.UseAccessibleHeader = true;
                    dgDownload.HeaderRow.TableSection = TableRowSection.TableHeader;
                    TableCellCollection cells = dgDownload.HeaderRow.Cells;
                    cells[0].Attributes.Add("data-hide", "phone");
                    cells[1].Attributes.Add("data-class", "expand");
                    cells[2].Attributes.Add("data-hide", "phone");
                    cells[3].Attributes.Add("data-hide", "phone");
                    cells[4].Attributes.Add("data-hide", "phone");
                    cells[5].Attributes.Add("data-hide", "phone");
                    //  cells[6].Attributes.Add("data-hide", "phone,tablet");
                    //cells[7].Attributes.Add("data-hide", "phone,tablet");
                    //cells[8].Attributes.Add("data-hide", "phone,tablet");
                    //cells[9].Attributes.Add("data-hide", "phone,tablet");

                    // cells[15].Attributes.Add("data-hide", "phone,tablet");
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
                    objErr.LogErr(AppId, "CKYCDownload.aspx.cs", "dgDownload_RowCreated", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion

        #region GridView Set Pager ButtonStates
        public void SetPagerButtonStates(GridView gridView, GridViewRow gvPagerRow, Page page, string DDlPagerL, string DDlPagerR)
        {
            try
            {
                int pageIndexL = gridView.PageIndex;
                int pageCountL = gridView.PageCount;
                int pageIndexR = gridView.PageIndex;
                int pageCountR = gridView.PageCount;//Initialize the variables

                ImageButton btnFirstL = (ImageButton)gvPagerRow.FindControl("ImgbtnFirst");
                ImageButton btnPreviousL = (ImageButton)gvPagerRow.FindControl("ImgbtnPrevious");
                ImageButton btnNextL = (ImageButton)gvPagerRow.FindControl("ImgbtnNext");
                ImageButton btnLastL = (ImageButton)gvPagerRow.FindControl("ImgbtnLast");
                ImageButton btnFirstR = (ImageButton)gvPagerRow.FindControl("ImgbtnFirst1");
                ImageButton btnPreviousR = (ImageButton)gvPagerRow.FindControl("ImgbtnPrevious1");
                ImageButton btnNextR = (ImageButton)gvPagerRow.FindControl("ImgbtnNext1");
                ImageButton btnLastR = (ImageButton)gvPagerRow.FindControl("ImgbtnLast1");//Find the controls

                btnFirstL.Visible = btnPreviousL.Visible = (pageIndexL != 0);
                btnNextL.Visible = btnLastL.Visible = (pageIndexL < (pageCountL - 1));
                btnFirstR.Visible = btnPreviousR.Visible = (pageIndexR != 0);
                btnNextR.Visible = btnLastR.Visible = (pageIndexR < (pageCountR - 1));//Manage the Buttons according to page number

                DropDownList ddlPageSelectorL = (DropDownList)gvPagerRow.FindControl(DDlPagerL);
                ddlPageSelectorL.Items.Clear();
                DropDownList ddlPageSelectorR = (DropDownList)gvPagerRow.FindControl(DDlPagerR);
                ddlPageSelectorR.Items.Clear();//Find Dropdowns

                for (int i = 1; i <= gridView.PageCount; i++)
                {
                    ddlPageSelectorL.Items.Add(i.ToString());
                    ddlPageSelectorR.Items.Add(i.ToString());
                }//Fill those dropdowns

                ddlPageSelectorL.SelectedIndex = pageIndexL;
                ddlPageSelectorR.SelectedIndex = pageIndexR;
                //Initialize the dropdowns

                string strPgeIndx = Convert.ToString(gridView.PageIndex + 1) + " of "
                                    + gridView.PageCount.ToString();//Initialize the Page Information.

                Label lblpageindx = (Label)gvPagerRow.FindControl("lblpageindx");
                lblpageindx.Text += strPgeIndx;
                Label lblpageindx2 = (Label)gvPagerRow.FindControl("lblpageindx2");
                lblpageindx2.Text += strPgeIndx;
                //Fill the Page Information section
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(AppId, "CKYCDownload.aspx.cs", "SetPagerButtonStates", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
        }
        #endregion
    }
}