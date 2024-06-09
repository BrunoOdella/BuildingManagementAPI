using Domain;

namespace Models.Out
{
    public class InvitationResponse
    {
        public Guid InvitationId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Status { get; set; }

        public InvitationResponse(Invitation invitation)
        {
            this.InvitationId = invitation.InvitationId;
            this.Email = invitation.Email;
            this.Name = invitation.Name;
            this.ExpirationDate = invitation.ExpirationDate;
            this.Status = invitation.Status;
        }

        public override bool Equals(object obj)
        {
            return obj is InvitationResponse response &&
                   InvitationId == response.InvitationId &&
                   Email == response.Email &&
                   Name == response.Name &&
                   ExpirationDate == response.ExpirationDate &&
                   Status == response.Status;
        }
    }
}
