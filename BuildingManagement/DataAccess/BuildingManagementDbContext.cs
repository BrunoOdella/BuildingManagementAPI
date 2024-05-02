﻿using Domain;
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
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<MaintenanceStaff> MaintenanceStaff { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de Location como una propiedad compleja de Building
            modelBuilder.Entity<Building>()
                .OwnsOne(b => b.Location);  // Define Location como una propiedad propia de Building

            modelBuilder.Entity<Manager>()
                .HasMany(m => m.Buildings)
                .WithOne(b => b.Manager)
                .HasForeignKey(b => b.ManagerId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascada en la eliminación de Manager

            modelBuilder.Entity<Building>()
                .HasMany(b => b.Apartments)
                .WithOne(a => a.Building)
                .HasForeignKey(a => a.BuildingId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascada en la eliminación de Building

            modelBuilder.Entity<Apartment>()
                .HasMany(a => a.Requests)
                .WithOne(r => r.Apartment)
                .HasForeignKey(r => r.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascada en la eliminación de Requests cuando se elimina Apartment

            modelBuilder.Entity<Building>()
                .HasMany(b => b.MaintenanceStaff)
                .WithOne(ms => ms.Building)
                .HasForeignKey(ms => ms.BuildingId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascada en la eliminación de MaintenanceStaff cuando se elimina Building

            modelBuilder.Entity<MaintenanceStaff>()
                .HasMany(ms => ms.Requests)
                .WithOne(r => r.MaintenanceStaff)
                .HasForeignKey(r => r.MaintenanceStaffId)
                .OnDelete(DeleteBehavior.Restrict);  // No se eliminan las Requests al eliminar MaintenanceStaff

            modelBuilder.Entity<Apartment>()
                .HasOne(a => a.Owner)
                .WithOne()
                .HasForeignKey<Owner>(o => o.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascada en la eliminación de Owner
        }






    }
}
