using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Buisiness.DTOs;
using Buisiness;

namespace MvcIng.Models
{
    public class ProfileModel
    {
        public ProfileModel()
        {
            children = new List<ChildrenDTO>();
            teams = new List<TeamDTO>();
        }
        public UserDTO user { get; set; }
        public List<ChildrenDTO> children { get; set; }
        public List<TeamDTO> teams { get; set; }
        public int NumberOfChildren { get; set; }
        public static ProfileModel Load(int id)
        {
            ProfileModel obj = new ProfileModel();
            obj.user = Users.ReadUser(id);
            obj.children = Users.ShowChildren(id);
            obj.teams = Users.ShowTeams(id);
            obj.NumberOfChildren = Users.NumberOfChildren(id, DateTime.Today);
            return obj;
        }
    }
}