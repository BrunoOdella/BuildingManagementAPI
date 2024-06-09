namespace CustomExceptions.InvitationExceptions
{
    public class InvitationAlreadyAcceptedException : CustomException
    {
        public InvitationAlreadyAcceptedException() : base("Invitation has already been accepted.")
        {
        }
    }
}
