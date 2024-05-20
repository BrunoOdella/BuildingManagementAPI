using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class MaintenanceStaffRepository : IMaintenanceStaffRepository
    {
        private readonly BuildingManagementDbContext _context;

        public MaintenanceStaffRepository(BuildingManagementDbContext context)
        {
            _context = context;
        }

        public MaintenanceStaff AddMaintenanceStaff(MaintenanceStaff maintenanceStaff)
        {
            _context.MaintenanceStaff.Add(maintenanceStaff);
            _context.SaveChanges();
            return maintenanceStaff;
        }

        public IEnumerable<MaintenanceStaff> GetAll(Guid managerId)
        {
            List<Building> buildings = _context.Buildings
                .Include(b => b.MaintenanceStaff)
                    .ThenInclude(s => s.Requests)
                .Where(b => b.ManagerId == managerId)
                .ToList();
            List<MaintenanceStaff> maintenanceStaff = new List<MaintenanceStaff>();

            foreach (Building building in buildings)
            {
                maintenanceStaff.AddRange(building.MaintenanceStaff);
            }
            return maintenanceStaff;
        }

        public MaintenanceStaff GetMaintenanceStaff(Guid managerId, Guid maintenancePersonId)
        {
            List<Building> buildings = _context.Buildings
                .Include(b => b.MaintenanceStaff)
                    .ThenInclude(s => s.Requests)
                .Where(b => b.ManagerId == managerId)
                .ToList();

            MaintenanceStaff maintenanceStaff = _context.MaintenanceStaff
                .FirstOrDefault(s => s.ID == maintenancePersonId);

            if (maintenanceStaff == null)
                return null;

            foreach (Building building in buildings)
            {
                if (maintenanceStaff.BuildingId == building.BuildingId)
                    return maintenanceStaff;
            }
            return null;
        }

        public Guid GetMaintenanceStaff(Guid maintenancePersonId)
        {
            var maintenanceStaff = _context.MaintenanceStaff
                .FirstOrDefault(s => s.ID == maintenancePersonId);

            return maintenanceStaff?.ID ?? Guid.Empty;
        }

        public bool EmailExistsInMaintenanceStaff(string email)
        {
            return _context.MaintenanceStaff.Any(ms => ms.Email == email);
        }
    }
}
