namespace CustomExceptions
{
    public class ConstructionCompanyAdminNotFoundException : CustomException
    {
        public ConstructionCompanyAdminNotFoundException()
            : base("The specified ConstructionCompanyAdmin does not exist.") { }
    }
}
