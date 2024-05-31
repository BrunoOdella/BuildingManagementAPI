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
        void DeleteBuilding(string? managerId, Guid buildingId);
        Building UpdateBuilding(string managerId, Building building);
        IEnumerable<Building> GetBuildings(string adminId);
        IEnumerable<Apartment> GetApartments(string managerId, Guid buildingId);
    }
}
