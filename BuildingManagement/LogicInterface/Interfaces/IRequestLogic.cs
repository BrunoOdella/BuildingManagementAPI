using Domain;

namespace LogicInterface.Interfaces;

public interface IRequestLogic
{
    IEnumerable<Request_> GetAllRequest(Guid managerId);
    IEnumerable<Request_> GetAllRequest(Guid managerId, int category);
    Request_ ActivateRequest(Guid managerId, Guid id, Guid maintenancePersonId, DateTime startTime);
    Request_ TerminateRequest(Guid managerId, Guid id, DateTime endTime, float totalCost);
    Request_ AsignMaintenancePerson(Guid managerId, Guid requestGuid, Guid maintenancePersonId);
}