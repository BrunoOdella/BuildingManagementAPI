using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessTest
{
    [TestClass]
    public class MaintenanceStaffRepositoryTest
    {
        private BuildingManagementDbContext CreateDbContext(string dbName)
        {
            DbContextOptions<BuildingManagementDbContext> options = new DbContextOptionsBuilder<BuildingManagementDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new BuildingManagementDbContext(options);
        }

        [TestMethod]
        public void AddMaintenanceStaffTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestAddMaintenanceStaff"))
            {
                MaintenanceStaffRepository repository = new MaintenanceStaffRepository(context);
                MaintenanceStaff expected = new MaintenanceStaff
                {
                    ID = Guid.NewGuid(),
                    Name = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Password = "securePassword123",
                    BuildingId = Guid.NewGuid()
                };

                MaintenanceStaff result = repository.AddMaintenanceStaff(expected);
                context.SaveChanges();

                Assert.IsNotNull(result);
                Assert.AreEqual(expected.ID, result.ID);
                Assert.AreEqual(expected.Email, result.Email);

                MaintenanceStaff storedStaff = context.MaintenanceStaff.FirstOrDefault(s => s.ID == expected.ID);
                Assert.IsNotNull(storedStaff);
                Assert.AreEqual(expected.Email, storedStaff.Email);
            }
        }

        [TestMethod]
        public void GetAllMaintenanceStaffTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetAllMaintenanceStaff"))
            {
                Guid managerId = Guid.NewGuid();
                Guid buildingId = Guid.NewGuid();

                Building building = new Building
                {
                    BuildingId = buildingId,
                    ManagerId = managerId,
                    Name = "Building 1",
                    Address = "123 Main St",
                    ConstructionCompany = "Company A"
                };
                context.Buildings.Add(building);

                MaintenanceStaff staff1 = new MaintenanceStaff
                {
                    ID = Guid.NewGuid(),
                    Name = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Password = "securePassword123",
                    BuildingId = buildingId
                };

                MaintenanceStaff staff2 = new MaintenanceStaff
                {
                    ID = Guid.NewGuid(),
                    Name = "Jane",
                    LastName = "Doe",
                    Email = "jane.doe@example.com",
                    Password = "securePassword123",
                    BuildingId = buildingId
                };

                context.MaintenanceStaff.AddRange(staff1, staff2);
                context.SaveChanges();

                MaintenanceStaffRepository repository = new MaintenanceStaffRepository(context);

                IEnumerable<MaintenanceStaff> result = repository.GetAll(managerId);

                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Count());
            }
        }

        [TestMethod]
        public void GetMaintenanceStaffTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetMaintenanceStaff"))
            {
                Guid managerId = Guid.NewGuid();
                Guid buildingId = Guid.NewGuid();
                Guid staffId = Guid.NewGuid();

                Building building = new Building
                {
                    BuildingId = buildingId,
                    ManagerId = managerId,
                    Name = "Building 1",
                    Address = "123 Main St",
                    ConstructionCompany = "Company A"
                };
                context.Buildings.Add(building);

                MaintenanceStaff staff = new MaintenanceStaff
                {
                    ID = staffId,
                    Name = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Password = "securePassword123",
                    BuildingId = buildingId
                };
                context.MaintenanceStaff.Add(staff);
                context.SaveChanges();

                MaintenanceStaffRepository repository = new MaintenanceStaffRepository(context);

                MaintenanceStaff result = repository.GetMaintenanceStaff(managerId, staffId);

                Assert.IsNotNull(result);
                Assert.AreEqual(staffId, result.ID);
            }
        }

        [TestMethod]
        public void GetMaintenanceStaff_NonExistingStaff_ReturnsNull()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetNonExistingMaintenanceStaff"))
            {
                Guid managerId = Guid.NewGuid();
                Guid staffId = Guid.NewGuid();

                MaintenanceStaffRepository repository = new MaintenanceStaffRepository(context);

                MaintenanceStaff result = repository.GetMaintenanceStaff(managerId, staffId);

                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public void GetMaintenanceStaffByIdTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetMaintenanceStaffById"))
            {
                Guid staffId = Guid.NewGuid();

                MaintenanceStaff staff = new MaintenanceStaff
                {
                    ID = staffId,
                    Name = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Password = "securePassword123",
                    BuildingId = Guid.NewGuid()
                };
                context.MaintenanceStaff.Add(staff);
                context.SaveChanges();

                MaintenanceStaffRepository repository = new MaintenanceStaffRepository(context);

                Guid result = repository.GetMaintenanceStaff(staffId);

                Assert.AreEqual(staffId, result);
            }
        }

        [TestMethod]
        public void GetMaintenanceStaffById_NonExistingStaff_ReturnsEmptyGuid()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetNonExistingMaintenanceStaffById"))
            {
                MaintenanceStaffRepository repository = new MaintenanceStaffRepository(context);

                Guid result = repository.GetMaintenanceStaff(Guid.NewGuid());

                Assert.AreEqual(Guid.Empty, result);
            }
        }

        [TestMethod]
        public void EmailExistsInMaintenanceStaff_EmailExists_ReturnsTrue()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestEmailExistsInMaintenanceStaff_True"))
            {
                MaintenanceStaff staff = new MaintenanceStaff
                {
                    ID = Guid.NewGuid(),
                    Email = "test@example.com",
                    Name = "John",
                    LastName = "Doe",
                    Password = "securePassword123",
                    BuildingId = Guid.NewGuid()
                };

                context.MaintenanceStaff.Add(staff);
                context.SaveChanges();

                MaintenanceStaffRepository repository = new MaintenanceStaffRepository(context);

                bool result = repository.EmailExistsInMaintenanceStaff("test@example.com");

                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void EmailExistsInMaintenanceStaff_EmailNotExists_ReturnsFalse()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestEmailExistsInMaintenanceStaff_False"))
            {
                MaintenanceStaffRepository repository = new MaintenanceStaffRepository(context);

                bool result = repository.EmailExistsInMaintenanceStaff("nonexistent.email@example.com");

                Assert.IsFalse(result);
            }
        }
    }
}
