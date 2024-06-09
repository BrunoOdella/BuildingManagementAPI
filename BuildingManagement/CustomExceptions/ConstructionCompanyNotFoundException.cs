namespace CustomExceptions
{
    public class ConstructionCompanyNotFoundException : CustomException
    {
        public ConstructionCompanyNotFoundException()
            : base("You should create your construction company first.") { }
    }
}
