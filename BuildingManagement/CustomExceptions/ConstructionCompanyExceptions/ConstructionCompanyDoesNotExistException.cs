namespace CustomExceptions.ConstructionCompanyExceptions;

public class ConstructionCompanyDoesNotExistException : CustomException
{
    public ConstructionCompanyDoesNotExistException() : base("Construction company does not exist.")
    { }
}