using Domain;
using IDataAccess;
using LogicInterface.Interfaces.IManagerLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logics
{
    public class ManagerLogic : IManagerLogic
    {
        IManagerRepository _managerRepository;
        public ManagerLogic(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }
        public IEnumerable<Manager> GetAll()
        {
            if (_managerRepository.GetAll() == null)
            {
                throw new ArgumentNullException("Not managers found");
            }
            return _managerRepository.GetAll();
        }
    }
}
