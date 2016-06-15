using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcIng.Models;
using Buisiness;
using MvcIng.Logic;

namespace MvcIng.Controllers
{
    public class EditLeavesController : Controller
    {
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.Menagement)]
        public ActionResult EditLeaves()
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            EditLeavesModel leaves = new EditLeavesModel();
            leaves = EditLeavesModel.Load();
            return View(leaves);
        }
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.Menagement)]
        public ActionResult Approve(int leaveID)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            CommentModel comment = new CommentModel();
            comment.leaveID = leaveID;
            return View(comment);
        }
        [HttpPost]
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.Menagement)]
        public ActionResult Approve(CommentModel model)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            int submitterID = SessionManager.Data.Get<int>(SessionKeys.UserID);
            EditLeavesModel.Approve(submitterID, model.leaveID, model.comment);
            EditLeavesModel leaves = new EditLeavesModel();
            leaves = EditLeavesModel.Load();
            return View("EditLeaves",leaves);
        }
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.Menagement)]
        public ActionResult Reject(int leaveID)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            CommentModel comment = new CommentModel();
            comment.leaveID = leaveID;
            return View(comment);
        }
        [HttpPost]
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.Menagement)]
        public ActionResult Reject(CommentModel model)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            int submitterID = SessionManager.Data.Get<int>(SessionKeys.UserID);
            EditLeavesModel.Reject(submitterID, model.leaveID, model.comment);
            EditLeavesModel leaves = new EditLeavesModel();
            leaves = EditLeavesModel.Load();
            return View("EditLeaves",leaves);
        }
    }
}