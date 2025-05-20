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
    public partial class ShortPortableSearch : System.Web.UI.Page
    {
        #region Declarartion
        DataTable dt;
        Hashtable hTable = new Hashtable();
        ErrorLog objErr = new ErrorLog();
        DataAccessLayer dataAccessLayer;
        private string Message = string.Empty;
        private MultilingualManager olng;
        private string strUserLang;
        string UserID = string.Empty;
        string msg = string.Empty;
        string strAppID = string.Empty;
        string strModuleID = string.Empty;
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
                    objErr.LogErr(1,  "ShortPortableSearch.aspx.cs", "Page_Load",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
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
                    btnNoMatch.Enabled = false;
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
                    objErr.LogErr(1,  "ShortPortableSearch.aspx.cs", "chkMatch_CheckedChanged",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion

        #region BindGrid
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
                        ScriptManager.GetCurrent(this).RegisterPostBackControl(chkCheckBox);
                        chkCheckBox.CheckedChanged += new EventHandler(chkMatch_CheckedChanged);
                        chkCheckBox.CssClass = "chk";
                        dgView.Rows[0].Cells[i].Controls.Add(chkCheckBox);

                        TextBox txtRemark = new TextBox();
                        txtRemark.CssClass = "form-control";
                        txtRemark.MaxLength = 50;
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
                    objErr.LogErr(1,  "ShortPortableSearch.aspx.cs", "BindGrid",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion

        #region ConvertData method
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
                    objErr.LogErr(1,  "ShortPortableSearch.aspx.cs", "ConvertData",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }

            }
            finally
            {
                dataAccessLayer = null;
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

                dt = dataAccessLayer.GetDataTable("Prc_getProbableShort", hTable);
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1,  "ShortPortableSearch.aspx.cs", "GetPMSDataTableCKYC",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
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
                    objErr.LogErr(1,  "ShortPortableSearch.aspx.cs", "btnCancel_Click",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
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
                    objErr.LogErr(1,  "ShortPortableSearch.aspx.cs", "btnOk_Click",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion

        #region btnComMatch_Click Event
        protected void btnComMatch_Click(object sender, EventArgs e)
        {
            try
            {
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
                    objErr.LogErr(1,  "ShortPortableSearch.aspx.cs", "btnComMatch_Click",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion

        #region btnNoMatch_Click Event
        protected void btnNoMatch_Click(object sender, EventArgs e)
        {
            try
            {
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
                    objErr.LogErr(1, "ShortPortableSearch.aspx.cs", "btnNoMatch_Click", ex.Message.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion
    }
}