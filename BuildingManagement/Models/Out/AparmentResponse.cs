namespace Models.Out
{
    public class ApartmentResponse
    {
        public Guid ApartmentId { get; set; }
        public int Floor { get; set; }
        public int Number { get; set; }
        public string Owner { get; set; }
        public int NumberOfBathrooms { get; set; }
        public bool HasTerrace { get; set; }

        public ApartmentResponse(Apartment apartment)
        {
            ApartmentId = apartment.ApartmentId;
            Floor = apartment.Floor;
            Number = apartment.Number;
            Owner = apartment.Owner.FirstName + " " + apartment.Owner.LastName;
            NumberOfBathrooms = apartment.NumberOfBathrooms;
            HasTerrace = apartment.HasTerrace;
        }
    }
}
