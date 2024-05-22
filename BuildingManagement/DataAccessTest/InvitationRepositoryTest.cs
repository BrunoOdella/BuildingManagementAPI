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
    public class InvitationRepositoryTest
    {
        private BuildingManagementDbContext CreateDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<BuildingManagementDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new BuildingManagementDbContext(options);
        }

        [TestMethod]
        public void CreateInvitationTest()
        {
            using (var context = CreateDbContext("TestCreateInvitation"))
            {
                var repository = new InvitationRepository(context);
                var expected = new Invitation
                {
                    InvitationId = Guid.NewGuid(),
                    Email = "odella@example.com",
                    Name = "Test",
                    ExpirationDate = DateTime.UtcNow,
                    Status = "pendiente",
                    Role = "manager"
                };

                var result = repository.CreateInvitation(expected);
                context.SaveChanges();

                Assert.IsNotNull(result);
                Assert.AreEqual(expected.InvitationId, result.InvitationId);
                Assert.AreEqual(expected.Email, result.Email);

                var storedInvitation = context.Invitations.FirstOrDefault(a => a.InvitationId == expected.InvitationId);
                Assert.IsNotNull(storedInvitation);
                Assert.AreEqual(expected.Email, storedInvitation.Email);
                Assert.AreEqual(expected.Role, storedInvitation.Role);
            }
        }

        [TestMethod]
        public void GetAllInvitationsTest()
        {
            using (var context = CreateDbContext("TestGetAllInvitations"))
            {
                var expected1 = new Invitation
                {
                    InvitationId = Guid.NewGuid(),
                    Email = "example1@example.com",
                    Name = "Mateo",
                    ExpirationDate = DateTime.UtcNow,
                    Status = "pendiente",
                    Role = "manager"
                };
                var expected2 = new Invitation
                {
                    InvitationId = Guid.NewGuid(),
                    Email = "example2@example.com",
                    Name = "Joaquin",
                    ExpirationDate = DateTime.UtcNow,
                    Status = "Aceptada",
                    Role = "construction_company_admin"
                };

                context.Set<Invitation>().AddRange(expected1, expected2);
                context.SaveChanges();
                var repository = new InvitationRepository(context);

                IEnumerable<Invitation> result = repository.GetAllInvitations();

                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Count());
            }
        }

        [TestMethod]
        public void DeleteInvitation_ExistingInvitation_ReturnsTrue()
        {
            using (var context = CreateDbContext("TestDeleteInvitation"))
            {
                var expected = new Invitation
                {
                    InvitationId = Guid.NewGuid(),
                    Email = "odella@example.com",
                    Name = "Test",
                    ExpirationDate = DateTime.UtcNow,
                    Status = "pendiente",
                    Role = "manager"
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

        [TestMethod]
        public void DeleteInvitation_NonExistingInvitation_ReturnsFalse()
        {
            using (var context = CreateDbContext("TestDeleteNonExistingInvitation"))
            {
                var repository = new InvitationRepository(context);

                bool result = repository.DeleteInvitation(Guid.NewGuid());

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void GetInvitationById_ReturnsInvitationWithGivenId()
        {
            using (var context = CreateDbContext("TestGetInvitationById"))
            {
                var expected = new Invitation
                {
                    InvitationId = Guid.NewGuid(),
                    Email = "test@example.com",
                    Name = "Test",
                    ExpirationDate = DateTime.UtcNow,
                    Status = "pendiente",
                    Role = "manager"
                };

                context.Invitations.Add(expected);
                context.SaveChanges();

                var repository = new InvitationRepository(context);

                var result = repository.GetInvitationById(expected.InvitationId);

                Assert.IsNotNull(result);
                Assert.AreEqual(expected.InvitationId, result.InvitationId);
                Assert.AreEqual(expected.Email, result.Email);
                Assert.AreEqual(expected.Role, result.Role);
            }
        }

        [TestMethod]
        public void GetInvitationById_NonExistingInvitation_ReturnsNull()
        {
            using (var context = CreateDbContext("TestGetNonExistingInvitationById"))
            {
                var repository = new InvitationRepository(context);

                var result = repository.GetInvitationById(Guid.NewGuid());

                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public void EmailExistsInInvitations_EmailExists_ReturnsTrue()
        {
            using (var context = CreateDbContext("TestEmailExistsInInvitations_True"))
            {
                var invitation = new Invitation
                {
                    InvitationId = Guid.NewGuid(),
                    Email = "test@example.com",
                    Name = "Test",
                    ExpirationDate = DateTime.UtcNow,
                    Status = "pendiente",
                    Role = "manager"
                };

                context.Invitations.Add(invitation);
                context.SaveChanges();

                var repository = new InvitationRepository(context);

                var result = repository.EmailExistsInInvitations("test@example.com");

                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void EmailExistsInInvitations_EmailNotExists_ReturnsFalse()
        {
            using (var context = CreateDbContext("TestEmailExistsInInvitations_False"))
            {
                var repository = new InvitationRepository(context);

                var result = repository.EmailExistsInInvitations("nonexistent.email@example.com");

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void UpdateInvitation_Success()
        {
            using (var context = CreateDbContext("TestUpdateInvitation_Success"))
            {
                var repository = new InvitationRepository(context);

                var invitation = new Invitation
                {
                    InvitationId = Guid.NewGuid(),
                    Email = "email@mail.com",
                    Name = "Test",
                    ExpirationDate = DateTime.Today.AddDays(-1),
                    Status = "pendiente",
                    Role = "manager"
                };

                context.Invitations.Add(invitation);
                context.SaveChanges();

                var updatedInvitation = new Invitation
                {
                    InvitationId = invitation.InvitationId,
                    Email = "email@mail.com",
                    Name = "Test",
                    ExpirationDate = DateTime.Today,
                    Status = "Aceptada",
                    Role = "manager"
                };

                repository.UpdateInvitation(updatedInvitation);

                var result = context.Invitations.Find(invitation.InvitationId);

                Assert.AreEqual(updatedInvitation.Status, result.Status);
            }
        }

        [TestMethod]
        public void UpdateInvitation_InvitationNull()
        {
            using (var context = CreateDbContext("TestUpdateNullInvitation"))
            {
                var repository = new InvitationRepository(context);

                var invitation = new Invitation
                {
                    InvitationId = Guid.NewGuid(),
                    Email = "email@mail.com",
                    Name = "Test",
                    ExpirationDate = DateTime.Today.AddDays(-1),
                    Status = "pendiente",
                    Role = "manager"
                };

                context.Invitations.Add(invitation);
                context.SaveChanges();

                var updatedInvitation = new Invitation
                {
                    InvitationId = Guid.NewGuid(),
                    Email = "email@mail.com",
                    Name = "Test",
                    ExpirationDate = DateTime.Today,
                    Status = "Aceptada",
                    Role = "manager"
                };

                Exception exception = null;

                try
                {
                    repository.UpdateInvitation(updatedInvitation);
                }
                catch (Exception e)
                {
                    exception = e;
                }

                Assert.IsNotNull(exception);
                Assert.AreEqual("Invitation not found.", exception.Message);
                Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            }
        }
    }
}
