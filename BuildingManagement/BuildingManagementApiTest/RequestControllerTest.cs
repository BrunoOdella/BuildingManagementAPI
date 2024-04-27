using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingManagementApi.Controllers;
using Domain;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;
using Moq;

namespace BuildingManagementApiTest
{
    [TestClass]
    public class RequestControllerTest
    {
        private Mock<IRequestsLogic> _RlogicMock;
        private RequestsController _Rcontroller;

        [TestInitialize]
        public void TestSetup()
        {
            _RlogicMock = new Mock<IRequestsLogic>(MockBehavior.Strict);
            _Rcontroller = new RequestsController(_RlogicMock.Object);
        }
    }
}