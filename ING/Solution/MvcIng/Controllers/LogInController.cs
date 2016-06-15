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
    public class LogInController : Controller
    {
        public ActionResult LogIn()
        {
            LogInModel model = new LogInModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult LogIn(LogInModel model, string returnUrl)
        {
            try
            {
                if (!SessionManager.LogIn(model.username, model.password) || !ModelState.IsValid)
                {
                    ViewBag.BadLogin = true;
                    return View(model);
                }

                if (string.IsNullOrEmpty(returnUrl))
                {
                    return CheckLogin(returnUrl);
                }

                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex);
            }
        }
        public ActionResult LogOut()
        {
            SessionManager.LogOut();
          
            return RedirectToAction("LogIn", "LogIn", new { area = "" });

        }

        public ActionResult CheckLogin(string returnUrl)
        {
            try
            {
                if (SessionManager.IsLogged)
                {
                    /*switch ((Definitions.Roles)SessionManager.Data.Get<LogonData>(SessionKeys.LogonData).RoleID)
                    {
                        case Definitions.Roles.Administrator:
                            return RedirectToAction("Summary", "Home");
                        case Definitions.Roles.Developer:
                            return RedirectToAction("Summary", "Home");
                        default:
                            return RedirectToAction("Login");
                    }*/

                    return RedirectToAction("Home", "Home");
                }

                // not logged in
                return RedirectToAction("Login", new { returnUrl });
            }
            catch (Exception exc)
            {
                return View("ErrorPage", exc);
            }
        }
    }
}