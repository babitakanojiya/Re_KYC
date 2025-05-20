
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMI.FRMWRK.Web.Account
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        #region variable declaration
        private string UserID = string.Empty;
        private string TockenID = string.Empty;
        private string Message= string.Empty;
        private BAL.Account Account;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["tockenid"] != null)
            {
                TockenID = Request.QueryString["tockenid"].ToString();
            }

            if (Session["UserId"] != null)
            {
                UserID = Session["UserId"].ToString();
            }
            if (string.IsNullOrEmpty(TockenID) && string.IsNullOrEmpty(UserID))
            {
               //TODO : Need to redirect on session expire page
            }
        }

        #region Control Events
        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateForm())
                {
                    Account = new BAL.Account();
                    Message = Account.ResetPassword(UserID, txtOldPassword.Text, txtNewPassword.Text, TockenID);
                }

                //show message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + Message + "');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + ex.Message + "');", true);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login");
        }
        #endregion

        #region Private Methods
        private bool ValidateForm()
        {
            try
            {
                if (string.IsNullOrEmpty(TockenID) && string.IsNullOrEmpty(txtOldPassword.Text))
                {
                    Message = "Please enter your old password.";
                    return false;
                }
                else if (string.IsNullOrEmpty(txtNewPassword.Text))
                {
                    Message = "Please enter new password.";
                    return false;
                }
                else if (string.IsNullOrEmpty(txtConfirmNewPassword.Text))
                {
                    Message = "Please enter confirm password.";
                    return false;
                }
                else if (!txtNewPassword.Text.Equals(txtConfirmNewPassword.Text))
                {
                    Message = "Password & Confirm Password must be same.";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion
    }
}