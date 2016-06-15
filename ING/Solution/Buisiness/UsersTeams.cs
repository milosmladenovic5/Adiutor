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
    static public class UsersTeams
    {
        static public void InsertUserTeam(UserTeamDTO t)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                UsersTeam newT = new UsersTeam()
                {
                    TeamID = t.TeamID,
                    UserID = t.UserID,
                };
                var IsThere = (from userTeam in db.UsersTeams
                               where userTeam.TeamID == t.TeamID && userTeam.UserID == t.UserID
                               select userTeam).Any();
                if (!IsThere)
                {
                    db.UsersTeams.InsertOnSubmit(newT);
                    db.SubmitChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public void UpdateUserTeam(UserTeamDTO t)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                UsersTeam newT = new UsersTeam()
                {
                    TeamID = t.TeamID,
                    UserID = t.UserID,
                };
                var newTeam = (from n in db.UsersTeams
                               where n.UserTeamID == t.UserTeamID
                               select n).Single();

                newTeam.TeamID = newT.TeamID;
                newTeam.UserID = newT.UserID;

                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public void DeleteUserTeam(UserTeamDTO t)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();

                var deleted = (from n in db.UsersTeams
                               where n.UserTeamID == t.UserTeamID
                               select n).Single();

                db.UsersTeams.DeleteOnSubmit(deleted);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public UserTeamDTO ReadUserTeam(int id)
        {
            UserTeamDTO found = new UserTeamDTO();
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                var wanted = (from n in db.UsersTeams
                              where n.UserTeamID == id
                              select n).Single();

                found.TeamID = wanted.TeamID;
                found.UserID = wanted.UserID;
                found.UserTeamID = wanted.UserTeamID;
                // return found;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return found;
        }
        static public UserTeamDTO FindUserTeam(int teamID, int userID)
        {
            UserTeamDTO found = new UserTeamDTO();
            ING_SoftwareDataContext db = new ING_SoftwareDataContext();
            var wanted = (from n in db.UsersTeams
                          where n.TeamID == teamID && n.UserID==userID
                          select n).Single();

            found.TeamID = wanted.TeamID;
            found.UserID = wanted.UserID;
            found.UserTeamID = wanted.UserTeamID;
            return found;
        }
    }
}

