using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataAccess
{
    using Domain;

    public interface IManagerRepository
    {
        void CreateManager(Manager manager);
        Manager GetManagerByEmail(string email);
        void UpdateManager(Manager manager);
        Guid Get(Guid managerID);
    }

}
