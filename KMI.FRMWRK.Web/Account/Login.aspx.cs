using KMI.FRMWRK.DAL;
using System;
using System.Web;
using System.Web.UI;

namespace KMI.FRMWRK.Web.Account
{
    public partial class Login : System.Web.UI.Page
    {
        ErrorLog objErr;
        int AppID;
        string UserID = string.Empty;
        private BAL.Account Account;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Account = new BAL.Account();
                if (Session["AppID"] != null)
                {
                    AppID = Convert.ToInt32(Session["AppID"]);
                }
                else
                {
                    //AppID = Convert.ToInt32(System.DBNull.Value);
                    AppID = 1;
                }
                if (Session["UserId"] != null)
                {
                    UserID = Session["UserId"].ToString();
                }
                else
                {
                    UserID = txtUserName.ToString().Trim();
                }
                //empty users current session if exist
                Account.EmptyUserSession();

                if (Request.Cookies["rlifetoken"] == null)
                {

                }
                else
                {
                    Response.Redirect(Account.SetupUserProfile(), false);
                }

                if (!Page.IsPostBack)
                {
                    txtUserName.Focus();
                    Account.InitializeCulture();
                    Account.InitializeControl();
                }
            }
            catch (Exception ex)
            {
                objErr = new ErrorLog();
                objErr.LogErr(AppID, "Login.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "USRMGMT");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
            finally
            {
                Account = null;
            }
        }

        #region Control Events
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                HttpBrowserCapabilities browser = Request.Browser;
                Account = new BAL.Account();
                var SesssioID = Account.Authenticate(txtUserName.Text, "", txtPassword.Text, browser.Type, browser.Version, "");
                var errDesc = Account.GetErroDescription(SesssioID);
                if (string.IsNullOrEmpty(errDesc))
                {
                    Response.Redirect("~/Default.aspx?sid=" + SesssioID + "&cid=null", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + errDesc + "');", true);
                }
            }
            catch (Exception ex)
            {
                objErr = new ErrorLog();
                objErr.LogErr(AppID, "Login.aspx.cs", "btnLogin_Click", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "USRMGMT");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
            finally { Account = null; }
        }
	
        #endregion
       
    }
}