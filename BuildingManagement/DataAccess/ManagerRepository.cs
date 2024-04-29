using Domain;
using IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return _context.Managers.FirstOrDefault(m => m.Email == email);
        }

        public void UpdateManager(Manager manager)
        {
            _context.Managers.Update(manager);
            _context.SaveChanges();
        }
    }
}
