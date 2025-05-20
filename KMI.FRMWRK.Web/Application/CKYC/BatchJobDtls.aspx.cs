using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using KMI.FRMWRK.DAL;
using Newtonsoft.Json;
using System.Collections;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class BatchJobDtls : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //GetBatchJobDate();
            //GetDashBoardDtls();
        }

 
            [WebMethod]
            [System.Web.Script.Services.ScriptMethod]
        public static string GetDashboardData(string flag)
        {
            try
            {
                string FlagID;
                FlagID = flag;

                DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
                DataTable dt = new DataTable();

                Hashtable htParam = new Hashtable();
                htParam.Add("@TabFlag", FlagID);
                dt = objDAL.GetDataTable("Prc_GetDashBoardDtls_test", htParam);

                dt.TableName = "BatchJobCount";
                //string str = "abc";
                string str = JsonConvert.SerializeObject(dt, Formatting.Indented);
                return str;
            }
            catch (Exception e)
            {

                throw;
            }

        }


        //    #region GetBatchJobDate
        //    [WebMethod]
        //public static string GetBatchJobDate()
        //{
        //    try
        //    {
        //        DataAccessLayer dataAccessLayer = new DataAccessLayer("UpdDwnldConnectionString");
        //        DataTable dt = new DataTable();
        //        dt = dataAccessLayer.GetDataTable("Prc_GetDashBoardDtls");
        //        return dt.Rows[0]["Result"].ToString(); 
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //    } 
        //}
        //#endregion

        //#region GetDashBoardDtls
        //[WebMethod]
        //public static string GetDashBoardDtls()
        //{
        //    try
        //    {
        //        DataAccessLayer dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
        //        //DataTable dt = new DataTable();
        //        DataSet ds = new DataSet();
        //        System.Collections.Hashtable ht = new System.Collections.Hashtable();
        //        ht.Add("@UserId", HttpContext.Current.Session["UserId"]);
        //        ds = dataAccessLayer.GetDataSet("Prc_GetDashBoardDtls", ht);
        //        ds.Tables[0].TableName = "BatchJobCount";
        //        string str = JsonConvert.SerializeObject(ds, Formatting.Indented);
        //        // string JSONString = JSONConvert.SerializeObject(ds);
        //        return str;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}
        //#endregion

    }
}