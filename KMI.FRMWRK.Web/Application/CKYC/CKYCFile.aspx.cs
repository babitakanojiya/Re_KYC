using System;
using System.Web;
using System.Net;
using System.IO;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class CKYCFile : System.Web.UI.Page
    {
        #region  Global Declaration
        string strPath = string.Empty;
        string strMessg = string.Empty;
      // ServiceReference2.ServiceClient sc = new ServiceReference2.ServiceClient();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            string userid = HttpContext.Current.Session["UserID"].ToString().Trim();
            //strPath = sc.genFile(userid);
            // string res=getdone(strPath);
            //if (res == "1")
            //{
            lbl.Text = " Files generated successfully.";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
            // }
        }

        public string getdone(string strPath)
        {
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(strPath) + "");
            //byte[] data = req.DownloadData((strPath));
            //response.BinaryWrite(data);
            Response.Flush();
            Response.SuppressContent = true;
            return "1";
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            string userid = HttpContext.Current.Session["UserID"].ToString().Trim();
            //strMessg = sc.ProcessFile(userid);
            if (strMessg.Equals("S"))
            {
                lbl.Text = " Files Processed successfully.";
                //mdlpopup.Show();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
            }
            else
            {

            }
        }
        protected void btnUpld_Click(object sender, EventArgs e)
        {
            //strMessg = sc.Upload();
            if (strMessg == "1")
            {
                lbl.Text = " Files Uploaded successfully.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
            }
            else
            {
                lbl.Text = " Files not Uploaded.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
            }
        }
        protected void btnDwnld_Click(object sender, EventArgs e)
        {
            //strMessg = sc.Download();
            if (strMessg == "1")
            {
                lbl.Text = " Files Downloaded successfully.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
            }
            else
            {
                lbl.Text = " Files not Downloaded.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "popup();", true);
            }
        }
    }
}