using System;
using System.Web;
using System.Web.UI;
using KMI.FRMWRK.DAL;
using System.Collections;
using System.Data;
using System.Text;
using KMI.FRMWRK.BAL;

namespace KMI.FRMWRK.Web
{

    public partial class _Default : Page
    {
        #region Declarartion
        string strAppID = "0";
        string UserID = string.Empty;
        string Url = string.Empty;
        private string sid = string.Empty;
        string BaseURL = string.Empty;
        string FullPath = string.Empty;


        DataSet ds;
        Hashtable hTable;

        AuthorizationBAL oBal = new AuthorizationBAL();
        ErrorLog objErr = new ErrorLog();
        DataAccessLayer objDAL = new DataAccessLayer();
        #endregion Declarartion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpBrowserCapabilities browser = Request.Browser;

            try
            {
                BaseURL = BaseUrl.BaseUrlPath;
                Admin.User cls = new Admin.User();
                if (HttpContext.Current.Session["UserId"] == null)
                {
                    Response.Redirect("~/ErrorSession.aspx", true);
                }
                if (Session["AppID"] != null)
                {
                    strAppID = Session["AppID"].ToString();
                }
                if (Session["UserId"] != null)
                {
                    UserID = Session["UserId"].ToString();
                }

                //TODO : Test log info, remove later
                Logger.LogInfo("AppID : " + strAppID);

                #region PostBack
                if (!IsPostBack)
                {
                    //GetProfileInfo();
                    MenuRendering();
                }
                #endregion


                if (Request.QueryString["sid"] != null)
                {
                    sid = Request.QueryString["sid"].ToString();
                }
                else
                {
                    sid = null;
                }

                //setup user default page with session variables
                InitializeUserSession();

                #region insert data in iLog

                string iUserId = string.Empty;
                if (HttpContext.Current.Session != null)
                    if (HttpContext.Current.Session["UserID"] != null)
                        iUserId = HttpContext.Current.Session["UserID"].ToString();

                string iUserGroupCode = string.Empty;
                if (HttpContext.Current.Session != null)
                    if (HttpContext.Current.Session["UserGroupCode"] != null)
                        iUserGroupCode = HttpContext.Current.Session["UserGroupCode"].ToString();

                string iCarrierCode = string.Empty;
                if (HttpContext.Current.Session != null)
                    if (HttpContext.Current.Session["CarrierCode"] != null)
                        iCarrierCode = HttpContext.Current.Session["CarrierCode"].ToString();

                string iRemoteHost = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];
                string iURL = HttpContext.Current.Request.ServerVariables["URL"];
                string iLogURL = HttpContext.Current.Request.Url.ToString();

                //BROWSER VERSION AND TYPE
                hTable = new Hashtable();
                hTable.Add("@LogURL", iLogURL);
                hTable.Add("@LogLevel", "ModAcs");
                hTable.Add("@LogTypeName", "Default.aspx.cs");
                hTable.Add("@UserId", iUserId);
                hTable.Add("@UserGroupCode", iUserGroupCode);
                hTable.Add("@URL", iURL);
                hTable.Add("@RemoteHost", iRemoteHost);
                hTable.Add("@BrowserVersion", browser.Version);
                hTable.Add("@BrowserType", browser.Type);
                objDAL.ExecuteScalar("prc_getInsertiLogDtls", hTable, "DefaultConn");

                hTable = null;
                #endregion iLog
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                objErr.LogErr(Convert.ToInt32(strAppID), "Default.aspx.cs", "Page_Load", ex.InnerException == null ? ex.Message.ToString() : (ex.Message.ToString() + " | " + ex.InnerException.ToString()), UserID, "USRMGMT");
                throw ex;
            }
            finally
            {
                ds = null;
            }
        }
        #endregion Page Load

        #region Dispay Error
        private void DisplayError()
        {
            this.__TARGETCONTENT.Attributes["src"] = "Error.aspx";
        }
        #endregion Dispay Error

        #region Initialize user Session
        protected void InitializeUserSession()
        {
            try
            {
                //get users default setup from database
                ds = new DataSet();
                hTable = new Hashtable();
                hTable.Add("@UserId", UserID);
                ds = objDAL.GetDataSet("prc_GetDefaultPage", hTable, "DefaultConn");
                hTable = null;

                //set up session variables
                HttpContext.Current.Session["LocType"] = ds.Tables[0].Rows[0]["LocType"];
                HttpContext.Current.Session["LocCode"] = ds.Tables[0].Rows[0]["LocCode"];
                HttpContext.Current.Session["DeptCode"] = ds.Tables[0].Rows[0]["DeptCode"];
                HttpContext.Current.Session["UserGroupCode"] = ds.Tables[0].Rows[0]["UserGroupCode"];
                HttpContext.Current.Session["isTeamLead"] = ds.Tables[0].Rows[0]["isTeamLead"];
                HttpContext.Current.Session["AppId"] = ds.Tables[0].Rows[0]["AppId"];
                HttpContext.Current.Session["LanguageCode"] = "eng";

                //setup default page to load inside iframe
                string strPath = BaseUrl.BaseUrlPath;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["IsWorkBench"] != null && ds.Tables[0].Rows[0]["IsWorkBench"].ToString().Equals("Y"))
                        Url = strPath + ds.Tables[0].Rows[0]["TargetName"].ToString();
                    //Url = "HighCharts.aspx?tabcode=0";
                    else //if page is inbox then send tabcode to load default tab
                         //Url = strPath + ds.Tables[0].Rows[0]["TargetName"].ToString() + "?tabcode=" + ds.Tables[0].Rows[0]["WorkBenchIndex"].ToString();
                        Url = strPath + "/HighCharts.aspx?tabcode=0";

                }
                else
                {
                    Url = strPath + "/Unauthorized.aspx";
                }

                this.__TARGETCONTENT.Attributes["src"] = Url;

            }
            catch (Exception ex)
            {
                throw;
            }
            finally { ds = null; }

        }
        #endregion Initialize Session

        #region GetProfileInfo
        private string GetProfileImage()
        {
            try
            {
                StringBuilder strProfileInfo = new StringBuilder();
                hTable = new Hashtable();
                ds = new DataSet();
                hTable.Add("@UserId", Session["UserId"].ToString());
                ds = objDAL.GetDataSet("Prc_GetUserDetails", hTable, "DefaultConn");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        strProfileInfo.Append("<ul id='ulSettingPannel' class='nav navbar-nav navbar-right responsive-Pro-Icon' style='margin-top:-4px;'>");
                        //strProfileInfo.Append("<li class='desktop' style='margin-top:5px;'><span style='padding:10px 15px;'>" + ds.Tables[0].Rows[0]["Display"].ToString().ToUpper() + ' ' + ds.Tables[0].Rows[0]["LastLoginDate"].ToString().ToUpper() + "</span></li>");
                        strProfileInfo.Append("<li class='ipad' style='margin-top:5px;'><span style='padding:10px 15px;'>" + ds.Tables[0].Rows[0]["Name"].ToString().ToUpper() + ' ' + ds.Tables[0].Rows[0]["LastLoginDate"].ToString().ToUpper() + "</span></li>");
                        strProfileInfo.Append("<li><Img ID='ImagePreView' class='img img-circle' src=");

                        if (ds.Tables[0].Rows[0]["imgpath"].ToString() != "")
                        {
                            strProfileInfo.Append("'" + BaseURL + ds.Tables[0].Rows[0]["imgpath"].ToString() + " 'Width='30px' BorderStyle='Solid' Height='30px'/></li>");
                        }
                        else
                        {
                            if (ds.Tables[0].Rows[0]["Gender"].ToString() == "Female")
                            {
                                strProfileInfo.Append(BaseURL + @"\Content\Images\ProfileImage\defaultFemale.jpg' 'Width='30px' BorderStyle='Solid' Height='30px'/></li>");
                            }
                            if (ds.Tables[0].Rows[0]["Gender"].ToString() == "Male")
                            {
                                strProfileInfo.Append(BaseURL + @"\Content\Images\ProfileImage\defaultmale.jpg' 'Width='30px' BorderStyle='Solid' Height='30px'/></li>");
                            }
                            if (ds.Tables[0].Rows[0]["Gender"].ToString() == "")
                            {
                                strProfileInfo.Append(BaseURL + @"\Content\Images\ProfileImage\download.jpg' 'Width='30px' BorderStyle='Solid' Height='30px'/></li>");
                            }
                        }

                        strProfileInfo.Append("<li class='dropdown' style='margin-top:5px;'><i class='fa fa-cog fa-override' data-toggle='dropdown'></i>");

                        strProfileInfo.Append("<ul class='dropdown-menu' style='width:200px;'>");
                        strProfileInfo.Append("<li>");
                        strProfileInfo.Append("<div class='row'>");
                        strProfileInfo.Append("<div class='col-sm-12'>");
                        strProfileInfo.Append("<p class='text-center'><b><span>Administrator</span></b></p>");
                        strProfileInfo.Append("</div>");
                        strProfileInfo.Append("</div>");
                        strProfileInfo.Append("</li>");
                        //strProfileInfo.Append("<li>");
                        //strProfileInfo.Append("<a href='javascript:void(0)' onClick=loadPageInIframe('#__TARGETCONTENT','"+ BaseURL + "/Account/UpdateProfile.aspx')>");
                        //strProfileInfo.Append("<span class='glyphicon glyphicon-user'></span>  Edit Profile");
                        //strProfileInfo.Append("</a>");
                        //strProfileInfo.Append("</li>");
                        strProfileInfo.Append("<li>");
                        strProfileInfo.Append("<a href='javascript:void(0)' onClick=loadPageInIframe('#__TARGETCONTENT','"+ BaseURL + "/Account/ResetPassword.aspx')>");
                        strProfileInfo.Append("<span class='glyphicon glyphicon-refresh'></span>  Change Password");
                        strProfileInfo.Append("</a>");
                        strProfileInfo.Append("</li>");
                        strProfileInfo.Append("<li class='divider'></li>");
                        strProfileInfo.Append("<li>");
                        strProfileInfo.Append("<div class='row pull-right'>");
                        strProfileInfo.Append("<div class='col-lg-12'>");
                        strProfileInfo.Append("<p>");
                        strProfileInfo.Append("<a id='btnSignOut' class='btn-animated bg-horrible pull-right disabled btn-link' href='"+ BaseURL + "/Account/Login.aspx'>Sign Out</a>");
                        strProfileInfo.Append("</p>");
                        strProfileInfo.Append("</div>");
                        strProfileInfo.Append("</div>");
                        strProfileInfo.Append("</li>");
                        strProfileInfo.Append("</ul>");
                        strProfileInfo.Append("</li>");
                    }
                }
                return strProfileInfo.ToString();
            }
            catch (Exception ex)
            {
                objErr.LogErr(Convert.ToInt32(strAppID), "Default.aspx.cs", "GetProfileInfo", ex.Message.ToString()+" | " + ex.InnerException == null ? "" : ex.InnerException.ToString(), UserID, "USRMGMT");
                throw ex;
            }
            finally
            {
                hTable = null;
                ds = null;
            }
        }

        #endregion

        #region MenuRendering

        public void MenuRendering()
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
                
                hTable = new Hashtable();
                ds = new DataSet();
                hTable.Add("@UserId", Session["UserId"].ToString());
                ds = objDAL.GetDataSet("Prc_getRootMenuBind", hTable, "DefaultConn");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        sbMenu.Append("<div class='navbar' role='navigation' style='margin-bottom:0px;'>");
                        sbMenu.Append("<div class='navbar-header'>");
                        
                        sbMenu.Append("<button type = 'button' class='navbar-toggle mobile' data-toggle='collapse' data-target='.navbar-collapse'>");
                        sbMenu.Append("<span class='sr-only'>Toggle navigation</span>");
                        sbMenu.Append("<span class='icon-bar'></span>");
                        sbMenu.Append("<span class='icon-bar'></span>");
                        sbMenu.Append("<span class='icon-bar'></span>");
                        sbMenu.Append("</button>");

                        sbMenu.Append("<ul class='navbar-toggle dropdown' data-toggle='collapse' data-target='.navbar-collapse'>");
                        sbMenu.Append("<i class='fa fa-cog fa-override' data-toggle='dropdown'></i>");
                        sbMenu.Append("<ul class='dropdown-menu' style='width:200px;left:-150px !important'>");
                        sbMenu.Append("<li>");
                        sbMenu.Append("<div class='row'>");
                        sbMenu.Append("<div class='col-sm-12'>");
                        sbMenu.Append("<p class='text-center'><b><span>Administrator</span></b></p>");
                        sbMenu.Append("</div>");
                        sbMenu.Append("</div>");
                        sbMenu.Append("</li>");
                        //sbMenu.Append("<li>");
                        //sbMenu.Append("<a href='javascript:void(0)' onClick=loadPageInIframe('#__TARGETCONTENT','" + BaseURL + "/Account/UpdateProfile.aspx')>");
                        //sbMenu.Append("<span class='glyphicon glyphicon-user'></span>  Edit Profile");
                        //sbMenu.Append("</a>");
                        //sbMenu.Append("</li>");
                        sbMenu.Append("<li>");
                        sbMenu.Append("<a href='javascript:void(0)' onClick=loadPageInIframe('#__TARGETCONTENT','" + BaseURL + "/Account/ResetPassword.aspx')>");
                        sbMenu.Append("<span class='glyphicon glyphicon-refresh'></span>  Change Password");
                        sbMenu.Append("</a>");
                        sbMenu.Append("</li>");
                        sbMenu.Append("<li class='divider'></li>");
                        sbMenu.Append("<li>");
                        sbMenu.Append("<div class='row pull-right'>");
                        sbMenu.Append("<div class='col-lg-12'>");
                        sbMenu.Append("<p>");
                        sbMenu.Append("<a id='btnSignOut' class='btn-animated bg-horrible pull-right disabled btn-link' href='" + BaseURL + "/Account/Login.aspx'>Sign Out</a>");
                        sbMenu.Append("</p>");
                        sbMenu.Append("</div>");
                        sbMenu.Append("</div>");
                        sbMenu.Append("</li>");
                        sbMenu.Append("</ul>");
                        sbMenu.Append("</ul>");

                        sbMenu.Append("<a class='navbar-brand' href='javascript:void(0)' onClick=loadPageInIframe('#__TARGETCONTENT','" + BaseURL + "/HighCharts.html') style='font - size: 22px;margin-bottom:-3%'><b>CKYC MIDDLEWARE VER 2.0</b></a>");
                        sbMenu.Append(" <h3 style='font-size:10px; color: white; padding-left:6%; margin-bottom: 1 %;'> CKYC Registry System V 1.2 Compliant </h3>");
                        sbMenu.Append("</div>");
                        sbMenu.Append("<div class='collapse navbar-collapse' align='center' style='padding-left: 22%; margin-top:0.5%'>");
                        sbMenu.Append("<ul class='nav navbar-nav'>");

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            HtParent = new Hashtable();
                            dsParent = new DataSet();
                            HtParent.Add("@RooTModule", ds.Tables[0].Rows[i]["ModuleID"].ToString());
                            HtParent.Add("@UserId", Session["UserId"].ToString());
                            dsParent = objDAL.GetDataSet("Prc_getParentMenuBind", HtParent, "DefaultConn");

                            if (dsParent.Tables.Count > 0)
                            {
                                if (dsParent.Tables[0].Rows.Count > 0)
                                {
                                    sbMenu.Append("<li><a class='dropdown-toggle' data-toggle='dropdown'>" + ds.Tables[0].Rows[i]["ModuleName"].ToString() + "<b class='caret'></b></a>");
                                    sbMenu.Append("<ul class='dropdown-menu'>");

                                    for (int p = 0; p < dsParent.Tables[0].Rows.Count; p++)
                                    {
                                        HtMenu = new Hashtable();
                                        dsMenu = new DataSet();
                                        HtMenu.Add("@RootModuleID", ds.Tables[0].Rows[i]["ModuleID"].ToString());
                                        HtMenu.Add("@ParentModuleID", dsParent.Tables[0].Rows[p]["ModuleID"].ToString());
                                        HtMenu.Add("@UserId", Session["UserId"].ToString());
                                        dsMenu = objDAL.GetDataSet("Prc_getMenuBind", HtMenu, "DefaultConn");

                                        if (dsMenu.Tables.Count > 0)
                                        {
                                            if (dsMenu.Tables[0].Rows.Count > 0)
                                            {
                                                sbMenu.Append("<li><a class='dropdown-toggle' data-toggle='dropdown'>" + dsParent.Tables[0].Rows[p]["ModuleName"].ToString() + "<b class='caret'></b></a>");
                                                sbMenu.Append("<ul class='dropdown-menu'>");

                                                for (int m = 0; m < dsMenu.Tables[0].Rows.Count; m++)
                                                {
                                                    HtSubMenu = new Hashtable();
                                                    dsSubMenu = new DataSet();
                                                    HtSubMenu.Add("@RootModuleID", ds.Tables[0].Rows[i]["ModuleID"].ToString());
                                                    HtSubMenu.Add("@ParentModuleID", dsMenu.Tables[0].Rows[m]["ModuleID"].ToString());
                                                    HtSubMenu.Add("@UserId", Session["UserId"].ToString());
                                                    dsSubMenu = objDAL.GetDataSet("Prc_getMenuBind", HtSubMenu, "DefaultConn");

                                                    if (dsSubMenu.Tables.Count > 0)
                                                    {
                                                        if (dsSubMenu.Tables[0].Rows.Count > 0)
                                                        {
                                                            sbMenu.Append("<li><a class='dropdown-toggle' data-toggle='dropdown'>" + dsMenu.Tables[0].Rows[m]["ModuleName"].ToString() + "<b class='caret'></b></a>");
                                                            sbMenu.Append("<ul class='dropdown-menu'>");
                                                            for (int s = 0; s < dsSubMenu.Tables[0].Rows.Count; s++)
                                                            {
                                                                sbMenu.Append("<li><a onclick='RenderPage(" + dsSubMenu.Tables[0].Rows[s]["ModuleID"].ToString() + ")'>" + dsSubMenu.Tables[0].Rows[s]["ModuleName"].ToString() + "</a></li>");
                                                            }
                                                            sbMenu.Append("</ul>");
                                                            sbMenu.Append("</li>");
                                                        }
                                                        else
                                                        {
                                                            sbMenu.Append("<li><a onclick='RenderPage(" + dsMenu.Tables[0].Rows[m]["ModuleID"].ToString() + ")'>" + dsMenu.Tables[0].Rows[m]["ModuleName"].ToString() + "</a></li>");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        sbMenu.Append("<li><a onclick='RenderPage(" + dsMenu.Tables[0].Rows[m]["ModuleID"].ToString() + ")'>" + dsMenu.Tables[0].Rows[m]["ModuleName"].ToString() + "</a></li>");
                                                    }
                                                }
                                                sbMenu.Append("</ul>");
                                                sbMenu.Append("</li>");
                                            }
                                            else
                                            {
                                                sbMenu.Append("<li><a onclick='RenderPage(" + dsParent.Tables[0].Rows[p]["ModuleID"].ToString() + ")'>" + dsParent.Tables[0].Rows[p]["ModuleName"].ToString() + "</a></li>");
                                            }
                                        }
                                        else
                                        {
                                            sbMenu.Append("<li><a onclick='RenderPage(" + dsParent.Tables[0].Rows[p]["ModuleID"].ToString() + ")'>" + dsParent.Tables[0].Rows[p]["ModuleName"].ToString() + "</a></li>");
                                        }
                                    }
                                    sbMenu.Append("</ul>");
                                    sbMenu.Append("</li>");
                                }
                                else
                                {
                                    sbMenu.Append("<li><a onclick='RenderPage(" + ds.Tables[0].Rows[i]["ModuleID"].ToString() + ")'>" + ds.Tables[0].Rows[i]["ModuleName"].ToString() + "</a></li>");
                                }
                            }
                            else
                            {
                                sbMenu.Append("<li><a onclick='RenderPage(" + ds.Tables[0].Rows[i]["ModuleID"].ToString() + ")'>" + ds.Tables[0].Rows[i]["ModuleName"].ToString() + "</a></li>");
                            }
                        }

                        sbMenu.Append("</ul>");
                        sbMenu.Append(GetProfileImage());
                        sbMenu.Append("</div>");
                        sbMenu.Append("</div>");
                    }
                    DivMenu.InnerHtml = sbMenu.ToString();
                }
            }
            catch (Exception ex)
            {
                objErr.LogErr(Convert.ToInt32(strAppID), "Default.aspx.cs", "MenuRendering", ex.Message.ToString()+" | " + ex.InnerException == null ? "" : ex.InnerException.ToString(), UserID, "USRMGMT");
                throw ex;
            }
            finally
            {
                hTable = null;
                HtParent = null;
                HtMenu = null;
                HtSubMenu = null;

                ds = null;
                dsParent = null;
                dsMenu = null;
                dsSubMenu = null;
            }
        }
        
        #endregion

        #region TO SHOW MENU SELECTION PATH
        protected void btnGetMenuSelection_Click(object sender, EventArgs e)
        {
            hTable = new Hashtable();
            ds = new DataSet();
            hTable.Add("@ModuleID", HdnScrumbSelectedModule.Value);
            ds = objDAL.GetDataSet("Prc_GetScrumbModuleID", hTable, "DefaultConn");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblFileName.Visible = true;
                    lblFileName.Text = ds.Tables[0].Rows[0]["RootName"].ToString() + " >> " + ds.Tables[0].Rows[0]["ParentName"].ToString() + " >> " + ds.Tables[0].Rows[0]["ModuleName"].ToString();
                }
            }

            if(ds.Tables[0].Rows[0]["TargetName"].ToString().Contains("?"))
                this.__TARGETCONTENT.Attributes["src"] = ds.Tables[0].Rows[0]["TargetName"].ToString() + "&ModuleId=" + HdnScrumbSelectedModule.Value;
            else
                this.__TARGETCONTENT.Attributes["src"] = ds.Tables[0].Rows[0]["TargetName"].ToString() + "?ModuleId=" + HdnScrumbSelectedModule.Value;

            hTable = null;
            ds = null;
        }
        #endregion
    }


}