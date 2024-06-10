using Domain;
using Microsoft.EntityFrameworkCore;

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
    }
}
