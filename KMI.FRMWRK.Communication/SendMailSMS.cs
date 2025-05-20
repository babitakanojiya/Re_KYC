using System;
using System.Collections;
using System.Text;
using System.Net.Mail;
using DAL = KMI.FRMWRK.DAL;


namespace KMI.FRMWRK.Communication
{
    public class SendMailSMS
    {
        #region Globle Declaration
        public string strMailRefNo = string.Empty;
        public string strSMSRefNo = string.Empty;
        string strSuccessMsg = string.Empty;
        public Hashtable htable = new Hashtable();
        DAL.DataAccessLayer objDAL = new DAL.DataAccessLayer();
        DAL.ErrorLog objErrLog = new DAL.ErrorLog();
        #endregion

        private string MailFrom { get; set; }
        
        #region Constructor
        public SendMailSMS()
        {
            MailFrom = Communication.Default["MailUserId"].ToString();
        }
        public SendMailSMS(string strMailFrom)
        {
            MailFrom = strMailFrom;
        }
        #endregion

        #region Methods 

        /// <summary>
        /// METHOD TO SEND MAIL 
        /// </summary>
        /// <param name="strAppId">APPLICATION ID</param>
        /// <param name="strCallerRefId1"></param>
        /// <param name="strCallerRefId2"></param>
        /// <param name="strCallerRefId3"></param>
        /// <param name="strMailFrom">MAIL SEND FROM</param>
        /// <param name="strMailTo">MAIL SEND TO</param>
        /// <param name="strMailCC">MAIL CC TO</param>
        /// <param name="strMailBCC">MAIL BCC TO</param>
        /// <param name="strMailSubject">SUBJECT of Mail</param>
        /// <param name="strMailContent">MAIL CONTENT</param>
        /// <param name="strCreatedBy">CREATED BY</param>
        /// <param name="Flag">FLAG TO SEND MAIL WITH DIFFERENT ID</param>
        /// <param name="List">ATTACHMENT LIST</param>
        /// <param name="strInteractionCode">INTERACTION NO</param>
        /// <param name="strStatus">CURRENT STATUS</param>
        /// <param name="ParamCode">ParamCode TO GET ACTIVITY STATUS</param>
        /// <param name="ParamValue">ParamValue TO GET ACTIVITY STATUS</param>
        /// <returns></returns>
        public string SendMail(string strAppId, string strCallerRefId1, string strCallerRefId2, string strCallerRefId3,
                               string strMailTo, string strMailCC, string strMailBCC, string strMailSubject,
                               string strMailContent, string strCreatedBy, string Flag, ArrayList List, string strInteractionCode,
                               string strStatus, string ParamCode, string ParamValue)
        {
            try
            {
                
                #region Inserting Data into DB and get MailRefNo
                htable.Clear();
                htable.Add("@ApplicationID", strAppId);
                htable.Add("@CallerRefID1", strCallerRefId1);
                htable.Add("@CallerRefID2", strCallerRefId2);
                htable.Add("@CallerRefID3", strCallerRefId3);
                htable.Add("@MailFrom", MailFrom);
                htable.Add("@MailTo", strMailTo);
                htable.Add("@MailCC", strMailCC);
                htable.Add("@MailBCC", strMailBCC);
                htable.Add("@MailSubject", strMailSubject);
                htable.Add("@MailContent", strMailContent);
                htable.Add("@CreatedBy", strCreatedBy);
                htable.Add("@InteractionCode", strInteractionCode);
                htable.Add("@status", strStatus);
                htable.Add("@ParamCode", ParamCode);
                htable.Add("@ParamValue", ParamValue);

                strMailRefNo = objDAL.ExecuteNonQuery("prc_InsMailDtls", "@MailRefNo", htable, "Communication");
                #endregion

                #region Sending Mail
                MailMessage message = new MailMessage();

                //ADD MAIL ID OF SENDER
                message.From = new MailAddress(MailFrom);

                //ADD MAIL ID OF RECEPIENT
                if (strMailTo != "")
                {
                    string[] strSplitedVal = null;
                    strSplitedVal = funSplit(strMailTo);
                    for (int j = 0; j < strSplitedVal.Length; j++)
                    {
                        if (strSplitedVal[j].Trim().ToString() != "")
                        {
                            message.To.Add(strSplitedVal[j]);
                        }
                    }
                    //message.To.Add(strMailTo);
                }

                if (strMailCC != "")
                {
                    string[] strSplitedVal1 = null;
                    strSplitedVal1 = funSplit(strMailCC);
                    for (int j = 0; j < strSplitedVal1.Length; j++)
                    {
                        if (strSplitedVal1[j].Trim().ToString() != "")
                        {
                            message.CC.Add(strSplitedVal1[j]);
                        }
                    }
                    //message.CC.Add(strMailCC);
                }
                if (strMailBCC != "")
                {
                    string[] strSplitedVal2 = null;
                    strSplitedVal2 = funSplit(strMailBCC);
                    for (int j = 0; j < strSplitedVal2.Length; j++)
                    {
                        if (strSplitedVal2[j].Trim().ToString() != "")
                        {
                            message.Bcc.Add(strSplitedVal2[j]);
                        }
                    }
                    //message.Bcc.Add(strMailBCC);
                }

                //ADD MAIL SUBJECT
                if (strMailSubject != "")
                {
                    message.Subject = strMailSubject;
                }

                //GET ATTACHMENT
                if (List.Count > 0)
                {
                    for (int i = 0; i < List.Count; i++)
                    {
                        message.Attachments.Add(new System.Net.Mail.Attachment((List[i].ToString())));
                    }
                }


                //CODE TO REPLACE THE TEMPLATE VARIABLES
                StringBuilder strBldr = new StringBuilder();
                message.IsBodyHtml = true;
                strBldr.Append(strMailContent);
                message.Body = strBldr.ToString();

                //CODE TO CONFIGURE THE SMTP CLIENT SETTINGS
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.UseDefaultCredentials = false;

                //SET USERID AND PASSWORD 
                smtpClient.Credentials = new System.Net.NetworkCredential
                (Communication.Default.MailUserId, Communication.Default.MailPswd);

                smtpClient.Host = Communication.Default.MailHostIP;
                smtpClient.Port = Convert.ToInt32(Communication.Default.MailPortNo); //to be got from the Settings.setting file.                
                smtpClient.EnableSsl = true;
                smtpClient.Send(message);
                message.Dispose();

                #endregion 

                #region Updating Response
                htable.Clear();
                htable.Add("@RefNo", strMailRefNo);
                htable.Add("@MSG", "SUCCESS");
                htable.Add("@flag", "2");
                objDAL.ExecuteNonQuery("prc_UpdMessage", htable, "Communication");
                #endregion
            }
            catch (Exception ex)
            {
                objErrLog.LogErr(Convert.ToInt32(strAppId), "SendMailSMS.cs", "SendMail", ex.Message.ToString()+" | "+ex.InnerException.ToString(), strCreatedBy, "COMMUNICATION");
            }

            return strMailRefNo;

        }


        /// <summary>
        /// METHOD TO SEND MAIL 
        /// </summary>
        /// <param name="strAppId">APPLICATION ID</param>
        /// <param name="strCallerRefId1"></param>
        /// <param name="strCallerRefId2"></param>
        /// <param name="strCallerRefId3"></param>
        /// <param name="strMailFrom">MAIL SEND FROM</param>
        /// <param name="strMailTo">MAIL SEND TO</param>
        /// <param name="strMailCC">MAIL CC TO</param>
        /// <param name="strMailBCC">MAIL BCC TO</param>
        /// <param name="strMailSubject">SUBJECT</param>
        /// <param name="strMailContent">MAIL CONTENT</param>
        /// <param name="strCreatedBy">CREATED BY</param>
        /// <param name="Flag">FLAG TO SEND MAIL WITH DIFFERENT ID</param>
        /// <param name="List">ATTACHMENT LIST</param>
        /// <param name="strInteractionCode">INTERACTION NO</param>
        /// <param name="strStatus">CURRENT STATUS</param>
        /// <param name="ParamCode">ParamCode TO GET ACTIVITY STATUS</param>
        /// <param name="ParamValue">ParamValue TO GET ACTIVITY STATUS</param>
        /// <param name="strOldMailRefNo">MailRefno OF 1ST MAIL ID</param>
        /// <returns></returns>
        public string ReSendMail(string strAppId, string strCallerRefId1, string strCallerRefId2, string strCallerRefId3,
                               string strMailFrom, string strMailTo, string strMailCC, string strMailBCC, string strMailSubject,
                               string strMailContent, string strCreatedBy, string Flag, ArrayList List, string strInteractionCode,
                               string strStatus, string ParamCode, string ParamValue, string strOldMailRefNo)
        {
            try
            {
                #region Inserting Data into DB and get MailRefNo
                htable.Clear();
                htable.Add("@ApplicationID", strAppId);
                htable.Add("@CallerRefID1", strCallerRefId1);
                htable.Add("@CallerRefID2", strCallerRefId2);
                htable.Add("@CallerRefID3", strCallerRefId3);
                htable.Add("@MailFrom", strMailFrom);
                htable.Add("@MailTo", strMailTo);
                htable.Add("@MailCC", strMailCC);
                htable.Add("@MailBCC", strMailBCC);
                htable.Add("@MailSubject", strMailSubject);
                htable.Add("@MailContent", strMailContent);
                htable.Add("@CreatedBy", strCreatedBy);
                htable.Add("@SrvcReqDtlCode", strInteractionCode);
                htable.Add("@status", strStatus);
                htable.Add("@ParamCode", ParamCode);
                htable.Add("@ParamValue", ParamValue);

                strMailRefNo = objDAL.ExecuteNonQuery("prc_InsMailDtls", "@MailRefNo", htable, "Communication");
                #endregion

                #region Sending Mail
                MailMessage message = new MailMessage();

                //ADD MAIL ID OF SENDER
                message.From = new MailAddress(strMailFrom);

                //ADD MAIL ID OF RECEPIENT
                if (strMailTo != "")
                {
                    string[] strSplitedVal = null;
                    strSplitedVal = funSplit(strMailTo);
                    for (int j = 0; j < strSplitedVal.Length; j++)
                    {
                        if (strSplitedVal[j].Trim().ToString() != "")
                        {
                            message.To.Add(strSplitedVal[j]);
                        }
                    }
                    //message.To.Add(strMailTo);
                }

                if (strMailCC != "")
                {
                    string[] strSplitedVal1 = null;
                    strSplitedVal1 = funSplit(strMailCC);
                    for (int j = 0; j < strSplitedVal1.Length; j++)
                    {
                        if (strSplitedVal1[j].Trim().ToString() != "")
                        {
                            message.CC.Add(strSplitedVal1[j]);
                        }
                    }
                    //message.CC.Add(strMailCC);
                }
                if (strMailBCC != "")
                {
                    string[] strSplitedVal2 = null;
                    strSplitedVal2 = funSplit(strMailBCC);
                    for (int j = 0; j < strSplitedVal2.Length; j++)
                    {
                        if (strSplitedVal2[j].Trim().ToString() != "")
                        {
                            message.Bcc.Add(strSplitedVal2[j]);
                        }
                    }
                    //message.Bcc.Add(strMailBCC);
                }

                //ADD MAIL SUBJECT
                if (strMailSubject != "")
                {
                    message.Subject = strMailSubject;
                }

                //GET ATTACHMENT
                if (List.Count > 0)
                {
                    for (int i = 0; i < List.Count; i++)
                    {
                        message.Attachments.Add(new System.Net.Mail.Attachment((List[i].ToString())));
                    }
                }


                //CODE TO REPLACE THE TEMPLATE VARIABLES
                StringBuilder strBldr = new StringBuilder();
                message.IsBodyHtml = true;
                strBldr.Append(strMailContent);
                message.Body = strBldr.ToString();

                //CODE TO CONFIGURE THE SMTP CLIENT SETTINGS
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.UseDefaultCredentials = false;

                //SET USERID AND PASSWORD 
                smtpClient.Credentials = new System.Net.NetworkCredential
                (Communication.Default.MailUserId, Communication.Default.MailPswd);

                smtpClient.Host = Communication.Default.MailHostIP;
                smtpClient.Port = Convert.ToInt32(Communication.Default.MailPortNo); //to be got from the Settings.setting file.                
                smtpClient.Send(message);
                message.Dispose();

                #endregion 

                #region Update Resend strMailRefNo
                htable.Clear();
                htable.Add("@RefNo", strMailRefNo);
                htable.Add("@MSG", strSuccessMsg);
                htable.Add("@InteractionCode", strInteractionCode);
                htable.Add("@flag", 2);
                htable.Add("@strOldMailRefNo", strOldMailRefNo);
                htable.Add("@strOldSmsRefNo", "");
                objDAL.ExecuteNonQuery("Prc_UpdRsendMessage", htable, "Communication");
                #endregion
            }
            catch (Exception ex)
            {
                objErrLog.LogErr(Convert.ToInt32(strAppId), "SendMailSMS.cs", "ReSendMail", ex.Message.ToString()+" | "+ex.InnerException.ToString(), strCreatedBy, "COMMUNICATION");
            }
            return strMailRefNo;
        }


        /// <summary>
        /// METHOD TO SEND SMS
        /// </summary>
        /// <param name="strAppId">APPLICATION ID</param>
        /// <param name="strCallerRefId"></param>
        /// <param name="strSMSFrom">SMS FROM</param>
        /// <param name="strSMSTo">SMS TO</param>
        /// <param name="strSMSContent">SMS CONTENT</param>
        /// <param name="strCreatedBy">CREATED BY</param>
        /// <param name="strInteractionCode">INTERACTION NO</param>
        /// <param name="strStatus">CURRENT STATUS</param>
        /// <param name="ParamCode"><ParamCode TO GET ACTIVITY STATUS</param>
        /// <param name="ParamValue">ParamValue TO GET ACTIVITY STATUS</param>
        /// <returns></returns>
        public string SendSMS(string strAppId, string strCallerRefId, string strSMSFrom, string strSMSTo, string strSMSContent, string strCreatedBy,
                               string strInteractionCode, string strStatus, string ParamCode, string ParamValue)
        {
            try
            {
                #region Inserting Data into DB and get SMSRefNo
                htable.Clear();
                htable.Add("@ApplicationID", strAppId);
                htable.Add("@CallerRefID", strCallerRefId);
                htable.Add("@SMSFrom", strSMSFrom);
                htable.Add("@SMSTo", strSMSTo);
                htable.Add("@SMSContent", strSMSContent);
                htable.Add("@CreatedBy", strCreatedBy);
                htable.Add("@InteractionCode", strInteractionCode);
                htable.Add("@status", strStatus);
                htable.Add("@ParamCode", ParamCode);
                htable.Add("@ParamValue", ParamValue);
                strSMSRefNo = objDAL.ExecuteNonQuery("prc_InsSMSDtls", "@SMSRefNo", htable, "Communication");
                #endregion

                #region Call sendSMS WebService for sending SMS
                //code here
                //SendSMSWS.SendSMS objSmsWS = new SendSMSWS.SendSMS();
                //strSuccessMsg = objSmsWS.sendSMS("IT", strSMSTo, strSMSContent);
                #endregion

                #region Updating Response
                if (strSuccessMsg != "")
                {
                    htable.Clear();
                    htable.Add("@RefNo", strSMSRefNo);
                    htable.Add("@MSG", strSuccessMsg);
                    htable.Add("@flag", "1");

                    objDAL.ExecuteNonQuery("prc_UpdMessage", htable, "Communication");
                }
                #endregion
            }
            catch (Exception ex)
            {
                objErrLog.LogErr(Convert.ToInt32(strAppId), "SendMailSMS.cs", "SendSMS", ex.Message.ToString()+" | "+ex.InnerException.ToString(), strCreatedBy, "Communication");
            }
            return strSMSRefNo;
        }

        /// <summary>
        /// METHOD TO RESEND SMS
        /// </summary>
        /// <param name="strAppId">APPLICATION ID</param>
        /// <param name="strCallerRefId"></param>
        /// <param name="strSMSFrom">SMS FROM</param>
        /// <param name="strSMSTo">SMS TO</param>
        /// <param name="strSMSContent">SMS CONTENT</param>
        /// <param name="strCreatedBy">CREATED BY</param>
        /// <param name="strInteractionCode">INTERACTION NO</param>
        /// <param name="strStatus">CURRENT STATUS</param>
        /// <param name="ParamCode"><ParamCode TO GET ACTIVITY STATUS</param>
        /// <param name="ParamValue">ParamValue TO GET ACTIVITY STATUS</param>
        /// <param name="strOldSMSRefNo">SMSRefno OF 1ST SMS ID</param>
        /// <returns></returns>
        public string ReSendSMS(string strAppId, string strCallerRefId, string strSMSFrom, string strSMSTo, string strSMSContent, string strCreatedBy,
            string strInteractionCode, string strStatus, string ParamCode, string ParamValue, string strOldSMSRefNo)
        {
            try
            {
                #region Inserting Data into DB and get SMSRefNo
                //Add parameters into hashtable
                htable.Clear();
                htable.Add("@ApplicationID", strAppId);
                htable.Add("@CallerRefID", strCallerRefId);
                htable.Add("@SMSFrom", strSMSFrom);
                htable.Add("@SMSTo", strSMSTo);
                htable.Add("@SMSContent", strSMSContent);
                htable.Add("@CreatedBy", strCreatedBy);
                htable.Add("@InteractionCode", strInteractionCode);
                htable.Add("@status", strStatus);
                htable.Add("@ParamCode", ParamCode);
                htable.Add("@ParamValue", ParamValue);
                strSMSRefNo = objDAL.ExecuteNonQuery("prc_InsSMSDtls", "@SMSRefNo", htable, "Communication");
                #endregion

                #region Call sendSMS WebService for sending SMS
                //code here
                //SendSMSWS.SendSMS objSmsWS = new SendSMSWS.SendSMS();
                //strSuccessMsg = objSmsWS.sendSMS("IT", strSMSTo, strSMSContent);
                #endregion

                #region Update Resend strSMSRefNo
                htable.Clear();
                htable.Add("@RefNo", strSMSRefNo);
                htable.Add("@MSG", strSuccessMsg);
                htable.Add("@SrvcReqDtlCode", strInteractionCode);
                htable.Add("@flag", "1");
                htable.Add("@strOldSMSRefNo", strOldSMSRefNo);
                htable.Add("@strOldMailRefNo", "");

                objDAL.ExecuteNonQuery("Prc_UpdRsendMessage", htable, "Communication");
                #endregion
            }
            catch (Exception ex)
            {
                objErrLog.LogErr(Convert.ToInt32(strAppId), "SendMailSMS.cs", "ReSendSMS", ex.Message.ToString()+ " | "+ex.InnerException.ToString(), strCreatedBy, "Communication");
            }
            return strSMSRefNo;
        }


        /// <summary>
        /// SPLIT FUNCTION
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string[] funSplit(string strValue)
        {
            string[] st = null;
            char[] splitchar = { ',', ';' };
            //ArrayList arrSplitList = new ArrayList();
            try
            {
                st = strValue.Split(splitchar);
            }
            catch (Exception)
            {
                st = null;
            }
            finally
            {
            }
            return st;
        }

        #endregion

    }

}