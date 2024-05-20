using CustomExceptions.CategoryExceptions;
using Domain;
using IDataAccess;
using LogicInterface.Interfaces;

namespace BusinessLogic.Logics;

public class CategoriesRequestsLogic : ICategoriesRequestsLogic
{
    private readonly ICategoryRepository _logic;

    public CategoriesRequestsLogic(ICategoryRepository logic)
    {
        _logic = logic;
    }

    public Category CreateCategory(Category category)
    {
        if (IsUnique(category))
        {
            category.ID = _logic.Count() + 1;
            return _logic.Add(category);
        }
        throw new CategoryAlreadyExistException();
    }

    private bool IsUnique(Category category)
    {
        return !_logic.Exist(category);
    }
}