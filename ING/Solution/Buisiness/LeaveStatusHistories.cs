using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Buisiness.DTOs;

namespace Buisiness
{
    static public class LeaveStatusHistories
    {
        static public void InsertLeaveStatusHistory(LeaveStatusHistoryDTO l)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                LeaveStatusHistory newH = new LeaveStatusHistory()
                {
                    LeaveStatusTypeID = l.LeaveStatusTypeID,

                    UserID = l.UserID,

                    LeaveID = l.LeaveID,

                    StartDate = l.StartDate,

                    EndDate = l.EndDate,

                    Comment = l.Comment
                };

                db.LeaveStatusHistories.InsertOnSubmit(newH);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public void UpdateLeaveStatusHistory(LeaveStatusHistoryDTO l)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                LeaveStatusHistory newH = new LeaveStatusHistory()
                {
                    Comment = l.Comment
                };
                if (l.LeaveStatusTypeID != 0)
                    newH.LeaveStatusTypeID = l.LeaveStatusTypeID;
                if (l.UserID != 0)
                    newH.UserID = l.UserID;
                if (l.LeaveStatusTypeID != 0)
                    newH.LeaveStatusTypeID = l.LeaveStatusTypeID;
                if (l.LeaveID != 0)
                    newH.LeaveID = l.LeaveID;
                if (l.StartDate.GetHashCode() != 0)
                    newH.StartDate = l.StartDate;
                if (l.EndDate.GetHashCode() != 0)
                    newH.EndDate = l.EndDate;

                var newHistory = (from n in db.LeaveStatusHistories
                                  where n.LeaveStatusHistoryID == l.LeaveStatusHistoryID
                                  select n).Single();
                if (newH.LeaveStatusTypeID != 0)
                    newHistory.LeaveStatusTypeID = newH.LeaveStatusTypeID;
                if (newH.UserID != 0)
                    newHistory.UserID = newH.UserID;
                if (newH.LeaveStatusTypeID != 0)
                    newHistory.LeaveStatusTypeID = newH.LeaveStatusTypeID;
                if (newH.LeaveID != 0)
                    newHistory.LeaveID = newH.LeaveID;
                if (newH.StartDate.GetHashCode() != 0)
                    newHistory.StartDate = newH.StartDate;
                if (newH.EndDate.GetHashCode() != 0)
                    newHistory.EndDate = newH.EndDate;
                newHistory.Comment = newH.Comment;

                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public void DeleteLeaveStatusHistory(LeaveStatusHistoryDTO l)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();

                var deleted = (from n in db.LeaveStatusHistories
                               where n.LeaveStatusHistoryID == l.LeaveStatusHistoryID
                               select n).Single();

                db.LeaveStatusHistories.DeleteOnSubmit(deleted);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public LeaveStatusHistoryDTO ReadLeaveStatusHistory(int id)
        {
            LeaveStatusHistoryDTO found = new LeaveStatusHistoryDTO();
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                var wanted = (from n in db.LeaveStatusHistories
                              where n.LeaveStatusHistoryID == id
                              select n).Single();

                found.LeaveStatusTypeID = wanted.LeaveStatusTypeID;
                found.UserID = wanted.UserID;
                found.StartDate = wanted.StartDate;
                found.EndDate = wanted.EndDate;
                found.LeaveID = wanted.LeaveID;
                found.Comment = wanted.Comment;
                // return found;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return found;
        }
        
        static public void CancelLeave(int LeaveId,int submitterID)
        {
            var db = new ING_SoftwareDataContext();

            var leaves = (from leaveSt in db.LeaveStatusHistories
                          join leave in db.Leaves on leaveSt.LeaveID equals leave.LeaveID
                          where leaveSt.EndDate == null && leaveSt.LeaveID==LeaveId
                          select new { leaveSt, leave }).Single();

            leaves.leaveSt.EndDate = DateTime.Today;
            db.SubmitChanges();

            LeaveStatusHistoryDTO newH = new LeaveStatusHistoryDTO();
            newH.LeaveID = leaves.leave.LeaveID;
            newH.LeaveStatusTypeID = (int)LeaveStatusTypes.LeaveStatusT.Cancelled;
            newH.StartDate = DateTime.Today;
            newH.UserID = submitterID;
            InsertLeaveStatusHistory(newH);
        }
    } 
}
