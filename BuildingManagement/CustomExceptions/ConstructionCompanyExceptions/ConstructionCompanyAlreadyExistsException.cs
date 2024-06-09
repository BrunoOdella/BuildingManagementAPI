namespace CustomExceptions.ConstructionCompanyExceptions
{
    public class ConstructionCompanyAlreadyExistsException : CustomException
    {
        public ConstructionCompanyAlreadyExistsException() : base("A construction company with the same name already exists.")
        {
        }
    }
}
