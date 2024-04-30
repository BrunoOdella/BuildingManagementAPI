namespace Domain
{
    public class Manager
    {
        public Guid ManagerId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Building> Buildings { get; set; }
    }
}
