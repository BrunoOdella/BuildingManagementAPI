using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Out
{
    public class CreateInvitationResponse
    {
        public Guid InvitationId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Status { get; set; }

        public CreateInvitationResponse(Invitation invitation)
        {
            this.InvitationId=invitation.InvitationId;
            this.Email = invitation.Email;
            this.Name = invitation.Name;
            this.ExpirationDate = invitation.ExpirationDate;
            this.Status = invitation.Status;
        }

        public override bool Equals(object obj)
        {
            return obj is CreateInvitationResponse response &&
                   InvitationId == response.InvitationId &&
                   Email == response.Email &&
                   Name == response.Name &&
                   ExpirationDate == response.ExpirationDate &&
                   Status == response.Status;
        }
    }
}
