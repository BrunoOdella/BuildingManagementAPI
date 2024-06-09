using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly BuildingManagementDbContext _context;

        public ManagerRepository(BuildingManagementDbContext context)
        {
            _context = context;
        }

        public void CreateManager(Manager manager)
        {
            _context.Managers.Add(manager);
            _context.SaveChanges();
        }

        public Manager GetManagerByEmail(string email)
        {
            return _context.Managers
                .Include(m => m.Buildings)
                .FirstOrDefault(m => m.Email == email);
        }

        public void UpdateManager(Manager manager)
        {
            _context.Managers.Update(manager);
            _context.SaveChanges();
        }

        public Guid Get(Guid managerID)
        {
            Manager manager = _context.Managers
                .FirstOrDefault(m => m.ManagerId.Equals(managerID));
            if (manager == null)
            {
                return Guid.Empty;
            }
            return manager.ManagerId;
        }

        public bool EmailExistsInManagers(string email)
        {
            return _context.Managers.Any(m => m.Email == email);
        }

        public Manager GetByEmailAndPassword(string email, string password)
        {
            return _context.Managers.FirstOrDefault(a => a.Email == email && a.Password == password);
        }

        public IEnumerable<Manager> GetAll()
        {
            return _context.Managers.ToList();
        }

        public Manager GetManagerById(Guid managerID)
        {
            return _context.Managers
                .Include(m => m.Buildings)
                .FirstOrDefault(m => m.ManagerId.Equals(managerID));
        }
    }
}
