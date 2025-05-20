using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace KMI.FRMWRK.Web.Admin
{
    public class UserSetupDAL
    {
        public static IDbConnection GenIDbConn(string connKey)
        {
            string strConn = ConfigurationManager.ConnectionStrings[connKey].ConnectionString;
            IDbConnection conn = new SqlConnection(strConn);
            return conn;
        }

        public static IDbCommand GenIDbCommand(string commandText, IDbConnection dbConn)
        {
            IDbCommand dbCmd = new SqlCommand();
            dbCmd.CommandText = commandText;
            dbCmd.Connection = dbConn;
            return dbCmd;
        }

        public static IDbCommand GenIDbCommandProc(string commandText, IDbConnection dbConn)
        {
            IDbCommand dbCmd = new SqlCommand();
            dbCmd.CommandText = commandText;
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Connection = dbConn;
            return dbCmd;
        }

        public static IDataParameter GenIDataParam(string ParameterName, string Value, DbType Type)
        {
            IDataParameter dbParam = new SqlParameter();
            dbParam.ParameterName = ParameterName;
            dbParam.Value = (Value == null) ? System.Convert.DBNull : Value;
            dbParam.DbType = Type;
            return dbParam;
        }

        public DataSet GetUserInfo()
        {
            IDbConnection dbConn = GenIDbConn("DefaultConn");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("EXEC prc_Get_UserProfile @UserID");
            IDbCommand dbCmd = GenIDbCommand(strSql.ToString(), dbConn);

            dbCmd.Parameters.Add(GenIDataParam("@UserID", UserSetupBAL.GetCurrentUserID(), DbType.String));

            IDbDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = dbCmd;

            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            return ds;
        }

        public static int UpdateUserInfo(string UserName, string UserAddr1, string UserAddr2, string UserAddr3,
                                         string UserContactNo1, string UserContactNo2, string UserEMailAddress, string LanguageNum, string Bandwidth, string DefCCCode, string UserId)
        {
            IDbConnection dbConn = GenIDbConn("DefaultConn");

            string strSQL = "UPDATE [iUser] SET [UserName] = @UserName, [UserAddr1] = @UserAddr1, [UserAddr2] = @UserAddr2, " +
                            "[UserAddr3] = @UserAddr3, [UserContactNo1] = @UserContactNo1, [UserContactNo2] = @UserContactNo2, " +
                            "[UserEMailAddress] = @UserEMailAddress, [LanguageNum] = @LanguageNum, [Bandwidth] = @Bandwidth, [DefCCCode] = @CarrierCode WHERE [UserId] = @UserId";

            IDbCommand dbCmd = GenIDbCommand(strSQL, dbConn);

            //TODO : Uncomment this code
            //Insc.Common.Data.Provider oDp = new Insc.Common.Data.Provider();

            dbCmd.Parameters.Add(GenIDataParam("@UserName", UserName, DbType.String));
            dbCmd.Parameters.Add(GenIDataParam("@UserAddr1", UserAddr1, DbType.String));
            dbCmd.Parameters.Add(GenIDataParam("@UserAddr2", UserAddr2, DbType.String));
            dbCmd.Parameters.Add(GenIDataParam("@UserAddr3", UserAddr3, DbType.String));
            dbCmd.Parameters.Add(GenIDataParam("@UserContactNo1", UserContactNo1, DbType.String));
            dbCmd.Parameters.Add(GenIDataParam("@UserContactNo2", UserContactNo2, DbType.String));
            dbCmd.Parameters.Add(GenIDataParam("@UserEMailAddress", UserEMailAddress, DbType.String));
            dbCmd.Parameters.Add(GenIDataParam("@LanguageNum", LanguageNum, DbType.String));
            dbCmd.Parameters.Add(GenIDataParam("@Bandwidth", Bandwidth, DbType.String));
            dbCmd.Parameters.Add(GenIDataParam("@CarrierCode", DefCCCode, DbType.String));
            dbCmd.Parameters.Add(GenIDataParam("@UserId", UserId, DbType.String));

            int rowEffected = 0;

            dbConn.Open();
            try
            {
                rowEffected = dbCmd.ExecuteNonQuery();
            }
            finally
            {
                dbConn.Close();
            }
            return rowEffected;
        }

        public DataSet GetUserAccessLog()
        {
            IDbConnection dbConn = GenIDbConn("DefaultConn");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("EXEC prc_Get_User_Access_Log @UserID, @CarrierCode");

            IDbCommand dbCmd = GenIDbCommand(strSql.ToString(), dbConn);

            dbCmd.Parameters.Add(GenIDataParam("@UserID", UserSetupBAL.GetCurrentUserID(), DbType.String));
            dbCmd.Parameters.Add(GenIDataParam("@CarrierCode", HttpContext.Current.Session["CarrierCode"].ToString(), DbType.String));

            IDbDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = dbCmd;

            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            return ds;
        }

        public static int UpdateLogonName(string UserLoginName, string UserId)
        {
            IDbConnection dbConn = GenIDbConn("DefaultConn");

            string strSQL = "UPDATE [iUser] SET [UserLoginName] = @UserLoginName WHERE [UserId] = @UserId";

            IDbCommand dbCmd = GenIDbCommand(strSQL, dbConn);

            dbCmd.Parameters.Add(GenIDataParam("@UserLoginName", UserLoginName, DbType.String));
            dbCmd.Parameters.Add(GenIDataParam("@UserId", UserId, DbType.String));

            int rowEffected = 0;

            dbConn.Open();
            try
            {
                rowEffected = dbCmd.ExecuteNonQuery();
                UserSetupBAL.UpdateCurrentLogonName(UserLoginName);
            }
            finally
            {
                dbConn.Close();
            }
            return rowEffected;
        }

        public static int UpdatePwd(string UserPIN, string UserId)
        {
            IDbConnection dbConn = GenIDbConn("DefaultConn");

            //Update db iUser
            string strSQL = "UPDATE [iUser] SET [UserPIN] = @UserPIN, LastPinChangeDate = getdate(),UserStatus = 0 WHERE [UserId] = @UserId";
            IDbCommand dbCmd = GenIDbCommand(strSQL, dbConn);
            UserPIN = UserSetupBAL.GetEncryptedPwd(UserPIN);
            dbCmd.Parameters.Add(GenIDataParam("@UserPIN", UserPIN, DbType.String));
            dbCmd.Parameters.Add(GenIDataParam("@UserId", UserId, DbType.String));

            int rowEffected = 0;

            dbConn.Open();
            try
            {
                rowEffected = dbCmd.ExecuteNonQuery();
                UserSetupBAL.UpdateCurrentPassword(UserPIN);
            }
            finally
            {
                dbConn.Close();
            }
            return rowEffected;
        }
        public static int UpdatePass(string UserPIN, string DOB, string UserId)
        {
            IDbConnection dbConn = GenIDbConn("DefaultConn");
            IDbCommand dbCmd = GenIDbCommandProc("Prc_UpdUsrDtls", dbConn);
            UserPIN = UserSetupBAL.GetEncryptedPwd(UserPIN);
            dbCmd.Parameters.Add(GenIDataParam("@UserPIN", UserPIN, DbType.String));
            dbCmd.Parameters.Add(GenIDataParam("@UserId", UserId, DbType.String));
            dbCmd.Parameters.Add(GenIDataParam("@DOB", DOB, DbType.String));

            int rowEffected = 0;

            dbConn.Open();
            try
            {
                rowEffected = dbCmd.ExecuteNonQuery();
                UserSetupBAL.UpdateCurrentPassword(UserPIN);
            }
            finally
            {
                dbConn.Close();
            }
            return rowEffected;
        }

        public static int UpdatePwdLog(string UserPIN, string UserId)
        {
            IDbConnection dbConn = GenIDbConn("DefaultConn");

            //Insert new password in iUserPinLog
            string strPWLog = "INSERT INTO iUserPinLog VALUES(@UserId,@UserPIN,GETDATE(),@RemoteHost) ";
            IDbCommand dbCmdLog = GenIDbCommand(strPWLog, dbConn);
            UserPIN = UserSetupBAL.GetEncryptedPwd(UserPIN);
            dbCmdLog.Parameters.Add(GenIDataParam("@UserPIN", UserPIN, DbType.String));
            dbCmdLog.Parameters.Add(GenIDataParam("@UserId", UserId, DbType.String));
            dbCmdLog.Parameters.Add(GenIDataParam("@RemoteHost", HttpContext.Current.Request.ServerVariables["REMOTE_HOST"], DbType.String));


            int rowEffected = 0;

            dbConn.Open();
            try
            {
                rowEffected = dbCmdLog.ExecuteNonQuery();
            }
            finally
            {
                dbConn.Close();
            }
            return rowEffected;
        }
    }
}