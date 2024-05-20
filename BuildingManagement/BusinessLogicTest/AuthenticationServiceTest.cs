using BusinessLogic.Logics;
using Domain;
using IDataAccess;
using Moq;

namespace BusinessLogicTest;

[TestClass]
public class AuthenticationServiceTest
{
    private Mock<IManagerRepository> _managerRepositoryMock;
    private Mock<IMaintenanceStaffRepository> _maintenanceStaffRepositoryMock;
    private Mock<IAdminRepository> _adminRepositoryMock;
    private AuthenticationService _authenticationServiceLogic;

    [TestInitialize]
    public void TestSetup()
    {
        _managerRepositoryMock = new Mock<IManagerRepository>(MockBehavior.Strict);
        _maintenanceStaffRepositoryMock = new Mock<IMaintenanceStaffRepository>(MockBehavior.Strict);
        _adminRepositoryMock = new Mock<IAdminRepository>(MockBehavior.Strict);
        _authenticationServiceLogic = new AuthenticationService(_managerRepositoryMock.Object, _maintenanceStaffRepositoryMock.Object, _adminRepositoryMock.Object);
    }

    [TestMethod]
    public void BuscarToken()
    {
        Guid token = Guid.NewGuid();
        string uri = "request";
        string verbo = "GET";

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(token);
        _maintenanceStaffRepositoryMock.Setup(r => r.GetMaintenanceStaff(token)).Returns(Guid.Empty);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _maintenanceStaffRepositoryMock.VerifyAll();
        _managerRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken2()
    {
        Guid token = Guid.NewGuid();
        string uri = "request";
        string verbo = "GET";

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(Guid.Empty);
        _maintenanceStaffRepositoryMock.Setup(r => r.GetMaintenanceStaff(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _maintenanceStaffRepositoryMock.VerifyAll();
        _managerRepositoryMock.VerifyAll();
    }


    [TestMethod]
    public void BuscarToken3()
    {
        Guid token = Guid.NewGuid();
        string uri = "request/finished";
        string verbo = "GET";

        _maintenanceStaffRepositoryMock.Setup(r => r.GetMaintenanceStaff(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _maintenanceStaffRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken4()
    {
        Guid token = Guid.NewGuid();
        string uri = "invitations";
        string verbo = "POST";

        //_managerRepositoryMock.Setup(r => r.Get(token)).Returns(Guid.Empty);
        //_maintenanceStaffRepositoryMock.Setup(r => r.GetMaintenanceStaff(token)).Returns(token);

        _adminRepositoryMock.Setup(r => r.Get(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);
    }

    [TestMethod]
    public void BuscarToken5()
    {
        Guid token = Guid.NewGuid();
        string uri = "invitations";
        string verbo = "PUT";

        //_managerRepositoryMock.Setup(r => r.Get(token)).Returns(Guid.Empty);
        //_maintenanceStaffRepositoryMock.Setup(r => r.GetMaintenanceStaff(token)).Returns(token);
        _adminRepositoryMock.Setup(r => r.Get(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        //_maintenanceStaffRepositoryMock.VerifyAll();
        //_managerRepositoryMock.VerifyAll();
        _adminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken6()
    {
        Guid token = Guid.NewGuid();
        string uri = "invitations";
        string verbo = "DELETE";

        //_managerRepositoryMock.Setup(r => r.Get(token)).Returns(Guid.Empty);
        //_maintenanceStaffRepositoryMock.Setup(r => r.GetMaintenanceStaff(token)).Returns(token);
        _adminRepositoryMock.Setup(r => r.Get(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        //_maintenanceStaffRepositoryMock.VerifyAll();
        //_managerRepositoryMock.VerifyAll();
        _adminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken7()
    {
        Guid token = Guid.NewGuid();
        string uri = "categoriesrequests";
        string verbo = "";

        //_managerRepositoryMock.Setup(r => r.Get(token)).Returns(Guid.Empty);
        //_maintenanceStaffRepositoryMock.Setup(r => r.GetMaintenanceStaff(token)).Returns(token);
        _adminRepositoryMock.Setup(r => r.Get(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        //_maintenanceStaffRepositoryMock.VerifyAll();
        //_managerRepositoryMock.VerifyAll();
        _adminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken8()
    {
        Guid token = Guid.NewGuid();
        string uri = "admins";
        string verbo = "";

        //_managerRepositoryMock.Setup(r => r.Get(token)).Returns(Guid.Empty);
        //_maintenanceStaffRepositoryMock.Setup(r => r.GetMaintenanceStaff(token)).Returns(token);
        _adminRepositoryMock.Setup(r => r.Get(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        //_maintenanceStaffRepositoryMock.VerifyAll();
        //_managerRepositoryMock.VerifyAll();
        _adminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken9()
    {
        Guid token = Guid.NewGuid();
        string uri = "buildings";
        string verbo = "";

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(token);
        //_maintenanceStaffRepositoryMock.Setup(r => r.GetMaintenanceStaff(token)).Returns(token);
        //_adminRepositoryMock.Setup(r => r.Get(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        //_maintenanceStaffRepositoryMock.VerifyAll();
        _managerRepositoryMock.VerifyAll();
        //_adminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken10()
    {
        Guid token = Guid.NewGuid();
        string uri = "reports";
        string verbo = "";

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(token);
        //_maintenanceStaffRepositoryMock.Setup(r => r.GetMaintenanceStaff(token)).Returns(token);
        //_adminRepositoryMock.Setup(r => r.Get(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        //_maintenanceStaffRepositoryMock.VerifyAll();
        _managerRepositoryMock.VerifyAll();
        //_adminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken11()
    {
        Guid token = Guid.NewGuid();
        string uri = "request";
        string verbo = "POST";

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(token);
        //_maintenanceStaffRepositoryMock.Setup(r => r.GetMaintenanceStaff(token)).Returns(Guid.Empty);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        //_maintenanceStaffRepositoryMock.VerifyAll();
        _managerRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken12()
    {
        Guid token = Guid.NewGuid();
        string uri = "request";
        string verbo = "PUT";

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(token);
        //_maintenanceStaffRepositoryMock.Setup(r => r.GetMaintenanceStaff(token)).Returns(Guid.Empty);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        //_maintenanceStaffRepositoryMock.VerifyAll();
        _managerRepositoryMock.VerifyAll();
    }

}