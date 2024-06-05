using Domain;

namespace LogicInterface.Interfaces;

public interface IRequestLogic
{
    IEnumerable<Request_> GetAllRequest(Guid personID);
    IEnumerable<Request_> GetAllRequest(Guid managerId, int category);
    Request_ ActivateRequest(Guid id, Guid maintenancePersonId, DateTime startTime);
    Request_ TerminateRequest(Guid staffId, Guid id, DateTime endTime, float totalCost);
    //Request_ AsignMaintenancePerson(Guid managerId, Guid requestGuid, Guid maintenancePersonId);
    Request_ CreateRequest(Guid managerID, Request_ request_);
}