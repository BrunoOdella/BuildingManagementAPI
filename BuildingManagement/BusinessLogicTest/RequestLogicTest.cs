using BusinessLogic.Logics;
using Domain;
using IDataAccess;
using LogicInterface.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTest
{
    [TestClass]
    public class RequestLogicTest
    {
        private Mock<IRequestRepository> _requestRepositoryMock;
        private Mock<IMaintenanceStaffRepository> _staffRepositoryMock;
        private RequestLogic _requestLogic;
        private Guid _managerID;

        [TestInitialize]
        public void TestSetup()
        {
            _requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            _staffRepositoryMock = new Mock<IMaintenanceStaffRepository>(MockBehavior.Strict);
            _requestLogic = new RequestLogic(_requestRepositoryMock.Object, _staffRepositoryMock.Object);
            _managerID = Guid.NewGuid();
        }

        // Start - CreateRequest 

        [TestMethod]
        public void CreateRequest_ValidatesData_CreateRequest()
        {
            Request_ request = new Request_()
            {
                Category = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                EndTime = DateTime.Now,
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Finished,
                TotalCost = 1000,
                MaintenanceStaff = new MaintenanceStaff()
            };

            _requestRepositoryMock.Setup(repository => repository.CreateRequest(_managerID, It.IsAny<Request_>())).Returns(request);


            Request_ result = _requestLogic.CreateRequest(_managerID, request);

            Assert.IsNotNull(result);
            Assert.AreEqual(request, result);
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
        public void CreateRequest_ValidatesEmptyCategory_DontCreateRequest()
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
            Assert.IsTrue(exception.Message.Equals("Category can not be empty."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateRequest_ValidatesEmptyDescription_DontCreateRequest()
        {
            Request_ request = new Request_()
            {
                Category = 1,
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
                Category = 1,
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
                Category = 1,
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
                Category = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                EndTime = DateTime.Now,
                Id = Guid.NewGuid(),
                //StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Pending,
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
            Assert.IsTrue(exception.Message.Equals("If status is Pending, Start Time or End Time need be empty."));

            _requestRepositoryMock.VerifyAll();
        }


        [TestMethod]
        public void CreateRequest_ValidatesStatusPending_StartTimeMustBeEmpty()
        {
            Request_ request = new Request_()
            {
                Category = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                //EndTime = DateTime.Now,
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1),
                Status = Status.Pending,
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
            Assert.IsTrue(exception.Message.Equals("If status is Pending, Start Time or End Time need be empty."));

            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void CreateRequest_ValidatesStatusActive_StartTimeCanNotBeEmpty()
        {
            Request_ request = new Request_()
            {
                Category = 1,
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
                Category = 1,
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
                Category = 1,
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
                Category = 1,
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
                Category = 1,
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
                Category = 1,
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
                Category = 1,
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
            List<Request_> expectedRequests = new List<Request_>()
            {
                new Request_()
                {
                    Category = 1,
                    CreationTime = DateTime.Now.AddDays(-2),
                    Description = "description A",
                    Id = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddDays(-1),
                    Status = Status.Active,
                    MaintenanceStaff = new MaintenanceStaff()
                },
                new Request_()
                {
                    Category = 1,
                    CreationTime = DateTime.Now.AddDays(-2),
                    Description = "description A",
                    EndTime = DateTime.Now,
                    Id = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddDays(-1),
                    Status = Status.Finished,
                    TotalCost = 1000,
                    MaintenanceStaff = new MaintenanceStaff()
                },
                new Request_()
                {
                    Id = new Guid(),
                    Description = "Description A",
                    Status = Status.Pending,
                    Category = 3,
                    CreationTime = DateTime.Now.AddDays(-1)
                }
            };

            _requestRepositoryMock.Setup(repository => repository.GetAllRequest(_managerID)).Returns(expectedRequests);

            // Act
            

            IEnumerable<Request_> result = _requestLogic.GetAllRequest(_managerID);

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expectedRequests, result.ToList());
           
            _requestRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void GetAllRequest_ByCategory_ReturnsAllRequestsOfCategory()
        {
            // Arrange
            int category = 1;
            List<Request_> expectedRequests = new List<Request_>()
            {
                new Request_()
                {
                    Category = category,
                    CreationTime = DateTime.Now.AddDays(-2),
                    Description = "description A",
                    Id = Guid.NewGuid(),
                    StartTime = DateTime.Now.AddDays(-1),
                    Status = Status.Active,
                    MaintenanceStaff = new MaintenanceStaff()
                },
                new Request_()
                {
                    Category = category,
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
        public void ActivateRequest_ValidIdAndStartTime_ChangesStatusToActive()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            DateTime startTime = DateTime.Now.AddDays(-1);
            var maintenanceStaff = new MaintenanceStaff();

            Request_ updatedRequest = new Request_()
            {
                Category = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                Id = id,
                StartTime = startTime,
                Status = Status.Active,
                MaintenanceStaff = maintenanceStaff
            };

            _requestRepositoryMock.Setup(repository => repository.GetRequest(_managerID, id)).Returns(updatedRequest);
            _staffRepositoryMock.Setup(repository => repository.GetMaintenanceStaff(_managerID, maintenanceStaff.ID))
                .Returns(maintenanceStaff);
            _requestRepositoryMock.Setup(repository => repository.Update(updatedRequest));
            _staffRepositoryMock.Setup(repository => repository.Update(maintenanceStaff));

            // Act

            Request_ result = _requestLogic.ActivateRequest(_managerID, id, maintenanceStaff.ID, startTime);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedRequest, result);
            Assert.AreEqual(Status.Active, result.Status);
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
                Category = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                EndTime = endTime,
                Id = id,
                StartTime = DateTime.Now.AddDays(-2),
                Status = Status.Finished,
                TotalCost = totalCost,
                MaintenanceStaff = new MaintenanceStaff()
            };

            _requestRepositoryMock.Setup(repository => repository.GetRequest(_managerID, id)).Returns(finishedRequest);
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
        public void AsignMaintenancePerson_ValidRequestAndPersonId_AsignsPersonToRequest()
        {
            // Arrange
            Guid requestGuid = Guid.NewGuid();
            var maintenancePerson = new MaintenanceStaff();
            Request_ unassignedRequest = new Request_()
            {
                Category = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                Id = requestGuid,
                Status = Status.Pending
            };

            Request_ assignedRequest = new Request_()
            {
                Category = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                Id = requestGuid,
                StartTime = DateTime.Now,
                Status = Status.Active,
                MaintenanceStaff = maintenancePerson
            };

            var maintenancePersonId = maintenancePerson.ID;

            _requestRepositoryMock.Setup(repository => repository.AsignMaintenancePerson(_managerID, requestGuid, maintenancePersonId)).Returns(assignedRequest);

            // Act
            

            Request_ result = _requestLogic.AsignMaintenancePerson(_managerID, requestGuid, maintenancePersonId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(assignedRequest, result);
            Assert.AreEqual(maintenancePersonId, result.MaintenanceStaff.ID);
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

            _requestRepositoryMock.Setup(repository => repository.GetRequest(_managerID, id)).Returns((Request_)null);
            _staffRepositoryMock.Setup(repository => repository.GetMaintenanceStaff(_managerID, maintenanceStaff.ID))
                .Returns(maintenanceStaff);
            Exception exception = null;
            // Act
            try
            {
                _requestLogic.ActivateRequest(_managerID, id, maintenanceStaff.ID, startTime);
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
        public void AsignMaintenancePerson_InvalidMaintenanceStaff_DontAsignsPersonToRequest()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            DateTime startTime = DateTime.Now.AddDays(-1);
            var maintenanceStaff = new MaintenanceStaff();

            Request_ updatedRequest = new Request_();

            _staffRepositoryMock.Setup(repository => repository.GetMaintenanceStaff(_managerID, maintenanceStaff.ID))
                .Returns((MaintenanceStaff)null);
            Exception exception = null;
            // Act
            try
            {
                _requestLogic.ActivateRequest(_managerID, id, maintenanceStaff.ID, startTime);
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
                repository.GetRequest(_managerID, requestId)).Returns((Request_)null);
            
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
                StartTime = DateTime.Now
            };

            _requestRepositoryMock.Setup(repository =>
                repository.GetRequest(_managerID, requestId)).Returns(request);

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
                CreationTime = DateTime.Now
            };

            _requestRepositoryMock.Setup(repository =>
                repository.GetRequest(_managerID, requestId)).Returns(request);

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
    }
}