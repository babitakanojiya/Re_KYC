using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KMI.FRMWRK.DAL;
using System.Collections;
using System.Data;
using System.IO;
using Renci.SshNet;
using System.Net;

namespace KMI.FRMWRK.Web.Admin
{
    public class FTPCnt
    {
        DataAccessLayer dataAccessLayer = new DataAccessLayer();
        ErrorLog objErr = new ErrorLog();
        Hashtable htParam = new Hashtable();
        DataSet dsResult = new DataSet();
        string ftpServerIP = "uploadtest.ckycindia.in";
        string ftpServerIPUpload;//= "ftp://180.179.24.75/Input"; 
        //string ftpServerIPDownload;// = "ftp://192.168.1.100/CkycCersai/Response/";
        //string ftpUserID = "IN1352";
        //string ftpPassword = "G0k!u4d8^n";
        string ftpUserID = "IA004130";
        string ftpPassword = "Nbrnlic@2022";//"Relianceoct2021$"; new added 06_07_2022
        string ftpPasswordDB = string.Empty;

        public string SyncUploadFolder(string LocalFolderToBeUploaded, string StrDocCode)
        {
            dataAccessLayer = new DataAccessLayer("CKYCConnectionString");
            string strSuccess = string.Empty;
            string RequestFolderPath = LocalFolderToBeUploaded;
            string[] myfiles = Directory.GetFiles(RequestFolderPath);
            foreach (string fname1 in myfiles)
            {
                string fname = Path.GetFileName(fname1);
                if (!fname.Contains("Error"))
                {
                    try
                    {
                        bool retry = false;
                        do
                        {
                            bool retrying = retry;
                            retry = false;

                            htParam.Clear();
                            htParam.Add("@flag", "1");
                            htParam.Add("@Seqno", "1");
                            htParam.Add("@DocType", "DNDCM");
                            //dsResult = objDAL.GetDataSet("Prc_getdata", htParam);
                            dsResult = dataAccessLayer.GetDataSet("Prc_getdata", htParam);
                            ftpServerIPUpload = dsResult.Tables[0].Rows[0]["Path2"].ToString().Trim();
                            ftpPasswordDB = dsResult.Tables[0].Rows[0]["Password"].ToString().Trim();
                            using (var client = new SftpClient("uploadtest.ckycindia.in", 22, ftpUserID, ftpPasswordDB))
                            {
                                htParam.Clear();
                                htParam.Add("@Path", "before connect");
                                //dsResult = objDAL.GetDataSet("Prc_getError", htParam);
                                dsResult = dataAccessLayer.GetDataSet("Prc_getError", htParam);

                                client.Connect();

                                htParam.Clear();
                                htParam.Add("@Path", "connected");
                                //dsResult = objDAL.GetDataSet("Prc_getError", htParam);
                                dsResult = dataAccessLayer.GetDataSet("Prc_getError", htParam);

                                if (!client.Exists(RequestFolderPath + fname))
                                {
                                    htParam.Clear();
                                    htParam.Add("@Path", "if cond");
                                    //dsResult = objDAL.GetDataSet("Prc_getError", htParam);
                                    dsResult = dataAccessLayer.GetDataSet("Prc_getError", htParam);

                                    //return false;
                                    retry = false;
                                }

                                //var fileName = Path.GetFileName(source);
                                //var destinationFile = Path.Combine(destination, fileName);
                                //client.Connect();
                                //FileStream fs = new FileStream(RequestFolderPath + fname, FileMode.Open);
                                //client.BufferSize = 4 * 1024;
                                //client.UploadFile(fs, fname);
                                //fs.Close();

                                client.ChangeDirectory("input");

                                htParam.Clear();
                                htParam.Add("@Path", "directory change");
                                //dsResult = objDAL.GetDataSet("Prc_getError", htParam);
                                dsResult = dataAccessLayer.GetDataSet("Prc_getError", htParam);

                                try
                                {
                                    htParam.Clear();
                                    htParam.Add("@Path", "beforedest Stream");
                                    //dsResult = objDAL.GetDataSet("Prc_getError", htParam);
                                    dsResult = dataAccessLayer.GetDataSet("Prc_getError", htParam);

                                    Stream destinationStream;
                                    if (retrying)
                                    {
                                        htParam.Clear();
                                        htParam.Add("@Path", "if retrying");
                                        //dsResult = objDAL.GetDataSet("Prc_getError", htParam);
                                        dsResult = dataAccessLayer.GetDataSet("Prc_getError", htParam);

                                        destinationStream = new FileStream(RequestFolderPath + fname, FileMode.Open);
                                        client.BufferSize = 4 * 1024;
                                        client.UploadFile(destinationStream, fname);
                                    }
                                    else
                                    {
                                        htParam.Clear();
                                        htParam.Add("@Path", "else retrying");
                                        //dsResult = objDAL.GetDataSet("Prc_getError", htParam);
                                        dsResult = dataAccessLayer.GetDataSet("Prc_getError", htParam);

                                        destinationStream = new FileStream(RequestFolderPath + fname, FileMode.Open);
                                        client.BufferSize = 4 * 1024;
                                        client.UploadFile(destinationStream, fname);
                                    }
                                    destinationStream.Close();

                                    htParam.Clear();
                                    htParam.Add("@Path", "CLOSEEE");
                                    //dsResult = objDAL.GetDataSet("Prc_getError", htParam);
                                    dsResult = dataAccessLayer.GetDataSet("Prc_getError", htParam);
                                }
                                catch (Exception ex)
                                {
                                    retry = true;
                                    objErr.LogErr(1, "WinCKYCSFTP", "service", "SyncUploadFolder", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "");
                                }
                            }
                        }
                        while (retry);


                        htParam.Clear();
                        htParam.Add("@Path", "retry end");
                        //dsResult = objDAL.GetDataSet("Prc_getError", htParam);
                        dsResult = dataAccessLayer.GetDataSet("Prc_getError", htParam);

                        //SftpClient request = new SftpClient("180.179.24.75", 22, ftpUserID, ftpPassword);
                        //request.Connect();
                        //request.ChangeDirectory("input");
                        //FileStream fs = new FileStream(RequestFolderPath + fname, FileMode.Open);
                        //request.BufferSize = 4 * 1024;
                        //request.UploadFile(fs, fname);
                        //fs.Close();
                        //request.Disconnect();

                        System.IO.File.Delete(RequestFolderPath + fname);
                        strSuccess = "1";
                        htParam.Clear();
                        htParam.Add("@Path", "All end");
                        //dsResult = objDAL.GetDataSet("Prc_getError", htParam);
                        dsResult = dataAccessLayer.GetDataSet("Prc_getError", htParam);
                    }

                    catch (WebException ex)
                    {
                        htParam.Clear();
                        htParam.Add("@Path", ex.ToString().Trim() + "," + ftpServerIP.ToString().Trim() + "," + ftpUserID.ToString().Trim() + "," + ftpPassword.ToString().Trim());
                        //dsResult = objDAL.GetDataSet("Prc_getError", htParam);
                        dsResult = dataAccessLayer.GetDataSet("Prc_getError", htParam);
                        objErr.LogErr(1, "WinCKYCSFTP", "service", "SyncUploadFolder", ex.InnerException == null ? ex.Message.ToString() : ex.Message.ToString() + " | " + ex.InnerException.ToString(), "");
                        throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
                    }
                }
            }
            return strSuccess;
        }
    }
}