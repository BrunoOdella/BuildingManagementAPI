using Domain;

public class Building
{
    public Guid BuildingId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public Location Location { get; set; }
    public Guid ConstructionCompanyId { get; set; }
    public ConstructionCompany ConstructionCompany { get; set; }
    public int CommonExpenses { get; set; }
    public List<Apartment> Apartments { get; set; } = new List<Apartment>();

    public Guid? ManagerId { get; set; }
    public Manager Manager { get; set; }
}
