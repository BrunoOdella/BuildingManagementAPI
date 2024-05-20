using Domain;

namespace IDataAccess;

public interface ICategoryRepository
{
    Category Add(Category category);
    bool Exist(Category category);
}