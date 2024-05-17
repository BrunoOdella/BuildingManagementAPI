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
    public class MaintenanceStaffLogicTest
    {
        private Mock<IMaintenanceStaffRepository> _maintenanceStaffRepositoryMock;
        private Mock<IBuildingRepository> _buildingRepositoryMock;
        private Mock<IAdminRepository> _adminRepositoryMock;
        private Mock<IManagerRepository> _managerRepositoryMock;
        private Mock<IInvitationRepository> _invitationRepositoryMock;
        private MaintenanceStaffLogic _maintenanceStaffLogic;

        [TestInitialize]
        public void TestSetup()
        {
            _maintenanceStaffRepositoryMock = new Mock<IMaintenanceStaffRepository>(MockBehavior.Strict);
            _buildingRepositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            _adminRepositoryMock = new Mock<IAdminRepository>(MockBehavior.Strict);
            _managerRepositoryMock = new Mock<IManagerRepository>(MockBehavior.Strict);
            _invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);

            _maintenanceStaffLogic = new MaintenanceStaffLogic(
                _maintenanceStaffRepositoryMock.Object,
                _buildingRepositoryMock.Object,
                _adminRepositoryMock.Object,
                _managerRepositoryMock.Object,
                _invitationRepositoryMock.Object);
        }

        [TestMethod]
        public void AddMaintenanceStaff_ValidData_AddsStaff()
        {
            // Arrange
            var managerId = Guid.NewGuid().ToString();
            var maintenanceStaff = new MaintenanceStaff
            {
                Name = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                BuildingId = Guid.NewGuid()
            };
            var building = new Building
            {
                BuildingId = maintenanceStaff.BuildingId,
                ManagerId = Guid.Parse(managerId)
            };

            _buildingRepositoryMock.Setup(repo => repo.GetBuilding(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(building);
            _adminRepositoryMock.Setup(repo => repo.EmailExistsInAdmins(maintenanceStaff.Email)).Returns(false);
            _managerRepositoryMock.Setup(repo => repo.EmailExistsInManagers(maintenanceStaff.Email)).Returns(false);
            _maintenanceStaffRepositoryMock.Setup(repo => repo.EmailExistsInMaintenanceStaff(maintenanceStaff.Email)).Returns(false);
            _invitationRepositoryMock.Setup(repo => repo.EmailExistsInInvitations(maintenanceStaff.Email)).Returns(false);
            _maintenanceStaffRepositoryMock.Setup(repo => repo.AddMaintenanceStaff(It.IsAny<MaintenanceStaff>())).Returns(maintenanceStaff);

            // Act
            var result = _maintenanceStaffLogic.AddMaintenanceStaff(managerId, maintenanceStaff);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(maintenanceStaff.Email, result.Email);
            _buildingRepositoryMock.VerifyAll();
            _adminRepositoryMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();
            _maintenanceStaffRepositoryMock.VerifyAll();
            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddMaintenanceStaff_InvalidManagerId_ThrowsException()
        {
            // Arrange
            var managerId = "invalid-guid";
            var maintenanceStaff = new MaintenanceStaff();

            // Act
            _maintenanceStaffLogic.AddMaintenanceStaff(managerId, maintenanceStaff);

            // Assert - Expects ArgumentException
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddMaintenanceStaff_BuildingNotFound_ThrowsException()
        {
            // Arrange
            var managerId = Guid.NewGuid().ToString();
            var maintenanceStaff = new MaintenanceStaff
            {
                BuildingId = Guid.NewGuid()
            };

            _buildingRepositoryMock.Setup(repo => repo.GetBuilding(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns((Building)null);

            // Act
            _maintenanceStaffLogic.AddMaintenanceStaff(managerId, maintenanceStaff);

            // Assert - Expects InvalidOperationException
            _buildingRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void AddMaintenanceStaff_ManagerNotAuthorized_ThrowsException()
        {
            // Arrange
            var managerId = Guid.NewGuid().ToString();
            var maintenanceStaff = new MaintenanceStaff
            {
                BuildingId = Guid.NewGuid()
            };
            var building = new Building
            {
                BuildingId = maintenanceStaff.BuildingId,
                ManagerId = Guid.NewGuid() // Different manager ID
            };

            _buildingRepositoryMock.Setup(repo => repo.GetBuilding(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(building);

            // Act
            _maintenanceStaffLogic.AddMaintenanceStaff(managerId, maintenanceStaff);

            // Assert - Expects UnauthorizedAccessException
            _buildingRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddMaintenanceStaff_MissingNameOrLastName_ThrowsException()
        {
            // Arrange
            var managerId = Guid.NewGuid().ToString();
            var maintenanceStaff = new MaintenanceStaff
            {
                BuildingId = Guid.NewGuid(),
                Name = "", // Missing name
                LastName = "Doe"
            };
            var building = new Building
            {
                BuildingId = maintenanceStaff.BuildingId,
                ManagerId = Guid.Parse(managerId)
            };

            _buildingRepositoryMock.Setup(repo => repo.GetBuilding(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(building);

            // Act
            _maintenanceStaffLogic.AddMaintenanceStaff(managerId, maintenanceStaff);

            // Assert - Expects ArgumentException
            _buildingRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(EmailAlreadyExistsException))]
        public void AddMaintenanceStaff_EmailAlreadyExists_ThrowsException()
        {
            // Arrange
            var managerId = Guid.NewGuid().ToString();
            var maintenanceStaff = new MaintenanceStaff
            {
                BuildingId = Guid.NewGuid(),
                Name = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };
            var building = new Building
            {
                BuildingId = maintenanceStaff.BuildingId,
                ManagerId = Guid.Parse(managerId)
            };

            _buildingRepositoryMock.Setup(repo => repo.GetBuilding(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(building);
            _adminRepositoryMock.Setup(repo => repo.EmailExistsInAdmins(maintenanceStaff.Email)).Returns(true);

            // Act
            _maintenanceStaffLogic.AddMaintenanceStaff(managerId, maintenanceStaff);

            // Assert - Expects EmailAlreadyExistsException
            _buildingRepositoryMock.VerifyAll();
            _adminRepositoryMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();
            _maintenanceStaffRepositoryMock.VerifyAll();
            _invitationRepositoryMock.VerifyAll();
        }
    }
}
