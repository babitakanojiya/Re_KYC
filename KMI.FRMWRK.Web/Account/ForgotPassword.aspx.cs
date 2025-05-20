using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KMI.FRMWRK;

namespace KMI.FRMWRK.Web.Account
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        private BAL.Account Account;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendLink_Click(object sender, EventArgs e)
        {
            try
            {
                Account = new BAL.Account();
                var message = Account.ForgotPassword(txtEmailID.Text);
                if (message.Contains("Success!"))
                    lblMessage.CssClass = "label label-success msg-label";
                else
                    lblMessage.CssClass = "label label-warning msg-label";

                lblMessage.Text = message;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            

        }
    }
}