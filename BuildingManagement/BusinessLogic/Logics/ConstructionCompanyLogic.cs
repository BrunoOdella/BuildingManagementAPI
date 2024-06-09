using CustomExceptions;
using CustomExceptions.ConstructionCompanyExceptions;
using Domain;
using IDataAccess;
using LogicInterface.Interfaces;

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


        public ConstructionCompany UpdateConstructionCompanyName(ConstructionCompany constructionCompany, string constructionCompanyAdminId)
        {
            var admin = _constructionCompanyRepository.GetConstructionCompanyAdminById(constructionCompanyAdminId);
            if (admin == null)
            {
                throw new ConstructionCompanyAdminNotFoundException();
            }

            var existingCompany = _constructionCompanyRepository.GetCompanyByAdminId(admin.Id);
            if (existingCompany == null)
            {
                throw new ConstructionCompanyNotFoundException();
            }

            if (_constructionCompanyRepository.NameExists(constructionCompany.Name))
            {
                throw new ConstructionCompanyAlreadyExistsException();
            }

            if (string.IsNullOrWhiteSpace(constructionCompany.Name))
            {
                throw new ConstructionCompanyNameCanNotBeEmptyException();
            }

            existingCompany.Name = constructionCompany.Name;

            _constructionCompanyRepository.UpdateConstructionCompany(existingCompany);

            return existingCompany;
        }

    }
}
