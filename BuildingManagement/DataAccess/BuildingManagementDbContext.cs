using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BuildingManagementDbContext : DbContext
    {
        public BuildingManagementDbContext(DbContextOptions<BuildingManagementDbContext> options)
    : base(options)
        { }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Invitation> Invitations { get; set; }

        public DbSet<Request_> Requests { get; set; }
    }
}
