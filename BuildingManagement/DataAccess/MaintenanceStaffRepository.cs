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

    public MaintenanceStaff AddMaintenanceStaff(MaintenanceStaff maintenanceStaff)
    {
        _context.MaintenanceStaff.Add(maintenanceStaff);
        _context.SaveChanges();
        return maintenanceStaff; // Retorna el objeto después de ser añadido a la base de datos.
    }

    public IEnumerable<MaintenanceStaff> GetAll(Guid managerId)
    {
        var buildings = _context.Buildings.Where(b => b.ManagerId.Equals(managerId)).ToList();
        List<MaintenanceStaff> maintenanceStaff = new List<MaintenanceStaff>();

        foreach (var buildong in buildings)
        {
            maintenanceStaff.AddRange(buildong.MaintenanceStaff);
        }
        return  maintenanceStaff;
    }

    public MaintenanceStaff GetMaintenanceStaff(Guid managerId, Guid maintenancePersonId)
    {
        var buildings = _context.Buildings.Where(b => b.ManagerId.Equals(managerId)).ToList();

        foreach (var buildong in buildings)
        {
            foreach (var staff in buildong.MaintenanceStaff)
            {
                if (staff.ID.Equals(maintenancePersonId))
                    return staff;
            }
        }
        return (MaintenanceStaff) null;
    }

    public Guid GetMaintenanceStaff(Guid maintenancePersonId)
    {
        var maintenanceStaff = _context.MaintenanceStaff.FirstOrDefault(s => s.ID.Equals(maintenancePersonId));
        return maintenanceStaff.ID;
    }

    /*
    public void Update(MaintenanceStaff actualMaintenanceStaff)
    {
        throw new NotImplementedException();
    }
    */
}