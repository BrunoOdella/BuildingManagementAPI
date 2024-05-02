using BuildingManagementApi.Controllers;
using Domain;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagementApiTest
{
    [TestClass]
    public class MaitenanceStaffControllerTest
    {
        private Mock<IMaintenanceStaffLogic> _maintenanceStaffLogicMock;
        private MaintenanceStaffController _maintenanceStaffController;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;

        [TestInitialize]
        public void Setup()
        {
            _maintenanceStaffLogicMock = new Mock<IMaintenanceStaffLogic>(MockBehavior.Strict);
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>(MockBehavior.Strict);
            var httpContext = new DefaultHttpContext();
            Guid expectedUserID = Guid.NewGuid();
            httpContext.Items["userID"] = expectedUserID.ToString();
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);
            _maintenanceStaffController = new MaintenanceStaffController(_maintenanceStaffLogicMock.Object, _httpContextAccessorMock.Object);
        }

        [TestMethod]
        public void CreateMaintenanceStaff_ReturnsCreated_WhenSuccessful()
        {
            var mockLogic = new Mock<IMaintenanceStaffLogic>();
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = Guid.NewGuid().ToString();
            httpContextAccessor.Setup(_ => _.HttpContext).Returns(httpContext);

            var controller = new MaintenanceStaffController(mockLogic.Object, httpContextAccessor.Object);
            var request = new CreateMaintenanceStaffRequest
            {
                Name = "John",
                LastName = "Doe",
                Email = "john@example.com",
                Password = "secure123"
            };
            var buildingId = Guid.NewGuid();
            var maintenanceStaff = request.ToEntity(buildingId);

            mockLogic.Setup(x => x.AddMaintenanceStaff(It.IsAny<string>(), It.IsAny<MaintenanceStaff>())).Returns(maintenanceStaff);

            var result = controller.CreateMaintenanceStaff(buildingId, request) as CreatedAtActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            var response = result.Value as MaintenanceStaff;
            Assert.IsNotNull(response);
            Assert.AreEqual(maintenanceStaff.Name, response.Name);
        }

    }
}
