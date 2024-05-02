using Domain;

namespace IDataAccess
{
    public interface IAdminRepository
    {
        Admin CreateAdmin(Admin admin);
        Guid Get(Guid adminID);
    }
}
