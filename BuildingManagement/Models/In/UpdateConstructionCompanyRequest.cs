using Domain;

namespace Models.In
{
    public class UpdateConstructionCompanyRequest
    {
        public string NewName { get; set; }
        public string ActualName { get; set; } = String.Empty;
        public string NewAdminEmail { get; set; } = String.Empty;

        public ConstructionCompany ToEntity()
        {
            return new ConstructionCompany
            {
                Name = NewName,
                ConstructionCompanyAdmin = new ConstructionCompanyAdmin()
                {
                    Email = NewAdminEmail
                }
            };
        }
    }
}