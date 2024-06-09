using Domain;

namespace LogicInterface.Interfaces
{
    public interface IInvitationLogic
    {
        Invitation AcceptInvitation(Guid invitationId, string email, string password);
        Invitation CreateInvitation(Invitation invitation);
        bool DeleteInvitation(Guid invitationId);
        IEnumerable<Invitation> GetAllInvitations();
    }
}
