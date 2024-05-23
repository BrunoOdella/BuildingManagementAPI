using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
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

        public ConstructionCompanyAdmin GetConstructionCompanyAdminById(Guid id)
        {
            return _context.ConstructionCompanyAdmins
                .Include(admin => admin.ConstructionCompany)
                .FirstOrDefault(admin => admin.Id == id);
        }
    }
}
