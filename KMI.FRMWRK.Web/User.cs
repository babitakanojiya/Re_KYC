using System;

namespace KMI.FRMWRK.Web.Admin
{
    internal class User
    {
        private string _UserId;
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        private string _SessionUserId;
        public string SessionUserId
        {
            get { return _SessionUserId; }
            set { _SessionUserId = value; }
        }
        private int _UserStatus;
        public int UserStatus
        {
            get { return _UserStatus; }
            set { _UserStatus = value; }
        }
		//Added Code by Venkat on 18/01/08
		private  string _UserIdCode;
		public string UserIdCode
		{
			get { return _UserIdCode; }
			set { _UserIdCode = value; }
		}
		//Ended Code
        private int _UserType;
        public int UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _UserLoginName;
        public string UserLoginName
        {
            get { return _UserLoginName; }
            set { _UserLoginName = value; }
        }

        private string _UserPIN;
        public string UserPIN
        {
            get { return _UserPIN; }
            set { _UserPIN = value; }
        }

        private Boolean _IsSystemAdmin;
        public Boolean IsSystemAdmin
        {
            get { return _IsSystemAdmin; }
            set { _IsSystemAdmin = value; }
        }

        private Boolean _IsDiscussAdmin;
        public Boolean IsDiscussAdmin
        {
            get { return _IsDiscussAdmin; }
            set { _IsDiscussAdmin = value; }
        }

        private Boolean _RestrictAccess;
        public Boolean RestrictAccess
        {
            get { return _RestrictAccess; }
            set { _RestrictAccess = value; }
        }

        private Boolean _RestrictDownload;
        public Boolean RestrictDownload
        {
            get { return _RestrictDownload; }
            set { _RestrictDownload = value; }
        }

        private string _LanguageNum;
        public string LanguageNum
        {
            get { return _LanguageNum; }
            set { _LanguageNum = value; }
        }

        private string _OperationUnit;
        public string OperationUnit
        {
            get { return _OperationUnit; }
            set { _OperationUnit = value; }
        }

        private string _NonLife;
        public string NonLife
        {
            get { return _NonLife; }
            set { _NonLife = value; }
        }

        private int _LogonFailureCount;
        public int LogonFailureCount
        {
            get { return _LogonFailureCount; }
            set { _LogonFailureCount = value; }
        }

        private string _LifeUserGroup;
        public string LifeUserGroup
        {
            get { return _LifeUserGroup; }
            set { _LifeUserGroup = value; }
        }

        private string _NLifeUserGroup;
        public string NLifeUserGroup
        {
            get { return _NLifeUserGroup; }
            set { _NLifeUserGroup = value; }
        }
        private string _BranchCode;
        public string BranchCode
        {
            get { return _BranchCode; }
            set { _BranchCode = value; }
        }
        private string _DeptCode;
        public string DeptCode
        {
            get { return _DeptCode; }
            set { _DeptCode = value; }
        }
        private string _UserRoleCode;
        public string UserRoleCode
        {
            get { return _UserRoleCode; }
            set { _UserRoleCode = value; }
        }

        private string _LocType;
        public string LocType
        {
            get { return _LocType; }
            set { _LocType = value; }
        }

        private string _LocCode;
        public string LocCode
        {
            get { return _LocCode; }
            set { _LocCode = value; }
        }
        //Added by darshana 18-01-13
        private Boolean _IsAppAdmin;
        public Boolean IsAppAdmin
        {
            get { return _IsAppAdmin; }
            set { _IsAppAdmin = value; }
        }
        //Added by darshana 29-01-13
        private Boolean _IsUsrAdmin;
        public Boolean IsUsrAdmin
        {
            get { return _IsUsrAdmin; }
            set { _IsUsrAdmin = value; }
        }
        // added by darshana 22-Aug-13
        private string _UsrEffectiveDT;
        public string UsrEffectiveDT
        {
            get { return _UsrEffectiveDT; }
            set { _UsrEffectiveDT = value; }
        }
        
        private string _UsrCeaseDT;
        public string UsrCeaseDT
        {
            get { return _UsrCeaseDT; }
            set { _UsrCeaseDT = value; }
        }
        // added by darshana 22-Aug-13
        //added by asrar
        //Added by darshana 28-05-13
        private string _UsrEmailId;
        public string  UsrEmailId
        {
            get { return _UsrEmailId; }
            set { _UsrEmailId = value; }
        }

        private string _UsrDob;
        public string UsrDob
        {
            get { return _UsrDob; }
            set { _UsrDob = value; }
        }
        private string _UsrMobNumber;
        public string UsrMobNumber
        {
            get { return _UsrMobNumber; }
            set { _UsrMobNumber = value; }
        }
				   //added by asrar
        //Added by darshana 28-05-13
        public User()
        {
            UserType = 4;
            LogonFailureCount = 0;
        }
    }
}