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
            var staff = _context.MaintenanceStaff.Where(s => s.BuildingId.Equals(buildong.BuildingId)).ToList();
            maintenanceStaff.AddRange(staff);
        }
        return  maintenanceStaff;
    }

    public MaintenanceStaff GetMaintenanceStaff(Guid managerId, Guid maintenancePersonId)
    {
        var buildings = _context.Buildings.Where(b => b.ManagerId.Equals(managerId)).ToList();

        var maintenanceStaff = _context.MaintenanceStaff.FirstOrDefault(s => s.ID.Equals(maintenancePersonId));

        if (maintenanceStaff == null)
            return null;

        foreach (var buildong in buildings)
        {
            if (maintenanceStaff.BuildingId.Equals(buildong.BuildingId))
                return maintenanceStaff;
        }
        return null;
    }

    public Guid GetMaintenanceStaff(Guid maintenancePersonId)
    {
        var maintenanceStaff = _context.MaintenanceStaff.FirstOrDefault(s => s.ID.Equals(maintenancePersonId));
        return maintenanceStaff.ID;
    }
    public bool EmailExistsInMaintenanceStaff(string email)
    {
        return _context.MaintenanceStaff.Any(ms => ms.Email == email);
    }
    /*
    public void Update(MaintenanceStaff actualMaintenanceStaff)
    {
        throw new NotImplementedException();
    }
    */
}