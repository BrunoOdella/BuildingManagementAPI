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
        private IConstructionCompanyLogic _constructionCompanyLogic;

        [TestInitialize]
        public void TestSetup()
        {
            _constructionCompanyRepositoryMock = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            _constructionCompanyLogic = new ConstructionCompanyLogic(_constructionCompanyRepositoryMock.Object);
        }

        [TestMethod]
        public void CreateConstructionCompany_ShouldCreateCompany()
        {
            // Arrange
            ConstructionCompany newCompany = new ConstructionCompany
            {
                ConstructionCompanyId = Guid.NewGuid(),
                Name = "New Construction Company"
            };

            _constructionCompanyRepositoryMock.Setup(repo => repo.NameExists(newCompany.Name)).Returns(false);
            _constructionCompanyRepositoryMock.Setup(repo => repo.CreateConstructionCompany(It.IsAny<ConstructionCompany>())).Returns(newCompany);

            // Act
            ConstructionCompany result = _constructionCompanyLogic.CreateConstructionCompany(newCompany);

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
            ConstructionCompany newCompany = new ConstructionCompany
            {
                ConstructionCompanyId = Guid.NewGuid(),
                Name = "Existing Construction Company"
            };

            _constructionCompanyRepositoryMock.Setup(repo => repo.NameExists(newCompany.Name)).Returns(true);

            // Act
            _constructionCompanyLogic.CreateConstructionCompany(newCompany);

            // Assert - Expects ConstructionCompanyAlreadyExistsException
            _constructionCompanyRepositoryMock.VerifyAll();
        }
    }
}
