using BuildingManagementApi.Controllers;
using Domain;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;
using Moq;

namespace BuildingManagementApiTest
{
    [TestClass]
    public class AdminsControllerTests
    {
        private Mock<IAdminLogic> _adminLogicMock;
        private AdminsController _adminsController;

        [TestInitialize]
        public void TestSetup()
        {
            _adminLogicMock = new Mock<IAdminLogic>(MockBehavior.Strict);
            _adminsController = new AdminsController(_adminLogicMock.Object);
        }


        [TestMethod]
        public void PostAdmin_ShouldReturnOkResponse_WhenAdminIsSuccessfullyCreated()
        {

            // Arrange
            CreateAdminRequest newAdminRequest = new CreateAdminRequest
            {
                FirstName = "Juan Carlos",
                LastName = "Odella",
                Email = "juancarlosodella@mail.com",
                Password = "1234"
            };

            Admin adminEntity = new Admin
            {
                AdminID = 1,
                FirstName = newAdminRequest.FirstName,
                LastName = newAdminRequest.LastName,
                Email = newAdminRequest.Email,
            };

            _adminLogicMock.Setup(logic => logic.CreateAdmin(It.IsAny<Admin>())).Returns(adminEntity);
            OkObjectResult expected = new OkObjectResult(new CreateAdminResponse(adminEntity));

            // Act
            OkObjectResult result = _adminsController.CreateAdmin(newAdminRequest) as OkObjectResult;

            CreateAdminResponse actualResponse = result.Value as CreateAdminResponse;

            // Assert
            Assert.IsNotNull(actualResponse);

            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            Assert.AreEqual(expected.Value, actualResponse);

            _adminLogicMock.VerifyAll();
        }

    }
}
