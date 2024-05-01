using Domain;
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

        public Apartment GetApartment(Guid managerId, Guid apartmentId, Guid buildingId)
        {
            var building = _context.Buildings.FirstOrDefault(i => i.BuildingId.Equals(apartmentId) && i.ManagerId.Equals(managerId));
            var apartment = building.Apartments.FirstOrDefault(a => a.ApartmentId.Equals(apartmentId));
            return apartment;
        }

        public Building GetBuilding(Guid managerId, Guid buildingId)
        {
            return _context.Buildings.FirstOrDefault(i => i.BuildingId.Equals(buildingId) && i.ManagerId.Equals(managerId));
        }
    }
}
