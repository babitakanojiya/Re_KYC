using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMI.FRMWRK.Web.Application
{
    public partial class CKyc_Image : System.Web.UI.Page
    {

        string ImageString = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["GridImage"] != null)
            {
                ImageString = Session["GridImage"].ToString();
                string imgType = "data:image/png;base64,";
                Image1.ImageUrl = imgType + ImageString;
            }
        }
    }
}