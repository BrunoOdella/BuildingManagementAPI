using BusinessLogic.Logics;
using CustomExceptions;
using CustomExceptions.InvitationExceptions;
using Domain;
using IDataAccess;
using LogicInterface.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace BusinessLogicTest
{
    [TestClass]
    public class InvitationLogicTest
    {
        private Mock<IInvitationRepository> _invitationRepositoryMock;
        private Mock<IManagerRepository> _managerRepositoryMock;
        private Mock<IAdminRepository> _adminRepositoryMock;
        private Mock<IMaintenanceStaffRepository> _maintenanceStaffRepositoryMock;
        private InvitationLogic _invitationLogic;

        [TestInitialize]
        public void TestSetup()
        {
            _invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            _managerRepositoryMock = new Mock<IManagerRepository>(MockBehavior.Strict);
            _adminRepositoryMock = new Mock<IAdminRepository>(MockBehavior.Strict);
            _maintenanceStaffRepositoryMock = new Mock<IMaintenanceStaffRepository>(MockBehavior.Strict);

            _invitationLogic = new InvitationLogic(
                _invitationRepositoryMock.Object,
                _managerRepositoryMock.Object,
                _adminRepositoryMock.Object,
                _maintenanceStaffRepositoryMock.Object);
        }



        [TestMethod]
        public void AcceptInvitation_ValidInvitation_UpdatesStatusAndCreatesManager()
        {
            // Arrange
            Guid invitationId = Guid.NewGuid();
            Invitation invitation = new Invitation
            {
                InvitationId = invitationId,
                Email = "test@example.com",
                Status = "No aceptada",
                ExpirationDate = DateTime.UtcNow.AddDays(1),
                Role = "encargado"
            };

            _invitationRepositoryMock.Setup(repo => repo.GetInvitationById(invitationId)).Returns(invitation);
            _invitationRepositoryMock.Setup(repo => repo.UpdateInvitation(It.IsAny<Invitation>()));
            _managerRepositoryMock.Setup(repo => repo.CreateManager(It.IsAny<Manager>()));

            // Act
            Invitation result = _invitationLogic.AcceptInvitation(invitationId, "test@example.com", "password123");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Aceptada", result.Status);
            _invitationRepositoryMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void AcceptInvitation_ValidInvitation_UpdatesStatusAndCreatesAdmin()
        {
            // Arrange
            Guid invitationId = Guid.NewGuid();
            Invitation invitation = new Invitation
            {
                InvitationId = invitationId,
                Email = "admin@example.com",
                Status = "No aceptada",
                ExpirationDate = DateTime.UtcNow.AddDays(1),
                Role = "admin"
            };

            _invitationRepositoryMock.Setup(repo => repo.GetInvitationById(invitationId)).Returns(invitation);
            _invitationRepositoryMock.Setup(repo => repo.UpdateInvitation(It.IsAny<Invitation>()));
            _adminRepositoryMock.Setup(repo => repo.CreateAdmin(It.IsAny<Admin>()));

            // Act
            Invitation result = _invitationLogic.AcceptInvitation(invitationId, "admin@example.com", "password123");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Aceptada", result.Status);
            _invitationRepositoryMock.VerifyAll();
            _adminRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvitationNotFoundException))]
        public void AcceptInvitation_InvitationNotFound_ThrowsException()
        {
            // Arrange
            Guid invitationId = Guid.NewGuid();

            _invitationRepositoryMock.Setup(repo => repo.GetInvitationById(invitationId)).Returns((Invitation)null);

            // Act
            _invitationLogic.AcceptInvitation(invitationId, "test@example.com", "password123");

            // Assert - Expects InvitationNotFoundException
            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvitationAlreadyAcceptedException))]
        public void AcceptInvitation_InvitationAlreadyAccepted_ThrowsException()
        {
            // Arrange
            Guid invitationId = Guid.NewGuid();
            Invitation invitation = new Invitation
            {
                InvitationId = invitationId,
                Email = "test@example.com",
                Status = "Aceptada"
            };

            _invitationRepositoryMock.Setup(repo => repo.GetInvitationById(invitationId)).Returns(invitation);

            // Act
            _invitationLogic.AcceptInvitation(invitationId, "test@example.com", "password123");

            // Assert - Expects InvitationAlreadyAcceptedException
            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvitationExpiredException))]
        public void AcceptInvitation_InvitationExpired_ThrowsException()
        {
            // Arrange
            Guid invitationId = Guid.NewGuid();
            Invitation invitation = new Invitation
            {
                InvitationId = invitationId,
                Email = "test@example.com",
                Status = "No aceptada",
                ExpirationDate = DateTime.UtcNow.AddDays(-1)
            };

            _invitationRepositoryMock.Setup(repo => repo.GetInvitationById(invitationId)).Returns(invitation);

            // Act
            _invitationLogic.AcceptInvitation(invitationId, "test@example.com", "password123");

            // Assert - Expects InvitationExpiredException
            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateInvitation_ValidatesData_AndCreatesInvitation()
        {
            // Arrange
            var invitation = new Invitation
            {
                Email = "test@example.com",
                Name = "Test Name",
                ExpirationDate = DateTime.UtcNow.AddDays(1)
            };

            _adminRepositoryMock.Setup(repo => repo.EmailExistsInAdmins(invitation.Email)).Returns(false);
            _managerRepositoryMock.Setup(repo => repo.EmailExistsInManagers(invitation.Email)).Returns(false);
            _maintenanceStaffRepositoryMock.Setup(repo => repo.EmailExistsInMaintenanceStaff(invitation.Email)).Returns(false);
            _invitationRepositoryMock.Setup(repo => repo.EmailExistsInInvitations(invitation.Email)).Returns(false);
            _invitationRepositoryMock.Setup(repo => repo.CreateInvitation(It.IsAny<Invitation>())).Returns(invitation);

            // Act
            var result = _invitationLogic.CreateInvitation(invitation);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("No aceptada", result.Status);
            _adminRepositoryMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();
            _maintenanceStaffRepositoryMock.VerifyAll();
            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(EmailAlreadyExistsException))]
        public void CreateInvitation_EmailAlreadyExists_ThrowsException()
        {
            // Arrange
            var invitation = new Invitation
            {
                Email = "test@example.com",
                Name = "Test Name",
                ExpirationDate = DateTime.UtcNow.AddDays(1)
            };

            _adminRepositoryMock.Setup(repo => repo.EmailExistsInAdmins(invitation.Email)).Returns(true);

            // Act
            _invitationLogic.CreateInvitation(invitation);

            // Assert - Expects EmailAlreadyExistsException
            _adminRepositoryMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();
            _maintenanceStaffRepositoryMock.VerifyAll();
            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteInvitation_ValidInvitation_DeletesInvitation()
        {
            // Arrange
            var invitationId = Guid.NewGuid();
            var invitation = new Invitation
            {
                InvitationId = invitationId,
                Status = "No aceptada"
            };

            _invitationRepositoryMock.Setup(repo => repo.GetInvitationById(invitationId)).Returns(invitation);
            _invitationRepositoryMock.Setup(repo => repo.DeleteInvitation(invitationId)).Returns(true);

            // Act
            var result = _invitationLogic.DeleteInvitation(invitationId);

            // Assert
            Assert.IsTrue(result);
            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(CannotDeleteAcceptedInvitationException))]
        public void DeleteInvitation_AcceptedInvitation_ThrowsException()
        {
            // Arrange
            var invitationId = Guid.NewGuid();
            var invitation = new Invitation
            {
                InvitationId = invitationId,
                Status = "Aceptada"
            };

            _invitationRepositoryMock.Setup(repo => repo.GetInvitationById(invitationId)).Returns(invitation);

            // Act
            _invitationLogic.DeleteInvitation(invitationId);

            // Assert - Expects CannotDeleteAcceptedInvitationException
            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void GetAllInvitations_ReturnsAllInvitations()
        {
            // Arrange
            var invitations = new List<Invitation>
            {
                new Invitation { InvitationId = Guid.NewGuid(), Email = "test1@example.com" },
                new Invitation { InvitationId = Guid.NewGuid(), Email = "test2@example.com" }
            };

            _invitationRepositoryMock.Setup(repo => repo.GetAllInvitations()).Returns(invitations);

            // Act
            var result = _invitationLogic.GetAllInvitations();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteInvitation_InvitationDoesNotExist_ReturnFalse()
        {

            _invitationRepositoryMock.Setup(repo => repo.GetInvitationById(It.IsAny<Guid>())).Returns((Invitation) null);

            // Act
            var result = _invitationLogic.DeleteInvitation(Guid.NewGuid());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result);
            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void AceptInvitation_InvitationEmailDiffersFromSavedOne_ThrowException()
        {
            string email1 = "test1@example.com";
            string email2 = "test2@example.com";

            Guid id = Guid.NewGuid();

            var invitation = new Invitation
            {
                InvitationId = id,
                Status = "No aceptada",
                Email = email1,
                ExpirationDate = DateTime.UtcNow.AddDays(1)
            };

            _invitationRepositoryMock.Setup(repo => repo.GetInvitationById(It.IsAny<Guid>())).Returns(invitation);

            Exception exception = null;

            try
            {
                _invitationLogic.AcceptInvitation(id, email2, "password");

            }
            catch (Exception ex)
            {
                exception = ex;
            }


            // Assert
            Assert.IsInstanceOfType(exception, typeof(UnauthorizedAccessException));
            Assert.IsTrue(exception.Message.Equals("Email does not match the invitation."));

            _invitationRepositoryMock.VerifyAll();
        }
    }
}
