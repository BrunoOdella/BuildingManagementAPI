using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Manager
    {
        public Guid ManagerId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
