
namespace LogicInterface.Interfaces;

public interface IAuthenticationService
{
    Guid BuscarToken(Guid token);
}