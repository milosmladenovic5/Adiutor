using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcIng.Models;
using Buisiness;
using Buisiness.DTOs;
using MvcIng.Logic;

namespace MvcIng.Controllers
{
    public class AddLeaveController : Controller
    {
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.Menagement)]
        public ActionResult AddLeave()
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            AddLeaveModel leave = new AddLeaveModel();
            int id = SessionManager.Data.Get<int>(SessionKeys.UserID);
            leave = AddLeaveModel.Load(id);
            return View(leave);
        }
        [HttpPost]
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.Menagement)]
        public ActionResult AddLeave(AddLeaveModel model)
        {
            if (!SessionManager.IsLogged)
            {
                return RedirectToAction("LogIn", "LogIn");
            }
            int menagerID = SessionManager.Data.Get<int>(SessionKeys.UserID);
            if(!ModelState.IsValid)
            {
                model.allEmployees = Users.ShowAllUsers(menagerID);
                return View(model);
            }
            if(model.end==null && model.leaveType==LeaveTypes.LeaveT.Personal)
            {
                ModelState.AddModelError("end","Please enter end date");
                model.allEmployees=Users.ShowAllUsers(menagerID);
                return View(model);
            }
            if(model.leaveType!=LeaveTypes.LeaveT.Medical && model.leaveType!=LeaveTypes.LeaveT.Suspension && !Users.ValidLeave(model.UserID,model.start,model.end))
            {
                ModelState.AddModelError("end","There is not enough free days for this leave");
                model.allEmployees=Users.ShowAllUsers(menagerID);
                return View(model);
            }
            int leaveTypeID = (int)model.leaveType;
            AddLeaveModel.RequestLeave(model.UserID, menagerID, model.start, model.end, model.comment, leaveTypeID);
            return RedirectToAction("EditLeaves", "EditLeaves");
        }
    }
}