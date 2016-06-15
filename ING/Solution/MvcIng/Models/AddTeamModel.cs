using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Buisiness;
using Buisiness.DTOs;

namespace MvcIng.Models
{
    public class AddTeamModel
    {
        public List<TeamDTO> AllTeams { get; set; }
        public int TeamID { get; set; }
        public int UserID { get; set; }
        public static AddTeamModel Load(int userID)
        {
            AddTeamModel model = new AddTeamModel();
            model.AllTeams = Teams.TeamsWithoutUser(userID);
            return model;
        }
        public static void InsertTeam(int TeamID,int UserID)
        {
            UserTeamDTO team = new UserTeamDTO();
            team.TeamID = TeamID;
            team.UserID = UserID;
            UsersTeams.InsertUserTeam(team);
        }
    }
}