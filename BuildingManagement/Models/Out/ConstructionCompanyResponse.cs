using System.Globalization;
using Domain;

namespace Models.Out;

public class ConstructionCompanyResponse
{
    public string Name { get; set; }
    public string AdminEmail { get; set; }

    public ConstructionCompanyResponse(ConstructionCompany company)
    {
        Name = company.Name;
        AdminEmail = company.ConstructionCompanyAdmin?.Email;
    }
    public override bool Equals(object obj)
    {
        return obj is ConstructionCompanyResponse response &&
               Name == response.Name
               && AdminEmail == response.AdminEmail;
    }
}