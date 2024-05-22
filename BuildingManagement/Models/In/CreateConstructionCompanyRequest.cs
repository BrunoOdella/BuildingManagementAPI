using Domain;

namespace Models.In
{
    public class CreateConstructionCompanyRequest
    {
        public string Name { get; set; }

        public ConstructionCompany ToEntity()
        {
            return new ConstructionCompany
            {
                Name = this.Name,
            };
        }
    }
}
