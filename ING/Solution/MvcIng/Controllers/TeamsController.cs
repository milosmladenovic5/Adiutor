using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcIng.Models;
using MvcIng.Logic;
using Buisiness;

namespace MvcIng.Controllers
{
    public class TeamsController : Controller
    {
        [Logic.Authorize(AllowedPermission=Definitions.PermissionKeys.Teams)]
        public ActionResult Teams()
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn", new { area = "" });
            AllTeamsModel allTeams = new AllTeamsModel();
            allTeams.teams = AllTeamsModel.Load();
            return View(allTeams);
        }

        [HttpPost]
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.Teams)]
        public ActionResult Search(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                AllTeamsModel teams = new AllTeamsModel();
                teams = AllTeamsModel.Search(name);
                return View("Teams", teams);
            }
            else
                return RedirectToAction("Teams", "Teams");
        }
    }
}