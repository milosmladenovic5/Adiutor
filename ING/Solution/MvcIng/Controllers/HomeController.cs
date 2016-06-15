using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Buisiness;
using MvcIng.Logic;
using Buisiness;

namespace MvcIng.Controllers
{
    public class HomeController : Controller
    {
        [Logic.Authorize(AllowedPermission=Definitions.PermissionKeys.Home)]
        public ActionResult Home()
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn", new { area = "" });
            Models.EventModel obj = new Models.EventModel();
            obj.events = Events.ReturnAllEvents();
            return View(obj);
        }
    }
}