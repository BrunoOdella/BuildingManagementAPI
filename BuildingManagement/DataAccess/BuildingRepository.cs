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

        public IEnumerable<Building> GetAll(Guid managerId)
        {
            return _context.Buildings.Where(b => b.ManagerId.Equals(managerId));
        }

        public Apartment GetApartment(Guid managerId, Guid apartmentId, Guid buildingId)
        {
            var apartment = _context.Apartments.FirstOrDefault(a => a.ApartmentId.Equals(apartmentId));

            var building = _context.Buildings.FirstOrDefault(b => b.BuildingId.Equals(apartment.BuildingId));

            if(building.ManagerId.Equals(managerId))
                return apartment;
            return null;
        }

        public List<Apartment> getAllApartments(Guid managerId, Guid buildingId)
        {
            var building = _context.Buildings.FirstOrDefault(b => b.ManagerId.Equals(managerId));

            if (building is null)
                return null;

            var apartments = _context.Apartments.Where(a => a.BuildingId.Equals(building.BuildingId)).ToList();

            if(apartments is null)
                return null;

            return apartments;
        }

        public bool DeleteBuilding(Guid buildingId)
        {
            var building = _context.Buildings.Find(buildingId);
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
            return _context.Buildings.FirstOrDefault(i => i.BuildingId.Equals(buildingId) && i.ManagerId.Equals(managerId));
        }

        public Building UpdateBuilding(Building existingBuilding)
        {
            var entity = _context.Buildings.Attach(existingBuilding);
            entity.State = EntityState.Modified;
            _context.SaveChanges();
            return existingBuilding;
        }

    }
}
