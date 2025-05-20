using KMI.FRMWRK.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace KMI.FRMWRK.Web.Application.CKYC.Common
{
    internal class CommonClass
    {

        int AppID = 0;

        internal void FillDocumentReceived(DropDownList ddl, string ContstitutionType)
        {
            try
            {
                Hashtable htParam = new Hashtable();
                htParam.Add("@LookupCode", "DocReceived");
                htParam.Add("@ParamUsage", ContstitutionType);
                FillDropdowns("prc_getDDLLookUpData", htParam, ddl, "CKYCConnectionString", isSelect: true);
            }
            catch (Exception ex)
            {
                ErrorLog objErr = new ErrorLog();
                objErr.LogErr(AppID, "LegalEntityDtls.aspx.cs", "FillDocumentReceived", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "CKYC");
                throw ex;
            }
        }

        internal void FillIdentificationType(DropDownList ddl, string IdentificationType)
        {
            try
            {
                Hashtable htParam = new Hashtable();
                htParam.Add("@LookupCode", IdentificationType);
                FillDropdowns("prc_getDDLLookUpData", htParam, ddl, "CKYCConnectionString", isSelect: true);
            }
            catch (Exception ex)
            {
                ErrorLog objErr = new ErrorLog();
                objErr.LogErr(AppID, "LegalEntityDtls.aspx.cs", "FillDocumentReceived", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "CKYC");
                throw ex;
            }
        }

        internal void FillDropdowns(string strQuery, Hashtable htable, DropDownList ddl, string strDBKey, string TextField = "ParamDesc1", string ValueField = "ParamValue", bool isSelect = true, string SelectText = "Select", string Selectvalue = "")
        {
            DataTable dt = new DataTable();
            DataAccessLayer objDAL = new DataAccessLayer(strDBKey);
            dt.Clear();
            dt = objDAL.GetDataTable(strQuery, htable, strDBKey);
            if (dt.Rows.Count > 0)
            {
                ddl.Items.Clear();
                ddl.DataSource = dt;
                ddl.DataTextField = TextField;
                ddl.DataValueField = ValueField;
                ddl.DataBind();
            }
            if (isSelect)
                ddl.Items.Insert(0, new ListItem(SelectText, Selectvalue));
        }

        public void FillDropdowns(string strQuery, Hashtable htable, ListBox ddl, string strDBKey, string TextField = "ParamDesc1", string ValueField = "ParamValue", bool isSelect = true, string SelectText = "Select", string Selectvalue = "")
        {
            DataTable dt = new DataTable();
            DataAccessLayer objDAL = new DataAccessLayer(strDBKey);
            dt.Clear();
            dt = objDAL.GetDataTable(strQuery, htable, strDBKey);
            if (dt.Rows.Count > 0)
            {
                ddl.Items.Clear();
                ddl.DataSource = dt;
                ddl.DataTextField = TextField;
                ddl.DataValueField = ValueField;
                ddl.DataBind();
            }
            if (isSelect)
                ddl.Items.Insert(0, new ListItem(SelectText, Selectvalue));
        }


        public void FillState(DropDownList ddl)
        {
            try
            {
                CommonUtility oCommonUtility = new CommonUtility();
                DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
                DataTable dt = objDAL.GetDataTable("Prc_GetStateCodeCKYC");
                oCommonUtility.FillDropDown(ddl, dt, "STATE_CODE", "STATE_Desc", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region If selected Country !=India
        public void ChngStatDistPinOnCountryCode(DropDownList ddlState, TextBox txtddlState
                                               , DropDownList ddlDistrict, TextBox txtddlDistrict
                                               , DropDownList ddlPinCode, TextBox txtddlPinCode, string IsIndiaFlag)
        {
            if (IsIndiaFlag == "N")
            {
                ddlState.Visible = false;
                txtddlState.Visible = true;
                ddlDistrict.Visible = false;
                txtddlDistrict.Visible = true;
                ddlPinCode.Visible = false;
                txtddlPinCode.Visible = true;
            }
            else
            {
                txtddlState.Visible = false;
                ddlState.Visible = true;
                txtddlDistrict.Visible = false;
                ddlDistrict.Visible = true;
                txtddlPinCode.Visible = false;
                ddlPinCode.Visible = true;
            }
        }
        #endregion

        public void MstFillDropdown(string strQuery, Hashtable htable, DropDownList ddl, string strDBKey, bool isSelect)
        {
            DataTable dt = new DataTable();
            DataAccessLayer objDAL = new DataAccessLayer("CKYCConnectionString");
            dt.Clear();
            dt = objDAL.GetDataTable(strQuery, htable, strDBKey);
            if (dt.Rows.Count > 0)
            {
                ddl.Items.Clear();
                ddl.DataSource = dt;
                ddl.DataTextField = "ParamDesc1";
                ddl.DataValueField = "ParamValue";
                ddl.DataBind();
            }
            if (isSelect)
                ddl.Items.Insert(0, new ListItem("Select", ""));
        }
        public string ChkInput_AddMaskingVal(string inputStr, string inputVal)
        {
            if (inputStr == "Proof of Possession of Aadhaar")
            {
                return "XXXXXXXX" + inputVal;
            }
            else if (inputStr == "Offline verification of Aadhaar" || inputStr == "Offline Verification of Aadhaar")
            {
                return "XXXXXXXX" + inputVal;
            }
            else if (inputStr == "E-KYC Authentication")
            {
                return "XXXXXXXX" + inputVal;
            }
            else
            {
                return inputVal;
            }
        }
    }
}