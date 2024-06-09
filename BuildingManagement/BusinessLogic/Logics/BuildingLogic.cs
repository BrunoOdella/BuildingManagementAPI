using CustomExceptions;
using Domain;
using IDataAccess;
using LogicInterface.Interfaces;

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

            // Verificar si tiene un manager asignado
            if (building.Manager != null)
            {
                if (!string.IsNullOrWhiteSpace(building.Manager.Email))
                {
                    Manager manager = _managerRepository.GetManagerByEmail(building.Manager.Email);
                    if (manager == null)
                    {
                        _managerRepository.CreateManager(building.Manager);
                        manager = _managerRepository.GetManagerByEmail(building.Manager.Email);
                    }
                    building.Manager = manager;
                    building.ManagerId = manager.ManagerId;
                }
            }

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
            if (building == null)
            {
                throw new UnauthorizedAccessException("Building not found or manager not authorized to delete this building.");
            }

            bool result = _buildingRepository.DeleteBuilding(buildingId);
            if (!result)
            {
                throw new InvalidOperationException("Failed to delete building.");
            }
        }

        public Building UpdateBuilding(string guid, Building building, Guid buildingId)
        {
            Guid Id;

            if (!Guid.TryParse(guid, out Id))
                throw new ArgumentException("Invalid ID");

            if (building == null)
                throw new ArgumentNullException("Building is null");

            if (building.BuildingId != buildingId)
                throw new ArgumentException("Building ID does not match");

            Building existingBuilding = _buildingRepository.GetBuilding(buildingId);

            if (existingBuilding == null)
                throw new ArgumentException("Building not found");

            if (_managerRepository.Get(Id) != Guid.Empty)
            {
                existingBuilding.Name = building.Name ?? existingBuilding.Name;
                existingBuilding.Address = building.Address ?? existingBuilding.Address;
                existingBuilding.Location = building.Location ?? existingBuilding.Location;
                if (building.CommonExpenses > 0)
                    existingBuilding.CommonExpenses = building.CommonExpenses;
            }
            else if (_constructionCompanyAdminRepository.Get(Id) != Guid.Empty)
            {
                var adminId = Id;
                if (existingBuilding.ConstructionCompany.ConstructionCompanyAdmin.Id != adminId)
                    throw new UnauthorizedAccessException("Construction Company Admin is not authorized to update this building");

                if (building.ManagerId != null)
                {
                    Manager manager = _managerRepository.GetManagerById(building.ManagerId.Value);
                    if (manager == null)
                        throw new ArgumentException("Invalid Manager ID");

                    existingBuilding.ManagerId = manager.ManagerId;
                    existingBuilding.Manager = manager;
                }
            }
            else
            {
                throw new ArgumentException("Invalid ID");
            }

            return _buildingRepository.UpdateBuilding(existingBuilding);
        }



        public IEnumerable<Building> GetBuildings(string personId)
        {
            if (!Guid.TryParse(personId, out Guid parsedAdminId))
            {
                throw new ArgumentException("Invalid admin ID");
            }
            parsedAdminId = Guid.Parse(personId);
            if (_constructionCompanyAdminRepository.Get(parsedAdminId) != Guid.Empty)
            {
                return _buildingRepository.GetBuildingsByConstructionCompanyAdminId(parsedAdminId);
            }
            if (_managerRepository.Get(parsedAdminId) != Guid.Empty)
            {
                IEnumerable<Building> building = _buildingRepository.GetBuildingsByManagerId(parsedAdminId);
                return building;
            }
            throw new ArgumentException("Not buildings found");
        }

        public IEnumerable<Apartment> GetApartments(string managerId, Guid buildingId)
        {
            if (!Guid.TryParse(managerId, out Guid parsedAdminId))
            {
                throw new ArgumentException("Invalid manager ID");
            }

            var apartments = _buildingRepository.GetAllApartments(parsedAdminId, buildingId);

            if (apartments == null)
            {
                throw new ArgumentException("Not apartments found");
            }

            return apartments;
        }

    }


}
