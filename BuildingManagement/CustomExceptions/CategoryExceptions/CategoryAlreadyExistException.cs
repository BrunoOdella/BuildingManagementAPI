namespace CustomExceptions.CategoryExceptions;

public class CategoryAlreadyExistException : CustomException
{
    public CategoryAlreadyExistException() : base("Can not create an already existing category.") { }
}