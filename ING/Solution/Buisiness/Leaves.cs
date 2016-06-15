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
    static public class Leaves
    {
        
        static public LeaveDTO InsertLeave(LeaveDTO l)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                Leave newL = new Leave()
                {
                    LeaveTypeID = l.LeaveTypeID,
                    UserID = l.UserID,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                };
                db.Leaves.InsertOnSubmit(newL);
                db.SubmitChanges();
                l.LeaveID = newL.LeaveID;
                return l;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return l;
        }
        static public void UpdateLeave(LeaveDTO l)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                LeaveDTO newL = new LeaveDTO()
                {
                    UserID = l.UserID,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                };
                if (l.LeaveTypeID != 0)
                    newL.LeaveTypeID = l.LeaveTypeID;
                var newLeave = (from n in db.Leaves
                                where n.LeaveID == l.LeaveID
                                select n).Single();
                if(newL.LeaveTypeID!=0)              
                    newLeave.LeaveTypeID = newL.LeaveTypeID;
                newLeave.UserID = newL.UserID;
                newLeave.StartDate = newL.StartDate;
                newLeave.EndDate = newL.EndDate;

                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public LeaveDTO ReadLeave(int id)
        {
            LeaveDTO found = new LeaveDTO();
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                var wanted = (from n in db.Leaves
                              where n.LeaveID == id
                              select n).Single();

                found.LeaveTypeID = wanted.LeaveTypeID;
                found.StartDate = wanted.StartDate;
                found.EndDate = wanted.EndDate;
                found.UserID = wanted.UserID;
                // return found;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return found;
        }
        
        static public void UserRequestLeave(int userID,DateTime start,DateTime end,string comment)
        {
            LeaveDTO newLeave = new LeaveDTO();
            newLeave.UserID = userID;
            newLeave.LeaveTypeID = (int)LeaveTypes.LeaveT.Personal;
            newLeave.StartDate=start;
            newLeave.EndDate = end; 
            LeaveDTO inserted=Leaves.InsertLeave(newLeave);

            LeaveStatusHistoryDTO newH = new LeaveStatusHistoryDTO();
            newH.LeaveID = inserted.LeaveID;
            newH.StartDate = DateTime.Now;
            newH.EndDate = null;
            newH.LeaveStatusTypeID = (int)LeaveStatusTypes.LeaveStatusT.Pending;
            newH.UserID = userID;
            newH.Comment = comment;

            LeaveStatusHistories.InsertLeaveStatusHistory(newH);
        }
        static public void MenagerRequestLeave(int userID, int submitterID, DateTime start, DateTime? end, string comment, int leaveTypeID)
        {
            LeaveDTO newLeave = new LeaveDTO();
            newLeave.UserID = userID;
            newLeave.LeaveTypeID = leaveTypeID;
            newLeave.StartDate = start;
            newLeave.EndDate = end;
            LeaveDTO inserted = Leaves.InsertLeave(newLeave);

            LeaveStatusHistoryDTO newH = new LeaveStatusHistoryDTO();
            newH.LeaveID = inserted.LeaveID;
            newH.StartDate = DateTime.Now;
            newH.EndDate = null;
            newH.LeaveStatusTypeID = (int)LeaveStatusTypes.LeaveStatusT.Approved;
            newH.UserID = submitterID;
            newH.Comment = comment;

            LeaveStatusHistories.InsertLeaveStatusHistory(newH);
        }
        static public void UpdateLeaveStatus(int userID,int statusType,int leaveID,string comment)
        {
            var db = new ING_SoftwareDataContext();
            try
            {
                var find = (from n in db.LeaveStatusHistories
                           where (n.LeaveID == leaveID && n.EndDate == null)
                           select n).Single();
                find.EndDate = DateTime.Now;
                db.SubmitChanges();
                LeaveStatusHistoryDTO newH = new LeaveStatusHistoryDTO()
                {
                    LeaveID = find.LeaveID,
                    LeaveStatusTypeID = statusType,
                    Comment = comment,
                    StartDate = DateTime.Now,
                    EndDate = null,
                    UserID = userID,//ne moze find.UserID, jer onaj koji je podneo zahtev se razlikuje od onog koji ga odobrava npr.
                };
                LeaveStatusHistories.InsertLeaveStatusHistory(newH);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public List<LeaveDTO> ShowAll()
        {
            List<LeaveDTO> allLeaves = new List<LeaveDTO>();
            var db = new ING_SoftwareDataContext();
            try
            {
                var find = from n in db.LeaveStatusHistories join leave in db.Leaves on n.LeaveID equals leave.LeaveID
                           where (n.LeaveStatusTypeID == (int)LeaveStatusTypes.LeaveStatusT.Pending && n.EndDate == null) || 
                           (leave.StartDate>DateTime.Today && (n.LeaveStatusTypeID==(int)LeaveStatusTypes.LeaveStatusT.Approved || n.LeaveStatusTypeID==(int)LeaveStatusTypes.LeaveStatusT.Rejected)
                           && n.EndDate==null)
                           select n;
                foreach(var found in find)
                {
                    LeaveDTO pen = new LeaveDTO();
                    var leaves = (from m in db.Leaves
                                 where m.LeaveID == found.LeaveID
                                 select m).Single();
                    pen.LeaveID = leaves.LeaveID;
                    pen.LeaveTypeID = leaves.LeaveID;
                    pen.UserID = leaves.UserID;
                    pen.StartDate = leaves.StartDate;
                    pen.EndDate = leaves.EndDate;
                    allLeaves.Add(pen);
                }
                return allLeaves;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return allLeaves;
        }
    };
}
