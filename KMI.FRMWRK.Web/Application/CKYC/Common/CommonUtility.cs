using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KMI.FRMWRK.DAL;
using System.Collections;
using System.Data;



namespace KMI.FRMWRK.Web.Application.CKYC.Common
{
    public class CommonUtility
    {
        #region Declarartion
        DataSet ds;
        DataTable dt;
        Hashtable hTable = new Hashtable();
        ErrorLog objErr = new ErrorLog();
        DataAccessLayer objDAL;

        #endregion

        /// <summary>
        /// Fill no of record Dropdown 
        /// </summary>
        /// <param name="drpList"></param>
        public void FillNoOfRecDropDown(System.Web.UI.WebControls.DropDownList drpList)
        {
            //drpList.Items.Insert(0, new ListItem("10", "10"));
            //drpList.Items.Insert(1, new ListItem("25", "25"));
            //drpList.Items.Insert(2, new ListItem("40", "40"));
            //added by babita on 10 oct

            drpList.Items.Insert(0, new ListItem("50", "50"));
            drpList.Items.Insert(1, new ListItem("60", "60"));
            drpList.Items.Insert(2, new ListItem("70", "70"));
            drpList.Items.Insert(3, new ListItem("80", "80"));
            drpList.Items.Insert(4, new ListItem("90", "90"));
            drpList.Items.Insert(5, new ListItem("100", "100"));
        }



        /// <summary>
        /// dropdown to be fill from ST_IsysLookupParam table by passing Lookup code
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="strLookUpCode"></param>
        public void GetCKYC(DropDownList ddl, string strLookUpCode)
        {
            string strSql = string.Empty;
            objDAL = new DataAccessLayer("CKYCConnectionString");
            dt = new DataTable();
            hTable.Clear();
            hTable.Add("@LookUpCode", strLookUpCode);
            dt = objDAL.GetDataTable("Prc_GetValueCKYC", hTable);
            if (dt.Rows.Count > 0)
            {
                FillDropDown(ddl, dt, "ParamValue", "ParamDesc");
            }
            dt = null;
            strSql = null;
        }

        #region GENERIC METHOD TO FILL DROPDOWN
        /// <summary>
        /// To Fill The DropDown
        /// </summary>
        /// <param name="drpList"></param>
        /// <param name="dtTable"></param>
        /// <param name="strValue"></param>
        /// <param name="strText"></param>
        public void FillDropDown(System.Web.UI.WebControls.DropDownList drpList, DataTable dtTable, string strValue, string strText)
        {
            drpList.Items.Clear();
            drpList.DataSource = dtTable;
            drpList.DataValueField = dtTable.Columns[strValue].ToString();
            drpList.DataTextField = dtTable.Columns[strText].ToString();
            drpList.DataBind();
        }

        public void FillDropDown(DropDownList drpList, DataTable dtTable, string strValue, string strText, bool showSelect = true, string selectText = "Select", string selectValue = "")
        {
            drpList.Items.Clear();
            drpList.DataSource = dtTable;
            drpList.DataValueField = dtTable.Columns[strValue].ToString();
            drpList.DataTextField = dtTable.Columns[strText].ToString();
            drpList.DataBind();

            if (showSelect)
            {
                drpList.Items.Insert(0, new ListItem(selectText, selectValue));
            }
        }

        public void FillDropDown(System.Web.UI.WebControls.ListBox drpList, DataTable dtTable, string strValue, string strText)
        {
            drpList.Items.Clear();
            drpList.DataSource = dtTable;
            drpList.DataValueField = dtTable.Columns[strValue].ToString();
            drpList.DataTextField = dtTable.Columns[strText].ToString();
            drpList.DataBind();
        }

        public void FillDropDown(ListBox drpList, DataTable dtTable, string strValue, string strText, bool showSelect = true, string selectText = "Select", string selectValue = "")
        {
            drpList.Items.Clear();
            drpList.DataSource = dtTable;
            drpList.DataValueField = dtTable.Columns[strValue].ToString();
            drpList.DataTextField = dtTable.Columns[strText].ToString();
            drpList.DataBind();

            if (showSelect)
            {
                drpList.Items.Insert(0, new ListItem(selectText, selectValue));
            }
        }


        #endregion

        #region Get User Details
        /// <summary>
        /// Get User Details
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataSet GetUserDtls(string UserID)
        {
            DataSet ds = new DataSet();
            hTable.Clear();
            hTable.Add("@UserID", UserID);
            ds = objDAL.GetDataSet("prc_GetDtlsFrmIuserMapExt", hTable);
            return ds;

        }
        #endregion

        public DataTable GetLookUpValuesFromUSRMGMT(string LookUpCode)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@LookUpCode", LookUpCode);
            return new DataAccessLayer("DefaultConn").GetDataTable("Prc_GetLookupValueUSRMGMT", ht);
        }

        public void BindLookupFromUSRMGMT(DropDownList ddl, string LookUpCode, bool showSelect = true, string selectText = "Select", string selectValue = "")
        {
            DataTable dt = GetLookUpValuesFromUSRMGMT(LookUpCode);
            FillDropDown(ddl, dt, "ParamValue", "ParamDesc", showSelect, selectText, selectValue);
        }

        public void BindLookupFromUSRMGMT(ListBox ddl, string LookUpCode, bool showSelect = true, string selectText = "Select", string selectValue = "")
        {
            DataTable dt = GetLookUpValuesFromUSRMGMT(LookUpCode);
            FillDropDown(ddl, dt, "ParamValue", "ParamDesc", showSelect, selectText, selectValue);
        }

        public string[] GetIntegrationValues(DropDownList ddl)
        {
            string[] list = new string[ddl.Items.Count];
            int counter = 0;
            foreach (ListItem item in ddl.Items)
            {
                if (item.Selected)
                {
                    list[counter] = item.Value;
                    counter++;
                }
            }
            return list;
        }

        public bool CheckValueSelected(DropDownList ddl, string value)
        {
            bool val = false;
            foreach (ListItem item in ddl.Items)
            {
                if (item.Value == value && item.Selected)
                {
                    val = true;
                    break;
                }
            }
            return val;
        }

        public string[] GetIntegrationValues(ListBox ddl)
        {
            string[] list = new string[ddl.Items.Count];
            int counter = 0;
            foreach (ListItem item in ddl.Items)
            {
                if (item.Selected)
                {
                    list[counter] = item.Value;
                    counter++;
                }
            }
            return list;
        }

        public bool CheckValueSelected(ListBox ddl, string value)
        {
            bool val = false;
            foreach (ListItem item in ddl.Items)
            {
                if (item.Value == value && item.Selected)
                {
                    val = true;
                    break;
                }
            }
            return val;
        }
    }
}