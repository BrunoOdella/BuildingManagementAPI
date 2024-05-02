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

            var building = _buildingRepository.GetBuilding(managerGuid, buildingId);
            if (building == null || building.ManagerId != managerGuid)
            {
                throw new UnauthorizedAccessException("Building not found or manager not authorized to delete this building.");
            }

            bool result = _buildingRepository.DeleteBuilding(buildingId);
            if (!result)
            {
                throw new InvalidOperationException("Failed to delete building.");
            }
        }


        public Building UpdateBuilding(string managerId, Building building)
        {
            if (!Guid.TryParse(managerId, out Guid parsedManagerId))
                throw new ArgumentException("Invalid manager ID format.");

            // Usar GetBuilding en lugar de GetBuildingById
            var existingBuilding = _buildingRepository.GetBuilding(parsedManagerId, building.BuildingId);
            if (existingBuilding == null)
                throw new InvalidOperationException("Building not found or manager does not have permission to update this building.");

            // No es necesario verificar el ManagerId aquí ya que GetBuilding debería manejarlo
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
