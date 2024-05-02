using Domain;
using IDataAccess;
using LogicInterface.Interfaces;

namespace BusinessLogic.Logics
{
    public class InvitationLogic : IInvitationLogic
    {

        private IInvitationRepository _invitationRepository;
        private IManagerRepository _managerRepository;

        public InvitationLogic(IInvitationRepository invitationRepository, IManagerRepository managerRepository)
        {
            _invitationRepository = invitationRepository;
            _managerRepository = managerRepository;
        }


        public Invitation AcceptInvitation(Guid invitationId, string password)
        {
            var invitation = _invitationRepository.GetInvitationById(invitationId);
            if (invitation == null)
                throw new InvalidOperationException("Invitation does not exist.");

            if (invitation.Status == "Aceptada")
                throw new InvalidOperationException("Invitation has already been accepted.");

            if (invitation.ExpirationDate < DateTime.UtcNow)
                throw new InvalidOperationException("Invitation has expired.");

            invitation.Status = "Aceptada";
            _invitationRepository.UpdateInvitation(invitation);

            var manager = new Manager { Email = invitation.Email, Password = password };
            _managerRepository.CreateManager(manager);

            return invitation;
        }

        public Invitation CreateInvitation(Invitation invitation)
        {
            invitation.Status = "No aceptada";
            return _invitationRepository.CreateInvitation(invitation);
        }

        public bool DeleteInvitation(Guid invitationId)
        {
            var invitation = _invitationRepository.GetInvitationById(invitationId);
            if (invitation == null)
            {
                return false; 
            }

            if (invitation.Status == "Aceptada")
            {
                throw new InvalidOperationException("No se puede eliminar una invitación aceptada.");
            }

            _invitationRepository.DeleteInvitation(invitationId);
            return true;
        }



        public IEnumerable<Invitation> GetAllInvitations()
        {
            return _invitationRepository.GetAllInvitations();
        }
    }
}
