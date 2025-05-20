using KMI.FRMWRK.DAL;
using KMI.FRMWRK.Multilingual;
using KMI.FRMWRK.Web.Application.CKYC.Common;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class ExecutionHistoImage : System.Web.UI.Page
    {
        #region Declare Veriables
        DataSet objds;
        Hashtable objht = new Hashtable();
        Hashtable objhtNew = new Hashtable();
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
        int AppId;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void GridImage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //HiddenField hdnid = (HiddenField)e.Row.FindControl("hdnid");
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

        protected void btnprev_Click(object sender, EventArgs e)
        {
            try
            {
                DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
                int intcode = Convert.ToInt32(ViewState["docCode"]);
                intcode = intcode - 1;

                //objds.Clear();
                objht.Clear();
                objht.Add("@RegNo", "10000163");
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
                objht.Add("@RegNo", "10000163");
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

        protected void btnnext_Click(object sender, EventArgs e)
        {
            try
            {
                DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
                int intcode = Convert.ToInt32(ViewState["docCode"]);

                intcode = intcode + 1;

                if (intcode > 2)
                {
                    btnprev.Enabled = true;
                }

                // objds.Clear();
                objht.Clear();
                objht.Add("@RegNo", "10000163");
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

                //Hashtable htParam = new Hashtable();
                //htParam.Clear();
                //DataSet dsResult1 = new DataSet();
                //objhtNew.Add("@RegNo", "10000163");
                //objhtNew.Add("@DOC_NAME", ViewState["DOC_NAME"]);
                ////dsResult1 = objDAL.GetDataSet("prc_GetImage", htParam);
                //dsResult1 = objDAL.GetDataTable("prc_GetImage", objhtNew);


                objDt.Clear();
                objht.Clear();
                objht.Add("@RegNo", "10000163");
                objht.Add("@DOC_NAME", ViewState["DOC_NAME"]);
                //  objds = objDAL.GetDataSet("prc_GetImage", objht, "CKYCConnectionString");
                objDt = objDAL.GetDataTable("prc_GetImage", objht);


                //if (intcode < objDt.Rows.Count + 1)
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
                divloaderqc.Attributes.Add("style", "display:none");

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

        int AppID = 0;
        DataTable dt;

        #region Bind document
        private void BindGrid()
        {
            try
            {
                DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
                #region photo shuffle start added by rachana on 01-07-2013
                objht.Clear();
                objht.Add("@RegNo", "10000163");
                objDt = objDAL.GetDataTable("prc_GetDocType", objht);
                //  objds = objDAL.GetDataSet("prc_GetDocType", objht, "CKYCConnectionString");

                if (objDt.Rows.Count > 0)
                {
                    ViewState["DOC_NAME"] = objDt.Rows[0]["DOC_NAME"].ToString();
                    ViewState["DocNo"] = objDt.Rows[0]["DOC_CODE"].ToString();
                    ViewState["docCode"] = objDt.Rows[0]["SHORTCODE"].ToString();
                }
                #endregion
                objDt.Clear();
                objht.Clear();
                objht.Add("@RegNo", "10000163");
                objht.Add("@DOC_NAME", ViewState["DOC_NAME"]);
                objDt = objDAL.GetDataTable("prc_GetImage", objht);
                //objDt = objDAL.GetDataSet("prc_GetImage", objht, "CKYCConnectionString");
                GridImage.DataSource = objDt;
                GridImage.DataBind();
                ViewState["Img"] = objDt;
                ViewState["Img1"] = objDt;
                lblpanelheader.Text = ViewState["DOC_NAME"].ToString();
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

    }
}