using Domain;

namespace LogicInterface.Interfaces
{
    public interface IConstructionCompanyLogic
    {
        ConstructionCompany CreateConstructionCompany(ConstructionCompany constructionCompany, string ConsCompanyAdminId);
        ConstructionCompany UpdateConstructionCompanyName(ConstructionCompany constructionCompany, string ConsCompanyAdminId);
    }
}
