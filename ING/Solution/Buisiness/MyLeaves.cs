using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Buisiness
{
    public class MyLeaves
    {
        public DateTime start { get; set; }
        public DateTime? end { get; set; }
        public string status { get; set; }
        public string comment { get; set; }
        public int leaveId { get; set; }
        public int histId { get; set; }

         static public List<MyLeaves> allLeaves(int userID)
        {
            DateTime today = DateTime.Now;
            var db = new ING_SoftwareDataContext();

            var leaves = from leave in db.Leaves
                         join l in db.LeaveStatusHistories on leave.LeaveID equals l.LeaveID 
                         where (leave.StartDate >= today || leave.EndDate >= today) && leave.UserID == userID &&
                         l.EndDate == null && l.LeaveStatusTypeID != (int)LeaveStatusTypes.LeaveStatusT.Cancelled
                         && leave.LeaveTypeID == (int)LeaveTypes.LeaveT.Personal
                         select new {leave,l};

            List<MyLeaves> allLeaves = new List<MyLeaves>();
            foreach(var found in leaves)
            {
                MyLeaves my = new MyLeaves();
                my.start = found.leave.StartDate;
                my.end = found.leave.EndDate;
                my.comment = found.l.Comment;
                my.leaveId = found.leave.LeaveID;
                my.histId = found.l.LeaveStatusHistoryID;
                if ((LeaveStatusTypes.LeaveStatusT)found.l.LeaveStatusTypeID == LeaveStatusTypes.LeaveStatusT.Approved)
                    my.status = "Approved";
                if ((LeaveStatusTypes.LeaveStatusT)found.l.LeaveStatusTypeID == LeaveStatusTypes.LeaveStatusT.Rejected)
                    my.status = "Rejected";
                if ((LeaveStatusTypes.LeaveStatusT)found.l.LeaveStatusTypeID == LeaveStatusTypes.LeaveStatusT.Pending)
                    my.status = "Pending";
                allLeaves.Add(my);
            }
            return allLeaves;
        }
    }
}
