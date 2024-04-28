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
                TotalCost = 1000
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
            Assert.IsTrue(exception.Message.Equals("If status is Finished, End Time can not be empty."));

            _requestRepositoryMock.VerifyAll();
        }

        // End - CreateRequest
    }
}