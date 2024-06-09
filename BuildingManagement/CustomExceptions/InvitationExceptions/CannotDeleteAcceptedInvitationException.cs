namespace CustomExceptions.InvitationExceptions
{
    public class CannotDeleteAcceptedInvitationException : CustomException
    {
        public CannotDeleteAcceptedInvitationException() : base("Cannot delete an accepted invitation.")
        {
        }
    }
}
