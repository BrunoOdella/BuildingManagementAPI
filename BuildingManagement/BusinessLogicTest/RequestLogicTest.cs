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
        public void CreateRequest_ValidatesData_DontCreateRequest()
        {
            Request_ request = new Request_();

            _requestRepositoryMock.Setup(repository => repository.CreateRequest(It.IsAny<Request_>())).Throws(new ArgumentException("Can not create an empty Request."));

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


    }
}