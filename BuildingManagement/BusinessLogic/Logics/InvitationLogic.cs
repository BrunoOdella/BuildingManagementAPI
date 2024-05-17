using CustomExceptions.InvitationExceptions;
using CustomExceptions;
using Domain;
using IDataAccess;
using LogicInterface.Interfaces;

namespace BusinessLogic.Logics
{
    public class InvitationLogic : IInvitationLogic
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IMaintenanceStaffRepository _maintenanceStaffRepository;

        public InvitationLogic(
            IInvitationRepository invitationRepository,
            IManagerRepository managerRepository,
            IAdminRepository adminRepository,
            IMaintenanceStaffRepository maintenanceStaffRepository)
        {
            _invitationRepository = invitationRepository;
            _managerRepository = managerRepository;
            _adminRepository = adminRepository;
            _maintenanceStaffRepository = maintenanceStaffRepository;
        }

        public Invitation AcceptInvitation(Guid invitationId, string password)
        {
            var invitation = _invitationRepository.GetInvitationById(invitationId);
            if (invitation == null)
                throw new InvitationNotFoundException();

            if (invitation.Status == "Aceptada")
                throw new InvitationAlreadyAcceptedException();

            if (invitation.ExpirationDate < DateTime.UtcNow)
                throw new InvitationExpiredException();

            invitation.Status = "Aceptada";
            _invitationRepository.UpdateInvitation(invitation);

            var manager = new Manager { Email = invitation.Email, Password = password };
            _managerRepository.CreateManager(manager);

            return invitation;
        }

        public Invitation CreateInvitation(Invitation invitation)
        {
            if (EmailExistsInSystem(invitation.Email))
                throw new EmailAlreadyExistsException();

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
                throw new CannotDeleteAcceptedInvitationException();
            }

            _invitationRepository.DeleteInvitation(invitationId);
            return true;
        }

        public IEnumerable<Invitation> GetAllInvitations()
        {
            return _invitationRepository.GetAllInvitations();
        }

        private bool EmailExistsInSystem(string email)
        {
            return _adminRepository.EmailExistsInAdmins(email) ||
                   _managerRepository.EmailExistsInManagers(email) ||
                   _maintenanceStaffRepository.EmailExistsInMaintenanceStaff(email) ||
                   _invitationRepository.EmailExistsInInvitations(email);
        }
    }
}
