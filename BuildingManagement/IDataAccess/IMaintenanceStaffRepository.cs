using Domain;

namespace IDataAccess;

public interface IMaintenanceStaffRepository
{
    MaintenanceStaff AddMaintenanceStaff(MaintenanceStaff maintenanceStaff);
    bool EmailExistsInMaintenanceStaff(string email);
    IEnumerable<MaintenanceStaff> GetAll(Guid managerId);
    MaintenanceStaff GetMaintenanceStaff(Guid managerId, Guid maintenancePersonId);
    Guid GetMaintenanceStaff(Guid maintenancePersonId);
    //void Update(MaintenanceStaff actualMaintenanceStaff);
    MaintenanceStaff GetByEmailAndPassword(string email, string password);
    IEnumerable<MaintenanceStaff> GetAllMaintenanceStaff();
    MaintenanceStaff Get(Guid maintenancePersonId);
}