using CustomExceptions;
using Domain;
using IDataAccess;
using LogicInterface.Interfaces;
using System.Security.Authentication;

namespace BusinessLogic.Logics;

public class AuthenticationService : IAuthenticationService
{
    private readonly IManagerRepository _managerRepository;
    private readonly IMaintenanceStaffRepository _maintenanceStaffRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly IConstructionCompanyAdminRepository _constructionCompanyAdminRepository;


    public AuthenticationService(IManagerRepository managerRepository, IMaintenanceStaffRepository maintenanceStaffRepository, IAdminRepository adminRepository, IConstructionCompanyAdminRepository constructionCompanyAdminRepository)
    {
        _managerRepository = managerRepository;
        _maintenanceStaffRepository =maintenanceStaffRepository;
        _adminRepository =adminRepository;
        _constructionCompanyAdminRepository = constructionCompanyAdminRepository;

    }

    public Guid BuscarToken(Guid token, string verbo, string uri)
    {
        if (uri.ToLower().Contains("request".ToLower()) && uri.Contains("finished".ToLower()))
            return BuscarTokenRequest(token, verbo, "staff");
        if (uri.ToLower().Contains("request".ToLower()) && !uri.ToLower().Contains("categories") && !uri.ToLower().Contains("reports"))
            return BuscarTokenRequest(token, verbo, "");
        if (uri.ToLower().Contains("invitations".ToLower()))
            return BuscarTokenInvitation(token, verbo);
        if (uri.ToLower().Contains("categoriesrequests"))
            return BuscarTokenCreateCategory(token, verbo);
        if (uri.ToLower().Contains("importbuildings"))
            return BuscarTokenCCAdmin(token);
        if (uri.ToLower().Contains("admins".ToLower()))
            return BuscarTokenAdmin(token, verbo);
        if (uri.ToLower().Contains("buildings".ToLower()))
            return BuscarTokenBuilding(token, verbo);
        if (uri.ToLower().Contains("reports".ToLower()))
            return BuscarTokenReport(token, verbo);
        if (uri.ToLower().Contains("constructioncompany".ToLower()))
            return BuscarTokenConstructionCompany(token, verbo);
        if (uri.ToLower().Contains("maintenancestaff".ToLower()))
            return BuscarTokenMaintenanceStaff(token, verbo);
        throw new NotImplementedException();
    }

    private Guid BuscarTokenMaintenanceStaff(Guid token, string verbo)
    {
        if (verbo == "POST")
            return BuscarTokenManager(token);
        return BuscarTokenManager(token);
    }

    private Guid BuscarTokenReport(Guid token, string verbo)
    {
         return BuscarTokenManager(token);
    }

    private Guid BuscarTokenConstructionCompany(Guid token, string verbo)
    {
        return BuscarTokenConstructionCompanyAdmin(token);
    }

    private Guid BuscarTokenBuilding(Guid token, string verbo)
    {
        if(verbo == "PUT") //manager o constructionCompanyAdmin
            return BuscarTokenManagerAndCCAdmin(token);
        if (verbo == "POST")
            return BuscarTokenCCAdmin(token);
        if (verbo == "GET")
            return BuscarTokenCCAdmin(token);
        return BuscarTokenManager(token);
    }

    private Guid BuscarTokenManagerAndCCAdmin(Guid token)
    {
        var manager = BuscarTokenManager(token);
        var ccAdmin = BuscarTokenCCAdmin(token);

        if (manager.Equals(Guid.Empty))
            if(ccAdmin.Equals(Guid.Empty))
                throw new ArgumentException("There is no person associated with the token.");
            else
                return ccAdmin;
        return manager;
    }

    private Guid BuscarTokenCCAdmin(Guid token)
    {
        var ccAdmin = _constructionCompanyAdminRepository.GetConstructionCompanyAdminById(token);
        if (ccAdmin == null)
            return Guid.Empty;
        return ccAdmin.Id;
    }

    private Guid BuscarTokenAdmin(Guid token, string verbo)
    {
        return buscarTokenAdmin(token);
    }

    private Guid BuscarTokenInvitation(Guid token, string verbo)
    {
        switch (verbo)
        {
            case "POST":
                return buscarTokenAdmin(token);
            case "PUT":
                return buscarTokenAdmin(token);
            case "DELETE":
                return buscarTokenAdmin(token);
            case "GET":
                return buscarTokenAdmin(token);
            default:
                throw new NotImplementedException();
        }
    }

    private Guid BuscarTokenRequest(Guid token, string verbo, string person)
    {
        if (person == "staff")
        {
            return buscartokenStaff(token);
        }
        switch (verbo)
        {
            case "GET":
                return BuscarTokenManagerAndStaff(token);
            case "POST":
                return BuscarTokenManager(token);
            case "PUT":
                return BuscarTokenManager(token);
            default:
                throw new NotImplementedException();
        }
    }
    private Guid BuscarTokenCreateCategory(Guid token, string verbo)
    {
        return buscarTokenAdmin(token);
    }



    private Guid BuscarTokenManagerAndStaff(Guid token)
    {
        Guid manager = _managerRepository.Get(token);
        Guid staff = _maintenanceStaffRepository.GetMaintenanceStaff(token);

        if (manager.Equals(Guid.Empty))
            if(staff.Equals(Guid.Empty))
                throw new ArgumentException("There is no person associated with the token.");
            else
                return staff;
        return manager;
    }

    private Guid buscartokenStaff(Guid token)
    {
        return _maintenanceStaffRepository.GetMaintenanceStaff(token);
    }

    private Guid buscarTokenAdmin(Guid token)
    {
        return _adminRepository.Get(token);
    }

    private Guid BuscarTokenManager(Guid token)
    {
        return _managerRepository.Get(token);
    }

    private Guid BuscarTokenConstructionCompanyAdmin(Guid token)
    {
        return _constructionCompanyAdminRepository.Get(token);
    }

    public AuthenticationResult Authenticate(string email, string password)
    {
        var admin = _adminRepository.GetByEmailAndPassword(email, password);
        if (admin != null)
        {
            return new AuthenticationResult { UserID = admin.AdminID, UserType = "Admin" };
        }

        var constructionCompanyAdmin = _constructionCompanyAdminRepository.GetByEmailAndPassword(email, password);
        if (constructionCompanyAdmin != null)
        {
            return new AuthenticationResult { UserID = constructionCompanyAdmin.Id, UserType = "ConstructionCompanyAdmin" };
        }

        var maintenanceStaff = _maintenanceStaffRepository.GetByEmailAndPassword(email, password);
        if (maintenanceStaff != null)
        {
            return new AuthenticationResult { UserID = maintenanceStaff.ID, UserType = "MaintenanceStaff" };
        }

        var manager = _managerRepository.GetByEmailAndPassword(email, password);
        if (manager != null)
        {
            return new AuthenticationResult { UserID = manager.ManagerId, UserType = "Manager" };
        }

        throw new InvalidCredentialsException();
    }

}