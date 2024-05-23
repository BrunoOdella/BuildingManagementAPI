using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain;
using LogicInterface.Interfaces;
using IDataAccess;
using System;
using BusinessLogic.Logics;
using CustomExceptions;

namespace BusinessLogicTest
{
    [TestClass]
    public class BuildingLogicTest
    {
        private Mock<IBuildingRepository> _buildingRepositoryMock;
        private Mock<IConstructionCompanyAdminRepository> _constructionCompanyAdminRepositoryMock;
        private IBuildingLogic _buildingLogic;

        [TestInitialize]
        public void TestSetup()
        {
            _buildingRepositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            _constructionCompanyAdminRepositoryMock = new Mock<IConstructionCompanyAdminRepository>(MockBehavior.Strict);
            _buildingLogic = new BuildingLogic(_buildingRepositoryMock.Object, _constructionCompanyAdminRepositoryMock.Object);
        }

        [TestMethod]
        public void CreateBuilding_ShouldCreateBuilding()
        {
            // Arrange
            Guid adminId = Guid.NewGuid();
            ConstructionCompanyAdmin constructionCompanyAdmin = new ConstructionCompanyAdmin
            {
                Id = adminId,
                ConstructionCompany = new ConstructionCompany
                {
                    ConstructionCompanyId = Guid.NewGuid(),
                    Name = "New Construction Company"
                }
            };

            Building newBuilding = new Building
            {
                BuildingId = Guid.NewGuid(),
                Name = "New Building",
                Location = new Location { Latitude = 1.0, Longitude = 1.0 },
                ConstructionCompanyAdminId = adminId
            };

            _constructionCompanyAdminRepositoryMock.Setup(repo => repo.GetConstructionCompanyAdminById(adminId)).Returns(constructionCompanyAdmin);
            _buildingRepositoryMock.Setup(repo => repo.GetBuildingByLocation(newBuilding.Location.Latitude, newBuilding.Location.Longitude)).Returns((Building)null);
            _buildingRepositoryMock.Setup(repo => repo.CreateBuilding(It.IsAny<Building>())).Returns(newBuilding);

            // Act
            Building result = _buildingLogic.CreateBuilding(adminId.ToString(), newBuilding);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newBuilding.Name, result.Name);
            _buildingRepositoryMock.VerifyAll();
            _constructionCompanyAdminRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void CreateBuilding_InvalidAdminId_ShouldThrowException()
        {
            // Arrange
            Guid invalidAdminId = Guid.NewGuid();
            Building newBuilding = new Building
            {
                BuildingId = Guid.NewGuid(),
                Name = "New Building",
                Location = new Location { Latitude = 1.0, Longitude = 1.0 },
                ConstructionCompanyAdminId = invalidAdminId
            };

            _constructionCompanyAdminRepositoryMock.Setup(repo => repo.GetConstructionCompanyAdminById(invalidAdminId)).Returns((ConstructionCompanyAdmin)null);

            // Act
            _buildingLogic.CreateBuilding(invalidAdminId.ToString(), newBuilding);

            // Assert - Expects UnauthorizedAccessException
            _constructionCompanyAdminRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateBuilding_AdminWithoutCompany_ShouldThrowException()
        {
            // Arrange
            Guid adminId = Guid.NewGuid();
            ConstructionCompanyAdmin constructionCompanyAdmin = new ConstructionCompanyAdmin
            {
                Id = adminId
            };

            Building newBuilding = new Building
            {
                BuildingId = Guid.NewGuid(),
                Name = "New Building",
                Location = new Location { Latitude = 1.0, Longitude = 1.0 },
                ConstructionCompanyAdminId = adminId
            };

            _constructionCompanyAdminRepositoryMock.Setup(repo => repo.GetConstructionCompanyAdminById(adminId)).Returns(constructionCompanyAdmin);

            // Act
            _buildingLogic.CreateBuilding(adminId.ToString(), newBuilding);

            // Assert - Expects InvalidOperationException
            _constructionCompanyAdminRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(LocationAlreadyExistsException))]
        public void CreateBuilding_LocationAlreadyExists_ShouldThrowException()
        {
            // Arrange
            Guid adminId = Guid.NewGuid();
            ConstructionCompanyAdmin constructionCompanyAdmin = new ConstructionCompanyAdmin
            {
                Id = adminId,
                ConstructionCompany = new ConstructionCompany
                {
                    ConstructionCompanyId = Guid.NewGuid(),
                    Name = "New Construction Company"
                }
            };

            Building newBuilding = new Building
            {
                BuildingId = Guid.NewGuid(),
                Name = "New Building",
                Location = new Location { Latitude = 1.0, Longitude = 1.0 },
                ConstructionCompanyAdminId = adminId
            };

            Building existingBuilding = new Building
            {
                BuildingId = Guid.NewGuid(),
                Name = "Existing Building",
                Location = new Location { Latitude = 1.0, Longitude = 1.0 }
            };

            _constructionCompanyAdminRepositoryMock.Setup(repo => repo.GetConstructionCompanyAdminById(adminId)).Returns(constructionCompanyAdmin);
            _buildingRepositoryMock.Setup(repo => repo.GetBuildingByLocation(newBuilding.Location.Latitude, newBuilding.Location.Longitude)).Returns(existingBuilding);

            // Act
            _buildingLogic.CreateBuilding(adminId.ToString(), newBuilding);

            // Assert - Expects LocationAlreadyExistsException
            _buildingRepositoryMock.VerifyAll();
            _constructionCompanyAdminRepositoryMock.VerifyAll();
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteBuilding_InvalidManagerId_ThrowsArgumentException()
        {
            // Arrange
            string invalidManagerId = "not-a-guid";

            // Act
            _buildingLogic.DeleteBuilding(invalidManagerId, Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void DeleteBuilding_UnauthorizedManager_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var managerId = Guid.NewGuid().ToString();
            var buildingId = Guid.NewGuid();
            _buildingRepositoryMock.Setup(r => r.GetBuilding(Guid.Parse(managerId), buildingId))
                .Returns((Building)null); // No building found

            // Act
            _buildingLogic.DeleteBuilding(managerId, buildingId);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteBuilding_FailureToDelete_ThrowsInvalidOperationException()
        {
            // Arrange
            var managerId = Guid.NewGuid().ToString();
            var buildingId = Guid.NewGuid();
            var building = new Building { BuildingId = buildingId, ManagerId = Guid.Parse(managerId) };
            _buildingRepositoryMock.Setup(r => r.GetBuilding(Guid.Parse(managerId), buildingId)).Returns(building);
            _buildingRepositoryMock.Setup(r => r.DeleteBuilding(buildingId)).Returns(false); // Fail to delete

            // Act
            _buildingLogic.DeleteBuilding(managerId, buildingId);
        }

        [TestMethod]
        public void DeleteBuilding_SuccessfulDeletion_CompletesWithoutException()
        {
            // Arrange
            var managerId = Guid.NewGuid().ToString();
            var buildingId = Guid.NewGuid();
            var building = new Building { BuildingId = buildingId, ManagerId = Guid.Parse(managerId) };
            _buildingRepositoryMock.Setup(r => r.GetBuilding(Guid.Parse(managerId), buildingId)).Returns(building);
            _buildingRepositoryMock.Setup(r => r.DeleteBuilding(buildingId)).Returns(true); // Successfully deleted

            // Act
            _buildingLogic.DeleteBuilding(managerId, buildingId); // No exception expected

            // Assert
            _buildingRepositoryMock.Verify(r => r.DeleteBuilding(buildingId), Times.Once);
            Assert.IsTrue(true); // If reached here, test passed
        }




        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateBuilding_InvalidManagerId_ThrowsArgumentException()
        {
            // Arrange
            string invalidManagerId = "invalid-guid";
            Building building = new Building { BuildingId = Guid.NewGuid() };

            // Act
            _buildingLogic.UpdateBuilding(invalidManagerId, building);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateBuilding_BuildingNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            string managerId = Guid.NewGuid().ToString();
            Building building = new Building { BuildingId = Guid.NewGuid() };

            _buildingRepositoryMock.Setup(r => r.GetBuilding(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns((Building)null);

            // Act
            _buildingLogic.UpdateBuilding(managerId, building);
        }

        [TestMethod]
        public void UpdateBuilding_ValidRequest_UpdatesBuilding()
        {
            // Arrange
            string managerId = Guid.NewGuid().ToString();
            Guid buildingId = Guid.NewGuid();
            Building building = new Building
            {
                BuildingId = buildingId,
                Name = "Updated Name",
                Address = "Updated Address",
                Location = new Location { Latitude = 40.0, Longitude = -74.0 },
                CommonExpenses = 500
            };

            Building existingBuilding = new Building
            {
                BuildingId = buildingId,
                ManagerId = Guid.Parse(managerId),
                Location = new Location { Latitude = 35.0, Longitude = -75.0 } // Asegurarse de inicializar Location aquí también
            };

            _buildingRepositoryMock.Setup(r => r.GetBuilding(Guid.Parse(managerId), buildingId)).Returns(existingBuilding);
            _buildingRepositoryMock.Setup(r => r.UpdateBuilding(It.IsAny<Building>())).Returns(existingBuilding);

            // Act
            var result = _buildingLogic.UpdateBuilding(managerId, building);

            // Assert
            Assert.AreEqual("Updated Name", result.Name);
            Assert.AreEqual("Updated Address", result.Address);
            Assert.AreEqual(40.0, result.Location.Latitude);
            Assert.AreEqual(-74.0, result.Location.Longitude);
            Assert.AreEqual(500, result.CommonExpenses);
        }


        [TestMethod]
        public void UpdateBuilding_PartialUpdate_UpdatesOnlySpecifiedFields()
        {
            // Arrange
            string managerId = Guid.NewGuid().ToString();
            Guid buildingId = Guid.NewGuid();
            Building building = new Building
            {
                BuildingId = buildingId,
                Name = "Partially Updated Name"
                // Address and other fields are not provided and should not be updated
            };
            Building existingBuilding = new Building
            {
                BuildingId = buildingId,
                ManagerId = Guid.Parse(managerId),
                Name = "Old Name",
                Address = "Old Address"
            };

            _buildingRepositoryMock.Setup(r => r.GetBuilding(Guid.Parse(managerId), buildingId)).Returns(existingBuilding);
            _buildingRepositoryMock.Setup(r => r.UpdateBuilding(It.IsAny<Building>())).Returns(existingBuilding);

            // Act
            var result = _buildingLogic.UpdateBuilding(managerId, building);

            // Assert
            Assert.AreEqual("Partially Updated Name", result.Name);
            Assert.AreEqual("Old Address", result.Address);  // Should not be updated
        }

        [TestMethod]
        public void GetBuildingsByConstructionCompanyAdminId_ReturnsBuildings()
        {
            Guid adminId = Guid.NewGuid();
            var buildings = new List<Building>
            {
                new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Building 1",
                    Address = "123 Main St",
                    ConstructionCompanyAdminId = adminId
                },
                new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Building 2",
                    Address = "456 Oak St",
                    ConstructionCompanyAdminId = adminId
                }
            };

            _buildingRepositoryMock.Setup(repo => repo.GetBuildingsByConstructionCompanyAdminId(adminId)).Returns(buildings);

            var result = _buildingLogic.GetBuildingsByAdminId(adminId.ToString());

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(b => b.Name == "Building 1"));
            Assert.IsTrue(result.Any(b => b.Name == "Building 2"));

            _buildingRepositoryMock.Verify(repo => repo.GetBuildingsByConstructionCompanyAdminId(adminId), Times.Once);
        }
    }
}
