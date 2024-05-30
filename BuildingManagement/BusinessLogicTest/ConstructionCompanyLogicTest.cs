using BusinessLogic.Logics;
using CustomExceptions;
using CustomExceptions.ConstructionCompanyExceptions;
using Domain;
using IDataAccess;
using LogicInterface.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

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
            ConstructionCompany newCompany = new ConstructionCompany
            {
                ConstructionCompanyId = Guid.NewGuid(),
                Name = "New Construction Company",
                ConstructionCompanyAdminId = Guid.NewGuid()
            };

            _constructionCompanyRepositoryMock.Setup(repo => repo.NameExists(newCompany.Name)).Returns(false);
            _constructionCompanyRepositoryMock.Setup(repo => repo.AdminHasCompany(newCompany.ConstructionCompanyAdminId)).Returns(false);
            _constructionCompanyRepositoryMock.Setup(repo => repo.CreateConstructionCompany(It.IsAny<ConstructionCompany>())).Returns(newCompany);

            // Act
            //ConstructionCompany result = _constructionCompanyLogic.CreateConstructionCompany(newCompany);

            // Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(newCompany.Name, result.Name);
            _constructionCompanyRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ConstructionCompanyAlreadyExistsException))]
        public void CreateConstructionCompany_NameAlreadyExists_ShouldThrowException()
        {
            // Arrange
            ConstructionCompany newCompany = new ConstructionCompany
            {
                ConstructionCompanyId = Guid.NewGuid(),
                Name = "Existing Construction Company",
                ConstructionCompanyAdminId = Guid.NewGuid()
            };

            _constructionCompanyRepositoryMock.Setup(repo => repo.NameExists(newCompany.Name)).Returns(true);

            // Act
            //_constructionCompanyLogic.CreateConstructionCompany(newCompany);

            // Assert - Expects ConstructionCompanyAlreadyExistsException
            _constructionCompanyRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(AdminAlreadyHasCompanyException))]
        public void CreateConstructionCompany_AdminAlreadyHasCompany_ShouldThrowException()
        {
            // Arrange
            ConstructionCompany newCompany = new ConstructionCompany
            {
                ConstructionCompanyId = Guid.NewGuid(),
                Name = "New Construction Company",
                ConstructionCompanyAdminId = Guid.NewGuid()
            };

            _constructionCompanyRepositoryMock.Setup(repo => repo.NameExists(newCompany.Name)).Returns(false);
            _constructionCompanyRepositoryMock.Setup(repo => repo.AdminHasCompany(newCompany.ConstructionCompanyAdminId)).Returns(true);

            // Act
            //_constructionCompanyLogic.CreateConstructionCompany(newCompany);

            // Assert - Expects AdminAlreadyHasCompanyException
            _constructionCompanyRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void UpdateConstructionCompanyName_ShouldUpdateCompany()
        {
            // Arrange
            string actualName = "Actual Construction Company";

            ConstructionCompany updatedCompany = new ConstructionCompany
            {
                Name = "Updated Construction Company",
            };

            _constructionCompanyRepositoryMock.Setup(repo => repo.UpdateConstructionCompanyName(It.IsAny<ConstructionCompany>(), actualName)).Returns(updatedCompany);
            _constructionCompanyRepositoryMock.Setup(repo => repo.NameExists(updatedCompany.Name)).Returns(false);
            _constructionCompanyRepositoryMock.Setup(repo => repo.NameExists(actualName)).Returns(true);

            // Act
            ConstructionCompany result = _constructionCompanyLogic.UpdateConstructionCompanyName(updatedCompany, actualName);

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

            _constructionCompanyRepositoryMock.Setup(repo => repo.NameExists(updatedCompany.Name)).Returns(true);

            // Act
            _constructionCompanyLogic.UpdateConstructionCompanyName(updatedCompany, actualName);

            // Assert - Expects ConstructionCompanyAlreadyExistsException
            _constructionCompanyRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ConstructionCompanyDoesNotExistException))]
        public void UpdateConstructionCompanyName_CompanyDoesNotExist_ShouldThrowException()
        {
            // Arrange
            string actualName = "Actual Construction Company";

            ConstructionCompany updatedCompany = new ConstructionCompany
            {
                Name = "Updated Construction Company",
            };

            _constructionCompanyRepositoryMock.Setup(repo => repo.NameExists(updatedCompany.Name)).Returns(false);
            _constructionCompanyRepositoryMock.Setup(repo => repo.NameExists(actualName)).Returns(false);

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

            // Act
            _constructionCompanyLogic.UpdateConstructionCompanyName(updatedCompany, actualName);
        }
    }
}
