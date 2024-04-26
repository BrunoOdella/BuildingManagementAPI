using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessTest
{
    [TestClass]
    public class InvitationRepositoryTest
    {
        private BuildingManagementDbContext CreateDbContext(string BuildingManagementDb)
        {
            var options = new DbContextOptionsBuilder<BuildingManagementDbContext>().UseInMemoryDatabase(BuildingManagementDb).Options;
            return new BuildingManagementDbContext(options);
        }

        [TestMethod]
        public void CreateInvitationTest()
        {
            using (var context = CreateDbContext("TestCreateInvitation"))
            {
                var repository = new InvitationRepository(context);
                Invitation expected = new Invitation
                {
                    InvitationId = Guid.NewGuid(),
                    Email = "odella@example.com",
                    Name = "Test",
                    ExpirationDate = DateTime.Now,
                    Status= "pendiente"
                };

                var result = repository.CreateInvitation(expected);
                context.SaveChanges();

                Assert.IsNotNull(result);
                Assert.AreEqual(expected.InvitationId, result.InvitationId);
                Assert.AreEqual(expected.Email, result.Email);

                var storedInvitation = context.Invitations.FirstOrDefault(a => a.InvitationId == expected.InvitationId);
                Assert.IsNotNull(storedInvitation);
                Assert.AreEqual(expected.Email, storedInvitation.Email);
            }
        }
    }
}
