using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisiness
{
    public class UserTypes
    {
        public enum UserT
        {
            Employee = 1,
            Manager,
        };
    };
    
    public class StatusTypes
    {
        public enum StatusT
        {
            Active = 1,
            Suspended,
            Inactive
        };
    };
    
    public class LeaveTypes
    {
        public enum LeaveT
        {
            Personal = 1,
            Medical,
            Suspension
        };
    };
    public class LeaveStatusTypes
    {
        public enum LeaveStatusT
        {
            Pending = 1,
            Approved,
            Rejected,
            Cancelled
        };
    };
}
