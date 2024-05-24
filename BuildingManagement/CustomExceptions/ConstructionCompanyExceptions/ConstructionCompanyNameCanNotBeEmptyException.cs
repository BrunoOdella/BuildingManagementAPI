namespace CustomExceptions.ConstructionCompanyExceptions;

public class ConstructionCompanyNameCanNotBeEmptyException : CustomException
{
    public ConstructionCompanyNameCanNotBeEmptyException() : base("Construction company name can not be empty.")
    {
    }
}