using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Buisiness;
using Buisiness.DTOs;

namespace MvcIng.Models
{
    public class AddEmployeeModel
    {
         public AddEmployeeModel()
        {
            children = new List<ChildrenDTO>();
            teams = new List<TeamDTO>();
        }
        public UserDTO user { get; set; }
        public List<ChildrenDTO> children { get; set; }
        public List<TeamDTO> teams { get; set; }
        public StatusTypes.StatusT employeeStatus { get; set; }
        public UserTypes.UserT employeeType { get; set; }

        public static AddEmployeeModel Load(int id)
        {
            AddEmployeeModel obj = new AddEmployeeModel();
            obj.user = Users.ReadUser(id);
            obj.children = Users.ShowChildren(id);
            obj.teams = Users.ShowTeams(id);
            obj.employeeStatus = (StatusTypes.StatusT)obj.user.StatusTypeID;
            obj.employeeType = (UserTypes.UserT)obj.user.UserTypeID;
            return obj;
        }
        public static void UpdateEmployee(AddEmployeeModel model)
        {
            UserDTO employee = new UserDTO();
            employee.UserID = model.user.UserID;
            employee.Address = model.user.Address;
            employee.BirthDate = model.user.BirthDate;
            employee.Email = model.user.Email;
            employee.FirstName = model.user.FirstName;
            employee.LastName = model.user.LastName;
            employee.StatusTypeID = (int)model.employeeStatus;
            employee.Telephone = model.user.Telephone;
            employee.Username = model.user.Username;
            employee.UserTypeID = (int)model.employeeType;
            employee.WorkStartDate = model.user.WorkStartDate;
            Users.UpdateUser(employee);
        }
        public static AddEmployeeModel InsertEmployee(AddEmployeeModel model)
        {
            UserDTO employee = new UserDTO();
            employee.Address = model.user.Address;
            employee.BirthDate = model.user.BirthDate;
            employee.Email = model.user.Email;
            employee.FirstName = model.user.FirstName;
            employee.LastName = model.user.LastName;
            employee.Password = model.user.Password;
            employee.StatusTypeID = (int)model.employeeStatus;
            employee.Telephone = model.user.Telephone;
            employee.Username = model.user.Username;
            employee.UserTypeID = (int)model.employeeType;
            employee.WorkStartDate = model.user.WorkStartDate;
            
            model.user=Users.InsertUser(employee);
            return model;
        }
    }
}