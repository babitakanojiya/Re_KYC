using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using KMI.FRMWRK.DAL;
using System.Data;

namespace KMI.FRMWRK.Web.App_Code
{
    public class VAPT
    {
        //string ModuleID;
        //string IsLoggedIn;
        //string mod;

        public VAPT()
        {


        }

        public void IsValidSession()
        {

            if (HttpContext.Current.Session["UserId"] != null)
            {
                string UserID = HttpContext.Current.Session["UserId"].ToString();
                //Added by Ashish p for VAPT
                Hashtable htable = new Hashtable();
                System.Data.DataSet DSSessionId = new System.Data.DataSet();
                //KMI.DAL.DataAccessLayer objDAL = new KMI.DAL.DataAccessLayer();
                DAL.DataAccessLayer objDal = new DAL.DataAccessLayer();
                DataTable dt = new DataTable();
                dt.Clear();
                //DataAccessLayer Dsa = new DataAccessLayer("CKYCConnectionString");
                objDal = new DataAccessLayer("DefaultConn");
                //if (dt.Rows.Count > 0)
                //{
                htable.Add("@UserID", HttpContext.Current.Session["UserId"].ToString().Trim());
                dt = objDal.GetDataTable("Prc_Admin_GetSessionID", htable);
                    if (dt.Rows.Count > 0)
                    {
                        //string type = dt.Rows[0]["IDENTITY_TYPE"].ToString();
                        if ((dt.Rows[0]["SessionID"].ToString() != HttpContext.Current.Session["SessionId"].ToString()))
                    {
                        HttpContext.Current.Response.Redirect("~/ErrorSession.aspx");
                    }
                }

            }
        }

    }
}