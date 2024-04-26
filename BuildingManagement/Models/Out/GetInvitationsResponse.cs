using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Out
{
    public class GetInvitationsResponse
    {
        public IEnumerable<CreateInvitationResponse> Invitations { get; set; }
        public GetInvitationsResponse(IEnumerable<Invitation> invitations)
        {
            this.Invitations = invitations.Select(invitation => new CreateInvitationResponse(invitation)).ToList();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (GetInvitationsResponse)obj;
            return Invitations.SequenceEqual(other.Invitations);
        }
    }
}
