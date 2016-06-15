using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisiness.DTOs
{
    public class LeaveStatusHistoryDTO
    {
        public int LeaveStatusHistoryID { get; set; }

        public int LeaveStatusTypeID { get; set; }

        public int UserID { get; set; }

        public int LeaveID { get; set; }

        public DateTime StartDate { get; set; }

        public System.Nullable<System.DateTime> EndDate { get; set; }

        public string Comment { get; set; }
    }
}
