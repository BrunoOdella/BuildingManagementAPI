using Domain;

namespace Models.In;

public class CreateCategoryRequest
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Category ToEntity()
    {
        return new Category
        {
            Name = Name,
            Description = Description
        };
    }
}