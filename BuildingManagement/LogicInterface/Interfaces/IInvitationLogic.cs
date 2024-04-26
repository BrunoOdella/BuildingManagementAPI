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
        Invitation CreateInvitation(Invitation invitation);
        IEnumerable<Invitation> GetAllInvitations();
    }
}
