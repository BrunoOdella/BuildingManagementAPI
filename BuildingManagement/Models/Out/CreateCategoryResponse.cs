using Domain;

namespace Models.Out;

public class CreateCategoryResponse
{
    public string Name { get; set; }
    public string Description { get; set; }

    public CreateCategoryResponse(Category category)
    {
        this.Name = category.Name;
        this.Description = category.Description;
    }

    public override bool Equals(object obj)
    {
        return obj is CreateCategoryResponse response &&
               Name == response.Name && 
               Description == response.Description;
    }
}