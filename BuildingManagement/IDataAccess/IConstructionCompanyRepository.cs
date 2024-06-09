using Domain;

namespace IDataAccess
{
    public interface IConstructionCompanyRepository
    {
        ConstructionCompany CreateConstructionCompany(ConstructionCompany constructionCompany);
        bool NameExists(string name);
        public bool AdminHasCompany(Guid constructionCompanyAdminId);
        ConstructionCompany UpdateConstructionCompanyName(ConstructionCompany company, string actualName);
        ConstructionCompanyAdmin GetConstructionCompanyAdminById(string constructionCompanyAdminId);
        ConstructionCompany GetCompanyByAdminId(Guid adminId);
        void UpdateConstructionCompany(ConstructionCompany constructionCompany);

    }
}
