using Domain;
using IDataAccess;
using LogicInterface.Interfaces;

namespace BusinessLogic.Logics;

public class RequestLogic : IRequestLogic
{
    private readonly IRequestRepository _logic;

    public RequestLogic(IRequestRepository logic)
    {
        _logic = logic;
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

    public Request_ ActivateRequest(Guid id, DateTime startTime)
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