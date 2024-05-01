using Domain;

public class Apartment
{
    public Guid ApartmentId { get; set; }
    public int Floor { get; set; }
    public int Number { get; set; }
    public Owner Owner { get; set; }
    public int NumberOfBathrooms { get; set; }
    public bool HasTerrace { get; set; }
    // Clave foránea hacia Building
    public Guid BuildingId { get; set; }
    // Propiedad de navegación hacia Building
    public Building Building { get; set; }
    public List<Request_> Requests { get; set; } = new List<Request_>();
}