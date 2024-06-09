using BusinessLogic.Logics;
using CustomExceptions;
using Domain;
using IDataAccess;
using LogicInterface.Interfaces;
using Moq;

namespace BusinessLogicTest
{
    [TestClass]
    public class BuildingLogicTest
    {
        private Mock<IBuildingRepository> _buildingRepositoryMock;
        private Mock<IConstructionCompanyAdminRepository> _constructionCompanyAdminRepositoryMock;
        private Mock<IManagerRepository> _managerRepositoryMock;
        private IBuildingLogic _buildingLogic;

        [TestInitialize]
        public void TestSetup()
        {
            _buildingRepositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            _constructionCompanyAdminRepositoryMock = new Mock<IConstructionCompanyAdminRepository>(MockBehavior.Strict);
            _managerRepositoryMock = new Mock<IManagerRepository>(MockBehavior.Strict);
            _buildingLogic = new BuildingLogic(_buildingRepositoryMock.Object, _constructionCompanyAdminRepositoryMock.Object, _managerRepositoryMock.Object);
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
            _buildingLogic.UpdateBuilding(invalidManagerId, building, building.BuildingId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateBuilding_BuildingNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            string managerId = Guid.NewGuid().ToString();
            Building building = new Building { BuildingId = Guid.NewGuid() };

            _buildingRepositoryMock.Setup(r => r.GetBuilding(It.IsAny<Guid>())).Returns((Building)null);

            // Act
            _buildingLogic.UpdateBuilding(managerId, building, building.BuildingId);
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

            _buildingRepositoryMock.Setup(r => r.GetBuilding(buildingId)).Returns(existingBuilding);
            _buildingRepositoryMock.Setup(r => r.UpdateBuilding(It.IsAny<Building>())).Returns(existingBuilding);
            _managerRepositoryMock.Setup(r => r.Get(Guid.Parse(managerId))).Returns(Guid.Parse(managerId));

            // Act
            var result = _buildingLogic.UpdateBuilding(managerId, building, building.BuildingId);

            //// Assert
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

            _buildingRepositoryMock.Setup(r => r.GetBuilding(buildingId)).Returns(existingBuilding);
            _managerRepositoryMock.Setup(r => r.Get(Guid.Parse(managerId))).Returns(Guid.Parse(managerId));
            _buildingRepositoryMock.Setup(r => r.UpdateBuilding(It.IsAny<Building>())).Returns(existingBuilding);

            // Act
            var result = _buildingLogic.UpdateBuilding(managerId, building, building.BuildingId);

            // Assert
            Assert.AreEqual("Partially Updated Name", result.Name);
            Assert.AreEqual("Old Address", result.Address);  // Should not be updated
        }

        [TestMethod]
        public void UpdateBuilding_PartialUpdate_UpdatesOnlySpecifiedFields2()
        {
            // Arrange
            string managerId = Guid.NewGuid().ToString();
            Guid buildingId = Guid.NewGuid();
            Building building = new Building
            {
                BuildingId = buildingId,
                // Address and other fields are not provided and should not be updated
            };
            Building existingBuilding = new Building
            {
                BuildingId = buildingId,
                ManagerId = Guid.Parse(managerId),
                Name = "Old Name",
                Address = "Old Address"
            };

            _buildingRepositoryMock.Setup(r => r.GetBuilding(buildingId)).Returns(existingBuilding);
            _managerRepositoryMock.Setup(r => r.Get(Guid.Parse(managerId))).Returns(Guid.Parse(managerId));
            _buildingRepositoryMock.Setup(r => r.UpdateBuilding(It.IsAny<Building>())).Returns(existingBuilding);

            // Act
            var result = _buildingLogic.UpdateBuilding(managerId, building, building.BuildingId);

            // Assert
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
           //         ConstructionCompanyAdminId = adminId
                },
                new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Building 2",
                    Address = "456 Oak St",
                //    ConstructionCompanyAdminId = adminId
                }
            };

            _buildingRepositoryMock.Setup(repo => repo.GetBuildingsByConstructionCompanyAdminId(adminId)).Returns(buildings);
            _constructionCompanyAdminRepositoryMock.Setup(r => r.Get(adminId)).Returns(adminId);

            var result = _buildingLogic.GetBuildings(adminId.ToString());

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(b => b.Name == "Building 1"));
            Assert.IsTrue(result.Any(b => b.Name == "Building 2"));

            _buildingRepositoryMock.Verify(repo => repo.GetBuildingsByConstructionCompanyAdminId(adminId), Times.Once);
        }


        [TestMethod]
        public void UpdateManager_BuildingWithManager_UpdatesManager()
        {
            // Arrange
            Guid adminId = Guid.NewGuid();
            Building building = new Building
            {
                BuildingId = Guid.NewGuid(),
                Manager = new Manager { Email = "manager1" },
                ConstructionCompany = new ConstructionCompany() { ConstructionCompanyAdminId = adminId, ConstructionCompanyAdmin = new ConstructionCompanyAdmin() { Id = adminId } }
            };

            Building updatedBuilding = new Building
            {
                BuildingId = building.BuildingId,
                Manager = new Manager { Email = "manager2" },
                ManagerId = Guid.NewGuid()
            };

            _buildingRepositoryMock.Setup(r => r.GetBuilding(It.IsAny<Guid>())).Returns(building);
            _managerRepositoryMock.Setup(r => r.Get(adminId)).Returns(Guid.Empty);
            _managerRepositoryMock.Setup(r => r.GetManagerById(updatedBuilding.ManagerId.Value)).Returns(updatedBuilding.Manager);
            _constructionCompanyAdminRepositoryMock.Setup(r => r.Get(adminId)).Returns(adminId);
            _buildingRepositoryMock.Setup(r => r.UpdateBuilding(It.IsAny<Building>())).Returns(updatedBuilding);

            //// Act
            var result = _buildingLogic.UpdateBuilding(adminId.ToString(), updatedBuilding, building.BuildingId);

            // Assert
            Assert.AreEqual("manager2", result.Manager.Email);

            _buildingRepositoryMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();
            _constructionCompanyAdminRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBuilding_InvalidGuid()
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
            };

            // Act
            _buildingLogic.CreateBuilding("invalid-guid", newBuilding);
        }

        [TestMethod]
        public void CreateBuilding_ManagerIsNotNull()
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
                ManagerId = Guid.NewGuid(),
                Manager = new Manager { Email = "manager1" }
            };

            _constructionCompanyAdminRepositoryMock.Setup(repo => repo.GetConstructionCompanyAdminById(adminId)).Returns(constructionCompanyAdmin);
            _buildingRepositoryMock.Setup(repo => repo.GetBuildingByLocation(newBuilding.Location.Latitude, newBuilding.Location.Longitude)).Returns((Building)null);
            _buildingRepositoryMock.Setup(repo => repo.CreateBuilding(It.IsAny<Building>())).Returns(newBuilding);
            _managerRepositoryMock.Setup(r => r.GetManagerByEmail(It.IsAny<string>())).Returns(newBuilding.Manager);

            // Act
            Building result = _buildingLogic.CreateBuilding(adminId.ToString(), newBuilding);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newBuilding.Name, result.Name);
            _buildingRepositoryMock.VerifyAll();
            _constructionCompanyAdminRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateBuilding_ManagerIsNull()
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
                Manager = new Manager { Email = "manager1", ManagerId = Guid.NewGuid() }
            };

            _constructionCompanyAdminRepositoryMock.Setup(repo => repo.GetConstructionCompanyAdminById(adminId)).Returns(constructionCompanyAdmin);
            _buildingRepositoryMock.Setup(repo => repo.GetBuildingByLocation(newBuilding.Location.Latitude, newBuilding.Location.Longitude)).Returns((Building)null);
            _buildingRepositoryMock.Setup(repo => repo.CreateBuilding(It.IsAny<Building>())).Returns(newBuilding);
            _managerRepositoryMock.SetupSequence(r => r.GetManagerByEmail(It.IsAny<string>()))
                .Returns((Manager)null)
                .Returns(newBuilding.Manager);
            _managerRepositoryMock.Setup(r => r.CreateManager(It.IsAny<Manager>()));

            // Act
            Building result = _buildingLogic.CreateBuilding(adminId.ToString(), newBuilding);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newBuilding.Name, result.Name);
            _buildingRepositoryMock.VerifyAll();
            _constructionCompanyAdminRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateBUilding_BuildingIsNull()
        {
            // Arrange
            string managerId = Guid.NewGuid().ToString();
            Building building = null;

            // Act
            _buildingLogic.UpdateBuilding(managerId, building, Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateBUilding_BuildingIdIsDifferent()
        {
            // Arrange
            string managerId = Guid.NewGuid().ToString();
            Building building = new Building() { BuildingId = Guid.NewGuid() };

            // Act
            _buildingLogic.UpdateBuilding(managerId, building, Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void UpdateManager_CCAdminIsNotAuthorized()
        {
            // Arrange
            Guid adminId = Guid.NewGuid();
            Building building = new Building
            {
                BuildingId = Guid.NewGuid(),
                Manager = new Manager { Email = "manager1" },
                ConstructionCompany = new ConstructionCompany() { ConstructionCompanyAdminId = Guid.NewGuid(), ConstructionCompanyAdmin = new ConstructionCompanyAdmin() { Id = Guid.NewGuid() } }
            };

            Building updatedBuilding = new Building
            {
                BuildingId = building.BuildingId,
                Manager = new Manager { Email = "manager2" },
                ManagerId = Guid.NewGuid()
            };

            _buildingRepositoryMock.Setup(r => r.GetBuilding(It.IsAny<Guid>())).Returns(building);
            _managerRepositoryMock.Setup(r => r.Get(adminId)).Returns(Guid.Empty);
            _constructionCompanyAdminRepositoryMock.Setup(r => r.Get(adminId)).Returns(adminId);

            _buildingLogic.UpdateBuilding(adminId.ToString(), updatedBuilding, building.BuildingId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateManager_InvalidManagerId()
        {
            // Arrange
            Guid adminId = Guid.NewGuid();
            Building building = new Building
            {
                BuildingId = Guid.NewGuid(),
                Manager = new Manager { Email = "manager1" },
                ConstructionCompany = new ConstructionCompany() { ConstructionCompanyAdminId = adminId, ConstructionCompanyAdmin = new ConstructionCompanyAdmin() { Id = adminId } }
            };

            Building updatedBuilding = new Building
            {
                BuildingId = building.BuildingId,
                Manager = new Manager { Email = "manager2" },
                ManagerId = Guid.NewGuid()
            };

            _buildingRepositoryMock.Setup(r => r.GetBuilding(It.IsAny<Guid>())).Returns(building);
            _managerRepositoryMock.Setup(r => r.Get(adminId)).Returns(Guid.Empty);
            _managerRepositoryMock.Setup(r => r.GetManagerById(updatedBuilding.ManagerId.Value)).Returns((Manager)null);
            _constructionCompanyAdminRepositoryMock.Setup(r => r.Get(adminId)).Returns(adminId);
            _buildingRepositoryMock.Setup(r => r.UpdateBuilding(It.IsAny<Building>())).Returns(updatedBuilding);

            //// Act
            _buildingLogic.UpdateBuilding(adminId.ToString(), updatedBuilding, building.BuildingId);

            _buildingRepositoryMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();
            _constructionCompanyAdminRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateManager_InvalidId()
        {
            // Arrange
            Guid adminId = Guid.NewGuid();
            Building building = new Building
            {
                BuildingId = Guid.NewGuid(),
                Manager = new Manager { Email = "manager1" },
                ConstructionCompany = new ConstructionCompany() { ConstructionCompanyAdminId = adminId, ConstructionCompanyAdmin = new ConstructionCompanyAdmin() { Id = adminId } }
            };

            Building updatedBuilding = new Building
            {
                BuildingId = building.BuildingId,
                Manager = new Manager { Email = "manager2" },
                ManagerId = Guid.NewGuid()
            };

            _buildingRepositoryMock.Setup(r => r.GetBuilding(It.IsAny<Guid>())).Returns(building);
            _managerRepositoryMock.Setup(r => r.Get(adminId)).Returns(Guid.Empty);
            _constructionCompanyAdminRepositoryMock.Setup(r => r.Get(adminId)).Returns(Guid.Empty);

            //// Act
            _buildingLogic.UpdateBuilding(adminId.ToString(), updatedBuilding, building.BuildingId);

            _buildingRepositoryMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();
            _constructionCompanyAdminRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBuildings_InvalidGuid()
        {
            // Arrange
            string personId = "invalid-guid";

            // Act
            _buildingLogic.GetBuildings(personId);
        }

        [TestMethod]
        public void GetBuildings_ReturnBuildings()
        {
            // Arrange
            Guid managerId = Guid.NewGuid();
            var buildings = new List<Building>
            {
                new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Building 1",
                    Address = "123 Main St",
                },
                new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Building 2",
                    Address = "456 Oak St",
                }
            };

            _constructionCompanyAdminRepositoryMock.Setup(r => r.Get(managerId)).Returns(Guid.Empty);
            _managerRepositoryMock.Setup(r => r.Get(managerId)).Returns(managerId);
            _buildingRepositoryMock.Setup(repo => repo.GetBuildingsByManagerId(managerId)).Returns(buildings);

            // Act
            var result = _buildingLogic.GetBuildings(managerId.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(b => b.Name == "Building 1"));
            Assert.IsTrue(result.Any(b => b.Name == "Building 2"));

            _buildingRepositoryMock.Verify(repo => repo.GetBuildingsByManagerId(managerId), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBuildings_IdNotExist()
        {
            // Arrange
            Guid personID = Guid.NewGuid();
            var buildings = new List<Building>
            {
                new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Building 1",
                    Address = "123 Main St",
                },
                new Building
                {
                    BuildingId = Guid.NewGuid(),
                    Name = "Building 2",
                    Address = "456 Oak St",
                }
            };

            _constructionCompanyAdminRepositoryMock.Setup(r => r.Get(personID)).Returns(Guid.Empty);
            _managerRepositoryMock.Setup(r => r.Get(personID)).Returns(Guid.Empty);

            // Act
            _buildingLogic.GetBuildings(personID.ToString());
        }

        [TestMethod]
        public void GetApartnebts()
        {
            // Arrange
            Guid buildingId = Guid.NewGuid();
            var apartments = new List<Apartment>
            {
                new Apartment
                {
                    ApartmentId = Guid.NewGuid(),
                    Floor = 1,
                    BuildingId = buildingId
                },
                new Apartment
                {
                    ApartmentId = Guid.NewGuid(),
                    Floor = 2,
                    BuildingId = buildingId
                }
            };

            Manager manager = new Manager
            {
                ManagerId = Guid.NewGuid()
            };

            _buildingRepositoryMock.Setup(repo => repo.GetAllApartments(manager.ManagerId, buildingId)).Returns(apartments);

            // Act
            var result = _buildingLogic.GetApartments(manager.ManagerId.ToString(), buildingId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(a => a.Floor == 1));

            _buildingRepositoryMock.Verify(repo => repo.GetAllApartments(manager.ManagerId, buildingId), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetApartments_InvalidManagerId()
        {
            // Arrange
            string managerId = "invalid-guid";
            Guid buildingId = Guid.NewGuid();

            // Act
            _buildingLogic.GetApartments(managerId, buildingId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetApartments_BuildingWithOutApartments()
        {
            // Arrange
            string managerId = Guid.NewGuid().ToString();
            Guid buildingId = Guid.NewGuid();

            _buildingRepositoryMock.Setup(repo => repo.GetAllApartments(Guid.Parse(managerId), buildingId)).Returns((List<Apartment>)null);

            // Act
            _buildingLogic.GetApartments(managerId, buildingId);
        }
    }
}
