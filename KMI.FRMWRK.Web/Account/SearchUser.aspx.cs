using KMI.FRMWRK.Multilingual;
using KMI.FRMWRK.Web.Admin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMI.FRMWRK.Web.Account
{
    public partial class SearchUser : System.Web.UI.Page
    {
        private MultilingualManager olng;
        private string strUserLang;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLangNum"] == null || Session["LanguageCode"] == null)
            {
                Response.Redirect("~/ErrorSession.aspx");
            }

            strUserLang = HttpContext.Current.Session["UserLangNum"].ToString();
            olng = new MultilingualManager("DefaultConn", "SearchUser.aspx", strUserLang);

            InitializeControl();
            if (!Page.IsPostBack)
            {
                InitCulture();
            }

            GetUser(0);
        }

        private void InitCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-us");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
        }

        private void InitializeControl()
        {
            lblModVer.Text = olng.GetItemDesc("lblModVer.Text");
            lblTitle.Text = olng.GetItemDesc("lblTitle.Text");
            lblUserID.Text = olng.GetItemDesc("lblUserID.Text"); 
            lblUserName.Text = olng.GetItemDesc("lblUserName.Text"); 
            lblStatus.Text = olng.GetItemDesc("lblStatus.Text"); 
            lblReturnRecord.Text = olng.GetItemDesc("lblReturnRecord.Text"); 
        }

        protected void cboStatus_DataBound(object sender, EventArgs e)
        {
            cboStatus.Items.Insert(0, new ListItem("-- ALL --", "ALL"));
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/RegisterUser.aspx?mode=new");
        }
        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Application/Admin/AdmUsrGrpSettings.aspx");
        }
        public void GetUser(int intPageIndex)
        {
            var adminDAL = new AdminDAL();
            var ds = adminDAL.GetSearchUserBy(txtUserID.Text, txtUserName.Text, cboStatus.SelectedItem != null ? cboStatus.SelectedItem.Value : "");

            gvResult.PageSize = int.Parse(cboReturnRecord.Text);
            gvResult.PageIndex = intPageIndex;
            gvResult.DataSource = ds;
            //ObjectDataSource1.DataBind();
            gvResult.DataBind();

            lblPageIndex.Text = "";
            int page = gvResult.PageIndex + 1;

            if (gvResult.Rows.Count < 1)
            {
                lblPageIndex.Text = "";

            }
            else
                lblPageIndex.Text = "Page " + page + " of " + gvResult.PageCount.ToString();
        }
        protected void gvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetUser(e.NewPageIndex);
        }
        protected void ddlPageSelectorR_SelectedIndexChanged(object sender, EventArgs e)
        {
            var adminDAL = new AdminDAL();
            var ds = adminDAL.GetSearchUserBy(txtUserID.Text, txtUserName.Text, cboStatus.SelectedItem.Value);

            gvResult.PageIndex = ((DropDownList)sender).SelectedIndex;
            gvResult.DataSource = ds;
            gvResult.DataBind();
        }
        public void SetPagerButtonStates(GridView gridView, GridViewRow gvPagerRow, Page page, string DDlPagerR)
        {
            int pageIndexL = gridView.PageIndex;
            int pageCountL = gridView.PageCount;
            int pageIndexR = gridView.PageIndex;
            int pageCountR = gridView.PageCount;//Initialize the variables

            ImageButton btnFirstR = (ImageButton)gvPagerRow.FindControl("ImgbtnFirst1");
            ImageButton btnPreviousR = (ImageButton)gvPagerRow.FindControl("ImgbtnPrevious1");
            ImageButton btnNextR = (ImageButton)gvPagerRow.FindControl("ImgbtnNext1");
            ImageButton btnLastR = (ImageButton)gvPagerRow.FindControl("ImgbtnLast1");//Find the controls

            btnFirstR.Visible = btnPreviousR.Visible = (pageIndexR != 0);
            btnNextR.Visible = btnLastR.Visible = (pageIndexR < (pageCountR - 1));//Manage the Buttons according to page number

            DropDownList ddlPageSelectorR = (DropDownList)gvPagerRow.FindControl(DDlPagerR);
            ddlPageSelectorR.Items.Clear();//Find Dropdowns

            for (int i = 1; i <= gridView.PageCount; i++)
            {
                ddlPageSelectorR.Items.Add(i.ToString());
            }//Fill those dropdowns

            ddlPageSelectorR.SelectedIndex = pageIndexR;
            //Initialize the dropdowns

            string strPgeIndx = Convert.ToString(gridView.PageIndex + 1) + " of "
                                + gridView.PageCount.ToString();//Initialize the Page Information.

            Label lblpageindx = (Label)gvPagerRow.FindControl("lblpageindx");
            lblpageindx.Text += strPgeIndx;
            //Fill the Page Information section
        }
        protected void gvResult_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                SetPagerButtonStates(gvResult, e.Row, this, "ddlPageSelectorR");
            }
        }
    }
}