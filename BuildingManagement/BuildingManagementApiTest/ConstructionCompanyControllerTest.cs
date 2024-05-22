using BuildingManagementApi.Controllers;
using Domain;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.In;
using Moq;
using System;

namespace BuildingManagementApiTest
{
    [TestClass]
    public class ConstructionCompanyControllerTest
    {
        private Mock<IConstructionCompanyLogic> _constructionCompanyLogicMock;
        private ConstructionCompanyController _constructionCompanyController;

        [TestInitialize]
        public void TestSetup()
        {
            _constructionCompanyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            _constructionCompanyController = new ConstructionCompanyController(_constructionCompanyLogicMock.Object);
        }

        [TestMethod]
        public void CreateConstructionCompany_ShouldReturnCreatedResponse()
        {
            CreateConstructionCompanyRequest newCreateConstructionCompanyRequest = new CreateConstructionCompanyRequest
            {
                Name = "Example Construction Company"
            };

            ConstructionCompany constructionCompanyEntity = new ConstructionCompany
            {
                ConstructionCompanyId = Guid.NewGuid(),
                Name = "Example Construction Company"
            };

            _constructionCompanyLogicMock.Setup(logic => logic.CreateConstructionCompany(It.IsAny<ConstructionCompany>())).Returns(constructionCompanyEntity);

            ObjectResult result = _constructionCompanyController.CreateConstructionCompany(newCreateConstructionCompanyRequest) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(constructionCompanyEntity, result.Value);

            _constructionCompanyLogicMock.VerifyAll();
        }
    }
}
