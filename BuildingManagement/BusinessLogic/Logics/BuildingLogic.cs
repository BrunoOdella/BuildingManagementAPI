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
            if (!Guid.TryParse(managerId, out Guid parsedManagerId))
                throw new ArgumentException("Invalid manager ID format.");

            var existingBuilding = _buildingRepository.GetBuildingById(building.BuildingId);
            if (existingBuilding == null)
                throw new InvalidOperationException("Building not found.");

            if (existingBuilding.ManagerId != parsedManagerId)
                throw new UnauthorizedAccessException("Manager does not have permission to update this building.");

            // Ejemplo de verificación de nulidad antes de la asignación
            if (building.Name != null)
                existingBuilding.Name = building.Name;
            if (building.Address != null)
                existingBuilding.Address = building.Address;
            if (building.Location != null)
            {
                existingBuilding.Location.Latitude = building.Location.Latitude;
                existingBuilding.Location.Longitude = building.Location.Longitude;
            }
            existingBuilding.ConstructionCompany = building.ConstructionCompany;
            existingBuilding.CommonExpenses = building.CommonExpenses;

            _buildingRepository.UpdateBuilding(existingBuilding);
            return existingBuilding;
        }


    }


}
