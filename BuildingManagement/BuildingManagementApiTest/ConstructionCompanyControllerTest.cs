using BuildingManagementApi.Controllers;
using Domain;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;
using Moq;

namespace BuildingManagementApiTest
{
    [TestClass]
    public class ConstructionCompanyControllerTest
    {
        private Mock<IConstructionCompanyLogic> _constructionCompanyLogicMock;
        private ConstructionCompanyController _constructionCompanyController;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;


        [TestInitialize]
        public void TestSetup()
        {
            _constructionCompanyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);

            _httpContextAccessorMock = new Mock<IHttpContextAccessor>(MockBehavior.Strict);
            var httpContext = new DefaultHttpContext();
            Guid expectedUserID = Guid.NewGuid();
            httpContext.Items["userID"] = expectedUserID.ToString();
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            _constructionCompanyController = new ConstructionCompanyController(_constructionCompanyLogicMock.Object, _httpContextAccessorMock.Object);
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
                Name = newCreateConstructionCompanyRequest.Name
            };

            ConstructionCompanyResponse response = new ConstructionCompanyResponse(newCreateConstructionCompanyRequest.ToEntity());

            string ccAdmin = _httpContextAccessorMock.Object.HttpContext.Items["userID"] as string;


            _constructionCompanyLogicMock.Setup(logic => logic.CreateConstructionCompany(It.IsAny<ConstructionCompany>(), ccAdmin)).Returns(constructionCompanyEntity);

            ObjectResult result = _constructionCompanyController.CreateConstructionCompany(newCreateConstructionCompanyRequest) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(response, result.Value);

            _constructionCompanyLogicMock.VerifyAll();
        }

        [TestMethod]
        public void UpdateConstructionCompanyName_ShouldReturnOKResponse()
        {
            UpdateConstructionCompanyRequest constructionCompany = new UpdateConstructionCompanyRequest
            {
                //ActualName = "CompanyName1",
                NewName = "CompanyName2"
            };

            ConstructionCompanyResponse response = new ConstructionCompanyResponse(constructionCompany.ToEntity());

            _constructionCompanyLogicMock.Setup(logic => logic.UpdateConstructionCompanyName(It.IsAny<ConstructionCompany>(), It.IsAny<string>())).Returns(constructionCompany.ToEntity());

            ObjectResult result = _constructionCompanyController.UpdateConstructionCompany(constructionCompany) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(response, result.Value);

            _constructionCompanyLogicMock.VerifyAll();
        }
    }
}