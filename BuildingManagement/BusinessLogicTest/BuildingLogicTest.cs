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
        public void DeleteBuilding_SuccessfulDeletion_Returns()
        {
            // Arrange
            string managerId = Guid.NewGuid().ToString();
            Guid buildingId = Guid.NewGuid();
            _buildingRepositoryMock.Setup(repo => repo.DeleteBuilding(buildingId)).Returns(true);

            // Act
            _buildingLogic.DeleteBuilding(managerId, buildingId);

            // Assert
            _buildingRepositoryMock.Verify(repo => repo.DeleteBuilding(buildingId), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteBuilding_BuildingNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            string managerId = Guid.NewGuid().ToString();
            Guid buildingId = Guid.NewGuid();
            _buildingRepositoryMock.Setup(repo => repo.DeleteBuilding(buildingId)).Returns(false);

            // Act
            _buildingLogic.DeleteBuilding(managerId, buildingId);

            // Assert is handled by ExpectedException
        }




        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateBuilding_WithInvalidManagerId_ThrowsArgumentException()
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

            _buildingRepositoryMock.Setup(r => r.GetBuildingById(building.BuildingId)).Returns((Building)null);

            // Act
            _buildingLogic.UpdateBuilding(managerId, building);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void UpdateBuilding_ManagerNotMatch_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            string managerId = Guid.NewGuid().ToString();
            Building building = new Building { BuildingId = Guid.NewGuid(), ManagerId = Guid.NewGuid() };
            Building existingBuilding = new Building { BuildingId = building.BuildingId, ManagerId = Guid.NewGuid() };

            _buildingRepositoryMock.Setup(r => r.GetBuildingById(building.BuildingId)).Returns(existingBuilding);

            // Act
            _buildingLogic.UpdateBuilding(managerId, building);
        }

        [TestMethod]
        public void UpdateBuilding_ValidRequest_UpdatesBuilding()
        {
            // Asegúrate de que todo esté correctamente inicializado
            string managerId = Guid.NewGuid().ToString();
            Building building = new Building
            {
                BuildingId = Guid.NewGuid(),
                Name = "Updated Name",
                Address = "Updated Address",
                Location = new Location { Latitude = 40.0, Longitude = -74.0 },
                ConstructionCompany = "New Company",
                CommonExpenses = 500,
                ManagerId = Guid.Parse(managerId)
            };

            Building existingBuilding = new Building
            {
                BuildingId = building.BuildingId,
                ManagerId = Guid.Parse(managerId),
                Location = new Location() // Asegúrate de que Location también está inicializado en existingBuilding
            };

            _buildingRepositoryMock.Setup(r => r.GetBuildingById(building.BuildingId)).Returns(existingBuilding);
            _buildingRepositoryMock.Setup(r => r.UpdateBuilding(It.IsAny<Building>())).Returns(existingBuilding);

            // Act
            var result = _buildingLogic.UpdateBuilding(managerId, building);

            // Assert
            Assert.IsNotNull(result);
            _buildingRepositoryMock.Verify(r => r.UpdateBuilding(It.IsAny<Building>()), Times.Once);
            Assert.AreEqual(building.Name, result.Name);
        }


    }
}
