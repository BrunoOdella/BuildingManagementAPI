using Domain;
using IDataAccess;
using LogicInterface.Interfaces;

namespace BusinessLogic.Logics;

public class CategoriesRequestsLogic : ICategoriesRequestsLogic
{
    private readonly ICategoriesRequestsLogic _logic;

    public CategoriesRequestsLogic(ICategoriesRequestsLogic logic)
    {
        _logic = logic;
    }

    public Category CreateCategory(Category category)
    {
        throw new NotImplementedException();
    }
}