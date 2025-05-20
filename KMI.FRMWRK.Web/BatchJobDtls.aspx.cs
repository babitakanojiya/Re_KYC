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
namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class BatchJobDtls : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetBatchJobDate();
            GetDashBoardDtls();
        }

        #region GetBatchJobDate
        [WebMethod]
        public static string GetBatchJobDate()
        {
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer("UpdDwnldConnectionString");
                DataTable dt = new DataTable();
                dt = dataAccessLayer.GetDataTable("Prc_GetBatchJobTime");
                return dt.Rows[0]["Result"].ToString(); ;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }
        #endregion

        #region GetDashBoardDtls
        [WebMethod]
        public static string GetDashBoardDtls()
        {
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                //DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                System.Collections.Hashtable ht = new System.Collections.Hashtable();
                ht.Add("@UserId", HttpContext.Current.Session["UserId"]);
                ds = dataAccessLayer.GetDataSet("Prc_GetDashBoardDtls",ht);
                ds.Tables[0].TableName = "BatchJobCount";
                string str = JsonConvert.SerializeObject(ds, Formatting.Indented);
                // string JSONString = JSONConvert.SerializeObject(ds);
                return str;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }
        #endregion

    }
}