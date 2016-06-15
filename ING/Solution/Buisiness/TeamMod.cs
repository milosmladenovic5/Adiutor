using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisiness.DTOs;
using DataBase;

namespace Buisiness
{
    public class TeamMod
    {
        public List<UserDTO> users { get; set; }
        public int numOfMembers { get; set; }
        public string name;

        public static List<UserDTO> FindMembers(int id)
        {
            var db = new ING_SoftwareDataContext();

            var users = from ut in db.UsersTeams
                        join team in db.Teams on ut.TeamID equals team.TeamID
                        where team.TeamID == id
                        select ut.UserID;
            List<UserDTO> list = new List<UserDTO>();
            foreach(var user in users)
            {
                UserDTO u = new UserDTO();
                u = Users.ReadUser(user);
                list.Add(u);
            }
            return list;
        }
        public static int NumOfMem(int id)
        {
            var db = new ING_SoftwareDataContext();

            var users = from ut in db.UsersTeams
                        join team in db.Teams on ut.TeamID equals team.TeamID
                        where team.TeamID == id
                        select ut.UserID;
            int n = 0;
            foreach (var user in users)
            {
                n++;
            }
            return n;
        }
    }
}
