using Domain;

namespace Models.Out;

public class Report_RequestByMaintenanceStaffResponse
{
    public List<MaintenanceStaffReport> MaintenanceStaffReports { get; set; }

    public Report_RequestByMaintenanceStaffResponse(Report reportRequestByMaintenanceStaff)
    {
        MaintenanceStaffReports = reportRequestByMaintenanceStaff.MaintenanceStaffReports;
    }
}