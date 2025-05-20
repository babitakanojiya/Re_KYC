using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using KMI.FRMWRK.DAL;
using KMI.FRMWRK.Security;
using System.Data.SqlClient;
using System.Configuration;
using KMI.FRMWRK.Multilingual;
using System.Collections.ObjectModel;
using KMI.FRMWRK.Web.Admin;
using KMI.FRMWRK.Security.Cryptography;
using KMI.FRMWRK.BAL;

namespace KMI.FRMWRK.Web.Account
{
    public partial class RegisterUser : System.Web.UI.Page
    {
        #region variable declaration
        DataAccessLayer dataAccessLayer;
        Hashtable htbl;
        DataSet ds;
        BAL.AuthorizationBAL oAut = new BAL.AuthorizationBAL();
        ICryptography Cryptography;
        private string appMode = string.Empty;
        private string UserID = string.Empty;
        private string createdUserID = string.Empty;
        private string strUserLangNumNum;
        private SqlConnection constr1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConn"].ToString());
        private string UserGroupCode;
        public MultilingualManager olng;
        string strSQLGrpName;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/ErrorSession.aspx");
            }
            else
            {
                UserID = Session["UserId"].ToString();
            }

            if (Request.QueryString["userid"]!=null)
            {
                createdUserID = Request.QueryString["userid"].ToString();
            }
            if (Request.QueryString["mode"]!=null)
            {
                appMode = Request.QueryString["mode"].ToString();
            }
            else
                appMode = "new";

            string pwd = txtPwd.Text;
            txtPwd.Attributes.Add("value", pwd);
            string pwdConfirm = txtConfirmPwd.Text;
            txtConfirmPwd.Attributes.Add("value", pwd);
            strUserLangNumNum = HttpContext.Current.Session["UserLangNum"].ToString();
            olng = new MultilingualManager("DefaultConn", "RegisterUser.aspx", strUserLangNumNum);
            if (HttpContext.Current.Session["IsSystemAdmin"].ToString() == "True")
            {
                string str = HttpContext.Current.Session["IsSystemAdmin"].ToString();
                chkIsSysAdmin.Visible = true;
            }
            else
            {
                chkIsSysAdmin.Visible = false;
            }

            if (!Page.IsPostBack)
            {
                InitializeControl();
                cboNonLife.Visible = false;
                lblNonLife.Visible = false;
                DisabledField();

                BindGridview();
                if (appMode.Equals("edit"))
                {
                    LoadUserDetails();
                    //commented by kalpak
                    //BindGridview();
                    rdInternal.Checked = true;
                    rdExternal.Enabled = false;
                    lblPwd.Visible = false;
                    txtPwd.Visible = false;
                    lblConfirmPwd.Visible = false;
                    txtConfirmPwd.Visible = false;
                    btnResetpw.Visible = true;
                    lblTitle.Text = "Edit User";
                    lblDplUserId.Text = "User ID = " + UserID + "";
                    EnabledField();
                }
                else
                {
                    btnResetpw.Visible = false;
                    lblTitle.Text = "Create New User";
                    cboStatus.SelectedValue = "0";
                }

                linkClear.Visible = false;
                linkSave.Visible = true;
            }
        }

        private void InitializeControl()
        {
            try
            {
                //view1
                lblTitle.Text = olng.GetItemDesc("lblTitle.Text");
                lblModVer.Text = olng.GetItemDesc("lblModVer.Text");
                L1.Text = olng.GetItemDesc("L1.Text");   //User Details
                L2.Text = olng.GetItemDesc("L2.Text");   //User Sanctioning
                lblUserType.Text = olng.GetItemDesc("lblUserType.Text");
                lblUserName.Text = olng.GetItemDesc("lblUserName.Text");
                lblUserID.Text = olng.GetItemDesc("lblUserID.Text");
                lblerror.Text = olng.GetItemDesc("lblerror.Text");
                lblPwd.Text = olng.GetItemDesc("lblPwd.Text");
                lblConfirmPwd.Text = olng.GetItemDesc("lblConfirmPwd.Text");
                lblStatus.Text = olng.GetItemDesc("lblStatus.Text");
                lblLogonName.Text = olng.GetItemDesc("lblLogonName.Text");
                lblLanguage.Text = olng.GetItemDesc("lblLanguage.Text");
                btnResetpw.Text = olng.GetItemDesc("btnResetpw.Text");
                chkIsSysAdmin.Text = "&nbsp;" + olng.GetItemDesc("chkIsSysAdmin.Text");
                chkIsUsrAdmin.Text = "&nbsp;" + olng.GetItemDesc("chkIsUsrAdmin.Text");
                txtEmail.Text = olng.GetItemDesc("txtEmail.Text");
                txtDob.Text = olng.GetItemDesc("txtDob.Text");
                chkTimingRestrict.Text = olng.GetItemDesc("chkTimingRestrict.Text");
                btnEditTime.Text = olng.GetItemDesc("btnEditTime.Text");
                chkIsForumModerator.Text = olng.GetItemDesc("chkIsForumModerator.Text");
                chkDownload.Text = olng.GetItemDesc("chkDownload.Text");
                chkLogonLocally.Text = olng.GetItemDesc("chkLogonLocally.Text");

                //view2
                L21.Text = olng.GetItemDesc("L1.Text");
                L22.Text = olng.GetItemDesc("L2.Text");
                lblSelectedModule.Text = olng.GetItemDesc("lblSelectedModule.Text");
                btnClose.Text = olng.GetItemDesc("btnCancel.Text");
                linkSave.Text = olng.GetItemDesc("linkSave.Text");
                linkCancel.Text = olng.GetItemDesc("btnCancel.Text");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //protected void GridView1_DataBound(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < GridView1.Rows.Count; i++)
        //    {
        //        LinkButton linkInsert = (LinkButton)GridView1.Rows[i].Cells[0].FindControl("linkInsert");
        //        linkInsert.Text = olng.GetItemDesc("linkInsert.Text");
        //        LinkButton linkPreview = (LinkButton)GridView1.Rows[i].Cells[5].FindControl("linkPreview");
        //        linkPreview.Text = olng.GetItemDesc("linkPreview.Text");
        //    }
        //    if (GridView1.Rows.Count > 0)
        //    {
        //        GridView1.HeaderRow.Cells[0].Text = olng.GetItemDesc("gvHeader0.Text");
        //        GridView1.HeaderRow.Cells[2].Text = olng.GetItemDesc("gvHeader2.Text");
        //        GridView1.HeaderRow.Cells[5].Text = olng.GetItemDesc("linkPreview.Text");
        //    }
        //}

        public void LoadDataSet(string Userid, string moduletype, string CarrierCode, string IsSystemAdmin)
        {
            try
            {
                ds = new DataSet();
                DataSet dss = new DataSet();
                htbl = new Hashtable();
                dataAccessLayer = new DataAccessLayer();

                string strUserLangNum = HttpContext.Current.Session["UserLangNum"].ToString();

                if (!string.IsNullOrEmpty(IsSystemAdmin))
                {
                    htbl.Add("@UserID", Userid);
                    htbl.Add("@CarrierCode", CarrierCode);
                    htbl.Add("@ModuleGroupCode", lblUGID.Text);
                    htbl.Add("@UserLanguage", strUserLangNum);
                    htbl.Add("@ModuleType", moduletype);
                    htbl.Add("@IsSystemAdmin", IsSystemAdmin);
                    ds = dataAccessLayer.GetDataSet("prc_ModuleUsrSuNew", htbl);
                    htbl.Clear();
                }
                
                //TODO : Need to check with sir, Commented by kalpak
                ////select module where CarrierCode belong 0 and itself
                //if (!string.IsNullOrEmpty(lblUGID.Text))
                //{
                //    htbl.Add("@strUserLangNum", strUserLangNum);
                //    htbl.Add("@UserGroupName", lblUGName.Text);
                //    htbl.Add("@CarrierCode", lblUGCC.Text);
                //    dss = dataAccessLayer.GetDataSet("pri_ModuleAccessMatrix", htbl);
                //    htbl.Clear();
                //}

                if (moduletype == "Template")
                {
                    //preview the tree view
                    PopulateModuleTreeView((DataTable)dss.Tables[0]);
                }
                else
                {
                    //load the tree view
                    if (ds != null && ds.Tables.Count > 0)
                        PopulateTreeView((DataTable)ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataAccessLayer = null;
                htbl = null;
            }
        }

        public void PopulateModuleTreeView(DataTable dt)
        {
            try
            {
                int iCount = dt.Rows.Count;
                TreeNode ModuleList = new TreeNode();
                Collection<TreeNode> AllNode = new Collection<TreeNode>();
                TreeNode finalNode = new TreeNode();
                Collection<string> nodeID = new Collection<string>();

                for (int icount = 0; icount < dt.Rows.Count; icount++)
                {
                    TreeNode CurNode = new TreeNode(dt.Rows[icount]["ResDsc"].ToString(), dt.Rows[icount]["ModuleID"].ToString());
                    CurNode.NavigateUrl = "javascript:void(0)";
                    AllNode.Add(CurNode);
                    nodeID.Add(dt.Rows[icount]["ModuleID"].ToString());

                    if (dt.Rows[icount]["AccessStatus"].ToString() != "0")
                    { CurNode.Checked = true; }
                    else { CurNode.Checked = false; }
                }

                TrVModule.Nodes.Clear();
                for (int icount = 0; icount < dt.Rows.Count; icount++)
                {
                    TreeNode CurNode = AllNode[nodeID.IndexOf(dt.Rows[icount]["ModuleID"].ToString())];
                    CurNode.NavigateUrl = "javascript:void(0)";
                    if (dt.Rows[icount]["ParentModuleID"].ToString() != "")
                        AllNode[nodeID.IndexOf(dt.Rows[icount]["ParentModuleID"].ToString())].ChildNodes.Add(CurNode);
                    else
                        TrVModule.Nodes.Add(CurNode);
                }

                TrVModule.ExpandAll();

                DataTable dtt = null;
                string UserGroupGrp = null;

                //select all the user group have been selected
                if (!string.IsNullOrEmpty(createdUserID))
                {
                    //UserID = Request.QueryString["userid"].ToString();
                    dataAccessLayer = new DataAccessLayer();
                    htbl = new Hashtable();

                    htbl.Add("@UserID", createdUserID);
                    DataSet ds1 = dataAccessLayer.GetDataSet("prc_GetUserModuleGroup", htbl);
                    dtt = ds1.Tables[0];

                    htbl = null;
                    ds1 = null;
                }

                for (int icount = 0; icount < dtt.Rows.Count; icount++)
                {
                    UserGroupGrp += dtt.Rows[icount]["UserGroupCode"].ToString() + ";";
                }

                lblUserGroupGrp.Text = UserGroupGrp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataAccessLayer = null;
                dt = null;
            }
        }

        public void PopulateTreeView(DataTable dt)
        {
            try
            {
                int iCount = dt.Rows.Count;
                TreeNode ModuleList = new TreeNode();
                Collection<TreeNode> AllNode = new Collection<TreeNode>();
                TreeNode finalNode = new TreeNode();
                Collection<string> nodeID = new Collection<string>();

                for (int icount = 0; icount < dt.Rows.Count; icount++)
                {
                    TreeNode CurNode = new TreeNode(dt.Rows[icount]["ResDsc"].ToString(), dt.Rows[icount]["ModuleID"].ToString());
                    CurNode.NavigateUrl = "javascript:void(0)";
                    AllNode.Add(CurNode);
                    nodeID.Add(dt.Rows[icount]["ModuleID"].ToString());

                    if (dt.Rows[icount]["AccessStatus"].ToString() != "0")
                    { CurNode.Checked = true; }
                    else { CurNode.Checked = false; }
                }

                TrVUser.Nodes.Clear();
                for (int icount = 0; icount < dt.Rows.Count; icount++)
                {
                    TreeNode CurNode = AllNode[nodeID.IndexOf(dt.Rows[icount]["ModuleID"].ToString())];
                    CurNode.NavigateUrl = "javascript:void(0)";
                    if (dt.Rows[icount]["ParentModuleID"].ToString() != "")
                        AllNode[nodeID.IndexOf(dt.Rows[icount]["ParentModuleID"].ToString())].ChildNodes.Add(CurNode);
                    else
                        TrVUser.Nodes.Add(CurNode);
                }

                TrVUser.ExpandAll();

                DataTable dtt = null;
                string UserGroupGrp = null;

                //select all the user group have been selected
                if (!string.IsNullOrEmpty(createdUserID))
                {
                    //UserID = Request.QueryString["userid"].ToString();

                    dataAccessLayer = new DataAccessLayer();
                    htbl = new Hashtable();

                    htbl.Add("@UserID", createdUserID);
                    DataSet ds1 = dataAccessLayer.GetDataSet("prc_GetUserModuleGroup", htbl);
                    dtt = ds1.Tables[0];

                    htbl = null;
                    ds1 = null;
                }

                for (int icount = 0; icount < dtt.Rows.Count; icount++)
                {
                    UserGroupGrp += dtt.Rows[icount]["UserGroupCode"].ToString() + ";";
                }
                lblUserGroupGrp.Text = UserGroupGrp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataAccessLayer = null;
                dt = null;
            }
        }

        protected void linkClear_Click(object sender, EventArgs e)
        {
            try
            {
                //UserID = Request.QueryString["userid"].ToString();

                if (!string.IsNullOrEmpty(createdUserID))
                {
                    dataAccessLayer = new DataAccessLayer();
                    htbl = new Hashtable();

                    htbl.Add("@UserID", createdUserID);
                    //TODO : Need to check with sir - it deletes all 
                    dataAccessLayer.ExecuteScalar("prc_DeleteUserGroupAcs", htbl);
                }

                LoadDataSet(UserID.Trim(), "", "", "");
                MultiView1.ActiveViewIndex = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataAccessLayer = null;
                htbl = null;
            }
        }

        protected void linkSave_Click(object sender, EventArgs e)
        {
            try
            {
                int cnt = 0;
                int Appcnt = 0;
                for (int i = 0; i < GridViewApp.Rows.Count; i++)
                {
                    CheckBox ChekApp = (CheckBox)GridViewApp.Rows[i].FindControl("chkApp") as CheckBox;
                    DropDownList ddlLocation = (DropDownList)GridViewApp.Rows[i].FindControl("ddlLocation") as DropDownList;
                    DropDownList ddlLoactionCode = (DropDownList)GridViewApp.Rows[i].FindControl("ddlLoactionCode") as DropDownList;
                    DropDownList ddlDepartment = (DropDownList)GridViewApp.Rows[i].FindControl("ddlDepartment") as DropDownList;
                    DropDownList ddlUserroleCode = (DropDownList)GridViewApp.Rows[i].FindControl("ddlUserroleCode") as DropDownList;
                    RadioButton rbtnDefaultApp = (RadioButton)GridViewApp.Rows[i].FindControl("rdbDefaultApp") as RadioButton;
                    DropDownList ddlStaus = (DropDownList)GridViewApp.Rows[i].FindControl("ddlAppEnblStatus") as DropDownList;
                    CheckBox chckTeamLead = (CheckBox)GridViewApp.Rows[i].FindControl("chkTeamLead") as CheckBox;
                    TextBox txtAppStrtDt = (TextBox)GridViewApp.Rows[i].FindControl("txtAppEffectDT") as TextBox;
                    TextBox txtAppEndDt = (TextBox)GridViewApp.Rows[i].FindControl("txtAppCeaseDT") as TextBox;

                    if (ChekApp.Checked == true)
                    {
                        Appcnt = Appcnt + 1;

                        if (ddlLocation.SelectedIndex == 0)
                        {
                            lblErrorMsg.Visible = true;
                            lblErrorMsg.Text = "Please select Location Type.";
                            ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>AlertMsg('Please select Location Type.')</script>");
                            SetFocus(ddlLocation);
                            return;
                        }
                        if (ddlLoactionCode.SelectedIndex == 0)
                        {
                            lblErrorMsg.Visible = true;
                            lblErrorMsg.Text = "Please select Location code.";
                            ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>AlertMsg('Please select Location code.')</script>");
                            SetFocus(ddlLoactionCode);
                            return;
                        }

                        if (rbtnDefaultApp.Checked == true)
                        {
                            cnt = cnt + 1;
                            if (ddlStaus.SelectedIndex == 0)
                            {
                                lblErrorMsg.Visible = true;
                                lblErrorMsg.Text = "Please select Application Status.";
                                ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>AlertMsg('Please select Application Status.')</script>");
                                SetFocus(ddlStaus);
                                return;
                            }
                            else if (ddlStaus.SelectedIndex == 2)
                            {
                                lblErrorMsg.Visible = true;
                                ddlStaus.SelectedIndex = 0;
                                lblErrorMsg.Text = "Kindly select Application Status Enabled.";
                                ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>AlertMsg('Kindly select Application Status Enabled.')</script>");
                                SetFocus(ddlStaus);
                                return;
                            }
                            else if (txtAppStrtDt.Text == "")
                            {
                                lblErrorMsg.Visible = true;
                                ddlStaus.SelectedIndex = 0;
                                lblErrorMsg.Text = "Kindly select Application Effective Date.";
                                ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>AlertMsg('Kindly select Application Effective Date.')</script>");
                                SetFocus(ddlStaus);
                                return;
                            }
                            else if (txtAppEndDt.Text == "")
                            {
                                lblErrorMsg.Visible = true;
                                ddlStaus.SelectedIndex = 0;
                                lblErrorMsg.Text = "Kindly select Application Cease Date.";
                                ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>AlertMsg('Kindly select Application Cease Date.')</script>");
                                SetFocus(ddlStaus);
                                return;
                            }
                        }
                    }
                }
                if (Appcnt == 0)
                {
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = "Kindly select Application.";
                    ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>AlertMsg('Kindly select Application.')</script>");
                    return;
                }
                if (cnt > 0)
                {
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = "";
                    if (txtUsrEffectiveDT.Text == "")
                    {
                        lblErrorMsg.Visible = true;
                        lblErrorMsg.Text = "Kindly select User Effective date.";
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>AlertMsg('Kindly select User Effective date.')</script>");
                        return;
                    }
                    if (txtUsrCeaseDT.Text == "")
                    {
                        lblErrorMsg.Visible = true;
                        lblErrorMsg.Text = "Kindly select User Cease date.";
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>AlertMsg('Kindly select User Cease date.')</script>");
                        return;
                    }
                    else
                    {
                        if (appMode.Equals("new"))
                        {
                            dataAccessLayer = new DataAccessLayer();
                            htbl = new Hashtable();
                            htbl.Add("@ColumnName", "UserId");
                            htbl.Add("@UserID", txtUserID.Text);
                            var ds1 = dataAccessLayer.GetDataSet("prc_GetColumnsFromUser", htbl);
                            dataAccessLayer = null;
                            if (ds1.Tables[0].Rows.Count == 0)
                            {
                                if (txtPwd.Text == txtConfirmPwd.Text)
                                {
                                    if (string.IsNullOrEmpty(oAut.IsValidPasswordLength(txtPwd.Text)))
                                    {
                                        var objUser = new User();
                                        Cryptography = new RCFEncryption();

                                        objUser.UserName = txtUserName.Text.Replace("'", "''");
                                        objUser.UserPIN = Cryptography.Encrypt(txtPwd.Text);
                                        objUser.UserStatus = int.Parse(cboStatus.SelectedValue.ToString());
                                        objUser.UserId = txtUserID.Text;
                                        objUser.UserLoginName = txtLogonName.Text;
                                        objUser.LanguageNum = cboLanguage.SelectedValue.ToString();
                                        objUser.IsSystemAdmin = chkIsSysAdmin.Checked;
                                        objUser.IsDiscussAdmin = chkIsForumModerator.Checked;
                                        objUser.RestrictAccess = chkTimingRestrict.Checked;
                                        objUser.RestrictDownload = chkDownload.Checked;
                                        objUser.UserStatus = int.Parse(cboStatus.SelectedValue.ToString());
                                        objUser.NonLife = cboNonLife.SelectedValue.ToString();
                                        objUser.UsrEmailId = txtEmail.Text.ToString();
                                        objUser.UsrDob = txtDob.Text.ToString();
                                        objUser.UsrEffectiveDT = txtUsrEffectiveDT.Text.ToString();
                                        objUser.UsrCeaseDT = txtUsrCeaseDT.Text.ToString();
                                        objUser.UsrMobNumber = txtMobNumber.Text.Trim().ToString();

                                        if (rdExternal.Checked)
                                        {
                                            objUser.UserType = 0;
                                            objUser.UserIdCode = txtUserID.Text;
                                        }
                                        else if (rdInternal.Checked)
                                        {
                                            objUser.UserType = 1;
                                            objUser.UserIdCode = txtUserID.Text;
                                        }
                                        objUser.IsAppAdmin = false;
                                        objUser.IsUsrAdmin = chkIsUsrAdmin.Checked;
                                        objUser.SessionUserId = UserID;

                                        var adminBAL = new AdminBAL();
                                        string i = adminBAL.AddNewUser(objUser);
                                        Session["LateshUserID"] = i.ToString();
                                        if (i != null)
                                        {
                                            insertgridview(i);
                                        }

                                        linkSave.Enabled = false;
                                        //added by kalpak
                                        var qryString = string.Empty;
                                        foreach (String key in Request.QueryString.AllKeys)
                                        {
                                            if (string.IsNullOrEmpty(qryString))
                                                qryString = key + "=" + Request.QueryString[key];
                                            else
                                                qryString = "&" + key + "=" + Request.QueryString[key];
                                        }

                                        //commented by kalpak
                                        Response.Redirect("~/Account/RegisterUser.aspx?" + (qryString != "" ? (qryString + "&") : qryString) + "mode=edit&userid=" + txtUserID.Text, true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + olng.GetItemDesc("Alert2") + "');", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + olng.GetItemDesc("Alert3") + "');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + olng.GetItemDesc("lblerror.Text") + "');", true);
                            }
                        }
                        else
                        {
                            //update user detail
                            dataAccessLayer = new DataAccessLayer();
                            var htbl = new Hashtable();
                            htbl.Add("@UserName", txtUserName.Text.Replace("'", "''"));
                            htbl.Add("@UserStatus", int.Parse(cboStatus.SelectedValue.ToString()));
                            htbl.Add("@LogonFailureCount", 0);
                            htbl.Add("@UserLoginName", txtLogonName.Text);
                            htbl.Add("@LanguageNum", cboLanguage.SelectedValue.ToString());
                            htbl.Add("@IsSystemAdmin", chkIsSysAdmin.Checked);
                            htbl.Add("@IsAppAdmin", "");
                            htbl.Add("@IsUsrAdmin", chkIsUsrAdmin.Checked);
                            htbl.Add("@IsDiscussAdmin", chkIsForumModerator.Checked);
                            htbl.Add("@RestrictAccess", chkTimingRestrict.Checked);
                            htbl.Add("@RestrictDownload", chkDownload.Checked);
                            htbl.Add("@UserRole", cboNonLife.SelectedValue.ToString());
                            htbl.Add("@UserMobileNo1", txtMobNumber.Text.Trim().ToString());
                            htbl.Add("@UserEmailAddress", txtEmail.Text.ToString());
                            htbl.Add("@DOB", Convert.ToDateTime(txtDob.Text));
                            htbl.Add("@EffectiveDtim", Convert.ToDateTime(txtUsrEffectiveDT.Text));
                            htbl.Add("@IneffectiveDate", Convert.ToDateTime(txtUsrCeaseDT.Text));
                            htbl.Add("@UserID", txtUserID.Text);
                            htbl.Add("@CurrentUser", UserID);
                            dataAccessLayer.ExecuteNonQuery("prc_UpdateUser", htbl);

                            htbl.Clear();
                            htbl.Add("@UserId", createdUserID);
                            dataAccessLayer.ExecuteScalar("prc_DelUserAppRoleAcs", htbl);
                            insertgridview(createdUserID);
                            dataAccessLayer = null;

                            if (!string.IsNullOrEmpty(createdUserID))
                            {
                                for (int icount = 0; icount < TrVUser.Nodes.Count; icount++)
                                {
                                    DisplayChildNodeText(TrVUser.Nodes[icount], createdUserID);
                                }
                            }

                            //TODO : Below code is commented by kalpak due to no use, need to confirm with sir
                            #region service rights
                            //if (!string.IsNullOrEmpty(createdUserID))
                            //{
                            //    GridViewRow[] ApprovalArray = new GridViewRow[dgSRARights.Rows.Count];
                            //    string strSQLRights = string.Empty;
                            //    dgSRARights.Rows.CopyTo(ApprovalArray, 0);
                            //    foreach (GridViewRow Row in ApprovalArray)
                            //    {
                            //        CheckBox chkAccessRight = (CheckBox)Row.FindControl("chkAccessRight");
                            //        Label SrvcReqTypeCode = (Label)Row.FindControl("SrvcReqTypeCode");
                            //        Label SrvcGrpCode = (Label)Row.FindControl("SrvcGrpCode");

                            //        dataAccessLayer = new DataAccessLayer();
                            //        htbl = new Hashtable();

                            //        htbl.Add("@UserID", createdUserID);
                            //        htbl.Add("@SrvcGrpCode", SrvcGrpCode.Text.Trim());
                            //        htbl.Add("@SrvcReqTypeCode", SrvcReqTypeCode.Text.Trim());
                            //        dataAccessLayer.Exec_Scaler("prc_UpdateUserSrvcRights", htbl);
                            //        htbl.Clear();

                            //        if (chkAccessRight.Checked == true)
                            //        {
                            //            //TODO : Need to debug & check the functionality
                            //            htbl.Add("@UserID", createdUserID);
                            //            htbl.Add("@SrvcGrpCode", SrvcGrpCode.Text.Trim());
                            //            htbl.Add("@SrvcReqTypeCode", SrvcReqTypeCode.Text.Trim());
                            //            htbl.Add("@Access", 1);
                            //            dataAccessLayer.Exec_Scaler("prc_InsertUserSrvcRights", htbl);
                            //            htbl = null;
                            //        }
                            //    }
                            //}
                            #endregion service rights

                            linkSave.Enabled = false;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please set default application');", true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { dataAccessLayer = null; }
        }

        protected void linkCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/SearchUser.aspx");
        }

        private void LoadUserDetails()
        {
            try
            {
                User objUser = new User();
                objUser.UserId = createdUserID;
                var adminBAL = new AdminBAL();
                objUser = adminBAL.LoadUserDetails(objUser);
                txtUserID.Text = objUser.UserId;
                txtUserName.Text = objUser.UserName;
                txtLogonName.Text = objUser.UserLoginName;
                cboStatus.SelectedValue = objUser.UserStatus.ToString();
                chkIsSysAdmin.Checked = objUser.IsSystemAdmin;
                chkIsUsrAdmin.Checked = objUser.IsUsrAdmin;
                chkIsForumModerator.Checked = objUser.IsDiscussAdmin;
                chkTimingRestrict.Checked = objUser.RestrictAccess;
                chkDownload.Checked = objUser.RestrictDownload;
                //cboNonLife.SelectedValue = objUser.NonLife.ToString();
                if (objUser.DeptCode.ToString().Trim() != "")
                {
                }
                //if (objUser.BranchCode.ToString().Trim() != "")
                //{
                //}
                UserGroupCode = objUser.UserRoleCode.ToString();
                txtUserID.Enabled = false;
                txtEmail.Text = objUser.UsrEmailId.ToString();

                if (objUser.UsrDob.ToString() != "")
                {
                    txtDob.Text = DateTime.Parse(objUser.UsrDob).ToString("dd MMM yyyy");
                }
                else
                {
                    txtDob.Text = "";
                }
                txtMobNumber.Text = objUser.UsrMobNumber.ToString();
                if (objUser.UsrEffectiveDT.ToString() != "")
                {
                    txtUsrEffectiveDT.Text = DateTime.Parse(objUser.UsrEffectiveDT).ToString("dd MMM yyyy");
                }
                else
                {
                    txtUsrEffectiveDT.Text = "";
                }
                if (objUser.UsrCeaseDT.ToString() != "")
                {
                    txtUsrCeaseDT.Text = DateTime.Parse(objUser.UsrCeaseDT).ToString("dd MMM yyyy");
                }
                else
                {
                    txtUsrCeaseDT.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void v1_click(object sender, EventArgs e)
        {
            try
            {
                linkClear.Visible = false;
                MultiView1.ActiveViewIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void v3_click(object sender, EventArgs e)
        {
            var ds = new DataSet();
            var htbl = new Hashtable();
            dataAccessLayer = new DataAccessLayer();

            try
            {
                if (!string.IsNullOrEmpty(appMode))
                {
                    //TODO : Need to confirm with sir. Tables used from CBFRMWRK database
                    //htbl.Add("@UserID", createdUser);
                    //string strUserLangNum = HttpContext.Current.Session["UserLangNum"].ToString();
                    //ds = dataAccessLayer.GetDataSet("Prc_getSrvcReqTypeOfUser", htbl);

                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    dgSRARights.DataSource = ((DataTable)ds.Tables[0]);
                    //    dgSRARights.DataBind();
                    //    MultiView1.ActiveViewIndex = 1;
                    //    linkClear.Visible = true;
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Please set services for User role code first');", true);
                    //}

                    if (!string.IsNullOrEmpty(appMode))
                    {
                        //string userid = Request.QueryString["userid"].ToString();
                        htbl.Clear();
                        htbl.Add("@ColumnName", "*");
                        htbl.Add("@UserID", createdUserID);
                        //TODO : needs to change use single value return function instead of dataset
                        DataSet dss = dataAccessLayer.GetDataSet("prc_GetColumnsFromUser", htbl);
                        DataTable dtt = dss.Tables[0];
                        Boolean isSystemAdmin = Boolean.Parse(dtt.Rows[0]["IsSystemAdmin"].ToString());

                        if (isSystemAdmin)
                        {
                            SqlDataSource3.SelectCommand = "prc_SelectUserGroup";
                            SqlDataSource3.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
                            SqlDataSource3.SelectParameters.Add(new Parameter { Name = "@CompType", DbType = DbType.String, DefaultValue = "" });
                            LoadDataSet(createdUserID, "", "", "true");
                        }
                        else
                        {
                            SqlDataSource3.SelectCommand = "prc_SelectUserGroup '99'";
                            SqlDataSource3.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
                            SqlDataSource3.SelectParameters.Add(new Parameter { Name = "@CompType", DbType = DbType.String, DefaultValue = "99" });
                            LoadDataSet(createdUserID, "", "99", "false");
                        }

                        MultiView1.ActiveViewIndex = 1;
                        linkClear.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + olng.GetItemDesc("Alert4") + "');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + olng.GetItemDesc("Alert4") + "');", true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                ds = null;
                dataAccessLayer = null;
                htbl = null;
            }
        }


        //TODO : Needs to un-comment below #region code only once( select all commented & click uncomment only once ) and modify / clean as above cleaned code
        #region Commented by kalpak due to moved by other project
        protected void v2_click(object sender, EventArgs e)
        {
            htbl = new Hashtable();
            dataAccessLayer = new DataAccessLayer();
            try
            {
                //preview the user sanctioning
                if (!string.IsNullOrEmpty(appMode))
                {
                    //string userid = Request.QueryString["userid"].ToString();
                    htbl.Clear();
                    htbl.Add("@ColumnName", "*");
                    htbl.Add("@UserID", createdUserID);
                    DataSet dss = dataAccessLayer.GetDataSet("prc_GetColumnsFromUser", htbl);

                    DataTable dtt = dss.Tables[0];
                    Boolean isSystemAdmin = Boolean.Parse(dtt.Rows[0]["IsSystemAdmin"].ToString());

                    if (isSystemAdmin)
                    {
                        SqlDataSource3.SelectCommand = "prc_SelectUserGroup";
                        SqlDataSource3.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
                        SqlDataSource3.SelectParameters.Add(new Parameter { Name = "@CompType", DbType = DbType.String, DefaultValue = "" });
                        LoadDataSet(createdUserID, "", "", "true");
                    }
                    else
                    {
                        SqlDataSource3.SelectCommand = "prc_SelectUserGroup";
                        SqlDataSource3.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
                        SqlDataSource3.SelectParameters.Add(new Parameter { Name = "@CompType", DbType = DbType.String, DefaultValue = "99" });
                        LoadDataSet(createdUserID, "", "", "");
                    }

                    MultiView1.ActiveViewIndex = 1;
                    linkClear.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + olng.GetItemDesc("Alert4") + "');", true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                dataAccessLayer = null;
                htbl = null;
            }
        }

        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    string moduleGroupList = lblUserGroupGrp.Text;
        //    try
        //    {
        //        htbl = new Hashtable();
        //        dataAccessLayer = new DataAccessLayer();

        //        switch (e.CommandName.ToLower())
        //        {
        //            //preview Module Access Matrix for User Group
        //            case ("template"):
        //                GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
        //                Label UserGroupCode = (Label)row.FindControl("UserGroupCode");
        //                Label UserGroupName = (Label)row.FindControl("UserGroupName");
        //                Label UserCarrierCode = (Label)row.FindControl("CarrierCode");
        //                lblUGID.Text = UserGroupCode.Text;
        //                lblUGName.Text = UserGroupName.Text;
        //                lblUGCC.Text = UserCarrierCode.Text;
        //                title.InnerText = "Module Access Matrix for User Group " + lblUGID.Text;
        //                LoadDataSet(UserGroupName.Text, "Template", lblUGCC.Text, "");

        //                //string height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height.ToString();
        //                string attributes = "z-index: 100; filter: alpha(opacity=40); left: 0px; width: 100%;display:block;";
        //                attributes += "position: absolute; top: 0px; height: 90%; background-color: #cccccc; moz-opacity: .40;  opacity: .40; font-size: 9pt;";
        //                attributes = "display:block;";
        //                attributes += " z-index: 101; left: 179pt; width: 350px;  top: 33pt; ";
        //                attributes += "height: 44px; font-size: 9pt; position: absolute;";
        //                Panel2.Visible = true;
        //                break;
        //            //insert User Group into Module Access Matrix
        //            case ("insert"):
        //                GridViewRow row1 = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
        //                Label UserGroupCode1 = (Label)row1.FindControl("UserGroupCode");
        //                Label UserGroupName1 = (Label)row1.FindControl("UserGroupNscame");
        //                Label UserCarrierCode1 = (Label)row1.FindControl("CarrierCode");
        //                lblUGID.Text = UserGroupCode1.Text;
        //                lblUGName.Text = UserGroupName1.Text;
        //                lblUGCC.Text = UserCarrierCode1.Text;

        //                //string userid = Request.QueryString["userid"].ToString();
        //                string CCode;
        //                htbl.Clear();
        //                htbl.Add("@UserGroupCode", UserGroupCode1.Text);
        //                htbl.Add("@AccessStatus", "1");
        //                DataSet ds1 = dataAccessLayer.GetDataSet("prc_GetUserGroupDetails", htbl);
        //                DataSet ds3;
        //                DataTable dt = ds1.Tables[0];
        //                for (int icount = 0; icount < dt.Rows.Count; icount++)
        //                {
        //                    htbl.Clear();
        //                    htbl.Add("@ModuleID", dt.Rows[icount]["moduleid"].ToString());
        //                    CCode = (string)dataAccessLayer.Exec_Scaler("prc_GetCompType", htbl);

        //                    htbl.Clear();
        //                    htbl.Add("@CompType", CCode);
        //                    htbl.Add("@ModuleID", Convert.ToInt32(dt.Rows[icount]["moduleid"]));
        //                    htbl.Add("@UserID", createdUserID);
        //                    ds3 = dataAccessLayer.GetDataSet("prc_getGroupDetails", htbl);
        //                    if (ds3.Tables[0].Rows.Count == 0)
        //                    {
        //                        htbl.Clear();
        //                        htbl.Add("@CompType", CCode);
        //                        htbl.Add("@UserId", createdUserID);
        //                        htbl.Add("@ModuleID",Convert.ToInt32(dt.Rows[icount]["moduleid"]));
        //                        htbl.Add("@UserGroupCode", dt.Rows[icount]["UserGroupCode"].ToString());
        //                        htbl.Add("@Server1", true);
        //                        htbl.Add("@Access", true);
        //                        htbl.Add("@CreatedUser", UserID);
        //                        dataAccessLayer.Exec_Ins_Command("prc_InsUserGroupModule", htbl);
        //                    }
        //                }
        //                LoadDataSet(createdUserID, "", "", "");
        //                break;

        //            //delete seleted User Group
        //            case ("remove"):
        //                string temModuleGrp = null;
        //                string[] moduleGroup = moduleGroupList.Split(';');
        //                //userid = Request.QueryString["userid"].ToString();

        //                GridViewRow row2 = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
        //                Label UserGroupCode2 = (Label)row2.FindControl("UserGroupCode");
        //                Label UserGroupName2 = (Label)row2.FindControl("UserGroupName");
        //                Label UserCarrierCode2 = (Label)row2.FindControl("CarrierCode");
        //                lblUGID.Text = UserGroupCode2.Text;
        //                lblUGName.Text = UserGroupName2.Text;
        //                lblUGCC.Text = UserCarrierCode2.Text;

        //                if (!string.IsNullOrEmpty(createdUserID))
        //                {
        //                    htbl.Clear();
        //                    htbl.Add("@CompType", UserCarrierCode2);
        //                    htbl.Add("@UserId",createdUserID);
        //                    htbl.Add("@UserGroupCode", UserGroupCode2);
        //                    dataAccessLayer.Exec_Ins_Command("prc_DelUserGroupModule", htbl);
        //                }

        //                //insert User Group into Module Access Matrix
        //                for (int i = 0; i < moduleGroup.Length; i++)
        //                {
        //                    if (null != moduleGroup[i] && !"".Equals(moduleGroup[i]) && moduleGroup[i] != UserGroupCode2.Text)
        //                    {
        //                        temModuleGrp += moduleGroup[i] + ";";
        //                        htbl.Clear();
        //                        htbl.Add("@UserGroupCode", moduleGroup[i]);
        //                        htbl.Add("@AccessStatus", "1");
        //                        ds1 = dataAccessLayer.GetDataSet("prc_GetUserGroupDetails", htbl);
        //                        dt = ds1.Tables[0];
        //                        for (int icount = 0; icount < dt.Rows.Count; icount++)
        //                        {
        //                            htbl.Clear();
        //                            htbl.Add("@ModuleID", dt.Rows[icount]["moduleid"].ToString());
        //                            CCode = (string)dataAccessLayer.Exec_Scaler("prc_GetCompType", htbl);

        //                            htbl.Clear();
        //                            htbl.Add("@CompType", CCode);
        //                            htbl.Add("@ModuleID", Convert.ToInt32(dt.Rows[icount]["moduleid"]));
        //                            htbl.Add("@UserID", createdUserID);
        //                            ds3 = dataAccessLayer.GetDataSet("prc_getGroupDetails", htbl);

        //                            if (ds3.Tables[0].Rows.Count == 0)
        //                            {
        //                                htbl.Clear();
        //                                htbl.Add("@CompType", CCode);
        //                                htbl.Add("@UserId", createdUserID);
        //                                htbl.Add("@ModuleID", Convert.ToInt32(dt.Rows[icount]["moduleid"]));
        //                                htbl.Add("@UserGroupCode", dt.Rows[icount]["UserGroupCode"].ToString());
        //                                htbl.Add("@Server1", true);
        //                                htbl.Add("@Access", true);
        //                                htbl.Add("@CreatedUser", UserID);
        //                                dataAccessLayer.Exec_Ins_Command("prc_InsUserGroupModule", htbl);
        //                            }
        //                        }
        //                    }
        //                }
        //                LoadDataSet(createdUserID, "", "", "");
        //                lblUserGroupGrp.Text = temModuleGrp;
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    finally
        //    {

        //    }
        //}

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                lblUGID.Text = "";
                Panel2.Visible = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(createdUserID))
            {
                for (int icount = 0; icount < TrVModule.Nodes.Count; icount++)
                {
                    DisplayChildNodeText(TrVModule.Nodes[icount], createdUserID);
                }
            }
        }

        protected void btnResetpw_Click(object sender, EventArgs e)
        {
            UserSetupDAL.UpdatePwd(txtUserID.Text.ToUpper(), txtUserID.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + olng.GetItemDesc("Alert5") + "');", true);
        }

        void DisplayChildNodeText(TreeNode node, string userid)
        {

            int chkstatus;
            DataSet ds1;
            DataSet ds3;
            DataSet ds4;
            htbl = new Hashtable();
            dataAccessLayer = new DataAccessLayer();
            try
            {
                if (node.Checked == true)
                { chkstatus = 1; }
                else { chkstatus = 0; }

                htbl.Add("@UserId", userid);
                ds3 = dataAccessLayer.GetDataSet("Prc_CBFrm_BindAppMapGridView", htbl);

                if (ds3.Tables[0].Rows.Count > 0)
                {
                    htbl.Clear();
                    htbl.Add("@UserGroupCode", ds3.Tables[0].Rows[0]["UserGroupCode"].ToString());
                    ds4 = dataAccessLayer.GetDataSet("prc_SelKGrp", htbl);

                    if (ds4.Tables[0].Rows.Count > 0)
                    {
                        strSQLGrpName = ds4.Tables[0].Rows[0]["UserGroupName01"].ToString();
                    }
                }

                htbl.Clear();
                htbl.Add("@ModuleID", Convert.ToInt32(node.Value));
                htbl.Add("@UserID", userid);
                ds1 = dataAccessLayer.GetDataSet("prc_getGroupDetails", htbl);
                if (ds1.Tables.Count > 0)
                {

                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        if (chkstatus == 1)
                        {
                            htbl.Clear();
                            htbl.Add("@ModuleID", Convert.ToInt32(node.Value));
                            string CCode = (string)dataAccessLayer.ExecuteScalar("prc_GetCompType", htbl);

                            //insert checked status
                            if (ds1.Tables[0].Rows.Count == 0)
                            {
                                htbl.Clear();
                                htbl.Add("@CompType", CCode);
                                htbl.Add("@UserId", userid);
                                htbl.Add("@ModuleID", Convert.ToInt32(node.Value));
                                htbl.Add("@UserGroupCode", ds3.Tables[0].Rows[0]["UserGroupCode"].ToString());
                                htbl.Add("@Server1", true);
                                htbl.Add("@Access", true);
                                htbl.Add("@CreatedUser", UserID);
                                dataAccessLayer.ExecuteNonQuery("prc_InsUserGroupModule", htbl);
                            }
                        }
                    }
                    else
                    {
                        if (chkstatus == 0)
                        {
                            //delete unchecked status
                            htbl.Clear();
                            htbl.Add("@ModuleID", Convert.ToInt32(node.Value));
                            htbl.Add("@UserID", userid);
                            dataAccessLayer.ExecuteNonQuery("prc_DelUserGroupModuleForModule", htbl);
                        }
                    }
                }

                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    DisplayChildNodeText(node.ChildNodes[i], userid);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                ds1 = null;
                ds3 = null;
                ds4 = null;
                dataAccessLayer = null;
            }
        }

        //protected void btnCheckID_Click(object sender, EventArgs e)
        //{
            //DataSet dsResult = new DataSet();
            //dataAccessLayer = new DataAccessLayer();
            //try
            //{
            //    if (rdInternal.Checked)
            //    {
            //        Hashtable htable = new Hashtable();
            //        htable.Clear();
            //        htable.Add("@AgentId", txtUserID.Text.ToString());
            //        //TODO : Comment this code. 
            //        //Need to discuss with adarsh sir - tables used in procedure are not available 

            //        dsResult = dataAccessLayer.GetDataSet("Prc_GetEmpDtls", htable);

            //        if (dsResult.Tables[0].Rows.Count > 0)
            //        {
            //            txtUserName.Text = dsResult.Tables[0].Rows[0]["LegalName"].ToString();
            //            lblSuccess.Visible = true;
            //            lblSuccess.Text = "valid ID";
            //            lblerror.Visible = false;
            //            txtPwd.Enabled = true;
            //            txtConfirmPwd.Enabled = true;
            //            txtLogonName.Enabled = true;
            //            txtUserName.Enabled = false;
            //            txtUserID.Enabled = false;
            //            btnCheckID.Enabled = false;
            //            cboLanguage.Enabled = false;
            //            txtEmail.Enabled = false;
            //            txtDob.Enabled = false;
            //            txtMobNumber.Enabled = false;
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Employee Does Not Exist Please Enter Valid EmployeeID');", true);
            //            txtUserID.Text = "";
            //            txtUserID.Focus();
            //        }
            //    }
            //    else
            //    {
            //        var dSet = new DataSet();
            //        var htbl = new Hashtable();

            //        htbl.Add("@ColumnName", "*");
            //        htbl.Add("@UserID", txtUserID.Text);
            //        dSet = dataAccessLayer.GetDataSet("prc_GetColumnsFromUser", htbl);
            //        htbl = null;

            //        if (dSet.Tables[0].Rows.Count != 0)
            //        {
            //            lblerror.Text = olng.GetItemDesc("lblerror.Text");
            //            lblerror.Visible = true;
            //            lblSuccess.Visible = false;
            //        }
            //        else
            //        {
            //            lblSuccess.Visible = true;
            //            lblSuccess.Text = "valid ID";
            //            lblerror.Visible = false;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
            //finally
            //{
            //    dsResult = null;
            //    dataAccessLayer = null;
            //}
        //}

        private void FillAuthorityCode()
        {
            DataSet dsResult = new DataSet();
            try
            {
                dsResult = dataAccessLayer.GetDataSet("prc_GetUnitCodes");
                if (dsResult.Tables.Count > 0)
                {
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { dsResult = null; }
        }

        protected void dgSRARights_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkAccessRight = (CheckBox)e.Row.FindControl("chkAccessRight");
                Label ACC = (Label)e.Row.FindControl("ACC");
                Label AcCPTL = (Label)e.Row.FindControl("AcCPTL");

                if (ACC.Text == "True")
                {
                    chkAccessRight.Checked = true;
                }
            }
        }

        protected void rdInternal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (rdInternal.Checked == true)
                {
                    //lblUserID.Text = olng.GetItemDesc("lblEmployee.Text");
                    //EnabledField();
                    //txtUserName.Text = "";
                    //txtUserID.Text = "";
                    //txtLogonName.Text = "";
                    //lblerror.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('No data available you to validate, please select External.');", true);
                }
                else if (rdExternal.Checked == true)
                {
                    lblUserID.Text = olng.GetItemDesc("lblUserID.Text");
                    EnabledField();
                    txtUserName.Text = "";
                    txtUserID.Text = "";
                    lblSuccess.Text = "";
                    txtLogonName.Text = "";
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void DisabledField()
        {
            try
            {
                txtUserName.Enabled = false;
                txtUserID.Enabled = false;
                //btnCheckID.Enabled = false;
                txtPwd.Enabled = false;
                txtConfirmPwd.Enabled = false;
                cboStatus.Enabled = false;
                txtLogonName.Enabled = false;
                cboLanguage.Enabled = false;
                txtUsrEffectiveDT.Enabled = false;
                txtUsrCeaseDT.Enabled = false;
                txtDob.Enabled = false;
                txtMobNumber.Enabled = false;
                GridViewApp.Enabled = false;
                chkIsSysAdmin.Enabled = false;
                chkIsUsrAdmin.Enabled = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EnabledField()
        {
            try
            {
                txtUserName.Enabled = true;
                txtUserID.Enabled = true;
                //btnCheckID.Enabled = true;
                txtPwd.Enabled = true;
                txtConfirmPwd.Enabled = true;
                cboStatus.Enabled = true;
                txtLogonName.Enabled = true;
                cboLanguage.Enabled = true;
                txtUsrEffectiveDT.Enabled = true;
                txtUsrCeaseDT.Enabled = true;
                txtDob.Enabled = true;
                txtMobNumber.Enabled = true;
                GridViewApp.Enabled = true;
                chkIsSysAdmin.Enabled = true;
                chkIsUsrAdmin.Enabled = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        protected void btnHdnDDL_Click(object sender, EventArgs e)
        {
            try
            {
                string strXML = dataAccessLayer.GetOutputXML("Prc_Fillloccodexml '" + hdnDDLLocTypeID.Value.ToString() + "'", "DefaultConn");
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "javascript:FillDropDown('ddlLoccode0','" + strXML + "','1','0');", true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk;
                DropDownList ddl1, ddl2, ddl3, ddl4;
                DropDownList ddlAppStatus;
                TextBox txtAppEffDt, txtInEffDt;
                RadioButton rbtDefaultApp;
                CheckBox ChkTL;

                foreach (GridViewRow rowItem in GridViewApp.Rows)
                {
                    chk = (CheckBox)(rowItem.Cells[0].FindControl("chkApp"));
                    ddl1 = (DropDownList)(rowItem.Cells[1].FindControl("ddlLocation"));
                    ddl2 = (DropDownList)(rowItem.Cells[2].FindControl("ddlLoactionCode"));
                    ddl3 = (DropDownList)(rowItem.Cells[3].FindControl("ddlDepartment"));
                    ddl4 = (DropDownList)(rowItem.Cells[4].FindControl("ddlUserroleCode"));
                    txtAppEffDt = (TextBox)(rowItem.Cells[5].FindControl("txtAppEffectDT"));
                    txtInEffDt = (TextBox)(rowItem.Cells[6].FindControl("txtAppCeaseDT"));
                    ddlAppStatus = (DropDownList)(rowItem.Cells[7].FindControl("ddlAppEnblStatus"));
                    rbtDefaultApp = (RadioButton)(rowItem.Cells[8].FindControl("rdbDefaultApp"));
                    ChkTL = (CheckBox)(rowItem.Cells[9].FindControl("chkTeamLead"));

                    if (chk.Checked == true)
                    {
                        ddl1.Enabled = true;
                        ddl2.Enabled = true;
                        ddl3.Enabled = true;
                        ddl4.Enabled = true;
                        txtAppEffDt.Enabled = true;
                        txtInEffDt.Enabled = true;
                        ddlAppStatus.Enabled = true;
                        rbtDefaultApp.Enabled = true;
                        ChkTL.Enabled = true;
                    }
                    else
                    {
                        ddl1.Enabled = false;
                        ddl2.Enabled = false;
                        ddl3.Enabled = false;
                        ddl4.Enabled = false;
                        ddl1.SelectedIndex = 0;
                        ddl2.Items.Clear();
                        ddl2.Items.Insert(0, "Select");
                        ddl3.Items.Clear();
                        ddl3.Items.Insert(0, "Select");
                        ddl4.SelectedIndex = 0;
                        txtAppEffDt.Enabled = false;
                        txtAppEffDt.Text = "";
                        txtInEffDt.Enabled = false;
                        txtInEffDt.Text = "";
                        ddlAppStatus.Enabled = false;
                        ddlAppStatus.SelectedIndex = 0;
                        rbtDefaultApp.Enabled = false;
                        ChkTL.Enabled = false;
                        ChkTL.Checked = false;
                    }

                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void GridViewApp_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            dataAccessLayer = new DataAccessLayer();
            DataSet ObjDs = new DataSet();
            htbl = new Hashtable();
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ctr = (DropDownList)e.Row.FindControl("ddlLocation");
                    DropDownList ctr1 = (DropDownList)e.Row.FindControl("ddlUserroleCode");
                    DropDownList ddlcloccode = (DropDownList)e.Row.FindControl("ddlLoactionCode");
                    DropDownList ddldeptcode = (DropDownList)e.Row.FindControl("ddlDepartment");
                    CheckBox chk = (CheckBox)e.Row.FindControl("chkApp");
                    HiddenField hdnIsChecked = (HiddenField)e.Row.FindControl("hdnIsChecked");
                    HiddenField hdnAppID = (HiddenField)e.Row.FindControl("hdnAppId");
                    RadioButton RdbDefaultApp = (RadioButton)e.Row.FindControl("rdbDefaultApp");
                    TextBox txtAppEffectDT = (TextBox)e.Row.FindControl("txtAppEffectDT");
                    TextBox txtAppCeaseDT = (TextBox)e.Row.FindControl("txtAppCeaseDT");
                    //TextBox txtUserEffectDT = (TextBox)e.Row.FindControl("txtUserEffectDT");
                    //TextBox txtUserCeaseDT = (TextBox)e.Row.FindControl("txtUserCeaseDT");
                    DropDownList ddlAppEnblStatus = (DropDownList)e.Row.FindControl("ddlAppEnblStatus");
                    RadioButton rbtnDefaultApp = (RadioButton)e.Row.FindControl("rdbDefaultApp");
                    CheckBox chkTLead = (CheckBox)e.Row.FindControl("chkTeamLead");//added by darshana 8 july 2013

                    txtAppEffectDT.Attributes.Add("onfocus", "javascript:showCalendarControl(" + txtAppEffectDT.ClientID + ")");
                    txtAppEffectDT.Attributes.Add("onchange", "javascript:setDateFormat('" + txtAppEffectDT.ClientID + "');calendarControl.hide();");
                    txtAppEffectDT.Attributes.Add("onkeypress", "javascript:funInputNumericCharOnly();");
                    txtAppCeaseDT.Attributes.Add("onfocus", "javascript:showCalendarControl(" + txtAppCeaseDT.ClientID + ")");
                    txtAppCeaseDT.Attributes.Add("onchange", "javascript:setDateFormat('" + txtAppCeaseDT.ClientID + "');calendarControl.hide();");
                    txtAppCeaseDT.Attributes.Add("onkeypress", "javascript:funInputNumericCharOnly();");
                    rbtnDefaultApp.Attributes.Add("onclick", "javascript:SelectSingleRadiobutton(this.id);");

                   
                   // dataAccessLayer = new DataAccessLayer();
                    htbl.Clear();
                    htbl.Add("@UserId", createdUserID);
                    ObjDs.Clear();
                    ObjDs = dataAccessLayer.GetDataSet("Prc_CBFrm_BindAppMapGridView", htbl);

                    if (ObjDs.Tables.Count > 0)
                    {
                        if (ObjDs.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ObjDs.Tables[0].Rows.Count; i++)
                            {
                                if (hdnAppID.Value.ToString() == ObjDs.Tables[0].Rows[i]["AppId"].ToString())
                                {
                                    ctr.SelectedValue = ObjDs.Tables[0].Rows[i]["LocType"].ToString();
                                    ctr1.SelectedValue = ObjDs.Tables[0].Rows[i]["UserGroupCode"].ToString();
                                    hdnUsrRoleCode.Value = ctr1.SelectedValue;
                                    ddlcloccode.SelectedValue = ObjDs.Tables[0].Rows[i]["LocCode"].ToString();
                                    ddldeptcode.SelectedValue = ObjDs.Tables[0].Rows[i]["DeptCode"].ToString();
                                    if (ObjDs.Tables[0].Rows[i]["isTeamLead"].ToString() == "True")
                                    {
                                        chkTLead.Checked = true;
                                    }
                                    else
                                    {
                                        chkTLead.Checked = false;
                                    }
                                    if (ObjDs.Tables[0].Rows[i]["AppStatus"].ToString().Trim() == "Y")
                                    {
                                        chk.Checked = true;
                                    }
                                    else
                                    {

                                        chk.Checked = false;
                                    }
                                    if (ObjDs.Tables[0].Rows[i]["DefaultApp"].ToString().Trim() == "True")
                                    {
                                        RdbDefaultApp.Checked = true;
                                    }

                                    if (ObjDs.Tables[0].Rows[i]["AppEffectiveDTim"].ToString().Trim() != null)
                                    {
                                        //txtAppEffectDT.Text = DateTime.Parse(ObjDs.Tables[0].Rows[i]["AppEffectiveDTim"].ToString().Trim()).ToString("dd MMM yyyy");//changed by Darshana 22 Aug 2013
                                        txtAppEffectDT.Text = ObjDs.Tables[0].Rows[i]["AppEffectiveDTim"].ToString();
                                    }
                                    else
                                    {
                                        txtAppEffectDT.Text = "";
                                    }
                                    if (ObjDs.Tables[0].Rows[i]["AppCeaseDTim"].ToString().Trim() != null)
                                    {
                                        txtAppCeaseDT.Text = ObjDs.Tables[0].Rows[i]["AppCeaseDTim"].ToString();
                                    }
                                    else
                                    {
                                        txtAppCeaseDT.Text = "";
                                    }
                                    if (ObjDs.Tables[0].Rows[i]["isEnabled"].ToString().Trim() != null)
                                    {
                                        ddlAppEnblStatus.SelectedValue = ObjDs.Tables[0].Rows[i]["isEnabled"].ToString().Trim();
                                    }

                                }

                            }
                        }
                    }

                    if (ctr.ID == "ddlLocation")
                    {
                        DataSet ds = new DataSet();
                        ds.Clear();

                        ds = dataAccessLayer.GetDataSet("prc_FillddlLocType");

                        ctr.DataSource = ds;
                        ctr.Items.Clear();
                        ctr.DataTextField = "UnitDesc01";
                        ctr.DataValueField = "UnitType";
                        ctr.DataBind();
                        ctr.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    if (ctr1.ID == "ddlUserroleCode")
                    {
                        DataSet ds = new DataSet();
                        Hashtable ht = new Hashtable();
                        ds.Clear();
                        ht.Clear();
                        ht.Add("@App", hdnAppID.Value);
                        ds = dataAccessLayer.GetDataSet("Prc_GetUserRoleCode", ht);
                        ctr1.DataSource = ds;
                        ctr1.DataTextField = "UserGroupName";
                        ctr1.DataValueField = "UserGroupCode";
                        ctr1.DataBind();
                        ctr1.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    //TODO : Need to discuss with adarsh sir - Not working, no tables for unt
                    FillDropDownLocCode(ddlcloccode, ctr);
                    FillDropDownDeptCode(ddldeptcode, ddlcloccode, chk.Text);

                }

            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        protected void BindGridview()
        {
            var ds = new DataSet();
            dataAccessLayer = new DataAccessLayer();
            try
            {
                ds = dataAccessLayer.GetDataSet("prc_GetApplicationList");
                GridViewApp.DataSource = ds;
                GridViewApp.DataBind();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                ds = null;
                dataAccessLayer = null;
            }
        }

        protected void ddlLocation_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                DropDownList ctr = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ctr.NamingContainer;

                DropDownList ddl2 = (DropDownList)row.FindControl("ddlLocation");
                DropDownList ddl1 = (DropDownList)row.FindControl("ddlLoactionCode");

                FillDropDownLocCode(ddl1, ddl2);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void FillDropDownLocCode(DropDownList ddl1, DropDownList ddl2)
        {
            DataSet ds = new DataSet();
            htbl = new Hashtable();
            dataAccessLayer = new DataAccessLayer();
            try
            {
                htbl.Clear();
                htbl.Add("@unittype", ddl2.SelectedValue);
                ds = dataAccessLayer.GetDataSet("prc_FillddlLocCode", htbl);
                ddl1.DataSource = ds;
                ddl1.Items.Clear();
                ddl1.DataTextField = "UnitLegalName";
                ddl1.DataValueField = "UnitID";
                ddl1.DataBind();
                ddl1.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                ds = null;
                htbl.Clear();
                dataAccessLayer = null;
            }
        }

        public void FillDropDownDeptCode(DropDownList ddl1, DropDownList ddl2, string appNamev)
        {

            DataSet ds = new DataSet();
            htbl = new Hashtable();
            dataAccessLayer = new DataAccessLayer();
            try
            {
                htbl.Clear();
                htbl.Add("@UnitId", ddl2.SelectedValue);
                htbl.Add("@AppNm", appNamev);
                ds = dataAccessLayer.GetDataSet("Prc_UsrMgmtProFillDropDownDeptCode", htbl);
                ddl1.DataSource = ds;
                ddl1.Items.Clear();
                ddl1.DataTextField = "DeptName01";
                ddl1.DataValueField = "DeptCode";
                ddl1.DataBind();
                ddl1.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                throw;
            }
            finally {
                ds = null;
                htbl.Clear();
                dataAccessLayer = null;
            }


        }

        protected void ddlLoactionCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                DropDownList ctr = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ctr.NamingContainer;
                DropDownList ddlLocType = (DropDownList)row.FindControl("ddlLocation");
                DropDownList ddl2 = (DropDownList)row.FindControl("ddlLoactionCode");
                DropDownList ddl1 = (DropDownList)row.FindControl("ddlDepartment");
                DropDownList ddl3 = (DropDownList)row.FindControl("ddlUserroleCode");
                CheckBox appchk = (CheckBox)row.FindControl("chkApp");
                string appNamev = appchk.Text;
                string loctype = ddlLocType.SelectedValue.ToString();
                FillDropDownDeptCode(ddl1, ddl2, appNamev);
                if (loctype == "BO")
                {
                    FillUserRole(ddlLocType, ddl2, ddl1, ddl3, appNamev, 1);//1 for loding all bo related user role code in drp down
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        protected void ddlUserroleCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hdnUsrRoleCode.Value = "";
                DropDownList ctr = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ctr.NamingContainer;

                DropDownList ddl4 = (DropDownList)row.FindControl("ddlUserroleCode");

                hdnUsrRoleCode.Value = ddl4.SelectedValue;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void insertgridview(string createdUserID)
        {
            dataAccessLayer = new DataAccessLayer();
            try
            {
                CheckBox chk, chckTeamLead;
                RadioButton RdbDefaultApp;
                HiddenField hdnAppStatus;
                DropDownList ddl1, ddl2, ddl3, ddl4, AppStat;
                TextBox AppEffDt, AppCeaseDt;

                foreach (GridViewRow rowItem in GridViewApp.Rows)
                {
                    RdbDefaultApp = (RadioButton)(rowItem.Cells[0].FindControl("rdbDefaultApp"));
                    chk = (CheckBox)(rowItem.Cells[0].FindControl("chkApp"));
                    hdnAppStatus = (HiddenField)(rowItem.Cells[0].FindControl("hdnIsChecked"));
                    ddl1 = (DropDownList)(rowItem.Cells[1].FindControl("ddlLocation"));
                    ddl2 = (DropDownList)(rowItem.Cells[2].FindControl("ddlLoactionCode"));
                    ddl3 = (DropDownList)(rowItem.Cells[3].FindControl("ddlDepartment"));
                    ddl4 = (DropDownList)(rowItem.Cells[4].FindControl("ddlUserroleCode"));
                    AppEffDt = (TextBox)(rowItem.Cells[5].FindControl("txtAppEffectDT"));
                    AppCeaseDt = (TextBox)(rowItem.Cells[6].FindControl("txtAppCeaseDT"));
                    AppStat = (DropDownList)(rowItem.Cells[7].FindControl("ddlAppEnblStatus"));
                    chckTeamLead = (CheckBox)(rowItem.Cells[8].FindControl("chkTeamLead"));
                    if (chk.Checked == true)
                    {
                        string AppId = string.Empty;
                        if (chk.Text.ToUpper() == "LIFE")
                            AppId = "1";
                        else if (chk.Text.ToUpper() == "GENERAL")
                            AppId = "2";
                        //string newuserid = UserID;
                        string d = ddl1.SelectedValue.ToString();
                        string dd = ddl2.SelectedValue.ToString();
                        string ddd = ddl3.SelectedValue.ToString();
                        string dddd = ddl4.SelectedValue.ToString();
                        string AppStatus = "Y";
                        string AEffDate = AppEffDt.Text.ToString();
                        string ACeasDate = AppCeaseDt.Text.ToString();
                        string ASt = AppStat.SelectedValue.ToString();
                        string chkTLead = chckTeamLead.Checked.ToString();

                        Hashtable htable = new Hashtable();
                        htable.Clear();
                        htable.Add("@UserId", createdUserID);
                        htable.Add("@appId", AppId);
                        htable.Add("@LocType", d);
                        htable.Add("@LocCode", dd);
                        htable.Add("@Deptcode", ddd);
                        htable.Add("@UserRoleCode", dddd);
                        htable.Add("@AppSatus", AppStatus);
                        htable.Add("@DefaultApp", RdbDefaultApp.Checked);
                        htable.Add("@AEffDate", AEffDate);
                        htable.Add("@ACeasDate", ACeasDate);
                        htable.Add("@UEffDate", "");
                        htable.Add("@UCeasDate", "");
                        htable.Add("@ASt", ASt);
                        htable.Add("@IsTeamLead", chkTLead);
                        htable.Add("@CreatedBy", UserID);
                        dataAccessLayer.ExecuteNonQuery("prc_InsUsrAppRoleAcs", htable);
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList grdDynGrid = (DropDownList)sender;
                GridViewRow rw = (GridViewRow)grdDynGrid.NamingContainer;

                DropDownList ddlLocType = (DropDownList)rw.FindControl("ddlLocation");
                DropDownList ddlLocCode = (DropDownList)rw.FindControl("ddlLoactionCode");
                DropDownList ddlDeptCode = (DropDownList)rw.FindControl("ddlDepartment");
                DropDownList ddlUsrRoleCode = (DropDownList)rw.FindControl("ddlUserroleCode");
                CheckBox chckApp = (CheckBox)rw.FindControl("chkApp");
                string AppName = chckApp.Text;
                FillUserRole(ddlLocType, ddlLocCode, ddlDeptCode, ddlUsrRoleCode, AppName, 2);// for loading specific user role code depending on selected values of other drop downs

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private DataSet FillUserRole(DropDownList cboLocType, DropDownList cboLocCode, DropDownList cboHODepartment, DropDownList ddlUsrRoleCode, string AppName, int i)
        {
            DataSet dsResult = new DataSet();
            Hashtable htTable = new Hashtable();
            dataAccessLayer = new DataAccessLayer();
            try
            {
                dsResult = null;
                htTable.Clear();
                htTable.Add("@LocType", cboLocType.SelectedValue);
                htTable.Add("@LocCode", cboLocCode.SelectedValue);
                htTable.Add("@DeptCOde", cboHODepartment.SelectedValue);
                htTable.Add("@AppName", AppName);
                htTable.Add("@Flag", i);
                dsResult = dataAccessLayer.GetDataSet("Prc_GetUserRoleOnLocTypeCodeDept", htTable);
                if (dsResult.Tables.Count > 0)
                {
                    ddlUsrRoleCode.Items.Clear();
                    ddlUsrRoleCode.DataSource = dsResult.Tables[0];
                    ddlUsrRoleCode.DataTextField = "UserGroupName";
                    ddlUsrRoleCode.DataValueField = "UserGroupCode";
                    ddlUsrRoleCode.Items.Insert(0, new ListItem("Select", "0"));
                    ddlUsrRoleCode.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                htTable = null;
                dataAccessLayer = null;
            }
            return dsResult;
        }
        #endregion

    }
}