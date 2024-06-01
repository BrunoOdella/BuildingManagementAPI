using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Out
{
    public class ManagerResponse
    {
        public Guid ManagerId { get; set; }
        public string Email { get; set; }

        public ManagerResponse(Domain.Manager manager)
        {
            ManagerId = manager.ManagerId;
            Email = manager.Email;
        }
    }
}
