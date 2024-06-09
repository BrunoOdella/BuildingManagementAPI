namespace Domain
{
    public class ConstructionCompany
    {
        public Guid ConstructionCompanyId { get; set; }
        public string Name { get; set; }
        public Guid ConstructionCompanyAdminId { get; set; }
        public ConstructionCompanyAdmin ConstructionCompanyAdmin { get; set; }
        public List<Building> Buildings { get; set; } = new List<Building>();
    }
}
