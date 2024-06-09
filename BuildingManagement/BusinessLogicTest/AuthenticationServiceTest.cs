using BusinessLogic.Logics;
using CustomExceptions;
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
    private Mock<IConstructionCompanyAdminRepository> _constructionCompanyAdminRepositoryMock;
    private AuthenticationService _authenticationServiceLogic;

    [TestInitialize]
    public void TestSetup()
    {
        _managerRepositoryMock = new Mock<IManagerRepository>(MockBehavior.Strict);
        _maintenanceStaffRepositoryMock = new Mock<IMaintenanceStaffRepository>(MockBehavior.Strict);
        _adminRepositoryMock = new Mock<IAdminRepository>(MockBehavior.Strict);
        _constructionCompanyAdminRepositoryMock = new Mock<IConstructionCompanyAdminRepository>(MockBehavior.Strict);
        _authenticationServiceLogic = new AuthenticationService(_managerRepositoryMock.Object,
            _maintenanceStaffRepositoryMock.Object, _adminRepositoryMock.Object,
            _constructionCompanyAdminRepositoryMock.Object);
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

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(Guid.Empty);
        _maintenanceStaffRepositoryMock.Setup(r => r.GetMaintenanceStaff(token)).Returns(token);

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

        _adminRepositoryMock.Setup(r => r.Get(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _managerRepositoryMock.VerifyAll();
        _adminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken6()
    {
        Guid token = Guid.NewGuid();
        string uri = "invitations";
        string verbo = "DELETE";

        _adminRepositoryMock.Setup(r => r.Get(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _adminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken7()
    {
        Guid token = Guid.NewGuid();
        string uri = "categoriesrequests";
        string verbo = "POST";

        _adminRepositoryMock.Setup(r => r.Get(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _adminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken7_5()
    {
        Guid token = Guid.NewGuid();
        string uri = "categoriesrequests";
        string verbo = "GET";

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _managerRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken8()
    {
        Guid token = Guid.NewGuid();
        string uri = "admins";
        string verbo = "";

        _adminRepositoryMock.Setup(r => r.Get(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _adminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken9_1()
    {
        Guid token = Guid.NewGuid();
        string uri = "buildings";
        string verbo = "PUT";

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(token);
        _constructionCompanyAdminRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(token))
            .Returns((ConstructionCompanyAdmin)null);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _constructionCompanyAdminRepositoryMock.VerifyAll();
        _adminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken9_1_2()
    {
        Guid token = Guid.NewGuid();
        string uri = "buildings";
        string verbo = "PUT";

        ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin()
        {
            Id = token
        };

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(Guid.Empty);
        _constructionCompanyAdminRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(token)).Returns(admin);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _constructionCompanyAdminRepositoryMock.VerifyAll();
        _adminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken9_2()
    {
        Guid token = Guid.NewGuid();
        string uri = "buildings";
        string verbo = "POST";

        ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin()
        {
            Id = token
        };

        _constructionCompanyAdminRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(token)).Returns(admin);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _constructionCompanyAdminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken9_2_2()
    {
        Guid token = Guid.NewGuid();
        string uri = "buildings";
        string verbo = "POST";

        ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin()
        {
            Id = token
        };

        _constructionCompanyAdminRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(token))
            .Returns((ConstructionCompanyAdmin)null);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(response, Guid.Empty);

        _constructionCompanyAdminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken9_3()
    {
        Guid token = Guid.NewGuid();
        string uri = "buildings";
        string verbo = "PUT";

        ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin()
        {
            Id = token
        };

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(Guid.Empty);
        _constructionCompanyAdminRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(token)).Returns(admin);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _constructionCompanyAdminRepositoryMock.VerifyAll();
        _adminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken9_4()
    {
        Guid token = Guid.NewGuid();
        string uri = "buildings";
        string verbo = "";

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _managerRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken10()
    {
        Guid token = Guid.NewGuid();
        string uri = "reports";
        string verbo = "";

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _managerRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken11()
    {
        Guid token = Guid.NewGuid();
        string uri = "request";
        string verbo = "POST";

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _managerRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken12()
    {
        Guid token = Guid.NewGuid();
        string uri = "request";
        string verbo = "PUT";

        _maintenanceStaffRepositoryMock.Setup(r => r.GetMaintenanceStaff(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _maintenanceStaffRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken13()
    {
        Guid token = Guid.NewGuid();
        string uri = "importbuildings";
        string verbo = "";

        ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin()
        {
            Id = token
        };

        _constructionCompanyAdminRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(token)).Returns(admin);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _constructionCompanyAdminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken14()
    {
        Guid token = Guid.NewGuid();
        string uri = "constructioncompany";
        string verbo = "";

        _constructionCompanyAdminRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(token))
            .Returns((ConstructionCompanyAdmin)null);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(response, Guid.Empty);

        _constructionCompanyAdminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken15()
    {
        Guid token = Guid.NewGuid();
        string uri = "maintenancestaff";
        string verbo = "";

        ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin()
        {
            Id = token
        };

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _managerRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken16()
    {
        Guid token = Guid.NewGuid();
        string uri = "constructioncompany";
        string verbo = "";

        _constructionCompanyAdminRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(token))
            .Returns((ConstructionCompanyAdmin)null);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(response, Guid.Empty);

        _constructionCompanyAdminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken17()
    {
        Guid token = Guid.NewGuid();
        string uri = "managers";
        string verbo = "";

        _constructionCompanyAdminRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(token))
            .Returns((ConstructionCompanyAdmin)null);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(response, Guid.Empty);

        _constructionCompanyAdminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken18()
    {
        Guid token = Guid.NewGuid();
        string uri = "invitations";
        string verbo = "GET";

        _adminRepositoryMock.Setup(r => r.Get(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _adminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken19()
    {
        Guid token = Guid.NewGuid();
        string uri = "invitations";
        string verbo = "";


        Exception exception = null;

        try
        {
            _authenticationServiceLogic.BuscarToken(token, verbo, uri);
        }
        catch (Exception e)
        {
            exception = e;
        }

        Assert.IsNotNull(exception);
        Assert.IsInstanceOfType(exception, typeof(NotImplementedException));
    }

    [TestMethod]
    public void BuscarToken20()
    {
        Guid token = Guid.NewGuid();
        string uri = "request";
        string verbo = "";


        Exception exception = null;

        try
        {
            _authenticationServiceLogic.BuscarToken(token, verbo, uri);
        }
        catch (Exception e)
        {
            exception = e;
        }

        Assert.IsNotNull(exception);
        Assert.IsInstanceOfType(exception, typeof(NotImplementedException));
    }

    [TestMethod]
    public void BuscarToken21()
    {
        Guid token = Guid.NewGuid();
        string uri = "";
        string verbo = "";


        Exception exception = null;

        try
        {
            _authenticationServiceLogic.BuscarToken(token, verbo, uri);
        }
        catch (Exception e)
        {
            exception = e;
        }

        Assert.IsNotNull(exception);
        Assert.IsInstanceOfType(exception, typeof(NotImplementedException));
    }

    [TestMethod]
    public void BuscarToken22()
    {
        Guid token = Guid.NewGuid();
        string uri = "buildings";
        string verbo = "PUT";

        ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin()
        {
            Id = token
        };

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(Guid.Empty);
        _constructionCompanyAdminRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(token))
            .Returns((ConstructionCompanyAdmin)null);

        Exception exception = null;

        try
        {
            _authenticationServiceLogic.BuscarToken(token, verbo, uri);
        }
        catch (Exception e)
        {
            exception = e;
        }

        Assert.IsNotNull(exception);
        Assert.IsInstanceOfType(exception, typeof(ArgumentException));
        Assert.AreEqual(exception.Message, "There is no person associated with the token.");
    }

    [TestMethod]
    public void BuscarToken23()
    {
        Guid token = Guid.NewGuid();
        string uri = "request";
        string verbo = "GET";

        ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin()
        {
            Id = token
        };

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(Guid.Empty);
        _maintenanceStaffRepositoryMock.Setup(r => r.GetMaintenanceStaff(token))
            .Returns(Guid.Empty);

        Exception exception = null;

        try
        {
            _authenticationServiceLogic.BuscarToken(token, verbo, uri);
        }
        catch (Exception e)
        {
            exception = e;
        }

        Assert.IsNotNull(exception);
        Assert.IsInstanceOfType(exception, typeof(ArgumentException));
        Assert.AreEqual(exception.Message, "There is no person associated with the token.");
    }

    [TestMethod]
    public void Authenticate_Admin()
    {
        string email = "email";
        string password = "password";

        Admin admin = new Admin()
        {
            AdminID = Guid.NewGuid()
        };

        _adminRepositoryMock.Setup(r => r.GetByEmailAndPassword(email, password)).Returns(admin);

        AuthenticationResult response = _authenticationServiceLogic.Authenticate(email, password);

        Assert.IsNotNull(response);
        Assert.AreEqual(response.UserID, admin.AdminID);
        Assert.AreEqual(response.UserType, "Admin");

        _adminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void Authenticate_ConstructionCompanyAdmin()
    {
        string email = "email";
        string password = "password";

        ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin()
        {
            Id = Guid.NewGuid()
        };

        _adminRepositoryMock.Setup(r => r.GetByEmailAndPassword(email, password)).Returns((Admin)null);
        _constructionCompanyAdminRepositoryMock.Setup(r => r.GetByEmailAndPassword(email, password)).Returns(admin);

        AuthenticationResult response = _authenticationServiceLogic.Authenticate(email, password);

        Assert.IsNotNull(response);
        Assert.AreEqual(response.UserID, admin.Id);
        Assert.AreEqual(response.UserType, "ConstructionCompanyAdmin");

        _adminRepositoryMock.VerifyAll();
        _constructionCompanyAdminRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void Authenticate_MaintenanceStaff()
    {
        string email = "email";
        string password = "password";

        MaintenanceStaff staff = new MaintenanceStaff()
        {
            ID = Guid.NewGuid()
        };

        _adminRepositoryMock.Setup(r => r.GetByEmailAndPassword(email, password)).Returns((Admin)null);
        _constructionCompanyAdminRepositoryMock.Setup(r => r.GetByEmailAndPassword(email, password))
            .Returns((ConstructionCompanyAdmin)null);
        _maintenanceStaffRepositoryMock.Setup(r => r.GetByEmailAndPassword(email, password)).Returns(staff);

        AuthenticationResult response = _authenticationServiceLogic.Authenticate(email, password);

        Assert.IsNotNull(response);
        Assert.AreEqual(response.UserID, staff.ID);
        Assert.AreEqual(response.UserType, "MaintenanceStaff");

        _adminRepositoryMock.VerifyAll();
        _constructionCompanyAdminRepositoryMock.VerifyAll();
        _maintenanceStaffRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void Authenticate_Manager()
    {
        string email = "email";
        string password = "password";

        Manager manager = new Manager()
        {
            ManagerId = Guid.NewGuid()
        };

        _adminRepositoryMock.Setup(r => r.GetByEmailAndPassword(email, password)).Returns((Admin)null);
        _constructionCompanyAdminRepositoryMock.Setup(r => r.GetByEmailAndPassword(email, password))
            .Returns((ConstructionCompanyAdmin)null);
        _maintenanceStaffRepositoryMock.Setup(r => r.GetByEmailAndPassword(email, password))
            .Returns((MaintenanceStaff)null);
        _managerRepositoryMock.Setup(r => r.GetByEmailAndPassword(email, password)).Returns(manager);

        AuthenticationResult response = _authenticationServiceLogic.Authenticate(email, password);

        Assert.IsNotNull(response);
        Assert.AreEqual(response.UserID, manager.ManagerId);
        Assert.AreEqual(response.UserType, "Manager");

        _adminRepositoryMock.VerifyAll();
        _constructionCompanyAdminRepositoryMock.VerifyAll();
        _maintenanceStaffRepositoryMock.VerifyAll();
        _managerRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void Authenticate_NoUser()
    {
        string email = "email";
        string password = "password";

        _adminRepositoryMock.Setup(r => r.GetByEmailAndPassword(email, password)).Returns((Admin)null);
        _constructionCompanyAdminRepositoryMock.Setup(r => r.GetByEmailAndPassword(email, password))
            .Returns((ConstructionCompanyAdmin)null);
        _maintenanceStaffRepositoryMock.Setup(r => r.GetByEmailAndPassword(email, password))
            .Returns((MaintenanceStaff)null);
        _managerRepositoryMock.Setup(r => r.GetByEmailAndPassword(email, password)).Returns((Manager)null);

        Exception exception = null;

        try
        {
            _authenticationServiceLogic.Authenticate(email, password);
        }
        catch (Exception e)
        {
            exception = e;
        }

        Assert.IsNotNull(exception);
        Assert.IsInstanceOfType(exception, typeof(InvalidCredentialsException));

        _adminRepositoryMock.VerifyAll();
        _constructionCompanyAdminRepositoryMock.VerifyAll();
        _maintenanceStaffRepositoryMock.VerifyAll();
        _managerRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken24()
    {
        Guid token = Guid.NewGuid();
        string uri = "buildings";
        string verbo = "GET";

        ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin()
        {
            Id = token
        };

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(token);
        _constructionCompanyAdminRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(token)).Returns(admin);

        Guid response = _authenticationServiceLogic.BuscarToken(token, verbo, uri);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _constructionCompanyAdminRepositoryMock.VerifyAll();
        _managerRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void BuscarToken25()
    {
        Guid token = Guid.NewGuid();
        string uri = "categoriesrequests";
        string verbo = "";

        ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin()
        {
            Id = token
        };

        _managerRepositoryMock.Setup(r => r.Get(token)).Returns(token);
        _constructionCompanyAdminRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(token)).Returns(admin);

        Exception exception = null;

        try
        {
            _authenticationServiceLogic.BuscarToken(token, verbo, uri);
        }
        catch (Exception e)
        {
            exception = e;
        }

        Assert.IsNotNull(exception);
        Assert.IsInstanceOfType(exception, typeof(NotImplementedException));

    }
}