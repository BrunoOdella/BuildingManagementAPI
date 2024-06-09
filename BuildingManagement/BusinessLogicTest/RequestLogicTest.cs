using BusinessLogic.Logics;
using Domain;
using IDataAccess;
using Moq;

namespace BusinessLogicTest
{
    [TestClass]
    public class RequestLogicTest
    {
        private Mock<IRequestRepository> _requestRepositoryMock;
        private Mock<IMaintenanceStaffRepository> _staffRepositoryMock;
        private Mock<IBuildingRepository> _buildingRepositoryMock;
        private Mock<IManagerRepository> _managerRepositoryMock;
        private RequestLogic _requestLogic;
        private Guid _managerID;

        [TestInitialize]
        public void TestSetup()
        {
            _requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            _staffRepositoryMock = new Mock<IMaintenanceStaffRepository>(MockBehavior.Strict);
            _buildingRepositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            _managerRepositoryMock = new Mock<IManagerRepository>(MockBehavior.Strict);

            _requestLogic = new RequestLogic(_requestRepositoryMock.Object, _staffRepositoryMock.Object, _buildingRepositoryMock.Object, _managerRepositoryMock.Object);
            _managerID = Guid.NewGuid();
        }

        // Start - CreateRequest 

        [TestMethod]
        public void CreateRequest_ValidatesData_CreateRequest()
        {
            Building building = new Building()
            {
                BuildingId = Guid.NewGuid()
            };

            Apartment apartment = new Apartment()
            {
                BuildingId = building.BuildingId
            };

            MaintenanceStaff staff = new MaintenanceStaff()
            {
                ID = new Guid()
            };

            Request_ request = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                EndTime = DateTime.Now,
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Finished,
                TotalCost = 1000,
                MaintenanceStaff = staff,
                Apartment = apartment
            };

            _requestRepositoryMock.Setup(repository => repository.CreateRequest(It.IsAny<Request_>())).Returns(request);
            _buildingRepositoryMock.Setup(repo => repo.GetBuilding(_managerID, It.IsAny<Guid>())).Returns(building);
            _buildingRepositoryMock.Setup(repo => repo.GetApartment(_managerID, It.IsAny<Guid>())).Returns(apartment);
            _staffRepositoryMock.Setup(r => r.GetMaintenanceStaff(_managerID, It.IsAny<Guid>())).Returns(staff);

            Request_ result = _requestLogic.CreateRequest(_managerID, request);

            Assert.IsNotNull(result);
            Assert.AreEqual(request, result);
            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateRequest_NullBuilding_ThrowException()
        {


            Request_ request = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                EndTime = DateTime.Now,
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Finished,
                TotalCost = 1000,
                MaintenanceStaff = new MaintenanceStaff(),
                Apartment = new Apartment() { BuildingId = new Guid(), ApartmentId = new Guid() }
            };

            _buildingRepositoryMock.Setup(repo => repo.GetApartment(_managerID, It.IsAny<Guid>())).Returns((Apartment)null);

            Exception exception = null;
            // Act
            try
            {
                _requestLogic.CreateRequest(_managerID, request);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.IsTrue(exception.Message.Equals("Apartment does not exist."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateRequest_ValidatesEmptyGuid_DontCreateRequest()
        {
            Request_ request = new Request_();

            Exception exception = null;

            try
            {
                _requestLogic.CreateRequest(_managerID, request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.IsTrue(exception.Message.Equals("Can not create an empty Request."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateRequest_ValidatesEmptyCategoryID_DontCreateRequest()
        {
            Request_ request = new Request_()
            {
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                EndTime = DateTime.Now,
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Finished,
                TotalCost = 1000,
                MaintenanceStaff = new MaintenanceStaff(),
            };

            Exception exception = null;

            try
            {

                _requestLogic.CreateRequest(_managerID, request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.IsTrue(exception.Message.Equals("Category can not be empty."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateRequest_ValidatesEmptyDescription_DontCreateRequest()
        {
            Request_ request = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "",
                EndTime = DateTime.Now,
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Finished,
                TotalCost = 1000,
                MaintenanceStaff = new MaintenanceStaff()
            };

            Exception exception = null;

            try
            {

                _requestLogic.CreateRequest(_managerID, request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.IsTrue(exception.Message.Equals("Description can not be empty."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateRequest_ValidatesNullDescription_DontCreateRequest()
        {
            Request_ request = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                EndTime = DateTime.Now,
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Finished,
                TotalCost = 1000,
                MaintenanceStaff = new MaintenanceStaff()
            };

            Exception exception = null;

            try
            {

                _requestLogic.CreateRequest(_managerID, request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.IsTrue(exception.Message.Equals("Description can not be empty."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateRequest_ValidatesEmptyCreationTime_DontCreateRequest()
        {
            Request_ request = new Request_()
            {
                CategoryID = 1,
                //CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                EndTime = DateTime.Now,
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Finished,
                TotalCost = 1000,
                MaintenanceStaff = new MaintenanceStaff()
            };

            Exception exception = null;

            try
            {

                _requestLogic.CreateRequest(_managerID, request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.IsTrue(exception.Message.Equals("Creation Time can not be empty."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateRequest_ValidatesStatusPending_EndTimeMustBeEmpty()
        {
            Request_ request = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                EndTime = DateTime.Now,
                Id = Guid.NewGuid(),
                //StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Pending,
                TotalCost = 1000,
                MaintenanceStaff = new MaintenanceStaff()
            };

            Exception exception = null;

            try
            {
                _requestLogic.CreateRequest(_managerID, request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.IsTrue(exception.Message.Equals("If status is Pending, Start Time or End Time need be empty."));

            _requestRepositoryMock.VerifyAll();
        }


        [TestMethod]
        public void CreateRequest_ValidatesStatusPending_StartTimeMustBeEmpty()
        {
            Request_ request = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                //EndTime = DateTime.Now,
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Pending,
                TotalCost = 1000,
                MaintenanceStaff = new MaintenanceStaff()
            };

            Exception exception = null;

            try
            {

                _requestLogic.CreateRequest(_managerID, request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.IsTrue(exception.Message.Equals("If status is Pending, Start Time or End Time need be empty."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateRequest_ValidatesStatusActive_StartTimeCanNotBeEmpty()
        {
            Request_ request = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                EndTime = DateTime.Now,
                Id = Guid.NewGuid(),
                //StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Active,
                TotalCost = 1000,
                MaintenanceStaff = new MaintenanceStaff()
            };

            Exception exception = null;

            try
            {

                _requestLogic.CreateRequest(_managerID, request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.IsTrue(exception.Message.Equals("If status is Active, Start Time can not be empty."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateRequest_ValidatesStatusActive_EndTimeMustBeEmpty()
        {
            Request_ request = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                EndTime = DateTime.Now,
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Active,
                TotalCost = 1000,
                MaintenanceStaff = new MaintenanceStaff()
            };

            Exception exception = null;

            try
            {

                _requestLogic.CreateRequest(_managerID, request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.IsTrue(exception.Message.Equals("If status is Active, End Time need to be empty."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateRequest_ValidatesStatusActive_CreationTimeMustGreaterthanStartTime()
        {
            Request_ request = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now,
                Description = "description A",
                //EndTime = DateTime.Now,
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Active,
                TotalCost = 1000,
                MaintenanceStaff = new MaintenanceStaff()
            };

            Exception exception = null;

            try
            {

                _requestLogic.CreateRequest(_managerID, request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.IsTrue(exception.Message.Equals("Start time can not be less than Creation Time."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateRequest_ValidatesStatusActive_MaintenancePersonIdCanNotBeEmpty()
        {
            Request_ request = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now,
                Description = "description A",
                //EndTime = DateTime.Now,
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Active,
                TotalCost = 1000
            };

            Exception exception = null;

            try
            {

                _requestLogic.CreateRequest(_managerID, request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.IsTrue(exception.Message.Equals("Start time can not be less than Creation Time."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateRequest_ValidatesStatusFinished_CreationTimeMustGreaterthanStartTime()
        {
            Request_ request = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now,
                Description = "description A",
                EndTime = DateTime.Now,
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Finished,
                TotalCost = 1000,
                MaintenanceStaff = new MaintenanceStaff()
            };

            Exception exception = null;

            try
            {

                _requestLogic.CreateRequest(_managerID, request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.IsTrue(exception.Message.Equals("Start time can not be less than Creation Time."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateRequest_ValidatesStatusFinished_EndTimeMustGreaterthanCreationTime()
        {
            Request_ request = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now,
                Description = "description A",
                EndTime = DateTime.Now.AddDays(-1),
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(1),
                Status = Status.Finished,
                TotalCost = 1000,
                MaintenanceStaff = new MaintenanceStaff()
            };

            Exception exception = null;

            try
            {

                _requestLogic.CreateRequest(_managerID, request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.IsTrue(exception.Message.Equals("End time can not be less than Creation Time."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateRequest_ValidatesStatusFinished_EndTimeCanNotBeEmpty()
        {
            Request_ request = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                //EndTime = DateTime.Now,
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Finished,
                TotalCost = 1000,
                MaintenanceStaff = new MaintenanceStaff()
            };

            Exception exception = null;

            try
            {
                _requestLogic.CreateRequest(_managerID, request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.IsTrue(exception.Message.Equals("If status is Finished, End Time can not be empty."));

            _requestRepositoryMock.VerifyAll();
        }

        // End - CreateRequest

        // Start - GetAllRequest

        [TestMethod]
        public void GetAllRequest_ShouldReturnAllRequest()
        {
            MaintenanceStaff staff = new MaintenanceStaff()
            {
                ID = new Guid(),
            };

            List<Request_> expectedRequests = new List<Request_>()
            {
                new Request_()
                {
                    CategoryID = 1,
                    CreationTime = DateTime.Now.AddDays(-2),
                    Description = "description A",
                    Id = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddDays(-1),
                    Status = Status.Active,
                    MaintenanceStaff = staff
                },
                new Request_()
                {
                    CategoryID = 1,
                    CreationTime = DateTime.Now.AddDays(-2),
                    Description = "description A",
                    EndTime = DateTime.Now,
                    Id = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddDays(-1),
                    Status = Status.Finished,
                    TotalCost = 1000,
                    MaintenanceStaff = staff
                },
                new Request_()
                {
                    Id = new Guid(),
                    Description = "Description A",
                    Status = Status.Pending,
                    CategoryID = 3,
                    CreationTime = DateTime.Now.AddDays(-1),
                    MaintenanceStaff = staff
                }
            };

            _requestRepositoryMock.Setup(repository => repository.GetAllRequest(_managerID)).Returns(expectedRequests);
            _managerRepositoryMock.Setup(r => r.Get(_managerID)).Returns(_managerID);

            // Act


            IEnumerable<Request_> result = _requestLogic.GetAllRequest(_managerID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedRequests.Count, result.ToList().Count);

            _requestRepositoryMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void GetAllRequest_ByCategoryID_ReturnsAllRequestsOfCategoryID()
        {
            // Arrange
            int category = 1;
            List<Request_> expectedRequests = new List<Request_>()
            {
                new Request_()
                {
                    CategoryID = category,
                    CreationTime = DateTime.Now.AddDays(-2),
                    Description = "description A",
                    Id = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddDays(-1),
                    Status = Status.Active,
                    MaintenanceStaff = new MaintenanceStaff()
                },
                new Request_()
                {
                    CategoryID = category,
                    CreationTime = DateTime.Now.AddDays(-2),
                    Description = "description A",
                    EndTime = DateTime.Now,
                    Id = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddDays(-1),
                    Status = Status.Finished,
                    TotalCost = 1000,
                    MaintenanceStaff = new MaintenanceStaff()
                }
            };

            _requestRepositoryMock.Setup(repository => repository.GetAllRequest(_managerID, category)).Returns(expectedRequests);

            // Act



            IEnumerable<Request_> result = _requestLogic.GetAllRequest(_managerID, category);

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expectedRequests, result.ToList());
            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void TerminateRequest_ValidIdEndTimeAndTotalCost_ChangesStatusToFinished()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            DateTime endTime = DateTime.Now;
            float totalCost = 1000;

            Request_ finishedRequest = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                EndTime = endTime,
                Id = id,
                StartTime = DateTime.Now.AddDays(-2),
                Status = Status.Active,
                TotalCost = totalCost,
                MaintenanceStaff = new MaintenanceStaff() { ID = _managerID },
                MaintenanceStaffId = _managerID
            };

            _requestRepositoryMock.Setup(repository => repository.Get(id)).Returns(finishedRequest);
            _requestRepositoryMock.Setup(repository => repository.Update(finishedRequest));

            // Act


            Request_ result = _requestLogic.TerminateRequest(_managerID, id, endTime, totalCost);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(finishedRequest, result);
            Assert.AreEqual(Status.Finished, result.Status);
            Assert.AreEqual(endTime, result.EndTime);
            Assert.AreEqual(totalCost, result.TotalCost);
            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void AsignMaintenancePerson_InvalidRequest_DontAsignsPersonToRequest()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            DateTime startTime = DateTime.Now.AddDays(-1);
            var maintenanceStaff = new MaintenanceStaff();

            Request_ updatedRequest = new Request_();

            _requestRepositoryMock.Setup(repository => repository.Get(id)).Returns((Request_)null);
            _staffRepositoryMock.Setup(repository => repository.Get(maintenanceStaff.ID))
                .Returns(maintenanceStaff);
            Exception exception = null;
            // Act
            try
            {
                _requestLogic.ActivateRequest(id, maintenanceStaff.ID, startTime);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.IsTrue(exception.Message.Equals("Request does not exist."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void AsignMaintenancePerson_ValidRequest_Succes()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            DateTime startTime = DateTime.Now.AddDays(-1);
            var maintenanceStaff = new MaintenanceStaff()
            {
                ID = new Guid()
            };

            Request_ assignedRequest = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                Id = id,
                Status = Status.Active
            };

            _requestRepositoryMock.Setup(repository => repository.Get(id)).Returns(assignedRequest);
            _staffRepositoryMock.Setup(repository => repository.Get(maintenanceStaff.ID))
                .Returns(maintenanceStaff);
            _requestRepositoryMock.Setup(r => r.Update(assignedRequest));

            // Act
            var response = _requestLogic.ActivateRequest(id, maintenanceStaff.ID, startTime);


            Assert.IsNotNull(response);
            Assert.AreEqual(response.Id, assignedRequest.Id);

            _requestRepositoryMock.VerifyAll();
            _staffRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void AsignMaintenancePerson_InvalidMaintenanceStaff_DontAsignsPersonToRequest()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            DateTime startTime = DateTime.Now.AddDays(-1);
            var maintenanceStaff = new MaintenanceStaff();

            Request_ updatedRequest = new Request_();

            _staffRepositoryMock.Setup(repository => repository.Get(maintenanceStaff.ID))
                .Returns((MaintenanceStaff)null);
            Exception exception = null;
            // Act
            try
            {
                _requestLogic.ActivateRequest(id, maintenanceStaff.ID, startTime);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.IsTrue(exception.Message.Equals("Maintenance staff does not exist."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void TerminateRequest_InvalidRequest_ThrowError()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            DateTime endTime = DateTime.Now;
            var requestId = new Guid();

            Request_ updatedRequest = new Request_();

            _requestRepositoryMock.Setup(repository =>
                repository.Get(requestId)).Returns((Request_)null);

            Exception exception = null;
            // Act
            try
            {
                _requestLogic.TerminateRequest(_managerID, requestId, endTime, 1000);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.IsTrue(exception.Message.Equals("Request does not exist."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void TerminateRequest_InvalidRequestStartTime_ThrowError()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            DateTime endTime = DateTime.Now.AddDays(-1);
            var requestId = new Guid();
            var request = new Request_()
            {
                Id = requestId,
                StartTime = DateTime.Now,
                MaintenanceStaffId = _managerID,
                Status = Status.Active
            };

            _requestRepositoryMock.Setup(repository =>
                repository.Get(requestId)).Returns(request);

            Exception exception = null;
            // Act
            try
            {
                _requestLogic.TerminateRequest(_managerID, requestId, endTime, 1000);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.IsTrue(exception.Message.Equals("End time has to be greater than Start time."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void TerminateRequest_InvalidRequestCreationTime_ThrowError()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            DateTime endTime = DateTime.Now.AddDays(-1);
            var requestId = new Guid();
            var request = new Request_()
            {
                Id = requestId,
                CreationTime = DateTime.Now,
                MaintenanceStaffId = _managerID,
                Status = Status.Active
            };

            _requestRepositoryMock.Setup(repository =>
                repository.Get(requestId)).Returns(request);

            Exception exception = null;
            // Act
            try
            {
                _requestLogic.TerminateRequest(_managerID, requestId, endTime, 1000);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.IsTrue(exception.Message.Equals("End time has to be greater than Creation time."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateRequest_InvalidMaintenancePerson_ThrowError()
        {
            Building building = new Building()
            {
                BuildingId = Guid.NewGuid()
            };

            Apartment apartment = new Apartment()
            {
                BuildingId = building.BuildingId
            };

            Request_ request = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Active,
                MaintenanceStaff = null,
                Apartment = apartment
            };
            Exception exception = null;
            // Act
            try
            {
                _requestLogic.CreateRequest(_managerID, request);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.IsTrue(exception.Message.Equals("A Maintenance Person must to be asigned."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void GetAllRequestOfStaff()
        {
            // Arrange
            Guid staffId = Guid.NewGuid();
            List<Request_> expectedRequests = new List<Request_>()
            {
                new Request_()
                {
                    CategoryID = 1,
                    CreationTime = DateTime.Now.AddDays(-2),
                    Description = "description A",
                    Id = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddDays(-1),
                    Status = Status.Active,
                    MaintenanceStaffId = staffId
                },
                new Request_()
                {
                    CategoryID = 1,
                    CreationTime = DateTime.Now.AddDays(-2),
                    Description = "description A",
                    EndTime = DateTime.Now,
                    Id = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddDays(-1),
                    Status = Status.Finished,
                    TotalCost = 1000,
                    MaintenanceStaffId = staffId
                }
            };

            _managerRepositoryMock.Setup(r => r.Get(It.IsAny<Guid>())).Returns(Guid.Empty);
            _requestRepositoryMock.Setup(repository => repository.GetAllRequestStaff(staffId)).Returns(expectedRequests);

            // Act

            IEnumerable<Request_> result = _requestLogic.GetAllRequest(staffId);

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expectedRequests, result.ToList());
            _requestRepositoryMock.VerifyAll();
            _managerRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void TerminateRequest_Throw_RequestIsNotActive()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            DateTime endTime = DateTime.Now;
            float totalCost = 1000;

            Request_ finishedRequest = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                EndTime = endTime,
                Id = id,
                StartTime = DateTime.Now.AddDays(-2),
                Status = Status.Finished,
                TotalCost = totalCost,
                MaintenanceStaff = new MaintenanceStaff() { ID = _managerID },
                MaintenanceStaffId = _managerID
            };

            _requestRepositoryMock.Setup(repository => repository.Get(id)).Returns(finishedRequest);

            Exception exception = null;
            // Act
            try
            {
                _requestLogic.TerminateRequest(_managerID, id, endTime, totalCost);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.IsTrue(exception.Message.Equals("Request is not active."));
            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void TerminateRequest_Throw_EndTimeLessThanStartTime()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            float totalCost = 1000;

            Request_ finishedRequest = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                EndTime = DateTime.MinValue,
                Id = id,
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Finished,
                TotalCost = totalCost,
                MaintenanceStaff = new MaintenanceStaff() { ID = _managerID },
                MaintenanceStaffId = _managerID
            };

            Exception exception = null;
            // Act
            try
            {
                _requestLogic.CreateRequest(_managerID, finishedRequest);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.IsTrue(exception.Message.Equals("If status is Finished, End Time can not be empty."));
        }

        [TestMethod]
        public void TerminateRequest_Throw_ApartmentError()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            float totalCost = 1000;

            Request_ finishedRequest = new Request_()
            {
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                EndTime = DateTime.Now,
                Id = id,
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Finished,
                TotalCost = totalCost,
                MaintenanceStaff = new MaintenanceStaff() { ID = _managerID },
                MaintenanceStaffId = _managerID,
                Apartment = new Apartment() { ApartmentId = Guid.NewGuid() }
            };

            _buildingRepositoryMock.Setup(repo => repo.GetApartment(_managerID, It.IsAny<Guid>())).Returns((Apartment)null);

            Exception exception = null;
            // Act
            try
            {
                _requestLogic.CreateRequest(_managerID, finishedRequest);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.IsTrue(exception.Message.Equals("Apartment does not exist."));
        }
    }

}