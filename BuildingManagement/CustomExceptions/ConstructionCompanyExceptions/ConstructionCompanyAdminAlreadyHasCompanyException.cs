namespace CustomExceptions.ConstructionCompanyExceptions
{
    public class AdminAlreadyHasCompanyException : CustomException
    {
        public AdminAlreadyHasCompanyException() : base("This admin already has a construction company.")
        {
        }
    }
}
