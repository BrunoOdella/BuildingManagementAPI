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
            };
            _buildingRepositoryMock.Setup(repo => repo.AddBuilding(building)).Returns(building);

            // Act
            Building result = _buildingLogic.CreateBuilding(managerId, building);

            // Assert
            Assert.IsNotNull(result);
            _buildingRepositoryMock.Verify(repo => repo.AddBuilding(It.IsAny<Building>()), Times.Once);
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
    }
}
