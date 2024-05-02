using BusinessLogic.Logics;
using Domain;
using IDataAccess;
using Moq;

namespace BusinessLogicTest;

[TestClass]
public class ConcreteReportFactory_RequestByMaintenanceStaffTest
{
    private Mock<IMaintenanceStaffRepository> _staffReposotoryMock;
    private Mock<IRequestRepository> _requestRepositoryMock;
    private ConcreteReportFactory_RequestByMaintenanceStaff _requestByBuilding;
    private Guid _managerID;


    [TestInitialize]
    public void TestSetup()
    {
        _staffReposotoryMock = new Mock<IMaintenanceStaffRepository>(MockBehavior.Strict);
        _requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
        _requestByBuilding = new ConcreteReportFactory_RequestByMaintenanceStaff(_staffReposotoryMock.Object, _requestRepositoryMock.Object);
        _managerID = Guid.NewGuid();
    }

    [TestMethod]
    public void RequestByBuilding_Succes()
    {
        MaintenanceStaff person = new MaintenanceStaff()
        {
            Requests = new List<Request_>(),
            Name = "nombre"
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
        };

        Request_ request2 = new Request_()
        {
            CategoryID = 1,
            CreationTime = DateTime.Now.AddDays(-2),
            Description = "description b",
            Id = Guid.NewGuid(),
            Status = Status.Pending,
            MaintenanceStaff = new MaintenanceStaff(),
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
        };

        person.Requests.Add(request);
        person.Requests.Add(request2);
        person.Requests.Add(request3);

        IEnumerable<MaintenanceStaff> people = new List<MaintenanceStaff>() { person };

        _staffReposotoryMock.Setup(repository => repository.GetAll(It.IsAny<Guid>())).Returns(people);

        Report result = _requestByBuilding.RequestByMaintenanceStaff(_managerID);
        var lines = result.MaintenanceStaffReports;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.MaintenanceStaffReports[0].AverageCompletionTimeInHours);
        Assert.AreEqual(lines.Count, 1);
        Assert.AreEqual(lines[0].CompletedRequests, 1);
        Assert.AreEqual(lines[0].StaffName, "nombre");

        _staffReposotoryMock.VerifyAll();
    }

    [TestMethod]
    public void RequestByBuilding_SpecificBuilding_Succes()
    {

        Guid maintenanceStaffId = Guid.NewGuid();

        MaintenanceStaff person = new MaintenanceStaff()
        {
            Requests = new List<Request_>(),
            Name = "nombre"
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
        };

        Request_ request2 = new Request_()
        {
            CategoryID = 1,
            CreationTime = DateTime.Now.AddDays(-2),
            Description = "description b",
            Id = Guid.NewGuid(),
            Status = Status.Pending,
            MaintenanceStaff = new MaintenanceStaff(),
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
        };

        person.Requests.Add(request);
        person.Requests.Add(request2);
        person.Requests.Add(request3);

        _staffReposotoryMock.Setup(repository => repository.GetMaintenanceStaff(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(person);

        Report result = _requestByBuilding.RequestByMaintenanceStaff(_managerID, maintenanceStaffId);
        var lines = result.MaintenanceStaffReports;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.MaintenanceStaffReports[0].AverageCompletionTimeInHours);
        Assert.AreEqual(lines.Count, 1);
        Assert.AreEqual(lines[0].CompletedRequests, 1);
        Assert.AreEqual(lines[0].StaffName, "nombre");

        _staffReposotoryMock.VerifyAll();
    }

    [TestMethod]
    public void RequestByBuilding_Succes_WithNoRequest()
    {
        MaintenanceStaff person = new MaintenanceStaff()
        {
            Requests = new List<Request_>(),
            Name = "nombre"
        };

        IEnumerable<MaintenanceStaff> people = new List<MaintenanceStaff>() { person };

        _staffReposotoryMock.Setup(repository => repository.GetAll(It.IsAny<Guid>())).Returns(people);

        Report result = _requestByBuilding.RequestByMaintenanceStaff(_managerID);
        var lines = result.MaintenanceStaffReports;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.MaintenanceStaffReports[0].AverageCompletionTimeInHours);
        Assert.AreEqual(lines.Count, 1);
        Assert.AreEqual(lines[0].CompletedRequests, 0);
        Assert.AreEqual(lines[0].StaffName, "nombre");

        _staffReposotoryMock.VerifyAll();
    }

    [TestMethod]
    public void RequestByBuilding_SpecificBuilding_Succes_WithNoRequest()
    {

        Guid maintenanceStaffId = Guid.NewGuid();

        MaintenanceStaff person = new MaintenanceStaff()
        {
            Requests = new List<Request_>(),
            Name = "nombre"
        };

        _staffReposotoryMock.Setup(repository => repository.GetMaintenanceStaff(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(person);

        Report result = _requestByBuilding.RequestByMaintenanceStaff(_managerID, maintenanceStaffId);
        var lines = result.MaintenanceStaffReports;

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.MaintenanceStaffReports[0].AverageCompletionTimeInHours);
        Assert.AreEqual(lines.Count, 1);
        Assert.AreEqual(lines[0].CompletedRequests, 0);
        Assert.AreEqual(lines[0].StaffName, "nombre");

        _staffReposotoryMock.VerifyAll();
    }
}