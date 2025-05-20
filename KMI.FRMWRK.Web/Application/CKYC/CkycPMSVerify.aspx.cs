using System;
using KMI.FRMWRK.DAL;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KMI.FRMWRK.Multilingual;
using System.IO;
using KMI.FRMWRK.Web.Admin;
using System.Collections.Generic;
using System.Linq;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class CkycPMSVerify : System.Web.UI.Page
    {
        #region Declarartion
        string InputsourcePath = @"C:\DEMO-PC\HOSTED-APPLICATIONS\CKYC\File\OutputFiles\ProbableMatch\";
        string InputtargetPath = @"C:\DEMO-PC\HOSTED-APPLICATIONS\CKYC\File\Output Archive\";
        DataTable dt;
        //private FTPCnt= new 
        FTPCnt C = new FTPCnt();
        string StrRemark = string.Empty;
        string strPathDoc = string.Empty;
        Hashtable hTable = new Hashtable();
        ErrorLog objErr = new ErrorLog();
        DataAccessLayer dataAccessLayer;
        private string Message = string.Empty;
        string strUsrrole = string.Empty;
        private MultilingualManager olng;
        private string strUserLang;
        string UserID = string.Empty;
        string msg = string.Empty;
        string strAppID = string.Empty;
        string strModuleID = string.Empty;
        string FlagPageTyp = "";
        string NewFilePath = string.Empty;
        string strSuccessMsg = string.Empty;
        DAL.DataAccessLayer objDal = new DAL.DataAccessLayer();
        string FileName = string.Empty;
        string FileNameToproc = string.Empty;
        Hashtable htParam = new Hashtable();
        DataSet dsResult = new DataSet();
        string filename = string.Empty;


        #endregion



        #region Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"] == null)
                {
                    Response.Redirect("~/ErrorSession.aspx", true);
                }
                if (Session["UserId"] != null)
                {
                    UserID = Session["UserId"].ToString();
                }
                if (Session["AppID"] != null)
                {
                    strAppID = Session["AppID"].ToString();
                }
                if (Session["ModuleID"] != null)
                {
                    strModuleID = Session["ModuleID"].ToString();
                }
                if (Request.QueryString["FlagPageTyp"] != null)
                {
                    FlagPageTyp = Request.QueryString["FlagPageTyp"].ToString();//added by shubham
                }

                olng = new MultilingualManager("DefaultConn", "CKYCSearch.aspx", Session["UserLangNum"].ToString());
                strUserLang = HttpContext.Current.Session["UserLangNum"].ToString();
                dt = GetPMSDataTableCKYC();
                ConvertData(dt);
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CkycPMSVerify.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion


        #region txtRemark_TextChanged Event 
        protected void txtRemark_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //TextBox txt = (TextBox)dgView.FindControl("TextBox");
                //String strRemark = txt.Text;
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CkycPMSVerify.aspx.cs", "chkMatch_CheckedChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region chkMatch_CheckedChanged Event 
        protected void chkMatch_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox ChkSub = (CheckBox)sender;
                string chk = ChkSub.ID.ToString();
                GridViewRow gv = (GridViewRow)ChkSub.NamingContainer;
                chk = (chk.Substring(chk.LastIndexOf("_"))).Substring(1);
                int colnumber = Convert.ToInt32(chk);
                int rownumber = gv.RowIndex;
                if (ChkSub.Checked == true)
                {
                    int i;
                    for (i = 2; i < dgView.Rows[0].Cells.Count; i++)
                    {
                        if (i != colnumber)
                        {
                            TextBox txtRemarks = ((TextBox)(dgView.Rows[1].Cells[i].FindControl("txt_" + i.ToString())));
                            txtRemarks.Enabled = false;
                            CheckBox chkcheckbox = ((CheckBox)(dgView.Rows[0].Cells[i].FindControl("chk_" + i.ToString())));
                            chkcheckbox.Enabled = false;
                        }
                    }
                    dgView.Rows[1].Visible = true;
                    ViewState["chkFlag"] = "Y";
                    divBindData.Visible = true;
                    btnComMatch.Enabled = true;
                    btnNoMatch.Enabled = true;
                    txtRefNo.Text = dgView.Rows[2].Cells[colnumber].Text;
                    txtName.Text = dgView.Rows[4].Cells[colnumber].Text;

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
                    objErr.LogErr(1, "CkycPMSVerify.aspx.cs", "chkMatch_CheckedChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region BindGrid event
        private void BindGrid(DataSet dt, bool rotate)
        {
            try
            {
                dgView.ShowHeader = !rotate;
                dgView.DataSource = dt;
                dgView.DataBind();
                if (divBindData.Visible == true)
                {
                    divBindData.Visible = false;
                    btnComMatch.Enabled = false;
                    btnNoMatch.Enabled = true;
                    ViewState["chkFlag"] = "N";

                }
                dgView.Rows[1].Visible = false;
                if (rotate)
                {
                    foreach (GridViewRow row in dgView.Rows)
                    {
                        row.Cells[0].CssClass = "GridViewtr";
                    }

                    for (int i = 2; i < dt.Tables[0].Columns.Count; i++)
                    {
                        Label lbl = new Label();
                        lbl.Text = "Probable Match " + Convert.ToString(i - 1);
                        dgView.Rows[0].Cells[i].Controls.Add(lbl);
                        CheckBox chkCheckBox = new CheckBox();
                        chkCheckBox.AutoPostBack = true;
                        chkCheckBox.ID = "chk_" + i.ToString();
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(chkCheckBox);
                        chkCheckBox.CheckedChanged += new EventHandler(chkMatch_CheckedChanged);
                        chkCheckBox.CssClass = "chk";
                        dgView.Rows[0].Cells[i].Controls.Add(chkCheckBox);

                        TextBox txtRemark = new TextBox();
                        txtRemark.AutoPostBack = true;
                        txtRemark.CssClass = "form-control";
                        txtRemark.MaxLength = 50;
                        txtRemark.Attributes.Add("runat", "server");
                        txtRemark.TextChanged += new EventHandler(txtRemark_TextChanged);
                        txtRemark.ID = "txt_" + i.ToString();
                        dgView.Rows[1].Cells[i].Controls.Add(txtRemark);
                        dgView.Rows[1].Visible = false;
                    }

                    for (int j = 0; j < dt.Tables[0].Rows.Count; j++)
                    {
                        if (((dgView.Rows[j].Cells[0].Text.Substring(dgView.Rows[j].Cells[0].Text.LastIndexOf(" "))).Substring(1)) == "Image")
                        {
                            for (int i = 1; i < dt.Tables[0].Columns.Count; i++)
                            {
                                if (dgView.Rows[j].Cells[i].Text.ToString() != "&nbsp;")
                                {
                                    System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image();
                                    image.ImageUrl = dgView.Rows[j].Cells[i].Text;
                                    image.Width = 70;
                                    image.Height = 80;
                                    image.CssClass = "image";
                                    dgView.Rows[j].Cells[i].Controls.Add(image);
                                }
                            }
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
                    objErr.LogErr(1, "CkycPMSVerify.aspx.cs", "BindGrid", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region ConvertData
        protected void ConvertData(DataTable dt)
        {
            try
            {
                DataTable dtw = new DataTable();
                DataSet dt2 = new DataSet();
                for (int i = 0; i <= dt.Rows.Count; i++)
                {
                    dtw.Columns.Add();
                }
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dtw.Rows.Add();
                    dtw.Rows[i][0] = dt.Columns[i].ColumnName;
                }
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j][i].ToString() != "" && ((dt.Columns[i].ColumnName.ToString().Substring(dt.Columns[i].ColumnName.ToString().LastIndexOf(" "))).Substring(1)) == "Image")
                        {
                            dtw.Rows[i][j + 1] = "data:image/jpg;base64," + Convert.ToBase64String((byte[])dt.Rows[j][i]);
                        }
                        else
                        {
                            dtw.Rows[i][j + 1] = dt.Rows[j][i];
                        }
                    }
                }
                dt2.Tables.Add(dtw);
                BindGrid(dt2, true);
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CkycPMSVerify.aspx.cs", "ConvertData", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region GetPMSDataTableCKYC
        protected DataTable GetPMSDataTableCKYC()
        {
            try
            {
                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt.Clear();
                hTable.Clear();

                hTable.Add("@RegRefNo", Request.QueryString["refno"].ToString().Trim());
                hTable.Add("@FlagPageTyp", FlagPageTyp);
                dt = dataAccessLayer.GetDataTable("Prc_getProbableMtch", hTable);
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CkycPMSVerify.aspx.cs", "GetPMSDataTableCKYC", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
            return dt;
        }
        #endregion

        #region btnCancel_Click event
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("CKYCSearch.aspx?status=PMS", false);
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CkycPMSVerify.aspx.cs", "btnCancel_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region btnNoMatch_Click Event
        protected void btnNoMatch_Click(object sender, EventArgs e)
        {
            try
            {

                //IN0730_25012019_RECON_1.2_00044_IA001292.txt NM
                string CndNo = "";
                string version = "V1.2";
                FileName = "IN2596" + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + "RECON" + "_" + version + "_";//+ hdnBatchId.Value;
                NewFilePath = BindDocument(CndNo, FileName, "DNDNM");

                #region Send Data to CERSAI
                Upload("DNDNM", NewFilePath);
                #endregion Send files to CERSAI 

                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt.Clear();
                hTable.Clear();
                hTable.Add("@RegRefNo", dgView.Rows[2].Cells[1].Text);
                hTable.Add("@UserId", HttpContext.Current.Session["UserId"].ToString());
                dataAccessLayer.ExecuteNonQuery("Prc_UpdCKYCRegistration", hTable);

                msg = "No Match Data Saved Successfully." + "</br></br>For CKYC Reference No. :" + " " + dgView.Rows[2].Cells[1].Text.ToString() + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "');", true);
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CkycPMSVerify.aspx.cs", "btnNoMatch_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion

        #region btnOk_Click Event
        protected void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("CKYCSearch.aspx?status=PMS", true);
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CkycPMSVerify.aspx.cs", "btnOk_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }
        #endregion

        #region btnComMatch_Click Event
        protected void btnComMatch_Click(object sender, EventArgs e)
        {
            try
            {
                string CndNo = "";
                FileName = "IN2596" + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + "RECON" + "_" + "1.2" +"_";//+ hdnBatchId.Value;
                NewFilePath = BindDocument(CndNo, FileName, "DNDCM");

                Upload("DNDCM", NewFilePath);

                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt.Clear();
                hTable.Clear();
                hTable.Add("@RegRefNo", dgView.Rows[2].Cells[1].Text);
                hTable.Add("@ProbableCKYCNo", txtRefNo.Text.ToString());
                hTable.Add("@UserId", HttpContext.Current.Session["UserId"].ToString());
                dataAccessLayer.ExecuteNonQuery("Prc_UpdCKYCRegistration", hTable);

                msg = "Match Data Saved Successfully." + "</br></br>For CKYC Reference No. :" + " " + dgView.Rows[2].Cells[1].Text.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "');", true);
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CkycPMSVerify.aspx.cs", "btnComMatch_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion

        private string BindDocument(string CndNo, string FileName, String StrDocCode)
        {
            try
            {
                //GET BATCHID
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                string strText = string.Empty;

                htParam.Clear();
                htParam.Add("@DocType", "DNDCM");
                string strBatchId = (string)objDal.ExecuteNonQuery("Prc_UpdtBatchId", "@Batch", htParam, "UpdDwnldConnectionString");

                htParam.Clear();
                htParam.Add("@flag", "1");
                htParam.Add("@Seqno", "1");
                htParam.Add("@DocType", "DNDCM");
                dsResult = dataAccessLayer.GetDataSet("Prc_getdata", htParam);
                if (dsResult.Tables.Count > 0)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        strPathDoc = dsResult.Tables[0].Rows[0]["Path1"].ToString().Trim();
                    }
                }

                dsResult.Clear();
                htParam.Clear();
                if (StrDocCode == "DNDCM")
                {
                    htParam.Add("@DocType", "DNDCM");
                }
                else if (StrDocCode == "DNDNM")
                {
                    htParam.Add("@DocType", "DNDNM");
                }
                htParam.Add("@Batchid", strBatchId.Trim());
                htParam.Add("@Userid", StrRemark);
                htParam.Add("FiNo", "");
                dsResult = dataAccessLayer.GetDataSet("Prc_GetDwnldRecordforCSV", htParam, "UpdDwnldConnectionString");

                if (dsResult.Tables.Count > 0)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        for (int cnt = 0; cnt < dsResult.Tables[0].Rows.Count; cnt++)
                        {
                            strText = strText + dsResult.Tables[0].Rows[cnt][1].ToString().Trim() + "|" + Environment.NewLine;
                            strText = strText + "\n";
                        }
                    }
                }

                FileNameToproc = FileName + strBatchId + "_IA004130.txt";
                string TrgFileName = FileName + strBatchId + "_IA004130";
                NewFilePath = strPathDoc + "\\" + FileNameToproc;
                System.IO.File.WriteAllText(strPathDoc + "\\" + FileNameToproc, strText);

                #region Generate .trg File
                genTrgFile(strPathDoc, TrgFileName);
                #endregion Generate .trg File
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CkycPMSVerify.aspx.cs", "btnComMatch_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            return strPathDoc;
        }

        #region code for Upload from ckyckmi-request to ckyccersai-requset
        public string Upload(string StrDocCode, string NewFilePath)
        {
            try
            {
                string strSrcFile = string.Empty;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                htParam.Clear();
                htParam.Add("@flag", "1");
                htParam.Add("@Seqno", "2");
                htParam.Add("@DocType", "DNDCM");
                dsResult = dataAccessLayer.GetDataSet("Prc_getdata", htParam);

                //if(dsResult.Tables.C)
                string strPath = dsResult.Tables[0].Rows[0]["Path1"].ToString().Trim();

                try
                {
                    if (Directory.Exists(NewFilePath))
                    {
                        foreach (var file in new DirectoryInfo(NewFilePath).GetFiles())
                        {
                            string path = InputtargetPath + file.Name;
                            if (!File.Exists(path))
                            {
                                file.CopyTo($@"{InputtargetPath}\{file.Name}");
                                //file.Delete();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    objErr.LogErr(1, "WinCKYCSFTP", "service", "Upload", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "");
                    throw ex;
                }

                strSuccessMsg = C.SyncUploadFolder(strPath, "DNDCM");

            }
            catch (Exception ex)
            {
                objErr.LogErr(0, "WinCKYCSFTP", "service", "Upload", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "");
                throw ex;

            }
            return strSuccessMsg;
        }
        #endregion

        #region  gernerate trigger file
        public string genTrgFile(string FilePath, string FileName)
        {
            FileStream fs;

            string strTrgFile = (FilePath + FileName + ".trg");
            fs = File.Create(strTrgFile);
            fs.Close();

            return "";
        }
        #endregion
    }
}