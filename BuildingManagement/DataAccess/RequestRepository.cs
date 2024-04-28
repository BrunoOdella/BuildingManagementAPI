using Domain;
using IDataAccess;

namespace DataAccess
{
    public class RequestRepository : IRequestRepository
    {
        private readonly BuildingManagementDbContext _context;

        public RequestRepository(BuildingManagementDbContext context)
        {
            _context = context;
        }

        public Request_ CreateRequest(Request_ request)
        {
            throw new NotImplementedException();
        }
    }
}