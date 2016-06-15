using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisiness.DTOs
{
    public class UserDTO
    {
        public int UserID { get; set; }

        public int UserTypeID { get; set; }

        public int StatusTypeID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Telephone { get; set; }

        public string Address { get; set; }

        public System.Nullable<System.DateTime> BirthDate { get; set; }

        public System.DateTime WorkStartDate { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
		
    }
}
