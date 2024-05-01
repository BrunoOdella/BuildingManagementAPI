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

        public Request_ CreateRequest(Guid managerId, Request_ request)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Request_> GetAllRequest(Guid managerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Request_> GetAllRequest(Guid managerId, int category)
        {
            throw new NotImplementedException();
        }

        public Request_ ActivateRequest(Guid managerId, Guid requestId, DateTime startTime)
        {
            throw new NotImplementedException();
        }

        public Request_ TerminateRequest(Guid managerId, Guid id, DateTime endTime, float totalCost)
        {
            throw new NotImplementedException();
        }

        public Request_ AsignMaintenancePerson(Guid managerId, Guid requestGuid, Guid maintenancePersonId)
        {
            throw new NotImplementedException();
        }

        public Request_ GetRequest(Guid managerId, Guid requestId)
        {
            throw new NotImplementedException();
        }

        public void Update(Request_ actualRequest)
        {
            throw new NotImplementedException();
        }
    }
}