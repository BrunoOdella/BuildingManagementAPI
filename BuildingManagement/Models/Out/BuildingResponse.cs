using Domain;

namespace Models.Out
{
    public class BuildingResponse
    {
        public Guid BuildingId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Location Location { get; set; }
        public string ConstructionCompanyName { get; set; }
        public int CommonExpenses { get; set; }

        public BuildingResponse(Building building)
        {
            BuildingId = building.BuildingId;
            Name = building.Name;
            Address = building.Address;
            Location = building.Location;
            ConstructionCompanyName = building.ConstructionCompany?.Name ?? "Unknown";
            CommonExpenses = building.CommonExpenses;
        }
    }

}
