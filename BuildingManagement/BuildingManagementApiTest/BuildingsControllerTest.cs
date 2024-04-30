using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using Domain;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuildingManagementApi.Controllers;
using LogicInterface.Interfaces;
using Models.In;
using Models.Out;

[TestClass]
public class BuildingsControllerTests
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
        _httpContext.Request.Headers["Authorization"] = "Guid-del-Manager";

        _httpContextAccessorMock.Setup(_ => _.HttpContext).Returns(_httpContext);

        _buildingsController = new BuildingsController(_buildingLogicMock.Object, _httpContextAccessorMock.Object);
    }

    [TestMethod]
    public void CreateBuilding_ReturnsCreatedResponse_WhenBuildingIsSuccessfullyCreated()
    {
        // Arrange
        Guid managerId = Guid.NewGuid();
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
        building.ManagerId = managerId;

        var buildingResponse = new BuildingResponse(building);

        _buildingLogicMock.Setup(x => x.CreateBuilding(managerId, building))
            .Returns(buildingResponse);

        // Act
        var result = _buildingsController.CreateBuilding(request) as CreatedResult;

        // Assert
        _buildingLogicMock.Verify(x => x.CreateBuilding(managerId, building), Times.Once);
        _buildingLogicMock.VerifyAll();
        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);
        Assert.AreEqual(buildingResponse, result.Value);
    }
}
