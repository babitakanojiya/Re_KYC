using System;
using System.Collections;
using System.Data;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web;
using SD = System.Drawing;
using System.Web.UI;
using KMI.FRMWRK.BAL;

namespace KMI.FRMWRK.Web.Account
{
    public partial class UpdateProfile : System.Web.UI.Page
    {
        #region Variable Declaration 
        string BaseURL = string.Empty;
        string FullPath = string.Empty;
        string strUserId = string.Empty;
        int AppId;

        DAL.DataAccessLayer objDal = new DAL.DataAccessLayer();
        DAL.ErrorLog objErr = new DAL.ErrorLog();
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserId"].ToString() != null)
            {
                strUserId = Session["UserId"].ToString();
            }
            if (HttpContext.Current.Session["AppId"] != null)
            {
                AppId = Convert.ToInt32(HttpContext.Current.Session["AppId"]);
            }

            BaseURL = BaseUrl.BaseUrlPath;
			//BaseURL = @"C:\HostedApplications\ILFS\APP\KMI.FRMWRK.Web\KMI.FRMWRK.Web";
            FullPath = BaseURL + @"\Content\Images\ProfileImage\";

            if (!IsPostBack)
            {
                GetProfileInfo();
            }
        }
        #endregion

        #region Get Profile Info
        public void GetProfileInfo()
        {
            var hTable = new Hashtable();
            var ds = new DataSet();
            try
            {
                hTable.Add("@UserId", strUserId);
                ds = objDal.GetDataSet("prc_GetUserProfileInfo", hTable, "DefaultConn");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblUserId.Text = strUserId;
                        lblUserName.Text = ds.Tables[0].Rows[0]["UserName"].ToString();
                        txtContactNo.Text = ds.Tables[0].Rows[0]["UserMobileNo1"].ToString();
                        txtEmailId.Text = ds.Tables[0].Rows[0]["UserEmailId"].ToString();
                        txtAddress.Text = ds.Tables[0].Rows[0]["UserAddr1"].ToString();
                        string ImgPath = ds.Tables[0].Rows[0]["imgpath"].ToString();
                        string strCropResult = ImgPath.Replace("crop", "");

                        string Gender = ds.Tables[0].Rows[0]["Gender"].ToString();
                        if (Gender == "")
                        {
                        }
                        else
                        {
                            ddlGender.SelectedValue = Gender;
                        }

                        if (ImgPath != "")
                        {
                            ImagePreView.ImageUrl = BaseURL + ds.Tables[0].Rows[0]["imgpath"].ToString();
                            Imgfullimage.ImageUrl = BaseURL + strCropResult;
                        }
                        else
                        {
                            if (Gender == "Female")
                            {
                                ImagePreView.ImageUrl = BaseURL + @"\Content\Images\ProfileImage\defaultFemale.jpg";
                                Imgfullimage.ImageUrl = BaseURL + @"\Content\Images\ProfileImage\defaultFemale.jpg";
                                lblError.Text = "";
                            }
                            if (Gender == "Male")
                            {
                                ImagePreView.ImageUrl = BaseURL + @"\Content\Images\ProfileImage\defaultmale.jpg";
                                Imgfullimage.ImageUrl = BaseURL + @"\Content\Images\ProfileImage\defaultmale.jpg";
                                lblError.Text = "";
                            }
                            if (Gender == "Not filled")
                            {
                                ImagePreView.ImageUrl = BaseURL + @"\Content\Images\ProfileImage\download.jpg";
                                Imgfullimage.ImageUrl = BaseURL + @"\Content\Images\ProfileImage\download.jpg";
                                lblError.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objErr.LogErr(AppId, "UpdateProfile.aspx.cs", "GetProfileInfo", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                throw ex;
            }
            finally
            {
                hTable = null;
                ds = null;
            }
        }
        #endregion

        #region btnCrop_Click
        protected void btnCrop_Click(object sender, EventArgs e)
        {
            string BaseURLGetImage = @"C:\HostedApplications\ILFS\APP\KMI.FRMWRK.Web\KMI.FRMWRK.Web";
            string FullPath1 = BaseURLGetImage + @"\Content\Images\ProfileImage\";
            string ImageName = HdnFilepath.Value.ToString();
            int w = Convert.ToInt32(W.Value.Replace(",", ""));
            int h = Convert.ToInt32(H.Value.Replace(",", ""));
            int x = Convert.ToInt32(X.Value.Replace(",", ""));
            int y = Convert.ToInt32(Y.Value.Replace(",", ""));

            byte[] CropImage = Crop(FullPath1 + ImageName, w, h, x, y);
            using (MemoryStream ms = new MemoryStream(CropImage, 0, CropImage.Length))
            {
                ms.Write(CropImage, 0, CropImage.Length);
                using (SD.Image CroppedImage = SD.Image.FromStream(ms, true))
                {
                    string SaveTo = FullPath1 + @"Crop\" + ImageName;
                    CroppedImage.Save(SaveTo, CroppedImage.RawFormat);
                    pnlCrop.Visible = false;

                    ImagePreView.Visible = true;
                    ImagePreView.ImageUrl = BaseURL + @"\Content\Images\ProfileImage\Crop\" + ImageName;
                }
            }

        }
        #endregion

        #region Crop Method
        static byte[] Crop(string Img, int Width, int Height, int X, int Y)
        {
            try
            {
                using (SD.Image OriginalImage = SD.Image.FromFile(Img))
                {
                    using (SD.Bitmap bmp = new SD.Bitmap(Width, Height))
                    {
                        bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
                        using (SD.Graphics Graphic = SD.Graphics.FromImage(bmp))
                        {
                            Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Graphic.DrawImage(OriginalImage, new SD.Rectangle(0, 0, Width, Height), X, Y, Width, Height, SD.GraphicsUnit.Pixel);
                            MemoryStream ms = new MemoryStream();
                            bmp.Save(ms, OriginalImage.RawFormat);
                            return ms.GetBuffer();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        #endregion

        #region btnSave_Click
        protected void btnSave_Click(object sender, EventArgs e)
        {
            btnUpdate_Click(null, null);
            btnSave.Visible = false;
            btnUpdate.Visible = true;
        }
        #endregion

        #region SaveData
        private void SaveData()
        {
            var hTable = new Hashtable();
            try
            {
                hTable.Add("@UserId", strUserId);
                hTable.Add("@ImgPath", ImagePreView.ImageUrl);
                hTable.Add("@MobNumber", txtContactNo.Text);
                hTable.Add("@EmailId", txtEmailId.Text);
                hTable.Add("@Address", txtAddress.Text);
                hTable.Add("@CreatedBy", strUserId);

                if (ddlGender.SelectedItem.ToString() == "--Select--")
                {
                    hTable.Add("@Gender", "");
                }
                else
                {
                    hTable.Add("@Gender", ddlGender.SelectedItem.Value.ToString());
                }
                objDal.ExecuteScalar("prc_updUserProfile", hTable, "DefaultConn");
                lblError.Visible = true;
                lblError.Text = "Data updated successfully." + lblError.Text;
            }
            catch (Exception ex)
            {
                objErr.LogErr(AppId, "UpdateProfile.aspx.cs", "SaveData", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                throw ex;
            }
        }
        #endregion

        #region btnCancel_Click
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HighCharts.html");
        }
        #endregion

        #region btnClear_Click
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtContactNo.Text = "";
            txtEmailId.Text = "";
            txtAddress.Text = "";
            ImagePreView.ImageUrl = BaseURL + @"\Content\Images\ProfileImage\download.jpg";
            ddlGender.Items.Clear();
            ddlGender.Items.Insert(0, "--Select--");
            ddlGender.Items.Insert(1, "Male");
            ddlGender.Items.Insert(2, "Female");
            lblError.Text = "";
        }
        #endregion

        #region ddlGender_SelectedIndexChanged
        protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGender.SelectedItem.Text == "Female")
            {
                ImagePreView.ImageUrl = BaseURL + @"\Content\Images\ProfileImage\defaultFemale.jpg";
            }
            if (ddlGender.SelectedItem.Text == "Male")
            {
                ImagePreView.ImageUrl = BaseURL + @"\Content\Images\ProfileImage\defaultmale.jpg";
            }
        }
        #endregion

        #region btnGetImage_Click
        protected void btnGetImage_Click(object sender, EventArgs e)
        {
            Boolean FileSaved = false;
            var hTable = new Hashtable();

            if (inputFileUpload.PostedFile.FileName.ToString() != "")
            {
                HdnFilepath.Value = inputFileUpload.PostedFile.FileName.ToString();
                hTable.Add("@counterId", "ProfileImage");
                string StrCounter = (string)objDal.ExecuteNonQuery("prc_iGetCtrNo", "@counterNo", hTable, "DefaultConn");

                HdnFilepath.Value = StrCounter + inputFileUpload.PostedFile.FileName.ToString();
            }

            try
            {
                if (inputFileUpload.PostedFile != null)
                {
                    string BaseURLGetImage = @"C:\HostedApplications\ILFS\APP\KMI.FRMWRK.Web\KMI.FRMWRK.Web";
                    string FullPath1 = BaseURLGetImage + @"\Content\Images\ProfileImage\";
                    inputFileUpload.PostedFile.SaveAs(FullPath1 + HdnFilepath.Value.ToString());
                }
                FileSaved = true;
            }
            catch (Exception ex)
            {
                lblError.Text = "File could not be uploaded." + ex.Message.ToString();
                lblError.Visible = true;
                FileSaved = false;
            }


            if (FileSaved)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMsg", "getCallCrop();", true);
                pnlCrop.Visible = true;
                Imgfullimage.Visible = false;
                imgCrop.ImageUrl = BaseURL + @"\Content\Images\ProfileImage\" + HdnFilepath.Value.ToString();
            }
        }
        #endregion

        #region btnUpdate_Click
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
            }
            catch (Exception ex)
            {
                objErr.LogErr(AppId, "UpdateProfile.aspx.cs", "btnUpdate_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                throw ex;
            }
        }
        #endregion

    }
}