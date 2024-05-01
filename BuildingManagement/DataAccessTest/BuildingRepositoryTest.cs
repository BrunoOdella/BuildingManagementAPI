using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DataAccessTest
{
    [TestClass]
    public class BuildingRepositoryTest
    {
        private BuildingManagementDbContext CreateDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<BuildingManagementDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new BuildingManagementDbContext(options);
        }

        [TestMethod]
        public void CreateBuildingTest()
        {
            using (var context = CreateDbContext("TestCreateBuilding"))
            {
                var repository = new BuildingRepository(context);
                var expected = new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Sky Tower",
                    ConstructionCompany = "x",
                    Address = "123 Main St",
                    Location = new Location { Latitude = 40.7128, Longitude = -74.0060 },
                    CommonExpenses = 500
                };

                var result = repository.CreateBuilding(expected);
                context.SaveChanges();

                Assert.IsNotNull(result);
                Assert.AreEqual(expected.BuildingId, result.BuildingId);
                Assert.AreEqual(expected.Name, result.Name);

                var storedBuilding = context.Buildings.FirstOrDefault(b => b.BuildingId == expected.BuildingId);
                Assert.IsNotNull(storedBuilding);
                Assert.AreEqual(expected.Name, storedBuilding.Name);
            }
        }

        [TestMethod]
        public void DeleteBuilding_ExistingBuilding_ReturnsTrue()
        {
            using (var context = CreateDbContext("TestDeleteBuilding"))
            {
                var repository = new BuildingRepository(context);
                var building = new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Sky Tower",
                    Address = "123 Main St",
                    ConstructionCompany = "contruction company"
                };

                context.Buildings.Add(building);
                context.SaveChanges();

                // Act
                bool result = repository.DeleteBuilding(building.BuildingId);

                // Assert
                Assert.IsTrue(result);
                var deletedBuilding = context.Buildings.FirstOrDefault(b => b.BuildingId == building.BuildingId);
                Assert.IsNull(deletedBuilding);
            }
        }

        [TestMethod]
        public void DeleteBuilding_NonExistingBuilding_ReturnsFalse()
        {
            using (var context = CreateDbContext("TestDeleteNonExistingBuilding"))
            {
                var repository = new BuildingRepository(context);

                // Act
                bool result = repository.DeleteBuilding(Guid.NewGuid());

                // Assert
                Assert.IsFalse(result);
            }
        }
    }
}
