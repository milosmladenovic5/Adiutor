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
    static public class Childrens
    {
        static public void InsertChild(ChildrenDTO c)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();

                Children newChild = new Children()
                {
                    UserID = c.UserID,
                    BirthDate = c.BirthDate,
                    Name=c.Name,
                };

                db.Childrens.InsertOnSubmit(newChild);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public void UpdateChild(ChildrenDTO c)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                Children newL = new Children()
                {
                    UserID = c.UserID,
                    BirthDate = c.BirthDate,
                    Name=c.Name,
                };
                var newChild = (from n in db.Childrens
                                where n.ChildID == c.ChildID
                                select n).Single();

                newChild.UserID = newL.UserID;
                newChild.BirthDate = newL.BirthDate;
                newChild.Name = newL.Name;

                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public void DeleteChild(ChildrenDTO c)
        {
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();

                var deleted = (from n in db.Childrens
                               where n.ChildID == c.ChildID
                               select n).Single();

                db.Childrens.DeleteOnSubmit(deleted);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static public ChildrenDTO ReadChild(int id)
        {
            ChildrenDTO found = new ChildrenDTO();
            try
            {
                ING_SoftwareDataContext db = new ING_SoftwareDataContext();
                var wanted = (from n in db.Childrens
                              where n.ChildID == id
                              select n).Single();

                found.BirthDate = wanted.BirthDate;
                found.UserID = wanted.UserID;
                found.Name = wanted.Name;
                found.ChildID = wanted.ChildID;
                // return found;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return found;
        }
    };
}
