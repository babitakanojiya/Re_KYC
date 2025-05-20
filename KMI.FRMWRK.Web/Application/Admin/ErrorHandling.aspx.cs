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
using System.Data.SqlClient;

namespace KMI.FRMWRK.Web.Application.Admin
{
    public partial class ErrorHandling : System.Web.UI.Page
    {

        CommonUtility cu = new CommonUtility();
        ErrorLog objErr = new ErrorLog();
        int AppId = 0;
        DataTable sdr;
        //DataAccessLayer objDAL = new DataAccessLayer();
        DataTable dt = new DataTable();
        Hashtable htParam = new Hashtable();
        string INTG_ID = string.Empty;
        DataSet DS = new DataSet();
        DataAccessLayer objDAL = new DataAccessLayer("INTGRTNConnectionString");
   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetSrctbl(ddlSourcetbl);
                GetOperator(ddlOperator);
                FillddlStatus(ddlStatus);
                ddlTblcolmnl.Items.Insert(0, new ListItem("-- SELECT --", ""));
                ddlStatus.SelectedIndex = 1;

                txtEfDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                //ErrHandlGridBind();

                if (Request.QueryString["INTG_ID"].ToString().Trim() != "")
                {
                    INTG_ID = Request.QueryString["INTG_ID"].ToString().Trim();
                }
                btnupd.Visible = false;
            }
            ErrHandlGridBind();
        }

        protected void GetSrctbl(DropDownList ddl)
        {
            try
            {
                ddlSourcetbl.Items.Clear();
                DataTable dt;
                Hashtable htparam = new Hashtable();
                htparam.Clear();
                htparam.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                dt = objDAL.GetDataTable("Prc_getSoursetbl", htparam);
                if (dt.Rows.Count > 0)
                {
                    ddlSourcetbl.DataSource = dt;
                    ddlSourcetbl.DataTextField = "TBL_NAME";
                    ddlSourcetbl.DataValueField = "TBL_NAME";
                    ddlSourcetbl.DataBind();
                }
                dt = null;
                ddlSourcetbl.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                  objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void GetTblcolmnl(DropDownList ddl, string tblname)
        {
            try
            {
                // ddlTblcolmnl.Items.Clear();
                ddl.Items.Clear();
                DataTable dt;
                Hashtable htparam = new Hashtable();
                htparam.Clear();
                string tblcolV = (string)(Session["TBL_NAME"]);
                htparam.Add("@TBL_NAME", tblname);
                dt = objDAL.GetDataTable("Prc_Tblcolmn", htparam);
                if (dt.Rows.Count > 0)
                {
                    ddl.DataSource = dt;
                    ddl.DataTextField = "COL_NAM";
                    ddl.DataValueField = "COL_NAM";
                    ddl.DataBind();
                }

                dt = null;
                ddl.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {

                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                  objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void GetOperator(DropDownList ddl)
        {
            try
            {

                ddlOperator.Items.Clear();
                DataTable dt;
                Hashtable htparam = new Hashtable();
                htparam.Clear();
                htparam.Add("@FLAG", "OPRTR");
                dt = objDAL.GetDataTable("Prc_Operator", htparam);
                if (dt.Rows.Count > 0)
                {
                    ddlOperator.DataSource = dt;
                    ddlOperator.DataTextField = "ParamDesc1";
                    ddlOperator.DataValueField = "ParamValue";
                    ddlOperator.DataBind();
                }
                dt = null;
                ddlOperator.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                  objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void FillddlStatus(DropDownList ddl)
        {
            try
            {

                Hashtable ht = new Hashtable();

                ht.Add("@FLAG", "STS");
                ddlStatus.Items.Clear();
                dt = objDAL.GetDataTable("Prc_Operator", ht);
                if (dt.Rows.Count > 0)
                {
                    ddlStatus.DataSource = dt;
                    ddlStatus.DataTextField = "ParamDesc1";
                    ddlStatus.DataValueField = "ParamValue";
                    ddlStatus.DataBind();

                }
              //  dt.Close();
                ddlStatus.Items.Insert(0, new ListItem("-- SELECT --", ""));

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
                Hashtable hterr = new Hashtable();
                hterr.Clear();
                DataSet dserr = new DataSet();
                dserr.Clear();
                string msgs = string.Empty;

                hterr.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                hterr.Add("@TBL_SRC", ddlSourcetbl.SelectedValue);
                hterr.Add("@TBL_COL", ddlTblcolmnl.SelectedValue);
                hterr.Add("@OPRTR", ddlOperator.SelectedValue);
                hterr.Add("@STATUS", ddlStatus.SelectedValue);
                hterr.Add("@COL_VAL", txtColmnVal.Text);
                hterr.Add("@ERR_DESC", txtErrDesc.Text);
                hterr.Add("@CREATED_BY", Session["UserID"].ToString().Trim());
                if (txtEfDate.Text == "")
                {
                    hterr.Add("@EFF_DATE", System.DBNull.Value);
                }
                else
                {
                    hterr.Add("@EFF_DATE", Convert.ToDateTime(txtEfDate.Text.ToString().Trim()));
                }
                if (txtCseDate.Text == "")
                {
                    hterr.Add("@CEASE_DTM", System.DBNull.Value);
                }
                else
                {
                    hterr.Add("@CEASE_DTM", Convert.ToDateTime(txtCseDate.Text.ToString().Trim()));
                }
                dserr = objDAL.GetDataSet("PRC_INS_MST_KPI_INTGRTN_ERR_JOBS", hterr);

                if (dserr.Tables.Count > 0 && dserr.Tables[0].Rows.Count > 0)
                {
                    msgs = dserr.Tables[0].Rows[0]["MSG"].ToString().Trim();
                    if (msgs == "FAILED")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Already exists..!!');", true);
                        //ddlAccMode.SelectedIndex = 0;
                        //rblCRYFWD.SelectedValue = "";
                        //txtMaxLmt.Text = "";

                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Data Saved Successfully...!!!')", true);
                    }
                }


                ErrHandlGridBind();

                ddlSourcetbl.SelectedValue = "";
                ddlTblcolmnl.SelectedValue = "";
                ddlOperator.SelectedValue = "";
                txtColmnVal.Text = "";
                txtErrDesc.Text = "";
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                 objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        //private void ShowNoResultFound(DataTable source, GridView gv)
        //{
        //    source.Rows.Add(source.NewRow());

        //    gv.DataSource = source;
        //    gv.DataBind();
        //    int columnsCount = gv.Columns.Count;
        //    int rowsCount = gv.Rows.Count;
        //    gv.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
        //    gv.Rows[0].Cells[columnsCount - 1].Text = "";
        //    gv.Rows[0].Cells[columnsCount - 2].Text = "";
        //    gv.Rows[0].Cells[0].Text = "No tables have been defined";
        //  }

        protected void ErrHandlGridBind()
        {
            try
            {
                DataSet dserrg = new DataSet();
                dserrg.Clear();
                Hashtable hterrg = new Hashtable();
                hterrg.Clear();
                hterrg.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                dserrg = objDAL.GetDataSet("PRC_GET_MST_KPI_INTGRTN_ERR_JOBS", hterrg);
                dgErrHandlng.DataSource = dserrg;
                dgErrHandlng.DataBind();

                //if (ds.Tables.Count > 0)
                //{
                //}
                //else
                //{
                //    DataTable dt = ds.Tables[0];
                //    ShowNoResultFound(dt, dgErrHandlng);
                //}
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                  objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }

        }


        protected void BtnClear_Click(object sender, EventArgs e)
        {
            ddlSourcetbl.SelectedValue = "";
            ddlTblcolmnl.SelectedValue = "";
            ddlOperator.SelectedValue = "";
            txtColmnVal.Text = "";
            txtErrDesc.Text = "";
            txtCseDate.Text = "";
            //txtEfDate.Text = "";
        }

        protected void ddlSourcetbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTblcolmnl(ddlTblcolmnl, ddlSourcetbl.SelectedValue.ToString().Trim());
        }

        protected void ddlTblcolmnl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlOperator_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlColmnVal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void lnkedit_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsed = new DataSet();
                Hashtable hted = new Hashtable();
                dsed.Clear();
                hted.Clear();
                txtCseDate.Enabled = true;
                BtnClear.Visible = false;
                btnupd.Visible = true;
                btnSave.Visible = false;
                //ddlSourcetbl.Enabled = false;
                //ddlTblcolmnl.Enabled = false;
                ddlStatus.Enabled = true;
                // btnupd.Attributes.Add("style", "display:block");
                string INTG_ID = string.Empty;
                LinkButton button = (LinkButton)sender;
                string TblCol = (string)button.Attributes["data-myData"];
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lbltblsrc = row.FindControl("lbltblsrc") as Label;
                Label lbltblcol = row.FindControl("lbltblcol") as Label;
                Label lbloprt = row.FindControl("lbloprt") as Label;
                Label lblcolvl = row.FindControl("lblcolvl") as Label;
                Label lblerrdesc = row.FindControl("lblerrdesc") as Label;
                Label lblsts = row.FindControl("lblsts") as Label;
                Label lblefd = row.FindControl("lblefd") as Label;
                Label lblcsd = row.FindControl("lblcsd") as Label;
                Label lblerrsttsvl = row.FindControl("lblerrsttsvl") as Label;
                Label lblseq = row.FindControl("lblseq") as Label;

                ViewState["hdnSEQNO"] = lblseq.Text;

                ddlSourcetbl.SelectedValue = lbltblsrc.Text;
                ddlOperator.SelectedValue = lbloprt.Text;
                txtColmnVal.Text = lblcolvl.Text;
                txtErrDesc.Text = lblerrdesc.Text;
                txtEfDate.Text = Convert.ToDateTime(lblefd.Text.Trim()).ToString("dd/MM/yyyy");
                GetTblcolmnl(ddlTblcolmnl, ddlSourcetbl.SelectedValue.ToString().Trim());
                ddlTblcolmnl.SelectedValue = lbltblcol.Text.ToString();
                // txtCseDate.Text = Convert.ToDateTime(lblcsd.Text.Trim()).ToString("dd/MM/yyyy");
                ddlStatus.SelectedValue = lblerrsttsvl.Text;


            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                 objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        protected void btnupd_Click(object sender, EventArgs e)
        {
            try
            {
                string msgs = string.Empty;
                DataSet DsUpdate = new DataSet();
                Hashtable htUpd = new Hashtable();
                DsUpdate.Clear();
                htUpd.Clear();
                htUpd.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                htUpd.Add("@TBL_SRC", ddlSourcetbl.SelectedValue);
                htUpd.Add("@TBL_COL", ddlTblcolmnl.SelectedValue);
                if (ddlStatus.SelectedValue == null || ddlStatus.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Status');", true);
                    return;
                }
                else
                {
                    htUpd.Add("@STATUS", ddlStatus.SelectedValue);
                }
                htUpd.Add("@OPRTR", ddlOperator.SelectedValue);
                htUpd.Add("@COL_VAL", txtColmnVal.Text);
                htUpd.Add("@ERR_DESC", txtErrDesc.Text);
                if (txtCseDate.Text == "")
                {
                    htUpd.Add("@CEASE_DTM", System.DBNull.Value);
                }
                else
                {

                    htUpd.Add("@CEASE_DTM", Convert.ToDateTime(txtCseDate.Text.ToString().Trim()));
                }
                htUpd.Add("@UPDATED_BY", Session["UserID"].ToString().Trim());
                htUpd.Add("@SEQNO", ViewState["hdnSEQNO"].ToString());
                htUpd.Add("@Flag", "UP");

                DsUpdate = objDAL.GetDataSet("PRC_UPD_MST_KPI_INTGRTN_ERR_JOBS", htUpd);
                if (DsUpdate.Tables.Count > 0 && DsUpdate.Tables[0].Rows.Count > 0)
                {
                    msgs = DsUpdate.Tables[0].Rows[0]["MSG"].ToString().Trim();
                    if (msgs == "FAILED")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Already exists..!!');", true);
                        // return;
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Updated Successfully...!!!')", true);
                    }

                }

                ddlSourcetbl.Enabled = true;
                ddlTblcolmnl.Enabled = true;
                ddlOperator.SelectedValue = "";
                ddlSourcetbl.SelectedValue = "";
                ddlTblcolmnl.SelectedValue = "";
                txtErrDesc.Text = "";
                txtColmnVal.Text = "";
                txtCseDate.Enabled = false;
                txtCseDate.Text = "";
                ErrHandlGridBind();
                ddlStatus.SelectedValue = "1";
                btnSave.Visible = true;
                btnupd.Visible = false;
                BtnClear.Visible = true;
                ddlStatus.Enabled = false;
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                 objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        protected void lnkdelt_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable htdel = new Hashtable();
                htdel.Clear();
                DataSet dsdel = new DataSet();
                dsdel.Clear();

                LinkButton lnkbtn = sender as LinkButton;
                GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
                Label lbltblcol = (Label)gvrow.FindControl("lbltblcol");
                Label lbltblsrc = (Label)gvrow.FindControl("lbltblsrc");
                Label lblseq = (Label)gvrow.FindControl("lblseq");
                int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

                htdel.Add("@TBL_SRC", lbltblsrc.Text);
                htdel.Add("@TBL_COL", lbltblcol.Text);
                htdel.Add("@SEQNO", lblseq.Text);

                dsdel = objDAL.GetDataSet("PRC_DEL_MST_kPI_ERR_JOBS", htdel);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Deleted Successfully...!!!')", true);
                ErrHandlGridBind();

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

        }
    }
}