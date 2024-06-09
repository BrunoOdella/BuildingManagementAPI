using BuildingManagementApi.Controllers;
using Domain;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Out;
using Moq;

namespace BuildingManagementApiTest;


[TestClass]
public class ReportControllerTest
{
    private Mock<IReportLogicByMaintenanceStaff> _reportLogicStaffMock;
    private Mock<IReportLogicByBuilding> _reportLogicBuildingMock;
    private Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private ReportsController _reportsController;

    [TestInitialize]
    public void TestSetup()
    {
        _reportLogicStaffMock = new Mock<IReportLogicByMaintenanceStaff>(MockBehavior.Strict);
        _reportLogicBuildingMock = new Mock<IReportLogicByBuilding>(MockBehavior.Strict);

        _httpContextAccessorMock = new Mock<IHttpContextAccessor>(MockBehavior.Strict);

        var httpContext = new DefaultHttpContext();
        Guid expectedUserID = Guid.NewGuid(); // o cualquier Guid que esperes
        httpContext.Items["userID"] = expectedUserID.ToString();
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        // Crear el controlador con los mocks
        _reportsController = new ReportsController(_reportLogicStaffMock.Object, _reportLogicBuildingMock.Object, _httpContextAccessorMock.Object);
    }

    [TestMethod]
    public void GetReport_RequestByBuilding_Succes()
    {
        Report report = new Report();

        var buildingID = Guid.NewGuid();

        BuildingReport buildingReport = new BuildingReport()
        {
            ActiveRequests = 3,
            BuildingName = "edificio",
            CompletedRequests = 2,
            PendingRequests = 0
        };

        report.BuildingReports = new List<BuildingReport>()
        {
            buildingReport
        };


        var response = new Report_RequestByBuildingResponse(report);

        _reportLogicBuildingMock.Setup(l => l.RequestByBuilding(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(report);

        ObjectResult result = _reportsController.GetReport_RequestByBuilding(buildingID.ToString());

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        _reportLogicBuildingMock.VerifyAll();
    }

    [TestMethod]
    public void GetReport_RequestByBuilding_WithOutBuildingID_Succes()
    {
        Report report = new Report();

        BuildingReport buildingReport = new BuildingReport()
        {
            ActiveRequests = 3,
            BuildingName = "edificio",
            CompletedRequests = 2,
            PendingRequests = 0
        };

        report.BuildingReports = new List<BuildingReport>()
        {
            buildingReport
        };


        var response = new Report_RequestByBuildingResponse(report);

        _reportLogicBuildingMock.Setup(l => l.RequestByBuilding(It.IsAny<Guid>())).Returns(report);

        ObjectResult result = _reportsController.GetReport_RequestByBuilding(null);

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        _reportLogicBuildingMock.VerifyAll();
    }

    [TestMethod]
    public void GetReport_RequestByMaintenanceStaff_Succes()
    {
        Report report = new Report();

        var buildingID = Guid.NewGuid();

        BuildingReport buildingReport = new BuildingReport()
        {
            ActiveRequests = 3,
            BuildingName = "edificio",
            CompletedRequests = 2,
            PendingRequests = 0
        };

        report.BuildingReports = new List<BuildingReport>()
        {
            buildingReport
        };


        var response = new Report_RequestByBuildingResponse(report);

        _reportLogicStaffMock.Setup(l => l.RequestByMaintenanceStaff(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(report);

        ObjectResult result = _reportsController.GetReport_RequestByMaintenanceStaff(buildingID.ToString());

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        _reportLogicStaffMock.VerifyAll();
    }

    [TestMethod]
    public void GetReport_RequestByMaintenanceStaff_WithOutBuildingID_Succes()
    {
        Report report = new Report();

        BuildingReport buildingReport = new BuildingReport()
        {
            ActiveRequests = 3,
            BuildingName = "edificio",
            CompletedRequests = 2,
            PendingRequests = 0
        };

        report.BuildingReports = new List<BuildingReport>()
        {
            buildingReport
        };


        var response = new Report_RequestByBuildingResponse(report);

        _reportLogicStaffMock.Setup(l => l.RequestByMaintenanceStaff(It.IsAny<Guid>())).Returns(report);

        ObjectResult result = _reportsController.GetReport_RequestByMaintenanceStaff(null);

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        _reportLogicStaffMock.VerifyAll();
    }

}