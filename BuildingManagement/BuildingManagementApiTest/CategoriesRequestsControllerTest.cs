using BuildingManagementApi.Controllers;
using Domain;
using LogicInterface.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.In;
using Models.Out;
using Moq;
using System.Collections;

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

            CategoryResponse response = new CategoryResponse(categoryEntity);
            _CRlogicMock.Setup(logic => logic.CreateCategory(It.IsAny<Category>())).Returns(categoryEntity);

            // Act
            ObjectResult result = _CRcontroller.CreateCategory(newCategoryRequest) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(response, result.Value);

            _CRlogicMock.VerifyAll();
        }

        [TestMethod]
        public void GetCategoriesRequests_ShouldReturnOkResponse_WhenCategoriesAreFound()
        {
            // Arrange
            List<Category> categories = new List<Category>
            {
                new Category
                {
                    ID = 1,
                    Name = "Category A",
                    Description = "Description A"
                },
                new Category
                {
                    ID = 2,
                    Name = "Category B",
                    Description = "Description B"
                }
            };

            List<CategoryResponse> response = categories.Select(c => new CategoryResponse(c)).ToList();
            _CRlogicMock.Setup(logic => logic.GetAllCategories()).Returns(categories);

            // Act
            ObjectResult result = _CRcontroller.GetAllCategories() as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            CollectionAssert.AreEqual(response, result.Value as ICollection);

            _CRlogicMock.VerifyAll();
        }
    }
}
