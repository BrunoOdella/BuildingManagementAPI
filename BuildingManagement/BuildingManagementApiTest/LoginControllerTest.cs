using BuildingManagementApi.Controllers;
using Domain;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BuildingManagementApiTest;

[TestClass]
public class LoginControllerTest
{
    private Mock<IAuthenticationService> _authenticationServiceMock;
    private LoginController _loginController;

    [TestInitialize]
    public void Setup()
    {
        _authenticationServiceMock = new Mock<IAuthenticationService>(MockBehavior.Strict);
        _loginController = new LoginController(_authenticationServiceMock.Object);
    }

    [TestMethod]
    public void Login_ReturnsOk_WhenLoginIsSuccessful()
    {
        // Arrange
        var loginRequest = new Models.In.LoginRequest
        {
            Email = "email",
            Password = "password"
        };

        var authResult = new AuthenticationResult
        {
            UserID = Guid.NewGuid(),
            UserType = "testUser"
        };

        _authenticationServiceMock.Setup(x => x.Authenticate(loginRequest.Email, loginRequest.Password))
                                  .Returns(authResult);

        // Act
        var result = _loginController.Login(loginRequest) as OkObjectResult;
        Assert.IsNotNull(result);

        // Assert
        Assert.AreEqual(200, result.StatusCode);

        var response = result.Value as Models.Out.LoginResponse;
        Assert.IsNotNull(response);
        Assert.AreEqual(authResult.UserID.ToString(), response.Token);

        _authenticationServiceMock.VerifyAll();
    }

}