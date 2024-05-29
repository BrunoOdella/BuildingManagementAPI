

using BuildingManagementApi.Controllers;
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
    public class ImportBuildingsControllerTests
    {
        private Mock<IBuildingImportLogic> _buildingImportLogicMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private ImportBuildingsController _importBuildingsController;

        [TestInitialize]
        public void TestSetup()
        {
            _buildingImportLogicMock = new Mock<IBuildingImportLogic>(MockBehavior.Strict);
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>(MockBehavior.Strict);

            var httpContext = new DefaultHttpContext();
            Guid expectedUserID = Guid.NewGuid(); // o cualquier Guid que esperes
            httpContext.Items["userID"] = expectedUserID.ToString();
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            _importBuildingsController = new ImportBuildingsController(_buildingImportLogicMock.Object, _httpContextAccessorMock.Object);
        }

        [TestMethod]
        public void PostImportBuildings_ShouldReturnCreatedResponse_WhenBuildingIsSuccessfullyImported()
        {
            // Arrange
            ImportBuildingRequest importBuildingRequest = new ImportBuildingRequest
            {
                AssemblyPath = "path"
            };

            var managerId = new Guid();

            var ccAdmin = new ConstructionCompanyAdmin()
            {
                Email = "email",
                Password = "password",
                Id = new Guid()
            };

            Building buildingEntity = new Building
            {
                BuildingId = Guid.Parse("d8119d3a-f0f1-4451-a0c3-d4abd21e13aa"),
                Name = "Building 1",
                Address = "1234 Building St.",
                Location = new Location()
                {
                    Latitude = 123.456,
                    Longitude = 123.456
                },
                ConstructionCompany = new ConstructionCompany()
                {
                    ConstructionCompanyAdmin = ccAdmin,
                    ConstructionCompanyId = new Guid(),
                    Name = "Construction Company 1",
                    ConstructionCompanyAdminId = ccAdmin.Id
                },
                CommonExpenses = 12345,
                Apartments = new List<Apartment>(),
                ManagerId = managerId,
                Manager = new Manager() { ManagerId = managerId },
                ConstructionCompanyAdmin = ccAdmin,
                ConstructionCompanyAdminId = ccAdmin.Id,
            };

            BuildingResponse response = new BuildingResponse(buildingEntity);
            _buildingImportLogicMock.Setup(logic => logic.ImportBuilding(It.IsAny<Guid>(), It.IsAny<string>())).Returns(buildingEntity);

            // Act
            ObjectResult result = _importBuildingsController.ImportBuildings(importBuildingRequest) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(response, result.Value);

            _buildingImportLogicMock.VerifyAll();
            _httpContextAccessorMock.VerifyAll();
        }
    }
}

