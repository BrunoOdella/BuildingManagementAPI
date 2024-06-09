namespace CustomExceptions
{
    public class EmailAlreadyExistsException : CustomException
    {
        public EmailAlreadyExistsException() : base("An account with the same email already exists in the system.")
        {
        }
    }
}
