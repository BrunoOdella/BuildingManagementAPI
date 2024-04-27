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
            var context = CreateDbContext("TestCreateInvitation");
            
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

        [TestMethod]
        public void GetAllInvtationsTest()
        {
            var context = CreateDbContext("TestGetInvitations");
            Invitation expected1 = new Invitation()
            {
                InvitationId = Guid.NewGuid(),
                Email = "example@.com",
                Name = "mateo",
                ExpirationDate = DateTime.UtcNow,
                Status = "pendiente"
            };
            Invitation expected2 = new Invitation()
            {
                InvitationId = Guid.NewGuid(),
                Email = "example2@.com",
                Name = "Joaquin",
                ExpirationDate = DateTime.UtcNow,
                Status = "Aceptada"
            };

            context.Set<Invitation>().Add(expected1);
            context.Set<Invitation>().Add(expected2);
            context.SaveChanges();
            var repository = new InvitationRepository(context);

            IEnumerable<Invitation> result = repository.GetAllInvitations();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 2);
        }
        [TestMethod]
        public void DeleteInvitationTest()
        {
            var context = CreateDbContext("TestDeleteInvitation");

            Invitation expected = new Invitation
            {
                InvitationId = Guid.NewGuid(),
                Email = "odella@example.com",
                Name = "Test",
                ExpirationDate = DateTime.Now,
                Status = "pendiente"
            };

            context.Invitations.Add(expected);
            context.SaveChanges();

            var repository = new InvitationRepository(context);

            bool result = repository.DeleteInvitation(expected.InvitationId);

            Assert.IsTrue(result);

            var deletedInvitation = context.Invitations.Find(expected.InvitationId);
            Assert.IsNull(deletedInvitation);
        }


    }
}
