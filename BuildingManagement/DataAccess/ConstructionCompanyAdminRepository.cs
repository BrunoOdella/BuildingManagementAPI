﻿using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

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

        public Guid Get(Guid ID)
        {
            ConstructionCompanyAdmin admin = _context.ConstructionCompanyAdmins
                .FirstOrDefault(m => m.Id.Equals(ID));
            if (admin == null)
            {
                return Guid.Empty;
            }
            return admin.Id;
        }
        public ConstructionCompanyAdmin GetByEmailAndPassword(string email, string password)
        {
            return _context.ConstructionCompanyAdmins.FirstOrDefault(a => a.Email == email && a.Password == password);
        }
    }
}
