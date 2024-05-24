using Domain;
using IDataAccess;
using LogicInterface.Interfaces;
using CustomExceptions.ConstructionCompanyExceptions;
using CustomExceptions;

namespace BusinessLogic.Logics
{
    public class ConstructionCompanyLogic : IConstructionCompanyLogic
    {
        private readonly IConstructionCompanyRepository _constructionCompanyRepository;
        private readonly IConstructionCompanyAdminRepository _constructionCompanyAdminRepository;

        public ConstructionCompanyLogic(IConstructionCompanyRepository constructionCompanyRepository, IConstructionCompanyAdminRepository constructionCompanyAdminRepository)
        {
            _constructionCompanyRepository = constructionCompanyRepository;
            _constructionCompanyAdminRepository = constructionCompanyAdminRepository;
        }

        public ConstructionCompany CreateConstructionCompany(ConstructionCompany constructionCompany)
        {
            if (_constructionCompanyRepository.NameExists(constructionCompany.Name))
            {
                throw new ConstructionCompanyAlreadyExistsException();
            }

            if (_constructionCompanyRepository.AdminHasCompany(constructionCompany.ConstructionCompanyAdminId))
            {
                throw new AdminAlreadyHasCompanyException();
            }

            return _constructionCompanyRepository.CreateConstructionCompany(constructionCompany);
        }

        public ConstructionCompany UpdateConstructionCompanyName(ConstructionCompany constructionCompany, string actualName)
        {
            if (string.IsNullOrWhiteSpace(constructionCompany.Name))
                throw new ConstructionCompanyNameCanNotBeEmptyException();

            if (_constructionCompanyRepository.NameExists(constructionCompany.Name))
                throw new ConstructionCompanyAlreadyExistsException();

            if (!_constructionCompanyRepository.NameExists(actualName))
                throw new ConstructionCompanyDoesNotExistException();

            return _constructionCompanyRepository.UpdateConstructionCompanyName(constructionCompany, actualName);
        }
    }
}
