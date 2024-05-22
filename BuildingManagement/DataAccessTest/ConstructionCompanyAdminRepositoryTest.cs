using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DataAccessTest
{
    [TestClass]
    public class ConstructionCompanyAdminRepositoryTest
    {
        private BuildingManagementDbContext CreateDbContext(string dbName)
        {
            DbContextOptions<BuildingManagementDbContext> options = new DbContextOptionsBuilder<BuildingManagementDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new BuildingManagementDbContext(options);
        }

        [TestMethod]
        public void CreateConstructionCompanyAdminTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestCreateConstructionCompanyAdmin"))
            {
                ConstructionCompanyAdminRepository repository = new ConstructionCompanyAdminRepository(context);
                ConstructionCompanyAdmin expected = new ConstructionCompanyAdmin
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@example.com",
                    Password = "password123"
                };

                repository.CreateConstructionCompanyAdmin(expected);
                context.SaveChanges();

                ConstructionCompanyAdmin storedAdmin = context.ConstructionCompanyAdmins.FirstOrDefault(a => a.Id == expected.Id);
                Assert.IsNotNull(storedAdmin);
                Assert.AreEqual(expected.Email, storedAdmin.Email);
            }
        }

        [TestMethod]
        public void EmailExistsInConstructionCompanyAdminsTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestEmailExistsInConstructionCompanyAdmins"))
            {
                ConstructionCompanyAdminRepository repository = new ConstructionCompanyAdminRepository(context);
                ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@example.com",
                    Password = "password123"
                };

                context.ConstructionCompanyAdmins.Add(admin);
                context.SaveChanges();

                bool exists = repository.EmailExistsInConstructionCompanyAdmins(admin.Email);
                Assert.IsTrue(exists);

                bool notExists = repository.EmailExistsInConstructionCompanyAdmins("nonexistent@example.com");
                Assert.IsFalse(notExists);
            }
        }
    }
}
