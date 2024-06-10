using BusinessLogic.Logics;
using CustomExceptions;
using CustomExceptions.ConstructionCompanyExceptions;
using Domain;
using IDataAccess;
using Moq;

namespace BusinessLogicTest
{
    [TestClass]
    public class ConstructionCompanyLogicTest
    {
        private Mock<IConstructionCompanyRepository> _constructionCompanyRepositoryMock;
        private Mock<IConstructionCompanyAdminRepository> _constructionCompanyAdminRepositoryMock;
        private ConstructionCompanyLogic _constructionCompanyLogic;

        [TestInitialize]
        public void TestSetup()
        {
            _constructionCompanyRepositoryMock = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            _constructionCompanyAdminRepositoryMock = new Mock<IConstructionCompanyAdminRepository>(MockBehavior.Strict);
            _constructionCompanyLogic = new ConstructionCompanyLogic(_constructionCompanyRepositoryMock.Object, _constructionCompanyAdminRepositoryMock.Object);
        }

        [TestMethod]
        public void CreateConstructionCompany_ShouldCreateCompany()
        {
            // Arrange

            ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin
            {
                Id = Guid.NewGuid()
            };

            ConstructionCompany newCompany = new ConstructionCompany
            {
                Name = "New Construction Company",
                ConstructionCompanyAdminId = Guid.NewGuid()
            };

            _constructionCompanyRepositoryMock.Setup(repo => repo.NameExists(newCompany.Name)).Returns(false);
            _constructionCompanyRepositoryMock.Setup(repo => repo.AdminHasCompany(It.IsAny<Guid>())).Returns(false);
            _constructionCompanyRepositoryMock.Setup(repo => repo.CreateConstructionCompany(It.IsAny<ConstructionCompany>())).Returns(newCompany);
            _constructionCompanyRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(It.IsAny<string>()))
                .Returns(admin);

            // Act
            ConstructionCompany result = _constructionCompanyLogic.CreateConstructionCompany(newCompany, newCompany.ConstructionCompanyAdminId.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newCompany.Name, result.Name);
            _constructionCompanyRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ConstructionCompanyAlreadyExistsException))]
        public void CreateConstructionCompany_NameAlreadyExists_ShouldThrowException()
        {
            // Arrange
            ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin
            {
                Id = Guid.NewGuid()
            };

            ConstructionCompany newCompany = new ConstructionCompany
            {
                Name = "New Construction Company",
                ConstructionCompanyAdminId = admin.Id
            };

            _constructionCompanyRepositoryMock.Setup(repo => repo.NameExists(newCompany.Name)).Returns(true);
            _constructionCompanyRepositoryMock.Setup(repo => repo.AdminHasCompany(It.IsAny<Guid>())).Returns(false);
            _constructionCompanyRepositoryMock.Setup(repo => repo.CreateConstructionCompany(It.IsAny<ConstructionCompany>())).Returns(newCompany);
            _constructionCompanyRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(It.IsAny<string>()))
                .Returns(admin);

            // Act
            _constructionCompanyLogic.CreateConstructionCompany(newCompany, newCompany.ConstructionCompanyAdminId.ToString());

            // Assert - Expects ConstructionCompanyAlreadyExistsException
            _constructionCompanyRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(AdminAlreadyHasCompanyException))]
        public void CreateConstructionCompany_AdminAlreadyHasCompany_ShouldThrowException()
        {
            // Arrange
            ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin
            {
                Id = Guid.NewGuid()
            };

            ConstructionCompany newCompany = new ConstructionCompany
            {
                Name = "New Construction Company",
                ConstructionCompanyAdminId = admin.Id
            };

            _constructionCompanyRepositoryMock.Setup(repo => repo.NameExists(newCompany.Name)).Returns(false);
            _constructionCompanyRepositoryMock.Setup(repo => repo.AdminHasCompany(newCompany.ConstructionCompanyAdminId)).Returns(true);
            _constructionCompanyRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(It.IsAny<string>()))
                .Returns(admin);

            // Act
            _constructionCompanyLogic.CreateConstructionCompany(newCompany, newCompany.ConstructionCompanyAdminId.ToString());

            // Assert - Expects AdminAlreadyHasCompanyException
            _constructionCompanyRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void UpdateConstructionCompanyName_ShouldUpdateCompany()
        {
            ConstructionCompany updatedCompany = new ConstructionCompany
            {
                Name = "Updated Construction Company",
            };

            ConstructionCompany oldCompany = new ConstructionCompany
            {
                Name = "Actual Construction Company",
            };

            ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin
            {
                Id = Guid.NewGuid()
            };


            _constructionCompanyRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(admin.Id.ToString())).Returns(admin);
            _constructionCompanyRepositoryMock.Setup(r => r.GetCompanyByAdminId(admin.Id))
                .Returns(oldCompany);
            _constructionCompanyRepositoryMock.Setup(r => r.UpdateConstructionCompany(It.IsAny<ConstructionCompany>()));
            _constructionCompanyRepositoryMock.Setup(repo => repo.NameExists(updatedCompany.Name)).Returns(false);

            // Act
            ConstructionCompany result = _constructionCompanyLogic.UpdateConstructionCompanyName(updatedCompany, admin.Id.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedCompany.Name, result.Name);
            _constructionCompanyRepositoryMock.VerifyAll();
            _constructionCompanyAdminRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ConstructionCompanyAlreadyExistsException))]
        public void UpdateConstructionCompanyName_NameAlreadyExists_ShouldThrowException()
        {
            // Arrange
            string actualName = "Actual Construction Company";

            ConstructionCompany updatedCompany = new ConstructionCompany
            {
                Name = "Existing Construction Company",
            };

            ConstructionCompanyAdmin admin = new ConstructionCompanyAdmin
            {
                Id = Guid.NewGuid()
            };

            _constructionCompanyRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(admin.Id.ToString())).Returns(admin);
            _constructionCompanyRepositoryMock.Setup(r => r.GetCompanyByAdminId(admin.Id))
                .Returns(new ConstructionCompany() { Name = actualName });
            _constructionCompanyRepositoryMock.Setup(repo => repo.NameExists(updatedCompany.Name)).Returns(true);

            // Act
            _constructionCompanyLogic.UpdateConstructionCompanyName(updatedCompany, admin.Id.ToString());

            // Assert - Expects ConstructionCompanyAlreadyExistsException
            _constructionCompanyRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ConstructionCompanyNotFoundException))]
        public void UpdateConstructionCompanyName_CompanyDoesNotExist_ShouldThrowException()
        {
            // Arrange
            string actualName = "Actual Construction Company";

            ConstructionCompany updatedCompany = new ConstructionCompany
            {
                Name = "Updated Construction Company",
            };

            _constructionCompanyRepositoryMock.Setup(repo => repo.NameExists(actualName)).Returns(false);
            _constructionCompanyRepositoryMock.Setup(r => r.GetCompanyByAdminId(It.IsAny<Guid>())).Returns((ConstructionCompany)null);
            _constructionCompanyRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(It.IsAny<string>()))
                .Returns(new ConstructionCompanyAdmin());

            // Act
            _constructionCompanyLogic.UpdateConstructionCompanyName(updatedCompany, actualName);

            // Assert - Expects ConstructionCompanyDoesNotExistException
            _constructionCompanyRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ConstructionCompanyNameCanNotBeEmptyException))]
        public void UpdateConstructionCompanyName_NameIsEmpty_ShouldThrowException()
        {
            // Arrange
            string actualName = "Actual Construction Company";

            ConstructionCompany updatedCompany = new ConstructionCompany
            {
                Name = "",
            };

            _constructionCompanyRepositoryMock.Setup(repo => repo.NameExists(It.IsAny<string>())).Returns(false);
            _constructionCompanyRepositoryMock.Setup(r => r.GetCompanyByAdminId(It.IsAny<Guid>())).Returns(new ConstructionCompany());
            _constructionCompanyRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(It.IsAny<string>()))
                .Returns(new ConstructionCompanyAdmin());

            // Act
            _constructionCompanyLogic.UpdateConstructionCompanyName(updatedCompany, actualName);
        }

        [TestMethod]
        [ExpectedException(typeof(ConstructionCompanyNameCanNotBeEmptyException))]
        public void UpdateConstructionCompanyName_NameIsSpaces_ShouldThrowException()
        {
            // Arrange
            string actualName = "Actual Construction Company";

            ConstructionCompany updatedCompany = new ConstructionCompany
            {
                Name = "     ",
            };

            _constructionCompanyRepositoryMock.Setup(repo => repo.NameExists(It.IsAny<string>())).Returns(false);
            _constructionCompanyRepositoryMock.Setup(r => r.GetCompanyByAdminId(It.IsAny<Guid>())).Returns(new ConstructionCompany());
            _constructionCompanyRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(It.IsAny<string>()))
                .Returns(new ConstructionCompanyAdmin());

            // Act
            _constructionCompanyLogic.UpdateConstructionCompanyName(updatedCompany, actualName);
        }

        [TestMethod]
        [ExpectedException(typeof(ConstructionCompanyAdminNotFoundException))]
        public void UpdateConstructionCompanyName_AdminDoesNotExist_ShouldThrowException()
        {
            // Arrange
            string actualName = "Actual Construction Company";

            ConstructionCompany updatedCompany = new ConstructionCompany
            {
                Name = "Updated Construction Company",
            };

            _constructionCompanyRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(It.IsAny<string>()))
                .Returns((ConstructionCompanyAdmin)null);

            // Act
            _constructionCompanyLogic.UpdateConstructionCompanyName(updatedCompany, actualName);
        }

        [TestMethod]
        [ExpectedException(typeof(ConstructionCompanyAdminNotFoundException))]
        public void CreateConstructionCompanyName_AdminDoesNotExist_ShouldThrowException()
        {
            ConstructionCompany updatedCompany = new ConstructionCompany
            {
                Name = "Updated Construction Company",
            };

            _constructionCompanyRepositoryMock.Setup(r => r.GetConstructionCompanyAdminById(It.IsAny<string>()))
                .Returns((ConstructionCompanyAdmin)null);

            // Act
            _constructionCompanyLogic.CreateConstructionCompany(updatedCompany, "");
        }


    }
}
