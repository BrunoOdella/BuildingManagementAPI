using Domain;

namespace IDataAccess
{
    public interface IInvitationRepository
    {
        Invitation CreateInvitation(Invitation invitation);
        IEnumerable<Invitation> GetAllInvitations();
    }
}
