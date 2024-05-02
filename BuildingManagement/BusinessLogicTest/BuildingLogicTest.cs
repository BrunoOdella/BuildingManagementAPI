using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain;
using LogicInterface.Interfaces;
using IDataAccess;
using System;
using BusinessLogic.Logics;

namespace BusinessLogicTest
{
    [TestClass]
    public class BuildingLogicTest
    {
        private BuildingLogic _buildingLogic;
        private Mock<IBuildingRepository> _buildingRepositoryMock;

        [TestInitialize]
        public void Setup()
        {
            _buildingRepositoryMock = new Mock<IBuildingRepository>();
            _buildingLogic = new BuildingLogic(_buildingRepositoryMock.Object);
        }

        [TestMethod]
        public void CreateBuilding_WithValidData_ReturnsBuilding()
        {
            // Arrange
            string managerId = Guid.NewGuid().ToString();
            Building building = new Building
            {
                Name = "Test Building",
                Address = "123 Test Ave",
                Apartments = new List<Apartment>()
                {
                    new Apartment()
                }
            };
            _buildingRepositoryMock.Setup(repo => repo.CreateBuilding(building)).Returns(building);

            // Act
            Building result = _buildingLogic.CreateBuilding(managerId, building);

            // Assert
            Assert.IsNotNull(result);
            _buildingRepositoryMock.Verify(repo => repo.CreateBuilding(It.IsAny<Building>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBuilding_WithInvalidManagerId_ThrowsException()
        {
            // Arrange
            string invalidManagerId = "invalid-guid";
            Building building = new Building(); 

            // Act
            _buildingLogic.CreateBuilding(invalidManagerId, building);

            // no hay hacer ya que ExpectedException
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteBuilding_InvalidManagerId_ThrowsArgumentException()
        {
            // Arrange
            string invalidManagerId = "not-a-guid";

            // Act
            _buildingLogic.DeleteBuilding(invalidManagerId, Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void DeleteBuilding_UnauthorizedManager_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var managerId = Guid.NewGuid().ToString();
            var buildingId = Guid.NewGuid();
            _buildingRepositoryMock.Setup(r => r.GetBuilding(Guid.Parse(managerId), buildingId))
                .Returns((Building)null); // No building found

            // Act
            _buildingLogic.DeleteBuilding(managerId, buildingId);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteBuilding_FailureToDelete_ThrowsInvalidOperationException()
        {
            // Arrange
            var managerId = Guid.NewGuid().ToString();
            var buildingId = Guid.NewGuid();
            var building = new Building { BuildingId = buildingId, ManagerId = Guid.Parse(managerId) };
            _buildingRepositoryMock.Setup(r => r.GetBuilding(Guid.Parse(managerId), buildingId)).Returns(building);
            _buildingRepositoryMock.Setup(r => r.DeleteBuilding(buildingId)).Returns(false); // Fail to delete

            // Act
            _buildingLogic.DeleteBuilding(managerId, buildingId);
        }

        [TestMethod]
        public void DeleteBuilding_SuccessfulDeletion_CompletesWithoutException()
        {
            // Arrange
            var managerId = Guid.NewGuid().ToString();
            var buildingId = Guid.NewGuid();
            var building = new Building { BuildingId = buildingId, ManagerId = Guid.Parse(managerId) };
            _buildingRepositoryMock.Setup(r => r.GetBuilding(Guid.Parse(managerId), buildingId)).Returns(building);
            _buildingRepositoryMock.Setup(r => r.DeleteBuilding(buildingId)).Returns(true); // Successfully deleted

            // Act
            _buildingLogic.DeleteBuilding(managerId, buildingId); // No exception expected

            // Assert
            _buildingRepositoryMock.Verify(r => r.DeleteBuilding(buildingId), Times.Once);
            Assert.IsTrue(true); // If reached here, test passed
        }




        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateBuilding_InvalidManagerId_ThrowsArgumentException()
        {
            // Arrange
            string invalidManagerId = "invalid-guid";
            Building building = new Building { BuildingId = Guid.NewGuid() };

            // Act
            _buildingLogic.UpdateBuilding(invalidManagerId, building);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateBuilding_BuildingNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            string managerId = Guid.NewGuid().ToString();
            Building building = new Building { BuildingId = Guid.NewGuid() };

            _buildingRepositoryMock.Setup(r => r.GetBuilding(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns((Building)null);

            // Act
            _buildingLogic.UpdateBuilding(managerId, building);
        }

        [TestMethod]
        public void UpdateBuilding_ValidRequest_UpdatesBuilding()
        {
            // Arrange
            string managerId = Guid.NewGuid().ToString();
            Guid buildingId = Guid.NewGuid();
            Building building = new Building
            {
                BuildingId = buildingId,
                Name = "Updated Name",
                Address = "Updated Address",
                Location = new Location { Latitude = 40.0, Longitude = -74.0 },
                ConstructionCompany = "New Company",
                CommonExpenses = 500
            };

            Building existingBuilding = new Building
            {
                BuildingId = buildingId,
                ManagerId = Guid.Parse(managerId),
                Location = new Location { Latitude = 35.0, Longitude = -75.0 } // Asegurarse de inicializar Location aquí también
            };

            _buildingRepositoryMock.Setup(r => r.GetBuilding(Guid.Parse(managerId), buildingId)).Returns(existingBuilding);
            _buildingRepositoryMock.Setup(r => r.UpdateBuilding(It.IsAny<Building>())).Returns(existingBuilding);

            // Act
            var result = _buildingLogic.UpdateBuilding(managerId, building);

            // Assert
            Assert.AreEqual("Updated Name", result.Name);
            Assert.AreEqual("Updated Address", result.Address);
            Assert.AreEqual(40.0, result.Location.Latitude);
            Assert.AreEqual(-74.0, result.Location.Longitude);
            Assert.AreEqual("New Company", result.ConstructionCompany);
            Assert.AreEqual(500, result.CommonExpenses);
        }


        [TestMethod]
        public void UpdateBuilding_PartialUpdate_UpdatesOnlySpecifiedFields()
        {
            // Arrange
            string managerId = Guid.NewGuid().ToString();
            Guid buildingId = Guid.NewGuid();
            Building building = new Building
            {
                BuildingId = buildingId,
                Name = "Partially Updated Name"
                // Address and other fields are not provided and should not be updated
            };
            Building existingBuilding = new Building
            {
                BuildingId = buildingId,
                ManagerId = Guid.Parse(managerId),
                Name = "Old Name",
                Address = "Old Address"
            };

            _buildingRepositoryMock.Setup(r => r.GetBuilding(Guid.Parse(managerId), buildingId)).Returns(existingBuilding);
            _buildingRepositoryMock.Setup(r => r.UpdateBuilding(It.IsAny<Building>())).Returns(existingBuilding);

            // Act
            var result = _buildingLogic.UpdateBuilding(managerId, building);

            // Assert
            Assert.AreEqual("Partially Updated Name", result.Name);
            Assert.AreEqual("Old Address", result.Address);  // Should not be updated
        }


    }
}
