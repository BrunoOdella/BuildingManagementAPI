namespace IDataAccess;

public interface IAuthenticationServiceRepository
{
    Guid BuscarToken(Guid token);
}