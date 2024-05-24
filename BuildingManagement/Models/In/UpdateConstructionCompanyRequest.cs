using Domain;

namespace Models.In
{
    public class UpdateConstructionCompanyRequest
    {
        public string NewName { get; set; } = String.Empty;
        public string ActualName { get; set; } = String.Empty;

        public ConstructionCompany ToEntity()
        {
            return new ConstructionCompany
            {
                Name = string.IsNullOrEmpty(NewName) ? ActualName : NewName,
            };
        }
    }
}