using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingManagementApi.Controllers;
using Domain;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;
using Moq;

namespace BuildingManagementApiTest
{
    [TestClass]
    public class CategoriesRequestsControllerTest
    {
        private Mock<ICategoriesRequestsLogic> _CRlogicMock;
        private CategoriesRequestsController _CRcontroller;

        [TestInitialize]
        public void TestSetup()
        {
            _CRlogicMock = new Mock<ICategoriesRequestsLogic>(MockBehavior.Strict);
            _CRcontroller = new CategoriesRequestsController(_CRlogicMock.Object);
        }

        [TestMethod]
        public void PostCategoriesRequests_ShouldReturnCreatedResponse_WhenCategoriesIsSuccessfullyCreated()
        {
            // Arrange
            CreateCategoryRequest newCategoryRequest = new CreateCategoryRequest()
            {
                Name = "Category A",
                Description = "Description A"
            };

            Category categoryEntity = new Category
            {
                Name = newCategoryRequest.Name,
                Description = newCategoryRequest.Description
            };

            CreateCategoryResponse response = new CreateCategoryResponse(categoryEntity);
            _CRlogicMock.Setup(logic => logic.CreateCategory(It.IsAny<Category>())).Returns(categoryEntity);

            // Act
            ObjectResult result = _CRcontroller.CreateCategory(newCategoryRequest) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(response, result.Value);

            _CRlogicMock.VerifyAll();
        }
    }
}
