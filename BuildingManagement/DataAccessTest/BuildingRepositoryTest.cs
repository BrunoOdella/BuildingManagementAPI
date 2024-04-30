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
        public void AddBuildingTest()
        {
            using (var context = CreateDbContext("TestAddBuilding"))
            {
                var repository = new BuildingRepository(context);
                var expected = new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Sky Tower",
                    Address = "123 Main St",
                    Location = new Location { Latitude = 40.7128, Longitude = -74.0060 },
                    CommonExpenses = 500
                };

                var result = repository.AddBuilding(expected);
                context.SaveChanges();

                Assert.IsNotNull(result);
                Assert.AreEqual(expected.BuildingId, result.BuildingId);
                Assert.AreEqual(expected.Name, result.Name);

                var storedBuilding = context.Buildings.FirstOrDefault(b => b.BuildingId == expected.BuildingId);
                Assert.IsNotNull(storedBuilding);
                Assert.AreEqual(expected.Name, storedBuilding.Name);
            }
        }
    }
}
