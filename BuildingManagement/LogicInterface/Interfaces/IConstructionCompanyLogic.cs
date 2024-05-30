using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterface.Interfaces
{
    public interface IConstructionCompanyLogic
    {
        ConstructionCompany CreateConstructionCompany(ConstructionCompany constructionCompany, string ConsCompanyAdminId);
        ConstructionCompany UpdateConstructionCompanyName(ConstructionCompany constructionCompany, string ConsCompanyAdminId);
    }
}
