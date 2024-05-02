using Domain;

namespace IDataAccess;

public interface ICategoryRepository
{
    Category Add(Category category);
}