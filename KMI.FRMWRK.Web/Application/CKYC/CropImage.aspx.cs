using System;
using System.Data;
using System.Collections;
using System.Web;
using System.IO;
using System.Drawing;
using KMI.FRMWRK.DAL;
using KMI.FRMWRK.Multilingual;
using KMI.FRMWRK.Web.Application.CKYC.Common;
using System.Web.UI;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class CropImage : System.Web.UI.Page
    {
        #region Global Declaration
        string strimage = string.Empty;
        string path = string.Empty;
        DataTable dt;
        Hashtable hTable = new Hashtable();
        ErrorLog objErr = new ErrorLog();
        DataAccessLayer dataAccessLayer;
        private string Message = string.Empty;
        string strUsrrole = string.Empty;
        string strPath;         
        private byte[] bytes;
        String ImgId = string.Empty;
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hdnRegRefNo.Value = Request.QueryString["RefNo"].ToString().Trim();
                ImgId = Request.QueryString["args3"].ToString().Trim();
                if (!IsPostBack)
                {
                    GetImageByte();
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
                    objErr.LogErr(1,  "CropImage.aspx.cs", "Page_Load",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        } 
        #endregion

        #region ToImage
        public Image ToImage(byte[] array)
        {
            Image returnImage = null;
            MemoryStream ms = new MemoryStream(array);
            returnImage = Image.FromStream(ms);
            return returnImage;
        } 
        #endregion

        #region imageToByteArray
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        } 
        #endregion
        #region CropImages
        public void CropImages(int X, int Y, int Width, int Height, byte[] array, string ID)
        {
            using (Image img = new Bitmap(ToImage(array)))
            {
                string ImgName = System.IO.Path.GetFileName(path);
                Bitmap bmpCropped = new Bitmap(Width, Height);
                {
                    using (Graphics g = Graphics.FromImage(bmpCropped))
                    {
                        Rectangle rectDestination = new Rectangle(0, 0, bmpCropped.Width, bmpCropped.Height);
                        Rectangle rectCropArea = new Rectangle(X, Y, Width, Height);
                        g.DrawImage(img, rectDestination, rectCropArea, GraphicsUnit.Pixel);

                        bytes = imageToByteArray(bmpCropped);
                        ViewState["bytes"] = bytes;
                    }
                    dt = new DataTable();
                    dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                    dt.Clear();
                    hTable.Clear();
                    hTable.Add("@ID", ID);
                    hTable.Add("@ImgByte", bytes);
                    hTable.Add("@flag", 1);
                    hTable.Add("@Action", "Crop");
                    hTable.Add("@UserId", HttpContext.Current.Session["UserID"].ToString().Trim());

                    dt = dataAccessLayer.GetDataTable("Prc_UpdateImgCrop", hTable);

                    ImFullImage.ImageUrl = "ImageCSharp.aspx?ImageID=" + "CKYC" + ID;
                    imCropped.ImageUrl = "ImgCropped.aspx?ImageID=" + ID;
                }
            }
        } 
        #endregion

        #region btnCrop_Click
        protected void btnCrop_Click(object sender, EventArgs e)
        {
            try
            {
                int x, y, w, h;
                if (!int.TryParse(hfX.Value, out x))
                {
                    //Set default x value
                    x = 0;
                }

                if (!int.TryParse(hfY.Value, out y))
                {
                    //Set default y value
                    y = 0;
                }

                if (!int.TryParse(hfHeight.Value, out h))
                {
                    //Set default height value
                    h = 0;
                }

                if (!int.TryParse(hfWidth.Value, out w))
                {
                    //Set default width value
                    w = 0;
                }

                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt.Clear();
                hTable.Clear();
                hTable.Add("@RegRefNo", hdnRegRefNo.Value);
                hTable.Add("@DocType", ImgId);
                hTable.Add("@Flag", "3");

                dt = dataAccessLayer.GetDataTable("Prc_GetDocNames", hTable);

                ViewState["ID"] = dt.Rows[0]["SR_NO"].ToString().Trim();
                bytes = (byte[])dt.Rows[0]["Image"];
                CropImages(x, y, w, h, bytes, dt.Rows[0]["SR_NO"].ToString().Trim());
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CropImage.aspx.cs", "btnCrop_Click",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }

        } 
        #endregion

        #region get image byte

        protected void GetImageByte()
        {

            try
            {
                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt.Clear();
                hTable.Clear();
                hTable.Add("@RegRefNo", hdnRegRefNo.Value);
                hTable.Add("@DocType", ImgId);
                hTable.Add("@Flag", "3");
                dt = dataAccessLayer.GetDataTable("Prc_GetDocNamesNew", hTable);

                bytes = (byte[])dt.Rows[0]["Image"];
                ViewState["Bytes"] = bytes;
                ImFullImage.ImageUrl = "ImageCSharp.aspx?ImageID=" + "CKYC" + dt.Rows[0]["SR_NO"].ToString().Trim();
                ViewState["SR_NO"] = dt.Rows[0]["SR_NO"].ToString().Trim();
                ddlDocType.Text = dt.Rows[0]["DOC_NAME"].ToString().Trim();

            }

            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1,  "CropImage.aspx.cs", "GetImageByte",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        #endregion
        

        #region btnok_Click
        protected void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt.Clear();
                hTable.Clear();

                hTable.Add("@ID", ViewState["ID"]);
                hTable.Add("@flag ", 2);
                hTable.Add("@Action", "Crop");
                hTable.Add("@CndNo", hdnRegRefNo.Value);
                hTable.Add("@UserId", HttpContext.Current.Session["UserID"].ToString().Trim());

                dt = dataAccessLayer.GetDataTable("Prc_UpdateImgCrop", hTable);
                lblpopup.Text = "";
                lblpopup.Text = "Image saved successfully.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
            }
            catch (Exception ex)
            {
                if (HttpContext.Current.Session["UserID"].ToString().Trim() == null || HttpContext.Current.Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "CropImage.aspx.cs", "btnok_Click",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        } 
        #endregion

        #region Btncancl_Click
        protected void Btncancl_Click(object sender, EventArgs e)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
        } 
        #endregion

        #region  SetPath for document upload  
        public void SetPath()
        {
            try
            {

                dt = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dt.Clear();
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
                    objErr.LogErr(1,  "CropImage.aspx.cs", "SetPath",ex.InnerException==null? ex.Message.ToString() : ex.Message.ToString() + " | " +ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        #endregion  SetPath
    }
}