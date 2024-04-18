using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTest
{
    public class AdminLogicTest
    {
        private Mock<IAdminRepository> _adminRepositoryMock;
        private AdminLogic _adminLogic;

        [TestInitialize]
        public void TestSetup()
        {
            _adminRepositoryMock = new Mock<IAdminRepository>(MockBehavior.Strict);
            _adminLogic = new AdminLogic(_adminRepositoryMock.Object);
        }

        //arrange

        //act

        //assert
    }
}
