using Domain;

namespace IDataAccess
{
    public interface IInvitationRepository
    {
        Invitation CreateInvitation(Invitation invitation);
        bool DeleteInvitation(int invitationId);
        IEnumerable<Invitation> GetAllInvitations();
    }
}
