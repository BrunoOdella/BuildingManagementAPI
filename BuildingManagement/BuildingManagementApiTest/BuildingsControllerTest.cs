using Moq;
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

        [TestMethod]
        public void UpdateBuilding_ReturnsOkResponse_WhenBuildingIsSuccessfullyUpdated()
        {
            // Arrange
            var buildingId = Guid.NewGuid();
            string managerId = _httpContextAccessorMock.Object.HttpContext.Items["userID"] as string;
            var request = new UpdateBuildingRequest
            {
                Name = "Updated Building",
                Address = "456 New St",
                Latitude = 40.7128,
                Longitude = -74.0060,
                CommonExpenses = 750,
                ManagerGuid = "9EF9B0D8-45CE-42FB-0067-08DC811FD951"
            };

            var building = request.ToEntity();
            building.BuildingId = buildingId;

            //_buildingLogicMock.Setup(l => l.UpdateBuilding(managerId, It.IsAny<Building>()))
            //                  .Returns(building);

            // Act
            var result = _buildingsController.UpdateBuilding(buildingId, request) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var response = result.Value as BuildingResponse;
            Assert.IsNotNull(response);
            Assert.AreEqual(building.BuildingId, response.BuildingId);
            //_buildingLogicMock.Verify(x => x.UpdateBuilding(managerId, It.IsAny<Building>()), Times.Once);
            _buildingLogicMock.VerifyAll();
        }


        [TestMethod]
        public void GetBuildings_ReturnsOkResponse_WithListOfBuildings()
        {
            string adminId = Guid.NewGuid().ToString();
            _httpContextAccessorMock.Setup(a => a.HttpContext.Items["userID"]).Returns(adminId);

            var buildings = new List<Building>
            {
                new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Building 1",
                    Address = "123 Main St",
                    Manager = new Manager { Name = "Manager 1" }
                },
                new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Building 2",
                    Address = "456 Oak St",
                    Manager = null
                }
            };

            //_buildingLogicMock.Setup(l => l.GetBuildingsByConstructionCompanyAdminId(adminId)).Returns(buildings);

            var result = _buildingsController.GetBuildings() as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var response = result.Value as List<BuildingResponse>;
            Assert.IsNotNull(response);
            Assert.AreEqual(2, response.Count);
            Assert.AreEqual("Building 1", response[0].Name);
            Assert.AreEqual("Manager 1", response[0].ManagerName);
            Assert.AreEqual("Building 2", response[1].Name);
            Assert.AreEqual("No Manager Assigned", response[1].ManagerName);

            //_buildingLogicMock.Verify(x => x.GetBuildingsByConstructionCompanyAdminId(adminId), Times.Once);
            _buildingLogicMock.VerifyAll();
        }
    }

}