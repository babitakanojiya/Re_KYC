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
        string flag = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["FLAG"].ToString().Trim() != "")
                {
                    if (Request.QueryString["FLAG"].ToString().Trim() == "Legal")
                    {
                        flag = "L";

                    }

                    if (Request.QueryString["FLAG"].ToString().Trim() == "Individual")
                    {
                        flag = "";

                    }
                    BindErrGrid(flag);
                }
            }
           
        }


        protected void BindErrGrid(string flag)
        {
            try
            {
                DataSet dsSyn = new DataSet();
                dsSyn.Clear();
                Hashtable htSyn = new Hashtable();
                htSyn.Clear();
                objDAL = new DataAccessLayer("UpdDwnldConnectionString");
                htSyn.Add("@flag", flag);
                dsSyn = objDAL.GetDataSet("PRC_GET_TX_Col_ErrorDtls", htSyn);
                grderrdt.DataSource = dsSyn;
                grderrdt.DataBind();

                if (dsSyn.Tables.Count > 0 && dsSyn.Tables[0].Rows.Count > 0)
                {
                    //dsSyn.DataSource = dsSyn.Tables[0];
                    //dsSyn.DataBind();
                    ViewState["gridgrderrdt"] = dsSyn.Tables[0];

                    if (grderrdt.PageCount==1)
                    {
                        txtPage.Text = "1";
                        btnnext.Enabled = false;
                        btnprevious.Enabled = false;
                    }

                    if (grderrdt.PageCount > 1)
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
                System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
                string sRet = oInfo.Name;
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                String LogClassName = method.ReflectedType.Name;
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");

            }
        }

        protected void btnnext_Click(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = grderrdt.PageIndex;
                grderrdt.PageIndex = pageIndex + 1;
                if (Request.QueryString["FLAG"].ToString().Trim() == "Legal")
                {
                     flag = "L";
                   // BindErrGrid(flag);
                }
                if (Request.QueryString["FLAG"].ToString().Trim() == "Individual")
                {
                    flag = "";

                }
                
                BindErrGrid(flag);
                txtPage.Text = Convert.ToString(grderrdt.PageIndex + 1);
                btnprevious.Enabled = true;
                if (txtPage.Text == Convert.ToString(grderrdt.PageCount))
                {
                    btnnext.Enabled = false;
                }

                int page = grderrdt.PageCount;
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
                int pageIndex = grderrdt.PageIndex;
                grderrdt.PageIndex = pageIndex - 1;
                if (Request.QueryString["FLAG"].ToString().Trim() == "Legal")
                {
                    flag = "L";
                    
                }
               if (Request.QueryString["FLAG"].ToString().Trim() == "Individual")
                    {
                        flag = "";

                    }
                    BindErrGrid(flag);
                
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



        //protected void grderrdt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    grderrdt.PageIndex = e.NewPageIndex;
        //    this.BindErrGrid("L");
        //}

        }

    }
