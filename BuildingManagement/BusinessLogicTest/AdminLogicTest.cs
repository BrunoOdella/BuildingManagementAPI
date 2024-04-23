using BusinessLogic.Logics;
using Domain;
using IDataAccess;
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

        [TestMethod]
        public void CreateAdmin_ValidatesData_AndCreatesAdmin()
        {
            // Arrange
            Admin admin = new Admin
            {
                AdminID = Guid.Parse("fd6021ba-dd96-4e90-9100-c25e448315eb"),
                FirstName = "Juan",
                LastName = "Perez",
                Email = "juan.perez@example.com",
                Password = "securePassword123"
            };

            _adminRepositoryMock.Setup(repository => repository.Add(It.IsAny<Admin>())).Returns(admin);

            // Act
            Admin result = _adminLogic.CreateAdmin(admin);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(Guid.Parse("fd6021ba-dd96-4e90-9100-c25e448315eb"), result.AdminID);
            _adminRepositoryMock.VerifyAll();
        }
    }
}
