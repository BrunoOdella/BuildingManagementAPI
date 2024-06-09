using Domain;

namespace LogicInterface.Interfaces
{
    public interface IMaintenanceStaffLogic
    {
        MaintenanceStaff AddMaintenanceStaff(string managerId, MaintenanceStaff maintenanceStaff);
        IEnumerable<MaintenanceStaff> GetAllMaintenanceStaff(string managerId);
    }
}
