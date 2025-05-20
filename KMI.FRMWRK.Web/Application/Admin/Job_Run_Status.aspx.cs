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
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Net;

namespace KMI.FRMWRK.Web.Application.Admin
{
    public partial class Job_Run_Status : System.Web.UI.Page
    {
        CommonUtility cu = new CommonUtility();
        ErrorLog objErr = new ErrorLog();
        int AppId = 0;
        SqlDataReader sdr;
        DataAccessLayer objDAL = new DataAccessLayer();
        DataTable dt = new DataTable();
        Hashtable htParam = new Hashtable();
        DataSet DS = new DataSet();
        string INTG_ID = string.Empty;
       


        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
              //  test();
                if (Request.QueryString["INTG_ID"].ToString().Trim() != "")
                {
                    INTG_ID = Request.QueryString["INTG_ID"].ToString().Trim();

                }
                BindParntGrid();
               //BindHistryGrd();
            }
            BindHistryGrd();
        }

        protected void BindParntGrid()
        {
            try
            {
                DataSet dsprnt = new DataSet();
                dsprnt.Clear();
                Hashtable htprnt = new Hashtable();
                htprnt.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htprnt.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                htprnt.Add("@Flag", 2);  
                dsprnt = objDAL.GetDataSet("PRC_TRX_INTGRTN_PRCSNG_DTLS", htprnt);
                dgprntgrd.DataSource = dsprnt;
                dgprntgrd.DataBind();

                if (dsprnt.Tables.Count > 0 && dsprnt.Tables[0].Rows.Count > 0)
                {
                    imgacccycl.Visible = true;
                }
                else
                {
                    imgacccycl.Visible = false;
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }

        }

        protected void BindChildGrid(string lblprcidtrx, GridView gv)
        {
            try
            {
                DataSet dscg = new DataSet();
                dscg.Clear();
                Hashtable htcg = new Hashtable();
                htcg.Clear();
              //  lblprcid
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htcg.Add("@PRCS_ID", lblprcidtrx.ToString()); // lblprcidtrx.ToString());
                dscg = objDAL.GetDataSet("PRC_GET_TRX_INTGRTN_SUB_PRCSNG_DTLS", htcg);
                //dgchild.datasource = dscg;
                //dgchild.databind();
                gv.DataSource = dscg;
                gv.DataBind();
                

               
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

        protected void dgprntgrd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                   // Label lblintgid = (Label)e.Row.FindControl("lblintgid");
                    Label lblprcidtrx = (Label)e.Row.FindControl("lblprcidtrx");
                    GridView dgchild = (GridView)e.Row.FindControl("dgchild");
                    ViewState["PRCS_ID"] = lblprcidtrx.Text;
                  //  lblprcid

                    BindChildGrid(lblprcidtrx.Text.ToString(), dgchild);

                    //Label lblprcidhst = (Label)e.Row.FindControl("lblprcidhst");
                    //GridView dgchildst = (GridView)e.Row.FindControl("dgchildst");

                    //BindChildGridHST(lblprcidhst.Text.ToString(), dgchildst);
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        public int ExportCSV(DataTable data, string fileName)
        {
            int Rest = 0;
            try
            {
                HttpContext context = HttpContext.Current;
                context.Response.Clear();
                context.Response.ContentType = "text/csv";
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".csv");

                //rite column header names
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    if (i > 0)
                    {
                        context.Response.Write(",");
                    }
                    context.Response.Write(data.Columns[i].ColumnName);
                }
                context.Response.Write(Environment.NewLine);
                //Write data
                foreach (DataRow row in data.Rows)
                {
                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        if (i > 0)
                        {
                            //row[i] = row[i].ToString().Replace(",", "");
                            context.Response.Write(",");

                            if (row[i].ToString() == "2252719")
                            {

                                string str = "12042468";
                            }
                        }
                        string strWrite = row[i].ToString();
                        strWrite = strWrite.Replace("<br>", "");
                        strWrite = strWrite.Replace("<br/>", "");
                        strWrite = strWrite.Replace("\n", "");
                        strWrite = strWrite.Replace("\t", "");
                        strWrite = strWrite.Replace("\r", "");
                        strWrite = strWrite.Replace(",", "");
                        strWrite = strWrite.Replace("\"", "");


                        context.Response.Write(strWrite);
                    }
                    context.Response.Write(Environment.NewLine);
                }
                context.Response.Flush();
                context.Response.End();
            }
            catch (Exception ex)
            {

            }
            return Rest;


        }

        protected void imgrun_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                htParam.Clear();
                DataSet dsResult = new DataSet();
                dsResult.Clear();
                GridViewRow row = (sender as ImageButton).NamingContainer as GridViewRow;
                Label lblprcid = row.FindControl("lblprcid") as Label;
                Label lblsbprcdesccld = row.FindControl("lblsbprcdesccld") as Label;
                Label lblsbprcid = row.FindControl("lblsbprcid") as Label;
                Label LBLSUBPRCIDVAL = row.FindControl("LBLSUBPRCIDVAL") as Label;
                GridView dgchild = new GridView();


                objDAL = new DataAccessLayer("INTGRTNConnectionString");

                if (LBLSUBPRCIDVAL.Text == "101")
                {
                    htParam.Add("@PRCS_ID", lblprcid.Text.ToString());
                    htParam.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                    htParam.Add("@CREATEDBY", Session["UserID"].ToString().Trim());
                    dsResult = objDAL.GetDataSet("PRC_INS_INTGRTN_SYNYM_TO_SRC_TBL", htParam);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Execution Done Successfully...!!!'); fnSetTabs('1')", true);
                     
                     BindChildGrid(lblprcid.Text.ToString(), dgchild);
                    BindParntGrid();
                }
                else if (LBLSUBPRCIDVAL.Text == "102")
                {
                    htParam.Add("@PRCS_ID", lblprcid.Text.ToString());
                    htParam.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                    htParam.Add("@CREATEDBY", Session["UserID"].ToString().Trim());
                    dsResult = objDAL.GetDataSet("PRC_INS_INTGRTN_SRC_DATA_PREPARATION", htParam);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Execution Done Successfully...!!!');fnSetTabs('1')", true);
                      BindChildGrid(lblprcid.Text.ToString(), dgchild);
                    BindParntGrid();
                }
                else
                {
                    htParam.Add("@PRCS_ID", lblprcid.Text.ToString());
                    htParam.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                    htParam.Add("@CREATEDBY", Session["UserID"].ToString().Trim());
                    dsResult = objDAL.GetDataSet("PRC_INS_INTGRTN_SRC_DATA_ERROR_HANDALING", htParam);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Execution Done Successfully...!!!');fnSetTabs('1')", true);
                       BindChildGrid(lblprcid.Text.ToString(), dgchild);
                    BindParntGrid();
                }

            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }

        }
        protected void BindHistryGrd()
        {
            try
            {
                DataSet dsprnt = new DataSet();
                dsprnt.Clear();
                Hashtable htprnt = new Hashtable();
                htprnt.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htprnt.Add("@INTGRTN_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                htprnt.Add("@Flag", "");
                dsprnt = objDAL.GetDataSet("PRC_TRX_INTGRTN_PRCSNG_DTLS", htprnt);
                grdhst.DataSource = dsprnt;
                grdhst.DataBind();
                if (dsprnt.Tables.Count > 0 && dsprnt.Tables[0].Rows.Count > 0)
                {
                    //dsSyn.DataSource = dsSyn.Tables[0];
                    //dsSyn.DataBind();
                    ViewState["grdhst"] = dsprnt.Tables[0];

                    if (grdhst.PageCount > 1)
                    {
                        //   btnprevious_hst.Enabled = true;
                    }
                    else
                    {
                        //   btnprevious_hst.Enabled = false;
                    }
                }
                else
                {
                    //btnprevious_hst.Enabled = false;
                    //btnnext_hst.Enabled = false;
                    //txtPage.Text = "1";

                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }

        }
      //  public void test() { return ; }
        protected void BindChildGridHST(string lblprcidhst, GridView gv)
        {
            try
            {
                DataSet dscg = new DataSet();
                dscg.Clear();
                Hashtable htcg = new Hashtable();
                htcg.Clear();
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htcg.Add("@PRCS_ID", lblprcidhst.ToString());
                dscg = objDAL.GetDataSet("PRC_GET_TRX_INTGRTN_SUB_PRCSNG_DTLS", htcg);
                gv.DataSource = dscg;
                gv.DataBind();

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

        protected void grdhst_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lblprcidhst = (Label)e.Row.FindControl("lblprcidhst");
                    GridView dgchildst = (GridView)e.Row.FindControl("dgchildst");

                       BindChildGridHST(lblprcidhst.Text.ToString(), dgchildst);
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }




        protected void imgacccycl_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                htParam.Clear();
                DataSet dsResult = new DataSet();
                dsResult.Clear();
                GridViewRow row = (sender as ImageButton).NamingContainer as GridViewRow;
                GridView dgchild = new GridView();
                    //row.FindControl("dgchild") as GridView;

                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@INTG_ID", Request.QueryString["INTG_ID"].ToString().Trim());

                dsResult = objDAL.GetDataSet("PRC_SQL_SCH_JOB", htParam);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Cycle Has Been Generated...!!!'); fnSetTabs('1')", true);
                BindParntGrid();
                BindChildGrid(ViewState["PRCS_ID"].ToString(), dgchild);

                ViewState["PRCS_ID"] = "";
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void IMGDWNLALLERR_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                htParam.Clear();
                DataSet dsResult = new DataSet();
                dsResult.Clear();
                GridViewRow row = (sender as ImageButton).NamingContainer as GridViewRow;
                Label lblsbprcid = row.FindControl("lblsbprcid") as Label;
                Label lblsbprcdesccld = row.FindControl("lblsbprcdesccld") as Label;
                string filename = string.Empty;
                filename = lblsbprcdesccld.Text + "_Error_Records";
                objDAL = new DataAccessLayer("INTGRTNConnectionString");
                htParam.Add("@SUB_PRC_ID", lblsbprcid.Text.ToString());
                dsResult = objDAL.GetDataSet("PRC_GET_TRX_ERR_IN_PROCESS_DTLS_EXCEL", htParam);
                if (dsResult.Tables.Count > 0)
                {
                    ExportCSV(dsResult.Tables[0], filename);
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        //protected void imgdwnlallerrhs_click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        htparam.clear();
        //        dataset dsresult = new dataset();
        //        dsresult.clear();
        //        gridviewrow row = (sender as imagebutton).namingcontainer as gridviewrow;
        //        label lblsbprcidhhs = row.findcontrol("lblsbprcidhhs") as label;
        //        label lblsbprcdesccldhs = row.findcontrol("lblsbprcdesccldhs") as label;
        //        string filename = string.empty;
        //        filename = lblsbprcdesccldhs.text + "_error_records";
        //        objdal = new dataaccesslayer("intgrtnconnectionstring");
        //        htparam.add("@sub_prc_id", lblsbprcidhhs.text.tostring());
        //        dsresult = objdal.getdataset("prc_get_trx_err_in_process_dtls_excel", htparam);
        //        if (dsresult.tables.count > 0)
        //        {
        //            exportcsv(dsresult.tables[0], filename);
        //        }
        //    }
        //    catch (exception ex)
        //    {
        //        string currentfile = new system.diagnostics.stacktrace(true).getframe(0).getfilename();
        //        system.reflection.methodbase method = system.reflection.methodbase.getcurrentmethod();
        //        objerr.logerr(appid, currentfile, method.name.tostring(), ex.innerexception == null ? ex.message.tostring() : ex.message.tostring() + " | " + ex.innerexception.tostring(), "", "usrmgmt");
        //    }
        //}
    }
}