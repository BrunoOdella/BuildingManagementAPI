using Domain;

namespace LogicInterface.Interfaces;

public interface IRequestLogic
{
    IEnumerable<Request_> GetAllRequest();
    IEnumerable<Request_> GetAllRequest(int category);
    Request_ ActivateRequest(Guid id, DateTime startTime);
    Request_ TerminateRequest(Guid id, DateTime endTime, float totalCost);
    Request_ AsignMaintenancePerson(Guid requestGuid, Guid maintenancePersonId);
}