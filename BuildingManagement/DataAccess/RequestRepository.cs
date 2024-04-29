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

        public IEnumerable<Request_> GetAllRequest()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Request_> GetAllRequest(int category)
        {
            throw new NotImplementedException();
        }

        public Request_ ActivateRequest(Guid requestId, DateTime startTime)
        {
            throw new NotImplementedException();
        }

        public Request_ TerminateRequest(Guid id, DateTime endTime, float totalCost)
        {
            throw new NotImplementedException();
        }

        public Request_ AsignMaintenancePerson(Guid requestGuid, Guid maintenancePersonId)
        {
            throw new NotImplementedException();
        }
    }
}