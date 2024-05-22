using DataAccess;
using Domain;
using IDataAccess;
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
                    Name = "New Construction Company"
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
                    Name = "Existing Construction Company"
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
    }
}
