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
    static public class Hollidays
    {
        static public void InsertHolliday(HollidayDTO h)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                Holliday newL = new Holliday()
                {
                    Name = h.Name,
                    StartDate = h.StartDate,
                    EndDate = h.EndDate,
                };
                db.Hollidays.InsertOnSubmit(newL);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public void UpdateLeave(HollidayDTO h)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                Holliday newL = new Holliday()
                {
                    Name = h.Name,
                    StartDate = h.StartDate,
                    EndDate = h.EndDate,
                };
                var newHolliday = (from n in db.Hollidays
                                   where n.HollidayID == h.HollidayID
                                   select n).Single();


                newHolliday.Name = newL.Name;
                newHolliday.StartDate = newL.StartDate;
                newHolliday.EndDate = newL.EndDate;

                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public void DeleteHolliday(HollidayDTO h)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();

                var deleted = (from n in db.Hollidays
                               where n.HollidayID == h.HollidayID
                               select n).Single();

                db.Hollidays.DeleteOnSubmit(deleted);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public HollidayDTO ReadHolliday(int id)
        {
            HollidayDTO found = new HollidayDTO();
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                var wanted = (from n in db.Hollidays
                              where n.HollidayID == id
                              select n).Single();

                found.StartDate = wanted.StartDate;
                found.EndDate = wanted.EndDate;
                found.Name = wanted.Name;
                // return found;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return found;
        }

        static public List<HollidayDTO> ShowAllHollidays(DateTime start,DateTime end)
        {
            List<HollidayDTO> listOfHollidays = new List<HollidayDTO>();
            var db = new ING_SoftwareDataContext();
            try
            {
                var find = from n in db.Hollidays
                           where (n.StartDate >= start && n.StartDate <= end) || (n.EndDate >= start && n.EndDate <= end)
                           select n;
                foreach(var found in find)
                {
                    HollidayDTO newH = new HollidayDTO();
                    newH.StartDate = found.StartDate;
                    newH.EndDate = found.EndDate;
                    if(found.StartDate<start)
                        newH.StartDate = start;
                    if(found.EndDate>end)
                        newH.EndDate = end;
                    newH.Name = found.Name;
                    listOfHollidays.Add(newH);
                }
                return listOfHollidays;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return listOfHollidays;
        }
        static public bool IsDayHolliday(DateTime day)
        {
            List<HollidayDTO> allHollidays = new List<HollidayDTO>();
            allHollidays = ShowAllHollidays(day, day);
            int m = allHollidays.Count;
            if(m==0) return false;
            else
            return true;
        }
    };
}
