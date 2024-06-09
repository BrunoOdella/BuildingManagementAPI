namespace CustomExceptions.InvitationExceptions
{
    public class InvitationNotFoundException : CustomException
    {
        public InvitationNotFoundException() : base("Invitation does not exist.")
        {
        }
    }
}
