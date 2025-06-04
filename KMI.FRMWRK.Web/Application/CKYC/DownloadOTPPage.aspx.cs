using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMI.FRMWRK.Web.Application.CKYC
{
	public partial class DownloadOTPPage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!string.IsNullOrEmpty(Request.QueryString["ckycRef"]))
            {
                hdnRegRefNo.Value = Request.QueryString["ckycRef"].Trim();
            }

        }
        //protected void QCPageopen_Click(object sender, EventArgs e)
        //{
           
        //        string filePath = ResolveUrl("~/Application/CKYC/CKYCQC.aspx?ckycRef=" + "&showDiv=1");

                

            
        //}

    }
}