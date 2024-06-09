using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccessTest;

[TestClass]
public class CategoryRepositoryTest
{
    private BuildingManagementDbContext CreateDbContext(string dbName)
    {
        DbContextOptions<BuildingManagementDbContext> options = new DbContextOptionsBuilder<BuildingManagementDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
        return new BuildingManagementDbContext(options);
    }

    [TestMethod]
    public void CreateCategory_Succes()
    {
        using (BuildingManagementDbContext context = CreateDbContext("TestCreateCategory"))
        {
            CategoryRepository categoryRepository = new CategoryRepository(context);

            Category category = new Category
            {
                Name = "Category1",
                Description = "Description1"
            };

            // Act
            categoryRepository.Add(category);
            context.SaveChanges();

            // Assert
            Assert.AreEqual(1, context.Category.Count());
            Assert.AreEqual("Category1", context.Category.First().Name);
            Assert.AreEqual("Description1", context.Category.First().Description);
            Assert.AreEqual(1, context.Category.First().ID);
        }
    }

    [TestMethod]
    public void CategoryAlreadyExist()
    {
        using (BuildingManagementDbContext context = CreateDbContext("TestAlreadyExist"))
        {
            CategoryRepository categoryRepository = new CategoryRepository(context);

            Category category = new Category
            {
                Name = "Category1",
                Description = "Description1"
            };

            // Act
            categoryRepository.Add(category);
            context.SaveChanges();

            bool exist = categoryRepository.Exist(category);

            // Assert
            Assert.IsTrue(exist);
        }
    }

    [TestMethod]
    public void CategoryNotExist()
    {
        using (BuildingManagementDbContext context = CreateDbContext("TestCategory_NotExist"))
        {
            CategoryRepository categoryRepository = new CategoryRepository(context);

            Category category = new Category
            {
                Name = "Category1",
                Description = "Description1"
            };

            // Act
            bool exist = categoryRepository.Exist(category);

            // Assert
            Assert.IsFalse(exist);
        }
    }

    [TestMethod]
    public void CreateCategory_Count()
    {
        using (BuildingManagementDbContext context = CreateDbContext("TestCategory_Count"))
        {
            CategoryRepository categoryRepository = new CategoryRepository(context);

            Category category = new Category
            {
                Name = "Category1",
                Description = "Description1"
            };

            // Act
            categoryRepository.Add(category);
            context.SaveChanges();

            // Assert
            Assert.AreEqual(1, categoryRepository.Count());
        }
    }

    [TestMethod]
    public void CountZero()
    {
        using (BuildingManagementDbContext context = CreateDbContext("TestCategory_CountZero"))
        {
            CategoryRepository categoryRepository = new CategoryRepository(context);

            // Act
            int count = categoryRepository.Count();

            // Assert
            Assert.AreEqual(0, count);
        }
    }

    [TestMethod]
    public void CreateCategory_CountTwo()
    {
        using (BuildingManagementDbContext context = CreateDbContext("TestCategory_CountTwo"))
        {
            CategoryRepository categoryRepository = new CategoryRepository(context);

            Category category = new Category
            {
                Name = "Category1",
                Description = "Description1"
            };

            Category category2 = new Category
            {
                Name = "Category2",
                Description = "Description2"
            };

            // Act
            categoryRepository.Add(category);
            categoryRepository.Add(category2);
            context.SaveChanges();

            // Assert
            Assert.AreEqual(2, categoryRepository.Count());
        }
    }

    [TestMethod]
    public void GetAllCategories()
    {
        using (BuildingManagementDbContext context = CreateDbContext("TestGetAllCategories"))
        {
            CategoryRepository categoryRepository = new CategoryRepository(context);

            Category category = new Category
            {
                Name = "Category1",
                Description = "Description1"
            };

            Category category2 = new Category
            {
                Name = "Category2",
                Description = "Description2"
            };

            // Act
            categoryRepository.Add(category);
            categoryRepository.Add(category2);
            context.SaveChanges();

            // Assert
            Assert.AreEqual(2, categoryRepository.GetAll().Count());
        }
    }
}