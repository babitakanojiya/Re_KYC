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
using System.Text.RegularExpressions;
using System.Web.Services;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;
using System.Configuration;
using System.Net.Http;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class Search : System.Web.UI.Page
    {
        #region Declare Veriables
        private MultilingualManager olng;
        Hashtable htParam = new Hashtable();
        ErrorLog objErr;
        DataTable dt;
        DataAccessLayer objDAL;
        CommonUtility oCommonUtility = new CommonUtility();
        int AppID;
        string UserID = string.Empty;
        public string FlagPageTyp;
        public string CheckBoxID; //added by rutuja on 4aug
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Flag"].ToString() != null)
            {
                FlagPageTyp = Request.QueryString["Flag"].ToString();
                hdnFlagPageTyp.Value = FlagPageTyp.ToString();
                iFrameLoadPage.Src = "ZipFileDetail.aspx?Flag=" + hdnFlagPageTyp.Value;
            }

            if (!IsPostBack)
            {
                subPopulateAccountType();
            }
            if (IsPostBack)
            {
                SetActiveMenu(hdnCurrActId.Value);
            }

            if (Session["UserId"].ToString() != null)
            {
                UserID = Session["UserId"].ToString();
            }
        }

        public void SetActiveMenu(string Flag)
        {
            divVal.Attributes.Add("style", "display:none;");
            divValTyp.Attributes.Add("style", "display:none;");
            btnSearchDtls.Attributes.Add("style", "display:block;");
            btnClr.Attributes.Add("style", "display:none;");
            if (Flag == "REG")
            {
                Imgregistration.Src = "../../assets/images/WorkbenchIcons/registration_hover_btnNew.png";
            }
            else { Imgregistration.Src = "../../assets/images/WorkbenchIcons/registration_btnNew.png"; }
            if (Flag == "SRCH")
            {
                Imgsearch.Src = "../../assets/images/WorkbenchIcons/search_hover_btn.png";
            }
            else { Imgsearch.Src = "../../assets/images/WorkbenchIcons/search_btn.png"; }
            if (Flag == "DOWN")
            {
                Imgdownload.Src = "../../assets/images/WorkbenchIcons/download_hover_btn.png";
            }
            else { Imgdownload.Src = "../../assets/images/WorkbenchIcons/download_btn.png"; }
            if (Flag == "UPD")
            {
                Imgupdate.Src = "../../assets/images/WorkbenchIcons/update_hover_btn.png";
            }
            else { Imgupdate.Src = "../../assets/images/WorkbenchIcons/update_btn.png"; }
            if (Flag == "ULS") { ImgUnSolicited.Src = "../../assets/images/WorkbenchIcons/unsolicited_update_hover_btn.png"; }
            else { ImgUnSolicited.Src = "../../assets/images/WorkbenchIcons/unsolicited_update_btn.png"; }
            iFrameLoadPage.Src = "ZipFileDetail.aspx?Flag=" + hdnFlagPageTyp.Value;
            iFrameLoadPage.Attributes.Add("style", "display:block;");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "MstShowHide('divImg', 'none');", true);

        }

        public void SetActiveCustJourney(string Flag)
        {
            if (Flag == "REG")
            {
                ImgReg.Src = "../../assets/images/Customer_Journey_New/02_New_Registration.png";//
                ImgUpload.Src = "../../assets/images/Customer_Journey_New/03_Upload_Documents_disable.png";
                ImgQc.Src = "../../assets/images/Customer_Journey_New/04_QC_icon_disable.png";
                ImgZip.Src = "../../assets/images/Customer_Journey_New/05_zip_generation_icon_disable.png";
                ImgPush.Src = "../../assets/images/Customer_Journey_New/06_Push_CRS_icon_disable.png";
                ImgResp.Src = "../../assets/images/Customer_Journey_New/07_Response_From_CRS_disable.png";
            }
            else if (Flag == "UPLOAD")
            {
                ImgReg.Src = "../../assets/images/Customer_Journey_New/02_New_Registration.png";
                ImgUpload.Src = "../../assets/images/Customer_Journey_New/03_Upload_Documents.png";//
                ImgQc.Src = "../../assets/images/Customer_Journey_New/04_QC_icon_disable.png";
                ImgZip.Src = "../../assets/images/Customer_Journey_New/05_zip_generation_icon_disable.png";
                ImgPush.Src = "../../assets/images/Customer_Journey_New/06_Push_CRS_icon_disable.png";
                ImgResp.Src = "../../assets/images/Customer_Journey_New/07_Response_From_CRS_disable.png";
            }
            else if (Flag == "QC")
            {
                ImgReg.Src = "../../assets/images/Customer_Journey_New/02_New_Registration.png";
                ImgUpload.Src = "../../assets/images/Customer_Journey_New/03_Upload_Documents.png";
                ImgQc.Src = "../../assets/images/Customer_Journey_New/04_QC_icon.png";//
                ImgZip.Src = "../../assets/images/Customer_Journey_New/05_zip_generation_icon_disable.png";
                ImgPush.Src = "../../assets/images/Customer_Journey_New/06_Push_CRS_icon_disable.png";
                ImgResp.Src = "../../assets/images/Customer_Journey_New/07_Response_From_CRS_disable.png";
            }
            else if (Flag == "ZIP")
            {
                ImgReg.Src = "../../assets/images/Customer_Journey_New/02_New_Registration.png";
                ImgUpload.Src = "../../assets/images/Customer_Journey_New/03_Upload_Documents.png";
                ImgQc.Src = "../../assets/images/Customer_Journey_New/04_QC_icon.png";
                ImgZip.Src = "../../assets/images/Customer_Journey_New/05_zip_generation_icon.png";//
                ImgPush.Src = "../../assets/images/Customer_Journey_New/06_Push_CRS_icon_disable.png";
                ImgResp.Src = "../../assets/images/Customer_Journey_New/07_Response_From_CRS_disable.png";
            }
            else if (Flag == "PUSH")
            {
                ImgReg.Src = "../../assets/images/Customer_Journey_New/02_New_Registration.png";
                ImgUpload.Src = "../../assets/images/Customer_Journey_New/03_Upload_Documents.png";
                ImgQc.Src = "../../assets/images/Customer_Journey_New/04_QC_icon.png";
                ImgZip.Src = "../../assets/images/Customer_Journey_New/05_zip_generation_icon.png";
                ImgPush.Src = "../../assets/images/Customer_Journey_New/06_Push_CRS_icon.png";//
                ImgResp.Src = "../../assets/images/Customer_Journey_New/07_Response_From_CRS_disable.png";
            }
            else if (Flag == "RESP")
            {
                ImgReg.Src = "../../assets/images/Customer_Journey_New/02_New_Registration.png";
                ImgUpload.Src = "../../assets/images/Customer_Journey_New/03_Upload_Documents.png";
                ImgQc.Src = "../../assets/images/Customer_Journey_New/04_QC_icon.png";
                ImgZip.Src = "../../assets/images/Customer_Journey_New/05_zip_generation_icon.png";
                ImgPush.Src = "../../assets/images/Customer_Journey_New/06_Push_CRS_icon.png";
                ImgResp.Src = "../../assets/images/Customer_Journey_New/07_Response_From_CRS.png";//
            }
            divRegSub.Attributes.Add("style", "display:block;");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            myModal23.Attributes.Add("style", "display:none;");
            trDgViewDtl.Attributes.Add("style", "display:block;margin-top:0.5%");
            GridView.DataSource = new System.Collections.Generic.Dictionary<string, string> { { "", "" } };
            GridView.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowDivProgressBar('Loading..Please wait');", true);

            try
            {
                StringBuilder htmlTable = new StringBuilder();
                DataTable objDt = new DataTable();
                DataAccessLayer dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                Hashtable ht = new Hashtable();
                if (ddlSrchTyp.SelectedIndex != 0 && (txtNum.Text != "" || DDStatus.SelectedIndex != 0))
                {
                    divVal.Attributes.Add("style", "display:block;");
                    divValTyp.Attributes.Add("style", "display:block;");
                    btnSearchDtls.Attributes.Add("style", "display:none;");
                    btnClr.Attributes.Add("style", "display:block;");
                    ht.Clear();
                    ht.Add("@NumTyp", ddlSrchTyp.SelectedValue);
                    if (ddlSrchTyp.SelectedValue == "STATUS")
                    {
                        ht.Add("@Num", DDStatus.SelectedValue);
                    }
                    else
                    {
                        ht.Add("@Num", txtNum.Text.ToString());
                    }
                    if (hdnCurrActId.Value == "REG")
                    { objDt = dataAccessLayer.GetDataTable("prc_getSeachData", ht); }
                    if (hdnCurrActId.Value == "SRCH")
                    { objDt = dataAccessLayer.GetDataTable("prc_getBulkSeachData", ht); }
                    if (hdnCurrActId.Value == "DOWN")
                    { objDt = dataAccessLayer.GetDataTable("prc_getBulkDownloadData", ht); }
                    if (hdnCurrActId.Value == "UPD")
                    { objDt = dataAccessLayer.GetDataTable("prc_getSeachData_Update", ht); }
                }
                else
                {
                    if (hdnCurrActId.Value == "REG")
                    {
                        ht.Clear();
                        ht.Add("@NumTyp", "CustType");
                        ht.Add("@Num", FlagPageTyp);
                        objDt = dataAccessLayer.GetDataTable("prc_getSeachData", ht);
                    }
                    if (hdnCurrActId.Value == "SRCH")
                    {

                        ht.Clear();
                        ht.Add("@Num", FlagPageTyp);
                        objDt = dataAccessLayer.GetDataTable("prc_getBulkSeachData", ht);
                    }
                    if (hdnCurrActId.Value == "DOWN")
                    {
                        ht.Clear();
                        ht.Add("@Num", FlagPageTyp);
                        objDt = dataAccessLayer.GetDataTable("prc_getBulkDownloadData", ht);
                    }
                    if (hdnCurrActId.Value == "UPD")
                    {
                        ht.Clear();
                        ht.Add("@NumTyp", "CustType");
                        ht.Add("@Num", FlagPageTyp);
                        objDt = dataAccessLayer.GetDataTable("prc_getSeachData_Update", ht);
                    }
                    if (hdnCurrActId.Value == "ULS")
                    {
                        objDt.Clear();
                        objDt = dataAccessLayer.GetDataTable("prc_getUnsolitatedNotificationData");
                    }
                }

                string imgID, divID, ControlTyp, ControlTxt, RegRefNo, FiRefNo, HdrId, TargetURL, FooterId, ImgURL, CndStatus = "''", batchid = "''";

                divSearchResult.InnerText = "";
                htmlTable.Append("<div class='row'>");
                htmlTable.Append("    <div class='col-sm-12' style= 'display: inline-flex;'>");
                if (objDt.Rows.Count != 0)
                {

                    #region binding if DataCount>0
                    for (int i = 0; i < objDt.Rows.Count; i++)
                    {
                        ControlTyp = (objDt.Rows[i]["ControlTyp"].ToString());// ControlTyp
                        ControlTxt = (objDt.Rows[i]["ControlTxt"].ToString());// ControlTyp
                        RegRefNo = "'" + ((objDt.Rows[i]["RegRefNo"].ToString() == "" ? objDt.Rows[i]["FiRefNo"].ToString() : objDt.Rows[i]["RegRefNo"].ToString())) + "'"; // RegRefNo 
                        FiRefNo = "'" + (objDt.Rows[i]["FiRefNo"].ToString()) + "'"; // FiRefNo
                        if ((hdnCurrActId.Value == "SRCH") || (hdnCurrActId.Value == "DOWN"))
                        {
                            batchid = "'" + (objDt.Rows[i]["batchid"].ToString()) + "'"; // batchid
                        }
                        divID = "'" + (objDt.Rows[i]["FiRefNo"].ToString()) + "'"; //Main Div ID
                        TargetURL = "'" + (objDt.Rows[i]["TargetURL"].ToString()) + "'"; //TargetURL by Shubham 22Mar2021
                        if (hdnCurrActId.Value == "REG")
                        { CndStatus = "'" + (objDt.Rows[i]["CndStatus"].ToString()) + "'"; }
                        ImgURL = "" + (objDt.Rows[i]["ImageURL"].ToString()) + ""; //TargetURL by Shubham 23Mar2021
                        HdrId = "HdrId" + i;
                        imgID = "Img" + (objDt.Rows[i]["FiRefNo"].ToString()) + "";
                        FooterId = "F" + (objDt.Rows[i]["FiRefNo"].ToString()) + "";
                        if (hdnCurrActId.Value == "REG" && hdnFIRefID.Value == objDt.Rows[i]["FiRefNo"].ToString() && objDt.Rows[i]["FiRefNo"].ToString() != "" &&
                            (objDt.Rows[i]["TargetURL"].ToString()) != "ERR" && hndCndStatus.Value != objDt.Rows[i]["CndStatus"].ToString())
                        {
                            htmlTable.Append("<div id=" + divID + " class='col-sm-2' style='border: 4px solid #7edf00;width: 14%;'>");
                            SetActiveCustJourney(hdnQCAppro.Value);
                        }
                        else
                        {
                            htmlTable.Append("<div id=" + divID + " class='col-sm-2' style='width: 14%;'>");
                        }
                        htmlTable.Append("<div style='text-align:center;white-space: nowrap;'>");
                        //started by rutuja
                        if (ControlTxt == "QC Completed" || ControlTxt == "Process" || ControlTxt == "Search" || ControlTxt == "Download" || ControlTxt == "Error")
                        {
                            htmlTable.Append("<input type=\"checkbox\" name=\"CheckSel").Append(objDt.Rows[i]["FiRefNo"].ToString()).Append("\">");
                            //                        htmlTable.Append(" <input type='checkbox' id =' "+ HdrId+"chk" + "' value= ' " + (objDt.Rows[i]["FiRefNo"].ToString()) + "' style='text-align:center;' >"); //added by rutuja
                            if (Request.Form["CheckSel" + objDt.Rows[i]["FiRefNo"].ToString()] != null)
                            {
                                CheckBoxID = CheckBoxID + "," + objDt.Rows[i]["FiRefNo"].ToString();
                                // CheckBoxID.Distinct();
                            }
                        }
                        //ended by rutuja

                        //htmlTable.Append("<a ID='" + HdrId + "' onclick=javascript:ChangeImg('" + imgID + "','" + FooterId + "','" + ImgURL + "'); href=javascript:ChangeImg('" + imgID + "','" + FooterId + "','" + ImgURL + "'); style='color: Blue; ' >" + (objDt.Rows[i]["FiRefNo"].ToString()) + "</a>");
                        if (hdnCurrActId.Value == "REG")
                        {

                            htmlTable.Append("<span ID='" + HdrId + "'  style='color: black; ' >" + " FiRefNo.:" + (objDt.Rows[i]["FiRefNo"].ToString()) + "</span>");
                        }
                        else
                        {
                            htmlTable.Append("<span ID='" + HdrId + "'  style='color: black; ' >" + " " + (objDt.Rows[i]["FiRefNo"].ToString()) + "</span>");

                        }
                        htmlTable.Append("</div>");
                        htmlTable.Append("<div style='text-align:center'");
                        //Default IMG
                        htmlTable.Append(" < img id=" + FiRefNo + "  src = 'ImageCSharp.aspx?ImageID=" + ImgURL + "' style = 'width: 100px; height: 100px; padding-left: 0px;' >");
                        htmlTable.Append("<img ID='" + imgID + "'");
                        //if (hdnCurrActId.Value == "REG" && hdnFIRefID.Value == objDt.Rows[i]["FiRefNo"].ToString() && (objDt.Rows[i]["TargetURL"].ToString()) != "ERR"
                        //    && hndCndStatus.Value != objDt.Rows[i]["CndStatus"].ToString())
                        //{
                        //    htmlTable.Append("style = 'width: 133px; height: 123px; border:4px solid #00b4bf;'");
                        //    SetActiveCustJourney(hdnQCAppro.Value);
                        //}
                        //else
                        //{
                        htmlTable.Append("style = 'width: 100px; height: 100px;'");
                        //}
                        // htmlTable.Append("href=javascript:ChangeImg('" + imgID + "'); ");
                        if ((objDt.Rows[i]["TargetURL"].ToString()) == "ERR")
                        {
                            htmlTable.Append(" src = '../../assets/images/Error_Image.jpg' />");
                        }
                        else if (ImgURL != "")
                        {
                            htmlTable.Append(" src = 'ImageCSharp.aspx?ImageID=" + ImgURL + "' />");
                        }
                        else
                        {
                            if (FlagPageTyp == "01")
                            {
                                htmlTable.Append(" src ='../../assets/images/IndImg.jpg' />");
                            }
                            else
                            {
                                htmlTable.Append(" src ='../../assets/images/Legal_Entity_icon.jpg' />");
                            }
                        }
                        //Default IMG
                        htmlTable.Append("</div>");
                        htmlTable.Append("<div ID='" + FooterId + "' style ='text-align:center; display:block;'>");
                        if (ControlTyp == "txt")
                        {
                            htmlTable.Append("<span  class='tsview'");
                            htmlTable.Append("id=btnview ");
                            htmlTable.Append(" ");
                            htmlTable.Append("style='color: black; '>" + ControlTxt + "</span>");
                        }
                        else
                        {
                            htmlTable.Append("<a onclick=javascript:LoadQCPage(" + TargetURL + "," + RegRefNo + "," + ((objDt.Rows[i]["FiRefNo"].ToString()) == "" ? FiRefNo : (objDt.Rows[i]["FiRefNo"].ToString())) + "," + CndStatus + "," + batchid + "); class='tsview'");
                            htmlTable.Append("id=btnview ");
                            htmlTable.Append("href=javascript:LoadQCPage(btnview, test); ");
                            htmlTable.Append("style='color: Blue; '>" + ControlTxt + "</a>");
                        }
                        htmlTable.Append("</div>");
                        htmlTable.Append("</div>");

                    }
                    #endregion
                    panel1.Attributes.Add("style", "height: 200px; overflow: hidden; overflow-x: scroll;");
                }
                else
                {
                    #region If No Record Found
                    htmlTable.Append("<span style='padding: 0px 430px 0px 540px;; color: black; font - size: 15px;' >No Record Found</span>");
                    panel1.Attributes.Add("style", "height:33px;");
                    divRegSub.Attributes.Add("style", "display:none;");
                    hdnQCAppro.Value = "";
                    #endregion
                }
                htmlTable.Append("    </div>");
                htmlTable.Append("</div>");
                divSearchResult.Controls.Add(new Literal { Text = htmlTable.ToString() });
                if (hdnQCAppro.Value == "QC")
                {
                    // divRegSub.Visible = true;
                    divRegSub.Attributes.Add("style", "display:block;");
                }

            }
            catch (Exception ex)
            {
            }
            //Added By Shubham 
        }

        private void subPopulateAccountType()
        {
            try
            {
                objDAL = new DataAccessLayer("CKYCConnectionString");
                htParam.Clear();
                htParam.Add("@Flag", "Y");
                dt = objDAL.GetDataTable("Prc_GetStatus", htParam);
                if (dt.Rows.Count > 0)
                {
                    oCommonUtility.FillDropDown(DDStatus, dt, "StatusValue", "StatusDesc");
                }
                dt = null;

                DDStatus.Items.Insert(0, new ListItem("Select", ""));
            }
            catch (Exception ex)
            {
                if (Session["UserID"].ToString().Trim() == null || Session["UserID"].ToString().Trim() == "")
                {
                    Response.Redirect("~/ErrorSession.aspx");
                }
                else
                {
                    objErr = new ErrorLog();
                    objErr.LogErr(AppID, "CkycReg.aspx.cs", "subPopulateAccountType", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), UserID, "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Something went wrong, Kindly contact to service provider.');", true);
                }
            }
        }

        public class Item
        {
            public string Text1 { get; set; }
            public string Text2 { get; set; }
        }

        //protected void lblshortview_Click(object sender, EventArgs e)
        //{
        //    //GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
        //    //System.Threading.Thread.Sleep(15000);
        //    //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "LoadQCPage('QC Approval','10000388')", true);
        //    //dvProgressBar.Attributes.Add("style", "display:none");

        //}

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            if (e.CommandName == "QC Approval1")
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "LoadQCPage('QC Approval','10000387')", true);
            }
            else if (e.CommandName == "QC Approval2")
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "LoadQCPage('QC Approval','10000388')", true);
            }
            else if (e.CommandName == "QC Approval3")
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "LoadQCPage('QC Approval','10000389')", true);
            }
            else if (e.CommandName == "QC Approval4")
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "LoadQCPage('QC Approval','20086435564680')", true);
            }
            else if (e.CommandName == "QC Approval5")
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "LoadQCPage('QC Approval','AJSPS2665N')", true);
            }
            else if (e.CommandName == "Download")
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "LoadQCPage('Download','20086435564680')", true);
            }
        }



        protected void btnStatus_Click(object sender, EventArgs e)
        {

        }
        [WebMethod]
        //public static string Name(string refno)
        //{
        //    string str;
        //    DataAccessLayer dataAccessLayer = new DataAccessLayer("UpdDwnldConnectionString");
        //    // objDAL = new DataAccessLayer("UpdDwnldConnectionString");
        //    DataSet ds = new DataSet();
        //    System.Collections.Hashtable ht = new System.Collections.Hashtable();
        //    //htParam.Clear();
        //    //htParam.Add("@Flag", "Y");
        //    ht.Add("@RefNo", refno);
        //    ds = dataAccessLayer.GetDataSet("Prc_BindErrorGrid_ErrDesc", ht);
        //       // string str = ds.Tables[0].Rows["ErrorDesc"].ToString();
        //       // string str = ds.Tables[0].Rows[0]["ErrorDesc"].ToString() + "\r\n" + ds.Tables[0].Rows[1]["ErrorDesc"].ToString();
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        str = ds.Tables[0].Rows[i]["ErrorDesc"].ToString();
        //        return str;

        //    }
        //    return "";
        //    // string JSONString = JSONConvert.SerializeObject(ds);

        //}

        public void getError_Click(object sender, EventArgs e)
        {
            //GetRefNo();
            var count = "";
            //var RefNoHidden = this.hdnFIRefID.Value;
            var RefNoHidden = hdnId.Value;

            //        var FIDchck = this.hdnFD.Value;
            DataAccessLayer dataAccessLayer = new DataAccessLayer("UpdDwnldConnectionString");
            // objDAL = new DataAccessLayer("UpdDwnldConnectionString");
            DataSet ds = new DataSet();
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            //htParam.Clear();
            //htParam.Add("@Flag", "Y");
            ht.Add("@RefNo", RefNoHidden);
            if (hdnId.Value == "")
            {
                ht.Add("@INbatchid", hdnbatchid.Value);
            }

            ht.Add("@flag", hdnCurrActId.Value);
            dt = dataAccessLayer.GetDataTable("Prc_BindErrorGrid_ErrCount", ht);
            count = dt.Rows[0]["Exists"].ToString();
            if (count == "Y")
            {
                ds = dataAccessLayer.GetDataSet("Prc_BindErrorGrid_ErrDesc", ht);
                gvErrorSearch.DataSource = ds;
                gvErrorSearch.DataBind();
                //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "MstShowHide('myModal23', 'block');", true);
                myModal23.Attributes.Add("style", "display:block;");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowHideModal();", true);
                // trDgViewDtl.Attributes.Add("style", "display:block;margin-top:0.5%");
                //                getError.Attributes.Add("onclick", "HideLink('" + FIDchck + "','" + count + "');");

            }
            else
            {
                gvErrorSearch.EmptyDataText = "No Record Found";
                gvErrorSearch.Visible = true;
                lblErrNoRecord.Visible = true;
                myModal23.Attributes.Add("style", "display:block;");
                // trDgViewDtl.Attributes.Add("style", "display:block;margin-top:0.5%");
                //              getError.Attributes.Add("onclick", "HideLink('" + FIDchck + "','" + count + "');");
            }
        }

        protected void btnTopMenu_Click(object sender, EventArgs e)
        {
            // SetActiveMenu(hdnCurrActId.Value);
        }

        protected void btnUpdAfterProcessOnWrkBench_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnQCAppro.Value == "QC" && hdnId.Value != "")
                {
                    string id = "Img" + hdnId.Value;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "MstSetActive("+ id + ");", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "MstSetActive(" + id + ");", true);
                }
                else
                {
                    objDAL = new DataAccessLayer("CKYCConnectionString");
                    htParam.Clear();
                    htParam.Add("@Flag", hdnCurrActId.Value);
                    htParam.Add("@Id", hdnId.Value);
                    objDAL.ExecuteScalar("PRC_UpdAfterProcessOnWrkBench", htParam);
                    btnSearch_Click(this, e);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnZipFileGeneration_Click(object sender, EventArgs e)
        {
            btnSearch_Click(this, e);    //added by rutujaon4aug for getting selectes checkbox for zipgen
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowDivProgressBar('Please wait Processing Data...');", true);
            ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.FileName = "E:/During WFH/CKYC/BKP/WindowsCkycSftp/WindowsCkycSftp/WindowsCkycSftp/bin/Debug/WindowsCkycSftp.exe";
            // startInfo.FileName = "C:/HostedApplications/CKYC/Demo/CKYC_DEMO_DEV_APP/CKYC/Applications/WindowsCkycSftp/WindowsCkycSftp/bin/Debug/WindowsCkycSftp.exe";
            startInfo.FileName = ConfigurationManager.AppSettings.GetValues("WindowsCkycSftp")[0].ToString();
            startInfo.Arguments = hdnCurrActId.Value + "_" + CheckBoxID + "_" + FlagPageTyp; //Added by Rutujaon4aug
            startInfo.UseShellExecute = false; //commented by ajay
            Process.Start(startInfo).WaitForExit();
            System.Threading.Thread.Sleep(1000);
            btnSearch_Click(this, e);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "  HideProgressBar();", true);
        }


        protected void ddlSrchTyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSrchTyp.SelectedValue == "STATUS")
            {
                DDStatus.Visible = true;
                DDStatus.SelectedIndex = 0;
                txtNum.Visible = false;
                btnSearch_Click(this, e);
            }
            else
            {
                DDStatus.Visible = false;
                txtNum.Visible = true;
                txtNum.Text = "";
                txtNum.Attributes.Add("placeholder", "Please Enter " + ddlSrchTyp.SelectedItem.Text);
                btnSearch_Click(this, e);
            }
            if (ddlSrchTyp.SelectedIndex == 0)
            {
                divVal.Attributes.Add("style", "display:none;");
                divValTyp.Attributes.Add("style", "display:none;");
            }
            else
            {
                divVal.Attributes.Add("style", "display:block;");
                divValTyp.Attributes.Add("style", "display:block;");
            }
            if (ddlSrchTyp.SelectedIndex != 0)
            {
                btnSearchDtls.Attributes.Add("style", "display:none;");
                btnClr.Attributes.Add("style", "display:block;");
            }
            else
            {
                btnSearchDtls.Attributes.Add("style", "display:block;");
                btnClr.Attributes.Add("style", "display:none;");
            }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "LoadCtrl();", true);
        }
        #region WindowsCKYCExcelSFTP- Upload Excel file for Regis.
        protected void btnExcelUpload_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                //startInfo.FileName = "C:/HostedApplications/CKYC/Demo/CKYC_DEMO_DEV_APP/CKYC/Applications/WindowsCKYCExcelSFTP/WindowsCKYCExcelSFTP/bin/Debug/WindowsCKYCExcelSFTP.exe";
                startInfo.FileName = ConfigurationManager.AppSettings.GetValues("WindowsCKYCExcelSFTP")[0].ToString();
                startInfo.Arguments = FlagPageTyp;  //added by rutuja
                Process.Start(startInfo).WaitForExit();
                //System.Threading.Thread.Sleep(20000);
                // btnSearch_Click(this, e);
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region WinBatchJobService- to Validate data
        protected void btnValidationJob_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.FileName = "C:/HostedApplications/CKYC/Demo/CKYC_DEMO_DEV_APP/CKYC/Applications/WinBatchJobService/WinBatchJobService/bin/Debug/WinBatchJobService.exe";
            startInfo.FileName = ConfigurationManager.AppSettings.GetValues("WinBatchJobService")[0].ToString();
            Process.Start(startInfo).WaitForExit();
            //System.Threading.Thread.Sleep(20000);
            // btnSearch_Click(this, e);
            ZipFileDetail zippageObj = new ZipFileDetail();
            zippageObj.bindgrid("AllCount");
            //Added by Akash on 13 march 24 for loader
            //divProgressBar.Style.Add("display", "none");
            //Added by Akash on 13 march 24 for loader
        }
        #endregion

        protected void btnClr_Click(object sender, EventArgs e)
        {
            ddlSrchTyp.SelectedIndex = 0;
            txtNum.Text = "";
            DDStatus.SelectedIndex = 0;
            btnSearch_Click(this, e);
            divVal.Attributes.Add("style", "display:none;");
            divValTyp.Attributes.Add("style", "display:none;");
            btnSearchDtls.Attributes.Add("style", "display:block;");
            btnClr.Attributes.Add("style", "display:none;");
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowHideSearchIcon('btnSearchDtls','EmptyPagePlaceholder_btnClr')", true);
        }

        public void BindDdl(string ParamUsage)
        {
            CommonClass common = new CommonClass();
            htParam.Clear();
            htParam.Add("@LookupCode", "Workbench");
            htParam.Add("@ParamUsage", ParamUsage);
            common.MstFillDropdown("prc_getDDLLookUpData", htParam, ddlSrchTyp, "CKYCConnectionString", true);


        }

        protected void btnSrch_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindDdl(hdnCurrActId.Value);
                divVal.Attributes.Add("style", "display:block;");
                divValTyp.Attributes.Add("style", "display:block;");
                btnSearch_Click(this, e);

                btnSearchDtls.Attributes.Add("style", "display:none;");
                btnClr.Attributes.Add("style", "display:block;");
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowHideSearchIcon('EmptyPagePlaceholder_btnClr','btnSearchDtls')", true);
            }
        }
        //added by Akash on 16 nov 2023
        protected void btnunsoliDwndexcelgenerate_Click(object sender, EventArgs e)
        {
            
                btnSearch_Click(this, e);
                string ckycunsolid = hdnCurrActId.Value;
                //string ckycunsoli=hdnId.Value;
                string ckycunsoli = CheckBoxID;
                string[] values = ckycunsoli.Split(',');

                //DataSet dsexl = new DataSet();
                //dsexl.Clear();

                foreach (string value in values)
                {
                    Hashtable htexl = new Hashtable();
                    htexl.Clear();
                    htexl.Add("@userid", "maker");
                    htexl.Add("@Ckycno", value);
                    //DataAccessLayer Dsa = new DataAccessLayer("CKYCConnectionString");
                    objDAL = new DataAccessLayer("CKYCConnectionString");
                    objDAL.ExecuteScalar("prc_getUnsolitatedDWNDexcelData", htexl);
                }
                //btnSearch_Click(this, e);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "  HideProgressBar();", true);
                //ZipFileDetail zippageObj = new ZipFileDetail();
                //zippageObj.bindcountforgrid("AllCount");
                Response.Redirect("ZipFileDetail.aspx?Flag=01");

            
        }
        //added by Akash on 16 nov 2023
        protected void btnDWNAPI_Click(object sender, EventArgs e)
        {
                try
                {
                    btnSearch_Click(this, e);
                    string ckycunsoli = CheckBoxID;
                    string[] values = ckycunsoli.Split(',');
                    foreach (string value in values)
                    {
                        if (value != "")
                        {
                            Hashtable htdwn = new Hashtable();
                            htdwn.Clear();
                            htdwn.Add("@Ckycno", value);
                            DataTable dt = new DataTable();
                            dt.Clear();
                            //DataAccessLayer Dsa = new DataAccessLayer("CKYCConnectionString");
                            objDAL = new DataAccessLayer("CKYCConnectionString");
                            dt = objDAL.GetDataTable("prc_getDWNDapiDatavalue", htdwn);
                            if (dt.Rows.Count > 0)
                            {
                                string CKYCNO = dt.Rows[0]["CKYC_NO"].ToString();
                                string AUTHFACTORTYPE = dt.Rows[0]["AUTHENTICATION_FACTOR_TYPE"].ToString();
                                string AUTHFACTOR = dt.Rows[0]["AUTHENTICATION_FACTOR"].ToString();

                            //          string apiUrl = @"http://kmi.centralindia.cloudapp.azure.com/ckycwebapi/api/CKYC/CKYCDownload?inputData=<?xml version=""1.0"" encoding=""UTF-8""?>
                            //<REQ_ROOT>
                            //<CKYC_DLD>
                            //    <CKYC_NO>" + CKYCNO + "</CKYC_NO>"
                            //                    + "<AUTH_FACTOR_TYPE>" + AUTHFACTORTYPE + "</AUTH_FACTOR_TYPE>"
                            //                    + "<AUTH_FACTOR>" + AUTHFACTOR + "</AUTH_FACTOR>"
                            //                    + "</CKYC_DLD>"
                            //                    + "</REQ_ROOT>";
                            string apiUrl = @"http://kmi.centralindia.cloudapp.azure.com/CKYCWEBAPI/api/CKYC/CKYCDownload?inputData=<?xml version=""1.0"" encoding=""UTF-8""?>
                      <REQ_ROOT>
                      <CKYC_DLD>
                          <CKYC_NO>" + CKYCNO + "</CKYC_NO>"
                                               + "<AUTH_FACTOR_TYPE>" + AUTHFACTORTYPE + "</AUTH_FACTOR_TYPE>"
                                               + "<AUTH_FACTOR>" + AUTHFACTOR + "</AUTH_FACTOR>"
                                               + "</CKYC_DLD>"
                                               + "</REQ_ROOT>";

                            var client = new HttpClient();
                                var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                                var content = new StringContent("", null, "text/plain");
                                request.Content = content;
                                var response = client.SendAsync(request).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                objDAL.ExecuteScalar("prc_updDWNDapiDatavalue", htdwn);

                            }
                            // HttpResponseMessage response = client.SendAsync(request).Result;
                            Console.WriteLine(response.EnsureSuccessStatusCode());
                                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                                var xmlres = response.Content.ReadAsStringAsync().Result;
                            
                            }
                            else { }
                        }
                    }
                }


                catch (Exception ex)
                {
                    objErr = new ErrorLog();
                    objErr.LogErr(AppID, "SearchDownload.aspx.cs", "PopulateSearchBy", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "CKYC");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Unable to find the details, Please try again later.');", true);
                }


            
        }

        //added by Akash on 12 march 24 
        protected void btnsrchAPI_Click(object sender, EventArgs e)
        {
            try
            {
                btnSearch_Click(this, e);
                string ckycsrch = CheckBoxID;
                string[] srchs = ckycsrch.Split(',');
                foreach (string srch in srchs)
                {
                    if (srch != "")
                    {
                        Hashtable htdwn = new Hashtable();
                        htdwn.Clear();
                        htdwn.Add("@IDENTITY_NUMBER", srch);
                        DataTable dt = new DataTable();
                        dt.Clear();
                        //DataAccessLayer Dsa = new DataAccessLayer("CKYCConnectionString");
                        objDAL = new DataAccessLayer("CKYCConnectionString");
                        dt = objDAL.GetDataTable("prc_getsrchapiDatavalue", htdwn);
                        if (dt.Rows.Count > 0)
                        {
                            string type = dt.Rows[0]["IDENTITY_TYPE"].ToString();
                            string Id = dt.Rows[0]["IDENTITY_NUMBER"].ToString();
                            //string AUTHFACTOR = dt.Rows[0]["AUTHENTICATION_FACTOR"].ToString();

                            //            string apiUrl = @"http://kmi.centralindia.cloudapp.azure.com/ckycwebapi/api/CKYC/CKYCSearch?inputData=<?xml version=""1.0"" encoding=""UTF-8""?>
                            //<REQ_ROOT>
                            // <CKYC_INQ>
                            //     <ID_TYPE>" + type + "</ID_TYPE>"
                            //                     + "<ID_NO>" + Id + "</ID_NO>"
                            //                 + "</CKYC_INQ>"
                            //                + "</REQ_ROOT>";

                            string apiUrl = @"http://kmi.centralindia.cloudapp.azure.com/CKYCWEBAPI/api/CKYC/CKYCSearch?inputData=<?xml version=""1.0"" encoding=""UTF-8""?>
                <REQ_ROOT>
                 <CKYC_INQ>
                     <ID_TYPE>" + type + "</ID_TYPE>"
                                     + "<ID_NO>" + Id + "</ID_NO>"
                                 + "</CKYC_INQ>"
                                + "</REQ_ROOT>";

                            var client = new HttpClient();
                            var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                            var content = new StringContent("", null, "text/plain");
                            request.Content = content;
                            var response = client.SendAsync(request).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                objDAL.ExecuteScalar("prc_updsrchapiDatavalue", htdwn);

                            }
                            // HttpResponseMessage response = client.SendAsync(request).Result;
                            Console.WriteLine(response.EnsureSuccessStatusCode());
                            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                            var xmlres = response.Content.ReadAsStringAsync().Result;

                        }
                        else { }
                    }
                }
            }


            catch (Exception ex)
            {
                objErr = new ErrorLog();
                objErr.LogErr(AppID, "SearchDownload.aspx.cs", "PopulateSearchBy", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Unable to find the details, Please try again later.');", true);
            }



        }
    }
}
