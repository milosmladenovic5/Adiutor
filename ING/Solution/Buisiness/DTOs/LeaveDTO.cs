using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisiness.DTOs
{
    public class LeaveDTO
    {
        public int LeaveID { get; set; }

        public int LeaveTypeID { get; set; }

        public int UserID { get; set; }

        public DateTime StartDate { get; set; }

        public System.Nullable<System.DateTime> EndDate { get; set; }
    }
}
