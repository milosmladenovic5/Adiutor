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
    public class Events
    {
        public string Who;
        public DateTime When;
        public string What;

        public static List<Events> ReturnAllEvents()
        {
            List<Events> allEvents = new List<Events>();
            DateTime now = DateTime.Today;
            DateTime end = now.AddMonths(1);

            
            var db = new ING_SoftwareDataContext();
            var findUser = from user in db.Users
                           where ((user.BirthDate.Value.Month == now.Month && user.BirthDate.Value.Day >= now.Day)
                           || ((user.BirthDate.Value.Month == now.Month + 1 || (user.BirthDate.Value.Month==1 && now.Month==12 )) && user.BirthDate.Value.Day <= now.Day)) ||
                           ((user.WorkStartDate.Month == now.Month && user.WorkStartDate.Day >= now.Day)
                           || ((user.WorkStartDate.Month == now.Month + 1 || (user.WorkStartDate.Month==1 && now.Month==12 )) && user.WorkStartDate.Day <= now.Day))
                           select user;
            foreach(var user in findUser)
            {
                now = DateTime.Today;
                while(now!=end)
                {
                    Events e = new Events();
                    if(user.WorkStartDate.Month==now.Month && user.WorkStartDate.Day==now.Day)
                    {
                        int i = now.Year - user.WorkStartDate.Year;
                        e.What = i + "-work anniversary";
                        e.Who = user.FirstName + " " + user.LastName;
                        e.When = now;
                        allEvents.Add(e);
                    }
                    if(user.BirthDate.Value.Month==now.Month && user.BirthDate.Value.Day==now.Day)
                    {
                        int i = now.Year - user.BirthDate.Value.Year;
                        e.What = i + ". Birthday";
                        e.Who = user.FirstName + " " + user.LastName;
                        e.When = now;
                        allEvents.Add(e);
                    }
                    now = now.AddDays(1);
                }
            }
            now = DateTime.Today;
            List<HollidayDTO> hollidays = new List<HollidayDTO>();
            hollidays = Hollidays.ShowAllHollidays(now, end);
            int m = hollidays.Count;
            for (int i = 0; i < m;i++)
            {
                Events e = new Events();
                e.What = "Holliday";
                e.When = hollidays[i].StartDate;
                e.Who = hollidays[i].Name;
                allEvents.Add(e);
            }
            
            allEvents=allEvents.OrderBy(Events => Events.When).ToList();
            return allEvents;
        }
    }
}
