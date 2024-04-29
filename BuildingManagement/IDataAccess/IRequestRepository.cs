using Domain;

namespace IDataAccess;

public interface IRequestRepository
{
    Request_ CreateRequest(Request_ request);
    IEnumerable<Request_> GetAllRequest();
    IEnumerable<Request_> GetAllRequest(int category);
    Request_ ActivateRequest(Guid requestId, DateTime startTime);
    Request_ TerminateRequest(Guid id, DateTime endTime, float totalCost);
    Request_ AsignMaintenancePerson(Guid requestGuid, Guid maintenancePersonId);
}