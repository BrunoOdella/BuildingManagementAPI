using Domain;
using IDataAccess;
using LogicInterface.Interfaces;

namespace BusinessLogic.Logics
{
    public class InvitationLogic : IInvitationLogic
    {
        private readonly IInvitationRepository _invitationRepository;
        public InvitationLogic(IInvitationRepository invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }

        public Invitation CreateInvitation(Invitation invitation)
        {
            return _invitationRepository.CreateInvitation(invitation);
        }

        public bool DeleteInvitation(Guid invitationId)
        {
            return _invitationRepository.DeleteInvitation(invitationId);
        }


        public IEnumerable<Invitation> GetAllInvitations()
        {
            return _invitationRepository.GetAllInvitations();
        }
    }
}
