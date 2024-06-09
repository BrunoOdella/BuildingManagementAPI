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
            CreateInvitationRequest newCreateInvitationRequest = new CreateInvitationRequest
            {
                Email = "mairafraga@mail.com",
                Name = "maira",
                ExpirationDate = DateTime.UtcNow,
                Role = "encargado"  // Nuevo campo de rol
            };

            Invitation invitationEntity = new Invitation
            {
                InvitationId = Guid.Parse("d8119d3a-f0f1-4451-a0c3-d4abd21e13aa"),
                Email = "mairafraga@mail.com",
                Name = "maira",
                ExpirationDate = DateTime.UtcNow,
                Role = "encargado",  // Nuevo campo de rol
                Status = "pendiente"
            };

            InvitationResponse response = new InvitationResponse(invitationEntity);
            _invitationLogicMock.Setup(logic => logic.CreateInvitation(It.IsAny<Invitation>())).Returns(invitationEntity);

            ObjectResult result = _invitationsController.CreateInvitation(newCreateInvitationRequest) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(response, result.Value);

            _invitationLogicMock.VerifyAll();
        }

        [TestMethod]
        public void GetAllInvitations_ShouldReturnOkWithInvitations()
        {
            IEnumerable<Invitation> expected = new List<Invitation>()
            {
                new Invitation()
                {
                    InvitationId=Guid.NewGuid(),
                    Email= "example@.com",
                    Name="mateo",
                    ExpirationDate = DateTime.UtcNow,
                    Role = "encargado",
                    Status="pendiente"
                },
                new Invitation()
                {
                    InvitationId=Guid.NewGuid(),
                    Email= "example2@.com",
                    Name="Joaquin",
                    ExpirationDate = DateTime.UtcNow,
                    Role = "admin",
                    Status="Aceptada"
                }
            };

            List<InvitationResponse> response = expected.Select(invitation => new InvitationResponse(invitation)).ToList();
            _invitationLogicMock.Setup(logic => logic.GetAllInvitations()).Returns(expected);

            ObjectResult result = _invitationsController.GetAllInvitations() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            CollectionAssert.AreEqual(response, (System.Collections.ICollection?)result.Value);

            _invitationLogicMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteInvitation_ShouldReturnNoContentOnSuccessfulDeletion()
        {
            Guid invitationId = new Guid();
            _invitationLogicMock.Setup(logic => logic.DeleteInvitation(invitationId)).Returns(true);

            IActionResult result = _invitationsController.DeleteInvitation(invitationId);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            Assert.AreEqual(204, ((NoContentResult)result).StatusCode);

            _invitationLogicMock.VerifyAll();
        }

        [TestMethod]
        public void AcceptInvitation_ShouldReturnOkWithUpdatedInvitation()
        {
            Guid invitationId = Guid.NewGuid();
            AcceptInvitationRequest request = new AcceptInvitationRequest
            {
                Email = "example@example.com",
                Password = "123"
            };

            Invitation updatedInvitation = new Invitation
            {
                InvitationId = invitationId,
                Email = request.Email,
                Name = "bruno mateo",
                Status = "Aceptada"
            };

            _invitationLogicMock.Setup(logic => logic.AcceptInvitation(invitationId, request.Email, request.Password)).Returns(updatedInvitation);

            ObjectResult result = _invitationsController.AcceptInvitation(invitationId, request) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(new InvitationResponse(updatedInvitation), result.Value);

            _invitationLogicMock.VerifyAll();
        }
    }
}
