﻿using CustomExceptions;
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
        private readonly IManagerRepository _managerRepository;

        public BuildingLogic(IBuildingRepository buildingRepository, IConstructionCompanyAdminRepository constructionCompanyAdminRepository, IManagerRepository managerRepository)
        {
            _buildingRepository = buildingRepository;
            _constructionCompanyAdminRepository = constructionCompanyAdminRepository;
            _managerRepository = managerRepository;
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

            building.ConstructionCompanyId = constructionCompanyAdmin.ConstructionCompany.ConstructionCompanyId;
            building.ConstructionCompany = constructionCompanyAdmin.ConstructionCompany;

            building.ManagerId = null;
            building.Manager = null;

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

            if (building.Manager is null || string.IsNullOrWhiteSpace(building.Manager.Email))
            {
                // Ahora no solo es un manager quien puede modificar un edificio, sino que también un administrador de la empresa constructora
                var existingBuilding = _buildingRepository.GetBuilding(parsedManagerId, building.BuildingId);
                if (existingBuilding == null)
                    throw new InvalidOperationException("Bui" +
                                                        "lding not found or manager does not have permission to update this building.");

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
            else
            {
                var existingBuilding = _buildingRepository.GetBuildingByAdmin(parsedManagerId, building.BuildingId);

                var manager = _managerRepository.GetManagerByEmail(building.Manager.Email);
                if (manager == null)
                    throw new InvalidOperationException("Manager not found.");
                existingBuilding.Manager = manager;
                
                _buildingRepository.UpdateBuilding(existingBuilding);
                return existingBuilding;
            }
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
