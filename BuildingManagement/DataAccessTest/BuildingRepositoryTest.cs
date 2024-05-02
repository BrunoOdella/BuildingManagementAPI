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


        [TestMethod]
        public void UpdateBuilding_ExistingBuilding_UpdatesAndSavesCorrectly()
        {
            using (var context = CreateDbContext("TestUpdateBuilding"))
            {
                var repository = new BuildingRepository(context);
                var originalBuilding = new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Original Name",
                    Address = "Original Address",
                    ConstructionCompany = "Original Company",
                    CommonExpenses = 100,
                    Location = new Location { Latitude = 40.7128, Longitude = -74.0060 }
                };

                context.Buildings.Add(originalBuilding);
                context.SaveChanges();

                // Change some properties
                originalBuilding.Name = "Updated Name";
                originalBuilding.Address = "Updated Address";
                originalBuilding.CommonExpenses = 200;

                // Act
                var updatedBuilding = repository.UpdateBuilding(originalBuilding);

                // Assert
                Assert.AreEqual("Updated Name", updatedBuilding.Name);
                Assert.AreEqual("Updated Address", updatedBuilding.Address);
                Assert.AreEqual(200, updatedBuilding.CommonExpenses);

                // Verify that changes are saved to the database
                var storedBuilding = context.Buildings.FirstOrDefault(b => b.BuildingId == originalBuilding.BuildingId);
                Assert.IsNotNull(storedBuilding);
                Assert.AreEqual("Updated Name", storedBuilding.Name);
                Assert.AreEqual("Updated Address", storedBuilding.Address);
                Assert.AreEqual(200, storedBuilding.CommonExpenses);
            }
        }

        [TestMethod]
        public void GetAll()
        {
            using (var context = CreateDbContext("TestGetAllBuildings"))
            {
                var repository = new BuildingRepository(context);

                Guid id1 = Guid.NewGuid();
                Guid id2 = Guid.NewGuid();

                Manager manager = new Manager()
                {
                    ManagerId = new Guid(),
                    Email = "email",
                    Password = "pass"
                };

                var building1 = new Building
                {
                    BuildingId = id1,
                    Name = "Name1",
                    Address = "Address",
                    ConstructionCompany = "Company",
                    CommonExpenses = 100,
                    Location = new Location { Latitude = 40.7128, Longitude = -74.0060 },
                    Manager = manager
                };

                var building2 = new Building
                {
                    BuildingId = id2,
                    Name = "Name2",
                    Address = "Address2",
                    ConstructionCompany = "Company2",
                    CommonExpenses = 200,
                    Location = new Location { Latitude = 20.123, Longitude = -20.123 },
                    Manager = manager
                };

                context.Buildings.Add(building1);
                context.Buildings.Add(building2);

                context.SaveChanges();

                // Act
                var buildings = repository.GetAll(manager.ManagerId).ToList();

                // Assert
                Assert.IsNotNull(buildings);
                Assert.AreEqual(buildings.Count(), 2);
                Assert.AreEqual(buildings[0].BuildingId, id1);
                Assert.AreEqual(buildings[1].BuildingId, id2);

                // Verify that changes are saved to the database
                var storedBuilding = context.Buildings.Where(b => b.ManagerId.Equals(manager.ManagerId)).ToList();
                Assert.IsNotNull(storedBuilding);
                Assert.AreEqual("Name1", storedBuilding[0].Name);
                Assert.AreEqual("Address", storedBuilding[0].Address);
                Assert.AreEqual(100, storedBuilding[0].CommonExpenses);
            }
        }

        [TestMethod]
        public void GetApartment()
        {
            using (var context = CreateDbContext("TestGetApartment"))
            {
                var repository = new BuildingRepository(context);

                Guid id1 = Guid.NewGuid();
                Guid id2 = Guid.NewGuid();

                Manager manager = new Manager()
                {
                    ManagerId = new Guid(),
                    Email = "email",
                    Password = "pass"
                };

                var building1 = new Building
                {
                    BuildingId = id1,
                    Name = "Name1",
                    Address = "Address",
                    ConstructionCompany = "Company",
                    CommonExpenses = 100,
                    Location = new Location { Latitude = 40.7128, Longitude = -74.0060 },
                    Manager = manager,
                    Apartments = new List<Apartment>()
                };

                Apartment apartment = new Apartment()
                {
                    BuildingId = building1.BuildingId,
                    ApartmentId = new Guid(),
                    Building = building1
                };

                building1.Apartments.Add(apartment);

                context.Buildings.Add(building1);

                context.SaveChanges();

                // Act
                var response = repository.GetApartment(manager.ManagerId, apartment.ApartmentId, building1.BuildingId);

                // Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(response.ApartmentId, apartment.ApartmentId);
                Assert.AreEqual(response.BuildingId, building1.BuildingId);
            }
        }

        [TestMethod]
        public void GetApartment_BUildingNotExist()
        {
            using (var context = CreateDbContext("TestGetApartment2"))
            {
                var repository = new BuildingRepository(context);

                Guid id1 = Guid.NewGuid();

                Manager manager = new Manager()
                {
                    ManagerId = new Guid(),
                    Email = "email",
                    Password = "pass"
                };

                var building1 = new Building
                {
                    BuildingId = id1,
                    Name = "Name1",
                    Address = "Address",
                    ConstructionCompany = "Company",
                    CommonExpenses = 100,
                    Location = new Location { Latitude = 40.7128, Longitude = -74.0060 },
                    Manager = new Manager(){ManagerId = new Guid(), Email = "email2", Password = "pass2"},
                    Apartments = new List<Apartment>()
                };

                Apartment apartment = new Apartment()
                {
                    BuildingId = building1.BuildingId,
                    ApartmentId = new Guid(),
                    Building = building1
                };

                building1.Apartments.Add(apartment);

                context.Buildings.Add(building1);

                context.SaveChanges();

                // Act
                var response = repository.GetApartment(manager.ManagerId, apartment.ApartmentId, building1.BuildingId);

                // Assert
                Assert.IsNull(response);
            }
        }

        [TestMethod]
        public void GetBuilding()
        {
            using (var context = CreateDbContext("TestGetBuilding"))
            {
                var repository = new BuildingRepository(context);

                Guid id1 = Guid.NewGuid();

                Manager manager = new Manager()
                {
                    ManagerId = new Guid(),
                    Email = "email",
                    Password = "pass"
                };

                var building1 = new Building
                {
                    BuildingId = id1,
                    Name = "Name1",
                    Address = "Address",
                    ConstructionCompany = "Company",
                    CommonExpenses = 100,
                    Location = new Location { Latitude = 40.7128, Longitude = -74.0060 },
                    Manager = manager,
                    Apartments = new List<Apartment>()
                };

                context.Buildings.Add(building1);

                context.SaveChanges();

                // Act
                var response = repository.GetBuilding(manager.ManagerId, building1.BuildingId);

                // Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(response.BuildingId, building1.BuildingId);
            }
        }
    }
}
