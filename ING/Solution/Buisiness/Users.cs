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
    public class Users
    {
        
        static public UserDTO InsertUser(UserDTO u)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                
               
                    User newEmployee = new User()
                    {
                        UserTypeID = u.UserTypeID,
                        StatusTypeID = u.StatusTypeID,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Password = u.Password,
                        Username = u.Username,
                        Telephone = u.Telephone,
                        Address = u.Address,
                        WorkStartDate = u.WorkStartDate,
                        BirthDate = u.BirthDate,
                        Email=u.Email,
                        RoleID=u.UserTypeID
                    };

                    db.Users.InsertOnSubmit(newEmployee);
                    db.SubmitChanges();
                    u.UserID = newEmployee.UserID;
                    return u;
             
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return u;
        }
        static public void UpdateUser(UserDTO u)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                User changeEmployee = new User()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Telephone = u.Telephone,
                    Address = u.Address,
                    BirthDate = u.BirthDate,
                    Email=u.Email,
                };
                if (u.StatusTypeID != 0)
                    changeEmployee.StatusTypeID = u.StatusTypeID;
                if (u.Password != null)
                    changeEmployee.Password = u.Password;
                if (u.UserTypeID != 0)
                    changeEmployee.UserTypeID = u.UserTypeID;
                if (u.WorkStartDate.GetHashCode()!=0)
                    changeEmployee.WorkStartDate = u.WorkStartDate;
                if (u.Username != null)
                    changeEmployee.Username = u.Username;
               
                var Change = (from newE in db.Users
                              where newE.UserID == u.UserID
                              select newE).Single();

                if (u.UserTypeID != 0)
                    Change.UserTypeID = changeEmployee.UserTypeID;
                if(u.StatusTypeID!=0)
                    Change.StatusTypeID = changeEmployee.StatusTypeID;
                Change.FirstName = changeEmployee.FirstName;
                Change.LastName = changeEmployee.LastName;
                if(u.Password!=null)
                    Change.Password = changeEmployee.Password;
                if(u.Username!=null)
                    Change.Username = changeEmployee.Username;
                Change.Telephone = changeEmployee.Telephone;
                Change.Address = changeEmployee.Address;
                if(u.WorkStartDate.GetHashCode()!=0)
                    Change.WorkStartDate = changeEmployee.WorkStartDate;
                Change.BirthDate = changeEmployee.BirthDate;
                Change.Email = changeEmployee.Email;

                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public UserDTO ReadUser(int id)
        {
            UserDTO found = new UserDTO();
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                var wanted = (from f in db.Users
                              where f.UserID == id
                              select f).Single();
                found.UserID = id;
                found.UserTypeID = wanted.UserTypeID;
                found.StatusTypeID = wanted.StatusTypeID;
                found.FirstName = wanted.FirstName;
                found.LastName = wanted.LastName;
                found.Telephone = wanted.Telephone;
                found.Address = wanted.Address;
                found.WorkStartDate = wanted.WorkStartDate;
                found.BirthDate = wanted.BirthDate;
                found.Username = wanted.Username;
                found.Password = wanted.Password;
                found.Email = wanted.Email;

                return found;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return found;
        }
        
        static public bool ChangePassword(int userID, string oldPass, string newPass)
        {
            var db = new ING_SoftwareDataContext();
            try
            {
                var find = (from n in db.Users
                            where n.UserID == userID
                            select n).Single();
                if (oldPass == find.Password)
                {
                    find.Password = newPass;
                    db.SubmitChanges();
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
        static public bool ChangeUsername(int userID, string pass, string newUsername)
        {
            var db = new ING_SoftwareDataContext();
            try
            {
                var find = (from n in db.Users
                            where n.UserID == userID
                            select n).Single();
                if (pass == find.Password)
                {
                    find.Username = newUsername;
                    db.SubmitChanges();
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
        static public List<TeamDTO> ShowTeams(int userID)
        {
            var db = new ING_SoftwareDataContext();
            List<TeamDTO> newL = new List<TeamDTO>();
            try
            {
                var find = from n in db.UsersTeams
                           where (n.UserID == userID)
                           select n.TeamID;
                foreach(var found in find)
                {
                    TeamDTO newT = new TeamDTO();
                    newT = Teams.ReadTeam(found);
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
        static public void RemoveFromTeams(int userID)
        {
            var db = new ING_SoftwareDataContext();
            try
            {
                var find = from n in db.UsersTeams
                           where n.UserID == userID
                           select n.UserTeamID;
                foreach(var found in find)
                {
                    UserTeamDTO newU = new UserTeamDTO();
                    newU.UserTeamID = found;
                    UsersTeams.DeleteUserTeam(newU);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public int NumberOfChildren(int userID,DateTime date)
        {
            int num = 0;
            var db = new ING_SoftwareDataContext();
            try
            {
                var find = from n in db.Childrens
                           where n.UserID == userID
                           select n;
                foreach(var found in find)
                {
                    if (found.BirthDate<date)
                        num++;
                }
                return num;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return num;
        }
     
        public static int YearsOfEmployment(int userID, int year)
        {
            int ExtraYear = 0;

            UserDTO user = Users.ReadUser(userID);

            DateTime TempDate = new DateTime(user.WorkStartDate.Year, user.WorkStartDate.Month, user.WorkStartDate.Day);

            try
            {
                var db = new ING_SoftwareDataContext();

                var leves = from details in db.Leaves
                            where details.UserID == userID && details.LeaveTypeID == (int)LeaveTypes.LeaveT.Suspension
                            select details.EndDate - details.StartDate;

                foreach (var row in leves)
                {
                    TempDate = (DateTime)(TempDate + row);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (TempDate.Month == 1 && TempDate.Day == 1)
            {
                ExtraYear++;
            }

            int Years = year - TempDate.Year + ExtraYear - 1;

            return Years;
        }
        static public int GrantedFreeDays(int userID,int year)
        {
            int total = 20;
            var db=new ING_SoftwareDataContext();
            try
            {
                var find=(from n in db.Users
                         where n.UserID==userID
                         select n).Single();
                DateTime newD=new DateTime(year+1,1,1);
                int kids = NumberOfChildren(userID, newD);
                int years = YearsOfEmployment(userID, newD.Year-1);
                total = total + kids + years / 2;
                return total;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return total;
        }
        static public int OldDaysLeft(int userID,DateTime date)
        {
            
           
            try
            {
                var db = new ING_SoftwareDataContext();

                var findUser = (from n in db.Users
                                where n.UserID == userID
                                select n).Single();

                DateTime workDate = new DateTime(findUser.WorkStartDate.Year,findUser.WorkStartDate.Month,findUser.WorkStartDate.Day);
                int oldDays = 0;
                int newDays = 0;
                bool yes=true;
               
                while (workDate.Year <= date.Year)
                {
                    DateTime start = new DateTime(workDate.Year, 1, 1);
                    DateTime end = new DateTime(workDate.Year, 7, 1);
                    DateTime end2 = new DateTime(workDate.Year + 1, 1, 1);


                    if (newDays >= 0)
                    {
                        oldDays = newDays;
                        newDays = 0;
                    }
                    else
                        oldDays = 0;
                    newDays = GrantedFreeDays(userID, workDate.Year) + newDays;

                    if (workDate.Year == date.Year)
                    {
                        if (date < new DateTime(date.Year, 7, 1))
                        {
                            yes = false;
                            end = date;
                        }
                        else
                        {
                            end2 = date;
                        }
                    }

                    var findLeave = from leave in db.Leaves
                                    join m in db.LeaveStatusHistories on leave.LeaveID equals m.LeaveID
                                    where (leave.UserID == userID && leave.StartDate > start && leave.StartDate < end &&
                                    leave.LeaveTypeID==(int)LeaveTypes.LeaveT.Personal &&
                                    ((m.LeaveStatusTypeID == (int)LeaveStatusTypes.LeaveStatusT.Approved || 
                                    m.LeaveStatusTypeID == (int)LeaveStatusTypes.LeaveStatusT.Pending)
                                    && m.EndDate == null))
                                    select leave;
                    foreach (var found in findLeave)
                    {
                        DateTime final;
                        if (found.EndDate > date && found.StartDate <= date)
                            final = date;
                        else final = (DateTime)found.EndDate;
                        DateTime begin = new DateTime(found.StartDate.Year, found.StartDate.Month, found.StartDate.Day);
                        while (begin <= final)
                        {
                            if (begin.DayOfWeek != DayOfWeek.Saturday && begin.DayOfWeek != DayOfWeek.Sunday && !Hollidays.IsDayHolliday(begin))
                            {
                                if (oldDays > 0) oldDays--;
                                else newDays--;
                            }
                            begin = begin.AddDays(1);
                        }
                    }
                    if (yes)
                    {
                        oldDays = 0;
                        var fiLeave = from n in db.Leaves
                                      join m in db.LeaveStatusHistories on n.LeaveID equals m.LeaveID
                                      where (n.UserID == userID && n.StartDate >= end && n.StartDate < end2 &&
                                      n.LeaveTypeID == (int)LeaveTypes.LeaveT.Personal &&
                                      ((m.LeaveStatusTypeID == (int)LeaveStatusTypes.LeaveStatusT.Approved || m.LeaveStatusTypeID == (int)LeaveStatusTypes.LeaveStatusT.Pending)
                                      && m.EndDate == null))
                                      select n;
                        foreach (var found in fiLeave)
                        {
                            DateTime final;
                            if (found.EndDate > date && found.StartDate <= date)
                                final = date;
                            else final = (DateTime)found.EndDate;
                            DateTime begin = new DateTime(found.StartDate.Year, found.StartDate.Month, found.StartDate.Day);
                            while (begin <= final)
                            {
                                if (begin.DayOfWeek != DayOfWeek.Saturday && begin.DayOfWeek != DayOfWeek.Sunday && !Hollidays.IsDayHolliday(begin))
                                {
                                    newDays--;
                                }
                                begin = begin.AddDays(1);
                            }
                        }
                        
                    }
                    workDate = workDate.AddYears(1);
                }
                return oldDays;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return 0;
        }
        static public int NewDaysLeft(int userID,DateTime date)
        {
            DateTime workDate = new DateTime();

            try
            {
                var db = new ING_SoftwareDataContext();

                var findUser = (from n in db.Users
                                where n.UserID == userID
                                select n).Single();

                workDate = findUser.WorkStartDate;//promeni
                int oldDays = 0;
                int newDays = 0;
                bool yes = true;
                while (workDate.Year <= date.Year)
                {
                    DateTime start = new DateTime(workDate.Year, 1, 1);
                    DateTime end = new DateTime(workDate.Year, 7, 1);
                    DateTime end2 = new DateTime(workDate.Year + 1, 1, 1);

                    if (newDays >= 0)
                    {
                        oldDays = newDays;
                        newDays = 0;

                    }
                    else
                        oldDays = 0;
                    newDays = GrantedFreeDays(userID, workDate.Year) + newDays;

                    if (workDate.Year == date.Year)
                    {
                        if (date < new DateTime(date.Year, 7, 1))
                        {
                            yes = false;
                            end = date;
                        }
                        else
                        {
                            end2 = date;
                        }

                    }

                    var findLeave = from n in db.Leaves
                                    join m in db.LeaveStatusHistories on n.LeaveID equals m.LeaveID
                                    where (n.UserID == userID && n.StartDate > start && n.StartDate < end &&
                                    n.LeaveTypeID == (int)LeaveTypes.LeaveT.Personal &&
                                    ((m.LeaveStatusTypeID == (int)LeaveStatusTypes.LeaveStatusT.Approved ||
                                    m.LeaveStatusTypeID == (int)LeaveStatusTypes.LeaveStatusT.Pending)
                                    && m.EndDate == null))
                                    select n;
                    foreach (var found in findLeave)
                    {
                        DateTime final;
                        if (found.EndDate > date && found.StartDate <= date)
                            final = date;
                        else final = (DateTime)found.EndDate;
                        DateTime begin = new DateTime(found.StartDate.Year, found.StartDate.Month, found.StartDate.Day);
                        while (begin <= final)
                        {
                            if (begin.DayOfWeek != DayOfWeek.Saturday && begin.DayOfWeek != DayOfWeek.Sunday && !Hollidays.IsDayHolliday(begin))
                            {
                                if (oldDays > 0) oldDays--;
                                else newDays--;
                            }
                            begin = begin.AddDays(1);
                        }
                    }
                    if (yes)
                    {
                        oldDays = 0;
                        var fiLeave = from n in db.Leaves
                                      join m in db.LeaveStatusHistories on n.LeaveID equals m.LeaveID
                                      where (n.UserID == userID && n.StartDate >= end && n.StartDate < end2 && 
                                      n.LeaveTypeID==(int)LeaveTypes.LeaveT.Personal &&
                                      ((m.LeaveStatusTypeID == (int)LeaveStatusTypes.LeaveStatusT.Approved || m.LeaveStatusTypeID == (int)LeaveStatusTypes.LeaveStatusT.Pending)
                                      && m.EndDate == null))
                                      select n;
                        foreach (var found in fiLeave)
                        {
                            DateTime final;
                            if (found.EndDate > date && found.StartDate <= date)
                                final = date;
                            else final = (DateTime)found.EndDate;
                            DateTime begin = new DateTime(found.StartDate.Year, found.StartDate.Month, found.StartDate.Day);
                            while (begin <= final)
                            {
                                if (begin.DayOfWeek != DayOfWeek.Saturday && begin.DayOfWeek != DayOfWeek.Sunday && !Hollidays.IsDayHolliday(begin))
                                {
                                    newDays--;
                                }
                                begin = begin.AddDays(1);
                            }
                        }
                    }

                    workDate = workDate.AddYears(1);
                }
                if (newDays < 0)
                    newDays = 0;
                return newDays;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return 0;
        }
        static private bool IsValidLeave(int userID,DateTime start,DateTime? end)
        {
            int n=0;
            DateTime begin = new DateTime(start.Year, start.Month, start.Day);
            while(begin<=end)
            {
                if(begin.DayOfWeek!=DayOfWeek.Saturday && begin.DayOfWeek!=DayOfWeek.Sunday && !Hollidays.IsDayHolliday(begin))
                    n++;
                begin=begin.AddDays(1);
            }
            if (start.Year == end.Value.Year)
                if ((OldDaysLeft(userID, start) + NewDaysLeft(userID, start)) >= n)
                    return true;
                else return false;
            else if (IsValidLeave(userID, start, new DateTime(start.Year, 12, 31)) && IsValidLeave(userID, new DateTime(end.Value.Year, 1, 1), end))
                return true;
            else return false;
        }
        static public bool ValidLeave(int userID,DateTime st,DateTime? en)
        {
            if (IsValidLeave(userID, st, en) == false)
                return false;
            else
            {
                int fac=0;
                DateTime begin = new DateTime(st.Year, st.Month, st.Day);
                while (begin <= en)
                {
                    if (begin.DayOfWeek != DayOfWeek.Saturday && begin.DayOfWeek != DayOfWeek.Sunday && !Hollidays.IsDayHolliday(begin))
                        fac++;
                    begin = begin.AddDays(1);
                }

                int old;
                int newD;
                DateTime date;
                DateTime date1=new DateTime(en.Value.Year,6,30);
                DateTime date2=new DateTime(en.Value.Year,12,31);
                if (en.Value.Month > 6)
                {
                    old = OldDaysLeft(userID, date2);
                    newD = NewDaysLeft(userID, date2);
                    date = new DateTime(date2.Year, date2.Month, date2.Day);
                }
                else
                {
                    old = OldDaysLeft(userID, date1);
                    newD = NewDaysLeft(userID, date1);
                    date = new DateTime(date1.Year, date1.Month, date1.Day);
                }
                if (fac < old + newD)
                {
                    while (fac != 0)
                    {
                        date = date.AddMonths(6);
                        old = OldDaysLeft(userID, date);
                        newD = NewDaysLeft(userID, date);
                        if (fac > old + newD)
                            return false;
                        if (old > fac)
                            fac = 0;
                        else
                            fac = fac - old;
                    }
                }
                return true;
            }
        }
        static public List<LeaveDTO> ShowAllLeaves(int userID,DateTime start,DateTime end)
        {
            List<LeaveDTO> newL = new List<LeaveDTO>();
            var db = new ING_SoftwareDataContext();
            try
            {
                var find = from n in db.LeaveStatusHistories join m in db.Leaves on n.LeaveID equals m.LeaveID
                           where (m.UserID == userID && ((m.EndDate >= start && m.StartDate <= start) ||
                            (m.StartDate <= end && m.EndDate >= end) || (m.EndDate <= end && m.StartDate >= start)) && n.LeaveStatusTypeID == (int)LeaveStatusTypes.LeaveStatusT.Approved && n.EndDate == null)
                           select m;
                foreach(var found in find)
                {

                    LeaveDTO leave=new LeaveDTO();
                    leave.LeaveID = found.LeaveID;
                    leave.LeaveTypeID = found.LeaveTypeID;
                    leave.UserID = found.UserID;
                    leave.StartDate = found.StartDate;
                    leave.EndDate = found.EndDate;
                    if(found.StartDate<start)
                        leave.StartDate=start;
                    if (found.EndDate > end)
                        leave.EndDate = end;
                    newL.Add(leave);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return newL;
        }
        public static List<ChildrenDTO> ShowChildren(int userID)
        {
            var ReturnList = new List<ChildrenDTO>();

            try
            {
                var db = new ING_SoftwareDataContext();

                var list = from details in db.Childrens
                           where details.UserID == userID
                           select details.ChildID;

                foreach (var row in list)
                {
                    ReturnList.Add(Childrens.ReadChild(row));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return ReturnList;
        }
        public static int GetID(string username)
        {
            var db = new ING_SoftwareDataContext();
            var findUserID = (from user in db.Users
                              where user.Username == username
                              select user.UserID).Single();
            int m = findUserID;
            return m;
        }
        public static bool IsMatch(string username,string password)
        {
            var db = new ING_SoftwareDataContext();
            var found = (from user in db.Users
                         where user.Password == password && user.Username == username
                         select user).Any();
            bool m = found;
            return m;
        }
        public static string GetFirstName(string username)
        {
            var db = new ING_SoftwareDataContext();
            var findUserName = (from user in db.Users
                              where user.Username == username
                              select user.FirstName).Single();
            string m = findUserName;
            return m;
        }
        public static string GetFirstName(int id)
        {
            var db = new ING_SoftwareDataContext();
            var findUserName = (from user in db.Users
                                where user.UserID == id
                                select user.FirstName).Single();
            string m = findUserName;
            return m;
        }
        public static string GetLastName(string username)
        {
            var db = new ING_SoftwareDataContext();
            var findUserName = (from user in db.Users
                              where user.Username == username
                              select user.LastName).Single();
            string m = findUserName;
            return m;
        }
        static public void UpdateUs(int id,string firstName,string lastName,string email,string telephone,string address,DateTime? birth)
        {
            UserDTO updateUser = new UserDTO();
            updateUser.UserID = id;
            updateUser.FirstName =firstName;
            updateUser.LastName = lastName;
            updateUser.Email = email;
            updateUser.Address = address;
            updateUser.Telephone =telephone;
            updateUser.BirthDate = birth;
            Users.UpdateUser(updateUser);
            
        }
        static public List<UserDTO> ShowAllUsers(int menagerID)
        {
            var db=new ING_SoftwareDataContext();
            var users=from user in db.Users
                      where user.UserID!=menagerID
                      select user.UserID;

            List<UserDTO> employees = new List<UserDTO>();
            foreach(var id in users)
            {
                UserDTO user=new UserDTO();
                user=Users.ReadUser(id);
                employees.Add(user);
            }
            return employees;
        }
        public static User GetUser(string username, string password)
        {
            using (var dc = new ING_SoftwareDataContext())
            {
                return string.IsNullOrWhiteSpace(password)
                           ? null
                           : dc.Users.SingleOrDefault(
                               item => item.Username == username && item.Password == password);
            }
        }
    }
}
