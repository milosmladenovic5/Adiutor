using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisiness.DTOs
{
    public class AuthenticationResultDTO
    {
        public bool Authenticated { get; set; }
        public LogonDataDTO LogonData { get; set; }
    }
}
