using Domain;

namespace IDataAccess
{
    public interface IConstructionCompanyAdminRepository
    {
        void CreateConstructionCompanyAdmin(ConstructionCompanyAdmin constructionCompanyAdmin);
        bool EmailExistsInConstructionCompanyAdmins(string email);
        ConstructionCompanyAdmin GetConstructionCompanyAdminById(Guid id);
        Guid Get(Guid Id);
        ConstructionCompanyAdmin GetByEmailAndPassword(string email, string password);
    }
}
