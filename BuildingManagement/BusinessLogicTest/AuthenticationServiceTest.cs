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

    
}