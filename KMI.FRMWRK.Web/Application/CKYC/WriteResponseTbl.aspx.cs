using KMI.FRMWRK.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMI.FRMWRK.Web.Application.CKYC
{
    public partial class WriteResponseTbl : System.Web.UI.Page
    {

        DataTable dt;
        DataAccessLayer objDAL;
        Hashtable htParam = new Hashtable();
        //string DestFile = @"E:\During WFH\CKYC\Demo\Demo\Register\Output - Copy",
        string SrcFile = @"C:\HostedApplications\CKYC\Demo\Demo\Register\Output";
        public string RecTyp, FIREFNO, RELATED_FIREFNO, CKYC_REFNO, PERIODIC_RES_DATE, KYC_NO, REMARK;
        public int BATCHID, REQUEST_TYPE, RESPONSE_STATUS, RECORD_STATUS;

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                string[] myfiles = System.IO.Directory.GetFiles(SrcFile);
                if (myfiles.Equals(""))
                {
                    // "There is no file in the folder for processing.";
                }
                else
                {
                    foreach (string fname in myfiles)
                    {
                        if (Path.GetExtension(fname).ToLower() == ".txt")
                        {
                            //string1 filename
                            string filename = fname.Substring(fname.LastIndexOf('\\') + 1);
                            StreamReader file = new StreamReader(SrcFile + "\\" + filename);
                            //string region = filename.Contains("BR").ToString();
                            #region Respone files Saving
                            if (!filename.Contains("_RES0") && filename.Contains("_RES"))
                            {
                                using (file)
                                {
                                    string ln;
                                    while ((ln = file.ReadLine()) != null)
                                    {
                                        var val = ln.Split('|');

                                        if (val.Length > 15)
                                        {
                                            //DataError.Add(" Error Ocured For Line " + val[0]);
                                        }
                                        else
                                        {
                                            RecTyp = val[0];
                                            if (RecTyp == "10")
                                            {
                                                BATCHID = Convert.ToInt32(val[1]);
                                                PERIODIC_RES_DATE = val[5];
                                            }
                                            if (RecTyp == "20")
                                            {
                                                FIREFNO = val[5];
                                                RELATED_FIREFNO = val[1];
                                                CKYC_REFNO = "";
                                                REQUEST_TYPE = Convert.ToInt32(val[2]);
                                                RESPONSE_STATUS = 0;
                                                RECORD_STATUS = Convert.ToInt32(val[3]);
                                                KYC_NO = "";
                                                REMARK = val[6];
                                                htParam.Clear();
                                                htParam.Add("@BATCHID", BATCHID);
                                                htParam.Add("@FIREFNO", FIREFNO);
                                                htParam.Add("@RELATED_FIREFNO", RELATED_FIREFNO);
                                                htParam.Add("@CKYC_REFNO", CKYC_REFNO);
                                                htParam.Add("@PERIODIC_RES_DATE", PERIODIC_RES_DATE);
                                                htParam.Add("@REQUEST_TYPE", REQUEST_TYPE);
                                                htParam.Add("@RESPONSE_STATUS", RESPONSE_STATUS);
                                                htParam.Add("@RECORD_STATUS", RECORD_STATUS);
                                                htParam.Add("@KYC_NO", KYC_NO);
                                                htParam.Add("@REMARK", REMARK);
                                                objDAL = new DataAccessLayer("CKYCConnectionString");
                                                objDAL.ExecuteNonQuery("PRC_Ins_TX_CKYCResponseDtls", htParam);
                                            }
                                        }
                                    }
                                    //System.IO.File.Move(SrcFile + "\\" + filename, DestFile);
                                    file.Close();
                                }
                            }
                            #endregion
                            #region Periodic Respone files Saving
                            if (!filename.Contains("_RES"))
                            {
                                using (file)
                                {
                                    string ln;
                                    while ((ln = file.ReadLine()) != null)
                                    {
                                        var val = ln.Split('|');

                                        if (val.Length > 15)
                                        {
                                            //DataError.Add(" Error Ocured For Line " + val[0]);
                                        }
                                        else
                                        {
                                            RecTyp = val[0];
                                            if (RecTyp == "10")
                                            {
                                                PERIODIC_RES_DATE = val[4];
                                            }
                                            if (RecTyp == "20")
                                            {
                                                string batchid = (val[2].Split('_'))[0];
                                                BATCHID = Convert.ToInt32(batchid);
                                                FIREFNO = val[7];
                                                RELATED_FIREFNO = "";
                                                CKYC_REFNO = val[8];
                                                REQUEST_TYPE = Convert.ToInt32(val[4]);
                                                RESPONSE_STATUS = Convert.ToInt32(val[5]);
                                                RECORD_STATUS = Convert.ToInt32(val[6]);
                                                KYC_NO = "";
                                                REMARK = val[9];
                                                htParam.Clear();
                                                htParam.Add("@BATCHID", BATCHID);
                                                htParam.Add("@FIREFNO", FIREFNO);
                                                htParam.Add("@RELATED_FIREFNO", RELATED_FIREFNO);
                                                htParam.Add("@CKYC_REFNO", CKYC_REFNO);
                                                htParam.Add("@PERIODIC_RES_DATE", PERIODIC_RES_DATE);
                                                htParam.Add("@REQUEST_TYPE", REQUEST_TYPE);
                                                htParam.Add("@RESPONSE_STATUS", RESPONSE_STATUS);
                                                htParam.Add("@RECORD_STATUS", RECORD_STATUS);
                                                htParam.Add("@KYC_NO", KYC_NO);
                                                htParam.Add("@REMARK", REMARK);
                                                objDAL = new DataAccessLayer("CKYCConnectionString");
                                                objDAL.ExecuteNonQuery("PRC_Ins_TX_CKYCResponseDtls", htParam);
                                            }
                                        }
                                    }
                                    file.Close();
                                    //System.IO.File.Move(SrcFile + "\\" + filename, DestFile);
                                }
                            }
                            #endregion
                        }
                    }
                }

            }
            catch (Exception ex)
            { }
        }
    }
}