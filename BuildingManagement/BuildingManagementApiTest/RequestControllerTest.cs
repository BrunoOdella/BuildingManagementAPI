﻿using BuildingManagementApi.Controllers;
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
    public class RequestControllerTest
    {
        private Mock<IRequestLogic> _RlogicMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private RequestsController _Rcontroller;

        [TestInitialize]
        public void TestSetup()
        {
            _RlogicMock = new Mock<IRequestLogic>(MockBehavior.Strict);
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>(MockBehavior.Strict);

            var httpContext = new DefaultHttpContext();
            Guid expectedUserID = Guid.NewGuid(); // o cualquier Guid que esperes
            httpContext.Items["userID"] = expectedUserID.ToString();
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            // Crear el controlador con los mocks
            _Rcontroller = new RequestsController(_RlogicMock.Object, _httpContextAccessorMock.Object);
        }

        [TestMethod]
        public void GetRequest_ShouldReturnAllRequest()
        {
            Request_ ActiveRequest = new Request_()
            {
                Id = new Guid(),
                Description = "Description A",
                Status = Status.Active,
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-1),
                StartTime = DateTime.Now
            };

            Request_ FinishedRequest = new Request_()
            {
                Id = new Guid(),
                Description = "Description A",
                Status = Status.Finished,
                CategoryID = 2,
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
                CategoryID = 3,
                CreationTime = DateTime.Now.AddDays(-1),
            };

            IEnumerable<Request_> expected = new List<Request_>() { ActiveRequest, PendingRequest, FinishedRequest };

            List<RequestResponse> response = expected.Select(request => new RequestResponse(request)).ToList();
            string userIDString = _httpContextAccessorMock.Object.HttpContext.Items["userID"] as string;
            Guid userID = new Guid(userIDString);
            _RlogicMock.Setup(logic => logic.GetAllRequest(userID)).Returns(expected);

            ObjectResult result = _Rcontroller.GetAllRequest(null) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            CollectionAssert.AreEqual(response, (System.Collections.ICollection?)result.Value);

            _RlogicMock.VerifyAll();
        }

        [TestMethod]
        public void GetRequest_ShouldReturnCategoryID1Request()
        {
            Request_ ActiveRequest = new Request_()
            {
                Id = new Guid(),
                Description = "Description A",
                Status = Status.Active,
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-1),
                StartTime = DateTime.Now
            };

            Request_ FinishedRequest = new Request_()
            {
                Id = new Guid(),
                Description = "Description A",
                Status = Status.Finished,
                CategoryID = 2,
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
                CategoryID = 3,
                CreationTime = DateTime.Now.AddDays(-1),
            };

            IEnumerable<Request_> expected = new List<Request_>() { ActiveRequest };

            List<RequestResponse> response = expected.Select(request => new RequestResponse(request)).ToList();

            string userIDString = _httpContextAccessorMock.Object.HttpContext.Items["userID"] as string;
            Guid userID = Guid.Parse(userIDString);

            _RlogicMock.Setup(logic => logic.GetAllRequest(userID, 1)).Returns(expected);

            ObjectResult result = _Rcontroller.GetAllRequest(1) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            CollectionAssert.AreEqual(response, (System.Collections.ICollection?)result.Value);

            _RlogicMock.VerifyAll();
        }

        /*
        [TestMethod]
        public void PutRequest_ShouldActivateResponse()
        {
            var id = new Guid();

            Request_ ActiveRequest = new Request_()
            {
                Id = id,
                Description = "Description A",
                Status = Status.Active,
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-1),
                StartTime = DateTime.Now
            };

            //RequestResponse response = new RequestResponse(ActiveRequest);
            string userIDString = _httpContextAccessorMock.Object.HttpContext.Items["userID"] as string;
            Guid userID = Guid.Parse(userIDString);

            _RlogicMock.Setup(logic => logic.ActivateRequest(userID, It.IsAny<Guid>(), It.IsAny<DateTime>())).Returns(ActiveRequest);

            var aux = new ActivateRequest();
            aux.StartTime = DateTime.Now;

            ObjectResult result = _Rcontroller.PutActivateRequest(id.ToString(), aux);

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            //CollectionAssert.AreEqual(response, (System.Collections.ICollection?)result.Value);


            _RlogicMock.VerifyAll();
        }
        */

        [TestMethod]
        public void PutRequest_ShouldTerminateResponse()
        {
            var id = new Guid();

            Request_ FinishedRequest = new Request_()
            {
                Id = new Guid(),
                Description = "Description A",
                Status = Status.Finished,
                CategoryID = 2,
                CreationTime = DateTime.Now.AddDays(-2),
                StartTime = DateTime.Now.AddDays(-1),
                EndTime = DateTime.Now,
                TotalCost = 1000
            };

            //RequestResponse response = new RequestResponse(FinishedRequest);
            string userIDString = _httpContextAccessorMock.Object.HttpContext.Items["userID"] as string;
            Guid userID = Guid.Parse(userIDString);

            _RlogicMock.Setup(logic => logic.TerminateRequest(userID, It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<float>())).Returns(FinishedRequest);

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
        public void PutRequest_ShouldActivateRequest()
        {
            var id = new Guid();

            ActivateRequest activeRequestRequest = new ActivateRequest()
            {
                Status = Status.Active,
                StartTime = DateTime.Now,
                MaintenancePersonId = id
            };

            Request_ ActiveRequest = new Request_()
            {
                Id = new Guid(),
                Description = "Description A",
                Status = Status.Active,
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-1),
                StartTime = activeRequestRequest.StartTime,
                MaintenanceStaffId = activeRequestRequest.MaintenancePersonId
            };


            _RlogicMock.Setup(logic => logic.ActivateRequest(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<DateTime>())).Returns(ActiveRequest);

            var result = _Rcontroller.PutMaintenancePersonRequest(ActiveRequest.Id.ToString(), activeRequestRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var response = result.Value as RequestResponse;

            Assert.IsNotNull(response);
            Assert.AreEqual(ActiveRequest.Id, response.Id);
            Assert.AreEqual(ActiveRequest.Description, response.Description);

            _RlogicMock.VerifyAll();
        }

        [TestMethod]
        public void PostRequest_ShouldCreateRequestResponse()
        {
            var id = Guid.NewGuid();

            CreateRequestRequest createdRequest = new CreateRequestRequest()
            {
                Description = "Description A",
                Category = 1,
                CreationTime = DateTime.Now.AddDays(-1),
                ApartmentID = new Guid()
            };



            Request_ request = new Request_()
            {
                Id = id,
                Status = Status.Pending,
                Description = "Description A",
                CategoryID = 1,
                CreationTime = DateTime.Now.AddDays(-1),
                Apartment = new Apartment() { ApartmentId = createdRequest.ApartmentID }
            };

            CreateRequestResponse response = new CreateRequestResponse(request);

            string userIDString = _httpContextAccessorMock.Object.HttpContext.Items["userID"] as string;
            Guid userID = Guid.Parse(userIDString);

            _RlogicMock.Setup(logic => logic.CreateRequest(userID, It.IsAny<Request_>())).Returns(request);

            ObjectResult result = _Rcontroller.PostRequest(createdRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(response, result.Value);

            _RlogicMock.VerifyAll();
        }
    }
}