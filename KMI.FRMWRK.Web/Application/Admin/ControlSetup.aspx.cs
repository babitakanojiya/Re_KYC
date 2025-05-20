using KMI.FRMWRK.DAL;
using KMI.FRMWRK.Web.Application.CKYC.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMI.FRMWRK.Web.Application.Admin
{
    public partial class ControlSetup : System.Web.UI.Page
    {
        CommonUtility cu = new CommonUtility();
        ErrorLog objErr = new ErrorLog();
        int AppId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    cu.GetCKYC(ddlSegment, "Segment");
                    ddlSegment.Items.Insert(0, new ListItem("Select", ""));

                    cu.GetCKYC(ddlControlType, "ControlType");
                    ddlControlType.Items.Insert(0, new ListItem("Select", ""));

                    cu.GetCKYC(ddlIsMandatory, "IsMandatory");
                    ddlIsMandatory.Items.Insert(0, new ListItem("Select", ""));

                    cu.GetCKYC(ddlIsMaster, "IsMaster");
                    ddlIsMaster.Items.Insert(0, new ListItem("Select", ""));

                    cu.GetCKYC(ddlIsVisible, "IsVisible");
                    ddlIsVisible.Items.Insert(0, new ListItem("Select", ""));

                    cu.GetCKYC(ddlActive, "IsActive");
                    ddlActive.Items.Insert(0, new ListItem("Select", ""));

                    cu.GetCKYC(ddlConstitution, "ConstType");
                    ddlConstitution.Items.Insert(0, new ListItem("Select", ""));

                    if (Convert.ToString(Request.QueryString["Mode"]) == "E")
                    {
                        DivUpdate.Visible = true;
                        DivSave.Visible = false;
                        BindData(Convert.ToString(Request.QueryString["ID"]));
                    }
                    else
                    {
                        DivUpdate.Visible = false;
                        DivSave.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable ht = GetParameters("A", "");
                DataAccessLayer l = new DataAccessLayer();
                l.ExecuteNonQuery("Prc_InsControlsSetup", ht, "CKYCConnectionString");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Insert Success", "AlertMsg('Control Setup Done Successfully')", true);
                //Prc_InsControlsSetup
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        public void BindData(string ID)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("@RecID", ID);
                DataAccessLayer l = new DataAccessLayer();
                DataTable dt =  l.GetDataTable("Prc_GetControlsSetup", ht, "CKYCConnectionString");
                if(dt.Rows.Count > 0)
                {
                    //RecId SegmentName ControlName ControlId   ControlType DataType    DataSize ConstitutionType    IsMandate IsMaster    
                    //    DatabaseTable ColumnName  ColumnValue IsVisible   ContainerDiv Order   IsActive CreatedBy   CreatedDtim UpdatedBy   UpdatedDtim
                    ddlSegment.SelectedValue = Convert.ToString(dt.Rows[0]["SegmentName"]);
                    txtControlName.Text = Convert.ToString(dt.Rows[0]["ControlName"]);
                    txtControlID.Text = Convert.ToString(dt.Rows[0]["ControlId"]);
                    ddlControlType.Text = Convert.ToString(dt.Rows[0]["ControlType"]);
                    txtDataSize.Text = Convert.ToString(dt.Rows[0]["DataSize"]);
                    ddlConstitution.SelectedValue = Convert.ToString(dt.Rows[0]["ConstitutionType"]);
                    ddlIsMandatory.SelectedValue = Convert.ToString(dt.Rows[0]["IsMandate"]);
                    ddlIsMaster.SelectedValue = Convert.ToString(dt.Rows[0]["IsMaster"]);
                    txtTableName.Text = Convert.ToString(dt.Rows[0]["DatabaseTable"]);
                    txtColumnName.Text = Convert.ToString(dt.Rows[0]["ColumnName"]);
                    txtColumnValue.Text = Convert.ToString(dt.Rows[0]["ColumnValue"]);
                    ddlIsVisible.SelectedValue = Convert.ToString(dt.Rows[0]["IsVisible"]);
                    txtContainer.Text = Convert.ToString(dt.Rows[0]["ContainerDiv"]);
                    txtOrder.Text = Convert.ToString(dt.Rows[0]["Order"]);
                    ddlActive.SelectedValue = Convert.ToString(dt.Rows[0]["IsActive"]);
                }
                
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ddlSegment.SelectedIndex = 0;
                txtControlName.Text = "";
                txtControlID.Text = "";
                ddlControlType.Text = "";
                txtDataSize.Text = "";
                ddlConstitution.SelectedIndex = 0;
                ddlIsMandatory.SelectedIndex = 0;
                ddlIsMaster.SelectedIndex = 0;
                txtTableName.Text = "";
                txtColumnName.Text = "";
                txtColumnValue.Text = "";
                ddlIsVisible.SelectedIndex = 0;
                txtContainer.Text = "";
                txtOrder.Text = "";
                ddlActive.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable ht = GetParameters("E", Convert.ToString(Request.QueryString["ID"]));
                DataAccessLayer l = new DataAccessLayer();
                l.ExecuteNonQuery("Prc_UpdControlsSetup", ht, "CKYCConnectionString");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Insert Success", "AlertMsg('Control Setup Updated Successfully')", true);
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.RawUrl, true);
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        public Hashtable GetParameters(string mode, string id)
        {
            Hashtable ht = new Hashtable();
            if (mode == "E")
                ht.Add("@ReciD", id);

            ht.Add("@segment", ddlSegment.SelectedValue);
            ht.Add("@ControlName", txtControlName.Text.Trim());
            ht.Add("@ControlId", txtControlID.Text.Trim());
            ht.Add("@ControlType", ddlControlType.Text.Trim());
            ht.Add("@DataType", "varchar");
            ht.Add("@DataSize", txtDataSize.Text.Trim());
            ht.Add("@ConstitutionType", ddlConstitution.SelectedValue);
            ht.Add("@IsMandate", ddlIsMandatory.SelectedValue);
            ht.Add("@IsMaster", ddlIsMaster.SelectedValue);
            ht.Add("@DatabaseTable", txtTableName.Text.Trim());
            ht.Add("@ColumnName", txtControlName.Text.Trim());
            ht.Add("@ColumnValue", txtColumnValue.Text.Trim());
            ht.Add("@IsVisible", ddlIsVisible.SelectedValue);
            ht.Add("@ContainerDiv", txtContainer.Text.Trim());
            ht.Add("@Order", txtOrder.Text.Trim());
            ht.Add("@IsActive", ddlActive.SelectedValue);
            ht.Add("@UserID", Convert.ToString(Session["UserID"]));
            return ht;
        }
    }
}