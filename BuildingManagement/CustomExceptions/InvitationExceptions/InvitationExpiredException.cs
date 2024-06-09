namespace CustomExceptions.InvitationExceptions
{
    public class InvitationExpiredException : CustomException
    {
        public InvitationExpiredException() : base("Invitation has expired.")
        {
        }
    }
}
