﻿using BuildingManagementApi.Controllers;
using Domain;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            };

            Invitation invitationEntity = new Invitation
            {
                InvitationId= Guid.Parse("d8119d3a-f0f1-4451-a0c3-d4abd21e13aa"),
                Email = "mairafraga@mail.com",
                Name = "maira",
                ExpirationDate = DateTime.UtcNow,
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
                    Status="pendiente"
                },
                new Invitation()
                {
                    InvitationId=Guid.NewGuid(),
                    Email= "example2@.com",
                    Name="Joaquin",
                    ExpirationDate = DateTime.UtcNow,
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
    }
}