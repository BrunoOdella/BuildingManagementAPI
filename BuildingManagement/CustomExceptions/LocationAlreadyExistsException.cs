namespace CustomExceptions
{
    public class LocationAlreadyExistsException : CustomException
    {
        public LocationAlreadyExistsException()
            : base("A building with the same location already exists.")
        {
        }
    }
}
