using CustomExceptions.CategoryExceptions;
using Domain;
using IDataAccess;
using LogicInterface.Interfaces;

namespace BusinessLogic.Logics;

public class CategoriesRequestsLogic : ICategoriesRequestsLogic
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesRequestsLogic(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public Category CreateCategory(Category category)
    {
        if (IsUnique(category))
        {
            return _categoryRepository.Add(category);
        }
        throw new CategoryAlreadyExistException();
    }

    private bool IsUnique(Category category)
    {
        return !_categoryRepository.Exist(category);
    }

    public IEnumerable<Category> GetAllCategories()
    {
        return _categoryRepository.GetAll();
    }
}