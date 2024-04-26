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
    public class InvitationLogicTest
    {
        private Mock<IInvitationRepository> _invitationRepositoryMock;
        private InvitationLogic _invitationLogic;

        [TestInitialize]
        public void TestSetup()
        {
            _invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            _invitationLogic = new AdminLogic(_invitationRepositoryMock.Object);
        }

        [TestMethod]
        public void CreateInvitation_ValidatesData_AndCreatesInvitation()
        {
            // Arrange
            Invitation invitation = new Invitation
            {
                InvitationId= Guid.NewGuid(),
                Email = "mairafraga@mail.com",
                Name = "maira",
                ExpirationDate = DateTime.UtcNow,
                Status = "pendiente"
            };

            _invitationRepositoryMock.Setup(repository => repository.CreateInvitation(It.IsAny<Invitation>())).Returns(invitation);

            // Act
            Invitation result = _invitationLogic.CreateInvitation(invitation);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(invitation.Email, result.Email);
            _invitationRepositoryMock.VerifyAll();
        }
    }
}
