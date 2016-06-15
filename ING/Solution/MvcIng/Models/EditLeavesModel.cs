using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Buisiness;
using Buisiness.DTOs;

namespace MvcIng.Models
{
    public class EditLeavesModel
    {
        public List<LeaveToEdit> leaves;

        public static EditLeavesModel Load()
        {
            EditLeavesModel LeavesToEdit = new EditLeavesModel();
            LeavesToEdit.leaves = LeaveToEdit.ShowAll();
            LeavesToEdit.leaves = LeavesToEdit.leaves.OrderBy(m => m.start).ToList();
            return LeavesToEdit;
        }
        public static void Approve(int submitterID,int leaveID,string com)
        {
            int leaveStatus=(int)LeaveStatusTypes.LeaveStatusT.Approved;
            Leaves.UpdateLeaveStatus(submitterID, leaveStatus, leaveID, com);
        }
        public static void Reject(int submitterID,int leaveID,string com)
        {
            int leaveStatus = (int)LeaveStatusTypes.LeaveStatusT.Rejected;
            Leaves.UpdateLeaveStatus(submitterID, leaveStatus, leaveID, com);
        }
    }
}