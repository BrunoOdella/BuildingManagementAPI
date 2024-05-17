using CustomExceptions;
using Domain;
using IDataAccess;
using LogicInterface.Interfaces;


namespace BusinessLogic.Logics
{
    public class AdminLogic : IAdminLogic
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IMaintenanceStaffRepository _maintenanceStaffRepository;

        public AdminLogic(
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

        public Admin CreateAdmin(Admin admin)
        {
            if (EmailExistsInSystem(admin.Email))
            {
                throw new EmailAlreadyExistsException();
            }

            return _adminRepository.CreateAdmin(admin);
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
