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
    public class EmployeesController : Controller
    {
        [Logic.Authorize(AllowedPermission=Definitions.PermissionKeys.Menagement)]
        public ActionResult ShowEmployees()
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            int menagerID = SessionManager.Data.Get<int>(SessionKeys.UserID);
            EmployeeModel all = new EmployeeModel();
            all = EmployeeModel.Load(menagerID);
            return View(all);
        }
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.Menagement)]
        public ActionResult Update(int userID)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            AddEmployeeModel employee = new AddEmployeeModel();
            employee = AddEmployeeModel.Load(userID);
            return View(employee);
        }
        [HttpPost]
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.Menagement)]
        public ActionResult Update(AddEmployeeModel model)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            AddEmployeeModel.UpdateEmployee(model);
            return View(model);
        }
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.Menagement)]
        public ActionResult UpdateChild(int childID)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            ChildModel child = new ChildModel();
            child = ChildModel.Load(childID);
            return View(child);
        }
        [HttpPost]
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.Menagement)]
        public ActionResult UpdateChild(ChildModel model)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            ChildModel.Update(model);
            return RedirectToAction("Update", "Employees", new { UserID = model.child.UserID });
        }
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.Menagement)]
        public ActionResult DeleteChild(int childId,int userID)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            ChildModel.Delete(childId);
            return RedirectToAction("Update", "Employees", new { userID = userID });
        }
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.Menagement)]
        public ActionResult DeleteTeam(int teamID,int UserID)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            UserTeamDTO userTeam = new UserTeamDTO();
            userTeam = UsersTeams.FindUserTeam(teamID, UserID);
            UsersTeams.DeleteUserTeam(userTeam);
            return RedirectToAction("Update", "Employees", new { UserID = UserID });
        }
        [HttpPost]
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.Menagement)]
        public ActionResult AddChild(AddEmployeeModel model)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            ChildModel newChild = new ChildModel();
            int id = model.user.UserID;
            newChild.child = new ChildrenDTO();
            newChild.child.UserID = id;
            return View(newChild);
        }
        [HttpPost]
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.Menagement)]
        public ActionResult AddKid(ChildModel kid)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            ChildModel newChild = new ChildModel();
            newChild.child = new ChildrenDTO();
            newChild.child.UserID = kid.child.UserID;
            newChild.child.Name = kid.child.Name;
            newChild.child.BirthDate = kid.child.BirthDate;
            ChildModel.Insert(newChild);
            return RedirectToAction("Update", "Employees", new { UserId = kid.child.UserID });
        }
        [HttpPost]
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.Menagement)]
        public ActionResult AddTeam(AddEmployeeModel model)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            AddTeamModel team = new AddTeamModel();
            team = AddTeamModel.Load(model.user.UserID);
            team.UserID = model.user.UserID;
            return View(team);
        }
        [HttpPost]
        [Logic.Authorize(AllowedPermission = Definitions.PermissionKeys.Menagement)]
        public ActionResult InsertTeam(AddTeamModel model)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            AddTeamModel.InsertTeam(model.TeamID, model.UserID);
            return RedirectToAction("Update", "Employees", new { UserID = model.UserID });
        }
        [Logic.Authorize(AllowedPermission=Definitions.PermissionKeys.Menagement)]
        public ActionResult AddEmployee()
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            AddEmployeeModel employee = new AddEmployeeModel();
            return View(employee);
        }
        [HttpPost]
        [Logic.Authorize(AllowedPermission=Definitions.PermissionKeys.Menagement)]
        public ActionResult AddEmployee(AddEmployeeModel model)
        {
            if (!SessionManager.IsLogged)
                return RedirectToAction("LogIn", "LogIn");
            model=AddEmployeeModel.InsertEmployee(model);
           
            return View(model);
        }
    }
}