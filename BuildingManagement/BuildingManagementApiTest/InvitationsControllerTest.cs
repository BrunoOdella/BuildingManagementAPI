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
    public class InvitationsControllerTest
    {
        private Mock<IInvitationLogic> _InvitationsLogicMock;
        private InvitationsController _invitationsController;

        [TestInitialize]
        public void TestSetup()
        {
            _invitationLogicMock = new Mock<IInvitationLogic>(MockBehavior.Strict);
            _invitationsController = new AdminsController(_invitationLogicMock.Object);
        }
    }
}