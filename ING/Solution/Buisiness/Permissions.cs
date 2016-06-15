using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase;

namespace Buisiness
{
    class Permissions
    {
        public static int GetMaximumID()
        {
            using (var dc = new ING_SoftwareDataContext())  
            {
                var permisionIDs = dc.Permissions.Select(item => item.PermissionID).ToList();
                return permisionIDs.Count == 0 ? 0 : permisionIDs.Max();
            }
        }

        public static int[] GetPermissions(int roleID)
        {
            using (var dc = new ING_SoftwareDataContext())
            {
                return
                    (from p in dc.Permissions
                     join pr in dc.RolePermissions on p.PermissionID equals pr.PermissionID
                     join r in dc.Roles on pr.RoleID equals r.RoleID
                     where r.RoleID == roleID
                     select p.PermissionID).ToArray();
            }
        }
    }
}
