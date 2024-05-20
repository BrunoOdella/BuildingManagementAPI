using IDataAccess;
using LogicInterface.Interfaces;

namespace BusinessLogic.Logics;

public class AuthenticationService : IAuthenticationService
{
    private readonly IManagerRepository _managerRepository;
    private readonly IMaintenanceStaffRepository _maintenanceStaffRepository;
    private readonly IAdminRepository _adminRepository;


    public AuthenticationService(IManagerRepository managerRepository, IMaintenanceStaffRepository maintenanceStaffRepository, IAdminRepository adminRepository)
    {
        _managerRepository = managerRepository;
        _maintenanceStaffRepository =maintenanceStaffRepository;
        _adminRepository =adminRepository;
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
        if (uri.ToLower().Contains("admins".ToLower()))
            return BuscarTokenAdmin(token, verbo);
        if (uri.ToLower().Contains("buildings".ToLower()))
            return BuscarTokenBuilding(token, verbo);
        if (uri.ToLower().Contains("reports".ToLower()))
            return BuscarTokenReport(token, verbo);
        throw new NotImplementedException();
    }

    private Guid BuscarTokenReport(Guid token, string verbo)
    {
         return BuscarTokenManager(token);
    }

    private Guid BuscarTokenBuilding(Guid token, string verbo)
    {
        return BuscarTokenManager(token);
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



    // mejorar solucion
    private Guid BuscarTokenManagerAndStaff(Guid token)
    {
        Guid manager = Guid.Empty;
        try
        {
            manager = _managerRepository.Get(token);
        }
        catch (Exception e)
        {
        }

        Guid staff = Guid.Empty;

        try
        {
            staff = _maintenanceStaffRepository.GetMaintenanceStaff(token);
        }
        catch (Exception e)
        {
        }
        if (manager.Equals(Guid.Empty))
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
}