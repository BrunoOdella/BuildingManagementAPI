using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterface.Interfaces.IManagerLogic
{
    public interface IManagerLogic
    {
        IEnumerable<Manager> GetAll();
    }
}
