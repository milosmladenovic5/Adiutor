using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Buisiness;

namespace MvcIng.Models
{
    public class EventModel
    {
        public List<Events> events;

        public static EventModel Load(int? param1, int? param2)
        {
            var model = new EventModel
            {
                events = new List<Events>()
            };

            return model;
        }
    }
}