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
    public partial class ImageCSharp : System.Web.UI.Page
    {
        #region Declaration
       DataTable dt;
        Hashtable hTable = new Hashtable();
        ErrorLog objErr = new ErrorLog();
        DataAccessLayer dataAccessLayer;
        private string Message = string.Empty;
        string UserID = string.Empty;
        int AppId;
        string strUserId = string.Empty;
        #endregion

        #region Page Load Event 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ImageID"] != null)
            {
                if (Session["UserId"].ToString() != null)
                {
                    strUserId = Session["UserId"].ToString();
                }
                if (HttpContext.Current.Session["AppId"] != null)
                {
                    AppId = Convert.ToInt32(HttpContext.Current.Session["AppId"]);
                }
                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt = null;
                hTable.Clear();
                hTable.Add("@ID", Request.QueryString["ImageID"]);
                hTable.Add("@Flag", System.DBNull.Value);
                dt = dataAccessLayer.GetDataTable("prc_GetImagesbinary", hTable);

                if (dt != null)
                {
                    try
                    {
                        if (dt.Rows.Count > 0)
                        {
                            Byte[] bytes = (Byte[])dt.Rows[0]["Images"];
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = dt.Rows[0]["DocType"].ToString();
                            Response.AddHeader("content-disposition", "attachment;filename="
                                + dt.Rows[0]["Images"].ToString());

                            Response.BinaryWrite(bytes);
                            Response.Flush();
                          
                        }
                        else
                        {
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
                            objErr.LogErr(AppId, "ImageCSharp.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                            throw ex;
                        }

                    }
                    finally
                    {
                        dataAccessLayer = null;
                    }
                }
            }
        } 
        #endregion
    }
}