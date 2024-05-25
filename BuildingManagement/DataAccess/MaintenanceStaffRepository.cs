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
            return _context.MaintenanceStaff
                .Include(s => s.Requests)
                .ToList();
        }

        public MaintenanceStaff GetMaintenanceStaff(Guid managerId, Guid maintenancePersonId)
        {
            return _context.MaintenanceStaff
                .Include(s => s.Requests)
                .FirstOrDefault(s => s.ID == maintenancePersonId);
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
