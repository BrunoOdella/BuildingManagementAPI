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
        public DbSet<ConstructionCompanyAdmin> ConstructionCompanyAdmins { get; set; }
        public DbSet<ConstructionCompany> ConstructionCompanies { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<MaintenanceStaff> MaintenanceStaff { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Building>()
                .OwnsOne(b => b.Location)
                .HasIndex(b => new { b.Latitude, b.Longitude })
                .IsUnique();

            modelBuilder.Entity<Manager>()
                .HasMany(m => m.Buildings)
                .WithOne(b => b.Manager)
                .HasForeignKey(b => b.ManagerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Building>()
                .HasMany(b => b.Apartments)
                .WithOne(a => a.Building)
                .HasForeignKey(a => a.BuildingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Apartment>()
                .HasMany(a => a.Requests)
                .WithOne(r => r.Apartment)
                .HasForeignKey(r => r.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MaintenanceStaff>()
                .HasMany(ms => ms.Requests)
                .WithOne(r => r.MaintenanceStaff)
                .HasForeignKey(r => r.MaintenanceStaffId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Apartment>()
                .HasOne(a => a.Owner)
                .WithOne()
                .HasForeignKey<Owner>(o => o.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Request_>()
                .HasOne(r => r.Category)
                .WithMany()
                .HasForeignKey(r => r.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConstructionCompany>()
                .HasOne(cc => cc.ConstructionCompanyAdmin)
                .WithOne(admin => admin.ConstructionCompany)
                .HasForeignKey<ConstructionCompany>(cc => cc.ConstructionCompanyAdminId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConstructionCompany>()
                .HasMany(cc => cc.Buildings)
                .WithOne(b => b.ConstructionCompany)
                .HasForeignKey(b => b.ConstructionCompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }


        //public static void SeedData(BuildingManagementDbContext context)
        //{
        //    using (var transaction = context.Database.BeginTransaction())
        //    {
        //        // Limpiar las tablas (opcional)
        //        if (context.Managers.Any())
        //        {
        //            context.Managers.RemoveRange(context.Managers);
        //        }
        //        if (context.Buildings.Any())
        //        {
        //            context.Buildings.RemoveRange(context.Buildings);
        //        }
        //        if (context.Apartments.Any())
        //        {
        //            context.Apartments.RemoveRange(context.Apartments);
        //        }
        //        if (context.MaintenanceStaff.Any())
        //        {
        //            context.MaintenanceStaff.RemoveRange(context.MaintenanceStaff);
        //        }
        //        if (context.Requests.Any())
        //        {
        //            context.Requests.RemoveRange(context.Requests);
        //        }
        //        if (context.Admins.Any())
        //        {
        //            context.Admins.RemoveRange(context.Admins);
        //        }
        //        if (context.Category.Any())
        //        {
        //            context.Category.RemoveRange(context.Category);
        //        }
        //        context.SaveChanges();

        //        // Añadir datos de prueba
        //        if (!context.Managers.Any())
        //        {
        //            // Crear Admin
        //            var admin = new Admin
        //            {
        //                AdminID = Guid.NewGuid(),
        //                FirstName = "Seba",
        //                LastName = "Escu",
        //                Email = "seba@mail.com",
        //                Password = "admin123"
        //            };
        //            context.Admins.Add(admin);

        //            // Crear Category
        //            var category = new Category
        //            {
        //                Name = "Plumbing",
        //                Description = "Issues related to plumbing"
        //            };
        //            context.Category.Add(category);
        //            context.SaveChanges();

        //            // Crear Manager
        //            var manager = new Manager
        //            {
        //                ManagerId = Guid.NewGuid(),
        //                Email = "manager@mail.com",
        //                Password = "manager123"
        //            };
        //            context.Managers.Add(manager);

        //            // Crear Building
        //            var building = new Building
        //            {
        //                BuildingId = Guid.NewGuid(),
        //                Name = "Test Building",
        //                Address = "123 Main St",
        //                Location = new Location { Latitude = 40.7128, Longitude = -74.0060 },
        //                ConstructionCompany = "Test Construction Co.",
        //                CommonExpenses = 500,
        //                ManagerId = manager.ManagerId
        //            };
        //            context.Buildings.Add(building);

        //            // Crear Apartment
        //            var apartment = new Apartment
        //            {
        //                ApartmentId = Guid.NewGuid(),
        //                Floor = 1,
        //                Number = 101,
        //                BuildingId = building.BuildingId
        //            };
        //            context.Apartments.Add(apartment);

        //            // Crear MaintenanceStaff
        //            var maintenanceStaff = new MaintenanceStaff
        //            {
        //                ID = Guid.NewGuid(),
        //                Name = "Bruno",
        //                LastName = "Ode",
        //                Email = "maintenancestaff@mail.com",
        //                Password = "password123",
        //                BuildingId = building.BuildingId
        //            };
        //            context.MaintenanceStaff.Add(maintenanceStaff);

        //            // Crear Request
        //            var request = new Request_
        //            {
        //                Id = Guid.NewGuid(),
        //                Description = "Fix the sink",
        //                Status = Status.Pending,
        //                CategoryID = category.ID, // Usar el ID generado automáticamente
        //                CreationTime = DateTime.UtcNow,
        //                ApartmentId = apartment.ApartmentId,
        //                MaintenanceStaffId = maintenanceStaff.ID
        //            };
        //            context.Requests.Add(request);

        //            context.SaveChanges();
        //        }

        //        transaction.Commit();
        //}
        //}

    }
}
