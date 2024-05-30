namespace ImporterInterface;
public class BuildingDTO
{
    public string Name { get; set; }
    public string Address { get; set; }
    public LocationDTO Location { get; set; }
    public int CommonExpenses { get; set; }
    public List<ApartmentDTO> Apartments { get; set; }
    public ManagerDTO Manager { get; set; }
}

public class LocationDTO
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class ApartmentDTO
{
    public int Floor { get; set; }
    public int Number { get; set; }
    public bool HasTerrace { get; set; }
    public int NumberOfBathrooms { get; set; }
    public int NumberOfRooms { get; set; }
    public OwnerDTO Owner { get; set; }
}

public class ManagerDTO
{
    public string Email { get; set; }
}

public class OwnerDTO
{
    public string Email { get; set; }
}
