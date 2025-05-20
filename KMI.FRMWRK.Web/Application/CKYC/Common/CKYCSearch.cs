using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;
using KMI.FRMWRK.DAL;

/// <summary>
/// Summary description for CKYCSearch
/// </summary>
/// 
namespace KMI.FRMWRK.Web.Application.CKYC.Common
{
    public class CKYCSearch
    {

        private DataAccessLayer dataAccessRecruit = new DataAccessLayer();
        Hashtable htParam = new Hashtable();
        DataSet ds = new DataSet();
        string strResponseXML = string.Empty;
        XmlDocument xmlInPut = new XmlDocument();
        XmlDocument xmlOutPut = new XmlDocument();
        DataSet xmlDS = new DataSet();
        String strout = String.Empty;

       

        #region Get response

        public String VerifyKyc(String str)
        {
            //str ="<REQ_ROOT><HEADER><FI_CODE>IN106</FI_CODE><REQUEST_ID>2</REQUEST_ID><VERSION>1.0</VERSION></HEADER><CKYC_INQ><SESSION_KEY>123</SESSION_KEY><PID>44333</PID><PID_DATA><DATE_TIME>12-04-2017 15:25:47</DATE_TIME><ID_NO>KACPH5899V</ID_NO><ID_TYPE>C</ID_TYPE></PID_DATA></CKYC_INQ></REQ_ROOT>";
            if (str != "")
            {
                xmlInPut.LoadXml(str);
                XmlDocument Objxml = new XmlDocument();
                // Objxml.InnerXml = str;

                DataSet xmlDS = new DataSet();


                xmlDS = ConvertXMLToDataSet(str);

                String DocID = xmlDS.Tables[2].Rows[0]["ID_NO"].ToString().Trim();
                String DocType = xmlDS.Tables[2].Rows[0]["ID_TYPE"].ToString().Trim();

                String[] seperator = DocID.Split('|');

                htParam.Clear();

                htParam.Add("@IDType", DocType);
                if (seperator.Length > 1)
                {
                    htParam.Add("@IDNO", seperator[0].ToString());
                    htParam.Add("@Name", seperator[1].ToString());
                    htParam.Add("@DOB", seperator[2].ToString());
                    htParam.Add("@Gender", seperator[3].ToString());
                }
                else
                {
                    htParam.Add("@IDNO", DocID);
                }


                //ds = dataAccessRecruit.GetDataSetForPrcDBConn("Prc_GetxmlResponseforSearch", htParam, "CKYC");
                //String[] strout =ds.Tables[0].Rows[0][0].ToString().Trim(); 
                strout = ds.Tables[0].Rows[0][0].ToString().Trim();

            }

            else
            {
                strout = "0 record found ";

            }
            return strout;
        }
        #endregion



        #region Get response
        public String Download(String str)
        {


            if (str != "")
            {
                xmlInPut.LoadXml(str);
                XmlDocument Objxml = new XmlDocument();
                // Objxml.InnerXml = str;

                DataSet xmlDS = new DataSet();


                xmlDS = ConvertXMLToDataSet(str);

                String DocID = xmlDS.Tables[2].Rows[0]["ID_NO"].ToString().Trim();
                String DocType = xmlDS.Tables[2].Rows[0]["ID_TYPE"].ToString().Trim();
                htParam.Clear();
                ds.Clear();
                htParam.Add("@IDType", DocType);
                htParam.Add("@IDNO", DocID);
                //ds = dataAccessRecruit.GetDataSetForPrcDBConn("Prc_GetxmlResponseforDownload", htParam, "CKYC");
                //String[] strout =ds.Tables[0].Rows[0][0].ToString().Trim(); 
                strout = ds.Tables[0].Rows[0][0].ToString().Trim();

            }

            else
            {
                strout = "0 record found ";

            }
            return strout;
        }
        #endregion

        public DataSet ConvertXMLToDataSet(string xmlData)
        {
            System.IO.StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new System.IO.StringReader(xmlData);
                // Load the XmlTextReader from the stream
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }

    }
}