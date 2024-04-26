namespace Domain
{
    public class Invitation
    {
        public Guid InvitationId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Status { get; set; }
    }
}
