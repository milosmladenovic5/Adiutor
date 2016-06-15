using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcIng.Models;
using Buisiness.DTOs;
using Buisiness;
using MvcIng.Logic;

namespace MvcIng.Controllers
{
    public class RequestLeaveController : Controller
    {
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.MyLeaves)]
        public ActionResult RequestLeave()
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            RequestLeaveModel newLeave = new RequestLeaveModel();
            newLeave = RequestLeaveModel.Load(SessionManager.Data.Get<int>(SessionKeys.UserID));
            return View(newLeave);
        }
        [HttpPost]
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.MyLeaves)]
        public ActionResult RequestLeave(RequestLeaveModel newLeave)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            if(ModelState.IsValid)
            {
                if(newLeave.start<=DateTime.Today)
                {
                    ModelState.AddModelError("start", "Invalid start date");
                    return View("RequestLeave",newLeave);
                }
                if(newLeave.end<newLeave.start)
                {
                    ModelState.AddModelError("end", "Invalid end date");
                    return View("RequestLeave", newLeave);
                }
                int id=SessionManager.Data.Get<int>(SessionKeys.UserID);
                if(Users.ValidLeave(id,newLeave.start,newLeave.end))
                {
                    Leaves.UserRequestLeave(id,newLeave.start,newLeave.end,newLeave.comment);
                    return RedirectToAction("MyLeaves","MyLeaves");
                }
                else
                {
                    ModelState.AddModelError("end","You cannot take this many days");
                    return View("RequestLeave",newLeave);
                }
            }
            else
                return View("RequestLeave",newLeave);
        }
    }
}