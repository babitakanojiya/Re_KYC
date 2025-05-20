using System;
using KMI.FRMWRK.DAL;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Web.Script.Serialization;
using KMI.FRMWRK.Web.Application.CKYC.Common;
using System.Xml.Linq;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Xml;
using Newtonsoft.Json;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class SearchDownload : System.Web.UI.Page
    {
		//
        ErrorLog objErr;
        CommonUtility oCommonUtility = new CommonUtility();
        int AppID;
        DataAccessLayer objDAL;
        string ckycNo = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                PopulateSearchBy();
                //hdnCurrentTab.Value = "Search";
                //Search.Visible = true;
                //Download.Visible = false;
            }
        }

		protected void btnSubmit_Click(object sender, EventArgs e)
		{
            try
            {
                if (hdnSearch.Value == "Search")
                {

                    Div1.Attributes.Add("style", "margin-top: 0px;display:block;");
                    string type = ddlProofofidn.SelectedValue.ToString().Trim();
                    string Id = txtidnummm.Text;
                    string apiUrl = @"http://kmi.centralindia.cloudapp.azure.com/ckycwebapi/api/CKYC/CKYCSearch?inputData=<?xml version=""1.0"" encoding=""UTF-8""?>
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
                    Console.WriteLine(response.EnsureSuccessStatusCode());
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    var xmlres = response.Content.ReadAsStringAsync().Result;
                    //DataTable dt = new DataTable();
                    //dt.Columns.Add("CKYC_NO", typeof(string));
                    //dt.Columns.Add("NAME", typeof(string));
                    //dt.Columns.Add("FATHERS_NAME", typeof(string));
                    //dt.Columns.Add("AGE", typeof(string));
                    //dt.Columns.Add("IMAGE_TYPE", typeof(string));
                    //dt.Columns.Add("PHOTO", typeof(string));
                    //dt.Columns.Add("KYC_DATE", typeof(string));
                    //dt.Columns.Add("UPDATED_DATE", typeof(string));
                    //dt.Columns.Add("REMARKS", typeof(string));

                    string cleanedInput = Regex.Replace(xmlres, @"\\u0000", "");
                    // Remove escaped characters and extra null characters
                    cleanedInput = Regex.Replace(cleanedInput, @"\\+", "");

                    // Remove leading and trailing double quotes
                    cleanedInput = cleanedInput.Trim('"');

                    XDocument xdoc = XDocument.Parse(cleanedInput);
                    if (!xdoc.Descendants("ERROR").Any())
                    {
                        foreach (XElement record in xdoc.Descendants("PID_DATA").Elements("SearchResponsePID"))
                        {
                            string ckycNo = record.Element("CKYC_NO")?.Value;
                            string name = record.Element("NAME")?.Value;
                            string fathersName = record.Element("FATHERS_NAME")?.Value;
                            string age = record.Element("AGE")?.Value;
                            string imageType = record.Element("IMAGE_TYPE")?.Value;
                            string photo = record.Element("PHOTO")?.Value;
                            string kycDate = record.Element("KYC_DATE")?.Value;
                            string updatedDate = record.Element("UPDATED_DATE")?.Value;
                            string remarks = record.Element("REMARKS")?.Value;
                            string imageBytes = "data:image/png;base64," + photo;
                            string selectedText = string.Empty;
                            string IdDoc = string.Empty;
                            StringBuilder html = new StringBuilder();

                            var types = xdoc.Descendants("ID_LIST")
                                            .Elements("ID")
                                            .Select(id => id.Element("TYPE")?.Value);
                            foreach (var items in types)
                            {
                                //Console.WriteLine("Type: " + items);
                                string targetValue = items; // Replace with the value you want to find
                                foreach (ListItem item in ddlProofofidn.Items)
                                {
                                    if (item.Value == targetValue)
                                    {
                                        selectedText = item.Text;
                                        break; // Exit the loop when a match is found
                                    }
                                }
                                //if(types.Count()>1)
                                //{
                                //	IdDoc = IdDoc + Environment.NewLine + selectedText;
                                //}
                                //else
                                //{
                                //	IdDoc = selectedText;
                                //}

                                //   html.Append("<asp:Label Text='"+selectedText+"'CssClass='control-label' runat='server'");
                                //html.Append("Font-Bold='False'></asp:Label><br />");
                                html.Append("<span class='control-label' style='font-weight:normal;'>" + selectedText);
                                html.Append("</span><br>");
                            }
                            //dt.Rows.Add(ckycNo, name, fathersName, age, imageType, imageBytes, kycDate, updatedDate,remarks);
                            Img1.ImageUrl = imageBytes;
                            lblCkycNoRes.Text = ckycNo;
                            lblFullNm.Text = name;
                            lblFthNm.Text = fathersName;
                            lblKycDt.Text = kycDate;
                            lblAge.Text = age;
                            //lblIdDoc.Text = IdDoc;
                            DivDocC.InnerHtml = html.ToString();
                            lblRmk.Text = remarks;
                        }
                    }
                    else
                    {
                        trgridsponsorship.Attributes.Add("style", "display:none;");
                        Div1.InnerHtml = "<span class='control-label' style='font-weight:normal;color: red;margin-left: 584px;'>No Record Found</span>";
                    }
                    //dgView.DataSource = dt;
                    //dgView.DataBind();
                    div4.Attributes.Add("style", "display:none;");
                    prfofidnty.Attributes.Add("style", "display:block;");
                    dividentynum.Attributes.Add("style", "display:block;");

                }

                else if (hdnSearch.Value == "Download")
                {
                    //string CKYCNO = txtckyno.Text;
                    //string AUTHFACTORTYPE = ddlauthtype.SelectedValue.ToString().Trim();
                    //string AUTHFACTOR = txtauthfactor.Text;
                    //string apiUrl = @"http://kmi.centralindia.cloudapp.azure.com/ckycwebapi/api/CKYC/CKYCDownload?inputData=<?xml version=""1.0"" encoding=""UTF-8""?>
                    //  <REQ_ROOT>
                    //  <CKYC_DLD>
                    //      <CKYC_NO>" + CKYCNO + "</CKYC_NO>"
                    //          + "<AUTH_FACTOR_TYPE>" + AUTHFACTORTYPE + "</AUTH_FACTOR_TYPE>"
                    //          + "<AUTH_FACTOR>" + AUTHFACTOR + "</AUTH_FACTOR>"
                    //          + "</CKYC_DLD>"
                    //          + "</REQ_ROOT>";

                    //var client = new HttpClient();
                    //var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                    //var content = new StringContent("", null, "text/plain");
                    //request.Content = content;
                    //var response = client.SendAsync(request).Result;
                    //// HttpResponseMessage response = client.SendAsync(request).Result;
                    //Console.WriteLine(response.EnsureSuccessStatusCode());
                    //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    //var xmlres = response.Content.ReadAsStringAsync().Result;
                    //string cleanedInput = Regex.Replace(xmlres, @"\\u0000", "");
                    //// Remove escaped characters and extra null characters
                    //cleanedInput = Regex.Replace(cleanedInput, @"\\+", "");

                    //// Remove leading and trailing double quotes
                    //cleanedInput = cleanedInput.Trim('"');
                    ////string ckycNo = row["CKYC_NO"].ToString();

                    //DataSet dataSet = new DataSet();
                    //dataSet.ReadXml(new System.IO.StringReader(cleanedInput));
                    //if (dataSet.Tables.Contains("PERSONAL_DETAILS"))
                    //{
                    //    DataTable personalDetailsTable = dataSet.Tables["PERSONAL_DETAILS"];
                    //    if (personalDetailsTable != null)
                    //    {
                    //        DataRow row = personalDetailsTable.Rows[0];
                    //        ckycNo = row["CKYC_NO"].ToString();
                    //    }

                    //}

                    //Response.Redirect("CKYCViewDetails.aspx?Status=view&refno=null&kycno=" + ckycNo + "&flag=DWNL");

                    string ckycNo = txtckyno.Text;
                    Response.Redirect("CKYCViewDetails.aspx?Status=view&refno=null&kycno=" + ckycNo + "&flag=DWNL");



                }


            }

            catch (Exception ex)
            {
                objErr = new ErrorLog();
                objErr.LogErr(AppID, "SearchDownload.aspx.cs", "PopulateSearchBy", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "", "CKYC");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('Unable to find the details, Please try again later.');", true);
            }
		}

		protected void btnCancel_Click(object sender, EventArgs e)
        {

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


    }
}