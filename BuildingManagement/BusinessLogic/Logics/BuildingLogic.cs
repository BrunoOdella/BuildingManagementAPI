using Domain;
using IDataAccess;
using LogicInterface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logics
{
    public class BuildingLogic : IBuildingLogic
    {
        private IBuildingRepository _buildingRepository;

        public BuildingLogic(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        public Building CreateBuilding(string managerId, Building building)
        {
            if (!Guid.TryParse(managerId, out Guid parsedManagerId))
            {
                throw new ArgumentException("Invalid manager ID");
            }
            building.ManagerId = parsedManagerId;


            return _buildingRepository.CreateBuilding(building);
        }


        public void DeleteBuilding(string managerId, Guid buildingId)
        {
            if (!Guid.TryParse(managerId, out Guid managerGuid))
            {
                throw new ArgumentException("Invalid manager ID");
            }

            bool result = _buildingRepository.DeleteBuilding(buildingId);
            if (!result)
            {
                throw new InvalidOperationException("Building not found or not permitted to delete.");
            }
        }

        public Building UpdateBuilding(string managerId, Building building)
        {
            throw new NotImplementedException();
        }
    }


}
