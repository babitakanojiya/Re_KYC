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
    public partial class test : System.Web.UI.Page
    {
        CommonUtility cu = new CommonUtility();
        ErrorLog objErr = new ErrorLog();
        int AppId = 0;
        SqlDataReader sdr ;
        DataAccessLayer objDAL = new DataAccessLayer();
        DataTable dt = new DataTable();
        Hashtable htParam = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "PopulateCalender", "PopulateCalender()", true);
                    txtED.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    FillDestinationDB();
                    FillStatus();
                    ddlstts.SelectedIndex = 1;
                    ddlstts.Enabled = false;
                }
                BindSynonymGrid();
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void FillDestinationDB()
        {
            try
            {

                objDAL = new DataAccessLayer("INTGRTNConnectionString");
             DataTable   dt = new DataTable();
                Hashtable htParam = new Hashtable();

                htParam.Clear();
                dt = objDAL.GetDataTable("PRC_FILL_DESTINATION_DB", htParam);
                Hashtable HT = new Hashtable();

                if (dt.Rows.Count > 0)
                {
                    ddlDestnDb.DataSource = dt;
                    ddlDestnDb.DataTextField = "DATABASE_NAME";
                    ddlDestnDb.DataValueField = "DATABASE_NAME";
                    ddlDestnDb.DataBind();
                }
               
                ddlDestnDb.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void FillStatus()
        {
            try
            {
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                DataTable dt = new DataTable();
                Hashtable HTS = new Hashtable();
                HTS.Clear();

                dt = objDAL.GetDataTable("PRC_FILL_STTS_SYNYM_SU_DRPDOWN", HTS);
                if (dt.Rows.Count > 0)
                {
                    ddlstts.DataSource = dt;
                    ddlstts.DataTextField = "ParamDesc1";
                    ddlstts.DataValueField = "ParamValue";
                    ddlstts.DataBind();
                }
                //drRead.Close();
                ddlstts.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void txttblnm_TextChanged(object sender, EventArgs e)
        {
            txtsynm.Text = "SYNYM_" + txttblnm.Text.Trim();
        }

        public void ClearControls()
        {
            txtsynm.Text = "";
            txtlnkid.Text = "";
            txtdb.Text = "";
            ddlDestnDb.SelectedIndex = 0;
            ddlstts.SelectedIndex = 1;
            //  txtED.Text = "";
            txtCD.Text = "";
            txttblnm.Text = "";
            txtsyndesc.Text = "";

        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                htParam.Clear();
                DataSet ds = new DataSet();
                ds.Clear();
                string msgs = string.Empty;

                htParam.Add("@LNK_SVR_ID", txtlnkid.Text.ToString().Trim());
                htParam.Add("@DB_NAME", txtdb.Text.ToString().Trim());
                htParam.Add("@TBL_NAME", txttblnm.Text.ToString().Trim());
                htParam.Add("@SYNYM_DESC", txtsyndesc.Text.ToString().Trim());
                htParam.Add("@DSTN_DB", ddlDestnDb.SelectedValue);
                htParam.Add("@CREATEDBY", Session["UserID"].ToString().Trim());
                htParam.Add("@STATUS", ddlstts.SelectedValue.ToString());
                if (txtED.Text == "")
                {
                    htParam.Add("@EFF_DTIM", System.DBNull.Value);
                }
                else
                {
                    htParam.Add("@EFF_DTIM", Convert.ToDateTime(txtED.Text.ToString().Trim()));
                }
                if (txtCD.Text == "")
                {
                    htParam.Add("@CSE_DTIM", System.DBNull.Value);
                }
                else
                {
                    htParam.Add("@CSE_DTIM", Convert.ToDateTime(txtCD.Text.ToString().Trim()));
                }

                //  ds = objDal.GetDataSetForPrc_SAIM("PRC_INS_MST_KPI_INTGRTN_SNYM_SU", htParam);
               // DataAccessLayer l = new DataAccessLayer();
               ds= objDAL.GetDataSet("PRC_INS_MST_KPI_INTGRTN_SNYM_SU", htParam, "INTGRTNConnectionString");
                ClearControls();
                BindSynonymGrid();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    msgs = ds.Tables[0].Rows[0]["MSG"].ToString().Trim();
                    if (msgs == "FAILED")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Synonym Name  already exists..!!');", true);
                        //ddlAccMode.SelectedIndex = 0;
                        //rblCRYFWD.SelectedValue = "";
                        //txtMaxLmt.Text = "";

                        return;
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Saved Successfully...!!!')", true);
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }

        }


        protected void BindSynonymGrid()
        {
            try
            {
                DataSet dsSyn = new DataSet();
                dsSyn.Clear();
                Hashtable htSyn = new Hashtable();
                htSyn.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                dsSyn = objDAL.GetDataSet("PRC_GET_SYNONYMDTLS", htSyn);
                grdsyn.DataSource = dsSyn;
                grdsyn.DataBind();

                if (dsSyn.Tables.Count > 0 && dsSyn.Tables[0].Rows.Count > 0)
                {
                    //dsSyn.DataSource = dsSyn.Tables[0];
                    //dsSyn.DataBind();
                    ViewState["gridgrdsyn"] = dsSyn.Tables[0];

                    if (grdsyn.PageCount > 1)
                    {
                        btnnext.Enabled = true;
                    }
                    else
                    {
                       btnnext.Enabled = false;
                    }
                }
                else
                {
                    btnprevious.Enabled = false;
                    btnnext.Enabled = false;
                    txtPage.Text = "1";

                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
                string sRet = oInfo.Name;
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                String LogClassName = method.ReflectedType.Name;
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }

        }

        protected void lnkedit_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                txtsynm.Enabled = false;
                btnupd.Visible = true;
                txtCD.Enabled = true;
                txtED.Enabled = false;
                ddlstts.Enabled = true;
                //Response.Buffer = false;
                //Response.ClearHeaders();
                btnclr.Visible = false;
                btnSave.Enabled = false;

                LinkButton button = (LinkButton)sender;
                string SYNYM = (string)button.Attributes["data-myData"];

                DataSet Dsedit = new DataSet();
                Dsedit.Clear();
                Hashtable htedit = new Hashtable();
                htedit.Clear();
                SqlDataAdapter sda;
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblsynSeqNo = row.FindControl("lblsynSeqNo") as Label;
         
                ViewState["hdnsynSEQNO"] = lblsynSeqNo.Text;
                if ("SYNYM" != null || "SYNYM" != "")
                {
                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["INTGRTNConnectionString"].ToString());
                    SqlCommand cmn;
                    cmn = new SqlCommand("PRC_GRD_SYNONYN_RECORDS", con);

                    cmn.CommandType = CommandType.StoredProcedure;

                    SqlDataReader sdr;
                    cmn.Parameters.Add("@SYNYM_NAME", SYNYM.ToString());
                    cmn.Connection = con;
                    con.Open();

                    sdr = cmn.ExecuteReader(CommandBehavior.CloseConnection);
                    bool temp = false;

                    while (sdr.Read())
                    {

                        txtlnkid.Text = sdr["LNK_SVR_ID"].ToString();
                        txtdb.Text = sdr["DB_NAME"].ToString();
                        txttblnm.Text = sdr["TBL_NAME"].ToString();
                        txtsynm.Text = sdr["SYNYM_NAME"].ToString();
                        ddlDestnDb.SelectedValue = sdr["DSTN_DB"].ToString();
                        ddlstts.SelectedValue = sdr["STATUS"].ToString();
                        txtED.Text = sdr["EFF_DTIM"].ToString();
                        txtCD.Text = sdr["CSE_DTIM"].ToString();
                        txtsyndesc.Text = sdr["SYNYM_DESC"].ToString();
                        temp = true;
                    }
                    //sdr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
                string sRet = oInfo.Name;
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                String LogClassName = method.ReflectedType.Name;
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }

        }

        protected void btnupd_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet DsUpdate = new DataSet();
                Hashtable htUpd = new Hashtable();
                DsUpdate.Clear();
                htUpd.Clear();

                string SYNYM_NAME = txtsynm.Text;
                string LNK_SVR_ID = txtlnkid.Text;
                string DB_NAME = txtdb.Text;
                string DSTN_DB = ddlDestnDb.SelectedValue;
                string TBL_NAME = txttblnm.Text;
                //txtCD.Enabled = false;

                if (SYNYM_NAME != "")
                {
                    htUpd.Add("@LNK_SVR_ID", LNK_SVR_ID);
                    htUpd.Add("@DB_NAME", DB_NAME);
                    htUpd.Add("@TBL_NAME", TBL_NAME);
                    htUpd.Add("@SYNYM_NAME", SYNYM_NAME);
                    htUpd.Add("@DSTN_DB", DSTN_DB);
                    htUpd.Add("@SYNYM_DESC", txtsyndesc.Text);
                    htUpd.Add("@STATUS", ddlstts.SelectedValue.ToString());
                    htUpd.Add("@SEQNO", ViewState["hdnsynSEQNO"].ToString().Trim());
                    if (txtCD.Text == "")
                    {
                        htUpd.Add("@CSE_DTIM", System.DBNull.Value);
                    }
                    else
                    {
                        htUpd.Add("@CSE_DTIM", Convert.ToDateTime(txtCD.Text.ToString().Trim()));
                    }
                    htUpd.Add("@UPDATEDBY", Session["UserID"].ToString().Trim());

                    objDAL = new DataAccessLayer("INTGRTNConnectionString");
                    DsUpdate = objDAL.GetDataSet("PRC_UPD_MST_KPI_INTGRTN_SNYM_SU", htUpd);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Updated Successfully...!!!')", true);

                    btnSave.Enabled = true;
                    btnupd.Visible = false;
                    btnclr.Visible = true;
                    txtCD.Enabled = false;
                    //txtED.Enabled = true;
                    ddlstts.Enabled = false;
                    BindSynonymGrid();
                    ClearControls();
                }
            }

            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
                string sRet = oInfo.Name;
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                String LogClassName = method.ReflectedType.Name;
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
          
            }
        }

        protected void btnclr_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
        protected void lnksyndel_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable htdel = new Hashtable();
                htdel.Clear();
                DataSet dsdel = new DataSet();
                dsdel.Clear();

                LinkButton lnkbtn = sender as LinkButton;
                GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
                Label lbltblnm = (Label)gvrow.FindControl("lbltblnm");
                Label lblSynym = (Label)gvrow.FindControl("lblSynym");
                int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

                htdel.Add("@TBL_NAME", lbltblnm.Text);
                htdel.Add("@SYNYM_NAME", lblSynym.Text);

                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                dsdel = objDAL.GetDataSet("PRC_DEL_MST_KPI_INTGRTN_SNYM_SU", htdel);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Deleted Successfully...!!!')", true);
                BindSynonymGrid();

            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
                string sRet = oInfo.Name;
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                String LogClassName = method.ReflectedType.Name;
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void btnprevious_Click(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = grdsyn.PageIndex;
                grdsyn.PageIndex = pageIndex - 1;
                BindSynonymGrid();
                //grdsyn.DataSource = (DataTable)Session["grid"];
                //grdsyn.DataBind();
                txtPage.Text = Convert.ToString(Convert.ToInt32(txtPage.Text) - 1);
                if (txtPage.Text == "1")
                {
                    btnprevious.Enabled = false;
                }
                else
                {
                    btnprevious.Enabled = true;
                }
                btnnext.Enabled = true;
            }
            catch (Exception ex)
            {
                throw ex;   
            }
        }

        protected void btnnext_Click(object sender, EventArgs e)
        {
             try
        {
            int pageIndex = grdsyn.PageIndex;
            grdsyn.PageIndex = pageIndex + 1;
            BindSynonymGrid();
            txtPage.Text = Convert.ToString(Convert.ToInt32(txtPage.Text) + 1);
            btnprevious.Enabled = true;
            if (txtPage.Text == Convert.ToString(grdsyn.PageCount))
            {
                btnnext.Enabled = false;
            }

            int page = grdsyn.PageCount;
        }
             catch (Exception ex)
             {
                 throw ex;
             }
        }

    }
}