using BusinessLogic.Logics;
using IDataAccess;
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
    }
}
