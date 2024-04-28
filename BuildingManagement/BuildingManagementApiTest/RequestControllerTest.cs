using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using BuildingManagementApi.Controllers;
using Domain;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Models.In;
using Models.Out;
using Moq;

namespace BuildingManagementApiTest
{
    [TestClass]
    public class RequestControllerTest
    {
        private Mock<IRequestLogic> _RlogicMock;
        private RequestsController _Rcontroller;

        [TestInitialize]
        public void TestSetup()
        {
            _RlogicMock = new Mock<IRequestLogic>(MockBehavior.Strict);
            _Rcontroller = new RequestsController(_RlogicMock.Object);
        }

        [TestMethod]
        public void GetRequest_ShouldReturnAllRequest()
        {
            Request_ ActiveRequest = new Request_()
            {
                Id = new Guid(),
                Description = "Description A",
                Status = Status.Active,
                Category = 1,
                CreationTime = DateTime.Now.AddDays(-1),
                StartTime = DateTime.Now
            };

            Request_ FinishedRequest = new Request_()
            {
                Id = new Guid(),
                Description = "Description A",
                Status = Status.Finished,
                Category = 2,
                CreationTime = DateTime.Now.AddDays(-2),
                StartTime = DateTime.Now.AddDays(-1),
                EndTime = DateTime.Now,
                TotalCost = 1000
            };

            Request_ PendingRequest = new Request_()
            {
                Id = new Guid(),
                Description = "Description A",
                Status = Status.Pending,
                Category = 3,
                CreationTime = DateTime.Now.AddDays(-1),
            };

            IEnumerable<Request_> expected = new List<Request_>() { ActiveRequest, PendingRequest, FinishedRequest };

            List<RequestResponse> response = expected.Select(request => new RequestResponse(request)).ToList();
            _RlogicMock.Setup(logic => logic.GetAllRequest()).Returns(expected);

            ObjectResult result = _Rcontroller.GetAllRequest(null) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            CollectionAssert.AreEqual(response, (System.Collections.ICollection?) result.Value);

            _RlogicMock.VerifyAll();
        }

        [TestMethod]
        public void GetRequest_ShouldReturnCategory1Request()
        {
            Request_ ActiveRequest = new Request_()
            {
                Id = new Guid(),
                Description = "Description A",
                Status = Status.Active,
                Category = 1,
                CreationTime = DateTime.Now.AddDays(-1),
                StartTime = DateTime.Now
            };

            Request_ FinishedRequest = new Request_()
            {
                Id = new Guid(),
                Description = "Description A",
                Status = Status.Finished,
                Category = 2,
                CreationTime = DateTime.Now.AddDays(-2),
                StartTime = DateTime.Now.AddDays(-1),
                EndTime = DateTime.Now,
                TotalCost = 1000
            };

            Request_ PendingRequest = new Request_()
            {
                Id = new Guid(),
                Description = "Description A",
                Status = Status.Pending,
                Category = 3,
                CreationTime = DateTime.Now.AddDays(-1),
            };

            IEnumerable<Request_> expected = new List<Request_>() { ActiveRequest};

            List<RequestResponse> response = expected.Select(request => new RequestResponse(request)).ToList();
            _RlogicMock.Setup(logic => logic.GetAllRequest(1)).Returns(expected);

            ObjectResult result = _Rcontroller.GetAllRequest("1") as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            CollectionAssert.AreEqual(response, (System.Collections.ICollection?)result.Value);

            _RlogicMock.VerifyAll();
        }

        [TestMethod]
        public void PutRequest_ShouldActivateResponse()
        {
            var id = new Guid();

            Request_ ActiveRequest = new Request_()
            {
                Id = id,
                Description = "Description A",
                Status = Status.Active,
                Category = 1,
                CreationTime = DateTime.Now.AddDays(-1),
                StartTime = DateTime.Now
            };

            //RequestResponse response = new RequestResponse(ActiveRequest);
            _RlogicMock.Setup(logic => logic.ActivateRequest(It.IsAny<Guid>(), It.IsAny<DateTime>())).Returns(ActiveRequest);

            var aux = new ActiveRequest();
            aux.StartTime = DateTime.Now;

            ObjectResult result = _Rcontroller.PutActivateRequest(id.ToString(), aux);

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            //CollectionAssert.AreEqual(response, (System.Collections.ICollection?)result.Value);


            _RlogicMock.VerifyAll();
        }

        [TestMethod]
        public void PutRequest_ShouldTerminateResponse()
        {
            var id = new Guid();

            Request_ FinishedRequest = new Request_()
            {
                Id = new Guid(),
                Description = "Description A",
                Status = Status.Finished,
                Category = 2,
                CreationTime = DateTime.Now.AddDays(-2),
                StartTime = DateTime.Now.AddDays(-1),
                EndTime = DateTime.Now,
                TotalCost = 1000
            };

            //RequestResponse response = new RequestResponse(FinishedRequest);
            _RlogicMock.Setup(logic => logic.TerminateRequest(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<float>())).Returns(FinishedRequest);

            var aux = new FinishedRequest();
            aux.EndTime = DateTime.Now;
            aux.TotalCost = 1000;

            ObjectResult result = _Rcontroller.PutFinishedRequest(id.ToString(), aux);

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            //CollectionAssert.AreEqual(response, (System.Collections.ICollection?)result.Value);

            _RlogicMock.VerifyAll();
        }

        [TestMethod]
        public void PutRequest_ShouldAsignMaintenancePersonResponse()
        {
            var id = new Guid();

            Request_ ActiveRequest = new Request_()
            {
                Id = new Guid(),
                Description = "Description A",
                Status = Status.Active,
                Category = 1,
                CreationTime = DateTime.Now.AddDays(-1),
                StartTime = DateTime.Now
            };

            //RequestResponse response = new RequestResponse(ActiveRequest);
            _RlogicMock.Setup(logic => logic.AsignMaintenancePerson(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(ActiveRequest);

            var aux = new MaintenancePersonRequest();
            aux.Id = new Guid();

            ObjectResult result = _Rcontroller.PutMaintenancePersonRequest(id.ToString(), aux);

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            //CollectionAssert.AreEqual(response, (System.Collections.ICollection?)result.Value);

            _RlogicMock.VerifyAll();
        }
    }
}