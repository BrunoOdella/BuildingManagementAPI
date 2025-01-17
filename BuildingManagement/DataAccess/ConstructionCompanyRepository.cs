﻿using Domain;
using IDataAccess;

namespace DataAccess
{
    public class ConstructionCompanyRepository : IConstructionCompanyRepository
    {
        private readonly BuildingManagementDbContext _context;

        public ConstructionCompanyRepository(BuildingManagementDbContext context)
        {
            _context = context;
        }

        public ConstructionCompany CreateConstructionCompany(ConstructionCompany constructionCompany)
        {
            _context.ConstructionCompanies.Add(constructionCompany);
            _context.SaveChanges();
            return constructionCompany;
        }

        public bool NameExists(string name)
        {
            return _context.ConstructionCompanies.Any(cc => cc.Name == name);
        }

        public bool AdminHasCompany(Guid constructionCompanyAdminId)
        {
            return _context.ConstructionCompanies.Any(cc => cc.ConstructionCompanyAdminId == constructionCompanyAdminId);
        }

        public ConstructionCompany UpdateConstructionCompanyName(ConstructionCompany company, string actualName)
        {
            var constructionCompany = _context.ConstructionCompanies.FirstOrDefault(cc => cc.Name == actualName);
            constructionCompany.Name = company.Name;
            _context.SaveChanges();
            return constructionCompany;
        }

        public ConstructionCompanyAdmin GetConstructionCompanyAdminById(string adminId)
        {
            return _context.ConstructionCompanyAdmins.Find(new Guid(adminId));
        }
        public void UpdateConstructionCompany(ConstructionCompany constructionCompany)
        {
            var existingCompany = _context.ConstructionCompanies.Local.FirstOrDefault(cc => cc.ConstructionCompanyId == constructionCompany.ConstructionCompanyId);
            if (existingCompany != null)
            {
                // Actualiza solo los campos que vienen en el objeto updatedCompany
                _context.Entry(existingCompany).CurrentValues.SetValues(constructionCompany);
                _context.SaveChanges();
            }
        }

        public ConstructionCompany GetCompanyByAdminId(Guid adminId)
        {
            return _context.ConstructionCompanies.FirstOrDefault(cc => cc.ConstructionCompanyAdminId == adminId);
        }
    }
}
