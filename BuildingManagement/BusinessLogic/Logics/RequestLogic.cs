﻿using Domain;
using IDataAccess;
using LogicInterface.Interfaces;

namespace BusinessLogic.Logics;

public class RequestLogic : IRequestLogic
{
    private readonly IRequestRepository _requestRepository;
    private readonly IMaintenanceStaffRepository _maintenanceStaffRepository;
    private readonly IBuildingRepository _buildingRepository;

    public RequestLogic(IRequestRepository requestRepository, IMaintenanceStaffRepository maintenanceStaffRepository, IBuildingRepository buildingRepository)
    {
        _requestRepository = requestRepository;
        _maintenanceStaffRepository = maintenanceStaffRepository;
        _buildingRepository = buildingRepository;
    }

    public Request_ CreateRequest(Guid managerId, Request_ request)
    {
        ValidateIncomingRequest(request);

        Building actualBuilding = _buildingRepository.GetBuilding(managerId, request.Apartment.BuildingId);
        Apartment actualApartment = _buildingRepository.GetApartment(managerId, request.Apartment.ApartmentId, request.Apartment.BuildingId);

        if (actualBuilding == null)
        {
            throw new InvalidOperationException("Building does not exist.");
        }

        request.Apartment = actualApartment;

        return _requestRepository.CreateRequest(request);
    }
    
    public IEnumerable<Request_> GetAllRequest(Guid managerId)
    {
        return _requestRepository.GetAllRequest(managerId);
    }

    public IEnumerable<Request_> GetAllRequest(Guid managerId, int category)
    {
        return _requestRepository.GetAllRequest(managerId, category);
    }

    public Request_ ActivateRequest(Guid managerId, Guid requestId, Guid maintenancePersonId, DateTime startTime)
    {
        MaintenanceStaff actualMaintenanceStaff = _maintenanceStaffRepository.GetMaintenanceStaff(managerId, maintenancePersonId);
        if (actualMaintenanceStaff == null)
        {
            throw new InvalidOperationException("Maintenance staff does not exist.");
        }

        Request_ actualRequest = _requestRepository.GetRequest(managerId, requestId);

        if (actualRequest == null)
        {
            throw new InvalidOperationException("Request does not exist.");
        }

        actualRequest.MaintenanceStaff = actualMaintenanceStaff;
        actualRequest.StartTime = startTime;

        //actualMaintenanceStaff.Requests.Add(actualRequest);

        _requestRepository.Update(actualRequest);
        //_maintenanceStaffRepository.Update(actualMaintenanceStaff);

        return actualRequest;
    }

    public Request_ TerminateRequest(Guid managerId, Guid id, DateTime endTime, float totalCost)
    {
        Request_ actualRequest = _requestRepository.GetRequest(managerId, id);

        if (actualRequest == null)
            throw new InvalidOperationException("Request does not exist.");

        if(actualRequest.StartTime >  endTime)
            throw new InvalidOperationException("End time has to be greater than Start time.");

        if (actualRequest.CreationTime > endTime)
            throw new InvalidOperationException("End time has to be greater than Creation time.");

        actualRequest.Status = Status.Finished;
        actualRequest.EndTime = endTime;
        actualRequest.TotalCost = totalCost;

        _requestRepository.Update(actualRequest);

        return actualRequest;
    }

    /*no usada
    public Request_ AsignMaintenancePerson(Guid managerId, Guid requestGuid, Guid maintenancePersonId)
    {
        throw new NotImplementedException();
    }
    */

    private void ValidateIncomingRequest(Request_ request)
    {
        // General validations
        // Empty request
        if (request.StartTime.Equals(request.CreationTime) && 
            request.CreationTime.Equals(request.EndTime) && 
            request.CreationTime.Equals(DateTime.MinValue))
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

        if(request.Status == Status.Active &&
           request.MaintenanceStaff == null)
            throw new ArgumentException("If status is Active, a Maintenance Person must to be asigned.");
        //Cosas por definir: si el status es Finished, el end time lo ponemos como DateTime.Now o lo debemos recibir??
        // Status = Finished
        if (request.Status == Status.Finished &&
            request.EndTime.Equals(DateTime.MinValue))
            throw new ArgumentException("If status is Finished, End Time can not be empty.");
    }
}