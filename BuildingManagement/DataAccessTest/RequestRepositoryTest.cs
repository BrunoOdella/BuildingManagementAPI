using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccessTest
{
    [TestClass]
    public class RequestRepositoryTest
    {
        private BuildingManagementDbContext CreateDbContext(string dbName)
        {
            DbContextOptions<BuildingManagementDbContext> options = new DbContextOptionsBuilder<BuildingManagementDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new BuildingManagementDbContext(options);
        }

        [TestMethod]
        public void CreateRequestTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestCreateRequest"))
            {
                RequestRepository repository = new RequestRepository(context);
                Request_ expected = new Request_
                {
                    Id = Guid.NewGuid(),
                    Description = "Fix the leaky faucet",
                    Status = Status.Pending,
                    CategoryID = 1,
                    CreationTime = DateTime.Now,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2),
                    TotalCost = 100.0f,
                    MaintenanceStaffId = Guid.NewGuid(),
                    ApartmentId = Guid.NewGuid()
                };

                Request_ result = repository.CreateRequest(expected);
                context.SaveChanges();

                Request_ storedRequest = context.Requests.FirstOrDefault(r => r.Id == expected.Id);
                Assert.IsNotNull(storedRequest);
                Assert.AreEqual(expected.Description, storedRequest.Description);
                Assert.AreEqual(expected.Status, storedRequest.Status);
            }
        }

        [TestMethod]
        public void GetAllRequestTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetAllRequest"))
            {
                RequestRepository repository = new RequestRepository(context);

                Manager manager = new Manager
                {
                    ManagerId = Guid.NewGuid(),
                    Name = "Default Manager Name",
                    Email = "manager@example.com",
                    Password = "password"
                };

                Building building = new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Building",
                    Address = "123 Main St",
                    CommonExpenses = 100,
                    ManagerId = manager.ManagerId
                };

                Apartment apartment = new Apartment
                {
                    ApartmentId = Guid.NewGuid(),
                    Floor = 1,
                    Number = 101,
                    BuildingId = building.BuildingId
                };

                Request_ request = new Request_
                {
                    Id = Guid.NewGuid(),
                    Description = "Fix the leaky faucet",
                    Status = Status.Pending,
                    CategoryID = 1,
                    CreationTime = DateTime.Now,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2),
                    TotalCost = 100.0f,
                    MaintenanceStaffId = Guid.NewGuid(),
                    ApartmentId = apartment.ApartmentId
                };

                context.Managers.Add(manager);
                context.Buildings.Add(building);
                context.Apartments.Add(apartment);
                context.Requests.Add(request);
                context.SaveChanges();

                List<Request_> result = repository.GetAllRequest(manager.ManagerId).ToList();
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(request.Id, result[0].Id);
            }
        }

        [TestMethod]
        public void GetAllRequestByCategoryTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetAllRequestByCategory"))
            {
                RequestRepository repository = new RequestRepository(context);

                Manager manager = new Manager
                {
                    ManagerId = Guid.NewGuid(),
                    Name = "Default Manager Name",
                    Email = "manager@example.com",
                    Password = "password"
                };

                Building building = new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Building",
                    Address = "123 Main St",
                    CommonExpenses = 100,
                    ManagerId = manager.ManagerId
                };

                Apartment apartment = new Apartment
                {
                    ApartmentId = Guid.NewGuid(),
                    Floor = 1,
                    Number = 101,
                    BuildingId = building.BuildingId
                };

                Request_ request1 = new Request_
                {
                    Id = Guid.NewGuid(),
                    Description = "Fix the leaky faucet",
                    Status = Status.Pending,
                    CategoryID = 1,
                    CreationTime = DateTime.Now,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2),
                    TotalCost = 100.0f,
                    MaintenanceStaffId = Guid.NewGuid(),
                    ApartmentId = apartment.ApartmentId
                };

                Request_ request2 = new Request_
                {
                    Id = Guid.NewGuid(),
                    Description = "Fix the broken window",
                    Status = Status.Pending,
                    CategoryID = 2,
                    CreationTime = DateTime.Now,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2),
                    TotalCost = 200.0f,
                    MaintenanceStaffId = Guid.NewGuid(),
                    ApartmentId = apartment.ApartmentId
                };

                context.Managers.Add(manager);
                context.Buildings.Add(building);
                context.Apartments.Add(apartment);
                context.Requests.Add(request1);
                context.Requests.Add(request2);
                context.SaveChanges();

                List<Request_> result = repository.GetAllRequest(manager.ManagerId, 1).ToList();
                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(request1.Id, result[0].Id);
            }
        }

        [TestMethod]
        public void GetRequestTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetRequest"))
            {
                RequestRepository repository = new RequestRepository(context);

                Manager manager = new Manager
                {
                    ManagerId = Guid.NewGuid(),
                    Name = "Default Manager Name",
                    Email = "manager@example.com",
                    Password = "password"
                };

                Building building = new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Building",
                    Address = "123 Main St",
                    CommonExpenses = 100,
                    ManagerId = manager.ManagerId
                };

                Apartment apartment = new Apartment
                {
                    ApartmentId = Guid.NewGuid(),
                    Floor = 1,
                    Number = 101,
                    BuildingId = building.BuildingId
                };

                Request_ request = new Request_
                {
                    Id = Guid.NewGuid(),
                    Description = "Fix the leaky faucet",
                    Status = Status.Pending,
                    CategoryID = 1,
                    CreationTime = DateTime.Now,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2),
                    TotalCost = 100.0f,
                    MaintenanceStaffId = Guid.NewGuid(),
                    ApartmentId = apartment.ApartmentId
                };

                context.Managers.Add(manager);
                context.Buildings.Add(building);
                context.Apartments.Add(apartment);
                context.Requests.Add(request);
                context.SaveChanges();

                Request_ result = repository.GetRequest(manager.ManagerId, request.Id);
                Assert.IsNotNull(result);
                Assert.AreEqual(request.Id, result.Id);
            }
        }

        [TestMethod]
        public void UpdateRequestTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestUpdateRequest"))
            {
                RequestRepository repository = new RequestRepository(context);

                Request_ request = new Request_
                {
                    Id = Guid.NewGuid(),
                    Description = "Fix the leaky faucet",
                    Status = Status.Pending,
                    CategoryID = 1,
                    CreationTime = DateTime.Now,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2),
                    TotalCost = 100.0f,
                    MaintenanceStaffId = Guid.NewGuid(),
                    ApartmentId = Guid.NewGuid()
                };

                context.Requests.Add(request);
                context.SaveChanges();

                request.Status = Status.Finished;
                request.EndTime = DateTime.Now;
                request.TotalCost = 150.0f;
                repository.Update(request);

                Request_ updatedRequest = context.Requests.FirstOrDefault(r => r.Id == request.Id);
                Assert.IsNotNull(updatedRequest);
                Assert.AreEqual(Status.Finished, updatedRequest.Status);
                Assert.AreEqual(150.0f, updatedRequest.TotalCost);
            }
        }

        [TestMethod]
        public void GetAllRequestStaffTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetAllRequestStaff"))
            {
                RequestRepository repository = new RequestRepository(context);

                MaintenanceStaff staff = new MaintenanceStaff
                {
                    ID = Guid.NewGuid(),
                    Name = "John",
                    LastName = "Doe",
                    Email = "johndoe@example.com",
                    Password = "password",
                };

                Request_ request1 = new Request_
                {
                    Id = Guid.NewGuid(),
                    Description = "Fix the leaky faucet",
                    Status = Status.Pending,
                    CategoryID = 1,
                    CreationTime = DateTime.Now,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2),
                    TotalCost = 100.0f,
                    MaintenanceStaffId = staff.ID,
                    ApartmentId = Guid.NewGuid()
                };

                Request_ request2 = new Request_
                {
                    Id = Guid.NewGuid(),
                    Description = "Fix the broken window",
                    Status = Status.Pending,
                    CategoryID = 2,
                    CreationTime = DateTime.Now,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2),
                    TotalCost = 200.0f,
                    MaintenanceStaffId = staff.ID,
                    ApartmentId = Guid.NewGuid()
                };

                context.MaintenanceStaff.Add(staff);
                context.Requests.Add(request1);
                context.Requests.Add(request2);
                context.SaveChanges();

                List<Request_> result = repository.GetAllRequestStaff(staff.ID).ToList();
                Assert.AreEqual(2, result.Count);
                Assert.IsTrue(result.Any(r => r.Id == request1.Id));
                Assert.IsTrue(result.Any(r => r.Id == request2.Id));
            }
        }

        [TestMethod]
        public void GetRequest_Succes()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetRequest"))
            {
                RequestRepository repository = new RequestRepository(context);
                Guid requestId = Guid.NewGuid();
                Request_ request = new Request_
                {
                    Id = requestId,
                    Description = "Fix the leaky faucet",
                    Status = Status.Pending,
                    CategoryID = 1,
                    CreationTime = DateTime.Now,
                    MaintenanceStaffId = Guid.NewGuid(),
                    ApartmentId = Guid.NewGuid()
                };

                context.Requests.Add(request);
                context.SaveChanges();

                Request_ result = repository.Get(requestId);
                Assert.IsNotNull(result);
                Assert.AreEqual(request.Id, result.Id);
            }
        }
    }
}
