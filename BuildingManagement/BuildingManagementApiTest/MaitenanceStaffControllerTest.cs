﻿using BuildingManagementApi.Controllers;
using Domain;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;
using Moq;

namespace BuildingManagementApiTest
{
    [TestClass]
    public class MaintenanceStaffControllerTest
    {
        private Mock<IMaintenanceStaffLogic> _maintenanceStaffLogicMock;
        private MaintenanceStaffController _maintenanceStaffController;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;

        [TestInitialize]
        public void Setup()
        {
            _maintenanceStaffLogicMock = new Mock<IMaintenanceStaffLogic>(MockBehavior.Strict);
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>(MockBehavior.Strict);

            DefaultHttpContext httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = Guid.NewGuid().ToString();
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            _maintenanceStaffController = new MaintenanceStaffController(_maintenanceStaffLogicMock.Object, _httpContextAccessorMock.Object);
        }

        [TestMethod]
        public void CreateMaintenanceStaff_ReturnsCreated_WhenSuccessful()
        {
            // Arrange
            Guid buildingId = Guid.NewGuid();
            CreateMaintenanceStaffRequest request = new CreateMaintenanceStaffRequest
            {
                Name = "John",
                LastName = "Doe",
                Email = "john@example.com",
                Password = "secure123"
            };
            MaintenanceStaff maintenanceStaff = request.ToEntity();


            _maintenanceStaffLogicMock
                .Setup(x => x.AddMaintenanceStaff(It.IsAny<string>(), It.IsAny<MaintenanceStaff>()))
                .Returns(maintenanceStaff);

            // Act
            IActionResult result = _maintenanceStaffController.CreateMaintenanceStaff(request);

            // Assert
            CreatedResult createdResult = result as CreatedResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode);

            CreateMaintenanceStaffResponse response = createdResult.Value as CreateMaintenanceStaffResponse;
            Assert.IsNotNull(response);
            Assert.AreEqual(maintenanceStaff.Name, response.Name);
            Assert.AreEqual(maintenanceStaff.LastName, response.LastName);

            _maintenanceStaffLogicMock.Verify(x => x.AddMaintenanceStaff(It.IsAny<string>(), It.IsAny<MaintenanceStaff>()), Times.Once);
            _maintenanceStaffLogicMock.VerifyAll();
        }

        [TestMethod]
        public void GetAllMaintenanceStaff_ReturnsOk_WhenSuccessful()
        {
            // Arrange

            List<MaintenanceStaff> mSList = new List<MaintenanceStaff>()
            {

                new MaintenanceStaff
                {
                    Name = "John",
                    LastName = "Doe",
                    Email = "email",
                    Password = "password",
                    ID = Guid.NewGuid(),
                },

                new MaintenanceStaff
                {
                    Name = "John2",
                    LastName = "Doe2",
                    Email = "email2",
                    Password = "password2",
                    ID = Guid.NewGuid(),
                }
            };

            _maintenanceStaffLogicMock
                .Setup(x => x.GetAllMaintenanceStaff(It.IsAny<string>()))
                .Returns(mSList);

            // Act

            IActionResult result = _maintenanceStaffController.GetAllMaintenanceStaff();
            Assert.IsNotNull(result);

            OkObjectResult okObjectResult = result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(StatusCodes.Status200OK, okObjectResult.StatusCode);

            _maintenanceStaffLogicMock.VerifyAll();
        }
    }
}
