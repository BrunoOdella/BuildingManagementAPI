using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccessTest
{
    [TestClass]
    public class MaintenanceStaffRepositoryTest
    {
        private BuildingManagementDbContext CreateDbContext(string dbName)
        {
            DbContextOptions<BuildingManagementDbContext> options = new DbContextOptionsBuilder<BuildingManagementDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
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
                    Email = "johndoe@example.com",
                    Password = "password",
                };

                MaintenanceStaff result = repository.AddMaintenanceStaff(expected);
                context.SaveChanges();

                MaintenanceStaff storedMaintenanceStaff = context.MaintenanceStaff.FirstOrDefault(ms => ms.ID == expected.ID);
                Assert.IsNotNull(storedMaintenanceStaff);
                Assert.AreEqual(expected.Name, storedMaintenanceStaff.Name);
                Assert.AreEqual(expected.LastName, storedMaintenanceStaff.LastName);
                Assert.AreEqual(expected.Email, storedMaintenanceStaff.Email);
                Assert.AreEqual(expected.Password, storedMaintenanceStaff.Password);
            }
        }

        [TestMethod]
        public void GetAllTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetAllMaintenanceStaff"))
            {
                MaintenanceStaffRepository repository = new MaintenanceStaffRepository(context);

                Manager manager = new Manager
                {
                    ManagerId = Guid.NewGuid(),
                    Name = "Default Manager Name",
                    Email = "manager@example.com",
                    Password = "password"
                };

                Building building1 = new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Building 1",
                    Address = "123 Main St",
                    CommonExpenses = 100,
                    ManagerId = manager.ManagerId,

                };

                Building building2 = new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Building 2",
                    Address = "456 Elm St",
                    CommonExpenses = 200,
                    ManagerId = manager.ManagerId,

                };

                MaintenanceStaff staff1 = new MaintenanceStaff
                {
                    ID = Guid.NewGuid(),
                    Name = "John",
                    LastName = "Doe",
                    Email = "johndoe@example.com",
                    Password = "password",

                };

                MaintenanceStaff staff2 = new MaintenanceStaff
                {
                    ID = Guid.NewGuid(),
                    Name = "Jane",
                    LastName = "Doe",
                    Email = "janedoe@example.com",
                    Password = "password",

                };


                context.Managers.Add(manager);
                context.Buildings.Add(building1);
                context.Buildings.Add(building2);
                context.MaintenanceStaff.Add(staff1);
                context.MaintenanceStaff.Add(staff2);
                context.SaveChanges();

                List<MaintenanceStaff> result = repository.GetAll(manager.ManagerId).ToList();
                Assert.AreEqual(2, result.Count);
                Assert.IsTrue(result.Any(ms => ms.ID == staff1.ID));
                Assert.IsTrue(result.Any(ms => ms.ID == staff2.ID));
            }
        }

        [TestMethod]
        public void GetMaintenanceStaffTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetMaintenanceStaff"))
            {
                MaintenanceStaffRepository repository = new MaintenanceStaffRepository(context);

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
                    ManagerId = manager.ManagerId,

                };

                MaintenanceStaff staff = new MaintenanceStaff
                {
                    ID = Guid.NewGuid(),
                    Name = "John",
                    LastName = "Doe",
                    Email = "johndoe@example.com",
                    Password = "password",
                };


                context.Managers.Add(manager);
                context.Buildings.Add(building);
                context.MaintenanceStaff.Add(staff);
                context.SaveChanges();

                MaintenanceStaff result = repository.GetMaintenanceStaff(manager.ManagerId, staff.ID);
                Assert.IsNotNull(result);
                Assert.AreEqual(staff.ID, result.ID);
            }
        }

        [TestMethod]
        public void GetMaintenanceStaff_NonExisting_ReturnsNull()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetMaintenanceStaff_NonExisting"))
            {
                MaintenanceStaffRepository repository = new MaintenanceStaffRepository(context);

                Manager manager = new Manager
                {
                    ManagerId = Guid.NewGuid(),
                    Name = "Default Manager Name",
                    Email = "manager@example.com",
                    Password = "password"
                };

                context.Managers.Add(manager);
                context.SaveChanges();

                MaintenanceStaff result = repository.GetMaintenanceStaff(manager.ManagerId, Guid.NewGuid());
                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public void GetMaintenanceStaffByIdTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetMaintenanceStaffById"))
            {
                MaintenanceStaffRepository repository = new MaintenanceStaffRepository(context);

                MaintenanceStaff expected = new MaintenanceStaff
                {
                    ID = Guid.NewGuid(),
                    Name = "John",
                    LastName = "Doe",
                    Email = "johndoe@example.com",
                    Password = "password",
                };

                context.MaintenanceStaff.Add(expected);
                context.SaveChanges();

                Guid result = repository.GetMaintenanceStaff(expected.ID);
                Assert.AreEqual(expected.ID, result);
            }
        }

        [TestMethod]
        public void GetMaintenanceStaffById_NonExisting_ReturnsEmptyGuid()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetMaintenanceStaffById_NonExisting"))
            {
                MaintenanceStaffRepository repository = new MaintenanceStaffRepository(context);

                Guid result = repository.GetMaintenanceStaff(Guid.NewGuid());
                Assert.AreEqual(Guid.Empty, result);
            }
        }

        [TestMethod]
        public void EmailExistsInMaintenanceStaffTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestEmailExistsInMaintenanceStaff"))
            {
                MaintenanceStaffRepository repository = new MaintenanceStaffRepository(context);

                MaintenanceStaff staff = new MaintenanceStaff
                {
                    ID = Guid.NewGuid(),
                    Name = "John",
                    LastName = "Doe",
                    Email = "johndoe@example.com",
                    Password = "password",
                };

                context.MaintenanceStaff.Add(staff);
                context.SaveChanges();

                bool exists = repository.EmailExistsInMaintenanceStaff(staff.Email);
                Assert.IsTrue(exists);

                bool notExists = repository.EmailExistsInMaintenanceStaff("nonexistent@example.com");
                Assert.IsFalse(notExists);
            }
        }

    }
}
