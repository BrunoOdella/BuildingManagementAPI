using Domain;
using IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ConstructionCompanyAdminRepository : IConstructionCompanyAdminRepository
    {
        private readonly BuildingManagementDbContext _context;

        public ConstructionCompanyAdminRepository(BuildingManagementDbContext context)
        {
            _context = context;
        }

        public void CreateConstructionCompanyAdmin(ConstructionCompanyAdmin constructionCompanyAdmin)
        {
            _context.ConstructionCompanyAdmins.Add(constructionCompanyAdmin);
            _context.SaveChanges();
        }

        public bool EmailExistsInConstructionCompanyAdmins(string email)
        {
            return _context.ConstructionCompanyAdmins.Any(admin => admin.Email == email);
        }
    }
}
