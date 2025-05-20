using KMI.FRMWRK.DAL;
using KMI.FRMWRK.Web.Application.CKYC.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Reflection;
using System.IO;
using System.Globalization;

namespace KMI.FRMWRK.Web.Application.Admin
{
    public partial class JobDetails : System.Web.UI.Page
    {

        DataSet ds = new DataSet();


        CommonUtility cu = new CommonUtility();
        ErrorLog objErr = new ErrorLog();
        int AppId = 0;
        Hashtable htParam = new Hashtable();
       // SqlDataReader dta;

        DataTable dta = new DataTable();
        Hashtable htparam = new Hashtable();
        DataSet dsfill = new DataSet();
        DataSet dsResult = new DataSet();
        string INTG_ID = string.Empty;
        DataAccessLayer objDAL = new DataAccessLayer("INTGRTNConnectionString");
   
        StringBuilder sb = new StringBuilder();
        StringBuilder sb1 = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        StringBuilder sb3 = new StringBuilder();
        StringBuilder sb4 = new StringBuilder();
        DateTime dt = DateTime.Parse(DateTime.Now.TimeOfDay.ToString());


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["INTG_ID"].ToString().Trim() != "")
                    {
                        INTG_ID = Request.QueryString["INTG_ID"].ToString().Trim();
                    }



                    bindJOBDTLS();
                    txtEffFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtJOBName.Text = Request.QueryString["INTG_ID"].ToString().Trim() + "_SYNCH_" + Request.QueryString["SrcTbl"].ToString().Trim();
                    txtStpNm.Text = Request.QueryString["INTG_ID"].ToString().Trim() + "_SYNCH_" + Request.QueryString["SrcTbl"].ToString().Trim();
                    TxtCommand.Text = "exec PRC_SQL_SCH_JOB '" + Request.QueryString["INTG_ID"].ToString().Trim() + "'";



                }

                //  DateTime time = DateTime.Parse(Request.Form[txtTime.UniqueID]);

                //  txtSchTim.Text = DateTime.Now.TimeOfDay.ToString();



                //if (dt.ToString("tt") == "AM")
                //{
                //    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                //}
                //else
                //{
                //    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                //}
                //// timeOccurs.SetTime(dt.Hour, dt.Minute);
                //timeOccurs.SetTime(dt.Hour, dt.Minute, am_pm);
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
                string sRet = oInfo.Name;
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                String LogClassName = method.ReflectedType.Name;
                 objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        public void bindJOBDTLS()
        {
            try
            {
                txtTime.Text = DateTime.Now.ToShortTimeString();
                if (dt.ToString("tt") == "AM")
                {
                    //  am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                }
                else
                {
                    //  am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                }
                // timeOccurs.SetTime(dt.Hour, dt.Minute);
                //  timeOccurs.SetTime(dt.Hour, dt.Minute, am_pm);


                //   DateTime time = DateTime.Parse(Request.Form[txtTime.UniqueID]);


                Fillddl(ddlStatus, "STS", string.Empty);
                ddlStatus.SelectedValue = "1";
                Fillddl(ddlDBName, "DB", string.Empty);
                ddlDBName.SelectedValue = "INTGRTN";
                Fillddl(ddlSchType, "SchTyp", string.Empty);

                Fillddl(ddlfrqdly, "JOB_FRQUNCY", string.Empty);

                //  bindJOBSTEPS();
                fill_JobCode();
                BingGridJobDtls(dgJobDtls);
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
                string sRet = oInfo.Name;
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                String LogClassName = method.ReflectedType.Name;
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void fill_JobCode()
        {
            try
            {
                ds.Clear();
                Hashtable ht = new Hashtable();
                ht.Add("@counterId", "JB_DTLS");
                ds = objDAL.GetDataSet("Prc_GetCTRNO", ht);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtJOBID.Text = ds.Tables[0].Rows[0]["CTRNO"].ToString();

                }

            }
            catch (Exception ex)
            {
                //objErr.LogErr("ISYS-SAIM", "KPIdesc", "fillKPI_MAP_CODE", ex.Message.ToString(), HttpContext.Current.Session["UserID"].ToString().Trim());
                ex.Message.ToString();
            }
        }



        protected void bindJOBSTEPS()
        {
            try
            {
                sb.Append("<div id = 'divJOBStepsBody'>");
                sb.Append("<div id = 'divJOBStepsInnerBody'>");
                sb.Append("<div class='col-sm-2' style='text-align:left'>");
                sb.Append("<label ID = 'lblStepName' Class='control-label required'>Step Name <span class='counter'> 1 </span></Label>");
                sb.Append("</div>");
                sb.Append("<div class='col-sm-3'>");
                sb.Append("<input type = 'text' name='txtStepName' placeholder='Step Name' maxlength='200' id='txtStepName' Class='form-control'>");
                sb.Append("</div>");
                sb.Append("<div class='col-sm-2' style='text-align:left'>");
                sb.Append("<label ID = 'lblProcName' Class='control-label required'>Procedure Name <span class='counter'> 1 </span> </Label>");
                sb.Append("</div>");
                sb.Append("<div class='col-sm-3'>");
                sb.Append("<input type= 'text' name='txtProcName' placeholder='Procedure Name' maxlength='200'  id='txtProcName' Class='form-control'>");
                sb.Append("</div>");
                sb.Append("<div class='col-sm-2'>");
                sb.Append("<input type ='hidden' value='0' id='addJS'>");
                string hiddenVal = "JS";
                string click = "customAddClick(\"divJOBStepsBody\",\"divJOBStepsInnerBody\"," + divJOBSteps.ClientID + ",\"JS\",\"StepName\",\"ProcName\")";
                string btn = String.Format(@"<button id = 'btnAddJS' onclick='{0}' class='icon-button' title='Add' type = 'button'><i style = 'font-size:x-large' class='viewadd' ></i></button>", click);
                sb.Append(btn);
                sb.Append("</div>");
                sb.Append("</div>");

                sb.Append("</div>");
                divJOBSteps.InnerHtml = sb.ToString();
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
                string sRet = oInfo.Name;
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                String LogClassName = method.ReflectedType.Name;
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void Fillddl(DropDownList ddl, string LookupCode, string synNAME)
        {
            try
            {
                ddl.Items.Clear();
                Hashtable ht = new Hashtable();
                ht.Add("@LookupCode", LookupCode);
                ht.Add("@synmNAME", synNAME);
                dta= objDAL.GetDataTable("Prc_GetINTSTFillUPddlVal", ht);
                if (dta.Rows.Count > 0)
                {
                    ddl.DataSource = dta;
                    ddl.DataTextField = "paramdesc";
                    ddl.DataValueField = "paramval";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("SELECT", ""));
                }
            }
            catch (Exception ex)
            {
                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.IO.FileInfo oInfo = new System.IO.FileInfo(currentFile);
                string sRet = oInfo.Name;
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                String LogClassName = method.ReflectedType.Name;
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ds.Clear();
            Hashtable ht = new Hashtable();

            ht.Add("@job_id", txtJOBID.Text.ToString());

            if (txtJOBName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Job Name');", true);
                return;
            }
            else
            {
                ht.Add("@job", txtJOBName.Text.ToString());
            }

            if (ddlDBName.SelectedItem.Text == "SELECT")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Database Name');", true);
                return;
            }
            else
            {
                ht.Add("@database_name", ddlDBName.SelectedItem.Text);
            }

            //if (ddlSchType.SelectedItem.Text == "SELECT")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Schedule Type');", true);
            //    return;
            //}
            //else
            //{
            ht.Add("@SchTyp", ddlSchType.SelectedItem.Value);
            //}

            if (txtStpNm.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Step Name');", true);
                return;
            }
            else
            {
                ht.Add("@step_name", txtStpNm.Text.ToString());
            }
            if (TxtCommand.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Command');", true);
                return;
            }
            else
            {
                ht.Add("@mycommand", TxtCommand.Text.ToString());
            }
            if (ddlSchType.SelectedValue == "O" && txtrcrsdy.Text == "")
            {
                ht.Add("@freqn_interval", System.DBNull.Value);
            }
            else if (ddlSchType.SelectedValue == "D" && txtrcrsdy.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Recur Day Value');", true);
                return;
            }
            else
            {
                ht.Add("@freqn_interval", Convert.ToInt32(txtrcrsdy.Text.ToString()));
            }

            if (ddlSchType.SelectedValue == "D" && ddlfrqdly.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Daily Frequency');", true);
                return;
            }
            else if (ddlSchType.SelectedValue == "O" && ddlfrqdly.SelectedIndex == 0)
            {
                ht.Add("@freqn_sub_day", System.DBNull.Value);
            }
            else
            {
                ht.Add("@freqn_sub_day", ddlfrqdly.SelectedValue.ToString());
            }
            if (ddlSchType.SelectedValue == "O" && fname.Value == "")
            {
                ht.Add("@freqn_subday_interval", System.DBNull.Value); //.Replace(":", "") + "00"
            }
            else if (fname.Value == "" && ddlfrqdly.SelectedValue == "1")
            {
                ht.Add("@freqn_subday_interval", System.DBNull.Value);// txtTime.Text.Replace(":", "") + "00"); //.Replace(":", "") + "00"
            }
            else if (fname.Value == "" && ddlfrqdly.SelectedValue == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Second(s) for Daily Frequency');", true);
                return;
            }
            else if (fname.Value == "" && ddlfrqdly.SelectedValue == "4")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Minute(s) for Daily Frequency')", true);
                return;
            }
            else if (fname.Value == "" && ddlfrqdly.SelectedValue == "8")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Hour(s) for Daily Frequency')", true);
                return;
            }
            else if (Convert.ToInt32(fname.Value.ToString()) > 100 && ddlfrqdly.SelectedValue == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Second(s) Less Than Or Equal To 100')", true);
                return;
            }
            else if (Convert.ToInt32(fname.Value.ToString()) > 100 && ddlfrqdly.SelectedValue == "4")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Minute(s) Less Than Or Equal To 100')", true);
                return;
            }
            else if (Convert.ToInt32(fname.Value.ToString()) > 100 && ddlfrqdly.SelectedValue == "8")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Hour(s) Less Than Or Equal To 100')", true);
                return;
            }
            else if (Convert.ToInt32(fname.Value.ToString()) < 10 && ddlfrqdly.SelectedValue == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Second(s) Greater Than Or Equal To 10')", true);
                return;
            }
            else if (Convert.ToInt32(fname.Value.ToString()) <= 0 && ddlfrqdly.SelectedValue == "4")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Minute(s) Greater Than 0')", true);
                return;
            }
            else if (Convert.ToInt32(fname.Value.ToString()) <= 0 && ddlfrqdly.SelectedValue == "8")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Hour(s) Greater Than 0')", true);
                return;
            }
            else
            {

                ht.Add("@freqn_subday_interval", Convert.ToInt32(fname.Value.ToString()));
            }

            DateTime currentDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime compareDate = Convert.ToDateTime(this.txtEffFrom.Text.Trim(), new CultureInfo("en-GB"));

            if (txtEffFrom.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Schedule Date');", true);
                return;
            }

            else if (currentDate == compareDate || compareDate >= currentDate)
            {
                ht.Add("@startdate", txtEffFrom.Text.Trim());
            }

            else  //(currentDate > compareDate)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Schedule date cannot be backdated');", true);
                return;
                txtEffFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }

            var currentTime = DateTime.Now.ToString("HH:mm"); //Convert.ToDateTime(DateTime.Now.ToString("HH:MM"));
            var comparetime = txtTime.Text.ToString(); //Convert.ToDateTime(this.txtTime.Text.Trim(), new CultureInfo("en-GB"));
            if (txtTime.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Schedule Time');", true);
                return;
            }

            else if (currentTime == comparetime || DateTime.Parse(currentTime) <= DateTime.Parse(txtTime.Text.ToString()) && compareDate == currentDate ||
                DateTime.Parse(currentTime) >= DateTime.Parse(txtTime.Text.ToString()) && compareDate > currentDate
                || DateTime.Parse(currentTime) <= DateTime.Parse(txtTime.Text.ToString()) && compareDate > currentDate)
            {
                ht.Add("@SchTim", txtTime.Text.ToString());
                ht.Add("@starttime", txtTime.Text.Replace(":", "") + "00");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Valid Schedule Time');", true);
                return;
                txtTime.Text = DateTime.Now.ToString("HH:mm");
                // DateTime time = DateTime.Parse(Request.Form[txtTime.UniqueID]);

                //  DateTime occurs_at = DateTime.Parse(string.Format("{0}:{1}:{2}", timeOccurs.Hour, timeOccurs.Minute, timeOccurs.Second));

                //ht.Add("@SchTim", time.TimeOfDay.ToString());
                // ht.Add("@starttime", time.Hour + "" + time.Minute + "00");
                //ht.Add("@SchTim", txtTime.Text.ToString());

                //  string [] time = txtTime.Text.Split(new char[] { ':' });
                //  int addedmin = Convert.ToInt16(time[1]) + 5;
                //int hour = Convert.ToInt16(time[0]);
                //  if (addedmin / 60 != 0)
                //  {
                //      addedmin = addedmin % 60;
                //      hour++;
                //  }


                //ht.Add("@SchTim", hour.ToString() +":"+ addedmin.ToString());
                //ht.Add("@starttime", hour.ToString() + addedmin.ToString() + "00");
            }




            ht.Add("@CREATED_BY", HttpContext.Current.Session["UserID"].ToString().Trim());

            if (ddlStatus.SelectedItem.Text == "SELECT")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Job Status');", true);
                return;
            }
            else
            {
                ht.Add("@STATUS", ddlStatus.SelectedValue.ToString().Trim());
            }

            ht.Add("@INTG_ID", Request.QueryString["INTG_ID"].ToString().Trim());
            //;
            if (txtEffFrom.Text.ToString() == "")
            {

                ht.Add("@EFF_FRM", txtEffFrom.Text);

            }
            else
            {


                ht.Add("@EFF_FRM", Convert.ToDateTime(txtEffFrom.Text.Trim()).ToString("MM/dd/yyyy"));
            }
            if (txtEffTo.Text.ToString() == "")
            {

                ht.Add("@CSE_DTIM", txtEffTo.Text);

            }
            else
            {


                ht.Add("@CSE_DTIM", Convert.ToDateTime(txtEffTo.Text.Trim()).ToString("MM/dd/yyyy"));
            }
            ds = objDAL.GetDataSet("CreateSQLAgentjobs", ht);
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Response"]) == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Job Created Successfully.');", true);



                DataSet dsCtr = new DataSet();
                Hashtable htCtr = new Hashtable();
                htCtr.Add("@counterId", "JB_DTLS");
                dsCtr = objDAL.GetDataSet("Prc_UPDCtrNO", htCtr);
                if (dsCtr.Tables[0].Rows.Count > 0)
                {
                    txtJOBID.Text = dsCtr.Tables[0].Rows[0]["CTRNO"].ToString();

                }
                //  BingGridJobDtls(dgJobDtls);

                btnClear_Click(sender, e);
                txtEffFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                fname.Attributes.Add("placeholder", "");
            }
            else if (Convert.ToInt32(ds.Tables[0].Rows[0]["Response"]) == 2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('JOB Name Already Exist.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Something Went Wrong.');", true);
            }
        }

        protected void BingGridJobDtls(GridView dg)
        {
            try
            {
                DataSet dsGrd = new DataSet();
                Hashtable htGrd = new Hashtable();
                dsGrd.Clear();
                htGrd.Clear();
                htGrd.Add("@INTG_ID", Request.QueryString["INTG_ID"].ToString().Trim());
                dsGrd = objDAL.GetDataSet("PRC_GET_MST_SQL_JOB_DTLS", htGrd);

                dg.DataSource = dsGrd;
                dg.DataBind();

                if (dsGrd.Tables.Count > 0)
                {

                    if (dsGrd.Tables[0].Rows.Count > 0)
                    {
                        dg.DataSource = dsGrd;
                        dg.DataBind();
                        //lblGrid.Visible = false;
                        if (dgJobDtls.PageCount > 1)
                        {
                            btnnext.Enabled = true;
                            btnprevious.Enabled = false;
                        }
                        else
                        {
                            btnnext.Enabled = false;
                            btnprevious.Enabled = false;
                        }

                    }
                    else
                    {
                        //lblGrid.Visible = true;
                        //DataTable dt = dsGrd.Tables[0];
                        //ShowNoResultFound(dt, dg);
                    }
                }
            }
            catch (Exception ex)
            {
                //objErr.LogErr("ISYS-SAIM", "KPIMapBseSrc", "GetSrcTblData", ex.Message.ToString(), HttpContext.Current.Session["UserID"].ToString().Trim());

                ex.Message.ToString();
            }
        }

        private void ShowNoResultFound(DataTable source, GridView gv)
        {
            source.Rows.Add(source.NewRow());

            gv.DataSource = source;
            gv.DataBind();
            int columnsCount = gv.Columns.Count;
            int rowsCount = gv.Rows.Count;
            gv.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
            gv.Rows[0].Cells[columnsCount - 1].Text = "";
            gv.Rows[0].Cells[columnsCount - 2].Text = "";
            gv.Rows[0].Cells[0].Text = "No Job have been defined";

            //source.Rows.Clear();
        }


        protected void btnprevious_Click(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = dgJobDtls.PageIndex;
                dgJobDtls.PageIndex = pageIndex - 1;
                BingGridJobDtls(dgJobDtls);
                txtPage.Text = Convert.ToString(Convert.ToInt32(txtPage.Text) - 1);
                if (txtPage.Text == "1")
                {
                    btnprevious.Enabled = false;
                }
                else
                {
                    btnprevious.Enabled = true;
                }
                btnnext.Enabled = true;
            }
            catch (Exception ex)
            {
              //  objErr.LogErr("ISYS-SAIM", "KPIMapBseSrc", "btnprevious_Click", ex.Message.ToString(), HttpContext.Current.Session["UserID"].ToString().Trim());

                ex.Message.ToString();
            }

        }

        protected void btnnext_Click(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = dgJobDtls.PageIndex;
                dgJobDtls.PageIndex = pageIndex + 1;
                BingGridJobDtls(dgJobDtls);
                txtPage.Text = Convert.ToString(Convert.ToInt32(txtPage.Text) + 1);
                btnprevious.Enabled = true;
                if (txtPage.Text == Convert.ToString(dgJobDtls.PageCount))
                {
                    btnnext.Enabled = false;
                }
                int page = dgJobDtls.PageCount;
            }
            catch (Exception ex)
            {
                //objErr.LogErr("ISYS-SAIM", "KPIMapBseSrc", "btnnext_Click", ex.Message.ToString(), HttpContext.Current.Session["UserID"].ToString().Trim());
                ex.Message.ToString();
            }
        }

        protected void BtnCncl_Click(object sender, EventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            bindJOBDTLS();
            //txtJOBName.Text = "";
            //txtStpNm.Text = "";
            //TxtCommand.Text = "";
            txtEffTo.Text = "";
            txtEffTo.Enabled = false;
            ddlStatus.Enabled = false;
            ddlfrqdly.SelectedIndex = 0;
            txtrcrsdy.Text = "";
            fname.Value = "";
            ddlfrqdly.Enabled = false;
            fname.Disabled = true;
            txtrcrsdy.Enabled = false;

        }

        protected void lnkMapEdit_Click(object sender, EventArgs e)
        {
            btnUpdSTFUL.Attributes.Add("style", "display:inline-block;");
            btnAdd.Attributes.Add("style", "display:none;");
            btnClear.Attributes.Add("style", "display:none;");
            txtJOBName.Enabled = false;
            ddlDBName.Enabled = false;
            ddlSchType.Enabled = false;
            txtStpNm.Enabled = false;
            TxtCommand.Enabled = false;
            txtEffFrom.Enabled = true;
            txtEffTo.Enabled = true;
            ddlStatus.Enabled = true;
            ddlfrqdly.Enabled = true;
            fname.Disabled = true;
            GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
            HiddenField hdnJBCode = (HiddenField)row.FindControl("hdnJBCode");
            Label lblJbName = (Label)row.FindControl("lblJbName");

            DataSet dsGrd = new DataSet();
            Hashtable htGrd = new Hashtable();
            dsGrd.Clear();
            htGrd.Clear();
            htGrd.Add("@INTG_ID", Request.QueryString["INTG_ID"].ToString().Trim());
            htGrd.Add("@Flag", "EDIT");
            htGrd.Add("@SEQNO", hdnJBCode.Value);
            dsGrd = objDAL.GetDataSet("PRC_GET_MST_SQL_JOB_DTLS", htGrd);
            if (dsGrd.Tables.Count > 0)
            {
                if (dsGrd.Tables[0].Rows.Count > 0)
                {
                    txtJOBID.Text = dsGrd.Tables[0].Rows[0]["JOB_ID"].ToString();
                    txtJOBName.Text = dsGrd.Tables[0].Rows[0]["JOB_NM"].ToString();
                    ddlDBName.SelectedItem.Text = dsGrd.Tables[0].Rows[0]["DB_NM"].ToString();
                    ddlDBName.SelectedItem.Value = dsGrd.Tables[0].Rows[0]["DB_NM"].ToString();
                    ddlSchType.SelectedItem.Text = dsGrd.Tables[0].Rows[0]["SCH_TYPDesc"].ToString();
                    ddlSchType.SelectedItem.Value = dsGrd.Tables[0].Rows[0]["SCH_TYP"].ToString();
                    // txtrcrsdy.Text = dsGrd.Tables[0].Rows[0]["FREQ_INTRVL"].ToString();
                    txtStpNm.Text = dsGrd.Tables[0].Rows[0]["STEP_NM"].ToString();
                    TxtCommand.Text = dsGrd.Tables[0].Rows[0]["COMMAND"].ToString();
                    txtEffFrom.Text = dsGrd.Tables[0].Rows[0]["EFF_FRM"].ToString();
                    txtEffTo.Text = dsGrd.Tables[0].Rows[0]["CSE_DTIM"].ToString();
                    Fillddl(ddlStatus, "STS", string.Empty);
                    // ddlStatus.SelectedItem.Text = dsGrd.Tables[0].Rows[0]["StatusDesc"].ToString();
                    //ddlStatus.SelectedItem.Value = dsGrd.Tables[0].Rows[0]["status"].ToString();
                    ddlStatus.SelectedValue = dsGrd.Tables[0].Rows[0]["status"].ToString();
                    DateTime dt = DateTime.Parse(dsGrd.Tables[0].Rows[0]["SCH_TIME"].ToString());
                    ddlfrqdly.SelectedValue = dsGrd.Tables[0].Rows[0]["FREQ_SUBDAY_TYPE"].ToString();
                    txtrcrsdy.Text = dsGrd.Tables[0].Rows[0]["FREQ_INTRVL"].ToString();

                    if (ddlSchType.SelectedItem.Value == "D")
                    {
                        ddlfrqdly.Enabled = true;
                        //fname.Disabled = false;
                    }
                    else
                    {
                        ddlfrqdly.Enabled = false;
                        //fname.Disabled = true;
                    }

                    if (ddlfrqdly.SelectedValue == "1")
                    {
                        //fname.Value = dsGrd.Tables[0].Rows[0]["FREQ_INTRVL"].ToString();
                        fname.Disabled = true;
                        fname.Value = dsGrd.Tables[0].Rows[0]["FREQ_SUBDAY_INTRVL"].ToString();
                    }
                    else
                    {
                        fname.Disabled = false;
                        fname.Value = dsGrd.Tables[0].Rows[0]["FREQ_SUBDAY_INTRVL"].ToString();
                    }
                    //  txtTime.Text = DateTime.Now.ToString("hh:mm");
                    txtTime.Text = dt.ToShortTimeString();


                    // MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
                    if (dt.ToString("tt") == "AM")
                    {
                        //  am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                    }
                    else
                    {
                        //   am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                    }
                    // timeOccurs.SetTime(dt.Hour, dt.Minute);
                    //  timeOccurs.SetTime(dt.Hour, dt.Minute, am_pm);


                }
            }



        }

        protected void btnUpdSTFUL_Click(object sender, EventArgs e)
        {
            ds.Clear();
            Hashtable ht = new Hashtable();

            // DateTime occurs_at = DateTime.Parse(string.Format("{0}:{1}:{2}", timeOccurs.Hour, timeOccurs.Minute, timeOccurs.Second));
            ht.Add("@job_id", txtJOBID.Text.ToString());
            if (txtJOBName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Job Name');", true);
                return;
            }
            else
            {
                ht.Add("@job", txtJOBName.Text.ToString());
            }

            if (ddlDBName.SelectedItem.Text == "SELECT")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Database Name');", true);
                return;
            }
            else
            {
                ht.Add("@database_name", ddlDBName.SelectedItem.Text);
            }

            //if (ddlSchType.SelectedItem.Text == "SELECT")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Schedule Type');", true);
            //    return;
            //}
            //else
            //{
            ht.Add("@SchTyp", ddlSchType.SelectedItem.Value);
            // }

            if (txtStpNm.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Step Name');", true);
                return;
            }
            else
            {
                ht.Add("@step_name", txtStpNm.Text.ToString());
            }
            if (TxtCommand.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Command');", true);
                return;
            }
            else
            {
                ht.Add("@mycommand", TxtCommand.Text.ToString());
            }
            DateTime currentDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime compareDate = Convert.ToDateTime(this.txtEffFrom.Text.Trim(), new CultureInfo("en-GB"));

            if (txtEffFrom.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Schedule Date');", true);
                return;
            }

            else if (currentDate == compareDate || compareDate >= currentDate)
            {
                ht.Add("@startdate", txtEffFrom.Text.Trim());
            }

            else  //(currentDate > compareDate)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Schedule date cannot be backdated');", true);
                return;
            }
            if (ddlSchType.SelectedValue == "O" && txtrcrsdy.Text == "")
            {
                ht.Add("@freqn_interval", System.DBNull.Value);
            }
            else if (ddlSchType.SelectedValue == "D" && txtrcrsdy.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Recur Day Value');", true);
                return;
            }
            else
            {
                ht.Add("@freqn_interval", Convert.ToInt32(txtrcrsdy.Text.ToString()));
            }

            if (ddlSchType.SelectedValue == "D" && ddlfrqdly.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Daily Frequency');", true);
                return;
            }
            else if (ddlSchType.SelectedValue == "O" && ddlfrqdly.SelectedIndex == 0)
            {
                ht.Add("@freqn_sub_day", System.DBNull.Value);
            }
            else
            {
                ht.Add("@freqn_sub_day", ddlfrqdly.SelectedValue.ToString());
            }

            if (fname.Value == "")
            {
                ht.Add("@freqn_subday_interval", System.DBNull.Value);
            }
            else if (fname.Value == "" && ddlfrqdly.SelectedValue == "1")
            {
                ht.Add("@freqn_subday_interval", System.DBNull.Value); //.Replace(":", "") + "00"
            }
            else if (fname.Value == "" && ddlfrqdly.SelectedValue == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Second(s) for Daily Frequency');", true);
                return;
            }
            else if (fname.Value == "" && ddlfrqdly.SelectedValue == "4")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Minute(s) for Daily Frequency')", true);
                return;
            }
            else if (fname.Value == "" && ddlfrqdly.SelectedValue == "8")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Hour(s) for Daily Frequency')", true);
                return;
            }
            else if (Convert.ToInt32(fname.Value.ToString()) > 100 && ddlfrqdly.SelectedValue == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Second(s) Less Than Or Equal To 100')", true);
                return;
            }
            else if (Convert.ToInt32(fname.Value.ToString()) > 100 && ddlfrqdly.SelectedValue == "4")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Minute(s) Less Than Or Equal To 100')", true);
                return;
            }
            else if (Convert.ToInt32(fname.Value.ToString()) > 100 && ddlfrqdly.SelectedValue == "8")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Hour(s) Less Than Or Equal To 100')", true);
                return;
            }
            else if (Convert.ToInt32(fname.Value.ToString()) < 10 && ddlfrqdly.SelectedValue == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Second(s) Greater Than Or Equal To 10')", true);
                return;
            }
            else if (Convert.ToInt32(fname.Value.ToString()) < 0 && ddlfrqdly.SelectedValue == "4")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Minute(s) Greater Than 0')", true);
                return;
            }
            else if (Convert.ToInt32(fname.Value.ToString()) < 0 && ddlfrqdly.SelectedValue == "8")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Hour(s) Greater Than 0')", true);
                return;
            }
            else
            {

                ht.Add("@freqn_subday_interval", Convert.ToInt32(fname.Value.ToString()));
            }
            var currentTime = DateTime.Now.ToString("HH:mm"); //Convert.ToDateTime(DateTime.Now.ToString("HH:MM"));
            var comparetime = txtTime.Text.ToString(); //Convert.ToDateTime(this.txtTime.Text.Trim(), new CultureInfo("en-GB"));
            if (txtTime.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Schedule Time');", true);
                return;
            }

            else if (currentTime == comparetime || DateTime.Parse(currentTime) <= DateTime.Parse(txtTime.Text.ToString()) && compareDate == currentDate ||
                 DateTime.Parse(currentTime) >= DateTime.Parse(txtTime.Text.ToString()) && compareDate > currentDate
                 || DateTime.Parse(currentTime) <= DateTime.Parse(txtTime.Text.ToString()) && compareDate > currentDate)
            {
                ht.Add("@SchTim", txtTime.Text.ToString());
                ht.Add("@starttime", txtTime.Text.Replace(":", "") + "00");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Valid Schedule Time');", true);
                return;
                txtTime.Text = DateTime.Now.ToString("HH:mm");
                // DateTime time = DateTime.Parse(Request.Form[txtTime.UniqueID]);

                //  DateTime occurs_at = DateTime.Parse(string.Format("{0}:{1}:{2}", timeOccurs.Hour, timeOccurs.Minute, timeOccurs.Second));
                //  ht.Add("@SchTim", time.TimeOfDay.ToString());
                // ht.Add("@starttime", time.Hour + "" + time.Minute + "00");

                //ht.Add("@SchTim", txtTime.Text.ToString());
                //ht.Add("@starttime", txtTime.Text.Replace(":", "") + "00");
            }



            ht.Add("@CREATED_BY", HttpContext.Current.Session["UserID"].ToString().Trim());

            if (ddlStatus.SelectedItem.Text == "SELECT")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Select Job Status');", true);
                return;
            }
            else
            {
                ht.Add("@STATUS", ddlStatus.SelectedValue.ToString().Trim());
            }

            ht.Add("@INTG_ID", Request.QueryString["INTG_ID"].ToString().Trim());

            //;
            if (txtEffFrom.Text.ToString() == "")
            {

                ht.Add("@EFF_FRM", txtEffFrom.Text);

            }
            else
            {


                ht.Add("@EFF_FRM", Convert.ToDateTime(txtEffFrom.Text.Trim()).ToString("MM/dd/yyyy"));
            }
            if (txtEffTo.Text.ToString() == "")
            {

                ht.Add("@CSE_DTIM", txtEffTo.Text);

            }
            else
            {


                ht.Add("@CSE_DTIM", Convert.ToDateTime(txtEffTo.Text.Trim()).ToString("MM/dd/yyyy"));
            }
            ds = objDAL.GetDataSet("EnblDsblModifySQLAgentjobs", ht);
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Response"]) == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Job Details Updated Successfully.');", true);


                btnUpdSTFUL.Attributes.Add("style", "display:none;");
                btnAdd.Attributes.Add("style", "display:inline-block;");
                btnClear.Attributes.Add("style", "display:inline-block;");

                //txtJOBName.Enabled = true;
                //ddlDBName.Enabled = true;
                ddlSchType.Enabled = true;
                //txtStpNm.Enabled = true;
                //TxtCommand.Enabled = true;
                txtEffFrom.Enabled = true;
                txtEffTo.Enabled = false;
                ddlStatus.Enabled = false;
                ddlfrqdly.Enabled = false;
                fname.Disabled = true;
                // ddlStatus.SelectedValue = "1";
                txtEffFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnClear_Click(sender, e);
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Something Went Wrong.');", true);
            }
        }

        protected void lnkMapDel_Click(object sender, EventArgs e)
        {



            GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
            HiddenField hdnJBCode = (HiddenField)row.FindControl("hdnJBCode");
            Label lblJbName = (Label)row.FindControl("lblJbName");

            DataSet dsGrd = new DataSet();
            Hashtable htGrd = new Hashtable();
            dsGrd.Clear();
            htGrd.Clear();
            htGrd.Add("@INTG_ID", Request.QueryString["INTG_ID"].ToString().Trim());
            htGrd.Add("@Flag", "DELETE");
            htGrd.Add("@SEQNO", hdnJBCode.Value);
            dsGrd = objDAL.GetDataSet("PRC_GET_MST_SQL_JOB_DTLS", htGrd);
            if (dsGrd.Tables.Count > 0)
            {
                if (dsGrd.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(dsGrd.Tables[0].Rows[0]["Response"]) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Job Deleted Successfully.');", true);
                        btnClear_Click(sender, e);
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Something Went Wrong.');", true);
                    }



                }
            }
        }

        protected void txtJOBName_TextChanged(object sender, EventArgs e)
        {
            txtJOBName.Text = Request.QueryString["INTG_ID"].ToString().Trim() + "_" + txtJOBName.Text.ToString();
        }

        protected void ddlSchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSchType.SelectedValue == "D")
            {
                ddlfrqdly.Enabled = true;
                // fname.Disabled = false;
                txtrcrsdy.Enabled = true;
            }
            else
            {
                ddlfrqdly.Enabled = false;
                ddlfrqdly.SelectedIndex = 0;
                txtrcrsdy.Enabled = false;
                fname.Disabled = true;
                txtrcrsdy.Text = "";
                fname.Value = "";
                fname.Attributes.Add("placeholder", "");
            }
        }
        protected void ddlfrqdly_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlfrqdly.SelectedValue == "2")
            {
                fname.Disabled = false;
                fname.Value = "";
                fname.Attributes.Add("placeholder", "Second(s)");
                fname.Attributes.Add("Min", "10");
                fname.Attributes.Add("Max", "100");
            }
            else if (ddlfrqdly.SelectedValue == "4")
            {
                fname.Disabled = false;
                fname.Value = "";
                fname.Attributes.Add("placeholder", "Minute(s)");
                fname.Attributes.Add("Min", "1");
                fname.Attributes.Add("Max", "100");
            }
            else if (ddlfrqdly.SelectedValue == "8")
            {
                fname.Disabled = false;
                fname.Value = "";
                fname.Attributes.Add("placeholder", "Hour(s)");
            }
            else
            {
                fname.Disabled = true;
                fname.Value = "";
                fname.Attributes.Add("placeholder", "");
            }
        }
    }
}