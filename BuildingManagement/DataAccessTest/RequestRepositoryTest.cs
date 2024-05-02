using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DataAccessTest;

[TestClass]
public class RequestRepositoryTest
{
    private BuildingManagementDbContext CreateDbContext(string BuildingManagementDb)
    {
        var options = new DbContextOptionsBuilder<BuildingManagementDbContext>()
            .UseInMemoryDatabase(BuildingManagementDb).Options;
        return new BuildingManagementDbContext(options);
    }

    [TestMethod]
    public void CreateRequest()
    {
        using (var context = CreateDbContext("TestCreateRequest"))
        {
            var repo = new RequestRepository(context);

            Building building = new Building()
            {
                BuildingId = Guid.NewGuid()
            };

            Apartment apartment = new Apartment()
            {
                BuildingId = building.BuildingId
            };

            Request_ expected = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Active,
                MaintenanceStaff = new MaintenanceStaff() { ID = new Guid(), Name = "nombre",
                    LastName = "apellido",
                    Email = "mail@example.com",
                    Password = "Password123"
                },
                Apartment = apartment
            };

            var result = repo.CreateRequest(expected);
            context.SaveChanges();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Id, result.Id);
            Assert.AreEqual(expected.MaintenanceStaff.ID, result.MaintenanceStaff.ID);

            var storedRequest = context.Requests.FirstOrDefault(r => r.Id.Equals(expected.Id));
            Assert.IsNotNull(storedRequest);
            Assert.AreEqual(expected.MaintenanceStaff.ID, storedRequest.MaintenanceStaff.ID);
        }
    }

    [TestMethod]
    public void GetAllRequest()
    {
        using (var context = CreateDbContext("TestGetAllRequest"))
        {
            var manager = new Manager()
            {
                ManagerId = new Guid(),
                Email = "mail",
                Password = "password"
            };

            Building building = new Building()
            {
                BuildingId = Guid.NewGuid(),
                ManagerId = manager.ManagerId,
                Manager = manager,
                Address = "direccion",
                ConstructionCompany = "company",
                Name = "nombre"
            };

            Apartment apartment = new Apartment()
            {
                BuildingId = building.BuildingId,
                Building = building
            };

            MaintenanceStaff maintenanceStaff = new MaintenanceStaff()
            {
                ID = new Guid(),
                Name = "nombre",
                LastName = "apellido",
                Email = "mail@example.com",
                Password = "Password123"
            };

            Request_ expected1 = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Active,
                MaintenanceStaff = maintenanceStaff,
                Apartment = apartment
            };

            Request_ expected2 = new Request_()
            {
                CategoryID = 2,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description b",
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Active,
                MaintenanceStaff = maintenanceStaff,
                Apartment = apartment
            };

            context.Set<Request_>().Add(expected1);
            context.Set<Request_>().Add(expected2);

            context.SaveChanges();

            var repo = new RequestRepository(context);

            var result = repo.GetAllRequest(manager.ManagerId).ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(expected1.Id, result[0].Id);
            Assert.AreEqual(expected2.Id, result[1].Id);
            Assert.AreEqual(expected1.MaintenanceStaff.ID, result[0].MaintenanceStaff.ID);
            Assert.AreEqual(expected2.MaintenanceStaff.ID, result[1].MaintenanceStaff.ID);
        }
    }

    [TestMethod]
    public void GetAllRequest_WithCategoryID2()
    {
        using (var context = CreateDbContext("TestGetAllRequestWithCategoryID2"))
        {
            var manager = new Manager()
            {
                ManagerId = new Guid(),
                Email = "mail",
                Password = "password"
            };

            Building building = new Building()
            {
                BuildingId = Guid.NewGuid(),
                ManagerId = manager.ManagerId,
                Manager = manager,
                Address = "direccion",
                ConstructionCompany = "company",
                Name = "nombre"
            };

            Apartment apartment = new Apartment()
            {
                BuildingId = building.BuildingId,
                Building = building
            };

            MaintenanceStaff maintenanceStaff = new MaintenanceStaff()
            {
                ID = new Guid(),
                Name = "nombre",
                LastName = "apellido",
                Email = "mail@example.com",
                Password = "Password123"
            };

            Request_ expected1 = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Active,
                MaintenanceStaff = maintenanceStaff,
                Apartment = apartment
            };

            Request_ expected2 = new Request_()
            {
                CategoryID = 2,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description b",
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Active,
                MaintenanceStaff = maintenanceStaff,
                Apartment = apartment
            };

            context.Set<Request_>().Add(expected1);
            context.Set<Request_>().Add(expected2);

            context.SaveChanges();

            var repo = new RequestRepository(context);

            var result = repo.GetAllRequest(manager.ManagerId, 2).ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(expected2.Id, result[0].Id);
            Assert.AreEqual(expected2.MaintenanceStaff.ID, result[0].MaintenanceStaff.ID);
        }
    }

    [TestMethod]
    public void GetOneRequest()
    {
        using (var context = CreateDbContext("TestGetOneRequest"))
        {
            var request1ID = Guid.NewGuid();
            var manager = new Manager()
            {
                ManagerId = new Guid(),
                Email = "mail",
                Password = "password"
            };

            Building building = new Building()
            {
                BuildingId = Guid.NewGuid(),
                ManagerId = manager.ManagerId,
                Manager = manager,
                Address = "direccion",
                ConstructionCompany = "company",
                Name = "nombre"
            };

            Apartment apartment = new Apartment()
            {
                BuildingId = building.BuildingId,
                Building = building
            };

            MaintenanceStaff maintenanceStaff = new MaintenanceStaff()
            {
                ID = new Guid(),
                Name = "nombre",
                LastName = "apellido",
                Email = "mail@example.com",
                Password = "Password123"
            };

            Request_ expected1 = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                Id = request1ID,
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Active,
                MaintenanceStaff = maintenanceStaff,
                Apartment = apartment
            };

            Request_ expected2 = new Request_()
            {
                CategoryID = 2,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description b",
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Active,
                MaintenanceStaff = maintenanceStaff,
                Apartment = apartment
            };

            context.Set<Request_>().Add(expected1);
            context.Set<Request_>().Add(expected2);

            context.SaveChanges();

            var repo = new RequestRepository(context);

            var result = repo.GetRequest(manager.ManagerId, request1ID);

            Assert.IsNotNull(result);
            Assert.AreEqual(expected1.Id, result.Id);
            Assert.AreEqual(expected1.MaintenanceStaff.ID, result.MaintenanceStaff.ID);
        }
    }

    [TestMethod]
    public void UpdateRequest()
    {
        using (var context = CreateDbContext("TestGetOneRequest"))
        {
            var requestID = Guid.NewGuid();
            var manager = new Manager()
            {
                ManagerId = new Guid(),
                Email = "mail",
                Password = "password"
            };

            Building building = new Building()
            {
                BuildingId = Guid.NewGuid(),
                ManagerId = manager.ManagerId,
                Manager = manager,
                Address = "direccion",
                ConstructionCompany = "company",
                Name = "nombre"
            };

            Apartment apartment = new Apartment()
            {
                BuildingId = building.BuildingId,
                Building = building
            };

            MaintenanceStaff maintenanceStaff = new MaintenanceStaff()
            {
                ID = new Guid(),
                Name = "nombre",
                LastName = "apellido",
                Email = "mail@example.com",
                Password = "Password123"
            };

            Request_ original = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                Id = requestID,
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Active,
                MaintenanceStaff = maintenanceStaff,
                Apartment = apartment
            };

            Request_ updated = new Request_()
            {
                CategoryID = 2,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description b",
                Id = requestID,
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Finished,
                MaintenanceStaff = maintenanceStaff,
                Apartment = apartment,
                EndTime = DateTime.Now,
                TotalCost = 1000
            };

            context.Set<Request_>().Add(original);

            context.SaveChanges();

            var repo = new RequestRepository(context);

            repo.Update(updated);

            var result = context.Requests.FirstOrDefault(r => r.Id.Equals(requestID));

            Assert.IsNotNull(result);
            Assert.AreEqual(requestID, result.Id);
            Assert.AreEqual(Status.Finished, result.Status);
            Assert.AreEqual(1000, result.TotalCost);
        }
    }
}