using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterface.Interfaces
{
    public interface IBuildingLogic
    {
        Building CreateBuilding(string managerId, Building building);
    }
}
