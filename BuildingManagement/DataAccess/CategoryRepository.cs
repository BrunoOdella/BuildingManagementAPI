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

    public bool Exist(Category category)
    {
        var exist = _context.Category.Where(c => c.Name.Equals(category.Name)).ToList();
        return exist.Count > 0;
    }

    public int Count()
    {
        return _context.Category.Count();
    }
}