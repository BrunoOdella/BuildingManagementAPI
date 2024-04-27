using BusinessLogic.Logics;
using Domain;
using IDataAccess;
using Moq;

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
            _invitationLogic = new InvitationLogic(_invitationRepositoryMock.Object);
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

        [TestMethod]
        public void GetAllInvitations_ReturnsAllInvitations()
        {
            IEnumerable<Invitation> expected = new List<Invitation>()
            {
                new Invitation { 
                    InvitationId= Guid.NewGuid(),
                    Name="bru",
                    Email="bru@example.com",
                    ExpirationDate= DateTime.UtcNow,
                    Status="pendiente"                    
                }
            };
            _invitationRepositoryMock.Setup(repository => repository.GetAllInvitations()).Returns(expected);

            IEnumerable<Invitation> result = _invitationLogic.GetAllInvitations();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
            _invitationRepositoryMock.VerifyAll();
        }


        [TestMethod]
        public void DeleteInvitation_InvitationNotAccepted_DeletesInvitation()
        {
            var invitationId = Guid.NewGuid();
            var invitation = new Invitation
            {
                InvitationId = invitationId,
                Email = "test@example.com",
                Name = "Test",
                ExpirationDate = DateTime.Now,
                Status = "pendiente"
            };

            _invitationRepositoryMock.Setup(repo => repo.GetInvitationById(invitationId)).Returns(invitation);
            _invitationRepositoryMock.Setup(repo => repo.DeleteInvitation(invitationId)).Returns(true);

            var result = _invitationLogic.DeleteInvitation(invitationId);

            Assert.IsTrue(result);
            _invitationRepositoryMock.Verify(repo => repo.GetInvitationById(invitationId), Times.Once);
            _invitationRepositoryMock.Verify(repo => repo.DeleteInvitation(invitationId), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteInvitation_InvitationAccepted_ThrowsException()
        {
            var invitationId = Guid.NewGuid();
            var invitation = new Invitation
            {
                InvitationId = invitationId,
                Email = "test@example.com",
                Name = "Test",
                ExpirationDate = DateTime.Now,
                Status = "Aceptada"
            };

            _invitationRepositoryMock.Setup(repo => repo.GetInvitationById(invitationId)).Returns(invitation);

            _invitationLogic.DeleteInvitation(invitationId);
        }



        [TestMethod]
        public void AcceptInvitation_InvitationCanBeAccepted_UpdatesInvitationStatus()
        {
            var invitationId = Guid.NewGuid();
            var invitation = new Invitation
            {
                InvitationId = invitationId,
                Email = "test@example.com",
                Name = "Test",
                ExpirationDate = DateTime.Now,
                Status = "pendiente"
            };

            _invitationRepositoryMock.Setup(repo => repo.GetInvitationById(invitationId)).Returns(invitation);
            _invitationRepositoryMock.Setup(repo => repo.UpdateInvitation(invitation)).Verifiable("Invitation should be updated.");

            Invitation result = _invitationLogic.AcceptInvitation(invitationId, "newPassword123");

            Assert.IsNotNull(result);
            Assert.AreEqual("Aceptada", result.Status);
            _invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AcceptInvitation_InvitationAlreadyAccepted_ThrowsException()
        {
            var invitationId = Guid.NewGuid();
            var invitation = new Invitation
            {
                InvitationId = invitationId,
                Email = "test@example.com",
                Name = "Test",
                ExpirationDate = DateTime.Now,
                Status = "Aceptada"
            };

            _invitationRepositoryMock.Setup(repo => repo.GetInvitationById(invitationId)).Returns(invitation);

            _invitationLogic.AcceptInvitation(invitationId, "newPassword123");
        }



    }
}
