using BusinessLogic.Logics;
using CustomExceptions;
using Domain;
using IDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BusinessLogicTest
{
    [TestClass]
    public class AdminLogicTest
    {
        private Mock<IAdminRepository> _adminRepositoryMock;
        private Mock<IInvitationRepository> _invitationRepositoryMock;
        private Mock<IManagerRepository> _managerRepositoryMock;
        private Mock<IMaintenanceStaffRepository> _maintenanceStaffRepositoryMock;
        private AdminLogic _adminLogic;

        [TestInitialize]
        public void TestSetup()
        {
            _adminRepositoryMock = new Mock<IAdminRepository>(MockBehavior.Strict);
            _invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            _managerRepositoryMock = new Mock<IManagerRepository>(MockBehavior.Strict);
            _maintenanceStaffRepositoryMock = new Mock<IMaintenanceStaffRepository>(MockBehavior.Strict);

            _adminLogic = new AdminLogic(
                _invitationRepositoryMock.Object,
                _managerRepositoryMock.Object,
                _adminRepositoryMock.Object,
                _maintenanceStaffRepositoryMock.Object);
        }

        [TestMethod]
        public void CreateAdmin_ValidatesData_AndCreatesAdmin()
        {
            // Arrange
            Admin admin = new Admin
            {
                AdminID = Guid.Parse("fd6021ba-dd96-4e90-9100-c25e448315eb"),
                FirstName = "Juan",
                LastName = "Perez",
                Email = "juan.perez@example.com",
                Password = "securePassword123"
            };

            _adminRepositoryMock.Setup(repository => repository.CreateAdmin(It.IsAny<Admin>())).Returns(admin);
            _adminRepositoryMock.Setup(repository => repository.EmailExistsInAdmins(admin.Email)).Returns(false);
            _managerRepositoryMock.Setup(repository => repository.EmailExistsInManagers(admin.Email)).Returns(false);
            _maintenanceStaffRepositoryMock.Setup(repository => repository.EmailExistsInMaintenanceStaff(admin.Email)).Returns(false);
            _invitationRepositoryMock.Setup(repository => repository.EmailExistsInInvitations(admin.Email)).Returns(false);

            // Act
            Admin result = _adminLogic.CreateAdmin(admin);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(Guid.Parse("fd6021ba-dd96-4e90-9100-c25e448315eb"), result.AdminID);
            _adminRepositoryMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();
            _maintenanceStaffRepositoryMock.VerifyAll();
            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(EmailAlreadyExistsException))]
        public void CreateAdmin_EmailAlreadyExistsInAdmins_ThrowsException()
        {
            // Arrange
            Admin admin = new Admin
            {
                AdminID = Guid.NewGuid(),
                FirstName = "Juan",
                LastName = "Perez",
                Email = "juan.perez@example.com",
                Password = "securePassword123"
            };

            _adminRepositoryMock.Setup(repository => repository.EmailExistsInAdmins(admin.Email)).Returns(true);

            // Act
            _adminLogic.CreateAdmin(admin);

            // Assert - Expects EmailAlreadyExistsException
            _adminRepositoryMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();
            _maintenanceStaffRepositoryMock.VerifyAll();
            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(EmailAlreadyExistsException))]
        public void CreateAdmin_EmailExistsInManagers_ThrowsException()
        {
            // Arrange
            Admin admin = new Admin
            {
                AdminID = Guid.NewGuid(),
                FirstName = "Juan",
                LastName = "Perez",
                Email = "juan.perez@example.com",
                Password = "securePassword123"
            };

            _adminRepositoryMock.Setup(repository => repository.EmailExistsInAdmins(admin.Email)).Returns(false);
            _managerRepositoryMock.Setup(repository => repository.EmailExistsInManagers(admin.Email)).Returns(true);

            // Act
            _adminLogic.CreateAdmin(admin);

            // Assert - Expects EmailAlreadyExistsException
            _adminRepositoryMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();
            _maintenanceStaffRepositoryMock.VerifyAll();
            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(EmailAlreadyExistsException))]
        public void CreateAdmin_EmailExistsInMaintenanceStaff_ThrowsException()
        {
            // Arrange
            Admin admin = new Admin
            {
                AdminID = Guid.NewGuid(),
                FirstName = "Juan",
                LastName = "Perez",
                Email = "juan.perez@example.com",
                Password = "securePassword123"
            };

            _adminRepositoryMock.Setup(repository => repository.EmailExistsInAdmins(admin.Email)).Returns(false);
            _managerRepositoryMock.Setup(repository => repository.EmailExistsInManagers(admin.Email)).Returns(false);
            _maintenanceStaffRepositoryMock.Setup(repository => repository.EmailExistsInMaintenanceStaff(admin.Email)).Returns(true);

            // Act
            _adminLogic.CreateAdmin(admin);

            // Assert - Expects EmailAlreadyExistsException
            _adminRepositoryMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();
            _maintenanceStaffRepositoryMock.VerifyAll();
            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(EmailAlreadyExistsException))]
        public void CreateAdmin_EmailExistsInInvitations_ThrowsException()
        {
            // Arrange
            Admin admin = new Admin
            {
                AdminID = Guid.NewGuid(),
                FirstName = "Juan",
                LastName = "Perez",
                Email = "juan.perez@example.com",
                Password = "securePassword123"
            };

            _adminRepositoryMock.Setup(repository => repository.EmailExistsInAdmins(admin.Email)).Returns(false);
            _managerRepositoryMock.Setup(repository => repository.EmailExistsInManagers(admin.Email)).Returns(false);
            _maintenanceStaffRepositoryMock.Setup(repository => repository.EmailExistsInMaintenanceStaff(admin.Email)).Returns(false);
            _invitationRepositoryMock.Setup(repository => repository.EmailExistsInInvitations(admin.Email)).Returns(true);

            // Act
            _adminLogic.CreateAdmin(admin);

            // Assert - Expects EmailAlreadyExistsException
            _adminRepositoryMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();
            _maintenanceStaffRepositoryMock.VerifyAll();
            _invitationRepositoryMock.VerifyAll();
        }
    }
}
