using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisiness;
using Buisiness.DTOs;
using DataBase;

namespace Buisiness
{
    public class LeaveToEdit
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public DateTime start { get; set; }
        public DateTime? end { get; set; }
        public string comment { get; set; }
        public int userID { get; set; }
        public int leaveID { get; set; }
        public int status { get; set; }

        public static List<LeaveToEdit> ShowAll()
        {
            List<LeaveToEdit> allLeaves = new List<LeaveToEdit>();
            var db = new ING_SoftwareDataContext();
            try
            {
                var find = from n in db.LeaveStatusHistories
                           join leave in db.Leaves on n.LeaveID equals leave.LeaveID
                           where (n.LeaveStatusTypeID == (int)LeaveStatusTypes.LeaveStatusT.Pending && n.EndDate == null) ||
                           (leave.StartDate > DateTime.Today && (n.LeaveStatusTypeID == (int)LeaveStatusTypes.LeaveStatusT.Approved || n.LeaveStatusTypeID == (int)LeaveStatusTypes.LeaveStatusT.Rejected)
                           && n.EndDate == null)
                           select new { n, leave };
                foreach (var found in find)
                {
                    LeaveToEdit l = new LeaveToEdit();
                    l.EmployeeFirstName = Users.ReadUser(found.leave.UserID).FirstName;
                    l.EmployeeLastName = Users.ReadUser(found.leave.UserID).LastName;
                    l.userID = found.leave.UserID;
                    l.start = found.leave.StartDate;
                    l.end = found.leave.EndDate;
                    l.comment = found.n.Comment;
                    l.leaveID = found.n.LeaveID;
                    l.status = found.n.LeaveStatusTypeID;
                    allLeaves.Add(l);
                }
                return allLeaves;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return allLeaves;
        }
    }
}
