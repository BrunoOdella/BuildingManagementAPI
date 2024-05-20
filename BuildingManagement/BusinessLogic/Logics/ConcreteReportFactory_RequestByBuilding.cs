using Domain;
using IDataAccess;
using LogicInterface.Interfaces;

namespace BusinessLogic.Logics;

public class ConcreteReportFactory_RequestByBuilding : IReportLogicByBuilding
{
    private readonly IBuildingRepository _buildingRepository;
    private readonly IRequestRepository _requestRepository;

    public ConcreteReportFactory_RequestByBuilding(IBuildingRepository buildingRepository, IRequestRepository requestRepository)
    {
        _buildingRepository = buildingRepository;
        _requestRepository = requestRepository;
    }

    public Report RequestByBuilding(Guid managerId)
    {
        var buildings = _buildingRepository.GetAll(managerId).ToList();

        Report report = new Report();
        report.BuildingReports = new List<BuildingReport>();

        foreach (Building building in buildings)
        {
            var actualLine = new BuildingReport();
            actualLine.BuildingName = building.Name;

            foreach (var apartment in building.Apartments)
            {
                foreach (var request in apartment.Requests)
                {
                    switch (request.Status)
                    {
                        case Status.Finished:
                            actualLine.CompletedRequests++;
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
            }
            report.BuildingReports.Add(actualLine);
        }

        return report;
    }

    public Report RequestByBuilding(Guid managerId, Guid buildingId)
    {
        var building = _buildingRepository.GetBuilding(managerId, buildingId);

        Report report = new Report();
        report.BuildingReports = new List<BuildingReport>();

        var actualLine = new BuildingReport();
        actualLine.BuildingName = building.Name;


        foreach (var apartment in building.Apartments)
        {

            foreach (var request in apartment.Requests)
            {
                switch (request.Status)
                {
                    case Status.Finished:
                        actualLine.CompletedRequests++;
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
            report.BuildingReports.Add(actualLine);
        }

        return report;
    }
}