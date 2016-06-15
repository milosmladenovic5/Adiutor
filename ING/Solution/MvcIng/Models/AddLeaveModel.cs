using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Buisiness;
using Buisiness.DTOs;

namespace MvcIng.Models
{
    public class AddLeaveModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please enter start date")]
        public DateTime start { get; set; }
        public DateTime? end { get; set; }
        public string comment { get; set; }
        public List<UserDTO> allEmployees { get; set; }
        [Required(ErrorMessage="Please enter leave type")]
        public LeaveTypes.LeaveT leaveType { get; set; }

        public static AddLeaveModel Load(int menagerID)
        {
            AddLeaveModel leave = new AddLeaveModel();
            leave.allEmployees = Users.ShowAllUsers(menagerID);
            return leave;
        }
        public static void RequestLeave(int userID,int submitterID,DateTime start,DateTime? end,string comment,int leaveTypeId)
        {
            Leaves.MenagerRequestLeave(userID, submitterID, start, end, comment,leaveTypeId);
        }
    }
}