using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccessTest
{
    [TestClass]
    public class AdminRepositoryTest
    {
        private BuildingManagementDbContext CreateDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<BuildingManagementDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new BuildingManagementDbContext(options);
        }

        [TestMethod]
        public void CreateAdminTest()
        {
            using (var context = CreateDbContext("TestAddAdmin"))
            {
                var repository = new AdminRepository(context);
                var expected = new Admin
                {
                    AdminID = Guid.NewGuid(),
                    FirstName = "Juan",
                    LastName = "Perez",
                    Email = "juan.perez@example.com",
                    Password = "securePassword123"
                };

                var result = repository.CreateAdmin(expected);
                context.SaveChanges();

                Assert.IsNotNull(result);
                Assert.AreEqual(expected.AdminID, result.AdminID);
                Assert.AreEqual(expected.Email, result.Email);

                var storedAdmin = context.Admins.FirstOrDefault(a => a.AdminID == expected.AdminID);
                Assert.IsNotNull(storedAdmin);
                Assert.AreEqual(expected.Email, storedAdmin.Email);
            }
        }

        [TestMethod]
        public void GetAdminTest()
        {
            using (var context = CreateDbContext("TestGetAdmin"))
            {
                var repository = new AdminRepository(context);

                var expected = new Admin
                {
                    AdminID = Guid.NewGuid(),
                    FirstName = "Juan",
                    LastName = "Perez",
                    Email = "juan.perez@example.com",
                    Password = "securePassword123"
                };

                context.Admins.Add(expected);
                context.SaveChanges();

                var result = repository.Get(expected.AdminID);

                Assert.AreEqual(expected.AdminID, result);
            }
        }

        [TestMethod]
        public void GetAdmin_NotExist_ReturnsEmptyGuid()
        {
            using (var context = CreateDbContext("TestGetAdmin_NotExist"))
            {
                var repository = new AdminRepository(context);

                var result = repository.Get(Guid.NewGuid());

                Assert.AreEqual(Guid.Empty, result);
            }
        }

        [TestMethod]
        public void EmailExistsInAdmins_EmailExists_ReturnsTrue()
        {
            using (var context = CreateDbContext("TestEmailExistsInAdmins_True"))
            {
                var repository = new AdminRepository(context);

                var admin = new Admin
                {
                    AdminID = Guid.NewGuid(),
                    FirstName = "Juan",
                    LastName = "Perez",
                    Email = "juan.perez@example.com",
                    Password = "securePassword123"
                };

                context.Admins.Add(admin);
                context.SaveChanges();

                var result = repository.EmailExistsInAdmins("juan.perez@example.com");

                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void EmailExistsInAdmins_EmailNotExists_ReturnsFalse()
        {
            using (var context = CreateDbContext("TestEmailExistsInAdmins_False"))
            {
                var repository = new AdminRepository(context);

                var result = repository.EmailExistsInAdmins("nonexistent.email@example.com");

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void GetByEmailAndPassword_AdminExists_ReturnsAdmin()
        {
            using (var context = CreateDbContext("TestGetByEmailAndPassword_AdminExists"))
            {
                var repository = new AdminRepository(context);

                var admin = new Admin
                {
                    AdminID = Guid.NewGuid(),
                    FirstName = "Juan",
                    LastName = "Perez",
                    Email = "email",
                    Password = "password",
                };

                context.Admins.Add(admin);
                context.SaveChanges();

                var result = repository.GetByEmailAndPassword(admin.Email, admin.Password);

                Assert.IsNotNull(result);
                Assert.AreEqual(admin.Email, result.Email);
                Assert.AreEqual(admin.AdminID, result.AdminID);
            }
        }
    }
}
