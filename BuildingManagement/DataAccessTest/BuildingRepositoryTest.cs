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
    public class BuildingRepositoryTest
    {
        private BuildingManagementDbContext CreateDbContext(string dbName)
        {
            DbContextOptions<BuildingManagementDbContext> options = new DbContextOptionsBuilder<BuildingManagementDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new BuildingManagementDbContext(options);
        }

        [TestMethod]
        public void CreateBuildingTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestCreateBuilding"))
            {
                BuildingRepository repository = new BuildingRepository(context);
                Building expected = new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Sky Tower",
                    Address = "123 Main St",
                    Location = new Location { Latitude = 40.7128, Longitude = -74.0060 },
                    CommonExpenses = 500
                };

                Building result = repository.CreateBuilding(expected);
                context.SaveChanges();

                Assert.IsNotNull(result);
                Assert.AreEqual(expected.BuildingId, result.BuildingId);
                Assert.AreEqual(expected.Name, result.Name);

                Building storedBuilding = context.Buildings.FirstOrDefault(b => b.BuildingId == expected.BuildingId);
                Assert.IsNotNull(storedBuilding);
                Assert.AreEqual(expected.Name, storedBuilding.Name);
            }
        }

        [TestMethod]
        public void DeleteBuilding_ExistingBuilding_ReturnsTrue()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestDeleteBuilding"))
            {
                BuildingRepository repository = new BuildingRepository(context);
                Building building = new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Sky Tower",
                    Address = "123 Main St",
                };

                context.Buildings.Add(building);
                context.SaveChanges();

                // Act
                bool result = repository.DeleteBuilding(building.BuildingId);

                // Assert
                Assert.IsTrue(result);
                Building deletedBuilding = context.Buildings.FirstOrDefault(b => b.BuildingId == building.BuildingId);
                Assert.IsNull(deletedBuilding);
            }
        }

        [TestMethod]
        public void DeleteBuilding_NonExistingBuilding_ReturnsFalse()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestDeleteNonExistingBuilding"))
            {
                BuildingRepository repository = new BuildingRepository(context);

                // Act
                bool result = repository.DeleteBuilding(Guid.NewGuid());

                // Assert
                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void UpdateBuilding_ExistingBuilding_UpdatesAndSavesCorrectly()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestUpdateBuilding"))
            {
                BuildingRepository repository = new BuildingRepository(context);
                Building originalBuilding = new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Original Name",
                    Address = "Original Address",
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
                Building updatedBuilding = repository.UpdateBuilding(originalBuilding);

                // Assert
                Assert.AreEqual("Updated Name", updatedBuilding.Name);
                Assert.AreEqual("Updated Address", updatedBuilding.Address);
                Assert.AreEqual(200, updatedBuilding.CommonExpenses);

                // Verify that changes are saved to the database
                Building storedBuilding = context.Buildings.FirstOrDefault(b => b.BuildingId == originalBuilding.BuildingId);
                Assert.IsNotNull(storedBuilding);
                Assert.AreEqual("Updated Name", storedBuilding.Name);
                Assert.AreEqual("Updated Address", storedBuilding.Address);
                Assert.AreEqual(200, storedBuilding.CommonExpenses);
            }
        }

        [TestMethod]
        public void GetAll()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetAllBuildings"))
            {
                BuildingRepository repository = new BuildingRepository(context);

                Guid id1 = Guid.NewGuid();
                Guid id2 = Guid.NewGuid();

                Manager manager = new Manager()
                {
                    ManagerId = Guid.NewGuid(),
                    Email = "email",
                    Password = "pass"
                };

                Building building1 = new Building
                {
                    BuildingId = id1,
                    Name = "Name1",
                    Address = "Address",
                    CommonExpenses = 100,
                    Location = new Location { Latitude = 40.7128, Longitude = -74.0060 },
                    ManagerId = manager.ManagerId
                };

                Building building2 = new Building
                {
                    BuildingId = id2,
                    Name = "Name2",
                    Address = "Address2",
                    CommonExpenses = 200,
                    Location = new Location { Latitude = 20.123, Longitude = -20.123 },
                    ManagerId = manager.ManagerId
                };

                context.Buildings.Add(building1);
                context.Buildings.Add(building2);
                context.Managers.Add(manager);

                context.SaveChanges();

                // Act
                List<Building> buildings = repository.GetAll(manager.ManagerId).ToList();

                // Assert
                Assert.IsNotNull(buildings);
                Assert.AreEqual(2, buildings.Count);
                Assert.AreEqual(id1, buildings[0].BuildingId);
                Assert.AreEqual(id2, buildings[1].BuildingId);
            }
        }

        [TestMethod]
        public void GetApartment_ShouldReturnApartment_WhenApartmentExistsAndManagerIsAuthorized()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetApartment"))
            {
                BuildingRepository repository = new BuildingRepository(context);

                Guid id1 = Guid.NewGuid();
                Guid apartmentId = Guid.NewGuid();

                Manager manager = new Manager()
                {
                    ManagerId = Guid.NewGuid(),
                    Email = "email",
                    Password = "pass"
                };

                Building building1 = new Building
                {
                    BuildingId = id1,
                    Name = "Name1",
                    Address = "Address",
                    CommonExpenses = 100,
                    Location = new Location { Latitude = 40.7128, Longitude = -74.0060 },
                    ManagerId = manager.ManagerId,
                    Apartments = new List<Apartment>()
                };

                Apartment apartment = new Apartment
                {
                    ApartmentId = apartmentId,
                    Floor = 1,
                    Number = 101,
                    BuildingId = id1
                };

                building1.Apartments.Add(apartment);
                context.Buildings.Add(building1);
                context.Apartments.Add(apartment);
                context.Managers.Add(manager);

                context.SaveChanges();

                // Act
                Apartment response = repository.GetApartment(manager.ManagerId, apartment.ApartmentId);

                // Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(apartment.ApartmentId, response.ApartmentId);
                Assert.AreEqual(building1.BuildingId, response.BuildingId);
            }
        }

        [TestMethod]
        public void GetApartment_ShouldReturnNull_WhenManagerIsNotAuthorized()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetApartmentUnauthorized"))
            {
                BuildingRepository repository = new BuildingRepository(context);

                Guid id1 = Guid.NewGuid();
                Guid apartmentId = Guid.NewGuid();

                Manager manager = new Manager()
                {
                    ManagerId = Guid.NewGuid(),
                    Email = "email",
                    Password = "pass"
                };

                Building building1 = new Building
                {
                    BuildingId = id1,
                    Name = "Name1",
                    Address = "Address",
                    CommonExpenses = 100,
                    Location = new Location { Latitude = 40.7128, Longitude = -74.0060 },
                    ManagerId = Guid.NewGuid() // Different ManagerId
                };

                Apartment apartment = new Apartment
                {
                    ApartmentId = apartmentId,
                    Floor = 1,
                    Number = 101,
                    BuildingId = id1
                };

                building1.Apartments.Add(apartment);
                context.Buildings.Add(building1);
                context.Apartments.Add(apartment);
                context.Managers.Add(manager);

                context.SaveChanges();

                // Act
                Apartment response = repository.GetApartment(manager.ManagerId, apartment.ApartmentId);

                // Assert
                Assert.IsNull(response);
            }
        }

        [TestMethod]
        public void GetApartment_ShouldReturnNull_WhenApartmentDoesNotExist()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetApartmentNonExistent"))
            {
                BuildingRepository repository = new BuildingRepository(context);

                Guid id1 = Guid.NewGuid();
                Guid apartmentId = Guid.NewGuid();

                Manager manager = new Manager()
                {
                    ManagerId = Guid.NewGuid(),
                    Email = "email",
                    Password = "pass"
                };

                Building building1 = new Building
                {
                    BuildingId = id1,
                    Name = "Name1",
                    Address = "Address",
                    CommonExpenses = 100,
                    Location = new Location { Latitude = 40.7128, Longitude = -74.0060 },
                    ManagerId = manager.ManagerId,
                    Apartments = new List<Apartment>()
                };

                context.Buildings.Add(building1);
                context.Managers.Add(manager);

                context.SaveChanges();

                // Act
                Apartment response = repository.GetApartment(manager.ManagerId, apartmentId);

                // Assert
                Assert.IsNull(response);
            }
        }

        [TestMethod]
        public void GetBuilding()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetBuilding"))
            {
                BuildingRepository repository = new BuildingRepository(context);

                Guid id1 = Guid.NewGuid();

                Manager manager = new Manager()
                {
                    ManagerId = Guid.NewGuid(),
                    Email = "email",
                    Password = "pass"
                };

                Building building1 = new Building
                {
                    BuildingId = id1,
                    Name = "Name1",
                    Address = "Address",
                    CommonExpenses = 100,
                    Location = new Location { Latitude = 40.7128, Longitude = -74.0060 },
                    ManagerId = manager.ManagerId
                };

                context.Buildings.Add(building1);
                context.Managers.Add(manager);

                context.SaveChanges();

                // Act
                Building response = repository.GetBuilding(manager.ManagerId, building1.BuildingId);

                // Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(building1.BuildingId, response.BuildingId);
            }
        }



        [TestMethod]
        public void GetBuildingByLocation_ReturnsCorrectBuilding()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetBuildingByLocation"))
            {
                BuildingRepository repository = new BuildingRepository(context);
                Building expected = new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Sky Tower",
                    Address = "123 Main St",
                    Location = new Location { Latitude = 40.7128, Longitude = -74.0060 },
                    CommonExpenses = 500
                };

                context.Buildings.Add(expected);
                context.SaveChanges();

                // Act
                Building result = repository.GetBuildingByLocation(40.7128, -74.0060);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(expected.BuildingId, result.BuildingId);
                Assert.AreEqual(expected.Name, result.Name);
            }
        }

        [TestMethod]
        public void GetBuildingByLocation_NoBuildingFound_ReturnsNull()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetBuildingByLocation_NoBuildingFound"))
            {
                BuildingRepository repository = new BuildingRepository(context);

                // Act
                Building result = repository.GetBuildingByLocation(40.7128, -74.0060);

                // Assert
                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public void GetAllApartment_ShouldReturnApartment_WhenApartmentExistsAndManagerIsAuthorized()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetApartment"))
            {
                BuildingRepository repository = new BuildingRepository(context);

                Guid id1 = Guid.NewGuid();
                Guid apartmentId = Guid.NewGuid();
                Guid apartmentId2 = Guid.NewGuid();

                Manager manager = new Manager()
                {
                    ManagerId = Guid.NewGuid(),
                    Email = "email",
                    Password = "pass"
                };

                Building building1 = new Building
                {
                    BuildingId = id1,
                    Name = "Name1",
                    Address = "Address",
                    CommonExpenses = 100,
                    Location = new Location { Latitude = 40.7128, Longitude = -74.0060 },
                    ManagerId = manager.ManagerId,
                    Apartments = new List<Apartment>()
                };

                Apartment apartment = new Apartment
                {
                    ApartmentId = apartmentId,
                    Floor = 1,
                    Number = 101,
                    BuildingId = id1
                };

                Apartment apartment2 = new Apartment
                {
                    ApartmentId = apartmentId2,
                    Floor = 2,
                    Number = 102,
                    BuildingId = id1
                };

                building1.Apartments.Add(apartment);
                building1.Apartments.Add(apartment2);
                context.Buildings.Add(building1);
                context.Apartments.Add(apartment);
                context.Managers.Add(manager);

                context.SaveChanges();

                // Act
                List<Apartment> response = repository.GetAllApartments(manager.ManagerId, id1);

                // Assert
                Assert.IsNotNull(response);
                Assert.AreEqual(2, response.Count);
                Assert.AreEqual(id1, response[0].BuildingId);
                Assert.AreEqual(id1, response[1].BuildingId);
                Assert.AreEqual(apartmentId, response[0].ApartmentId);
                Assert.AreEqual(apartmentId2, response[1].ApartmentId);
            }
        }

        [TestMethod]
        public void GetBuildingsByConstructionCompanyAdminId_ReturnsBuildings()
        {
            using (var context = CreateDbContext("TestGetBuildingsByConstructionCompanyAdminId"))
            {
                var adminId = Guid.NewGuid();
                var buildings = new List<Building>
                {
                    new Building
                    {
                        BuildingId = Guid.NewGuid(),
                        Name = "Building 1",
                        Address = "123 Main St",
                        ConstructionCompanyAdminId = adminId
                    },
                    new Building
                    {
                        BuildingId = Guid.NewGuid(),
                        Name = "Building 2",
                        Address = "456 Oak St",
                        ConstructionCompanyAdminId = adminId
                    }
                };

                context.Buildings.AddRange(buildings);
                context.SaveChanges();

                var repository = new BuildingRepository(context);
                var result = repository.GetBuildingsByConstructionCompanyAdminId(adminId);

                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Count());
                Assert.IsTrue(result.Any(b => b.Name == "Building 1"));
                Assert.IsTrue(result.Any(b => b.Name == "Building 2"));
            }
        }
    }
}
