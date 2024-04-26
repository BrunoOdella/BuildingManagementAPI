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
        private Mock<IInvitationLogic> _invitationLogicMock;
        private InvitationsController _invitationsController;

        [TestInitialize]
        public void TestSetup()
        {
            _invitationLogicMock = new Mock<IInvitationLogic>(MockBehavior.Strict);
            _invitationsController = new InvitationsController(_invitationLogicMock.Object);
        }


        [TestMethod]
        public void PostInvitation_ShouldReturnCreatedResponse()
        {
            // Arrange
            CreateInvitationRequest newCreateInvitationRequest = new CreateInvitationRequest
            {
                Email = "mairafraga@mail.com",
                Name = "maira",
                ExpirationDate = DateTime.UtcNow,
            };

            Invitation invitationEntity = new Invitation
            {
                InvitationId= Guid.Parse("d8119d3a-f0f1-4451-a0c3-d4abd21e13aa"),
                Email = "mairafraga@mail.com",
                Name = "maira",
                ExpirationDate = DateTime.UtcNow,
                Status = "pendiente"
            };

            CreateInvitationResponse response = new CreateInvitationResponse(invitationEntity);
            _invitationLogicMock.Setup(logic => logic.CreateInvitation(It.IsAny<Invitation>())).Returns(invitationEntity);

            // Act
            ObjectResult result = _invitationsController.CreateInvitation(newCreateInvitationRequest) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(response, result.Value);

            _invitationLogicMock.VerifyAll();
        }
    }
}