using Domain;
using IDataAccess;
using LogicInterface.Interfaces;


namespace BusinessLogic.Logics
{
    public class AdminLogic : IAdminLogic
    {
        private readonly IAdminRepository _adminRepository; 

        public AdminLogic(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public Admin CreateAdmin(Admin admin)
        {
            return _adminRepository.CreateAdmin(admin);
        }
    }
}
