namespace CustomExceptions
{
    public class InvalidCredentialsException : CustomException
    {
        public InvalidCredentialsException() : base("Invalid email or password")
        {
        }
    }
}
