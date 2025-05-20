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
    public partial class IntgrtnSrcTblLogic : System.Web.UI.Page
    {
        CommonUtility cu = new CommonUtility();
        ErrorLog objErr = new ErrorLog();
        int AppId = 0;
        SqlDataReader sdr;
        DataAccessLayer objDAL = new DataAccessLayer();
        DataTable dt = new DataTable();
        Hashtable htParam = new Hashtable();
        string INTG_ID = string.Empty;
        DataSet DS = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString["INTG_ID"].ToString().Trim() != "")
                    {
                        INTG_ID = Request.QueryString["INTG_ID"].ToString().Trim();
                    }
                    bindINTSTFillUP(INTG_ID);
                    ddlSynmCol.Items.Insert(0, new ListItem("Select", ""));
                    bindgvINTSTFillUP();
                    bindDEFJOIN(INTG_ID);
                    bindgridDefJoin();
                    bindGridDefCol();
                    bindWhereCondtn(INTG_ID);
                    bindgvDwhereCondition();

                   
                    ddlDEFWCondColName.Items.Insert(0, new ListItem("Select", ""));

                    txtEffTo.Enabled = false;
                    txtDJEffTo.Enabled = false;
                    txtDCJoinEffTo.Enabled = false;
                    txtDEFWEffTo.Enabled = false;

                    txtEffFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDJEffFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDCJoinEffFrm.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDEFWEffFrm.Text = DateTime.Now.ToString("dd/MM/yyyy");


                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        public void bindINTSTFillUP(string INTG_ID)
        {
            try
            {
                htParam.Clear();
                DS.Clear();
                htParam.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                DS = objDAL.GetDataSet("Prc_GETMST_KPI_INTGRTN_SYNYM_SRC_MAP_SU_BsdOnINTGID", htParam);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    TextBoxSrcTbl.Text = DS.Tables[0].Rows[0]["SRC_TBL_ID"].ToString().Trim();
                    TextBoxSrcTbl.ToolTip = DS.Tables[0].Rows[0]["SRC_TBL_ID"].ToString().Trim();
                }
                else
                {
                    TextBoxSrcTbl.Text = "";
                    TextBoxSrcTbl.ToolTip = "";
                }
                FillddlSynm(ddlSynm, "synm", INTG_ID);
                Fillddl(ddlSTcol, "STCol", TextBoxSrcTbl.Text);
                Fillddl(ddlStatus, "STS", string.Empty);
                ddlStatus.SelectedIndex = 1;
                ddlStatus.Enabled = false;


            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void FillddlSynm(DropDownList ddl, string LookupCode, string INTG_ID)
        {
            try
            {
                ddl.Items.Clear();
                Hashtable ht = new Hashtable();
                htParam.Clear();
                DS.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                ht.Add("@INTGRTN_ID", INTG_ID);
                DS = objDAL.GetDataSet("Prc_GETMST_KPI_INTGRTN_SYNYM_SRC_MAP_SU_BsdOnINTGID", ht);
                if (DS.Tables[1].Rows.Count > 0)
                {
                    ddl.DataSource = DS.Tables[1];
                    ddl.DataTextField = "paramdesc";
                    ddl.DataValueField = "paramval";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("Select", ""));
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void Fillddl(DropDownList ddl, string LookupCode, string synNAME)
        {
            try
            {
                ddl.Items.Clear();
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                ht.Add("@LookupCode", LookupCode);
                ht.Add("@synmNAME", synNAME);
                if (LookupCode == "STCol")
                {
                    ht.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                }
                dt = objDAL.GetDataTable("Prc_GetINTSTFillUPddlVal", ht);
                if (dt.Rows.Count > 0)
                {
                    ddl.DataSource = dt;
                    ddl.DataTextField = "paramdesc";
                    ddl.DataValueField = "paramval";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("Select", ""));
                }
                else
                {
                    ddl.Items.Insert(0, new ListItem("Select", ""));
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void ddlSynm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSynm.SelectedIndex != 0)
            {
                ddlSTcol.Enabled = true;
            }
            else
            {
                ddlSTcol.SelectedIndex = 0;
                ddlSTcol.Enabled = false;
            }
        }

        protected void FillddlSAIMWRK1(DropDownList ddl, string LookupCode, string synNAME)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Clear();
                DS.Clear();
                ht.Add("@SrcTbl", TextBoxSrcTbl.Text);
                ht.Add("@SrcCol", ddlSTcol.SelectedValue.ToString().Trim());

               DataAccessLayer objDAL1 = new DataAccessLayer("INTGRTNConnectionString");
                DS = objDAL1.GetDataSet("Prc_FindDatatype", ht);
                string datatype = DS.Tables[0].Rows[0]["DATA_TYP_ID"].ToString().Trim();
                //if (datatype.ToString().Trim() == "40" || datatype.ToString().Trim() == "41" || datatype.ToString().Trim() == "42" || datatype.ToString().Trim() == "43"
                //    || datatype.ToString().Trim() == "58" || datatype.ToString().Trim() == "61")
                //{
                //    divbtnGrp1.Disabled = true;
                //}
                //else
                //{
                //    divbtnGrp1.Disabled = false;
                //}

                ht.Clear();
                ddl.Items.Clear();
               
                DataTable dt = new DataTable();
                ht.Clear();
                ht.Add("@LookupCode", LookupCode);
                ht.Add("@synmNAME", synNAME);
                 ht.Add("@dattyp", datatype.ToString().Trim());

                objDAL = new DataAccessLayer("INTGRTN_WORKConnectionString");
                dt = objDAL.GetDataTable("Prc_GetINTSTFillUPddlValSAIMWORK_1", ht);
                if (dt.Rows.Count>0)
                {
                    ddl.DataSource = dt;
                    ddl.DataTextField = "paramdesc";
                    ddl.DataValueField = "paramval";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("Select", ""));
                }
                else
                {
                    ddl.Items.Insert(0, new ListItem("Select", ""));
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void ddlSTcol_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSynmCol.Enabled = true;
            FillddlSAIMWRK1(ddlSynmCol, "SYNMCol", ddlSynm.SelectedValue.ToString().Trim());
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                htParam.Clear();
                DS.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                htParam.Add("@SRC_TBL", TextBoxSrcTbl.Text);
                htParam.Add("@SYNONYMS", ddlSynm.SelectedValue);
                htParam.Add("@SCR_TBL_COL", ddlSTcol.SelectedValue);
                if (ddlSynmCol.SelectedItem.Text == "Select")
                {
                    DataSet dsST = new DataSet();
                    Hashtable htST = new Hashtable();
                    htST.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                    htST.Add("@TBL_NAME", ddlSynm.SelectedValue);
                    htST.Add("@SRC_TBL_COL", ddlSTcol.SelectedValue);
                    htST.Add("@MODE", "S");
                    DataAccessLayer objDAL1 = new DataAccessLayer("INTGRTNConnectionString");
                    dsST = objDAL1.GetDataSet("Prc_CHK_FR_FORMULA_IN_MST_KPI_INTGRTN_SYNYM_SRC_COL_MAP_SU_WITH_PARAM", htST);

                    if (dsST.Tables.Count > 0 && dsST.Tables[0].Rows.Count > 0)
                    {
                        if (dsST.Tables[0].Rows[0]["Response"].ToString().Trim() == "0")
                        {
                            ddlSynmCol.Enabled = false;
                            htParam.Add("@SYNM_TBL_COL", "");
                        }
                        else if (dsST.Tables[0].Rows[0]["Response"].ToString().Trim() == "1")
                        {
                            ddlSynmCol.Enabled = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Synonym Column');", true);
                            return;
                        }
                    }
                }
                else
                {
                    htParam.Add("@SYNM_TBL_COL", ddlSynmCol.SelectedValue.ToString().Trim());
                }
                //htparam.Add("@SYNM_TBL_COL", ddlSynmCol.SelectedValue);
                htParam.Add("@CREATEDBY", HttpContext.Current.Session["UserID"].ToString().Trim());
                htParam.Add("@EFF_DTIM", txtEffFrom.Text);
                htParam.Add("@CSE_DTIM", txtEffTo.Text);
                htParam.Add("@STATUS", ddlStatus.SelectedValue);
                htParam.Add("@MODE", "S");
                htParam.Add("@SEQNO", hdnSTFULSEQNO.Value);
                DS = objDAL.GetDataSet("Prc_MST_KPI_INTGRTN_SYNYM_SRC_COL_MAP_SU", htParam);
                if (DS.Tables[0].Rows[0]["Response"].ToString().Trim() == "0")
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Data added successfully.');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data saved successfully.');", true);
                    btnClear_Click(EventArgs.Empty, EventArgs.Empty);
                    bindgvINTSTFillUP();
                    Fillddl(ddlSTcol, "STCol", TextBoxSrcTbl.Text);
                    ddlSTcol.SelectedIndex = 0;
                    ddlSTcol.Enabled = false;

                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Something went wrong');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data already exists');", true);
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        public void bindgvINTSTFillUP()
        {
            try
            {
                htParam.Clear();
                DS.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                DS = objDAL.GetDataSet("Prc_GETMST_KPI_INTGRTN_SYNYM_SRC_COL_MAP_SU", htParam);
                if (DS.Tables.Count > 0)
                {
                    gvINTSTFillUP.DataSource = DS;
                    gvINTSTFillUP.DataBind();
                    ViewState["grid"] = DS.Tables[0];
                    if (gvINTSTFillUP.PageCount > 1)
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
                    //ds = null;
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        public string getCTRNotxt(string ctrid)
        {
            try
            {
                htParam.Clear();
                DS.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@counterId", ctrid);
                DS = objDAL.GetDataSet("Prc_GetCTRNO", htParam);
                return DS.Tables[0].Rows.Count > 0 ? DS.Tables[0].Rows[0]["CTRNO"].ToString().Trim() : "";
            }
            catch (Exception ex)
            {
                return "";

                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }


        public void bindDEFJOIN(string INTG_ID)
        {
            try
            {
                txtIntGid.Text = INTG_ID;
                txtJId.Text = getCTRNotxt("Join_Id");
                FillddlSynm(ddlSynm1, "synm", INTG_ID);
                FillddlSynm(ddlSynm2, "synm", INTG_ID);
                Fillddl(ddlPrmJnt, "IPJ", string.Empty);
                Fillddl(ddlJoinType, "JTYP", string.Empty);
                Fillddl(ddlDJStatus, "STS", string.Empty);
                ddlDJStatus.SelectedIndex = 1;
                ddlDJStatus.Enabled = false;
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void btnDEFJOINSave_Click(object sender, EventArgs e)
        {
            try
            {
                htParam.Clear();
                DS.Clear();
                
                htParam.Add("@INTGRTN_ID", txtIntGid.Text);
                htParam.Add("@JN_ID", txtJId.Text);
                htParam.Add("@TABLE_1", ddlSynm1.SelectedValue);
                htParam.Add("@TABLE_2", ddlSynm2.SelectedValue);
                htParam.Add("@IS_PRIMARY_JOIN", ddlPrmJnt.SelectedValue);
                htParam.Add("@JN_TYP", ddlJoinType.SelectedValue);
                htParam.Add("@CREATEDBY", HttpContext.Current.Session["UserID"].ToString().Trim());
                htParam.Add("@EFF_DTIM", txtDJEffFrom.Text);
                htParam.Add("@CSE_DTIM", txtDJEffTo.Text);
                htParam.Add("@STATUS", ddlDJStatus.SelectedValue);
                htParam.Add("@MODE", "S");

                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                DS = objDAL.GetDataSet("Prc_MST_KPI_INTGRTN_CLT_SRC_JOIN_DTLS", htParam);
                if (Convert.ToInt32(DS.Tables[0].Rows[0]["Response"]) == 0)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Data added successfully.');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data saved successfully.');", true);
                    bindgridDefJoin();
                    htParam.Clear();
                    DS.Clear();
                    htParam.Add("@counterId", "Join_Id");
                    DS = objDAL.GetDataSet("Prc_UPDCtrNO", htParam);
                    txtJId.Text = getCTRNotxt("Join_Id");
                    ddlSynm1.SelectedIndex = 0;
                    ddlSynm2.SelectedIndex = 0;
                    ddlPrmJnt.SelectedIndex = 0;
                    ddlJoinType.SelectedIndex = 0;
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Something went wrong.');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data already exists.');", true);
                }
            }
            catch (Exception ex)
            {

                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }


        public void bindgridDefJoin()
        {
            try
            {
                htParam.Clear();
                DS.Clear();

                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                DS = objDAL.GetDataSet("Prc_GETMST_KPI_INTGRTN_CLT_SRC_JOIN_DTLS", htParam);
                if (DS.Tables.Count > 0)
                {
                    gridDefJoin.DataSource = DS;
                    gridDefJoin.DataBind();
                    ViewState["grid"] = DS.Tables[0];
                    if (gridDefJoin.PageCount > 1)
                    {
                       // btnGrdDEFJOINNext.Enabled = true;
                    }
                    else
                    {
                       // btnGrdDEFJOINNext.Enabled = false;
                    }
                }

                else
                {
                    //ds = null;
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void lnkGRIDDefJOINEdit_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grd = ((LinkButton)sender).NamingContainer as GridViewRow;
                Label lblJN_ID = (Label)grd.FindControl("lblJN_ID");
                Label lblTABLE_1 = (Label)grd.FindControl("lblTABLE_1");
                Label lblTABLE_2 = (Label)grd.FindControl("lblTABLE_2");

                bindDEFCOLFORJOIN(lblTABLE_1.Text, lblTABLE_2.Text, lblJN_ID.Text);
                ddlSValCol.Enabled = true;
                ddlDCFJSynmCol.Enabled = true;
                ddlDCFJSOpertr.Enabled = true;
                ddlDCFJColVal.Enabled = true;
                ddlSyn1Col.Enabled = false;
                ddlSyn2Col.Enabled = false;
                ddlDCJoinStatus.Enabled = false;
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void FillddlSAIMWRK(DropDownList ddl, string LookupCode, string synNAME)
        {
            try
            {
                ddl.Items.Clear();
                Hashtable ht = new Hashtable();
                dt.Clear();
                objDAL = new DataAccessLayer("INTGRTN_WORKConnectionString");
                ht.Add("@LookupCode", LookupCode);
                ht.Add("@synmNAME", synNAME);
                dt = objDAL.GetDataTable("Prc_GetINTSTFillUPddlValSAIMWORK", ht);
                if (dt.Rows.Count>0)
                {
                    ddl.DataSource = dt;
                    ddl.DataTextField = "paramdesc";
                    ddl.DataValueField = "paramval";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("Select", ""));
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        public void bindDEFCOLFORJOIN(string synm1, string synm2, string joinID)
        {
            try
            {
                FillddlSAIMWRK(ddlSyn1Col, "SYNMCol", synm1);
                FillddlSAIMWRK(ddlSyn2Col, "SYNMCol", synm2);
                Fillddl(ddlDCJoinStatus, "STS", string.Empty);
                ddlDCJoinStatus.SelectedIndex = 1;
                //Fillddl(ddlSyn1Col, "synm", string.Empty);
                //Fillddl(ddlSyn2Col, "synm", string.Empty);

                divDefColJoin.Attributes.Add("style", "display:block");
                ddlSValCol.Enabled = false;
                ddlDCFJSynmCol.Enabled = false;
                ddlDCFJSOpertr.Enabled = false;
                ddlDCFJColVal.Enabled = false;
                TextBox2.Text = joinID;
                TextBox1.Text = Request.QueryString["INTG_ID"].ToString().Trim();
               // bindGridDefCol();
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void lnkDefColJoinbtn_Click(object sender, EventArgs e)
        {
            try
            {
                htParam.Clear();
                DS.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@INTGRTN_ID", TextBox1.Text);
                htParam.Add("@JN_ID", TextBox2.Text);
                htParam.Add("@SET_VAL_AS_CLMN", rdbSVACol.SelectedValue);
                htParam.Add("@TBL_1_COL", ddlSyn1Col.SelectedValue);
                htParam.Add("@TBL_2_COL", ddlSyn2Col.SelectedValue);
                htParam.Add("@EFF_DTIM", txtDCJoinEffFrm.Text);
                htParam.Add("@CSE_DTIM", txtDCJoinEffTo.Text);
                htParam.Add("@STATUS", ddlDCJoinStatus.SelectedValue);
                htParam.Add("@MODE", "S");
                htParam.Add("@SEQNO", "");
                DS = objDAL.GetDataSet("Prc_MST_KPI_INTGRTN_CLT_SRC_JOIN_COL_DTLS", htParam);
                if (Convert.ToInt32(DS.Tables[0].Rows[0]["Response"]) == 0)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Data added successfully.');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Data saved successfully.');", true);
                    bindGridDefCol();
                    ddlSyn1Col.SelectedIndex = 0;
                    ddlSyn2Col.SelectedIndex = 0;
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Something went wrong.');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data already exists.');", true);
                }

            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        public void bindGridDefCol()
        {
            try
            {
                htParam.Clear();
                DS.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                DS = objDAL.GetDataSet("Prc_GETMST_KPI_INTGRTN_CLT_SRC_JOIN_COL_DTLS", htParam);
                if (DS.Tables.Count > 0)
                {
                    GridDefCol.DataSource = DS;
                    GridDefCol.DataBind();
                    ViewState["grid"] = DS.Tables[0];
                    if (GridDefCol.PageCount > 1)
                    {
                       // btnGrdDEFWHERECondNext.Enabled = true;
                    }
                    else
                    {
                      //  btnGrdDEFWHERECondNext.Enabled = false;
                    }
                }
                else
                {
                    //ds = null;
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void rdbSVACol_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbSVACol.SelectedValue == "1")
                {
                    //ddlSValCol.Enabled = true;
                    //ddlDCFJSynmCol.Enabled = true;
                    //ddlDCFJSOpertr.Enabled = true;
                    //ddlDCFJColVal.Enabled = true;
                    ddlSValCol.Enabled = false;
                    ddlDCFJSynmCol.Enabled = false;
                    ddlDCFJSOpertr.Enabled = false;
                    ddlDCFJColVal.Enabled = false;
                    ddlSyn1Col.Enabled = true;
                    ddlSyn2Col.Enabled = true;
                }
                else
                {
                    //ddlSValCol.Enabled = false;
                    //ddlDCFJSynmCol.Enabled = false;
                    //ddlDCFJSOpertr.Enabled = false;
                    //ddlDCFJColVal.Enabled = false;
                    ddlSValCol.Enabled = true;
                    ddlDCFJSynmCol.Enabled = true;
                    ddlDCFJSOpertr.Enabled = true;
                    ddlDCFJColVal.Enabled = true;
                    ddlSyn1Col.Enabled = false;
                    ddlSyn2Col.Enabled = false;
                }
            }
            catch (Exception ex)
            {

                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }


        public void bindWhereCondtn(string INTG_ID)
        {
            try
            {
                FillddlSynm(ddlDEFWCondSynm, "synm", INTG_ID);
                Fillddl(ddlDEFWCondSynm, "synm", string.Empty);
                Fillddl(ddlDEFWCondOptr, "OPRTR", string.Empty);
                Fillddl(ddlDEFWstatus, "STS", string.Empty);
                ddlDEFWstatus.SelectedIndex = 1;
                ddlDEFWstatus.Enabled = false;
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void ddlDEFWCondSynm_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillddlSAIMWRK(ddlDEFWCondColName, "SYNMCol", ddlDEFWCondSynm.SelectedValue.ToString().Trim());
        }

        public void bindgvDwhereCondition()
        {
            try
            {
                htParam.Clear();
                DS.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                DS = objDAL.GetDataSet("Prc_GETMST_KPI_INTGRTN_WHR_CNDTN", htParam);
                if (DS.Tables.Count > 0)
                {
                    gvDwhereCondition.DataSource = DS;
                    gvDwhereCondition.DataBind();
                    ViewState["grid"] = DS.Tables[0];
                    if (gvDwhereCondition.PageCount > 1)
                    {
                     //   btnDwhereConditionNEXT.Enabled = true;
                    }
                    else
                    {
                     //   btnDwhereConditionNEXT.Enabled = false;
                    }
                }

                else
                {
                    //ds = null;
                }
            }
            catch (Exception ex)
            {

                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void lnkDWCondBtn_Click(object sender, EventArgs e)
        {
            try
            {
                htParam.Clear();
                DS.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                htParam.Add("@SYNM_NAME", ddlDEFWCondSynm.SelectedValue);
                htParam.Add("@WHR_CNDTN_COL_NAME", ddlDEFWCondColName.SelectedValue);
                htParam.Add("@WHR_CNDTN_OPRT", ddlDEFWCondOptr.SelectedValue);
                htParam.Add("@WHR_CNDTN_COL_VAL", txtDEFWColVal.Text);
                htParam.Add("@CREATEDBY", HttpContext.Current.Session["UserID"].ToString().Trim());
                htParam.Add("@EFF_DTIM", txtDEFWEffFrm.Text);
                htParam.Add("@CSE_DTIM", txtDEFWEffTo.Text);
                htParam.Add("@STATUS", ddlDEFWstatus.SelectedValue);
                htParam.Add("@MODE", "S");
                htParam.Add("@SEQNO", "");
                DS = objDAL.GetDataSet("Prc_MST_KPI_INTGRTN_WHR_CNDTN", htParam);
                if (Convert.ToInt32(DS.Tables[0].Rows[0]["Response"]) == 0)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Data added successfully.');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data saved successfully.');", true);
                    bindgvDwhereCondition();
                    //bindgridDefJoin();
                    //htparam.Clear();
                    //dsfill.Clear();
                    //htparam.Add("@counterId", "Join_Id");
                    //dsfill = objDal.GetDataSetForPrc_SAIM("Prc_UPDCtrNO", htparam);
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Something went wrong.');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Record already exists.');", true);
                }
                ddlDEFWCondSynm.SelectedIndex = 0;
                ddlDEFWCondColName.SelectedIndex = 0;
                ddlDEFWCondOptr.SelectedIndex = 0;
                txtDEFWColVal.Text = "";
                txtDEFWEffFrm.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDEFWEffTo.Text = "";
                ddlDEFWstatus.SelectedIndex = 1;
                ddlDEFWCondSynm.Enabled = true;
                ddlDEFWCondColName.Enabled = true;
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void btnprevious_Click(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = gvINTSTFillUP.PageIndex;
                gvINTSTFillUP.PageIndex = pageIndex - 1;
                bindgvINTSTFillUP();
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
                int pageIndex = gvINTSTFillUP.PageIndex;
                gvINTSTFillUP.PageIndex = pageIndex + 1;
                bindgvINTSTFillUP();
                txtPage.Text = Convert.ToString(gvINTSTFillUP.PageIndex + 1);
                btnprevious.Enabled = true;
                if (txtPage.Text == Convert.ToString(gvINTSTFillUP.PageCount))
                {
                    btnnext.Enabled = false;
                }

                int page = gvINTSTFillUP.PageCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            //TextBoxSrcTbl.Text = "";
            ddlSynm.SelectedIndex = 0;
            ddlSTcol.SelectedIndex = 0;
            ddlSynmCol.SelectedIndex = 0;
            txtEffFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtEffTo.Text = "";
            ddlStatus.SelectedIndex = 1;
            btnUpdSTFUL.Attributes.Add("style", "display:none;");
            btnAdd.Attributes.Add("style", "display:inline-block;");
            //ddlSTcol.Enabled = true;
            ddlSTcol.Enabled = false;
            divbtnGrp1.Disabled = false;
            ddlSynmCol.Enabled = true;
            btnClear.Enabled = true;

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lblSTFULSEQNO = row.FindControl("lblSTFULSEQNO") as Label;
            Label lblsrcTblISTFUL = row.FindControl("lblsrcTblISTFUL") as Label;
            Label lblSynonyms = row.FindControl("lblSynonyms") as Label;
            Label lblSRC_TBL_COL = row.FindControl("lblSRC_TBL_COL") as Label;
            Label lblSYNM_COL = row.FindControl("lblSYNM_COL") as Label;
            Label lblEffDate = row.FindControl("lblEffDate") as Label;
            Label lblCeaseDate = row.FindControl("lblCeaseDate") as Label;
            Label lblstatusISTFUL = row.FindControl("lblstatusISTFUL") as Label;

            txtEffTo.Enabled = true;
            ViewState["hdnSTFULSEQNO"] = lblSTFULSEQNO.Text;
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["INTGRTNConnectionString"].ToString());
            String query = " select a.COL_NAM as paramval, a.COL_DESC as paramdesc from MST_TBL_COL_DTLS  a inner join  MST_KPI_BSE_SRC_TBL b on a.OBJ_ID = b.OBJ_ID  where b.TBL_NAME = @synmNAME";
            SqlCommand queryCommand = new SqlCommand(query, con);
            //SqlDataReader sdr;
            queryCommand.Parameters.Add("@synmNAME", TextBoxSrcTbl.Text);
            con.Open();
            //sdr = queryCommand.ExecuteReader(CommandBehavior.CloseConnection);
            SqlDataAdapter sda = new SqlDataAdapter(queryCommand);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ddlSTcol.DataSource = dt;
            ddlSTcol.DataTextField = "paramdesc";
            ddlSTcol.DataValueField = "paramval";
            ddlSTcol.DataBind();
            ddlSTcol.Items.Insert(0, new ListItem("Select", ""));
            ddlSTcol.SelectedValue = lblSRC_TBL_COL.Text;
            con.Close();
            TextBoxSrcTbl.Text = lblsrcTblISTFUL.Text;
            //ddlSynm.SelectedValue = lblSynonyms.Text;
            //ddlSTcol.SelectedValue = lblSRC_TBL_COL.Text;
            ddlSTcol.Enabled = false;
            ddlSynm.SelectedValue = lblSynonyms.Text;
            //FillddlSAIMWRK(ddlSynmCol, "SYNMCol", ddlSynm.SelectedValue.ToString().Trim());
            FillddlSAIMWRK1(ddlSynmCol, "SYNMCol", ddlSynm.SelectedValue.ToString().Trim());
            if (lblSYNM_COL.Text.ToString().Trim()!= "")
            {
                ddlSynmCol.SelectedValue = lblSYNM_COL.Text.ToString().Substring(3);
            }
            else
            {
                ddlSynmCol.Enabled = false;
            }
            txtEffFrom.Text = Convert.ToDateTime(lblEffDate.Text.Trim()).ToString("dd/MM/yyyy");
            txtEffTo.Text = lblCeaseDate.Text == "" ? "" : Convert.ToDateTime(lblCeaseDate.Text.Trim()).ToString("dd/MM/yyyy");
            ddlStatus.SelectedValue = lblstatusISTFUL.Text;
            btnUpdSTFUL.Attributes.Add("style", "display:inline-block;");
            btnAdd.Attributes.Add("style", "display:none;");
            ddlStatus.Enabled = true;
            btnClear.Enabled = false;
        }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void btnUpdSTFUL_Click(object sender, EventArgs e)
        {
            try
        {
                
            if(ddlStatus.SelectedIndex == 1)
            {
                if (txtEffTo.Text !="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Invalid status');", true);
                    return;

                }
            }
            if(ddlStatus.SelectedIndex == 2)
            {
                if (String.IsNullOrEmpty(txtEffTo.Text.Trim().ToString()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Cease Date');", true);
                    return;

                }
            }

            htParam.Clear();
            DS.Clear();
            objDAL = new DataAccessLayer("INTGRTNConnectionString");
            htParam.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
            htParam.Add("@SRC_TBL", TextBoxSrcTbl.Text);
            htParam.Add("@SYNONYMS", ddlSynm.SelectedValue);
            htParam.Add("@SCR_TBL_COL", ddlSTcol.SelectedValue);
            if (ddlSynmCol.SelectedItem.Text == "Select")
            {
                DataSet dsST = new DataSet();
                Hashtable htST = new Hashtable();
                htST.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                htST.Add("@TBL_NAME", ddlSynm.SelectedValue);
                htST.Add("@SRC_TBL_COL", ddlSTcol.SelectedValue);
                htST.Add("@MODE", "S");

                dsST = objDAL.GetDataSet("Prc_CHK_FR_FORMULA_IN_MST_KPI_INTGRTN_SYNYM_SRC_COL_MAP_SU_WITH_PARAM", htST);

                if (dsST.Tables.Count > 0 && dsST.Tables[0].Rows.Count > 0)
                {
                    if (dsST.Tables[0].Rows[0]["Response"].ToString().Trim() == "0")
                    {
                        ddlSynmCol.Enabled = false;
                        htParam.Add("@SYNM_TBL_COL", "");
                    }
                    else if (dsST.Tables[0].Rows[0]["Response"].ToString().Trim() == "1")
                    {
                        ddlSynmCol.Enabled = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Synonym Column');", true);
                        return;
                    }
                }
            }
            else
            {
                htParam.Add("@SYNM_TBL_COL", ddlSynmCol.SelectedValue.ToString().Trim());
            }
            //htparam.Add("@SYNM_TBL_COL", ddlSynmCol.SelectedValue);
            htParam.Add("@CREATEDBY", HttpContext.Current.Session["UserID"].ToString().Trim());
            htParam.Add("@EFF_DTIM", txtEffFrom.Text);
            htParam.Add("@CSE_DTIM", txtEffTo.Text);
            htParam.Add("@STATUS", ddlStatus.SelectedValue);
            htParam.Add("@MODE", "U");
            htParam.Add("@SEQNO", ViewState["hdnSTFULSEQNO"].ToString().Trim());
            
            DS = objDAL.GetDataSet("Prc_MST_KPI_INTGRTN_SYNYM_SRC_COL_MAP_SU", htParam);
            if(DS.Tables[0].Rows[0]["Response"].ToString().Trim() == "0")
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Data Updated successfully.');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data Updated successfully.');", true);
                btnClear_Click(EventArgs.Empty, EventArgs.Empty);
                bindgvINTSTFillUP();
                btnUpdSTFUL.Attributes.Add("style", "display:none;");
                btnAdd.Attributes.Add("style", "display:inline-block;");
                //ddlSTcol.Enabled = true;
                ddlSynm.SelectedIndex = 0;
                Fillddl(ddlSTcol, "STCol", TextBoxSrcTbl.Text);
                ddlSTcol.SelectedIndex = 0;
                ddlSynmCol.SelectedIndex = 0;
                txtEffFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtEffTo.Text = "";
                ddlStatus.SelectedIndex = 1;
                ddlStatus.Enabled = false;
                btnClear.Enabled = true;
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Something went wrong');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data Already exist');", true);
                txtEffTo.Enabled = true;
            }
            }
             catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
           }
        
        }
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblSTFULSEQNO = row.FindControl("lblSTFULSEQNO") as Label;
                Label lblsrcTblISTFUL = row.FindControl("lblsrcTblISTFUL") as Label;
                Label lblSynonyms = row.FindControl("lblSynonyms") as Label;
                Label lblSRC_TBL_COL = row.FindControl("lblSRC_TBL_COL") as Label;
                Label lblSYNM_COL = row.FindControl("lblSYNM_COL") as Label;
                Label lblEffDate = row.FindControl("lblEffDate") as Label;
                Label lblCeaseDate = row.FindControl("lblCeaseDate") as Label;
                Label lblstatusISTFUL = row.FindControl("lblstatusISTFUL") as Label;

                htParam.Clear();
                DS.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                htParam.Add("@SRC_TBL_COL", lblSRC_TBL_COL.Text);
                DS = objDAL.GetDataSet("Prc_CHK_FR_FORMULA_IN_MST_KPI_INTGRTN_SYNYM_SRC_COL_MAP_SU_WITH_PARAM_DEL", htParam);
                if (DS.Tables[0].Rows[0]["Response"].ToString().Trim() == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Cannot delete as parameter exist for this record.');", true);
                    return;
                }
                else if (DS.Tables[0].Rows[0]["Response"].ToString().Trim() == "1")
                {
                    htParam.Clear();
                    DS.Clear();
                    htParam.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                    htParam.Add("@SRC_TBL", lblsrcTblISTFUL.Text);
                    htParam.Add("@SYNONYMS", lblSynonyms.Text);
                    htParam.Add("@SCR_TBL_COL", lblSRC_TBL_COL.Text);
                    htParam.Add("@SYNM_TBL_COL", lblSYNM_COL.Text.ToString());
                    htParam.Add("@CREATEDBY", HttpContext.Current.Session["UserID"].ToString().Trim());
                    htParam.Add("@EFF_DTIM", Convert.ToDateTime(lblEffDate.Text.Trim()).ToString("dd/MM/yyyy"));
                    htParam.Add("@CSE_DTIM", lblCeaseDate.Text == "" ? "" : Convert.ToDateTime(lblCeaseDate.Text.Trim()).ToString("dd/MM/yyyy"));
                    htParam.Add("@STATUS", lblstatusISTFUL.Text);
                    htParam.Add("@MODE", "D");
                    htParam.Add("@SEQNO", lblSTFULSEQNO.Text);
                    DS = objDAL.GetDataSet("Prc_MST_KPI_INTGRTN_SYNYM_SRC_COL_MAP_SU", htParam);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Record has been deleted');", true); //Added by Abuzar on 04112020
                    bindgvINTSTFillUP();
                    Fillddl(ddlSTcol, "STCol", TextBoxSrcTbl.Text);
                    ddlSTcol.SelectedIndex = 0;
                    btnClear_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        
        }

        protected void lnkGRIDDefJOINEdit1_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grd = ((LinkButton)sender).NamingContainer as GridViewRow;
                Label lblJN_ID = (Label)grd.FindControl("lblJN_ID");
                string JN_ID = lblJN_ID.Text;
                htParam.Clear();
                DS.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@JN_ID", JN_ID);
                DS = objDAL.GetDataSet("Prc_FillMST_KPI_INTGRTN_CLT_SRC_JOIN_DTLS", htParam);
                if (DS.Tables[0].Rows[0]["Response"].ToString().Trim() == "1")
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Cannot update it as column join exists.');", true);
                    return;
                }
                else
                {
                    Session["JN_ID"] = txtJId.Text;
                    btnDEFJOINSave.Attributes.Add("style", "display:none;");
                    btnDEFJOINUpd.Attributes.Add("style", "display:inline-block;");
                    txtJId.Text = DS.Tables[1].Rows[0]["JN_ID"].ToString();
                    string synonyms1 = DS.Tables[1].Rows[0]["TABLE_1"].ToString();
                    string synonyms2 = DS.Tables[1].Rows[0]["TABLE_2"].ToString();
                    string primary = DS.Tables[1].Rows[0]["IS_PRIMARY_JOIN"].ToString();
                    string JN_TYP = DS.Tables[1].Rows[0]["JN_TYP"].ToString();
                    string STATUS = DS.Tables[1].Rows[0]["STATUS"].ToString();
                    txtDJEffFrom.Text = Convert.ToDateTime(DS.Tables[1].Rows[0]["EFF_DTIM"]).ToString("dd/MM/yyyy");
                    txtDJEffTo.Enabled = true;
                    INTG_ID = Request.QueryString["INTG_ID"].ToString().Trim();
                    FillddlSynm(ddlSynm1, "synm", INTG_ID);
                    FillddlSynm(ddlSynm2, "synm", INTG_ID);
                    ddlSynm1.SelectedValue = synonyms1;
                    ddlSynm2.SelectedValue = synonyms2;
                    Fillddl(ddlPrmJnt, "IPJ", string.Empty);
                    ddlPrmJnt.SelectedValue = primary;
                    Fillddl(ddlJoinType, "JTYP", string.Empty);
                    Fillddl(ddlDJStatus, "STS", string.Empty);
                    ddlJoinType.SelectedValue = JN_TYP;
                    ddlDJStatus.SelectedValue = STATUS;
                    ddlDJStatus.Enabled = true;
                    btnClr.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        
        }

        protected void lnkGRIDDefJOINDelete_Click(object sender, EventArgs e)
        {
             try
        {
            GridViewRow grd = ((LinkButton)sender).NamingContainer as GridViewRow;
            Label lblJN_ID = (Label)grd.FindControl("lblJN_ID");
            string JN_ID = lblJN_ID.Text;
            htParam.Clear();
            DS.Clear();
            objDAL = new DataAccessLayer("INTGRTNConnectionString");
            htParam.Add("@JN_ID", JN_ID);
            DS = objDAL.GetDataSet("Prc_DelMST_KPI_INTGRTN_CLT_SRC_JOIN_DTLS", htParam);
            if (DS.Tables[0].Rows[0]["Response"].ToString().Trim() == "1")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Record cannot be deleted as column join exists.');", true);
                return;
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Record has been deleted.');", true);
                bindgridDefJoin();
            }

        }
             catch (Exception ex)
             {
                 string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                 System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                 objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
             }
        }

        protected void btnDEFJOINUpd_Click(object sender, EventArgs e)
        {
        try
        {
            if (ddlDJStatus.SelectedIndex == 1)
            {
                if (txtDJEffTo.Text != "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Invalid status');", true);
                    return;

                }
            }
            if (ddlDJStatus.SelectedIndex == 2)
            {
                if (String.IsNullOrEmpty(txtDJEffTo.Text.Trim().ToString()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Cease Date');", true);
                    return;

                }
            }
            htParam.Clear();
            DS.Clear();
            objDAL = new DataAccessLayer("INTGRTNConnectionString");
            htParam.Add("@INTGRTN_ID", txtIntGid.Text);
            htParam.Add("@JN_ID", txtJId.Text);
            htParam.Add("@TABLE_1", ddlSynm1.SelectedValue);
            htParam.Add("@TABLE_2", ddlSynm2.SelectedValue);
            htParam.Add("@IS_PRIMARY_JOIN", ddlPrmJnt.SelectedValue);
            htParam.Add("@JN_TYP", ddlJoinType.SelectedValue);
            htParam.Add("@CREATEDBY", HttpContext.Current.Session["UserID"].ToString().Trim());
            htParam.Add("@EFF_DTIM", txtDJEffFrom.Text);
            htParam.Add("@CSE_DTIM", txtDJEffTo.Text);
            htParam.Add("@STATUS", ddlDJStatus.SelectedValue);
            htParam.Add("@MODE", "U");
            DS = objDAL.GetDataSet("Prc_MST_KPI_INTGRTN_CLT_SRC_JOIN_DTLS", htParam);
            if (Convert.ToInt32(DS.Tables[0].Rows[0]["Response"]) == 0)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Data added successfully.');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data Updated successfully.');", true);
                bindgridDefJoin();
                txtJId.Text = (string)(Session["JN_ID"]);
                ddlSynm1.ClearSelection();
                ddlSynm2.ClearSelection();
                ddlPrmJnt.ClearSelection();
                ddlJoinType.ClearSelection();
                txtDJEffFrom.Text= DateTime.Now.ToString("dd/MM/yyyy");
                ddlDJStatus.SelectedIndex = 1;
                ddlDJStatus.Enabled = false;
                btnClr.Enabled = true;
                btnDEFJOINUpd.Attributes.Add("style", "display:none;");
                btnDEFJOINSave.Attributes.Add("style", "display:inline-block;");
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Something went wrong.');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data Already exists.');", true);
            }

        }
catch (Exception ex)
{
    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
    objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
}
        }

        protected void lnkUPDDefColJoinbtn_Click(object sender, EventArgs e)
        {
            try
        {
            objDAL = new DataAccessLayer("INTGRTNConnectionString");
            if (ddlDCJoinStatus.SelectedIndex == 1)
            {
                if (txtDCJoinEffTo.Text != "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Invalid status');", true);
                    return;

                }
            }
            if (ddlDCJoinStatus.SelectedIndex == 2)
            {
                if (String.IsNullOrEmpty(txtDCJoinEffTo.Text.Trim().ToString()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Cease Date');", true);
                    return;

                }
            }
            htParam.Clear();
            DS.Clear();
            htParam.Add("@INTGRTN_ID", TextBox1.Text);
            htParam.Add("@JN_ID", TextBox2.Text);
            htParam.Add("@SET_VAL_AS_CLMN", rdbSVACol.SelectedValue);
            htParam.Add("@TBL_1_COL", ddlSyn1Col.SelectedValue);
            htParam.Add("@TBL_2_COL", ddlSyn2Col.SelectedValue);
            htParam.Add("@EFF_DTIM", txtDCJoinEffFrm.Text);
            htParam.Add("@CSE_DTIM", txtDCJoinEffTo.Text);
            htParam.Add("@STATUS", ddlDCJoinStatus.SelectedValue);
            htParam.Add("@MODE", "U");
            htParam.Add("@SEQNO", ViewState["hdnDCForJSEQNO"].ToString().Trim());
            DS = objDAL.GetDataSet("Prc_MST_KPI_INTGRTN_CLT_SRC_JOIN_COL_DTLS", htParam);
            if (Convert.ToInt32(DS.Tables[0].Rows[0]["Response"]) == 0)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Data updated successfully.');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data updated successfully.');", true);
                bindGridDefCol();
                lnkUPDDefColJoinbtn.Attributes.Add("style", "display:none;");
                lnkDefColJoinbtn.Attributes.Add("style", "display:inline-block;");
                ddlSyn1Col.SelectedIndex = 0;
                ddlSyn2Col.SelectedIndex = 0;
                ddlDCJoinStatus.Enabled = false;
                ddlDCJoinStatus.SelectedIndex = 1;
                txtDCJoinEffTo.Text = "";
                lnkDefColJoinClrbtn.Enabled = true;
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Something went wrong.');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data already exists.');", true);
            }
        }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void lnkDEFCOLJOINEdit_Click(object sender, EventArgs e)
        {
             try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lblDEFCOLJOINSEQNO = row.FindControl("lblDEFCOLJOINSEQNO") as Label;
            Label lblDEFCOLJOINEFFDTIM = row.FindControl("lblDEFCOLJOINEFFDTIM") as Label;
            Label lblDEFCOLJOINCSEDTIM = row.FindControl("lblDEFCOLJOINCSEDTIM") as Label;
            Label lblDEFCOLJOINJSTATUS = row.FindControl("lblDEFCOLJOINJSTATUS") as Label;
            Label lblDEFCOLJOINJN_ID = row.FindControl("lblDEFCOLJOINJN_ID") as Label;
            Label lblJDEFCOLJOINTBL_1_COL = row.FindControl("lblJDEFCOLJOINTBL_1_COL") as Label;
            Label lblJDEFCOLJOINTBL_2_COL = row.FindControl("lblJDEFCOLJOINTBL_2_COL") as Label;
            Label lblDEFCOLJOINJSVACol = row.FindControl("lblDEFCOLJOINJSVACol") as Label;

            ddlDCJoinStatus.Enabled = true;
            txtDCJoinEffTo.Enabled = true;
            rdbSVACol.SelectedValue = lblDEFCOLJOINJSVACol.Text;
            ViewState["hdnDCForJSEQNO"] = lblDEFCOLJOINSEQNO.Text;
            TextBox2.Text = lblDEFCOLJOINJN_ID.Text;
            ddlSyn1Col.SelectedValue = lblJDEFCOLJOINTBL_1_COL.Text;
            ddlSyn2Col.SelectedValue = lblJDEFCOLJOINTBL_2_COL.Text;
            txtDCJoinEffFrm.Text = Convert.ToDateTime(lblDEFCOLJOINEFFDTIM.Text.Trim()).ToString("dd/MM/yyyy");
            txtDCJoinEffTo.Text = lblDEFCOLJOINCSEDTIM.Text == "" ? "" : Convert.ToDateTime(lblDEFCOLJOINCSEDTIM.Text.Trim()).ToString("dd/MM/yyyy");
            ddlDCJoinStatus.SelectedValue = lblDEFCOLJOINJSTATUS.Text;
            lnkDefColJoinClrbtn.Enabled = false;
            txtDCJoinEffFrm.Enabled = false;

            lnkUPDDefColJoinbtn.Attributes.Add("style", "display:inline-block;");
            lnkDefColJoinbtn.Attributes.Add("style", "display:none;");
            if (lblDEFCOLJOINJSVACol.Text == "1")
            {
                ddlSValCol.Enabled = false;
                ddlDCFJSynmCol.Enabled = false;
                ddlDCFJSOpertr.Enabled = false;
                ddlDCFJColVal.Enabled = false;
                ddlSyn1Col.Enabled = true;
                ddlSyn2Col.Enabled = true;
            }
            else
            {
                ddlSValCol.Enabled = true;
                ddlDCFJSynmCol.Enabled = true;
                ddlDCFJSOpertr.Enabled = true;
                ddlDCFJColVal.Enabled = true;
                ddlSyn1Col.Enabled = false;
                ddlSyn2Col.Enabled = false;
            }

        }
             catch (Exception ex)
             {
                 string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                 System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                 objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
             }
        }

        protected void lnkDEFCOLJOINDelete_Click(object sender, EventArgs e)
        {
        try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lblDEFCOLJOINSEQNO = row.FindControl("lblDEFCOLJOINSEQNO") as Label;
            htParam.Clear();
            DS.Clear();
            objDAL = new DataAccessLayer("INTGRTNConnectionString");
            htParam.Add("@SEQ_NO", lblDEFCOLJOINSEQNO.Text);
            DS = objDAL.GetDataSet("Prc_DelMST_KPI_INTGRTN_CLT_SRC_JOIN_COL_DTLS", htParam);
            if (Convert.ToInt32(DS.Tables[0].Rows[0]["Response"]) == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data has been deleted.');", true);
                bindGridDefCol();
            }

        }
catch (Exception ex)
{
    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
    objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
}
        }

        protected void lnkUPDDWCondBtn_Click(object sender, EventArgs e)
        {
            try
        {
            if (ddlDEFWstatus.SelectedIndex == 1)
            {
                if (txtDEFWEffTo.Text != "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Invalid status');", true);
                    return;

                }
            }
            if (ddlDEFWstatus.SelectedIndex == 2)
            {
                if (String.IsNullOrEmpty(txtDEFWEffTo.Text.Trim().ToString()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Cease Date');", true);
                    return;

                }
            }

            htParam.Clear();
            DS.Clear();
            htParam.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
            htParam.Add("@SYNM_NAME", ddlDEFWCondSynm.SelectedValue);
            htParam.Add("@WHR_CNDTN_COL_NAME", ddlDEFWCondColName.SelectedValue);
            htParam.Add("@WHR_CNDTN_OPRT", ddlDEFWCondOptr.SelectedValue);
            htParam.Add("@WHR_CNDTN_COL_VAL", txtDEFWColVal.Text);
            htParam.Add("@CREATEDBY", HttpContext.Current.Session["UserID"].ToString().Trim());
            htParam.Add("@EFF_DTIM", txtDEFWEffFrm.Text);
            htParam.Add("@CSE_DTIM", txtDEFWEffTo.Text);
            htParam.Add("@STATUS", ddlDEFWstatus.SelectedValue);
            htParam.Add("@MODE", "U");
            htParam.Add("@SEQNO", ViewState["hdnDWCSEQNO"].ToString().Trim());

            objDAL = new DataAccessLayer("INTGRTNConnectionString");
            DS = objDAL.GetDataSet("Prc_MST_KPI_INTGRTN_WHR_CNDTN", htParam);
            if (Convert.ToInt32(DS.Tables[0].Rows[0]["Response"]) == 0)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Data Updated successfully.');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data Updated successfully.');", true);
                bindgvDwhereCondition();
                ddlDEFWstatus.SelectedIndex = 1;
                ddlDEFWstatus.Enabled = false;
                lnkDWCondclrbtn.Enabled = true;
                ddlDEFWCondSynm.SelectedIndex = 0;
                ddlDEFWCondColName.SelectedIndex = 0;
                ddlDEFWCondOptr.SelectedIndex = 0;
                txtDEFWColVal.Text = "";
                txtDEFWEffFrm.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDEFWEffTo.Text = "";
                ddlDEFWCondSynm.Enabled = true;
                ddlDEFWCondColName.Enabled = true;
                txtDEFWEffFrm.Enabled = true;
                lnkUPDDWCondBtn.Attributes.Add("style", "display:none;");
                lnkDWCondBtn.Attributes.Add("style", "display:inline-block;");
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Something went wrong.');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data already exists.');", true);
                txtDEFWEffTo.Enabled = true;
                return;
            }
        }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }

        }

        protected void lnkDWCEdit_Click(object sender, EventArgs e)
        {
             try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lblDWCSynmName = row.FindControl("lblDWCSynmName") as Label;
            Label lblDWCSEQNO = row.FindControl("lblDWCSEQNO") as Label;
            Label lblDWCEFF_DTIM = row.FindControl("lblDWCEFF_DTIM") as Label;
            Label lblDWCCSE_DTIM = row.FindControl("lblDWCCSE_DTIM") as Label;
            Label lblDWCColName = row.FindControl("lblDWCColName") as Label;
            Label lblDWCOPRTR = row.FindControl("lblDWCOPRTR") as Label;
            Label lblDWCCOlVAL = row.FindControl("lblDWCCOlVAL") as Label;
            Label lblDWCStatus = row.FindControl("lblDWCStatus") as Label;

            ddlDEFWstatus.Enabled = true;
            txtDEFWEffTo.Enabled = true;
            ViewState["hdnDWCSEQNO"] = lblDWCSEQNO.Text;
            ddlDEFWCondSynm.SelectedValue = lblDWCSynmName.Text;
            FillddlSAIMWRK(ddlDEFWCondColName, "SYNMCol", ddlDEFWCondSynm.SelectedValue.ToString().Trim());
            ddlDEFWCondColName.SelectedValue = lblDWCColName.Text.ToString().Substring(3);
            ddlDEFWCondOptr.SelectedValue = lblDWCOPRTR.Text;
            txtDEFWColVal.Text = lblDWCCOlVAL.Text;
            txtDEFWEffFrm.Text = Convert.ToDateTime(lblDWCEFF_DTIM.Text.Trim()).ToString("dd/MM/yyyy");
            txtDEFWEffTo.Text = lblDWCCSE_DTIM.Text == "" ? "" : Convert.ToDateTime(lblDWCCSE_DTIM.Text.Trim()).ToString("dd/MM/yyyy");
            ddlDEFWstatus.SelectedValue = lblDWCStatus.Text;
            ddlDEFWCondSynm.Enabled = false;
            ddlDEFWCondColName.Enabled = false;
            lnkUPDDWCondBtn.Attributes.Add("style", "display:inline-block;");
            lnkDWCondBtn.Attributes.Add("style", "display:none;");
            lnkDWCondclrbtn.Enabled = false;
            txtDEFWEffFrm.Enabled = false;
        }
             catch (Exception ex)
             {
                 string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                 System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                 objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
             }

        }

        protected void lnkDWCDelete_Click(object sender, EventArgs e)
        {
             try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lblDWCSEQNO = row.FindControl("lblDWCSEQNO") as Label;
            htParam.Clear();
            DS.Clear();
            objDAL = new DataAccessLayer("INTGRTNConnectionString");
            htParam.Add("@SEQ_NO", lblDWCSEQNO.Text);
            DS = objDAL.GetDataSet("Prc_DelMST_KPI_INTGRTN_WHR_CNDTN", htParam);
            if (Convert.ToInt32(DS.Tables[0].Rows[0]["Response"]) == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data has been deleted.');", true);
                bindgvDwhereCondition();
            }

        }
             catch (Exception ex)
             {
                 string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                 System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                 objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
             }

        }

      
        protected void btnClr_Click(object sender, EventArgs e)
        {
            ddlSynm1.SelectedIndex = 0;
            ddlSynm2.SelectedIndex = 0;
            ddlPrmJnt.SelectedIndex = 0;
            ddlJoinType.SelectedIndex = 0;
            txtDJEffFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDJEffTo.Text = "";
            ddlDJStatus.SelectedIndex = 1;
        }

        protected void lnkDefColJoinClrbtn_Click(object sender, EventArgs e)
        {
            ddlSyn1Col.SelectedIndex = 0;
            ddlSyn2Col.SelectedIndex = 0;
            txtDCJoinEffFrm.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDCJoinEffTo.Text = "";
            ddlDCJoinStatus.SelectedIndex = 1;
            ddlDCJoinStatus.Enabled = false;
            lnkUPDDefColJoinbtn.Attributes.Add("style", "display:none;");
            lnkDefColJoinbtn.Attributes.Add("style", "display:inline-block;");
        }

        protected void lnkDWCondclrbtn_Click(object sender, EventArgs e)
        {
            ddlDEFWCondSynm.SelectedIndex = 0;
            ddlDEFWCondColName.SelectedIndex = 0;
            ddlDEFWCondOptr.SelectedIndex = 0;
            txtDEFWColVal.Text = "";
            txtDEFWEffFrm.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDEFWEffTo.Text = "";
            ddlDEFWstatus.SelectedIndex = 1;
            ddlDEFWCondSynm.Enabled = true;
            ddlDEFWCondColName.Enabled = true;
            lnkUPDDWCondBtn.Attributes.Add("style", "display:none;");
            lnkDWCondBtn.Attributes.Add("style", "display:inline-block;");
        }

       
    }
}