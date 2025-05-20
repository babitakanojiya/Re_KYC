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
    public partial class MstInsRulSrcTbl : System.Web.UI.Page
    {
        CommonUtility cu = new CommonUtility();
        ErrorLog objErr = new ErrorLog();
        int AppId = 0;
        SqlDataReader sdr;
        DataAccessLayer objDAL = new DataAccessLayer();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        Hashtable htParam = new Hashtable();
        public string INT_ID;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtIntGid.Text = getCTRNotxt("SYNM_MAP_INT_ID");
                    Fillddl(ddlSrcTbl, "srctbl");
                    BindListBox();
                    Fillddl(ddlDATASYNCTYP, "DATASYNCTYP");
                   Fillddl(ddlStatus, "STS");
                    ddlStatus.SelectedIndex = 1;
                    ddlStatus.Enabled = false; //Added by Abuzar on 04112020
                    bindgvMSAST();

                }
                txtIntGid.Attributes.Add("readonly", "readonly");
                txtEffFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtEffTo.Enabled = false;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "fnSetTabs('" + hdnTabIndex.Value.ToString() + "','');", true);
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void Fillddl(DropDownList ddl, string LookupCode)
        {
            try
            {
                ddl.Items.Clear();
                dt.Clear();
                Hashtable ht = new Hashtable();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                ht.Add("@LookupCode", LookupCode);
                dt = objDAL.GetDataTable("Prc_GetSYNMSRCTBLddlVal", ht);
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

        public void BindListBox()
        {
            try
            {
                htParam.Clear();
                ds.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@LookupCode", "bsdonsynm");
                ds = objDAL.GetDataSet("Prc_GetSYNMSRCTBLddlVal", htParam);
                lstbdonSynm.DataSource = ds.Tables[0];
                lstbdonSynm.DataTextField = "paramdesc";
                lstbdonSynm.DataValueField = "paramval";
                lstbdonSynm.DataBind();
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
                ds.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@counterId", ctrid);
                ds = objDAL.GetDataSet("Prc_GetCTRNO", htParam);
                return ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["CTRNO"].ToString().Trim() : "";
            }
            catch (Exception ex)
            {
                return "";
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int alias = 1;
                string alreadyExists = "";
                string bsedOnSYNM = "";
                foreach (ListItem lst in lstbdonSynm.Items)
                {
                    if (lst.Selected == true)
                    {
                        if (bsedOnSYNM == "")
                        {
                            bsedOnSYNM = lst.Value;
                        }
                        else
                        {
                            bsedOnSYNM += "|" + lst.Value;
                        }
                    }
                }
                if (ddlDATASYNCTYP.SelectedIndex == 1)
                {
                    htParam.Clear();
                    ds.Clear();
                    htParam.Add("@INTGRTN_ID", txtIntGid.Text);
                    if (ddlIncreTYp.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please select Increamental type');", true);
                        return;

                    }
                    else
                    {
                        htParam.Add("@INCRMNTL_TYP", ddlIncreTYp.SelectedValue);
                    }
                    if (ddlIncreTYp.SelectedIndex == 1)
                    {
                        if (ddlrefcol.SelectedIndex == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please select Reference Column');", true);
                            return;

                        }
                        else
                        {
                            htParam.Add("@REF_COL", ddlrefcol.SelectedValue);
                        }

                    }
                    else if (ddlIncreTYp.SelectedIndex == 2)
                    {
                        if (ddlrefcol.SelectedIndex == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please select Reference Column');", true);
                            return;

                        }
                        else
                        {
                            htParam.Add("@REF_COL", ddlrefcol.SelectedValue);
                        }
                        if (ddlperfreq.SelectedIndex == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please select Period frequency');", true);
                            return;

                        }
                        else
                        {
                            htParam.Add("@PRD_FRQUNCY", ddlperfreq.SelectedValue);
                        }
                    }
                    else if (ddlIncreTYp.SelectedIndex == 3)
                    {
                        if (ddlprmykeycol.SelectedIndex == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please select Primary key Column');", true);
                            return;

                        }
                        else
                        {
                            htParam.Add("@REF_COL", ddlprmykeycol.SelectedValue);
                        }
                        if (ddlveridcol.SelectedIndex == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please select Version ID Column');", true);
                            return;

                        }
                        else
                        {
                            htParam.Add("@VER_ID_CLMN", ddlveridcol.SelectedValue);
                        }
                    }
                    htParam.Add("@CREATEDBY", HttpContext.Current.Session["UserID"].ToString().Trim());
                    objDAL = new DataAccessLayer("INTGRTNConnectionString");
                    ds = objDAL.GetDataSet("Prc_INS_MST_KPI_INTGRTN_INCRMNTL_TYP", htParam);
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["Response"]) == 0)
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data saved successfully for incremental type.');", true);
                        if (ddlIncreTYp.SelectedIndex == 1)
                        {
                            ddlIncreTYp.SelectedIndex = 0;
                            ddlrefcol.SelectedIndex = 0;
                        }
                        else if (ddlIncreTYp.SelectedIndex == 2)
                        {
                            ddlIncreTYp.SelectedIndex = 0;
                            ddlrefcol.SelectedIndex = 0;
                            ddlperfreq.SelectedIndex = 0;
                            Var1.Attributes.Add("style", "display:none;");
                        }
                        else if (ddlIncreTYp.SelectedIndex == 3)
                        {
                            ddlIncreTYp.SelectedIndex = 0;
                            ddlprmykeycol.SelectedIndex = 0;
                            ddlveridcol.SelectedIndex = 0;
                            Var1.Attributes.Add("style", "display:none;");
                        }
                        lblrefcol.Attributes.Add("style", "display:inline-block;");
                        ddlrefcol.Attributes.Add("style", "display:inline-block;");
                        lblprmykeycol.Attributes.Add("style", "display:none;");
                        ddlprmykeycol.Attributes.Add("style", "display:none;");
                        lblperfreq.Attributes.Add("style", "display:none;");
                        ddlperfreq.Attributes.Add("style", "display:none;");
                        lblveridcol.Attributes.Add("style", "display:none;");
                        ddlveridcol.Attributes.Add("style", "display:none;");
                        div2.Visible = false;
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data already exists for this integration ID.');", true);
                        //return;
                    }
                }
                foreach (ListItem lst in lstbdonSynm.Items)
                {
                    if (lst.Selected == true)
                    {
                        htParam.Clear();
                        ds.Clear();
                        DataAccessLayer objDAL1 =  new DataAccessLayer ("INTGRTNConnectionString");
                        htParam.Add("@INTGRTN_ID", txtIntGid.Text);
                        htParam.Add("@SYNYM_NAME", lst.Value);
                        htParam.Add("@SRC_TBL_ID", ddlSrcTbl.SelectedValue);
                        htParam.Add("@ALIAS", "A" + alias.ToString().Trim());
                        htParam.Add("@CREATEDBY", HttpContext.Current.Session["UserID"].ToString().Trim());
                        htParam.Add("@EFF_DTIM", txtEffFrom.Text);
                        htParam.Add("@CSE_DTIM", txtEffTo.Text);
                        htParam.Add("@STATUS", ddlStatus.SelectedValue);
                        htParam.Add("@BSED_ON_SYNM", bsedOnSYNM);
                        htParam.Add("@DATA_SYNCH_TYP", ddlDATASYNCTYP.SelectedValue);
                        ds = objDAL1.GetDataSet("Prc_INS_MST_KPI_INTGRTN_SYNYM_SRC_MAP_SU", htParam);
                        alias += 1;
                        //if (Convert.ToInt32(dsfill.Tables[0].Rows[0]["Response"]) == 2)
                        //{
                        //    if (alreadyExists == "")
                        //    {
                        //        alreadyExists = lst.Value + " and" + ddlSrcTbl.SelectedValue + " combination already Exists.";
                        //    }
                        //    else
                        //    {
                        //        alreadyExists += " \n " + lst.Value + " and" + ddlSrcTbl.SelectedValue + " combination already Exists.";
                        //    }

                        //}
                        //else
                        //{
                        //    alias += 1;
                        //}
                    }
                }
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Response"]) == 0)
                {
                    //if (ddlDATASYNCTYP.SelectedIndex == 1)
                    //{
                    //    htparam.Clear();
                    //    dsfill.Clear();
                    //    htparam.Add("@INTGRTN_ID", txtIntGid.Text);
                    //    if (ddlIncreTYp.SelectedIndex == 0)
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please select Increamental type');", true);
                    //        return;

                    //    }
                    //    else
                    //    {
                    //        htparam.Add("@INCRMNTL_TYP", ddlIncreTYp.SelectedValue);
                    //    }
                    //    if (ddlIncreTYp.SelectedIndex == 1)
                    //    {
                    //        if (ddlrefcol.SelectedIndex == 0)
                    //        {
                    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please select Reference Column');", true);
                    //            return;

                    //        }
                    //        else
                    //        {
                    //            htparam.Add("@REF_COL", ddlrefcol.SelectedValue);
                    //        }

                    //    }
                    //    else if (ddlIncreTYp.SelectedIndex == 2)
                    //    {
                    //        if (ddlrefcol.SelectedIndex == 0)
                    //        {
                    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please select Reference Column');", true);
                    //            return;

                    //        }
                    //        else
                    //        {
                    //            htparam.Add("@REF_COL", ddlrefcol.SelectedValue);
                    //        }
                    //        if (ddlperfreq.SelectedIndex == 0)
                    //        {
                    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please select Period frequency');", true);
                    //            return;

                    //        }
                    //        else
                    //        {
                    //            htparam.Add("@PRD_FRQUNCY", ddlperfreq.SelectedValue);
                    //        }
                    //    }
                    //    else if (ddlIncreTYp.SelectedIndex == 3)
                    //    {
                    //        if (ddlprmykeycol.SelectedIndex == 0)
                    //        {
                    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please select Primary key Column');", true);
                    //            return;

                    //        }
                    //        else
                    //        {
                    //            htparam.Add("@REF_COL", ddlprmykeycol.SelectedValue);
                    //        }
                    //        if (ddlveridcol.SelectedIndex == 0)
                    //        {
                    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please select Version ID Column');", true);
                    //            return;

                    //        }
                    //        else
                    //        {
                    //            htparam.Add("@VER_ID_CLMN", ddlveridcol.SelectedValue);
                    //        }
                    //    }
                    //    htparam.Add("@CREATEDBY", HttpContext.Current.Session["UserID"].ToString().Trim());
                    //    dsfill = objDal.GetDataSetForPrc_SAIM("Prc_INS_MST_KPI_INTGRTN_INCRMNTL_TYP", htparam);
                    //    if (Convert.ToInt32(dsfill.Tables[0].Rows[0]["Response"]) == 0)
                    //    {
                    //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data saved successfully for incremental type.');", true);
                    //        if (ddlIncreTYp.SelectedIndex == 1)
                    //        {
                    //            ddlIncreTYp.SelectedIndex = 0;
                    //            ddlrefcol.SelectedIndex = 0;
                    //        }
                    //        else if (ddlIncreTYp.SelectedIndex == 2)
                    //        {
                    //            ddlIncreTYp.SelectedIndex = 0;
                    //            ddlrefcol.SelectedIndex = 0;
                    //            ddlperfreq.SelectedIndex = 0;
                    //            Var1.Attributes.Add("style", "display:none;");
                    //        }
                    //        else if (ddlIncreTYp.SelectedIndex == 3)
                    //        {
                    //            ddlIncreTYp.SelectedIndex = 0;
                    //            ddlprmykeycol.SelectedIndex = 0;
                    //            ddlveridcol.SelectedIndex = 0;
                    //            Var1.Attributes.Add("style", "display:none;");
                    //        }
                    //        lblrefcol.Attributes.Add("style", "display:inline-block;");
                    //        ddlrefcol.Attributes.Add("style", "display:inline-block;");
                    //        lblprmykeycol.Attributes.Add("style", "display:none;");
                    //        ddlprmykeycol.Attributes.Add("style", "display:none;");
                    //        lblperfreq.Attributes.Add("style", "display:none;");
                    //        ddlperfreq.Attributes.Add("style", "display:none;");
                    //        lblveridcol.Attributes.Add("style", "display:none;");
                    //        ddlveridcol.Attributes.Add("style", "display:none;");
                    //        div2.Visible = false;
                    //    }
                    //    else
                    //    {
                    //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data already exists for this integration ID.');", true);
                    //        //return;
                    //    }
                    //}
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Data saved successfully.');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Data saved successfully.');", true);
                    htParam.Clear();
                    ds.Clear();
                  DataAccessLayer objDAL2 = new DataAccessLayer ("INTGRTNConnectionString");
                    htParam.Add("@counterId", "SYNM_MAP_INT_ID");
                    ds = objDAL2.GetDataSet("Prc_UPDCtrNO", htParam);
                    txtIntGid.Text = ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["CTRNO"].ToString().Trim() : "";
                    btnClr_Click(EventArgs.Empty, EventArgs.Empty);
                    bindgvMSAST();
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "alert('Something went wrong.');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Something went wrong.');", true);
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
            ddlSrcTbl.SelectedIndex = 0;
            ddlDATASYNCTYP.SelectedIndex = 0;
            lstbdonSynm.SelectedIndex = -1;
            txtEffFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtEffTo.Text = "";
            ddlStatus.SelectedIndex = 1;
            if (ddlIncreTYp.SelectedIndex == 1)
            {
                ddlIncreTYp.SelectedIndex = 0;
                ddlrefcol.SelectedIndex = 0;
            }
            else if (ddlIncreTYp.SelectedIndex == 2)
            {
                ddlIncreTYp.SelectedIndex = 0;
                ddlrefcol.SelectedIndex = 0;
                ddlperfreq.SelectedIndex = 0;
                Var1.Attributes.Add("style", "display:none;");
            }
            else if (ddlIncreTYp.SelectedIndex == 3)
            {
                ddlIncreTYp.SelectedIndex = 0;
                ddlprmykeycol.SelectedIndex = 0;
                ddlveridcol.SelectedIndex = 0;
                Var1.Attributes.Add("style", "display:none;");
            }
            lblrefcol.Attributes.Add("style", "display:inline-block;");
            ddlrefcol.Attributes.Add("style", "display:inline-block;");
            lblprmykeycol.Attributes.Add("style", "display:none;");
            ddlprmykeycol.Attributes.Add("style", "display:none;");
            lblperfreq.Attributes.Add("style", "display:none;");
            ddlperfreq.Attributes.Add("style", "display:none;");
            lblveridcol.Attributes.Add("style", "display:none;");
            ddlveridcol.Attributes.Add("style", "display:none;");
            div2.Visible = false;

        }

        public void bindgvMSAST()
        {
            try
            {
                htParam.Clear();
                ds.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@INTGRTN_ID", "");
                ds = objDAL.GetDataSet("Prc_GETMST_KPI_INTGRTN_SYNYM_SRC_MAP_SU", htParam);
                if (ds.Tables.Count > 0)
                {
                    gvMSAST.DataSource = ds;
                    gvMSAST.DataBind();
                    ViewState["grid"] = ds.Tables[0];

                    btnprevious.Visible = true;
                    btnnext.Visible = true;
                    txtPage.Visible = true;

                    if (gvMSAST.PageCount > 1)
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

        protected void lnkINTGID_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grd = ((LinkButton)sender).NamingContainer as GridViewRow;
                LinkButton lblJN_ID = (LinkButton)grd.FindControl("lnkINTGID");
                Label lblSRC_TBL_COL = (Label)grd.FindControl("lblSRC_TBL_COL");//lblSRC_TBL_COL
                ValueHiddenField.Value = lblJN_ID.Text;
                HdnSrcTbl.Value = lblSRC_TBL_COL.Text;
                INT_ID = lblJN_ID.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), UniqueID, "onINTGCLICK('1','" + lblJN_ID.Text + "','" + lblSRC_TBL_COL.Text + "');", true);
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
                int pageIndex = gvMSAST.PageIndex;
                gvMSAST.PageIndex = pageIndex - 1;
                bindgvMSAST();
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
                int pageIndex = gvMSAST.PageIndex;
                gvMSAST.PageIndex = pageIndex + 1;
                bindgvMSAST();
                txtPage.Text = Convert.ToString(gvMSAST.PageIndex + 1);
                btnprevious.Enabled = true;
                if (txtPage.Text == Convert.ToString(gvMSAST.PageCount))
                {
                    btnnext.Enabled = false;
                }

                int page = gvMSAST.PageCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}