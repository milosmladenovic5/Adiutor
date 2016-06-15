using MvcIng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Buisiness;
using Buisiness.DTOs;
using MvcIng.Logic;

namespace MvcIng.Controllers
{
    public class MyLeavesController : Controller
    {
        [Logic.Authorize(AllowedPermission=Definitions.PermissionKeys.MyLeaves)]
        public ActionResult MyLeaves()
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            int id = SessionManager.Data.Get<int>(SessionKeys.UserID);
            MyLeavesModel obj = new MyLeavesModel();
            obj.myLeaves = Buisiness.MyLeaves.allLeaves(id);
            obj.FirstName = Users.GetFirstName(id);
            return View(obj);
        }
       /* [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.MyLeaves)]
        public ActionResult Update(int leaveId)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            int id = SessionManager.Data.Get<int>(SessionKeys.UserID);
            MyLeavesModel obj = new MyLeavesModel();
            obj.myLeaves = Buisiness.MyLeaves.allLeaves(id);

            Buisiness.MyLeaves myLeave = new Buisiness.MyLeaves();
            int c = obj.myLeaves.Count;
            for (int j = 0; j < c;j++ )
                if(leaveId==obj.myLeaves[j].leaveId)
                {
                    myLeave.start = obj.myLeaves[j].start;
                    myLeave.end = obj.myLeaves[j].end;
                    myLeave.histId = obj.myLeaves[j].histId;
                    myLeave.leaveId = obj.myLeaves[j].leaveId;
                    myLeave.comment = obj.myLeaves[j].comment;
                    myLeave.status = obj.myLeaves[j].status;
                }
                return View(myLeave);
        }*/
        [HttpPost]
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.MyLeaves)]
        public ActionResult Update(MyLeaves m)  
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            if(m.start.GetHashCode()==0)
            {
                ViewData["Error"] = "You must enter start date";
                int id = SessionManager.Data.Get<int>(SessionKeys.UserID);
                MyLeavesModel obj = new MyLeavesModel();
                obj.myLeaves = Buisiness.MyLeaves.allLeaves(id);
                obj.FirstName = Users.GetFirstName(id);
                return View("MyLeaves",obj);
            }
            if (m.end.GetHashCode() == 0)
            {
                ViewData["Error"] = "You must enter end date";
                int id = SessionManager.Data.Get<int>(SessionKeys.UserID);
                MyLeavesModel obj = new MyLeavesModel();
                obj.myLeaves = Buisiness.MyLeaves.allLeaves(id);
                obj.FirstName = Users.GetFirstName(id);
                return View("MyLeaves", obj);
            }
            if (m.start <= DateTime.Today)
            {
                ViewData["Error"] = "Invalid start date";
                int id = SessionManager.Data.Get<int>(SessionKeys.UserID);
                MyLeavesModel obj = new MyLeavesModel();
                obj.myLeaves = Buisiness.MyLeaves.allLeaves(id);
                obj.FirstName = Users.GetFirstName(id);
                return View("MyLeaves", obj);
            }
            if (m.end < m.start)
            {
                ViewData["Error"] = "Invalid end date";
                int id = SessionManager.Data.Get<int>(SessionKeys.UserID);
                MyLeavesModel obj = new MyLeavesModel();
                obj.myLeaves = Buisiness.MyLeaves.allLeaves(id);
                obj.FirstName = Users.GetFirstName(id);
                return View("MyLeaves", obj);
            }
            LeaveDTO l = new LeaveDTO();
            l.UserID = SessionManager.Data.Get<int>(SessionKeys.UserID);
            l.LeaveID = m.leaveId;
            l.StartDate = m.start;
            l.EndDate = m.end;
            Leaves.UpdateLeave(l);

            LeaveStatusHistoryDTO h = new LeaveStatusHistoryDTO();
            h.LeaveStatusHistoryID = m.histId;
            h.Comment = m.comment;
            h.LeaveStatusTypeID = (int)LeaveStatusTypes.LeaveStatusT.Pending;
            LeaveStatusHistories.UpdateLeaveStatusHistory(h);

            return RedirectToAction("MyLeaves", "MyLeaves", new { area = "" });
        }
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.MyLeaves)]
        public ActionResult Cancel(int leaveId)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            int m = leaveId;
            int s = SessionManager.Data.Get<int>(SessionKeys.UserID);
            LeaveStatusHistories.CancelLeave(m, s);

            MyLeavesModel obj = new MyLeavesModel();
            obj.myLeaves = Buisiness.MyLeaves.allLeaves(s);
            return View("MyLeaves",obj);
        }
    }
}