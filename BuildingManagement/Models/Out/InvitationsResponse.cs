using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Out
{
    public class InvitationsResponse
    {
        public IEnumerable<InvitationResponse> Invitations { get; set; }
        public InvitationsResponse(IEnumerable<Invitation> invitations)
        {
            this.Invitations = invitations.Select(invitation => new InvitationResponse(invitation)).ToList();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (InvitationsResponse)obj;
            return Invitations.SequenceEqual(other.Invitations);
        }
    }
}
