using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Buisiness;
using System.ComponentModel.DataAnnotations;

namespace MvcIng.Models
{
    public class RequestLeaveModel
    {
        [Required(ErrorMessage="Please enter start date")]
        public DateTime start { get; set; }
        [Required(ErrorMessage="Please enter end date")]
        public DateTime end { get; set; }
        public string comment { get; set; }
        public int oldDaysLeft { get; set; }
        public int newDaysLeft { get; set; }

        public static RequestLeaveModel Load(int id)
        {
            RequestLeaveModel leave = new RequestLeaveModel();
            leave.oldDaysLeft = Users.OldDaysLeft(id,DateTime.Today);
            leave.newDaysLeft = Users.NewDaysLeft(id, DateTime.Today);
            return leave;
        }
    }
}