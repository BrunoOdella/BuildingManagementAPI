using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.In
{
    public class CreateInvitationRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Invitation ToEntity()
        {
            return new Invitation
            {
                Email = Email,
                Name = Name,
                ExpirationDate = ExpirationDate
            };
        }
    }
}
