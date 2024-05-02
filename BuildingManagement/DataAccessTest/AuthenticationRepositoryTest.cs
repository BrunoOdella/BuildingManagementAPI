using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccessTest;

[TestClass]
public class AuthenticationRepositoryTest
{
    private BuildingManagementDbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<BuildingManagementDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
        return new BuildingManagementDbContext(options);
    }

    [TestMethod]
    public void BuscarToken()
    {
        using (var context = CreateDbContext("TestSearchToken"))

    }
}