using Domain;

namespace IDataAccess;

public interface IRequestRepository
{
    Request_ CreateRequest(Request_ request);
    IEnumerable<Request_> GetAllRequest(Guid managerId);
    IEnumerable<Request_> GetAllRequest(Guid managerId, int category);
    //Request_ ActivateRequest(Guid managerId, Guid requestId, DateTime startTime);
    //Request_ TerminateRequest(Guid managerId, Guid id, DateTime endTime, float totalCost);
    //Request_ AsignMaintenancePerson(Guid managerId, Guid requestGuid, Guid maintenancePersonId);
    Request_ GetRequest(Guid managerId, Guid requestId);
    void Update(Request_ actualRequest);
    IEnumerable<Request_> GetAllRequestStaff(Guid staffID);
    Request_ Get(Guid requestId);
}