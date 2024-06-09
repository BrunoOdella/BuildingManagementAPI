using BusinessLogic.Logics;
using Domain;
using IDataAccess;
using Moq;

namespace BusinessLogicTest;


[TestClass]
public class ConcreteReportFactory_RequestByBuildingTest
{
    private Mock<IBuildingRepository> _buildingMock;
    private Mock<IRequestRepository> _requestRepositoryMock;
    private ConcreteReportFactory_RequestByBuilding _requestByBuilding;
    private Guid _managerID;


    [TestInitialize]
    public void TestSetup()
    {
        _buildingMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
        _requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
        _requestByBuilding = new ConcreteReportFactory_RequestByBuilding(_buildingMock.Object, _requestRepositoryMock.Object);
        _managerID = Guid.NewGuid();
    }

    [TestMethod]
    public void RequestByBuilding_Succes()
    {

        Building building = new Building()
        {
            BuildingId = Guid.NewGuid(),
            Name = "edificio",
            Apartments = new List<Apartment>()
        };

        Apartment apartment = new Apartment()
        {
            BuildingId = building.BuildingId,
            Requests = new List<Request_>()
        };

        Request_ request = new Request_()
        {
            CategoryID = 1,
            CreationTime = DateTime.Now.AddDays(-2),
            Description = "description A",
            EndTime = DateTime.Now,
            Id = Guid.NewGuid(),
            StartTime = DateTime.Now.AddDays(-1),
            Status = Status.Finished,
            TotalCost = 1000,
            MaintenanceStaff = new MaintenanceStaff(),
            Apartment = apartment
        };

        Request_ request2 = new Request_()
        {
            CategoryID = 1,
            CreationTime = DateTime.Now.AddDays(-2),
            Description = "description b",
            Id = Guid.NewGuid(),
            Status = Status.Pending,
            MaintenanceStaff = new MaintenanceStaff(),
            Apartment = apartment
        };

        Request_ request3 = new Request_()
        {
            CategoryID = 1,
            CreationTime = DateTime.Now.AddDays(-2),
            Description = "description c",
            Id = Guid.NewGuid(),
            StartTime = DateTime.Now.AddDays(-1),
            Status = Status.Active,
            MaintenanceStaff = new MaintenanceStaff(),
            Apartment = apartment
        };

        apartment.Requests.Add(request);
        apartment.Requests.Add(request2);
        apartment.Requests.Add(request3);

        building.Apartments.Add(apartment);

        IEnumerable<Building> buildings = new List<Building>() { building };

        _buildingMock.Setup(repository => repository.GetAll(It.IsAny<Guid>())).Returns(buildings);

        Report result = _requestByBuilding.RequestByBuilding(_managerID);
        var lines = result.BuildingReports;

        Assert.IsNotNull(result);
        Assert.AreEqual(lines.Count, 1);
        Assert.AreEqual(lines[0].CompletedRequests, 1);
        Assert.AreEqual(lines[0].BuildingName, "edificio");

        _buildingMock.VerifyAll();
    }

    [TestMethod]
    public void RequestByBuilding_SpecificBuilding_Succes()
    {

        Guid buildingID = Guid.NewGuid();

        Building building = new Building()
        {
            BuildingId = buildingID,
            Name = "edificio",
            Apartments = new List<Apartment>()
        };

        Apartment apartment = new Apartment()
        {
            BuildingId = building.BuildingId,
            Requests = new List<Request_>()
        };

        Request_ request = new Request_()
        {
            CategoryID = 1,
            CreationTime = DateTime.Now.AddDays(-2),
            Description = "description A",
            EndTime = DateTime.Now,
            Id = Guid.NewGuid(),
            StartTime = DateTime.Now.AddDays(-1),
            Status = Status.Finished,
            TotalCost = 1000,
            MaintenanceStaff = new MaintenanceStaff(),
            Apartment = apartment
        };

        Request_ request2 = new Request_()
        {
            CategoryID = 1,
            CreationTime = DateTime.Now.AddDays(-2),
            Description = "description b",
            Id = Guid.NewGuid(),
            Status = Status.Pending,
            MaintenanceStaff = new MaintenanceStaff(),
            Apartment = apartment
        };

        Request_ request3 = new Request_()
        {
            CategoryID = 1,
            CreationTime = DateTime.Now.AddDays(-2),
            Description = "description c",
            Id = Guid.NewGuid(),
            StartTime = DateTime.Now.AddDays(-1),
            Status = Status.Active,
            MaintenanceStaff = new MaintenanceStaff(),
            Apartment = apartment
        };

        apartment.Requests.Add(request);
        apartment.Requests.Add(request2);
        apartment.Requests.Add(request3);
        building.Apartments.Add(apartment);

        _buildingMock.Setup(repository => repository.GetBuilding(It.IsAny<Guid>(), buildingID)).Returns(building);

        Report result = _requestByBuilding.RequestByBuilding(_managerID, buildingID);
        var lines = result.BuildingReports;

        Assert.IsNotNull(result);
        Assert.AreEqual(lines.Count, 1);
        Assert.AreEqual(lines[0].CompletedRequests, 1);
        Assert.AreEqual(lines[0].BuildingName, "edificio");

        _buildingMock.VerifyAll();
    }

}