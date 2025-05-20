using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KMI.FRMWRK.DAL;
using System.Collections;
using System.Data;

namespace KMI.FRMWRK.Web.Admin
{
    public class AdminDAL
    {
        private DataAccessLayer dataAccessLayer;

        public string InsertNewUser(User objUser)
        {
            string newid = string.Empty;
            dataAccessLayer  = new DataAccessLayer();
            var htbl = new Hashtable();
            try
            {
                htbl.Add("@UserId", objUser.UserId);
                htbl.Add("@UserStatus", objUser.UserStatus);
                htbl.Add("@UserType", objUser.UserType);
                htbl.Add("@UserName", objUser.UserName);
                htbl.Add("@UserLoginName", objUser.UserLoginName);
                htbl.Add("@UserPIN", objUser.UserPIN);
                htbl.Add("@IsSystemAdmin", objUser.IsSystemAdmin);
                htbl.Add("@LanguageNum", objUser.LanguageNum);
                htbl.Add("@UserGroupCode", objUser.NonLife);
                //htbl.Add("@LastPINChangeDate", null);
                //htbl.Add("@LastAccessDate", null);
                htbl.Add("@LogonFailureCount", objUser.LogonFailureCount);
                //htbl.Add("@IneffectiveDate", null);
                htbl.Add("@LifeUsrGrp", objUser.LifeUserGroup);
                htbl.Add("@NLifeUsrGrp", objUser.NLifeUserGroup);
                htbl.Add("@UserIdCode", objUser.UserIdCode);
                //htbl.Add("@BranchCode", objUser.BranchCode);
                //htbl.Add("@DeptCode", objUser.DeptCode);
                //htbl.Add("@UserRoleCode", objUser.UserRoleCode);
                //htbl.Add("@LocType", objUser.LocType);
                //htbl.Add("@LocCode", objUser.LocCode);
                htbl.Add("@IsAppAdmin", objUser.IsAppAdmin);
                htbl.Add("@IsUsrAdmin", objUser.IsUsrAdmin);
                htbl.Add("@UsrMobileNumber", objUser.UsrMobNumber);
                htbl.Add("@UserEMailAddress", objUser.UsrEmailId);
                htbl.Add("@DOB", objUser.UsrDob);
                htbl.Add("@UserEffectiveDt", objUser.UsrEffectiveDT);
                htbl.Add("@UserCeaseDt", objUser.UsrCeaseDT);
                htbl.Add("@SessionUserId", objUser.SessionUserId);

                newid = (string)dataAccessLayer.ExecuteScalar("prc_InsUser", htbl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataAccessLayer = null;
                htbl = null;
            }

            return newid;
        }

        public DataSet GetUserDetails(string UserId)
        {
            dataAccessLayer = new DataAccessLayer();
            var htbl = new Hashtable();

            try
            {
                htbl.Add("@UserId", UserId);
                htbl.Add("@CId", "");
                htbl.Add("@PId", "");
                var ds = dataAccessLayer.GetDataSet("prc_getUserAppRoleDtls", htbl);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetSearchUserBy(string UserId, string UserName, string UserStatus)
        {
            var ds = new DataSet();
            dataAccessLayer = new DataAccessLayer();
            var htbl = new Hashtable();
            string strUserLangNum = HttpContext.Current.Session["UserLangNum"].ToString();

            try
            {
                htbl.Add("@UserId", UserId);
                htbl.Add("@UserName", UserName);
                htbl.Add("@UserStatus", UserStatus);
                htbl.Add("@UserLangNum", strUserLangNum);
                ds = dataAccessLayer.GetDataSet("prc_SearchUser", htbl);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataAccessLayer = null;
                htbl = null;
            }
        }

    }
}