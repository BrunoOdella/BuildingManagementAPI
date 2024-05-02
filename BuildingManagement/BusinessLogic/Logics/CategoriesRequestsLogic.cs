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
        return _logic.Add(category);
    }
}