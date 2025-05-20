using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Text;
using KMI.FRMWRK.DAL;
using System.Collections;
using System.Security;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace KMI.FRMWRK.Web.Application.CKYC
{

    public partial class ZipFileDetail : System.Web.UI.Page
    {
        public DataTable dt;
        public string FlagPageTyp;
        DataAccessLayer dataAccessLayer;
        DataTable dtCRS_to_MW = new DataTable();
        DataRow drCRS_to_MW;
        public static DataTable dtFindTxt = new DataTable();
        public void binddtCRS_to_MW(string img, string Name, string Record, string File_Length, string File_Extension, string Creation_Date_Time, string Mode, string Location)
        {
            if (drCRS_to_MW == null)
            {
                dtCRS_to_MW.Columns.Add(new System.Data.DataColumn("Img", typeof(String)));
                dtCRS_to_MW.Columns.Add(new System.Data.DataColumn("Name", typeof(String)));
                dtCRS_to_MW.Columns.Add(new System.Data.DataColumn("Records", typeof(String)));
                dtCRS_to_MW.Columns.Add(new System.Data.DataColumn("Length", typeof(String)));
                dtCRS_to_MW.Columns.Add(new System.Data.DataColumn("Extension", typeof(String)));
                dtCRS_to_MW.Columns.Add(new System.Data.DataColumn("CreationTime", typeof(String)));
                dtCRS_to_MW.Columns.Add(new System.Data.DataColumn("Location", typeof(String)));
                dtCRS_to_MW.Columns.Add(new System.Data.DataColumn("Mode", typeof(String)));
            }
            drCRS_to_MW = dtCRS_to_MW.NewRow();
            if (drCRS_to_MW["Name"] != Name)
            {
                drCRS_to_MW = dtCRS_to_MW.NewRow();
                drCRS_to_MW["Img"] = img.ToString();
                drCRS_to_MW["Name"] = Name.ToString();
                drCRS_to_MW["Records"] = Record.ToString();
                drCRS_to_MW["Length"] = File_Length.ToString();
                drCRS_to_MW["Extension"] = File_Extension.ToString();
                drCRS_to_MW["CreationTime"] = Creation_Date_Time.ToString();
                drCRS_to_MW["Location"] = Location.ToString();
                drCRS_to_MW["Mode"] = Mode.ToString();
                dtCRS_to_MW.Rows.Add(drCRS_to_MW);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string str1 = searchTextBox.Text;
            string str2 = searchtextbox2.Text;


            if (str1 != "")
            {
                SetDefaultButton(searchTextBox, lnkdelete);

            }
            if (str2 != "")
            {
                SetDefaultButton(searchtextbox2, Button1);
            }
            if (Request.QueryString["Flag"].ToString() != null)
            {
                FlagPageTyp = Request.QueryString["Flag"].ToString();
            }
            if (!IsPostBack)
            {
                ViewState["FileType"] = "All";     // GET THE FILE TYPE.
                                                   //                ViewState["Length"] = "KB";

                bindgrid("AllCount");
                bindgrid("UnsolitatedNotificationCount");
                GetOutputFilesFromFolder();
                GetInputFilesFromFolder();
                lblTime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            }
            if (!IsPostBack)
            {
                try
                {
                    //GridView1.DataSource = dtCRS_to_MW;
                    //GridView1.DataBind();
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void GetOutputFilesFromFolder()
        {
            // GET A LIST OF FILES FROM A SPECIFILED FOLDER.

            dataAccessLayer = new DataAccessLayer("UpdDwnldConnectionString");

            Hashtable hTableIN = new Hashtable();
            DataTable dTblIN = new DataTable();
            dTblIN.Clear();


            dTblIN = dataAccessLayer.GetDataTable("Prc_GetInputAllFile", hTableIN);

            foreach (DataRow row in dTblIN.Rows)
            {
                //dTblIN.Columns.Contains("_Error");
                //{
                //   dTblIN.Columns.s
                //}
                string Ext = row.Field<string>("Extension");
                if (Ext == ".txt")
                {
                    row[2] = "../../assets/images/dashboard-icon/txt_file_icon.png";
                    //img = "../../assets/images/dashboard-icon/txt_file_icon.png";
                }
                if (Ext == ".zip")
                {
                    row[2] = "../../assets/images/dashboard-icon/zip_icon_New.png";
                    // img = "../../assets/images/dashboard-icon/zip_icon_New.png";
                }
                if (Ext == ".csv")
                {
                    row[2] = "../../assets/images/dashboard-icon/CSV_file_icon.png";
                    //img = "../../assets/images/dashboard-icon/CSV_file_icon.png";
                }
                if (Ext == ".trg")
                {
                    row[2] = "../../assets/images/dashboard-icon/TRG_icon.png";
                    //img = "../../assets/images/TRG_icon.png";
                }

            }
            GridView2.DataSource = dTblIN;
            //GridView1.DataSource = bindDtable(dTblIN);
            GridView2.DataBind();


            //DirectoryInfo objDir = new DirectoryInfo(@"C:\HostedApplications\CKYC\Demo\CKYC_DEMO_DEV_APP\CKYC\File\InputFiles\");

            //FileInfo[] listfiles = objDir.GetFiles("*." + ((string)ViewState["FileType"] != "All" ? ViewState["FileType"] : "*")).OrderByDescending(p => p.CreationTime).ToArray();
            //if (listfiles.Length > 0)
            //{

            //    GridView1.Visible = true;
            //    GridView1.DataSource = listfiles;
            //    GridView1.DataBind();
            //    GridView2.Visible = false;
            //    lblMsg.Text = listfiles.Length + " files found";
            //    for (int i = 0; i < listfiles.Length; i++)
            //    {
            //        Image img = (Image)GridView1.Rows[i].FindControl("ImgFiletyp");
            //        if (listfiles[i].Extension == ".txt")
            //        {
            //            img.ImageUrl = "../../assets/images/dashboard-icon/txt_file_icon.png";
            //        }
            //        if (listfiles[i].Extension == ".zip")
            //        {
            //            img.ImageUrl = "../../assets/images/dashboard-icon/zip_icon_New.png";
            //        }
            //        if (listfiles[i].Extension == ".trg")
            //        {
            //            img.ImageUrl = "../../assets/images/TRG_icon.png";
            //        }
            //    }
            //}
            //else
            //{
            //    GridView1.Visible = false;
            //    lblMsg.Text = "No files found";
            //}
        }
        protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            //GridView1.PageIndex = e.NewPageIndex;
            //GetOutputFilesFromFolder();

            string OutputFiles = searchTextBox.Text;
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"" + ConfigurationManager.AppSettings.GetValues("InputArchive")[0].ToString());

            if (OutputFiles != "")
            {
                FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + OutputFiles + "*.*");
                dt = bindDtable(filesInDir);
               
            }
            else
            {
                //DirectoryInfo objDir = new DirectoryInfo(@"" + ConfigurationManager.AppSettings.GetValues("OutputFiles")[0].ToString());
                //FileInfo[] listfiles = objDir.GetFiles("*." + ((string)ViewState["FileType"] != "All" ? ViewState["FileType"] : "*")).OrderByDescending(p => p.CreationTime).ToArray();
                //dt =  bindDtable(listfiles);
                bindgrid("UnsolitatedNotificationCount");
                GetInputFilesFromFolder();
                dt = dtCRS_to_MW;

            }
            DataView dv = new DataView(dt);
            GridView dgSource = (GridView)sender;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = dv;
            GridView1.DataBind();
        }

        //Below added by Rutuja
        public string img = "";
        private void GetInputFilesFromFolder()
        {
            // GET A LIST OF FILES FROM A SPECIFILED FOLDER.
            //DirectoryInfo objDir = new DirectoryInfo(@"E:\BitBucket\ckyc_base\Applications\Doc\InputFiles\");
            // DirectoryInfo objDir = new DirectoryInfo(@"C:\HostedApplications\CKYC\Demo\CKYC_DEMO_DEV_APP\CKYC\File\InputFiles\");
            // DirectoryInfo objDir = new DirectoryInfo(@"C:\HostedApplications\CKYC\Demo\CKYC_DEMO_DEV_APP\CKYC\File\OutputFiles\");
            //DirectoryInfo objDir = new DirectoryInfo(@"" + ConfigurationManager.AppSettings.GetValues("InputFiles")[0].ToString());
            //// DirectoryInfo objDir = new DirectoryInfo(@"E:\During WFH\CKYC\Demo\CKYC_DEMO_DEV_APP\CKYC\File\InputFiles\");

            //FileInfo[] listfiles = objDir.GetFiles("*." + ((string)ViewState["FileType"] != "All" ? ViewState["FileType"] : "*")).OrderByDescending(p => p.CreationTime).ToArray();
            //if (listfiles.Length > 0)
            //{
            //    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "formatSizeValue(listfiles.Length);", true);
            //    // BIND THE LIST OF FILES (IF ANY) WITH GRIDVIEW.
            //    GridView1.Visible = true;
            //    //GridView1.DataSource = listfiles;
            //    //GridView1.DataBind();
            //    foreach (var item in listfiles)
            //    {
            //        if (item.Extension == ".txt")
            //        {
            //            img = "../../assets/images/dashboard-icon/txt_file_icon.png";
            //        }
            //        if (item.Extension == ".zip")
            //        {
            //            img = "../../assets/images/dashboard-icon/zip_icon_New.png";
            //        }

            //        if (item.Extension == ".csv")
            //        {
            //            img = "../../assets/images/dashboard-icon/CSV_file_icon.png";
            //        }

            //        if (item.Extension == ".trg")
            //        {
            //            img = "../../assets/images/TRG_icon.png";
            //        }
            //        string[] sizes = { "Bytes", "KB", "MB", "GB", "TB" };
            //        double len = item.Length;
            //        int order = 0;
            //        while (len >= 1024 && order < sizes.Length - 1)
            //        {
            //            order++;
            //            len = len / 1024;
            //        }

            //        // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            //        // show a single decimal place, and no space.
            //        string result = String.Format("{0:0.##} {1}", len, sizes[order]);
            //        binddtCRS_to_MW(img, item.Name, "", result, item.Extension.ToString(), item.CreationTime.ToString(), "SFTP", "InputFiles");
            //        //binddtCRS_to_MW(img, item.Name, "", item.Length.ToString(), item.Extension.ToString(), item.CreationTime.ToString(), "SFTP");
            //    }

            //    lblMsg2.Text = listfiles.Length + " files found";
            //    for (int i = 0; i < listfiles.Length; i++)
            //    {
            //        Image img = null;// (Image)GridView1.Rows[i].FindControl("ImgFiletyp");
            //        if (img != null)
            //        {
            //            if (listfiles[i].Extension == ".txt")
            //            {
            //                img.ImageUrl = "../../assets/images/dashboard-icon/txt_file_icon.png";
            //            }
            //            if (listfiles[i].Extension == ".zip")
            //            {
            //                img.ImageUrl = "../../assets/images/dashboard-icon/zip_icon_New.png";
            //            }
            //            if (listfiles[i].Extension == ".csv")
            //            {
            //                img.ImageUrl = "../../assets/images/dashboard-icon/CSV_file_icon.png";
            //            }
            //            if (listfiles[i].Extension == ".trg")
            //            {
            //                img.ImageUrl = "../../assets/images/TRG_icon.png";
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    //GridView2.Visible = false;
            //    lblMsg2.Text = "No files found";
            //}

            dataAccessLayer = new DataAccessLayer("UpdDwnldConnectionString");

            Hashtable hTableIN = new Hashtable();
            DataTable DTinputfile = new DataTable();
            DTinputfile.Clear();


            DTinputfile = dataAccessLayer.GetDataTable("Prc_GetInputAllFileFromCKYCForGrid", hTableIN);

            foreach (DataRow row in DTinputfile.Rows)
            {
                //dTblIN.Columns.Contains("_Error");
                //{
                //   dTblIN.Columns.s
                //}
                string Ext = row.Field<string>("Extension");
                if (Ext == ".txt")
                {
                    row[2] = "../../assets/images/dashboard-icon/txt_file_icon.png";
                    //img = "../../assets/images/dashboard-icon/txt_file_icon.png";
                }
                if (Ext == ".zip")
                {
                    row[2] = "../../assets/images/dashboard-icon/zip_icon_New.png";
                    // img = "../../assets/images/dashboard-icon/zip_icon_New.png";
                }
                if (Ext == ".csv")
                {
                    row[2] = "../../assets/images/dashboard-icon/CSV_file_icon.png";
                    //img = "../../assets/images/dashboard-icon/CSV_file_icon.png";
                }
                if (Ext == ".trg")
                {
                    row[2] = "../../assets/images/dashboard-icon/TRG_icon.png";
                    //img = "../../assets/images/TRG_icon.png";
                }


            }
            GridView1.DataSource = DTinputfile;
            //GridView1.DataSource = bindDtable(dTblIN);
            GridView1.DataBind();
        }
        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //GridView2.PageIndex = e.NewPageIndex;
            //GetInputFilesFromFolder();
            string inputpartialName = searchtextbox2.Text;
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"" + ConfigurationManager.AppSettings.GetValues("OutputArchive")[0].ToString());

            if (inputpartialName != "")
            {
                FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + inputpartialName + "*.*");
                dt = bindDtable(filesInDir);
            }
            else
            {
                dataAccessLayer = new DataAccessLayer("UpdDwnldConnectionString");
                Hashtable hTableIN = new Hashtable();
                dt = dataAccessLayer.GetDataTable("Prc_GetInputAllFile", hTableIN);

            }
            DataView dv = new DataView(dt);
            GridView dgSource = (GridView)sender;
            GridView2.PageIndex = e.NewPageIndex;
            GridView2.DataSource = dv;
            GridView2.DataBind();

        }

        protected void lnkdelete_Click(object sender, EventArgs e)
        {

            string outputpartialName = searchTextBox.Text;
            string inputpartialName = searchtextbox2.Text;
            if (outputpartialName != "")
            {
                //DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"C:\HostedApplications\CKYC\Demo\CKYC_DEMO_DEV_APP\CKYC\File\Output Archive\");
                //DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"" + ConfigurationManager.AppSettings.GetValues("InputArchive")[0].ToString());
                ////DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"E:\BitBucket\ckyc_base\Applications\Doc\Output Archive\");
                //FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + outputpartialName + "*.*");
                //Added by Akash on 13 dec 2023 for search 
                dataAccessLayer = new DataAccessLayer("UpdDwnldConnectionString");

                Hashtable hTableINsearch = new Hashtable();
                DataTable DTinputfileserch = new DataTable();
                DTinputfileserch.Clear();
                hTableINsearch.Clear();
                hTableINsearch.Add("@FileName", outputpartialName);
                DTinputfileserch = dataAccessLayer.GetDataTable("Prc_GetInputAllFileFromCKYCForGridsearch", hTableINsearch);

                foreach (DataRow row in DTinputfileserch.Rows)
                {
                    //dTblIN.Columns.Contains("_Error");
                    //{
                    //   dTblIN.Columns.s
                    //}
                    string Ext = row.Field<string>("Extension");
                    if (Ext == ".txt")
                    {
                        row[2] = "../../assets/images/dashboard-icon/txt_file_icon.png";
                        //img = "../../assets/images/dashboard-icon/txt_file_icon.png";
                    }
                    if (Ext == ".zip")
                    {
                        row[2] = "../../assets/images/dashboard-icon/zip_icon_New.png";
                        // img = "../../assets/images/dashboard-icon/zip_icon_New.png";
                    }
                    if (Ext == ".csv")
                    {
                        row[2] = "../../assets/images/dashboard-icon/CSV_file_icon.png";
                        //img = "../../assets/images/dashboard-icon/CSV_file_icon.png";
                    }
                    if (Ext == ".trg")
                    {
                        row[2] = "../../assets/images/dashboard-icon/TRG_icon.png";
                        //img = "../../assets/images/TRG_icon.png";
                    }


                }
                
                //ended by akash on 13 dec 23 for search 

                GridView1.Visible = true;
                GridView2.Visible = true;
                GridView1.DataSource = DTinputfileserch;
                GridView1.DataBind();
                //comented by akash on 13 dec 23 for search 
                //foreach (FileInfo foundFile in filesInDir)
                //{
                //    GridView1.Visible = true;
                //    //GridView1.DataSource = filesInDir;
                //    //GridView1.DataBind();
                //    lblMsg.Text = filesInDir.Length + " files found";
                //    //divInput.Attributes.Add("style", "display:none");
                //    //string fullName = foundFile.FullName;
                //    //Console.WriteLine(fullName);
                //}
                //comented by akash on 13 dec 23 for search 
            }
            if (inputpartialName != "")
            {
                DataTable dt = new DataTable();
                //DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"C:\HostedApplications\CKYC\Demo\CKYC_DEMO_DEV_APP\CKYC\File\Input Archive\");
                DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"" + ConfigurationManager.AppSettings.GetValues("OutputArchive")[0].ToString());
                //DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"E:\BitBucket\ckyc_base\Applications\Doc\Input Archive\");
                //string[] filesInDirN = hdDirectoryInWhichToSearch.GetFiles("*" + inputpartialName + "*.*");
                FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + inputpartialName + "*.*");
                //dt.Columns.Add("Name");
                //dt.Columns.Add("File Length");
                //dt.Columns.Add("Extension");
                //dt.Columns.Add("Creation Date & Time");
                //dt.Columns.Add("View");
                GridView1.Visible = true;
                GridView2.Visible = true;
                GridView2.DataSource = bindDtable(filesInDir);
                GridView2.DataBind();
                foreach (FileInfo foundFile in filesInDir)
                {
                    //dt.Rows.Add(foundFile);
                    //dt.Columns.Add("FileFlag");
                    GridView2.Visible = true;
                    //GridView2.DataSource = filesInDir;
                    //GridView2.DataBind();
                    //                GridView1.Visible = false;
                    lblMsg.Text = filesInDir.Length + " files found";
                    //divOutput.Attributes.Add("style", "display:none");
                    //string fullName = foundFile.FullName;
                    //Console.WriteLine(fullName);
                }


            }
        }
        private void SetDefaultButton(TextBox txt, Button defaultButton)

        {
            txt.Attributes.Add("onkeydown", "funfordefautenterkey(" + defaultButton.ClientID + ",event)");
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            string partialName = searchtextbox2.Text;
            //DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"C:\HostedApplications\CKYC\Demo\CKYC_DEMO_DEV_APP\CKYC\File\Input Archive\");
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"" + ConfigurationManager.AppSettings.GetValues("InputArchive")[0].ToString());
            //DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"E:\BitBucket\ckyc_base\Applications\Doc\Input Archive\");
            FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + partialName + "*.*");

            foreach (FileInfo foundFile in filesInDir)
            {
                GridView2.Visible = true;
                GridView2.DataSource = filesInDir;
                GridView2.DataBind();
                //                GridView1.Visible = false;
                lblMsg.Text = filesInDir.Length + " files found";
                divOutput.Attributes.Add("style", "display:none");
                //string fullName = foundFile.FullName;
                //Console.WriteLine(fullName);
            }
        }

        protected void btnViewMiddltoCERSAI_Click(object sender, EventArgs e)
        {
            try
            {

                GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
                Label lblName = (Label)row.FindControl("lblName");
                Label lblLoc = (Label)row.FindControl("lblLoc");
                if (lblName.Text == "Unsolicited Notification")
                {
                    btnViewMiddltoFI_Click(sender, e);
                    //bindgrid("Bulk Unsolicited Notification");
                }
                else if (lblName.Text != " Unsolicited Notification" && searchTextBox.Text != "")
                {
                    //if (lblLoc.Text == "InputFiles")
                    //{
                        string FilePath = @"http:\\\\20.193.230.174\\CKYCDOC\\InputFiles\\" + lblName.Text.ToString().Trim();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ViewDocument('" + FilePath.ToString().Trim() + "')", true);
                    //}
                    //if (lblLoc.Text == "Input Archive")
                    //{
                    //    string FilePath = @"http:\\\\20.193.230.174\\CKYCDOC\\Input Archive\\" + lblName.Text.ToString().Trim();
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ViewDocument('" + FilePath.ToString().Trim() + "')", true);
                    //}
                }
                else
                {
                    //string FilePath = @"http:\\\\20.193.230.174\\CKYCDOC\\Input Archive\\" + lblName.Text.ToString().Trim();
                    string FilePath = @"http:\\\\20.193.230.174\\CKYCDOC\\InputFiles\\" + lblName.Text.ToString().Trim();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ViewDocument('" + FilePath.ToString().Trim() + "')", true);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private SecureString ConvertToSecureString(string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            var securePassword = new SecureString();

            foreach (char c in password)
                securePassword.AppendChar(c);

            securePassword.MakeReadOnly();
            return securePassword;
        }

        protected void btnViewCERSAItoMiddl_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
                Label lblFileType = (Label)row.FindControl("lblFileType");
                Label lblName = (Label)row.FindControl("lblName");
                Label FileFlags = (Label)row.FindControl("FileFlags");

                //if (FileFlags.Text == "M")
                //{
                //    string FilePath = @"http:\\\\20.193.230.174\\CKYCDOC\\InputFiles\\" + lblName.Text.ToString().Trim();
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ViewDocument('" + FilePath.ToString() + "')", true);
                //}
                //else if (FileFlags.Text == "C")
                //{
                //if(lblName.Text.Contains !="")
                if (searchtextbox2.Text == "")
                {
                    DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"" + ConfigurationManager.AppSettings.GetValues("Registration")[0].ToString());
                    //DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"http:\\\\20.193.230.174\\CKYCDOC\\OutputFiles\\Registration\\");
                    FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + lblName.Text.ToString() + "*.*");
                    if (filesInDir.Count() != 0)
                    {
                        string FilePath = @"http:\\\\20.193.230.174\\CKYCDOC\\OutputFiles\\Registration\\" + (lblName.Text.ToString().Trim() + lblFileType.Text.ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ViewDocument('" + FilePath.ToString() + "')", true);
                    }
                    hdDirectoryInWhichToSearch = new DirectoryInfo(@"" + ConfigurationManager.AppSettings.GetValues("Search")[0].ToString());
                    // DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"http:\\\\20.193.230.174\\CKYCDOC\\OutputFiles\\Search\\");
                    filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + lblName.Text.ToString() + "*.*");
                    if (filesInDir.Count() != 0)
                    {
                        string FilePath = @"http:\\\\20.193.230.174\\CKYCDOC\\OutputFiles\\Search\\" + (lblName.Text.ToString().Trim() + lblFileType.Text.ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ViewDocument('" + FilePath.ToString() + "')", true);

                    }
                    hdDirectoryInWhichToSearch = new DirectoryInfo(@"" + ConfigurationManager.AppSettings.GetValues("Download")[0].ToString());
                    //DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"http:\\\\20.193.230.174\\CKYCDOC\\OutputFiles\\Download\\");
                    filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + lblName.Text.ToString() + "*.*");
                    if (filesInDir.Count() != 0)
                    {
                        string FilePath = @"http:\\\\20.193.230.174\\CKYCDOC\\OutputFiles\\Download\\" + (lblName.Text.ToString().Trim() + lblFileType.Text.ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ViewDocument('" + FilePath.ToString() + "')", true);

                    }
                    hdDirectoryInWhichToSearch = new DirectoryInfo(@"" + ConfigurationManager.AppSettings.GetValues("Update")[0].ToString());
                    //DirectoryInfo  hdDirectoryInWhichToSearch = new DirectoryInfo(@"http:\\\\20.193.230.174\\CKYCDOC\\OutputFiles\\Update\\");
                    filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + lblName.Text.ToString() + "*.*");
                    //if (filesInDir.Count() != 0)
                    //{
                    //    string FilePath = @"http:\\\\20.193.230.174\\CKYCDOC\\OutputFiles\\Update\\" + (lblName.Text.ToString().Trim() + lblFileType.Text.ToString());
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ViewDocument('" + FilePath.ToString() + "')", true);

                    //}
                    //added by babita on 12 april 2025 for zip file download 

                    if (filesInDir.Count() != 0)
                    {
                        string fileName = lblName.Text.Trim() + lblFileType.Text.Trim();
                        string FilePath = "http://20.193.230.174/CKYCDOC/OutputFiles/Update/" + fileName;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), $"window.open('{FilePath}', '_blank');", true);
                    }
                    //ended by babita on 12 april 2025 for zip file download 
                    hdDirectoryInWhichToSearch = new DirectoryInfo(@"" + ConfigurationManager.AppSettings.GetValues("OutputArchive")[0].ToString());
                    //DirectoryInfo  hdDirectoryInWhichToSearch = new DirectoryInfo(@"http:\\\\20.193.230.174\\CKYCDOC\\OutputFiles\\Update\\");
                    filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + lblName.Text.ToString() + "*.*");
                    //if (filesInDir.Count() != 0)
                    //{
                    //    string FilePath = @"http:\\\\20.193.230.174\\CKYCDOC\\Output Archive\\" + (lblName.Text.ToString().Trim() + lblFileType.Text.ToString());
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ViewDocument('" + FilePath.ToString() + "')", true);

                    //}
                    //added by babita on 12 april 2025 for zip file download 
                    if (filesInDir.Count() != 0)
                    {
                        string fileName = lblName.Text.Trim() + lblFileType.Text.Trim();
                        string FilePath = "http://20.193.230.174/CKYCDOC/Output%20Archive/" + fileName;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), $"window.open('{FilePath}', '_blank');", true);
                    }
                    //ended by babita on 12 april 2025 for zip file download 

                }
                else
                {
                    string FilePath = @"http:\\\\20.193.230.174\\CKYCDOC\\Output Archive\\" + lblName.Text.ToString().Trim();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ViewDocument('" + FilePath.ToString() + "')", true);
                    //}

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Added By Shubham
        public void bindgrid(string flag)
        {
            try
            {
                DataTable dTbl = new DataTable();
                dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
                dTbl.Clear();
                Hashtable hTable = new Hashtable();
                hTable.Clear();
                hTable.Add("@Flag", flag);
                hTable.Add("@CustType", FlagPageTyp);
                dTbl = dataAccessLayer.GetDataTable("PRC_BindMiddltoFIData", hTable);
                dtFindTxt = dTbl;
                txtSrchFIStoMW.Text = "";
                if (flag == "AllCount")
                {
                    GridView3.DataSource = dTbl;
                    GridView3.DataBind();
                    //Gridunsolitateddata.Visible = false;
                }
                if (flag == "UnsolitatedNotificationCount")
                {
                    GridView4.DataSource = dTbl;
                    GridView4.DataBind();
                    for (int i = dTbl.Rows.Count - 1; i <= dTbl.Rows.Count; i--)
                    {
                        binddtCRS_to_MW("../../assets/images/CKYC_Table.png", dTbl.Rows[i]["TName"].ToString(), dTbl.Rows[i]["Records"].ToString(), "", "", dTbl.Rows[i]["Creation"].ToString(), dTbl.Rows[i]["Mode"].ToString(), "db");
                    }
                }
                if (flag == "Search")
                {
                    txtSrchFIStoMW.Attributes.Add("placeholder", "Search by Identity No");
                    gvBulkSearch.DataSource = dTbl;
                    gvBulkSearch.DataBind();
                    gvBulkDownload.Visible = false;
                    gvBulkSearch.Visible = true;
                    gvFIUpdate.Visible = false;
                    gvUnsolitatedNotification.Visible = false;
                    gvBulkUpload.Visible = false;
                    Gridunsolitateddata.Visible = false;
                }
                if (flag == "Download")
                {
                    txtSrchFIStoMW.Attributes.Add("placeholder", "Search by CKYC Number");
                    gvBulkDownload.DataSource = dTbl;
                    gvBulkDownload.DataBind();
                    gvBulkSearch.Visible = false;
                    gvBulkDownload.Visible = true;
                    gvFIUpdate.Visible = false;
                    gvUnsolitatedNotification.Visible = false;
                    gvBulkUpload.Visible = false;
                    Gridunsolitateddata.Visible = false;
                }
                if (flag == "Registration")
                {
                    txtSrchFIStoMW.Attributes.Add("placeholder", "Search by FIREFNO CKYC");
                    gvBulkUpload.DataSource = dTbl;
                    gvBulkUpload.DataBind();
                    if (FlagPageTyp == "01")
                    {
                        if ((gvBulkUpload.Columns[21]).HeaderText.Contains("ENTITY NAME"))
                        {
                            gvBulkUpload.Columns[21].Visible = false;
                        }
                        if ((gvBulkUpload.Columns[40]).HeaderText.Contains("DOB DATE OF INCORPORATION"))
                        {
                            gvBulkUpload.Columns[40].Visible = false;
                        }
                        if ((gvBulkUpload.Columns[41]).HeaderText.Contains("PLACE OF INCORPORATION"))
                        {
                            gvBulkUpload.Columns[41].Visible = false;
                        }
                        if ((gvBulkUpload.Columns[42]).HeaderText.Contains("DATE OF COMMENCEMENT OF BUSINESS"))
                        {
                            gvBulkUpload.Columns[42].Visible = false;
                        }
                        if ((gvBulkUpload.Columns[43]).HeaderText.Contains("COUNTRY OF INCORPORATION"))
                        {
                            gvBulkUpload.Columns[43].Visible = false;
                        }
                    }
                    gvBulkSearch.Visible = false;
                    gvBulkDownload.Visible = false;
                    gvBulkUpload.Visible = true;
                    gvFIUpdate.Visible = false;
                    gvUnsolitatedNotification.Visible = false;
                    Gridunsolitateddata.Visible = false;
                }
                //if (flag == "Unsolicited Notification")
                //{
                //    gvUnsolitatedNotification.DataSource = dTbl;
                //    gvUnsolitatedNotification.DataBind();
                //    gvBulkSearch.Visible = false;
                //    gvBulkDownload.Visible = false;
                //    gvFIUpdate.Visible = false;
                //    gvUnsolitatedNotification.Visible = true;
                //    gvBulkUpload.Visible = false;
                //    Gridunsolitateddata.Visible = false;
                //}
                if (flag == "Update")
                {
                    txtSrchFIStoMW.Attributes.Add("placeholder", "Search by FIREFNO CKYC");
                    gvFIUpdate.DataSource = dTbl;
                    gvFIUpdate.DataBind();
                    gvBulkSearch.Visible = false;
                    gvBulkDownload.Visible = false;
                    gvFIUpdate.Visible = true;
                    gvUnsolitatedNotification.Visible = false;
                    gvBulkUpload.Visible = false;
                    Gridunsolitateddata.Visible = false;
                }
                //added by babita
                if (flag == "Unsolicited")
                {
                    Gridunsolitateddata.DataSource = dTbl;
                    Gridunsolitateddata.DataBind();
                    Gridunsolitateddata.Visible = true;
                    gvBulkSearch.Visible = false;
                    gvBulkDownload.Visible = false;
                    gvFIUpdate.Visible = true;
                    gvUnsolitatedNotification.Visible = false;
                    gvBulkUpload.Visible = false;
                }
                //ended by babita

            }
            catch (Exception ex1)
            {
            }
        }

        protected void btnViewMiddltoFI_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
            Label lblName = ((Label)row.FindControl("lblTName") != null ? (Label)row.FindControl("lblTName") : (Label)row.FindControl("lblName"));
            Label lblRecords = (Label)row.FindControl("lblRecords");
            if (true)
            {
                bindgrid(lblName.Text.ToString().Trim());
                hdnFlagFIStoMW.Value = lblName.Text.ToString();
            }
            else
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg(No Record Found);", true); }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowHideModal();", true);

        }

        protected void lblStatus_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
            Label lblFIREFNO_CKYC = (Label)row.FindControl("lblFIREFNO_CKYC");
            var RefNoForError = lblFIREFNO_CKYC.Text;
            DataTable dTbl2 = new DataTable();
            dataAccessLayer = new DataAccessLayer("UpdDwnldConnectionString");
            dTbl2.Clear();
            Hashtable hTable2 = new Hashtable();

            hTable2.Add("@RefNo", RefNoForError);
            dTbl2 = dataAccessLayer.GetDataTable("Prc_BindErrorGrid_ErrDesc", hTable2);
            gvErrorSearch.DataSource = dTbl2;
            gvErrorSearch.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowHideModal23();", true);
        }

        protected void gvBulkUpload_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Namecolumnvalue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Status"));
                LinkButton lnk2 = (LinkButton)e.Row.FindControl("lblStatus");
                if (Namecolumnvalue == "FAIL")
                {
                    lnk2.Enabled = true;
                }
                else
                {
                    lnk2.Enabled = false;
                }
            }
        }

        protected void lblStatus2_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).NamingContainer as GridViewRow;
            Label lblFIREFNO_CKYC = (Label)row.FindControl("lblFIREFNO_CKYC");
            var RefNoForError = lblFIREFNO_CKYC.Text;
            DataTable dTbl2 = new DataTable();
            dataAccessLayer = new DataAccessLayer("UpdDwnldConnectionString");
            dTbl2.Clear();
            Hashtable hTable2 = new Hashtable();

            hTable2.Add("@RefNo", RefNoForError);
            hTable2.Add("@flag", "UPD");
            dTbl2 = dataAccessLayer.GetDataTable("Prc_BindErrorGrid_ErrDesc", hTable2);
            gvErrorSearch.DataSource = dTbl2;
            gvErrorSearch.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowHideModal23();", true);
        }
        public DataTable bindDtable(FileInfo[] filesInDir)
        {
            DataTable DynDtable = new DataTable();
            DataRow DynDrow;
            DynDtable.Columns.Add(new System.Data.DataColumn("Img", typeof(String)));
            DynDtable.Columns.Add(new System.Data.DataColumn("Name", typeof(String)));
            DynDtable.Columns.Add(new System.Data.DataColumn("Records", typeof(String)));
            DynDtable.Columns.Add(new System.Data.DataColumn("Length", typeof(String)));
            DynDtable.Columns.Add(new System.Data.DataColumn("Extension", typeof(String)));
            DynDtable.Columns.Add(new System.Data.DataColumn("CreationTime", typeof(String)));
            DynDtable.Columns.Add(new System.Data.DataColumn("Location", typeof(String)));
            DynDtable.Columns.Add(new System.Data.DataColumn("Mode", typeof(String)));

            if (filesInDir.Length != 0)
            {
                foreach (var item in filesInDir)
                {
                    if (item.Extension == ".txt")
                    {
                        img = "../../assets/images/dashboard-icon/txt_file_icon.png";
                    }
                    if (item.Extension == ".zip")
                    {
                        img = "../../assets/images/dashboard-icon/zip_icon_New.png";
                    }
                    if (item.Extension == ".csv")
                    {
                        img = "../../assets/images/dashboard-icon/CSV_file_icon.png";
                    }
                    //if (item.Extension == ".trg")
                    //{
                    //    img = "../../assets/images/TRG_icon.png";
                    //}
                    if (item.Extension != ".trg")
                    {
                        DynDrow = DynDtable.NewRow();
                        DynDrow["Img"] = img.ToString();
                        DynDrow["Name"] = item.Name.ToString();
                        DynDrow["Records"] = "";
                        string[] sizes = { "Bytes", "KB", "MB", "GB", "TB" };
                        double len = item.Length;
                        int order = 0;
                        while (len >= 1024 && order < sizes.Length - 1)
                        {
                            order++;
                            len = len / 1024;
                        }

                        // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
                        // show a single decimal place, and no space.
                        string result = String.Format("{0:0.##} {1}", len, sizes[order]);
                        DynDrow["Length"] = result;
                        DynDrow["Extension"] = item.Extension.ToString();
                        DynDrow["CreationTime"] = item.CreationTime.ToString();
                        DynDrow["Location"] = (filesInDir[0].Directory).Name.ToString();
                        DynDrow["Mode"] = "SFTP";
                        DynDtable.Rows.Add(DynDrow);
                    }
                }
            }
            DataView dv = new DataView();
            dv = DynDtable.DefaultView;
            dv.Sort = "CreationTime DESC";
            return DynDtable;
        }


        public DataTable getDtable_SerachVal(string SearchVal)
        {
            DataTable DynDtable = new DataTable();
            DataRow DynDrow;
            DynDtable.Columns.Add(new System.Data.DataColumn("Img", typeof(String)));
            DynDtable.Columns.Add(new System.Data.DataColumn("Name", typeof(String)));
            DynDtable.Columns.Add(new System.Data.DataColumn("Records", typeof(String)));
            DynDtable.Columns.Add(new System.Data.DataColumn("Length", typeof(String)));
            DynDtable.Columns.Add(new System.Data.DataColumn("Extension", typeof(String)));
            DynDtable.Columns.Add(new System.Data.DataColumn("CreationTime", typeof(String)));
            DynDtable.Columns.Add(new System.Data.DataColumn("Location", typeof(String)));
            DynDtable.Columns.Add(new System.Data.DataColumn("Mode", typeof(String)));

            #region InputArchive
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"" + ConfigurationManager.AppSettings.GetValues("InputArchive")[0].ToString());
            FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + SearchVal + "*.*");

            if (filesInDir.Count() != 0)
            {
                if (filesInDir.Length != 0)
                {
                    foreach (var item in filesInDir)
                    {
                        if (item.Extension == ".txt")
                        {
                            img = "../../assets/images/dashboard-icon/txt_file_icon.png";
                        }
                        if (item.Extension == ".zip")
                        {
                            img = "../../assets/images/dashboard-icon/zip_icon_New.png";
                        }
                        if (item.Extension != ".trg")
                        {
                            DynDrow = DynDtable.NewRow();
                            DynDrow["Img"] = img.ToString();
                            DynDrow["Name"] = item.Name.ToString();
                            DynDrow["Records"] = "";
                            string[] sizes = { "Bytes", "KB", "MB", "GB", "TB" };
                            double len = item.Length;
                            int order = 0;
                            while (len >= 1024 && order < sizes.Length - 1)
                            {
                                order++;
                                len = len / 1024;
                            }

                            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
                            // show a single decimal place, and no space.
                            string result = String.Format("{0:0.##} {1}", len, sizes[order]);
                            DynDrow["Length"] = result;
                            DynDrow["Extension"] = item.Extension.ToString();
                            DynDrow["CreationTime"] = item.CreationTime.ToString();
                            DynDrow["Location"] = (filesInDir[0].Directory).Name.ToString();
                            DynDrow["Mode"] = "SFTP";
                            DynDtable.Rows.Add(DynDrow);
                        }
                    }
                }
            }
            #endregion
            DirectoryInfo Search_Frm_InputArchive = new DirectoryInfo(@"" + ConfigurationManager.AppSettings.GetValues("InputFiles")[0].ToString());
            FileInfo[] filesIn_InputArchive = Search_Frm_InputArchive.GetFiles("*" + SearchVal + "*.*");

            if (filesIn_InputArchive.Count() != 0)
            {
                if (filesIn_InputArchive.Length != 0)
                {
                    foreach (var item in filesIn_InputArchive)
                    {
                        if (item.Extension == ".txt")
                        {
                            img = "../../assets/images/dashboard-icon/txt_file_icon.png";
                        }
                        if (item.Extension == ".zip")
                        {
                            img = "../../assets/images/dashboard-icon/zip_icon_New.png";
                        }
                        if (item.Extension != ".trg")
                        {
                            DynDrow = DynDtable.NewRow();
                            DynDrow["Img"] = img.ToString();
                            DynDrow["Name"] = item.Name.ToString();
                            DynDrow["Records"] = "";
                            string[] sizes = { "Bytes", "KB", "MB", "GB", "TB" };
                            double len = item.Length;
                            int order = 0;
                            while (len >= 1024 && order < sizes.Length - 1)
                            {
                                order++;
                                len = len / 1024;
                            }

                            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
                            // show a single decimal place, and no space.
                            string result = String.Format("{0:0.##} {1}", len, sizes[order]);
                            DynDrow["Length"] = result;
                            DynDrow["Extension"] = item.Extension.ToString();
                            DynDrow["CreationTime"] = item.CreationTime.ToString();
                            DynDrow["Location"] = (filesIn_InputArchive[0].Directory).Name.ToString();
                            DynDrow["Mode"] = "SFTP";
                            DynDtable.Rows.Add(DynDrow);
                        }
                    }
                }
            }
            DataView dv = new DataView();
            dv = DynDtable.DefaultView;
            dv.Sort = "CreationTime DESC";
            return DynDtable;
        }

        protected void txtSrchFIStoMW_TextChanged(object sender, EventArgs e)
        {

            if (hdnFlagFIStoMW.Value == "Registration")
            {
                if (gvBulkUpload.Rows.Count > 0)
                {
                    DataTable dtResult = new DataTable();
                    //string t = gvBulkUpload.Columns[15].ToString();
                    //dtFindTxt.Select("CONSTTYPE like '%01%'");
                    //dtFindTxt.Select("[FIREFNO_CKYC] like '%" + txtSrchFIStoMW.Text + "%'");
                    DataRow[] result = dtFindTxt.Select("FIREFNO_CKYC = '" + txtSrchFIStoMW.Text + "'");
                    if (result.Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('No Record Found');", true);
                    }
                    else
                    {
                        // dtData is DataTable that contain data
                        DataTable dt = dtFindTxt.Select("[FIREFNO_CKYC] like '%" + txtSrchFIStoMW.Text + "%'").CopyToDataTable();
                        gvBulkUpload.DataSource = dt;
                        gvBulkUpload.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowHideModal();", true);
                    }
                }
            }
            else if (hdnFlagFIStoMW.Value == "Search")
            {
                if (gvBulkSearch.Rows.Count > 0)
                {
                    DataTable dtResult = new DataTable();
                    //string t = gvBulkUpload.Columns[15].ToString();
                    //dtFindTxt.Select("CONSTTYPE like '%01%'");
                    //dtFindTxt.Select("[FIREFNO_CKYC] like '%" + txtSrchFIStoMW.Text + "%'");
                    // DataRow[] result = dtFindTxt.Select("[Identity_Number] like '%" + txtSrchFIStoMW.Text + "%'");
                    // dtData is DataTable that contain data
                    DataRow[] result = dtFindTxt.Select("Identity_Number = '" + txtSrchFIStoMW.Text + "'");
                    if (result.Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('No Record Found');", true);
                    }
                    else
                    {
                        DataTable dt = dtFindTxt.Select("[Identity_Number] like '%" + txtSrchFIStoMW.Text + "%'").CopyToDataTable();
                        gvBulkSearch.DataSource = dt;
                        gvBulkSearch.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowHideModal();", true);
                    }
                }
            }
            else if (hdnFlagFIStoMW.Value == "Download")
            {
                if (gvBulkDownload.Rows.Count > 0)
                {
                    DataTable dtResult = new DataTable();
                    //string t = gvBulkUpload.Columns[15].ToString();
                    //dtFindTxt.Select("CONSTTYPE like '%01%'");
                    //dtFindTxt.Select("[FIREFNO_CKYC] like '%" + txtSrchFIStoMW.Text + "%'");
                    // DataRow[] result = dtFindTxt.Select("[Identity_Number] like '%" + txtSrchFIStoMW.Text + "%'");
                    // dtData is DataTable that contain data
                    DataRow[] result = dtFindTxt.Select("CKYC_No = '" + txtSrchFIStoMW.Text + "'");
                    if (result.Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('No Record Found');", true);
                    }
                    else
                    {
                        DataTable dt = dtFindTxt.Select("[CKYC_No] like '%" + txtSrchFIStoMW.Text + "%'").CopyToDataTable();
                        gvBulkDownload.DataSource = dt;
                        gvBulkDownload.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowHideModal();", true);
                    }
                }
            }

            else if (hdnFlagFIStoMW.Value == "Update")
            {
                if (gvFIUpdate.Rows.Count > 0)
                {
                    DataTable dtResult = new DataTable();
                    //string t = gvBulkUpload.Columns[15].ToString();
                    //dtFindTxt.Select("CONSTTYPE like '%01%'");
                    //dtFindTxt.Select("[FIREFNO_CKYC] like '%" + txtSrchFIStoMW.Text + "%'");
                    // DataRow[] result = dtFindTxt.Select("[Identity_Number] like '%" + txtSrchFIStoMW.Text + "%'");
                    // dtData is DataTable that contain data
                    DataRow[] result = dtFindTxt.Select("FIREFNO_CKYC = '" + txtSrchFIStoMW.Text + "'");
                    if (result.Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "AlertMsg('No Record Found');", true);
                    }
                    else
                    {
                        DataTable dt = dtFindTxt.Select("[FIREFNO_CKYC] like '%" + txtSrchFIStoMW.Text + "%'").CopyToDataTable();
                        gvFIUpdate.DataSource = dt;
                        gvFIUpdate.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "ShowHideModal();", true);
                    }
                }
            }
            else { }
        }

        //Ended By Shubham
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Assuming "StatusColumn" is the name of the column whose value you want to check
                string status = DataBinder.Eval(e.Row.DataItem, "fileexist").ToString();

                // Assuming you want to disable the button if the status is "Inactive"
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                LinkButton btnViewMiddltoCERSAI = (LinkButton)e.Row.FindControl("btnViewMiddltoCERSAI");
                //HtmlImage loadingImage = (HtmlImage)e.Row.FindControl("loadingImage");
                Image loadingImage = (Image)e.Row.FindControl("loadingImage");

                if (status == "Y")
                {
                    btnViewMiddltoCERSAI.CssClass = "glyphicon glyphicon-eye-open";
                    btnViewMiddltoCERSAI.Enabled = true;
                    loadingImage.Visible = false;
                    lblStatus.Visible = false;
                }
                else
                {
                    //btnViewMiddltoCERSAI.Style["display"] = "none";
                    //loadingImage.Style["display"] = "inline";
                    loadingImage.ImageUrl = "Images/spinner.gif";
                    lblStatus.Text = "Fetching...";
                    //btnViewMiddltoCERSAI.CssClass = "glyphicon glyphicon-refresh glyphicon-spin";
                    btnViewMiddltoCERSAI.Enabled = false;
                }
            }
        }

        protected void Gridunsolitateddata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Assuming "StatusColumn" is the name of the column whose value you want to check
                string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();

                // Assuming you want to disable the button if the status is "Inactive"
                //Label lblcurrentStatus = (Label)e.Row.FindControl("lblcurrentStatus");
                Image loadingImage = (Image)e.Row.FindControl("loadingImage");

                if (status == "Customer Verification Pending")
                {
                    loadingImage.ImageUrl = "Images/spinner.gif";
                    //lblcurrentStatus.Text = "Fetching...";
                    
                }
                else
                {
                    loadingImage.Visible = false;
                    //lblcurrentStatus.Visible = false;

                }
            }
        }

    }

}
