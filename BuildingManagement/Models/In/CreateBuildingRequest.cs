using Domain;

namespace Models.In
{
    public class CreateBuildingRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid ConstructionCompanyId { get; set; }
        public int CommonExpenses { get; set; }
        public List<ApartmentData> Apartments { get; set; }

        public Building ToEntity()
        {
            return new Building
            {
                Name = this.Name,
                Address = this.Address,
                Location = new Location { Latitude = this.Latitude, Longitude = this.Longitude },
                ConstructionCompany = new ConstructionCompany { CompanyId = this.ConstructionCompanyId },
                CommonExpenses = this.CommonExpenses,
                Apartments = this.Apartments.Select(a => a.ToEntity()).ToList()
            };
        }
    }
}
