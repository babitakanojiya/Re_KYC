using System;
using System.Web;
using System.Data;
using System.Collections;
using KMI.FRMWRK.DAL;
using KMI.FRMWRK.Multilingual;
using KMI.FRMWRK.Web.Application.CKYC.Common;
using System.Web.UI;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class ImgCropped : System.Web.UI.Page
    {
        #region Global Declaration
        DataTable dt;
        Hashtable hTable = new Hashtable();
        DataAccessLayer dataAccessLayer;
        ErrorLog objErr = new ErrorLog();
        private MultilingualManager olng;
        private string strUserLang;
        CommonUtility oCommonUtility = new CommonUtility();

        #endregion
        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["ImageID"] != null)
            {
                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt = null;
                hTable.Clear();
                hTable.Add("@ID", Request.QueryString["ImageID"]);
                hTable.Add("@Flag", "1");
                dt = dataAccessLayer.GetDataTable("prc_GetImagesbinary", hTable);

                if (dt != null)
                {
                    try
                    {
                        if (dt.Rows.Count > 0)
                        {
                            byte[] bytes = (byte[])dt.Rows[0]["ImagesCrop"];
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = dt.Rows[0]["DocType"].ToString();
                            Response.AddHeader("content-disposition", "attachment;filename="
                                + dt.Rows[0]["ImagesCrop"].ToString());

                            Response.BinaryWrite(bytes);
                            Response.Flush();
                            Response.End();
                        }
                        else
                        {
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
                            objErr.LogErr(1, "ImgCropped.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
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
    }
    }