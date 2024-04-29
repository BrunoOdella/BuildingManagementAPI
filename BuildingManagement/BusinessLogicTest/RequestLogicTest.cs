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
        private RequestLogic _requestLogic;

        [TestInitialize]
        public void TestSetup()
        {
            _requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            _requestLogic = new RequestLogic(_requestRepositoryMock.Object);
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
                MaintenancePersonId = new Guid()
            };

            _requestRepositoryMock.Setup(repository => repository.CreateRequest(It.IsAny<Request_>())).Returns(request);

            Request_ result = _requestLogic.CreateRequest(request);

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
                _requestLogic.CreateRequest(request);
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
                MaintenancePersonId = new Guid()
            };

            Exception exception = null;

            try
            {
                _requestLogic.CreateRequest(request);
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
                MaintenancePersonId = new Guid()
            };

            Exception exception = null;

            try
            {
                _requestLogic.CreateRequest(request);
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
                MaintenancePersonId = new Guid()
            };

            Exception exception = null;

            try
            {
                _requestLogic.CreateRequest(request);
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
                MaintenancePersonId = new Guid()
            };

            Exception exception = null;

            try
            {
                _requestLogic.CreateRequest(request);
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
                _requestLogic.CreateRequest(request);
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
                _requestLogic.CreateRequest(request);
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
                MaintenancePersonId = new Guid()
            };

            Exception exception = null;

            try
            {
                _requestLogic.CreateRequest(request);
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
                MaintenancePersonId = new Guid()
            };

            Exception exception = null;

            try
            {
                _requestLogic.CreateRequest(request);
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
                MaintenancePersonId = new Guid()
            };

            Exception exception = null;

            try
            {
                _requestLogic.CreateRequest(request);
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
                _requestLogic.CreateRequest(request);
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
                MaintenancePersonId = new Guid()
            };

            Exception exception = null;

            try
            {
                _requestLogic.CreateRequest(request);
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
                MaintenancePersonId = new Guid()
            };

            Exception exception = null;

            try
            {
                _requestLogic.CreateRequest(request);
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
                MaintenancePersonId = new Guid()
            };

            Exception exception = null;

            try
            {
                _requestLogic.CreateRequest(request);
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
                    MaintenancePersonId = new Guid()
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
                    MaintenancePersonId = new Guid()
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

            _requestRepositoryMock.Setup(repository => repository.GetAllRequest()).Returns(expectedRequests);

            // Act
            IEnumerable<Request_> result = _requestLogic.GetAllRequest();

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
                    MaintenancePersonId = new Guid()
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
                    MaintenancePersonId = new Guid()
                }
            };

            _requestRepositoryMock.Setup(repository => repository.GetAllRequest(category)).Returns(expectedRequests);

            // Act
            IEnumerable<Request_> result = _requestLogic.GetAllRequest(category);

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

            Request_ updatedRequest = new Request_()
            {
                Category = 1,
                CreationTime = DateTime.Now.AddDays(-2),
                Description = "description A",
                Id = id,
                StartTime = startTime,
                Status = Status.Active,
                MaintenancePersonId = new Guid()
            };

            _requestRepositoryMock.Setup(repository => repository.ActivateRequest(id, startTime)).Returns(updatedRequest);

            // Act
            Request_ result = _requestLogic.ActivateRequest(id, startTime);

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
                MaintenancePersonId = new Guid()
            };

            _requestRepositoryMock.Setup(repository => repository.TerminateRequest(id, endTime, totalCost)).Returns(finishedRequest);

            // Act
            Request_ result = _requestLogic.TerminateRequest(id, endTime, totalCost);

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
            Guid maintenancePersonId = Guid.NewGuid();
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
                MaintenancePersonId = maintenancePersonId
            };

            _requestRepositoryMock.Setup(repository => repository.AsignMaintenancePerson(requestGuid, maintenancePersonId)).Returns(assignedRequest);

            // Act
            Request_ result = _requestLogic.AsignMaintenancePerson(requestGuid, maintenancePersonId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(assignedRequest, result);
            Assert.AreEqual(maintenancePersonId, result.MaintenancePersonId);
            _requestRepositoryMock.VerifyAll();
        }


    }
}