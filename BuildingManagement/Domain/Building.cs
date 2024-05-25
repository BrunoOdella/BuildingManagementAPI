namespace Domain
{
    public class Building
    {
        public Guid BuildingId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Location Location { get; set; }
        public ConstructionCompany ConstructionCompany { get; set; }
        public int CommonExpenses { get; set; }
        public List<Apartment> Apartments { get; set; } = new List<Apartment>();
        // Clave foránea hacia Manager
        public Guid ManagerId { get; set; }
        // Propiedad de navegación hacia Manager
        public Manager Manager { get; set; }

        public Guid ConstructionCompanyAdminId { get; set; }
        public ConstructionCompanyAdmin ConstructionCompanyAdmin { get; set; }

    }
}
