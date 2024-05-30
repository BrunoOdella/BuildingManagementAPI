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

        public ConstructionCompany CreateConstructionCompany(ConstructionCompany constructionCompany, string constructionCompanyAdminId)
        {
            // Check if the ConstructionCompanyAdmin exists
            var admin = _constructionCompanyRepository.GetConstructionCompanyAdminById(constructionCompanyAdminId);
            if (admin == null)
            {
                throw new ConstructionCompanyAdminNotFoundException();
            }

            // Check if the ConstructionCompany name already exists
            if (_constructionCompanyRepository.NameExists(constructionCompany.Name))
            {
                throw new ConstructionCompanyAlreadyExistsException();
            }

            // Check if the ConstructionCompanyAdmin already has a company
            if (_constructionCompanyRepository.AdminHasCompany(admin.Id))
            {
                throw new AdminAlreadyHasCompanyException();
            }

            // Assign the existing admin's ID to the ConstructionCompany
            constructionCompany.ConstructionCompanyAdminId = admin.Id;
            constructionCompany.ConstructionCompanyAdmin = admin;

            // Create the ConstructionCompany
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
