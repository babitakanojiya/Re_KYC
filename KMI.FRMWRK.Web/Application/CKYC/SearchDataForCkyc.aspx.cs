using KMI.FRMWRK.DAL;
using KMI.FRMWRK.Multilingual;
using KMI.FRMWRK.Web.Application.CKYC.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class SearchDataForCkyc : System.Web.UI.Page
    {
        ErrorLog objErr = new ErrorLog();
        DataTable dt;
        private string Message = string.Empty;
        DataAccessLayer dataAccessLayer;
        Hashtable hTable = new Hashtable();
        string strAppID = string.Empty;
        string msg = string.Empty;
        string strModuleID = string.Empty;
        private MultilingualManager olng;
        private string strUserLang;
        CommonUtility oCommonUtility = new CommonUtility();
        DataTable dtResult = new DataTable();
        int AppID;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager scriptmanager = ScriptManager.GetCurrent(this.Page);
                scriptmanager.RegisterPostBackControl(this.btnexceldata);

                if (HttpContext.Current.Session["UserId"] == null)
                {
                    Response.Redirect("~/ErrorSession.aspx", true);
                }

                if (HttpContext.Current.Session["UserId"] == null)
                {
                    Response.Redirect("~/ErrorSession.aspx", true);
                }
                if (Session["AppID"] != null)
                {
                    strAppID = Session["AppID"].ToString();
                }
                if (Session["ModuleID"] != null)
                {
                    strModuleID = Session["ModuleID"].ToString();
                }
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                olng = new MultilingualManager("DefaultConn", "SearchDataForCkyc.aspx", Session["UserLangNum"].ToString());
                strUserLang = HttpContext.Current.Session["UserLangNum"].ToString();
                if (!IsPostBack)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "ShowProgressBar('Loading..Please wait');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "HideProgressBar();", true);

                    //oCommonUtility.FillNoOfRecDropDown(ddlShwRecrds);  // comment by babita on 14 apr 2024
                    trDgViewDtl.Visible = false;
                    PopulateSearchBy();
                }
                //txtIdno.Enabled = false;
            }
            catch (Exception ex)
            {
                objErr.LogErr(Convert.ToInt32(strAppID), "SearchDataForCkyc.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString().Trim(), "CKYC");
            }
            finally
            {
                dataAccessLayer = null;
            }


        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                hdnbachNo.Value = txtbatchno.Text.Trim();
                hdnKycNo.Value = txtKycNo.Text.Trim();
                if (txtDTsearchFrom.Text.ToString().Trim() != "" && txtDTsearchTo.Text.ToString().Trim() != "")
                {
                    if (DateTime.ParseExact(txtDTsearchTo.Text.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture) < DateTime.ParseExact(txtDTsearchFrom.Text.ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture))
                    {
                        msg = "Registration Date From should be less than Registration Date To";
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
                objErr.LogErr(Convert.ToInt32(strAppID), "SearchDataForCkyc.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString().Trim(), "CKYC");
            }
        }
        protected DataTable GetDataTableCKYC()
        {
            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            try
            {
                dt.Clear();
                hTable.Clear();


                hTable.Add("@batchid", txtbatchno.Text.Trim());
                hTable.Add("@KYC_Number_20", txtKycNo.Text.Trim());
                //hTable.Add("@applicantname", txtName.Text.Trim());
                hTable.Add("@identityno", txtidnummm.Text.Trim());
                if (txtDTsearchFrom.Text.Trim() != "")
                {
                    hTable.Add("@CreateFrmDtim", txtDTsearchFrom.Text.Trim());
                }
                else
                {
                    hTable.Add("@CreateFrmDtim ", System.DBNull.Value);
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
                objErr.LogErr(Convert.ToInt32(strAppID), "SearchDataForCkyc.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString().Trim(), "CKYC");
            }
            finally
            {
                dataAccessLayer = null;

            }
            return dt;
        }
        protected void BindDataGrid()
        {
            try
            {
                DataTable dtResult_Kyc = GetDataTableCKYC();

                if (dtResult_Kyc != null && dtResult_Kyc.Rows.Count > 0)
                {
                    // Add the DownloadUrl column if it does not exist
                    if (!dtResult_Kyc.Columns.Contains("DownloadUrl"))
                        dtResult_Kyc.Columns.Add("DownloadUrl", typeof(string));

                    // Populate DownloadUrl for each row
                    foreach (DataRow row in dtResult_Kyc.Rows)
                    {
                        string ckycRef = row["CKYCReferenceNumber"].ToString();


                        //string filePath = ResolveUrl("~/Downloads/" + ckycRef + ".pdf");
                        //row["DownloadUrl"] = filePath;
                        string filePath = ResolveUrl("~/Application/CKYC/DownloadOTPPage.aspx?ckycRef=" + ckycRef + "&showDiv=1");

                        row["DownloadUrl"] = filePath;

                    }

                    // Hide column 4 if exists (as per your existing logic)
                    if (dgView.Columns.Count > 5)
                    {
                        dgView.Columns[4].Visible = false;
                    }

                    // Bind to GridView
                    dgView.DataSource = dtResult_Kyc;
                    dgView.DataBind();

                    // Save to ViewState for paging or postback use (if needed)
                    ViewState["grid"] = dtResult_Kyc;

                    // Show UI elements
                    trDgViewDtl.Visible = true;
                    trtitle.Visible = true;
                    lblMessage.Visible = false;
                    trnote.Visible = true;
                    dgView.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hideDivReg", "document.getElementById('divregistration').style.display='none';", true);
                }
                else
                {
                    dgView.DataSource = null;
                    dgView.DataBind();

                    trDgViewDtl.Visible = false;
                    trtitle.Visible = false;
                    trRecord.Visible = true;
                    lblMessage.Text = "0 Record Found";
                    lblMessage.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showDivReg", "document.getElementById('divregistration').style.display='block';", true);
                }
            }
            catch (Exception ex)
            {
                objErr.LogErr(Convert.ToInt32(strAppID), "SearchDataForCkyc.aspx.cs", "BindDataGrid",
                    ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(),
                    Session["UserID"].ToString().Trim(), "CKYC");
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        protected void GetSearchData(GridView grd)
        {
            try
            {
                if (ViewState["SearchBindGrid"] != null)
                {
                    dt = (DataTable)ViewState["SearchBindGrid"];
                    grd.DataSource = dt;
                    grd.DataBind();
                }
                else
                {
                    this.BindDataGrid();
                }
            }

            catch (Exception ex)
            {
                objErr.LogErr(Convert.ToInt32(strAppID), "SearchDataForCkyc.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString().Trim(), "CKYC");
            }
            finally
            {
                dataAccessLayer = null;
            }
        }
        protected void ddlShwRecrds_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataGrid();
        }

        protected void ddlPageSelectorR_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgView.EditIndex = -1;
                dgView.PageIndex = ((DropDownList)sender).SelectedIndex;
                GetSearchData(dgView);
            }
            catch (Exception ex)
            {
                objErr.LogErr(Convert.ToInt32(strAppID), "SearchDataForCkyc.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString().Trim(), "CKYC");
            }
        }

        protected void ddlPageSelectorL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgView.EditIndex = -1;
                dgView.PageIndex = ((DropDownList)sender).SelectedIndex;
                GetSearchData(dgView);
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr.LogErr(1, "SearchDataForCkyc.aspx.cs", "ddlPageSelectorL_SelectedIndexChanged", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "USRMGMT");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        protected void dgView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dt = GetDataTableCKYC();
                DataView dv = new DataView(dt);
                GridView dgSource = (GridView)sender;
                dgSource.PageIndex = e.NewPageIndex;
                dgSource.DataSource = dv;
                dgSource.DataBind();
                ShowPageInformation();

            }
            catch (Exception ex)
            {
                objErr.LogErr(Convert.ToInt32(strAppID), "SearchDataForCkyc.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString().Trim(), "CKYC");
            }
            finally
            {
                dt = null;
            }
        }

        private void ShowPageInformation()
        {
            try
            {
                int intPageIndex = dgView.PageIndex + 1;
                lblPageInfo.Text = "Page " + intPageIndex.ToString() + " of " + dgView.PageCount;
            }
            catch (Exception ex)
            {
                objErr.LogErr(Convert.ToInt32(strAppID), "SearchDataForCkyc.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString().Trim(), "CKYC");
            }
        }

        protected void dgView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    SetPagerButtonStates(dgView, e.Row, this, "ddlPageSelectorL", "ddlPageSelectorR");
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    dgView.UseAccessibleHeader = true;
                    dgView.HeaderRow.TableSection = TableRowSection.TableHeader;

                    if (dgView.HeaderRow != null && dgView.HeaderRow.Cells.Count >= 7)
                    {
                        TableCellCollection cells = dgView.HeaderRow.Cells;
                        cells[0].Attributes.Add("data-hide", "phone");
                        cells[1].Attributes.Add("data-class", "expand");
                        cells[2].Attributes.Add("data-hide", "phone");
                        cells[3].Attributes.Add("data-hide", "phone");
                        cells[4].Attributes.Add("data-hide", "phone");
                        cells[5].Attributes.Add("data-hide", "phone");
                        cells[6].Attributes.Add("data-hide", "phone,tablet");
                    }
                    else
                    {
                        // Handle the case where the header row or its cells are null or insufficient
                        // For example:
                        // throw new Exception("Header row or cells are null or insufficient.");
                    }
                }
            
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                objErr.LogErr(Convert.ToInt32(strAppID), "SearchDataForCkyc.aspx.cs", "dgView_RowCreated", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString().Trim(), "CKYC");
            }
        }



        public void SetPagerButtonStates(GridView gridView, GridViewRow gvPagerRow, Page page, string DDlPagerL, string DDlPagerR)
        {
            try
            {
                int pageIndexL = gridView.PageIndex;
                int pageCountL = gridView.PageCount;
                int pageIndexR = gridView.PageIndex;
                int pageCountR = gridView.PageCount;

                ImageButton btnFirstL = (ImageButton)gvPagerRow.FindControl("ImgbtnFirst");
                ImageButton btnPreviousL = (ImageButton)gvPagerRow.FindControl("ImgbtnPrevious");
                ImageButton btnNextL = (ImageButton)gvPagerRow.FindControl("ImgbtnNext");
                ImageButton btnLastL = (ImageButton)gvPagerRow.FindControl("ImgbtnLast");
                ImageButton btnFirstR = (ImageButton)gvPagerRow.FindControl("ImgbtnFirst1");
                ImageButton btnPreviousR = (ImageButton)gvPagerRow.FindControl("ImgbtnPrevious1");
                ImageButton btnNextR = (ImageButton)gvPagerRow.FindControl("ImgbtnNext1");
                ImageButton btnLastR = (ImageButton)gvPagerRow.FindControl("ImgbtnLast1");

                btnFirstL.Visible = btnPreviousL.Visible = (pageIndexL != 0);
                btnNextL.Visible = btnLastL.Visible = (pageIndexL < (pageCountL - 1));
                btnFirstR.Visible = btnPreviousR.Visible = (pageIndexR != 0);
                btnNextR.Visible = btnLastR.Visible = (pageIndexR < (pageCountR - 1));

                DropDownList ddlPageSelectorL = (DropDownList)gvPagerRow.FindControl(DDlPagerL);
                ddlPageSelectorL.Items.Clear();
                DropDownList ddlPageSelectorR = (DropDownList)gvPagerRow.FindControl(DDlPagerR);
                ddlPageSelectorR.Items.Clear();

                for (int i = 1; i <= gridView.PageCount; i++)
                {
                    ddlPageSelectorL.Items.Add(i.ToString());
                    ddlPageSelectorR.Items.Add(i.ToString());
                }

                ddlPageSelectorL.SelectedIndex = pageIndexL;
                ddlPageSelectorR.SelectedIndex = pageIndexR;

                string strPgeIndx = Convert.ToString(gridView.PageIndex + 1) + " of "
                                    + gridView.PageCount.ToString();

                Label lblpageindx = (Label)gvPagerRow.FindControl("lblpageindx");
                lblpageindx.Text += strPgeIndx;
                Label lblpageindx2 = (Label)gvPagerRow.FindControl("lblpageindx2");
                lblpageindx2.Text += strPgeIndx;
            }
            catch (Exception ex)
            {
                objErr.LogErr(Convert.ToInt32(strAppID), "SearchDataForCkyc.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString().Trim(), "CKYC");
            }
        }

        protected void dgView_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                dt = GetDataTableCKYC();
                DataView dv = new DataView(dt);
                GridView dgSource = (GridView)sender;
                string strSort = string.Empty;
                string strASC = string.Empty;
                if (dgSource.Attributes["SortExpression"] != null)
                {
                    strSort = dgSource.Attributes["SortExpression"].ToString();
                }
                if (dgSource.Attributes["SortASC"] != null)
                {
                    strASC = dgSource.Attributes["SortASC"].ToString();
                }
                dgSource.Attributes["SortExpression"] = e.SortExpression;
                dgSource.Attributes["SortASC"] = "Yes";
                if (e.SortExpression == strSort)
                {
                    if (strASC == "Yes")
                    {
                        dgSource.Attributes["SortASC"] = "No";
                    }
                    else
                    {
                        dgSource.Attributes["SortASC"] = "Yes";
                    }
                }

                dv.Sort = dgSource.Attributes["SortExpression"];
                if (dgSource.Attributes["SortASC"] == "No")
                {
                    dv.Sort += " DESC";
                }
                dgSource.PageIndex = 0;
                dgSource.DataSource = dv;
                dgSource.DataBind();
                ShowPageInformation();
            }
            catch (Exception ex)
            {
                objErr.LogErr(Convert.ToInt32(strAppID), "SearchDataForCkyc.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString().Trim(), "CKYC");
            }
            finally
            {
                dataAccessLayer = null;

            }
        }

        protected void ddlPageSize_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["grid"] != null)
                {
                    dt = (DataTable)ViewState["grid"];
                    dgView.DataSource = dt;
                    dgView.DataBind();
                }
                else
                    this.BindDataGrid();
            }
            catch (Exception ex)
            {
                objErr.LogErr(Convert.ToInt32(strAppID), "SearchDataForCkyc.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString().Trim(), "CKYC");
            }
            finally
            {
                dataAccessLayer = null;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtbatchno.Text = "";
                txtKycNo.Text = "";
                //txtName.Text = "";
                txtidnummm.Text = "";

                txtDTsearchFrom.Text = "";
                txtDTsearchTo.Text = "";
                
                lblMessage.Visible = false;
                dgView.Visible = false;
               // ddlShwRecrds.SelectedIndex = 0;  // comment  by babita on 14 apr 2024
                trDgViewDtl.Visible = false;
                
            }

            catch (Exception ex)
            {
                objErr.LogErr(Convert.ToInt32(strAppID), "CkycSearch.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString().Trim(), "CKYC");
            }
        }

        protected DataTable GetDataTableCKYCforexpord()
        {
            //DataTable objDt = new DataTable();
            dt = new DataTable();
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            try
            {
                dt.Clear();
                hTable.Clear();

                string myString = null;
                int length = myString?.Length ?? 0; // Safely access the Length property, returns 0 if myString is null.


                hTable.Add("@batchid", txtbatchno.Text.Trim());
                hTable.Add("@KYC_Number_20", txtKycNo.Text.Trim());
                //hTable.Add("@applicantname", txtName.Text.Trim());
                hTable.Add("@identityno", txtidnummm.Text.Trim());
                if (txtDTsearchFrom.Text.Trim() != "")
                {
                    hTable.Add("@CreateFrmDtim", txtDTsearchFrom.Text.Trim());
                }
                else
                {
                    hTable.Add("@CreateFrmDtim ", System.DBNull.Value);
                }
                if (txtDTsearchTo.Text.Trim() != "")
                {
                    hTable.Add("@CreateToDtim", txtDTsearchTo.Text.Trim());
                }
                else
                {
                    hTable.Add("@CreateToDtim", System.DBNull.Value);
                }




                dt = dataAccessLayer.GetDataTable("Prc_GetSearchResponseDataexportinexcel", hTable);
                hTable = null;


            }
            catch (Exception ex)
            {
                objErr.LogErr(Convert.ToInt32(strAppID), "SearchDataForCkyc.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString().Trim(), "CKYC");
            }
            finally
            {
                dataAccessLayer = null;

            }
            return dt;
        }

        public static void ExportCSV(DataTable data, string fileName)
        {
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
                //foreach (DataRow row in data.Rows)
                //{
                //    for (int i = 0; i < data.Columns.Count; i++)
                //    {
                //        if (i > 0)
                //        {
                //            context.Response.Write(",");
                //        }
                //        context.Response.Write(row[i].ToString());
                //    }
                //    context.Response.Write(Environment.NewLine);
                //}
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
                        strWrite = strWrite.Replace("+", "");
                        strWrite = strWrite.Replace("=", "");
                        strWrite = strWrite.Replace("-", "");
                        strWrite = strWrite.Replace("@", "");
                        //strWrite = strWrite.Replace("\r", "");
                        //strWrite = strWrite.Replace(",", "");
                        //strWrite = strWrite.Replace("\"", "");//added by kalpak on 06102015


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

        }


        protected void btnexceldata_Click(object sender, EventArgs e)
        {
            try
            {

                //DataTable data = GetDataTableCKYCforexpord();
                dtResult.Clear();
                dtResult = GetDataTableCKYCforexpord();
                ExportCSV(dtResult, "Report");
                //if (data != null && data.Rows.Count > 0)
                //{

                //    ExportCSV(data, "REGISTRATION_DATA_REPORT");
                //}

            }
            catch (Exception ex)
            {
                //if(ex.Message.ToString() != "Thread was being aborted.")
                //{ 
                objErr.LogErr(Convert.ToInt32(strAppID), "SearchDataForCkyc.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString().Trim(), "CKYC");
                //}
            }

        }

        protected void btnend_Click(object sender, EventArgs e)
        {

        }
        private void PopulateSearchBy()
        {
            try
            {
                oCommonUtility.GetCKYC(ddlSearchby, "SrchBy");
                ddlSearchby.Items.Insert(0, new ListItem("Select", ""));
                oCommonUtility.GetCKYC(ddlProofofidn, "KId");
                ddlProofofidn.Items.Insert(0, new ListItem("Select", ""));
                oCommonUtility.GetCKYC(ddlGender, "KGender");
                ddlGender.Items.Insert(0, new ListItem("Select", ""));
                oCommonUtility.GetCKYC(ddlauthtype, "AutFacType");
                ddlauthtype.Items.Insert(0, new ListItem("Select", ""));
            }
            catch (Exception ex)
            {
                objErr = new ErrorLog();
                objErr.LogErr(AppID, "SearchDownload.aspx.cs", "PopulateSearchBy", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }

        protected void ddlSearchby_SelectedIndexChanged(object sender, EventArgs e)
        {
            div4.Attributes.Add("style", "display:none;");
            if (ddlSearchby.SelectedValue == "1")
            {
                prfofidnty.Attributes.Add("style", "display:block;");
                gender.Attributes.Add("style", "display:none;");
                applcntnme.Attributes.Add("style", "display:none;");
                dividentynum.Attributes.Add("style", "display:block;");
                txtidtnum.Attributes.Add("style", "display:none;");
                ddlProofofidn.SelectedIndex = 0;
                ddlProofofidn.Enabled = true;
            }
            else if (ddlSearchby.SelectedValue == "2")
            {
                prfofidnty.Attributes.Add("style", "display:block;");
                gender.Attributes.Add("style", "display:block;");
                applcntnme.Attributes.Add("style", "display:block;");
                dividentynum.Attributes.Add("style", "display:none;");
                txtidtnum.Attributes.Add("style", "display:block;");
                ddlProofofidn.SelectedValue = "E";
                ddlProofofidn.Enabled = false;
            }
            else
            {
                prfofidnty.Attributes.Add("style", "display:none;");
                gender.Attributes.Add("style", "display:none;");
                applcntnme.Attributes.Add("style", "display:none;");
                dividentynum.Attributes.Add("style", "display:none;");
                txtidtnum.Attributes.Add("style", "display:none;");
            }
        }

        protected void btnregistration_Click(object sender, EventArgs e)
        {
            Response.Redirect("CkycIndReg.aspx?Status=Reg", false);
        }

    }
}