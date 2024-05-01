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
    }
}
