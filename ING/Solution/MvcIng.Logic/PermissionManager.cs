using System;
using Buisiness;

namespace MvcIng.Logic
{
    public class PermissionManager
    {
        private static bool[] PermissionsArray
        {
            get { return SessionManager.Data.Get<bool[]>(SessionKeys.Permissions); }
        }

        public static void Init(bool[] permissions)
        {
            SessionManager.Data.Set(SessionKeys.Permissions, permissions);
        }

        public static bool IsPermissionGranted(Definitions.PermissionKeys permissionKey)
        {
            var permissionIndex = ((int)permissionKey);
            if (permissionIndex >= PermissionsArray.Length)
            {
                throw new Exception("Requested permission outside of bounds of Permissions array.");
            }

            return PermissionsArray[permissionIndex];
        }
    }
}
