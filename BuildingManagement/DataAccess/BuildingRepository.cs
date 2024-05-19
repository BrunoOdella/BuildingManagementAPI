using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
                .Include(b => b.MaintenanceStaff)
                .Include(b => b.Location)
                .Where(b => b.ManagerId.Equals(managerId))
                .ToList();
            return buildings;
        }

        public Apartment GetApartment(Guid managerId, Guid apartmentId, Guid buildingId)
        {
            Building building = _context.Buildings
                .Include(b => b.Apartments)
                .FirstOrDefault(b => b.BuildingId == buildingId && b.ManagerId == managerId);

            return building?.Apartments.FirstOrDefault(a => a.ApartmentId == apartmentId);
        }

        public List<Apartment> GetAllApartments(Guid managerId, Guid buildingId)
        {
            Building building = _context.Buildings
                .Include(b => b.Apartments)
                .FirstOrDefault(b => b.BuildingId == buildingId && b.ManagerId == managerId);

            return building?.Apartments.ToList();
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
                .Include(b => b.MaintenanceStaff)
                .Include(b => b.Location)
                .FirstOrDefault(i => i.BuildingId.Equals(buildingId) && i.ManagerId.Equals(managerId));
            return building;
        }

        public Building UpdateBuilding(Building existingBuilding)
        {
            _context.Entry(existingBuilding).State = EntityState.Modified;
            _context.SaveChanges();
            return existingBuilding;
        }
    }
}
