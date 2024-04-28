using Domain;
using IDataAccess;
using LogicInterface.Interfaces;

namespace BusinessLogic.Logics;

public class RequestLogic : IRequestLogic
{
    private readonly IRequestRepository _requestRepository;

    public RequestLogic(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public Request_ CreateRequest(Request_ request)
    {
        ValidateIncomingRequest(request);

        return _requestRepository.CreateRequest(request);
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

    private void ValidateIncomingRequest(Request_ request)
    {
        // General validations
        // Empty request
        if (request.Id == Guid.Empty)
            throw new ArgumentException("Can not create an empty Request.");

        // Cateogry can not be empty
        if (request.Category is null)
            throw new ArgumentException("Category can not be empty.");

        // Description can not be empty
        if (request.Description is null || request.Description.Length == 0)
            throw new ArgumentException("Description can not be empty.");

        // Creation Time can not be empty
        if (request.CreationTime.Equals(DateTime.MinValue))
            throw new ArgumentException("Creation Time can not be empty.");

        if ((request.Status == Status.Active || request.Status == Status.Finished) &&
            !request.StartTime.Equals(DateTime.MinValue) &&
            request.StartTime < request.CreationTime)
            throw new ArgumentException("Start time can not be less than Creation Time.");

        if (request.Status == Status.Finished &&
            !request.EndTime.Equals(DateTime.MinValue) &&
            request.EndTime < request.CreationTime)
            throw new ArgumentException("End time can not be less than Creation Time.");
        // End general validations

        // Status = Pending
        if (request.Status == Status.Pending &&
            (!request.EndTime.Equals(DateTime.MinValue) ||
             !request.StartTime.Equals(DateTime.MinValue)))
            throw new ArgumentException("If status is Pending, Start Time or End Time need be empty.");

        //Cosas por definir: si el status es Active, el start time lo ponemos como DateTime.Now o lo debemos recibir??
        // Status = Active
        if (request.Status == Status.Active &&
            (request.StartTime.Equals(DateTime.MinValue)))
            throw new ArgumentException("If status is Active, Start Time can not be empty.");

        if (request.Status == Status.Active &&
            (!request.EndTime.Equals(DateTime.MinValue)))
            throw new ArgumentException("If status is Active, End Time need to be empty.");

        //Cosas por definir: si el status es Finished, el end time lo ponemos como DateTime.Now o lo debemos recibir??
        // Status = Finished
        if (request.Status == Status.Finished &&
            request.EndTime.Equals(DateTime.MinValue))
            throw new ArgumentException("If status is Finished, End Time can not be empty.");
    }

}