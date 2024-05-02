using Domain;
using IDataAccess;
using LogicInterface.Interfaces;
using System;

namespace BusinessLogic.Logics;

public class ConcreteReportFactory_RequestByMaintenanceStaff : IReportLogicByMaintenanceStaff
{
    private readonly IMaintenanceStaffRepository _maintenanceStaffRepository;
    private readonly IRequestRepository _requestRepository;
    
    public ConcreteReportFactory_RequestByMaintenanceStaff(IMaintenanceStaffRepository maintenanceStaffRepository, IRequestRepository requestRepository)
    {
        _maintenanceStaffRepository = maintenanceStaffRepository;
        _requestRepository = requestRepository;
    }

    public Report RequestByMaintenanceStaff(Guid managerId)
    {
        var maintenanceStaff = _maintenanceStaffRepository.GetAll(managerId);

        Report report = new Report();
        report.MaintenanceStaffReports = new List<MaintenanceStaffReport>();
        
        foreach (MaintenanceStaff person in maintenanceStaff)
        {
            var requests = _requestRepository.GetAllRequestStaff(person.ID).ToList();
            var actualLine = new MaintenanceStaffReport();
            actualLine.StaffName = person.Name;
            double totalTimeSpend = 0;
            
            foreach (var request in requests)
            {
                switch (request.Status)
                {
                    case Status.Finished:
                        actualLine.CompletedRequests++;
                        totalTimeSpend += (request.EndTime - request.CreationTime).TotalHours;
                        break;
                    case Status.Active:
                        actualLine.ActiveRequests++;
                        break;
                    case Status.Pending:
                        actualLine.PendingRequests++;
                        break;
                    default:
                        break;
                }    
            }

            if (actualLine.CompletedRequests == 0)
            {
                actualLine.AverageCompletionTimeInHours = 0;
            }
            else
            {
                actualLine.AverageCompletionTimeInHours = totalTimeSpend / actualLine.CompletedRequests;
            }
            report.MaintenanceStaffReports.Add(actualLine);
        }

        return report;
    }

    public Report RequestByMaintenanceStaff(Guid managerId, Guid maintenanceStaffId)
    {
        var maintenanceStaff = _maintenanceStaffRepository.GetMaintenanceStaff(managerId, maintenanceStaffId);

        Report report = new Report();
        report.MaintenanceStaffReports = new List<MaintenanceStaffReport>();

        var requests = _requestRepository.GetAllRequestStaff(maintenanceStaff.ID).ToList();
        var actualLine = new MaintenanceStaffReport();
        actualLine.StaffName = maintenanceStaff.Name;
        double totalTimeSpend = 0;

        foreach (var request in requests)
        {
            switch (request.Status)
            {
                case Status.Finished:
                    actualLine.CompletedRequests++;
                    totalTimeSpend += (request.EndTime - request.CreationTime).TotalHours;
                    break;
                case Status.Active:
                    actualLine.ActiveRequests++;
                    break;
                case Status.Pending:
                    actualLine.PendingRequests++;
                    break;
                default:
                    break;
            }
        }

        if (actualLine.CompletedRequests == 0)
        {
            actualLine.AverageCompletionTimeInHours = 0;
        }
        else
        {
            actualLine.AverageCompletionTimeInHours = totalTimeSpend / actualLine.CompletedRequests;
        }

        report.MaintenanceStaffReports.Add(actualLine);

        return report;
    }
}