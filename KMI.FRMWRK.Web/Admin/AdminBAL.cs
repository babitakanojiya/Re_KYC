using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMI.FRMWRK.Web.Admin
{
    public class AdminBAL
    {
        private AdminDAL adminDAL;
        public string AddNewUser(User objUser)
        {
            adminDAL = new AdminDAL();
            try
            {
                return adminDAL.InsertNewUser(objUser).ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { adminDAL = null; }
        }

        public User LoadUserDetails(User objUser)
        {
            try
            {
                adminDAL = new AdminDAL();
                var ds = adminDAL.GetUserDetails(objUser.UserId);
                objUser.UserName = ds.Tables[0].Rows[0]["UserName1"].ToString();
                objUser.UserLoginName = ds.Tables[0].Rows[0]["UserLoginName"].ToString();
                objUser.IsSystemAdmin = (Boolean)ds.Tables[0].Rows[0]["IsSystemAdmin"];
                //objUser.IsDiscussAdmin = (Boolean)ds.Tables[0].Rows[0]["IsDiscussAdmin"];
                //objUser.RestrictDownload = (Boolean)ds.Tables[0].Rows[0]["RestrictDownload"];
                //objUser.RestrictAccess = (Boolean)ds.Tables[0].Rows[0]["RestrictAccess"];
                objUser.UserStatus = int.Parse(ds.Tables[0].Rows[0]["UserStatus"].ToString());
                objUser.LanguageNum = ds.Tables[0].Rows[0]["LanguageNum"].ToString();
                //objUser.OperationUnit = ds.Tables[0].Rows[0]["OpUnitCode"].ToString();
                //objUser.NonLife = ds.Tables[0].Rows[0]["UserRole"].ToString();
                objUser.UserType = int.Parse(ds.Tables[0].Rows[0]["UserType"].ToString());
                //objUser.UserIdCode = ds.Tables[0].Rows[0]["UserIdCode"].ToString();
                //objUser.BranchCode = ds.Tables[0].Rows[0]["BranchCode"].ToString();
                objUser.DeptCode = ds.Tables[0].Rows[0]["DeptCode"].ToString();
                objUser.UserRoleCode = ds.Tables[0].Rows[0]["UserGroupCode"].ToString();
                objUser.LocType = ds.Tables[0].Rows[0]["LocType"].ToString();
                objUser.LocCode = ds.Tables[0].Rows[0]["LocCode"].ToString();
                objUser.IsUsrAdmin = (Boolean)ds.Tables[0].Rows[0]["IsUsrAdmin"];
                objUser.UsrMobNumber = ds.Tables[0].Rows[0]["UserMobileNo1"].ToString();
                objUser.UsrEmailId = ds.Tables[0].Rows[0]["UserEmailId"].ToString();
                objUser.UsrDob = ds.Tables[0].Rows[0]["DOB"].ToString();
                objUser.UsrEffectiveDT = ds.Tables[0].Rows[0]["EffFrom"].ToString();
                objUser.UsrCeaseDT = ds.Tables[0].Rows[0]["EffTo"].ToString();

            }
            catch (Exception ex)
            {

                throw;
            }
            return objUser;
        }
    }
}