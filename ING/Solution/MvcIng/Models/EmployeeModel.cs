using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Buisiness;
using Buisiness.DTOs;

namespace MvcIng.Models
{
    public class EmployeeModel
    {
        public List<UserDTO> Employees { get; set; }

        public static EmployeeModel Load(int menagerId)
        {
            EmployeeModel model = new EmployeeModel();
            model.Employees = Users.ShowAllUsers(menagerId);
            return model;
        }
    }
}