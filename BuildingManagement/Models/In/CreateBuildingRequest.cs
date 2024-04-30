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
    }
}
