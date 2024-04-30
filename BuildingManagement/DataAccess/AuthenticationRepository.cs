using IDataAccess;

namespace DataAccess;

public class AuthenticationRepository : IAuthenticationServiceRepository
{
    private readonly BuildingManagementDbContext _context;

    public AuthenticationRepository(BuildingManagementDbContext context)
    {
        _context = context;
    }

    public Guid BuscarToken(Guid token)
    {
        throw new NotImplementedException();
    }
}