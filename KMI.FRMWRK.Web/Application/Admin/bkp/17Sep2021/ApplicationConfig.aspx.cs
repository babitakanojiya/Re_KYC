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
using System.Web.Services; //added by rutuja
using Newtonsoft.Json;

namespace KMI.FRMWRK.Web.Application.Admin
{
    public partial class ApplicationConfig : System.Web.UI.Page
    {
        DataAccessLayer objDAL = new DataAccessLayer();
        ErrorLog objErr = new ErrorLog();
        CommonUtility cu = new CommonUtility();
        int AppId;
        string UserID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //strUserId = Request.QueryString["UserID"].ToString();
                    //GetFIMissingFields();
                    if (Session["UserId"] != null)
                    {
                        UserID = Session["UserId"].ToString();
                    }

                    txtCKYCPassKey.Attributes["type"] = "password";

                    ListFISMW1.SelectedIndex = 0;

                    ListFISMW2.SelectedIndex = 0;


                    SFTPDetails.Visible = false;
                    BULKDetails.Visible = false;
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;

                    cu.BindLookupFromUSRMGMT(ListConstType, "ConstType", false);
                    cu.BindLookupFromUSRMGMT(ListIntTypeData, "IntData", false);
                    //cu.BindLookupFromUSRMGMT(ListIntTypeDoc, "IntDocument", false);
                    cu.BindLookupFromUSRMGMT(ddlMatch, "ProbableMatch", false);
                    cu.BindLookupFromUSRMGMT(ddlSearch, "Search", true);
                    cu.BindLookupFromUSRMGMT(ddlDownload, "Download", true);

                    DataTable dt = GetEntitySetup();
                    if (dt.Rows.Count > 0)
                    {
                        if (UserID != "systemadmin")
                        {
                            cu.BindLookupFromUSRMGMT(ListIntTypeData, "SFTDefault", false);
                            ListIntTypeData.SelectedValue = "6";
                            ListIntTypeDoc.SelectedValue = "2";
                            //ListIntTypeData.SelectedItem.Text = "FTP \ SFTP";
                            filePath.Attributes.Add("style", "display:none");
                            div22.Attributes.Add("style", "display:none");
                            div23.Attributes.Add("style", "display:none");
                            div24.Attributes.Add("style", "display:none");
                            div25.Attributes.Add("style", "display:none");
                            div26.Attributes.Add("style", "display:none");
                            divOfficeandUSer.Attributes.Add("style", "display:none");
                        }

                        ViewState["Mode"] = "U";
                        ViewState["Data"] = dt;
                        string Const_code = Convert.ToString(dt.Rows[0]["ConstType"]);
                        string IntData = Convert.ToString(dt.Rows[0]["IntTypeForData"]);
                        string IntDoc = Convert.ToString(dt.Rows[0]["IntTypeForDoc"]);
                        if (Convert.ToString(dt.Rows[0]["InstitutionCode"]) == "")
                        {
                            txtInsCode.Enabled = true;
                        }
                        else
                        {
                            txtInsCode.Enabled = false;
                        }
                        BindMultiSelectData(ListConstType, Const_code);
                        BindMultiSelectData(ListIntTypeData, IntData);

                        ListIntTypeData_SelectedIndexChanged(sender, EventArgs.Empty);
                        BindMultiSelectData(ListIntTypeDoc, IntDoc);

                        //Convert.ToString(dt.Rows[0]["RecID"]);
                        //Convert.ToString(dt.Rows[0]["CreatedBy"]);
                        //Convert.ToString(dt.Rows[0]["CreatedDtim"]);
                        //Convert.ToString(dt.Rows[0]["UpdatedBy"]);
                        //Convert.ToString(dt.Rows[0]["UpdatedDtim"]);

                        btnClear.Visible = false;
                        txtEntityName.Enabled = false;
                        txtbranch.Enabled = false;
                        txtregion.Enabled = false;
                        // txtInsCode.Enabled = false;
                        txtDSPath.Enabled = false;
                        txtPrvKey.Enabled = false;
                        txtPubKey.Enabled = false;
                        cbFIRef.Enabled = false;
                        cbRelRef.Enabled = false;
                        txtPrefix.Enabled = false;
                        txtFIRef.Enabled = false;
                        txtRelRef.Enabled = false;

                        txtEntityName.Text = Convert.ToString(dt.Rows[0]["EntityName"]);
                        txtInsCode.Text = Convert.ToString(dt.Rows[0]["InstitutionCode"]);

                        txtFilePath.Text = Convert.ToString(dt.Rows[0]["FilePath"]);
                        //  rdoCERSAIIntList.SelectedValue = Convert.ToString(dt.Rows[0]["CersaiIntType"]);

                        //rdoCERSAIIntList_SelectedIndexChanged(sender, EventArgs.Empty);

                        ListFISMW1_SelectedIndexChanged(sender, EventArgs.Empty);

                        string CersaiIntType = Convert.ToString(dt.Rows[0]["CersaiIntType"]);

                        BindMultiSelectData(ListFISMW1, CersaiIntType);

                        if (Convert.ToString(dt.Rows[0]["EmailId"]) != "")
                        {
                            divemail.Attributes.Add("style", "display:block");
                            txtEmailID.Text = Convert.ToString(dt.Rows[0]["EmailId"]);
                        }
                        txtbranch.Text = Convert.ToString(dt.Rows[0]["BranchCode"]);
                        txtregion.Text = Convert.ToString(dt.Rows[0]["RegionCode"]);

                        if (Convert.ToString(dt.Rows[0]["BranchCode"]) == "")
                        {
                            txtbranch.Enabled = true;
                        }
                        if (Convert.ToString(dt.Rows[0]["RegionCode"]) == "")
                        {
                            txtregion.Enabled = true;
                        }
                        txtCERSAISFTPPath.Text = Convert.ToString(dt.Rows[0]["CERSAIPath"]);
                        txtCERSAIUserNameKey.Text = Convert.ToString(dt.Rows[0]["CERSAIUserName"]);
                        txtCERSAIPassKey.Text = Convert.ToString(dt.Rows[0]["CERSAIPassword"]);
                        txtCERSAIPassKey.Text = "************";
                        txtCERSAIPassKey.Enabled = false;

                        txtCERSAIPassKey.TextMode.Equals("Password");
                        txtCKYCSFTPPath.Text = Convert.ToString(dt.Rows[0]["CKYCPath"]);
                        txtCKYCUserNameKey.Text = Convert.ToString(dt.Rows[0]["CKYCUserName"]);
                        txtCKYCPassKey.Text = Convert.ToString(dt.Rows[0]["CKYCPassword"]);

                        ddlMatch.SelectedValue = Convert.ToString(dt.Rows[0]["ProbableMatch"]);
                        ddlSearch.SelectedValue = Convert.ToString(dt.Rows[0]["Search"]);
                        ddlDownload.SelectedValue = Convert.ToString(dt.Rows[0]["Download"]);
                        txtDSPath.Text = Convert.ToString(dt.Rows[0]["DSPath"]);
                        txtPubKey.Text = Convert.ToString(dt.Rows[0]["PublicKey"]);
                        txtPrvKey.Text = Convert.ToString(dt.Rows[0]["PrivateKey"]);
                        if (Convert.ToString(dt.Rows[0]["DSPath"]) == "")
                        {
                            txtDSPath.Enabled = true;
                        }
                        if (Convert.ToString(dt.Rows[0]["PublicKey"]) == "")
                        {
                            txtPubKey.Enabled = true;
                        }
                        if (Convert.ToString(dt.Rows[0]["PrivateKey"]) == "")
                        {
                            txtPrvKey.Enabled = true;
                        }

                        if (Convert.ToString(dt.Rows[0]["FIRefPrefix"]) != "")
                        {
                            txtPrefix.Text = Convert.ToString(dt.Rows[0]["FIRefPrefix"]);
                            txtFIRef.Text = Convert.ToString(dt.Rows[0]["FIRefNo"]);
                            txtRelRef.Text = Convert.ToString(dt.Rows[0]["RelRefNo"]);
                        }
                        btnSave.Visible = false;
                        btnUpdate.Visible = true;
                        if (Convert.ToString(dt.Rows[0]["FIRefNo"]) == "")
                        {
                            cbFIRef.Enabled = true;
                            ///txtFIRef.Enabled = false;

                        }
                        if (Convert.ToString(dt.Rows[0]["RelRefNo"]) == "")
                        {
                            cbRelRef.Enabled = true;
                            //txtRelRef.Enabled = false;
                        }

                    }

                    FilldgCmp();
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, "ApplicationConfig.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        public void FilldgCmp()
        {
            Hashtable htnew = new Hashtable();
            DataSet dsnew = new DataSet();
            htnew.Add("OFFICE_CODE", 1);
            dsnew = objDAL.GetDataSet("PRC_GET_OFFICE_DETAILS", htnew, "DefaultConn");

            dgCmp.DataSource = dsnew;
            dgCmp.DataBind();

        }

        protected void dgCmp_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView dgCntst = (GridView)e.Row.FindControl("dgCntst");
                Label lnk = (Label)e.Row.FindControl("lnkCmpCode");
                Hashtable htnew2 = new Hashtable();
                DataSet dsnew2 = new DataSet();
                htnew2.Add("OFFICE_CODE", lnk.Text.ToString());
                dsnew2 = objDAL.GetDataSet("PRC_GET_USER_DETAILS", htnew2, "DefaultConn");

                dgCntst.DataSource = dsnew2;
                dgCntst.DataBind();

            }

        }
        public void BindMultiSelectData(ListBox listBox, string data)
        {
            try
            {
                char[] arr = { ',' };
                string[] data_values = data.Split(arr);

                for (int i = 0; i < data_values.Length; i++)
                {
                    foreach (ListItem item in listBox.Items)
                    {
                        if (item.Value == data_values[i])
                        {
                            item.Selected = true;
                        }
                    }
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
                    ht.Add("ConstType", string.Join(",", cu.GetIntegrationValues(ListConstType))); //ddlConstType.SelectedValue.Trim() 
                    ht.Add("DataType", string.Join(",", cu.GetIntegrationValues(ListIntTypeData)));
                    ht.Add("DocumentType", string.Join(",", cu.GetIntegrationValues(ListIntTypeDoc)));
                    ht.Add("FilePath", txtFilePath.Text);
                    if (cbFIRef.Checked == true)
                    {
                        ht.Add("FIRefPrefix", txtPrefix.Text);
                        ht.Add("FIRefNo", txtFIRef.Text);
                    }
                    else
                    {
                        ht.Add("FIRefPrefix", System.DBNull.Value);
                        ht.Add("FIRefNo", System.DBNull.Value);
                    }
                    if (cbRelRef.Checked == true)
                    {
                        ht.Add("RelRefNo", txtRelRef.Text);
                    }
                    else
                    {
                        ht.Add("RelRefNo", System.DBNull.Value);
                    }
                    //  ht.Add("CersaiIntType", rdoCERSAIIntList.SelectedValue);
                    ht.Add("CersaiIntType", string.Join(",", cu.GetIntegrationValues(ListFISMW1)));
                    ht.Add("CersaiIntTypeData", string.Join(",", cu.GetIntegrationValues(ListFISMW2)));
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
                    ht.Add("EmailId", txtEmailID.Text.Trim());  //added by ruutuja
                    ht.Add("BranchCode", txtbranch.Text.Trim());  //added by ruutuja
                    ht.Add("RegionCode", txtregion.Text.Trim());  //added by ruutuja
                    objDAL.GetDataSet("Prc_InsEntitySetup", ht, "DefaultConn");
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "SuccessAlert('Entity Setup is Done Successfully')", true);
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
                if (validate())
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("EntityName", txtEntityName.Text.Trim());
                    ht.Add("InstitutionCode", txtInsCode.Text.Trim());
                    ht.Add("ConstType", string.Join(",", cu.GetIntegrationValues(ListConstType))); //ddlConstType.SelectedValue.Trim() 
                    ht.Add("DataType", string.Join(",", cu.GetIntegrationValues(ListIntTypeData)));
                    ht.Add("DocumentType", string.Join(",", cu.GetIntegrationValues(ListIntTypeDoc)));
                    ht.Add("FilePath", txtFilePath.Text);
                    //ht.Add("CersaiIntType", rdoCERSAIIntList.SelectedValue);
                    ht.Add("CersaiIntType", string.Join(",", cu.GetIntegrationValues(ListFISMW1)));
                    ht.Add("CersaiIntTypeData", string.Join(",", cu.GetIntegrationValues(ListFISMW2)));
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
                    ht.Add("EmailId", txtEmailID.Text.Trim());  //added by ruutuja
                    ht.Add("BranchCode", txtbranch.Text.Trim());  //added by ruutuja
                    ht.Add("RegionCode", txtregion.Text.Trim());  //added by ruutuja
                    ht.Add("FIPrefix", txtPrefix.Text.Trim());  //added by ruutuja
                    ht.Add("FIRefNo", txtFIRef.Text.Trim());  //added by ruutuja
                    ht.Add("RelRefNo", txtRelRef.Text.Trim());  //added by ruutuja
                    objDAL.GetDataSet("Prc_UpdEntitySetup", ht, "DefaultConn");
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "SuccessAlert('Entity Setup is Updated Successfully')", true);
                }
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
            //ListConstType.SelectedIndex

            if (cu.GetIntegrationValues(ListConstType).Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Select Constitution Code')", true);
                return false;
            }

            if (txtbranch.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Enter Branch Code')", true);
                return false;
            }

            if (txtregion.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Enter Region Code')", true);
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

            string IntgtnData = string.Join(",", cu.GetIntegrationValues(ListIntTypeData));
            string IntrgtnDoc = string.Join(",", cu.GetIntegrationValues(ListIntTypeDoc));
            if (IntgtnData.Contains("6") == true)
            {
                if (IntrgtnDoc.Contains("2") == true)
                {
                    if (txtEmailID.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Enter OutPut Email Id')", true);
                        return false;
                    }
                }
                else
                {
                    txtEmailID.Text = "";
                }

            }
            else
            {
                txtEmailID.Text = "";
            }

            if (filePath.Style.Value == null)
            {
                if (cu.CheckValueSelected(ListIntTypeDoc, "2") && txtFilePath.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Input File Path is Mandatory when System Integration Mode Bulk is Selected')", true);
                    return false;
                }
            }



            //if (rdoCERSAIIntList.SelectedIndex == -1)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Select CERSAI Intergration Type')", true);
            //    return false;
            //}
            if (div22.Style.Value == null)
            {
                if (ListFISMW1.SelectedIndex == -1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Select CERSAI Intergration Type')", true);
                    return false;
                }
                if (ListFISMW1.SelectedIndex == 0)
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
                else if (ListFISMW1.SelectedIndex == 1)
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
            }



            //if (ddlMatch.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Probable Match')", true);
            //    return false;
            //}
            if (div24.Style.Value == null)
            {
                if (ddlSearch.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Select Search Dropdown')", true);
                    return false;
                }

                if (ddlDownload.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Alert", "alert('Please Select Download Dropdown')", true);
                    return false;
                }


            }
            if (div25.Style.Value == null)
            {
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
            txtCERSAISFTPPath.Text = "";
            txtCERSAIUserNameKey.Text = "";
            txtCERSAIPassKey.Text = "";
            txtCKYCSFTPPath.Text = "";
            txtCKYCUserNameKey.Text = "";
            txtCKYCPassKey.Text = "";


            if (rdoCERSAIIntList.SelectedValue == "1")
            {
                SFTPDetails.Visible = true;
                BULKDetails.Visible = false;
                if (Convert.ToString(ViewState["Mode"]) == "U")
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    if (Convert.ToString(dt.Rows[0]["CersaiIntType"]) == "1")
                    {
                        txtCERSAISFTPPath.Text = Convert.ToString(dt.Rows[0]["CERSAIPath"]);
                        txtCERSAIUserNameKey.Text = Convert.ToString(dt.Rows[0]["CERSAIUserName"]);
                        txtCERSAIPassKey.Text = Convert.ToString(dt.Rows[0]["CERSAIPassword"]);
                    }
                }
            }
            else if (rdoCERSAIIntList.SelectedValue == "2")
            {
                SFTPDetails.Visible = false;
                BULKDetails.Visible = true;

                if (Convert.ToString(ViewState["Mode"]) == "U")
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    if (Convert.ToString(dt.Rows[0]["CersaiIntType"]) == "2")
                    {
                        txtCKYCSFTPPath.Text = Convert.ToString(dt.Rows[0]["CKYCPath"]);
                        txtCKYCUserNameKey.Text = Convert.ToString(dt.Rows[0]["CKYCUserName"]);
                        txtCKYCPassKey.Text = Convert.ToString(dt.Rows[0]["CKYCPassword"]);
                    }
                }
            }
        }

        public DataTable GetEntitySetup()
        {
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", Session["UserId"]);  //Added by Rutuja
            return objDAL.GetDataTable("Prc_GetEntitySetup", ht, "DefaultConn");
        }

        protected void ListIntTypeData_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("@LookupCode", "IntDocument");
                ht.Add("@DataType", string.Join(",", cu.GetIntegrationValues(ListIntTypeData)));
                DataTable dt = objDAL.GetDataTable("Prc_GetDocTypeForIntegration", ht, "DefaultConn");
                cu.FillDropDown(ListIntTypeDoc, dt, "ParamValue", "ParamDesc");
                CheckEmail();
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, "ApplicationConfig.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void cbFIRef_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFIRef.Checked == true)
            {
                txtPrefix.Enabled = true;
                txtFIRef.Enabled = true;
            }
            else
            {
                txtPrefix.Enabled = false;
                txtFIRef.Enabled = false;
            }
        }

        protected void cbRelRef_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRelRef.Checked == true)
            {
                txtRelRef.Enabled = true;
            }
            else
            {
                txtRelRef.Enabled = false;
            }
        }

        protected void ListFISMW1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCERSAISFTPPath.Text = "";
            txtCERSAIUserNameKey.Text = "";
            txtCERSAIPassKey.Text = "";
            txtCKYCSFTPPath.Text = "";
            txtCKYCUserNameKey.Text = "";
            txtCKYCPassKey.Text = "";


            if (ListFISMW1.SelectedIndex == 0)
            {
                SFTPDetails.Visible = true;
                BULKDetails.Visible = false;
                if (Convert.ToString(ViewState["Mode"]) == "U")
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    if (Convert.ToString(dt.Rows[0]["CersaiIntType"]).Replace(",", "") == "1")
                    {
                        txtCERSAISFTPPath.Text = Convert.ToString(dt.Rows[0]["CERSAIPath"]);
                        txtCERSAIUserNameKey.Text = Convert.ToString(dt.Rows[0]["CERSAIUserName"]);
                        txtCERSAIPassKey.Text = Convert.ToString(dt.Rows[0]["CERSAIPassword"]);
                    }
                    if (Convert.ToString(dt.Rows[0]["CersaiIntType"]).Replace(",", "") == "12")
                    {
                        txtCERSAISFTPPath.Text = Convert.ToString(dt.Rows[0]["CERSAIPath"]);
                        txtCERSAIUserNameKey.Text = Convert.ToString(dt.Rows[0]["CERSAIUserName"]);
                        txtCERSAIPassKey.Text = Convert.ToString(dt.Rows[0]["CERSAIPassword"]);

                        txtCKYCSFTPPath.Text = Convert.ToString(dt.Rows[0]["CKYCPath"]);
                        txtCKYCUserNameKey.Text = Convert.ToString(dt.Rows[0]["CKYCUserName"]);
                        txtCKYCPassKey.Text = Convert.ToString(dt.Rows[0]["CKYCPassword"]);
                    }
                }
            }
            else if (ListFISMW1.SelectedIndex == 1)
            {
                SFTPDetails.Visible = false;
                BULKDetails.Visible = true;

                if (Convert.ToString(ViewState["Mode"]) == "U")
                {
                    DataTable dt = (DataTable)ViewState["Data"];
                    if (Convert.ToString(dt.Rows[0]["CersaiIntType"]).Replace(",", "") == "2")
                    {
                        txtCKYCSFTPPath.Text = Convert.ToString(dt.Rows[0]["CKYCPath"]);
                        txtCKYCUserNameKey.Text = Convert.ToString(dt.Rows[0]["CKYCUserName"]);
                        txtCKYCPassKey.Text = Convert.ToString(dt.Rows[0]["CKYCPassword"]);
                    }

                    if (Convert.ToString(dt.Rows[0]["CersaiIntType"]).Replace(",", "") == "12")
                    {
                        txtCERSAISFTPPath.Text = Convert.ToString(dt.Rows[0]["CERSAIPath"]);
                        txtCERSAIUserNameKey.Text = Convert.ToString(dt.Rows[0]["CERSAIUserName"]);
                        txtCERSAIPassKey.Text = Convert.ToString(dt.Rows[0]["CERSAIPassword"]);

                        txtCKYCSFTPPath.Text = Convert.ToString(dt.Rows[0]["CKYCPath"]);
                        txtCKYCUserNameKey.Text = Convert.ToString(dt.Rows[0]["CKYCUserName"]);
                        txtCKYCPassKey.Text = Convert.ToString(dt.Rows[0]["CKYCPassword"]);
                    }
                }
            }

        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static string getHeaderbyIDMethod(string id)
        {
            try
            {
                string ID;
                ID = id;

                DataAccessLayer dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                //DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                System.Collections.Hashtable ht = new System.Collections.Hashtable();
                ht.Add("@SegCode", ID);
                ht.Add("@KYCCategory", "01");
                ds = dataAccessLayer.GetDataSet("Prc_CBFrm_GetFrmSegmentDtls", ht);
                string str = ds.Tables[0].Rows[0]["Desc"].ToString();
                // string JSONString = JSONConvert.SerializeObject(ds);
                return str;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }

        protected void ListIntTypeDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckEmail();
        }

        public void CheckEmail()
        {
            string IntgtnData = string.Join(",", cu.GetIntegrationValues(ListIntTypeData));
            string IntrgtnDoc = string.Join(",", cu.GetIntegrationValues(ListIntTypeDoc));
            if (IntgtnData.Contains("6") == true)
            {
                if (IntrgtnDoc.Contains("2") == true)
                {
                    divemail.Attributes.Add("style", "display:block");
                }
                else
                {
                    divemail.Attributes.Add("style", "display:none");
                    //  txtEmailID.Text = "";
                }
            }
            else
            {
                divemail.Attributes.Add("style", "display:none");
                //txtEmailID.Text = "";
            }
        }

    }
}