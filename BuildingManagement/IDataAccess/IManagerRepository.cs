namespace IDataAccess
{
    using Domain;

    public interface IManagerRepository
    {
        void CreateManager(Manager manager);
        Manager GetManagerByEmail(string email);
        void UpdateManager(Manager manager);
        Guid Get(Guid managerID);
        Manager GetManagerById(Guid managerID);
        bool EmailExistsInManagers(string email);
        Manager GetByEmailAndPassword(string email, string password);
        IEnumerable<Manager> GetAll();
    }

}
