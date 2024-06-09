using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly BuildingManagementDbContext _context;

        public BuildingRepository(BuildingManagementDbContext context)
        {
            _context = context;
        }

        public Building CreateBuilding(Building building)
        {
            _context.Buildings.Add(building);
            _context.SaveChanges();
            return building;
        }

        public IEnumerable<Building> GetAll(Guid managerId)
        {
            List<Building> buildings = _context.Buildings
                .Include(b => b.Apartments)
                    .ThenInclude(a => a.Requests)
                .Include(b => b.Location)
                .Where(b => b.ManagerId.Equals(managerId))
                .ToList();
            return buildings;
        }

        public Apartment GetApartment(Guid managerId, Guid apartmentId)
        {
            // Buscar el apartamento incluyendo el edificio y el manager
            var apartment = _context.Apartments
                .Include(a => a.Building)
                .FirstOrDefault(a => a.ApartmentId == apartmentId && a.Building.ManagerId == managerId);

            // Verificar que el apartamento pertenece a un edificio gestionado por el manager
            if (apartment == null || apartment.Building.ManagerId != managerId)
            {
                return null;
            }

            return apartment;
        }


        public List<Apartment> GetAllApartments(Guid managerId, Guid buildingId)
        {
            return _context.Apartments
                .Include(a => a.Building)
                .Include(a => a.Requests)
                .Include(a => a.Owner)
                .Where(a => a.Building.ManagerId == managerId && a.Building.BuildingId == buildingId)
                .ToList();
        }

        public bool DeleteBuilding(Guid buildingId)
        {
            Building building = _context.Buildings.Find(buildingId);
            if (building == null)
            {
                return false;
            }
            _context.Buildings.Remove(building);
            _context.SaveChanges();
            return true;
        }

        public Building GetBuilding(Guid managerId, Guid buildingId)
        {
            Building building = _context.Buildings
                .Include(b => b.Apartments)
                .Include(b => b.Location)
                .FirstOrDefault(i => i.BuildingId.Equals(buildingId) && i.ManagerId.Equals(managerId));
            return building;
        }

        public Building GetBuildingByAdmin(Guid adminId, Guid buildingId)
        {
            Building building = _context.Buildings
                .Include(b => b.Apartments)
                .Include(b => b.Location)
                .Include(b => b.ConstructionCompany)
                .FirstOrDefault(b => b.ConstructionCompany.ConstructionCompanyAdminId.Equals(adminId) && b.BuildingId.Equals(buildingId));
            return building;
        }


        public Building UpdateBuilding(Building existingBuilding)
        {
            _context.Entry(existingBuilding).State = EntityState.Modified;
            _context.SaveChanges();
            return existingBuilding;
        }


        public Building GetBuildingByLocation(double latitude, double longitude)
        {
            return _context.Buildings
                .Include(b => b.Location)
                .FirstOrDefault(b => b.Location.Latitude == latitude && b.Location.Longitude == longitude);
        }

        public IEnumerable<Building> GetBuildingsByConstructionCompanyAdminId(Guid constructionCompanyAdminId)
        {
            return _context.Buildings
                .Include(b => b.Manager)
                .Include(b => b.ConstructionCompany)
                .Where(b => b.ConstructionCompany.ConstructionCompanyAdminId == constructionCompanyAdminId)
                .ToList();
        }

        public IEnumerable<Building> GetBuildingsByManagerId(Guid managerId)
        {
            List<Building> buildings = _context.Buildings.Include(b => b.Apartments)
                .Include(b => b.Location)
                .Include(b => b.Manager)
                .Include(b => b.ConstructionCompany)
                .Where(b => b.ManagerId.Equals(managerId))
                .ToList();
            return buildings;
        }

        public Building GetBuilding(Guid buildingId)
        {
            return _context.Buildings
                .Include(b => b.Apartments)
                .Include(b => b.Location)
                .Include(b => b.Manager)
                .Include(b => b.ConstructionCompany)
                .FirstOrDefault(b => b.BuildingId == buildingId);
        }
    }
}
