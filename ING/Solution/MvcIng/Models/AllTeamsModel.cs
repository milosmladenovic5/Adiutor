using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Buisiness;
using Buisiness.DTOs;

namespace MvcIng.Models
{
    public class AllTeamsModel
    {
        public List<TeamMod> teams { get; set; }

        public static List<TeamMod> Load()
        {
            List<TeamDTO> allTeams = new List<TeamDTO>();
            allTeams = Teams.AllTeams();

            List<TeamMod> ret = new List<TeamMod>();

            int m = allTeams.Count;
            for(int i=0;i<m;i++)
            {
                TeamMod newTeam = new TeamMod();
                newTeam.name = allTeams[i].Name;
                newTeam.numOfMembers = TeamMod.NumOfMem(allTeams[i].TeamID);
                newTeam.users = TeamMod.FindMembers(allTeams[i].TeamID);
                ret.Add(newTeam);
            }
            return ret;
        }
        public static AllTeamsModel Search(string teamName)
        {
            AllTeamsModel allTeams = new AllTeamsModel();
            allTeams.teams = Load();
            AllTeamsModel returnTeams = new AllTeamsModel();
            returnTeams.teams = new List<TeamMod>();
            foreach(var team in allTeams.teams)
            {
                if(team.name.Contains(teamName))
                {
                    TeamMod foundTeam = new TeamMod();
                    foundTeam.name = team.name;
                    foundTeam.numOfMembers = team.numOfMembers;
                    foundTeam.users = team.users;
                    returnTeams.teams.Add(foundTeam);
                }
            }
            return returnTeams;
        }
    }
}