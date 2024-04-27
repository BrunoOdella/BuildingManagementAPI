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
        public void DeleteInvitation_ReturnsTrueOnSuccessfulDeletion()
        {
            int invitationId = 1;
            _invitationRepositoryMock.Setup(repository => repository.DeleteInvitation(invitationId)).Returns(true);

            bool result = _invitationLogic.DeleteInvitation(invitationId);

            Assert.IsTrue(result);
            _invitationRepositoryMock.VerifyAll();
        }

    }
}
