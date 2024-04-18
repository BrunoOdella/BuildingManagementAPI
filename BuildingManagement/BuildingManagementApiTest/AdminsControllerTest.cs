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
        public void PostAdmin_ShouldReturnCreatedResponse_WhenAdminIsSuccessfullyCreated()
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
                AdminID = Guid.Parse("d8119d3a-f0f1-4451-a0c3-d4abd21e13aa"),
                FirstName = newAdminRequest.FirstName,
                LastName = newAdminRequest.LastName,
                Email = newAdminRequest.Email,
            };

            CreateAdminResponse response = new CreateAdminResponse(adminEntity);
            _adminLogicMock.Setup(logic => logic.CreateAdmin(It.IsAny<Admin>())).Returns(adminEntity);

            // Act
            ObjectResult result = _adminsController.CreateAdmin(newAdminRequest) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(response, result.Value);

            _adminLogicMock.VerifyAll();
        }
    }
}
