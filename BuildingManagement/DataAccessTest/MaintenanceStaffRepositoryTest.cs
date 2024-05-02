using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessTest
{
    [TestClass]
    public class MaintenanceStaffRepositoryTest
    {
        private BuildingManagementDbContext CreateDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<BuildingManagementDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new BuildingManagementDbContext(options);
        }

        [TestMethod]
        public void AddMaintenanceStaff_StoresStaffCorrectly()
        {
            // Arrange
            var context = CreateDbContext("AddMaintenanceStaffTest");
            var repo = new MaintenanceStaffRepository(context);
            var maintenanceStaff = new MaintenanceStaff
            {
                Name = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                Password = "secure1234",
                BuildingId = Guid.NewGuid()
            };

            // Act
            var addedStaff = repo.AddMaintenanceStaff(maintenanceStaff);

            // Assert
            Assert.IsNotNull(addedStaff);
            Assert.AreEqual("John", addedStaff.Name);
            Assert.AreEqual("Doe", addedStaff.LastName);
            Assert.AreEqual("johndoe@example.com", addedStaff.Email);

            // Cleanup
            context.Database.EnsureDeleted();
        }
    }
}
