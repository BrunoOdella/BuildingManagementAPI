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
    public class ManagerRepositoryTest
    {
        private BuildingManagementDbContext CreateDbContext(string dbName)
        {
            DbContextOptions<BuildingManagementDbContext> options = new DbContextOptionsBuilder<BuildingManagementDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new BuildingManagementDbContext(options);
        }

        [TestMethod]
        public void CreateManagerTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestCreateManager"))
            {
                ManagerRepository repository = new ManagerRepository(context);
                Manager expected = new Manager
                {
                    ManagerId = Guid.NewGuid(), Name = "Default Manager Name",
                    Email = "manager@example.com",
                    Password = "password"
                };

                repository.CreateManager(expected);
                context.SaveChanges();

                Manager storedManager = context.Managers.FirstOrDefault(m => m.ManagerId == expected.ManagerId);
                Assert.IsNotNull(storedManager);
                Assert.AreEqual(expected.Email, storedManager.Email);
            }
        }

        [TestMethod]
        public void GetManagerByEmailTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetManagerByEmail"))
            {
                ManagerRepository repository = new ManagerRepository(context);
                Manager expected = new Manager
                {
                    ManagerId = Guid.NewGuid(), Name = "Default Manager Name",
                    Email = "manager@example.com",
                    Password = "password"
                };

                context.Managers.Add(expected);
                context.SaveChanges();

                Manager result = repository.GetManagerByEmail(expected.Email);
                Assert.IsNotNull(result);
                Assert.AreEqual(expected.ManagerId, result.ManagerId);
                Assert.AreEqual(expected.Email, result.Email);
            }
        }

        [TestMethod]
        public void UpdateManagerTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestUpdateManager"))
            {
                ManagerRepository repository = new ManagerRepository(context);
                Manager manager = new Manager
                {
                    ManagerId = Guid.NewGuid(), Name = "Default Manager Name",
                    Email = "manager@example.com",
                    Password = "password"
                };

                context.Managers.Add(manager);
                context.SaveChanges();

                manager.Password = "newpassword";
                repository.UpdateManager(manager);
                context.SaveChanges();

                Manager updatedManager = context.Managers.FirstOrDefault(m => m.ManagerId == manager.ManagerId);
                Assert.IsNotNull(updatedManager);
                Assert.AreEqual("newpassword", updatedManager.Password);
            }
        }

        [TestMethod]
        public void GetManagerByIdTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetManagerById"))
            {
                ManagerRepository repository = new ManagerRepository(context);
                Manager expected = new Manager
                {
                    ManagerId = Guid.NewGuid(), Name = "Default Manager Name",
                    Email = "manager@example.com",
                    Password = "password"
                };

                context.Managers.Add(expected);
                context.SaveChanges();

                Guid result = repository.Get(expected.ManagerId);
                Assert.AreEqual(expected.ManagerId, result);
            }
        }

        [TestMethod]
        public void GetManagerById_NonExisting_ReturnsEmptyGuid()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetManagerById_NonExisting"))
            {
                ManagerRepository repository = new ManagerRepository(context);

                Guid result = repository.Get(Guid.NewGuid());
                Assert.AreEqual(Guid.Empty, result);
            }
        }

        [TestMethod]
        public void EmailExistsInManagersTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestEmailExistsInManagers"))
            {
                ManagerRepository repository = new ManagerRepository(context);
                Manager manager = new Manager
                {
                    ManagerId = Guid.NewGuid(), Name = "Default Manager Name",
                    Email = "manager@example.com",
                    Password = "password"
                };

                context.Managers.Add(manager);
                context.SaveChanges();

                bool exists = repository.EmailExistsInManagers(manager.Email);
                Assert.IsTrue(exists);

                bool notExists = repository.EmailExistsInManagers("nonexistent@example.com");
                Assert.IsFalse(notExists);
            }
        }

        [TestMethod]
        public void GetByEmailAndPasswordTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetByEmailAndPassword"))
            {
                ManagerRepository repository = new ManagerRepository(context);
                Manager manager = new Manager
                {
                    ManagerId = Guid.NewGuid(),
                    Name = "Default Manager Name",
                    Email = "email",
                    Password = "password"
                };

                context.Managers.Add(manager);
                context.SaveChanges();

                Manager result = repository.GetByEmailAndPassword(manager.Email, manager.Password);
                Assert.IsNotNull(result);
                Assert.AreEqual(manager.Email, result.Email);   
            }
        }

        [TestMethod]
        public void GetAllTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetAll"))
            {
                ManagerRepository repository = new ManagerRepository(context);
                Manager manager1 = new Manager
                {
                    ManagerId = Guid.NewGuid(),
                    Name = "Manager1",
                    Email = "email",
                    Password = "password"
                };

                Manager manager2 = new Manager
                {
                    ManagerId = Guid.NewGuid(),
                    Name = "Manager2",
                    Email = "email",
                    Password = "password"
                };

                context.Managers.Add(manager1);
                context.Managers.Add(manager2);
                context.SaveChanges();

                IEnumerable<Manager> result = repository.GetAll();

                Assert.AreEqual(2, result.Count());
                Assert.IsTrue(result.Any(m => m.ManagerId == manager1.ManagerId));
                Assert.IsTrue(result.Any(m => m.ManagerId == manager2.ManagerId));
                Assert.IsTrue(result.Any(m => m.Name == manager1.Name));
            }
        }

        [TestMethod]
        public void GetManagerByIdTest2()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetManagerById2"))
            {
                ManagerRepository repository = new ManagerRepository(context);
                Manager manager = new Manager
                {
                    ManagerId = Guid.NewGuid(),
                    Name = "Default Manager Name",
                    Email = "email",
                    Password = "password"
                };

                context.Managers.Add(manager);
                context.SaveChanges();

                Manager result = repository.GetManagerById(manager.ManagerId);

                Assert.IsNotNull(result);
                Assert.AreEqual(manager.ManagerId, result.ManagerId);
                Assert.AreEqual(manager.Email, result.Email);
            }
        }

    }
}
