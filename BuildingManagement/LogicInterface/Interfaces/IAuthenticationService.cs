
using Domain;

namespace LogicInterface.Interfaces;

public interface IAuthenticationService
{
    Guid BuscarToken(Guid token, string verbo, string uri);
    AuthenticationResult Authenticate(string email, string password);
}