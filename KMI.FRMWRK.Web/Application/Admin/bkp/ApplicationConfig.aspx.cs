using KMI.FRMWRK.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KMI.FRMWRK.Web.Application.CKYC.Common;


namespace KMI.FRMWRK.Web.Application.Admin
{
    public partial class ApplicationConfig : System.Web.UI.Page
    {
        DataAccessLayer objDAL = new DataAccessLayer();
        ErrorLog objErr = new ErrorLog();
        CommonUtility cu = new CommonUtility();
        int AppId;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SFTPDetails.Visible = false;
                    BULKDetails.Visible = false;
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;

                    cu.BindLookupFromUSRMGMT(ddlConstType, "ConstType", true);
                    cu.BindLookupFromUSRMGMT(ListIntTypeData, "IntData", false);
                    //cu.BindLookupFromUSRMGMT(ListIntTypeDoc, "IntDocument", false);
                    cu.BindLookupFromUSRMGMT(ddlMatch, "ProbableMatch", false);
                    cu.BindLookupFromUSRMGMT(ddlSearch, "Search", true);
                    cu.BindLookupFromUSRMGMT(ddlDownload, "Download", true);


                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, "ApplicationConfig.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (validate())
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("EntityName", txtEntityName.Text.Trim());
                    ht.Add("InstitutionCode", txtInsCode.Text.Trim());
                    ht.Add("ConstType", ddlConstType.SelectedValue.Trim());
                    ht.Add("DataType", string.Join(",", cu.GetIntegrationValues(ListIntTypeData)));
                    ht.Add("DocumentType", string.Join(",", cu.GetIntegrationValues(ListIntTypeDoc)));
                    ht.Add("FilePath", txtFilePath.Text);
                    ht.Add("CersaiIntType", rdoCERSAIIntList.SelectedValue);
                    ht.Add("CERSAIPath", txtCERSAISFTPPath.Text.Trim());
                    ht.Add("CERSAIUserName", txtCERSAIUserNameKey.Text.Trim());
                    ht.Add("CERSAIPassword", txtCERSAIPassKey.Text.Trim());
                    ht.Add("CKYCPath", txtCKYCSFTPPath.Text.Trim());
                    ht.Add("CKYCUserName", txtCKYCSFTPPath.Text.Trim());
                    ht.Add("CKYCPassword", txtCKYCSFTPPath.Text.Trim());
                    ht.Add("ProbableMatch", ddlMatch.SelectedValue);
                    ht.Add("Search", ddlSearch.SelectedValue);
                    ht.Add("Download", ddlDownload.SelectedValue);
                    ht.Add("DSPath", txtDSPath.Text.Trim());
                    ht.Add("PublicKey", txtPubKey.Text.Trim());
                    ht.Add("PrivateKey", txtPrvKey.Text.Trim());
                    ht.Add("UserID", Session["UserId"]);
                    objDAL.GetDataSet("Prc_InsEntitySetup", ht, "DefaultConn");
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Entity Setup is Done Successfully')", true);
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, "ApplicationConfig.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.RawUrl, false);
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, "ApplicationConfig.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, "ApplicationConfig.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        public bool validate()
        {
            if (txtEntityName.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Enter Entity Name')", true);
                return false;
            }

            if (txtInsCode.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Enter Institution Code')", true);
                return false;
            }


            if (ddlConstType.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Select Constitution Code')", true);
                return false;
            }

            if (cu.GetIntegrationValues(ListIntTypeData).Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Select System Integration Type For Data')", true);
                return false;
            }

            if (cu.GetIntegrationValues(ListIntTypeDoc).Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Select System Integration Type For Document')", true);
                return false;
            }

            if (cu.CheckValueSelected(ListIntTypeDoc, "2") && txtFilePath.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('File Path is Mandatory when System Integration Mode Bulk is Selected')", true);
                return false;
            }

            if (rdoCERSAIIntList.SelectedIndex == -1)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Select CERSAI Intergration Type')", true);
                return false;
            }

            if (rdoCERSAIIntList.SelectedValue == "1")
            {
                if (txtCERSAISFTPPath.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Select SFTP Path')", true);
                    return false;
                }

                if (txtCERSAIUserNameKey.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Select SFTP User Name')", true);
                    return false;
                }


                if (txtCERSAIPassKey.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Select SFTP Password')", true);
                    return false;
                }
            }
            else if (rdoCERSAIIntList.SelectedValue == "2")
            {
                if (txtCKYCSFTPPath.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Select SFTP Path')", true);
                    return false;
                }

                if (txtCKYCUserNameKey.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Select SFTP User Name')", true);
                    return false;
                }


                if (txtCKYCPassKey.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Select SFTP Password')", true);
                    return false;
                }
            }

            //if (ddlMatch.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Probable Match')", true);
            //    return false;
            //}

            if (ddlSearch.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Search Dropdown')", true);
                return false;
            }

            if (ddlDownload.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Download Dropdown')", true);
                return false;
            }

            if (ddlDownload.SelectedValue == "1" || ddlSearch.SelectedValue == "1")
            {
                if (txtDSPath.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Enter Digital Signature Path')", true);
                    return false;
                }

                if (txtPubKey.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Enter Digital Signature Public Key')", true);
                    return false;
                }

                if (txtPrvKey.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Enter Digital Signature Private Key')", true);
                    return false;
                }
            }
            return true;
        }

        protected void chkListIntType_SelectedIndexChange(object sender, EventArgs e)
        {
            filePath.Visible = cu.CheckValueSelected(ListIntTypeDoc, "2");
            txtFilePath.Text = "";

        }

        protected void rdoCERSAIIntList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoCERSAIIntList.SelectedValue == "1")
            {
                SFTPDetails.Visible = true;
                BULKDetails.Visible = false;
                txtCERSAISFTPPath.Text = "";
                txtCERSAIUserNameKey.Text = "";
                txtCERSAIPassKey.Text = "";
            }
            else if (rdoCERSAIIntList.SelectedValue == "2")
            {
                SFTPDetails.Visible = false;
                BULKDetails.Visible = true;
                txtCKYCSFTPPath.Text = "";
                txtCKYCUserNameKey.Text = "";
                txtCKYCPassKey.Text = "";
            }
        }

        protected void ListIntTypeData_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("@LookupCode", "IntDocument");
                ht.Add("@DataType", string.Join(",", cu.GetIntegrationValues(ListIntTypeData))) ;
                DataTable dt = objDAL.GetDataTable("Prc_GetDocTypeForIntegration", ht, "DefaultConn");
                cu.FillDropDown(ListIntTypeDoc, dt, "ParamValue", "ParamDesc");
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, "ApplicationConfig.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
    }
}