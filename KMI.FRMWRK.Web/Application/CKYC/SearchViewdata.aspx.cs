using System;
using KMI.FRMWRK.DAL;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KMI.FRMWRK.Multilingual;
using System.Data.SqlClient;
using KMI.FRMWRK.Web.Application.CKYC.Common;
using System.Globalization;
using System.Linq;
using System.IO;
using KMI.FRMWRK.Web.Admin;
using System.Configuration;


namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class SearchViewdata : System.Web.UI.Page
    {
        #region Declarartion
        DataTable dt;
        Hashtable hTable = new Hashtable();
        ErrorLog objErr = new ErrorLog();
        DataAccessLayer dataAccessLayer;
        private string Message = string.Empty;
        string strUsrrole = string.Empty;
        private MultilingualManager olng;
        private string strUserLang;
        string strAppID = string.Empty;
        string strModuleID = string.Empty;
        CommonUtility oCommonUtility = new CommonUtility();
        string UserID = string.Empty;
        string msg = string.Empty;
        public string date;
        string kycno;
        string FlagPageTyp = "";
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            dataAccessLayer = null;

        }

        protected void ddlShwRecrds_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataGrid();
        }

        protected void BindDataGrid()
        {
            try
            {
                dgView.PageSize = Convert.ToInt32(ddlShwRecrds.SelectedValue.Trim());
                DataTable dtResult_Kyc = new DataTable();
                dtResult_Kyc = GetDataTableCKYC();
                if (dtResult_Kyc != null)
                {
                    #region BinddataGrid
                    if (dtResult_Kyc.Rows.Count > 0)
                    {
                        dgView.Columns[4].Visible = false;
                        trDgViewDtl.Visible = true;
                        dgView.Visible = true;
                        trtitle.Visible = true;
                        lblMessage.Visible = false;
                        dgView.DataSource = dtResult_Kyc;
                        dgView.DataBind();
                        ViewState["grid"] = dtResult_Kyc;
                        
                        dgView.DataBind();
                        trnote.Visible = true;
                        dgView.Visible = true;
                    }
                    else
                    {
                        trDgViewDtl.Visible = false;
                        trtitle.Visible = false;
                        dgView.DataSource = null;
                        dgView.DataBind();
                        trRecord.Visible = true;
                        lblMessage.Text = "0 Record Found";
                        lblMessage.Visible = true;
                    }
                }
                

            }
            catch (Exception ex)
            {
                objErr.LogErr(Convert.ToInt32(strAppID), "SearchViewdata.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString().Trim(), "CKYC");
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        private void BindGrid(DataSet dt, bool rotate)
        {
            dgView.DataSource = dt;
            dgView.DataBind();
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                hdnbatchno.Value = txtbatchno.Text.Trim();
                hdnKycNo.Value = txtKycNo.Text.Trim();
                if (txtDTsearchFrom.Text.ToString().Trim() != "" && txtDTsearchTo.Text.ToString().Trim() != "")
                {
                    if (DateTime.ParseExact(txtDTsearchTo.Text.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture) < DateTime.ParseExact(txtDTsearchFrom.Text.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture))
                    {
                        msg = "Date From should be less than Registration Date To";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('" + msg + "')", true);
                        return;
                    }
                }

              
                BindDataGrid();
                //added by babita 
                ClientScript.RegisterStartupScript(this.GetType(), "alert12", "showHideDiv('trSearchDetails', 'btnToggle');", true);
            }
            catch (Exception ex)
            {
                objErr.LogErr(Convert.ToInt32(strAppID), "SearchViewdata.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString().Trim(), "CKYC");
            }
        }


        #region ddlIdType_SelectedIndexChanged Event
        protected void ddlIdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlIdType.Text != "Select")
                {
                    txtIdno.Enabled = true;
                }
                else
                {
                    txtIdno.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                objErr.LogErr(Convert.ToInt32(strAppID), "SearchViewdata.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString().Trim(), "CKYC");
            }
        }
        #endregion

       

        #region GetDataTableCKYC
        protected DataTable GetDataTableCKYC()
        {
            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            try
            {
                dt.Clear();
                hTable.Clear();

                hTable.Add("@KYC_Number_20", txtKycNo.Text.Trim());
                hTable.Add("@batchid", txtbatchno.Text.Trim());
                hTable.Add("@identityno", txtPanno.Text.Trim());
                hTable.Add("@applicantname", txtName.Text.Trim());
                
                if (txtDTsearchFrom.Text.Trim() != "")
                {
                    hTable.Add("@CreateFrmDtim", txtDTsearchFrom.Text.Trim());
                }
                else
                {
                    hTable.Add("@CreateFrmDtim", System.DBNull.Value);
                }
                if (txtDTsearchTo.Text.Trim() != "")
                {
                    hTable.Add("@CreateToDtim", txtDTsearchTo.Text.Trim());
                }
                else
                {
                    hTable.Add("@CreateToDtim", System.DBNull.Value);
                }
               
                
                
                dt = dataAccessLayer.GetDataTable("Prc_GetSearchResponseData", hTable);
                    hTable = null;
                

            }
            catch (Exception ex)
            {
                objErr.LogErr(Convert.ToInt32(strAppID), "SearchViewdata.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString().Trim(), "CKYC");
            }
            finally
            {
                dataAccessLayer = null;

            }
            return dt;
        }
        #endregion





    }

}