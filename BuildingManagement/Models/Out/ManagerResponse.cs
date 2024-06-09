namespace Models.Out
{
    public class ManagerResponse
    {
        public Guid ManagerId { get; set; }
        public string Email { get; set; }

        public ManagerResponse(Domain.Manager manager)
        {
            ManagerId = manager.ManagerId;
            Email = manager.Email;
        }
    }
}
