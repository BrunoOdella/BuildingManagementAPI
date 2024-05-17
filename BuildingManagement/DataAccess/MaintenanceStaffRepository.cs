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
            var buildings = _context.Buildings.Where(b => b.ManagerId == managerId).ToList();
            var maintenanceStaff = new List<MaintenanceStaff>();

            foreach (var building in buildings)
            {
                var staff = _context.MaintenanceStaff.Where(s => s.BuildingId == building.BuildingId).ToList();
                maintenanceStaff.AddRange(staff);
            }
            return maintenanceStaff;
        }

        public MaintenanceStaff GetMaintenanceStaff(Guid managerId, Guid maintenancePersonId)
        {
            var buildings = _context.Buildings.Where(b => b.ManagerId == managerId).ToList();
            var maintenanceStaff = _context.MaintenanceStaff.FirstOrDefault(s => s.ID == maintenancePersonId);

            if (maintenanceStaff == null)
                return null;

            foreach (var building in buildings)
            {
                if (maintenanceStaff.BuildingId == building.BuildingId)
                    return maintenanceStaff;
            }
            return null;
        }

        public Guid GetMaintenanceStaff(Guid maintenancePersonId)
        {
            var maintenanceStaff = _context.MaintenanceStaff.FirstOrDefault(s => s.ID == maintenancePersonId);
            return maintenanceStaff?.ID ?? Guid.Empty;
        }

        public bool EmailExistsInMaintenanceStaff(string email)
        {
            return _context.MaintenanceStaff.Any(ms => ms.Email == email);
        }
    }
}
