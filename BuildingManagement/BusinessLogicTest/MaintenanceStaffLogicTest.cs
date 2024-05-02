using BusinessLogic.Logics;
using Domain;
using IDataAccess;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTest
{
    [TestClass]
    public class MaintenanceStaffLogicTest
    {
        private MaintenanceStaffLogic _maintenanceStaffLogic;
        private Mock<IMaintenanceStaffRepository> _maintenanceStaffRepositoryMock;
        private Mock<IBuildingRepository> _buildingRepositoryMock;

        [TestInitialize]
        public void Setup()
        {
            _maintenanceStaffRepositoryMock = new Mock<IMaintenanceStaffRepository>();
            _buildingRepositoryMock = new Mock<IBuildingRepository>();
            _maintenanceStaffLogic = new MaintenanceStaffLogic(_maintenanceStaffRepositoryMock.Object, _buildingRepositoryMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddMaintenanceStaff_InvalidManagerId_ThrowsArgumentException()
        {
            _maintenanceStaffLogic.AddMaintenanceStaff("invalid-guid", new MaintenanceStaff { BuildingId = Guid.NewGuid() });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddMaintenanceStaff_UnauthorizedManager_ThrowsException()
        {
            string managerId = Guid.NewGuid().ToString();
            var buildingId = Guid.NewGuid();
            _buildingRepositoryMock.Setup(x => x.GetBuilding(Guid.Parse(managerId), buildingId))
                .Returns((Building)null); // Simulating no building found

            _maintenanceStaffLogic.AddMaintenanceStaff(managerId, new MaintenanceStaff { BuildingId = buildingId });
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddMaintenanceStaff_InvalidMaintenanceStaffData_ThrowsArgumentException()
        {
            string managerId = Guid.NewGuid().ToString();
            var building = new Building { BuildingId = Guid.NewGuid(), ManagerId = Guid.Parse(managerId) };
            _buildingRepositoryMock.Setup(x => x.GetBuilding(Guid.Parse(managerId), building.BuildingId)).Returns(building);

            _maintenanceStaffLogic.AddMaintenanceStaff(managerId, new MaintenanceStaff { BuildingId = building.BuildingId });
        }

        [TestMethod]
        public void AddMaintenanceStaff_ValidData_AddsSuccessfully()
        {
            string managerId = Guid.NewGuid().ToString();
            var buildingId = Guid.NewGuid();
            var maintenanceStaff = new MaintenanceStaff
            {
                Name = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                BuildingId = buildingId
            };
            var building = new Building { BuildingId = buildingId, ManagerId = Guid.Parse(managerId) };

            _buildingRepositoryMock.Setup(x => x.GetBuilding(Guid.Parse(managerId), buildingId)).Returns(building);
            _maintenanceStaffRepositoryMock.Setup(x => x.AddMaintenanceStaff(It.IsAny<MaintenanceStaff>())).Returns(maintenanceStaff);

            var result = _maintenanceStaffLogic.AddMaintenanceStaff(managerId, maintenanceStaff);

            Assert.IsNotNull(result);
            _maintenanceStaffRepositoryMock.Verify(x => x.AddMaintenanceStaff(It.IsAny<MaintenanceStaff>()), Times.Once);
        }
    }
}
