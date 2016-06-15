using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Buisiness;
using Buisiness.DTOs;

namespace MvcIng.Models
{
    public class ChildModel
    {
        public ChildrenDTO child { get; set; }
        public ChildModel()
        {
            child = new ChildrenDTO();
        }
        public static ChildModel Load(int childID)
        {
            ChildrenDTO child = new ChildrenDTO();
            child = Childrens.ReadChild(childID);
            ChildModel model = new ChildModel();
            model.child = child;
            return model;
        }
        public static void Update(ChildModel model)
        {
            ChildrenDTO kid = new ChildrenDTO();
            kid.ChildID = model.child.ChildID;
            kid.UserID = model.child.UserID;
            kid.BirthDate = model.child.BirthDate;
            kid.Name = model.child.Name;
            Childrens.UpdateChild(kid);
        }
        public static void Delete(int childID)
        {
            ChildrenDTO child = new ChildrenDTO();
            child = Childrens.ReadChild(childID);
            Childrens.DeleteChild(child);
        }
        public static void Insert(ChildModel child)
        {
            ChildrenDTO kid = new ChildrenDTO();
            kid.BirthDate = child.child.BirthDate;
            kid.Name = child.child.Name;
            kid.UserID = child.child.UserID;
            Childrens.InsertChild(kid);
        }
    }
}