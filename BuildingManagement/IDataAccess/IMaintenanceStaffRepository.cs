using Domain;

namespace IDataAccess;

public interface IMaintenanceStaffRepository
{
    MaintenanceStaff AddMaintenanceStaff(MaintenanceStaff maintenanceStaff);
    IEnumerable<MaintenanceStaff> GetAll(Guid managerId);
    MaintenanceStaff GetMaintenanceStaff(Guid managerId, Guid maintenancePersonId);
    void Update(MaintenanceStaff actualMaintenanceStaff);
}