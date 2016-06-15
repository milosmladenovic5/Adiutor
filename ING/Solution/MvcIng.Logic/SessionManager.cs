using System;
using System.Web;
using Buisiness;
using Buisiness.DTOs;

namespace MvcIng.Logic
{
    public static class SessionManager
    {
        private const string SESSION_KEY = "OM_SESSION_KEY";

        public static bool IsLogged
        {
            get { return Data.Get<LogonDataDTO>(SessionKeys.LogonData).UserID != null; }
        }

        public static SessionBag Data
        {
            get
            {
                if (HttpContext.Current == null || HttpContext.Current.Session == null)
                {
                    return null;
                }

                if (HttpContext.Current.Session[SESSION_KEY] == null)
                {
                    InitCurrentSession();
                }

                return (SessionBag)HttpContext.Current.Session[SESSION_KEY];
            }
        }

        public static void InitCurrentSession()
        {
            var sessionData = new SessionBag();
            HttpContext.Current.Session[SESSION_KEY] = sessionData;
            
            SetPublicLogonData();
        }

       public static bool LogIn(string username, string password)
        {
            var authenticationResult = SecurityManager.Instance.Authenticate(username, password);

            if (!authenticationResult.Authenticated)
            {
                return false;
            }

            Data.Set(SessionKeys.LogonData, authenticationResult.LogonData);
            PermissionManager.Init(Data.Get<LogonDataDTO>(SessionKeys.LogonData).Permissions);
            Data.Set(SessionKeys.UserID, authenticationResult.LogonData.UserID);
            Data.Set(SessionKeys.FirstName, authenticationResult.LogonData.FirstName);
            Data.Set(SessionKeys.LastName, authenticationResult.LogonData.LastName);
            return true;
        }

        public static void LogOut()
        {
            SetPublicLogonData();
        }

        private static void SetPublicLogonData()
        {
            var logonData = SecurityManager.Instance.GetPublicLogonData();
            Data.Set(SessionKeys.LogonData, logonData);
            PermissionManager.Init(Data.Get<LogonDataDTO>(SessionKeys.LogonData).Permissions);
        }
    }
}
