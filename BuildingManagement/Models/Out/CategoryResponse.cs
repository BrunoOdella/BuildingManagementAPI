using Domain;

namespace Models.Out;

public class CategoryResponse
{
    public string Name { get; set; }
    public string Description { get; set; }

    public CategoryResponse(Category category)
    {
        this.Name = category.Name;
        this.Description = category.Description;
    }

    public override bool Equals(object obj)
    {
        return obj is CategoryResponse response &&
               Name == response.Name && 
               Description == response.Description;
    }
}