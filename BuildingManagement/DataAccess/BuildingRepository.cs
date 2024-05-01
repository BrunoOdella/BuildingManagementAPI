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
