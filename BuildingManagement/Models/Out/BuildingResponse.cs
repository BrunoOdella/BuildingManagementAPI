using Domain;

namespace Models.Out
{
    public class BuildingResponse
    {
        public Guid BuildingId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ConstructionCompanyName { get; set; }
        public int CommonExpenses { get; set; }
        public string ManagerName { get; set; } // Nueva propiedad

        public BuildingResponse(Building building)
        {
            BuildingId = building.BuildingId;
            Name = building.Name;
            Address = building.Address;
            Longitude = building.Location.Longitude;
            Latitude = building.Location.Latitude;
            ConstructionCompanyName = building.ConstructionCompany?.Name ?? "Unknown";
            CommonExpenses = building.CommonExpenses;
            ManagerName = building.Manager?.Name ?? "No Manager Assigned"; // Asignación del nombre del encargado
        }
    }
}
