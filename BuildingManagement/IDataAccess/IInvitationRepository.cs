using Domain;

namespace IDataAccess
{
    public interface IInvitationRepository
    {
        Invitation CreateInvitation(Invitation invitation);
        bool DeleteInvitation(Guid invitationId);
        IEnumerable<Invitation> GetAllInvitations();
        Invitation GetInvitationById(Guid invitationId);
        void UpdateInvitation(Invitation updatedInvitation);
    }
}
