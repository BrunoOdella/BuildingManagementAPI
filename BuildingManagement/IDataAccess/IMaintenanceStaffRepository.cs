using Domain;

namespace IDataAccess;

public interface IMaintenanceStaffRepository
{
    MaintenanceStaff GetMaintenanceStaff(Guid managerId, Guid maintenancePersonId);
    void Update(MaintenanceStaff actualMaintenanceStaff);
}