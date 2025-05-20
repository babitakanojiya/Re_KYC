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
    public partial class Defbassrctbl : System.Web.UI.Page
    {
        CommonUtility cu = new CommonUtility();
        ErrorLog objErr = new ErrorLog();
        int AppId = 0;
        SqlDataReader sdr;
        DataAccessLayer objDAL = new DataAccessLayer();
        DataTable dt = new DataTable();
        Hashtable htParam = new Hashtable();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["UserLangNum"] == null || Session["CarrierCode"] == null || Session["LanguageCode"] == null)
            //{
            //    Response.Redirect("~/ErrorSession.aspx");
            //}
            try
            {
                if (!IsPostBack)
                {
                    GetSrcTblData(dgbassrctbl);
                    BindddlStatus(ddlStatus);
                    lblIsIdentity.Text = "Is Identity";
                    lblIsPrimary.Text = "Is Primary";
                    lblIsAvailable.Text = "Is NULLABLE";
                    lblIsforkey.Text = "Is Foreign Key";
                    txtEfDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtCeDate.Enabled = false;
                    txtEfDate.Enabled = false;
                    ddlStatus.SelectedIndex = 1;
                    ddlStatus.Enabled = false;
                    ddlForCol.Items.Insert(0, new ListItem("SELECT", ""));
                    //if (Request.QueryString["Flag"].ToString().Trim() == "B")
                    //{
                    //    ddlTblType.Items.Insert(0, new ListItem("BASE", "B"));
                    //}
                    if (Request.QueryString["Flag"].ToString().Trim() == "S")
                    {
                        ddlTblType.Items.Insert(0, new ListItem("SOURCE", "S"));
                    }
                    ddlTblType.Enabled = false;

                    //DEfine table click binding
                    bindddlcoldattyp(ddldattyp);
                    txtcsedatecol.Enabled = false;
                    txteffdatecol.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txteffdatecol.Enabled = false;
                    ddlstatus1.SelectedIndex = 1;
                    ddlstatus1.Enabled = false;
                    BindddlCltyp(ddlcoltyp);
                    Bindddlabs(ddlabsflg);
                    ddlcoltyp.Enabled = false;
                    ddlcoltyp.SelectedIndex = 2;
                    ddlabsflg.Enabled = false;
                    ddlabsflg.SelectedIndex = 2;
                 
                }
                if (rdIsforkey.Checked == false && rdIsforkey1.Checked == false)
                {
                    ddlForCol.Enabled = false;
                    ddlFortbl.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }


        public void BindddlStatus(DropDownList ddl)
        {
            try
            {
                htParam.Clear();
                dt.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                dt = objDAL.GetDataTable("PRC_DDLSTATUS", htParam);
                if (dt.Rows.Count > 0)
                {
                    ddl.DataSource = dt;
                    ddl.DataTextField = "ParamDesc1";
                    ddl.DataValueField = "ParamValue";
                    ddl.DataBind();
                }
                ddl.Items.Insert(0, new ListItem("SELECT", ""));

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
                DataSet ds = new DataSet();
                Hashtable ht = new Hashtable();
                ds.Clear();
                ht.Clear();
                if (String.IsNullOrEmpty(txtTblNmae.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "confPromptBox();", true);
                    return;

                }
                else
                {
                    ht.Add("@TBL_NME", txtTblNmae.Text);
                }

                ht.Add("@TBL_TYP", Request.QueryString["Flag"].ToString().Trim());
                if (ddlStatus.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "confPromptBox2();", true);
                    return;

                }
                else
                {
                    ht.Add("@TBL_STA", ddlStatus.SelectedValue.ToString().Trim());
                }
                if (String.IsNullOrEmpty(TxtTblDesc.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "confPromptBox3();", true);
                    return;

                }
                else
                {
                    ht.Add("@TBL_DESC", TxtTblDesc.Text);
                }
                if (String.IsNullOrEmpty(txtEfDate.Text.Trim().ToString()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "confPromptBox4();", true);
                    return;

                }
                else
                {
                    ht.Add("@EFF_DTIM", Convert.ToDateTime(txtEfDate.Text.Trim()).ToString("MM/dd/yyyy"));
                }
                if (txtCeDate.Text.ToString() == "")
                {

                    ht.Add("@CSE_DTIM", DBNull.Value);

                }
                else
                {

                    ht.Add("@CSE_DTIM", Convert.ToDateTime(txtCeDate.Text.Trim()).ToString("MM/dd/yyyy"));
                }
                ht.Add("@CREATEDBY", HttpContext.Current.Session["UserID"].ToString().Trim());
                {
                    ds = objDAL.GetDataSet("PRC_INS_MST_KPI_BSE_SRC_TBL", ht, "INTGRTNConnectionString");
                   
                }
                string msg = string.Empty;
                msg = ds.Tables[1].Rows[0]["MESSAGE"].ToString().Trim();
                if (msg == "FAILED")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "alert('Table already exits');", true);
                    return;
                }
                else if (msg == "DESCRIPTION")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "alert('Table Description exist');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "alert('Table Created Successfully');", true);
                }
                //Response.Redirect(Page.Request.Url.AbsoluteUri);
                GetSrcTblData(dgbassrctbl);
                TxtTblDesc.Text = "";
                txtTblNmae.Text = "";

            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        private void ShowNoResultFound(DataTable source, GridView gv)
        {
            source.Rows.Add(source.NewRow());

            gv.DataSource = source;
            gv.DataBind();
            int columnsCount = gv.Columns.Count;
            int rowsCount = gv.Rows.Count;
            gv.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
            gv.Rows[0].Cells[columnsCount - 1].Text = "";
            gv.Rows[0].Cells[columnsCount - 2].Text = "";
            gv.Rows[0].Cells[0].Text = "No tables have been defined";

            //source.Rows.Clear();
        }

        protected void GetSrcTblData(GridView dg)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Clear();
                htParam.Clear();
                htParam.Add("@Flag", Request.QueryString["Flag"].ToString().Trim());
                
               ds= objDAL.GetDataSet("PRC_BASE_SRC_TBL", htParam, "INTGRTNConnectionString");
                dg.DataSource = ds;
                dg.DataBind();
                if (ds.Tables.Count > 0)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        btnnext.Visible = true;
                        btnprevious.Visible = true;
                        txtPage.Visible = true;

                        if (dgbassrctbl.PageCount > 1)
                        {
                            btnnext.Enabled = true;
                            btnprevious.Enabled = false;
                        }
                        else
                        {
                            btnnext.Enabled = false;
                            btnprevious.Enabled = false;
                        }

                    }
                    else
                    {
                        DataTable dt = ds.Tables[0];
                        ShowNoResultFound(dt, dg);
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

       

        protected void Deftblcol_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
                HiddenField hdntblnam = (HiddenField)row.FindControl("hdntblnam");
                htParam.Clear();
                ds.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@TBL_NME", hdntblnam.Value.ToString().Trim());
                ds = objDAL.GetDataSet("PRC_BASE_SRC_TBL_NME", htParam);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    string tablename = ds.Tables[0].Rows[0]["TBL_NAME"].ToString();
                    Session["tablename"] = tablename;
                }

                //   txtpagecol.Text = "1";
                GetSrcTblDatacol(dgbassrctblcol);
                // bindddlcoldattyp(ddldattyp);
                  bindddlfortblnam(ddlFortbl);
                BindddlStatus(ddlstatus1);
                //txtcsedatecol.Enabled = false;
                //txteffdatecol.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //txteffdatecol.Enabled = false;
                ddlstatus1.SelectedIndex = 1;
                ddlstatus1.Enabled = false;
                //checkforidentity();
                //BindddlCltyp(ddlcoltyp);
                //Bindddlabs(ddlabsflg);
                //ddlcoltyp.Enabled = false;
                //ddlcoltyp.SelectedIndex = 2;
                //ddlabsflg.Enabled = false;
                //ddlabsflg.SelectedIndex = 2;
                checkforidentity();
               

                //  divbassrctblcolhdrcollapse.Attributes.Add("display", "block");
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        public void bindddlfortblnam(DropDownList ddl)
        {
            try
            {
                ds.Clear();
                htParam.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                string tablename2 = (string)(Session["tablename"]);
                htParam.Add("@TBL_NME", tablename2.ToString().Trim());
                ds = objDAL.GetDataSet("PRC_BASE_SRC_TBL_COL_FOR_TBL", htParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl.DataSource = ds.Tables[0];
                    ddl.DataTextField = "TBL_NAME";
                    ddl.DataValueField = "OBJ_ID";
                    ddl.DataBind();
                }
                ddl.Items.Insert(0, new ListItem("SELECT", ""));

            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        public void bindddlcoldattyp(DropDownList ddl)
        {
            try
            {
                dt.Clear();
                htParam.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                dt = objDAL.GetDataTable("PRC_BASE_SRC_TBL_COL_DAT_TYP", htParam);
                if (dt.Rows.Count > 0)
                {
                    
                    ddl.DataSource = dt;
                    ddl.DataTextField = "DATA_TYP";
                    ddl.DataValueField = "DATA_TYP_ID";
                    ddl.DataBind();
                }
                ddl.Items.Insert(0, new ListItem("SELECT", ""));

            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        public void BindddlCltyp(DropDownList ddl)
        {
            try
            {
                dt.Clear();
                htParam.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                dt = objDAL.GetDataTable("PRC_DDLCLTYP", htParam);
                if (dt.Rows.Count > 0)
                {
                    ddl.DataSource = dt;
                    ddl.DataTextField = "ParamDesc1";
                    ddl.DataValueField = "ParamValue";
                    ddl.DataBind();
                }
                ddl.Items.Insert(0, new ListItem("SELECT", ""));

            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        public void Bindddlabs(DropDownList ddl)
        {
            try
            {
                dt.Clear();
                htParam.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                dt = objDAL.GetDataTable("PRC_DDLABSFLG", htParam);
                if (dt.Rows.Count > 0)
                {
                    ddl.DataSource = dt;
                    ddl.DataTextField = "ParamDesc1";
                    ddl.DataValueField = "ParamValue";
                    ddl.DataBind();
                }
                ddl.Items.Insert(0, new ListItem("SELECT", ""));

            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        //public void bindddlfortblnam(DropDownList ddl)
        //{
        //    try
        //    {
        //        dt.Clear();
        //        htParam.Clear();
        //        string tablename2 = (string)(Session["tablename"]);
        //        htParam.Add("@TBL_NME", tablename2.ToString().Trim());
               
        //        objDAL = new DataAccessLayer("INTGRTNConnectionString");
        //        dt = objDAL.GetDataTable("PRC_BASE_SRC_TBL_COL_FOR_TBL", htParam);
        //        if (dt.Rows.Count > 0)
        //        {
        //            ddl.DataSource = ds.Tables[0];
        //            ddl.DataTextField = "TBL_NAME";
        //            ddl.DataValueField = "OBJ_ID";
        //            ddl.DataBind();
        //        }
        //        ddl.Items.Insert(0, new ListItem("SELECT", ""));

        //    }
        //    catch (Exception ex)
        //    {

        //        string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
        //        System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
        //        objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
        //    }
        //}

        protected void ddldattyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ds.Clear();
                htParam.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@DAT_TYP", ddldattyp.SelectedValue.ToString().Trim());
                ds = objDAL.GetDataSet("PRC_CHK_SIZE_REQ", htParam);
                string a = ds.Tables[0].Rows[0]["SIZE_REQ"].ToString();
                Session["a"] = a;
                if (a == "0" || ddldattyp.SelectedValue == "60")
                {
                    txtSize.Enabled = false;
                    txtprec.Text = "";
                    txtSize.Text = "";
                }
                else
                {
                    txtSize.Enabled = true;
                    txtSize.Text = "";
                    txtprec.Text = "";
                }
                if (ddldattyp.SelectedValue == "106" || ddldattyp.SelectedValue == "108")
                {
                    txtprec.Enabled = true;
                    txtSize.Text = "";
                    txtSize.Enabled = true;
                }
                else
                {
                    txtprec.Enabled = false;
                }
            }
            catch (Exception ex)
            {
               string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
               System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
               objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        public void checkforidentity()
        {
            try
            {
                dt.Clear();
                ds.Clear();
                htParam.Clear();
                string tablename1 = (string)(Session["tablename"]);
                htParam.Add("@TBL_NME", tablename1.ToString().Trim());
                //ds = objDal.GetDataSetForPrc_SAIM("PRC_GETOBJID", htParam);
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                ds = objDAL.GetDataSet("PRC_GETOBJID", htParam);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string OBJ_ID = ds.Tables[0].Rows[0]["OBJ_ID"].ToString();
                    Session["OBJ_ID"] = OBJ_ID;
                }
                ds.Clear();
                htParam.Clear();
                string OBJ_ID1 = (string)(Session["OBJ_ID"]);
                htParam.Add("@OBJ_ID", OBJ_ID1.ToString().Trim());
              DataAccessLayer  objDAL1 = new DataAccessLayer("INTGRTNConnectionString");
                ds = objDAL1.GetDataSet("PRC_CHK_FOR_IDENTITY", htParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string IDENTITY = ds.Tables[0].Rows[0]["SUM"].ToString();
                    if (IDENTITY == "1")
                    {
                        rdSingleCycle1.Checked = true;
                        rdSingleCycle1.Enabled = false;
                        rdSingleCycle.Enabled = false;
                    }
                    else
                    {
                        rdSingleCycle1.Enabled = true;
                        rdSingleCycle.Enabled = true;
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

        protected void btnSave1_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                Hashtable ht = new Hashtable();
                ds.Clear();
                ht.Clear();
                htParam.Clear();
                ds1.Clear();
                string tablename1 = (string)(Session["tablename"]);
                htParam.Add("@TBL_NME", tablename1.ToString().Trim());
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                ds = objDAL.GetDataSet("PRC_GETOBJID", htParam);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string OBJ_ID = ds.Tables[0].Rows[0]["OBJ_ID"].ToString();
                    Session["OBJ_ID"] = OBJ_ID;
                }
                string OBJ_ID1 = (string)(Session["OBJ_ID"]);
                if (btnSave1.Text == "<span class='glyphicon glyphicon-floppy-disk' style='color: White;'></span>  Update")
                {
                    if (String.IsNullOrEmpty(txtColdesc.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Column Description Channel');", true);
                        return;

                    }
                    else
                    {
                        ht.Add("@COL_DESC", txtColdesc.Text);
                    }
                    if (ddldattyp.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select data type');", true);
                        return;

                    }
                    else
                    {
                        ht.Add("@DAT_TYP", ddldattyp.SelectedValue.ToString().Trim());
                    }
                    string a1 = (string)(Session["a"]);
                    if (a1 == "1" && ddldattyp.SelectedValue != "60")
                    {
                        if (String.IsNullOrEmpty(txtSize.Text))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Size');", true);
                            return;

                        }
                        else
                        {
                            ht.Add("@SIZE", txtSize.Text);
                        }
                    }
                    else
                    {
                        ht.Add("@SIZE", txtSize.Text);
                    }
                    if (ddldattyp.SelectedValue == "106" || ddldattyp.SelectedValue == "108")
                    {
                        if (String.IsNullOrEmpty(txtprec.Text))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Precision');", true);
                            return;

                        }
                        else
                        {
                            ht.Add("@PREC", txtprec.Text);
                        }
                    }
                    else
                    {
                        ht.Add("@PREC", txtprec.Text);
                    }
                    if (txtcsedatecol.Text.ToString() == "")
                    {

                        ht.Add("@CSE_DTIM1", DBNull.Value);

                    }
                    else
                    {

                        ht.Add("@CSE_DTIM1", Convert.ToDateTime(txtcsedatecol.Text.Trim()).ToString("MM/dd/yyyy"));
                    }
                    if (txtcsedatecol.Text.ToString() != "")
                    {

                        if (ddlstatus1.SelectedIndex != 2)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please select inactive as status');", true);
                            return;
                        }
                        else
                        {
                            ht.Add("@STATUS1", ddlstatus1.SelectedValue.ToString().Trim());
                        }
                    }
                    else
                    {
                        if (ddlstatus1.SelectedIndex == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please select Status');", true);
                            return;
                        }
                        else
                        {
                            ht.Add("@STATUS1", ddlstatus1.SelectedValue.ToString().Trim());
                        }
                    }
                    string ColName = (string)(Session["ColName"]);
                    ht.Add("@COL_NME", ColName.ToString().Trim());
                    ht.Add("@OBJ_ID", OBJ_ID1.ToString().Trim());
                    ht.Add("@TBL_NME", tablename1.ToString().Trim());
                    ht.Add("@FLAG", "U");

                }

                else
                {
                    if (String.IsNullOrEmpty(txtColName.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Column Name Channel');", true);
                        return;

                    }
                    else
                    {
                        ht.Add("@COL_NME", txtColName.Text);
                    }
                    if (String.IsNullOrEmpty(txtColdesc.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Column Description Channel');", true);
                        return;

                    }
                    else
                    {
                        ht.Add("@COL_DESC", txtColdesc.Text);
                    }
                    if (ddldattyp.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select data type');", true);
                        return;

                    }
                    else
                    {
                        ht.Add("@DAT_TYP", ddldattyp.SelectedValue.ToString().Trim());
                    }
                    string a1 = (string)(Session["a"]);
                    if (a1 == "1" && ddldattyp.SelectedValue != "60")
                    {
                        if (String.IsNullOrEmpty(txtSize.Text))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Size');", true);
                            return;

                        }
                        else
                        {
                            ht.Add("@SIZE", txtSize.Text);
                        }
                    }
                    else
                    {
                        ht.Add("@SIZE", txtSize.Text);
                    }
                    if (ddldattyp.SelectedValue == "106" || ddldattyp.SelectedValue == "108")
                    {
                        if (String.IsNullOrEmpty(txtprec.Text))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Precision');", true);
                            return;

                        }
                        else
                        {
                            ht.Add("@PREC", txtprec.Text);
                        }
                    }
                    else
                    {
                        ht.Add("@PREC", txtprec.Text);
                    }
                    if (ddlcoltyp.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select column type');", true);
                        return;

                    }
                    else
                    {
                        ht.Add("@COL_TYP", ddlcoltyp.SelectedValue.ToString().Trim());
                    }
                    if (ddlabsflg.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please select abs flag');", true);
                        return;

                    }
                    else
                    {
                        ht.Add("@ABS_FLG", ddlabsflg.SelectedValue.ToString().Trim());
                    }

                    if (String.IsNullOrEmpty(txteffdatecol.Text.Trim().ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Effective Date');", true);
                        return;

                    }
                    else
                    {
                        ht.Add("@EFF_DTIM1", Convert.ToDateTime(txteffdatecol.Text.Trim()).ToString("MM/dd/yyyy"));
                    }

                    if (rdSingleCycle.Checked == false && rdSingleCycle1.Checked == false)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please check atleast one 0ption');", true);
                        return;

                    }
                    else
                    {
                        if (rdSingleCycle.Checked == true)
                        {
                            ht.Add("@IsIdentity", "1");
                        }

                        if (rdSingleCycle1.Checked == true)
                        {
                            ht.Add("@IsIdentity", "0");
                        }
                    }
                    if (rdIsPrimary.Checked == false && rdIsPrimary1.Checked == false)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please select Is Primary field value');", true);
                        return;

                    }
                    else
                    {
                        if (rdIsPrimary.Checked == true)
                        {
                            ht.Add("@IsPrimary", "1");
                        }

                        if (rdIsPrimary1.Checked == true)
                        {
                            ht.Add("@IsPrimary", "0");
                        }
                    }
                    if (rdIsAvailable.Checked == false && rdIsAvailable1.Checked == false)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please select Is Nullable field value');", true);
                        return;

                    }
                    else
                    {
                        if (rdIsAvailable.Checked == true)
                        {
                            ht.Add("@IsAvailable", "1");
                        }

                        if (rdIsAvailable1.Checked == true)
                        {
                            ht.Add("@IsAvailable", "0");
                        }
                    }
                    if (rdIsforkey.Checked == false && rdIsforkey1.Checked == false)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please select Is Foreign key field value');", true);
                        return;

                    }
                    else
                    {
                        if (rdIsforkey.Checked == true)
                        {
                            ht.Add("@IsFor", "1");
                            if (ddlFortbl.SelectedIndex == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Foreign Table');", true);
                                return;

                            }
                            else
                            {
                                ht.Add("@FOR_TBL", ddlFortbl.SelectedValue.ToString().Trim());
                            }
                            if (ddlForCol.SelectedIndex == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Foreign Column');", true);
                                return;

                            }
                            else
                            {
                                ht.Add("@FOR_COL", ddlForCol.SelectedValue.ToString().Trim());
                            }
                        }

                        if (rdIsforkey1.Checked == true)
                        {
                            ht.Add("@IsFor", "0");
                        }
                    }
                    if (ddlstatus1.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Status');", true);
                        return;

                    }
                    else
                    {
                        ht.Add("@STATUS1", ddlstatus1.SelectedValue.ToString().Trim());
                    }
                    ht.Add("@FLAG", "S");
                    ht.Add("@OBJ_ID", OBJ_ID1.ToString().Trim());
                    ht.Add("@TBL_NME", tablename1.ToString().Trim());
                    ht.Add("@CREATEDBY", HttpContext.Current.Session["UserID"].ToString().Trim());

                }
               DataAccessLayer objDAL1 = new DataAccessLayer("INTGRTNConnectionString");
                ds = objDAL1.GetDataSet("PRC_INS_MST_TBL_COL_DTLS", ht);
                if (ds.Tables.Count > 0)
                {
                    string msg1 = string.Empty;
                    msg1 = ds.Tables[0].Rows[0]["MESSAGE"].ToString().Trim();
                    if (msg1 == "FAILED")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "alert('Column already exits');", true);
                        return;
                    }
                    else if (msg1 == "DESCRIPTION")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "alert('Column description already exits');", true);
                        return;
                    }
                }
                else if (btnSave1.Text == "<span class='glyphicon glyphicon-floppy-disk' style='color: White;'></span>  Update")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "alert('Column Updated Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "alert('Column Created Successfully');", true);
                }
                if (btnSave1.Text == "<span class='glyphicon glyphicon-floppy-disk' style='color: White;'></span>  Update")
                {
                    txtColdesc.Text = "";
                    txtSize.Text = "";
                    ddldattyp.SelectedIndex = 0;
                    ddlstatus1.Enabled = false;
                    txtColName.Text = "";
                    txtColName.Enabled = true;
                    rdIsAvailable.Enabled = true;
                    rdSingleCycle.Enabled = true;
                    rdSingleCycle1.Enabled = true;
                    rdIsforkey.Enabled = true;
                    rdIsforkey1.Enabled = true;
                    rdIsAvailable.Enabled = true;
                    rdIsAvailable1.Enabled = true;
                    rdIsPrimary.Enabled = true;
                    rdIsPrimary1.Enabled = true;
                    rdIsforkey.Checked = false;
                    rdIsforkey1.Checked = false;
                    if (rdIsforkey.Checked == false && rdIsforkey1.Checked == false)
                    {
                        ddlForCol.Enabled = false;
                        ddlFortbl.Enabled = false;
                    }
                    txtprec.Text = "";
                    txtprec.Enabled = false;
                    txteffdatecol.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtcsedatecol.Enabled = false;
                    rdIsPrimary.Checked = false;
                    rdIsPrimary1.Checked = false;
                    rdIsAvailable.Checked = false;
                    rdIsAvailable1.Checked = false;
                    rdIsforkey.Checked = false;
                    rdIsforkey1.Checked = false;
                    ddlstatus1.Enabled = false;
                    BindddlStatus(ddlstatus1);
                    ddlstatus1.SelectedIndex = 1;
                    BindddlCltyp(ddlcoltyp);
                    Bindddlabs(ddlabsflg);
                    ddlcoltyp.Enabled = false;
                    ddlabsflg.Enabled = false;
                    ddlcoltyp.SelectedIndex = 2;
                    ddlabsflg.SelectedIndex = 2;
                    txtSize.Enabled = true;
                    btnSave1.Text = "<span class='glyphicon glyphicon-floppy-disk' style='color: White;'></span>  Save";
                    btnClear1.Enabled = true;
                }
                else
                {
                    txtColName.Text = "";
                    txtColdesc.Text = "";
                    txtSize.Text = "";
                    TxtTblDesc.Text = "";
                    ddldattyp.SelectedIndex = 0;
                  //  ddlstatus1.SelectedIndex = 1;
                    rdIsAvailable.Checked = false;
                    rdIsAvailable1.Checked = false;
                    rdIsforkey.Checked = false;
                    rdIsforkey1.Checked = false;
                    rdIsPrimary.Checked = false;
                    rdIsPrimary1.Checked = false;
                    rdSingleCycle.Checked = false;
                    rdSingleCycle1.Checked = false;
                    ddlForCol.SelectedIndex = 0;
                    ddlFortbl.SelectedIndex = 0;
                    ddlcoltyp.SelectedIndex = 2;
                    ddlabsflg.SelectedIndex = 2;
                    ddlcoltyp.Enabled = false;
                    ddlabsflg.Enabled = false;
                    txtSize.Enabled = true;
                    if (rdIsforkey.Checked == false && rdIsforkey1.Checked == false)
                    {
                        ddlForCol.Enabled = false;
                        ddlFortbl.Enabled = false;
                    }
                    txtprec.Text = "";
                    txtprec.Enabled = false;
                }
                GetSrcTblDatacol(dgbassrctblcol);
                checkforidentity();
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void GetSrcTblDatacol(GridView dg)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Clear();
                htParam.Clear();
                string tablename1 = (string)(Session["tablename"]);
                lbltablenameVal.Text = tablename1.ToString().Trim();
                htParam.Add("@TBL_NME", tablename1.ToString().Trim());
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                ds = objDAL.GetDataSet("PRC_BASE_SRC_TBL_COL", htParam);
                dg.DataSource = ds;
                dg.DataBind();
                if (ds.Tables.Count > 0)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        btnpreviouscol.Visible = true;
                        btnnextcol.Visible = true;
                        txtpagecol.Visible = true;

                        if (dgbassrctblcol.PageCount > 1)
                        {
                            btnnextcol.Enabled = true;
                            btnpreviouscol.Enabled = false;
                        }
                        else
                        {
                            btnnextcol.Enabled = false;
                            btnpreviouscol.Enabled = false;
                        }
                    }
                    else
                    {
                        DataTable dt = ds.Tables[0];
                        ShowNoResultFound(dt, dg);
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


        public void bindddlforcolname(DropDownList ddl)
        {
            try
            {
                ds.Clear();
                htParam.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@TBL_NME", ddlFortbl.SelectedValue.ToString().Trim());
                ds = objDAL.GetDataSet("PRC_BASE_SRC_TBL_COL_FOR_TBL_COL", htParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl.DataSource = ds.Tables[0];
                    ddl.DataTextField = "COL_NAM";
                    ddl.DataValueField = "COL_NAM";
                    ddl.DataBind();
                }
                ddl.Items.Insert(0, new ListItem("SELECT", ""));


            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void ddlFortbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFortbl.SelectedValue != "")
            {
                bindddlforcolname(ddlForCol);
            }
            else
            {
                ddlForCol.Items.Clear();
                ddlForCol.Items.Insert(0, new ListItem("SELECT", ""));
            }
            ddlFortbl.Focus();
        }
        protected void btnpreviouscol_Click(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = dgbassrctblcol.PageIndex;
                dgbassrctblcol.PageIndex = pageIndex - 1;
                GetSrcTblDatacol(dgbassrctblcol); 
                txtpagecol.Text = Convert.ToString(Convert.ToInt32(txtpagecol.Text) - 1);
                if (txtpagecol.Text == "1")
                {
                    btnpreviouscol.Enabled = false;
                }
                else
                {
                    btnpreviouscol.Enabled = true;
                }
                btnnextcol.Enabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnnextcol_Click(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = dgbassrctblcol.PageIndex;
                dgbassrctblcol.PageIndex = pageIndex + 1;
                GetSrcTblDatacol(dgbassrctblcol);
                txtpagecol.Text = Convert.ToString(dgbassrctblcol.PageIndex + 1);
                btnpreviouscol.Enabled = true;
                if (txtpagecol.Text == Convert.ToString(dgbassrctblcol.PageCount))
                {
                    btnnextcol.Enabled = false;
                }

                int page = dgbassrctblcol.PageCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnprevious_Click(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = dgbassrctbl.PageIndex;
                dgbassrctbl.PageIndex = pageIndex - 1;
                GetSrcTblData(dgbassrctbl); 
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
                int pageIndex = dgbassrctbl.PageIndex;
                dgbassrctbl.PageIndex = pageIndex + 1;
                GetSrcTblData(dgbassrctbl); 
                txtPage.Text = Convert.ToString(dgbassrctbl.PageIndex + 1);
                btnprevious.Enabled = true;
                if (txtPage.Text == Convert.ToString(dgbassrctbl.PageCount))
                {
                    btnnext.Enabled = false;
                }

                int page = dgbassrctbl.PageCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rdIsforkey_CheckedChanged(object sender, EventArgs e)
        {
            if (rdIsforkey.Checked)
            {
                ddlForCol.Enabled = true;
                ddlFortbl.Enabled = true;
            }
            else
            {
                ddlForCol.Enabled = false;
                ddlFortbl.Enabled = false;
            }
        }

        protected void rdIsforkey1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdIsforkey1.Checked)
            {
                ddlForCol.Enabled = false;
                ddlFortbl.Enabled = false;
            }
            else
            {
                ddlForCol.Enabled = true;
                ddlFortbl.Enabled = true;

            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
           
                txtTblNmae.Text = "";
                TxtTblDesc.Text = "";
            
        }
        protected void btnClear1_Click(object sender, EventArgs e)
        {
            txtColName.Text = "";
            txtColdesc.Text = "";
            txtSize.Text = "";
            TxtTblDesc.Text = "";
            ddldattyp.SelectedIndex = 0;
            rdIsAvailable.Checked = false;
            rdIsAvailable1.Checked = false;
            rdIsforkey.Checked = false;
            rdIsforkey1.Checked = false;
            rdIsPrimary.Checked = false;
            rdIsPrimary1.Checked = false;
            ddlForCol.SelectedIndex = 0;
            ddlFortbl.SelectedIndex = 0;
            if (rdIsforkey.Checked == false && rdIsforkey1.Checked == false)
            {
                ddlForCol.Enabled = false;
                ddlFortbl.Enabled = false;
            }
        }

        public void bindddlcoldattypEdit(DropDownList ddl)
        {
            try
            {
                ds.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                string DAT_TYP_ID = (string)(Session["DAT_TYP_ID"]);
                ds = objDAL.GetDataSet("PRC_BASE_SRC_TBL_COL_DAT_TYP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl.DataSource = ds.Tables[0];
                    ddl.DataTextField = "DATA_TYP";
                    ddl.DataValueField = "DATA_TYP_ID";
                    ddl.DataBind();
                }
                ddl.Items.Insert(0, new ListItem("SELECT", ""));
                if (DAT_TYP_ID == "127")
                {
                    ddl.SelectedIndex = 1;
                }
                if (DAT_TYP_ID == "173")
                {
                    ddl.SelectedIndex = 2;
                }
                if (DAT_TYP_ID == "104")
                {
                    ddl.SelectedIndex = 3;
                }
                if (DAT_TYP_ID == "175")
                {
                    ddl.SelectedIndex = 4;
                }
                if (DAT_TYP_ID == "40")
                {
                    ddl.SelectedIndex = 5;
                }
                if (DAT_TYP_ID == "61")
                {
                    ddl.SelectedIndex = 6;
                }
                if (DAT_TYP_ID == "42")
                {
                    ddl.SelectedIndex = 7;
                }
                if (DAT_TYP_ID == "43")
                {
                    ddl.SelectedIndex = 8;
                }
                if (DAT_TYP_ID == "106")
                {
                    ddl.SelectedIndex = 9;
                }
                if (DAT_TYP_ID == "62")
                {
                    ddl.SelectedIndex = 10;
                }
                if (DAT_TYP_ID == "130")
                {
                    ddl.SelectedIndex = 11;
                }
                if (DAT_TYP_ID == "129")
                {
                    ddl.SelectedIndex = 12;
                }
                if (DAT_TYP_ID == "128")
                {
                    ddl.SelectedIndex = 13;
                }
                if (DAT_TYP_ID == "34")
                {
                    ddl.SelectedIndex = 14;
                }
                if (DAT_TYP_ID == "56")
                {
                    ddl.SelectedIndex = 15;
                }
                if (DAT_TYP_ID == "60")
                {
                    ddl.SelectedIndex = 16;
                }
                if (DAT_TYP_ID == "239")
                {
                    ddl.SelectedIndex = 17;
                }
                if (DAT_TYP_ID == "99")
                {
                    ddl.SelectedIndex = 18;
                }
                if (DAT_TYP_ID == "108")
                {
                    ddl.SelectedIndex = 19;
                }
                if (DAT_TYP_ID == "231")
                {
                    ddl.SelectedIndex = 20;
                }
                if (DAT_TYP_ID == "59")
                {
                    ddl.SelectedIndex = 21;
                }
                if (DAT_TYP_ID == "58")
                {
                    ddl.SelectedIndex = 22;
                }
                if (DAT_TYP_ID == "52")
                {
                    ddl.SelectedIndex = 23;
                }
                if (DAT_TYP_ID == "122")
                {
                    ddl.SelectedIndex = 24;
                }
                if (DAT_TYP_ID == "98")
                {
                    ddl.SelectedIndex = 25;
                }
                if (DAT_TYP_ID == "256")
                {
                    ddl.SelectedIndex = 26;
                }
                if (DAT_TYP_ID == "35")
                {
                    ddl.SelectedIndex = 27;
                }
                if (DAT_TYP_ID == "41")
                {
                    ddl.SelectedIndex = 28;
                }
                if (DAT_TYP_ID == "189")
                {
                    ddl.SelectedIndex = 29;
                }
                if (DAT_TYP_ID == "48")
                {
                    ddl.SelectedIndex = 30;
                }
                if (DAT_TYP_ID == "36")
                {
                    ddl.SelectedIndex = 31;
                }
                if (DAT_TYP_ID == "165")
                {
                    ddl.SelectedIndex = 32;
                }
                if (DAT_TYP_ID == "167")
                {
                    ddl.SelectedIndex = 33;
                }
                if (DAT_TYP_ID == "241")
                {
                    ddl.SelectedIndex = 34;
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void lnkedit_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
                HiddenField hdncoltyp = (HiddenField)row.FindControl("hdncoltyp");
                string col_typ = hdncoltyp.Value.ToString().Trim();
                if (col_typ == "A")
                {
                    btnSave1.Text = "<span class='glyphicon glyphicon-floppy-disk' style='color: White;'></span>  Update";
                    btnClear1.Enabled = false;
                    txteffdatecol.Enabled = false;
                    txtcsedatecol.Enabled = true;
                    DataSet ds = new DataSet();
                    HiddenField hdncolnam = (HiddenField)row.FindControl("hdncolnam");
                    HiddenField hdntblobjid = (HiddenField)row.FindControl("hdntblobjid");
                    htParam.Clear();
                    ds.Clear();
                    objDAL = new DataAccessLayer("INTGRTNConnectionString");
                    htParam.Add("@COL_NME", hdncolnam.Value.ToString().Trim());
                    htParam.Add("@OBJ_ID", hdntblobjid.Value.ToString().Trim());
                    ds = objDAL.GetDataSet("PRC_FILLCOLEDIT", htParam);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        txtColName.Text = ds.Tables[0].Rows[0]["COL_NAM"].ToString();
                        Session["ColName"] = ds.Tables[0].Rows[0]["COL_NAM"].ToString();
                        String identity = ds.Tables[0].Rows[0]["IS_IDENTITY"].ToString();
                        String primary = ds.Tables[0].Rows[0]["IS_PRIMARY"].ToString();
                        String nullable = ds.Tables[0].Rows[0]["IS_NULLABLE"].ToString();
                        String foreign = ds.Tables[0].Rows[0]["IS_FOREIGN_KEY"].ToString();
                        txtColdesc.Text = ds.Tables[0].Rows[0]["COL_DESC"].ToString();
                        Session["Status"] = ds.Tables[0].Rows[0]["STATUS1"].ToString();
                        //if (ds.Tables[0].Rows.Count > 0)
                        //{
                        //    ddlstatus1.DataSource = ds.Tables[0];
                        //    ddlstatus1.DataTextField = "STATUS";
                        //    ddlstatus1.DataValueField = "STATUS";
                        //    ddlstatus1.DataBind();
                        //}
                        //ddlstatus1.Items.Insert(0, new ListItem("SELECT", ""));
                        //ddlstatus1.SelectedIndex = 1;

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlcoltyp.DataSource = ds.Tables[0];
                            ddlcoltyp.DataTextField = "COL_TYP";
                            ddlcoltyp.DataValueField = "COL_TYP";
                            ddlcoltyp.DataBind();
                        }
                        ddlcoltyp.Items.Insert(0, new ListItem("SELECT", ""));
                        ddlcoltyp.SelectedIndex = 1;

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlabsflg.DataSource = ds.Tables[0];
                            ddlabsflg.DataTextField = "ABS_FLAG";
                            ddlabsflg.DataValueField = "ABS_FLAG";
                            ddlabsflg.DataBind();
                        }
                        ddlabsflg.Items.Insert(0, new ListItem("SELECT", ""));
                        ddlabsflg.SelectedIndex = 1;

                        if (identity == "0")
                        {
                            rdSingleCycle.Checked = false;
                            rdSingleCycle1.Checked = true;
                        }
                        else
                        {
                            rdSingleCycle.Checked = true;
                            rdSingleCycle1.Checked = false;
                        }
                        if (primary == "0")
                        {
                            rdIsPrimary.Checked = false;
                            rdIsPrimary1.Checked = true;
                        }
                        else
                        {
                            rdIsPrimary.Checked = true;
                            rdIsPrimary1.Checked = false;
                        }
                        if (nullable == "0")
                        {
                            rdIsAvailable.Checked = false;
                            rdIsAvailable1.Checked = true;
                        }
                        else
                        {
                            rdIsAvailable.Checked = true;
                            rdIsAvailable1.Checked = false;
                        }
                        if (foreign == "0")
                        {
                            rdIsforkey.Checked = false;
                            rdIsforkey1.Checked = true;
                        }
                        else
                        {
                            rdIsforkey.Checked = true;
                            rdIsforkey1.Checked = false;

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                ddlFortbl.DataSource = ds.Tables[0];
                                ddlFortbl.DataTextField = "FOREIGN_TBL";
                                ddlFortbl.DataValueField = "FOREIGN_TBL";
                                ddlFortbl.DataBind();
                            }
                            ddlFortbl.Items.Insert(0, new ListItem("SELECT", ""));
                            ddlFortbl.SelectedIndex = 1;

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                ddlForCol.DataSource = ds.Tables[0];
                                ddlForCol.DataTextField = "FK_COL_ID";
                                ddlForCol.DataValueField = "FK_COL_ID";
                                ddlForCol.DataBind();
                            }
                            ddlForCol.Items.Insert(0, new ListItem("SELECT", ""));
                            ddlForCol.SelectedIndex = 1;
                        }
                        txteffdatecol.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EFF_DTIM"]).ToString("dd/MM/yyyy");
                        string size = ds.Tables[0].Rows[0]["SIZE_REQ"].ToString();
                        string prec = ds.Tables[0].Rows[0]["PRECISION"].ToString();
                        if (size == "1")
                        {
                            txtSize.Text = ds.Tables[0].Rows[0]["SIZE"].ToString();
                            txtSize.Enabled = true;
                        }
                        else
                        {
                            txtSize.Enabled = false;
                            txtSize.Text = "";
                            txtprec.Text = "";
                            txtprec.Enabled = false;
                        }
                        if (prec != "0")
                        {
                            txtprec.Text = ds.Tables[0].Rows[0]["PRECISION1"].ToString();
                            txtprec.Enabled = true;
                            txtSize.Enabled = true;
                        }
                        else
                        {
                            txtprec.Enabled = false;
                            txtprec.Text = "";
                        }
                        string DAT_TYP_ID = ds.Tables[0].Rows[0]["DATA_TYP_ID"].ToString();
                        Session["DAT_TYP_ID"] = DAT_TYP_ID;
                    }
                    txtColName.Enabled = false;
                    rdSingleCycle.Enabled = false;
                    rdSingleCycle1.Enabled = false;
                    rdIsforkey.Enabled = false;
                    rdIsforkey1.Enabled = false;
                    rdIsAvailable.Enabled = false;
                    rdIsAvailable1.Enabled = false;
                    rdIsPrimary.Enabled = false;
                    rdIsPrimary1.Enabled = false;
                    ddlstatus1.Enabled = true;
                    ddlcoltyp.Enabled = false;
                    ddlabsflg.Enabled = false;
                    BindddlStatus(ddlstatus1);
                    ddlstatus1.SelectedValue = Session["Status"].ToString().Trim();
                    bindddlcoldattypEdit(ddldattyp);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "alert('Default column cannot be changed');", true);
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
    }
}