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
    public class ProfileController : Controller
    {
        [Logic.Authorize(AllowedPermission=Definitions.PermissionKeys.Profile)]
        public ActionResult Prof()
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            int m = SessionManager.Data.Get<int>(SessionKeys.UserID);
            ProfileModel ob = ProfileModel.Load(m);
            return View(ob);
        }

        [HttpPost]
        [Logic.Authorize(AllowedPermission=Definitions.PermissionKeys.Profile)]
        public ActionResult Prof(ProfileModel p)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            int m = SessionManager.Data.Get<int>(SessionKeys.UserID);
            Users.UpdateUs(m, p.user.FirstName, p.user.LastName, p.user.Email, p.user.Telephone, p.user.Address, p.user.BirthDate);
            return View(p);
        }

       /* public ActionResult ChangePassword()
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            return View();
        }*/

        [HttpPost]
        [Logic.Authorize(AllowedPermission=Definitions.PermissionKeys.Profile)]
        public ActionResult ChangePassword(PasswordModel p)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            if (ModelState.IsValid)
            {
                int m = SessionManager.Data.Get<int>(SessionKeys.UserID);
                bool isChanged = Users.ChangePassword(m, p.oldPassword, p.newPassword);
                if (isChanged == false)
                {
                    ModelState.AddModelError("oldPassword", "Passwords do not match");
                    return View(p);
                }
                return RedirectToAction("Prof", "Profile");
            }
            else
                return View(p);
        }
    }
}