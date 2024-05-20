using BusinessLogic.Logics;
using CustomExceptions.CategoryExceptions;
using Domain;
using IDataAccess;
using LogicInterface.Interfaces;
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

        _categoryRepositoryMock.Setup(c => c.Exist(category)).Returns(false);
        _categoryRepositoryMock.Setup(c => c.Count()).Returns(0);
        _categoryRepositoryMock.Setup(c => c.Add(category)).Returns(category);

        Category response = _categoriesRequestsLogic.CreateCategory(category);

        Assert.IsNotNull(response);
        Assert.AreEqual(category.Name, response.Name);
        
        _categoryRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void CreateCategory_Fail()
    {
        Category category = new Category()
        {
            Description = "descripcion",
            Name = "nombre"
        };

        _categoryRepositoryMock.Setup(c => c.Exist(category)).Returns(true);

        Exception exception = null;

        try
        {
            _categoriesRequestsLogic.CreateCategory(category);
        }
        catch (Exception e)
        {
            exception = e;
        }

        Assert.IsInstanceOfType(exception, typeof(CategoryAlreadyExistException));
        Assert.IsTrue(exception.Message.Equals("Can not create an already existing category."));
        _categoryRepositoryMock.VerifyAll();
    }
}