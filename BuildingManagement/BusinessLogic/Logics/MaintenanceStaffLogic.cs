using CustomExceptions;
using Domain;
using IDataAccess;
using LogicInterface.Interfaces;

namespace BusinessLogic.Logics
{
    public class MaintenanceStaffLogic : IMaintenanceStaffLogic
    {
        private readonly IMaintenanceStaffRepository _maintenanceStaffRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IInvitationRepository _invitationRepository;

        public MaintenanceStaffLogic(
            IMaintenanceStaffRepository maintenanceStaffRepository,
            IBuildingRepository buildingRepository,
            IAdminRepository adminRepository,
            IManagerRepository managerRepository,
            IInvitationRepository invitationRepository)
        {
            _maintenanceStaffRepository = maintenanceStaffRepository;
            _buildingRepository = buildingRepository;
            _adminRepository = adminRepository;
            _managerRepository = managerRepository;
            _invitationRepository = invitationRepository;
        }

        public MaintenanceStaff AddMaintenanceStaff(string managerId, MaintenanceStaff maintenanceStaff)
        {
            if (!Guid.TryParse(managerId, out Guid managerGuid))
            {
                throw new ArgumentException("Invalid manager ID.");
            }
            // Verificar que el edificio existe y está asociado al manager.
            if (string.IsNullOrWhiteSpace(maintenanceStaff.Name) || string.IsNullOrWhiteSpace(maintenanceStaff.LastName))
            {
                throw new ArgumentException("Name and last name are required.");
            }

            if (EmailExistsInSystem(maintenanceStaff.Email))
            {
                throw new EmailAlreadyExistsException();
            }
            return _maintenanceStaffRepository.AddMaintenanceStaff(maintenanceStaff);
        }

        private bool EmailExistsInSystem(string email)
        {
            return _adminRepository.EmailExistsInAdmins(email) ||
                   _managerRepository.EmailExistsInManagers(email) ||
                   _maintenanceStaffRepository.EmailExistsInMaintenanceStaff(email) ||
                   _invitationRepository.EmailExistsInInvitations(email);
        }

        public IEnumerable<MaintenanceStaff> GetAllMaintenanceStaff(string managerId)
        {
            if (!Guid.TryParse(managerId, out Guid managerGuid))
            {
                throw new ArgumentException("Invalid manager ID.");
            }

            if (_managerRepository.Get(managerGuid) == Guid.Empty)
            {
                throw new ArgumentException("Manager not found.");
            }
            return _maintenanceStaffRepository.GetAllMaintenanceStaff();
        }
    }
}
