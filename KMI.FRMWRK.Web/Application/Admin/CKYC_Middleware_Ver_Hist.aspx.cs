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
using System.Data.SqlClient;
using System.Text;
using KMI.FRMWRK.BAL;
using System.Web.Script.Services;
using System.Web.Services;

namespace KMI.FRMWRK.Web.Application.Admin
{
    public partial class CKYC_Middleware_Ver_Hist : System.Web.UI.Page
    {

        CommonUtility cu = new CommonUtility();
        ErrorLog objErr = new ErrorLog();
        int AppId = 0;
        SqlDataReader sdr;
        DataAccessLayer objDAL = new DataAccessLayer();
        DataTable dt = new DataTable();
        Hashtable htParam = new Hashtable();
        string anchid = string.Empty;
        public string StrTimeFlag = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
               
            }
        }

        protected void BindVerHstGrid(string entity)
        {
            try
            {

                DataSet dsSyn = new DataSet();
                dsSyn.Clear();
                Hashtable htSyn = new Hashtable();
                htSyn.Clear();
                string newflag = hdnsubflag.Value.ToString();
                if (hdntimeflag.Value.ToString() != null)
                {
                    StrTimeFlag = hdntimeflag.Value.ToString();
                }
                //string entity = ViewState["hdntype"].ToString(); //hdnindleglflag.Value.ToString();
                //lblgrdhead.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "test('" + newflag + "');", true);
                //  string header = string.Empty;
                divsyngrd.Attributes.Add("style", "display:block;");
                if (hdnindleglflag.Value.ToString() == "Legal")
                {
                    divsyngrd.Attributes.Add("style", "width: 75%;display:block;");
                }
                else
                {
                    divsyngrd.Attributes.Add("style", "width: 79%;display:block;");
                }
                //divsyngrd.Attributes.Add("style", "display:block;");

                objDAL = new DataAccessLayer("CKYCConnectionString");
                htSyn.Add("@flag", newflag);
                htSyn.Add("@Entity", entity);
                htSyn.Add("@TimeFlag", StrTimeFlag);
                dsSyn = objDAL.GetDataSet("PRC_GET_TX_TblCKYCMiddlewareVerHist", htSyn);
                grdhst.DataSource = dsSyn;
                grdhst.DataBind();

                if (dsSyn.Tables.Count > 0 && dsSyn.Tables[0].Rows.Count > 0)
                {
                    //dsSyn.DataSource = dsSyn.Tables[0];
                    //dsSyn.DataBind();
                    ViewState["gridgrdhst"] = dsSyn.Tables[0];
                    //   header = dsSyn.Tables[0].Rows[0]["SubModuleName"].ToString();

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


        protected void FillDesc(string new_flag)
        {
            try
            {
                DataSet DS = new DataSet();
                DS.Clear();
                Hashtable htParam = new Hashtable();
                htParam.Clear();
                lblgrdhead.Visible = true;

                string header = string.Empty;
                //  string new_flag = hdnsubflag.Value.ToString();
                objDAL = new DataAccessLayer("CKYCConnectionString");

                htParam.Add("@flag", new_flag);
                htParam.Add("@Entity", "");
                htParam.Add("@Param", "1");
                htParam.Add("@TimeFlag", "");
                DS = objDAL.GetDataSet("PRC_GET_TX_TblCKYCMiddlewareVerHist", htParam);

                if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    header = DS.Tables[0].Rows[0]["Desc01"].ToString().Trim();
                }

                lblgrdhead.Text = header.ToString() + " | " + lblverhst.Text;

            }
            catch (Exception ex)
            {

                string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                objErr.LogErr(AppId, currentFile, method.Name.ToString(), ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "USRMGMT");
            }
        }
        public void BindSubMenu()
        {
            Hashtable HtParent = new Hashtable();
            Hashtable HtMenu = new Hashtable();
            Hashtable HtSubMenu = new Hashtable();

            DataSet dsParent = new DataSet();
            DataSet dsMenu = new DataSet();
            DataSet dsSubMenu = new DataSet();

            try
            {
                StringBuilder sbMenu = new StringBuilder();

                Hashtable hTable = new Hashtable();
                DataSet ds = new DataSet();
                string lookp = hdntext.Value.ToString();

                objDAL = new DataAccessLayer("CKYCConnectionString");
                hTable.Add("@LookUpcode", lookp);
                ds = objDAL.GetDataSet("PRC_GET_SUBMENU_BIND", hTable);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            // sbMenu.Append("<ul class='nav nav-tabs-sub'>");
                            anchid = "'" + (ds.Tables[0].Rows[i]["ParamValue"].ToString()) + "'";
                            sbMenu.Append("<li style='float:left;height: 2.5em; line-height: 12px; margin-top:5px; border: 1px solid gray; width:100%; border-right: hidden; border-left: hidden; border-bottom:hidden; padding:15px; margin-bottom:10px'> <a id =" + anchid + "href=# style='font-weight: bold; float:left; color:#808080; margin-top:-15px ;text-align: center; justify-content: center !important' runat=server onclick=fnsetsubmenu(" + anchid + ");test(" + anchid + ") >" + ds.Tables[0].Rows[i]["ParamDesc"].ToString() + "</a></li><br/>");
                        }
                        //<hr style='width:100%;text-align:center;margin-left: 0; margin-bottom:18px; margin-top:18px;height:1px; color:gray; background-color:gray'>
                        //li style='float:left;height: 3.2em; line-height: 20px; margin-top:2%; border: 1px solid gray; width:117%; border-right: hidden; border-left: hidden; border-bottom:hidden; padding:12px; margin-bottom:2%'
                        //sbMenu.Append("</li>");  javascript:void(0)
                        //sbMenu.Append("</a>");
                        //sbMenu.Append("</ul>");
                    }

                    // if (enttype.ToString().Trim() == "Individual")


                    if (hdnindleglflag.Value.ToString() == "Legal")
                    {
                        if (lookp.ToString() == "KLglCustReg")
                        {
                            imglglcst.Src = "../../Images/minus_icon.png";
                            subulglreg.InnerHtml = sbMenu.ToString();
                        }
                        else
                        {
                            imglglrprt.Src = "../../Images/minus_icon.png";
                            sublglrprt.InnerHtml = sbMenu.ToString();
                        }
                    }
                    else
                    {
                        if (lookp.ToString() == "KCustReg")
                        {
                            img2.Src = "../../Images/minus_icon.png";
                            submen.InnerHtml = sbMenu.ToString();

                        }


                        else
                        {
                            img3.Src = "../../Images/minus_icon.png";
                            Ulcustinf.InnerHtml = sbMenu.ToString();
                        }
                    }

                    // "<hr style='width:100%;text-align:left;margin-left:0;height:1px; color:gray; background-color:gray' >" + id='tabcu
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

        protected void btnGetMenuSelection_Click(object sender, EventArgs e)
        {
            try
            {
                //changeImage();
                //  img2.Attributes.Add("onclick", "changeImage()");

                if (hdntext.Value.ToString() == "KCustReg" && img2.Src == "../../Images/minus_icon.png")
                {
                    img2.Src = "../../Images/Plus_icon.png";
                    submen.InnerHtml = "";
                }
                else if (hdntext.Value.ToString() == "KCustInfo" && img3.Src == "../../Images/minus_icon.png")
                {
                    img3.Src = "../../Images/Plus_icon.png";
                    Ulcustinf.InnerHtml = "";
                }
                else if (hdntext.Value.ToString() == "KLglCustReg" && imglglcst.Src == "../../Images/minus_icon.png")
                {
                    imglglcst.Src = "../../Images/Plus_icon.png";
                    subulglreg.InnerHtml = "";
                }
                else if (hdntext.Value.ToString() == "KRprts" && imglglrprt.Src == "../../Images/minus_icon.png")
                {
                    imglglrprt.Src = "../../Images/Plus_icon.png";
                    sublglrprt.InnerHtml = "";
                }
                else
                {
                    BindSubMenu();
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

        protected void btnclicksubmenu_Click(object sender, EventArgs e)
        {


            Button btn = new Button();
            btn.Text = hdnsubflag.Value;


            // btn.Attributes.Add("style", "color:#22afb8 !important;");

            // btn.BackColor = System.Drawing.Color.Red;   //System.Drawing.ColorTranslator.FromHtml("#22afb8");
            btn.Style.Add("color", "#22afb8 !important");



            FillDesc(hdnsubflag.Value.ToString());

            if (hdnindleglflag.Value.ToString() == "Legal")
            {
                BindVerHstGrid("Legal");
            }
            else
            {
                BindVerHstGrid("Individual");
            }

        }

        //   public void SetActiveMenu(string Flag)
        //{
        //    if (Flag == "CReg" && hdnindleglflag.Value.ToString() == "Individual" || hdnindleglflag.Value.ToString() == "Legal")
        //    {



        //    }

        //if (Flag == "REG") { Imgregistration.Src = "../../assets/images/WorkbenchIcons/registration_hover_btn.png"; }
        //else { Imgregistration.Src = "../../assets/images/WorkbenchIcons/registration_btn.png"; }
        //if (Flag == "SRCH") { Imgsearch.Src = "../../assets/images/WorkbenchIcons/search_hover_btn.png"; }
        //else { Imgsearch.Src = "../../assets/images/WorkbenchIcons/search_btn.png"; }
        //if (Flag == "DOWN") { Imgdownload.Src = "../../assets/images/WorkbenchIcons/download_hover_btn.png"; }
        //else { Imgdownload.Src = "../../assets/images/WorkbenchIcons/download_btn.png"; }
        //if (Flag == "UPD") { Imgupdate.Src = "../../assets/images/WorkbenchIcons/update_hover_btn.png"; }
        //else { Imgupdate.Src = "../../assets/images/WorkbenchIcons/update_btn.png"; }
        //if (Flag == "ULS") { ImgUnSolicited.Src = "../../assets/images/WorkbenchIcons/unsolicited_update_hover_btn.png"; }
        //else { ImgUnSolicited.Src = "../../assets/images/WorkbenchIcons/unsolicited_update_btn.png"; }
        //// iFrameLoadPage.Visible = true;
        //iFrameLoadPage.Attributes.Add("style", "display:block;");
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "MstShowHide('divImg', 'none');", true);

        // }

        protected void BTNLEGLINTAB_Click(object sender, EventArgs e)
        {
            if (hdnindleglflag.Value.ToString() == "Legal")
            {
                divsyngrd.Attributes.Add("style", "width: 76%;");
                lglimg.Src = "../../Images/Legal_entityl_tab_hover.jpg";
                indvdl.Src = "../../Images/Individual_tab.jpg";

                myTabsub1.Attributes.Add("style", "display:none;");
                myTabsub.Attributes.Add("style", "display:none;");
                hrindcuinf.Attributes.Add("style", "display:none;width:101%;text-align:left;margin-left:0;height:1px; color:gray; background-color:gray; margin-top:7px; margin-bottom:18px");

                hrlgl.Attributes.Add("style", "display:block;width:101%;text-align:left;margin-left:0;height:1px; color:gray; background-color:gray; margin-top:7px; margin-bottom:18px");
                lglcustreg.Attributes.Add("style", "display:block;");
                lglrprts.Attributes.Add("style", "display:block;");

                BindVerHstGrid("");
                divsyngrd.Attributes.Add("style", "display:none;width: 76%;");

                ViewState["hdntype"] = "";
                ViewState["hdntype"] = "Legal";
            }
            else
            {
                divsyngrd.Attributes.Add("style", "width: 79%;");
                lglimg.Src = "../../Images/Legal_entityl_tab.jpg";
                indvdl.Src = "../../Images/Individual_tab_hover.jpg";

                myTabsub1.Attributes.Add("style", "display:block;");
                myTabsub.Attributes.Add("style", "display:block;");
                hrindcuinf.Attributes.Add("style", "display:block;width:101%;text-align:left;margin-left:0;height:1px; color:gray; background-color:gray; margin-top:7px; margin-bottom:18px");

                hrlgl.Attributes.Add("style", "display:none;width:101%;text-align:left;margin-left:0;height:1px; color:gray; background-color:gray; margin-top:7px; margin-bottom:18px");
                lglcustreg.Attributes.Add("style", "display:none;");
                lglrprts.Attributes.Add("style", "display:none;");

                BindVerHstGrid("");
                divsyngrd.Attributes.Add("style", "display:none;width: 79%;");

                ViewState["hdntype"] = "";
                ViewState["hdntype"] = "Individual";
            }

        }


    }
}