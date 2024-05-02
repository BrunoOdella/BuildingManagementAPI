using Domain;

namespace Models.Out;

public class Report_RequestByBuildingResponse
{
    public List<BuildingReport> MaintenanceStaffReports { get; set; }

    public Report_RequestByBuildingResponse(Report reportRequestByBuilding)
    {
        MaintenanceStaffReports = reportRequestByBuilding.BuildingReports;
    }
}