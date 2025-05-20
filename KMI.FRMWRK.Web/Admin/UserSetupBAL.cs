using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KMI.FRMWRK.DAL;
using System.Collections;
using KMI.FRMWRK.Security;
using KMI.FRMWRK.Security.Cryptography;
using KMI.FRMWRK.BAL;

namespace KMI.FRMWRK.Web.Admin
{
    public class UserSetupBAL
    {
        private static AuthorizationBAL authorizationBAL;
        private static ICryptography cryptography;

        public static string GetCurrentUserID()
        {
            if (HttpContext.Current.Session["UserID"] != null)
            {
                return HttpContext.Current.Session["UserID"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string GetUserName()
        {
            try
            {
                string UserID = string.Empty;
                authorizationBAL = new AuthorizationBAL();

                if (HttpContext.Current.Session["UserID"] != null)
                {
                    UserID = HttpContext.Current.Session["UserID"].ToString();
                    return (string)authorizationBAL.GetColumnsFromUser("UserName");
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                authorizationBAL = null;
            }
        }

        public static Boolean ValCurrentLogonName(string strLogonName)
        {
            try
            {
                string strCurrLogonName = string.Empty;
                string UserID = string.Empty;
                authorizationBAL = new AuthorizationBAL();

                if (HttpContext.Current.Session["UserID"] != null)
                {
                    UserID = HttpContext.Current.Session["UserID"].ToString();
                    strCurrLogonName = (string)authorizationBAL.GetColumnsFromUser("UserLoginName");
                    return (strCurrLogonName == strLogonName) ? true : false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                authorizationBAL = null;
            }
        }

        public static void UpdateCurrentLogonName(string strLogonName)
        {
            if (HttpContext.Current.Session["LogonName"] != null)
            {
                HttpContext.Current.Session["LogonName"] = strLogonName;
            }
        }

        public static void UpdateCurrentPassword(string Password)
        {
            if (HttpContext.Current.Session["Password"] != null)
            {
                HttpContext.Current.Session["Password"] = Password;
            }
        }

        public static Boolean ValCurrentPwd(string strPwd)
        {
            try
            {
                cryptography = new RCFEncryption();
                string strCurrPwd = cryptography.Encrypt(strPwd);

                if (HttpContext.Current.Session["Password"] != null)
                {
                    strPwd = HttpContext.Current.Session["Password"].ToString();
                    return (strCurrPwd == strPwd) ? true : false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { cryptography = null; }
        }

        public static Boolean ValSamePwd(string strPwd)
        {
            try
            {
                cryptography = new RCFEncryption();
                string strCurrPwd = cryptography.Encrypt(strPwd);

                if (HttpContext.Current.Session["Password"] != null)
                {
                    strPwd = HttpContext.Current.Session["Password"].ToString();
                    return (strCurrPwd == strPwd) ? true : false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { cryptography = null; }
        }

        public static string GetEncryptedPwd(string strPwd)
        {
            cryptography = new RCFEncryption();
            return cryptography.Encrypt(strPwd);
        }

    }
}