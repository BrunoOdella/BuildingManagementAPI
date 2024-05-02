using BusinessLogic.Logics;
using Domain;
using IDataAccess;
using Moq;

namespace BusinessLogicTest;

[TestClass]
public class CategoriesRequestsLogicTest
{
    private Mock<ICategoryRepository> _categoryRepositoryMock;
    private CategoriesRequestsLogic _categoriesRequestsLogic;

    [TestInitialize]
    public void TestSetup()
    {
        _categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
        _categoriesRequestsLogic = new CategoriesRequestsLogic(_categoryRepositoryMock.Object);
    }

    [TestMethod]
    public void CreateCategory_Succes()
    {
        Category category = new Category()
        {
            Description = "descripcion",
            Name = "nombre"
        };

        _categoryRepositoryMock.Setup(c => c.Add(category)).Returns(category);

        Category response = _categoriesRequestsLogic.CreateCategory(category);

        Assert.IsNotNull(response);
        Assert.AreEqual(category.Name, response.Name);
        
        _categoryRepositoryMock.VerifyAll();
    }
}