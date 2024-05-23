using CustomExceptions;
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
        private readonly IBuildingRepository _buildingRepository;
        private readonly IConstructionCompanyAdminRepository _constructionCompanyAdminRepository;

        public BuildingLogic(IBuildingRepository buildingRepository, IConstructionCompanyAdminRepository constructionCompanyAdminRepository)
        {
            _buildingRepository = buildingRepository;
            _constructionCompanyAdminRepository = constructionCompanyAdminRepository;
        }

        public Building CreateBuilding(string constructionCompanyAdminId, Building building)
        {
            if (!Guid.TryParse(constructionCompanyAdminId, out Guid parsedConstructionCompanyAdminId))
            {
                throw new ArgumentException("Invalid construction company admin ID");
            }

            ConstructionCompanyAdmin constructionCompanyAdmin = _constructionCompanyAdminRepository.GetConstructionCompanyAdminById(parsedConstructionCompanyAdminId);
            if (constructionCompanyAdmin == null)
            {
                throw new UnauthorizedAccessException("Construction company admin not found.");
            }

            if (constructionCompanyAdmin.ConstructionCompany == null)
            {
                throw new InvalidOperationException("Construction company admin does not have an associated construction company.");
            }

            // Asignar la empresa constructora y el administrador al edificio
            building.ConstructionCompanyAdminId = parsedConstructionCompanyAdminId;
            building.ConstructionCompanyAdmin = constructionCompanyAdmin;
            building.ConstructionCompany = constructionCompanyAdmin.ConstructionCompany;

            // Verificar si ya existe un edificio con la misma ubicación
            Building existingBuilding = _buildingRepository.GetBuildingByLocation(building.Location.Latitude, building.Location.Longitude);
            if (existingBuilding != null)
            {
                throw new LocationAlreadyExistsException();
            }

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

        public IEnumerable<Building> GetBuildingsByConstructionCompanyAdminId(string adminId)
        {
            if (!Guid.TryParse(adminId, out Guid parsedAdminId))
            {
                throw new ArgumentException("Invalid admin ID");
            }

            return _buildingRepository.GetBuildingsByConstructionCompanyAdminId(parsedAdminId);
        }

    }


}
