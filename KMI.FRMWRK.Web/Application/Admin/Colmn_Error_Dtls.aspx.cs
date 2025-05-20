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
    public partial class Colmn_Error_Dtls : System.Web.UI.Page
    {

        CommonUtility cu = new CommonUtility();
        ErrorLog objErr = new ErrorLog();
        int AppId = 0;
        SqlDataReader sdr;
        DataAccessLayer objDAL = new DataAccessLayer();
        DataTable dt = new DataTable();
        Hashtable htParam = new Hashtable();

        protected void Page_Load(object sender, EventArgs e)
        {
            BindErrGrid();
        }


        protected void BindErrGrid()
        {
            try
            {
                DataSet dsSyn = new DataSet();
                dsSyn.Clear();
                Hashtable htSyn = new Hashtable();
                htSyn.Clear();
                objDAL = new DataAccessLayer("UpdDwnldConnectionString");
                dsSyn = objDAL.GetDataSet("PRC_GET_TX_Col_ErrorDtls", htSyn);
                grderrdt.DataSource = dsSyn;
                grderrdt.DataBind();

                if (dsSyn.Tables.Count > 0 && dsSyn.Tables[0].Rows.Count > 0)
                {
                    //dsSyn.DataSource = dsSyn.Tables[0];
                    //dsSyn.DataBind();
                    ViewState["gridgrderrdt"] = dsSyn.Tables[0];
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



        protected void grderrdt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grderrdt.PageIndex = e.NewPageIndex;
            this.BindErrGrid();
        }

        }

    }
