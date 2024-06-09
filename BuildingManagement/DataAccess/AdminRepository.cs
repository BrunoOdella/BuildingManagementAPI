using Domain;
using IDataAccess;

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
            _context.SaveChanges();
            return admin;
        }

        public Guid Get(Guid adminID)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.AdminID.Equals(adminID));
            if (admin == null)
                return Guid.Empty;
            return admin.AdminID;
        }
        public bool EmailExistsInAdmins(string email)
        {
            return _context.Admins.Any(a => a.Email == email);
        }

        public Admin GetByEmailAndPassword(string email, string password)
        {
            return _context.Admins.FirstOrDefault(a => a.Email == email && a.Password == password);
        }
    }

}
