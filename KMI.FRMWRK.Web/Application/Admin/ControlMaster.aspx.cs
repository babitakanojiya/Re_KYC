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

namespace KMI.FRMWRK.Web.Application.Admin
{
    public partial class ControlMaster : System.Web.UI.Page
    {
        CommonUtility cu = new CommonUtility();
        ErrorLog objErr = new ErrorLog();
        int AppId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    cu.GetCKYC(ddlSegment, "Segment");
                    ddlSegment.Items.Insert(0, new ListItem("Select", ""));

                    cu.GetCKYC(ddlControlType, "ControlType");
                    ddlControlType.Items.Insert(0, new ListItem("Select", ""));

                    //cu.GetCKYC(ddlIsMandatory, "IsMandatory");
                    //ddlIsMandatory.Items.Insert(0, new ListItem("Select", ""));

                    cu.GetCKYC(ddlIsMaster, "IsMaster");
                    ddlIsMaster.Items.Insert(0, new ListItem("Select", ""));

                    cu.GetCKYC(ddlIsVisible, "IsVisible");
                    ddlIsVisible.Items.Insert(0, new ListItem("Select", ""));

                    cu.GetCKYC(ddlActive, "IsActive");
                    ddlActive.Items.Insert(0, new ListItem("Select", ""));

                    //cu.GetCKYC(ddlConstitution, "ConstType");
                    //ddlConstitution.Items.Insert(0, new ListItem("Select", ""));

                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "ControlMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }


        public DataTable GetControlsData()
        {
            var dalObj = new DataAccessLayer();
            return dalObj.GetDataTable("Prc_GetControlsSetup", "CKYCConnectionString");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("ControlSetup.aspx?Mode=A");
        }

        public void BindGrid()
        {
            Hashtable ht = new Hashtable();
            ht.Add("@segment", ddlSegment.SelectedValue);
            ht.Add("@ControlName", txtControlName.Text.Trim());
            ht.Add("@ControlId", txtControlID.Text.Trim());
            ht.Add("@ControlType", ddlControlType.Text.Trim());
            //ht.Add("@DataSize", txtDataSize.Text.Trim());
            //ht.Add("@ConstitutionType", ddlConstitution.SelectedValue);
            //ht.Add("@IsMandate", ddlIsMandatory.SelectedValue);
            ht.Add("@IsMaster", ddlIsMaster.SelectedValue);
            //ht.Add("@DatabaseTable", txtTableName.Text.Trim());
            
            //ht.Add("@ColumnValue", txtColumnValue.Text.Trim());
            ht.Add("@IsVisible", ddlIsVisible.SelectedValue);
            //ht.Add("@ContainerDiv", txtContainer.Text.Trim());
            //ht.Add("@Order", txtOrder.Text.Trim());
            ht.Add("@IsActive", ddlActive.SelectedValue);
            DataAccessLayer l = new DataAccessLayer();
            gvControl.DataSource = l.GetDataTable("Prc_GetControlsSetup", ht, "CKYCConnectionString");
            gvControl.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "ControlMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "ControlMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }

        protected void lnkEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                Response.Redirect("ControlSetup.aspx?Mode=E&ID=" + e.CommandArgument, false);
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "ControlMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }

        protected void gvControl_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    SetPagerButtonStates(gvControl, e.Row, this, "ddlPageSelectorL", "ddlPageSelectorR");
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    gvControl.UseAccessibleHeader = true;
                    gvControl.HeaderRow.TableSection = TableRowSection.TableHeader;
                    TableCellCollection cells = gvControl.HeaderRow.Cells;
                    cells[1].Attributes.Add("data-class", "expand");
                    cells[2].Attributes.Add("data-hide", "phone");
                }
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "ControlMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }

        protected void gvControl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvControl.EditIndex = -1;
                gvControl.PageIndex = e.NewPageIndex;
                this.BindGrid();
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "ControlMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }

        protected void gvControl_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                GridView dgSource = (GridView)sender;
                dgSource.EditIndex = -1;
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

                if (e.SortExpression.Equals(strSort))
                {
                    if (strASC.Equals("Yes"))
                    {
                        dgSource.Attributes["SortASC"] = "No";
                    }
                    else
                    {
                        dgSource.Attributes["SortASC"] = "Yes";
                    }
                }
                this.BindGrid();
                //if (ViewState["SearchBindGrid"] != null)
                //{
                //    DataTable dt = (DataTable)ViewState["SearchBindGrid"];
                //    DataView dv = new DataView(dt);
                //    dv.Sort = dgSource.Attributes["SortExpression"];

                //    if (dgSource.Attributes["SortASC"] == "No")
                //    {
                //        dv.Sort += " DESC";
                //    }

                //    dgSource.PageIndex = 0;
                //    dgSource.DataSource = dv;
                //    dgSource.DataBind();
                //    ViewState["SearchBindGrid"] = dv.ToTable();
                //}
                //else

            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "ControlMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }

        public void SetPagerButtonStates(GridView gridView, GridViewRow gvPagerRow, Page page, string DDlPagerL, string DDlPagerR)
        {
            try
            {
                int pageIndexL = gridView.PageIndex;
                int pageCountL = gridView.PageCount;
                int pageIndexR = gridView.PageIndex;
                int pageCountR = gridView.PageCount;//Initialize the variables
                ImageButton btnFirstL = (ImageButton)gvPagerRow.FindControl("ImgbtnFirst");
                ImageButton btnPreviousL = (ImageButton)gvPagerRow.FindControl("ImgbtnPrevious");
                ImageButton btnNextL = (ImageButton)gvPagerRow.FindControl("ImgbtnNext");
                ImageButton btnLastL = (ImageButton)gvPagerRow.FindControl("ImgbtnLast");
                ImageButton btnFirstR = (ImageButton)gvPagerRow.FindControl("ImgbtnFirst1");
                ImageButton btnPreviousR = (ImageButton)gvPagerRow.FindControl("ImgbtnPrevious1");
                ImageButton btnNextR = (ImageButton)gvPagerRow.FindControl("ImgbtnNext1");
                ImageButton btnLastR = (ImageButton)gvPagerRow.FindControl("ImgbtnLast1");//Find the controls
                btnFirstL.Visible = btnPreviousL.Visible = (pageIndexL != 0);
                btnNextL.Visible = btnLastL.Visible = (pageIndexL < (pageCountL - 1));
                btnFirstR.Visible = btnPreviousR.Visible = (pageIndexR != 0);
                btnNextR.Visible = btnLastR.Visible = (pageIndexR < (pageCountR - 1));//Manage the Buttons according to page number
                DropDownList ddlPageSelectorL = (DropDownList)gvPagerRow.FindControl(DDlPagerL);
                ddlPageSelectorL.Items.Clear();
                DropDownList ddlPageSelectorR = (DropDownList)gvPagerRow.FindControl(DDlPagerR);
                ddlPageSelectorR.Items.Clear();//Find Dropdowns
                for (int i = 1; i <= gridView.PageCount; i++)
                {
                    ddlPageSelectorL.Items.Add(i.ToString());
                    ddlPageSelectorR.Items.Add(i.ToString());
                }//Fill those dropdowns
                ddlPageSelectorL.SelectedIndex = pageIndexL;
                ddlPageSelectorR.SelectedIndex = pageIndexR;
                //Initialize the dropdowns
                string strPgeIndx = Convert.ToString(gridView.PageIndex + 1) + " of " + gridView.PageCount.ToString();//Initialize the Page Information.
                Label lblpageindx = (Label)gvPagerRow.FindControl("lblpageindx");
                lblpageindx.Text += strPgeIndx;
                Label lblpageindx2 = (Label)gvPagerRow.FindControl("lblpageindx2");
                lblpageindx2.Text += strPgeIndx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region ddlPageSelectorL Event
        protected void ddlPageSelectorL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvControl.EditIndex = -1;
                gvControl.PageIndex = ((DropDownList)sender).SelectedIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }
        #endregion

        #region ddlPageSelectorR Event
        protected void ddlPageSelectorR_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                gvControl.EditIndex = -1;
                gvControl.PageIndex = ((DropDownList)sender).SelectedIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                objErr.LogErr(1, "CountryMaster.aspx.cs.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), Session["UserID"].ToString(), "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
            }
        }
        #endregion
    }
}