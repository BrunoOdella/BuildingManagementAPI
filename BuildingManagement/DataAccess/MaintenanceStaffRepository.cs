using Domain;
using IDataAccess;

namespace DataAccess;

public class MaintenanceStaffRepository : IMaintenanceStaffRepository
{
    private readonly BuildingManagementDbContext _context;

    public MaintenanceStaffRepository(BuildingManagementDbContext context)
    {
        _context = context;
    }

    public IEnumerable<MaintenanceStaff> GetAll(Guid managerId)
    {
        throw new NotImplementedException();
    }

    public MaintenanceStaff GetMaintenanceStaff(Guid managerId, Guid maintenancePersonId)
    {
        throw new NotImplementedException();
    }

    public void Update(MaintenanceStaff actualMaintenanceStaff)
    {
        throw new NotImplementedException();
    }
}