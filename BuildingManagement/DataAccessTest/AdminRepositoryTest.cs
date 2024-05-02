using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccessTest
{
    [TestClass]
    public class AdminRepositoryTest
    {
        private BuildingManagementDbContext CreateDbContext(string BuildingManagementDb)
        {
            var options = new DbContextOptionsBuilder<BuildingManagementDbContext>().UseInMemoryDatabase(BuildingManagementDb).Options;
            return new BuildingManagementDbContext(options);
        }


        [TestMethod]
        public void CreateAdminTest()
        {
            using (var context = CreateDbContext("TestAddAdmin"))
            {
                var repository = new AdminRepository(context);
                Admin expected = new Admin
                {
                    AdminID = Guid.NewGuid(),
                    FirstName = "Juan",
                    LastName = "Odella",
                    Email = "odella@example.com",
                    Password = "password",
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
        public void Get()
        {
            using (var context = CreateDbContext("TestGetAdmin"))
            {
                var repository = new AdminRepository(context);

                Guid id = Guid.NewGuid();

                Admin expected = new Admin
                {
                    AdminID = id,
                    FirstName = "Juan",
                    LastName = "Odella",
                    Email = "odella@example.com",
                    Password = "password",
                };

                context.Admins.Add(expected);

                context.SaveChanges();

                var result = repository.Get(id);

                Assert.IsNotNull(result);
                Assert.AreEqual(expected.AdminID, result);
            }
        }

        [TestMethod]
        public void Get_AdminNotExist()
        {
            using (var context = CreateDbContext("TestGetAdmin"))
            {
                var repository = new AdminRepository(context);

                Guid id = Guid.NewGuid();

                var result = repository.Get(id);

                Assert.IsNotNull(result);
                Assert.AreEqual(Guid.Empty, result);
            }
        }




    }
}
