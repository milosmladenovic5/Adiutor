using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisiness;
using Buisiness.DTOs;
using Buisiness;

namespace Buisiness
{
    public class SecurityManager
    {
        private static readonly SecurityManager _Instance = new SecurityManager();
        public static SecurityManager Instance { get { return _Instance; } }

        private SecurityManager()
        {
        }

        public AuthenticationResultDTO Authenticate(string username, string password)
        {
            var user = Users.GetUser(username, password);

            if (user == null)
            {
                return new AuthenticationResultDTO
                {
                    Authenticated = false
                };
            }

            var permissionsArray = GetPermissionsArray(user.RoleID);

            var logonData = new LogonDataDTO
            {
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleID = user.RoleID,
                Permissions = permissionsArray
            };

            return new AuthenticationResultDTO
            {
                LogonData = logonData,
                Authenticated = true
            };
        }

        public bool[] GetPermissionsArray(int roleID)
        {
            var arrayLength = GetMaximumPermissionID() + 1;
            var permissionIDs = Permissions.GetPermissions(roleID);

            // checking enum maximum if bigger
            foreach (var val in Enum.GetValues(typeof(Definitions.PermissionKeys)))
            {
                if (((int)val + 1) > arrayLength)
                {
                    arrayLength = (int)val + 1;
                }
            }

            var permissionsArray = new bool[arrayLength];

            foreach (var permissionID in permissionIDs)
            {
                permissionsArray[permissionID] = true;
                // all others are false by default;
            }

            return permissionsArray;
        }

        public int GetMaximumPermissionID()
        {
            return Permissions.GetMaximumID();
        }

        public LogonDataDTO GetPublicLogonData()
        {
            var arrayLength = GetMaximumPermissionID() + 1;
            var permissionsArray = new bool[arrayLength];

            var logonData = new LogonDataDTO
                            {
                                Permissions = permissionsArray
                            };

            return logonData;
        }
    }
}
