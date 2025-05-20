using System;
using System.Collections;
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
    public partial class PinCodeDtls : System.Web.UI.Page
    {
        #region Declare Veriables
        Hashtable ht;
        DataAccessLayer objDAL;
        ErrorLog objErr = new ErrorLog();
        string strUserId = string.Empty;
        int AppId;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"].ToString() != null)
                {
                    strUserId = Session["UserId"].ToString();
                }
                if (HttpContext.Current.Session["AppId"] != null)
                {
                    AppId = Convert.ToInt32(HttpContext.Current.Session["AppId"]);
                }
                hdnflag.Value = Request.QueryString["flag"].ToString();
                BindPincode();
            }
        }

        #region ddlPincodeList_SelectedIndexChanged
        protected void ddlPincodeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                ht = new Hashtable();
                ht.Add("@StateCode", Request.QueryString["StateCode"].ToString());
                ht.Add("@PinCode", ddlPincodeList.SelectedValue);
                string strdistrict = Convert.ToString(objDAL.ExecuteScalar("Prc_GetDistrictCKYC", ht));
                txtDistrict.Text = strdistrict.ToString();
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    objErr.LogErr(AppId, "PincodeDtls.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
            finally
            {
                //con.Close();
            }

        }
        #endregion

        #region BindPincode
        public void BindPincode()
        {
            try
            {
                string strstate = Request.QueryString["StateCode"].ToString();
                objDAL = new DataAccessLayer("CKYCConnectionString");
                ht = new Hashtable();
                ht.Add("@stateName", strstate);
                DataSet ds = new DataSet();
                ds = objDAL.GetDataSet("Prc_GetPinCKYC", ht);
                ddlPincodeList.DataSource = ds;
                ddlPincodeList.DataTextField = "PIN_CODE";
                ddlPincodeList.DataValueField = "PIN_CODE";
                ddlPincodeList.DataBind();
                ddlPincodeList.Items.Insert(0, new ListItem("Select", string.Empty));
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    objErr.LogErr(AppId, "PincodeDtls.aspx.cs", method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), strUserId, "USRMGMT");
                    throw ex;
                }
            }
            finally
            {
                //con.Close();
            }
        }


        #endregion


        ////[WebMethod]
        ////public static List<string> GetAutoCompleteData(string pincode)
        ////{
        ////    List<string> result = new List<string>();
        ////    using (SqlConnection con = new SqlConnection("Data Source = (local); Database = ckyc; Password = pass@123; User Id = sa; "))
        ////    {
        ////        using (SqlCommand cmd = new SqlCommand("SELECT PIN_CODE FROM ckyc..MS_District  WHERE PIN_CODE LIKE '%{0}%', @SearchText", con))
        ////        {
        ////            con.Open();
        ////            cmd.Parameters.AddWithValue("@SearchText", pincode);
        ////            SqlDataReader dr = cmd.ExecuteReader();
        ////            while (dr.Read())
        ////            {
        ////                result.Add(dr["PIN_CODE"].ToString());
        ////            }
        ////            return result;
        ////        }
        ////    }
        ////}

        //#region GetPincode
        //[WebMethod]
        //public static string GetPincode()
        //{
        //    try
        //    {
        //        DataAccessLayer dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
        //        //DataTable dt = new DataTable();
        //        DataSet ds = new DataSet();
        //        ds = dataAccessLayer.GetDataSet("Prc_GetDashBoardDtls");
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