using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterface.Interfaces
{
    public interface IInvitationLogic
    {
        Invitation AcceptInvitation(Guid invitationId, string password);
        Invitation CreateInvitation(Invitation invitation);
        bool DeleteInvitation(Guid invitationId);
        IEnumerable<Invitation> GetAllInvitations();
    }
}
