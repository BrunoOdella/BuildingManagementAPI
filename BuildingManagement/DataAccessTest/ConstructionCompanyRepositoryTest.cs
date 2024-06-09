using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DataAccessTest
{
    [TestClass]
    public class ConstructionCompanyRepositoryTest
    {
        private BuildingManagementDbContext CreateDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<BuildingManagementDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new BuildingManagementDbContext(options);
        }

        [TestMethod]
        public void CreateConstructionCompanyTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestCreateConstructionCompany"))
            {
                ConstructionCompanyRepository repository = new ConstructionCompanyRepository(context);
                ConstructionCompany expected = new ConstructionCompany
                {
                    ConstructionCompanyId = Guid.NewGuid(),
                    Name = "New Construction Company",
                    ConstructionCompanyAdminId = Guid.NewGuid()
                };

                ConstructionCompany result = repository.CreateConstructionCompany(expected);
                context.SaveChanges();

                Assert.IsNotNull(result);
                Assert.AreEqual(expected.ConstructionCompanyId, result.ConstructionCompanyId);
                Assert.AreEqual(expected.Name, result.Name);

                ConstructionCompany storedCompany = context.ConstructionCompanies.FirstOrDefault(cc => cc.ConstructionCompanyId == expected.ConstructionCompanyId);
                Assert.IsNotNull(storedCompany);
                Assert.AreEqual(expected.Name, storedCompany.Name);
            }
        }

        [TestMethod]
        public void NameExists_ShouldReturnTrueIfExists()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestNameExists_True"))
            {
                ConstructionCompanyRepository repository = new ConstructionCompanyRepository(context);
                ConstructionCompany existingCompany = new ConstructionCompany
                {
                    ConstructionCompanyId = Guid.NewGuid(),
                    Name = "Existing Construction Company",
                    ConstructionCompanyAdminId = Guid.NewGuid()
                };

                context.ConstructionCompanies.Add(existingCompany);
                context.SaveChanges();

                bool exists = repository.NameExists(existingCompany.Name);
                Assert.IsTrue(exists);
            }
        }

        [TestMethod]
        public void NameExists_ShouldReturnFalseIfNotExists()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestNameExists_False"))
            {
                ConstructionCompanyRepository repository = new ConstructionCompanyRepository(context);

                bool exists = repository.NameExists("Nonexistent Construction Company");
                Assert.IsFalse(exists);
            }
        }

        [TestMethod]
        public void AdminHasCompany_ShouldReturnTrueIfAdminHasCompany()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestAdminHasCompany_True"))
            {
                ConstructionCompanyRepository repository = new ConstructionCompanyRepository(context);
                ConstructionCompanyAdmin constructionCompanyAdmin = new ConstructionCompanyAdmin
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@example.com",
                    Password = "password123",
                    Name = "nombre"
                };
                ConstructionCompany company = new ConstructionCompany
                {
                    ConstructionCompanyId = Guid.NewGuid(),
                    Name = "Existing Construction Company",
                    ConstructionCompanyAdminId = constructionCompanyAdmin.Id,
                    ConstructionCompanyAdmin = constructionCompanyAdmin
                };

                context.ConstructionCompanyAdmins.Add(constructionCompanyAdmin);
                context.ConstructionCompanies.Add(company);
                context.SaveChanges();

                bool hasCompany = repository.AdminHasCompany(constructionCompanyAdmin.Id);
                Assert.IsTrue(hasCompany);
            }
        }

        [TestMethod]
        public void AdminHasCompany_ShouldReturnFalseIfAdminHasNoCompany()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestAdminHasCompany_False"))
            {
                ConstructionCompanyRepository repository = new ConstructionCompanyRepository(context);

                bool hasCompany = repository.AdminHasCompany(Guid.NewGuid());
                Assert.IsFalse(hasCompany);
            }
        }

        [TestMethod]
        public void UpdateConstructionCompanyNameTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestUpdateConstructionCompanyName"))
            {
                ConstructionCompanyRepository repository = new ConstructionCompanyRepository(context);
                ConstructionCompany existingCompany = new ConstructionCompany
                {
                    ConstructionCompanyId = Guid.NewGuid(),
                    Name = "Existing Construction Company",
                    ConstructionCompanyAdminId = Guid.NewGuid()
                };

                context.ConstructionCompanies.Add(existingCompany);
                context.SaveChanges();

                ConstructionCompany updatedCompany = new ConstructionCompany
                {
                    ConstructionCompanyId = existingCompany.ConstructionCompanyId,
                    Name = "Updated Construction Company",
                    ConstructionCompanyAdminId = existingCompany.ConstructionCompanyAdminId
                };

                ConstructionCompany result = repository.UpdateConstructionCompanyName(updatedCompany, existingCompany.Name);
                context.SaveChanges();

                Assert.IsNotNull(result);
                Assert.AreEqual(updatedCompany.ConstructionCompanyId, result.ConstructionCompanyId);
                Assert.AreEqual(updatedCompany.Name, result.Name);

                ConstructionCompany storedCompany = context.ConstructionCompanies.FirstOrDefault(cc => cc.ConstructionCompanyId == updatedCompany.ConstructionCompanyId);
                Assert.IsNotNull(storedCompany);
                Assert.AreEqual(updatedCompany.Name, storedCompany.Name);
            }
        }

        [TestMethod]
        public void GetConstructionCompanyAdminByIdTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetConstructionCompanyAdminById"))
            {
                ConstructionCompanyRepository repository = new ConstructionCompanyRepository(context);
                ConstructionCompanyAdmin constructionCompanyAdmin = new ConstructionCompanyAdmin
                {
                    Id = Guid.NewGuid(),
                    Email = "email",
                    Name = "name",
                    Password = "password"
                };

                ConstructionCompany company = new ConstructionCompany
                {
                    ConstructionCompanyId = Guid.NewGuid(),
                    Name = "Construction Company",
                    ConstructionCompanyAdminId = constructionCompanyAdmin.Id,
                    ConstructionCompanyAdmin = constructionCompanyAdmin
                };

                context.ConstructionCompanyAdmins.Add(constructionCompanyAdmin);
                context.ConstructionCompanies.Add(company);
                context.SaveChanges();

                ConstructionCompanyAdmin retrievedAdmin = repository.GetConstructionCompanyAdminById(constructionCompanyAdmin.Id.ToString());

                Assert.IsNotNull(retrievedAdmin);
                Assert.AreEqual(constructionCompanyAdmin.Email, retrievedAdmin.Email);
                Assert.IsNotNull(retrievedAdmin.ConstructionCompany);
                Assert.AreEqual(constructionCompanyAdmin.ConstructionCompany.Name, retrievedAdmin.ConstructionCompany.Name);
            }
        }

        [TestMethod]
        public void UpdateConstructionCompany_Success()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            using (BuildingManagementDbContext context = CreateDbContext("TestUpdateConstructionCompany123"))
            {
                var repository = new ConstructionCompanyRepository(context);
                var initialCompany = new ConstructionCompany
                {
                    ConstructionCompanyId = companyId,
                    Name = "Old Name",
                    ConstructionCompanyAdminId = Guid.NewGuid(),
                };

                context.ConstructionCompanies.Add(initialCompany);
                context.SaveChanges();

                // Act
                var updatedCompany = new ConstructionCompany
                {
                    ConstructionCompanyId = companyId,  
                    Name = "Updated Name"
                };
                repository.UpdateConstructionCompany(updatedCompany);

                // Assert
                var companyInDb = context.ConstructionCompanies.Find(companyId);
                Assert.IsNotNull(companyInDb);
                Assert.AreEqual(initialCompany.ConstructionCompanyId, companyInDb.ConstructionCompanyId);
                Assert.AreEqual("Updated Name", companyInDb.Name);
            }
        }

        [TestMethod]
        public void GetCompanyByAdminIdTest()
        {
            using (BuildingManagementDbContext context = CreateDbContext("TestGetCompanyByAdminId"))
            {
                ConstructionCompanyRepository repository = new ConstructionCompanyRepository(context);
                Guid adminId = Guid.NewGuid();
                ConstructionCompany company = new ConstructionCompany
                {
                    ConstructionCompanyId = Guid.NewGuid(),
                    Name = "Construction Company",
                    ConstructionCompanyAdminId = adminId
                };

                context.ConstructionCompanies.Add(company);
                context.SaveChanges();

                ConstructionCompany result = repository.GetCompanyByAdminId(adminId);
                Assert.IsNotNull(result);
                Assert.AreEqual(company.ConstructionCompanyId, result.ConstructionCompanyId);
                Assert.AreEqual(company.Name, result.Name);
                Assert.AreEqual(company.ConstructionCompanyAdminId, result.ConstructionCompanyAdminId);
            }
        }
    }
}
