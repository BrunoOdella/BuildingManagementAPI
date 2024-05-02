using BusinessLogic.Logics;
using Domain;
using IDataAccess;
using Moq;

namespace BusinessLogicTest;

[TestClass]
public class AuthenticationServiceTest
{
    private Mock<IAuthenticationServiceRepository> _authenticationRepository;
    private AuthenticationService _authenticationServiceLogic;

    [TestInitialize]
    public void TestSetup()
    {
        _authenticationRepository = new Mock<IAuthenticationServiceRepository>(MockBehavior.Strict);
        _authenticationServiceLogic = new AuthenticationService(_authenticationRepository.Object);
    }

    [TestMethod]
    public void CreateCategory_Succes()
    {
        Guid token = Guid.NewGuid();

        _authenticationRepository.Setup(c => c.BuscarToken(token)).Returns(token);

        Guid response = _authenticationServiceLogic.BuscarToken(token);

        Assert.IsNotNull(response);
        Assert.AreEqual(token, response);

        _authenticationRepository.VerifyAll();
    }
}