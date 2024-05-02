using Domain;
using IDataAccess;
using LogicInterface.Interfaces;

namespace BusinessLogic.Logics
{
    public class MaintenanceStaffLogic : IMaintenanceStaffLogic
    {

        private IMaintenanceStaffRepository _maintenanceStaffRepository;
        private IBuildingRepository _buildingRepository;

        public MaintenanceStaffLogic(IMaintenanceStaffRepository maintenanceStaffRepository, IBuildingRepository buildingRepository)
        {
            _maintenanceStaffRepository = maintenanceStaffRepository;
            _buildingRepository = buildingRepository;
        }


        public MaintenanceStaff AddMaintenanceStaff(string managerId, MaintenanceStaff maintenanceStaff)
        {
            if (!Guid.TryParse(managerId, out Guid managerGuid))
            {
                throw new ArgumentException("Invalid manager ID.");
            }

            // Verificar que el edificio existe y está asociado al manager.
            var building = _buildingRepository.GetBuilding(Guid.Parse(managerId),maintenanceStaff.BuildingId);
            if (building == null)
            {
                throw new InvalidOperationException("Building not found.");
            }
            if (building.ManagerId != managerGuid)
            {
                throw new UnauthorizedAccessException("Manager does not have permission to add staff to this building.");
            }

            if (string.IsNullOrWhiteSpace(maintenanceStaff.Name) || string.IsNullOrWhiteSpace(maintenanceStaff.LastName))
            {
                throw new ArgumentException("Name and last name are required.");
            }

            // Si pasa todas las verificaciones, añadir el personal de mantenimiento
            return _maintenanceStaffRepository.AddMaintenanceStaff(maintenanceStaff);
        }

    }
}
