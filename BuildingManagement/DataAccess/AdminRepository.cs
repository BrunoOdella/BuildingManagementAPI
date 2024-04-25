using Domain;
using IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AdminRepository : IAdminRepository
    {
        private readonly BuildingManagementDbContext _context;

        public AdminRepository(BuildingManagementDbContext context)
        {
            _context = context;
        }

        public Admin CreateAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            return admin;
        }
    }

}
