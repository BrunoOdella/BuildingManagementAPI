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
        private DefaultHttpContext _httpContext;

        [TestInitialize]
        public void Setup()
        {
            _buildingLogicMock = new Mock<IBuildingLogic>(MockBehavior.Strict);
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _httpContext = new DefaultHttpContext();
            _httpContext.Request.Headers["Authorization"] = Guid.NewGuid().ToString();

            _httpContextAccessorMock.Setup(_ => _.HttpContext).Returns(_httpContext);
            _buildingsController = new BuildingsController(_buildingLogicMock.Object, _httpContextAccessorMock.Object);
        }

        [TestMethod]
        public void CreateBuilding_ReturnsCreatedResponse_WhenBuildingIsSuccessfullyCreated()
        {
            // Arrange
            string managerId = _httpContext.Request.Headers["Authorization"];
            var request = new CreateBuildingRequest
            {
                Name = "New Building",
                Address = "123 Main St",
                Latitude = 45.0,
                Longitude = -75.0,
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 500,
                Apartments = new List<ApartmentData>()
            };

            var building = request.ToEntity();

            _buildingLogicMock.Setup(l => l.CreateBuilding(managerId, It.IsAny<Building>()))
                              .Returns(building);

            // Act
            var result = _buildingsController.CreateBuilding(request) as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            var response = result.Value as BuildingResponse;
            Assert.IsNotNull(response);
            Assert.AreEqual(building.BuildingId, response.BuildingId);
            _buildingLogicMock.Verify(x => x.CreateBuilding(managerId, It.IsAny<Building>()), Times.Once);
            _buildingLogicMock.VerifyAll();
        }
    }

}