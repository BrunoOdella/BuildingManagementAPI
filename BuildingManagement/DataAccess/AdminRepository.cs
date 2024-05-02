﻿using Domain;
using IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AdminRepository : IAdminRepository
    {
        private readonly BuildingManagementDbContext _context;

        public AdminRepository(BuildingManagementDbContext context)
        {
            _context = context;
        }

        public Admin CreateAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
            return admin;
        }

        public Guid Get(Guid adminID)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.AdminID.Equals(adminID));
            if(admin == null)
                return Guid.Empty;
            return admin.AdminID;
        }
    }

}
