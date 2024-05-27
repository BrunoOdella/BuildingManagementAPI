using Domain;

namespace IDataAccess
{
    public interface IAdminRepository
    {
        Admin CreateAdmin(Admin admin);
        Guid Get(Guid adminID);
        bool EmailExistsInAdmins(string email);
        Admin GetByEmailAndPassword(string email, string password);
    }
}
