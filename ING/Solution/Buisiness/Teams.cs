using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Buisiness.DTOs;

namespace Buisiness
{
    static public class Teams
    {
        
        static public void InsertTeam(TeamDTO t)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                Team newT = new Team()
                {
                    Name = t.Name,
                };
                db.Teams.InsertOnSubmit(newT);
                db.SubmitChanges();
                t.TeamID = newT.TeamID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public void UpdateTeam(TeamDTO t)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                Team newT = new Team()
                {
                    Name = t.Name,
                };
                var newTeam = (from n in db.Teams
                               where n.TeamID == t.TeamID
                               select n).Single();

                newTeam.Name = newT.Name;

                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public void DeleteTeam(TeamDTO t)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();

                var deleted = (from n in db.Teams
                               where n.TeamID == t.TeamID
                               select n).Single();

                db.Teams.DeleteOnSubmit(deleted);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public TeamDTO ReadTeam(int id)
        {
            TeamDTO found = new TeamDTO();
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                var wanted = (from n in db.Teams
                              where n.TeamID == id
                              select n).Single();
                found.TeamID = id;
                found.Name = wanted.Name;

                // return found;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return found;
        }
        
        static public TeamDTO NewTeam(string name)
        {
            TeamDTO newT = new TeamDTO()
            {
                Name = name,
            };
            InsertTeam(newT);
            return newT;
        }
        static public void InsertUser(int userID, int teamID)
        {
            UserTeamDTO newU = new UserTeamDTO()
            {
                UserID = userID,
                TeamID = teamID,
            };
            UsersTeams.InsertUserTeam(newU);
        }
        static public void DeleteTeam(int teamID)
        {
            var db = new ING_SoftwareDataContext();
            TeamDTO newT = new TeamDTO()
            {
                TeamID = teamID,
            };
            var find = from n in db.UsersTeams
                       where n.TeamID == teamID
                       select n.UserTeamID;
            foreach (var found in find)
            {
                UserTeamDTO u = new UserTeamDTO();
                u.UserTeamID = found;
                UsersTeams.DeleteUserTeam(u);
            }
            DeleteTeam(newT);
        }
        static public List<UserDTO> AllMembers(int teamID)
        {
            List<UserDTO> wanted = new List<UserDTO>();
            var db = new ING_SoftwareDataContext();

            var member = from n in db.UsersTeams
                         where n.TeamID == teamID
                         select n.UserID;

            foreach (var mem in member)
            {
                UserDTO newU = new UserDTO();
                newU = Users.ReadUser(mem);
                wanted.Add(newU);
            }
            return wanted;
        }
        static public bool IsPartOfTeam(int userID, int teamID)
        {
            var db = new ING_SoftwareDataContext();
            var find = (from n in db.UsersTeams
                        where (n.UserID == userID && n.TeamID == teamID)
                        select n.UserTeamID).Any();
            return find;
        }
        static public void DeleteUserFromTeam(int userID, int teamID)
        {
            var db = new ING_SoftwareDataContext();
            try
            {
                var find = (from n in db.UsersTeams
                            where (n.UserID == userID && n.TeamID == teamID)
                            select n.UserTeamID).Single();

                UserTeamDTO newU = new UserTeamDTO();
                newU.UserTeamID = find;
                UsersTeams.DeleteUserTeam(newU);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public List<TeamDTO> AllTeams()
        {
            List<TeamDTO> newList = new List<TeamDTO>();
            var db = new ING_SoftwareDataContext();
            try
            {
                var find = (from n in db.Teams
                            select n.TeamID).Any();


                var f = from n in db.Teams
                        select n.TeamID;
                foreach (var found in f)
                {
                    TeamDTO newT = new TeamDTO();
                    newT = ReadTeam(found);
                    newList.Add(newT);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           
            return newList;
        }
        static public List<TeamDTO> AllEmptyTeams()
        {
            var db = new ING_SoftwareDataContext();
            List<TeamDTO> newL = new List<TeamDTO>();
            try
            {
                var find = (from n in db.Teams
                            select n.TeamID).Except(from m in db.UsersTeams
                                                    select m.TeamID);
                foreach(var found in find)
                {
                    TeamDTO newT = new TeamDTO();
                    newT = ReadTeam(found);
                    newL.Add(newT);
                }
                return newL;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return newL;
        }
        static public List<TeamDTO> TeamsWithoutUser(int userID)
        {
             List<TeamDTO> newList = new List<TeamDTO>();
            var db = new ING_SoftwareDataContext();
            try
            {
                var find = (from n in db.Teams
                            select n.TeamID).Except(from userTeam in db.UsersTeams
                                                    where userTeam.UserID == userID
                                                    select userTeam.TeamID);
                foreach (var found in find)
                {
                    TeamDTO newT = new TeamDTO();
                    newT = ReadTeam(found);
                    newList.Add(newT);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           
            return newList;
        }
        
    }
}

