using System;
using KMI.FRMWRK.DAL;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Web.Script.Serialization;
using System.Linq;


namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class CKYCEntDocUpld : System.Web.UI.Page
    {
        #region Declarartion
        DataAccessLayer dataAccessLayer;
        DataTable dt;
        DataSet ds;
        Hashtable hTable = new Hashtable();
        ErrorLog objErr = new ErrorLog();
        private string Message = string.Empty;
        string strUsrrole = string.Empty;

        string UserID = string.Empty;
        string strAppID = string.Empty;
        string strModuleID = string.Empty;
        string strFIRefNo = string.Empty;

        int BMaxImgSize1;
        string strPath;
        string strDocName = string.Empty;
        string strPhotoExt = string.Empty;
        private string strFileName1 = string.Empty;
        private string strFileName = string.Empty;
        int image_height;
        int image_width;
        int max_height;
        int max_width;
        byte[] data;
        string id = string.Empty;
        private string strDestPath = string.Empty;
        string strRefNo = string.Empty;
        string msg = string.Empty;
        #endregion

        #region Page_Load Events events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                

                if (Session["AppID"] != null)
                {
                    strAppID = Session["AppID"].ToString();
                }
                if (Session["ModuleID"] != null)
                {
                    strModuleID = Session["ModuleID"].ToString();
                }
                strRefNo = Request.QueryString["RefNo"].ToString().Trim();
                hdnRegRefNo.Value = Request.QueryString["RefNo"].ToString().Trim();
                if (!IsPostBack)
                {
                    hdnTabIndex.Value = "1";
                    FillData(strRefNo);
                    Bindgridview();

                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "fnSetTabs('" + hdnTabIndex.Value + "');", true);
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CKYCDocupld.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }

        }
        #endregion

        #region FillData
        protected void FillData(string strRefNo)
        {
            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            dt = null;
            hTable.Clear();
            hTable.Add("@RegRefNo", strRefNo);
            dt = dataAccessLayer.GetDataTable("Prc_getDOCUPDData", hTable);
            lblAdvNameValue.Text = dt.Rows[0]["FNAME"].ToString().Trim();
            lblRefNoValue.Text = dt.Rows[0]["FIRefNo"].ToString().Trim();//strRefNo;
            lblCreateDtValue.Text = dt.Rows[0]["DATECREATED"].ToString().Trim();
        }
        #endregion

        #region binding document
        private void Bindgridview()
        {
            try
            {
                ds = new DataSet();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                ds = null;
                hTable.Clear();
                hTable.Add("@TypeofDoc", "UPLD");
                hTable.Add("@RegRefNo", strRefNo);
                ds = dataAccessLayer.GetDataSet("Prc_GetDocNamesNew", hTable);// fuction getSearchData_Web replace by meena with Prc_GetDocNames
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dgView.DataSource = ds.Tables[0];
                        dgView.DataBind();
                        //gvRelPrsn.DataSource = dt;
                        //gvRelPrsn.DataBind();
                        //gvCtrlPrsn.DataSource = dt;
                        //gvCtrlPrsn.DataBind();
                        Label2.Attributes.Add("style", "display:block");
                        Label5.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        dgView.DataSource = null;
                        dgView.DataBind();
                        tblupload.Visible = false;
                        Label2.Attributes.Add("style", "display:none");
                        Label5.Attributes.Add("style", "display:block");
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        gvRelPrsn.DataSource = ds.Tables[1];
                        gvRelPrsn.DataBind();
                        //gvRelPrsn.DataSource = dt;
                        //gvRelPrsn.DataBind();
                        //gvCtrlPrsn.DataSource = dt;
                        //gvCtrlPrsn.DataBind();
                        Label1.Attributes.Add("style", "display:block");
                        Label6.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        gvRelPrsn.DataSource = null;
                        gvRelPrsn.DataBind();
                        tblupload.Visible = false;
                        Label1.Attributes.Add("style", "display:none");
                        Label6.Attributes.Add("style", "display:block");
                    }

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        gvCtrlPrsn.DataSource = ds.Tables[2];
                        gvCtrlPrsn.DataBind();
                        //gvRelPrsn.DataSource = dt;
                        //gvRelPrsn.DataBind();
                        //gvCtrlPrsn.DataSource = dt;
                        //gvCtrlPrsn.DataBind();
                        Label4.Attributes.Add("style", "display:block");
                        Label7.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        gvCtrlPrsn.DataSource = null;
                        gvCtrlPrsn.DataBind();
                        tblupload.Visible = false;
                        Label4.Attributes.Add("style", "display:none");
                        Label7.Attributes.Add("style", "display:block");
                    }
                }
                else
                {
                    dgView.DataSource = null;
                    dgView.DataBind();
                    gvRelPrsn.DataSource = null;
                    gvRelPrsn.DataBind();
                    gvCtrlPrsn.DataSource = null;
                    gvCtrlPrsn.DataBind();
                    tblupload.Visible = false;
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
                    objErr.LogErr(1, "CKYCDocupld.aspx.cs", "Bindgridview", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion

        #region dgView_RowCommand Event
        protected void dgView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Preview")
                {
                    ViewState["hdndoccode"] = "";
                    ViewState["DocName"] = "";
                    ViewState["EntRefNo"] = "";
                    string Preview = e.CommandArgument.ToString().Trim();
                    string RefNo = strRefNo;
                    lblDocDesc.Text = "";

                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                    Label lbldocName = (Label)row.FindControl("lbldocName");
                    Label lblEntRefNo = (Label)row.FindControl("lblEntRefNo");
                    Label lbldoccode = (Label)row.FindControl("lbldoccode");
                    ViewState["DocName"] = lbldocName.Text;
                    ViewState["EntRefNo"] = lblEntRefNo.Text;
                    hdnRegRefNo.Value = RefNo;
                    string ShowImage = string.Empty;
                    lblDocDesc.Text = lbldocName.Text;
                    dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                    dt = new DataTable();
                    dt = null;
                    hTable.Clear();
                    hTable.Add("@RegRefNo", strRefNo);
                    hTable.Add("@Flag", "2");
                    hTable.Add("@DocType", lbldocName.Text);
                    dt = dataAccessLayer.GetDataTable("Prc_GetDocNamesNew", hTable);

                    ViewState["hdndoccode"] = dt.Rows[0]["SR_NO"].ToString().Trim();
                    Byte[] bytes = (Byte[])dt.Rows[0]["IMAGE"];


                    System.Drawing.Image image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(bytes));
                    int height = image.Height;
                    int width = image.Width;
                    int total = height * width;
                    string MstWidth = dt.Rows[0]["ImgWidth"].ToString().Trim();
                    string MstHeight = dt.Rows[0]["ImgHeight"].ToString().Trim();
                    ZinSize.Value = total.ToString();

                    ZoutSize.Value = dt.Rows[0]["MaxImgSize"].ToString().Trim();
                    id = dt.Rows[0]["SR_NO"].ToString().Trim();
                    string Doccode = dt.Rows[0]["DOC_CODE"].ToString().Trim();
                    string Imgsrc = "ImageCSharp.aspx?ImageID=" + id;
                    string Doctype = dt.Rows[0]["DOC_NAME"].ToString().Trim();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "showimage(" + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + ", 1);", true);

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
                    objErr.LogErr(1, "CKYCDocupld.aspx.cs", "dgView_RowCommand", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        protected void gvRelPrsn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Preview")
                {
                    ViewState["hdndoccode1"] = "";
                    ViewState["DocName1"] = "";
                    ViewState["RelRefNo"] = "";
                    lblDocDesc.Text = "";
                    string Preview = e.CommandArgument.ToString().Trim();
                    string RefNo = strRefNo;

                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                    Label lbldocName = (Label)row.FindControl("lbldocName1");
                    Label lbldoccode = (Label)row.FindControl("lbldoccode1");
                    Label lblRefNo = (Label)row.FindControl("lblRelRefNo");
                    ViewState["DocName1"] = lbldocName.Text;
                    ViewState["RelRefNo"] = lblRefNo.Text;
                    hdnRegRefNo1.Value = lblRefNo.Text.ToString();
                    string ShowImage = string.Empty;
                    lblDocDesc.Text = lbldocName.Text;
                    dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                    dt = new DataTable();
                    dt = null;
                    hTable.Clear();
                    hTable.Add("@RegRefNo", lblRefNo.Text.ToString());
                    hTable.Add("@Flag", "2");
                    hTable.Add("@DocType", lbldocName.Text);
                    dt = dataAccessLayer.GetDataTable("Prc_GetDocNamesNew", hTable);

                    ViewState["hdndoccode1"] = dt.Rows[0]["SR_NO"].ToString().Trim();
                    Byte[] bytes = (Byte[])dt.Rows[0]["IMAGE"];


                    System.Drawing.Image image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(bytes));
                    int height = image.Height;
                    int width = image.Width;
                    int total = height * width;
                    string MstWidth = dt.Rows[0]["ImgWidth"].ToString().Trim();
                    string MstHeight = dt.Rows[0]["ImgHeight"].ToString().Trim();
                    ZinSize.Value = total.ToString();

                    ZoutSize.Value = dt.Rows[0]["MaxImgSize"].ToString().Trim();
                    id = dt.Rows[0]["SR_NO"].ToString().Trim();
                    string Doccode = dt.Rows[0]["DOC_CODE"].ToString().Trim();
                    string Imgsrc = "ImageCSharp.aspx?ImageID=" + id;
                    string Doctype = dt.Rows[0]["DOC_NAME"].ToString().Trim();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "showimage1(" + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + ", 1);", true);

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
                    objErr.LogErr(1, "CKYCDocupld.aspx.cs", "dgView_RowCommand", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        protected void gvCtrlPrsn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Preview")
                {
                    ViewState["hdndoccode2"] = "";
                    ViewState["DocName2"] = "";
                    ViewState["CtrlRefNo"] = "";
                    lblDocDesc.Text = "";
                    string Preview = e.CommandArgument.ToString().Trim();
                    string RefNo = strRefNo;

                    //lblRelRefNo2

                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                    Label lbldocName = (Label)row.FindControl("lbldocName2");
                    Label lbldoccode = (Label)row.FindControl("lbldoccode2");
                    Label lblRelRefNo = (Label)row.FindControl("lblRelRefNo2");
                    ViewState["DocName2"] = lbldocName.Text;
                    ViewState["CtrlRefNo"] = lblRelRefNo.Text;
                    hdnRegRefNo2.Value = lblRelRefNo.Text.ToString();
                    string ShowImage = string.Empty;
                    lblDocDesc.Text = lbldocName.Text;
                    dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                    dt = new DataTable();
                    dt = null;
                    hTable.Clear();
                    hTable.Add("@RegRefNo", lblRelRefNo.Text.ToString());
                    hTable.Add("@Flag", "2");
                    hTable.Add("@DocType", lbldocName.Text);
                    dt = dataAccessLayer.GetDataTable("Prc_GetDocNamesNew", hTable);

                    ViewState["hdndoccode2"] = dt.Rows[0]["SR_NO"].ToString().Trim();
                    Byte[] bytes = (Byte[])dt.Rows[0]["IMAGE"];


                    System.Drawing.Image image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(bytes));
                    int height = image.Height;
                    int width = image.Width;
                    int total = height * width;
                    string MstWidth = dt.Rows[0]["ImgWidth"].ToString().Trim();
                    string MstHeight = dt.Rows[0]["ImgHeight"].ToString().Trim();
                    ZinSize.Value = total.ToString();

                    ZoutSize.Value = dt.Rows[0]["MaxImgSize"].ToString().Trim();
                    id = dt.Rows[0]["SR_NO"].ToString().Trim();
                    string Doccode = dt.Rows[0]["DOC_CODE"].ToString().Trim();
                    string Imgsrc = "ImageCSharp.aspx?ImageID=" + id;
                    string Doctype = dt.Rows[0]["DOC_NAME"].ToString().Trim();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "showimage2(" + id + "," + Doccode + "," + height + "," + width + "," + ZinSize.Value + "," + ZoutSize.Value + "," + MstWidth + "," + MstHeight + ", 1);", true);

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
                    objErr.LogErr(1, "CKYCDocupld.aspx.cs", "dgView_RowCommand", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion

        #region dgView_RowDataBound Event
        protected void dgView_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblupdSize = (Label)e.Row.FindControl("lblupdSize");
                Label lblManDoc = (Label)e.Row.FindControl("lblManDoc");
                LinkButton btn_Upload = (LinkButton)e.Row.FindControl("btn_Upload");
                LinkButton btn_ReUpload = (LinkButton)e.Row.FindControl("btn_ReUpload");
                Label lbldoccode = (Label)e.Row.FindControl("lbldoccode");
                Label lbldocName = (Label)e.Row.FindControl("lbldocName");
                
                LinkButton lnkPreview = (LinkButton)e.Row.FindControl("lnkPreview");
                if (lblupdSize != null && lblupdSize.Text != "")
                {
                    int updsize = Convert.ToInt32(lblupdSize.Text);
                    int sizeupd = updsize / 1024;
                    lblupdSize.Text = Convert.ToString(sizeupd);
                }

                //string flag;
                //hTable.Clear();
                //hTable.Add("@RegRefNo", strRefNo);
                //dt = dataAccessLayer.GetDataTable("Prc_GetDocumentsNameByFlag", hTable);
                //flag = dt.Rows[0]["Flag"].ToString().Trim();

                //if (flag == "01")
                //{
                //    int[] rownum = { 0, 1, 2 };
                //    if (rownum.Contains(e.Row.RowIndex))
                //    {
                //        e.Row.BackColor = Color.LightPink;
                //    }
                //}
                //else if (flag == "02")
                //{
                //    int[] rownum = { 3, 4, 5, 6, 7, 8, 9 };
                //    if (rownum.Contains(e.Row.RowIndex))
                //    {
                //        e.Row.BackColor = Color.LightPink;
                //    }
                //}


                if (lblManDoc.Text == "Y")
                {
                    e.Row.BackColor = Color.LightPink;

                }

                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt.Clear();
                hTable.Clear();
                hTable.Add("@Flag", "1");
                hTable.Add("@RegRefNo", strRefNo.Trim());
                dt = dataAccessLayer.GetDataTable("Prc_GetDocNamesNew", hTable);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (lbldoccode.Text == dt.Rows[i]["DocCode"].ToString().Trim() && lbldocName.Text == dt.Rows[i]["ImgDesc01"].ToString().Trim())
                        {
                            btn_Upload.Enabled = true;
                            btn_ReUpload.Enabled = false;
                            btn_Upload.Visible = true;
                            btn_ReUpload.Visible = false;
                            lnkPreview.Visible = false;
                        }
                    }
                }
            }
        }
        #endregion


        #region gvRelPrsn_RowDataBound Event
        protected void gvRelPrsn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblupdSize = (Label)e.Row.FindControl("lblupdSize1");
                Label lblManDoc = (Label)e.Row.FindControl("lblManDoc1");
                LinkButton btn_Upload = (LinkButton)e.Row.FindControl("btn_Upload1");
                LinkButton btn_ReUpload = (LinkButton)e.Row.FindControl("btn_ReUpload1");
                Label lbldoccode = (Label)e.Row.FindControl("lbldoccode1");
                LinkButton lnkPreview = (LinkButton)e.Row.FindControl("lnkPreview1");
                Label lblRelRefNo = (Label)e.Row.FindControl("lblRelRefNo");
                
                if (lblupdSize != null && lblupdSize.Text != "")
                {
                    int updsize = Convert.ToInt32(lblupdSize.Text);
                    int sizeupd = updsize / 1024;
                    lblupdSize.Text = Convert.ToString(sizeupd);
                }

                //string strFIRefNo3 = GetStrFIRefNo(strRefNo, "2");

                //string flag;
                //hTable.Clear();
                //hTable.Add("@RegRefNo", strRefNo);
                //dt = dataAccessLayer.GetDataTable("Prc_GetDocumentsNameByFlag", hTable);
                //flag = dt.Rows[0]["Flag"].ToString().Trim();

                //if (flag == "01")
                //{
                //    int[] rownum = { 0, 1, 2 };
                //    if (rownum.Contains(e.Row.RowIndex))
                //    {
                //        e.Row.BackColor = Color.LightPink;
                //    }
                //}
                //else if (flag == "02")
                //{
                //    int[] rownum = { 3, 4, 5, 6, 7, 8, 9 };
                //    if (rownum.Contains(e.Row.RowIndex))
                //    {
                //        e.Row.BackColor = Color.LightPink;
                //    }
                //}


                if (lblManDoc.Text == "Y")
                {
                    e.Row.BackColor = Color.LightPink;

                }

                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt.Clear();
                hTable.Clear();
                hTable.Add("@Flag", "4");
                hTable.Add("@RegRefNo", lblRelRefNo.Text.Trim());
                dt = dataAccessLayer.GetDataTable("Prc_GetDocNamesNew", hTable);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (lbldoccode.Text == dt.Rows[i]["DocCode"].ToString().Trim())
                        {
                            btn_Upload.Enabled = true;
                            btn_ReUpload.Enabled = false;
                            btn_Upload.Visible = true;
                            btn_ReUpload.Visible = false;
                            lnkPreview.Visible = false;
                        }
                    }
                }
            }
        }
        #endregion

        #region gvCtrlPrsn_RowDataBound Event
        protected void gvCtrlPrsn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblupdSize = (Label)e.Row.FindControl("lblupdSize2");
                Label lblManDoc = (Label)e.Row.FindControl("lblManDoc2");
                LinkButton btn_Upload = (LinkButton)e.Row.FindControl("btn_Upload2");
                LinkButton btn_ReUpload = (LinkButton)e.Row.FindControl("btn_ReUpload2");
                Label lbldoccode = (Label)e.Row.FindControl("lbldoccode2");
                LinkButton lnkPreview = (LinkButton)e.Row.FindControl("lnkPreview2");
                Label lblRelRefNo = (Label)e.Row.FindControl("lblRelRefNo2");
                if (lblupdSize != null && lblupdSize.Text != "")
                {
                    int updsize = Convert.ToInt32(lblupdSize.Text);
                    int sizeupd = updsize / 1024;
                    lblupdSize.Text = Convert.ToString(sizeupd);
                }

                //string flag;
                //hTable.Clear();
                //hTable.Add("@RegRefNo", strRefNo);
                //dt = dataAccessLayer.GetDataTable("Prc_GetDocumentsNameByFlag", hTable);
                //flag = dt.Rows[0]["Flag"].ToString().Trim();

                //if (flag == "01")
                //{
                //    int[] rownum = { 0, 1, 2 };
                //    if (rownum.Contains(e.Row.RowIndex))
                //    {
                //        e.Row.BackColor = Color.LightPink;
                //    }
                //}
                //else if (flag == "02")
                //{
                //    int[] rownum = { 3, 4, 5, 6, 7, 8, 9 };
                //    if (rownum.Contains(e.Row.RowIndex))
                //    {
                //        e.Row.BackColor = Color.LightPink;
                //    }
                //}


                if (lblManDoc.Text == "Y")
                {
                    e.Row.BackColor = Color.LightPink;

                }

                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt.Clear();
                hTable.Clear();
                hTable.Add("@Flag", "4");
                hTable.Add("@RegRefNo", lblRelRefNo.Text.Trim());
                dt = dataAccessLayer.GetDataTable("Prc_GetDocNamesNew", hTable);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (lbldoccode.Text == dt.Rows[i]["DocCode"].ToString().Trim())
                        {
                            btn_Upload.Enabled = true;
                            btn_ReUpload.Enabled = false;
                            btn_Upload.Visible = true;
                            btn_ReUpload.Visible = false;
                            lnkPreview.Visible = false;
                        }
                    }
                }
            }
        }
        #endregion

        #region btn_Upload_Click Event
        protected void GetTabSelected(string strTabNo)
        {
            //if (strTabNo == "1")
            //{
            //    liSec1.Attributes.Add("class", "active");
            //    liSec2.Attributes.Add("class", "");
            //    liSec3.Attributes.Add("class", "");
            //    Section1.Attributes.Add("class", "tab-pane fade in active");
            //    Section2.Attributes.Add("class", "tab-pane fade");
            //    Section3.Attributes.Add("class", "tab-pane fade");
            //}

            //if (strTabNo == "2")
            //{
            //    liSec1.Attributes.Add("class", "");
            //    liSec2.Attributes.Add("class", "active");
            //    liSec3.Attributes.Add("class", "");
            //    Section1.Attributes.Add("class", "tab-pane fade");
            //    Section2.Attributes.Add("class", "tab-pane fade in active");
            //    Section3.Attributes.Add("class", "tab-pane fade");
            //}

            //if (strTabNo == "3")
            //{
            //    liSec1.Attributes.Add("class", "");
            //    liSec2.Attributes.Add("class", "");
            //    liSec3.Attributes.Add("class", "active");
            //    Section1.Attributes.Add("class", "tab-pane fade");
            //    Section2.Attributes.Add("class", "tab-pane fade");
            //    Section3.Attributes.Add("class", "tab-pane fade in active");
            //}
        }

        protected void btn_Upload_Click(object sender, EventArgs e)
        {
            SetPath(); //for doc upload path

            strFIRefNo = GetStrFIRefNo(strRefNo, "1");

            GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
            FileUpload fuData = (FileUpload)row.FindControl("FileUpload");
            Label lbldocName = (Label)row.FindControl("lbldocName");
            Label lblUpldBy = (Label)row.FindControl("lblUpldBy");
            Label lblUpdDtTm = (Label)row.FindControl("lblUpdDtTm");
            Label lblFileName = (Label)row.FindControl("lblFileName");
            Label lblimgsize1 = (Label)row.FindControl("lblimgsize");
            Label lblimgshrt1 = (Label)row.FindControl("lblimgshrt");
            Label lblimgwidth1 = (Label)row.FindControl("lblimgwidth");
            Label lblimgheight1 = (Label)row.FindControl("lblimgheight");
            Label lbldoccode1 = (Label)row.FindControl("lbldoccode");
            LinkButton lnkPreview = (LinkButton)row.FindControl("lnkPreview");
            LinkButton btnreupd = (LinkButton)row.FindControl("btn_ReUpload");
            LinkButton btn_Upload = (LinkButton)row.FindControl("btn_Upload");
            BMaxImgSize1 = Convert.ToInt32(lblimgsize1.Text);
            string strFileRePath = string.Empty;
            if (Directory.Exists(strPath) == false)
            {
                strPath = strPath + strFIRefNo.Trim();
                Directory.CreateDirectory(strPath);
            }
            else
            {
                strFileRePath = strPath + strFIRefNo.Trim();
                if (!Directory.Exists(strFileRePath))
                {
                    Directory.CreateDirectory(strFileRePath);
                }
                else
                {
                    strFileRePath = strPath + strFIRefNo.Trim();
                }
            }
            #region ReUpload

            if (fuData.HasFile)
            {
                if (fuData.HasFile)
                {
                    strDocName = fuData.PostedFile.FileName;
                    strPhotoExt = strDocName.Substring(strDocName.LastIndexOf('.') + 1).ToUpper();
                }
            }
            else
            {
                lbl.Text = "";
                lbl.Text = "Please select " + lbldocName.Text + " file for upload.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select " + lbldocName.Text + " file for upload');", true);
                return;
            }
            if (strPhotoExt == "JPG" || strPhotoExt == "jpg")
            {
                strFileName1 = strFIRefNo.Trim() + "_" + lbldocName.Text + "." + strPhotoExt;
                strFileName = strFileRePath + "\\" + strFileName1;
            }
            else if (strPhotoExt == "GIF" || strPhotoExt == "gif")
            {

                strFileName1 = strFIRefNo.Trim() + "_" + lbldocName.Text + "." + strPhotoExt;
                strFileName = strFileRePath + "\\" + strFileName1;
            }
            else if (strPhotoExt == "JPEG" || strPhotoExt == "jpeg")
            {
                strFileName1 = strFIRefNo.Trim() + "_" + lbldocName.Text + "." + strPhotoExt;
                strFileName = strFileRePath + "\\" + strFileName1;
            }
            else
            {
                lbl.Text = "";
                lbl.Text = "Invalid file format.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Invalid file format');", true);
                return;
            }

            //pranj
            System.Drawing.Image image_file = System.Drawing.Image.FromStream(fuData.PostedFile.InputStream);
            if (strPhotoExt != string.Empty)
            {

                image_height = image_file.Height;
                image_width = image_file.Width;
                //Set image height and width to panel height and width iff the image is greater than panel dimensions start
                if ((image_height > Convert.ToInt32(lblimgheight1.Text) && image_width > Convert.ToInt32(lblimgwidth1.Text))
                            || (image_height > Convert.ToInt32(lblimgheight1.Text) || image_width > Convert.ToInt32(lblimgwidth1.Text)))
                {
                    max_height = Convert.ToInt32(lblimgheight1.Text);
                    max_width = Convert.ToInt32(lblimgwidth1.Text);
                }
                else
                {
                    max_height = image_height;
                    max_width = image_width;
                }
                //Set image height and width to panel height and width iff the image is greater than panel dimensions end

                image_height = (image_height * max_width) / image_width;
                image_width = max_width;

                if (image_height > max_height)
                {
                    image_width = (image_width * max_height) / image_height;
                    image_height = max_height;
                }
                else
                {
                }
                Bitmap bitmap_file = new Bitmap(image_file, image_width, image_height);
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                bitmap_file.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Position = 0;

                data = new byte[stream.Length + 1];
                stream.Read(data, 0, data.Length);
            }

            else
            {
                lbl.Text = "";
                lbl.Text = "Please upload an image.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //var message = new JavaScriptSerializer().Serialize("Please Upload an image.");
                //var script = string.Format("alert({0});", message);
                //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
                return;

            }

            FileInfo fi = new FileInfo(strPath);
            {
                if (fuData.PostedFile.ContentLength <= BMaxImgSize1)
                {
                    if (File.Exists(strFileName))
                    {
                        string stroldpath = strFileRePath + "\\" + strFileName1;
                        string[] strfile = strFileName1.Split('.');
                        string ImageNamenew = strfile[0];

                        string strnewpath = strFileRePath + "\\" + ImageNamenew + "_R" + "." + strPhotoExt;
                        System.IO.File.Copy(stroldpath, strnewpath, true);
                    }
                }
                else
                {
                    int SIZE1 = BMaxImgSize1 / 1024;
                    lbl.Text = "";
                    lbl.Text = "Max file size should be less than " + SIZE1 + "Kb.";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Max File size should be less than " + SIZE1 + "Kb');", true);
                    return;
                }
            }

            #endregion

            strDestPath = System.IO.Path.Combine(strFileRePath, strFileName);

            fuData.PostedFile.SaveAs((strFileName));
            string str1 = strFileName.Replace(@"\", @"/");
            string[] actualpath = str1.Split('/');
            strFileName = actualpath[0] + "\\" + actualpath[1] + "\\" + actualpath[3];

            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            dt.Clear();
            hTable.Clear();
            hTable.Add("@RegRefNo", strRefNo.Trim());
            hTable.Add("@DocType", lbldocName.Text.Trim());
            hTable.Add("@UserID", HttpContext.Current.Session["UserID"].ToString().Trim());
            hTable.Add("@Imagebin", data);
            hTable.Add("@DocCode", lbldoccode1.Text.Trim());
            try
            {
                dt = dataAccessLayer.GetDataTable("Proc_InsertDocReUpload", hTable); 

            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CKYCDocupld.aspx.cs", "btn_Upload_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                //GetTabSelected("1");
                dataAccessLayer = null;
                fuData.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                btnreupd.Enabled = true;
                btnreupd.Visible = true;
                btn_Upload.Enabled = false;
                btn_Upload.Visible = false;
                lnkPreview.Visible = true;
                string Docname = lbldocName.Text;

                //liSec1.CssClass = "hom_but_a";
            }

        }

        protected void btn_Upload_Click1(object sender, EventArgs e)
        {
            SetPath(); //for doc upload path

            GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
            FileUpload fuData = (FileUpload)row.FindControl("FileUpload1");
            Label lblRelRefNo = (Label)row.FindControl("lblRelRefNo");
            Label lbldocName = (Label)row.FindControl("lbldocName1");
            Label lblUpldBy = (Label)row.FindControl("lblUpldBy1");
            Label lblUpdDtTm = (Label)row.FindControl("lblUpdDtTm1");
            Label lblFileName = (Label)row.FindControl("lblFileName1");
            Label lblimgsize1 = (Label)row.FindControl("lblimgsize1");
            Label lblimgshrt1 = (Label)row.FindControl("lblimgshrt1");
            Label lblimgwidth1 = (Label)row.FindControl("lblimgwidth1");
            Label lblimgheight1 = (Label)row.FindControl("lblimgheight1");
            Label lbldoccode1 = (Label)row.FindControl("lbldoccode1");
            LinkButton lnkPreview = (LinkButton)row.FindControl("lnkPreview1");
            LinkButton btnreupd = (LinkButton)row.FindControl("btn_ReUpload1");
            LinkButton btn_Upload = (LinkButton)row.FindControl("btn_Upload1");
            BMaxImgSize1 = Convert.ToInt32(lblimgsize1.Text);
            string strFileRePath = string.Empty;

            strFIRefNo = GetStrFIRefNo(strRefNo, "1");

            string strFIRefNo1 = GetStrFIRefNo(lblRelRefNo.Text.ToString(), "2");

            if (Directory.Exists(strPath) == false)
            {
                strPath = strPath + strFIRefNo.Trim();
                Directory.CreateDirectory(strPath);
            }
            else
            {
                strFileRePath = strPath + strFIRefNo.Trim();
                if (!Directory.Exists(strFileRePath))
                {
                    Directory.CreateDirectory(strFileRePath);
                }
                else
                {
                    strFileRePath = strPath + strFIRefNo.Trim();
                }
            }
            #region ReUpload

            if (fuData.HasFile)
            {
                if (fuData.HasFile)
                {
                    strDocName = fuData.PostedFile.FileName;
                    strPhotoExt = strDocName.Substring(strDocName.LastIndexOf('.') + 1).ToUpper();
                }
            }
            else
            {
                lbl.Text = "";
                lbl.Text = "Please select " + lbldocName.Text + " file for upload.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select " + lbldocName.Text + " file for upload');", true);
                return;
            }
            if (strPhotoExt == "JPG" || strPhotoExt == "jpg")
            {
                strFileName1 = strFIRefNo1.Trim() + "_" + lbldocName.Text + "." + strPhotoExt;
                strFileName = strFileRePath + "\\" + strFileName1;
            }
            else if (strPhotoExt == "GIF" || strPhotoExt == "gif")
            {

                strFileName1 = strFIRefNo1.Trim() + "_" + lbldocName.Text + "." + strPhotoExt;
                strFileName = strFileRePath + "\\" + strFileName1;
            }
            else if (strPhotoExt == "JPEG" || strPhotoExt == "jpeg")
            {
                strFileName1 = strFIRefNo1.Trim() + "_" + lbldocName.Text + "." + strPhotoExt;
                strFileName = strFileRePath + "\\" + strFileName1;
            }
            else
            {
                lbl.Text = "";
                lbl.Text = "Invalid file format.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Invalid file format');", true);
                return;
            }

            //pranj
            System.Drawing.Image image_file = System.Drawing.Image.FromStream(fuData.PostedFile.InputStream);
            if (strPhotoExt != string.Empty)
            {

                image_height = image_file.Height;
                image_width = image_file.Width;
                //Set image height and width to panel height and width iff the image is greater than panel dimensions start
                if ((image_height > Convert.ToInt32(lblimgheight1.Text) && image_width > Convert.ToInt32(lblimgwidth1.Text))
                            || (image_height > Convert.ToInt32(lblimgheight1.Text) || image_width > Convert.ToInt32(lblimgwidth1.Text)))
                {
                    max_height = Convert.ToInt32(lblimgheight1.Text);
                    max_width = Convert.ToInt32(lblimgwidth1.Text);
                }
                else
                {
                    max_height = image_height;
                    max_width = image_width;
                }
                //Set image height and width to panel height and width iff the image is greater than panel dimensions end

                image_height = (image_height * max_width) / image_width;
                image_width = max_width;

                if (image_height > max_height)
                {
                    image_width = (image_width * max_height) / image_height;
                    image_height = max_height;
                }
                else
                {
                }
                Bitmap bitmap_file = new Bitmap(image_file, image_width, image_height);
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                bitmap_file.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Position = 0;

                data = new byte[stream.Length + 1];
                stream.Read(data, 0, data.Length);
            }

            else
            {
                lbl.Text = "";
                lbl.Text = "Please upload an image.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //var message = new JavaScriptSerializer().Serialize("Please Upload an image");
                //var script = string.Format("alert({0});", message);
                //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
                return;

            }

            FileInfo fi = new FileInfo(strPath);
            {
                if (fuData.PostedFile.ContentLength <= BMaxImgSize1)
                {
                    if (File.Exists(strFileName))
                    {
                        string stroldpath = strFileRePath + "\\" + strFileName1;
                        string[] strfile = strFileName1.Split('.');
                        string ImageNamenew = strfile[0];

                        string strnewpath = strFileRePath + "\\" + ImageNamenew + "_R" + "." + strPhotoExt;
                        System.IO.File.Copy(stroldpath, strnewpath, true);
                    }
                }
                else
                {
                    int SIZE1 = BMaxImgSize1 / 1024;
                    lbl.Text = "";
                    lbl.Text = "Max File size should be less than " + SIZE1 + "Kb.";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Max File size should be less than " + SIZE1 + "Kb');", true);
                    return;
                }
            }

            #endregion

            strDestPath = System.IO.Path.Combine(strFileRePath, strFileName);

            fuData.PostedFile.SaveAs((strFileName));
            string str1 = strFileName.Replace(@"\", @"/");
            string[] actualpath = str1.Split('/');
            strFileName = actualpath[0] + "\\" + actualpath[1] + "\\" + actualpath[3];

            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            dt.Clear();
            hTable.Clear();
            hTable.Add("@RegRefNo", lblRelRefNo.Text.Trim());
            hTable.Add("@DocType", lbldocName.Text.Trim());
            hTable.Add("@UserID", HttpContext.Current.Session["UserID"].ToString().Trim());
            hTable.Add("@Imagebin", data);
            hTable.Add("@DocCode", lbldoccode1.Text.Trim());
            try
            {
                dt = dataAccessLayer.GetDataTable("Proc_InsertDocReUpload", hTable);

            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CKYCDocupld.aspx.cs", "btn_Upload_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                //GetTabSelected("2");
                dataAccessLayer = null;
                fuData.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                btnreupd.Enabled = true;
                btnreupd.Visible = true;
                btn_Upload.Enabled = false;
                btn_Upload.Visible = false;
                lnkPreview.Visible = true;
                string Docname = lbldocName.Text;
            }

        }

        protected void btn_Upload_Click2(object sender, EventArgs e)
        {
            SetPath(); //for doc upload path

            GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
            FileUpload fuData = (FileUpload)row.FindControl("FileUpload2");  //lblRelRefNo2
            Label lblRelRefNo2 = (Label)row.FindControl("lblRelRefNo2");
            Label lbldocName = (Label)row.FindControl("lbldocName2");
            Label lblUpldBy = (Label)row.FindControl("lblUpldBy2");
            Label lblUpdDtTm = (Label)row.FindControl("lblUpdDtTm2");
            Label lblFileName = (Label)row.FindControl("lblFileName2");
            Label lblimgsize1 = (Label)row.FindControl("lblimgsize2");
            Label lblimgshrt1 = (Label)row.FindControl("lblimgshrt2");
            Label lblimgwidth1 = (Label)row.FindControl("lblimgwidth2");
            Label lblimgheight1 = (Label)row.FindControl("lblimgheight2");
            Label lbldoccode1 = (Label)row.FindControl("lbldoccode2");
            LinkButton lnkPreview = (LinkButton)row.FindControl("lnkPreview2");
            LinkButton btnreupd = (LinkButton)row.FindControl("btn_ReUpload2");
            LinkButton btn_Upload = (LinkButton)row.FindControl("btn_Upload2");
            BMaxImgSize1 = Convert.ToInt32(lblimgsize1.Text);
            string strFileRePath = string.Empty;

            strFIRefNo = GetStrFIRefNo(strRefNo, "1");
            string strFIRefNo2 = GetStrFIRefNo(lblRelRefNo2.Text.ToString(), "3");

            if (Directory.Exists(strPath) == false)
            {
                strPath = strPath + strFIRefNo.Trim();
                Directory.CreateDirectory(strPath);
            }
            else
            {
                strFileRePath = strPath + strFIRefNo.Trim();
                if (!Directory.Exists(strFileRePath))
                {
                    Directory.CreateDirectory(strFileRePath);
                }
                else
                {
                    strFileRePath = strPath + strFIRefNo.Trim();
                }
            }
            #region ReUpload

            if (fuData.HasFile)
            {
                if (fuData.HasFile)
                {
                    strDocName = fuData.PostedFile.FileName;
                    strPhotoExt = strDocName.Substring(strDocName.LastIndexOf('.') + 1).ToUpper();
                }
            }
            else
            {
                lbl.Text = "";
                lbl.Text = "Please select " + lbldocName.Text + " file for upload.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select " + lbldocName.Text + " file for upload');", true);
                return;
            }
            if (strPhotoExt == "JPG" || strPhotoExt == "jpg")
            {
                strFileName1 = strFIRefNo2.Trim() + "_" + lbldocName.Text + "." + strPhotoExt;
                strFileName = strFileRePath + "\\" + strFileName1;
            }
            else if (strPhotoExt == "GIF" || strPhotoExt == "gif")
            {

                strFileName1 = strFIRefNo2.Trim() + "_" + lbldocName.Text + "." + strPhotoExt;
                strFileName = strFileRePath + "\\" + strFileName1;
            }
            else if (strPhotoExt == "JPEG" || strPhotoExt == "jpeg")
            {
                strFileName1 = strFIRefNo2.Trim() + "_" + lbldocName.Text + "." + strPhotoExt;
                strFileName = strFileRePath + "\\" + strFileName1;
            }
            else
            {
                lbl.Text = "";
                lbl.Text = "Invalid file format.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Invalid file format');", true);
                return;
            }

            //pranj
            System.Drawing.Image image_file = System.Drawing.Image.FromStream(fuData.PostedFile.InputStream);
            if (strPhotoExt != string.Empty)
            {

                image_height = image_file.Height;
                image_width = image_file.Width;
                //Set image height and width to panel height and width iff the image is greater than panel dimensions start
                if ((image_height > Convert.ToInt32(lblimgheight1.Text) && image_width > Convert.ToInt32(lblimgwidth1.Text))
                            || (image_height > Convert.ToInt32(lblimgheight1.Text) || image_width > Convert.ToInt32(lblimgwidth1.Text)))
                {
                    max_height = Convert.ToInt32(lblimgheight1.Text);
                    max_width = Convert.ToInt32(lblimgwidth1.Text);
                }
                else
                {
                    max_height = image_height;
                    max_width = image_width;
                }
                //Set image height and width to panel height and width iff the image is greater than panel dimensions end

                image_height = (image_height * max_width) / image_width;
                image_width = max_width;

                if (image_height > max_height)
                {
                    image_width = (image_width * max_height) / image_height;
                    image_height = max_height;
                }
                else
                {
                }
                Bitmap bitmap_file = new Bitmap(image_file, image_width, image_height);
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                bitmap_file.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Position = 0;

                data = new byte[stream.Length + 1];
                stream.Read(data, 0, data.Length);
            }

            else
            {
                lbl.Text = "";
                lbl.Text = "Please upload an image.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //var message = new JavaScriptSerializer().Serialize("Please Upload an image");
                //var script = string.Format("alert({0});", message);
                //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
                return;

            }

            FileInfo fi = new FileInfo(strPath);
            {
                if (fuData.PostedFile.ContentLength <= BMaxImgSize1)
                {
                    if (File.Exists(strFileName))
                    {
                        string stroldpath = strFileRePath + "\\" + strFileName1;
                        string[] strfile = strFileName1.Split('.');
                        string ImageNamenew = strfile[0];

                        string strnewpath = strFileRePath + "\\" + ImageNamenew + "_R" + "." + strPhotoExt;
                        System.IO.File.Copy(stroldpath, strnewpath, true);
                    }
                }
                else
                {
                    int SIZE1 = BMaxImgSize1 / 1024;
                    lbl.Text = "";
                    lbl.Text = "Max File size should be less than " + SIZE1 + "Kb.";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Max File size should be less than " + SIZE1 + "Kb');", true);
                    return;
                }
            }

            #endregion

            strDestPath = System.IO.Path.Combine(strFileRePath, strFileName);

            fuData.PostedFile.SaveAs((strFileName));
            string str1 = strFileName.Replace(@"\", @"/");
            string[] actualpath = str1.Split('/');
            strFileName = actualpath[0] + "\\" + actualpath[1] + "\\" + actualpath[3];

            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            dt.Clear();
            hTable.Clear();
            hTable.Add("@RegRefNo", lblRelRefNo2.Text.Trim());
            hTable.Add("@DocType", lbldocName.Text.Trim());
            hTable.Add("@UserID", HttpContext.Current.Session["UserID"].ToString().Trim());
            hTable.Add("@Imagebin", data);
            hTable.Add("@DocCode", lbldoccode1.Text.Trim());
            try
            {
                dt = dataAccessLayer.GetDataTable("Proc_InsertDocReUpload", hTable);

            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CKYCDocupld.aspx.cs", "btn_Upload_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                //GetTabSelected("3");
                dataAccessLayer = null;
                fuData.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                btnreupd.Enabled = true;
                btnreupd.Visible = true;
                btn_Upload.Enabled = false;
                btn_Upload.Visible = false;
                lnkPreview.Visible = true;
                string Docname = lbldocName.Text;
            }

        }
        #endregion

        #region Get FI Ref.No. Event
        protected string GetStrFIRefNo(string strReferenceNo, string strFlag)
        {
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt.Clear();
                hTable.Clear();
                hTable.Add("@RefNo", strReferenceNo.ToString().Trim());
                hTable.Add("@Flag", strFlag.ToString().Trim());
                dt = dataAccessLayer.GetDataTable("prc_GetFIRefNo", hTable);
                string strFIRefNo = dt.Rows[0]["FIRefNo"].ToString();
                return strFIRefNo;
        }

        #endregion

        #region btn_ReUpload_Click Event
        protected void btn_ReUpload_Click(object sender, EventArgs e)
        {
            SetPath();

            strFIRefNo = GetStrFIRefNo(strRefNo, "1");



            GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
            FileUpload fuData = (FileUpload)row.FindControl("FileUpload");
            
            Label lbldocName = (Label)row.FindControl("lbldocName");
            Label lblUpldBy = (Label)row.FindControl("lblUpldBy");
            Label lblUpdDtTm = (Label)row.FindControl("lblUpdDtTm");
            Label lblFileName = (Label)row.FindControl("lblFileName");
            Label lblimgsize1 = (Label)row.FindControl("lblimgsize");
            Label lblimgshrt1 = (Label)row.FindControl("lblimgshrt");
            Label lblimgwidth1 = (Label)row.FindControl("lblimgwidth");
            Label lblimgheight1 = (Label)row.FindControl("lblimgheight");
            Label lbldoccode1 = (Label)row.FindControl("lbldoccode");
            LinkButton btnreupd = (LinkButton)row.FindControl("btn_ReUpload");
            LinkButton btn_Upload = (LinkButton)row.FindControl("btn_Upload");
            BMaxImgSize1 = Convert.ToInt32(lblimgsize1.Text);
            string strFileRePath = string.Empty;
            if (Directory.Exists(strPath) == false)
            {
                strPath = strPath + strFIRefNo.Trim();
                Directory.CreateDirectory(strPath);
            }
            else
            {
                strFileRePath = strPath + strFIRefNo.Trim();
                //if (!Directory.Exists(Server.MapPath(strFilePath)))
                if (!Directory.Exists(strFileRePath))
                {
                    // Directory.sCreateDirectory(Server.MapPath(strFilePath));
                    Directory.CreateDirectory(strFileRePath);
                }
                else
                {
                    strFileRePath = strPath + strFIRefNo.Trim();
                }
            }


            #region ReUpload


            //Akash End

            if (fuData.HasFile)
            {
                if (fuData.HasFile)
                {
                    strDocName = fuData.PostedFile.FileName;
                    strPhotoExt = strDocName.Substring(strDocName.LastIndexOf('.') + 1).ToUpper();
                }
            }
            else
            {
                lbl.Text = "";
                lbl.Text = "Please select " + lbldocName.Text + " file for ReUpload.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select " + lbldocName.Text + " file for reupload');", true);

                return;
            }
            if (strPhotoExt == "JPG" || strPhotoExt == "jpg")
            {
                strFileName1 = strFIRefNo.Trim() + "_" + lbldocName.Text + "." + strPhotoExt;
                strFileName = strFileRePath + "\\" + strFileName1;
            }
            else if (strPhotoExt == "GIF" || strPhotoExt == "gif")
            {

                strFileName1 = strFIRefNo.Trim() + "_" + lbldocName.Text + "." + strPhotoExt;
                strFileName = strFileRePath + "\\" + strFileName1;
            }
            else if (strPhotoExt == "JPEG" || strPhotoExt == "jpeg")
            {
                strFileName1 = strFIRefNo.Trim() + "_" + lbldocName.Text + "." + strPhotoExt;
                strFileName = strFileRePath + "\\" + strFileName1;
            }
            else
            {
                lbl.Text = "";
                lbl.Text = "Invalid file format.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Invalid file format');", true);
                return;
            }
            //pranj
            System.Drawing.Image image_file = System.Drawing.Image.FromStream(fuData.PostedFile.InputStream);
            if (strPhotoExt != string.Empty)
            {

                image_height = image_file.Height;
                image_width = image_file.Width;
                //Set image height and width to panel height and width iff the image is greater than panel dimensions start
                if ((image_height > Convert.ToInt32(lblimgheight1.Text) && image_width > Convert.ToInt32(lblimgwidth1.Text))
                            || (image_height > Convert.ToInt32(lblimgheight1.Text) || image_width > Convert.ToInt32(lblimgwidth1.Text)))
                {
                    max_height = Convert.ToInt32(lblimgheight1.Text);
                    max_width = Convert.ToInt32(lblimgwidth1.Text);
                }
                else
                {
                    max_height = image_height;
                    max_width = image_width;
                }
                Bitmap bitmap_file = new Bitmap(image_file, image_file.Width, image_file.Height);
                System.IO.MemoryStream stream = new MemoryStream();
                bitmap_file.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Position = 0;

                data = new byte[stream.Length + 1];
                stream.Read(data, 0, data.Length);

            }
            else
            {
                lbl.Text = "";
                lbl.Text = "Please upload an image.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //var message = new JavaScriptSerializer().Serialize("Please Upload an image");
                //var script = string.Format("alert({0});", message);
                //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
                return;

            }

            FileInfo fi = new FileInfo(strPath);
            {
                if (fuData.PostedFile.ContentLength <= BMaxImgSize1)
                {
                    if (File.Exists(strFileName))
                    {
                        string stroldpath = strFileRePath + "\\" + strFileName1;
                        string[] strfile = strFileName1.Split('.');
                        string ImageNamenew = strfile[0];

                        string strnewpath = strFileRePath + "\\" + ImageNamenew + "_R" + "." + strPhotoExt;
                        System.IO.File.Copy(stroldpath, strnewpath, true);
                    }
                }
                else
                {
                    int SIZE1 = BMaxImgSize1 / 1024;
                    lbl.Text = "";
                    lbl.Text = "Max file size should be less than " + SIZE1 + "Kb.";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Max file size should be less than " + SIZE1 + "Kb');", true);
                    return;
                }
            }

            #endregion

            strDestPath = System.IO.Path.Combine(strFileRePath, strFileName);

            fuData.PostedFile.SaveAs((strFileName));
            string str1 = strFileName.Replace(@"\", @"/");
            string[] actualpath = str1.Split('/');
            strFileName = actualpath[0] + "\\" + actualpath[1] + "\\" + actualpath[3];
            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            dt.Clear();
            hTable.Clear();
            hTable.Add("@RegRefNo", strRefNo.Trim());
            hTable.Add("@DocType", lbldocName.Text.Trim());
            hTable.Add("@UserID", HttpContext.Current.Session["UserID"].ToString().Trim());
            hTable.Add("@Imagebin", data);
            hTable.Add("@DocCode", lbldoccode1.Text.Trim());
            try
            {
                dataAccessLayer.GetDataTable("Proc_InsertDocReUpload", hTable);

            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CKYCDocupld.aspx.cs", "btn_ReUpload_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
                fuData.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                string Docname = lbldocName.Text;
            }

        }

        protected void btn_ReUpload_Click1(object sender, EventArgs e)
        {
            SetPath();
            GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
            FileUpload fuData = (FileUpload)row.FindControl("FileUpload1");
            Label lblRelRefNo = (Label)row.FindControl("lblRelRefNo");
            Label lbldocName = (Label)row.FindControl("lbldocName1");
            Label lblUpldBy = (Label)row.FindControl("lblUpldBy1");
            Label lblUpdDtTm = (Label)row.FindControl("lblUpdDtTm1");
            Label lblFileName = (Label)row.FindControl("lblFileName1");
            Label lblimgsize1 = (Label)row.FindControl("lblimgsize1");
            Label lblimgshrt1 = (Label)row.FindControl("lblimgshrt1");
            Label lblimgwidth1 = (Label)row.FindControl("lblimgwidth1");
            Label lblimgheight1 = (Label)row.FindControl("lblimgheight1");
            Label lbldoccode1 = (Label)row.FindControl("lbldoccode1");
            LinkButton btnreupd = (LinkButton)row.FindControl("btn_ReUpload1");
            LinkButton btn_Upload = (LinkButton)row.FindControl("btn_Upload1");
            BMaxImgSize1 = Convert.ToInt32(lblimgsize1.Text);
            string strFileRePath = string.Empty;

            strFIRefNo = GetStrFIRefNo(strRefNo, "1");

            string strFIRefNo1 = GetStrFIRefNo(lblRelRefNo.Text.ToString(), "2");

            if (Directory.Exists(strPath) == false)
            {
                strPath = strPath + strFIRefNo.Trim();
                Directory.CreateDirectory(strPath);
            }
            else
            {
                strFileRePath = strPath + strFIRefNo.Trim();
                //if (!Directory.Exists(Server.MapPath(strFilePath)))
                if (!Directory.Exists(strFileRePath))
                {
                    // Directory.sCreateDirectory(Server.MapPath(strFilePath));
                    Directory.CreateDirectory(strFileRePath);
                }
                else
                {
                    strFileRePath = strPath + strFIRefNo.Trim();
                }
            }


            #region ReUpload

            if (fuData.HasFile)
            {
                if (fuData.HasFile)
                {
                    strDocName = fuData.PostedFile.FileName;
                    strPhotoExt = strDocName.Substring(strDocName.LastIndexOf('.') + 1).ToUpper();
                }
            }
            else
            {
                lbl.Text = "";
                lbl.Text = "Please select " + lbldocName.Text + " file for ReUpload.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select " + lbldocName.Text + " file for reupload');", true);
                return;
            }
            if (strPhotoExt == "JPG" || strPhotoExt == "jpg")
            {
                strFileName1 = strFIRefNo1.Trim() + "_" + lbldocName.Text + "." + strPhotoExt;
                strFileName = strFileRePath + "\\" + strFileName1;
            }
            else if (strPhotoExt == "GIF" || strPhotoExt == "gif")
            {

                strFileName1 = strFIRefNo1.Trim() + "_" + lbldocName.Text + "." + strPhotoExt;
                strFileName = strFileRePath + "\\" + strFileName1;
            }
            else if (strPhotoExt == "JPEG" || strPhotoExt == "jpeg")
            {
                strFileName1 = strFIRefNo1.Trim() + "_" + lbldocName.Text + "." + strPhotoExt;
                strFileName = strFileRePath + "\\" + strFileName1;
            }
            else
            {
                lbl.Text = "";
                lbl.Text = "Invalid file format.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Invalid file format');", true);
                return;
            }
            //pranj
            System.Drawing.Image image_file = System.Drawing.Image.FromStream(fuData.PostedFile.InputStream);
            if (strPhotoExt != string.Empty)
            {

                image_height = image_file.Height;
                image_width = image_file.Width;
                //Set image height and width to panel height and width iff the image is greater than panel dimensions start
                if ((image_height > Convert.ToInt32(lblimgheight1.Text) && image_width > Convert.ToInt32(lblimgwidth1.Text))
                            || (image_height > Convert.ToInt32(lblimgheight1.Text) || image_width > Convert.ToInt32(lblimgwidth1.Text)))
                {
                    max_height = Convert.ToInt32(lblimgheight1.Text);
                    max_width = Convert.ToInt32(lblimgwidth1.Text);
                }
                else
                {
                    max_height = image_height;
                    max_width = image_width;
                }
                Bitmap bitmap_file = new Bitmap(image_file, image_file.Width, image_file.Height);
                System.IO.MemoryStream stream = new MemoryStream();
                bitmap_file.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Position = 0;

                data = new byte[stream.Length + 1];
                stream.Read(data, 0, data.Length);

            }
            else
            {
                lbl.Text = "";
                lbl.Text = "Please upload an image.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //var message = new JavaScriptSerializer().Serialize("Please Upload an image");
                //var script = string.Format("alert({0});", message);
                //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
                return;

            }

            FileInfo fi = new FileInfo(strPath);
            {
                if (fuData.PostedFile.ContentLength <= BMaxImgSize1)
                {
                    if (File.Exists(strFileName))
                    {
                        string stroldpath = strFileRePath + "\\" + strFileName1;
                        string[] strfile = strFileName1.Split('.');
                        string ImageNamenew = strfile[0];

                        string strnewpath = strFileRePath + "\\" + ImageNamenew + "_R" + "." + strPhotoExt;
                        System.IO.File.Copy(stroldpath, strnewpath, true);
                    }
                }
                else
                {
                    int SIZE1 = BMaxImgSize1 / 1024;
                    lbl.Text = "";
                    lbl.Text = "Max file size should be less than " + SIZE1 + "Kb.";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Max file size should be less than " + SIZE1 + "Kb');", true);
                    return;
                }
            }

            #endregion

            strDestPath = System.IO.Path.Combine(strFileRePath, strFileName);

            fuData.PostedFile.SaveAs((strFileName));
            string str1 = strFileName.Replace(@"\", @"/");
            string[] actualpath = str1.Split('/');
            strFileName = actualpath[0] + "\\" + actualpath[1] + "\\" + actualpath[3];
            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            dt.Clear();
            hTable.Clear();
            hTable.Add("@RegRefNo", lblRelRefNo.Text.Trim());
            hTable.Add("@DocType", lbldocName.Text.Trim());
            hTable.Add("@UserID", HttpContext.Current.Session["UserID"].ToString().Trim());
            hTable.Add("@Imagebin", data);
            hTable.Add("@DocCode", lbldoccode1.Text.Trim());
            try
            {
                dataAccessLayer.GetDataTable("Proc_InsertDocReUpload", hTable);

            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CKYCDocupld.aspx.cs", "btn_ReUpload_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
                fuData.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                string Docname = lbldocName.Text;
            }

        }

        protected void btn_ReUpload_Click2(object sender, EventArgs e)
        {
            SetPath();
            GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
            FileUpload fuData = (FileUpload)row.FindControl("FileUpload2");
            Label lblRelRefNo2 = (Label)row.FindControl("lblRelRefNo2");
            Label lbldocName = (Label)row.FindControl("lbldocName2");
            Label lblUpldBy = (Label)row.FindControl("lblUpldBy2");
            Label lblUpdDtTm = (Label)row.FindControl("lblUpdDtTm2");
            Label lblFileName = (Label)row.FindControl("lblFileName2");
            Label lblimgsize1 = (Label)row.FindControl("lblimgsize2");
            Label lblimgshrt1 = (Label)row.FindControl("lblimgshrt2");
            Label lblimgwidth1 = (Label)row.FindControl("lblimgwidth2");
            Label lblimgheight1 = (Label)row.FindControl("lblimgheight2");
            Label lbldoccode1 = (Label)row.FindControl("lbldoccode2");
            LinkButton btnreupd = (LinkButton)row.FindControl("btn_ReUpload2");
            LinkButton btn_Upload = (LinkButton)row.FindControl("btn_Upload2");
            BMaxImgSize1 = Convert.ToInt32(lblimgsize1.Text);
            string strFileRePath = string.Empty;

            strFIRefNo = GetStrFIRefNo(strRefNo, "1");
            string strFIRefNo2 = GetStrFIRefNo(lblRelRefNo2.Text.ToString(), "3");

            if (Directory.Exists(strPath) == false)
            {
                strPath = strPath + strFIRefNo.Trim();
                Directory.CreateDirectory(strPath);
            }
            else
            {
                strFileRePath = strPath + strFIRefNo.Trim();
                //if (!Directory.Exists(Server.MapPath(strFilePath)))
                if (!Directory.Exists(strFileRePath))
                {
                    // Directory.sCreateDirectory(Server.MapPath(strFilePath));
                    Directory.CreateDirectory(strFileRePath);
                }
                else
                {
                    strFileRePath = strPath + strFIRefNo.Trim();
                }
            }


            #region ReUpload

            if (fuData.HasFile)
            {
                if (fuData.HasFile)
                {
                    strDocName = fuData.PostedFile.FileName;
                    strPhotoExt = strDocName.Substring(strDocName.LastIndexOf('.') + 1).ToUpper();
                }
            }
            else
            {
                lbl.Text = "";
                lbl.Text = "Please select " + lbldocName.Text + " file for ReUpload.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select " + lbldocName.Text + " file for reupload');", true);

                return;
            }
            if (strPhotoExt == "JPG" || strPhotoExt == "jpg")
            {
                strFileName1 = strFIRefNo2.Trim() + "_" + lbldocName.Text + "." + strPhotoExt;
                strFileName = strFileRePath + "\\" + strFileName1;
            }
            else if (strPhotoExt == "GIF" || strPhotoExt == "gif")
            {

                strFileName1 = strFIRefNo2.Trim() + "_" + lbldocName.Text + "." + strPhotoExt;
                strFileName = strFileRePath + "\\" + strFileName1;
            }
            else if (strPhotoExt == "JPEG" || strPhotoExt == "jpeg")
            {
                strFileName1 = strFIRefNo2.Trim() + "_" + lbldocName.Text + "." + strPhotoExt;
                strFileName = strFileRePath + "\\" + strFileName1;
            }
            else
            {
                lbl.Text = "";
                lbl.Text = "Invalid file format.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Invalid file format');", true);
                return;
            }
            //pranj
            System.Drawing.Image image_file = System.Drawing.Image.FromStream(fuData.PostedFile.InputStream);
            if (strPhotoExt != string.Empty)
            {

                image_height = image_file.Height;
                image_width = image_file.Width;
                //Set image height and width to panel height and width iff the image is greater than panel dimensions start
                if ((image_height > Convert.ToInt32(lblimgheight1.Text) && image_width > Convert.ToInt32(lblimgwidth1.Text))
                            || (image_height > Convert.ToInt32(lblimgheight1.Text) || image_width > Convert.ToInt32(lblimgwidth1.Text)))
                {
                    max_height = Convert.ToInt32(lblimgheight1.Text);
                    max_width = Convert.ToInt32(lblimgwidth1.Text);
                }
                else
                {
                    max_height = image_height;
                    max_width = image_width;
                }
                Bitmap bitmap_file = new Bitmap(image_file, image_file.Width, image_file.Height);
                System.IO.MemoryStream stream = new MemoryStream();
                bitmap_file.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Position = 0;

                data = new byte[stream.Length + 1];
                stream.Read(data, 0, data.Length);

            }
            else
            {
                lbl.Text = "";
                lbl.Text = "Please upload an image.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //var message = new JavaScriptSerializer().Serialize("Please Upload an image");
                //var script = string.Format("alert({0});", message);
                //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
                return;

            }

            FileInfo fi = new FileInfo(strPath);
            {
                if (fuData.PostedFile.ContentLength <= BMaxImgSize1)
                {
                    if (File.Exists(strFileName))
                    {
                        string stroldpath = strFileRePath + "\\" + strFileName1;
                        string[] strfile = strFileName1.Split('.');
                        string ImageNamenew = strfile[0];

                        string strnewpath = strFileRePath + "\\" + ImageNamenew + "_R" + "." + strPhotoExt;
                        System.IO.File.Copy(stroldpath, strnewpath, true);
                    }
                }
                else
                {
                    int SIZE1 = BMaxImgSize1 / 1024;
                    lbl.Text = "";
                    lbl.Text = "Max file size should be less than " + SIZE1 + "Kb.";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Max file size should be less than " + SIZE1 + "Kb');", true);
                    return;
                }
            }

            #endregion

            strDestPath = System.IO.Path.Combine(strFileRePath, strFileName);

            fuData.PostedFile.SaveAs((strFileName));
            string str1 = strFileName.Replace(@"\", @"/");
            string[] actualpath = str1.Split('/');
            strFileName = actualpath[0] + "\\" + actualpath[1] + "\\" + actualpath[3];
            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            dt.Clear();
            hTable.Clear();
            hTable.Add("@RegRefNo", lblRelRefNo2.Text.Trim());
            hTable.Add("@DocType", lbldocName.Text.Trim());
            hTable.Add("@UserID", HttpContext.Current.Session["UserID"].ToString().Trim());
            hTable.Add("@Imagebin", data);
            hTable.Add("@DocCode", lbldoccode1.Text.Trim());
            try
            {
                dataAccessLayer.GetDataTable("Proc_InsertDocReUpload", hTable);

            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CKYCDocupld.aspx.cs", "btn_ReUpload_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
                fuData.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                string Docname = lbldocName.Text;
            }

        }
        #endregion

        #region btnSubmit_Click Event
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt.Clear();
                hTable.Clear();
                //hTable.Add("@Flag", "1");
                hTable.Add("@RegRefNo", strRefNo.Trim());
                //hTable.Add("@TypeofDoc", "UPLD");
                dt = dataAccessLayer.GetDataTable("prc_GetDocUploadValidated", hTable);

                //if (dt.Rows.Count > 0)
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //        int i;
                //        for (i = 0; i < dt.Rows.Count; i++)
                //        {
                //            string mandtry = dt.Rows[i]["IsMandatory"].ToString().Trim();

                //            if (mandtry == "Y")    //// if (mandtry == "Y")
                //            {
                //                //string ImgDesc = dt.Rows[i]["ImgDesc01"].ToString().Trim();
                //                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please Upload " + ImgDesc + " ');", true);
                //                //return;

                //                lbl.Text = "";
                //                lbl.Text = "Please upload all mandatory documents.";        //" + dt.Rows[i]["ImgDesc01"].ToString().Trim() + " .
                //                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                //                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please select " + lbldocName.Text + " file for upload');", true);
                //                return;
                //            }
                //        }
                //    }
                //}
                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");

                hTable.Clear();
                hTable.Add("@UserId", HttpContext.Current.Session["UserID"].ToString().Trim());
                hTable.Add("@RefNo", strRefNo);
                dataAccessLayer.ExecuteNonQuery("prc_UpdStatus", hTable);



                lbl.Text = "Document uploaded successfully for " + "</br>Reference No. : " + strRefNo;


                // ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + "Document uploaded successfully for " + "</br></br>Reference No. : " + strRefNo + "');", true);
                // lbl.Text = " Files generated successfully.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
                btnSubmit.Enabled = false;
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CKYCDocupld.aspx.cs", "btnSubmit_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        #endregion

        #region  SaveButn  after image rotating
        public void SaveButn(object sender, EventArgs e)
        {
            try
            {
                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt.Clear();
                hTable.Clear();

                if (hdnTabIndex.Value.ToString() == "1")
                {
                    hTable.Add("@RefNo", ViewState["EntRefNo"].ToString());
                    hTable.Add("@Id", ViewState["hdndoccode"].ToString());
                    hTable.Add("@DocType", ViewState["DocName"].ToString());
                }

                if (hdnTabIndex.Value.ToString() == "2")
                {
                    hTable.Add("@RefNo", ViewState["RelRefNo"].ToString());
                    hTable.Add("@Id", ViewState["hdndoccode1"].ToString());
                    hTable.Add("@DocType", ViewState["DocName1"].ToString());
                }

                if (hdnTabIndex.Value.ToString() == "3")
                {
                    hTable.Add("@RefNo", ViewState["CtrlRefNo"].ToString());
                    hTable.Add("@Id", ViewState["hdndoccode2"].ToString());
                    hTable.Add("@DocType", ViewState["DocName2"].ToString());
                }
                
                
                //if (ViewState["hdndoccode"] != null)
                //{
                //    hTable.Add("@Id", ViewState["hdndoccode"].ToString());
                //}

                //if (ViewState["hdndoccode1"] != null)
                //{
                //    hTable.Add("@Id", ViewState["hdndoccode1"].ToString());
                //}

                //if (ViewState["hdndoccode2"] != null)
                //{
                //    hTable.Add("@Id", ViewState["hdndoccode2"].ToString());
                //}

                //if (ViewState["DocName"] != null)
                //{
                //    hTable.Add("@DocType", ViewState["DocName"].ToString());
                //}

                //if (ViewState["DocName1"] != null)
                //{
                //    hTable.Add("@DocType", ViewState["DocName1"].ToString());
                //}

                //if (ViewState["DocName2"] != null)
                //{
                //    hTable.Add("@DocType", ViewState["DocName2"].ToString());
                //}

                //hTable.Add("@DocType", ViewState["DocName"].ToString());

                dt = dataAccessLayer.GetDataTable("prc_GetImagesforQC", hTable);




                //convert into bite
                byte[] bytes = (byte[])dt.Rows[0]["IMAGE"];


                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                ViewState["vpath"] = base64String;
                img3.ImageUrl = "ImageCSharp.aspx?ImageID=" + dt.Rows[0]["ID"].ToString();

                System.Drawing.Image resim = new Bitmap(ToImage(bytes));

                int degree = Convert.ToInt32(hdnRotateValue.Value);
                int w = Convert.ToInt32(hdnHt.Value);
                // w=Convert.ToInt32(w*2*0.1);
                int h = Convert.ToInt32(hdnWt.Value);

                if (degree != 0)
                {
                    resim = cevir((Bitmap)resim, degree);
                }


                bytes = imageToByteArray(resim, w, h);

                // dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                //dt.Clear();
                hTable.Clear();
                hTable.Add("@ID", dt.Rows[0]["ID"].ToString());
                hTable.Add("@ImgByte", bytes);

                if (hdnTabIndex.Value.ToString() == "1")
                {
                    hTable.Add("@RefNo", ViewState["EntRefNo"].ToString());
                }

                if (hdnTabIndex.Value.ToString() == "2")
                {
                    hTable.Add("@RefNo", ViewState["RelRefNo"].ToString());
                }

                if (hdnTabIndex.Value.ToString() == "3")
                {
                    hTable.Add("@RefNo", ViewState["CtrlRefNo"].ToString());
                }

                dataAccessLayer.ExecuteNonQuery("Prc_UpdateImg", hTable);


                Bindgridview();
                lbl.Text = "";
                lbl.Text = "Image saved successfully.";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);

                //var msz = "Image saved successfully for  <br/><br/>" + Convert.ToString(hdnImgId.Value);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msz + "');", true);
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CKYCDocupld.aspx.cs", "SaveButn", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
                
            }
        }
        #endregion  SaveButn

        #region cevir
        private static Bitmap cevir(Bitmap b, float angle)
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
        #endregion

        #region imageToByteArray
        public byte[] imageToByteArray(System.Drawing.Image imageIn, int w, int h)
        {
            imageIn = new Bitmap(imageIn, w, h);
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
        #endregion

        #region ToImage
        public System.Drawing.Image ToImage(byte[] array)
        {
            System.Drawing.Image returnImage = null;
            MemoryStream ms = new MemoryStream(array);
            returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }
        #endregion

        #region btnCancel_Click Event
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CKYCSearch.aspx?Status=DocUpld", false);
        }
        #endregion

        #region GridView Left And Right Button Indexing Change Event
        protected void ddlPageSelectorL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgView.EditIndex = -1;
                dgView.PageIndex = ((DropDownList)sender).SelectedIndex;
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CKYCDocupld.aspx.cs", "ddlPageSelectorL_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        protected void ddlPageSelectorR_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgView.EditIndex = -1;
                dgView.PageIndex = ((DropDownList)sender).SelectedIndex;
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CKYCDocupld.aspx.cs", "ddlPageSelectorR_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion

        #region GridView Row Created Change Event
        protected void dgView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
            //    if (e.Row.RowType.Equals(DataControlRowType.Pager))
            //    {
            //        SetPagerButtonStates(dgView, e.Row, this, "ddlPageSelectorL", "ddlPageSelectorR");
            //    }

            //    if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            //    {
            //        dgView.UseAccessibleHeader = true;
            //        dgView.HeaderRow.TableSection = TableRowSection.TableHeader;
            //        TableCellCollection cells = dgView.HeaderRow.Cells;
            //        cells[0].Attributes.Add("data-hide", "phone");
            //        cells[1].Attributes.Add("data-class", "expand");
            //        cells[2].Attributes.Add("data-hide", "phone");
            //        cells[3].Attributes.Add("data-hide", "phone");
            //        cells[4].Attributes.Add("data-hide", "phone");
            //        cells[5].Attributes.Add("data-hide", "phone");
            //        cells[6].Attributes.Add("data-hide", "phone,tablet");
            //        cells[7].Attributes.Add("data-hide", "phone,tablet");
            //        cells[8].Attributes.Add("data-hide", "phone,tablet");
            //        cells[9].Attributes.Add("data-hide", "phone,tablet");

            //        // cells[15].Attributes.Add("data-hide", "phone,tablet");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
            //    {
            //        Response.Redirect("~/ErrorSession.aspx");
            //    }
            //    else
            //    {
            //        objErr.LogErr(1,  "CkycSearch.aspx.cs", "Page_Load",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            //    
            //}
        }
        #endregion

        #region GridView Row Created Change Event
        protected void gvRelPrsn_RowCreated(object sender, GridViewRowEventArgs e)
        {
        }
        #endregion

        #region GridView Row Created Change Event
        protected void gvCtrlPrsn_RowCreated(object sender, GridViewRowEventArgs e)
        {
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
                    objErr.LogErr(1, "CKYCDocupld.aspx.cs", "SetPagerButtonStates", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion

        #region  btnCrop
        public void btnCroppreview_click(object sender, EventArgs e)
        {
            try
            {
                if (hdnTabIndex.Value == "1")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "funcopencrop2();", true);
                }

                if (hdnTabIndex.Value == "2")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "funcopencrop3();", true);
                }

                if (hdnTabIndex.Value == "3")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "funcopencrop4();", true);
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
                    objErr.LogErr(1, "CKYCDocupld.aspx.cs", "btnCroppreview_click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion  SaveButn

        #region  SetPath for document upload
        public void SetPath()
        {
            try
            {


                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt.Clear();
                hTable.Clear(); dt.Clear();
                hTable.Clear();
                hTable.Add("@flag", "1");
                hTable.Add("@Seqno", "7");
                dt = dataAccessLayer.GetDataTable("Prc_getdata", hTable);
                strPath = dt.Rows[0]["Path1"].ToString().Trim();
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CKYCDocupld.aspx.cs", "SetPath", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion  SaveButn
    }
}