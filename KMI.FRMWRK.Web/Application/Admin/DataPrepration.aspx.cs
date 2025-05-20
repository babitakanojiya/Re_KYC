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
    public partial class DataPrepration : System.Web.UI.Page
    {
        CommonUtility cu = new CommonUtility();
        ErrorLog objErr = new ErrorLog();
        int AppId = 0;
        SqlDataReader sdr;
        DataAccessLayer objDAL = new DataAccessLayer();
        DataTable dt = new DataTable();
        Hashtable htParam = new Hashtable();
        DataSet DS = new DataSet();
        string INT_ID = string.Empty;
        string DATA_TYP_ID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["INTG_ID"].ToString().Trim() != "")
                    {
                        lblintgrtnId.Text = Request.QueryString["INTG_ID"].ToString().Trim();
                    }
                    FillDataPrepTextbox();
                    DATAPREP_TYPE();
                    FillStatus();
                    FillFrmTbl();
                    FillSetTbl();
                    BindDataPreparation();
                    FillSetTblcol_Status();
                    FillDJ_Status();
                    FillWhrCnd_Status();
                    FillOprtr();

                    ddlst.SelectedValue = "1";
                    ddlsest.SelectedValue = "1";
                    ddljstat.SelectedIndex = 1;
                    ddlwhstat.SelectedIndex = 1;

                    txtED.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtEF.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txteff.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtefrm.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    ddlcn.Items.Insert(0, new ListItem("-- SELECT --", ""));
                    ddlsttblcol.Items.Insert(0, new ListItem("-- SELECT --", ""));
                    ddlvalstcol.Items.Insert(0, new ListItem("-- SELECT --", ""));
                    ddlsttbcol.Items.Insert(0, new ListItem("-- SELECT --", ""));
                    ddlfrmtbcol.Items.Insert(0, new ListItem("-- SELECT --", ""));

                }

            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");

            }
        }

        #region Bind DataPrep_ID Textbox
        protected void FillDataPrepTextbox()
        {
            try
            {
                DS.Clear();
                htParam.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@FLAG", "C");
                DS = objDAL.GetDataSet("PRC_GET_PREP_ID", htParam);

                if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    txtdprpId.Text = DS.Tables[0].Rows[0]["CTRNO"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {

                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion

        #region DATAPREP_TYPE DROPDOWN
        protected void DATAPREP_TYPE()
        {
            try
            {
                Hashtable HTS = new Hashtable();
                HTS.Clear();
                dt.Clear();
                ddldpt.Items.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                HTS.Add("@FLAG", "DPT");
                dt = objDAL.GetDataTable("PRC_GET_PREP_ID", HTS);
                if (dt.Rows.Count > 0)
                {
                    ddldpt.DataSource = dt;
                    ddldpt.DataTextField = "ParamDesc1";
                    ddldpt.DataValueField = "ParamValue";
                    ddldpt.DataBind();
                }
                ddldpt.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion

        #region Status Dropdown data prep
        protected void FillStatus()
        {
            try
            {
                Hashtable HTS = new Hashtable();
                HTS.Clear();
                dt.Clear();
                ddlst.Items.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                HTS.Add("@FLAG", "S");
                dt = objDAL.GetDataTable("PRC_GET_PREP_ID", HTS);
                if (dt.Rows.Count > 0)
                {
                    ddlst.DataSource = dt;
                    ddlst.DataTextField = "ParamDesc1";
                    ddlst.DataValueField = "ParamValue";
                    ddlst.DataBind();
                }
                //drRead.Close();
                ddlst.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion

        #region Fill Set Table Dropdown
        protected void FillSetTbl()
        {
            try
            {
                Hashtable HT = new Hashtable();
                HT.Clear();
                dt.Clear();
                ddlsettbl.Items.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                HT.Add("@FLAG", "STD");
                HT.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString());
                dt = objDAL.GetDataTable("PRC_FILL_TBLLIST_INTGRTN", HT);
                if (dt.Rows.Count > 0)
                {
                    ddlsettbl.DataSource = dt;
                    ddlsettbl.DataTextField = "SRC_TBL_ID";
                    ddlsettbl.DataValueField = "SRC_TBL_ID";
                    ddlsettbl.DataBind();
                }
                // drRead.Close();
                ddlsettbl.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion

        #region From Table Dropdown
        protected void FillFrmTbl()
        {
            try
            {
                Hashtable HTF = new Hashtable();
                HTF.Clear();
                ddlfrmtbl.Items.Clear();
                dt.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                HTF.Add("@FLAG", "FTD");
                HTF.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString());
                dt = objDAL.GetDataTable("PRC_FILL_TBLLIST_INTGRTN", HTF);
                if (dt.Rows.Count > 0)
                {
                    ddlfrmtbl.DataSource = dt;
                    ddlfrmtbl.DataTextField = "SYNYM_NAME";
                    ddlfrmtbl.DataValueField = "SYNYM_NAME";
                    ddlfrmtbl.DataBind();
                }
                // drRead.Close();
                ddlfrmtbl.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion

        protected void RDSETFRMANTHER_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RDSETFRMANTHER.SelectedValue == "0")
            {
                ddlfrmtbl.Enabled = false;
                //  spnvlstco.Visible = false;
                ddlfrmtbl.SelectedValue = "";
                //  ddlsvf.Enabled = true;
                //txtval.Visible = true;
            }
            else
            {
                ddlfrmtbl.Enabled = true;
                //spnvlstco.Visible = true;
                //ddlsvf.Enabled = true;
            }
        }

        protected void ddldpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldpt.SelectedValue == "0")
            {
                RDSETFRMANTHER.Enabled = false;
                ddlsettbl.Enabled = false;
                ddlfrmtbl.Enabled = false;
                txtprcnme.Enabled = true;
                ddlsettbl.SelectedValue = "";
                ddlfrmtbl.SelectedValue = "";

            }
            else
            {
                RDSETFRMANTHER.Enabled = true;
                ddlsettbl.Enabled = true;
                ddlfrmtbl.Enabled = true;
                txtprcnme.Enabled = false;
                txtprcnme.Text = "";
            }
        }

        protected void btndataprep_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet Dp = new DataSet();
                Dp.Clear();
                Hashtable hdp = new Hashtable();
                hdp.Clear();
                string msgs = string.Empty;
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                hdp.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                hdp.Add("@DATA_PRPRTN_ID", txtdprpId.Text.ToString());
                if (ddldpt.SelectedValue == "0" && ddlsettbl.SelectedItem.Text == "--SELECT--")
                {
                    hdp.Add("@SET_TBL", System.DBNull.Value);
                }
                else if (ddldpt.SelectedValue == "1" && ddlsettbl.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Set Table Field');", true);
                    return;
                }
                else
                {
                    hdp.Add("@SET_TBL", ddlsettbl.SelectedValue.ToString());
                }
                if (ddldpt.SelectedValue == "0" && ddlfrmtbl.SelectedIndex == 0)
                {
                    hdp.Add("@FRM_TBL", System.DBNull.Value);
                }
                else if (ddldpt.SelectedValue == "1" && RDSETFRMANTHER.SelectedItem.Text == "Yes" && ddlfrmtbl.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select From Table Field');", true);
                    return;
                }

                //else if (ddlfrmtbl.SelectedValue!=null || ddlfrmtbl.SelectedValue!="" && RDSETFRMANTHER.SelectedItem.Text == "No")
                //{
                //    hdp.Add("@FRM_TBL", "");
                //    ddlfrmtbl.Enabled = false;
                //}
                else
                {
                    hdp.Add("@FRM_TBL", ddlfrmtbl.SelectedValue.ToString());
                }
                hdp.Add("@DATA_PRPR_TYPE", ddldpt.SelectedValue.ToString());
                if (txtprcnme.Text == "" && ddldpt.SelectedValue == "1")
                {
                    hdp.Add("@PRCSR_NAME", System.DBNull.Value);
                }
                else if (txtprcnme.Text == "" && ddldpt.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Processor Name');", true);
                    return;
                }

                else
                {
                    hdp.Add("@PRCSR_NAME", txtprcnme.Text.ToString());
                }
                hdp.Add("@STATUS", ddlst.SelectedValue.ToString().Trim());
                hdp.Add("@FLAG", "SV");
                hdp.Add("@SEQNO", "");
                if (txtED.Text == "")
                {
                    hdp.Add("@EFF_FRM_DT", System.DBNull.Value);
                }
                else
                {
                    hdp.Add("@EFF_FRM_DT", Convert.ToDateTime(txtED.Text.ToString().Trim()));
                }
                if (txtcd.Text == "")
                {
                    hdp.Add("@CEASE_DT", System.DBNull.Value);
                }
                else
                {
                    hdp.Add("@CEASE_DT", Convert.ToDateTime(txtcd.Text.ToString().Trim()));
                }

                hdp.Add("@CREATED_BY", Session["UserID"].ToString().Trim());

                if (ddldpt.SelectedValue == "1" && RDSETFRMANTHER.SelectedItem.Text == "Yes")
                {
                    hdp.Add("@SET_FRM_ANTHR_TBL", 1);
                }
                else if (ddldpt.SelectedValue == "1" && RDSETFRMANTHER.SelectedItem.Text == "No")
                {
                    hdp.Add("@SET_FRM_ANTHR_TBL", 0);
                }
                else //(ddldpt.SelectedValue == "0" && RDSETFRMANTHER.SelectedItem.Text == "Yes")
                {
                    hdp.Add("@SET_FRM_ANTHR_TBL", System.DBNull.Value);
                }
                Dp = objDAL.GetDataSet("PRC_INS_MST_KPI_INTGRTN_DATA_PRPR", hdp);
                if (Dp.Tables.Count > 0 && Dp.Tables[0].Rows.Count > 0)
                {
                    msgs = Dp.Tables[0].Rows[0]["MSG"].ToString().Trim();
                    if (msgs == "FAILED")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Record Already exists..!!');", true);
                        // return;
                        // ddlsettbl.SelectedValue = "";
                        //ddlfrmtbl.SelectedValue = "";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Data Saved Successfully...!!!')", true);
                    }
                }

                BindDataPreparation();
                ddlsettbl.SelectedValue = "";
                ddlfrmtbl.SelectedValue = "";
                FillDataPrepTextbox();
                ddlst.SelectedValue = "1";
                RDSETFRMANTHER.SelectedValue = "1";
                ddlfrmtbl.Enabled = true;
                ddldpt.SelectedIndex = 0;
                ddlsettbl.Enabled = true;
                txtprcnme.Text = "";
                txtprcnme.Enabled = false;
                RDSETFRMANTHER.Enabled = true;
            }

            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        #region Gridview of data prepration
        protected void BindDataPreparation()
        {
            try
            {
                DataSet dsdp = new DataSet();
                dsdp.Clear();
                Hashtable htdp = new Hashtable();
                htdp.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htdp.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                dsdp = objDAL.GetDataSet("PRC_GET_MST_KPI_INTGRTN_DATA_PRPR", htdp);
                //grddp.DataSource = dsdp;
                //grddp.DataBind();

                if (dsdp.Tables.Count > 0 && dsdp.Tables[0].Rows.Count > 0)
                {
                    grddp.DataSource = dsdp;
                    grddp.DataBind();
                    ViewState["grid"] = dsdp.Tables[0];

                    txtPage.Visible = true;
                    btnprevious.Visible = true;
                    btnnext.Visible = true;

                    if (grddp.PageCount == 1)
                    {
                        txtPage.Text = "1";
                        btnnext.Enabled = false;
                        btnprevious.Enabled = false;

                    }

                    if (grddp.PageCount > 1)
                    {

                        btnnext.Enabled = true;
                    }
                    else
                    {
                        btnnext.Enabled = false;
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
        #endregion
        protected void btnprevious_Click(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = grddp.PageIndex;
                grddp.PageIndex = pageIndex - 1;
                BindDataPreparation();
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
                int pageIndex = grddp.PageIndex;
                grddp.PageIndex = pageIndex + 1;
                BindDataPreparation();
                txtPage.Text = Convert.ToString(grddp.PageIndex + 1);
                btnprevious.Enabled = true;
                if (txtPage.Text == Convert.ToString(grddp.PageCount))
                {
                    btnnext.Enabled = false;
                }

                int page = grddp.PageCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lnksettbcol_Click(object sender, EventArgs e)
        {
            LinkButton button = (LinkButton)sender;
            string SETFROMANTHERTBL = (string)button.Attributes["data-myData"];

            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            Label lbldpid = (Label)gvrow.FindControl("lbldpid");
            Label lblstvl = (Label)gvrow.FindControl("lblstvl");
            Label lblsfanvl = (Label)gvrow.FindControl("lblsfanvl");
            // HiddenField dndataprepid = (HiddenField)gvrow.FindControl("lbldpid");
            HiddenField hdndpi = gvrow.FindControl("hdndpi") as HiddenField;

            HiddenField HdnDATA_PREP = new HiddenField();
            HdnDATA_PREP.Value = lbldpid.Text;
            ViewState["hdnstfrmanthr"] = lblsfanvl.Text;
            ViewState["hdndataprepid"] = lbldpid.Text;
            SETVALFRM();
            if (SETFROMANTHERTBL == "No")
            {

                divsettblcol.Visible = true;
                divtblcolbdy.Visible = true;
                ddlvalstcol.Enabled = false;
                Lnkvalstcol.Visible = true;
                divwhcondn.Visible = false;
                divjcond.Visible = false;
                spnvlstco.Visible = false;
                divp.Visible = false;
                divjc.Visible = false;
                FillSettblcol(lbldpid.Text, "ST");
                BindSetTblColumn();
                //lnkedstc.Visible = false;
                //    ddlvalstcol.SelectedIndex = 0;
                btnsetupd.Attributes.Add("style", "display:none;");
                btnsetadd.Attributes.Add("style", "display:inline-block;");
                btnsetclr.Attributes.Add("style", "display:inline-block;");
                txtCED.Enabled = false;
                txtCED.Text = "";
                //  txtCED.Attributes.Add("disabled", "true");
                ddlsest.Enabled = false;
                // ddlsvf.Visible = false;
                //  lblsvf.Visible = false;
                //  Span15.Visible = false;
                txtval.Visible = false;
                lblvl.Visible = false;
            }
            else
            {

                divsettblcol.Visible = true;
                divtblcolbdy.Visible = true;
                ddlvalstcol.Enabled = true;
                Lnkvalstcol.Visible = false;
                divjcond.Visible = false;
                divwhcondn.Visible = false;
                divp.Visible = false;
                divjc.Visible = false;
                spnvlstco.Visible = true;
                FillSettblcol(lbldpid.Text, "ST");
                ddlsvf.Visible = true;
                Span15.Visible = true;
                lblsvf.Visible = true;
                //  FillValSetCol(lbldpid.Text);
                BindSetTblColumn();
                btnsetupd.Attributes.Add("style", "display:none;");
                btnsetadd.Attributes.Add("style", "display:inline-block;");
                btnsetclr.Attributes.Add("style", "display:inline-block;");
                txtCED.Text = "";
                ddlsest.Enabled = false;
                // txtCED.Enabled = false;
                // txtCED.Attributes.Add("disabled", "true");
            }
        }

        #region SetTableCol div Status Dropdown
        protected void FillSetTblcol_Status()
        {
            try
            {
                Hashtable HTSb = new Hashtable();
                HTSb.Clear();
                dt.Clear();
                ddlsest.Items.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                HTSb.Add("@FLAG", "S");
                dt = objDAL.GetDataTable("PRC_GET_PREP_ID", HTSb);
                if (dt.Rows.Count > 0)
                {
                    ddlsest.DataSource = dt;
                    ddlsest.DataTextField = "ParamDesc1";
                    ddlsest.DataValueField = "ParamValue";
                    ddlsest.DataBind();
                }
                // drRead.Close();
                ddlsest.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion

        #region SetTblcol dropdown
        protected void FillSettblcol(string Data_PrepID, string Flag) //DropDownList ddl
        {
            try
            {

                Hashtable HTstc = new Hashtable();
                HTstc.Clear();
                dt.Clear();
                ddlsttblcol.Items.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                HTstc.Add("@Flag", Flag);
                HTstc.Add("@DATA_PRPRTN_ID", Data_PrepID.ToString());
                dt = objDAL.GetDataTable("PRC_FILL_SET_TBL_COLUMN_DRPDWN", HTstc);
                if (dt.Rows.Count > 0)
                {
                    ddlsttblcol.DataSource = dt;
                    ddlsttblcol.DataTextField = "COL_NAM";
                    ddlsttblcol.DataValueField = "COL_NAM";
                    ddlsttblcol.DataBind();
                }
                //  drRead = null;
                ddlsttblcol.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion

        protected void SETVALFRM()
        {
            try
            {
                Hashtable HTS = new Hashtable();
                HTS.Clear();
                dt.Clear();
                ddlsvf.Items.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                HTS.Add("@FLAG", "SVF");
                dt = objDAL.GetDataTable("PRC_GET_PREP_ID", HTS);
                if (dt.Rows.Count > 0)
                {
                    ddlsvf.DataSource = dt;
                    ddlsvf.DataTextField = "ParamDesc1";
                    ddlsvf.DataValueField = "ParamValue";
                    ddlsvf.DataBind();
                }
                //drRead.Close();
                ddlsvf.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }


        #region FillValSetCol of settblcol tab
        protected void FillValSetCol(string Data_PrepID, string FLAG)
        {
            try
            {

                Hashtable HTvc = new Hashtable();
                HTvc.Clear();
                dt.Clear();
                //HTvc.Add("@SYNYM_NAME", );
                ddlvalstcol.Items.Clear();
                objDAL = new DataAccessLayer("INTGRTN_WORKConnectionString");
                HTvc.Add("@DATA_PRPRTN_ID", Data_PrepID);
                HTvc.Add("@FLAG", FLAG);
                //drRead = objDal.Common_exec_reader_prc_SAIM("PRC_FILL_VALSETCOL_FRMTBL", HTvc);
                dt = objDAL.GetDataTable("PRC_FILL_VALSETCOL_FRMTBL", HTvc);
                if (dt.Rows.Count > 0)
                {
                    ddlvalstcol.DataSource = dt;
                    ddlvalstcol.DataTextField = "paramval";
                    ddlvalstcol.DataValueField = "paramdesc";
                    ddlvalstcol.DataBind();
                }
                //   drRead.Close();
                ddlvalstcol.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion

        protected void ddlsvf_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string data_prep_ID = ViewState["hdndataprepid"].ToString();
                string anthertbl = ViewState["hdnstfrmanthr"].ToString();
                if (ddlsvf.SelectedValue.ToString() == "SRC" && anthertbl.ToString() == "1")
                {
                    FillValSetCol(data_prep_ID, "SRC");
                    ddlvalstcol.Enabled = true;
                    txtval.Visible = false;
                    lblvl.Visible = false;
                    txtval.Text = "";
                }
                else if (ddlsvf.SelectedValue.ToString() == "SYN" && anthertbl.ToString() == "1")
                {
                    FillValSetCol(data_prep_ID, "");
                    ddlvalstcol.Enabled = true;
                    txtval.Visible = false;
                    lblvl.Visible = false;
                    txtval.Text = "";
                }
                else if (ddlsvf.SelectedValue.ToString() == "M" && anthertbl.ToString() == "0" || anthertbl.ToString() == "1")
                {
                    lblvl.Visible = true;
                    txtval.Visible = true;
                    ddlvalstcol.Enabled = false;

                    // Lnkvalstcol.Visible = true;
                }
                //else if (ddlsvf.SelectedValue.ToString() == "M" && anthertbl.ToString() == "1")
                //{
                //    lblvl.Visible = true;
                //    txtval.Visible = true;
                //    ddlvalstcol.Enabled = false;
                //   // Lnkvalstcol.Visible = true;
                //}

                else
                {
                    ddlvalstcol.Enabled = false;
                    txtval.Visible = false;
                    lblvl.Visible = false;
                    txtval.Text = "";
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        #region Save SetTblCol tab data
        protected void btnsetadd_Click(object sender, EventArgs e)
        {
            try
            {

                DataSet Dstc = new DataSet();
                Dstc.Clear();
                Hashtable hstc = new Hashtable();
                hstc.Clear();
                string anthertbl, msgs = string.Empty;
                anthertbl = ViewState["hdnstfrmanthr"].ToString();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                //string DATA_PRPRTN_ID1 = (string)(Session["Data_PrepID"]);
                hstc.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                hstc.Add("@DATA_PRPRTN_ID", ViewState["hdndataprepid"].ToString().Trim());
                hstc.Add("@SET_VAL_FRM", ddlsvf.SelectedValue.ToString().Trim());
                if (ddlsttblcol.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Set Table Column');", true);
                    return;
                }

                else
                {
                    hstc.Add("@SET_TBL_COL", ddlsttblcol.SelectedValue);
                }
                if (anthertbl == "1" && ddlsvf.SelectedValue.ToString() == "SRC" && ddlvalstcol.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Value Set Column');", true);
                    return;
                }
                else if (anthertbl == "1" && ddlsvf.SelectedValue.ToString() == "SYN" && ddlvalstcol.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Value Set Column');", true);
                    return;
                }
                else
                {
                    hstc.Add("@VAL_SET_COL", ddlvalstcol.SelectedValue.ToString());
                }
                hstc.Add("@STATUS", ddlsest.SelectedValue.ToString().Trim());
                if (txtEF.Text == "")
                {
                    hstc.Add("@EFF_FRM_DT", System.DBNull.Value);
                }
                else
                {
                    hstc.Add("@EFF_FRM_DT", Convert.ToDateTime(txtEF.Text.ToString().Trim()));
                }
                if (txtCED.Text == "")
                {
                    hstc.Add("@CEASE_DT", System.DBNull.Value);
                }
                else
                {
                    hstc.Add("@CEASE_DT", Convert.ToDateTime(txtCED.Text.ToString().Trim()));
                }

                if (ddlsvf.SelectedValue.ToString() == "SRC" || ddlsvf.SelectedValue.ToString() == "SYN" && txtval.Text == "")
                {
                    hstc.Add("@VALUE", System.DBNull.Value);
                }
                else if (ddlsvf.SelectedValue.ToString() == "M" && txtval.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Value');", true);
                    return;
                }
                else
                {
                    hstc.Add("@VALUE", txtval.Text.ToString().Trim());
                }


                hstc.Add("@CREATED_BY", Session["UserID"].ToString().Trim());
                hstc.Add("@Flag", "SAV");
                hstc.Add("@SEQNO", "");
                Dstc = objDAL.GetDataSet("PRC_INS_MST_KPI_INTGRTN_DATA_PRPR_SET_COL", hstc);
                if (Dstc.Tables.Count > 0 && Dstc.Tables[0].Rows.Count > 0)
                {
                    msgs = Dstc.Tables[0].Rows[0]["MSG"].ToString().Trim();
                    if (msgs == "FAILED")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Record Already exists..!!');", true);
                        // return;
                        // ddlsettbl.SelectedValue = "";
                        //ddlfrmtbl.SelectedValue = "";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Data Saved Successfully...!!!')", true);
                    }
                }

                BindSetTblColumn();
                FillSettblcol(ViewState["hdndataprepid"].ToString(), "ST");

                txtCED.Text = "";
                ddlsttblcol.SelectedValue = "";
                ddlvalstcol.SelectedValue = "";
                ddlsest.SelectedValue = "1";
                ddlsvf.SelectedIndex = 0;
                txtval.Text = "";
                txtval.Visible = false;
                lblvl.Visible = false;
            }

            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion

        #region Gridview of SetTblCol
        protected void BindSetTblColumn()
        {
            try
            {
                DataSet dstbc = new DataSet();
                dstbc.Clear();
                Hashtable httbc = new Hashtable();
                httbc.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                // httbc.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                httbc.Add("@DATA_PRPRTN_ID", ViewState["hdndataprepid"].ToString().Trim());
                dstbc = objDAL.GetDataSet("PRC_GET_MST_KPI_INTGRTN_DATA_PRPR_SET_COL", httbc);
                gridsttblcol.DataSource = dstbc;
                gridsttblcol.DataBind();

            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }

        }
        #endregion

        protected void lnkdj_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkbtn = sender as LinkButton;
                GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
                Label lbldpid = (Label)gvrow.FindControl("lbldpid");
                ViewState["hdndataprepid"] = lbldpid.Text;
                FillSettblcolmn(lbldpid.Text, "DJ");
                BindDefineJoin();
                divp.Visible = true;
                divjc.Visible = true;
                divsettblcol.Visible = false;
                divtblcolbdy.Visible = false;
                FillFromTblCol(lbldpid.Text, "");
                divwhcondn.Visible = false;
                divjcond.Visible = false;
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        #region Fill FromTblCol drpdwm Define join
        protected void FillFromTblCol(string Data_PrepID, string FLAG)
        {
            try
            {

                Hashtable HTtc = new Hashtable();
                HTtc.Clear();
                ddlfrmtbcol.Items.Clear();
                dt.Clear();
                //HTtc.Add("@SYNYM_NAME", ddlfrmtbl.SelectedValue.ToString());
                objDAL = new DataAccessLayer("INTGRTN_WORKConnectionString");
                HTtc.Add("@DATA_PRPRTN_ID", Data_PrepID.ToString());
                HTtc.Add("@FLAG", FLAG.ToString());
                dt = objDAL.GetDataTable("PRC_FILL_VALSETCOL_FRMTBL", HTtc);
                if (dt.Rows.Count > 0)
                {
                    ddlfrmtbcol.DataSource = dt;
                    ddlfrmtbcol.DataTextField = "paramval";
                    ddlfrmtbcol.DataValueField = "paramdesc";
                    ddlfrmtbcol.DataBind();
                }
                //    drRead.Close();

                ddlfrmtbcol.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion

        #region DefineJoins statusDropdown
        protected void FillDJ_Status()
        {
            try
            {
                Hashtable HTD = new Hashtable();
                HTD.Clear();
                ddljstat.Items.Clear();
                dt.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                HTD.Add("@FLAG", "S");
                dt = objDAL.GetDataTable("PRC_GET_PREP_ID", HTD);
                if (dt.Rows.Count > 0)
                {
                    ddljstat.DataSource = dt;
                    ddljstat.DataTextField = "ParamDesc1";
                    ddljstat.DataValueField = "ParamValue";
                    ddljstat.DataBind();
                }
                //  drRead.Close();
                ddljstat.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion

        #region SetTblcolmn dropdown for DefineJoin
        protected void FillSettblcolmn(string Data_PrepID, string Flag)
        {
            try
            {
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SAIMWRKConnectionString"].ConnectionString);
                //con.Open();
                Hashtable HTdjsc = new Hashtable();
                HTdjsc.Clear();
                dt.Clear();
                ddlsttbcol.Items.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                string Data_PrepID1 = (string)(Session["Data_PrepID"]);
                HTdjsc.Add("@Flag", Flag);
                HTdjsc.Add("@DATA_PRPRTN_ID", Data_PrepID.ToString());
                dt = objDAL.GetDataTable("PRC_FILL_SET_TBL_COLUMN_DRPDWN", HTdjsc);
                if (dt.Rows.Count > 0)
                {
                    ddlsttbcol.DataSource = dt;
                    ddlsttbcol.DataTextField = "COL_NAM";
                    ddlsttbcol.DataValueField = "COL_NAM";
                    ddlsttbcol.DataBind();

                }
                //  drRead = null;
                //.Close();
                ddlsttbcol.Items.Insert(0, new ListItem("-- SELECT --", " "));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion
        #region Define Join Gridview
        protected void BindDefineJoin()
        {
            try
            {
                DataSet dsj = new DataSet();
                dsj.Clear();
                Hashtable htj = new Hashtable();
                htj.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htj.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                htj.Add("@DATA_PRPRTN_ID", ViewState["hdndataprepid"].ToString().Trim());
                dsj = objDAL.GetDataSet("PRC_GET_MST_KPI_INTGRTN_DATA_PRPR_JOIN_DTLS", htj);
                grddefjoin.DataSource = dsj;
                grddefjoin.DataBind();

            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }

        }
        #endregion


        #region Save DefineJoins Data
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string msgs = string.Empty;
                DataSet DsDJ = new DataSet();
                DsDJ.Clear();
                Hashtable hsdj = new Hashtable();
                hsdj.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                //hsdj.Add("@SET_TBL", ddlsettbl.SelectedValue.ToString());
                hsdj.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                hsdj.Add("@DATA_PRPRTN_ID", ViewState["hdndataprepid"].ToString().Trim());
                hsdj.Add("@TBL_1_COL", ddlsttbcol.SelectedValue);
                //      ViewState["hdnddlsttbcol"] = ddlsttbcol.SelectedValue.ToString();
                hsdj.Add("@TBL_2_COL", ddlfrmtbcol.SelectedValue);
                hsdj.Add("@JN_ID", "");
                hsdj.Add("@STATUS", ddljstat.SelectedValue.ToString().Trim());

                if (txteff.Text == "")
                {
                    hsdj.Add("@EFF_FRM_DT", System.DBNull.Value);
                }
                else
                {
                    hsdj.Add("@EFF_FRM_DT", Convert.ToDateTime(txteff.Text.ToString().Trim()));
                }
                if (txtces.Text == "")
                {
                    hsdj.Add("@CEASE_DT", System.DBNull.Value);
                }
                else
                {
                    hsdj.Add("@CEASE_DT", Convert.ToDateTime(txtces.Text.ToString().Trim()));
                }

                hsdj.Add("@CREATED_BY", Session["UserID"].ToString().Trim());
                hsdj.Add("@Flag", "SV");
                hsdj.Add("@SEQNO", "");

                DsDJ = objDAL.GetDataSet("PRC_INS_MST_KPI_INTGRTN_DATA_PRPR_JOIN_DTLS", hsdj);
                if (DsDJ.Tables.Count > 0 && DsDJ.Tables[0].Rows.Count > 0)
                {
                    msgs = DsDJ.Tables[0].Rows[0]["MSG"].ToString().Trim();
                    if (msgs == "FAILED")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Record Already exists..!!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Data Saved Successfully...!!!')", true);
                    }
                }
                BindDefineJoin();
                FillSettblcolmn(ViewState["hdndataprepid"].ToString(), "DJ");
                txtces.Text = "";

                ddlfrmtbcol.SelectedValue = "";
                ddljstat.SelectedValue = "1";
                ddlsttbcol.SelectedValue = "";
            }

            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion

        protected void lnkwhcnd_Click(object sender, EventArgs e)
        {
            LinkButton button = (LinkButton)sender;
            string SETFROMANTHERTBL = (string)button.Attributes["data-myData"];
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            Label lbldpid = (Label)gvrow.FindControl("lbldpid");
            ViewState["hdndataprepid"] = lbldpid.Text;

            if (SETFROMANTHERTBL == "No")
            {
                divjcond.Visible = true;
                divwhcondn.Visible = true;
                divp.Visible = false;
                divjc.Visible = false;
                divsettblcol.Visible = false;
                divtblcolbdy.Visible = false;
                //FillDWC_TblCol(ddlcn, ddltb.SelectedValue.ToString());
                FillTable(lbldpid.Text);
                  BindDefineWhrCndn();
                btnwcupdte.Attributes.Add("style", "display:none;");
                btndwcsave.Attributes.Add("style", "display:inline-block;");
                btndwcClr.Attributes.Add("style", "display:inline-block;");
                ddlwhstat.Enabled = false;
                txtcsdt.Text = "";
                txtwhrcolval.Text = "";
                //ddlop.SelectedIndex = 0;
                //ddlcn.SelectedIndex = 0;
                txtcsdt.Enabled = false;
            }
            else
            {
                divjcond.Visible = true;
                divwhcondn.Visible = true;
                divp.Visible = false;
                divjc.Visible = false;
                divsettblcol.Visible = false;
                divtblcolbdy.Visible = false;
                BindDefineWhrCndn();
                FillTable(lbldpid.Text);
                // FillTable("FT", lbldpid.Text);
                btnwcupdte.Attributes.Add("style", "display:none;");
                btndwcsave.Attributes.Add("style", "display:inline-block;");
                btndwcClr.Attributes.Add("style", "display:inline-block;");
                ddlwhstat.Enabled = false;
                txtcsdt.Text = "";
                txtwhrcolval.Text = "";
                //ddlop.SelectedIndex = 0;
                //ddlcn.SelectedIndex = 0;
                txtcsdt.Enabled = false;
                //   FillDWC_TblCol(ddlcn,  ddltb.SelectedValue.ToString());
            }

        }

        #region FillOprtr dropdown
        protected void FillOprtr()
        {
            try
            {
                Hashtable HTop = new Hashtable();
                HTop.Clear();
                ddlop.Items.Clear();
                dt.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                dt = objDAL.GetDataTable("PRC_FILL_OPRTR_DRPDWN", HTop);
                if (dt.Rows.Count > 0)
                {
                    ddlop.DataSource = dt;
                    ddlop.DataTextField = "ParamDesc1";
                    ddlop.DataValueField = "ParamValue";
                    ddlop.DataBind();
                }
                // drRead.Close();
                ddlop.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion

        #region Fill Whr cndn Tabl drpdwn
        protected void FillTable(string DATA_PREP)
        {
            try
            {
                Hashtable HTtb = new Hashtable();
                HTtb.Clear();
                ddltb.Items.Clear();
                dt.Clear();
                //  HTtb.Add("@FLAG", Flag.ToString());
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                HTtb.Add("@DATA_PRPRTN_ID", DATA_PREP.ToString());
                dt = objDAL.GetDataTable("PRC_FILL_TBLLIST_INTGRTN_WHR_CNDN_DATA_PRPR", HTtb);
                if (dt.Rows.Count > 0)
                {
                    ddltb.DataSource = dt;
                    ddltb.DataTextField = "paramdesc";
                    ddltb.DataValueField = "paramval";
                    ddltb.DataBind();
                }
                //   drRead.Close();
                ddltb.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion

        #region ColumName drpdwn of DefWhrCndn
        protected void FillDWC_TblCol(DropDownList ddl, string Data_PrepID, string TBL_NAME)
        {
            try
            {
                Hashtable HTwc = new Hashtable();
                HTwc.Clear();
                ddl.Items.Clear();
                dt.Clear();
                objDAL = new DataAccessLayer("INTGRTN_WORKConnectionString");
                HTwc.Add("@DATA_PRPRTN_ID", Data_PrepID);
                HTwc.Add("@TBL_NAME", TBL_NAME.ToString());
                //  drRead = objDal.Common_exec_reader_prc_SAIM("PRC_FILL_TBLCOL_WHR_CND_DATA_PRPR", HTwc);
                dt = objDAL.GetDataTable("PRC_FILL_TBLCOL_WHR_CND_DATA_PRPR", HTwc);
                if (dt.Rows.Count > 0)
                {
                    ddl.DataSource = dt;
                    ddl.DataTextField = "paramdesc";
                    ddl.DataValueField = "paramval";
                    ddl.DataBind();
                }
                // drRead.Close();
                ddl.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion

        #region WhrCnd Status Dropdown
        protected void FillWhrCnd_Status()
        {
            try
            {
                Hashtable HTwh = new Hashtable();
                HTwh.Clear();
                dt.Clear();
                ddlwhstat.Items.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                HTwh.Add("@FLAG", "S");
                dt = objDAL.GetDataTable("PRC_GET_PREP_ID", HTwh);
                if (dt.Rows.Count > 0)
                {
                    ddlwhstat.DataSource = dt;
                    ddlwhstat.DataTextField = "ParamDesc1";
                    ddlwhstat.DataValueField = "ParamValue";
                    ddlwhstat.DataBind();
                }
                // drRead.Close();
                ddlwhstat.Items.Insert(0, new ListItem("-- SELECT --", ""));
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion

        protected void ddltb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string DATA_PREP_ID, flag = string.Empty;
            //  string flag = "SRC";
            DATA_PREP_ID = ViewState["hdndataprepid"].ToString();

            FillDWC_TblCol(ddlcn, DATA_PREP_ID, ddltb.SelectedValue.ToString());

        }

        #region Save Define_Where_cond Data
        protected void btndwcsave_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet DsDWC = new DataSet();
                DsDWC.Clear();
                Hashtable hTDWC = new Hashtable();
                hTDWC.Clear();
                string msgs = string.Empty;
                // hTDWC.Add("@SET_TBL", ddlsettbl.SelectedValue.ToString());
                hTDWC.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                hTDWC.Add("@DATA_PRPRTN_ID", ViewState["hdndataprepid"].ToString());
                hTDWC.Add("@TBL_NAME", ddltb.SelectedValue.ToString());
                hTDWC.Add("@WHR_CNDTN_COL_NAME", ddlcn.SelectedValue.ToString());
                hTDWC.Add("@WHR_CNDTN_OPRT", ddlop.SelectedValue.ToString().Trim());
                if (txtwhrcolval.Text=="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Column Value');", true);
                    return;
                }
                else{
                hTDWC.Add("@WHR_CNDTN_COL_VAL", string.Format(txtwhrcolval.Text.ToString().Trim()));
                }
                hTDWC.Add("@STATUS", ddlwhstat.SelectedValue.ToString().Trim());
                hTDWC.Add("@SEQNO", "");
                if (txtefrm.Text == "")
                {
                    hTDWC.Add("@EFF_FRM_DT", System.DBNull.Value);
                }
                else
                {
                    hTDWC.Add("@EFF_FRM_DT", Convert.ToDateTime(txtefrm.Text.ToString().Trim()));
                }
                if (txtcsdt.Text == "")
                {
                    hTDWC.Add("@CEASE_DT", System.DBNull.Value);
                }
                else
                {
                    hTDWC.Add("@CEASE_DT", Convert.ToDateTime(txtcsdt.Text.ToString().Trim()));
                }

                hTDWC.Add("@CREATED_BY", Session["UserID"].ToString().Trim());
                hTDWC.Add("@Flag", "SAVW");
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                DsDWC = objDAL.GetDataSet("PRC_INS_MST_KPI_INTGRTN_DATA_PRPR_WHR_CNDTN", hTDWC);
                if (DsDWC.Tables.Count > 0 && DsDWC.Tables[0].Rows.Count > 0)
                {
                    msgs = DsDWC.Tables[0].Rows[0]["MSG"].ToString().Trim();
                    if (msgs == "FAILED")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Record Already exists..!!');", true);
                        return;
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Data Saved Successfully...!!!')", true);
                BindDefineWhrCndn();
                // FillDWC_TblCol(ddlcn, ddltb.SelectedValue.ToString());
                ddltb.SelectedValue = "";
                ddlcn.SelectedValue = "";
                ddlop.SelectedValue = ""; txtwhrcolval.Text = "";
                ddlwhstat.SelectedValue = "1";
            }

            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion

        #region Define_Whr_Cndn Gridview
        protected void BindDefineWhrCndn()
        {
            try
            {
                DataSet dsWC = new DataSet();
                dsWC.Clear();
                Hashtable htWC = new Hashtable();
                htWC.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htWC.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                htWC.Add("@DATA_PRPRTN_ID", ViewState["hdndataprepid"].ToString());
                dsWC = objDAL.GetDataSet("PRC_GET_MST_KPI_INTGRTN_DATA_PRPR_WHR_CNDTN", htWC);
                grddefwhcnd.DataSource = dsWC;
                grddefwhcnd.DataBind();


            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }

        }
        #endregion

        #region Data_Prep_Tab_edit_update
        protected void lnkdpedt_Click(object sender, EventArgs e)
        {
            try
            {
                ddlst.Enabled = true;
                btndataprepclr.Visible = false;
                btndataprep_upd.Visible = true;
                btndataprep.Visible = false;
               // btndataprep.Attributes.Add("disabled", "disabled");

                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lbldpid = row.FindControl("lbldpid") as Label;
                Label lbltb = row.FindControl("lbltb") as Label;
                Label lblsttban = row.FindControl("lblsttban") as Label;
                Label lblfrmtb = row.FindControl("lblfrmtb") as Label;
                Label Lblstatus1 = row.FindControl("Lblstatus1") as Label;
                Label lblefd = row.FindControl("lblefd") as Label;
                Label lblcgd = row.FindControl("lblcgd") as Label;
                HiddenField hdnsts1 = row.FindControl("hdnsts1") as HiddenField;
                Label lbldpseq = row.FindControl("lbldpseq") as Label;
                Label lblsfanvl = row.FindControl("lblsfanvl") as Label;
                HiddenField hdndpt = row.FindControl("hdndpt") as HiddenField;
                Label lblprcnm = row.FindControl("lblprcnm") as Label;


                ViewState["hdnDPREPSEQ"] = lbldpseq.Text;

                ddldpt.SelectedValue = hdndpt.Value.ToString();
                txtprcnme.Text = lblprcnm.Text;
                txtdprpId.Text = lbldpid.Text;
                ddlsettbl.SelectedValue = lbltb.Text;
                ddlfrmtbl.SelectedValue = lblfrmtb.Text;
                txtED.Text = Convert.ToDateTime(lblefd.Text.Trim()).ToString("dd/MM/yyyy");
                txtcd.Text = lblcgd.Text;
                ddlst.SelectedValue = Lblstatus1.Text;


                if (ddldpt.SelectedValue == "0")
                {
                    RDSETFRMANTHER.Enabled = false;
                    ddlsettbl.Enabled = false;
                    ddlfrmtbl.Enabled = false;
                    txtprcnme.Enabled = true;
                }
                else
                {
                    RDSETFRMANTHER.Enabled = true;
                    ddlsettbl.Enabled = true;
                    ddlfrmtbl.Enabled = true;
                    txtprcnme.Enabled = false;
                }
                RDSETFRMANTHER.SelectedValue = lblsfanvl.Text;
                if (lblsttban.Text == "" || lblsttban.Text == null)
                {
                    RDSETFRMANTHER.SelectedValue = "1";
                }
                else if (RDSETFRMANTHER.SelectedValue == "0")
                {
                    ddlfrmtbl.Enabled = false;
                }
                else
                {
                    ddlfrmtbl.Enabled = true;
                }


                // btndataprep_upd.Attributes.Add("style", "display:inline-block;");
                //txtcd.Enabled = true;
                // btndataprep.Visible = false;
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void btndataprep_upd_Click(object sender, EventArgs e)
        {
             try
        {
            txtcd.Enabled = true;
            Hashtable htUpd = new Hashtable();
            DataSet DsUpdate = new DataSet();
            DsUpdate.Clear();
            htUpd.Clear();
            string msgs = string.Empty;
            objDAL = new DataAccessLayer("INTGRTNConnectionString");
                 
            htUpd.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
            htUpd.Add("@DATA_PRPRTN_ID", txtdprpId.Text.ToString());
            if (ddldpt.SelectedValue == "0" && ddlsettbl.SelectedItem.Text == "--SELECT--")
            {
                htUpd.Add("@SET_TBL", System.DBNull.Value);
            }
            else if (ddldpt.SelectedValue == "1" && ddlsettbl.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Set Table Field');", true);
                return;
            }
            else
            {
                htUpd.Add("@SET_TBL", ddlsettbl.SelectedValue.ToString());
            }
            if (ddldpt.SelectedValue == "1" && RDSETFRMANTHER.SelectedItem.Text == "Yes")
            {
                htUpd.Add("@SET_FRM_ANTHR_TBL", 1);
            }
            else if (ddldpt.SelectedValue == "1" && RDSETFRMANTHER.SelectedItem.Text == "No")
            {
                htUpd.Add("@SET_FRM_ANTHR_TBL", 0);
            }
            else //(ddldpt.SelectedValue == "0" && RDSETFRMANTHER.SelectedItem.Text == "Yes")
            {
                htUpd.Add("@SET_FRM_ANTHR_TBL", System.DBNull.Value);
            }    
        
            if (ddldpt.SelectedValue == "0" && ddlfrmtbl.SelectedIndex == 0)
            {
                htUpd.Add("@FRM_TBL", System.DBNull.Value);
            }
                else if(ddldpt.SelectedValue == "1" && RDSETFRMANTHER.SelectedItem.Text == "Yes" && ddlfrmtbl.SelectedIndex==0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select From Table Field');", true);
                return;
            }
            else
            {
                htUpd.Add("@FRM_TBL", ddlfrmtbl.SelectedValue.ToString());
            }
            htUpd.Add("@DATA_PRPR_TYPE", ddldpt.SelectedValue.ToString());
            if (txtprcnme.Text == "" && ddldpt.SelectedValue == "1")
            {
                htUpd.Add("@PRCSR_NAME", System.DBNull.Value);
            }
            else if (txtprcnme.Text == "" && ddldpt.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Processor Name');", true);
                return;
            }

            else
            {
                htUpd.Add("@PRCSR_NAME", txtprcnme.Text.ToString());
            }
            if (ddlst.SelectedValue == "" || ddlst.SelectedValue == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Status');", true);
                return;
            }
            else
            {
                htUpd.Add("@STATUS", ddlst.SelectedValue);
            }

            if (txtcd.Text == ""  && ddlst.SelectedValue == "1")
            {
                htUpd.Add("@CEASE_DT", System.DBNull.Value);
                           }
            else if (txtcd.Text == ""  && ddlst.SelectedValue == "0")
            {
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Cease Date');", true);
                return; 
            }
           
            else
            {
                htUpd.Add("@CEASE_DT", Convert.ToDateTime(txtcd.Text.ToString().Trim()));
            }

            htUpd.Add("@CREATED_BY", Session["UserID"].ToString().Trim());
            htUpd.Add("@SEQNO", ViewState["hdnDPREPSEQ"].ToString());
            htUpd.Add("@FLAG", "UP");
                
            DsUpdate = objDAL.GetDataSet("PRC_INS_MST_KPI_INTGRTN_DATA_PRPR", htUpd);
            if (DsUpdate.Tables.Count > 0 && DsUpdate.Tables[0].Rows.Count > 0)
            {
                msgs = DsUpdate.Tables[0].Rows[0]["MSG"].ToString().Trim();
                if (msgs == "FAILED")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Record Already exists..!!');", true);
                    // return;
                    // ddlsettbl.SelectedValue = "";
                    //ddlfrmtbl.SelectedValue = "";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Updated Successfully...!!!')", true);
                }
            }
            BindDataPreparation();
            btndataprepclr.Visible = true;
            ddlsettbl.SelectedValue = "";
            ddlfrmtbl.SelectedValue = "";
            txtcd.Text = "";
            btndataprep_upd.Visible = false;
           btndataprep.Visible = true;
            //btndataprep.Enabled = true;
            //btndataprep.Attributes.Add("Enabled", "Enabled");
            FillDataPrepTextbox();
            ddlst.SelectedIndex = 1;
            ddlst.Enabled = false;
            txtcd.Enabled = false;
            RDSETFRMANTHER.SelectedValue = "1";
            ddlfrmtbl.Enabled = true;
            ddldpt.SelectedValue = "";
            txtprcnme.Text = "";
            ddlsettbl.Enabled = true;
            RDSETFRMANTHER.Enabled = true;

        }
            catch(Exception ex)
             {
             string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
             System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
             objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        #endregion

        protected void lnkdpdel_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable htdel = new Hashtable();
                htdel.Clear();
                DataSet dsdel = new DataSet();
                dsdel.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                LinkButton lnkbtnD = sender as LinkButton;
                GridViewRow gvrow = lnkbtnD.NamingContainer as GridViewRow;
                Label lbldpid = (Label)gvrow.FindControl("lbldpid");
                Label lbltb = (Label)gvrow.FindControl("lbltb");
                Label lbldpseq = (Label)gvrow.FindControl("lbldpseq");
                int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

                htdel.Add("@DATA_PRPRTN_ID", lbldpid.Text);
                htdel.Add("@SET_TABL", lbltb.Text);
                htdel.Add("@Flag", "DP");
                htdel.Add("@SEQNO", lbldpseq.Text);
                dsdel = objDAL.GetDataSet("PRC_DEL_MST_KPI_INTGRTN_DATA_PRPR", htdel);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Deleted Successfully...!!!')", true);
                BindDataPreparation();

            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        #region EDIT, Update ,Delete of Set Tbl Col tab
        protected void lnkedstc_Click(object sender, EventArgs e)
        {
             try
        {
            string anthertbl = string.Empty;
            string data_prep_ID = ViewState["hdndataprepid"].ToString();
            anthertbl = ViewState["hdnstfrmanthr"].ToString();
            ddlsttblcol.Enabled = false;

            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lbltbcol = row.FindControl("lbltbcol") as Label;
            Label lblstvl = row.FindControl("lblstvl") as Label;
            Label lbl2st = row.FindControl("lbl2st") as Label;
            Label lbl2efd = row.FindControl("lbl2efd") as Label;
            Label lbl2cd = row.FindControl("lbl2cd") as Label;
            Label Lblstatus2 = row.FindControl("Lblstatus2") as Label;
            Label lblstseq = row.FindControl("lblstseq") as Label;
            Label lblSTid = row.FindControl("lblSTid") as Label;
            Label LBLSETVALFRM = row.FindControl("LBLSETVALFRM") as Label;
            ViewState["hdnSTTBLSEQNO"] = lblstseq.Text;

            FillSettblcol(lblSTid.Text, "");
            ddlsttblcol.SelectedValue = lbltbcol.Text;
            ddlsvf.SelectedValue = LBLSETVALFRM.Text;
            if (ddlsvf.SelectedValue.ToString() == "SRC")
            {
                FillValSetCol(lblSTid.Text, ddlsvf.SelectedValue.ToString());
                txtval.Visible = false;
                lblvl.Visible = false;
                txtval.Text = "";
            }
            else if (ddlsvf.SelectedValue.ToString() == "SYN")
            {
                FillValSetCol(lblSTid.Text, "");
                txtval.Visible = false;
                lblvl.Visible = false;
                txtval.Text = "";
            }
            else
            {
                ddlvalstcol.Enabled = false;
                txtval.Visible = true;
                lblvl.Visible = true;
                Label lblVALUE = row.FindControl("lblVALUE") as Label;
                txtval.Text = lblVALUE.Text;
            }
         
            ddlvalstcol.SelectedValue = lblstvl.Text;
           
            ddlsest.SelectedValue =      Lblstatus2.Text;
            txtCED.Text = lbl2cd.Text;
            txtEF.Text = lbl2efd.Text;
           

          
           // btnsetadd.Visible = false;
          //  btnsetclr.Visible = false;
            txtEF.Enabled = false;
            
            btnsetupd.Visible = true;
            ddlsest.Enabled = true;
            //txtCED.Enabled = true;
            btnsetclr.Attributes.Add("style", "display:none;");
            btnsetadd.Attributes.Add("style", "display:none;");
           // btnsetadd.Attributes.Add("disabled", "disabled");
            btnsetupd.Attributes.Add("style", "display:inline-block;");
            data_prep_ID = "";
              }
             catch (Exception ex)
             {
                 string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                 System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                 objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
             }
        }

        protected void lnksttbldel_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lbltbcol = row.FindControl("lbltbcol") as Label;
                Label lblSTid = row.FindControl("lblSTid") as Label;
                Label lblstseq = row.FindControl("lblstseq") as Label;
                htParam.Clear();
                DS.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@DATA_PRPRTN_ID", lblSTid.Text);
                htParam.Add("@SET_TABL", lbltbcol.Text);
                htParam.Add("@Flag", "ST");
                htParam.Add("@SEQNO", lblstseq.Text);

                DS = objDAL.GetDataSet("PRC_DEL_MST_KPI_INTGRTN_DATA_PRPR", htParam);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Deleted Successfully...!!!')", true);
                BindSetTblColumn();
                FillSettblcol(lblSTid.Text, "ST");
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        
        protected void btnsetupd_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet DsUpdate = new DataSet();
                Hashtable htUpd = new Hashtable();
                DsUpdate.Clear();
                htUpd.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                string anthertbl, msgs = string.Empty;
                anthertbl = ViewState["hdnstfrmanthr"].ToString();
                if ("SET_TBL_COL" != null || "SET_TBL_COL" != "")
                {
                    htUpd.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                    htUpd.Add("@DATA_PRPRTN_ID", ViewState["hdndataprepid"].ToString().Trim());
                    htUpd.Add("@SET_VAL_FRM", ddlsvf.SelectedValue.ToString().Trim());
                    if (ddlsttblcol.SelectedItem.Text == "-- SELECT --")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Set Table Column');", true);
                        return;
                    }
                    else
                    {
                        htUpd.Add("@SET_TBL_COL", ddlsttblcol.SelectedItem.Text);
                    }
                    if (anthertbl == "1" && ddlsvf.SelectedValue.ToString() == "SRC" && ddlvalstcol.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Value Set Column');", true);
                        return;
                    }
                    else if (anthertbl == "1" && ddlsvf.SelectedValue.ToString() == "SYN" && ddlvalstcol.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Value Set Column');", true);
                        return;
                    }
                    else
                    {
                        htUpd.Add("@VAL_SET_COL", ddlvalstcol.SelectedValue);
                    }
                    if (ddlsest.SelectedValue == null || ddlsest.SelectedValue == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Status');", true);
                        return;
                    }
                    else
                    {
                        htUpd.Add("@STATUS", ddlsest.SelectedValue.ToString());
                    }
                    if (ddlsvf.SelectedValue.ToString() == "SRC" || ddlsvf.SelectedValue.ToString() == "SYN" && txtval.Text == "")
                    {
                        htUpd.Add("@VALUE", System.DBNull.Value);
                    }
                    else if (ddlsvf.SelectedValue.ToString() == "M" && txtval.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Value');", true);
                        return;
                    }
                    else
                    {
                        htUpd.Add("@VALUE", txtval.Text.ToString().Trim());
                    }
                    if (txtCED.Text == "" && ddlsest.SelectedValue == "1")
                    {
                        htUpd.Add("@CEASE_DT", System.DBNull.Value);
                    }
                    else if (txtCED.Text == "" && ddlsest.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Cease Date');", true);
                        return;
                    }
                    else
                    {
                        htUpd.Add("@CEASE_DT", Convert.ToDateTime(txtCED.Text.ToString().Trim()));
                    }
                    htUpd.Add("@EFF_FRM_DT", Convert.ToDateTime(txtEF.Text.ToString().Trim()));
                    htUpd.Add("@CREATED_BY", Session["UserID"].ToString().Trim());
                    htUpd.Add("@Flag", "UPD");
                    htUpd.Add("@SEQNO", ViewState["hdnSTTBLSEQNO"].ToString());
                    DsUpdate = objDAL.GetDataSet("PRC_INS_MST_KPI_INTGRTN_DATA_PRPR_SET_COL", htUpd);
                    if (DsUpdate.Tables.Count > 0 && DsUpdate.Tables[0].Rows.Count > 0)
                    {
                        msgs = DsUpdate.Tables[0].Rows[0]["MSG"].ToString().Trim();
                        if (msgs == "FAILED")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Record Already exists..!!');", true);
                            // return;
                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Updated Successfully...!!!')", true);
                        }

                    }

                    BindSetTblColumn();
                    FillSettblcol(ViewState["hdndataprepid"].ToString(), "ST");

                    ddlsttblcol.SelectedItem.Text = "--SELECT--";
                    ddlvalstcol.SelectedIndex = 0;
                    ddlsest.SelectedValue = "1";
                    btnsetupd.Attributes.Add("style", "display:none;");
                    btnsetadd.Attributes.Add("style", "display:inline-block;");
                   // btnsetadd.Attributes.Add("enabled", "enabled");
                    btnsetclr.Attributes.Add("style", "display:inline-block;");
                    txtCED.Text = "";
                    txtCED.Enabled = false;
                    ddlsttblcol.Enabled = true;
                    txtCED.Enabled = false;
                    ddlsest.Enabled = false;
                    txtval.Visible = false;
                    lblvl.Visible = false;
                    ddlsvf.SelectedIndex = 0;
                    txtval.Text = "";
                    txtval.Visible = false;
                    lblvl.Visible = false;
                    ViewState["hdnstfrmanthr"] = "";

                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
           }
        }
        #endregion

        #region Define Join Edit , Update , Delete tab
        protected void BtnJ_Upd_Click(object sender, EventArgs e)
        {
            try
        {
            string msgs,status = string.Empty;
            DataSet DsUpdate = new DataSet();
            Hashtable htUpd = new Hashtable();
            DsUpdate.Clear();
            htUpd.Clear();
            objDAL = new DataAccessLayer("INTGRTNConnectionString");
           // txtces.Enabled = true;

           // htUpd.Add("@JN_ID", hdnJnd.Value);
           // htUpd.Add("@SET_TBL", ddlsettbl.SelectedValue.ToString());
            htUpd.Add("@DATA_PRPRTN_ID", ViewState["hdndataprepid"].ToString().Trim());
           htUpd.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString());
           if (ddlsttbcol.SelectedValue==null || ddlsttbcol.SelectedValue=="")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Set Table Col');", true);
                return;
           }
            else{
                htUpd.Add("@TBL_1_COL", ddlsttbcol.SelectedValue);
            }
           if (ddlfrmtbcol.SelectedValue == "" || ddlfrmtbcol.SelectedValue ==null)
           {
               ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select From Table Col');", true);
                return;
           }
           else
           {
               htUpd.Add("@TBL_2_COL", ddlfrmtbcol.SelectedValue);
           }
            if (ddljstat.SelectedValue == "" || ddljstat.SelectedValue==null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Status');", true);
                return;
            }
            else{
            htUpd.Add("@STATUS", ddljstat.SelectedValue.ToString());
                      }


            if (txtces.Text == ""  && ddljstat.SelectedValue=="1" )
            {
                htUpd.Add("@CEASE_DT", System.DBNull.Value);
            }
            else if (txtces.Text == "" && ddljstat.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Cease Date');", true);
                return;
            }
            else
            {
                htUpd.Add("@CEASE_DT", Convert.ToDateTime(txtces.Text.ToString().Trim()));
            }
            htUpd.Add("@JN_ID", ViewState["hdnDJ_JNID"].ToString());
            htUpd.Add("@CREATED_BY", Session["UserID"].ToString().Trim());
            htUpd.Add("@Flag", "UP");
            htUpd.Add("@SEQNO", ViewState["hdnDJSEQ"].ToString());
            DsUpdate = objDAL.GetDataSet("PRC_INS_MST_KPI_INTGRTN_DATA_PRPR_JOIN_DTLS", htUpd);
            if (DsUpdate.Tables.Count > 0 && DsUpdate.Tables[0].Rows.Count > 0)
            {
                msgs = DsUpdate.Tables[0].Rows[0]["MSG"].ToString().Trim();
                if (msgs == "FAILED")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Record Already exists..!!');", true);
                    // return;
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Updated Successfully...!!!')", true);
                }

            }
            BindDefineJoin();
          
            ddlsttbcol.SelectedItem.Text = "--SELECT--";
           // ddlfrmtbcol.SelectedItem.Text = "--SELECT--";
           // ddlsttbcol.SelectedValue = "";
            ddlfrmtbcol.SelectedValue = "";
            ddljstat.SelectedValue = "1";
            BtnJ_Upd.Visible = false;
            btnAdd.Visible = true;
            lnkclrj.Visible = true;
            txtces.Text = "";
            txtces.Enabled = false;
            ddlsttbcol.Enabled = true;
            ddljstat.Enabled = false;
            FillSettblcolmn(ViewState["hdndataprepid"].ToString(),"DJ");
          // FillFromTblCol(ViewState["hdndataprepid"].ToString());
          //  ViewState["hdndataprepid"]= "";
            ViewState["hdnDJSEQ"] = "";
            ViewState["hdnDJ_JNID"] = "";
        }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void lnkeddj_Click(object sender, EventArgs e)
        {
            try
        {
            btnAdd.Visible = false;
            lnkclrj.Visible = false;
            txteff.Enabled = false;
          //  txtces.Enabled = true;
            BtnJ_Upd.Visible = true;
           ddlsttbcol.Enabled = false;
            ddljstat.Enabled = true;
            LinkButton button = (LinkButton)sender;
            string TBL1_COL = (string)button.Attributes["data-myData"];


            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lblftc = row.FindControl("lblftc") as Label;
            Label lblstd = row.FindControl("lblstd") as Label;
            Label lbl3st = row.FindControl("lbl3st") as Label;
            Label lbl3efd = row.FindControl("lbl3efd") as Label;
            Label lbl2cd = row.FindControl("lbl3cd") as Label;
            Label Lblstatus3 = row.FindControl("Lblstatus3") as Label;
            Label lbldjseq = row.FindControl("lbldjseq") as Label;
            Label LBLDJJD = row.FindControl("LBLDJJD") as Label;
            Label lbldjdp = row.FindControl("lbldjdp") as Label;
            ViewState["hdnDJSEQ"] = lbldjseq.Text;
            ViewState["hdnDJ_JNID"] = LBLDJJD.Text;
            FillSettblcolmn(lbldjdp.Text, "");
           // ddlsttbcol.SelectedItem.Text = lblstd.Text;
           ddlsttbcol.SelectedValue = lblstd.Text;
        
            ddlfrmtbcol.SelectedValue = lblftc.Text;
            ddljstat.SelectedValue = Lblstatus3.Text;
          txteff.Text = lbl3efd.Text;
          txtces.Text = lbl2cd.Text;
        }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void lnkDefjndel_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                Label LBLDJJD = row.FindControl("LBLDJJD") as Label;
                Label lblstd = row.FindControl("lblstd") as Label;
                Label lbldjdp = row.FindControl("lbldjdp") as Label;
                Label lbldjseq = row.FindControl("lbldjseq") as Label;
                htParam.Clear();
                DS.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@DATA_PRPRTN_ID", LBLDJJD.Text);
                htParam.Add("@SET_TABL", lblstd.Text);
                htParam.Add("@SEQNO", lbldjseq.Text);
                htParam.Add("@Flag", "DJ");

                DS = objDAL.GetDataSet("PRC_DEL_MST_KPI_INTGRTN_DATA_PRPR", htParam);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Deleted Successfully...!!!')", true);
                BindDefineJoin();
                FillSettblcolmn(lbldjdp.Text.ToString(), "DJ");
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        #endregion

        #region Edit , Update , Delete of Where Cond tab
        protected void lnkedt_Click(object sender, EventArgs e)
        {
            try
        {
            string flag = string.Empty;
             string   DATA_PREP_ID = string.Empty;
            //btndwcsave.Visible = false;
            //btndwcClr.Visible = false;
            btnwcupdte.Visible = true;
           // txtcsdt.Enabled = true;
            //ddltb.Enabled = false;
            //ddlcn.Enabled = false;
            ddlwhstat.Enabled = true;
           
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lblwctbnm = row.FindControl("lblwctbnm") as Label;
            Label lblwccol = row.FindControl("lblwccol") as Label;
            Label lblwcop = row.FindControl("lblwcop") as Label;
            Label lblwccv = row.FindControl("lblwccv") as Label;
            Label lbl4st = row.FindControl("lbl4st") as Label;
            Label lbl4efd = row.FindControl("lbl4efd") as Label;
            Label lbl4cd = row.FindControl("lbl4cd") as Label;
            Label Lblstatus4 = row.FindControl("Lblstatus4") as Label;
            Label lblwcseq = row.FindControl("lblwcseq") as Label;

            ViewState["hdnWCSEQ"] = lblwcseq.Text;

            ddlwhstat.SelectedValue = Lblstatus4.Text;
            ddltb.SelectedValue = lblwctbnm.Text;
          //  FillDWC_TblCol(ddlcn,  ddltb.SelectedValue.ToString());
         //  FillDWC_TblCol(ddlcn, ddltb.SelectedItem.Text.ToString());
            DATA_PREP_ID = ViewState["hdndataprepid"].ToString();
            
            FillDWC_TblCol(ddlcn,  DATA_PREP_ID,ddltb.SelectedValue.ToString());
                              
          
            ddlcn.SelectedValue = lblwccol.Text;
                ddlop.SelectedValue=lblwcop.Text;
                     txtwhrcolval.Text=lblwccv.Text;
                     txtefrm.Text = lbl4efd.Text;
                     txtcsdt.Text = lbl4cd.Text;
                 //    ViewState["hdndataprepid"] = "";
                     //btnwcupdte.Attributes.Add("style", "display:inline-block;");
                     btndwcsave.Attributes.Add("style", "display:none;");
                     btndwcClr.Attributes.Add("style", "display:none;");
                     btnwcupdte.Attributes.Add("style", "display:inline-block;");
                     
        }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void lnkwhrdel_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblwhrdp = row.FindControl("lblwhrdp") as Label;
                Label lblwccol = row.FindControl("lblwccol") as Label;
                Label lblwcseq = row.FindControl("lblwcseq") as Label;
                htParam.Clear();
                DS.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@DATA_PRPRTN_ID", lblwhrdp.Text);
                htParam.Add("@SET_TABL", lblwccol.Text);
                htParam.Add("@SEQNO", lblwcseq.Text);
                htParam.Add("@Flag", "DW");

                DS = objDAL.GetDataSet("PRC_DEL_MST_KPI_INTGRTN_DATA_PRPR", htParam);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Deleted Successfully...!!!')", true);
                BindDefineWhrCndn();
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void btnwcupdte_Click(object sender, EventArgs e)
        {
             try
        {
            DataSet DsUpdate = new DataSet();
            Hashtable htUpd = new Hashtable();
            DsUpdate.Clear();
            htUpd.Clear(); txtcsdt.Enabled = true;
            string msgs = string.Empty;

            objDAL = new DataAccessLayer("INTGRTNConnectionString");
            string DATA_PREP = ViewState["hdndataprepid"].ToString();
           // htUpd.Add("@SET_TBL", ddlsettbl.SelectedValue.ToString());
            htUpd.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
            htUpd.Add("@DATA_PRPRTN_ID", DATA_PREP.ToString());
            htUpd.Add("@WHR_CNDTN_COL_VAL", txtwhrcolval.Text);
            htUpd.Add("@TBL_NAME", ddltb.SelectedItem.Text);
            htUpd.Add("@WHR_CNDTN_COL_NAME", ddlcn.SelectedItem.Text);
            if (ddlwhstat.SelectedValue == null || ddlwhstat.SelectedValue=="")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Status');", true);
                return;
            }
            else{
            htUpd.Add("@STATUS", ddlwhstat.SelectedValue.ToString());
            }
            htUpd.Add("@WHR_CNDTN_OPRT", ddlop.SelectedValue.ToString());
            if (txtcsdt.Text == "" && ddlwhstat.SelectedValue=="1")
            { htUpd.Add("@CEASE_DT",System.DBNull.Value);}
            else if (txtcsdt.Text == "" && ddlwhstat.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Cease Date');", true);
                return; 
            }
            else
            {
                htUpd.Add("@CEASE_DT", Convert.ToDateTime(txtcsdt.Text.ToString().Trim()));
            }
            htUpd.Add("@CREATED_BY", Session["UserID"].ToString().Trim());
            htUpd.Add("@Flag", "UPDW");
            htUpd.Add("@SEQNO", ViewState["hdnWCSEQ"].ToString());
            DsUpdate = objDAL.GetDataSet("PRC_INS_MST_KPI_INTGRTN_DATA_PRPR_WHR_CNDTN", htUpd);
            if (DsUpdate.Tables.Count > 0 && DsUpdate.Tables[0].Rows.Count > 0)
            {
                msgs = DsUpdate.Tables[0].Rows[0]["MSG"].ToString().Trim();
                if (msgs == "FAILED")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Record Already exists..!!');", true);
                    //return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Updated Successfully...!!!')", true);
                }
            }
           
          
            BindDefineWhrCndn();
            ddltb.SelectedItem.Text = "--SELECT--";
            ddlcn.SelectedItem.Text = "--SELECT--";

            ddlwhstat.SelectedValue = "1";
            ddlop.SelectedValue = "";
           // btnwcupdte.Visible = false;
            txtwhrcolval.Text = "";
            //btndwcsave.Visible = true;
          //  btndwcClr.Visible = true;
            txtcsdt.Text = "";
            txtcsdt.Enabled = false;
            ddltb.Enabled = true;
            ddlcn.Enabled = true;
            ddlwhstat.Enabled = false;
          
            FillDWC_TblCol(ddlcn, DATA_PREP, ddltb.SelectedValue.ToString());
            FillTable( DATA_PREP.ToString());
           // FillTable("FT", DATA_PREP.ToString());
            btnwcupdte.Attributes.Add("style", "display:none;");
            btndwcsave.Attributes.Add("style", "display:inline-block;");
            btndwcClr.Attributes.Add("style", "display:inline-block;");
            //ViewState["hdndataprepid"] = "";
            ViewState["hdnWCSEQ"] = "";
        }
             catch (Exception ex)
             {
                 string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                 System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                 objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
             }
        }

        #endregion

        protected void ddlsest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsest.SelectedValue == "0")
            {
                txtCED.Enabled = true;
                spancsdt2.Visible = true;
            }
            else
            {
                txtCED.Enabled = false;
                txtCED.Text = "";
                spancsdt2.Visible = false;
            }
        }
        protected void ddlst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlst.SelectedValue == "0")
            {
                Spancsdt1.Visible = true;
                txtcd.Enabled = true;
            }
            else
            {
                txtcd.Enabled = false;
                txtcd.Text = "";
                Spancsdt1.Visible = false;
            }
        }
        protected void ddljstat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddljstat.SelectedValue == "0")
                {
                    txtces.Enabled = true;
                    spancsdt3.Visible = true;
                }
                else
                {
                    txtces.Text = "";
                    txtces.Enabled = false;
                    spancsdt3.Visible = false;
                }
            }
            
                 catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
           }
        }
        protected void ddlwhstat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlwhstat.SelectedValue == "0")
            {
                txtcsdt.Enabled = true;
                spncsdtwhrc.Visible = true;
            }
            else
            {
                txtcsdt.Enabled = false;
                txtcsdt.Text = "";
                spncsdtwhrc.Visible = false;
            }
        }

        protected void btndwcClr_Click(object sender, EventArgs e)
        {
            //  ddlwhstat.SelectedValue = "";
            txtcsdt.Text = "";
            //  txtefrm.Text = "";
            txtwhrcolval.Text = "";
            ddlop.SelectedValue = "";
            ddlcn.SelectedValue = "";
            ddltb.SelectedValue = "";
        }

        protected void btndataprepclr_Click(object sender, EventArgs e)
        {
            ddlsettbl.SelectedValue = "";
            ddlfrmtbl.SelectedValue = "";
            ddldpt.SelectedIndex = 0;
            txtprcnme.Text = "";
            txtcd.Text = "";
        }

        protected void btnsetclr_Click(object sender, EventArgs e)
        {
            ddlvalstcol.SelectedIndex = 0;
            ddlsttblcol.SelectedIndex = 0;
            // ddlsest.SelectedIndex = 0;
            txtCED.Text = "";
            ddlsvf.SelectedIndex = 0;
            txtval.Text = "";
            txtval.Visible = false;
            lblvl.Visible = false;
        }

        protected void lnkclrj_Click(object sender, EventArgs e)
        {
            // ddljstat.SelectedValue = "";
            ddlsttbcol.SelectedIndex = 0;
            txtces.Text = "";
            //txteff.Text = "";
            ddlfrmtbcol.SelectedValue = "";
        }
    }
}