using Domain;
using IDataAccess;

namespace DataAccess;

public class CategoryRepository : ICategoryRepository
{
    private readonly BuildingManagementDbContext _context;

    public CategoryRepository(BuildingManagementDbContext context)
    {
        _context = context;
    }

    public Category Add(Category category)
    {
        _context.Category.Add(category);
        _context.SaveChanges();
        return category;
    }
}