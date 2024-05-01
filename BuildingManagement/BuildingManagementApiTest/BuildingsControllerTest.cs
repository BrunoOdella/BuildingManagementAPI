﻿using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuildingManagementApi.Controllers;
using LogicInterface.Interfaces;
using Models.In;
using Models.Out;
using Domain;

namespace BuildingManagementApiTest
{
    [TestClass]
    public class BuildingsControllerTest
    {
        private Mock<IBuildingLogic> _buildingLogicMock;
        private BuildingsController _buildingsController;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;

        [TestInitialize]
        public void Setup()
        {
            _buildingLogicMock = new Mock<IBuildingLogic>(MockBehavior.Strict);
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>(MockBehavior.Strict);
            var httpContext = new DefaultHttpContext();
            Guid expectedUserID = Guid.NewGuid();
            httpContext.Items["userID"] = expectedUserID.ToString();
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);
            _buildingsController = new BuildingsController(_buildingLogicMock.Object, _httpContextAccessorMock.Object);
        }

        [TestMethod]
        public void CreateBuilding_ReturnsCreatedResponse_WhenBuildingIsSuccessfullyCreated()
        {
            // Arrange
            string userIDString = _httpContextAccessorMock.Object.HttpContext.Items["userID"] as string;
            var request = new CreateBuildingRequest
            {
                Name = "New Building",
                Address = "123 Main St",
                Latitude = 45.0,
                Longitude = -75.0,
                ConstructionCompany = "Empresa 1",
                CommonExpenses = 500,
                Apartments = new List<ApartmentData>()
            };

            var building = request.ToEntity();

            _buildingLogicMock.Setup(l => l.CreateBuilding(userIDString, It.IsAny<Building>()))
                              .Returns(building);

            // Act
            var result = _buildingsController.CreateBuilding(request) as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            var response = result.Value as BuildingResponse;
            Assert.IsNotNull(response);
            Assert.AreEqual(building.BuildingId, response.BuildingId);
            _buildingLogicMock.Verify(x => x.CreateBuilding(userIDString, It.IsAny<Building>()), Times.Once);
            _buildingLogicMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteBuilding_ReturnsNoContent_WhenDeletionIsSuccessful()
        {
            // Arrange
            var buildingId = Guid.NewGuid();
            string userIDString = _httpContextAccessorMock.Object.HttpContext.Items["userID"] as string;
            _buildingLogicMock.Setup(l => l.DeleteBuilding(userIDString, buildingId)).Verifiable();

            // Act
            var result = _buildingsController.DeleteBuilding(buildingId) as NoContentResult;

            // Assert
            _buildingLogicMock.Verify(l => l.DeleteBuilding(userIDString, buildingId), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }


    }

}