using BuildingManagementApi.Controllers;
using Domain;
using LogicInterface.Interfaces.IManagerLogic;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BuildingManagementApiTest;

[TestClass]
public class ManagersControllerTest
{
    private Mock<IManagerLogic> _managerLogic;
    private ManagersController _managersController;

    [TestInitialize]
    public void Setup()
    {
        _managerLogic = new Mock<IManagerLogic>(MockBehavior.Strict);
        _managersController = new ManagersController(_managerLogic.Object);
    }

    [TestMethod]
    public void GetAll_ReturnsOk_WhenManagersAreFound()
    {
        // Arrange
        var managers = new List<Manager>
        {
            new Manager
            {
                ManagerId = Guid.NewGuid(),
                Name = "John",
                Email = "email"
            },
            new Manager
            {
                ManagerId = Guid.NewGuid(),
                Name = "Jane",
                Email = "email2"
            }
        };

        _managerLogic.Setup(x => x.GetAll()).Returns(managers);

        // Act
        var result = _managersController.GetAll() as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        var response = result.Value as List<Models.Out.ManagerResponse>;
        Assert.IsNotNull(response);

        _managerLogic.VerifyAll();
    }
}