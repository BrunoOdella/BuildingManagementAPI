using IDataAccess;
using LogicInterface.Interfaces;

namespace BusinessLogic.Logics;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAuthenticationServiceRepository _authenticationRepository;

    public AuthenticationService(IAuthenticationServiceRepository adminRepository)
    {
        _authenticationRepository = adminRepository;
    }

    public Guid BuscarToken(Guid token)
    {
        return _authenticationRepository.BuscarToken(token);
    }
}