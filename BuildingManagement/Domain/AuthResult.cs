using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AuthenticationResult
    {
        public Guid UserID { get; set; }
        public string UserType { get; set; }
    }
}
