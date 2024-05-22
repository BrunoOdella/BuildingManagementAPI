using Domain;
using IDataAccess;
using LogicInterface.Interfaces;
using CustomExceptions.ConstructionCompanyExceptions;

namespace BusinessLogic.Logics
{
    public class ConstructionCompanyLogic : IConstructionCompanyLogic
    {
        private readonly IConstructionCompanyRepository _constructionCompanyRepository;

        public ConstructionCompanyLogic(IConstructionCompanyRepository constructionCompanyRepository)
        {
            _constructionCompanyRepository = constructionCompanyRepository;
        }

        public ConstructionCompany CreateConstructionCompany(ConstructionCompany constructionCompany)
        {
            if (_constructionCompanyRepository.NameExists(constructionCompany.Name))
            {
                throw new ConstructionCompanyAlreadyExistsException();
            }
            
            return _constructionCompanyRepository.CreateConstructionCompany(constructionCompany);
        }
    }
}
